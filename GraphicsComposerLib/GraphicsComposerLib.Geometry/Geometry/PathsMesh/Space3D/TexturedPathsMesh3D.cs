using System.Collections;
using System.Collections.Generic;
using DataStructuresLib.Sequences.Periodic1D;
using EuclideanGeometryLib.GraphicsGeometry.Textures;
using GraphicsComposerLib.Geometry.Geometry.PointsPath;

namespace GraphicsComposerLib.Geometry.Geometry.PathsMesh.Space3D
{
    public sealed class TexturedPathsMesh3D : IPathsMesh3D
    {
        public IPathsMesh3D BaseMesh { get; }

        public GraphicsTextureCoordinatesGrid TextureCoordinatesGrid { get; }

        public IReadOnlyList<double> TextureURange
            => TextureCoordinatesGrid.TextureURange;

        public IReadOnlyList<double> TextureVRange
            => TextureCoordinatesGrid.TextureVRange;

        public int Count 
            => BaseMesh.Count;

        public IPointsPath3D this[int index] 
            => BaseMesh[index];

        public bool IsBasic 
            => false;

        public bool IsOperator 
            => true;

        public int PathPointsCount 
            => BaseMesh.PathPointsCount;

        public int MeshPointsCount 
            => BaseMesh.MeshPointsCount;


        internal TexturedPathsMesh3D(IPathsMesh3D baseMesh, double firstTextureU, double lastTextureU, double firstTextureV, double lastTextureV)
        {
            BaseMesh = baseMesh;

            var textureURange = new PSeqLinearDouble1D(
                firstTextureU, 
                lastTextureU, 
                baseMesh.Count
            );

            var textureVRange = new PSeqLinearDouble1D(
                firstTextureV, 
                lastTextureV, 
                baseMesh.PathPointsCount
            );
            
            TextureCoordinatesGrid = new GraphicsTextureCoordinatesGrid(
                textureURange,
                textureVRange
            );
        }


        public IEnumerator<IPointsPath3D> GetEnumerator()
        {
            return BaseMesh.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return BaseMesh.GetEnumerator();
        }
    }
}
