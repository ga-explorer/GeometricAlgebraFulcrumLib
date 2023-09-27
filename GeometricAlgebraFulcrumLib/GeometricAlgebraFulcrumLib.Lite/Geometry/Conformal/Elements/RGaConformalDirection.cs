using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Blades;
using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using System.Data;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Elements;

public class RGaConformalDirection :
    RGaConformalElement
{
    public override double RadiusSquared
    {
        get => 0d;
        set => throw new ReadOnlyException();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaConformalDirection(RGaConformalSpace conformalSpace, double weight, RGaConformalBlade direction)
        : base(
            conformalSpace,
            RGaConformalElementKind.Direction,
            weight, 
            conformalSpace.ZeroVectorBlade,
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
    public override bool IsSameElement(RGaConformalElement element2, bool ignoreWeight = false)
    {
        if (element2 is not RGaConformalDirection direction2)
            return false;

        if (!ignoreWeight && !Weight.IsNearEqual(element2.Weight))
            return false;

        if (!Direction.IsNearEqual(direction2.Direction))
            return false;

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaConformalBlade EncodeOpnsBlade()
    {
        return Weight * Direction.Op(ConformalSpace.Ei);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaConformalBlade EncodeIpnsBlade()
    {
        return -Weight * Direction.EGaDual().Op(ConformalSpace.Ei);
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalDirection TranslateBy(Float64Vector2D egaVector)
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalDirection TranslateBy(Float64Vector3D egaVector)
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalDirection TranslateBy(Float64Vector egaVector)
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalDirection TranslateBy(RGaFloat64Vector egaVector)
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalDirection TranslateBy(RGaConformalBlade egaVector)
    {
        Debug.Assert(egaVector.IsEGaVector());

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        if (Weight.IsNearZero())
            return "Zero Conformal Direction";

        return new StringBuilder()
            .AppendLine("Conformal Direction:")
            .AppendLine($"   Weight: ${ConformalSpace.ToLaTeX(Weight)}$")
            .AppendLine($"   Unit Direction Grade: ${Direction.Grade}$")
            .AppendLine($"   Unit Direction: ${Direction.ToLaTeX()}$")
            .AppendLine($"   Unit Direction Normal: ${NormalDirection.ToLaTeX()}$")
            .AppendLine($"   OPNS Blade: ${EncodeOpnsBlade().ToLaTeX()}$")
            .AppendLine($"   IPNS Blade: ${EncodeIpnsBlade().ToLaTeX()}$")
            .ToString();
    }
}