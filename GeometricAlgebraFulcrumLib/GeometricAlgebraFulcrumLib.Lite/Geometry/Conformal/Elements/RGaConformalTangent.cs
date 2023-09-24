using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Elements;

public class RGaConformalTangent :
    RGaConformalElement
{
    public override double RadiusSquared
    {
        get => 0d;
        set => throw new ReadOnlyException();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaConformalTangent(RGaConformalSpace conformalSpace, double weight, RGaConformalBlade position, RGaConformalBlade direction)
        : base(
            conformalSpace, 
            RGaConformalElementKind.Tangent, 
            weight, 
            position, 
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
               Position.IsEGaBlade();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsSameElement(RGaConformalElement element2, bool ignoreWeight = false)
    {
        if (element2 is not RGaConformalTangent tangent2)
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
    public override RGaConformalBlade EncodeOpnsBlade()
    {
        return Weight * ConformalSpace.Eo.Op(Direction).TranslateBy(
            Position
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaConformalBlade EncodeIpnsBlade()
    {
        var direction = 
            ((VSpaceDimensions - 2).IsEven() ? Direction : -Direction).EGaDual();

        return Weight * ConformalSpace.Eo.Op(direction).TranslateBy(
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
            .AppendLine($"   Weight: ${ConformalSpace.ToLaTeX(Weight)}$")
            .AppendLine($"   Unit Direction Grade: ${Direction.Grade}$")
            .AppendLine($"   Unit Direction: ${Direction.ToLaTeX()}$")
            .AppendLine($"   Unit Direction Normal: ${NormalDirection.ToLaTeX()}$")
            .AppendLine($"   Position: ${Position.DecodeEGaVector3D()}$")
            .AppendLine($"   OPNS Blade: ${EncodeOpnsBlade().ToLaTeX()}$")
            .AppendLine($"   IPNS Blade: ${EncodeIpnsBlade().ToLaTeX()}$")
            .ToString();
    }
}