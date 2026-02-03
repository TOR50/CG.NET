using System;

public class Solution
{
    public static string EvaluateExpression(string expression)
    {
        if (string.IsNullOrWhiteSpace(expression))
            return "Error:InvalidExpression";

        string[] parts = expression.Split(' ');
        if (parts.Length != 3)
            return "Error:InvalidExpression";

        string aStr = parts[0];
        string op = parts[1];
        string bStr = parts[2];

        if (op != "+" && op != "-" && op != "*" && op != "/")
            return "Error:UnknownOperator";

        if (!int.TryParse(aStr, out int a) || !int.TryParse(bStr, out int b))
            return "Error:InvalidNumber";

        if (op == "/" && b == 0)
            return "Error:DivideByZero";

        int result = 0;
        switch (op)
        {
            case "+": result = a + b; break;
            case "-": result = a - b; break;
            case "*": result = a * b; break;
            case "/": result = a / b; break;
        }

        return result.ToString();
    }
}