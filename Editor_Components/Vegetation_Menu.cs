using Engine_lib.UI_Framework;
using Engine_lib.UI_Framework.Interfaces;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace DinkleBurg.Editor_Components
{
	public class Vegetation_Menu : View
	{
		public override void Initialize(Vector2 pos, float height, float width)
		{
			var base_pos = new Vector2((pos.X - width) - 12.5f, 0);
			this.background = new Box(
					"Main_Panel",
					base_pos,
					width,
					height,
			Globals.DeviceManager.GraphicsDevice);

			this.background.Set_Background(Color.Gray,
			Globals.DeviceManager.GraphicsDevice);
			Vector2 last = base_pos;
			var tLabel = new Label("title_label", "Landscape Textures", this.background.Position, 100, 30,
			Globals.DeviceManager.GraphicsDevice);
			tLabel.Set_Background(Color.Transparent,
			Globals.DeviceManager.GraphicsDevice);
			this.background.AddChild(tLabel, false, false);

			var h_box = new Box("hbox", new Vector2(this.background.Position.X, this.background.Position.Y + tLabel.Height), width, height,
			Globals.DeviceManager.GraphicsDevice);
			h_box.Orientation = WidgetOrientation.Horizontal;
			foreach (var item in Engine_Texture_Loader.vegetation_textures)
			{
				Button b = new Button(item.Key, ""/*item.Key*/, last, 32, 32,
			Globals.DeviceManager.GraphicsDevice);
				b.background = item.Value;
				b.Click += () => {
					Debug.WriteLine(item.Key);
					if (Editor.current.tile_manager == null)
					{
						throw new System.Exception("Tile manager should not be null.");
					}

					if (item.Key == "Boulder")
					{
						Editor.current.tile_manager.curr_type = Resource_Type.Stone;
					}
					else if (item.Key == "Bush")
					{
						Editor.current.tile_manager.curr_type = Resource_Type.Food;
					}
					else if (item.Key == "Maple_Tree")
					{
						Editor.current.tile_manager.curr_type = Resource_Type.Wood;
					}

					Editor.current.tile_manager.selected_texture = b.background;
					Editor.current.tile_manager.selected_texture_name = b.name;
				};
				h_box.AddChild(b);
				last = new Vector2(last.X, last.Y + 32);
			}
			this.background.AddChild(h_box, centered_items: true);
		}

		public override void Draw()
		{
			background.Draw(true, Globals.Sprite_Batch, Globals.Viewport, Globals.Game_Font);
		}

		public override void Update()
		{
			background.Update();
		}
	}
}
