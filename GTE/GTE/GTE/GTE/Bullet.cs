using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GTE
{

    public class Bullet
    {
        //FIELDS
        bool Is_Visible;
        Vector2 distance;
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

        private int bullet_damage;
        public int Bullet_damage
        {
            get { return bullet_damage; }
            set { bullet_damage = value; }
        }

        

        //CONSTRUCTORS
        public Bullet()
        {

        }
    
        //METHODS


        public void Bullet_Speed(Bullet bullet)
        {
            bullet.position += bullet.velocity;
        }
        public void Initialize()
        {

        }
        public void Update()
        {
        
        }
        public void Draw(SpriteBatch spritebatch)
        {

        }


    }
}
