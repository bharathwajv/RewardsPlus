namespace RewardsPlus.Application.Payment;
public interface IPaymentGateway
{
    Task<bool> Sale(PayRequest request);
}