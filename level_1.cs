using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RC_Framework;
namespace JellyTelly2
{
    public class level_1 : RC_GameStateParent
    {
        Texture2D texBG;
        Texture2D texPlat;
        Texture2D texPlayer;
        Texture2D texWalking_1;
        Texture2D texWalking_2;
        Texture2D texCandy;
        Texture2D texCandy2;
        Texture2D texCandy3;
        Texture2D texCandy4;
        Texture2D texCandy5;
        Texture2D texDoor;
        Texture2D texDoor1;

        ImageBackground lvlBg;
        Sprite3 block;
        Sprite3 player;
        Player ply;
        Sprite3 candy;
        Sprite3 door;
        Sprite3 door1;
        SpriteList groundList;
        SpriteList platfrom_1;
        SpriteList platfrom_2;
        SpriteList platfrom_3;
        SpriteList candyList;
        SpriteList candyList2;
        SpriteFont font;
       
        KeyboardState prevk;
        KeyboardState k;
        float xpos;
        float ypos;
        float ground;
        float yvel = 0;
        bool fall = false;
        int candyCount = 0;
        float score = 0;
        bool showbb = false;
        bool allCollected = false;
        SoundEffect candySound;
        SoundEffect doorSound;
        SoundEffectInstance doorOpen;
        float soundtime = 1;
        public override void LoadContent()
        {
            spriteBatch = new SpriteBatch(graphicsDevice);
            LineBatch.init(graphicsDevice);

            groundList = new SpriteList();
            platfrom_1 = new SpriteList();
            platfrom_2 = new SpriteList();
            platfrom_3 = new SpriteList();
            candyList = new SpriteList();
            candyList2 = new SpriteList();
            font = Content.Load<SpriteFont>("Fonty");

            texBG = Content.Load<Texture2D>("Images/candyWorld_bg");
            texPlat = Content.Load<Texture2D>("Images/platfrom");
            texPlayer = Content.Load<Texture2D>("Images/Telly_2");
            texWalking_1 = Content.Load<Texture2D>("Images/Telly_walking_1");
            texWalking_2 = Content.Load<Texture2D>("Images/Telly_walking_2");
            texDoor = Content.Load<Texture2D>("Images/door");
            texDoor1 = Content.Load<Texture2D>("Images/door_open");
            texCandy = Content.Load<Texture2D>("Images/candy");
            texCandy2 = Content.Load<Texture2D>("Images/candy2");
            texCandy3 = Content.Load<Texture2D>("Images/candy3");
            texCandy4 = Content.Load<Texture2D>("Images/candy4");
            texCandy5 = Content.Load<Texture2D>("Images/candy5");

            door = new Sprite3(true, texDoor, 700, 150 - texDoor.Height);
            door1 = new Sprite3(true, texDoor1, 700, 150 - texDoor1.Height);
            door1.setVisible(false);
            candySound = Content.Load<SoundEffect>("music/mixkit-fairy-arcade-sparkle-866");
            doorSound = Content.Load<SoundEffect>("music/mixkit-automatic-door-shut-204");
            doorOpen = doorSound.CreateInstance(); 
            lvlBg = new ImageBackground(texBG, Color.White, graphicsDevice);
            for (int i = 0; i < 30; i++)
            {
                block = new Sprite3(true, texPlat, i * texPlat.Width, 720 - texPlat.Height);
                groundList.addSpriteReuse(block);
            }
            for (int i = 0; i < 2; i++) 
            {
                block = new Sprite3(true, texPlat, 100 + i * texPlat.Width, 400);
                platfrom_1.addSpriteReuse(block);
            }
            for (int i = 0; i < 2; i++)
            {
                block = new Sprite3(true, texPlat, 800 + i * texPlat.Width, 400);
                platfrom_2.addSpriteReuse(block);
            }
            for (int i = 0; i < 8; i++)
            {
                block = new Sprite3(true, texPlat, 250 + i * texPlat.Width, 150);
                platfrom_3.addSpriteReuse(block);
            }
            for (int i = 0; i < 2; i++) 
            {
                if (i == 1) 
                {
                    candy = new Sprite3(true, texCandy, 800, 400 - texCandy.Height);
                }
                else
                    candy = new Sprite3(true, texCandy, 100, 400 - texCandy.Height);
                candyList.addSpriteReuse(candy);
            }
            for (int i = 0; i < 3; i++) 
            {
                candy = new Sprite3(true, texCandy, 400+i*(texCandy.Width + 50), 150 - texCandy.Height);
                candyList2.addSpriteReuse(candy);
            }

            candyCount = candyList.count() + candyList2.count();
            player = new Sprite3(true, texPlayer, 100, 720 - texPlat.Height - texPlayer.Height);
            ply = new Player(texPlayer, player, Color.White)
            {
                texWalking1 = texWalking_1,
                texWalking2 = texWalking_2
            };
            player.varInt0 = 0;
            candy.varInt0 = 0;
        }
        public override void Update(GameTime gameTime)
        {
            prevk = k;
            k = Keyboard.GetState();
            xpos = player.getPosX();
            ypos = player.getPosY();
            ground = 720 - texPlat.Height - texPlayer.Height;
            CandyAnimation(10);
            if (k.IsKeyDown(Keys.Right)) 
            {
                WalkingAnimation(8);
                if (xpos > 1020 - texPlayer.Width)
                {
                    ply.setXvel(0);
                }
                else
                    ply.setXvel(5);
                ply.MoveRight();
                xpos = ply.getXpos();
            }

            if (k.IsKeyDown(Keys.Left)) 
            {
                WalkingAnimation(8);
                if (xpos < 0)
                {
                    ply.setXvel(0);
                }
                else
                    ply.setXvel(5);
                ply.MoveLeft();
                xpos = ply.getXpos();
            }
            if (k.IsKeyDown(Keys.B) && prevk.IsKeyUp(Keys.B)) 
            {
                showbb = !showbb;
            }
            
            if (k.IsKeyDown(Keys.Space) && prevk.IsKeyUp(Keys.Space) && yvel ==0) 
            {
                yvel = -20;
                yvel +=  0.5f;
                ypos += yvel;
                fall = true;
            }

            for (int i =0; i<platfrom_1.count();i++) 
            {
                Sprite3 s = platfrom_1.getSprite(i);
                if (ply.HitLeft(s) || ply.HitRight(s)) 
                {
                   player.moveByDeltaX(0);
                }
                if (ply.HitTop(s)) 
                {
                    ypos = s.getPosY() - texPlayer.Height;
                    yvel = 0;                 
                    fall = false;
                }
                
                if (player.getPosX() > platfrom_1.getSprite(1).getPosX() + texPlat.Width && player.getPosX() < platfrom_3.getSprite(0).getPosX()
                   || player.getPosX() < platfrom_1.getSprite(0).getPosX() && player.getPosX() > 0)
                { 
                    fall = true; 
                }
              
                if (ply.HitBottom(s) )
                {
                    yvel = 1;
                }
            }
            for (int i = 0; i < platfrom_2.count(); i++)
            {
                Sprite3 s1 = platfrom_2.getSprite(i);
                if (ply.HitLeft(s1) || ply.HitRight(s1))
                {
                    player.moveByDeltaX(0);
                }

                if (ply.HitTop(s1))
                {
                    ypos = s1.getPosY() - texPlayer.Height;
                    yvel = 0;
                    fall = false;
                }
                if (player.getPosX() > platfrom_2.getSprite(1).getPosX() + texPlat.Width && player.getPosX() < 1020 - texPlayer.Width
                   || player.getPosX() < platfrom_2.getSprite(0).getPosX() && player.getPosX() > platfrom_3.getSprite(7).getPosX())
                {
                    fall = true;
                }
                if (ply.HitBottom(s1))
                {
                    yvel = 1;
                }
            }
            for (int i = 0; i < platfrom_3.count(); i++) 
            {
                Sprite3 s1 = platfrom_3.getSprite(i);
                if (ply.HitLeft(s1) || ply.HitRight(s1))
                {
                    player.moveByDeltaX(0);
                }

                if (ply.HitTop(s1))
                {
                    ypos = s1.getPosY() - texPlayer.Height;
                    yvel = 0;
                    fall = false;
                }
                if (player.getPosX() > platfrom_3.getSprite(7).getPosX() + texPlat.Width && player.getPosX() < platfrom_2.getSprite(0).getPosX()
                   || player.getPosX() < platfrom_3.getSprite(0).getPosX() && player.getPosX() > platfrom_1.getSprite(1).getPosX())
                {
                    fall = true;
                }
                if (ply.HitBottom(s1))
                {
                    yvel = 1;
                }
            }
            for (int i = 0; i < candyList.count(); i++) 
            {
                Sprite3 s = candyList.getSprite(i);
                s.varInt0 = 0;
                CandyAnimation(10);
                if (player.collision(s)) 
                {
                    s.setVisible(false);
                    candyCount -= 1;
                    score += 100;
                    candySound.Play();
                }
            }
            for (int i = 0; i < candyList2.count(); i++)
            {
                Sprite3 s = candyList2.getSprite(i);
                s.varInt0 = 0;
                CandyAnimation(10);
                if (player.collision(s))
                {
                    s.setVisible(false);
                    candyCount -= 1;
                    score += 100;
                    candySound.Play();
                }
            }
            if (fall) 
            {
                yvel += 0.5f;
                ypos += yvel;
            }
            if (ypos > ground) 
            {
                yvel = 0;
                ypos = ground;
                fall = false;
            }
            if (candyCount == 0) 
            {
                door.setVisible(false);
                door1.setVisible(true);
                allCollected = true;
            }
            if (soundtime == 1 && allCollected) 
            {
                doorOpen.Pause();
                doorOpen.Play();
                soundtime = 0;
            }
            
            if (player.collision(door1)) 
            {
                gameStateManager.setLevel(3);
            }
            player.setPos(new Vector2(xpos, ypos));
            player.savePosition();
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            lvlBg.Draw(spriteBatch);
            groundList.Draw(spriteBatch);
            platfrom_1.Draw(spriteBatch);
            platfrom_2.Draw(spriteBatch);
            platfrom_3.Draw(spriteBatch);
            player.Draw(spriteBatch);
            candyList.Draw(spriteBatch);
            candyList2.Draw(spriteBatch);
            door.Draw(spriteBatch);

            spriteBatch.DrawString(font, "Score: " +score.ToString(), new Vector2(10, 10), Color.Black);
            if (allCollected) 
            {
                door1.Draw(spriteBatch);
            }
            if (showbb) 
            {
                player.drawInfo(spriteBatch, Color.Red, Color.Blue);
                platfrom_1.drawInfo(spriteBatch, Color.Red, Color.Blue);
                platfrom_2.drawInfo(spriteBatch, Color.Red, Color.Blue);
                platfrom_3.drawInfo(spriteBatch, Color.Red, Color.Blue);
            }
            spriteBatch.End();
        }
        public void WalkingAnimation(int time)
        {
            player.varInt0 = player.varInt0 + 1;
            if (player.varInt0 == 1) player.setTexture(texWalking_1, false);
            if (player.varInt0 == 1 + time) player.setTexture(texPlayer, false);
            if (player.varInt0 == 1 + 2 * time - 1) player.setTexture(texWalking_2, false);
            if (player.varInt0 == 1 + 3 * time) player.setTexture(texPlayer, false);
            if (player.varInt0 == 1 + 4 * time) player.varInt0 = 0;
        }

        public void CandyAnimation(int time)
        {
            candy.varInt0 = candy.varInt0 + 1;
            if (candy.varInt0 == 1) candy.setTexture(texCandy2, false);
            if (candy.varInt0 == 1 + time) candy.setTexture(texCandy3, false);
            if (candy.varInt0 == 1 + 2 * time - 1) candy.setTexture(texCandy4, false);
            if (candy.varInt0 == 1 + 3 * time) candy.setTexture(texCandy5, false);
            if (candy.varInt0 == 1 + 4 * time) candy.setTexture(texCandy, false);
            if (candy.varInt0 == 1 + 5 * time) candy.varInt0 = 0;
        }
    }
}
