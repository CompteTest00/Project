using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GTE
{
    public class Camera
    {
        private Game1 game;
        private Matrix transform;
        public Matrix Transform { get { return transform; } set { transform = value; } }
        Viewport view;
        private Vector2 postion;
        public Vector2 Position { get { return postion; } set { postion = value; } }

        public Camera(Viewport newView,Game1 _game)
        {
            game = _game;
            view = newView;
        }

        public void Update(GameTime Gametime, Game1 game)
        {
            postion = new Vector2(game.player.Position.X + (game.player.Hitbox.Width / 2) - game.screenwidth / 2, game.player.Position.Y + (game.player.Hitbox.Height / 2) - game.screenheight / 2);
            transform =  Matrix.CreateScale(new Vector3(1,1,0))*Matrix.CreateTranslation(new Vector3(-postion.X,-postion.Y,0));
        }
    }
}
