using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Elements;

public class XGaConformalDirection<T> :
    XGaConformalElement<T>
{
    public override XGaConformalBlade<T> Position 
        => ConformalSpace.ZeroVectorBlade;

    public override Scalar<T> RadiusSquared
    {
        get => Position.ConformalSpace.ScalarZero;
        set => throw new ReadOnlyException();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaConformalDirection(XGaConformalSpace<T> conformalSpace, IScalar<T> weight, XGaConformalBlade<T> direction)
        : base(
            conformalSpace,
            XGaConformalElementKind.Direction,
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
               Direction.IsEGaBlade() &&
               Direction.Norm().IsNearOne() &&
               Position.IsZero();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsSameElement(XGaConformalElement<T> element2, bool ignoreWeight = false)
    {
        if (element2 is not XGaConformalDirection<T> direction2)
            return false;

        if (!ignoreWeight && !Weight.IsNearEqualTo(element2.Weight))
            return false;

        if (!Direction.IsNearEqual(direction2.Direction))
            return false;

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaConformalBlade<T> EncodeOpnsBlade()
    {
        return Weight * Direction.Op(ConformalSpace.Ei);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaConformalBlade<T> EncodeIpnsBlade()
    {
        return -Weight * Direction.EGaDual().Op(ConformalSpace.Ei);
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