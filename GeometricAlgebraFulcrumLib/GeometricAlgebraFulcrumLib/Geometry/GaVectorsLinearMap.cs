namespace GeometricAlgebraFulcrumLib.Geometry
{
    //public sealed class IGaOutermorphism<T> : 
    //    IGaOutermorphism<T>
    //{
    //    public static IGaOutermorphism<T> Create(IGaProcessor<T> processor, int basisVectorsCount, Func<IGasVector<T>, IGasVector<T>> basisVectorMappingFunc)
    //    {
    //        var basisVectorImagesDictionary = 
    //            new Dictionary<ulong, IGasVector<T>>();

    //        for (var index = 0; index < basisVectorsCount; index++)
    //        {
    //            var basisVector = processor.CreateBasisVector(index
    //            );

    //            var mappedBasisVector = 
    //                basisVectorMappingFunc(basisVector);

    //            basisVectorImagesDictionary.Add(
    //                (ulong) index, 
    //                mappedBasisVector
    //            );
    //        }

    //        return new IGaOutermorphism<T>(
    //            processor,
    //            basisVectorImagesDictionary
    //        );
    //    }

    //    public static IGaOutermorphism<T> Create(IGaProcessor<T> processor, Dictionary<ulong, IGasVector<T>> basisVectorImagesDictionary)
    //    {
    //        return new IGaOutermorphism<T>(
    //            processor,
    //            basisVectorImagesDictionary
    //        );
    //    }

    //    public static IGaOutermorphism<T> Create(IGaProcessor<T> processor, T[,] matrix)
    //    {
    //        var rowsCount = matrix.GetLength(0);
    //        var colsCount = matrix.GetLength(1);

    //        var basisVectorImagesDictionary = 
    //            new Dictionary<ulong, IGasVector<T>>();

    //        for (var j = 0; j < colsCount; j++)
    //        {
    //            var composer = new GaKVectorStorageComposer<T>(processor, 1);

    //            for (var i = 0; i < rowsCount; i++)
    //                composer.AddTerm(
    //                    (ulong) i, 
    //                    matrix[i, j] ?? processor.ZeroScalar
    //                );

    //            basisVectorImagesDictionary.Add(
    //                (ulong)j, 
    //                composer.GetVectorStorage()
    //            );
    //        }

    //        return new IGaOutermorphism<T>(
    //            processor,
    //            basisVectorImagesDictionary
    //        );
    //    }
        
    //    public static IGaOutermorphism<T> Create(IGaProcessor<T> processor, IGaMatrix<T> matrix)
    //    {
    //        var rowsCount = matrix.RowsCount;
    //        var colsCount = matrix.ColumnsCount;

    //        var basisVectorImagesDictionary = 
    //            new Dictionary<ulong, IGasVector<T>>();

    //        for (var j = 0; j < colsCount; j++)
    //        {
    //            var composer = new GaKVectorStorageComposer<T>(processor, 1);

    //            for (var i = 0; i < rowsCount; i++)
    //                composer.AddTerm(
    //                    (ulong) i, 
    //                    matrix[i, j] ?? processor.ZeroScalar
    //                );

    //            basisVectorImagesDictionary.Add(
    //                (ulong)j, 
    //                composer.GetVectorStorage()
    //            );
    //        }

    //        return new IGaOutermorphism<T>(
    //            processor,
    //            basisVectorImagesDictionary
    //        );
    //    }


    //    private readonly Dictionary<ulong, IGasVector<T>> _basisVectorImagesDictionary;


    //    public uint VSpaceDimension 
    //        => Processor.VSpaceDimension;

    //    public ulong GaSpaceDimension
    //        => Processor.GaSpaceDimension;

    //    public ulong MaxBasisBladeId { get; }

    //    public uint GradesCount { get; }

    //    public IEnumerable<uint> Grades { get; }

    //    public IGaScalarProcessor<T> ScalarProcessor { get; }

    //    public IGasKVector<T> MappedPseudoScalar { get; }

    //    public IGaProcessor<T> Processor { get; }

    //    public bool IsValid
    //        => true;

    //    public bool IsInvalid
    //        => false;


    //    private IGaOutermorphism([NotNull] IGaProcessor<T> processor, [NotNull] Dictionary<ulong, IGasVector<T>> basisVectorImagesDictionary)
    //    {
    //        Processor = processor;
    //        _basisVectorImagesDictionary = basisVectorImagesDictionary;
    //    }


    //    public bool ContainsBasisVectorImage(ulong index)
    //    {
    //        return _basisVectorImagesDictionary.ContainsKey(index);
    //    }

    //    public bool TryGetBasisVectorImage(ulong index, out IGasVector<T> vector)
    //    {
    //        return _basisVectorImagesDictionary.TryGetValue(index, out vector);
    //    }
        
    //    private bool TryGetBasisBladeImage(IReadOnlyList<int> basisVectorIndices, out IGasKVector<T> kVector)
    //    {
    //        Debug.Assert(basisVectorIndices.Count > 0);

    //        var grade = basisVectorIndices.Count;

    //        var basisVectorImages = 
    //            new IGasVector<T>[grade];

    //        for (var i = 0; i < grade; i++)
    //        {
    //            var index = basisVectorIndices[i];

    //            if (!_basisVectorImagesDictionary.TryGetValue((ulong)index, out var vector))
    //            {
    //                kVector = null;
    //                return false;
    //            }

    //            basisVectorImages[index] = vector;
    //        }

    //        kVector = Processor.Op(basisVectorImages);
    //        return true;
    //    }

    //    public bool TryGetBasisBladeImage(ulong id, out IGasKVector<T> kVector)
    //    {
    //        if (id != 0) 
    //            return TryGetBasisBladeImage(
    //                id.BasisVectorIndexesInside().Select(i => (int)i).ToArray(), 
    //                out kVector
    //            );

    //        kVector = Processor.CreateBasisScalar();
    //        return true;
    //    }

    //    public bool TryGetBasisBladeImage(uint grade, ulong index, out IGasKVector<T> kVector)
    //    {
    //        if (grade > 0) 
    //            return TryGetBasisBladeImage(
    //                index.IndexToCombinadic((int) grade).ToArray(), 
    //                out kVector
    //            );
            
    //        kVector = Processor.CreateBasisScalar();
    //        return true;
    //    }
        
    //    public T[,] GetArray(int rowsCount, int colsCount)
    //    {
    //        var array = new T[rowsCount, colsCount];

    //        for (var j = 0; j < colsCount; j++)
    //        {
    //            if (_basisVectorImagesDictionary.TryGetValue((ulong) j, out var basisVectorImage))
    //            {
    //                for (var i = 0; i < rowsCount; i++)
    //                {
    //                    array[i, j] = basisVectorImage.TryGetTermScalarByIndex((ulong) i, out var scalar)
    //                        ? scalar
    //                        : Processor.ZeroScalar;
    //                }

    //                continue;
    //            }

    //            for (var i = 0; i < rowsCount; i++)
    //                array[i, j] = Processor.ZeroScalar;
    //        }

    //        return array;
    //    }

    //    public IGaOutermorphism<T> GetAdjoint()
    //    {
    //        var basisVectorImagesDictionary =
    //            new Dictionary<ulong, Dictionary<ulong, T>>();

    //        foreach (var (colIndex, vectorStorage) in _basisVectorImagesDictionary)
    //        {
    //            foreach (var (rowIndex, scalar) in vectorStorage.GetIndexScalarPairs())
    //            {
    //                if (!basisVectorImagesDictionary.TryGetValue(rowIndex, out var storageDictionary))
    //                {
    //                    storageDictionary = new Dictionary<ulong, T>();

    //                    basisVectorImagesDictionary.Add(rowIndex, storageDictionary);
    //                }

    //                storageDictionary.Add(colIndex, scalar);
    //            }
    //        }

    //        return new IGaOutermorphism<T>(
    //            Processor,
    //            basisVectorImagesDictionary.CopyToDictionary(
    //                storageDictionary => (IGasVector<T>)Processor.CreateVector(storageDictionary)
    //            )
    //        );
    //    }

    //    public IGasVector<T> MapBasisVector(int index)
    //    {
    //        return _basisVectorImagesDictionary.TryGetValue((ulong)index, out var basisVectorImage)
    //            ? basisVectorImage
    //            : Processor.CreateZeroVector();
    //    }

    //    public IReadOnlyList<IGasVector<T>> GetMappedBasisVectors()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IGasVector<T> MapBasisVector(ulong index)
    //    {
    //        return _basisVectorImagesDictionary.TryGetValue(index, out var basisVectorImage)
    //            ? basisVectorImage
    //            : Processor.CreateZeroVector();
    //    }

    //    public IGasBivector<T> MapBasisBivector(int index1, int index2)
    //    {
    //        if (index1 == index2)
    //            throw new InvalidOperationException();

    //        if (index1 < index2)
    //        {
    //            return _basisVectorImagesDictionary.TryGetValue((ulong)index1, out var basisVectorImage1) &&
    //                   _basisVectorImagesDictionary.TryGetValue((ulong)index2, out var basisVectorImage2)
    //                ? basisVectorImage1.Op(basisVectorImage2).GetBivectorPart()
    //                : Processor.CreateZeroBivector();
    //        }
    //        else
    //        {
    //            return _basisVectorImagesDictionary.TryGetValue((ulong) index2, out var basisVectorImage1) &&
    //                   _basisVectorImagesDictionary.TryGetValue((ulong) index1, out var basisVectorImage2)
    //                ? basisVectorImage1.Op(basisVectorImage2).GetBivectorPart()
    //                : Processor.CreateZeroBivector();
    //        }
    //    }

    //    public IGasBivector<T> MapBasisBivector(ulong index1, ulong index2)
    //    {
    //        if (index1 == index2)
    //            throw new InvalidOperationException();

    //        if (index1 < index2)
    //        {
    //            return _basisVectorImagesDictionary.TryGetValue(index1, out var basisVectorImage1) &&
    //                   _basisVectorImagesDictionary.TryGetValue(index2, out var basisVectorImage2)
    //                ? basisVectorImage1.Op(basisVectorImage2).GetBivectorPart()
    //                : Processor.CreateZeroBivector();
    //        }
    //        else
    //        {
    //            return _basisVectorImagesDictionary.TryGetValue(index2, out var basisVectorImage1) &&
    //                   _basisVectorImagesDictionary.TryGetValue(index1, out var basisVectorImage2)
    //                ? basisVectorImage1.Op(basisVectorImage2).GetBivectorPart()
    //                : Processor.CreateZeroBivector();
    //        }
    //    }

    //    public IGasKVector<T> MapBasisBlade(ulong id)
    //    {
    //        return TryGetBasisBladeImage(id, out var basisBladeImage)
    //            ? basisBladeImage
    //            : Processor.CreateZeroKVector(id.BasisBladeGrade());
    //    }

    //    public IGasKVector<T> MapBasisBlade(uint grade, ulong index)
    //    {
    //        return TryGetBasisBladeImage(grade, index, out var basisBladeImage)
    //            ? basisBladeImage
    //            : Processor.CreateZeroKVector(grade);
    //    }


    //    public IGasScalar<T> MapScalar(IGasScalar<T> storage)
    //    {
    //        return storage;
    //    }

    //    public IGasKVector<T> MapTerm(IGasKVectorTerm<T> storage)
    //    {
    //        if (!TryGetBasisBladeImage(storage.Id, out var basisBladeImage))
    //            return Processor.CreateZeroKVector(storage.Grade);

    //        var composer = new GaKVectorStorageComposer<T>(Processor, storage.Grade);

    //        composer.AddLeftScaledTerms(
    //            storage.Scalar, 
    //            basisBladeImage.GetIndexScalarPairs()
    //        );

    //        return composer.GetKVectorStorage();
    //    }

    //    public IGasVector<T> MapVector(IGasVector<T> storage)
    //    {
    //        var composer = new GaKVectorStorageComposer<T>(Processor, 1);

    //        foreach (var (index, scalar) in storage.GetIndexScalarPairs())
    //        {
    //            if (!_basisVectorImagesDictionary.TryGetValue(index, out var basisVectorImage))
    //                continue;

    //            composer.AddLeftScaledTerms(
    //                scalar,
    //                basisVectorImage.GetIndexScalarPairs()
    //            );
    //        }

    //        return composer.GetVectorStorage();
    //    }

    //    public IGasBivector<T> MapBivector(IGasBivector<T> storage)
    //    {
    //        var composer = new GaBivectorStorageComposer<T>(Processor);

    //        foreach (var (index, scalar) in storage.GetIndexScalarPairs())
    //        {
    //            var basisVectorIndices = 
    //                index.IndexToCombinadic(2).ToArray();

    //            if (!TryGetBasisVectorImage((ulong)basisVectorIndices[0], out var basisVectorImage1))
    //                continue;

    //            if (!TryGetBasisVectorImage((ulong)basisVectorIndices[1], out var basisVectorImage2))
    //                continue;

    //            var basisBladeImage = 
    //                basisVectorImage1.Op(basisVectorImage2);

    //            composer.AddLeftScaledTerms(
    //                scalar,
    //                basisBladeImage.GetIndexScalarPairs()
    //            );
    //        }

    //        return composer.GetBivectorStorage();
    //    }
        
    //    public IGasKVector<T> MapKVector(IGasKVector<T> storage)
    //    {
    //        if (storage.Grade == 0)
    //            return Processor.CreateScalar(
    //                storage.GetTermScalarByIndex(0)
    //            );

    //        var composer = new GaKVectorStorageComposer<T>(Processor, storage.Grade);

    //        foreach (var (index, scalar) in storage.GetIndexScalarPairs())
    //        {
    //            var basisVectorIndices = 
    //                index.IndexToCombinadic((int) storage.Grade).ToArray();

    //            if (!TryGetBasisBladeImage(basisVectorIndices, out var basisBladeImage))
    //                continue;

    //            composer.AddLeftScaledTerms(
    //                scalar,
    //                basisBladeImage.GetIndexScalarPairs()
    //            );
    //        }

    //        return composer.GetKVectorStorage();
    //    }

    //    public IGasMultivector<T> MapMultivector(IGasGradedMultivector<T> storage)
    //    {
    //        var composer = new GaMultivectorGradedStorageComposer<T>(Processor);

    //        foreach (var (grade, indexScalarDictionary) in storage.GetGradeIndexScalarDictionary())
    //        {
    //            if (grade == 0)
    //                return Processor.CreateScalar(
    //                    indexScalarDictionary.TryGetValue(0, out var scalar)
    //                        ? scalar : Processor.ZeroScalar
    //                );

    //            foreach (var (index, scalar) in indexScalarDictionary)
    //            {
    //                var basisVectorIndices =
    //                    index.IndexToCombinadic((int) grade).ToArray();

    //                if (!TryGetBasisBladeImage(basisVectorIndices, out var basisBladeImage))
    //                    continue;

    //                composer.AddLeftScaledKVector(
    //                    scalar,
    //                    grade,
    //                    basisBladeImage.GetIndexScalarPairs()
    //                );
    //            }
    //        }

    //        return composer.GetCompactGradedStorage();
    //    }
        
    //    public IGasMultivector<T> MapMultivector(IGasTermsMultivector<T> storage)
    //    {
    //        var composer = new GaMultivectorTermsStorageComposer<T>(Processor);

    //        foreach (var (id, scalar) in storage.GetIdScalarPairs())
    //        {
    //            if (id == 0)
    //                return Processor.CreateScalar(scalar);

    //            if (!TryGetBasisBladeImage(id, out var basisBladeImage))
    //                continue;

    //            composer.AddLeftScaledTerms(
    //                scalar,
    //                basisBladeImage.GetIdScalarPairs()
    //            );
    //        }

    //        return composer.GetMultivectorStorage();
    //    }

    //    public IGasMultivector<T> MapMultivector(IGasMultivector<T> storage)
    //    {
    //        return storage switch
    //        {
    //            IGasScalar<T> scalarStorage => MapScalar(scalarStorage),
    //            IGasKVectorTerm<T> termStorage => MapTerm(termStorage),
    //            IGasVector<T> vectorStorage => MapVector(vectorStorage),
    //            IGasBivector<T> bivectorStorage => MapBivector(bivectorStorage),
    //            IGasKVector<T> kVectorStorage => MapKVector(kVectorStorage),
    //            //IGaMultivectorGradedStorage<T> gradedMultivectorStorage => MapMultivector(gradedMultivectorStorage),
    //            _ => MapMultivector((IGasTermsMultivector<T>)storage)
    //        };
    //    }
    //}
}