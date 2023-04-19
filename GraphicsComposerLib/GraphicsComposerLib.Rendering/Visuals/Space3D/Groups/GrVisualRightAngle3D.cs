using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Curves;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Surfaces;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Groups
{
    public sealed class GrVisualRightAngle3D :
        GrVisualCurve3D
    {
        public Float64Tuple3D Origin { get; }

        public Float64Tuple3D Direction1 { get; }

        public Float64Tuple3D Direction2 { get; }

        public double Radius { get; }

        public double Width 
            => Radius / 2d.Sqrt();

        public double Height 
            => Radius / 2d.Sqrt();

        public GrVisualSurfaceThinStyle3D? InnerStyle { get; set; }


        public GrVisualRightAngle3D(string name, IFloat64Tuple3D origin, IFloat64Tuple3D direction1, IFloat64Tuple3D direction2, double radius) 
            : base(name)
        {
            Origin = origin.ToTuple3D();
            Direction1 = direction1.ToUnitVector();
            Direction2 = direction2.RejectOnUnitVector(Direction1).ToUnitVector();
            Radius = radius;
        }


        public Triplet<Float64Tuple3D> GetArcPointsTriplet()
        {
            var s = Radius / Math.Sqrt(2d);

            return new Triplet<Float64Tuple3D>(
                Origin + s * Direction1, 
                Origin + s * (Direction1 + Direction2), 
                Origin + s * Direction2
            );
        }

        public Float64Tuple3D GetArcStartUnitVector()
        {
            return Direction1;
        }
    
        public Float64Tuple3D GetArcEndUnitVector()
        {
            return Direction2;
        }
    
        public Float64Tuple3D GetUnitNormal()
        {
            var normal = 
                Direction1.VectorCross(Direction2);

            return normal.GetVectorNormSquared().IsNearZero() 
                ? Direction1.GetUnitNormal() 
                : normal.ToUnitVector();
        }

        public double GetLength()
        {
            return 2d.Sqrt() * Radius;
        } 
    }
}