using Microsoft.Xna.Framework;
using System.Diagnostics;
using UI_Framework;

public static class MainMenu
{
    private static Box Background { get; set; }
    private static Label Title { get; set; }
    private static Button Game_Start_Button { get; set; }
    private static Button Editor_Button { get; set; }
    private static Button Settings_Button { get; set; }

    public static void Initialize()
    {
        Background = new Box(
            "background",
            new Vector2(0, 0),
            Globals.DeviceManager.GraphicsDevice.DisplayMode.Width,
            Globals.DeviceManager.GraphicsDevice.DisplayMode.Height
            );

        Title = new Label(
            "title",
            "Empires",
            new Vector2(10, 15),
            300,
            30
            );
        Title.Set_Background(Color.Transparent);
        Game_Start_Button = new Button(
            "start_button",
            "Play",
            new Vector2(10, 60),
            300,
            50
            );
        Editor_Button = new Button(
            "editor_button",
            "Editor",
            new Vector2(10, 125),
            300,
            50
            );
        Settings_Button = new Button(
            "settings_button",
            "Settings",
            new Vector2(10, 190),
            300,
            50
            );

        Game_Start_Button.Set_Background(Color.Gray);
        Editor_Button.Set_Background(Color.Gray);
        Settings_Button.Set_Background(Color.Gray);

        // onclicks to be set up here
        Game_Start_Button.Click = () => {
            Globals.state = Game_State.Start_Game_Menu;
        };
        Editor_Button.Click = () =>
        {
            Globals.state = Game_State.Editor_Menu;
        };
        Settings_Button.Click = () =>
        {
            Globals.state = Game_State.Settings;
        };
    }

    public static void Update()
    {
        if (
            Globals.state == Game_State.Editor_New_Menu 
            || 
            Globals.state == Game_State.Settings 
            || 
            Globals.state == Game_State.MainMenu 
            || 
            Globals.state == Game_State.Editor_Menu 
            || 
            Globals.state == Game_State.Start_Game_Menu
            )
        {
            Game_Start_Button.Update();
            Editor_Button.Update();
            Settings_Button.Update();
        }
    }

    public static void Draw()
    {
        if (
            Globals.state == Game_State.Editor_New_Menu 
            || 
            Globals.state == Game_State.Settings 
            || 
            Globals.state == Game_State.MainMenu 
            || 
            Globals.state == Game_State.Editor_Menu 
            || 
            Globals.state == Game_State.Start_Game_Menu
            )
        {
            Background.Draw(true);
            Title.Draw(true);
            Game_Start_Button.Draw(true);
            Editor_Button.Draw(true);
            Settings_Button.Draw(true);
        }
    }
}

public static class EditorMenu
{
    public static Label Title { get; set; }
    public static Button new_button { get; set; }
    public static Button load_button { get; set; }

    public static void Initialize()
    {
        Title = new Label(
            "title",
            "Editor",
            new Vector2(340, 15),
            300,
            30
            );
        Title.Set_Background(Color.Transparent);
        new_button = new Button(
            "editor_new_button",
            "New",
            new Vector2(340, 60),
            300,
            50
            );
        load_button = new Button(
            "editor_load_button",
            "Load",
            new Vector2(340, 125),
            300,
            50
            );

        new_button.Set_Background(Color.Gray);
        load_button.Set_Background(Color.Gray);

        // onclicks to be set up here
        new_button.Click = () => {
            Globals.state = Game_State.Editor_New_Menu;
        };
        load_button.Click = () =>
        {
            Debug.WriteLine("Working");
        };
    }

    public static void Update()
    {
        if (
            Globals.state == Game_State.Editor_New_Menu 
            || 
            Globals.state == Game_State.Editor_Menu
            )
        {
            new_button.Update();
            load_button.Update();
        }
    }

    public static void Draw()
    {
        if (
            Globals.state == Game_State.Editor_New_Menu 
            || 
            Globals.state == Game_State.Editor_Menu
            )
        {
            Title.Draw(true);
            new_button.Draw(true);
            load_button.Draw(true);
        }
    }
}

public static class PlayMenu
{
    public static Label Title { get; set; }
    public static Button new_button { get; set; }
    public static Button load_button { get; set; }

    public static void Initialize()
    {
        Title = new Label(
            "title",
            "Play",
            new Vector2(340, 15),
            300,
            30
            );
        Title.Set_Background(Color.Transparent);
        new_button = new Button(
            "play_new_button",
            "New Game",
            new Vector2(340, 60),
            300,
            50
            );
        load_button = new Button(
            "play_load_button",
            "Load Game",
            new Vector2(340, 125),
            300,
            50
            );

        new_button.Set_Background(Color.Gray);
        load_button.Set_Background(Color.Gray);

        // onclicks to be set up here
        new_button.Click = () => {
            Debug.WriteLine("Working");
        };
        load_button.Click = () =>
        {
            Debug.WriteLine("Working");
        };
    }

    public static void Update()
    {
        if (Globals.state == Game_State.Start_Game_Menu)
        {
            new_button.Update();
            load_button.Update();
        }
    }

    public static void Draw()
    {
        if (Globals.state == Game_State.Start_Game_Menu)
        {
            Title.Draw(true);
            new_button.Draw(true);
            load_button.Draw(true);
        }
    }
}
