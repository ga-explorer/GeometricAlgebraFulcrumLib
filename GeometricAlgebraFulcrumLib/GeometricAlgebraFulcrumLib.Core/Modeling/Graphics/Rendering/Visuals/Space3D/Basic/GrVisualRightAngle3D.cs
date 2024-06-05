using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsPath;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsPath.Space3D;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Visuals.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Visuals.Space3D.Basic;

public sealed class GrVisualRightAngle3D :
    GrVisualCurveWithAnimation3D
{
    public static GrVisualRightAngle3D CreateStatic(string name, GrVisualCurveStyle3D style, ILinFloat64Vector3D origin, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, double radius)
    {
        return new GrVisualRightAngle3D(
            name,
            style,
            origin,
            direction1,
            direction2,
            radius,
            GrVisualAnimationSpecs.Static
        );
    }

    public static GrVisualRightAngle3D CreateStatic(string name, GrVisualCurveStyle3D style, GrVisualSurfaceThinStyle3D innerStyle, ILinFloat64Vector3D origin, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, double radius)
    {
        return new GrVisualRightAngle3D(
            name,
            style,
            origin,
            direction1,
            direction2,
            radius,
            GrVisualAnimationSpecs.Static
        )
        {
            InnerStyle = innerStyle
        };
    }

    public static GrVisualRightAngle3D Create(string name, GrVisualCurveStyle3D style, ILinFloat64Vector3D origin, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, double radius, GrVisualAnimationSpecs animationSpecs)
    {
        return new GrVisualRightAngle3D(
            name,
            style,
            origin,
            direction1,
            direction2,
            radius,
            animationSpecs
        );
    }

    public static GrVisualRightAngle3D Create(string name, GrVisualCurveStyle3D style, GrVisualSurfaceThinStyle3D innerStyle, ILinFloat64Vector3D origin, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, double radius, GrVisualAnimationSpecs animationSpecs)
    {
        return new GrVisualRightAngle3D(
            name,
            style,
            origin,
            direction1,
            direction2,
            radius,
            animationSpecs
        )
        {
            InnerStyle = innerStyle
        };
    }


    public LinFloat64Vector3D Origin { get; }

    public LinFloat64Vector3D Direction1 { get; }

    public LinFloat64Vector3D Direction2 { get; }

    public double Radius { get; }

    public double Width
        => Radius / 2d.Sqrt();

    public double Height
        => Radius / 2d.Sqrt();

    public override int PathPointCount { get; }

    public override double Length
        => Radius * 2d.Sqrt();

    public LinFloat64Vector3D Normal
        => Direction1.VectorUnitCross(Direction2);

    public GrVisualSurfaceThinStyle3D? InnerStyle { get; set; }


    private GrVisualRightAngle3D(string name, GrVisualCurveStyle3D style, ILinFloat64Vector3D origin, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, double radius, GrVisualAnimationSpecs animationSpecs)
        : base(name, style, animationSpecs)
    {
        Origin = origin.ToLinVector3D();
        Direction1 = direction1.ToUnitLinVector3D();
        Direction2 = direction2.RejectOnUnitVector(Direction1).ToUnitLinVector3D();
        Radius = radius;

        Debug.Assert(IsValid());
    }


    public override bool IsValid()
    {
        throw new NotImplementedException();
    }

    public Triplet<LinFloat64Vector3D> GetArcPointsTriplet()
    {
        var s = Radius / Math.Sqrt(2d);

        return new Triplet<LinFloat64Vector3D>(
            Origin + s * Direction1,
            Origin + s * (Direction1 + Direction2),
            Origin + s * Direction2
        );
    }

    public override IPointsPath3D GetPositionsPath()
    {
        return new ArrayPointsPath3D(
            GetArcPointsTriplet().GetItemArray().Cast<ILinFloat64Vector3D>()
        );
    }

    public override IPointsPath3D GetPositionsPath(double time)
    {
        throw new NotImplementedException();
    }

    public override IReadOnlyList<GrVisualAnimatedGeometry> GetAnimatedGeometries()
    {
        throw new NotImplementedException();
    }

    public GrVisualRightAngle3D SetAnimatedVisibility(GrVisualAnimatedScalar visibility)
    {
        AnimatedVisibility = visibility;

        return this;
    }


}