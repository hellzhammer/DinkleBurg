using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace UI_Framework
{
    public enum WidgetOrientation
    {
        Vertical,
        Horizontal
    }
    public class Box : Widget
    {
        public WidgetOrientation Orientation = WidgetOrientation.Vertical;
        public int padding = 0;
        
        private List<Widget> Children { get; set; }

        public Box(string name, Vector2 pos, float width, float height)
        {
            this.Children = new List<Widget>();
            this.Height = height;
            this.Width = width;

            this.name = name;
            this.Position = pos;
            this.Initialize();
        }

        public void AddChild(Widget widget, bool stretch_items_to_fit = false, bool centered_items = true)
        {
            if (this.Orientation == WidgetOrientation.Vertical)
            {
                if (!this.Children.Contains(widget))
                {
                    if (stretch_items_to_fit)
                    {
                        widget.Width = this.Width;
                    }

                    if (centered_items && !stretch_items_to_fit)
                    {
                        var y = this.Position.Y;
                        if (this.Children.Count > 0)
                        {
                            // continue pos
                            y = this.Children[Children.Count - 1].Position.Y + widget.Height - 16 + padding;
                        }
                        Vector2 v2 = new Vector2(this.Position.X + (this.Width / 2) - 16, y);
                        widget.Position = v2;
                    }

                    this.Children.Add(widget);
                }
            } 
            else if (this.Orientation == WidgetOrientation.Horizontal)
            {
                if (!this.Children.Contains(widget))
                {
                    if (stretch_items_to_fit)
                    {
                        widget.Width = this.Width;
                    }

                    if (centered_items && !stretch_items_to_fit)
                    {
                        var x = this.Position.X;
                        if (this.Children.Count > 0)
                        {
                            // continue pos
                            x = this.Children[Children.Count - 1].Position.X + widget.Width - 16 + padding;
                        }
                        Vector2 v2 = new Vector2(x, this.Position.Y);
                        widget.Position = v2;
                    }

                    this.Children.Add(widget);
                }
            }
        }

        public void KillChild(Widget child)
        {
            if (this.Children.Contains(child))
            {
                this.Children.Remove(child);
            }
        }

        public override void Draw(bool simple_draw)
        {
            base.Draw(simple_draw);
            if (this.Children != null)
            {
                for (int c = 0; c < this.Children.Count; c++)
                {
                    Children[c].Draw(true);
                }
            }
        }

        public override void Update()
        {
            var mstate = Input.Get_Mouse_State();
            Vector2 mouse = new Vector2(mstate.X, mstate.Y);
            OnMouseOver(mouse);
        }
    }
}
