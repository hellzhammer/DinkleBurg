using Microsoft.Xna.Framework;

namespace UI_Framework
{
    public class Box : Widget
    {
        public bool is_horizontal = false;
        /*public List<Widget> Children { get; protected set; }*/

        public Box(string name, Vector2 pos, float width, float height)
        {
            //this.Children = new List<Widget>();
            this.Height = height;
            this.Width = width;

            this.name = name;
            this.Position = pos;
            this.Initialize();
        }

        public override void Draw(bool simple_draw)
        {
            base.Draw(simple_draw);
            /*if (this.Children != null)
            {
                for (int c = 0; c < this.Children.Count; c++)
                {
                    Children[c].Draw();
                }
            }
            else
            {
                throw new System.Exception("Children cannot be null!");
            }*/
        }

        public override void Update()
        {
            var mstate = Input.Get_Mouse_State();
            Vector2 mouse = new Vector2(mstate.X, mstate.Y);
            OnMouseOver(mouse);
        }

        private void Add_Child(Widget child)
        {
            /*if (this.Children == null)
            {
                throw new System.Exception("Children cannot be null.");
            }
            if (child == null)
            {
                throw new System.Exception("Child cannot be null.");
            }

            this.Children.Add(child);*/
        }
    }
}
