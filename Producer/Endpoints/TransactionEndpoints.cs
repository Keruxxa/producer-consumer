using Producer.Requests;

namespace Producer.Endpoints;

public static class TransactionEndpoints
{
    public static void MapTransactionEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder
            .MapGroup("/api/transactions")
            .WithTags("Transactions");

        group.MapPost("/initiate", InitiateTransaction);
    }

    private static async Task InitiateTransaction(InitiateTransactionRequest request, CancellationToken cancellationToken)
    {
        await Task.Delay(1, cancellationToken);
    }
}
