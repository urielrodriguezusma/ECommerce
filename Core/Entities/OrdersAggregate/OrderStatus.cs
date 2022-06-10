using System.Runtime.Serialization;

namespace Core.Entities.OrdersAggregate
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "Paument Received")]
        PaumentReceived,
        [EnumMember(Value = "Payment Failed")]
        PaymentFailed
    }
}
