using Manager;
using Manager.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Manager.Core;

namespace CG_lab1.Entities
{
    /// <summary>
    /// A predefinition of the components needed for the main entity, merely a convenient shortcut
    /// </summary>
    public class Chopper
    {
        public static Component[] createComponents(String name,bool hasTransformables, Vector3 scale, Vector3 position, Matrix rotation, Matrix objectWorld, Vector3 speed)
        {
            return new Component[]
            {
                new CameraComponent(),
                new ModelComponent(name, hasTransformables),
                new TransformComponent(scale, position, rotation, objectWorld, speed),
                new InputComponent()
            };
        }
    }
}