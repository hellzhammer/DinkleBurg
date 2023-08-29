using DinkleBurg.Map_Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;

namespace DinkleBurg.Editor_Components
{
    public class Editor : GameComponent
    {
        public class Activated
        {
            public string selected_texture_name { get; set; }
            public Texture2D selected_texture { get; set; }
			public Resource_Type curr_type = Resource_Type.none;
			public Editor_UI_Manager.TabState curr_tab_state = Editor_UI_Manager.TabState.none;
        }

        private enum EditorState
        {
            Running,
            Menu
        }
        public Activated tile_manager { get; set; }
        private EditorState State = EditorState.Running;
        public static Editor current { get; set; }

        private int pixel_X = 32;
        private int pixel_Y = 32;
        private Terrain terrain { get; set; }
        private Editor_UI_Manager ui_manager { get; set; }

        public Editor(int Map_Width, int Map_Height, Game game) : base(game)
        {
            current = this;

            this.tile_manager = new Activated();
            this.ui_manager = new Editor_UI_Manager();
            terrain = new Terrain(Map_Width, Map_Height, pixel_X, pixel_Y);

            game.Components.Add(this);
        }

        public void Save()
        {
            this.terrain.Save();
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
            };
            ui_manager.terrain_Menu.background.Mouse_Exit += () => {
                this.State = EditorState.Running;
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
                            terrain.Tile_Map[i][j].Texture = Engine_Texture_Loader.gui_textures["Prototype_Tile_Selected"];
                            Active_Tile.mouse_over = terrain.Tile_Map[i][j];
                        }

                        if (Input.MouseDown(MouseButton.Left) || Input.MouseHold(MouseButton.Left))
                        {
                            if (tile_manager.curr_tab_state == Editor_UI_Manager.TabState.terrain)
                            {
                                if (terrain.Tile_Map[i][j].OnClick != null)
                                {
                                    terrain.Tile_Map[i][j].OnClick.Invoke();
                                }
                                Active_Tile.tile = terrain.Tile_Map[i][j];
                            }
                            else if (tile_manager.curr_tab_state == Editor_UI_Manager.TabState.vegetation)
                            {
                                terrain.Scenery_Map[i][j] = new Resource((int)terrain.Tile_Map[i][j].Position.X, (int)terrain.Tile_Map[i][j].Position.Y, tile_manager.selected_texture, tile_manager.selected_texture_name, Editor.current.tile_manager.curr_type);
                            }
                        }

                        // these are hardcoded editor functions
                        if (Input.MouseDown(MouseButton.Right) || Input.MouseHold(MouseButton.Right))
                        {
                            if (tile_manager.curr_tab_state == Editor_UI_Manager.TabState.terrain)
                            {
                                terrain.Tile_Map[i][j].name = "air";
                                terrain.Tile_Map[i][j].is_empty = true;
                                terrain.Tile_Map[i][j].is_walkable = false;
                                terrain.Tile_Map[i][j].Texture = Engine_Texture_Loader.gui_textures["Prototype_Tile"];
                            }
                            else if (tile_manager.curr_tab_state == Editor_UI_Manager.TabState.vegetation)
                            {
                                terrain.Scenery_Map[i][j] = null;
                            }
                        }
                    }
                    else if (!Mouse_Rect.Intersects(Object_Rect) && terrain.Tile_Map[i][j].is_empty)
                    {
                        terrain.Tile_Map[i][j].Texture = Engine_Texture_Loader.gui_textures["Prototype_Tile"];
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
}
