using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Manager.Core;
using Manager;
using Manager.Subsystems;
using CG_lab1.Entities;
using Manager.Components;
using System.Collections.Generic;

namespace CG_lab1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : GameImpl
    {
        static Matrix world = Matrix.Identity;
        Entity chopper;
        public override void init()
        {
			Engine.GetInst().addEntity(HeightMap.createComponents(
					"US_Canyon",
					"mudcrack"
		   		));

            Engine.GetInst().Window.Title = "Get to the Choppaaaaargh!";
			Engine.GetInst().Subsystems.Add(new HeightmapSystem());
            Engine.GetInst().Subsystems.Add(new SkyboxSystem(world));
            Engine.GetInst().Subsystems.Add(new CameraSystem());
            Engine.GetInst().Subsystems.Add(new ModelSystem(world));
            Engine.GetInst().Subsystems.Add(new TransformSystem());
            Engine.GetInst().Subsystems.Add(new InputSystem());

           Engine.GetInst().addEntity(Chopper.createComponents(
                "chopper",
                true,
                new Vector3(1f, 1f, 1f), 
                new Vector3(0f, 300f, 0f),
                world, 
                world,
                new Vector3(0.1f, 0.1f, 0.1f)
                ));

			Engine.GetInst().addEntity(Tropper.createComponents(
                "BigBoySmooth",
                true,

				new Vector3(0.05f, 0.05f, 0.05f), 
                new Vector3(0f, 172f, 0f),
                world, 
                world,
                new Vector3(0.0f, 0.0f, 0.0f)
                ));
			Engine.GetInst().addEntity(Tropper.createComponents(
                "SmallBoy",
                true,

				new Vector3(0.02f, 0.02f, 0.02f),
                new Vector3(35f, 182f, -10f),
                world, 
                world,
                new Vector3(0.0f, 0.0f, 0.0f)
                ));
			Engine.GetInst().addEntity(Tropper.createComponents(
                "House1Smooth",
                true,

				new Vector3(0.1f, 0.1f, 0.1f), 
                new Vector3(30f, 180f, 20f),
                world, 
                world,
                new Vector3(0.0f, 0.0f, 0.0f)
                ));



        }

        public override void update(GameTime gameTime)
        {

        }
    }
}