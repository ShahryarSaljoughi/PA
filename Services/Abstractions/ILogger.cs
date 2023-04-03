using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface ILogger
    {
        void Log(Exception error);
        void Log(string message);
    }
}
