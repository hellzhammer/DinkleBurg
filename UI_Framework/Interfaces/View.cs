using Microsoft.Xna.Framework;
using System.Collections.Generic;
using UI_Framework;

namespace DinkleBurg.UI_Framework.Interfaces
{
    public abstract class View : IView
    {
        public int Width, Height;
        public Vector2 Position { get; set; }
        public Box background { get; set; }
        public Dictionary<string, Button> buttons { get; set; }

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
