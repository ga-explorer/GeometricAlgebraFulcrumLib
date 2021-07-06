using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraLib.Algebra.Basis;
using GeometricAlgebraLib.Processing.Scalars;

namespace GeometricAlgebraLib.Algebra.Signatures
{
    public readonly struct GaTermsBilinearProductResult<T>
    {
        public IGaScalarProcessor<T> ScalarProcessor { get; }

        public int Signature { get; }

        public ulong Id { get; }

        public int Grade => 
            Id.BasisBladeGrade();

        public ulong Index => 
            Id.BasisBladeIndex();

        public bool IsZeroSignature 
            => Signature == 0;

        public T Scalar1 { get; }

        public T Scalar2 { get; }

        public T Scalar
        {
            get
            {
                if (Signature == 0)
                    return ScalarProcessor.ZeroScalar;

                return Signature < 0 
                    ? ScalarProcessor.NegativeTimes(Scalar1, Scalar2)
                    : ScalarProcessor.Times(Scalar1, Scalar2);
            }
        } 

        internal GaTermsBilinearProductResult([NotNull] IGaScalarProcessor<T> scalarProcessor, int signature, ulong id, T scalar1, T scalar2)
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