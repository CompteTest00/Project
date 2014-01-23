using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GTE
{
    public enum Bullet_Type
    {
        sniper_bullet, rifle_bullet, gun_bullet, bazooka_bullet,shotgun_bullet
    }
    public class Bullet
    {
        //FIELDS
        Vector2 position;
        Vector2 velocity;
        Vector2 origin;

        private int speed;
        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        private int bullet_damage;
        public int Bullet_damage
        {
            get { return bullet_damage; }
            set { bullet_damage = value; }
        }

        private Bullet_Type type;
        public Bullet_Type Type
        {
            get { return type; }
            set { type = value; }
        }

        //CONSTRUCTORS
        public Bullet(Bullet_Type type, int speed, int bullet_damage)
        {
            this.speed = speed;
            this.bullet_damage = bullet_damage;
            this.type = type;
        }

    
        //METHODS
        public int Damage(Bullet_Type type)
        {
            Random rand = new Random();
            int jet = rand.Next(0, 100);
            int damage = 0;
            if (type == Bullet_Type.sniper_bullet)
            {
                damage = 70;
                int critical_bonus_rate = 50;
                int critical_rate = 33;
                if (jet >= critical_rate)
                {
                    damage = damage + damage * critical_bonus_rate / 100;
                }
                return damage;
            }
            else
                if (type == Bullet_Type.rifle_bullet)
                {
                    damage = 10;
                    int critical_bonus_rate = 20;
                    int critical_rate = 20;
                    if (jet >= critical_rate)
                    {
                        damage = damage + damage * critical_bonus_rate / 100;
                    }
                    return damage;
                }
                else
                    if (type == Bullet_Type.gun_bullet)
                    {
                        damage = 5;
                        int critical_bonus_rate = 10;
                        int critical_rate = 30;
                        if (jet >= critical_rate)
                        {
                            damage = damage + damage * critical_bonus_rate / 100;
                        }
                        return damage;
                    }
                    else
                        if (type == Bullet_Type.bazooka_bullet)
                        {
                            damage = 100;
                            int critical_bonus_rate = 1;
                            int critical_rate = 5;
                            if (jet >= critical_rate)
                            {
                                damage = damage + damage * critical_bonus_rate / 100;
                            }
                            return damage;
                        }
                        else
                            return damage;
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
