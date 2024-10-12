using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace WpfCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SelectedOperator _selectedOperator;
        private const string ResultInitialContent = "0";
        private double _lastNumber;
        private double _result;
        
        public MainWindow()
        {
            InitializeComponent();
            
            AcButton.Click += AcButtonOnClick;
            NegativeButton.Click += NegativeButtonOnClick;
            PercentButton.Click += PercentButtonOnClick;
            EqualButton.Click += EqualButtonOnClick;
        }

        private void EqualButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ResultLabel.Content.ToString(), out var newNumber))
            {
                switch (_selectedOperator)
                {
                    case SelectedOperator.Addition:
                        _result = SimplyMath.Add(_lastNumber, newNumber);   
                        break;
                    case SelectedOperator.Subtraction:
                        _result = SimplyMath.Subtraction(_lastNumber, newNumber);   
                        break;
                    case SelectedOperator.Multiplication:
                        _result = SimplyMath.Mult(_lastNumber, newNumber);   
                        break;
                    case SelectedOperator.Division:
                        _result = SimplyMath.Div(_lastNumber, newNumber);   
                        break;
                }

                ResultLabel.Content = _result.ToString(CultureInfo.InvariantCulture);
            }            
        }

        private void PercentButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ResultLabel.Content.ToString(), out var tempNumber))
            {
                tempNumber = (tempNumber / 100);
                if (_lastNumber != 0)
                {
                    tempNumber *= _lastNumber;
                }
                ResultLabel.Content = _lastNumber.ToString(CultureInfo.InvariantCulture);
            }
        }

        private void NegativeButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ResultLabel.Content.ToString(), out _lastNumber))
            {
                _lastNumber *= -1;
                ResultLabel.Content = _lastNumber.ToString(CultureInfo.InvariantCulture);
            }
        }

        private void AcButtonOnClick(object sender, RoutedEventArgs e)
        {
            ResultLabel.Content = ResultInitialContent;
            _result = 0;
            _lastNumber = 0;
        }

        private void DigitButton_OnClick(object sender, RoutedEventArgs e)
        {
            var content = (sender as Button)?.Content.ToString();
            
            if (!string.IsNullOrEmpty(content))
            {
                if (IsResultLabelEmpty())
                {
                    ResultLabel.Content = content;
                }
                else
                {
                    AddToResult(content);
                }
            }
        }

        private void AddToResult(string content)
        {
            ResultLabel.Content = string.Concat(ResultLabel.Content, content);
        }

        private bool IsResultLabelEmpty()
        {
            return ResultLabel.Content.ToString() == ResultInitialContent;
        }

        private void OperationButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ResultLabel.Content.ToString(), out _lastNumber))
            {
                ResultLabel.Content = ResultInitialContent;
            }

            if (Equals(sender, PlusButton))
            {
                _selectedOperator = SelectedOperator.Addition;
            }
            else if (Equals(sender, MinusButton))
            {
                _selectedOperator = SelectedOperator.Subtraction;
            }
            else if (Equals(sender, MultiplyButton))
            {
                _selectedOperator = SelectedOperator.Multiplication;
            }
            else if (Equals(sender, DivideButton))
            {
                _selectedOperator = SelectedOperator.Division;
            }
            
            
        }

        private void DotButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (ResultLabel is {Content: { }} && !ResultLabel.Content.ToString().Contains("."))
            {
                ResultLabel.Content = $"{ResultLabel.Content}.";
            }
        }
    }
}