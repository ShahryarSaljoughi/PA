using DataModel.Model;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;
using Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services
{
    public class EscallationCalculator : IEscallationCalculator
    {
        private IIndexService IndexService { get; set; }
        private EscallationInputDto escallationInputDto;
        private Escalation Escalation { get; set; }
        public EscallationInputDto EscalationInputDto
        {
            get { return escallationInputDto; }
            private set { escallationInputDto = value; }
        }
        public PaDbContext Db { get; set; }
        public EscallationCalculator(PaDbContext db, IIndexService indexService)
        {
            Db = db;
            IndexService = indexService;
            escallationInputDto = new EscallationInputDto();
        }
        public async Task<Escalation> CalculateAsync()
        {
            MapEscallationDtoToEntity();

            var timeboxes = await GetWorkingTimeBoxesAsync();
            foreach (var item in Escalation.Items)
            {
                var baseIndex = await IndexService.GetIndexAsync(item.Subfield.Id, Escalation.BaseTimeBox.Id);
                item.BaseIndex = baseIndex;
                foreach (var timebox in timeboxes)
                {
                    var workingIndex = await IndexService.GetIndexAsync(item.Subfield.Id, timebox.Id);
                    var escalationCoefficient = ((workingIndex / baseIndex) - 1) * Escalation.Coefficient;
                    var row = new EscalationItemRow(item)
                    {
                        WorkingTimeBox = timebox,
                        WorkingTimeBoxIndex = workingIndex,
                        EscalationCoefficient = escalationCoefficient,
                    };
                    row.EscalationPrice =
                        (decimal)escalationCoefficient * item.PriceDifference * (decimal)row.WorkingProportion;
                    item.Rows.Add(row);
                }
            }
            Escalation.IsCalculated = true;
            Db.Add(Escalation);
            await Db.SaveChangesAsync();
            return Escalation;

        }

        private async Task<List<TimeBox>> GetWorkingTimeBoxesAsync()
        {
            var result = await Db.TimeBoxes
                .Where(timebox => timebox.End >= Escalation.PreviousStatementTime && timebox.Start <= Escalation.CurrentStatementTime)
                .ToListAsync();

            return result;
        }

        void MapEscallationDtoToEntity()
        {
            Escalation = new Escalation()
            {
                Id = Guid.NewGuid(),
                BaseTimeBox = escallationInputDto.BaseTimeBox,
                Coefficient = escallationInputDto.Coefficient,
                CurrentStatementTime = escallationInputDto.CurrentStatementTime,
                PreviousStatementTime = escallationInputDto.PreviousStatementTime
            };
            var items = EscalationInputDto.Prices.Select(d => new EscalationItem(Escalation)
            {
                CurrentPrice = d.CurrentPrice,
                PreviousPrice = d.PreviousPrice,
                Subfield = d.Subfield,
                Rows = new List<EscalationItemRow>(),
                EscalationId = Escalation.Id
            }).ToList();
            Escalation.Items.AddRange(items);
        }
    }
}
