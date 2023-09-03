using Engine_lib.UI_Framework;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace DinkleBurg.Editor_Components.views
{
    public class Editor_UI_Manager
    {
        public Vegetation_Menu vegetation_Menu { get; protected set; }
        public Terrain_Menu terrain_Menu { get; protected set; }

        public enum TabState { terrain, vegetation, none }

        // tabs + title label
        Button Terrain_Tab { get; set; }
        Button Vegetation_Tab { get; set; }
        Button save_button { get; set; }
        Button spawn_point_button { get; set; }

        public Box side_panel { get; protected set; }

        public Editor_UI_Manager()
        {
            // init a box along side of screen
            Editor.current.tile_manager.curr_tab_state = TabState.terrain;
            vegetation_Menu = new Vegetation_Menu();
            terrain_Menu = new Terrain_Menu();
        }

        public void Initialize(Game game)
        {
            var val = game.Window.ClientBounds.Width - game.Window.ClientBounds.Width / 11;

            side_panel = new Box("Main_Panel",
                new Vector2(val, 0),
                    game.Window.ClientBounds.Width / 11,
                    game.Window.ClientBounds.Height,
            Globals.DeviceManager.GraphicsDevice
                );
            side_panel.Set_Background(Color.Gray, Globals.DeviceManager.GraphicsDevice);

            Terrain_Tab = new Button(
                "terrain_button",
                "Terrain",
                new Vector2(side_panel.Position.X, side_panel.Position.Y),
                game.Window.ClientBounds.Width / 11,
                30,
            Globals.DeviceManager.GraphicsDevice
             );

            Terrain_Tab.Set_Background(Color.Green,
            Globals.DeviceManager.GraphicsDevice);

            Terrain_Tab.Click = () =>
            {
                if (Editor.current.tile_manager.curr_tab_state != TabState.terrain)
                {
                    Editor.current.tile_manager.curr_tab_state = TabState.terrain;
                }
                else
                {
                    Editor.current.tile_manager.curr_tab_state = TabState.none;
                }
            };

            Vegetation_Tab = new Button(
                "vegetation_button",
                "Vegetation",
                new Vector2(side_panel.Position.X, side_panel.Position.Y + 45),
                game.Window.ClientBounds.Width / 11,
                30,
            Globals.DeviceManager.GraphicsDevice
             );

            Vegetation_Tab.Set_Background(Color.Green,
            Globals.DeviceManager.GraphicsDevice);

            Vegetation_Tab.Click = () =>
            {
                if (Editor.current.tile_manager.curr_tab_state != TabState.vegetation)
                {
                    Editor.current.tile_manager.curr_tab_state = TabState.vegetation;
                }
                else
                {
                    Editor.current.tile_manager.curr_tab_state = TabState.none;
                }
            };

            spawn_point_button = new Button(
                "vegetation_button",
                "New Spawn",
                new Vector2(Vegetation_Tab.Position.X, Vegetation_Tab.Position.Y + 45),
                game.Window.ClientBounds.Width / 11,
                30,
            Globals.DeviceManager.GraphicsDevice
             );

            spawn_point_button.Set_Background(Color.Green,
            Globals.DeviceManager.GraphicsDevice);

            spawn_point_button.Click = () =>
            {
                Debug.WriteLine("Spawn point selected!");

                // need a spawn tile model and use the prototype "air" blocks to make mark where spawns are. 
                // they get a seperate layer so they can be turned off when needed or just after the game starts.

                // set selected to spawn point(prototype block)
                // set selected name to "prototype block key"
            };

            save_button = new Button("save_button", "Save", new Vector2(side_panel.Position.X + (side_panel.Width * 0.4f - 50), side_panel.Position.Y + (side_panel.Height * 0.9f - 50)), 100, 45,
            Globals.DeviceManager.GraphicsDevice);
            save_button.Set_Background(Color.Red,
            Globals.DeviceManager.GraphicsDevice);
            save_button.Click = () =>
            {
                Editor.current.Save();
            };

            vegetation_Menu.Initialize(side_panel.Position, game.Window.ClientBounds.Height, 200);
            terrain_Menu.Initialize(side_panel.Position, game.Window.ClientBounds.Height, 200);
        }

        public void Update()
        {
            side_panel.Update();
            Terrain_Tab.Update();
            Vegetation_Tab.Update();
            save_button.Update();
            spawn_point_button.Update();

            if (Editor.current != null)
            {
				if (Editor.current.tile_manager.curr_tab_state == TabState.vegetation)
				{
					vegetation_Menu.Update();
				}
				else if (Editor.current.tile_manager.curr_tab_state == TabState.terrain)
				{
					terrain_Menu.Update();
				}
			}
        }

        public void Draw()
        {
            side_panel.Draw(true, Globals.Sprite_Batch, Globals.Viewport, Globals.Game_Font);
            Terrain_Tab.Draw(true, Globals.Sprite_Batch, Globals.Viewport, Globals.Game_Font);
            Vegetation_Tab.Draw(true, Globals.Sprite_Batch, Globals.Viewport, Globals.Game_Font);
            if (Editor.current.tile_manager.curr_tab_state == TabState.vegetation)
            {
                vegetation_Menu.Draw();
            }
            else if (Editor.current.tile_manager.curr_tab_state == TabState.terrain)
            {
                terrain_Menu.Draw();
            }
            save_button.Draw(true, Globals.Sprite_Batch, Globals.Viewport, Globals.Game_Font);
            spawn_point_button.Draw(true, Globals.Sprite_Batch, Globals.Viewport, Globals.Game_Font);
        }
    }
}

