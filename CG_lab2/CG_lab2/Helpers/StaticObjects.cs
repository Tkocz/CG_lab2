using CG_lab2.Entities;
using Manager;
using Manager.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG_lab2.Helpers
{
    public class StaticObjects
    {
        private List<String> names = new List<string>() { "tree1", "Rock", "BigBoy" };
        private String name;
        private Vector3 scale, position, speed;
        private Quaternion orientation;
        private Matrix objectWorld;
        public StaticObjects(int nObjects)
        {
            Random rnd = new Random();
            for(int i=0; i<nObjects; i++)
            {

                name = names[rnd.Next(3)];
                scale = new Vector3(0.1f, 0.1f, 0.1f);
                position= new Vector3((float)(rnd.NextDouble() * 1081), 180f, (float)(rnd.NextDouble() * 1081));
                if (position.X > (1081 / 2))
                    position.X = -position.X / 2;
                if (position.Z > (1081 / 2))
                    position.Z = -position.Z / 2;
                position.Y = getHeightMapY(position);
                orientation = Quaternion.Identity;
                objectWorld = Matrix.Identity;
                speed = Vector3.Zero;

                Engine.GetInst().addEntity(WorldObject.createComponents(
                    name,
                    scale,
                    position,
                    orientation,
                    objectWorld
                    ));
            }
        }
        public float getHeightMapY(Vector3 position)
        {
            foreach (var entity in Engine.GetInst().Entities.Values)
            {
                var hMComp = entity.GetComponent<HeightmapComponent>();
                if (hMComp != null)
                {
                    int xvalue = (int)position.X + (1081 / 2);
                    int zvalue = (int)position.Z + (1081 / 2);
                    var yValue = hMComp.heightMapData[xvalue, zvalue];
                    return yValue;
                }
            }
            return 0f;
        }
    }
}
