using DinkleBurg.Editor_Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Security.AccessControl;

namespace DinkleBurg.Map_Components
{
	public class Resource : Tile
	{
		public Resource_Type resrouce_type = Resource_Type.none;

		public Resource(int _x, int _y, Texture2D text, string _name, Resource_Type type)
		{
			this.resrouce_type = type;
			this.name = _name;
			this.Position = new Vector2(_x, _y);
			this.Texture = text;
			init();
		}

		public Resource(ResourceTileModel model)
		{
			this.resrouce_type = model.type;
			this.name = model.texture_name;
			this.Position = new Vector2(model.x, model.y);
			this.Texture = Engine_Texture_Loader.terrain_textures[model.texture_name];
			this.is_empty = model.is_empty;
			init();
		}

		public ResourceTileModel save_resource_model()
		{
			return new ResourceTileModel()
			{
				type = this.resrouce_type,
				is_empty = this.is_empty,
				texture_name = this.name,
				x = (int)Position.X,
				y = (int)Position.Y
			};
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
				}
			};
		}
	}

	public class ResourceTileModel : TileModel
	{
		public Resource_Type type = Resource_Type.none;
	}
}
