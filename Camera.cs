using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

public class Camera
{
    /// <summary>
    /// main camera singleton
    /// </summary>
    public static Camera main_camera { get; set; }
    public float move_speed = 3;

    public readonly Viewport _viewport;

    public Camera(Viewport viewport)
    {
        _viewport = viewport;
        this.transform = Vector2.Zero;
        Zoom = 1;
        Origin = new Vector2(_viewport.Width / 2f, _viewport.Height / 2f);

        main_camera = this;
    }

    public Vector2 transform { get; set; }

    public float Zoom { get; set; }
    public Vector2 Origin { get; set; }

    public Matrix GetViewMatrix()
    {
        return
            Matrix.CreateTranslation(new Vector3(-this.transform, 0.0f)) *
            Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) *
            //Matrix.CreateRotationZ(MathHelper.ToRadians(45f)) * // altered
            Matrix.CreateRotationZ(0) * // == original
            Matrix.CreateScale(Zoom, Zoom, 1) *
            Matrix.CreateTranslation(new Vector3(Origin, 0.0f));
    }

    public void Update(/*Terrain terrain*/)
    {
        if (Input.Allow_Keyboard)
        {
            if (Input.KeyUp(Keys.OemPlus) || Input.KeyHold(Keys.OemPlus))
                Zoom += 0.1f;
            else if (Input.KeyUp(Keys.OemMinus) || Input.KeyHold(Keys.OemMinus))
                Zoom -= 0.1f;

            if (Zoom < 1)
                Zoom = 1;

            Vector2 nvect = transform;
            // movement
            var speed = move_speed;
            if (Input.KeyHold(Keys.LeftShift))
            {
                speed *= 2;
            }
            if (Input.KeyHold(Keys.Up) || Input.KeyHold(Keys.W))
                nvect.Y -= speed;

            else if (Input.KeyHold(Keys.Down) || Input.KeyHold(Keys.S))
                nvect.Y += speed;

            if (Input.KeyHold(Keys.Left) || Input.KeyHold(Keys.A))
                nvect.X -= speed;

            else if (Input.KeyHold(Keys.Right) || Input.KeyHold(Keys.D))
                nvect.X += speed;

            /*if (nvect.X > Map_Width - _viewport.Width / 2)
            {
                nvect.X = Map_Width - _viewport.Width / 2;
            }
            if (nvect.Y > Map_Height - _viewport.Height / 2)
            {
                nvect.Y = Map_Height - _viewport.Height / 2;
            }
            if (nvect.X < 0 - _viewport.Width / 2)
            {
                nvect.X = 0 - _viewport.Width / 2;
            }
            if (nvect.Y < 0 - _viewport.Height / 2)
            {
                nvect.Y = 0 - _viewport.Height / 2;
            }*/
            this.transform = nvect;
        }
    }
}
