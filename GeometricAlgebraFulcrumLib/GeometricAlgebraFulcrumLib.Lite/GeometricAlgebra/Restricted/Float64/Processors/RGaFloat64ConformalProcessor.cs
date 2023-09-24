using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Processors
{
    /// <summary>
    /// https://en.wikipedia.org/wiki/Conformal_geometric_algebra
    /// </summary>
    public class RGaFloat64ConformalProcessor :
        RGaFloat64Processor
    {
        public static RGaFloat64ConformalProcessor Instance { get; }
            = new RGaFloat64ConformalProcessor();


        public RGaFloat64Vector OriginBasisVector { get; }

        public RGaFloat64Vector InfinityBasisVector { get; }


        private RGaFloat64ConformalProcessor()
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
}