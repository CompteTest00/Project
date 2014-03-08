using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GTE
{
    public enum Direction
    {
        Up, Down, Left, Right
    };

   public class Player
   {
       #region FIELDS

       Direction Direction;

       int FrameLine;
       int FrameColumn;
       bool Animation;
       int Timer;
       SpriteEffects Effect;
       int Speed = 2;
       int AnimationSpeed = 9;

       int _screenheight, _screenwidth;
       private float Rotationangle;
       private Vector2 origin,distance,position;
       private Weapons weapon;
       public Vector2 Position
       {
           get { return position; }
           set { position = value; }
       }
        private Rectangle hitbox;
        public Rectangle Hitbox
        {
            get { return hitbox; }
            set { hitbox = value; }
        }
        private Rectangle rec_pointer;

        private Game1 game;
        public Game1 Game
        {
            get { return game; }
            set { game = value; }
        }

        private int bullet_number;
        public int Bullet_Number
        {
            get { return bullet_number; }
            set { bullet_number = value; }
        }
        private Weapons.Weapon_Type p_weapon;
        public Weapons.Weapon_Type P_Weapon
        {
            get { return p_weapon; }
            set { p_weapon = value; }
        }

        private List<Bullet> bullet_list; // Liste des balles qui ont été tirées
        public List<Bullet> Bullet_List
        {
            get { return bullet_list; }
            set { bullet_list = value; }
        }

       #endregion

        #region CONSTRUCTORS
        public Player (Game1 game)
        {
            this.game = game;
            _screenwidth = game.screenwidth;
            _screenheight = game.screenheight;
            this.Hitbox = new Rectangle(0, 0, 32, 32);
            this.FrameLine = 2;
            this.FrameColumn = 2;
            this.Direction = Direction.Down;
            this.Effect = SpriteEffects.None;
            this.Animation = true;
            this.Timer = 0;
        }
        #endregion

        #region METHODS

        public void Animate()
        {
            this.Timer++;
            if (this.Timer == this.AnimationSpeed)
            {
                this.Timer = 0;
                if (this.Animation)
                {
                    this.FrameColumn++;
                    if (this.FrameColumn > 3)
                    {
                        this.FrameColumn = 2;
                        this.Animation = false;
                    }
                }
                else
                {
                    this.FrameColumn--;
                    if (this.FrameColumn < 1)
                    {
                        this.FrameColumn = 2;
                        this.Animation = true;
                    }
                }
            }
        }

        public void Update(MouseState mouse, KeyboardState keyboard)
        {
            if (keyboard.IsKeyDown(Keys.Up))
            {
                this.hitbox.Y -= this.Speed;
                this.Direction = Direction.Up;
                this.Animate();
            }
            else if (keyboard.IsKeyDown(Keys.Down))
            {
                this.hitbox.Y += this.Speed;
                this.Direction = Direction.Down;
                this.Animate();
            }
            else if (keyboard.IsKeyDown(Keys.Left))
            {
                this.hitbox.X -= this.Speed;
                this.Direction = Direction.Left;
                this.Animate();
            }
            else if (keyboard.IsKeyDown(Keys.Right))
            {
                this.hitbox.X += this.Speed;
                this.Direction = Direction.Right;
                this.Animate();
            }
            if (keyboard.IsKeyUp(Keys.Up) && keyboard.IsKeyUp(Keys.Down) && keyboard.IsKeyUp(Keys.Left) && keyboard.IsKeyUp(Keys.Right))
            {
                this.FrameColumn = 2;
                this.Timer = 0;
            }

            switch (this.Direction)
            {
                case Direction.Up:
                    this.FrameLine = 2;
                    this.Effect = SpriteEffects.None;
                    break;
                case Direction.Down:
                    this.FrameLine = 1;
                    this.Effect = SpriteEffects.None;
                    break;
                case Direction.Left:
                    this.FrameLine = 3;
                    this.Effect = SpriteEffects.None;
                    break;
                case Direction.Right:
                    this.FrameLine = 3;
                    this.Effect = SpriteEffects.FlipHorizontally;
                    break;
            }

            Orienter(Mouse.GetState());
        }

        public void Orienter(MouseState mouse)
        {

            if (mouse.X > 0 && mouse.X < _screenwidth - rec_pointer.Width)
            {
                rec_pointer.X = mouse.X;
            }
            if (mouse.Y > 0 && mouse.Y < _screenheight - rec_pointer.Height )
            {
                rec_pointer.Y = mouse.Y;
            }
            distance.X = mouse.X - hitbox.X;
            distance.Y = mouse.Y - hitbox.Y;
            Rotationangle = (float)Math.Atan2(distance.Y, distance.X);
            Rotationangle += (float)Math.PI / 2;
            origin = new Vector2((hitbox.Width / 2), (hitbox.Height / 2));

        }


        public void Initialize()
        {
            rec_pointer = new Rectangle(_screenwidth / 2, (_screenheight / 2) , 23, 24);
            position = new Vector2(_screenwidth / 2, _screenheight / 2);
            p_weapon = Weapons.Weapon_Type.Gun;
            weapon = new Weapons(game);
            weapon.Type = p_weapon;
            bullet_number = weapon.Create_Clip(p_weapon);
            bullet_list = new List<Bullet>();
        }


        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Resources.texture_player, this.Hitbox, new Rectangle((this.FrameColumn - 1) * 32, (this.FrameLine - 1) * 32, 32, 32),
               Color.White, Rotationangle, origin, SpriteEffects.None, 0f);
            spritebatch.Draw(Resources.texture_pointer,rec_pointer,new Rectangle(0,0,23,24),Color.White);
        }
        #endregion
   }
}
