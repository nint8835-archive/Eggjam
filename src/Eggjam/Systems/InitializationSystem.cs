using System;
using Eggjam.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;

namespace Eggjam.Systems;

public class InitializationSystem : ISystem {
    private readonly GraphicsDevice _graphicsDevice;

    public InitializationSystem(GraphicsDevice graphicsDevice) {
        _graphicsDevice = graphicsDevice;
    }

    public void Initialize(World world) {
        var egg = world.CreateEntity();

        var eggSize = new Vector2(128, 128);
        var eggPosition = new Vector2(
            _graphicsDevice.Viewport.Width / 2f,
            _graphicsDevice.Viewport.Height / 2f
        );

        egg.Attach(
            new Transform2 {
                Position = eggPosition,
                Scale = eggSize
            }
        );
        egg.Attach(new Sprite(SpriteIdentifier.Egg));
        egg.Attach(
            new Clickable(
                eggSize,
                eggSize * 1.1f,
                eggSize * 0.9f,
                (_, _) => { State.Instance.Height++; }
            )
        );
    }

    public void Dispose() {
        GC.SuppressFinalize(this);
    }
}
