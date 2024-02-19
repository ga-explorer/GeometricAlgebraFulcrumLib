using System.Runtime.CompilerServices;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic.LinearMaps;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Outermorphisms;

public sealed class XGaIdentityOutermorphism<T> : 
    IXGaAutomorphism<T>
{
    public XGaProcessor<T> Processor { get; }

    public XGaMetric Metric 
        => Processor;

    public IScalarProcessor<T> ScalarProcessor 
        => Processor.ScalarProcessor;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaIdentityOutermorphism(XGaProcessor<T> processor)
    {
        Processor = processor;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaOutermorphism<T> GetOmAdjoint()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> OmMapBasisVector(int index)
    {
        return Processor.CreateTermVector(index);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> OmMapBasisBivector(int index1, int index2)
    {
        return Processor.CreateTermBivector(
            index1, 
            index2,
            ScalarProcessor.ScalarOne
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> OmMapBasisBlade(IIndexSet id)
    {
        return Processor.CreateTermKVector(
            id, 
            ScalarProcessor.ScalarOne
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> OmMap(XGaVector<T> vector)
    {
        return vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> OmMap(XGaBivector<T> bivector)
    {
        return bivector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaHigherKVector<T> OmMap(XGaHigherKVector<T> kVector)
    {
        return kVector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> OmMap(XGaKVector<T> kVector)
    {
        return kVector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> OmMap(XGaMultivector<T> multivector)
    {
        return multivector;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return true;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaUnilinearMap<T> GetAdjoint()
    {
        return this;
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


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<IIndexSet, XGaMultivector<T>>> GetMappedBasisBlades(int vSpaceDimensions)
    {
        return Processor
            .GetBasisBladeIds(vSpaceDimensions)
            .Select(id => 
                new KeyValuePair<IIndexSet, XGaMultivector<T>>(
                    id, 
                    Processor.CreateTermKVector(id, ScalarProcessor.ScalarOne)
                )
            );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<IIndexSet, XGaVector<T>>> GetOmMappedBasisVectors(int vSpaceDimensions)
    {
        return Processor
            .GetBasisVectorIds(vSpaceDimensions)
            .Select(id => 
                new KeyValuePair<IIndexSet, XGaVector<T>>(
                    id, 
                    Processor.CreateTermVector(id.FirstIndex)
                )
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMap<T> GetVectorMapPart(int vSpaceDimensions)
    {
        return ScalarProcessor.CreateIdentityLinUnilinearMap(vSpaceDimensions);
    }
}