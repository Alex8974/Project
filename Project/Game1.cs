using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using CollisionExample.Collisions;
using System.Collections;
using System;
using nkast.Aether.Physics2D.Dynamics;
using Microsoft.VisualBasic;
using System.Xml.Schema;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
//using System.Windows.Forms;

namespace Project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private KeyboardState keyboardState;
        private KeyboardState prevkeyboardState;
        private GameScreens gameScreens = GameScreens.Start;
        private SpriteFont font;
        private double totalPauseTime = 0;
        SongCollection songCollection;
        Song song1;
        Song song2;
        private SoundEffect swordSlash;
        Texture2D backgoundTexture;
        double timer = 260;
        double summontime = 0;
        double cooldown = 4;

        private List<Person> StartscreenTeam = new List<Person>();
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
            font = Content.Load<SpriteFont>("bangers");
            song1 = Content.Load<Song>("song1");
            song2 = Content.Load<Song>("song2");
            backgoundTexture = Content.Load<Texture2D>("Backgound");
            StartscreenTeam.Add(new SwordsMan(Content, "Team1"));
            StartscreenTeam.Add(new SwordsMan(Content, "Team2"));
            StartscreenTeam[0].Position = new Vector2(400, 327);
            StartscreenTeam[1].Position = new Vector2(450, 327);


            MediaPlayer.Play(song1);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {

            bool[] winloss = CheckForWin();
            if (winloss[0] || timer < 0) gameScreens = GameScreens.Win;
            if (winloss[1]) gameScreens = GameScreens.Lose;
            if(gameScreens == GameScreens.Win || gameScreens == GameScreens.Lose)
            {
                if (keyboardState.IsKeyDown(Keys.Escape)) Exit();
            }

            prevkeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
            // TODO: Add your update logic here

            if(gameScreens == GameScreens.Start)
            {
                if (keyboardState.IsKeyDown(Keys.Enter))
                {
                    gameScreens = GameScreens.Running;
                    MediaPlayer.Stop();
                    MediaPlayer.Play(song2);
                }
            }
            else
            {
                if (gameScreens == GameScreens.Pause)
                {
                    timer += gameTime.ElapsedGameTime.TotalSeconds;
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                        Exit();
                }

                if (gameScreens == GameScreens.Running)
                {
                    #region new attack 

                for (int i = 0; i < Math.Max(Team1.Count, Team2.Count); i++)
                {
                    //check attacks for team 1
                    if (i < Team1.Count)
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
                    if (i < Team2.Count)
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

                    #region Keys for summon

                    if (keyboardState.IsKeyDown(Keys.A) && !prevkeyboardState.IsKeyDown(Keys.A))
                    {
                        Team1.Add(new SwordsMan(Content, "Team1"));
                    }
                    if (timer < 165) cooldown = 2;
                    if(summontime >= cooldown)
                    {
                        Team2.Add(new SwordsMan(Content, "Team2"));
                        summontime = 0;
                    }
                    else summontime += gameTime.ElapsedGameTime.TotalSeconds;

                //if (keyboardState.IsKeyDown(Keys.S) && !prevkeyboardState.IsKeyDown(Keys.S))
                //{
                //    Team2.Add(new SwordsMan(Content, "Team2"));
                //}

                //if (keyboardState.IsKeyDown(Keys.Q) && !prevkeyboardState.IsKeyDown(Keys.Q))
                //{
                //    Team1.Add(new Archer(Content, "Team1"));
                //}

                //if (keyboardState.IsKeyDown(Keys.W) && !prevkeyboardState.IsKeyDown(Keys.W))
                //{
                //    Team2.Add(new Archer(Content, "Team2"));
                //}

                #endregion

                    foreach (Person p in Team1) p.Update(gameTime);
                    foreach (Person p in Team2) p.Update(gameTime);
                }

                if (keyboardState.IsKeyDown(Keys.P) && !prevkeyboardState.IsKeyDown(Keys.P))
                {
                    if (gameScreens == GameScreens.Pause)
                    {
                        gameScreens = GameScreens.Running;
                        MediaPlayer.Resume();
                    }
                    else if (gameScreens == GameScreens.Running)
                    {
                        gameScreens = GameScreens.Pause;
                        MediaPlayer.Pause();
                    }
                }
            }
            

            base.Update(gameTime);
        }

        private bool[] CheckForWin()
        {
            bool[] winLoss = new bool[2];
            winLoss[0] = false;
            winLoss[0] = false;
            foreach (Person p in Team1) if (p.Position.X > 750 && p.IsAlive == true) winLoss[0] = true;
            foreach (Person p in Team2) if (p.Position.X < 0 && p.IsAlive == true) winLoss[1] = true;
            return winLoss;
        }

        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            if(gameScreens == GameScreens.Win || gameScreens == GameScreens.Lose)
            {
                if (gameScreens == GameScreens.Win) _spriteBatch.DrawString(font, "You Win!", new Vector2(325, 225), Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
                else if (gameScreens == GameScreens.Lose) _spriteBatch.DrawString(font, "You Lose", new Vector2(325, 225), Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
                _spriteBatch.DrawString(font, "Press (ESC) to Quit", new Vector2(325, 275), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
            }
            else _spriteBatch.Draw(backgoundTexture, new Vector2(0, 0), Color.White);

            // TODO: Add your drawing code here
            if(gameScreens == GameScreens.Start)
            {
                //put backgound here 
                _spriteBatch.DrawString(font, "Castle Crusaders", new Vector2(250, 150), Color.Black);
                _spriteBatch.DrawString(font, "Press 'Enter' to start", new Vector2(300, 200), Color.Black, 0, new Vector2(0,0), 0.5f, SpriteEffects.None, 0);
                foreach (Person p in StartscreenTeam) p.Draw(_spriteBatch, gameTime);
            }
            if(gameScreens == GameScreens.Running || gameScreens == GameScreens.Pause)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds ;

                if(Team1.Count == 0)
                {
                    _spriteBatch.DrawString(font, "Press (A) to summon", new Vector2(307, 100), Color.Black, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
                    _spriteBatch.DrawString(font, "Press (P) to Pause", new Vector2(320, 125), Color.Black, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);

                }
                //put other backgound here
                if (gameScreens == GameScreens.Pause)
                {
                    _spriteBatch.DrawString(font, "Game Paused", new Vector2(250, 150), Color.Black);
                    _spriteBatch.DrawString(font, "(P) to Unpause", new Vector2(325, 200), Color.Black, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
                    _spriteBatch.DrawString(font, "(ESC) to Quit", new Vector2(325, 225), Color.Black, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);

                }
                foreach (Person p in Team1) p.Draw(_spriteBatch, gameTime);
                foreach (Person p in Team2) p.Draw(_spriteBatch, gameTime);

                _spriteBatch.DrawString(font, $"{Math.Round( timer)} : Seconds to Surivive", new Vector2(310, 50), Color.Black, 0, new Vector2(0, 0), 0.25f, SpriteEffects.None, 0);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}