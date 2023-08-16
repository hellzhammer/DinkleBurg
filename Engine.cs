using DinkleBurg.Editor_Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace DinkleBurg;

public class Engine : Game
{
    public Engine()
    {
        Globals.DeviceManager = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        this.Window.TextInput += (s, a) => {
            // find active ui element and update with keyboard input.
            if (UIInputManager.active_element != null)
            {
                Debug.WriteLine(a.Character);
                Regex r = new Regex("^[a-zA-Z0-9]*$");
                if (r.IsMatch(a.Character.ToString()) && !char.IsPunctuation(a.Character))
                {
                    if (!UIInputManager.active_element.numeric_input)
                    {
                        UIInputManager.active_element.Content += a.Character;
                    }
                    else
                    {
                        if (UIInputManager.active_element.numeric_input && char.IsNumber(a.Character))
                        {
                            UIInputManager.active_element.Content += a.Character;
                        }
                    }
                }
                else if (!r.IsMatch(a.Character.ToString()) && !char.IsPunctuation(a.Character))
                {
                    if (a.Key == Keys.Back)
                    {
                        var _s = UIInputManager.active_element.Content;
                        string n_string = string.Empty;
                        for (int i = 0; i < _s.Length - 1; i++)
                        {
                            n_string += _s[i];
                        }
                        UIInputManager.active_element.Content = n_string;
                    }
                    else if (a.Key == Keys.Space)
                    {
                        UIInputManager.active_element.Content += " ";
                    }
                    else if (a.Key == Keys.Enter)
                    {
                        UIInputManager.active_element = null;
                    }
                }
                else if (!r.IsMatch(a.Character.ToString()) && char.IsPunctuation(a.Character))
                {
                    UIInputManager.active_element.Content += a.Character;
                }
            }
        };
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        Globals.DeviceManager.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
        Globals.DeviceManager.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
        Globals.DeviceManager.ApplyChanges();

        this.Window.Title = "God Simulator";
        this.Window.AllowUserResizing = true;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        // TODO: use this.Content to load your game content here
        Globals.Load(Content, this);
        Globals.Sprite_Batch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            Globals.state = Game_State.MainMenu;
            if (Editor.current != null)
            {
                Editor.current = null;
            }
        }

        // TODO: Add your update logic here
        Globals.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        Globals.Sprite_Batch.Begin(transformMatrix: Globals.Viewport);

        //draw all objects
        Globals.Draw();

        // end batch
        Globals.Sprite_Batch.End();

        base.Draw(gameTime);
    }
}
