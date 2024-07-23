using System;
using Eggjam.Components;
using Eggjam.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using UnitsNet;

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
                (_, _) => { State.Instance.Height += (int)Math.Floor(Math.Pow(1.25, State.Instance.CrankCount)); }
            )
        );

        var crank = world.CreateEntity();
        crank.Attach(
            new Transform2 {
                Position = new Vector2(32, _graphicsDevice.Viewport.Height - 32),
                Scale = new Vector2(64, 64)
            }
        );
        crank.Attach(new Sprite(SpriteIdentifier.Crank));
        crank.Attach(
            new Clickable(
                new Vector2(64, 64),
                new Vector2(72, 72),
                new Vector2(56, 56),
                (_, _) => {
                    var currentCost = State.Instance.CrankCost;

                    if (State.Instance.Height < currentCost)
                        return;

                    State.Instance.Height -= currentCost;
                    State.Instance.CrankCount++;

                    var newCost = State.Instance.CrankCost;
                    var displayCost = UnitRendering.AsCurrentHeightUnit(Length.FromCentimeters(newCost));
                    Console.WriteLine($"New cost: {displayCost.Value:N2} {Length.GetAbbreviation(displayCost.Unit)}!");
                }
            )
        );
    }

    public void Dispose() {
        GC.SuppressFinalize(this);
    }
}
