using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RC_Framework;

namespace JellyTelly2
{
    public class level_menu : RC_GameStateParent
    {
        ImageBackground levelMenu;
        Sprite3 menuAnimation;
        Sprite3 menuAnimation2;
        Texture2D texLevel1;
        Texture2D texLevel2;
        Texture2D texBG;
        Texture2D texStar;

        Sprite3 lvl1;
        Sprite3 lvl2;
        SpriteList lvlList;
        bool hover;
        int tick = 0;
        public override void LoadContent()
        {
            spriteBatch = new SpriteBatch(graphicsDevice);
            LineBatch.init(graphicsDevice);

            lvlList = new SpriteList();
            texBG = Content.Load<Texture2D>("Images/select_level");
            texLevel1 = Content.Load<Texture2D>("Images/level_button_11");
            texLevel2 = Content.Load<Texture2D>("Images/level_button_12");
            texStar = Content.Load<Texture2D>("Images/select_level2");

            levelMenu = new ImageBackground(texBG, Color.White, graphicsDevice);
            menuAnimation = new Sprite3(true, texStar, 0,0);
            menuAnimation2 = new Sprite3(true, texStar, 0 - texStar.Width-50, 0);
            lvl1 = new Sprite3(true, texLevel1, 100, 200);
            lvl2 = new Sprite3(true, texLevel2, 100 + texLevel1.Width + 100, 200);

            lvlList.addSpriteReuse(lvl1);
            lvlList.addSpriteReuse(lvl2);
        }
        public override void Update(GameTime gameTime)
        {
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            Rectangle mouseRect = new Rectangle(currentMouseState.X, currentMouseState.Y, 1, 1);
            tick++;
            if (menuAnimation.getPosX() > 1020) 
            {
                menuAnimation.setPosX(0-texStar.Width-70);
            }
            if (menuAnimation2.getPosX() > 1020)
            {
                menuAnimation2.setPosX(0 - texStar.Width-50);
            }
            menuAnimation.setDeltaSpeed(new Vector2(2, 0));
            menuAnimation.moveByDeltaXY();
            menuAnimation2.setDeltaSpeed(new Vector2(2, 0));
            menuAnimation2.moveByDeltaXY();
           
           
            for (int i = 0; i < lvlList.count(); i++)
            {
                Sprite3 bttn = lvlList.getSprite(i);
                if (mouseRect.Intersects(bttn.getBoundingBoxAA()))
                {
                    hover = true;
                    if (hover)
                    {
                        bttn.setColor(Color.Gray);
                    }
                    if (currentMouseState.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (bttn.getPosX() == lvl1.getPosX())
                            gameStateManager.setLevel(2);
                        if (bttn.getPosX() == lvl2.getPosX())
                            gameStateManager.setLevel(3);
                       

                    }
                }
                else
                    bttn.setColor(Color.White);
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            levelMenu.Draw(spriteBatch);
            menuAnimation.Draw(spriteBatch);
            menuAnimation2.Draw(spriteBatch);
            lvlList.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
