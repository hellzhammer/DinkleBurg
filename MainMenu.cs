﻿using Engine_lib.UI_Framework;
using Microsoft.Xna.Framework;

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
            Globals.DeviceManager.GraphicsDevice.DisplayMode.Height,
            Globals.DeviceManager.GraphicsDevice
            );

        Title = new Label(
            "title",
            "Empires",
            new Vector2(10, 15),
            300,
            30,
			Globals.DeviceManager.GraphicsDevice
			);
        Title.Set_Background(Color.Transparent, Globals.DeviceManager.GraphicsDevice);
        Game_Start_Button = new Button(
            "start_button",
            "Play",
            new Vector2(10, 60),
            300,
            50,
			Globals.DeviceManager.GraphicsDevice
			);
        Editor_Button = new Button(
            "editor_button",
            "Editor",
            new Vector2(10, 125),
            300,
            50,
			Globals.DeviceManager.GraphicsDevice
			);
        Settings_Button = new Button(
            "settings_button",
            "Settings",
            new Vector2(10, 190),
            300,
            50,
			Globals.DeviceManager.GraphicsDevice
			);

        Game_Start_Button.Set_Background(Color.Gray,
			Globals.DeviceManager.GraphicsDevice);
        Editor_Button.Set_Background(Color.Gray,
			Globals.DeviceManager.GraphicsDevice);
        Settings_Button.Set_Background(Color.Gray,
			Globals.DeviceManager.GraphicsDevice);

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

		Background.AddChild(Title);
		Background.AddChild(Game_Start_Button);
		Background.AddChild(Editor_Button);
		Background.AddChild(Settings_Button);
	}

    public static void Update()
    {
        if (
            Globals.state == Game_State.Settings // to remove
            || 
            Globals.state == Game_State.MainMenu 
            || 
            Globals.state == Game_State.Start_Game_Menu // to remove
            )
        {
            Background.Update();
        }
    }

    public static void Draw()
    {
        if (
            Globals.state == Game_State.Settings // to remove
            || 
            Globals.state == Game_State.MainMenu 
            || 
            Globals.state == Game_State.Start_Game_Menu // to remove
            )
        {
            Background.Draw(true, Globals.Sprite_Batch, Globals.Viewport, Globals.Game_Font);
        }
    }
}
