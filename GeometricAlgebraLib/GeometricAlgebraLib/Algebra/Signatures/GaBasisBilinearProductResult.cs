using System.Diagnostics;
using GeometricAlgebraLib.Algebra.Basis;
using GeometricAlgebraLib.Processing.Implementations.Float64;
using GeometricAlgebraLib.Storage;

namespace GeometricAlgebraLib.Algebra.Signatures
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