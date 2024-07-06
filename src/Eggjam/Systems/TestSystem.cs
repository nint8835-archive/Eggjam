using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities.Systems;

namespace Eggjam.Systems;

public class TestSystem : DrawSystem {
    private readonly GraphicsDevice _graphicsDevice;

    public TestSystem(GraphicsDevice graphicsDevice) {
        _graphicsDevice = graphicsDevice;
    }

    public override void Draw(GameTime gameTime) {
        _graphicsDevice.Clear(Color.Blue);
    }
}
