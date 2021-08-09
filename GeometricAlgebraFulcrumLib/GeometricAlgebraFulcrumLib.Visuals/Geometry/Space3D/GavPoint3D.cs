using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using EuclideanGeometryLib.BasicMath.Tuples;

namespace GeometricAlgebraFulcrumLib.Visuals.Geometry.Space3D
{
    public sealed class GavPoint3D : 
        ITuple3D, IGavGeometry3D
    {
        public GavGeometryContext3D GeometryContext { get; }

        public string Name { get; private set; }

        public double X { get; set; }
        
        public double Y { get; set; }
        
        public double Z { get; set; }

        public double Item1 => X;
        
        public double Item2 => Y;
        
        public double Item3 => Z;
        
        public bool IsValid 
            => !IsInvalid;
        
        public bool IsInvalid
            => double.IsNaN(X) ||
               double.IsNaN(Y) ||
               double.IsNaN(Z);

        public GavPointStyle3D Style { get; }


        internal GavPoint3D([NotNull] GavGeometryContext3D geometryContext, [NotNull] string name, double x, double y, double z, [NotNull] GavPointStyle3D style)
        {
            GeometryContext = geometryContext;
            Name = name;
            X = x;
            Y = y;
            Z = z;
            Style = style;

            Debug.Assert(IsValid);
        }


        public override string ToString()
        {
            return new StringBuilder()
                .Append($"Point({X:G}, {Y:G}, {Z:G})")
                .ToString();
        }
    }
}
