using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures
{
    public sealed record GaTermsBilinearProductResult<T>
    {
        public IScalarProcessor<T> ScalarProcessor { get; }

        public int Signature { get; }

        public ulong Id { get; }

        public uint Grade => 
            Id.BasisBladeIdToGrade();

        public ulong Index => 
            Id.BasisBladeIdToIndex();

        public bool IsZeroSignature 
            => Signature == 0;

        public T Scalar1 { get; }

        public T Scalar2 { get; }

        public T Scalar
        {
            get
            {
                if (Signature == 0)
                    return ScalarProcessor.ScalarZero;

                return Signature < 0 
                    ? ScalarProcessor.NegativeTimes(Scalar1, Scalar2)
                    : ScalarProcessor.Times(Scalar1, Scalar2);
            }
        } 

        internal GaTermsBilinearProductResult([NotNull] IScalarProcessor<T> scalarProcessor, int signature, ulong id, T scalar1, T scalar2)
        {
            Debug.Assert(signature is >= -1 and <= 1);

            ScalarProcessor = scalarProcessor;
            Signature = signature;
            Id = id;
            Scalar1 = scalar1;
            Scalar2 = scalar2;
        }
        
    }
}