using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Operations;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Elements;

public class XGaConformalTangent<T> :
    XGaConformalElement<T>
{
    public override XGaConformalBlade<T> Position { get; }

    public override Scalar<T> RadiusSquared
    {
        get => Position.ConformalSpace.ScalarZero;
        set => throw new ReadOnlyException();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaConformalTangent(XGaConformalSpace<T> conformalSpace, IScalar<T> weight, XGaConformalBlade<T> position, XGaConformalBlade<T> direction)
        : base(
            conformalSpace, 
            XGaConformalElementKind.Tangent, 
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
               Direction.IsEGaBlade() &&
               Position.IsEGaVector() &&
               Direction.Norm().IsNearOne();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsSameElement(XGaConformalElement<T> element2, bool ignoreWeight = false)
    {
        if (element2 is not XGaConformalTangent<T> tangent2)
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
    public override XGaConformalBlade<T> EncodeOpnsBlade()
    {
        return Weight * ConformalSpace.Eo.Op(Direction).TranslateBy(
            Position
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaConformalBlade<T> EncodeIpnsBlade()
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
            .AppendLine($"   Weight: ${BasisSpecs.ToLaTeX(Weight)}$")
            .AppendLine($"   Unit Direction Grade: ${Direction.Grade}$")
            .AppendLine($"   Unit Direction: ${Direction.ToLaTeX()}$")
            .AppendLine($"   Unit Direction Normal: ${NormalDirection.ToLaTeX()}$")
            .AppendLine($"   Position: ${Position.DecodeEGaVector3D()}$")
            .AppendLine($"   OPNS Blade: ${EncodeOpnsBlade().ToLaTeX()}$")
            .AppendLine($"   IPNS Blade: ${EncodeIpnsBlade().ToLaTeX()}$")
            .ToString();
    }
}