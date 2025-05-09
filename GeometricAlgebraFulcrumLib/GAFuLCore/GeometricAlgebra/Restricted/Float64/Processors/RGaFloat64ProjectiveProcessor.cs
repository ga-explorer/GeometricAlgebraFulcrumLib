using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Processors;

public class RGaFloat64ProjectiveProcessor :
    RGaFloat64Processor
{
    public static RGaFloat64ProjectiveProcessor Instance { get; }
        = new RGaFloat64ProjectiveProcessor();


    private RGaFloat64ProjectiveProcessor()
        : base(0, 1)
    {
    }


    public RGaFloat64KVector PGaDual(RGaFloat64KVector kVector, int vSpaceDimensions)
    {
        if (kVector.VSpaceDimensions > vSpaceDimensions)
            throw new InvalidCastException();

        var sign = vSpaceDimensions.IsEven() ? 1 : -1;

        var termList =
            kVector.IdScalarPairs.Select(
                term =>
                {
                    var signedBasisBlade = EDual(term.Key, vSpaceDimensions);

                    return new KeyValuePair<ulong, double>(
                        signedBasisBlade.Id,
                        term.Value * (sign * signedBasisBlade.Sign)
                    );
                }
            );

        return this
            .CreateComposer()
            .AddTerms(termList)
            .GetKVector(vSpaceDimensions - kVector.Grade);
    }

    public RGaFloat64Multivector PGaDual(RGaFloat64Multivector mv, int vSpaceDimensions)
    {
        if (mv.VSpaceDimensions > vSpaceDimensions)
            throw new InvalidCastException();

        var sign = vSpaceDimensions.IsEven() ? 1 : -1;

        var termList =
            mv.IdScalarPairs.Select(
                term =>
                {
                    var signedBasisBlade = EDual(term.Key, vSpaceDimensions);

                    return new KeyValuePair<ulong, double>(
                        signedBasisBlade.Id,
                        term.Value * (sign * signedBasisBlade.Sign)
                    );
                }
            );

        return this
            .CreateComposer()
            .AddTerms(termList)
            .GetSimpleMultivector();
    }
}