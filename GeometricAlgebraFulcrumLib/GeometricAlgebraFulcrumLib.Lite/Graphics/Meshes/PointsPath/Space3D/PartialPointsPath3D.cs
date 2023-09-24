using DataStructuresLib.Permutations;
using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath.Space3D
{
    public class PartialPointsPath3D : 
        PSeqPartial1D<IFloat64Vector3D>, 
        IPointsPath3D
    {
        public IPointsPath3D BasePath { get; }


        public PartialPointsPath3D(IPointsPath3D basePath, IndexMapRange1D baseIndexRange) 
            : base(basePath, baseIndexRange)
        {
            BasePath = basePath;
        }

        public PartialPointsPath3D(IPointsPath3D basePath, int firstIndex) 
            : base(basePath, firstIndex)
        {
            BasePath = basePath;
        }

        public PartialPointsPath3D(IPointsPath3D basePath, int firstIndex, int count) 
            : base(basePath, firstIndex, count)
        {
            BasePath = basePath;
        }

        public PartialPointsPath3D(IPointsPath3D basePath, int firstIndex, int count, bool moveForward) 
            : base(basePath, firstIndex, count, moveForward)
        {
            BasePath = basePath;
        }


        public bool IsValid()
        {
            return this.All(p => p.IsValid());
        }

        public IPointsPath3D MapPoints(Func<IFloat64Vector3D, IFloat64Vector3D> pointMapping)
        {
            return new ArrayPointsPath3D(
                this.Select(pointMapping).ToArray()
            );
        }
    }
}