using System;
using Manager.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Manager.Core;

namespace CG_lab1
{
	public class HeightMap
	{
        /// <summary>
        /// A predefinition of the components needed for the heightmap entity, merely a convenient shortcut
        /// </summary>
        public static Component[] createComponents(string heighmap, string heightMapTexture)
		{
			return new Component[]
			{
				new HeightmapComponent(heighmap, heightMapTexture)
			};

		}
	}
}


