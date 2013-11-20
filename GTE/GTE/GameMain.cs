using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GTE
{
    class GameMain
    {
        // FIELDS
        Player LocalPlayer;

        // CONSTRUCTOR
        public GameMain()
        {
            LocalPlayer = new Player();
        }

        // METHODS

        // UPDTA & DRAW
        public void Updtate(MouseState mouse, KeyboardState keyboard)
        {
            LocalPlayer.Updtate(mouse, keyboard);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            LocalPlayer.Draw(spriteBatch);
        }
    }
}
