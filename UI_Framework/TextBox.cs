using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UI_Framework
{
    public class TextBox : Widget
    {
        public bool numeric_input = false;

        public string Content { get; set; }
        public Color font_color = Color.White;

        public System.Action OnActivated { get; set; }
        public System.Action OnDeactivated { get; set; }

        // width / 12 = charcount
        private const int text_char_width = 9;
        private const int text_char_height = 18;
        private string display_string = string.Empty;

        public TextBox(string name, string content, Vector2 pos, int width, int height)
        {
            this.Height = height;
            if (this.Height < text_char_height)
                this.Height = text_char_height + text_char_width;

            this.Width = width;

            this.name = name;
            this.Position = pos;
            this.Content = content;

            Initialize();
        }

        public override void Update()
        {
            display_string = string.Empty;
            int max = (int)Width - 18;
            
            int cur_len = 0;
            for (int i = 0; i < Content.Length; i++)
            {
                var len = Globals.Game_Font.MeasureString(display_string + Content[i]);
                if (len.X < max)
                {
                    display_string += Content[i];
                }
                else
                    break;
            }
            Content = display_string;

            var mstate = Input.Get_Mouse_State();
            Vector2 mouse = new Vector2(mstate.X, mstate.Y);
            OnMouseOver(mouse);
            HandleActivation();
        }

        private void HandleActivation()
        {
            if (this.is_mouse_over && Input.MouseDown(MouseButton.Left))
            {
                TextInputManger.active_element = this;
                if (this.OnActivated != null)
                    OnActivated.Invoke();
            }
            else if(!this.is_mouse_over && Input.MouseDown(MouseButton.Left))
            {
                if (TextInputManger.active_element == this)
                {
                    TextInputManger.active_element = null;
                    if (this.OnDeactivated != null)
                        OnDeactivated.Invoke();
                }
            }
        }

        public override void Draw(bool simple_draw)
        {
            base.Draw(simple_draw);
            if (!string.IsNullOrWhiteSpace(Content))
            {
                Globals.Sprite_Batch.DrawString(
                    Globals.Game_Font,
                    display_string,
                    Globals.ScreenToWorldSpace(new Vector2(Position.X + 5, Position.Y + 5), Globals.Viewport),
                    font_color,
                    0,
                    Origin,
                    1.0f,
                    SpriteEffects.None,
                    0.5f
                    );
            }
        }
    }
}
