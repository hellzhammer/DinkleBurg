using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace UI_Framework
{
    public class Box : Widget
    {
        public bool stretch_items_to_fit = true;
        public bool centered_items = false;
        //public bool is_horizontal = false;
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

        public void AddChild(Widget widget)
        {
            if (!this.Children.Contains(widget))
            {
                if (stretch_items_to_fit)
                {
                    widget.Width = this.Width;
                }

                if (centered_items)
                {
                    var y = this.Position.Y;
                    if (this.Children.Count > 0)
                    {
                        // continue pos
                        y = this.Children[Children.Count - 1].Position.Y + widget.Height + widget.Height / 2;
                    }
                    Vector2 v2 = new Vector2(this.Position.X + this.Width / 2, y) ;
                    widget.Position = v2;
                }

                this.Children.Add(widget);
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
