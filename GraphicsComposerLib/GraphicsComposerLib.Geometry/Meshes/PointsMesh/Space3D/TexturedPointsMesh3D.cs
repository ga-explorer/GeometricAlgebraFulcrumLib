using System.Collections;
using System.Collections.Generic;
using DataStructuresLib.Sequences.Periodic1D;
using NumericalGeometryLib.BasicMath.Tuples;
using GraphicsComposerLib.Geometry.Meshes.PointsPath.Space3D;
using GraphicsComposerLib.Geometry.Textures;

namespace GraphicsComposerLib.Geometry.Meshes.PointsMesh.Space3D
{
    public sealed class TexturedPointsMesh3D : IPointsMesh3D
    {
        public IPointsMesh3D BaseMesh { get; }

        public GraphicsTextureCoordinatesGrid TextureCoordinatesGrid { get; }

        public IReadOnlyList<double> TextureURange
            => TextureCoordinatesGrid.TextureURange;

        public IReadOnlyList<double> TextureVRange
            => TextureCoordinatesGrid.TextureVRange;

        public int Count1 
            => BaseMesh.Count1;

        public int Count2 
            => BaseMesh.Count2;

        public int Count 
            => BaseMesh.Count;

        public ITuple3D this[int index] 
            => BaseMesh[index];

        public ITuple3D this[int index1, int index2] 
            => BaseMesh[index1, index2];

        public bool IsBasic 
            => false;

        public bool IsOperator 
            => true;


        internal TexturedPointsMesh3D(IPointsMesh3D baseMesh, double firstTextureU, double lastTextureU, double firstTextureV, double lastTextureV)
        {
            BaseMesh = baseMesh;

            var textureURange = new PSeqLinearDouble1D(
                firstTextureU, 
                lastTextureU, 
                baseMesh.Count1
            );

            var textureVRange = new PSeqLinearDouble1D(
                firstTextureV, 
                lastTextureV, 
                baseMesh.Count
            );
            
            TextureCoordinatesGrid = new GraphicsTextureCoordinatesGrid(
                textureURange,
                textureVRange
            );
        }

        
        public PSeqSlice1D<ITuple3D> GetSliceAt(int dimension, int index)
        {
            return new PointsMeshSlicePointsPath3D(this, dimension, index);
        }

        public PointsMeshSlicePointsPath3D GetSlicePathAt(int dimension, int index)
        {
            return new PointsMeshSlicePointsPath3D(this, dimension, index);
        }

        public IEnumerator<ITuple3D> GetEnumerator()
        {
            return BaseMesh.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return BaseMesh.GetEnumerator();
        }
    }
}