using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Manager;
using Manager.Components;
using Microsoft.Xna.Framework.Graphics;

namespace Manager.Subsystems
{
	public class HeightmapChunkSystem : Core
	{
		GraphicsDevice device;

		public HeightmapChunkSystem()
		{

			device = Engine.GetInst().GraphicsDevice;
			LoadHeightMap();
		}

		private void LoadHeightMap()
		{
			foreach (var entity in Engine.GetInst().Entities.Values)
			{
				var heightmapComponent = entity.GetComponent<HeightmapComponent>();
				if (heightmapComponent == null)
					continue;
				LoadHeightMapData(heightmapComponent.heightMap, heightmapComponent);
				var offsetWidth = heightmapComponent.terrainWidth / (int) Math.Sqrt(heightmapComponent.nHeightMapChunks);
				var offsetHeight = heightmapComponent.terrainHeight / (int) Math.Sqrt(heightmapComponent.nHeightMapChunks);
				int count = 0;
				for (int i = 0; i < heightmapComponent.nHeightMapChunks / (int) Math.Sqrt(heightmapComponent.nHeightMapChunks); i++)
				{
					for (int j = 0; j < heightmapComponent.nHeightMapChunks / (int) Math.Sqrt(heightmapComponent.nHeightMapChunks); j++)
					{
						var widthstart = i * offsetWidth;
						var widthend = (i + 1) * offsetWidth;
						var heightstart = j * offsetHeight;
						var heightend = (j + 1) * offsetHeight;

						heightmapComponent.heightMapChunk[count].vertices = SetUpVertices(widthstart, widthend, heightstart, heightend, heightmapComponent, count);
						heightmapComponent.heightMapChunk[count].indices = SetUpIndices(widthstart, widthend, heightstart, heightend);
						Random rand = new Random();
						heightmapComponent.heightMapChunk[count].boundColor = new Color(rand.Next(255), rand.Next(255), rand.Next(255));
						heightmapComponent.heightMapChunk[count].chunkBoundingBox = CreateBoundingBox(widthend, heightend, widthstart, heightstart, heightmapComponent.heightMapChunk[count]);
						Console.Out.WriteLine("Cunt: " + count);
						Console.Out.WriteLine("WidthStart: " + widthstart);
						Console.Out.WriteLine("WidthEnd: " + widthend);
						Console.Out.WriteLine("HeightStart: " + heightstart);
						Console.Out.WriteLine("WidthEnd: " + heightend);
						count++;
					}

				}
                CalculateNormals(heightmapComponent);
				SetUpBuffers(heightmapComponent);
			}
		}

		private BoundingBox CreateBoundingBox(int withend, int heightend, int widthstart, int heightstart, HeightmapComponent.HeightMapChunk chunk)
		{
			
			Vector3[] boundaryPoints = new Vector3[2];
			boundaryPoints[0] = new Vector3(widthstart, 0, -heightstart);
			boundaryPoints[1] = new Vector3(withend, (int)chunk.heighestPoint - 2, -heightend);
			return BoundingBox.CreateFromPoints(boundaryPoints);
		}


		private void SetUpBuffers(HeightmapComponent component)
		{
			for (int i = 0; i < component.nHeightMapChunks; i++)
			{

				component.heightMapChunk[i].vertexBuffer = new VertexBuffer(device, VertexPositionNormalTexture.VertexDeclaration, component.heightMapChunk[i].vertices.Length, BufferUsage.WriteOnly);
				component.heightMapChunk[i].vertexBuffer.SetData(component.heightMapChunk[i].vertices);
				component.heightMapChunk[i].indexBuffer = new IndexBuffer(device, typeof(int), component.heightMapChunk[i].indices.Length, BufferUsage.WriteOnly);
				component.heightMapChunk[i].indexBuffer.SetData(component.heightMapChunk[i].indices);
			}

		}

		private VertexPositionNormalTexture[] SetUpVertices(int widthstart, int widthend, int heightstart, int heightend, HeightmapComponent component, int chunk)
		{
			var vertices = new VertexPositionNormalTexture[widthend * heightend];
			float heighestPoint = 0;
			for (int x = widthstart; x < widthend; x++)
			{
				for (int y = heightstart; y < heightend; y++)
				{
					var heightData = component.heightMapData[x, y];
					if (heighestPoint < heightData) heighestPoint = heightData;
					vertices[x + y * widthend].Position = new Vector3(x, component.heightMapData[x, y], -y);
					vertices[x + y * widthend].TextureCoordinate = new Vector2(x / 22.5f, y / 22.5f);
				}
			}
			component.heightMapChunk[chunk].heighestPoint = heighestPoint;
			return vertices;
		}

		private int[] SetUpIndices(int widthstart, int widthend, int heightstart, int heightend)
		{
			var indices = new int[(widthend - 1) * (heightend - 1) * 6];
			int counter = 0;
			for (int y = heightstart; y < heightend - 1; y++)
			{
				for (int x = widthstart; x < widthend - 1; x++)
				{
					int lowerLeft = x + y * widthend;
					int lowerRight = (x + 1) + y * widthend;
					int topLeft = x + (y + 1) * widthend;
					int topRight = (x + 1) + (y + 1) * widthend;

					indices[counter++] = topLeft;
					indices[counter++] = lowerRight;
					indices[counter++] = lowerLeft;

					indices[counter++] = topLeft;
					indices[counter++] = topRight;
					indices[counter++] = lowerRight;
				}
			}
			return indices;
		}

		private void CalculateNormals(HeightmapComponent component)
		{
			for (int k = 0; k < component.nHeightMapChunks; k++)
			{

				for (int i = 0; i < component.heightMapChunk[k].vertices.Length; i++)
					component.heightMapChunk[k].vertices[i].Normal = new Vector3(0, 0, 0);

				for (int i = 0; i < component.heightMapChunk[k].indices.Length / 3; i++)
				{

					int index1 = component.heightMapChunk[k].indices[i * 3];
					int index2 = component.heightMapChunk[k].indices[i * 3 + 1];
					int index3 = component.heightMapChunk[k].indices[i * 3 + 2];

					Vector3 side1 = component.heightMapChunk[k].vertices[index1].Position -
											 component.heightMapChunk[k].vertices[index3].Position;
					Vector3 side2 = component.heightMapChunk[k].vertices[index1].Position -
											 component.heightMapChunk[k].vertices[index2].Position;
					Vector3 normal = Vector3.Cross(side1, side2);
					normal.Normalize();
					component.heightMapChunk[k].vertices[index1].Normal += normal;
					component.heightMapChunk[k].vertices[index2].Normal += normal;
					component.heightMapChunk[k].vertices[index3].Normal += normal;
				}
			}
		}
		private void LoadHeightMapData(Texture2D heightMap, HeightmapComponent component)
		{

			Color[] heightMapColors = new Color[component.terrainWidth * component.terrainHeight];
			heightMap.GetData(heightMapColors);

			component.heightMapData = new float[component.terrainWidth, component.terrainHeight];
			for (int x = 0; x < component.terrainWidth; x++)
				for (int y = 0; y < component.terrainHeight; y++)
					component.heightMapData[x, y] = heightMapColors[x + y * component.terrainWidth].R; //5.0f
		}

		//Creates and renders all the HeightmapComponents.
		public override void draw(GameTime gameTime)
		{
			device.Clear(ClearOptions.Target | ClearOptions.Target, Color.Black, 1.0f, 0);
			CameraComponent camera = null;
			foreach (var entity in Engine.GetInst().Entities.Values)
			{
				var cameraModel = entity.GetComponent<CameraComponent>();
				if (cameraModel != null)
					camera = cameraModel;
			}
			foreach (var entity in Engine.GetInst().Entities.Values)
			{
				var heightMapComponent = entity.GetComponent<HeightmapComponent>();
				if (heightMapComponent == null)
					continue;
				RasterizerState rs = new RasterizerState();
				device.RasterizerState = rs;
				var effect = heightMapComponent.basicEffect;
				effect.World = Matrix.Identity;
				effect.View = camera.view;
				effect.Projection = camera.projection;

				BoundingFrustum cameraFrustrum = new BoundingFrustum(camera.view * camera.projection);

				effect.EnableDefaultLighting();
				effect.LightingEnabled = true;

				effect.DirectionalLight0.DiffuseColor = new Vector3(0.5f, 0.5f, 0.5f);
				effect.DirectionalLight0.Direction = new Vector3(-0.5f, 1f, -3.5f);
				effect.DirectionalLight0.SpecularColor = new Vector3(-0.1f, -0.1f, -0.1f);
				effect.DirectionalLight0.Enabled = true;
				effect.FogEnabled = true;
				effect.FogStart = 200;
				effect.FogEnd = 300;
				effect.FogColor = Color.DimGray.ToVector3();
				effect.AmbientLightColor = new Vector3(0.1f, 0.1f, 0.1f);
				effect.PreferPerPixelLighting = true;
				effect.SpecularPower = 100;
				effect.EmissiveColor = new Vector3(0.1f, 0.1f, 0.1f);
				effect.TextureEnabled = true;
				effect.Texture = heightMapComponent.heightMapTexture;
				var count = 0;
				Engine.GetInst().Window.Title = "";
				foreach (HeightmapComponent.HeightMapChunk chunk in heightMapComponent.heightMapChunk)
				{

					device.SetVertexBuffer(chunk.vertexBuffer);
					device.Indices = chunk.indexBuffer;
					if (!chunk.chunkBoundingBox.Intersects(cameraFrustrum))
						continue;
					
						
					foreach (EffectPass pass in effect.CurrentTechnique.Passes)
					{
						pass.Apply();
						device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, chunk.indices.Length / 3);
					}
					count++;
					Engine.GetInst().Window.Title = "Rendered Nodes: " +  count;

                	DrawBoundingBox(chunk.chunkBoundingBox, chunk.boundColor, device, new BasicEffect(device), Matrix.Identity, camera.view, camera.projection);
				}
			}
		}

		public static void DrawBoundingBox(BoundingBox bBox, Color color, GraphicsDevice device, BasicEffect basicEffect, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix)
		{
			Vector3 v1 = bBox.Min;
			Vector3 v2 = bBox.Max;

			VertexPositionColor[] cubeLineVertices = new VertexPositionColor[8];
			cubeLineVertices[0] = new VertexPositionColor(v1, color);
			cubeLineVertices[1] = new VertexPositionColor(new Vector3(v2.X, v1.Y, v1.Z), color);
			cubeLineVertices[2] = new VertexPositionColor(new Vector3(v2.X, v1.Y, v2.Z), color);
			cubeLineVertices[3] = new VertexPositionColor(new Vector3(v1.X, v1.Y, v2.Z), color);

			cubeLineVertices[4] = new VertexPositionColor(new Vector3(v1.X, v2.Y, v1.Z), color);
			cubeLineVertices[5] = new VertexPositionColor(new Vector3(v2.X, v2.Y, v1.Z), color);
			cubeLineVertices[6] = new VertexPositionColor(v2, color);
			cubeLineVertices[7] = new VertexPositionColor(new Vector3(v1.X, v2.Y, v2.Z), color);

			short[] cubeLineIndices = { 0, 1, 1, 2, 2, 3, 3, 0, 4, 5, 5, 6, 6, 7, 7, 4, 0, 4, 1, 5, 2, 6, 3, 7 };
			RasterizerState rs = new RasterizerState();
			basicEffect.World = worldMatrix;
			basicEffect.View = viewMatrix;
			basicEffect.Projection = projectionMatrix;
			basicEffect.VertexColorEnabled = true;
			rs.FillMode = FillMode.Solid;
			foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
			{
				pass.Apply();
				device.DrawUserIndexedPrimitives(PrimitiveType.LineList, cubeLineVertices, 0, 8, cubeLineIndices, 0, 12);

			}
		}
	}
}
