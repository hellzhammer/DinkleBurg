using DinkleBurg.UI_Framework.Interfaces;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using UI_Framework;

namespace DinkleBurg.Editor_Components
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
                    game.Window.ClientBounds.Height
                );

            Terrain_Tab = new Button(
                "terrain_button", 
                "Terrain", 
                new Vector2(side_panel.Position.X, side_panel.Position.Y),
                game.Window.ClientBounds.Width / 11, 
                30
             );

            Terrain_Tab.Set_Background(Color.Green);

            Terrain_Tab.Click = () => {
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
                30
             );

            Vegetation_Tab.Set_Background(Color.Green);

            Vegetation_Tab.Click = () => {
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
                30
             );

			spawn_point_button.Set_Background(Color.Green);

			spawn_point_button.Click = () => {
                Debug.WriteLine("Spawn point selected!");
            };

            this.save_button = new Button("save_button", "Save", new Vector2(side_panel.Position.X + ((side_panel.Width * 0.4f) - 50), side_panel.Position.Y + ((side_panel.Height * 0.9f)-50)), 100, 45);
            this.save_button.Set_Background(Color.Red);
            this.save_button.Click = () => {
                Editor.current.Save();
            };

            this.vegetation_Menu.Initialize(side_panel.Position, game.Window.ClientBounds.Height, 200);
            this.terrain_Menu.Initialize(side_panel.Position, game.Window.ClientBounds.Height, 200);
        }

        public void Update()
        {
            this.side_panel.Update();
            Terrain_Tab.Update();
            Vegetation_Tab.Update();
            this.save_button.Update();
            this.spawn_point_button.Update();

            if (Editor.current.tile_manager.curr_tab_state == TabState.vegetation)
            {
                this.vegetation_Menu.Update();
            }
            else if (Editor.current.tile_manager.curr_tab_state == TabState.terrain)
            {
                this.terrain_Menu.Update();
            }
        }

        public void Draw()
        {
            this.side_panel.Draw(true);
            Terrain_Tab.Draw(true);
            Vegetation_Tab.Draw(true);
            if (Editor.current.tile_manager.curr_tab_state == TabState.vegetation)
            {
                this.vegetation_Menu.Draw();
            }
            else if (Editor.current.tile_manager.curr_tab_state == TabState.terrain)
            {
                this.terrain_Menu.Draw();
            }
            this.save_button.Draw(true);
            this.spawn_point_button.Draw(true);
        }
    }
}

