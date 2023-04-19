using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GraphicsComposerLib.Geometry.Meshes.PathsMesh.Space3D;

namespace GraphicsComposerLib.Geometry.Composers
{
    public sealed class GridsBoxComposer
    {
        public XyGridComposer UpperGridComposer { get; }

        public XyGridComposer LowerGridComposer { get; }

        public YzGridComposer FrontGridComposer { get; }

        public YzGridComposer BackGridComposer { get; }

        public ZxGridComposer LeftGridComposer { get; }

        public ZxGridComposer RighGridComposer { get; }


        public GridsBoxComposer(Float64Tuple3D center)
        {
            //TODO: Adjust the centers to make a box
            UpperGridComposer = new XyGridComposer();
            LowerGridComposer = new XyGridComposer();
            FrontGridComposer = new YzGridComposer();
            BackGridComposer = new YzGridComposer();
            LeftGridComposer = new ZxGridComposer();
            RighGridComposer = new ZxGridComposer();
        }


        /// <summary>
        /// Create a path meshes from the specs of this grid box composer
        /// </summary>
        /// <returns></returns>
        public ListPathsMesh3D[] ComposeMeshes()
        {
            return new[]
            {
                UpperGridComposer.ComposeMesh(),
                LowerGridComposer.ComposeMesh(),
                FrontGridComposer.ComposeMesh(),
                BackGridComposer.ComposeMesh(),
                LeftGridComposer.ComposeMesh(),
                RighGridComposer.ComposeMesh()
            };
        }

        /// <summary>
        /// Compose path mesh patches from the specs of this grid box composer
        /// </summary>
        /// <returns></returns>
        public TexturedPathsMesh3D[] ComposePatches(bool upperPatches = true)
        {
            return new[]
            {
                UpperGridComposer.ComposeTexturedMesh(),
                LowerGridComposer.ComposeTexturedMesh(),
                FrontGridComposer.ComposeTexturedMesh(),
                BackGridComposer.ComposeTexturedMesh(),
                LeftGridComposer.ComposeTexturedMesh(),
                RighGridComposer.ComposeTexturedMesh()
            };
        }
    }
}
