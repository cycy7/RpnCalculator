using System;
using System.ComponentModel;
using System.Windows.Input;
using RpnCalculatorDomain;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace RpnCalculatorUI
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public string Title => "RPN Calculator";
        private string _input;
        private string _result;

        public string Input
        {
            get => _input;
            set
            {
                _input = value;
                OnPropertyChanged(nameof(Input));
            }
        }

        public string Result
        {
            get => _result;
            set
            {
                _result = value;
                OnPropertyChanged(nameof(Result));
            }
        }

        public ICommand EvaluateCommand { get; }

        public MainWindowViewModel()
        {
            EvaluateCommand = new RelayCommand(Evaluate);
        }

        private void Evaluate()
        {
            try
            {
                var rpnCalculator = new RpnCalculator(); 
                decimal resultValue = rpnCalculator.Process(Input);
                Result = resultValue.ToString();
            }
            catch (RpnCalculatorException ex)
            {
                Result = $"{ex.Message}";
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

  
}
