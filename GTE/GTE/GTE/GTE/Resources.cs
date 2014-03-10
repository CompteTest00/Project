using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GTE
{
    public class Resources
    {
        //STATIC FIELDS
        public static Texture2D texture_player, texture_pointer, texture_bullet, texture_enemy, texture_blood, texture_bloodground;
        /* map textures */
        public static Texture2D texture_window1, texture_window2, texture_wood, texture_wood_down, texture_wood_right, texture_wood_left, texture_wood_up; 
        //RESOURCES
        public static void LoadContent(ContentManager Content)
        {
            texture_pointer = Content.Load<Texture2D>("green_pointer");
            texture_player = Content.Load<Texture2D>("Perso");
            texture_bullet = Content.Load<Texture2D>("temporarybulletsprite");
            texture_enemy = Content.Load<Texture2D>("Enemy_temp");
            texture_blood = Content.Load<Texture2D>("blood");
            texture_window1 = Content.Load<Texture2D>("MapSprites//window1");
            texture_window2 = Content.Load<Texture2D>("MapSprites//window2");
            texture_wood = Content.Load<Texture2D>("MapSPrites//wood");
            texture_wood_down = Content.Load<Texture2D>("MapSprites//woodWdown");
            texture_wood_left = Content.Load<Texture2D>("MapSprites//woodWleft");
            texture_wood_right = Content.Load<Texture2D>("MapSprites//woodWright");
            texture_wood_up = Content.Load<Texture2D>("MapSprites//woodWup");
        }
    }
}
