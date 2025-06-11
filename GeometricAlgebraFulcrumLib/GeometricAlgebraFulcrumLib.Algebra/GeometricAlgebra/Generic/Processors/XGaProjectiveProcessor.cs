using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Spaces;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;

public class XGaProjectiveProcessor<T> :
    XGaProcessor<T>
{
    internal XGaProjectiveProcessor(IScalarProcessor<T> scalarProcessor)
        : base(scalarProcessor, 0, 1)
    {
    }
    

    public XGaKVector<T> PGaDual(XGaKVector<T> kVector, int vSpaceDimensions)
    {
        if (kVector.VSpaceDimensions > vSpaceDimensions)
            throw new InvalidCastException();

        var sign = vSpaceDimensions.CliffordConjugateIsPositiveOfGrade() ? 1 : -1;

        var termList =
            kVector.IdScalarPairs.Select(
                term =>
                {
                    var signedBasisBlade = EUnDual(term.Key, vSpaceDimensions);

                    return new KeyValuePair<IndexSet, T>(
                        signedBasisBlade.Id,
                        ScalarProcessor.Times(
                            term.Value, 
                            sign * signedBasisBlade.Sign
                        ).ScalarValue
                    );
                }
            );

        return CreateKVectorComposer(vSpaceDimensions - kVector.Grade)
            .AddTerms(termList)
            .GetKVector();
    }

    public XGaMultivector<T> PGaDual(XGaMultivector<T> mv, int vSpaceDimensions)
    {
        if (mv.VSpaceDimensions > vSpaceDimensions)
            throw new InvalidCastException();

        var sign = vSpaceDimensions.CliffordConjugateIsPositiveOfGrade() ? 1 : -1;

        var termList =
            mv.IdScalarPairs.Select(
                term =>
                {
                    var signedBasisBlade = EUnDual(term.Key, vSpaceDimensions);

                    return new KeyValuePair<IndexSet, T>(
                        signedBasisBlade.Id,
                        ScalarProcessor.Times(
                            term.Value, 
                            sign * signedBasisBlade.Sign
                        ).ScalarValue
                    );
                }
            );

        return CreateMultivectorComposer()
            .AddTerms(termList)
            .GetSimpleMultivector();
    }

    
    public XGaKVector<T> PGaUnDual(XGaKVector<T> kVector, int vSpaceDimensions)
    {
        if (kVector.VSpaceDimensions > vSpaceDimensions)
            throw new InvalidCastException();

        var sign = vSpaceDimensions.GradeInvolutionIsPositiveOfGrade() ? 1 : -1;

        var termList =
            kVector.IdScalarPairs.Select(
                term =>
                {
                    var signedBasisBlade = EUnDual(term.Key, vSpaceDimensions);

                    return new KeyValuePair<IndexSet, T>(
                        signedBasisBlade.Id,
                        ScalarProcessor.Times(
                            term.Value, 
                            sign * signedBasisBlade.Sign
                        ).ScalarValue
                    );
                }
            );

        return CreateKVectorComposer(vSpaceDimensions - kVector.Grade)
            .AddTerms(termList)
            .GetKVector();
    }

    public XGaMultivector<T> PGaUnDual(XGaMultivector<T> mv, int vSpaceDimensions)
    {
        if (mv.VSpaceDimensions > vSpaceDimensions)
            throw new InvalidCastException();

        var sign = vSpaceDimensions.GradeInvolutionIsPositiveOfGrade() ? 1 : -1;

        var termList =
            mv.IdScalarPairs.Select(
                term =>
                {
                    var signedBasisBlade = EUnDual(term.Key, vSpaceDimensions);

                    return new KeyValuePair<IndexSet, T>(
                        signedBasisBlade.Id,
                        ScalarProcessor.Times(
                            term.Value, 
                            sign * signedBasisBlade.Sign
                        ).ScalarValue
                    );
                }
            );

        return CreateMultivectorComposer()
            .AddTerms(termList)
            .GetSimpleMultivector();
    }


    public XGaKVector<T> PGaPolarity(XGaKVector<T> kVector, int vSpaceDimensions)
    {
        if (kVector.VSpaceDimensions > vSpaceDimensions)
            throw new InvalidCastException();

        var termList =
            kVector.IdScalarPairs.Select(
                term =>
                {
                    var signedBasisBlade = UnDual(term.Key, vSpaceDimensions);

                    return new KeyValuePair<IndexSet, T>(
                        signedBasisBlade.Id,
                        ScalarProcessor.Times(
                            term.Value, 
                            signedBasisBlade.Sign
                        ).ScalarValue
                    );
                }
            );

        return CreateKVectorComposer(vSpaceDimensions - kVector.Grade)
            .AddTerms(termList)
            .GetKVector();
    }
    
    public XGaMultivector<T> PGaPolarity(XGaMultivector<T> mv, int vSpaceDimensions)
    {
        if (mv.VSpaceDimensions > vSpaceDimensions)
            throw new InvalidCastException();

        var termList =
            mv.IdScalarPairs.Select(
                term =>
                {
                    var signedBasisBlade = UnDual(term.Key, vSpaceDimensions);

                    return new KeyValuePair<IndexSet, T>(
                        signedBasisBlade.Id,
                        ScalarProcessor.Times(
                            term.Value, 
                            signedBasisBlade.Sign
                        ).ScalarValue
                    );
                }
            );

        return CreateMultivectorComposer()
            .AddTerms(termList)
            .GetSimpleMultivector();
    }

    
    public XGaKVector<T> InnerProduct(XGaKVector<T> kVector1, XGaKVector<T> kVector2)
    {
        return kVector1.Fdp(kVector2);
    }

    public XGaKVector<T> Meet(XGaKVector<T> kVector1, XGaKVector<T> kVector2)
    {
        return kVector1.Op(kVector2);
    }
    
    public XGaMultivector<T> Meet(XGaMultivector<T> kVector1, XGaMultivector<T> kVector2)
    {
        return kVector1.Op(kVector2);
    }

    public XGaKVector<T> Join(XGaKVector<T> kVector1, XGaKVector<T> kVector2, int vSpaceDimensions)
    {
        var kVector1Dual = PGaDual(kVector1, vSpaceDimensions);
        var kVector2Dual = PGaDual(kVector2, vSpaceDimensions);

        return PGaUnDual(
            kVector1Dual.Op(kVector2Dual),
            vSpaceDimensions
        );
    }
    
    public XGaMultivector<T> Join(XGaMultivector<T> kVector1, XGaMultivector<T> kVector2, int vSpaceDimensions)
    {
        var kVector1Dual = PGaDual(kVector1, vSpaceDimensions);
        var kVector2Dual = PGaDual(kVector2, vSpaceDimensions);

        return PGaUnDual(
            kVector1Dual.Op(kVector2Dual),
            vSpaceDimensions
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaProjectiveSpace<T> CreateSpace(int vSpaceDimensions)
    {
        return new XGaProjectiveSpace<T>(this, vSpaceDimensions);
    }

}