using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RC_Framework;

namespace JellyTelly2
{
    public class win_screen : RC_GameStateParent
    {
        Texture2D texBG;
        Texture2D texLG;
        Texture2D texLG2;
        Texture2D texLG3;
        Texture2D texLG4;
        Texture2D texStar;

        ImageBackground lvlBg;
        Sprite3 winLogo;

        ParticleSystem p;
        Rectangle rectangle = new Rectangle(0, 0, 1020, 720);
        public override void LoadContent()
        {
            spriteBatch = new SpriteBatch(graphicsDevice);
            LineBatch.init(graphicsDevice);

            texBG = Content.Load<Texture2D>("Images/win_screen");
            texLG = Content.Load<Texture2D>("Images/win_logo");
            texLG2 = Content.Load<Texture2D>("Images/win_logo2");
            texLG3 = Content.Load<Texture2D>("Images/win_logo3");
            texLG4 = Content.Load<Texture2D>("Images/win_logo4");
            texStar = Content.Load<Texture2D>("Images/star");


            lvlBg = new ImageBackground(texBG, Color.White, graphicsDevice);
            winLogo = new Sprite3(true, texLG, 100, 100);
            winLogo.varInt0 = 0;
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
            winLogo.Draw(spriteBatch);
            p.Draw(spriteBatch);
            spriteBatch.End();
        }
        public void LogoAnimation(int time)
        {
            winLogo.varInt0 = winLogo.varInt0 + 1;
            if (winLogo.varInt0 == 1) winLogo.setTexture(texLG2, false);
            if (winLogo.varInt0 == 1 + time) winLogo.setTexture(texLG3, false);
            if (winLogo.varInt0 == 1 + 2 * time - 1) winLogo.setTexture(texLG4, false);
            if (winLogo.varInt0 == 1 + 3 * time) winLogo.setTexture(texLG3, false);
            if (winLogo.varInt0 == 1 + 4 * time) winLogo.setTexture(texLG2, false);
            if (winLogo.varInt0 == 1 + 5 * time) winLogo.setTexture(texLG, false);
            if (winLogo.varInt0 == 1 + 6 * time) winLogo.varInt0 = 0;
        }

        void setSys()
        {
            p = new ParticleSystem(new Vector2(100, 100), 40, 200, 102);
            p.setMandatory1(texStar, new Vector2(6, 6), new Vector2(24, 24), Color.White, new Color(255, 255, 255, 100));
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
