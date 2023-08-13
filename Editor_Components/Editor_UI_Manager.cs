﻿using DinkleBurg.UI_Framework.Interfaces;
using Microsoft.Xna.Framework;
using UI_Framework;

namespace DinkleBurg.Editor_Components
{
    public class Editor_UI_Manager
    {
        public Vegetation_Menu vegetation_Menu { get; protected set; }
        public Terrain_Menu terrain_Menu { get; protected set; }

        private enum TabState { terrain, vegetation, none }
        private TabState curr_state = TabState.terrain;

        // tabs + title label
        Button Terrain_Tab { get; set; }
        Button Vegetation_Tab { get; set; }

        public Box side_panel { get; protected set; }

        public Editor_UI_Manager()
        {
            // init a box along side of screen
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
                if (this.curr_state != TabState.terrain)
                {
                    this.curr_state = TabState.terrain;
                }
                else
                {
                    this.curr_state = TabState.none;
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
                if (this.curr_state != TabState.vegetation)
                {
                    this.curr_state = TabState.vegetation;
                }
                else
                {
                    this.curr_state = TabState.none;
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

            if (this.curr_state == TabState.vegetation)
            {
                this.vegetation_Menu.Update();
            }
            else if (this.curr_state == TabState.terrain)
            {
                this.terrain_Menu.Update();
            }
        }

        public void Draw()
        {
            this.side_panel.Draw(true);
            Terrain_Tab.Draw(true);
            Vegetation_Tab.Draw(true);
            if (this.curr_state == TabState.vegetation)
            {
                this.vegetation_Menu.Draw();
            }
            else if (this.curr_state == TabState.terrain)
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
            this.buttons = new System.Collections.Generic.Dictionary<string, Button>();
            this.background = new Box("Main_Panel",
                    base_pos,
                    width,
                    height
                );
            this.background.Set_Background(Color.Red);
            Vector2 last = base_pos;
            foreach (var item in Engine_Textures.terrain_textures)
            {
                Button b = new Button(item.Key, ""/*item.Key*/, last, 40, 40);
                b.background = item.Value;
                this.buttons.Add(item.Key, b);
                last = new Vector2(last.X, last.Y + 40 + 10);
            }
        }

        public override void Draw()
        {
            background.Draw(true);
            foreach (var b in this.buttons)
            {
                b.Value.Draw(true);
            }
        }

        public override void Update()
        {
            background.Update();
            foreach (var b in this.buttons)
            {
                b.Value.Update();
            }
        }
    }
    #endregion

    #region Vegetation
    public class Vegetation_Menu : View
    {
        public override void Initialize(Vector2 pos, float height, float width)
        {
            this.buttons = new System.Collections.Generic.Dictionary<string, Button>();
            this.background = new Box("Main_Panel",
                new Vector2((pos.X - width) - 12.5f, 0),
                    width,
                    height
                );
            this.background.Set_Background(Color.Green);
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
