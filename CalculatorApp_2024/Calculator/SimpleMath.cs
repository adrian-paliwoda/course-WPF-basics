namespace Calculator;

public static class SimpleMath
{
    public static double Add(double x, double y)
    {
        return x + y;
    }

    public static double Subtract(double x, double y)
    {
        return x - y;
    }

    public static double Multiply(double x, double y)
    {
        return x * y;
    }

    public static double Divide(double x, double y)
    {
        if (y == 0)
        {
            throw new DivideByZeroException();
        }

        return x / y;
    }
}