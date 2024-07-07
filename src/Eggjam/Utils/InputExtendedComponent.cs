using Microsoft.Xna.Framework;
using MonoGame.Extended.Input;

namespace Eggjam.Utils;

internal class InputExtendedComponent : GameComponent {
    public InputExtendedComponent(Game game) : base(game) { }

    public override void Update(GameTime gameTime) {
        MouseExtended.Refresh();
        KeyboardExtended.Refresh();
    }
}
