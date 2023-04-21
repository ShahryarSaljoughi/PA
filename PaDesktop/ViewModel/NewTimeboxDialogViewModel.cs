using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaDesktop.ViewModel
{
    public class NewTimeboxDialogViewModel : ValidatableViewModelBase
    {
        private int solarYear = default;
        private int threeMonthNo;
        private DateTime? start;
        private DateTime? end;

        public int SolarYear
        {
            get => solarYear; 
            set
            {
                solarYear =  value;
                CheckModelValidity();
            }
        }
        public int ThreeMonthNo
        {
            get => threeMonthNo; set
            {
                threeMonthNo = value;
                CheckModelValidity();
            }
        }
        public bool IsInterim { get; set; } = false;
        public virtual DateTime? Start { get => start; set => start = value; }
        public virtual DateTime? End { get => end; set => end = value; }
        private bool _isFormValid;
        public bool IsFormValid
        {
            get
            {
                return _isFormValid;
            }
            set
            {
                _isFormValid = value;
                OnPropertyChanged(nameof(IsFormValid));
            }
        }

        protected sealed override bool CheckModelValidity()
        {
            var isValid = SolarYear > 1300 && new[] { 1, 2, 3, 4 }.Contains(ThreeMonthNo);
            if (!isValid) { AddError("form data is not valid!", nameof(SolarYear)); }
            if (isValid) { ClearErrors(); }
            IsFormValid = isValid;
            return isValid;
        }


    }
}
