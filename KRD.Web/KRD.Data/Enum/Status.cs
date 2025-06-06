using System.ComponentModel;

namespace KRD.Data.Enum;

public enum Status
{
    [Description("Получен")]
    received=0,
    [Description("Ожидает подтверждения")]
    awaiting_confirmation=1,
    [Description("Закрыт")]
    closed=2,
}