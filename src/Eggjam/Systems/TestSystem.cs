using System;
using Eggjam.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;

namespace Eggjam.Systems;

public class TestSystem : EntityProcessingSystem {
    private readonly GraphicsDevice _graphicsDevice;
    private ComponentMapper<Transform2> _transformMapper;

    public TestSystem(GraphicsDevice graphicsDevice) : base(Aspect.All(typeof(Transform2))) {
        _graphicsDevice = graphicsDevice;
    }

    public override void Initialize(World world) {
        base.Initialize(world);
        var testEntity = world.CreateEntity();
        testEntity.Attach(new Sprite(SpriteIdentifier.Egg));
        testEntity.Attach(new Transform2(0, 0));
    }

    public override void Initialize(IComponentMapperService mapperService) {
        _transformMapper = mapperService.GetMapper<Transform2>();
    }

    public override void Process(GameTime gameTime, int entityId) {
        var screenWidth = _graphicsDevice.Viewport.Bounds.Width - 16;
        var screenHeight = _graphicsDevice.Viewport.Bounds.Height - 16;

        var newX = (float)(Math.Sin(gameTime.TotalGameTime.TotalSeconds) * (screenWidth / 2.0) + screenWidth / 2.0);
        var newY = (float)(Math.Cos(gameTime.TotalGameTime.TotalSeconds * 2) * (screenHeight / 2.0) + screenHeight / 2.0);

        var transform = _transformMapper.Get(entityId);
        transform.Position = new Vector2(newX, newY);
    }
}
