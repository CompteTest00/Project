using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace GTE
{
    class Weapons
    {
        public enum Weapon_Type
        {
            Sniper,Gun,Rifle,Bazooka
        }
        private Game1 game = new Game1();
        Player player;
        private float RotationAngle;
        private Weapon_Type type;
        public Weapon_Type Type
        {
            get { return type; }
            set { type = value; }
        }

        public Weapons(Weapon_Type type)
        {
            this.type = type;
        }

        public void Reload(Weapon_Type weapon_type)
        {
            GameTime game_time = new GameTime();
            if (weapon_type == Weapon_Type.Sniper)
            {
                List<Bullet> clip = new List<Bullet>(7);
                TimeSpan timer = new TimeSpan(0, 0, 3);
                while (game_time.ElapsedGameTime <= timer)
                {
                    continue;
                }
                for (int i = 0; i < clip.Count; i++)
                {
                    clip[i] = (new Bullet());
                }
            }

            if (weapon_type == Weapon_Type.Rifle)
            {
                List<Bullet> clip = new List<Bullet>(30);
                TimeSpan timer = new TimeSpan(0,0,2);
                while (game_time.ElapsedGameTime <= timer)
                {
                    continue;
                }
                for (int i = 0; i < clip.Count; i++)
                {
                    clip[i] = (new Bullet());
                }
            }

            if (weapon_type == Weapon_Type.Bazooka)
            {
                
                List<Bullet> clip = new List<Bullet>(1);
                TimeSpan timer = new TimeSpan(0, 0, 4);
                while (game_time.ElapsedGameTime <= timer)
                {
                    continue;
                }
                for (int i = 0; i < clip.Count; i++)
                {
                    clip[i] = (new Bullet());
                }
                
            }
            if (weapon_type == Weapon_Type.Gun)
            {
                List<Bullet> clip = new List<Bullet>(10);
                TimeSpan timer = new TimeSpan(0, 0, 1);
                while (game_time.ElapsedGameTime <= timer)
                {
                    continue;
                }
                for(int i = 0; i < clip.Count ; i++)
                {
                    clip[i] = (new Bullet());
                }
            }
        }

        public void Shoot(MouseState mouse)
        {
            player = new Player(player.Game);
            Vector2 position = new Vector2(player.Rec_Player.X, player.Rec_Player.Y);
            Vector2 distance;
            mouse = Mouse.GetState();
            if (mouse.RightButton == ButtonState.Pressed)
            {
                Bullet new_bullet = new Bullet();
                distance.X = mouse.X - new_bullet.Rec_ball.X;
                distance.Y = mouse.Y - new_bullet.Rec_ball.Y;
                RotationAngle = (float)Math.Atan2(distance.Y, distance.X);
                new_bullet.Velocity = new Vector2((float)Math.Cos(RotationAngle), (float)Math.Sin(RotationAngle)) * 5f; // A modifier en rajoutant la vitesse du joueur pour qu'il ne se prenne pas la balle
                new_bullet.Position = position + new_bullet.Velocity;

            }
            
        }
    }
}
