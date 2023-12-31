﻿using DinkleBurg.Map_Components;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace DinkleBurg
{
    public class Terrain
    {
        private int Map_Width = 0, Map_Height = 0;
        private int pixel_X = 0, pixel_Y = 0;
        private bool initialized = false;
        public List<List<Tile>> Tile_Map { get; set; }
        public Resource[][] Scenery_Map { get; set; }

        public void Save()
        {
            List<List<TileModel>> tmodels = new List<List<TileModel>>();
            for (int x = 0; x < Tile_Map.Count; x++)
            {
                List<TileModel> tmods = new List<TileModel>();
                for (int y = 0; y < Tile_Map[x].Count; y++)
                {
                    if (Tile_Map[x][y].is_empty)
                    {
                        tmods.Add(null);
                    }
                    else
                    {
                        var tm = Tile_Map[x][y].save_tile_model();
                        tmods.Add(tm);
                    }
                }
                tmodels.Add(tmods);
            }

            List<List<ResourceTileModel>> vmodels = new List<List<ResourceTileModel>>();
            for (int x = 0; x < Scenery_Map.Length; x++)
            {
                List<ResourceTileModel> vmods = new List<ResourceTileModel>();
                for (int y = 0; y < Scenery_Map[x].Length; y++)
                {
                    if (Scenery_Map[x][y] != null)
                    {
                        var vm = Scenery_Map[x][y].save_resource_model();
                        vm.type = Scenery_Map[x][y].resrouce_type;

						vmods.Add(vm);
                    }
                    else
                    {
                        vmods.Add(null);
                    }
                }
                vmodels.Add(vmods);
            }

            Dictionary<string, string> models = new Dictionary<string, string>();

            var json = JsonConvert.SerializeObject(tmodels);
            var json2 = JsonConvert.SerializeObject(vmodels);
            models.Add("terrain", json);
            models.Add("scenery", json2);

            var save_json = JsonConvert.SerializeObject(models);

            string file_id = Directory.GetFiles(Environment.CurrentDirectory + "/Maps").Length.ToString();
            using (StreamWriter sw = new StreamWriter(Environment.CurrentDirectory + "/Maps/gm_" + file_id + ".json"))
            {
                sw.Write(save_json);
                sw.Close();
                sw.Dispose();
            }
        }

        public Terrain(float map_X, float map_Y, int pixel_width, int pixel_height)
        {
            pixel_X = pixel_width;
            pixel_Y = pixel_height;
            Map_Width = (int)map_X;
            Map_Height = (int)map_Y;
            this.Tile_Map = new List<List<Tile>>();
        }

        public Terrain(List<List<Tile>> tiles, Resource[][] scenery)
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
                string water_tile = "Water";
                for (int i = 0; i < Map_Width; i += pixel_X)
                {
                    List<Tile> tiles = new List<Tile>();
                    for (int j = 0; j < Map_Height; j += pixel_Y)
                    {
                        Tile tile = new Tile(i, j, Engine_Texture_Loader.terrain_textures[water_tile], water_tile);
                        tile.is_walkable = Globals.terrain_definitions[water_tile].is_walkable;
                        tile.is_empty = false;
                        tiles.Add(tile);
                    }
                    Tile_Map.Add(tiles);
                }

                // init the scene objects
                this.Scenery_Map = new Resource[Tile_Map.Count][];
                for (int y = 0; y < Scenery_Map.Length; y++)
                {
                    Scenery_Map[y] = new Resource[Tile_Map[y].Count];
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
