using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures
{
    public sealed record GaBasisBilinearProductResult
    {
        public int Signature { get; }

        public ulong Id { get; }

        public uint Grade 
            => Id.BasisBladeIdToGrade();

        public ulong Index 
            => Id.BasisBladeIdToIndex();

        public bool IsZeroSignature 
            => Signature == 0;
        

        internal GaBasisBilinearProductResult(int signature, ulong id)
        {
            Debug.Assert(signature is >= -1 and <= 1);

            Signature = signature;
            Id = id;
        }


        public IGaKVectorStorage<double> GetBasisBladeTermFloat64()
        {
            return Float64ScalarProcessor.DefaultProcessor.CreateKVectorStorage(Id, Signature);
        }
    }
}