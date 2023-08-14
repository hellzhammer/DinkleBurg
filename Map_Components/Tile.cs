using DinkleBurg.Editor_Components;
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

        public Tile(int _x, int _y, Texture2D text, string _name)
        {
            this.name = _name;
            this.Position = new Vector2(_x, _y);
            this.Texture = text;
            this.OnClick = () =>
            {
                if (Editor.current.tile_manager.selected_texture != null && !string.IsNullOrWhiteSpace(Editor.current.tile_manager.selected_texture_name))
                {
                    this.is_empty = false;
                    this.Texture = Editor.current.tile_manager.selected_texture; //Selection.Selected_Texture;
                    this.name = Editor.current.tile_manager.selected_texture_name;
                }
            };

            this.OnHold = () =>
            {
                if (Active_Tile.tile != null)
                {
                    this.is_empty = false;
                    this.Texture = Editor.current.tile_manager.selected_texture; //Selection.Selected_Texture;
                    this.name = Editor.current.tile_manager.selected_texture_name;
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

        /*public TileModel save_model()
        {
            return new TileModel()
            {
                empty = this.is_empty,
                texture_name = this.name,
                x = (int)Position.X,
                y = (int)Position.Y
            };
        }*/
    }
}
