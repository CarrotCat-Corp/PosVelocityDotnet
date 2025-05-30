using System.Net;
using System.Text.Json;
using PosVelocityDotnet.Model.Common;
using PosVelocityDotnet.Model.Error;

namespace PosVelocityDotnet.Utilities;

internal static class ResponseProcessor
{
    internal static async Task<PosVelocityResult<T>> ProcessCollectionHttpResponseMessageAsync<T>(
        HttpResponseMessage httpResponse)
        where T : IEnumerable<IPosVelocityApiResponse>
    {
        if (httpResponse.StatusCode == HttpStatusCode.Forbidden)
            return new TransactionError
            {
                Message = "You are not authorized. Please ensure that your configuration is correct."
            };


        var contentString = await httpResponse.Content.ReadAsStringAsync();
        contentString = ValueConverter.ConvertCodeToString(contentString);

        try
        {
            if (ResponseDeserializer.TryDeserializeTypedCollectionResponseString<T>(contentString,
                    out var typedResponseResult))
            {
                return typedResponseResult!;
            }

            if (ResponseDeserializer.TryDeserializeTypedResponse<TransactionError>(contentString,
                    out var transactionErrorResponseResult))
            {
                return transactionErrorResponseResult!;
            }
        }
        catch (ArgumentNullException e)
        {
            return new TransactionError { Message = "Received empty response" };
        }
        catch (JsonException e)
        {
            return new TransactionError { Message = "Response has invalid format" };
        }
        catch (NotSupportedException e)
        {
            return new TransactionError
                { Message = $"Response has a parameter that cannot be parsed: {e.Message}" };
        }
        catch (Exception e)
        {
            return new TransactionError { Message = e.Message };
        }

        return new TransactionError { Message = "Unknown response format" };
    }


    internal static async Task<PosVelocityResult<T>> ProcessHttpResponseMessageAsync<T>(HttpResponseMessage httpResponse)
        where T : IPosVelocityApiResponse
    {
        if (httpResponse.StatusCode == HttpStatusCode.Forbidden)
            return new TransactionError
            {
                Message = "You are not authorized. Please ensure that your configuration is correct."
            };

        var contentString = await httpResponse.Content.ReadAsStringAsync();
        contentString = ValueConverter.ConvertCodeToString(contentString);

        try
        {
            if (ResponseDeserializer.TryDeserializeTypedResponse<T>(contentString,
                    out var typedResponseResult))
            {
                return typedResponseResult!;
            }

            if (ResponseDeserializer.TryDeserializeTypedResponse<TransactionError>(contentString,
                    out var transactionErrorResponseResult))
            {
                return transactionErrorResponseResult!;
            }
        }
        catch (ArgumentNullException e)
        {
            return new TransactionError { Message = "Received empty response" };
        }
        catch (JsonException e)
        {
            return new TransactionError { Message = "Response has invalid format" };
        }
        catch (NotSupportedException e)
        {
            return new TransactionError
                { Message = $"Response has a parameter that cannot be parsed: {e.Message}" };
        }
        catch (Exception e)
        {
            return new TransactionError { Message = e.Message };
        }

        return new TransactionError { Message = "Unknown response format" };
    }

    internal static async Task<PosVelocityResult<object>> ProcessHttpResponseMessageAsync(
        HttpResponseMessage httpResponse)
    {
        if (httpResponse.StatusCode == HttpStatusCode.Forbidden)
            return new TransactionError
            {
                Message = "You are not authorized. Please ensure that your configuration is correct."
            };

        if (httpResponse.IsSuccessStatusCode)
        {
            return new object();
        }

        // if it is not a forbidden and not a success, continue to parse for error messages.
        var contentString = await httpResponse.Content.ReadAsStringAsync();
        contentString = ValueConverter.ConvertCodeToString(contentString);

        try
        {
            if (ResponseDeserializer.TryDeserializeTypedResponse<TransactionError>(contentString,
                    out var transactionErrorResponseResult))
            {
                return transactionErrorResponseResult!;
            }
        }
        catch (ArgumentNullException e)
        {
            return new TransactionError { Message = "Received empty response" };
        }
        catch (JsonException e)
        {
            return new TransactionError { Message = "Response has invalid format" };
        }
        catch (NotSupportedException e)
        {
            return new TransactionError
                { Message = $"Response has a parameter that cannot be parsed: {e.Message}" };
        }
        catch (Exception e)
        {
            return new TransactionError { Message = e.Message };
        }

        return new TransactionError { Message = "Unknown response format" };
    }
}