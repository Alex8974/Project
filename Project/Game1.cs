﻿using Microsoft.Xna.Framework;
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
using Project.Screens;
//using SharpDX.Direct3D9;
using ParticleSystemExample;
using SharpDX.MediaFoundation;
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
        Song song1;
        Song song2;
        private SoundEffect swordSlash;
        Texture2D backgoundTexture;
        double timer = 260;
        //double timer = 20; //test line of code
        double summontime = 0;
        double cooldown = 4;
        
        #region the screens

        private StartScreen startScreen;
        private ControlScreen controlScreen;
        private PauseScreen pauseScreen;
        private WinLoseScreen winLoseScreen;

        #endregion


        private List<Person> Team1 = new List<Person>();
        private List<Person> Team2 = new List<Person>();

        private World world;
        private ExplosionParticleSystem _explosion;
        private FireworkParticleSystem _fireworks;


        private Camera camera;
        int cameraZoom = 0;

        public Game1()
        {
            _explosion = new ExplosionParticleSystem(this, 20);
            _fireworks = new FireworkParticleSystem(this, 1000);
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

            startScreen = new StartScreen();
            controlScreen = new ControlScreen();
            pauseScreen = new PauseScreen();
            winLoseScreen = new WinLoseScreen();

            foreach (var edge in edges)
            {
                edge.BodyType = BodyType.Static;
                edge.SetRestitution(1.0f);
            }
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            camera = new Camera(GraphicsDevice.Viewport, 800, 480);
            startScreen.Initilze(Content);
            controlScreen.Initilze(Content);
            Components.Add(_explosion);
            Components.Add(_fireworks);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("bangers");
            song1 = Content.Load<Song>("song1");
            song2 = Content.Load<Song>("song2");
            backgoundTexture = Content.Load<Texture2D>("Backgound");
            MediaPlayer.Play(song1);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            #region zoom
            if (keyboardState.IsKeyDown(Keys.Z))
            {
                camera.ZoomIn(1.05f);
            }

            // Zoom out with the 'X' key
            if (keyboardState.IsKeyDown(Keys.X))
            {
                camera.ZoomOut(1.05f);
            }

            camera.UpdateTransform(GraphicsDevice.Viewport);
            //if(Team1.Count > 0)
            //{
            //    if (camera.Position.X > Team1[0].Position.X) 
            //        camera.Move(new Vector2(-1, 0));
            //    else if (camera.Position.X < Team1[0].Position.X) 
            //        camera.Move(new Vector2(1, 0));
            //    if (camera.Position.Y > Team1[0].Position.Y) camera.Move(new Vector2(0, -1));
            //    else if (camera.Position.Y < Team1[0].Position.Y) camera.Move(new Vector2(0, 1));
            //}
            //else if(Team1.Count == 0 && Team2.Count != 0)
            //{
            //    if (camera.Position.X > Team2[0].Position.X)
            //        camera.Move(new Vector2(-1, 0));
            //    else if (camera.Position.X < Team2[0].Position.X)
            //        camera.Move(new Vector2(1, 0));
            //    if (camera.Position.Y > Team2[0].Position.Y) camera.Move(new Vector2(0, -1));
            //    else if (camera.Position.Y < Team2[0].Position.Y) camera.Move(new Vector2(0, 1));
            //}

            if (keyboardState.IsKeyDown(Keys.Left)) camera.Move(new Vector2(-2, 0));
            if (keyboardState.IsKeyDown(Keys.Right)) camera.Move(new Vector2(2, 0));
            if (keyboardState.IsKeyDown(Keys.Up)) camera.Move(new Vector2(0, -2));
            if (keyboardState.IsKeyDown(Keys.Down)) camera.Move(new Vector2(0, 2));
            #endregion

            #region Check for win/loss

                bool[] winloss = winLoseScreen.CheckforWin(Team1, Team2);
            if (winloss[0] || timer < 0) gameScreens = GameScreens.Win;
            if (winloss[1]) gameScreens = GameScreens.Lose;
            if(gameScreens == GameScreens.Win || gameScreens == GameScreens.Lose)
            {
                 if(winLoseScreen.Update(gameTime, keyboardState, _fireworks)) Exit();
            }

            #endregion

            prevkeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
            // TODO: Add your update logic here

            #region start screen

            if (gameScreens == GameScreens.Start)
            {
                if(startScreen.Update(gameTime, keyboardState) == GameScreens.Running)
                {
                    gameScreens = GameScreens.Running;
                    MediaPlayer.Stop();
                    MediaPlayer.Play(song2);
                }
                if (startScreen.Update(gameTime, keyboardState) == GameScreens.Controls) gameScreens = GameScreens.Controls;
            }
            #endregion

            else if (gameScreens == GameScreens.Controls) { if (controlScreen.Update(gameTime, keyboardState) != GameScreens.Controls) { gameScreens = GameScreens.Start; } }

            #region running / pause screen

            else
            {
                if (gameScreens == GameScreens.Pause)
                {
                    timer += gameTime.ElapsedGameTime.TotalSeconds;
                    if (pauseScreen.Update(gameTime, keyboardState)) Exit();                    
                }

                if (gameScreens == GameScreens.Running)
                {
                    #region cheking for attack 

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

                    #region Keys for summon

                    if (keyboardState.IsKeyDown(Keys.A) && !prevkeyboardState.IsKeyDown(Keys.A))
                    {
                        Team1.Add(new SwordsMan(Content, "Team1"));
                    }
                    if(Team2.Count == 0) Team2.Add(new Dragon(Content, -1, _explosion));

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

                    if (keyboardState.IsKeyDown(Keys.S) && !prevkeyboardState.IsKeyDown(Keys.S))
                    {
                        Team1.Add(new Archer(Content, "Team1"));
                    }

                    //if (keyboardState.IsKeyDown(Keys.W) && !prevkeyboardState.IsKeyDown(Keys.W))
                    //{
                    //    Team2.Add(new Archer(Content, "Team2"));
                    //}

                    #endregion

                    foreach (Person p in Team1) p.Update(gameTime);
                    foreach (Person p in Team2) p.Update(gameTime);

                    #region cleaning lists

                    for (int i = 0; i < Team1.Count; i++) if (Team1[i].IsAlive == false) Team1.Remove(Team1[i]);
                    for (int i = 0; i < Team2.Count; i++) if (Team2[i].IsAlive == false) Team2.Remove(Team2[i]);

                    #endregion
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

            #endregion

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            

            //_spriteBatch.Begin(transformMatrix: camera.Transform);
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera.Transform);
            #region The win / lose section
            if (gameScreens == GameScreens.Win || gameScreens == GameScreens.Lose)
            {
                if (gameScreens == GameScreens.Win) _spriteBatch.DrawString(font, "You Win!", new Vector2(300, 150), Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
                else if (gameScreens == GameScreens.Lose) _spriteBatch.DrawString(font, "You Lose", new Vector2(300, 150), Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
                _spriteBatch.DrawString(font, "Press (ESC) to Quit", new Vector2(295, 200), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
            }
            #endregion

            if (gameScreens == GameScreens.Start) startScreen.Draw(gameTime, _spriteBatch, font, backgoundTexture);

            else if(gameScreens == GameScreens.Controls) controlScreen.Draw(gameTime, _spriteBatch, font, backgoundTexture);
            
            else if(gameScreens == GameScreens.Running || gameScreens == GameScreens.Pause)
            {
                _spriteBatch.Draw(backgoundTexture, new Vector2(0, 0), Color.White);
                timer -= gameTime.ElapsedGameTime.TotalSeconds ;

                if (gameScreens == GameScreens.Pause) pauseScreen.Draw(gameTime, _spriteBatch, font);

                foreach (Person p in Team1) p.Draw(_spriteBatch, gameTime);
                foreach (Person p in Team2) p.Draw(_spriteBatch, gameTime);

                //_spriteBatch.DrawString(font, $"{Math.Round( timer)} : Seconds to Surivive", new Vector2(camera.Position.X - (80 / camera.Scale), camera.Position.Y- (200 / camera.Scale)), Color.Black, 0, new Vector2(0, 0), 0.5f / camera.Scale, SpriteEffects.None, 0);
            }
            
            _spriteBatch.End();

            _spriteBatch.Begin();
            if (gameScreens == GameScreens.Running || gameScreens == GameScreens.Pause)
            {
                _spriteBatch.DrawString(font, $"{Math.Round(timer)} : Seconds to Surivive", new Vector2(280, 50), Color.Black, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
            }
            _spriteBatch.End();

            base.Draw(gameTime);

        }
    }
}