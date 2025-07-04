# PosVelocityDotnet
#### v0.0.1-beta

Please review `LICENSE` file before using this library.

## What is PosVelocityDotnet?
PosVelocityDotnet is a API client that provides comprehensive and convenient way to make requests with [PosVelocity API.](https://sandbox.posvelocity.com/api-docs/v2)

## Compatibility
PaymentGatewayDotnet is .NET Standard 2.0 library. You can check .NET Standard 2.0 compatibility with your version of .NET on [this](https://learn.microsoft.com/en-us/dotnet/standard/net-standard?tabs=net-standard-2-0) page

## How to get Started?

1. Install `CarrotCat.PosVelocityDotnet` nuget package via package manager or command
```shell
dotnet add package CarrotCat.PaymentGatewayDotnet
```


2. Add `IPosVelocityApiClient` service as typed or named HttpClient.

Here is an example how to add it as typed HttpClient:
```csharp
// Register Service
builder.Services.AddScoped<IPosVelocityApiClient, PosVelocityApiClient>(); // Transient, Scoped, or Singleton as needed.

// Register HttpClient with settings
builder.Services.AddHttpClient<IPosVelocityApiClient, PosVelocityApiClient>((serviceProvider, client) =>
{
    client.BaseAddress = new Uri("https://sandbox.posvelocity.com"); // or https://semi.posvelocity.com for production API
});
```


## Usage
Inject the API client to your controller or service:
``` csharp
public class Foo{
    private readonly IPosVelocityApiClient _posVelocityApiClient;

    public Foo(IPosVelocityApiClient posVelocityApiClient)
    {
        _posVelocityApiClient = posVelocityApiClient;
    }

    //ToDo: Implement rest of the class methods
}
```

You can now use it in your class:
``` csharp
var auth = new PosVelocityAuthParameters("your-api-key");
var device = new PosVelocityDeviceTarget(
    "terminal-serial",
    PosVelocityDeviceReferenceType.Serial
);
var request = new PaymentTransactionRequest(
    amount: 1.01m,
    new PaymentOptions(capture: true), 
    final: true, 
    external: "123");

PosVelocityResult<PaymentResponse> response = await _posVelocityApiClient.ProcessPaymentTransactionAsync(auth, device, request, pos: null, timeout: 60);
```


## Requests
There are some API requests that are not supported by this API client yet.  Unsupported requests are marked as `Obsolete` with specific reason provided. If you try to use those methods, you will encounter `NotImplementedException` and again the specific reason will be provided.
Currently the unsuported list is:
- GetPrintersAsync
- GetAccountTransactionsAsync
- GetPaymentReceiptAsync
- GetCustomerInputAsync

## Results
API results are wrapped in `PosVelocityResult<T>` object which is basic implementation of the *result pattern*.  Within this object you will have the `IsError
` and `IsSuccess` flags, and either `TransactionError` or response data value of type `T`

```csharp
PosVelocityResult<PaymentResponse> result = await _posVelocityClient.ProcessPaymentTransactionAsync(auth, device, request);

if (result.IsError)
{
	// do error handling
	var error = result.Error;
    _logger.LogError(error.Message);
}

// do successful result handling
PaymentResponse response = result.Value;

```

Note: This client will catch and process JSON deserialization exceptions, but not `HttpClient` exceptions. Do implement exceptions handling as reqiured by your project.

## Contributors
[Empyrean Merchant Services](https://empyreanms.com/)

[PosVelocity](https://posvelocity.com/)


## Versions and Changes

### v0.0.1
- Initial release
- Implemented most of the endpoints 

### v0.0.2
- Extended Options object in Payment Request to accommodate braking changes introduced within the same API version. Also added default values to avoid bracing changes in this library.
