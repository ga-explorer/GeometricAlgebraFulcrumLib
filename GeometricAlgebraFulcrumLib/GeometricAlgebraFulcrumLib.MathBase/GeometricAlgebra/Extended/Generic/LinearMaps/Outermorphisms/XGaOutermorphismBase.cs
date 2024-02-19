using System.Runtime.CompilerServices;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic.LinearMaps;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Outermorphisms;

public abstract class XGaOutermorphismBase<T> :
    IXGaOutermorphism<T>
{
    public abstract XGaProcessor<T> Processor { get; }

    public XGaMetric Metric 
        => Processor;

    public IScalarProcessor<T> ScalarProcessor 
        => Processor.ScalarProcessor;


    public abstract bool IsValid();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaUnilinearMap<T> GetAdjoint()
    {
        return GetOmAdjoint();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> MapBasisBlade(IIndexSet id)
    {
        return OmMapBasisBlade(id);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> Map(XGaMultivector<T> multivector)
    {
        return OmMap(multivector);
    }
        
    public abstract IEnumerable<KeyValuePair<IIndexSet, XGaMultivector<T>>> GetMappedBasisBlades(
        int vSpaceDimensions);
        
    public abstract IXGaOutermorphism<T> GetOmAdjoint();
        
    public abstract XGaVector<T> OmMapBasisVector(int index);
        
    public abstract XGaBivector<T> OmMapBasisBivector(int index1, int index2);
        
    public abstract XGaKVector<T> OmMapBasisBlade(IIndexSet id);
        
    public abstract XGaVector<T> OmMap(XGaVector<T> vector);
        
    public abstract XGaBivector<T> OmMap(XGaBivector<T> bivector);
        
    public abstract XGaHigherKVector<T> OmMap(XGaHigherKVector<T> kVector);
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> OmMap(XGaKVector<T> kVector)
    {
        return kVector switch
        {
            XGaScalar<T> s => s,
            XGaVector<T> v => OmMap(v),
            XGaBivector<T> bv => OmMap(bv),
            XGaHigherKVector<T> kv => OmMap(kv),
            _ => throw new InvalidOperationException()
        };
    }

    public abstract XGaMultivector<T> OmMap(XGaMultivector<T> multivector);
        
    public abstract IEnumerable<KeyValuePair<IIndexSet, XGaVector<T>>> GetOmMappedBasisVectors(int vSpaceDimensions);
        
    public abstract LinUnilinearMap<T> GetVectorMapPart(int vSpaceDimensions);
}