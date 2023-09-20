using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using CollisionExample.Collisions;
using System.Collections;
using System;
using nkast.Aether.Physics2D.Dynamics;
using Microsoft.VisualBasic;
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

        private World world;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            world = new World();
            world.Gravity = new Vector2(0, 2);
            var top = 0;
            var bottom = 450;
            var left = 0;
            var right = 750;
            var edges = new Body[] {
                world.CreateEdge(new Vector2(left, top), new Vector2(right, top)),
                world.CreateEdge(new Vector2(left, top), new Vector2(left, bottom)),
                world.CreateEdge(new Vector2(left, bottom), new Vector2(right, bottom)),
                world.CreateEdge(new Vector2(right, top), new Vector2(right, bottom))
            };

            foreach (var edge in edges)
            {
                edge.BodyType = BodyType.Static;
                edge.SetRestitution(1.0f);
            }

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
            #region new attack 

            for( int i = 0; i < Math.Max(Team1.Count, Team2.Count); i++)
            {
                //check attacks for team 1
                if(i < Team1.Count)
                {
                    Team1[i].CheckForAttack(Team2);
                    for (int j = i + 1; j < Team1.Count; j++)
                    {
                        if (CollisionHelper.Collides(Team1[i].Bounds, Team1[j].Bounds))
                        {
                            //p.Speed = 0;
                            Team1[j].Speed = 0;
                            Team1[j].frameRow = SpriteSheetPicker.StandingRight;
                        }
                    }
                }
                //check attacks for team 2
                if(i < Team2.Count)
                {
                    Team2[i].CheckForAttack(Team1);
                    for (int j = i + 1; j < Team2.Count; j++)
                    {
                        if (CollisionHelper.Collides(Team2[i].Bounds, Team2[j].Bounds))
                        {
                            //p.Speed = 0;
                            Team2[j].Speed = 0;
                            Team2[j].frameRow = SpriteSheetPicker.StandingRight;
                        }
                    }
                }
            }
            #endregion

            #region old attack 
            //for (int i = 0; i < Team1.Count; i++)
            //    foreach (Person p in Team1)
            //    {
            //        var p = Team1[i];
            //        foreach (Person p1 in Team2)
            //        {
            //            Team1[i].CheckForAttack(Team2);
            //            if (CollisionHelper.Collides(p.Bounds, p1.Bounds) && p.IsAlive && p1.IsAlive)
            //            {
            //                p.Speed = 0;
            //                p1.Speed = 0;

            //                p1.Attack(p);
            //                p.Attack(p1);

            //                if (p.Health <= 0)
            //                {
            //                    p.IsAlive = false;
            //                }
            //                if (p1.Health <= 0)
            //                {
            //                    p1.IsAlive = false;
            //                }
            //            }
            //        }
            //        for (int j = i + 1; j < Team1.Count; j++)
            //        {
            //            if (CollisionHelper.Collides(p.Bounds, Team1[j].Bounds))
            //            {
            //                //p.Speed = 0;
            //                Team1[j].Speed = 0;
            //                Team1[j].frameRow = SpriteSheetPicker.StandingRight;
            //            }
            //        }
            //    }

            #endregion


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