using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;

namespace Eggjam.Components;

internal delegate void OnClick(World world, int entityId);

internal record Clickable(Vector2 DefaultSize, Vector2 HoverSize, Vector2 ClickSize, OnClick OnClick);
