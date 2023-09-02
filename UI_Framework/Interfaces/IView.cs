using Microsoft.Xna.Framework;
using old.UI_Framework;
using System.Collections.Generic;

namespace old.DinkleBurg.UI_Framework.Interfaces
{
    public interface IView
    {
        Vector2 Position { get; set; }
        Box background { get; set; }

        void Initialize(Vector2 pos, float height, float width);
        void Draw();
        void Update();
    }
}
