using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.LinearMaps.Versors;

public abstract class XGaFloat64VersorBase :
    XGaFloat64OutermorphismBase,
    IXGaFloat64Versor
{
    public override XGaFloat64Processor Processor { get; }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected XGaFloat64VersorBase(XGaFloat64Processor metric)
    {
        Processor = metric;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract IXGaFloat64Versor GetVersorInverse();

    public abstract XGaFloat64Multivector GetMultivector();
        
    public abstract XGaFloat64Multivector GetMultivectorReverse();
        
    public abstract XGaFloat64Multivector GetMultivectorInverse();
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IXGaFloat64Outermorphism GetOmAdjoint()
    {
        return GetVersorInverse();
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Vector OmMapBasisVector(int index)
    {
        return OmMap(
            Processor.VectorTerm(index)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Bivector OmMapBasisBivector(int index1, int index2)
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
    public override XGaFloat64KVector OmMapBasisBlade(IIndexSet id)
    {
        return OmMap(
            Processor.KVectorTerm(id, 1d)
        );
    }
        
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64UnilinearMap GetVectorMapPart(int vSpaceDimensions)
    {
        return vSpaceDimensions.CreateLinUnilinearMap(
            index => 
                OmMapBasisVector(index).ToLinVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IEnumerable<KeyValuePair<IIndexSet, XGaFloat64Multivector>> GetMappedBasisBlades(
        int vSpaceDimensions)
    {
        return Processor
            .GetBasisBladeIds(vSpaceDimensions)
            .Select(id => 
                new KeyValuePair<IIndexSet, XGaFloat64Multivector>(
                    id, 
                    OmMapBasisBlade(id)
                )
            );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IEnumerable<KeyValuePair<IIndexSet, XGaFloat64Vector>> GetOmMappedBasisVectors(
        int vSpaceDimensions)
    {
        return vSpaceDimensions
            .GetRange()
            .Select(index => 
                new KeyValuePair<IIndexSet, XGaFloat64Vector>(
                    index.IndexToIndexSet(), 
                    OmMapBasisVector(index)
                )
            );
    }


}