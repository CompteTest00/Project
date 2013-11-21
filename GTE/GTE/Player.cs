using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public enum Direction
{
    Up, Down, Left, Right
};

namespace GTE
{
    class Player
    {
        // FIELDS
        Rectangle Hitbox; // Dans XNA les joueurs sont des rectangles !

        Direction Direction; // Sens du personnage.
        int FrameLine;
        int FrameColumn;
        SpriteEffects Effect;
        bool Animation;
        int Timer;

        int Speed = 2;
        int AnimationSpeed = 7;

        // CONSTRUCTOR
        public Player()
        {
            this.Hitbox = new Rectangle(0, 0, 25, 27);
            this.FrameLine = 2;
            this.FrameColumn = 2;
            this.Direction = Direction.Down;
            this.Effect = SpriteEffects.None;
            this.Animation = true;
            this.Timer = 0;
        }

        // METHODS
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
        }// Anime le personnage.

        // UPDATE & DRAW
        public void Updtate(MouseState mouse, KeyboardState keyboard, List<Wall> walls)
        {
            // On fait se déplacer le personnage.
            if (keyboard.IsKeyDown(Keys.Up)) // UP
            {
                Rectangle newHitbox = new Rectangle(this.Hitbox.X, this.Hitbox.Y - this.Speed, this.Hitbox.Width, this.Hitbox.Height);
                bool collide = false;
                foreach (Wall wall in walls)
                {
                    if (newHitbox.Intersects(wall.Hitbox))
                    {
                        collide = true;
                        break;
                    }
                }
                if(!collide)
                    this.Hitbox.Y -= this.Speed;
                this.Direction = Direction.Up;
                this.Animate();
            }
            else if (keyboard.IsKeyDown(Keys.Down)) // DOWN
            {
                Rectangle newHitbox = new Rectangle(this.Hitbox.X, this.Hitbox.Y + this.Speed, this.Hitbox.Width, this.Hitbox.Height);
                bool collide = false;
                foreach (Wall wall in walls)
                {
                    if (newHitbox.Intersects(wall.Hitbox))
                    {
                        collide = true;
                        break;
                    }
                }
                if (!collide)
                    this.Hitbox.Y += this.Speed;
                this.Direction = Direction.Down;
                this.Animate();
            }
            else if (keyboard.IsKeyDown(Keys.Left)) // LEFT
            {
                Rectangle newHitbox = new Rectangle(this.Hitbox.X - this.Speed, this.Hitbox.Y, this.Hitbox.Width, this.Hitbox.Height);
                bool collide = false;
                foreach (Wall wall in walls)
                {
                    if (newHitbox.Intersects(wall.Hitbox))
                    {
                        collide = true;
                        break;
                    }
                }
                if (!collide)
                    this.Hitbox.X -= this.Speed;
                this.Direction = Direction.Left;
                this.Animate();
            }
            else if (keyboard.IsKeyDown(Keys.Right)) // RIGHT
            {
                Rectangle newHitbox = new Rectangle(this.Hitbox.X + this.Speed, this.Hitbox.Y , this.Hitbox.Width, this.Hitbox.Height);
                bool collide = false;
                foreach (Wall wall in walls)
                {
                    if (newHitbox.Intersects(wall.Hitbox))
                    {
                        collide = true;
                        break;
                    }
                }
                if (!collide)
                    this.Hitbox.X += this.Speed;
                this.Direction = Direction.Right;
                this.Animate();
            }


            ///////
            if (keyboard.IsKeyUp(Keys.Up) && keyboard.IsKeyUp(Keys.Down) && keyboard.IsKeyUp(Keys.Left)
                && keyboard.IsKeyUp(Keys.Right))
            {
                this.FrameColumn = 2;
                this.Timer = 0;
            }


            ///////
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
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // On dessine un carré par exemple.
            spriteBatch.Draw(Resources.Pikachu, this.Hitbox, new Rectangle((this.FrameColumn - 1) * 25, (this.FrameLine - 1) * 27, 25, 27),
                Color.White, 0f, new Vector2(0, 0), this.Effect, 0f);
        }
    }
}
