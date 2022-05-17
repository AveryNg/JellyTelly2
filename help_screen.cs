using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RC_Framework;
namespace JellyTelly2
{
    public class help_screen: RC_GameStateParent
    {
        Texture2D texBG;
        ImageBackground lvlBg;
        SpriteFont font;
        public override void LoadContent()
        {
            spriteBatch = new SpriteBatch(graphicsDevice);
            LineBatch.init(graphicsDevice);
            texBG = Content.Load<Texture2D>("Images/candyWorld_bg");
            font = Content.Load<SpriteFont>("Fonty");
            lvlBg = new ImageBackground(texBG, Color.Gray, graphicsDevice);
        }
        public override void Update(GameTime gameTime)
        {
            prevKeyState = keyState;
            keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Back) && prevKeyState.IsKeyUp(Keys.Back))
            {
                gameStateManager.setLevel(0);
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            lvlBg.Draw(spriteBatch);
            spriteBatch.DrawString(font, "HELP GUIDE", new Vector2(10, 10), Color.Black);
            spriteBatch.DrawString(font, "1. Using arrow keys to move the character and space to jump", new Vector2(10, 100), Color.Black);
            spriteBatch.DrawString(font, "2. Collecting all the candies to open the door", new Vector2(10, 200), Color.Black);
            spriteBatch.DrawString(font, "3. Going through the door to proceed the next level", new Vector2(10, 300), Color.Black);
            spriteBatch.End();
        }
}
}
