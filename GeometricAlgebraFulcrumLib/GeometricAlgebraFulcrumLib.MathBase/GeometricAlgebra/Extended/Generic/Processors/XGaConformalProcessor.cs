using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors
{
    /// <summary>
    /// https://en.wikipedia.org/wiki/Conformal_geometric_algebra
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class XGaConformalProcessor<T> :
        XGaProcessor<T>
    {
        public XGaVector<T> OriginBasisVector { get; }

        public XGaVector<T> InfinityBasisVector { get; }


        internal XGaConformalProcessor(IScalarProcessor<T> scalarProcessor)
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