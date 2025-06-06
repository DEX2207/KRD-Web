using System.ComponentModel;

namespace KRD.Data.Enum;

public enum OptionType
{
    [Description("Рестайлинг")]
    facelift=0,
    [Description("ГУР")]
    hps=1,
    [Description("Регулировка положения руля")]
    swpa=2,
    [Description("Антиблокировочкая система")]
    abs=3,
    [Description("Противобуксовочная система")]
    esp=4,
    [Description("Подогрев сидений")]
    heated_seats=5,
    [Description("Электроподъем стекол")]
    electric_windows=6,
    [Description("Литые диски")]
    alloy_wheels=7
}