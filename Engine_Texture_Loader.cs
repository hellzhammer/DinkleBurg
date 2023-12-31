﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace DinkleBurg
{
    public static class Engine_Texture_Loader
    {
        public static Dictionary<string, Texture2D> gui_textures { get; set; }
        public static Dictionary<string, Texture2D> terrain_textures { get; set; }
        public static Dictionary<string, Texture2D> vegetation_textures { get; set; }

        /// <summary>
        /// Loads all engine gui textures.
        /// </summary>
        /// <param name="content"></param>
        public static void LoadEngineGUITextures(ContentManager content)
        {
            gui_textures = new Dictionary<string, Texture2D>();
            gui_textures.Add("Prototype_Tile", content.Load<Texture2D>("Editor/Engine_Textures/Prototype_Tile"));
            gui_textures.Add("Prototype_Tile_Selected", content.Load<Texture2D>("Editor/Engine_Textures/Prototype_Tile_Selected"));
        }

        /// <summary>
        /// Loads all engine editor textures.
        /// </summary>
        /// <param name="content"></param>
        public static void LoadTerrainTextures(ContentManager content)
        {
            terrain_textures = new Dictionary<string, Texture2D>();
            foreach (var entry in Globals.terrain_definitions)
            {
				terrain_textures.Add(entry.Key, content.Load<Texture2D>(entry.Value.file_path));
			}
        }

        /// <summary>
        /// Loads in the vegeation textures.
        /// </summary>
        /// <param name="content"></param>
        public static void LoadVegetationTextures(ContentManager content)
        {
            vegetation_textures = new Dictionary<string, Texture2D>();
            vegetation_textures.Add("Boulder", content.Load<Texture2D>("Editor/Vegetation_Textures/Boulder"));
            vegetation_textures.Add("Tree_Maple", content.Load<Texture2D>("Editor/Vegetation_Textures/Tree_Maple"));
            vegetation_textures.Add("Bush", content.Load<Texture2D>("Editor/Vegetation_Textures/Bush"));
        }
    }
}
