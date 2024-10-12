using System.Globalization;

namespace Calculator;

public partial class MainPage : ContentPage
{
    private double lastNumber;
    private double result;
    private SelectedOperator? selectedOperator;

    public MainPage()
    {
        InitializeComponent();

        acButton.Clicked += AcButton_Click;
        negativeButton.Clicked += NegativeButton_Click;
        percentageButton.Clicked += PercentageButton_Click;
        equalButton.Clicked += EqualButton_Click;
    }

    private async void EqualButton_Click(object? sender, EventArgs e)
    {
        if (double.TryParse(ResultLabel.Text, new CultureInfo("en-US"), out var newNumber))
        {
            switch (selectedOperator)
            {
                case SelectedOperator.Addition:
                    result = SimpleMath.Add(lastNumber, newNumber);
                    break;
                case SelectedOperator.Subtraction:
                    result = SimpleMath.Subtract(lastNumber, newNumber);
                    break;
                case SelectedOperator.Multiplication:
                    result = SimpleMath.Multiply(lastNumber, newNumber);
                    break;
                case SelectedOperator.Division:
                    try
                    {
                        result = SimpleMath.Divide(lastNumber, newNumber);
                    }
                    catch (DivideByZeroException)
                    {
                        result = 0;
                        var message = "Cannot divide by zero";
                        await DisplayAlert("Error", message, "OK", FlowDirection.LeftToRight);
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            lastNumber = result;
            ResultLabel.Text = result.ToString();
        }
    }


    private void PointButton_OnClicked(object? sender, EventArgs e)
    {
        if (!ResultLabel.Text.Contains(',') && !ResultLabel.Text.Contains('.') &&
            !ResultLabel.Text.Contains(CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator))
        {
            ResultLabel.Text = $"{ResultLabel.Text}.";
        }
    }

    private void PercentageButton_Click(object? sender, EventArgs e)
    {
        if (double.TryParse(ResultLabel.Text, new CultureInfo("en-US"), out var parsedNumber))
        {
            parsedNumber /= 100;
            if (lastNumber != 0)
            {
                parsedNumber *= lastNumber;
            }

            ResultLabel.Text = parsedNumber.ToString(CultureInfo.InvariantCulture);
        }
    }

    private void NegativeButton_Click(object? sender, EventArgs e)
    {
        if (double.TryParse(ResultLabel.Text, new CultureInfo("en-US"), out result))
        {
            lastNumber *= -1;
            ResultLabel.Text = lastNumber.ToString(CultureInfo.InvariantCulture);
        }
    }

    private void AcButton_Click(object? sender, EventArgs e)
    {
        ResultLabel.Text = "0";
        result = 0;
        lastNumber = 0;
    }

    private void NumberButton_OnClicked(object? sender, EventArgs e)
    {
        if (sender is Button button &&
            int.TryParse(button.CommandParameter?.ToString(), new CultureInfo("en-US"), out var number))
        {
            if (ResultLabel.Text == "0")
            {
                ResultLabel.Text = number.ToString();
            }
            else
            {
                ResultLabel.Text = $"{ResultLabel.Text}{number}";
            }
        }
    }

    private void OperatorButton_OnClicked(object? sender, EventArgs e)
    {
        if (double.TryParse(ResultLabel.Text, new CultureInfo("en-US"), out lastNumber))
        {
            ResultLabel.Text = "0";
        }

        if (sender is Button { CommandParameter: not null } button &&
            OperatorConverter.IsOperator(button.CommandParameter?.ToString()?[0]))
        {
            selectedOperator = OperatorConverter.ToSelectedOperator(button.CommandParameter?.ToString());
            if (selectedOperator != null)
            {
                ResultLabel.Text = "0";
            }
        }
    }
    
}