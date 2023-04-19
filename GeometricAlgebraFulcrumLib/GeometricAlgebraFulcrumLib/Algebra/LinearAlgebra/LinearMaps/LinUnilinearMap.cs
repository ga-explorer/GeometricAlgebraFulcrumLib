//using System.Collections.Generic;
//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Vectors;
//using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
//using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
//using GeometricAlgebraFulcrumLib.Storage;
//using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
//using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
//using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
//using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

//namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.LinearMaps
//{
//    public class LinUnilinearMap<T> :
//        ILinUnilinearMap<T>
//    {
//        public IScalarProcessor<T> ScalarProcessor 
//            => LinearProcessor;

//        public ILinearAlgebraProcessor<T> LinearProcessor { get; }

//        public ILinMatrixStorage<T> MatrixStorage { get; }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        internal LinUnilinearMap(ILinearAlgebraProcessor<T> linearProcessor, ILinMatrixStorage<T> matrixStorage)
//        {
//            LinearProcessor = linearProcessor;
//            MatrixStorage = matrixStorage;
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public ILinUnilinearMap<T> GetLinAdjoint()
//        {
//            return new LinUnilinearMap<T>(
//                LinearProcessor, 
//                MatrixStorage.GetTranspose()
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public ILinVectorStorage<T> LinMapBasisVector(ulong index)
//        {
//            return MatrixStorage.GetColumn(index);
//        }

//        public LinVector<T> LinMapVector(LinVector<T> vector)
//        {
//            var composer = LinearProcessor.CreateVectorStorageComposer();

//            foreach (var (index, scalar) in vector.VectorStorage.GetIndexScalarRecords())
//                composer.AddScaledTerms(
//                    scalar,
//                    LinMapBasisVector(index).GetIndexScalarRecords()
//                );

//            return composer.CreateLinVector();
//        }

//        public ILinMatrixStorage<T> LinMapMatrix(ILinMatrixStorage<T> matrixStorage)
//        {
//            throw new System.NotImplementedException();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public ILinMatrixStorage<T> GetLinMappingMatrix()
//        {
//            return MatrixStorage;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetLinMappedBasisVectors()
//        {
//            return MatrixStorage.GetColumns();
//        }
//    }
//}