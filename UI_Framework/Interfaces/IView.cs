using Microsoft.Xna.Framework;
using System.Collections.Generic;
using UI_Framework;

namespace DinkleBurg.UI_Framework.Interfaces
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
