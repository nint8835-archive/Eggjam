using Eggjam.Components;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Input;

namespace Eggjam.Systems;

public class InputSystem : EntityProcessingSystem {
    private ComponentMapper<Clickable> _clickableMapper;
    private ComponentMapper<Transform2> _transformMapper;
    private World _world;

    public InputSystem() : base(Aspect.All(typeof(Clickable), typeof(Transform2))) { }

    public override void Initialize(World world) {
        base.Initialize(world);
        _world = world;
    }

    public override void Initialize(IComponentMapperService mapperService) {
        _clickableMapper = mapperService.GetMapper<Clickable>();
        _transformMapper = mapperService.GetMapper<Transform2>();
    }

    public override void Process(GameTime gameTime, int entityId) {
        var clickable = _clickableMapper.Get(entityId);
        var transform = _transformMapper.Get(entityId);

        var bounds = new Rectangle(
            (transform.Position - transform.Scale / 2).ToPoint(),
            transform.Scale.ToPoint()
        );
        var mouseState = MouseExtended.GetState();

        if (bounds.Contains(mouseState.Position)) {
            if (mouseState.IsButtonDown(MouseButton.Left)) {
                transform.Scale = clickable.ClickSize;
                if (mouseState.IsButtonPressed(MouseButton.Left))
                    clickable.OnClick(_world, entityId);
            }
            else {
                transform.Scale = clickable.HoverSize;
            }
        }
        else {
            transform.Scale = clickable.DefaultSize;
        }
    }
}
