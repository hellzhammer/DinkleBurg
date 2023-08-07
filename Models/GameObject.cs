using Microsoft.Xna.Framework;
using System;

namespace DinkleBurg.Models
{
    public class GameObject
    {
        public Action OnClick { get; set; }
        public Action OnHold { get; set; }

        public string name { get; set; }
        public Vector2 Position { get; set; }

        public GameObject() { }
        public GameObject(string name, Vector2 pos)
        {
            this.name = name;
            this.Position = pos;
        }
        public GameObject(float x, float y, string name)
        {
            this.Position = new Vector2(x, y);
            this.name = name;
        }

        public virtual void Update() { }
    }
}
