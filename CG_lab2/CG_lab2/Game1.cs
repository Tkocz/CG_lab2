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

            chopper = Engine.GetInst().addEntity(Chopper.createComponents(
                "Chopper",
                true,
                new Vector3(0.5f, 0.5f, 0.5f), 
                new Vector3(0f, 300f, 0f),
                world, 
                world,
                new Vector3(0.1f, 0.1f, 0.1f)
                ));
        }

        public override void update(GameTime gameTime)
        {

        }
    }
}