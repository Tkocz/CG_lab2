using Manager.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Subsystems
{
    public class InputSystem : Core
    {
        public override void update(GameTime gameTime)
        {
            float elapsedGameTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            foreach (var entity in Engine.GetInst().Entities.Values)
            {
                var tC = entity.GetComponent<TransformComponent>();
                var userInput = entity.GetComponent<InputComponent>();
                if (tC == null || userInput == null)
                    continue;

                Quaternion addRot;
                float yaw = 0, pitch = 0, roll = 0;
                float angle = elapsedGameTime * 0.001f;

                float rotation = 0;

                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    tC.currentKey = Keys.Up;
                    if (tC.speed < 1)
                        tC.speed += 0.01f * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (tC.modelRotation < tC.MAXROTATION)
                    {
                        if (tC.direction)
                            tC.modelRotation += tC.speed * tC.rotationSpeed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                        else
                            tC.modelRotation -= tC.speed * tC.rotationSpeed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    }
                    else
                    {
                        if (!tC.direction)
                            tC.modelRotation -= tC.speed * tC.rotationSpeed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                        else
                            tC.modelRotation += tC.speed * tC.rotationSpeed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    }

                    if (tC.modelRotation > tC.MAXROTATION || tC.modelRotation < -tC.MAXROTATION)
                        tC.direction = !tC.direction;
                    tC.position += tC.speed * elapsedGameTime * tC.objectWorld.Backward;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    tC.currentKey = Keys.Left;
                    rotation += tC.speed * MathHelper.PiOver4 * (float)gameTime.ElapsedGameTime.Milliseconds;
                    tC.speed -= 0.01f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    yaw = angle;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    tC.currentKey = Keys.Right;
                    rotation -= tC.speed * MathHelper.PiOver4 * (float)gameTime.ElapsedGameTime.Milliseconds;
                    tC.speed -= 0.01f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    yaw = -angle;
                }
                if (tC.currentKey != Keys.Up && tC.currentKey != Keys.Left && tC.currentKey != Keys.Right)
                {
                    tC.currentKey = Keys.None;
                    if (tC.speed > 0)
                        tC.speed -= 0.1f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (tC.speed < 0)
                        tC.speed = 0;
                }
                addRot = Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll);

                addRot.Normalize();
                tC.orientation *= addRot;
            }
        }
    }
}
