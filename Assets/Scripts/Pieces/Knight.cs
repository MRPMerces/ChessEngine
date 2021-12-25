using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{

    public Knight(Tile tile, Color color) {
        this.tile = tile;
        this.color = color;

        pieceType = PieceType.KNIGHT;
    }

    public override Tile[] getMovableTiles(bool OnlyTakes = false) {
        List<Tile> tiles = new List<Tile>(8);

        if (World.world.isValidMove(tile.X + 1, tile.Y + 2, color) == State.VALID || World.world.isValidMove(tile.X + 1, tile.Y + 2, color) == State.TAKES) {
            tiles.Add(World.world.getTileAt(tile.X + 1, tile.Y + 2));
        }

        if (World.world.isValidMove(tile.X + 1, tile.Y - 2, color) == State.VALID || World.world.isValidMove(tile.X + 1, tile.Y - 2, color) == State.TAKES) {
            tiles.Add(World.world.getTileAt(tile.X + 1, tile.Y - 2));
        }

        if (World.world.isValidMove(tile.X - 1, tile.Y + 2, color) == State.VALID || World.world.isValidMove(tile.X - 1, tile.Y + 2, color) == State.TAKES) {
            tiles.Add(World.world.getTileAt(tile.X - 1, tile.Y + 2));
        }

        if (World.world.isValidMove(tile.X - 1, tile.Y - 2, color) == State.VALID || World.world.isValidMove(tile.X - 1, tile.Y - 2, color) == State.TAKES) {
            tiles.Add(World.world.getTileAt(tile.X - 1, tile.Y - 2));
        }

        if (World.world.isValidMove(tile.X + 2, tile.Y + 1, color) == State.VALID || World.world.isValidMove(tile.X + 2, tile.Y + 1, color) == State.TAKES) {
            tiles.Add(World.world.getTileAt(tile.X + 2, tile.Y + 1));
        }

        if (World.world.isValidMove(tile.X + 2, tile.Y - 1, color) == State.VALID || World.world.isValidMove(tile.X + 2, tile.Y - 1, color) == State.TAKES) {
            tiles.Add(World.world.getTileAt(tile.X + 2, tile.Y - 1));
        }

        if (World.world.isValidMove(tile.X - 2, tile.Y + 1, color) == State.VALID || World.world.isValidMove(tile.X - 2, tile.Y + 1, color) == State.TAKES) {
            tiles.Add(World.world.getTileAt(tile.X - 2, tile.Y + 1));
        }

        if (World.world.isValidMove(tile.X - 2, tile.Y - 1, color) == State.VALID || World.world.isValidMove(tile.X - 2, tile.Y - 1, color) == State.TAKES) {
            tiles.Add(World.world.getTileAt(tile.X - 2, tile.Y - 1));
        }

        return tiles.ToArray();
    }
}
