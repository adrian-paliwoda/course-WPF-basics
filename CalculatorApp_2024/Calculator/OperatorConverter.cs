namespace Calculator;

public static class OperatorConverter
{
    public static SelectedOperator? ToSelectedOperator(string? character)
    {
        return character switch
        {
            "+" => SelectedOperator.Addition,
            "-" => SelectedOperator.Subtraction,
            "*" => SelectedOperator.Multiplication,
            "/" => SelectedOperator.Division,
            _ => null
        };
    }

    public static bool IsOperator(char? character)
    {
        return character switch
        {
            '+' or '-' or '*' or '/' or '%' => true,
            _ => false
        };
    }
}