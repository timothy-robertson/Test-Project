using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using PhysicsEngine;
using Airplane;

namespace DecoratorPattern
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private const int SCREEN_X = 1024;
        private const int SCREEN_Y = 680;
        private float simulationSpeed = 0.1f;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Texture2D pixel;

        private Texture2D paperAirplane;
        private Texture2D smallAircraft;
        private Texture2D jet;
        private Texture2D background;
        private Texture2D cursor;
        private Texture2D clouds;

        private SpriteFont menuFont;

        private AirplaneBase plane;

        private Rectangle sEngineRect = new Rectangle(SCREEN_X - 200 - 100, 70 - 5, 200, 30);
        private Rectangle mEngineRect = new Rectangle(SCREEN_X - 200 - 100, 90 - 5, 200, 30);
        private Rectangle lEngineRect = new Rectangle(SCREEN_X - 200 - 100, 110 - 5, 200, 30);

        private Rectangle sFuelRect = new Rectangle(SCREEN_X - 200 - 100, 170 - 5, 200, 30);
        private Rectangle mFuelRect = new Rectangle(SCREEN_X - 200 - 100, 190 - 5, 200, 30);
        private Rectangle lFuelRect = new Rectangle(SCREEN_X - 200 - 100, 210 - 5, 200, 30);

        private Rectangle sElevateRect = new Rectangle(SCREEN_X - 200 - 100, 270 - 5, 200, 30);
        private Rectangle mElevateRect = new Rectangle(SCREEN_X - 200 - 100, 290 - 5, 200, 30);
        private Rectangle lElevateRect = new Rectangle(SCREEN_X - 200 - 100, 310 - 5, 200, 30);

        private Rectangle sThrusterRect = new Rectangle(SCREEN_X - 200 - 100, 370 - 5, 200, 30);
        private Rectangle mThrusterRect = new Rectangle(SCREEN_X - 200 - 100, 390 - 5, 200, 30);
        private Rectangle lThrusterRect = new Rectangle(SCREEN_X - 200 - 100, 410 - 5, 200, 30);

        private Rectangle undoRect = new Rectangle(SCREEN_X - 200 - 100, 500 - 5, 200, 30);

        private MouseState previousMouseState = Mouse.GetState();

        private bool inMenu = true;
        private bool planeSelection = true;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            graphics.PreferredBackBufferWidth = SCREEN_X;
            graphics.PreferredBackBufferHeight = SCREEN_Y;
            graphics.ApplyChanges();

            pixel = Content.Load<Texture2D>("pixel");
            paperAirplane = Content.Load<Texture2D>("PaperAirplane");
            smallAircraft = Content.Load<Texture2D>("SmallAircraft");
            jet = Content.Load<Texture2D>("Jet");
            background = Content.Load<Texture2D>("BackgroundBlank");
            cursor = Content.Load<Texture2D>("Cursor");
            clouds = Content.Load<Texture2D>("Clouds");

            menuFont = Content.Load<SpriteFont>("MenuFont");
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            if (inMenu)
            {
                UpdateMenu(gameTime);
            }
            else
            {
                UpdateGame(gameTime);
            }

            base.Update(gameTime);
        }

        public void UpdateMenu(GameTime time)
        {
            if (planeSelection)
            {
                Rectangle paperBox = new Rectangle(12, 50, paperAirplane.Width, paperAirplane.Height);
                Rectangle smallBox = new Rectangle(12 + paperAirplane.Width + 12, 50, smallAircraft.Width, smallAircraft.Height);
                Rectangle jetBox = new Rectangle(12 + paperAirplane.Width + 12 + smallAircraft.Width + 12, 50, jet.Width, jet.Height);
                Point mousePos = new Point(Mouse.GetState().X, Mouse.GetState().Y);

                if (Mouse.GetState().LeftButton.Equals(ButtonState.Pressed) && previousMouseState.LeftButton.Equals(ButtonState.Released))
                {
                    if (paperBox.Contains(mousePos))
                    {
                        plane = new PaperAirplane(paperAirplane);
                        plane.SetPosition(new Vector2(34, SCREEN_Y / 2));
                        planeSelection = false;
                    }
                    else if (smallBox.Contains(mousePos))
                    {
                        plane = new SmallAirplane(smallAircraft);
                        plane.SetPosition(new Vector2(34, SCREEN_Y / 2));
                        planeSelection = false;
                    }
                    else if (jetBox.Contains(mousePos))
                    {
                        plane = new FighterJet(jet);
                        plane.SetPosition(new Vector2(34, SCREEN_Y / 2));
                        planeSelection = false;
                    }
                }
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    plane.AddInitialRotation(-0.01f);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    plane.AddInitialRotation(0.01f);
                }

                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift))
                {
                    plane.Launch(time);
                    inMenu = false;
                }

                Point mousePos = new Point(Mouse.GetState().X, Mouse.GetState().Y);

                if (Mouse.GetState().LeftButton.Equals(ButtonState.Pressed) && previousMouseState.LeftButton.Equals(ButtonState.Released))
                {
                    if (sEngineRect.Contains(mousePos))
                    {
                        plane = new SmallEngine(plane);
                    }
                    else if (mEngineRect.Contains(mousePos))
                    {
                        plane = new MediumEngine(plane);
                    }
                    else if (lEngineRect.Contains(mousePos))
                    {
                        plane = new LargeEngine(plane);
                    }
                    else if (sFuelRect.Contains(mousePos))
                    {
                        plane = new SmallFuelTank(plane);
                    }
                    else if (mFuelRect.Contains(mousePos))
                    {
                        plane = new MediumFuelTank(plane);
                    }
                    else if (lFuelRect.Contains(mousePos))
                    {
                        plane = new LargeFuelTank(plane);
                    }
                    else if (sElevateRect.Contains(mousePos))
                    {
                        plane = new SmallElevators(plane);
                    }
                    else if (mElevateRect.Contains(mousePos))
                    {
                        plane = new MediumElevators(plane);
                    }
                    else if (lElevateRect.Contains(mousePos))
                    {
                        plane = new LargeElevators(plane);
                    }
                    else if (sThrusterRect.Contains(mousePos))
                    {
                        plane = new SmallThruster(plane);
                    }
                    else if (mThrusterRect.Contains(mousePos))
                    {
                        plane = new MediumThruster(plane);
                    }
                    else if (lThrusterRect.Contains(mousePos))
                    {
                        plane = new LargeThruster(plane);
                    }
                    else if (undoRect.Contains(mousePos))
                    {
                        try
                        {
                            plane = ((AirplanePart)plane).RemovePart();
                        }
                        catch (Exception e)
                        {
                            Console.Out.WriteLine("No Parts to Remove!");
                        }
                    }
                }
            }
            previousMouseState = Mouse.GetState();
        }

        public void UpdateGame(GameTime time)
        {
            GameTime fakeTime = new GameTime(time.TotalGameTime, new TimeSpan((long)((float)time.ElapsedGameTime.Ticks * simulationSpeed)));
            ForceIntegratorRegistry.IntegrateForces(fakeTime);
            plane.Update(fakeTime);

            if (plane.GetPosition().Y >= SCREEN_Y)
            {
                plane = null;
                inMenu = true;
                planeSelection = true;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            spriteBatch.Draw(clouds, Vector2.Zero, Color.White);
            
            if (inMenu)
            {
                DrawMenu(gameTime, spriteBatch);
            }
            else
            {
                int x = (int)(plane.GetPosition().X) / SCREEN_X;
                int y = (int)(plane.GetPosition().Y) / SCREEN_Y;
                
                Vector2 pos = plane.GetPosition();

                if (pos.Y < 0.0f)
                {
                    Vector2 guide = new Vector2((pos.X - (x * SCREEN_X)), 12.0f);
                    spriteBatch.Draw(cursor, guide, Color.Red);
                    spriteBatch.DrawString(menuFont, ("" + (-(pos.Y - 100))), new Vector2(guide.X, guide.Y + 15), Color.Black);
                }

                plane.SetPosition(new Vector2((pos.X - (x * SCREEN_X)), pos.Y));
                DrawGame(gameTime, spriteBatch);
                plane.SetPosition(pos);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void DrawGame(GameTime time, SpriteBatch batch)
        {
            plane.Draw(batch);
        }

        public void DrawMenu(GameTime time, SpriteBatch batch)
        {

            if (planeSelection)
            {
                batch.DrawString(menuFont, "Choose your Aircraft", new Vector2(12, 12), Color.Black);
                batch.Draw(paperAirplane, new Vector2(12, 50), Color.White);
                batch.Draw(smallAircraft, new Vector2(12 + paperAirplane.Width + 12, 50), Color.White);
                batch.Draw(jet, new Vector2(12 + paperAirplane.Width + 12 + smallAircraft.Width + 12, 50), Color.White);
            }
            else
            {
                batch.DrawString(menuFont, "Press Shift To Launch", new Vector2(SCREEN_X - 250, SCREEN_Y - 24), Color.Black);
                batch.DrawString(menuFont, "Add your parts", new Vector2(SCREEN_X - 250, 12), Color.Black);

                batch.DrawString(menuFont, "Engines", new Vector2(SCREEN_X - 250, 50), Color.Black);
                batch.DrawString(menuFont, "Small", new Vector2(SCREEN_X - 200, 70), Color.Black);
                batch.DrawString(menuFont, "Medium", new Vector2(SCREEN_X - 200, 90), Color.Black);
                batch.DrawString(menuFont, "Large", new Vector2(SCREEN_X - 200, 110), Color.Black);

                batch.DrawString(menuFont, "Fuel Tanks", new Vector2(SCREEN_X - 250, 150), Color.Black);
                batch.DrawString(menuFont, "Small", new Vector2(SCREEN_X - 200, 170), Color.Black);
                batch.DrawString(menuFont, "Medium", new Vector2(SCREEN_X - 200, 190), Color.Black);
                batch.DrawString(menuFont, "Large", new Vector2(SCREEN_X - 200, 210), Color.Black);

                batch.DrawString(menuFont, "Elevators", new Vector2(SCREEN_X - 250, 250), Color.Black);
                batch.DrawString(menuFont, "Small", new Vector2(SCREEN_X - 200, 270), Color.Black);
                batch.DrawString(menuFont, "Medium", new Vector2(SCREEN_X - 200, 290), Color.Black);
                batch.DrawString(menuFont, "Large", new Vector2(SCREEN_X - 200, 310), Color.Black);

                batch.DrawString(menuFont, "Thrusters", new Vector2(SCREEN_X - 250, 350), Color.Black);
                batch.DrawString(menuFont, "Small", new Vector2(SCREEN_X - 200, 370), Color.Black);
                batch.DrawString(menuFont, "Medium", new Vector2(SCREEN_X - 200, 390), Color.Black);
                batch.DrawString(menuFont, "Large", new Vector2(SCREEN_X - 200, 410), Color.Black);


                batch.DrawString(menuFont, "UNDO", new Vector2(SCREEN_X - 200, 500), Color.Black);

                batch.DrawString(menuFont, plane.GetString(), new Vector2(SCREEN_X - 500, 50), Color.Black);
                plane.Draw(batch);
            }

            batch.Draw(cursor, new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Color.White);
        
            
        }
    }
}
