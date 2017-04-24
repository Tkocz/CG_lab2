using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manager.Components;
using Microsoft.Xna.Framework;
using Manager.Helpers;

namespace Manager.Subsystems
{
    public class TransformSystem : Core
    {
        //Computes the transformation matrices (world-matrices) for all TransformComponents.
        public override void update(GameTime gameTime)
        {
			foreach (var entity in Engine.GetInst().Entities.Values)
			{
				var tC = entity.GetComponent<TransformComponent>();
                var cC = entity.GetComponent<CameraComponent>();
				if (tC == null || cC == null)
					continue;
				var scale = tC.scale;
				var orientation = tC.orientation;
				var objectWorld = tC.objectWorld;
				var position = tC.position;
				tC.objectWorld = Matrix.CreateScale(scale) * Matrix.CreateFromQuaternion(orientation) * Matrix.CreateTranslation(position);

                if (tC.currentKey == Microsoft.Xna.Framework.Input.Keys.Up)
                {
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
                }
                tC.position.Y = new HeightMapHelper().getHeightMapY(tC.position);
            }
        }
    }
}
