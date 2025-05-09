using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Core.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Processors;

public class RGaProjectiveProcessor<T> :
    RGaProcessor<T>
{
    internal RGaProjectiveProcessor(IScalarProcessor<T> scalarProcessor)
        : base(scalarProcessor, 0, 1)
    {
    }
    

    public RGaKVector<T> PGaDual(RGaKVector<T> kVector, int vSpaceDimensions)
    {
        if (kVector.VSpaceDimensions > vSpaceDimensions)
            throw new InvalidCastException();

        var sign = vSpaceDimensions.IsEven() ? 1 : -1;

        var termList =
            kVector.IdScalarPairs.Select(
                term =>
                {
                    var signedBasisBlade = EDual(term.Key, vSpaceDimensions);

                    return new KeyValuePair<ulong, T>(
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

    public RGaMultivector<T> PGaDual(RGaMultivector<T> mv, int vSpaceDimensions)
    {
        if (mv.VSpaceDimensions > vSpaceDimensions)
            throw new InvalidCastException();

        var sign = vSpaceDimensions.IsEven() ? 1 : -1;

        var termList =
            mv.IdScalarPairs.Select(
                term =>
                {
                    var signedBasisBlade = EDual(term.Key, vSpaceDimensions);

                    return new KeyValuePair<ulong, T>(
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
}