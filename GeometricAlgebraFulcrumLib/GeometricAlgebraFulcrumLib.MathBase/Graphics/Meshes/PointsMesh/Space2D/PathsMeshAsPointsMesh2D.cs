using System.Collections;
using DataStructuresLib.Basic;
using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PathsMesh;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsPath.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsMesh.Space2D
{
    public sealed class PathsMeshAsPointsMesh2D
        : IPointsMesh2D
    {
        public IPathsMesh2D BaseMesh { get; }

        public int Count
            => BaseMesh.MeshPointsCount;

        public IFloat64Tuple2D this[int index]
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

        public IFloat64Tuple2D this[int index1, int index2]
            => BaseMesh[index2][index1];

        public bool IsBasic
            => true;

        public bool IsOperator
            => false;


        public PathsMeshAsPointsMesh2D(IPathsMesh2D baseMesh)
        {
            BaseMesh = baseMesh;
        }


        public PSeqSlice1D<IFloat64Tuple2D> GetSliceAt(int dimension, int index)
        {
            return new PointsMeshSlicePointsPath2D(this, dimension, index);
        }

        public PointsMeshSlicePointsPath2D GetSlicePathAt(int dimension, int index)
        {
            return new PointsMeshSlicePointsPath2D(this, dimension, index);
        }

        public IEnumerator<IFloat64Tuple2D> GetEnumerator()
        {
            return BaseMesh.SelectMany(p => p).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}