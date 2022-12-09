using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra
{
    public sealed record BasisBilinearProductResult
    {
        public int Signature { get; }

        public ulong Id { get; }

        public uint Grade 
            => Id.BasisBladeIdToGrade();

        public ulong Index 
            => Id.BasisBladeIdToIndex();

        public bool IsZeroSignature 
            => Signature == 0;
        

        internal BasisBilinearProductResult(int signature, ulong id)
        {
            Debug.Assert(signature is >= -1 and <= 1);

            Signature = signature;
            Id = id;
        }


        public KVectorStorage<double> GetBasisBladeTermFloat64()
        {
            return ScalarAlgebraFloat64Processor.DefaultProcessor.CreateKVectorStorageTerm(Id, Signature);
        }
    }
}