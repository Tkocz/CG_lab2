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
		public int chunkWidth;
		public int chunkHeight;
		public int nHeightMapChunks;

		public BasicEffect basicEffect;

		public int[] indices;
        
		public float[,] heightMapData;

		public class HeigthMapChunk
		{
			public static int nRenderedChunks;
			public BoundingBox ChunkBoundingBox;
			public HeigthMapChunk Up;
			public HeigthMapChunk Left;
			public HeigthMapChunk Down;
			public HeigthMapChunk Right;
		}

		public HeigthMapChunk root;

		public HeightmapComponent(string heighMap, string heightMapTexture, int nHeightMapChunks, int prefHeightMapWidth = -1, int prefHeightMapHeight = -1)
		{
			this.nHeightMapChunks = nHeightMapChunks;
			this.heightMap = Engine.GetInst().Content.Load<Texture2D>(heighMap);
			this.heightMapTexture = Engine.GetInst().Content.Load<Texture2D>(heightMapTexture);
			if (prefHeightMapWidth == -1)
				this.terrainWidth = heightMap.Width;
			else
				this.terrainWidth = prefHeightMapWidth;
			if (prefHeightMapHeight == -1)
                this.terrainHeight = heightMap.Height;
			else
                this.terrainHeight = prefHeightMapHeight;
			this.basicEffect = new BasicEffect(Engine.GetInst().GraphicsDevice);
		}
	}
}