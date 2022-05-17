using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RC_Framework;

namespace JellyTelly2
{
     public class lose_screen : RC_GameStateParent
    {
        Texture2D texBG;
        Texture2D texLG;
        Texture2D texLG2;
        Texture2D texLG3;
        Texture2D texLG4;
        Texture2D texStar;

        ImageBackground lvlBg;
        Sprite3 loseLogo;

        ParticleSystem p;
        Rectangle rectangle = new Rectangle(0, 0, 1020, 720);
        public override void LoadContent()
        {
            spriteBatch = new SpriteBatch(graphicsDevice);
            LineBatch.init(graphicsDevice);

            texBG = Content.Load<Texture2D>("Images/Lose_screen");
            texLG = Content.Load<Texture2D>("Images/lose_logo");
            texLG2 = Content.Load<Texture2D>("Images/lose_logo2");
            texLG3 = Content.Load<Texture2D>("Images/lose_logo3");
            texLG4 = Content.Load<Texture2D>("Images/lose_logo4");
            texStar = Content.Load<Texture2D>("Images/star");

            lvlBg = new ImageBackground(texBG, Color.White, graphicsDevice);
            loseLogo = new Sprite3(true, texLG, 100, 100);
            loseLogo.varInt0 = 0;
            setSys();
        }
        public override void Update(GameTime gameTime)
        {
            LogoAnimation(4);
            p.Update(gameTime);
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            lvlBg.Draw(spriteBatch);
            loseLogo.Draw(spriteBatch);
            p.Draw(spriteBatch);
            spriteBatch.End();
        }
        public void LogoAnimation(int time)
        {
            loseLogo.varInt0 = loseLogo.varInt0 + 1;
            if (loseLogo.varInt0 == 1) loseLogo.setTexture(texLG2, false);
            if (loseLogo.varInt0 == 1 + time) loseLogo.setTexture(texLG3, false);
            if (loseLogo.varInt0 == 1 + 2 * time - 1) loseLogo.setTexture(texLG4, false);
            if (loseLogo.varInt0 == 1 + 3 * time) loseLogo.setTexture(texLG3, false);
            if (loseLogo.varInt0 == 1 + 4 * time) loseLogo.setTexture(texLG2, false);
            if (loseLogo.varInt0 == 1 + 5 * time) loseLogo.setTexture(texLG, false);
            if (loseLogo.varInt0 == 1 + 6 * time) loseLogo.varInt0 = 0;
        }

        void setSys()
        {
            p = new ParticleSystem(new Vector2(100, 100), 40, 200, 102);
            p.setMandatory1(texStar, new Vector2(6, 6), new Vector2(24, 24), Color.Black, Color.DarkGreen);
            p.setMandatory2(-1, 1, 1, 3, 0);
            rectangle = new Rectangle(0, 0, 1020, 720);
            p.setMandatory3(120, rectangle);
            p.setMandatory4(new Vector2(0, 0.1f), new Vector2(1, 0), new Vector2(0, 0));
            p.randomDelta = new Vector2(0.1f, 0.1f);
            p.Origin = 1;
            p.originRectangle = new Rectangle(0, 0, 1020, 10);
            p.activate();
        }
    }
}
