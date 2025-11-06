using System;
using System.Text.RegularExpressions;

public class CprValidator
{
    // Regex pattern: Matches DD (01-31), MM (01-12), YY (00-99), hyphen, SSSS (0000-9999)
    private static readonly Regex CprRegex = new Regex(
        @"^(?<day>0[1-9]|[12]\d|3[01])(?<month>0[1-9]|1[0-2])(?<year>\d{2})-(?<sequence>\d{4})$",
        RegexOptions.Compiled | RegexOptions.CultureInvariant
    );

    public static bool IsValidCpr(string cpr)
    {
        if (string.IsNullOrWhiteSpace(cpr) || cpr.Length != 11)
            return false;

        var match = CprRegex.Match(cpr);
        if (!match.Success)
            return false;

        // Extract groups for further validation (optional: full date check)
        int day = int.Parse(match.Groups["day"].Value);
        int month = int.Parse(match.Groups["month"].Value);
        int year = int.Parse("19" + match.Groups["year"].Value);  // Assume 1900s; adjust for 2000s if needed (e.g., YY >= 40 for 2000+)

        try
        {
            // Basic date validation (checks leap years and days in month)
            DateTime.ParseExact($"{day:D2}{month:D2}{year:D4}", "ddMMyyyy", null);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}