using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace GTE
{
   public class Weapons
   {
        #region FIELDS
        private bool can_shoot = false;
        private float x = 0f;
        public enum Weapon_Type
        {
            Sniper,Gun,Rifle,Bazooka
        }
        private Game1 game;
        Player player;
        private float RotationAngle;
        private Weapon_Type type;
        public Weapon_Type Type
        {
            get { return type; }
            set { type = value; }
        }
       #endregion

        #region CONSTRUCTORS
        public Weapons(Game1 game)
        {
            this.game = game;
        }
        #endregion

        #region METHODS
        public int Create_Clip(Weapon_Type weapon_type)
        {
                int count = 0;
                if (weapon_type == Weapon_Type.Gun)
                {
                    return count = 10;
                }
                if (weapon_type == Weapon_Type.Bazooka)
                {
                    return count = 1;
                }
                if (weapon_type == Weapon_Type.Rifle)
                {
                    return count = 20;
                }
                if (weapon_type == Weapon_Type.Sniper)
                {
                    return count = 5;
                }
                else
                    return 0;
        }

        public SoundEffect SoundOf(Weapon_Type weapon)
        {
            switch (weapon)
            {
               case Weapon_Type.Sniper:
                    return game.seffect_gun;
                    
                case Weapon_Type.Rifle:
                   return game.seffect_gun;
                    
                case Weapon_Type.Gun:
                    return game.seffect_gun;
                    
                case Weapon_Type.Bazooka:
                    return game.seffect_gun;

                default:
                    return game.seffect_gun;
            }
        }

       
        public void Shoot(MouseState mouse, GameTime gameTime)
        {
            if (x < 0.1f)
            {
                x = x + (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (x >= 0.1f)
            {
                can_shoot = true;
            }
            player = game.player;
            if (mouse.LeftButton == ButtonState.Pressed && can_shoot)
          {
            Bullet new_bullet ;
            Vector2 position = new Vector2(player.Hitbox.X, player.Hitbox.Y);
            Vector2 distance;
            mouse = Mouse.GetState();

            if (player.Bullet_Number != 0)
            {
                SoundOf(player.P_Weapon).Play();
                new_bullet = new Bullet(game);
                player.Bullet_Number--;
                distance.X = mouse.X - new_bullet.Rec_ball.X;
                distance.Y = mouse.Y - new_bullet.Rec_ball.Y;
                new_bullet.Position = position;
                new_bullet.Velocity = new_bullet.Bullet_Speed(distance, mouse, new_bullet);
                new_bullet.Position = position + new_bullet.Velocity;
                new_bullet.Is_Visible = true;
                player.Bullet_List.Add(new_bullet);
                x = 0f;
                can_shoot = false;
            }
           }
            Reload(player.P_Weapon);

        }

        public void Reload(Weapon_Type weapon_type)
        {
            player = game.player;
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                player.Bullet_Number = Create_Clip(weapon_type);
            }
        }

        public void Update(GameTime gameTime)
        {
            Shoot(Mouse.GetState(),gameTime);
        }
        #endregion
   }
}
