using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    public static class Input
    {
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);

        public static Dictionary<Keys, bool> PressedKeys { get; set; } = new Dictionary<Keys, bool>();

        public static void ReadInput()
        {
            if (Input.KeyPressed(Keys.Right))
            {
                PressedKeys[Keys.Right] = true;
            }
            else
            {
                PressedKeys[Keys.Right] = false;
            }

            if (Input.KeyPressed(Keys.Left))
            {
                PressedKeys[Keys.Left] = true;
            }
            else
            {

                PressedKeys[Keys.Left] = false;

            }


            if (Input.KeyPressed(Keys.Space))
            {
                PressedKeys[Keys.Space] = true;
            }
            else
            {
                PressedKeys[Keys.Space] = false;
            }
        }

        public static bool KeyPressed(Keys key)
        {
            return (GetAsyncKeyState((int)key) & 0x8000) != 0;

        }
    }
}
