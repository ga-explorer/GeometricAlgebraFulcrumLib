using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors.Composers;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Processors;

/// <summary>
/// https://en.wikipedia.org/wiki/Conformal_geometric_algebra
/// </summary>
public class XGaFloat64ConformalProcessor :
    XGaFloat64Processor
{
    public static XGaFloat64ConformalProcessor Instance { get; }
        = new XGaFloat64ConformalProcessor();


    public XGaFloat64Vector OriginBasisVector { get; }

    public XGaFloat64Vector InfinityBasisVector { get; }


    private XGaFloat64ConformalProcessor()
        : base(1, 0)
    {
        InfinityBasisVector =
            this.CreateComposer()
                .SetVectorTerm(0, 1d)
                .SetVectorTerm(1, 1d)
                .GetVector();

        OriginBasisVector =
            this.CreateComposer()
                .SetVectorTerm(0, 05d)
                .SubtractVectorTerm(1, 05d)
                .GetVector();
    }
}