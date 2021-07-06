using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.Combinations;
using DataStructuresLib.Extensions;
using GeometricAlgebraLib.Algebra.Basis;
using GeometricAlgebraLib.Processing.Matrices;
using GeometricAlgebraLib.Processing.Multivectors;
using GeometricAlgebraLib.Processing.Scalars;
using GeometricAlgebraLib.Storage;
using GeometricAlgebraLib.Storage.Composers;

namespace GeometricAlgebraLib.Geometry
{
    public sealed class GaVectorsLinearMap<T> : IGaVectorsLinearMap<T>
    {
        public static GaVectorsLinearMap<T> Create(IGaScalarProcessor<T> scalarProcessor, int basisVectorsCount, Func<IGaVectorStorage<T>, IGaVectorStorage<T>> basisVectorMappingFunc)
        {
            var basisVectorImagesDictionary = 
                new Dictionary<ulong, IGaVectorStorage<T>>();

            for (var index = 0; index < basisVectorsCount; index++)
            {
                var basisVector = GaVectorTermStorage<T>.CreateBasisVector(
                    scalarProcessor, 
                    index
                );

                var mappedBasisVector = 
                    basisVectorMappingFunc(basisVector);

                basisVectorImagesDictionary.Add(
                    (ulong) index, 
                    mappedBasisVector
                );
            }

            return new GaVectorsLinearMap<T>(
                scalarProcessor,
                basisVectorImagesDictionary
            );
        }

        public static GaVectorsLinearMap<T> Create(IGaScalarProcessor<T> scalarProcessor, Dictionary<ulong, IGaVectorStorage<T>> basisVectorImagesDictionary)
        {
            return new GaVectorsLinearMap<T>(
                scalarProcessor,
                basisVectorImagesDictionary
            );
        }

        public static GaVectorsLinearMap<T> Create(IGaScalarProcessor<T> scalarProcessor, T[,] matrix)
        {
            var rowsCount = matrix.GetLength(0);
            var colsCount = matrix.GetLength(1);

            var basisVectorImagesDictionary = 
                new Dictionary<ulong, IGaVectorStorage<T>>();

            for (var j = 0; j < colsCount; j++)
            {
                var composer = new GaKVectorStorageComposer<T>(scalarProcessor, 1);

                for (var i = 0; i < rowsCount; i++)
                    composer.AddTerm(
                        (ulong) i, 
                        matrix[i, j] ?? scalarProcessor.ZeroScalar
                    );

                basisVectorImagesDictionary.Add(
                    (ulong)j, 
                    composer.GetVectorStorage()
                );
            }

            return new GaVectorsLinearMap<T>(
                scalarProcessor,
                basisVectorImagesDictionary
            );
        }
        
        public static GaVectorsLinearMap<T> Create(IGaScalarProcessor<T> scalarProcessor, IGaMatrix<T> matrix)
        {
            var rowsCount = matrix.RowsCount;
            var colsCount = matrix.ColumnsCount;

            var basisVectorImagesDictionary = 
                new Dictionary<ulong, IGaVectorStorage<T>>();

            for (var j = 0; j < colsCount; j++)
            {
                var composer = new GaKVectorStorageComposer<T>(scalarProcessor, 1);

                for (var i = 0; i < rowsCount; i++)
                    composer.AddTerm(
                        (ulong) i, 
                        matrix[i, j] ?? scalarProcessor.ZeroScalar
                    );

                basisVectorImagesDictionary.Add(
                    (ulong)j, 
                    composer.GetVectorStorage()
                );
            }

            return new GaVectorsLinearMap<T>(
                scalarProcessor,
                basisVectorImagesDictionary
            );
        }


        private readonly Dictionary<ulong, IGaVectorStorage<T>> _basisVectorImagesDictionary;


        public IGaScalarProcessor<T> ScalarProcessor { get; }

        public bool IsValid
            => true;

        public bool IsInvalid
            => false;


        private GaVectorsLinearMap([NotNull] IGaScalarProcessor<T> scalarProcessor, [NotNull] Dictionary<ulong, IGaVectorStorage<T>> basisVectorImagesDictionary)
        {
            ScalarProcessor = scalarProcessor;
            _basisVectorImagesDictionary = basisVectorImagesDictionary;
        }


        public bool ContainsBasisVectorImage(ulong index)
        {
            return _basisVectorImagesDictionary.ContainsKey(index);
        }

        public bool TryGetBasisVectorImage(ulong index, out IGaVectorStorage<T> vector)
        {
            return _basisVectorImagesDictionary.TryGetValue(index, out vector);
        }
        
        private bool TryGetBasisBladeImage(IReadOnlyList<int> basisVectorIndices, out IGaKVectorStorage<T> kVector)
        {
            Debug.Assert(basisVectorIndices.Count > 0);

            var grade = basisVectorIndices.Count;

            var basisVectorImages = 
                new IGaVectorStorage<T>[grade];

            for (var i = 0; i < grade; i++)
            {
                var index = basisVectorIndices[i];

                if (!_basisVectorImagesDictionary.TryGetValue((ulong)index, out var vector))
                {
                    kVector = null;
                    return false;
                }

                basisVectorImages[index] = vector;
            }

            kVector = ScalarProcessor.Op(basisVectorImages);
            return true;
        }

        public bool TryGetBasisBladeImage(ulong id, out IGaKVectorStorage<T> kVector)
        {
            if (id != 0) 
                return TryGetBasisBladeImage(
                    id.BasisVectorIndexesInside().Select(i => (int)i).ToArray(), 
                    out kVector
                );

            kVector = GaScalarTermStorage<T>.CreateBasisScalar(ScalarProcessor);
            return true;
        }

        public bool TryGetBasisBladeImage(int grade, ulong index, out IGaKVectorStorage<T> kVector)
        {
            if (grade > 0) 
                return TryGetBasisBladeImage(
                    index.IndexToCombinadic(grade).ToArray(), 
                    out kVector
                );
            
            kVector = GaScalarTermStorage<T>.CreateBasisScalar(ScalarProcessor);
            return true;
        }
        
        public T[,] GetArray(int rowsCount, int colsCount)
        {
            var array = new T[rowsCount, colsCount];

            for (var j = 0; j < colsCount; j++)
            {
                if (_basisVectorImagesDictionary.TryGetValue((ulong) j, out var basisVectorImage))
                {
                    for (var i = 0; i < rowsCount; i++)
                    {
                        array[i, j] = basisVectorImage.TryGetTermScalarByIndex((ulong) i, out var scalar)
                            ? scalar
                            : ScalarProcessor.ZeroScalar;
                    }

                    continue;
                }

                for (var i = 0; i < rowsCount; i++)
                    array[i, j] = ScalarProcessor.ZeroScalar;
            }

            return array;
        }

        public IGaVectorsLinearMap<T> GetAdjoint()
        {
            var basisVectorImagesDictionary =
                new Dictionary<ulong, Dictionary<ulong, T>>();

            foreach (var (colIndex, vectorStorage) in _basisVectorImagesDictionary)
            {
                foreach (var (rowIndex, scalar) in vectorStorage.GetIndexScalarPairs())
                {
                    if (!basisVectorImagesDictionary.TryGetValue(rowIndex, out var storageDictionary))
                    {
                        storageDictionary = new Dictionary<ulong, T>();

                        basisVectorImagesDictionary.Add(rowIndex, storageDictionary);
                    }

                    storageDictionary.Add(colIndex, scalar);
                }
            }

            return new GaVectorsLinearMap<T>(
                ScalarProcessor,
                basisVectorImagesDictionary.CopyToDictionary(
                    storageDictionary => (IGaVectorStorage<T>)GaVectorStorage<T>.Create(ScalarProcessor, storageDictionary)
                )
            );
        }

        public IGaVectorStorage<T> MapBasisVector(int index)
        {
            return _basisVectorImagesDictionary.TryGetValue((ulong)index, out var basisVectorImage)
                ? basisVectorImage
                : GaVectorTermStorage<T>.CreateZero(ScalarProcessor);
        }

        public IGaVectorStorage<T> MapBasisVector(ulong index)
        {
            return _basisVectorImagesDictionary.TryGetValue(index, out var basisVectorImage)
                ? basisVectorImage
                : GaVectorTermStorage<T>.CreateZero(ScalarProcessor);
        }

        public IGaBivectorStorage<T> MapBasisBivector(int index1, int index2)
        {
            if (index1 == index2)
                throw new InvalidOperationException();

            if (index1 < index2)
            {
                return _basisVectorImagesDictionary.TryGetValue((ulong)index1, out var basisVectorImage1) &&
                       _basisVectorImagesDictionary.TryGetValue((ulong)index2, out var basisVectorImage2)
                    ? basisVectorImage1.Op(basisVectorImage2).GetBivectorPart()
                    : GaBivectorTermStorage<T>.CreateZero(ScalarProcessor);
            }
            else
            {
                return _basisVectorImagesDictionary.TryGetValue((ulong) index2, out var basisVectorImage1) &&
                       _basisVectorImagesDictionary.TryGetValue((ulong) index1, out var basisVectorImage2)
                    ? basisVectorImage1.Op(basisVectorImage2).GetBivectorPart()
                    : GaBivectorTermStorage<T>.CreateZero(ScalarProcessor);
            }
        }

        public IGaBivectorStorage<T> MapBasisBivector(ulong index1, ulong index2)
        {
            if (index1 == index2)
                throw new InvalidOperationException();

            if (index1 < index2)
            {
                return _basisVectorImagesDictionary.TryGetValue(index1, out var basisVectorImage1) &&
                       _basisVectorImagesDictionary.TryGetValue(index2, out var basisVectorImage2)
                    ? basisVectorImage1.Op(basisVectorImage2).GetBivectorPart()
                    : GaBivectorTermStorage<T>.CreateZero(ScalarProcessor);
            }
            else
            {
                return _basisVectorImagesDictionary.TryGetValue(index2, out var basisVectorImage1) &&
                       _basisVectorImagesDictionary.TryGetValue(index1, out var basisVectorImage2)
                    ? basisVectorImage1.Op(basisVectorImage2).GetBivectorPart()
                    : GaBivectorTermStorage<T>.CreateZero(ScalarProcessor);
            }
        }

        public IGaKVectorStorage<T> MapBasisBlade(ulong id)
        {
            return TryGetBasisBladeImage(id, out var basisBladeImage)
                ? basisBladeImage
                : GaKVectorTermStorage<T>.CreateZero(ScalarProcessor, id.BasisBladeGrade());
        }

        public IGaKVectorStorage<T> MapBasisBlade(int grade, ulong index)
        {
            return TryGetBasisBladeImage(grade, index, out var basisBladeImage)
                ? basisBladeImage
                : GaKVectorTermStorage<T>.CreateZero(ScalarProcessor, grade);
        }


        public IGaScalarStorage<T> MapScalar(IGaScalarStorage<T> storage)
        {
            return GaScalarTermStorage<T>.Create(ScalarProcessor, storage.Scalar);
        }

        public IGaKVectorStorage<T> MapTerm(IGaKVectorTermStorage<T> storage)
        {
            if (!TryGetBasisBladeImage(storage.Id, out var basisBladeImage))
                return GaKVectorTermStorage<T>.CreateZero(ScalarProcessor, storage.Grade);

            var composer = new GaKVectorStorageComposer<T>(ScalarProcessor, storage.Grade);

            composer.AddLeftScaledTerms(
                storage.Scalar, 
                basisBladeImage.GetIndexScalarPairs()
            );

            return composer.GetKVectorStorage();
        }

        public IGaVectorStorage<T> MapVector(IGaVectorStorage<T> storage)
        {
            var composer = new GaKVectorStorageComposer<T>(ScalarProcessor, 1);

            foreach (var (index, scalar) in storage.GetIndexScalarPairs())
            {
                if (!_basisVectorImagesDictionary.TryGetValue(index, out var basisVectorImage))
                    continue;

                composer.AddLeftScaledTerms(
                    scalar,
                    basisVectorImage.GetIndexScalarPairs()
                );
            }

            return composer.GetVectorStorage();
        }

        public IGaBivectorStorage<T> MapBivector(IGaBivectorStorage<T> storage)
        {
            var composer = new GaBivectorStorageComposer<T>(ScalarProcessor);

            foreach (var (index, scalar) in storage.GetIndexScalarPairs())
            {
                var basisVectorIndices = 
                    index.IndexToCombinadic(2).ToArray();

                if (!TryGetBasisVectorImage((ulong)basisVectorIndices[0], out var basisVectorImage1))
                    continue;

                if (!TryGetBasisVectorImage((ulong)basisVectorIndices[1], out var basisVectorImage2))
                    continue;

                var basisBladeImage = 
                    basisVectorImage1.Op(basisVectorImage2);

                composer.AddLeftScaledTerms(
                    scalar,
                    basisBladeImage.GetIndexScalarPairs()
                );
            }

            return composer.GetBivectorStorage();
        }
        
        public IGaKVectorStorage<T> MapKVector(IGaKVectorStorage<T> storage)
        {
            if (storage.Grade == 0)
                return GaScalarTermStorage<T>.Create(
                    ScalarProcessor, 
                    storage.GetTermScalarByIndex(0)
                );

            var composer = new GaKVectorStorageComposer<T>(ScalarProcessor, storage.Grade);

            foreach (var (index, scalar) in storage.GetIndexScalarPairs())
            {
                var basisVectorIndices = 
                    index.IndexToCombinadic(storage.Grade).ToArray();

                if (!TryGetBasisBladeImage(basisVectorIndices, out var basisBladeImage))
                    continue;

                composer.AddLeftScaledTerms(
                    scalar,
                    basisBladeImage.GetIndexScalarPairs()
                );
            }

            return composer.GetKVectorStorage();
        }

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorGradedStorage<T> storage)
        {
            var composer = new GaMultivectorGradedStorageComposer<T>(ScalarProcessor);

            foreach (var (grade, indexScalarDictionary) in storage.GetGradeIndexScalarDictionary())
            {
                if (grade == 0)
                    return GaScalarTermStorage<T>.Create(
                        ScalarProcessor,
                        indexScalarDictionary.TryGetValue(0, out var scalar)
                            ? scalar : ScalarProcessor.ZeroScalar
                    );

                foreach (var (index, scalar) in indexScalarDictionary)
                {
                    var basisVectorIndices =
                        index.IndexToCombinadic(grade).ToArray();

                    if (!TryGetBasisBladeImage(basisVectorIndices, out var basisBladeImage))
                        continue;

                    composer.AddLeftScaledKVector(
                        scalar,
                        grade,
                        basisBladeImage.GetIndexScalarPairs()
                    );
                }
            }

            return composer.GetCompactGradedStorage();
        }
        
        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorTermsStorage<T> storage)
        {
            var composer = new GaMultivectorTermsStorageComposer<T>(ScalarProcessor);

            foreach (var (id, scalar) in storage.GetIdScalarPairs())
            {
                if (id == 0)
                    return GaScalarTermStorage<T>.Create(
                        ScalarProcessor,
                        scalar
                    );

                if (!TryGetBasisBladeImage(id, out var basisBladeImage))
                    continue;

                composer.AddLeftScaledTerms(
                    scalar,
                    basisBladeImage.GetIdScalarPairs()
                );
            }

            return composer.GetMultivectorStorage();
        }

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorStorage<T> storage)
        {
            return storage switch
            {
                IGaScalarStorage<T> scalarStorage => MapScalar(scalarStorage),
                IGaKVectorTermStorage<T> termStorage => MapTerm(termStorage),
                IGaVectorStorage<T> vectorStorage => MapVector(vectorStorage),
                IGaBivectorStorage<T> bivectorStorage => MapBivector(bivectorStorage),
                IGaKVectorStorage<T> kVectorStorage => MapKVector(kVectorStorage),
                //IGaMultivectorGradedStorage<T> gradedMultivectorStorage => MapMultivector(gradedMultivectorStorage),
                _ => MapMultivector((IGaMultivectorTermsStorage<T>)storage)
            };
        }
    }
}