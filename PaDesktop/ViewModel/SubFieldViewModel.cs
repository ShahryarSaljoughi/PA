using DataModel.Model;
using PaDesktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaDesktop.ViewModel
{
    public class SubFieldViewModel : ViewModelBase
    {
        private string number;
        private string field;
        private string title;

        private Subfield subfield { get; set; }
        public SubFieldViewModel(Subfield subfield)
        {
            this.subfield = subfield;
        }
        public string Number
        {
            get => number; set
            {
                number = value;
                OnPropertyChanged();
            }
        }
        public string Field
        {
            get => field; set
            {
                field = value;
                OnPropertyChanged();
            }
        }
        public string Title
        {
            get => title; set
            {
                title = value;
                OnPropertyChanged();
            }
        }
    }
}
