﻿using Manager;
using Manager.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Manager.Core;

namespace CG_lab2.Entities
{
    /// <summary>
    /// A predefinition of the components needed for the main entity, merely a convenient shortcut
    /// </summary>
    public class WorldObject
    {
        public static Component[] createComponents(String name, Vector3 scale, Vector3 position, Quaternion orientation, Matrix objectWorld)
		{
			ModelComponent model = new ModelComponent(name, false);
			TransformComponent trans = new TransformComponent(scale, position, orientation, objectWorld);
            return new Component[]
            {
                model,
                trans,
				new CollisionComponent(model, trans)
            };
        }
    }
}