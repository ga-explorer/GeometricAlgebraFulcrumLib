//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
//using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
//using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Records.Extended;

//namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.LinearMaps.Outermorphisms
//{
//    public class XGaOutermorphism<T> :
//        XGaOutermorphismBase<T>
//    {
//        public override IScalarProcessor<T> GeometricProcessor { get; }

//        public OutermorphismStorage<T> OmStorage { get; }


//        internal XGaOutermorphism(IScalarProcessor<T> geometricProcessor, OutermorphismStorage<T> omStorage)
//        {
//            GeometricProcessor = geometricProcessor;
//            OmStorage = omStorage;
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override bool IsValid()
//        {
//            return true;
//        }
        

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override IXGaOutermorphism<T> GetOmAdjoint()
//        {
//            return new XGaOutermorphism<T>(
//                GeometricProcessor,
//                OmStorage.GetTranspose()
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override XGaVector<T> OmMapBasisVector(ulong index)
//        {
//            return GeometricProcessor.Vector(
//                OmStorage.GetMappedBasisVector(index)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override XGaBivector<T> OmMapBasisBivector(ulong index)
//        {
//            return GeometricProcessor.Bivector(
//                OmStorage.GetMappedBasisBivector(index)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override XGaBivector<T> OmMapBasisBivector(ulong index1, ulong index2)
//        {
//            return GeometricProcessor.Bivector(
//                OmStorage.GetMappedBasisBivector(index1, index2)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override XGaKVector<T> OmMapBasisBlade(ulong id)
//        {
//            return GeometricProcessor.KVector(
//                OmStorage.GetMappedBasisBlade(id)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override XGaKVector<T> OmMapBasisBlade(uint grade, ulong index)
//        {
//            return GeometricProcessor.KVector(
//                OmStorage.GetMappedBasisBlade(grade, index)
//            );
//        }


//        public override XGaVector<T> OmMap(XGaVector<T> vector)
//        {
//            var composer = LinearProcessor.CreateVectorStorageComposer();

//            foreach (var (index, scalar) in vector.VectorStorage.GetIndexScalarRecords())
//                composer.AddScaledTerms(
//                    scalar,
//                    OmMapBasisVector(index).VectorStorage.GetIndexScalarRecords()
//                );

//            return composer.Vector();
//        }

//        public override XGaBivector<T> OmMap(XGaBivector<T> bivector)
//        {
//            var composer = LinearProcessor.CreateVectorStorageComposer();

//            foreach (var (index, scalar) in bivector.BivectorStorage.GetIndexScalarRecords())
//                composer.AddScaledTerms(
//                    scalar,
//                    OmMapBasisBivector(index).BivectorStorage.GetIndexScalarRecords()
//                );

//            return composer.Bivector();
//        }

//        public override XGaKVector<T> OmMap(XGaKVector<T> kVector)
//        {
//            var composer = LinearProcessor.CreateVectorStorageComposer();

//            foreach (var (index, scalar) in kVector.KVectorStorage.GetIndexScalarRecords())
//                composer.AddScaledTerms(
//                    scalar,
//                    OmMapBasisVector(index).VectorStorage.GetIndexScalarRecords()
//                );

//            return composer.KVector(kVector.Grade);
//        }

//        public override XGaMultivector<T> OmMap(XGaMultivector<T> multivector)
//        {
//            var composer = LinearProcessor.CreateVectorStorageComposer();

//            foreach (var (id, scalar) in multivector.MultivectorStorage.GetIdScalarRecords())
//                composer.AddScaledTerms(
//                    scalar,
//                    OmMapBasisBlade(id).KVectorStorage.GetIdScalarRecords()
//                );

//            return composer.Multivector();
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override ILinMatrixStorage<T> GetMultivectorMappingMatrixStorage()
//        {
//            return OmStorage
//                .GetMultivectorGradedMappingMatrix()
//                .ToMatrixStorage(BasisBladeUtils.BasisBladeGradeIndexToId);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override ILinMatrixStorage<T> GetVectorOmMappingMatrixStorage()
//        {
//            return OmStorage.GetVectorMappingMatrix();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override ILinMatrixStorage<T> GetBivectorOmMappingMatrixStorage()
//        {
//            return OmStorage.GetBivectorMappingMatrix();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override ILinMatrixStorage<T> GetKVectorOmMappingMatrixStorage(uint grade)
//        {
//            return OmStorage.GetKVectorMappingMatrix(grade);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override ILinMatrixGradedStorage<T> GetMultivectorOmMappingMatrixStorage()
//        {
//            return OmStorage.GetMultivectorGradedMappingMatrix();
//        }

        
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override IEnumerable<KeyValuePair<IIndexSet, XGaMultivector<T>>> GetMappedBasisBlades()
//        {
//            return OmStorage
//                .GetMappedBasisBladesById()
//                .Select(r => 
//                    new KeyValuePair<IIndexSet, XGaMultivector<T>>(
//                        r.Id, 
//                        r.Storage.Multivector(GeometricProcessor)
//                    )
//                );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override IEnumerable<XGaIdVectorRecord<T>> GetOmMappedBasisVectors()
//        {
//            return OmStorage
//                .GetVectorMappingMatrix()
//                .GetColumns()
//                .Select(r => 
//                    new XGaIdVectorRecord<T>(
//                        r.Index, 
//                        r.Storage.Vector(GeometricProcessor)
//                    )
//            );
//        }
//    }
//}