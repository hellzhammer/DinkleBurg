using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;

namespace old.UI_Framework
{
    public abstract class UI_Object
    {
        public string name { get; set; }
        public Vector2 Position { get; set; }

        public float Width { get; set; }
        public float Height { get; set; }
        private readonly float font_char_size = 14;

        public void SetWidth(float width)
        {
            this.Width = width + this.font_char_size;
        }

        public void SetHeight(float height)
        {
            this.Height = height + this.font_char_size;
        }

        public virtual void Update() { }

        protected Texture2D CreateTexture(GraphicsDevice device, int width, int height, Func<int, Color> paint)
        {
            Texture2D texture = new Texture2D(device, width, height);
            Color[] data = new Color[width * height];

            for (int pixel = 0; pixel < data.Count(); pixel++)
            {
                data[pixel] = paint(pixel);
            }

            texture.SetData(data);
            return texture;
        }
    }
}
