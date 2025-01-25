using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.LinearMaps;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.LinearMaps.Outermorphisms;

public abstract class RGaOutermorphismBase<T> :
    IRGaOutermorphism<T>
{
    public abstract RGaProcessor<T> Processor { get; }

    public RGaMetric Metric 
        => Processor;

    public IScalarProcessor<T> ScalarProcessor 
        => Processor.ScalarProcessor;


    public abstract bool IsValid();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaUnilinearMap<T> GetAdjoint()
    {
        return GetOmAdjoint();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T> MapBasisBlade(ulong id)
    {
        return OmMapBasisBlade(id);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T> Map(RGaMultivector<T> multivector)
    {
        return OmMap(multivector);
    }
        
    public abstract IEnumerable<KeyValuePair<ulong, RGaMultivector<T>>> GetMappedBasisBlades(int vSpaceDimensions);
        
    public abstract IRGaOutermorphism<T> GetOmAdjoint();
        
    public abstract RGaVector<T> OmMapBasisVector(int index);
        
    public abstract RGaBivector<T> OmMapBasisBivector(int index1, int index2);
        
    public abstract RGaKVector<T> OmMapBasisBlade(ulong id);
        
    public abstract RGaVector<T> OmMap(RGaVector<T> vector);
        
    public abstract RGaBivector<T> OmMap(RGaBivector<T> bivector);
        
    public abstract RGaHigherKVector<T> OmMap(RGaHigherKVector<T> kVector);
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaKVector<T> OmMap(RGaKVector<T> kVector)
    {
        return kVector switch
        {
            RGaScalar<T> s => s,
            RGaVector<T> v => OmMap(v),
            RGaBivector<T> bv => OmMap(bv),
            RGaHigherKVector<T> kv => OmMap(kv),
            _ => throw new InvalidOperationException()
        };
    }

    public abstract RGaMultivector<T> OmMap(RGaMultivector<T> multivector);
        
    public abstract IEnumerable<KeyValuePair<ulong, RGaVector<T>>> GetOmMappedBasisVectors(int vSpaceDimensions);

    public virtual LinUnilinearMap<T> GetVectorMapPart(int vSpaceDimensions)
    {
        var indexVectorDictionary = vSpaceDimensions.GetRange(
                index =>
                    new KeyValuePair<int, RGaVector<T>>(
                        index, 
                        OmMapBasisVector(index)
                    )
            ).Where(p => !p.Value.IsZero)
            .ToDictionary(
                p => p.Key,
                p => p.Value.ToLinVector()
            );

        return indexVectorDictionary.ToLinUnilinearMap(ScalarProcessor);
    }
}