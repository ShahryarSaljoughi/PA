using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaDesktop.ViewModel
{
    public class NewTimeboxDialogViewModel
    {
        public int SolarYear { get; set; }
        public int ThreeMonthNo { get; set; }
        public bool IsInterim { get; set; } = false;
        public virtual DateTime? Start { get; set; }
        public virtual DateTime? End { get; set; }
    }
}
