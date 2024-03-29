﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace tacocopterbase
{
    public class MouseHandler
    {
        public Vector2 Position;
        public MouseButton[] Buttons;
        private MouseState m_MouseState;
        private MouseState m_MouseStateLast;

        public MouseHandler()
        {
            Buttons = new MouseButton[2];
            for (int i = 0; i < 2; i++)
            {
                Buttons[i] = new MouseButton();
            }
        }

        public void Update(GameTime gameTime)
        {
            m_MouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();
            Position = new Vector2(m_MouseState.X, m_MouseState.Y);

            Buttons[0].Update(m_MouseState.LeftButton, m_MouseStateLast.LeftButton);
            Buttons[1].Update(m_MouseState.RightButton, m_MouseStateLast.RightButton);

            m_MouseStateLast = m_MouseState;
        }
    }

    public class MouseButton
    {
        public bool IsDown;
        public bool Press;
        public bool Release;

        internal void Update(ButtonState nStateCurrent, ButtonState nStateLast)
        {
            Press = false;
            Release = false;

            if (nStateCurrent == ButtonState.Pressed)
            {
                IsDown = true;
                if (nStateLast == ButtonState.Released)
                    Press = true;
            }
            else
            {
                IsDown = false;
                if (nStateLast == ButtonState.Pressed)
                    Release = true;
            }
        }
    }
}
