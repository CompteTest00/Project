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
        Rectangle Player;

        // CONSTRUCTOR
        public GameMain()
        {
            Player = new Rectangle(0, 0, 16, 16);
        }

        // METHODS

        // UPDTA & DRAW
        public void Updtate(MouseState mouse, KeyboardState keyboard)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Resources.Player, Player, Color.LimeGreen);
        }
    }
}
