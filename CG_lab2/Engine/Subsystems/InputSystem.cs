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
                if (tC == null)
                    continue;
                var userInput = entity.GetComponent<InputComponent>();

                if (Keyboard.GetState().IsKeyDown(userInput.add))
                    tC.scale *= 1.1f; // should be changed to distance from camera

                if (Keyboard.GetState().IsKeyDown(userInput.sub))
                    tC.scale *= 0.9f; // should be changed to distance from camera

                if (Keyboard.GetState().IsKeyDown(userInput.a))
					tC.position += tC.speed.X * elapsedGameTime * tC.objectWorld.Left;

				if (Keyboard.GetState().IsKeyDown(userInput.d))
					tC.position += tC.speed.X * elapsedGameTime * tC.objectWorld.Right;

                if (Keyboard.GetState().IsKeyDown(userInput.w))
					tC.position += tC.speed.Z * elapsedGameTime * tC.objectWorld.Forward;

                if (Keyboard.GetState().IsKeyDown(userInput.s))
                    tC.position += tC.speed.Z * elapsedGameTime * tC.objectWorld.Backward;

                if (Keyboard.GetState().IsKeyDown(userInput.space))
                    tC.position += tC.speed.Y * elapsedGameTime * tC.objectWorld.Up;

                if (Keyboard.GetState().IsKeyDown(userInput.lShift))
                    tC.position += tC.speed.Y * elapsedGameTime * tC.objectWorld.Down;


                Vector3 axis = new Vector3(0, 0, 0);
                float angle = -elapsedGameTime * 0.001f;
                
                if (Keyboard.GetState().IsKeyDown(userInput.left))
                    axis = new Vector3(0, -1f, 0);
                
                if (Keyboard.GetState().IsKeyDown(userInput.right))
                    axis = new Vector3(0, 1f, 0);
                
                if (Keyboard.GetState().IsKeyDown(userInput.up))
					axis = new Vector3(1f, 0, 0);
                
                if (Keyboard.GetState().IsKeyDown(userInput.down))
                    axis = new Vector3(-1f, 0, 0);
                
				if (Keyboard.GetState().IsKeyDown(userInput.q))
					axis = new Vector3(0, 0, 1f);
                
                if (Keyboard.GetState().IsKeyDown(userInput.e))
                    axis = new Vector3(0, 0, -1f);

				Quaternion rot = Quaternion.CreateFromAxisAngle(axis, angle);
                rot.Normalize();
				tC.rotation *= Matrix.CreateFromQuaternion(rot);

                // Reset to original (zero) rotation
                if (Keyboard.GetState().IsKeyDown(userInput.r))
                    tC.rotation = Matrix.Identity;
            }
        }
    }
}
