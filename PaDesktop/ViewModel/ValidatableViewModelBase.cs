using PaDesktop.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace PaDesktop.ViewModel
{
    public class ValidatableViewModelBase : ViewModelBase, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> ErrorsByPropertyName = new();
        public bool HasErrors => ErrorsByPropertyName.Any();
        public bool IsFormValid { get; set; } = false;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            if (propertyName is null) return Enumerable.Empty<string>();
            if (!ErrorsByPropertyName.Any()) return Enumerable.Empty<string>();
            if (!ErrorsByPropertyName.ContainsKey(propertyName)) return Enumerable.Empty<string>();
            return ErrorsByPropertyName[propertyName];
        }

        protected virtual void AddError(string error, [CallerMemberName] string? propertyName = null)
        {
            if (propertyName is null) return;

            if (!ErrorsByPropertyName.ContainsKey(propertyName))
            {
                ErrorsByPropertyName[propertyName] = new List<string>();
            }
            if (!ErrorsByPropertyName[propertyName].Contains(error))
            {
                ErrorsByPropertyName[propertyName].Add(error);
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
                OnPropertyChanged(nameof(HasErrors));
                OnPropertyChanged(nameof(IsFormValid));
            }
        }

        protected virtual void ClearErrors([CallerMemberName] string? propertyName = null)
        {
            if (propertyName is null) { ErrorsByPropertyName.Clear(); }

            if (ErrorsByPropertyName.ContainsKey(propertyName))
            {
                ErrorsByPropertyName.Remove(propertyName);
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
                
            }
            OnPropertyChanged(nameof(HasErrors));
            OnPropertyChanged(nameof(IsFormValid));
        }

        protected virtual bool CheckModelValidity() { return true; }
    }
}
