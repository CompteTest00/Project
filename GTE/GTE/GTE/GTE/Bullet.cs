using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace GTE
{

    public class Bullet
    {
        #region FIELDS
        Vector2 distance;
        private Game1 game;
        Player player;
        private Rectangle rec_ball;
        public Rectangle Rec_ball  
        {
            get { return rec_ball; }
            set { rec_ball = value; }
        }

       private Vector2 position;
       public Vector2 Position
       {
           get { return position; }
           set { position = value; }
       }
       private Vector2 velocity;
       public Vector2 Velocity
       {
           get { return position; }
           set { position = value; }
       }
        public Vector2 origin;
        private bool is_visible;
        public bool Is_Visible
        {
            get { return is_visible; }
            set { is_visible = value; }
        }

        #endregion

        #region CONSTRUCTORS
        //CONSTRUCTORS
        public Bullet(Game1 game)
        {
            this.game = game;
        }
        #endregion

        #region METHODS
        public void Update()
        {
            player = game.player;
            if (player.Bullet_List != null)
            {
                foreach (Bullet bullet in player.Bullet_List)
                {
                    bullet.position += bullet.velocity;
                    bullet.rec_ball.X = (int)bullet.position.X;
                    bullet.rec_ball.Y = (int)bullet.position.Y;
                    if (bullet.position.X > game.screenwidth || bullet.position.X < 0 || bullet.position.Y > game.screenheight || bullet.position.Y < 0)
                    {
                        bullet.is_visible = false;
                    }
                }

                for (int i = 0; i < player.Bullet_List.Count; i++) // Si la balle sort de l'écran, on la vire de la liste
                {
                    Bullet bullet = player.Bullet_List[i];
                    if (!bullet.is_visible)
                    {
                        player.Bullet_List.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        public Vector2 Bullet_Speed(Vector2 distance, MouseState mouse, Bullet bullet)
        {
            distance.X = mouse.X - game.player.Hitbox.X;
            distance.Y = mouse.Y - game.player.Hitbox.Y;
            float Rotationangle = (float)Math.Atan2(distance.Y, distance.X);
            bullet.velocity = new Vector2((float)Math.Cos(Rotationangle), (float)Math.Sin(Rotationangle)) * 10f;
            return bullet.velocity;
        }

        public void Initialize()
        {
            rec_ball.X = game.player.Hitbox.X;
            rec_ball.Y = game.player.Hitbox.Y;
            origin = Vector2.Zero;
           
        }

        public void Draw(SpriteBatch spritebatch)
        {
            foreach (Bullet bullet in player.Bullet_List)
            {
                spritebatch.Draw(Resources.texture_bullet, bullet.position, new Rectangle(0,0,2,2), Color.White, 0f, origin, 1f, SpriteEffects.None
                    ,0f);
            }
        }

        #endregion
    }
}
