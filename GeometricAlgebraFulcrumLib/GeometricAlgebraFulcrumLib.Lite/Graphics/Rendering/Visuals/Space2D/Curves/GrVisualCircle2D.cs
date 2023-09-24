using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath.Space2D;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space2D.Styles;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space2D.Curves
{
    public sealed class GrVisualCircle2D :
        GrVisualCurve2D
    {
        public IFloat64Vector2D Center { get; }

        public double Radius { get; }
        
        public override int PathPointCount 
            => 360;

        public override double Length 
            => 2d * Math.PI * Radius;


        public GrVisualCircle2D(string name, IGrVisualCurveStyle2D style, IFloat64Vector2D center, double radius)
            : base(name, style)
        {
            Center = center;
            Radius = radius;
        }


        public override bool IsValid()
        {
            return Center.IsValid() &&
                   Radius.IsValid() &&
                   Radius > 0;
        }
        
        public Triplet<Float64Vector2D> GetPointsTriplet()
        {
            const double angle = 2d * Math.PI / 3d;

            var a = Radius * Math.Cos(angle);
            var b = Radius * Math.Sin(angle);

            var point1 = Center + Float64Vector2D.Create(Radius, 0);
            var point2 = Center + Float64Vector2D.Create(a, b);
            var point3 = Center + Float64Vector2D.Create(a, -b);

            return new Triplet<Float64Vector2D>(point1, point2, point3);
        }
        
        public override IPointsPath2D GetPositionsPath()
        {
            var angles = 
                0d.GetLinearRange(2d * Math.PI, PathPointCount, false);

            var points =
                angles.Select(angle => 
                    Center + Radius * angle.Cos() * Float64Vector2D.E1 +
                    Center + Radius * angle.Sin() * Float64Vector2D.E2
                );

            return new ArrayPointsPath2D(points);
        }

    }
}