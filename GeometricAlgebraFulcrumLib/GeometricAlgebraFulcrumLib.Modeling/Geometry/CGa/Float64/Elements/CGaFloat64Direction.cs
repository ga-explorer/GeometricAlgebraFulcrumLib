using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;

public class CGaFloat64Direction :
    CGaFloat64Element
{
    public override CGaFloat64Blade Position
        => GeometricSpace.ZeroVectorBlade;

    public override double RadiusSquared
    {
        get => 0d;
        set => throw new ReadOnlyException();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal CGaFloat64Direction(CGaFloat64GeometricSpace cgaGeometricSpace, double weight, CGaFloat64Blade direction)
        : base(
            cgaGeometricSpace,
            CGaFloat64ElementKind.Direction,
            weight,
            direction
        )
    {
        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public sealed override bool IsValid()
    {
        return Weight.IsValid() &&
               Weight >= 0 &&
               Direction.IsVGaBlade() &&
               Direction.Norm().IsNearOne() &&
               Position.IsZero();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsSameElement(CGaFloat64Element element2, bool ignoreWeight = false)
    {
        if (element2 is not CGaFloat64Direction direction2)
            return false;

        if (!ignoreWeight && !Weight.IsNearEqual(element2.Weight))
            return false;

        if (!Direction.IsNearEqual(direction2.Direction))
            return false;

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override CGaFloat64Blade EncodeOpnsBlade()
    {
        return Weight * Direction.Op(GeometricSpace.Ei);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override CGaFloat64Blade EncodeIpnsBlade()
    {
        return -Weight * Direction.VGaDual().Op(GeometricSpace.Ei);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        if (Weight.IsNearZero())
            return "Zero Conformal Direction";

        return new StringBuilder()
            .AppendLine("Conformal Direction:")
            .AppendLine($"   Weight: ${BasisSpecs.ToLaTeX(Weight)}$")
            .AppendLine($"   Unit Direction Grade: ${Direction.Grade}$")
            .AppendLine($"   Unit Direction: ${Direction.ToLaTeX()}$")
            .AppendLine($"   Unit Direction Normal: ${NormalDirection.ToLaTeX()}$")
            .AppendLine($"   OPNS Blade: ${EncodeOpnsBlade().ToLaTeX()}$")
            .AppendLine($"   IPNS Blade: ${EncodeIpnsBlade().ToLaTeX()}$")
            .ToString();
    }
}