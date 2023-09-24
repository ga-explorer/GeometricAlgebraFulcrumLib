using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Processors;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Basis;

public static class RGaFloat64ScaledBasisBladeComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ScaledBasisBlade CreateScaledBasisVector(this RGaFloat64Processor processor, int index, double scalar)
    {
        return new RGaFloat64ScaledBasisBlade(
            processor,
            index.BasisVectorIndexToId(),
            scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ScaledBasisBlade CreateScaledBasisBlade(this RGaFloat64Processor processor, ulong id)
    {
        return new RGaFloat64ScaledBasisBlade(
            processor,
            id,
            Float64Scalar.One
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ScaledBasisBlade CreateScaledBasisBlade(this RGaFloat64Processor processor, ulong id, double scalar)
    {
        return new RGaFloat64ScaledBasisBlade(
            processor,
            id,
            scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ScaledBasisBlade CreateScaledBasisBlade(this RGaFloat64Processor processor, ulong id, IntegerSign scalar)
    {
        return new RGaFloat64ScaledBasisBlade(
            processor,
            id,
            scalar.ToFloat64()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ScaledBasisBlade CreateScaledBasisBlade(this RGaFloat64Processor processor, RGaBasisBlade basisBlade)
    {
        return new RGaFloat64ScaledBasisBlade(
            processor,
            basisBlade.Id,
            Float64Scalar.One
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ScaledBasisBlade CreateScaledBasisBlade(this RGaFloat64Processor processor, RGaBasisBlade basisBlade, double scalar)
    {
        return new RGaFloat64ScaledBasisBlade(
            processor,
            basisBlade.Id,
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ScaledBasisBlade CreateScaledBasisBlade(this RGaFloat64Processor processor, IRGaSignedBasisBlade basisBlade)
    {
        return new RGaFloat64ScaledBasisBlade(
            processor,
            basisBlade.Id,
            basisBlade.Sign.ToFloat64()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ScaledBasisBlade CreateScaledBasisBlade(this RGaFloat64Processor processor, IRGaSignedBasisBlade basisBlade, IntegerSign sign)
    {
        return new RGaFloat64ScaledBasisBlade(
            processor,
            basisBlade.Id,
            (basisBlade.Sign * sign).ToFloat64()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ScaledBasisBlade CreateScaledBasisBlade(this RGaFloat64Processor processor, IRGaSignedBasisBlade basisBlade, double scalar)
    {
        return new RGaFloat64ScaledBasisBlade(
            processor,
            basisBlade.Id,
            basisBlade.Sign.ToFloat64() * scalar
        );
    }

}