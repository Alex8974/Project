using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using CollisionExample.Collisions;
using System.Collections;
using System;
//using System.Windows.Forms;

namespace Project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private KeyboardState keyboardState;
        private KeyboardState prevkeyboardState;

        private List<Person> Team1 = new List<Person>();
        private List<Person> Team2 = new List<Person>();


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            prevkeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
            // TODO: Add your update logic here

            for(int i = 0; i < Team1.Count; i++)
            //foreach(Person p in Team1)
            {
                var p = Team1[i];
                foreach(Person p1 in Team2)
                {
                    if(CollisionHelper.Collides(p.Bounds, p1.Bounds) && p.IsAlive && p1.IsAlive)
                    {
                        p.Speed = 0;
                        p1.Speed = 0;

                        p1.Attack(p);
                        p.Attack(p1);

                        if(p.Health <= 0)
                        {
                            p.IsAlive = false;
                        }
                        if(p1.Health <= 0)
                        {
                            p1.IsAlive = false;
                        }

                    }
                }
                for(int j = i + 1; j < Team1.Count; j++)
                {
                    if (CollisionHelper.Collides(p.Bounds, Team1[j].Bounds))
                    {
                        //p.Speed = 0;
                        Team1[j].Speed = 0;
                        Team1[j].frameRow = SpriteSheetPicker.StandingRight;
                    }
                }
            }





            if (keyboardState.IsKeyDown(Keys.A) && !prevkeyboardState.IsKeyDown(Keys.A))
            {
                Team1.Add(new SwordsMan(Content, "Team1"));
            }

            if (keyboardState.IsKeyDown(Keys.S) && !prevkeyboardState.IsKeyDown(Keys.S))
            {
                Team2.Add(new SwordsMan(Content, "Team2"));
            }

            if (keyboardState.IsKeyDown(Keys.Q) && !prevkeyboardState.IsKeyDown(Keys.Q))
            {
                Team1.Add(new Archer(Content, "Team1"));
            }

            if (keyboardState.IsKeyDown(Keys.W) && !prevkeyboardState.IsKeyDown(Keys.W))
            {
                Team2.Add(new Archer(Content, "Team2"));
            }


            foreach (Person p in Team1) p.Update(gameTime);
            foreach (Person p in Team2) p.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            // TODO: Add your drawing code here
            foreach (Person p in Team1) p.Draw(_spriteBatch, gameTime);
            foreach (Person p in Team2) p.Draw(_spriteBatch, gameTime);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}