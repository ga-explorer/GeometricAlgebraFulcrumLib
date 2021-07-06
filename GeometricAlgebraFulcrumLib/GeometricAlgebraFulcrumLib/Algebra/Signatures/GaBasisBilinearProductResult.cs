using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Processing.Implementations.Float64;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Algebra.Signatures
{
    public readonly struct GaBasisBilinearProductResult
    {
        public int Signature { get; }

        public ulong Id { get; }

        public int Grade 
            => Id.BasisBladeGrade();

        public ulong Index 
            => Id.BasisBladeIndex();

        public bool IsZeroSignature 
            => Signature == 0;
        

        internal GaBasisBilinearProductResult(int signature, ulong id)
        {
            Debug.Assert(signature is >= -1 and <= 1);

            Signature = signature;
            Id = id;
        }


        public GaKVectorTermStorage<double> GetBasisBladeTermFloat64()
        {
            return GaKVectorTermStorage<double>.Create(
                GaScalarProcessorFloat64.DefaultProcessor, 
                Id,
                Signature
            );
        }
    }
}