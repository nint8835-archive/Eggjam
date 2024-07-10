using System;

namespace Eggjam;

internal sealed class State {
    private static readonly Lazy<State> Lazy = new(() => new State());

    public int Height = 0;
    public static State Instance => Lazy.Value;
}
