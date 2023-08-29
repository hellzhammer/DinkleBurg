using Microsoft.Xna.Framework;
using System.Diagnostics;
using UI_Framework;

namespace DinkleBurg.Editor_Components
{
    public static class Editor_New_View
    {
        private static int Map_Size = 128;
        private static Label title { get; set; }

        //first collection // horizontal
        private static Label map_size_label { get; set; }
        private static TextBox map_size_input_textbox { get; set; }

        private static TextBox map_name_textbox { get; set; }

        private static Button start_button { get; set; }

        public static void Initialize(Game game)
        {
            map_size_label = new Label(
                "map_size",
                "Map Size",
                new Vector2(680, 15 + 60 + 60),
                100,
                30
                );

            map_size_input_textbox = new TextBox(
                "map_size",
                Map_Size.ToString(),
                new Vector2(map_size_label.Position.X + map_size_label.Width + 10, map_size_label.Position.Y),
                300 + 10,
                30
                )
            { numeric_input = true };

            start_button = new Button(
                "start",
                "Start",
                new Vector2(680, map_size_input_textbox.Height + 150),
                (int)map_size_label.Width + (int)map_size_input_textbox.Width + 10,
                30
                );

            title = new Label(
                "title",
                "Game Options",
                new Vector2(680, 15),
                (int)map_size_label.Width + (int)map_size_input_textbox.Width + 10,
                30
                );

            map_name_textbox = new TextBox(
                "map_name",
                "World Name",
                new Vector2(680, 15 + 60),
                (int)map_size_label.Width + (int)map_size_input_textbox.Width + 10,
                30
                )
            { numeric_input = false };

            map_name_textbox.Set_Background(Color.Gray);
            map_size_input_textbox.Set_Background(Color.Gray);
            map_size_label.Set_Background(Color.Gray);
            start_button.Set_Background(Color.DarkGreen);

            start_button.Click = () => {
                Debug.WriteLine("Starting Editor...");
                if (string.IsNullOrWhiteSpace(map_size_input_textbox.Content))
                {
                    map_size_input_textbox.Content = 0.ToString();
                }

                // stack overflow -- interger value to great??
                int val = int.Parse(map_size_input_textbox.Content);
                Editor.current = new Editor(val, val, game);
                Editor.current.Initialize();

                Globals.state = Game_State.Editor_Running;
            };
        }

        public static void Draw()
        {
            if (Globals.state == Game_State.Editor_Menu)
            {
				title.Draw(true);
				map_name_textbox.Draw(true);
				map_size_label.Draw(true);
				start_button.Draw(true);

				map_size_input_textbox.Draw(true);
			}
		}

        public static void Update()
        {
            if (Globals.state == Game_State.Editor_Menu)
            {
				map_name_textbox.Update();
				start_button.Update();
				map_size_input_textbox.Update();
			}
		}
    }
}
