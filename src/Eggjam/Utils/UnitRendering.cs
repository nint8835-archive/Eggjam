using System.Linq;
using UnitsNet;

namespace Eggjam.Utils;

public static class UnitRendering {
    public static Length GetCurrentHeight() {
        return Length.FromCentimeters(State.Instance.Height);
    }

    public static Length GetSmallestUnit(Length input) {
        if (input.Value == 0)
            return input;

        return input.QuantityInfo.UnitInfos
            .Select(info => input.ToUnit(info.Value))
            .Where(length => length.Value >= 1)
            .MinBy(length => length.Value);
    }

    public static Length AsCurrentHeightUnit(Length input) {
        var currentHeightUnit = GetSmallestUnit(GetCurrentHeight());
        return input.ToUnit(currentHeightUnit.Unit);
    }
}
