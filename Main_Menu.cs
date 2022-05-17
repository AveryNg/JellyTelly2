using System;
using System.Collections.Generic;
using System.Text;
using RC_Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace JellyTelly2
{
   public class Main_Menu : RC_GameStateParent
    {
        Texture2D texBG;
        Texture2D texLG;
        Texture2D texNG;
        Texture2D texLDG;
        Texture2D texOP;
        Texture2D texQ;

        Sprite3 NG;
        Sprite3 LDG;
        Sprite3 OP;
        Sprite3 Q;
        Sprite3 logo;
        SpriteList buttons;
        ImageBackground bg;
        bool hover;
        
        public override void LoadContent()
        {
            spriteBatch = new SpriteBatch(graphicsDevice);
            LineBatch.init(graphicsDevice);
            buttons = new SpriteList();

            texBG = Content.Load<Texture2D>("Images/JellyTelly_bg");
            texLG = Content.Load<Texture2D>("Images/TellyJelly_logo");
            texNG = Content.Load<Texture2D>("Images/new_game");
            texLDG = Content.Load<Texture2D>("Images/load_game");
            texOP = Content.Load<Texture2D>("Images/option");
            texQ = Content.Load<Texture2D>("Images/quit_game");
            texLG = Content.Load<Texture2D>("Images/TellyJelly_logo");

            bg = new ImageBackground(texBG, Color.White, graphicsDevice);
            logo = new Sprite3(true, texLG, 300, 5);
            NG = new Sprite3(true, texNG, 450, logo.getPosY() + texLG.Height + 20);
            LDG = new Sprite3(true, texLDG, 450, NG.getPosY() + texNG.Height + 10);
            OP = new Sprite3(true, texOP, 450, LDG.getPosY() + texLDG.Height + 10);
            Q = new Sprite3(true, texQ, 450, OP.getPosY() + texOP.Height + 10);

            buttons.addSpriteReuse(NG);
            buttons.addSpriteReuse(LDG);
            buttons.addSpriteReuse(OP);
            buttons.addSpriteReuse(Q);
        }
        public override void Update(GameTime gameTime)
        {
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            Rectangle mouseRect = new Rectangle(currentMouseState.X,currentMouseState.Y, 1, 1);
            for (int i = 0; i < buttons.count(); i++)
            {
                Sprite3 bttn = buttons.getSprite(i);
                if (mouseRect.Intersects(bttn.getBoundingBoxAA()))
                {
                    hover = true;
                    if (hover)
                    {
                        bttn.setColor(Color.Gray);
                    }
                    if (currentMouseState.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (bttn.getPosY() == NG.getPosY())
                            gameStateManager.setLevel(1);
                        if (bttn.getPosY() == OP.getPosY())
                            gameStateManager.setLevel(6);
                        if (bttn.getPosY() == Q.getPosY())
                            Environment.Exit(0);
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
            bg.Draw(spriteBatch);
            logo.Draw(spriteBatch);
            buttons.Draw(spriteBatch);
            spriteBatch.End();

        }
    }
}
