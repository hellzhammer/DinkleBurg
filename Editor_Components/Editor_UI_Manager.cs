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

        public Box side_panel { get; protected set; }

        public Editor_UI_Manager()
        {
            // init a box along side of screen
            Editor.current.tile_manager.current = TabState.terrain;
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
                if (Editor.current.tile_manager.current != TabState.terrain)
                {
                    Editor.current.tile_manager.current = TabState.terrain;
                }
                else
                {
                    Editor.current.tile_manager.current = TabState.none;
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
                if (Editor.current.tile_manager.current != TabState.vegetation)
                {
                    Editor.current.tile_manager.current = TabState.vegetation;
                }
                else
                {
                    Editor.current.tile_manager.current = TabState.none;
                }
            };

            this.vegetation_Menu.Initialize(side_panel.Position, game.Window.ClientBounds.Height, 200);
            this.terrain_Menu.Initialize(side_panel.Position, game.Window.ClientBounds.Height, 200);
        }

        public void Update()
        {
            this.side_panel.Update();
            Terrain_Tab.Update();
            Vegetation_Tab.Update();

            if (Editor.current.tile_manager.current == TabState.vegetation)
            {
                this.vegetation_Menu.Update();
            }
            else if (Editor.current.tile_manager.current == TabState.terrain)
            {
                this.terrain_Menu.Update();
            }
        }

        public void Draw()
        {
            this.side_panel.Draw(true);
            Terrain_Tab.Draw(true);
            Vegetation_Tab.Draw(true);
            if (Editor.current.tile_manager.current == TabState.vegetation)
            {
                this.vegetation_Menu.Draw();
            }
            else if (Editor.current.tile_manager.current == TabState.terrain)
            {
                this.terrain_Menu.Draw();
            }
        }
    }

    #region Terrain
    public class Terrain_Menu : View
    {
        public override void Initialize(Vector2 pos, float height, float width)
        {
            var base_pos = new Vector2((pos.X - width) - 12.5f, 0);
            this.background = new Box(
                    "Main_Panel",
                    base_pos,
                    width,
                    height);

            this.background.Set_Background(Color.Red);
            Vector2 last = base_pos;
            var tLabel = new Label("title_label", "Terrain Textures", this.background.Position, 100, 30);
            tLabel.Set_Background(Color.Transparent);
            this.background.AddChild(tLabel, false, false);

            var h_box = new Box("hbox", new Vector2(this.background.Position.X, this.background.Position.Y + tLabel.Height), width, height);
            h_box.Orientation = WidgetOrientation.Horizontal;
            foreach (var item in Engine_Texture_Loader.terrain_textures)
            {
                Button b = new Button(item.Key, ""/*item.Key*/, last, 32, 32);
                b.background = item.Value;
                b.Click += () => {
                    Debug.WriteLine(item.Key);
                    if (Editor.current.tile_manager == null)
                    {
                        throw new System.Exception("Tile manager should not be null.");
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
            background.Draw(true);
        }

        public override void Update()
        {
            background.Update();
        }
    }
    #endregion

    #region Vegetation
    public class Vegetation_Menu : View
    {
        public override void Initialize(Vector2 pos, float height, float width)
        {
            var base_pos = new Vector2((pos.X - width) - 12.5f, 0);
            this.background = new Box(
                    "Main_Panel",
                    base_pos,
                    width,
                    height);

            this.background.Set_Background(Color.Green);
            Vector2 last = base_pos;
            var tLabel = new Label("title_label", "Landscape Textures", this.background.Position, 100, 30);
            tLabel.Set_Background(Color.Transparent);
            this.background.AddChild(tLabel, false, false);

            var h_box = new Box("hbox", new Vector2(this.background.Position.X, this.background.Position.Y + tLabel.Height), width, height);
            h_box.Orientation = WidgetOrientation.Horizontal;
            foreach (var item in Engine_Texture_Loader.vegetation_textures)
            {
                Button b = new Button(item.Key, ""/*item.Key*/, last, 32, 32);
                b.background = item.Value;
                b.Click += () => {
                    Debug.WriteLine(item.Key);
                    if (Editor.current.tile_manager == null)
                    {
                        throw new System.Exception("Tile manager should not be null.");
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
            background.Draw(true);
        }

        public override void Update()
        {
            background.Update();
        }
    }
    #endregion
}

