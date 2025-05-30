using System.Text.Json;
using PosVelocityDotnet.Model.Common;

namespace PosVelocityDotnet.Utilities;

public static class ResponseDeserializer
{
    public static bool TryDeserializeTypedResponse<T>(string contentString, out T? response)
        where T : IPosVelocityApiResponse
    {

        var sanitizedJsonString = JsonSanitizer.SanitizeNestedJsonStrings(contentString);
        var result = JsonSerializer.Deserialize<T>(sanitizedJsonString);
        if (result?.IsValid == true)
        {
            response = result;
            return true;
        }

        //if JSON was successfully parsed, but it is not T
        response = default ;
        return false;
    }

    public static bool TryDeserializeTypedCollectionResponseString<T>(string contentString, out T? response)
        where T : IEnumerable<IPosVelocityApiResponse>
    {
        var sanitizedJsonString = JsonSanitizer.SanitizeNestedJsonStrings(contentString);
        var result = JsonSerializer.Deserialize<T>(sanitizedJsonString);
        if (result is not null)
        {
            response = result;
            return true;
        }

        //if JSON was successfully parsed, but it is not T
        response = default ;
        return false;
    }
}