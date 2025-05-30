using System.Text.RegularExpressions;

namespace PosVelocityDotnet.Utilities;

internal static class JsonSanitizer
{
    /// <summary>
    /// Detect patterns like: "card":"{\"BanknetData\":\"0529MCC902519\"}",
    /// And replace with: "card":{"BanknetData":"0529MCC902519"}
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    internal static string SanitizeNestedJsonStrings(string json)
    {
        // Detect patterns like: "card":"{\"BanknetData\":\"0529MCC902519\"}"
        // And replace with: "card":{"BanknetData":"0529MCC902519"}

        // This regex finds property values that are JSON strings (enclosed in quotes and starting with { or [)
        var regex = new Regex("\"(\\w+)\"\\s*:\\s*\"\\{(.+?)\\}\"", RegexOptions.Compiled);

        return regex.Replace(json, match =>
        {
            var propertyName = match.Groups[1].Value;
            var jsonContent = match.Groups[2].Value.Replace("\\\"", "\"");
            return $"\"{propertyName}\":{{{jsonContent}}}";
        });
    }
}