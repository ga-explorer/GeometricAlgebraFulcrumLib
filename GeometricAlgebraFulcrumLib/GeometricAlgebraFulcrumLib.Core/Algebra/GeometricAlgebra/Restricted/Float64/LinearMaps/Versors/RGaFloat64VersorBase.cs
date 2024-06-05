using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps.Versors;

public abstract class RGaFloat64VersorBase :
    RGaFloat64OutermorphismBase,
    IRGaFloat64Versor
{
    public override RGaFloat64Processor Processor { get; }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected RGaFloat64VersorBase(RGaFloat64Processor metric)
    {
        Processor = metric;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract IRGaFloat64Versor GetVersorInverse();

    public abstract RGaFloat64Multivector GetMultivector();
        
    public abstract RGaFloat64Multivector GetMultivectorReverse();
        
    public abstract RGaFloat64Multivector GetMultivectorInverse();
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IRGaFloat64Outermorphism GetOmAdjoint()
    {
        return GetVersorInverse();
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Vector OmMapBasisVector(int index)
    {
        return OmMap(
            Processor.VectorTerm(index)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Bivector OmMapBasisBivector(int index1, int index2)
    {
        if (index1 < 0 || index1 >= index2)
            throw new InvalidOperationException();

        return OmMap(
            Processor.BivectorTerm(
                index1, 
                index2, 
                1d
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64KVector OmMapBasisBlade(ulong id)
    {
        return OmMap(
            Processor.KVectorTerm(id, 1d)
        );
    }
        
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IEnumerable<KeyValuePair<ulong, RGaFloat64Multivector>> GetMappedBasisBlades(int vSpaceDimensions)
    {
        return Processor
            .GetBasisBladeIds(vSpaceDimensions)
            .Select(id => 
                new KeyValuePair<ulong, RGaFloat64Multivector>(
                    id, 
                    OmMapBasisBlade(id)
                )
            );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IEnumerable<KeyValuePair<ulong, RGaFloat64Vector>> GetOmMappedBasisVectors(int vSpaceDimensions)
    {
        return vSpaceDimensions
            .GetRange()
            .Select(index => 
                new KeyValuePair<ulong, RGaFloat64Vector>(
                    index.BasisVectorIndexToId(), 
                    OmMapBasisVector(index)
                )
            );
    }


}