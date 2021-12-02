using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using UnityEngine;

public enum State { VALID, INVALID, TAKES }

public class World : IXmlSerializable
{
    public World(int Width, int Height) {
        setupWorld(Width, Height);
    }

    public static World world { get; protected set; }

    // A two-dimensional array to hold our tiles.
    public Tile[,] tiles { get; protected set; }
    public int Width { get; protected set; }
    public int Height { get; protected set; }

    void setupWorld(int Width, int Height) {
        this.Width = Width;
        this.Height = Height;
        world = this;

        tiles = new Tile[Width, Height];

        bool black = false;
        // Populate Tile array with (Width * Height) tiles
        for (int x = 0; x < Width; x++) {
            if (!black) {
                black = true;
            }

            else {
                black = false;
            }

            for (int y = 0; y < Height; y++) {
                if (black) {
                    tiles[x, y] = new Tile(x, y, Color.BLACK);
                    black = false;
                }

                else {
                    tiles[x, y] = new Tile(x, y, Color.WHITE);
                    black = true;
                }
            }
        }

        Debug.Log("World created with " + (Width * Height) + " tiles.");
    }

    // Get Tile with cordinates x,y
    public Tile getTileAt(int x, int y) {
        if (x >= Width || x < 0 || y >= Height || y < 0) {
            return null;
        }

        return tiles[x, y];
    }

    public State isValidMove(int x, int y, Color color) {
        Tile tile = getTileAt(x, y);
        if (tile == null) {
            return State.INVALID;
        }

        if (tile.piece == null) {
            return State.VALID;
        }

        if (tile.piece.color != color) {
            return State.TAKES;
        }

        return State.INVALID;
    }

    public State isValidMove(Tile tile, Color color) {
        if (tile == null) {
            return State.INVALID;
        }

        if (tile.piece == null) {
            return State.VALID;
        }

        if (tile.piece.color != color) {
            return State.TAKES;
        }

        return State.INVALID;
    }

    #region Saving and loading

    /// <summary>
    /// Default constructor for saving and loading.
    /// </summary>
    private World() {
    }

    public XmlSchema GetSchema() {
        return null;
    }

    public void WriteXml(XmlWriter writer) {
        writer.WriteAttributeString("Width", Width.ToString());
        writer.WriteAttributeString("Height", Height.ToString());

        writer.WriteStartElement("Tiles");
        for (int x = 0; x < Width; x++) {
            for (int y = 0; y < Height; y++) {
                writer.WriteStartElement("Tile");
                tiles[x, y].WriteXml(writer);
                writer.WriteEndElement();
            }
        }
        writer.WriteEndElement();
    }

    public void ReadXml(XmlReader reader) {

        Width = int.Parse(reader.GetAttribute("Width"));
        Height = int.Parse(reader.GetAttribute("Height"));

        setupWorld(Width, Height);

        // We are in the "Tiles" element, so read elements until we run out of "Tile" nodes.
        if (reader.ReadToDescendant("Tile")) {
            // We have at least one tile, so do something with it.

            do {
                //Debug.Log("Name: " + reader.Name);
                int x = int.Parse(reader.GetAttribute("X"));
                int y = int.Parse(reader.GetAttribute("Y"));
                tiles[x, y].ReadXml(reader);

                Debug.Log(reader.NodeType + "Name: " + reader.Name + "value: " + reader.Value);

            } while (reader.ReadToNextSibling("Tile"));
        }
    }

    #endregion Saving and loading
}
