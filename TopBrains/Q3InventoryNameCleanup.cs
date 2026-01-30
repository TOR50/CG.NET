using System;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

public class InventoryCleanup
{
    public static void Main(string[] args)
    {
        string input = "  llapppptop    bag  ";
        string trimmed = input.Trim();
        string singleSpaced = Regex.Replace(trimmed, @"\s+", " ");
        StringBuilder deduplicated = new StringBuilder();
        if (singleSpaced.Length > 0)
        {
            deduplicated.Append(singleSpaced[0]);
            for (int i = 1; i < singleSpaced.Length; i++)
            {
                if (char.ToLower(singleSpaced[i]) != char.ToLower(singleSpaced[i - 1]))
                {
                    deduplicated.Append(singleSpaced[i]);
                }
            }
        }

        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        string result = textInfo.ToTitleCase(deduplicated.ToString().ToLower());

        Console.WriteLine(result);
    }
}