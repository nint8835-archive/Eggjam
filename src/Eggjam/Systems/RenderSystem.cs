using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Aseprite;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using Sprite = Eggjam.Components.Sprite;

namespace Eggjam.Systems;

public class RenderSystem : EntityDrawSystem {
    private readonly GraphicsDevice _graphicsDevice;
    private readonly SpriteBatch _spriteBatch;
    private readonly TextureAtlas _sprites;
    private ComponentMapper<Sprite> _spriteMapper;
    private ComponentMapper<Transform2> _transformMapper;

    public RenderSystem(GraphicsDevice graphicsDevice, TextureAtlas sprites) : base(Aspect.All(typeof(Sprite))) {
        _graphicsDevice = graphicsDevice;
        _spriteBatch = new SpriteBatch(graphicsDevice);
        _sprites = sprites;
    }

    public override void Initialize(IComponentMapperService mapperService) {
        _spriteMapper = mapperService.GetMapper<Sprite>();
        _transformMapper = mapperService.GetMapper<Transform2>();
    }

    public override void Draw(GameTime gameTime) {
        _graphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin();

        foreach (var entityId in ActiveEntities) {
            var sprite = _spriteMapper.Get(entityId);
            var transform = _transformMapper.Get(entityId);

            // TODO: Don't create a new sprite texture every frame
            var spriteTexture = _sprites.CreateSprite((int)sprite.Identifier);
            var spritePosition = transform.Position;

            _spriteBatch.Draw(spriteTexture, spritePosition);
        }

        _spriteBatch.End();
    }
}
