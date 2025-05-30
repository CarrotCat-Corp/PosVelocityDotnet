using System.Text.RegularExpressions;

namespace PosVelocityDotnet.Utilities;

internal static class ValueConverter
{
    /// <summary>
    /// Converts a cent amount to a decimal amount
    /// </summary>
    /// <param name="value">The nullable long value to be converted.</param>
    /// <returns>A nullable decimal value that is divided by 100 if the input is not null; otherwise, returns null.</returns>
    internal static decimal? ToDecimalAmount(long? value)
        => value.HasValue
            ? (decimal)value.Value / 100m
            : null;


    internal static DateTimeOffset? ToDateTimeOffset(long? value)
        => value.HasValue
            ? DateTimeOffset.FromUnixTimeMilliseconds(value.Value)
            : null;

    internal static string FixJsonString(string value)
        => value
            .Replace("\"", "\\\"");

    internal static int ToIntAmount(decimal amount)
        => (int)(Math.Round(amount, 2, MidpointRounding.AwayFromZero) * 100);


    /// <summary>
    /// Some of the code parameters in error responses are integers. This function replaces them with strings.
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    internal static string ConvertCodeToString(string json)
    {
        // This regex pattern looks for "code": followed by a number
        // (?<="code":)\s*\d+ - positive lookbehind for "code":, then optional whitespace, then one or more digits
        const string pattern = """(?<="code":)\s*(\d+)""";

        // If the pattern is found, replace the number with a quoted string version
        return Regex.Replace(json, pattern, match => $" \"{match.Groups[1].Value}\"");
    }


}