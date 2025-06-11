//using System.Collections;
//using System.Diagnostics;
//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
//using GeometricAlgebraFulcrumLib.Algebra.BasicMath.Tuples;
//using MathNet.Numerics.LinearAlgebra;

//namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space3D
//{
//    public class LinFloat64UnilinearMap :
//        ILinFloat64UnilinearMap3D,
//        IReadOnlyDictionary<IndexPair, double>
//    {
//        
//        public static LinFloat64UnilinearMap operator -(LinFloat64UnilinearMap map1)
//        {
//            var indexVectorDictionary =
//                map1._indexVectorDictionary.ToDictionary(
//                    p => p.Key,
//                    p => p.Value.Negative()
//                );

//            return indexVectorDictionary.ToLinUnilinearMap();
//        }

//        
//        public static LinFloat64UnilinearMap operator +(LinFloat64UnilinearMap map1, LinFloat64UnilinearMap map2)
//        {
//            return new LinFloat64UnilinearMapComposer()
//                .SetMap(map1)
//                .AddMap(map2)
//                .GetMap();
//        }
        
//        
//        public static LinFloat64UnilinearMap operator -(LinFloat64UnilinearMap map1, LinFloat64UnilinearMap map2)
//        {
//            return new LinFloat64UnilinearMapComposer()
//                .SetMap(map1)
//                .SubtractMap(map2)
//                .GetMap();
//        }

//        
//        public static LinFloat64UnilinearMap operator *(double scalar1, LinFloat64UnilinearMap map2)
//        {
//            var indexVectorDictionary =
//                map2._indexVectorDictionary.ToDictionary(
//                    p => p.Key,
//                    p => p.Value.Times(scalar1)
//                );

//            return indexVectorDictionary.ToLinUnilinearMap();
//        }
        
//        
//        public static LinFloat64UnilinearMap operator *(LinFloat64UnilinearMap map1, double scalar2)
//        {
//            var indexVectorDictionary =
//                map1._indexVectorDictionary.ToDictionary(
//                    p => p.Key,
//                    p => p.Value.Times(scalar2)
//                );

//            return indexVectorDictionary.ToLinUnilinearMap();
//        }

//        
//        public static LinFloat64UnilinearMap operator *(LinFloat64UnilinearMap map1, LinFloat64UnilinearMap map2)
//        {
//            return map1.Map(map2);
//        }


//        private readonly IReadOnlyDictionary<int, Float64Tuple3D> _indexVectorDictionary;
    
//        public int Count 
//            => _indexVectorDictionary.Values.Sum(v => v.Count);

//        public IEnumerable<IndexPair> Keys
//        {
//            get
//            {
//                foreach (var (index2, vector) in _indexVectorDictionary)
//                foreach (var index1 in vector.Keys)
//                    yield return new IndexPair(index1, index2);
//            }
//        }

//        public IEnumerable<double> Values 
//            => _indexVectorDictionary
//                .Values
//                .SelectMany(v => v.Values);

//        public double this[IndexPair key] 
//            => _indexVectorDictionary.TryGetValue(key.Index2, out var vector) && 
//               vector.TryGetValue(key.Index1, out var scalar)
//                ? scalar
//                : 0d;

//        public Float64Tuple3D this[int key] 
//            => _indexVectorDictionary.TryGetValue(key, out var mv)
//                ? mv : Float64Tuple3D.VectorZero;

//        public int VSpaceDimensions 
//            => _indexVectorDictionary
//                .Values
//                .Max(v => v.VSpaceDimensions);

//        public bool SwapsHandedness { get; }

//        public bool IsIdentity()
//        {
//            throw new NotImplementedException();
//        }

//        public bool IsNearIdentity(double zeroEpsilon = 1E-12)
//        {
//            throw new NotImplementedException();
//        }

//        public ILinFloat64UnilinearMap3D GetInverseMap()
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<KeyValuePair<int, Float64Tuple3D>> IndexVectorPairs
//            => _indexVectorDictionary;

//        public IEnumerable<KeyValuePair<IndexPair, double>> IndexScalarPairs 
//        {
//            get
//            {
//                foreach (var (index2, vector) in _indexVectorDictionary)
//                {
//                    foreach (var (index1, scalar) in vector)
//                        yield return new KeyValuePair<IndexPair, double>(
//                            new IndexPair(index1, index2), 
//                            scalar
//                        );
//                }
//            }
//        }
        
//        public IEnumerable<KeyValuePair<IndexPair, double>> TransposedIndexScalarPairs 
//        {
//            get
//            {
//                foreach (var (index1, vector) in _indexVectorDictionary)
//                {
//                    foreach (var (index2, scalar) in vector)
//                        yield return new KeyValuePair<IndexPair, double>(
//                            new IndexPair(index1, index2), 
//                            scalar
//                        );
//                }
//            }
//        }


//        
//        internal LinFloat64UnilinearMap(IReadOnlyDictionary<int, Float64Tuple3D> indexVectorDictionary)
//        {
//            _indexVectorDictionary = indexVectorDictionary;

//            Debug.Assert(
//                IsValid()
//            );
//        }


//        
//        public bool IsValid()
//        {
//            return _indexVectorDictionary.Values.All(
//                d => d.IsValidLinVectorDictionary()
//            );
//        }
        
//        
//        public bool ContainsKey(IndexPair key)
//        {
//            return _indexVectorDictionary.TryGetValue(key.Index2, out var vector) &&
//                   vector.ContainsKey(key.Index1);
//        }

//        
//        public bool ContainsVector(int index)
//        {
//            return _indexVectorDictionary.ContainsKey(index);
//        }

//        
//        public bool TryGetValue(IndexPair key, out double value)
//        {
//            if (_indexVectorDictionary.TryGetValue(key.Index2, out var vector))
//            {
//                if (vector.TryGetValue(key.Index1, out value))
//                    return true;
//            }

//            value = 0d;
//            return false;
//        }

//        
//        public bool TryGetVector(int index, out Float64Tuple3D vector)
//        {
//            return _indexVectorDictionary.TryGetValue(index, out vector);
//        }

//        
//        public bool ContainsColumnVector(int index2)
//        {
//            return _indexVectorDictionary.ContainsKey(index2);
//        }

//        
//        public bool TryGetColumnVector(int index2, out Float64Tuple3D? vector)
//        {
//            return _indexVectorDictionary.TryGetValue(index2, out vector);
//        }

//        
//        public LinFloat64UnilinearMap GetAdjoint()
//        {
//            if (_indexVectorDictionary.Count == 0)
//                return this;

//            var indexVectorDictionary = new Dictionary<int, Float64Tuple3D>();

//            var group = 
//                TransposedIndexScalarPairs.GroupBy(
//                    p => p.Key.Item2
//                );

//            foreach (var g in group)
//            {
//                var index = g.Key;

//                var vector = g.ToDictionary(
//                    p => p.Key.Item1,
//                    p => p.Value
//                ).ToTuple3D();

//                indexVectorDictionary.Add(index, vector);
//            }

//            return indexVectorDictionary.ToLinUnilinearMap();
//        }

//        
//        public Float64Tuple3D MapBasisVector(int index)
//        {
//            return _indexVectorDictionary.TryGetValue(index, out var mv)
//                ? mv
//                : Float64Tuple3D.VectorZero;
//        }

//        public Float64Tuple3D MapVector(Float64Tuple3D vector)
//        {
//            var composer = new Float64Tuple3DComposer();

//            if (Count <= vector.Count)
//            {
//                foreach (var (index, mv) in _indexVectorDictionary)
//                {
//                    if (!vector.TryGetTermScalar(index, out var scalar))
//                        continue;

//                    composer.AddVector(mv, scalar);
//                }
//            }
//            else
//            {
//                foreach (var (index, scalar) in vector)
//                {
//                    if (!_indexVectorDictionary.TryGetValue(index, out var mv))
//                        continue;

//                    composer.AddVector(mv, scalar);
//                }
//            }

//            return composer.GetVector();
//        }

//        
//        public LinFloat64UnilinearMap Map(LinFloat64UnilinearMap map2)
//        {
//            var indexVectorDictionary =
//                map2
//                    .GetMappedBasisVectors()
//                    .Select(p => 
//                        new KeyValuePair<int, Float64Tuple3D>(p.Key, MapVector(p.Value))
//                    )
//                    .Where(p => !p.Value.IsZero)
//                    .ToDictionary(
//                        p => p.Key, 
//                        p => p.Value
//                    );

//            return indexVectorDictionary.ToLinUnilinearMap();
//        }
        
//        
//        public IEnumerable<KeyValuePair<int, Float64Tuple3D>> GetMappedBasisVectors()
//        {
//            return _indexVectorDictionary;
//        }

//        
//        public IEnumerable<KeyValuePair<int, Float64Tuple3D>> GetMappedBasisVectors(int vSpaceDimensions)
//        {
//            return _indexVectorDictionary
//                .Where(p => p.Key < vSpaceDimensions)
//                .Select(p => 
//                    new KeyValuePair<int, Float64Tuple3D>(p.Key, p.Value)
//                );
//        }
        
//        
//        public LinFloat64UnilinearMap MapScalars(Func<double, double> mappingFunc)
//        {
//            var indexVectorDictionary = new Dictionary<int, Float64Tuple3D>();

//            foreach (var (index, vector) in _indexVectorDictionary)
//            {
//                var vector1 = vector.MapScalars(mappingFunc);

//                if (vector1.IsZero)
//                    continue;

//                indexVectorDictionary.Add(index, vector1);
//            }

//            return indexVectorDictionary.ToLinUnilinearMap();
//        }

//        
//        public LinFloat64UnilinearMap MapScalars(Func<int, int, double, double> mappingFunc)
//        {
//            var indexVectorDictionary = new Dictionary<int, Float64Tuple3D>();

//            foreach (var (index, vector) in _indexVectorDictionary)
//            {
//                var vector1 = vector.MapScalars((i, s) => mappingFunc(i, index, s));

//                if (vector1.IsZero)
//                    continue;

//                indexVectorDictionary.Add(index, vector1);
//            }

//            return indexVectorDictionary.ToLinUnilinearMap();
//        }

//        public LinFloat64UnilinearMap GetSubMap(int vSpaceDimensions)
//        {
//            var indexVectorDictionary = new Dictionary<int, Float64Tuple3D>();

//            foreach (var (index, vector) in _indexVectorDictionary)
//            {
//                if (index >= vSpaceDimensions)
//                    continue;

//                var vector1 = vector.GetSubVector(vSpaceDimensions);

//                if (vector1.IsZero)
//                    continue;

//                indexVectorDictionary.Add(index, vector);
//            }

//            return indexVectorDictionary.ToLinUnilinearMap();
//        }

//        
//        public double[,] ToArray(int size)
//        {
//            return ToArray(size, size);
//        }

//        public double[,] ToArray(int rowCount, int colCount)
//        {
//            var mapArray = 
//                new double[rowCount, colCount];

//            if (_indexVectorDictionary.Count == 0)
//                return mapArray;

//            var minRowCount = 
//                _indexVectorDictionary.Values.Max(v => v.VSpaceDimensions);

//            if (rowCount < minRowCount)
//                throw new InvalidOperationException();

//            var minColCount = _indexVectorDictionary.Keys.Max();

//            if (colCount < minColCount)
//                throw new InvalidOperationException();

//            foreach (var (colIndex, vector) in _indexVectorDictionary)
//            foreach (var (rowIndex, scalar) in vector)
//                mapArray[rowIndex, colIndex] = scalar;

//            return mapArray;
//        }

//        public Matrix<double> ToMatrix(int rowCount, int colCount)
//        {
//            throw new NotImplementedException();
//        }

//        
//        public LinFloat64UnilinearMap ToUnilinearMap(int vSpaceDimensions)
//        {
//            return GetSubMap(vSpaceDimensions);
//        }

//        
//        public IEnumerator<KeyValuePair<IndexPair, double>> GetEnumerator()
//        {
//            foreach (var (index2, vector) in _indexVectorDictionary)
//            foreach (var (index1, scalar) in vector)
//                yield return new KeyValuePair<IndexPair, double>(
//                    new IndexPair(index1, index2), 
//                    scalar
//                );
//        }

//        
//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return GetEnumerator();
//        }
//    }
//}