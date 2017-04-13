using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Manager.Core;

namespace Manager.Components
{
    /// <summary>
    /// Component-values, should be made with get-set instead, but, time...
    /// </summary>
    public class HeightmapComponent : Component
    {
        //Holds all the data related to the height map(e.g.height data, vertex/index buffers).
        public Texture2D heightMap;
		public Texture2D heightMapTexture;
		public VertexPositionNormalTexture[] vertices;
		public VertexBuffer vertexBuffer;
		public IndexBuffer indexBuffer;

		public int terrainWidth;
		public int terrainHeight;

		public BasicEffect basicEffect;

		public int[] indices;
        
		public float[,] heightMapData;
		public struct VertexPositionColorNormal
		{
			public Vector3 Position;
			public Color Color;
			public Vector3 Normal;

            public readonly static VertexDeclaration VertexDeclaration = new VertexDeclaration
            (
                new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
                new VertexElement(sizeof(float) * 3, VertexElementFormat.Color, VertexElementUsage.Color, 0),
                new VertexElement(sizeof(float) * 3 + 4, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0)
            );
		}
		public HeightmapComponent(string heighMap, string heightMapTexture)
		{
			this.heightMap = Engine.GetInst().Content.Load<Texture2D>(heighMap);
			this.heightMapTexture = Engine.GetInst().Content.Load<Texture2D>(heightMapTexture);
			this.terrainWidth = heightMap.Width;
			this.terrainHeight = heightMap.Height;
			this.basicEffect = new BasicEffect(Engine.GetInst().GraphicsDevice);
		}
	}
}