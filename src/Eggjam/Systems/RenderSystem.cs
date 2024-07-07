using System;
using System.Collections.Generic;
using Eggjam.Components;
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
    private readonly Dictionary<SpriteIdentifier, MonoGame.Aseprite.Sprite> _spriteCache;
    private readonly TextureAtlas _sprites;
    private ComponentMapper<Sprite> _spriteMapper;
    private ComponentMapper<Transform2> _transformMapper;

    public RenderSystem(GraphicsDevice graphicsDevice, TextureAtlas sprites) : base(Aspect.All(typeof(Sprite))) {
        _graphicsDevice = graphicsDevice;
        _spriteBatch = new SpriteBatch(graphicsDevice);
        _sprites = sprites;

        _spriteCache = new Dictionary<SpriteIdentifier, MonoGame.Aseprite.Sprite>();
        PrepareSpriteCache();
    }

    private void PrepareSpriteCache() {
        foreach (var spriteIdentifier in Enum.GetValues<SpriteIdentifier>())
            _spriteCache[spriteIdentifier] = _sprites.CreateSprite((int)spriteIdentifier);
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

            _spriteBatch.Draw(_spriteCache[sprite.Identifier], transform.Position);
        }

        _spriteBatch.End();
    }
}
