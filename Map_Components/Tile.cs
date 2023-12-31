﻿using DinkleBurg.Editor_Components;
using DinkleBurg.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DinkleBurg.Map_Components
{
    public class Tile : GameObject
    {
        public bool is_empty = true;
        
        public Texture2D Texture { get; set; }

        public bool is_walkable = false;

        public Tile() { }

        public Tile(int _x, int _y, Texture2D text, string _name)
        {
            this.name = _name;
            this.Position = new Vector2(_x, _y);
            this.Texture = text;
            init();
        }

        public Tile(TileDef def, Vector2 pos, string name)
        {
            this.is_walkable = def.is_walkable;
            this.name = name;
			this.Position = pos;
            this.Texture = Engine_Texture_Loader.terrain_textures[def.texture_name];
		}

        public Tile(TileModel model)
        {
            this.name = model.texture_name;
            this.Position = new Vector2(model.x, model.y);
            this.Texture = Engine_Texture_Loader.terrain_textures[model.texture_name];
            this.is_empty = model.is_empty;
            this.is_walkable = model.is_walkable;
            init();
        }

        private void init()
        {
            this.OnClick = () =>
            {
                if (Editor.current.tile_manager.selected_texture != null && !string.IsNullOrWhiteSpace(Editor.current.tile_manager.selected_texture_name))
                {
                    if (Editor.current.tile_manager.selected_texture != null)
                    {
                        this.is_empty = false;
                        this.Texture = Editor.current.tile_manager.selected_texture; //Selection.Selected_Texture;
                        this.name = Editor.current.tile_manager.selected_texture_name;
                        this.is_walkable = Globals.terrain_definitions[this.name].is_walkable;
                    }
                }
            };

            this.OnHold = () =>
            {
                if (Editor.current.tile_manager.selected_texture != null)
                {
                    this.is_empty = false;
                    this.Texture = Editor.current.tile_manager.selected_texture; //Selection.Selected_Texture;
                    this.name = Editor.current.tile_manager.selected_texture_name;
					this.is_walkable = Globals.terrain_definitions[this.name].is_walkable;
				}
            };
        }

        public void Draw()
        {
            Globals.Sprite_Batch.Draw(Texture, this.Position, Color.White);
        }

        public override void Update()
        {
            // to do
        }

        public TileModel save_tile_model()
        {
            return new TileModel()
            {
                is_walkable = this.is_walkable,
                is_empty = this.is_empty,
                texture_name = this.name,
                x = (int)Position.X,
                y = (int)Position.Y
            };
        }
    }

    public class TileDef
    {
        public string texture_name { get; set; }
        public bool is_walkable { get; set; }
        public string file_path { get; set; }
	}

    public class TileModel
    {
        public string texture_name { get; set; }
		public bool is_empty { get; set; }
		public int x { get; set; }
        public int y { get; set; }
		public bool is_walkable { get; set; }
	}
}
