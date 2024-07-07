using System;
using Eggjam.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Input;

namespace Eggjam.Systems;

public class TestSystem : EntityProcessingSystem {
    private readonly GraphicsDevice _graphicsDevice;
    private ComponentMapper<TimeCreated> _timeCreatedMapper;
    private ComponentMapper<Transform2> _transformMapper;
    private World _world;

    public TestSystem(GraphicsDevice graphicsDevice) : base(Aspect.All(typeof(Transform2), typeof(TimeCreated))) {
        _graphicsDevice = graphicsDevice;
    }

    public override void Initialize(World world) {
        base.Initialize(world);
        _world = world;
    }

    public override void Initialize(IComponentMapperService mapperService) {
        _timeCreatedMapper = mapperService.GetMapper<TimeCreated>();
        _transformMapper = mapperService.GetMapper<Transform2>();
    }

    public override void Update(GameTime gameTime) {
        base.Update(gameTime);

        var mouseState = MouseExtended.GetState();

        if (!mouseState.IsButtonPressed(MouseButton.Left))
            return;

        var entity = _world.CreateEntity();
        entity.Attach(new Transform2(0, 0));
        entity.Attach(new Sprite(SpriteIdentifier.Egg));
        entity.Attach(new TimeCreated(gameTime.TotalGameTime));
    }

    public override void Process(GameTime gameTime, int entityId) {
        var timeCreated = _timeCreatedMapper.Get(entityId);
        var transform = _transformMapper.Get(entityId);

        var screenWidth = _graphicsDevice.Viewport.Bounds.Width - 16;
        var screenHeight = _graphicsDevice.Viewport.Bounds.Height - 16;

        var timeSinceCreation = gameTime.TotalGameTime - timeCreated.Time;

        var newX = (float)(Math.Sin(timeSinceCreation.TotalSeconds) * (screenWidth / 2.0) + screenWidth / 2.0);
        var newY = (float)(Math.Cos(timeSinceCreation.TotalSeconds * 2) * (screenHeight / 2.0) + screenHeight / 2.0);

        transform.Position = new Vector2(newX, newY);
    }
}
