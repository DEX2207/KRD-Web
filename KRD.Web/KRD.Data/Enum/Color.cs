using System.ComponentModel;

namespace KRD.Data.Enum;

public enum Color
{
    [Description("Три панда цвета AE86: черно-белый матовый, черно-красный матовый и черно-серый металик")]
    high_tech_two_tone=0,
    high_flash_two_tone=1,
    high_metal_two_tone=2,
    black=3,
    white=4,
    green=5,
    red=6,
    blue=7,
}