using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Basis;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Basis;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps.BasisMaps;

public static class RGaFloat64UnilinearBasisMapComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ComputedUnilinearBasisMap CreateBasisMap(this RGaFloat64Processor processor, Func<ulong, RGaFloat64ScaledBasisBlade> basisMappingFunc)
    {
        return new RGaFloat64ComputedUnilinearBasisMap(
            processor,
            basisMappingFunc
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ComputedUnilinearBasisMap CreateBasisMapFromNegative(this RGaFloat64Processor processor)
    {
        return new RGaFloat64ComputedUnilinearBasisMap(
            processor,
            id1 => 
                processor.CreateScaledBasisBlade(id1, -1d)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ComputedUnilinearBasisMap CreateBasisMapFromTimes(this RGaFloat64Processor processor, double scalar)
    {
        return new RGaFloat64ComputedUnilinearBasisMap(
            processor,
            id1 => 
                processor.CreateScaledBasisBlade(id1, scalar)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ComputedUnilinearBasisMap CreateBasisMapFromReverse(this RGaFloat64Processor processor)
    {
        return new RGaFloat64ComputedUnilinearBasisMap(
            processor,
            id1 => 
                processor.CreateScaledBasisBlade(id1, processor.ReverseSign(id1))
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ComputedUnilinearBasisMap CreateBasisMapFromGradeInvolution(this RGaFloat64Processor processor)
    {
        return new RGaFloat64ComputedUnilinearBasisMap(
            processor,
            id1 => 
                processor.CreateScaledBasisBlade(id1, processor.GradeInvolutionSign(id1))
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ComputedUnilinearBasisMap CreateBasisMapFromCliffordConjugate(this RGaFloat64Processor processor)
    {
        return new RGaFloat64ComputedUnilinearBasisMap(
            processor,
            id1 => 
                processor.CreateScaledBasisBlade(id1, processor.CliffordConjugateSign(id1))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ComputedUnilinearBasisMap CreateBasisMapFromEDual(this RGaFloat64Processor processor, int vSpaceDimensions)
    {
        var pseudoScalarInverse = 
            processor.CreateBasisPseudoScalarEInverse(vSpaceDimensions);

        var id2 = pseudoScalarInverse.Id;
        var sign2 = pseudoScalarInverse.Sign;

        return new RGaFloat64ComputedUnilinearBasisMap(
            processor,
            id1 =>
            {
                var basisBlade = processor.ELcp(id1, id2);

                return processor.CreateScaledBasisBlade(
                    basisBlade.Id, 
                    sign2 * basisBlade.Sign
                );
            }
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ComputedUnilinearBasisMap CreateBasisMapFromDual(this RGaFloat64Processor processor, int vSpaceDimensions)
    {
        var pseudoScalarInverse = 
            processor.CreateBasisPseudoScalarInverse(vSpaceDimensions);

        var id2 = pseudoScalarInverse.Id;
        var sign2 = pseudoScalarInverse.Sign;

        return new RGaFloat64ComputedUnilinearBasisMap(
            processor,
            id1 =>
            {
                var basisBlade = processor.Lcp(id1, id2);

                return processor.CreateScaledBasisBlade(
                    basisBlade.Id, 
                    sign2 * basisBlade.Sign
                );
            }
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ComputedUnilinearBasisMap CreateBasisMapFromEUnDual(this RGaFloat64Processor processor, int vSpaceDimensions)
    {
        var id2 = 
            processor.GetBasisPseudoScalarId(vSpaceDimensions);
        
        return new RGaFloat64ComputedUnilinearBasisMap(
            processor,
            id1 =>
            {
                var basisBlade = processor.ELcp(id1, id2);

                return processor.CreateScaledBasisBlade(basisBlade);
            }
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ComputedUnilinearBasisMap CreateBasisMapFromUnDual(this RGaFloat64Processor processor, int vSpaceDimensions)
    {
        var id2 = 
            processor.GetBasisPseudoScalarId(vSpaceDimensions);
        
        return new RGaFloat64ComputedUnilinearBasisMap(
            processor,
            id1 =>
            {
                var basisBlade = processor.Lcp(id1, id2);

                return processor.CreateScaledBasisBlade(basisBlade);
            }
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ComputedUnilinearBasisMap CreateBasisMapFromENormal(this RGaFloat64Processor processor, int vSpaceDimensions)
    {
        var id2 = 
            processor.GetBasisPseudoScalarId(vSpaceDimensions);
        
        return new RGaFloat64ComputedUnilinearBasisMap(
            processor,
            id1 =>
            {
                var basisBlade = processor.ELcp(id1, id2);
                var sign = processor.ESpSquaredSign(id1);

                return processor.CreateScaledBasisBlade(basisBlade, sign);
            }
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ComputedUnilinearBasisMap CreateBasisMapFromNormal(this RGaFloat64Processor processor, int vSpaceDimensions)
    {
        var id2 = 
            processor.GetBasisPseudoScalarId(vSpaceDimensions);
        
        return new RGaFloat64ComputedUnilinearBasisMap(
            processor,
            id1 =>
            {
                var basisBlade = processor.Lcp(id1, id2);
                var sign = processor.SpSquaredSign(id1);

                return sign.IsZero
                    ? throw new InvalidOperationException()
                    : processor.CreateScaledBasisBlade(basisBlade, sign);
            }
        );
    }
}