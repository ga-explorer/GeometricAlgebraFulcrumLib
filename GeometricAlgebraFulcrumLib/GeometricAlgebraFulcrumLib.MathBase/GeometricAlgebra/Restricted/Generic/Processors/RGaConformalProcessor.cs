using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors
{
    /// <summary>
    /// https://en.wikipedia.org/wiki/Conformal_geometric_algebra
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RGaConformalProcessor<T> :
        RGaProcessor<T>
    {
        public RGaVector<T> OriginBasisVector { get; }

        public RGaVector<T> InfinityBasisVector { get; }


        internal RGaConformalProcessor(IScalarProcessor<T> scalarProcessor)
            : base(scalarProcessor, 1, 0)
        {
            var scalarOne = ScalarProcessor.ScalarOne;
            var scalarHalf = ScalarProcessor.Inverse(ScalarProcessor.ScalarTwo);

            InfinityBasisVector =
                this.CreateComposer()
                    .SetVectorTerm(0, scalarOne)
                    .SetVectorTerm(1, scalarOne)
                    .GetVector();

            OriginBasisVector =
                this.CreateComposer()
                    .SetVectorTerm(0, scalarHalf)
                    .SubtractVectorTerm(1, scalarHalf)
                    .GetVector();
        }
    }
}