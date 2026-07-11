namespace Producer.Requests;

public record InitiateTransactionRequest(Guid FromUserId, Guid ToUserId);
