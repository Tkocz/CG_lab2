using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Manager.Core;
using Manager;
using Manager.Subsystems;
using CG_lab2.Entities;
using CG_lab2.Helpers;
using Manager.Components;
using System.Collections.Generic;

namespace CG_lab2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : GameImpl
    {
        static Matrix world = Matrix.Identity;
        public override void init()
        {
			Engine.GetInst().addEntity(HeightMap.createComponents(
					"US_Canyon",
					"mudcrack",
					3
		   		));

            Engine.GetInst().Window.Title = "I walk funny!";
			Engine.GetInst().Subsystems.Add(new HeightmapChunkSystem());
            Engine.GetInst().Subsystems.Add(new SkyboxSystem(world));
            Engine.GetInst().Subsystems.Add(new CameraSystem());
            Engine.GetInst().Subsystems.Add(new ModelSystem(world));
            Engine.GetInst().Subsystems.Add(new TransformSystem());
            Engine.GetInst().Subsystems.Add(new InputSystem());

           Engine.GetInst().addEntity(Robot.createComponents(
                "robot",
                true,
                new Vector3(1f, 1f, 1f), 
                new Vector3(540f, 300f, -540f),
                Quaternion.Identity, 
                world
                ));
			Engine.GetInst().addEntity(Tropper.createComponents(
                "TreeDesertSmoothbb",
                true,
				new Vector3(0.05f, 0.05f, 0.05f),
                new Vector3(575f, 182f, -530f),
                Quaternion.Identity, 
                world
                ));
			Engine.GetInst().addEntity(Tropper.createComponents(
                "House1Smooth",
                true,
				new Vector3(0.1f, 0.1f, 0.1f), 
                new Vector3(540f, 180f, -540f),
                Quaternion.Identity, 
                world
                ));
            new StaticObjects(100);
        }

        public override void update(GameTime gameTime)
        {

        }
    }
}