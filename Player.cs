using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RC_Framework;

namespace JellyTelly2
{
    public class Player : Sprite3
    {
        Texture2D tex;
        Sprite3 player;
        public Texture2D texWalking1;
        public Texture2D texWalking2;

        Color col = Color.White;
        float xvel = 0;
        float yvel = 0;
        //float gravity = 0.5f;
        public float xpos;
        public float ypos;
        bool left = true;
        bool right = false;
        public bool falling = false;
        public float getXpos() 
        {
            return xpos;
        }
        public float getYpos()
        {
            return ypos;
        }
        public float getXvel()
        {
            return xvel;
        }
        public void setXvel(float speed) 
        {
            xvel = speed;
        }
        public float getYvel()
        {
            return yvel;
        }
        public void setYvel(float speed)
        {
            yvel = speed;
        }
        public Player(Texture2D tex_, Sprite3 s, Color color) 
        {
            tex = tex_;
            player = s;
            col = color;
        }
        public void MoveRight() 
        {
            left = true;
            
            if (right) 
            {
                player.flip = SpriteEffects.None;
                right = !right;
            }
            xpos = player.getPosX();
            xpos += xvel;
        }
        public void MoveLeft() 
        {
            right = true;
            if (left) 
            {
                player.flip = SpriteEffects.FlipHorizontally;
                left = !left;
            }
            xpos = player.getPosX();
            xpos -= xvel;
        }
        
        public bool HitLeft(Sprite3 s) 
        {
            Rectangle playerRec = player.getBoundingBoxAA();
            Rectangle rect = s.getBoundingBoxAA();
            if (playerRec.Right + xvel > rect.Left
               && playerRec.Left < rect.Left
               && playerRec.Bottom > rect.Top
               && playerRec.Top < rect.Bottom)
                return true;
            else
                return false;
        }
        public bool HitRight(Sprite3 s)
        {
            Rectangle playerRec = player.getBoundingBoxAA();
            Rectangle rect = s.getBoundingBoxAA();
            if (playerRec.Left + xvel < rect.Right
               && playerRec.Right < rect.Right
               && playerRec.Bottom > rect.Top
               && playerRec.Top < rect.Bottom)
                return true;
            else
                return false;
        }
        public bool HitTop(Sprite3 s)
        {
            Rectangle playerRec = player.getBoundingBoxAA();
            Rectangle rect = s.getBoundingBoxAA();
            if (playerRec.Bottom + yvel > rect.Top
               && playerRec.Top < rect.Top
               && playerRec.Right > rect.Left
               && playerRec.Left < rect.Right)
                return true;
            else
                return false;
        }
        public bool HitBottom(Sprite3 s)
        {
            Rectangle playerRec = player.getBoundingBoxAA();
            Rectangle rect = s.getBoundingBoxAA();
            if (playerRec.Top + yvel < rect.Bottom
               && playerRec.Bottom > rect.Bottom
               && playerRec.Right > rect.Left
               && playerRec.Left < rect.Right)
                return true;
            else
                return false;
        }
    }
     
 }


