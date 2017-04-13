using Manager.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Subsystems
{
    public class SkyboxSystem : Core
    {
        private GraphicsDeviceManager graphics;
        private Model skyModel;
        private BasicEffect skyEffect;
        private Matrix viewM, projM, skyworldM, world;
        private static float skyscale = 1000;
        private float slow = skyscale / 200f;  // step width of movements
        private Vector3 nullPos = Vector3.Zero;
        public SkyboxSystem(Matrix world)
        {
            this.world = world;
            skyModel = Engine.GetInst().Content.Load<Model>("skybox");
            skyEffect = (BasicEffect)skyModel.Meshes[0].Effects[0];
            graphics = Engine.GetInst().graphics;
        }
        public override void update(GameTime gameTime)
        {
            foreach (var entity in Engine.GetInst().Entities.Values)
            {
                var transformComponent = entity.GetComponent<TransformComponent>();
                if (transformComponent == null)
                    continue;
                var cameraComponent = entity.GetComponent<CameraComponent>();
                var position = transformComponent.position;
                var view = transformComponent.scale;

                var scale = transformComponent.scale;
                var rotation = transformComponent.rotation;
                var objectWorld = transformComponent.objectWorld;
                var elapsedGameTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                skyworldM = Matrix.CreateScale(skyscale, skyscale, skyscale);
                projM = Matrix.CreatePerspectiveFieldOfView(MathHelper.Pi / 3, 1f, 1f, 10f * skyscale);
                viewM = cameraComponent.view;
            }
        }
        public override void draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.RasterizerState = RasterizerState.CullNone;
            skyEffect.World = skyworldM;
            skyEffect.View = viewM;
            skyEffect.Projection = projM;
            skyModel.Meshes[0].Draw();
            graphics.GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
        }
    }
}
