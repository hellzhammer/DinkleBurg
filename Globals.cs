using DinkleBurg.Editor_Components;
using DinkleBurg.Map_Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Threading.Tasks;
using UI_Framework;

public static class Engine_Textures
{
    public static Dictionary<string, Texture2D> textures { get; set; }
} 

public static class Globals
{
    public static Game_State state = Game_State.MainMenu;

    public static GraphicsDeviceManager DeviceManager { get; set; }
    public static SpriteBatch Sprite_Batch { get; set; }
    public static SpriteFont Game_Font { get; set; }
    public static Matrix Viewport { get; set; }

    public static void Load(ContentManager content, Game game)
    {
        Game_Font = content.Load<SpriteFont>("GameFont");

        // here load all game textures to be used in the engine/games
        Engine_Textures.textures = new Dictionary<string, Texture2D>();
        Engine_Textures.textures.Add("Prototype_Tile", content.Load<Texture2D>("Editor/Engine_Textures/Prototype_Tile"));
        Engine_Textures.textures.Add("Prototype_Tile_Selected", content.Load<Texture2D>("Editor/Engine_Textures/Prototype_Tile_Selected"));

        // load the camera logic
        Camera cam = new Camera(Globals.DeviceManager.GraphicsDevice.Viewport);
        
        // init the UI intiial views
        MainMenu.Initialize();
        EditorMenu.Initialize();
        PlayMenu.Initialize();
        Editor_New_View.Initialize(game);
    }

    public static Vector2 ScreenToWorldSpace(Vector2 point, Matrix _viewport)
    {
        Matrix invertedMatrix = Matrix.Invert(_viewport);
        return Vector2.Transform(point, invertedMatrix);
    }

    public static void Update(GameTime time)
    {
        Parallel.Invoke(
            new ParallelOptions() { MaxDegreeOfParallelism = 1 },
            new System.Action[] { 
                () => { Camera.main_camera.Update(); Viewport = Camera.main_camera.GetViewMatrix(); },
                ()=>{ Input.Update(); },
                ()=>{ MainMenu.Update(); },
                ()=>{ EditorMenu.Update(); },
                ()=>{ Editor_New_View.Update(); },
                ()=>{ PlayMenu.Update(); },
            });
    }

    public static void Draw()
    {
        Parallel.Invoke(
            new ParallelOptions() { MaxDegreeOfParallelism = 1 },
            new System.Action[] {
                ()=>{ MainMenu.Draw(); },
                ()=>{ EditorMenu.Draw(); },
                ()=>{ PlayMenu.Draw(); },
                ()=>{ Editor_New_View.Draw(); },
                () =>
                {
                    if (Editor.current != null)
                    {
                        Editor.current.Draw();
                    }
                }
            });
    }
}

public static class Active_Tile
{
    public static Tile tile { get; set; }
    public static Tile mouse_over { get; set; }
}

public static class TextInputManger
{
    public static TextBox active_element { get; set; }
}

public enum Game_State
{
    Save,
    Load,
    MainMenu,
    Settings,
    Start_Game_Menu,
    Editor_Menu,
    Editor_New_Menu,
    Editor_Running
}
