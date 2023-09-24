using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath.Space3D
{
    public sealed class Mapped2DPointsPath3D : 
        PSeqMapped1D<IFloat64Vector2D, IFloat64Vector3D>, 
        IPointsPath3D
    {
        public Func<IFloat64Vector2D, IFloat64Vector3D> Mapping { get; set; }


        public Mapped2DPointsPath3D(IPointsPath2D basePath, Func<IFloat64Vector2D, IFloat64Vector3D> mapping)
            : base(basePath)
        {
            Mapping = mapping;
        }


        protected override IFloat64Vector3D MappingFunction(IFloat64Vector2D input)
        {
            return Mapping(input);
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