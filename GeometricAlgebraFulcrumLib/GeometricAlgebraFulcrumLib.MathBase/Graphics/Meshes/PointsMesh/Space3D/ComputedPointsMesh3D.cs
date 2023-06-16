using DataStructuresLib.Sequences.Periodic2D;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsPath.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsMesh.Space3D
{
    public sealed class ComputedPointsMesh3D : 
        PSeqComputed2D<IFloat64Tuple3D>, 
        IPointsMesh3D
    {
        public ComputedPointsMesh3D(int count1, int count2, Func<int, int, IFloat64Tuple3D> mappingFunc) 
            : base(count1, count2, mappingFunc)
        {
        }

        
        public PointsMeshSlicePointsPath3D GetSlicePathAt(int dimension, int index)
        {
            return new PointsMeshSlicePointsPath3D(this, dimension, index);
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}