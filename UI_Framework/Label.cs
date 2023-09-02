using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace old.UI_Framework
{
    public class Label : Widget
    {
        public string Content { get; set; }
        public Color font_color = Color.White;
        public Label(string name, string content, Vector2 pos, int width, int height)
        {
            this.Height = height;
            this.Width = width;

            this.name = name;
            this.Position = pos;
            this.Content = content;

            this.Initialize();
        }

        public override void Draw(bool simple_draw)
        {
            base.Draw(simple_draw);

            if (!string.IsNullOrWhiteSpace(Content))
            {
                Globals.Sprite_Batch.DrawString(
                    Globals.Game_Font, 
                    Content, 
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
