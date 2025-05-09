using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Processors;

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

        return this
            .CreateComposer()
            .AddTerms(termList)
            .GetKVector(vSpaceDimensions - kVector.Grade);
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

        return this
            .CreateComposer()
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

        return this
            .CreateComposer()
            .AddTerms(termList)
            .GetKVector(vSpaceDimensions - kVector.Grade);
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

        return this
            .CreateComposer()
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

        return this
            .CreateComposer()
            .AddTerms(termList)
            .GetKVector(vSpaceDimensions - kVector.Grade);
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

        return this
            .CreateComposer()
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
}