namespace PosVelocityDotnet.Tests.Helpers;

public static class AssertExtensions
{
    public static void NotNullOrWhiteSpace(string? value, string? message = null)
    {
        Assert.False(string.IsNullOrWhiteSpace(value), message ?? "String should not be empty or whitespace");
    }
}