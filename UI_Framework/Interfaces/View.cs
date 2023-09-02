using Microsoft.Xna.Framework;
using old.UI_Framework;

namespace old.DinkleBurg.UI_Framework.Interfaces
{
    public abstract class View : IView
    {
        public int Width, Height;
        public Vector2 Position { get; set; }
        public Box background { get; set; }

        public virtual void Initialize(Vector2 pos, float height, float width)
        {

        }

        public virtual void Draw()
        {

        }

        public virtual void Update()
        {

        }
    }
}
