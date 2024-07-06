using System.Diagnostics.CodeAnalysis;
using Eggjam.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Entities;

namespace Eggjam;

public class EggjamGame : Game {
    [SuppressMessage(
        "ReSharper",
        "NotAccessedField.Local",
        Justification =
            "A GraphicsDeviceManager must be initialized to not encounter a 'no graphics device service' error. Storing here to prevent it from being garbage collected."
    )]
    private GraphicsDeviceManager _graphics;

    public EggjamGame() {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize() {
        var world = new WorldBuilder()
            .AddSystem(new TestSystem(GraphicsDevice))
            .Build();

        Components.Add(world);
    }

    protected override void Update(GameTime gameTime) {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        base.Update(gameTime);
    }
}
