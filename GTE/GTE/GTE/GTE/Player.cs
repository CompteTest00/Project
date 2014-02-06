using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GTE
{
   public class Player
    {
        //FIELDS

       int _screenheight, _screenwidth;
       private float Rotationangle;
       private Vector2 origin,distance,position;
       private Weapons weapon;
       public Vector2 Position
       {
           get { return position; }
           set { position = value; }
       }
        private Rectangle rec_player;
        public Rectangle Rec_Player
        {
            get { return rec_player; }
            set { rec_player = value; }
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

        //CONSTRUCTORS
       public Player (Game1 game)
        {
            this.game = game;
            _screenwidth = game.screenwidth;
            _screenheight = game.screenheight;
        }

        //METHODS
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
            distance.X = mouse.X - rec_player.X;
            distance.Y = mouse.Y - rec_player.Y;
            Rotationangle = (float)Math.Atan2(distance.Y, distance.X);
            Rotationangle = (float)Math.PI / 2 + Rotationangle;
            origin = new Vector2((rec_player.Width / 2), (rec_player.Height / 2));

        }


        public void Initialize()
        {
            rec_pointer = new Rectangle(_screenwidth / 2, (_screenheight / 2) , 23, 24);
            rec_player = new Rectangle(_screenwidth / 2, _screenheight / 2, 29, 30);
            position = new Vector2(_screenwidth / 2, _screenheight / 2);
            p_weapon = Weapons.Weapon_Type.Gun;
            weapon = new Weapons(game);
            weapon.Type = p_weapon;
            bullet_number = weapon.Reload(p_weapon);
            bullet_list = new List<Bullet>();
        }

        public void Update()
        {
            Orienter(Mouse.GetState());
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Resources.texture_player, position, new Rectangle(0, 0, 29, 30), Color.White, Rotationangle , origin, 1f, SpriteEffects.None, 0);
            spritebatch.Draw(Resources.texture_pointer,rec_pointer,new Rectangle(0,0,23,24),Color.White);
        }

    }
}
