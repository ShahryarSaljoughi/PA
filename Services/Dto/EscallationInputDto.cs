using DataModel.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dto
{
    public class EscallationInputDto : INotifyPropertyChanged
    {
        private TimeBox? baseTimeBox;
        private double coefficient;
        private DateTime? previousStatementTime;
        private DateTime? currentStatementTime;
        private DateTime? landSurrenderTime;
        private bool isCurrentStatementFinal = false;
        private string contractor = string.Empty;
        private string employer = string.Empty;
        private string projectTitle = string.Empty;
        private DateTime? contractStartDateTime;

        public TimeBox? BaseTimeBox { get => baseTimeBox; set => baseTimeBox = value; }
        public double Coefficient
        {
            get => coefficient; set
            {
                coefficient = value;
                RaisePropertyChanged();
            }
        } // 0.95 or 0.975 or 1
        public DateTime? PreviousStatementTime
        {
            get => previousStatementTime; set
            {
                previousStatementTime = value;
                RaisePropertyChanged();
            }
        }
        public DateTime? CurrentStatementTime
        {
            get => currentStatementTime; set
            {
                currentStatementTime = value;
                RaisePropertyChanged();
            }
        }
        public DateTime? LandSurrenderTime { get => landSurrenderTime; set => landSurrenderTime = value; }
        public bool IsCurrentStatementFinal
        {
            get => isCurrentStatementFinal; set
            {
                isCurrentStatementFinal = value;
                RaisePropertyChanged();
            }
        }
        public string Contractor { get => contractor; set => contractor = value; }
        public string Employer { get => employer; set => employer = value; }
        public string ProjectTitle { get => projectTitle; set => projectTitle = value; }
        public DateTime? ContractStartDateTime
        {
            get => contractStartDateTime; set
            {
                contractStartDateTime = value;
                RaisePropertyChanged();
            }
        }
        public List<PricesInputRowDto> Prices { get; } = new List<PricesInputRowDto> { };

        public event PropertyChangedEventHandler? PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
