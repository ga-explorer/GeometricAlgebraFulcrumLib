using System.Runtime.CompilerServices;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.LinearMaps.Projectors;

public sealed class XGaFloat64Projector :
    XGaFloat64OutermorphismBase,
    IXGaFloat64Projector
{
    public override XGaFloat64Processor Processor 
        => Blade.Processor;
        
    public XGaFloat64KVector Blade { get; }

    public XGaFloat64KVector BladePseudoInverse { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaFloat64Projector(XGaFloat64KVector blade, bool useBladeInverse)
    {
        Blade = blade;
        BladePseudoInverse = useBladeInverse ? blade.PseudoInverse() : blade;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return true;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IXGaFloat64Outermorphism GetOmAdjoint()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Vector OmMapBasisVector(int index)
    {
        var id = index.IndexToIndexSet();

        return OmMapBasisBlade(id).GetVectorPart();

    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Bivector OmMapBasisBivector(int index1, int index2)
    {
        var id = IndexSetUtils.IndexPairToIndexSet(index1, index2);

        return OmMapBasisBlade(id).GetBivectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector OmMapBasisBlade(IIndexSet id)
    {
        return OmMap(
            Processor.CreateTermKVector(id, 1d)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Vector OmMap(XGaFloat64Vector vector)
    {
        return vector.Fdp(BladePseudoInverse).Gp(Blade).GetVectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Bivector OmMap(XGaFloat64Bivector bivector)
    {
        return bivector.Fdp(BladePseudoInverse).Gp(Blade).GetBivectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64HigherKVector OmMap(XGaFloat64HigherKVector kVector)
    {
        return kVector.Fdp(BladePseudoInverse).Gp(Blade).GetHigherKVectorPart(kVector.Grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector OmMap(XGaFloat64Multivector multivector)
    {
        return multivector.Fdp(BladePseudoInverse).Gp(Blade);
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
    public override IEnumerable<KeyValuePair<IIndexSet, XGaFloat64Vector>> GetOmMappedBasisVectors(int vSpaceDimensions)
    {
        return Processor
            .GetBasisVectorIds(vSpaceDimensions)
            .Select(id => 
                new KeyValuePair<IIndexSet, XGaFloat64Vector>(
                    id,
                    OmMap(Processor.CreateTermVector(id))
                )
            ).Where(r => !r.Value.IsZero);
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
            ).Where(r => !r.Value.IsZero);
    }
}