namespace Eggjam.Components;

internal enum SpriteIdentifier {
    Egg = 0,
    Crank = 1
}

internal record Sprite(SpriteIdentifier Identifier);
