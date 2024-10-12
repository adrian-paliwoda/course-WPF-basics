using System;
using System.Windows;

namespace WpfCalculator
{
    public static class SimplyMath
    {
        public static double Add(double digitOne, double digitSecond)
        {
            return digitOne + digitSecond;
        }
        
        public static double Subtraction(double digitOne, double digitSecond)
        {
            return digitOne - digitSecond;
        }
        public static double Mult(double digitOne, double digitSecond)
        {
            return digitOne * digitSecond;
        }
        
        public static double Div(double digitOne, double digitSecond)
        {
            if (digitSecond == 0)
            {
                MessageBox.Show("Division by 0 is not allowed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                
                return 0;
            }

            return digitOne / digitSecond;
        }
    }
}