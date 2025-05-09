using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.LinearMaps;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Core.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.LinearMaps.Versors;

public abstract class XGaVersorBase<T> :
    XGaOutermorphismBase<T>,
    IXGaVersor<T>
{
    public override XGaProcessor<T> Processor { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected XGaVersorBase(XGaProcessor<T> processor)
    {
        Processor = processor;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract IXGaVersor<T> GetVersorInverse();

    public abstract XGaMultivector<T> GetMultivector();
        
    public abstract XGaMultivector<T> GetMultivectorReverse();
        
    public abstract XGaMultivector<T> GetMultivectorInverse();
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IXGaOutermorphism<T> GetOmAdjoint()
    {
        return GetVersorInverse();
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaVector<T> OmMapBasisVector(int index)
    {
        return OmMap(
            Processor.VectorTerm(index)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> OmMapBasisBivector(int index1, int index2)
    {
        if (index1 < 0 || index1 >= index2)
            throw new InvalidOperationException();

        return OmMap(
            Processor.BivectorTerm(
                index1, 
                index2, 
                ScalarProcessor.OneValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> OmMapBasisBlade(IndexSet id)
    {
        return OmMap(
            Processor.KVectorTerm(
                id, 
                ScalarProcessor.OneValue
            )
        );
    }
        
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IEnumerable<KeyValuePair<IndexSet, XGaMultivector<T>>> GetMappedBasisBlades(
        int vSpaceDimensions)
    {
        return Processor
            .GetBasisBladeIds(vSpaceDimensions)
            .Select(id => 
                new KeyValuePair<IndexSet, XGaMultivector<T>>(
                    id, 
                    OmMapBasisBlade(id)
                )
            );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IEnumerable<KeyValuePair<IndexSet, XGaVector<T>>> GetOmMappedBasisVectors(int vSpaceDimensions)
    {
        return vSpaceDimensions
            .GetRange()
            .Select(index => 
                new KeyValuePair<IndexSet, XGaVector<T>>(
                    index.IndexToIndexSet(), 
                    OmMapBasisVector(index)
                )
            );
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinUnilinearMap<T> GetVectorMapPart(int vSpaceDimensions)
    {
        return ScalarProcessor.CreateLinUnilinearMap(
            vSpaceDimensions,
            index => OmMapBasisVector(index).ToLinVector()
        );
    }

}