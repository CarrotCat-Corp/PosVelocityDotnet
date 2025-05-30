using System.Text.Json.Serialization;

namespace PosVelocityDotnet.Model.ThreeDs;

public class ThreeDsRequestRecipient
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    public ThreeDsRequestRecipient(string name, string? email, string? phone)
    {
        if (string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException("At least one of email or phone must be provided.");
        Name = name;
        Email = email;
        Phone = phone;
    }
}