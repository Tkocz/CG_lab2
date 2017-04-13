using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manager.Components;
using Microsoft.Xna.Framework;

namespace Manager.Subsystems
{
    public class TransformSystem : Core
    {
        //Computes the transformation matrices (world-matrices) for all TransformComponents.
        public override void update(GameTime gameTime)
        {
			foreach (var entity in Engine.GetInst().Entities.Values)
			{
				var transformComp = entity.GetComponent<TransformComponent>();
				if (transformComp == null)
					continue;
				var scale = transformComp.scale;
				var rotation = transformComp.rotation;
				var objectWorld = transformComp.objectWorld;
				var position = transformComp.position;
				transformComp.objectWorld = Matrix.CreateScale(scale) * rotation * Matrix.CreateTranslation(position);
			}
        }
    }
}
