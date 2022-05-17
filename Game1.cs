using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RC_Framework;

namespace JellyTelly2
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        RC_GameStateManager lvlManager;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1020;
            graphics.PreferredBackBufferHeight = 720;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            LineBatch.init(GraphicsDevice);
           
            lvlManager = new RC_GameStateManager();
            lvlManager.AddLevel(0,new Main_Menu());
            lvlManager.getLevel(0).InitializeLevel(GraphicsDevice, spriteBatch, Content, lvlManager);
            lvlManager.getLevel(0).LoadContent();
            lvlManager.setLevel(0);

            lvlManager.AddLevel(1, new level_menu());
            lvlManager.getLevel(1).InitializeLevel(GraphicsDevice, spriteBatch, Content, lvlManager);
            lvlManager.getLevel(1).LoadContent();

            lvlManager.AddLevel(2, new level_1());
            lvlManager.getLevel(2).InitializeLevel(GraphicsDevice, spriteBatch, Content, lvlManager);
            lvlManager.getLevel(2).LoadContent();

            lvlManager.AddLevel(3, new Level_2());
            lvlManager.getLevel(3).InitializeLevel(GraphicsDevice, spriteBatch, Content, lvlManager);
            lvlManager.getLevel(3).LoadContent();

            lvlManager.AddLevel(4, new win_screen());
            lvlManager.getLevel(4).InitializeLevel(GraphicsDevice, spriteBatch, Content, lvlManager);
            lvlManager.getLevel(4).LoadContent();

            lvlManager.AddLevel(5, new lose_screen());
            lvlManager.getLevel(5).InitializeLevel(GraphicsDevice, spriteBatch, Content, lvlManager);
            lvlManager.getLevel(5).LoadContent();

            lvlManager.AddLevel(6, new help_screen());
            lvlManager.getLevel(6).InitializeLevel(GraphicsDevice, spriteBatch, Content, lvlManager);
            lvlManager.getLevel(6).LoadContent();

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            lvlManager.getCurrentLevel().Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            lvlManager.getCurrentLevel().Draw(gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
