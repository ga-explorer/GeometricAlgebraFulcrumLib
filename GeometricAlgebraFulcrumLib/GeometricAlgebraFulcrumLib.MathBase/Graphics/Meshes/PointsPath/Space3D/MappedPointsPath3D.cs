using DataStructuresLib.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsPath.Space3D
{
    public sealed class MappedPointsPath3D : 
        PSeqMapped1D<IFloat64Vector3D>, 
        IPointsPath3D
    {
        public IPointsPath3D BasePath { get; }

        public Func<IFloat64Vector3D, IFloat64Vector3D> Mapping { get; set; }


        public MappedPointsPath3D(IPointsPath3D basePath)
            : base(basePath)
        {
            BasePath = basePath;
            Mapping = (point => point);
        }

        public MappedPointsPath3D(IPointsPath3D basePath, Func<IFloat64Vector3D, IFloat64Vector3D> mapping)
            : base(basePath)
        {
            BasePath = basePath;
            Mapping = mapping;
        }


        protected override IFloat64Vector3D MappingFunction(IFloat64Vector3D input)
        {
            return Mapping(input);
        }


        public bool IsValid()
        {
            return this.All(p => p.IsValid());
        }
        
        public IPointsPath3D MapPoints(Func<IFloat64Vector3D, IFloat64Vector3D> pointMapping)
        {
            return new MappedPointsPath3D(
                BasePath,
                p => pointMapping(Mapping(p))
            );
        }
    }
}