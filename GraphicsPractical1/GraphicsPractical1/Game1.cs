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

namespace GraphicsPractical1
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private FrameRateCounter frameRateCounter;
        private BasicEffect effect;
        private VertexPositionColor[] vertices;
        private Camera camera;
        private float angle;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.frameRateCounter = new FrameRateCounter(this);
            this.Components.Add(this.frameRateCounter);
        }

        protected override void Initialize()
        {
            this.graphics.PreferredBackBufferWidth = 800;
            this.graphics.PreferredBackBufferHeight = 600;
            this.graphics.IsFullScreen = false;
            this.graphics.SynchronizeWithVerticalRetrace = false;
            this.graphics.ApplyChanges();
            this.IsFixedTimeStep = false;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            this.effect = new BasicEffect(this.GraphicsDevice);
            this.setupVertices();
            this.effect.VertexColorEnabled = true;
            this.camera = new Camera(new Vector3(0, 0, 50), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            spriteBatch = new SpriteBatch(GraphicsDevice);

        }


        protected override void UnloadContent()
        {
 
        }

        protected override void Update(GameTime gameTime)
        {
            float timeStep = (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.Window.Title = "Graphics Tutorial | FPS: " + this.frameRateCounter.FrameRate;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            this.angle += timeStep * 3.0f;

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.RasterizerState = new RasterizerState
            {
                CullMode = CullMode.None
            };
            GraphicsDevice.Clear(Color.DarkSlateBlue);
            this.effect.Projection = this.camera.ProjectionMatrix;
            this.effect.View = this.camera.ViewMatrix;
            Vector3 rotAxis = new Vector3(3 * this.angle, this.angle, 2 * this.angle);
            rotAxis.Normalize();
            Matrix translation = Matrix.CreateTranslation(-20.0f / 3.0f, -10.0f / 3.0f, 0);
            Matrix rotation = Matrix.CreateFromAxisAngle(rotAxis, this.angle);
            this.effect.World = translation * rotation;
            foreach (EffectPass pass in this.effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                this.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, this.vertices, 0, 1, VertexPositionColor.VertexDeclaration);
            }
            base.Draw(gameTime);
        }

        private void setupVertices()
        {
            this.vertices = new VertexPositionColor[3];

            this.vertices[0].Position = new Vector3(0f, 0f, 0f);
            this.vertices[0].Color = Color.Red;
            this.vertices[1].Position = new Vector3(10f, 10f, 0f);
            this.vertices[1].Color = Color.Yellow;
            this.vertices[2].Position = new Vector3(10f, 0f, -5f);
            this.vertices[2].Color = Color.Green;
        }
    }
}
