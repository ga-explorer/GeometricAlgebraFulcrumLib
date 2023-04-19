using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Basic;
using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GraphicsComposerLib.Geometry.Meshes.PathsMesh;
using GraphicsComposerLib.Geometry.Meshes.PointsPath.Space3D;

namespace GraphicsComposerLib.Geometry.Meshes.PointsMesh.Space3D
{
    public sealed class PathsMeshAsPointsMesh3D
        : IPointsMesh3D
    {
        public IPathsMesh3D BaseMesh { get; }

        public int Count 
            => BaseMesh.MeshPointsCount;

        public IFloat64Tuple3D this[int index]
        {
            get
            {
                index = index.Mod(Count);
                var index1 = index % Count1;
                var index2 = (index - index1) / Count1;

                return BaseMesh[index2][index1];
            }
        }

        public int Count1 
            => BaseMesh.PathPointsCount;

        public int Count2 
            => BaseMesh.Count;

        public IFloat64Tuple3D this[int index1, int index2] 
            => BaseMesh[index2][index1];

        public bool IsBasic 
            => true;

        public bool IsOperator 
            => false;


        public PathsMeshAsPointsMesh3D(IPathsMesh3D baseMesh)
        {
            BaseMesh = baseMesh;
        }


        public PSeqSlice1D<IFloat64Tuple3D> GetSliceAt(int dimension, int index)
        {
            return new PointsMeshSlicePointsPath3D(this, dimension, index);
        }

        public PointsMeshSlicePointsPath3D GetSlicePathAt(int dimension, int index)
        {
            return new PointsMeshSlicePointsPath3D(this, dimension, index);
        }

        public IEnumerator<IFloat64Tuple3D> GetEnumerator()
        {
            return BaseMesh.SelectMany(p => p).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}