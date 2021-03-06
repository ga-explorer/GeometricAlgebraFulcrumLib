using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra
{
    public class GeometricAlgebraConformalProcessor<T> :
        GeometricAlgebraOrthonormalProcessor<T>
    {
        public Vector<T> OriginBasisVector { get; }

        public Vector<T> InfinityBasisVector { get; }


        internal GeometricAlgebraConformalProcessor(IScalarAlgebraProcessor<T> scalarProcessor, uint vSpaceDimension) 
            : base(
                scalarProcessor, 
                BasisBladeSet.CreateConformal(vSpaceDimension)
            )
        {
            InfinityBasisVector = GetInfinityBasisVectorStorage().CreateVector(this);
            OriginBasisVector = GetOriginBasisVectorStorage().CreateVector(this);
        }


        private VectorStorage<T> GetInfinityBasisVectorStorage()
        {
            return this
                .CreateVectorStorageComposer()
                .AddTerm(VSpaceDimension - 2, ScalarProcessor.ScalarOne)
                .AddTerm(VSpaceDimension - 1, ScalarProcessor.ScalarOne)
                .CreateVectorStorage();
        }
        
        private VectorStorage<T> GetOriginBasisVectorStorage()
        {
            return this
                .CreateVectorStorageComposer()
                .AddTerm(VSpaceDimension - 2, ScalarProcessor.ScalarOne)
                .AddTerm(VSpaceDimension - 1, ScalarProcessor.ScalarOne)
                .CreateVectorStorage();
        }

        
        
    }
}