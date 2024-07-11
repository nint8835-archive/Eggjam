using System;
using System.Collections.Generic;
using Eggjam.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Aseprite;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using Sprite = Eggjam.Components.Sprite;

namespace Eggjam.Systems;

public class RenderSystem : EntityDrawSystem {
    private readonly BitmapFont _font;
    private readonly GraphicsDevice _graphicsDevice;
    private readonly SpriteBatch _spriteBatch;
    private readonly Dictionary<SpriteIdentifier, TextureRegion> _spriteCache;
    private readonly TextureAtlas _sprites;
    private ComponentMapper<Sprite> _spriteMapper;
    private ComponentMapper<Transform2> _transformMapper;

    public RenderSystem(GraphicsDevice graphicsDevice, TextureAtlas sprites, BitmapFont font) :
        base(Aspect.All(typeof(Sprite))) {
        _graphicsDevice = graphicsDevice;
        _spriteBatch = new SpriteBatch(graphicsDevice);
        _sprites = sprites;
        _font = font;

        _spriteCache = new Dictionary<SpriteIdentifier, TextureRegion>();
        PrepareSpriteCache();
    }

    private void PrepareSpriteCache() {
        foreach (var spriteIdentifier in Enum.GetValues<SpriteIdentifier>())
            _spriteCache[spriteIdentifier] = _sprites.CreateSprite((int)spriteIdentifier).TextureRegion;
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

            _spriteBatch.Draw(
                _spriteCache[sprite.Identifier],
                new Rectangle(
                    (transform.Position - transform.Scale / 2).ToPoint(),
                    transform.Scale.ToPoint()
                ),
                Color.White
            );
        }

        _spriteBatch.DrawString(
            _font,
            "Aaaaa",
            new Vector2(),
            Color.White,
            0f,
            new Vector2(),
            new Vector2(0.5f),
            SpriteEffects.None, 
            0f);

        _spriteBatch.End();
    }
}
