using System.Diagnostics.CodeAnalysis;
using AsepriteDotNet.Aseprite;
using Eggjam.Systems;
using Eggjam.Utils;
using Microsoft.Xna.Framework;
using MonoGame.Aseprite;
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

    private TextureAtlas _sprites;

    public EggjamGame() {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize() {
        Components.Add(new InputExtendedComponent(this));
        base.Initialize();
    }

    protected override void LoadContent() {
        var asepriteFile = Content.Load<AsepriteFile>("Sprites");
        _sprites = asepriteFile.CreateTextureAtlas(GraphicsDevice);

        var world = new WorldBuilder()
            .AddSystem(new InitializationSystem(GraphicsDevice))
            .AddSystem(new RenderSystem(GraphicsDevice, _sprites))
            .Build();

        Components.Add(world);
    }
}
