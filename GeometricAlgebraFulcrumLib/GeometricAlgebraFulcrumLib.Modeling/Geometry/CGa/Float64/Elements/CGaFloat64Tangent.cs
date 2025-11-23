using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;

public class CGaFloat64Tangent :
    CGaFloat64Element
{
    public override CGaFloat64Blade Position { get; }

    public override double RadiusSquared
    {
        get => 0d;
        set => throw new ReadOnlyException();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal CGaFloat64Tangent(CGaFloat64GeometricSpace cgaGeometricSpace, double weight, CGaFloat64Blade position, CGaFloat64Blade direction)
        : base(
            cgaGeometricSpace,
            CGaFloat64ElementKind.Tangent,
            weight,
            direction
        )
    {
        Position = position;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public sealed override bool IsValid()
    {
        return Weight.IsValid() &&
               Weight >= 0 &&
               Direction.IsVGaBlade() &&
               Position.IsVGaVector() &&
               Direction.Norm().IsNearOne();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsSameElement(CGaFloat64Element element2, bool ignoreWeight = false)
    {
        if (element2 is not CGaFloat64Tangent tangent2)
            return false;

        if (!ignoreWeight && !Weight.IsNearEqual(element2.Weight))
            return false;

        if (!Position.IsNearEqual(tangent2.Position))
            return false;

        if (!Direction.IsNearEqual(tangent2.Direction))
            return false;

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override CGaFloat64Blade EncodeOpnsBlade()
    {
        return Weight * GeometricSpace.Eo.Op(Direction).TranslateBy(
            Position
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override CGaFloat64Blade EncodeIpnsBlade()
    {
        var direction =
            ((VSpaceDimensions - 2).IsEven() ? Direction : -Direction).VGaDual();

        return Weight * GeometricSpace.Eo.Op(direction).TranslateBy(
            Position
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public new CGaFloat64Tangent TranslateBy(LinFloat64Vector2D egaVector)
    {
        return new CGaFloat64Tangent(
            GeometricSpace,
            Weight,
            Position + egaVector.EncodeVGaVector(GeometricSpace),
            Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public new CGaFloat64Tangent TranslateBy(LinFloat64Vector3D egaVector)
    {
        return new CGaFloat64Tangent(
            GeometricSpace,
            Weight,
            Position + egaVector.EncodeVGaVector(GeometricSpace),
            Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public new CGaFloat64Tangent TranslateBy(LinFloat64Vector egaVector)
    {
        return new CGaFloat64Tangent(
            GeometricSpace,
            Weight,
            Position + egaVector.EncodeVGaVector(GeometricSpace),
            Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public new CGaFloat64Tangent TranslateBy(XGaFloat64Vector egaVector)
    {
        return new CGaFloat64Tangent(
            GeometricSpace,
            Weight,
            Position + egaVector.EncodeVGaVector(GeometricSpace),
            Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public new CGaFloat64Tangent TranslateBy(CGaFloat64Blade egaVector)
    {
        Debug.Assert(egaVector.IsVGaVector());

        return new CGaFloat64Tangent(
            GeometricSpace,
            Weight,
            Position + egaVector,
            Direction
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public new CGaFloat64Tangent ScaleBy(double scalingFactor)
    {
        return EncodeOpnsBlade().ScaleBy(scalingFactor).DecodeOpnsTangent.Element();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public new CGaFloat64Tangent RotateUsing(LinFloat64Angle angle, LinFloat64Vector2D egaAxisPoint)
    {
        return EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint)
            .DecodeOpnsTangent.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public new CGaFloat64Tangent RotateUsing(LinFloat64Angle angle, LinFloat64Vector3D egaAxisPoint, LinFloat64Vector3D egaAxisVector)
    {
        return EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint, egaAxisVector)
            .DecodeOpnsTangent.Element();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        if (Weight.IsNearZero())
            return "Zero Conformal Tangent";

        return new StringBuilder()
            .AppendLine("Conformal Tangent:")
            .AppendLine($"   Weight: ${BasisSpecs.ToLaTeX(Weight)}$")
            .AppendLine($"   Unit Direction Grade: ${Direction.Grade}$")
            .AppendLine($"   Unit Direction: ${Direction.ToLaTeX()}$")
            .AppendLine($"   Unit Direction Normal: ${NormalDirection.ToLaTeX()}$")
            .AppendLine($"   Position: ${Position.DecodeVGaDirection.Vector3D()}$")
            .AppendLine($"   OPNS Blade: ${EncodeOpnsBlade().ToLaTeX()}$")
            .AppendLine($"   IPNS Blade: ${EncodeIpnsBlade().ToLaTeX()}$")
            .ToString();
    }
}