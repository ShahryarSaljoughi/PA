using DataModel.Model;
using Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IEscallationCalculator
    {
        EscallationInputDto EscalationInputDto { get; }
        Task<Escalation> CalculateAsync();
    }
}
