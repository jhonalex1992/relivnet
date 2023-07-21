using System.Runtime.Serialization;

namespace relivnet.domain.enums;

public enum BookingStatusEnum
{
    [EnumMember(Value = "NEW")]
    New,
    [EnumMember(Value = "ACCEPTED")]
    Accepted
}