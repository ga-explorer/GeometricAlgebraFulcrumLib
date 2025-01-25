using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PathsMesh;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PathsMesh.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives.Triangles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GeometricAlgebraFulcrumLib.MonoGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private GraphicsDevice _graphicsDevice;
        private SpriteBatch _spriteBatch;
        private Effect _effect;
        private VertexPositionColorNormal[] vertexList;
        private short[] indexList;
        private VertexBuffer _myVertexBuffer;
        private IndexBuffer _myIndexBuffer;
        private Texture2D _copyrightTexture;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1024;
            _graphics.PreferredBackBufferHeight = 768;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();

            Window.Title = "Riemer's MonoGame Tutorials -- 3D Series 1";

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _graphicsDevice = _graphics.GraphicsDevice;

            // TODO: use this.Content to load your game content here
            _effect = Content.Load<Effect>("effects");

            _copyrightTexture = Content.Load<Texture2D>("Copyright");

            var firstPath = new LinearPointsPath3D(
                LinFloat64Vector3D.Create(-3, 0, 0),
                LinFloat64Vector3D.Create(3, 0, 0),
                5
            );

            var lastPath = new LinearPointsPath3D(
                LinFloat64Vector3D.Create(0, -2, 0),
                LinFloat64Vector3D.Create(0, 2, 2),
                5
            );

            var pathMesh = new LerpPathsMesh3D(firstPath, lastPath, 10);
            var composer = new GrTriangleGeometryComposer3D
            {
                NormalComputationMethod = GrVertexNormalComputationMethod.WeightedNormals
            };

            composer
                .BeginBatch()
                .AddTriangles(pathMesh.GetTriangles())
                .EndBatch();

            composer
                .BeginBatch()
                .AddTriangles(pathMesh.GetTriangles(true))
                .EndBatch();

            vertexList = 
                composer
                    .GeometryVertices
                    .Select(v => 
                        v.ToXnaVertexPositionColorNormal()
                    ).ToArray();

            indexList =
                composer.GeometryIndices.Select(
                    i => (short)i
                ).ToArray();

            _myVertexBuffer = new VertexBuffer(
                _graphicsDevice, 
                VertexPositionColorNormal.VertexDeclaration, 
                vertexList.Length, 
                BufferUsage.WriteOnly
            );
            
            _myVertexBuffer.SetData(vertexList);

            _myIndexBuffer = new IndexBuffer(
                _graphicsDevice, 
                typeof(short), 
                indexList.Length, 
                BufferUsage.WriteOnly
            );

            _myIndexBuffer.SetData(indexList);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            foreach (EffectPass pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                _graphicsDevice.Indices = _myIndexBuffer;
                _graphicsDevice.SetVertexBuffer(_myVertexBuffer);
                _graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, indexList.Length / 3);
            }

            _spriteBatch.Begin();
            _spriteBatch.Draw(_copyrightTexture, new Vector2(0, 0), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
