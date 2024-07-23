using System;

namespace Eggjam;

internal sealed class State {
    private static readonly Lazy<State> Lazy = new(() => new State());
    public int CrankCount = 1;

    public int Height = 0;

    public static State Instance => Lazy.Value;

    public int CrankCost => (int)Math.Ceiling(Math.Pow(1.5, CrankCount));
}
