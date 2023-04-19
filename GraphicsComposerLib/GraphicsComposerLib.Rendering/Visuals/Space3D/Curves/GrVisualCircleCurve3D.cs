using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Constants;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Curves
{
    public sealed class GrVisualCircleCurve3D :
        GrVisualCurve3D
    {
        public IFloat64Tuple3D Center { get; set; }

        public IFloat64Tuple3D Normal { get; set; }

        public double Radius { get; set; }


        public GrVisualCircleCurve3D(string name) 
            : base(name)
        {
        }


        public double GetLength()
        {
            return 2d * Math.PI * Radius;
        }

        public Triplet<Float64Tuple3D> GetPointsTriplet()
        {
            var quaternion = Axis3D.PositiveZ.CreateAxisToVectorRotationQuaternion(
                Normal.ToUnitVector()
            );

            const double angle = 2d * Math.PI / 3d;

            var a = Radius * Math.Cos(angle);
            var b = Radius * Math.Sin(angle);

            var point1 = Center + quaternion.QuaternionRotate(Radius, 0, 0);
            var point2 = Center + quaternion.QuaternionRotate(a, b, 0);
            var point3 = Center + quaternion.QuaternionRotate(a, -b, 0);

            return new Triplet<Float64Tuple3D>(point1, point2, point3);
        }
    }
}