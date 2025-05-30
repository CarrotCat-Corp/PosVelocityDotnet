using PosVelocityDotnet.Model.ThreeDs;
using PosVelocityDotnet.Model.TransactionCredit;
using PosVelocityDotnet.Model.TransactionPayment;
using PosVelocityDotnet.Tests.Helpers;
using PosVelocityDotnet.Utilities;

namespace PosVelocityDotnet.Tests.UnitTests;

public sealed class ResponseDeserializerUnitTests
{
    [Fact]
    public async Task TryDeserializeTypedResponseString_CreditResponse_ReturnsValidResponse()
    {
        // Arrange
        var jsonString = """
                         {
                           "credit" : {
                             "amount" : 100,
                             "cardTransaction" : {
                               "authCode" : "OK9688",
                               "cardType" : "MC",
                               "entryType" : "EMV_CONTACT",
                               "extra" : {
                                 "applicationLabel" : "4D415354455243415244",
                                 "authorizingNetworkName" : "MASTERCARD",
                                 "cvmResult" : "NO_CVM_REQUIRED",
                                 "applicationIdentifier" : "A0000000041010",
                                 "card" : "{\"BanknetData\":\"0529MCC274211\"}"
                               },
                               "last4" : "4111",
                               "referenceId" : "514900000163",
                               "state" : "CLOSED",
                               "type" : "NAKEDREFUND"
                             },
                             "createdTime" : 1748544877904,
                             "employee" : { },
                             "id" : "1A9BJ3Y7DT3GW",
                             "taxAmount" : 0
                           }
                         }
                         """;


        // Act

        var isSuccessful = ResponseDeserializer.TryDeserializeTypedResponse<CreditResponse>(
            jsonString,
            out var result
        );

        // Assert
        Assert.True(isSuccessful);
        Assert.NotNull(result?.Credit);

        var transactionResult = result.Credit;
        AssertExtensions.NotNullOrWhiteSpace(transactionResult.Id);
        Assert.NotNull(transactionResult.CreatedTime);
        Assert.NotNull(transactionResult.TaxAmount);
        AssertExtensions.NotNullOrWhiteSpace(transactionResult.CardTransaction?.AuthCode);
        // Assert.NotNull(transactionResult?.Employee); // No need to test for employee at this moment in time.
        Assert.Equal(1m, transactionResult.Amount);


        var cardTransaction = transactionResult.CardTransaction;
        Assert.NotNull(cardTransaction);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.AuthCode);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.CardType);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.EntryType);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.Last4);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.ReferenceId);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.State);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.Type);

        var cardTransactionExtra = cardTransaction.Extra;
        Assert.NotNull(cardTransactionExtra);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtra.ApplicationLabel);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtra.AuthorizingNetworkName);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtra.CvmResult);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtra.ApplicationIdentifier);

        var cardTransactionExtraCard = cardTransactionExtra.Card;
        Assert.NotNull(cardTransactionExtraCard);
        // AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCard.Aci);
    }


    [Fact]
    public async Task TryDeserializeTypedResponseString_PaymentResponse_ReturnsValidResponse()
    {
        // Arrange
        var jsonString = """
                         {
                           "payment" : {
                             "amount" : 101,
                             "attributes" : {
                               "source_type" : "card_present",
                               "is_quick_chip" : "false",
                               "tip_source" : "disabled",
                               "payment_app_version" : "1.0-629-prod",
                               "create_auth" : "null",
                               "source_ip" : "333.333.333.333"
                             },
                             "cardTransaction" : {
                               "authCode" : "OK2728",
                               "cardType" : "VISA",
                               "entryType" : "EMV_CONTACTLESS",
                               "extra" : {
                                 "applicationLabel" : "5649534120435245444954",
                                 "authorizingNetworkName" : "VISA",
                                 "cvmResult" : "NO_CVM_REQUIRED",
                                 "applicationIdentifier" : "A0000000031010",
                                 "card" : "{\"TransID\":\"015149233303735G087\",\"ACI\":\"E\"}"
                               },
                               "first6" : "452084",
                               "last4" : "1111",
                               "referenceId" : "514900502070",
                               "state" : "CLOSED",
                               "token" : "1945152211457085",
                               "transactionNo" : "100055",
                               "type" : "AUTH",
                               "vaultedCard" : {
                                 "expirationDate" : "0626",
                                 "first6" : "452084",
                                 "last4" : "1111",
                                 "token" : "1945152211457085"
                               }
                             },
                             "createdTime" : 1748545672630,
                             "employee" : {
                               "id" : "DFLTEMPLOYEE"
                             },
                             "externalPaymentId" : "432c29d5af474c08a6a08db3d6b1698e",
                             "id" : "2BRTK9PFEDEPE",
                             "offline" : false,
                             "order" : {
                               "id" : "V6SA5297FEQ4C"
                             },
                             "result" : "SUCCESS",
                             "taxAmount" : 0,
                             "tender" : {
                               "id" : "78Z018QR7RPQC",
                               "label" : "Credit Card",
                               "labelKey" : "com.clover.tender.credit_card",
                               "opensCashDrawer" : false
                             },
                             "tipAmount" : 0
                           }
                         }
                         """;


        // Act

        var isSuccessful = ResponseDeserializer.TryDeserializeTypedResponse<PaymentResponse>(
            jsonString,
            out var result
        );


// Assert
        Assert.True(isSuccessful);
        var transactionResult = result?.Payment;
        Assert.NotNull(transactionResult);

        Assert.Equal(1.01m, transactionResult.Amount);
        Assert.NotNull(transactionResult.CreatedTime);
        AssertExtensions.NotNullOrWhiteSpace(transactionResult.Id);
        Assert.NotNull(transactionResult.Offline);
        Assert.Equal("SUCCESS", transactionResult.Result?.ToUpper());
        Assert.NotNull(transactionResult.TaxAmount);

        var attributes = transactionResult.Attributes;
        Assert.NotNull(attributes);
        AssertExtensions.NotNullOrWhiteSpace(attributes.SourceType);
        AssertExtensions.NotNullOrWhiteSpace(attributes.IsQuickChip);
        AssertExtensions.NotNullOrWhiteSpace(attributes.TipSource);
        AssertExtensions.NotNullOrWhiteSpace(attributes.PaymentAppVersion);
        AssertExtensions.NotNullOrWhiteSpace(attributes.CreateAuth);
        AssertExtensions.NotNullOrWhiteSpace(attributes.SourceIp);

        var cardTransaction = transactionResult.CardTransaction;
        Assert.NotNull(cardTransaction);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.AuthCode);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.CardType);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.EntryType);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.First6);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.Last4);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.ReferenceId);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.State);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.Token);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.TransactionNo);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.Type);

        var cardTransactionExtra = cardTransaction.Extra;
        Assert.NotNull(cardTransactionExtra);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtra.ApplicationLabel);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtra.AuthorizingNetworkName);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtra.CvmResult);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtra.ApplicationIdentifier);

        var cardTransactionExtraCard = cardTransactionExtra.Card;
        Assert.NotNull(cardTransactionExtraCard);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCard.TransID);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCard.Aci);

        var vaultedCard = cardTransaction.VaultedCard;
        Assert.NotNull(vaultedCard);
        AssertExtensions.NotNullOrWhiteSpace(vaultedCard.ExpirationDate);
        AssertExtensions.NotNullOrWhiteSpace(vaultedCard.First6);
        AssertExtensions.NotNullOrWhiteSpace(vaultedCard.Last4);
        AssertExtensions.NotNullOrWhiteSpace(vaultedCard.Token);

        var employee = transactionResult.Employee;
        Assert.NotNull(employee);
        AssertExtensions.NotNullOrWhiteSpace(employee.Id);

        var order = transactionResult.Order;
        Assert.NotNull(order);
        AssertExtensions.NotNullOrWhiteSpace(order.Id);

        var tender = transactionResult.Tender;
        Assert.NotNull(tender);
        AssertExtensions.NotNullOrWhiteSpace(tender.Id);
        AssertExtensions.NotNullOrWhiteSpace(tender.Label);
        AssertExtensions.NotNullOrWhiteSpace(tender.LabelKey);
        Assert.NotNull(tender.OpensCashDrawer);
    }

    [Fact]
    public async Task TryDeserializeTypedResponseString_ThreeDsResponse_ReturnsValidResponse()
    {
        // Arrange
        var jsonString = """
                         {
                           "date" : "2025-05-29T22:35:09.714Z",
                           "notes" : "Test request",
                           "email" : "oleg@empyreanms.com",
                           "phone" : null,
                           "amount" : 1.01,
                           "callback" : null,
                           "reference" : "1234567890",
                           "status" : "pending",
                           "url" : "http://localhost:8193/stx/1818005d350f2e256ce62ec035b8b76838e11dd5378fd9ef2b27c0",
                           "id" : "6838e11dd5378fd9ef2b27c0"
                         }
                         """;

        // Act
        var isSuccessful = ResponseDeserializer.TryDeserializeTypedResponse<ThreeDsResponse>(
            jsonString,
            out var result
        );

        // Assert
        Assert.True(isSuccessful);
        Assert.NotNull(result);
        Assert.NotNull(result.Id);
        Assert.NotNull(result.Date);
        Assert.NotNull(result.Amount);
        Assert.True(result.Email is not null || result.Phone is not null);
        Assert.NotNull(result.Url);
        Assert.NotNull(result.Status);
    }
}