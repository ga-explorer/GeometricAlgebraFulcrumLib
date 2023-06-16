using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Visuals.Geometry.Space3D
{
    public sealed class GeovPoint3D : 
        IFloat64Tuple3D, 
        IGeovGeometry3D
    {
        public int VSpaceDimensions 
            => 3;

        public GeovGeometryContext3D GeometryContext { get; }

        public string Name { get; private set; }

        public Float64Scalar X { get; set; }
        
        public Float64Scalar Y { get; set; }
        
        public Float64Scalar Z { get; set; }

        public double Item1 => X;
        
        public double Item2 => Y;
        
        public double Item3 => Z;
        
        public bool IsValid()
            => !double.IsNaN(X) &&
               !double.IsNaN(Y) &&
               !double.IsNaN(Z);

        public GeovPointStyle3D Style { get; }


        internal GeovPoint3D([NotNull] GeovGeometryContext3D geometryContext, [NotNull] string name, double x, double y, double z, [NotNull] GeovPointStyle3D style)
        {
            GeometryContext = geometryContext;
            Name = name;
            X = x;
            Y = y;
            Z = z;
            Style = style;

            Debug.Assert(IsValid());
        }


        public override string ToString()
        {
            return new StringBuilder()
                .Append($"Point({X:G}, {Y:G}, {Z:G})")
                .ToString();
        }

        
    }
}
