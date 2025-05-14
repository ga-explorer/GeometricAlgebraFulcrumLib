using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.Processors;

public class XGaFloat64ProjectiveProcessor :
    XGaFloat64Processor
{
    public static XGaFloat64ProjectiveProcessor Instance { get; }
        = new XGaFloat64ProjectiveProcessor();


    private XGaFloat64ProjectiveProcessor()
        : base(0, 1)
    {
    }


    public XGaFloat64KVector PGaDual(XGaFloat64KVector kVector, int vSpaceDimensions)
    {
        if (kVector.VSpaceDimensions > vSpaceDimensions)
            throw new InvalidCastException();

        var sign = vSpaceDimensions.IsEven() ? 1 : -1;

        var termList =
            kVector.IdScalarPairs.Select(
                term =>
                {
                    var signedBasisBlade = EDual(term.Key, vSpaceDimensions);

                    return new KeyValuePair<IndexSet, double>(
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

    public XGaFloat64Multivector PGaDual(XGaFloat64Multivector mv, int vSpaceDimensions)
    {
        if (mv.VSpaceDimensions > vSpaceDimensions)
            throw new InvalidCastException();

        var sign = vSpaceDimensions.IsEven() ? 1 : -1;

        var termList =
            mv.IdScalarPairs.Select(
                term =>
                {
                    var signedBasisBlade = EDual(term.Key, vSpaceDimensions);

                    return new KeyValuePair<IndexSet, double>(
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