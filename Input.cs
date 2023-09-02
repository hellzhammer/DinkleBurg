/*using Microsoft.Xna.Framework.Input;

public enum MouseButton
{
    Left, Right, Middle
}
public static class Input
{
    public static bool Allow_Keyboard = true;
    private static KeyboardState key_board_state { get; set; }
    private static MouseState mouse_state { get; set; }

    private static KeyboardState last_key_board_state { get; set; }
    private static MouseState last_mouse_state { get; set; }

    public static MouseState Get_Mouse_State()
    {
        return mouse_state;
    }

    public static KeyboardState Get_Keyboard_State()
    {
        return key_board_state;
    }

    public static void Update()
    {
        last_mouse_state = mouse_state;
        last_key_board_state = key_board_state;
        key_board_state = Keyboard.GetState();
        mouse_state = Mouse.GetState();
    }

    public static bool KeyUp(Keys key)
    {
        return last_key_board_state.IsKeyDown(key) && key_board_state.IsKeyUp(key);
    }

    public static bool MouseUp(MouseButton mouse)
    {
        bool rtn = false;
        if (mouse == MouseButton.Left)
        {
            var last_left = last_mouse_state.LeftButton;
            var curr_left = mouse_state.LeftButton;
            if (last_left == ButtonState.Pressed && curr_left != ButtonState.Pressed)
            {
                rtn = true;
            }
        }
        else if (mouse == MouseButton.Right)
        {
            var last_right = last_mouse_state.RightButton;
            var curr_right = mouse_state.RightButton;
            if (last_right == ButtonState.Pressed && curr_right != ButtonState.Pressed)
            {
                rtn = true;
            }
        }

        return rtn;
    }

    public static bool KeyDown(Keys key)
    {
        return !last_key_board_state.IsKeyDown(key) && key_board_state.IsKeyDown(key);
    }

    public static bool MouseDown(MouseButton mouse)
    {
        bool rtn = false;
        if (mouse == MouseButton.Left)
        {
            var last_left = last_mouse_state.LeftButton;
            var curr_left = mouse_state.LeftButton;
            if (last_left != ButtonState.Pressed && curr_left == ButtonState.Pressed)
            {
                rtn = true;
            }
        }
        else if (mouse == MouseButton.Right)
        {
            var last_right = last_mouse_state.RightButton;
            var curr_right = mouse_state.RightButton;
            if (last_right != ButtonState.Pressed && curr_right == ButtonState.Pressed)
            {
                rtn = true;
            }
        }

        return rtn;
    }

    public static bool KeyHold(Keys key)
    {
        return key_board_state.IsKeyDown(key) && last_key_board_state.IsKeyDown(key);
    }

    public static bool MouseHold(MouseButton mouse)
    {
        bool rtn = false;
        if (mouse == MouseButton.Left)
        {
            var last_left = last_mouse_state.LeftButton;
            var curr_left = mouse_state.LeftButton;
            if (last_left == ButtonState.Pressed && curr_left == ButtonState.Pressed)
            {
                rtn = true;
            }
        }
        else if (mouse == MouseButton.Right)
        {
            var last_right = last_mouse_state.RightButton;
            var curr_right = mouse_state.RightButton;
            if (last_right == ButtonState.Pressed && curr_right == ButtonState.Pressed)
            {
                rtn = true;
            }
        }

        return rtn;
    }
}
*/