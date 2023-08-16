using DinkleBurg.Map_Components;
using System.Collections.Generic;

namespace DinkleBurg
{
    public class Terrain
    {
        private int Map_Width = 0, Map_Height = 0;
        private int pixel_X = 0, pixel_Y = 0;
        private bool initialized = false;
        public List<List<Tile>> Tile_Map { get; set; }
        public Tile[][] Scenery_Map { get; set; }

        public Terrain(float map_X, float map_Y, int pixel_width, int pixel_height)
        {
            pixel_X = pixel_width;
            pixel_Y = pixel_height;
            Map_Width = (int)map_X;
            Map_Height = (int)map_Y;
            this.Tile_Map = new List<List<Tile>>();
        }

        public Terrain(List<List<Tile>> tiles, Tile[][] scenery)
        {
            Map_Width = tiles[0].Count * pixel_X;
            Map_Height = tiles.Count * pixel_Y;

            this.Tile_Map = tiles;
            this.Scenery_Map = scenery;
            this.initialized = true;
        }

        public void Initialize()
        {
            if (!initialized)
            {
                for (int i = 0; i < Map_Width; i += pixel_X)
                {
                    List<Tile> tiles = new List<Tile>();
                    for (int j = 0; j < Map_Height; j += pixel_Y)
                    {
                        Tile tile = new Tile(i, j, Engine_Texture_Loader.gui_textures["Prototype_Tile"], "air");
                        tiles.Add(tile);
                    }
                    Tile_Map.Add(tiles);
                }

                // init the scene objects
                this.Scenery_Map = new Tile[Tile_Map.Count][];
                for (int y = 0; y < Scenery_Map.Length; y++)
                {
                    Scenery_Map[y] = new Tile[Tile_Map[y].Count];
                }
            }
        }

        public void Update()
        {
            if (Globals.state == Game_State.Editor_Running)
            {
                // not yet implemented.
            }
        }

        public void Draw()
        {
            if (Globals.state == Game_State.Editor_Running)
            {
                for (int i = 0; i < Tile_Map.Count; i++)
                {
                    for (int j = 0; j < Tile_Map[i].Count; j++)
                    {
                        Tile_Map[i][j].Draw();
                    }
                }

                for (int i = 0; i < Scenery_Map.Length; i++)
                {
                    for (int j = 0; j < Scenery_Map[i].Length; j++)
                    {
                        if (Scenery_Map[i][j] != null)
                        {
                            Scenery_Map[i][j].Draw();
                        }
                    }
                }
            }
        }
    }
}
