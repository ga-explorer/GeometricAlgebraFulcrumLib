using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Decoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Operations;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Elements;

public class CGaTangent<T> :
    CGaElement<T>
{
    public override CGaBlade<T> Position { get; }

    public override Scalar<T> RadiusSquared
    {
        get => Position.GeometricSpace.ScalarZero;
        set => throw new ReadOnlyException();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal CGaTangent(CGaGeometricSpace<T> cgaGeometricSpace, IScalar<T> weight, CGaBlade<T> position, CGaBlade<T> direction)
        : base(
            cgaGeometricSpace,
            CGaElementKind.Tangent,
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
    public override bool IsSameElement(CGaElement<T> element2, bool ignoreWeight = false)
    {
        if (element2 is not CGaTangent<T> tangent2)
            return false;

        if (!ignoreWeight && !Weight.IsNearEqualTo(element2.Weight))
            return false;

        if (!Position.IsNearEqual(tangent2.Position))
            return false;

        if (!Direction.IsNearEqual(tangent2.Direction))
            return false;

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override CGaBlade<T> EncodeOpnsBlade()
    {
        return Weight * GeometricSpace.Eo.Op(Direction).TranslateBy(
            Position
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override CGaBlade<T> EncodeIpnsBlade()
    {
        var direction =
            ((VSpaceDimensions - 2).IsEven() ? Direction : -Direction).VGaDual();

        return Weight * GeometricSpace.Eo.Op(direction).TranslateBy(
            Position
        );
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
            .AppendLine($"   Position: ${Position.DecodeVGaVector3D()}$")
            .AppendLine($"   OPNS Blade: ${EncodeOpnsBlade().ToLaTeX()}$")
            .AppendLine($"   IPNS Blade: ${EncodeIpnsBlade().ToLaTeX()}$")
            .ToString();
    }
}