using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UI_Framework
{
    public enum WidgetOrientation
    {
        Vertical,
        Horizontal
    }

    public abstract class Widget : UI_Object
    {
        public bool is_mouse_over { get; protected set; }
        public System.Action Mouse_Over { get; set; }
        public System.Action Mouse_Exit { get; set; }
        public System.Action Click { get; set; }
        public Texture2D background { get; set; }
        public Color background_color = Color.Black;
        public Rectangle rect { get; set; }
        protected Vector2 Origin { get; set; }

        /// <summary>
        /// Call after Your Code has been run.
        /// </summary>
        public virtual void Initialize()
        {
            this.is_mouse_over = false;
            SetHeight(this.Height);
            SetWidth(this.Width);
            Set_Background(background_color);
            this.rect = new Rectangle(this.Position.ToPoint(), new Point((int)this.Width, (int)this.Height));
            this.Origin = new Vector2(0, 0);
        }

        public void Set_Background(Color color)
        {
            this.background_color = color;
            this.background = this.CreateTexture(Globals.DeviceManager.GraphicsDevice, (int)this.Width, (int)this.Height, pixel => background_color);
        }

        protected void OnMouseOver(Vector2 mouse)
        {
            var r = new Rectangle(mouse.ToPoint(), this.Origin.ToPoint());
            if (r.Intersects(this.rect) && !this.is_mouse_over)
            {
                this.is_mouse_over = true;
                if (Mouse_Over != null)
                {
                    this.Mouse_Over.Invoke();
                }
            }
            else if (!r.Intersects(this.rect) && this.is_mouse_over)
            {
                this.is_mouse_over = false;
                if (this.Mouse_Exit != null)
                {
                    Mouse_Exit.Invoke();
                }
            }
        }

        public virtual void Draw(bool simple_draw)
        {
            if (this.background != null)
            {
                if (simple_draw)
                    Globals.Sprite_Batch.Draw(background, Globals.ScreenToWorldSpace(Position, Globals.Viewport), Color.White); // this works for menu, where below does not.
                else if (simple_draw)
                    Globals.Sprite_Batch.Draw(background, Globals.ScreenToWorldSpace(Position, Globals.Viewport), this.rect, Color.White, 0.0f, Origin, 1, SpriteEffects.None, 1);
            }
        }
    }
}
