using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Basis;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.LinearMaps.BasisMaps;

public static class RGaFloat64BilinearBasisMapComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ComputedBilinearBasisMap CreateBasisMap(this RGaFloat64Processor processor, Func<ulong, ulong, RGaFloat64ScaledBasisBlade> basisMappingFunc)
    {
        return new RGaFloat64ComputedBilinearBasisMap(
            processor,
            basisMappingFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ComputedBilinearBasisMap CreateBasisMapFromOp(this RGaFloat64Processor processor)
    {
        return new RGaFloat64ComputedBilinearBasisMap(
            processor,
            (id1, id2) => 
                processor.CreateScaledBasisBlade(
                    processor.Op(id1, id2)
                )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ComputedBilinearBasisMap CreateBasisMapFromGp(this RGaFloat64Processor processor)
    {
        return new RGaFloat64ComputedBilinearBasisMap(
            processor,
            (id1, id2) => 
                processor.CreateScaledBasisBlade(
                    processor.Gp(id1, id2)
                )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ComputedBilinearBasisMap CreateBasisMapFromEGp(this RGaFloat64Processor processor)
    {
        return new RGaFloat64ComputedBilinearBasisMap(
            processor,
            (id1, id2) => 
                processor.CreateScaledBasisBlade(
                    processor.EGp(id1, id2)
                )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ComputedBilinearBasisMap CreateBasisMapFromLcp(this RGaFloat64Processor processor)
    {
        return new RGaFloat64ComputedBilinearBasisMap(
            processor,
            (id1, id2) => 
                processor.CreateScaledBasisBlade(
                    processor.Lcp(id1, id2)
                )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ComputedBilinearBasisMap CreateBasisMapFromELcp(this RGaFloat64Processor processor)
    {
        return new RGaFloat64ComputedBilinearBasisMap(
            processor,
            (id1, id2) => 
                processor.CreateScaledBasisBlade(
                    processor.ELcp(id1, id2)
                )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ComputedBilinearBasisMap CreateBasisMapFromRcp(this RGaFloat64Processor processor)
    {
        return new RGaFloat64ComputedBilinearBasisMap(
            processor,
            (id1, id2) => 
                processor.CreateScaledBasisBlade(
                    processor.Rcp(id1, id2)
                )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ComputedBilinearBasisMap CreateBasisMapFromERcp(this RGaFloat64Processor processor)
    {
        return new RGaFloat64ComputedBilinearBasisMap(
            processor,
            (id1, id2) => 
                processor.CreateScaledBasisBlade(
                    processor.ERcp(id1, id2)
                )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ComputedBilinearBasisMap CreateBasisMapFromFdp(this RGaFloat64Processor processor)
    {
        return new RGaFloat64ComputedBilinearBasisMap(
            processor,
            (id1, id2) => 
                processor.CreateScaledBasisBlade(
                    processor.Fdp(id1, id2)
                )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ComputedBilinearBasisMap CreateBasisMapFromEFdp(this RGaFloat64Processor processor)
    {
        return new RGaFloat64ComputedBilinearBasisMap(
            processor,
            (id1, id2) => 
                processor.CreateScaledBasisBlade(
                    processor.EFdp(id1, id2)
                )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ComputedBilinearBasisMap CreateBasisMapFromHip(this RGaFloat64Processor processor)
    {
        return new RGaFloat64ComputedBilinearBasisMap(
            processor,
            (id1, id2) => 
                processor.CreateScaledBasisBlade(
                    processor.Hip(id1, id2)
                )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ComputedBilinearBasisMap CreateBasisMapFromEHip(this RGaFloat64Processor processor)
    {
        return new RGaFloat64ComputedBilinearBasisMap(
            processor,
            (id1, id2) => 
                processor.CreateScaledBasisBlade(
                    processor.EHip(id1, id2)
                )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ComputedBilinearBasisMap CreateBasisMapFromCp(this RGaFloat64Processor processor)
    {
        return new RGaFloat64ComputedBilinearBasisMap(
            processor,
            (id1, id2) => 
                processor.CreateScaledBasisBlade(
                    processor.Cp(id1, id2)
                )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ComputedBilinearBasisMap CreateBasisMapFromECp(this RGaFloat64Processor processor)
    {
        return new RGaFloat64ComputedBilinearBasisMap(
            processor,
            (id1, id2) => 
                processor.CreateScaledBasisBlade(
                    processor.ECp(id1, id2)
                )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ComputedBilinearBasisMap CreateBasisMapFromAcp(this RGaFloat64Processor processor)
    {
        return new RGaFloat64ComputedBilinearBasisMap(
            processor,
            (id1, id2) => 
                processor.CreateScaledBasisBlade(
                    processor.Acp(id1, id2)
                )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ComputedBilinearBasisMap CreateBasisMapFromEAcp(this RGaFloat64Processor processor)
    {
        return new RGaFloat64ComputedBilinearBasisMap(
            processor,
            (id1, id2) => 
                processor.CreateScaledBasisBlade(
                    processor.EAcp(id1, id2)
                )
        );
    }


}