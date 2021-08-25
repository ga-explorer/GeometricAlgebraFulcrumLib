using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Processing.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

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


        public IGaStorageKVector<double> GetBasisBladeTermFloat64()
        {
            return GaScalarProcessorFloat64.DefaultProcessor.CreateStorageKVector(Id, Signature);
        }
    }
}