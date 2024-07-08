using Eggjam.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Input;

namespace Eggjam.Systems;

public class PlayerSystem : EntityProcessingSystem {
    private ComponentMapper<Player> _playerMapper;
    private ComponentMapper<Transform2> _transformMapper;

    public PlayerSystem() : base(Aspect.All(typeof(Transform2), typeof(Player))) { }

    public override void Initialize(World world) {
        base.Initialize(world);

        var playerEntity = world.CreateEntity();
        playerEntity.Attach(new Transform2 { Position = new Vector2(100, 100) });
        playerEntity.Attach(new Player());
        playerEntity.Attach(new Sprite(SpriteIdentifier.Egg));
    }

    public override void Initialize(IComponentMapperService mapperService) {
        _transformMapper = mapperService.GetMapper<Transform2>();
        _playerMapper = mapperService.GetMapper<Player>();
    }

    public override void Process(GameTime gameTime, int entityId) {
        var transform = _transformMapper.Get(entityId);

        var keyboardState = KeyboardExtended.GetState();

        var movement = Vector2.Zero;

        if (keyboardState.IsKeyDown(Keys.W))
            movement.Y -= 1;
        if (keyboardState.IsKeyDown(Keys.S))
            movement.Y += 1;
        if (keyboardState.IsKeyDown(Keys.A))
            movement.X -= 1;
        if (keyboardState.IsKeyDown(Keys.D))
            movement.X += 1;

        transform.Position += movement * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
}
