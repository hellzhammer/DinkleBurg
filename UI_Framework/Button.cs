using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UI_Framework
{
    public class Button : Widget
    {
        public string Content { get; set; }
        public Color font_color = Color.White;

        public Button(string name, string content, Vector2 pos, int width, int height)
        {
            this.Height = height;
            this.Width = width;

            this.name = name;
            this.Position = pos;
            this.Content = content;

            Initialize();
        }

        public override void Update()
        {
            var mstate = Input.Get_Mouse_State();
            Vector2 mouse = new Vector2(mstate.X, mstate.Y);
            OnMouseOver(mouse);
            OnClick();
        }

        private void OnClick()
        {
            if (Click != null)
            {
                if (this.is_mouse_over && Input.MouseDown(MouseButton.Left))
                {
                    this.Click.Invoke();
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
                    Content, 
                    Globals.ScreenToWorldSpace(new Vector2(Position.X + 5, Position.Y + 5), 
                    Globals.Viewport), 
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
