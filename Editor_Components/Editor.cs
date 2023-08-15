using DinkleBurg.Map_Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DinkleBurg.Editor_Components
{
    // want to use game component in order to update seperate modules individually? 
    // need to test to see if this actually increases any speed in the future? 
    // this is all going to require some rethinking in order to work how I want it to 
    public class Editor : GameComponent
    {
        public class Activated
        {
            public string selected_texture_name { get; set; }
            public Texture2D selected_texture { get; set; }
        }

        public enum EditorState
        {
            Running,
            Menu
        }
        public Activated tile_manager { get; set; }
        public EditorState State = EditorState.Running;
        public static Editor current { get; set; }

        private int pixel_X = 32;
        private int pixel_Y = 32;
        private Terrain terrain { get; set; }
        private Editor_UI_Manager ui_manager { get; set; }

        public Editor(int Map_Width, int Map_Height, Game game) : base(game)
        {
            // load the user interface for the application
            this.tile_manager = new Activated();
            this.ui_manager = new Editor_UI_Manager();

            terrain = new Terrain(Map_Width, Map_Height, pixel_X, pixel_Y);
            current = this;

            game.Components.Add(this);
        }

        public void Initialize()
        {
            terrain.Initialize();
            ui_manager.Initialize(Game);

            ui_manager.side_panel.Mouse_Over += () => {
                this.State = EditorState.Menu;
            };
            ui_manager.side_panel.Mouse_Exit += () => {
                this.State = EditorState.Running;
            };

            ui_manager.terrain_Menu.background.Mouse_Over += () => {
                this.State = EditorState.Menu;
                Debug.WriteLine("Working");
            };
            ui_manager.terrain_Menu.background.Mouse_Exit += () => {
                this.State = EditorState.Running;
                Debug.WriteLine("Working");
            };
        }

        public void Draw()
        {
            if (Globals.state == Game_State.Editor_Running && terrain != null)
            {
                terrain.Draw();
            }
            this.ui_manager.Draw();
        }

        public override void Update(GameTime time)
        {
            if (Globals.state == Game_State.Editor_Running && State != EditorState.Menu)
            {
                HandleMouseInput(Globals.Viewport);
            }

            ui_manager.Update();
        }

        private void HandleMouseInput(Matrix viewport)
        {
            var mstate = Input.Get_Mouse_State();
            Vector2 mouse = Globals.ScreenToWorldSpace(new Vector2(mstate.X, mstate.Y), viewport);

            for (int i = 0; i < terrain.Tile_Map.Count; i++)
            {
                for (int j = 0; j < terrain.Tile_Map[i].Count; j++)
                {
                    var Mouse_Rect = new Rectangle(mouse.ToPoint(), new Point(pixel_X / 8, pixel_Y / 8));
                    var nvect = new Vector2(terrain.Tile_Map[i][j].Position.X, terrain.Tile_Map[i][j].Position.Y);
                    var Object_Rect = new Rectangle(nvect.ToPoint(), new Point(pixel_X - pixel_X / 8, pixel_Y - pixel_Y / 8));

                    if (Mouse_Rect.Intersects(Object_Rect))
                    {
                        if (terrain.Tile_Map[i][j].is_empty)
                        {
                            terrain.Tile_Map[i][j].Texture = Engine_Textures.gui_textures["Prototype_Tile_Selected"];
                            Active_Tile.mouse_over = terrain.Tile_Map[i][j];
                        }

                        if (Input.MouseDown(MouseButton.Left))
                        {
                            if (terrain.Tile_Map[i][j].OnClick != null)
                            {
                                terrain.Tile_Map[i][j].OnClick.Invoke();
                            }
                            Active_Tile.tile = terrain.Tile_Map[i][j];
                        }
                        else if (Input.MouseHold(MouseButton.Left))
                        {
                            if (terrain.Tile_Map[i][j].OnHold != null)
                            {
                                terrain.Tile_Map[i][j].OnHold.Invoke();
                            }
                            Active_Tile.tile = terrain.Tile_Map[i][j];
                        }

                        // these are hardcoded editor functions
                        if (Input.MouseDown(MouseButton.Right))
                        {
                            terrain.Tile_Map[i][j].name = "air";
                            terrain.Tile_Map[i][j].is_empty = true;
                            terrain.Tile_Map[i][j].is_walkable = false;
                            terrain.Tile_Map[i][j].Texture = Engine_Textures.gui_textures["Prototype_Tile"];
                        }
                        else if (Input.MouseHold(MouseButton.Right))
                        {
                            terrain.Tile_Map[i][j].name = "air";
                            terrain.Tile_Map[i][j].is_empty = true;
                            terrain.Tile_Map[i][j].is_walkable = false;
                            terrain.Tile_Map[i][j].Texture = Engine_Textures.gui_textures["Prototype_Tile"];
                        }
                    }
                    else if (!Mouse_Rect.Intersects(Object_Rect) && terrain.Tile_Map[i][j].is_empty)
                    {
                        terrain.Tile_Map[i][j].Texture = Engine_Textures.gui_textures["Prototype_Tile"];
                    }
                }
            }
        }
        
        protected Texture2D CreateTexture(GraphicsDevice device, int width, int height, Func<int, Color> paint)
        {
            Texture2D texture = new Texture2D(device, width, height);
            Color[] data = new Color[width * height];

            for (int pixel = 0; pixel < data.Count(); pixel++)
            {
                data[pixel] = paint(pixel);
            }

            texture.SetData(data);
            return texture;
        }
    }

    public class Terrain
    {
        private int Map_Width = 0, Map_Height = 0;
        private int pixel_X = 0, pixel_Y = 0;
        private bool initialized = false;
        public List<List<Tile>> Tile_Map { get; set; }
        public List<List<Tile>> Scenery_Map { get; set; }

        public Terrain(float map_X, float map_Y, int pixel_width, int pixel_height)
        {
            pixel_X = pixel_width;
            pixel_Y = pixel_height;
            Map_Width = (int)map_X;
            Map_Height = (int)map_Y;
            this.Tile_Map = new List<List<Tile>>();
            this.Scenery_Map = new List<List<Tile>>();
        }

        public Terrain(List<List<Tile>> tiles, List<List<Tile>> scenery)
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
                /*
                    this section needs to be reworked so that vegetation can be added. 
                    essentially this is the vegetation layers7
                    
                    so essentially:
                        top_layer vegetation
                        bottom_layer terrain
                */

                for (int i = 0; i < Map_Width; i += pixel_X)
                {
                    List<Tile> tiles = new List<Tile>();
                    for (int j = 0; j < Map_Height; j += pixel_Y)
                    {
                        Tile tile = new Tile(i, j, Engine_Textures.gui_textures["Prototype_Tile"], "air");
                        tiles.Add(tile);
                    }
                    Tile_Map.Add(tiles);
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

                for (int i = 0; i < Scenery_Map.Count; i++)
                {
                    for (int j = 0; j < Scenery_Map[i].Count; j++)
                    {
                        Scenery_Map[i][j].Draw();
                    }
                }
            }
        }
    }
}
