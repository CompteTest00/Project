using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GTE
{
    public class Enemy
    {
        #region FIELDS
        private Game1 game;
        private Vector2 position;
        private bool is_alive;
        public bool Is_Alive
        {
            get { return is_alive;}
            set { is_alive = value; }
        }
        private Rectangle enemy_rec;
        public Rectangle Enemy_Rec
        {
            get { return enemy_rec; }
            set { enemy_rec = value; }
        }
        private Vector2 velocity;
        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        private bool is_aware;
        public bool Is_Aware
        {
            get { return is_aware; }
            set { is_aware = value; }
        }

        #endregion

        #region CONSTRUCTORS
        public Enemy(Game1 game)
        {
            this.game = game;
        }
        #endregion

        #region METHODS
        //METHODS
        public void SpawnEnemies(int enemy_number)
        {
            Enemy new_enemy;
            Random random_pos = new Random();
            int rand_x, rand_y;
            while (enemy_number > 0)
            {
                new_enemy = new Enemy(game);
                rand_x = random_pos.Next(0, game.screenwidth-enemy_rec.X);
                rand_y = random_pos.Next(0, game.screenheight-enemy_rec.Y);
                new_enemy.position = new Vector2(rand_x, rand_y);
                new_enemy.enemy_rec = new Rectangle(rand_x, rand_y, 32, 32);
                new_enemy.is_alive = true;
                new_enemy.is_aware = false;
                new_enemy.velocity = Vector2.Zero;
                game.Enemy_List.Add(new_enemy);
                enemy_number--;
            }
        }

        public void Enemy_Die()
        {
            if (game.Enemy_List != null)
            {
                foreach (Enemy enemy in game.Enemy_List)
                {
                    foreach (Bullet bullet in game.player.Bullet_List)
                    {
                        if (enemy.enemy_rec.Intersects(bullet.Rec_ball))
                        {
                            enemy.is_alive = false;
                            bullet.Is_Visible = false;
                            int particle_generated = 100;
                            Random rand = new Random();
                            while (particle_generated > 0)
                            {
                                Particle new_particle = new Particle(game);
                                new_particle.Texture = Resources.texture_blood;
                                new_particle.Position = enemy.position;
                                new_particle.Velocity = new Vector2((float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1);
                                new_particle.Angle = 0;
                                new_particle.TDV = 10;
                                new_particle.AngularVelocity = 0.1f * (float)(rand.NextDouble() * 2 - 1);
                                game.Particle_list.Add(new_particle);
                                particle_generated--;
                            }
                        }
                    }
                }
                    for (int i = 0; i < game.Enemy_List.Count; i++)
                    {
                        Enemy listed_enemy = game.Enemy_List[i];
                        if (!listed_enemy.is_alive)
                        {
                            game.Enemy_List.RemoveAt(i);
                            i--;
                        }
                    }
            }
        }


        public void Detect(Enemy enemy) //  Ne fonctionne pas correctement
        {
            Vector2 distance;
            distance.X = game.player.Hitbox.X - enemy.enemy_rec.X;
            distance.Y = game.player.Hitbox.Y - enemy.enemy_rec.Y;
            if (Math.Abs(distance.X) < 50 || Math.Abs(distance.Y) > 50)
            {
                is_aware = true;
                float rotationangle = (float)Math.Atan2(distance.X, distance.Y);
                enemy.velocity = new Vector2((float)Math.Cos(rotationangle), (float)Math.Sin(rotationangle)) * 1f;
            }
            enemy.position += enemy.velocity;
            enemy.enemy_rec.X = (int)enemy.position.X;
            enemy.enemy_rec.Y = (int)enemy.position.Y;
        }

        public void Track(Enemy enemy) // Ne fonctionne pas correctement
        {
            if (enemy.is_aware)
            {
                Vector2 distance;
                distance.X = game.player.Hitbox.X - enemy.enemy_rec.X;
                distance.Y = game.player.Hitbox.Y - enemy.enemy_rec.Y;
                float rotationangle = (float)Math.Atan2(distance.X, distance.Y);
                enemy.velocity = new Vector2((float)Math.Cos(rotationangle), (float)Math.Sin(rotationangle)) * 1f;
                enemy.position += enemy.velocity;
            }
        }

        public void Initialize()
        {
            SpawnEnemies(2);
        }

        public void Update()
        {
            Enemy_Die();
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy enemy in game.Enemy_List)
            {
                spriteBatch.Draw(Resources.texture_enemy,enemy.position, new Rectangle(0,0,32,32), Color.White);
            }
        }

        #endregion
    }
}
