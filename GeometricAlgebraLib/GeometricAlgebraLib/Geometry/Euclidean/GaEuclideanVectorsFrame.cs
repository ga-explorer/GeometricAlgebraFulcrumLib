using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.Permutations;
using GeometricAlgebraLib.Algebra.Signatures;
using GeometricAlgebraLib.Processing.Multivectors;
using GeometricAlgebraLib.Processing.Scalars;
using GeometricAlgebraLib.Storage;

namespace GeometricAlgebraLib.Geometry.Euclidean
{
    public sealed class GaEuclideanVectorsFrame<T> : 
        IReadOnlyList<IGaVectorStorage<T>>, 
        IGaEuclideanGeometry<T>
    {
        public static GaEuclideanVectorsFrame<T> CreateEmptyFrame(IGaScalarProcessor<T> scalarProcessor)
        {
            return new(scalarProcessor);
        }
        
        public static GaEuclideanVectorsFrame<T> Create(IGaScalarProcessor<T> scalarProcessor, params IGaVectorStorage<T>[] vectorsList)
        {
            return new(scalarProcessor, vectorsList);
        }
        
        public static GaEuclideanVectorsFrame<T> Create(IGaScalarProcessor<T> scalarProcessor, IEnumerable<IGaVectorStorage<T>> vectorsList)
        {
            return new(scalarProcessor, vectorsList);
        }

        public static GaEuclideanVectorsFrame<T> CreateBasisFrame(IGaScalarProcessor<T> scalarProcessor, int vSpaceDimension)
        {
            var vectorsList = Enumerable
                .Range(0, vSpaceDimension)
                .Select(
                    index => GaVectorTermStorage<T>.Create(
                        scalarProcessor, 
                        index, 
                        scalarProcessor.OneScalar
                    )
                );

            return new GaEuclideanVectorsFrame<T>(scalarProcessor, vectorsList);
        }


        private readonly List<IGaVectorStorage<T>> _vectorStoragesList;


        public int Count 
            => _vectorStoragesList.Count;

        public IGaScalarProcessor<T> ScalarProcessor { get; }

        public IGaVectorStorage<T> this[int index]
        {
            get => _vectorStoragesList[index];
            set => _vectorStoragesList[index] = 
                value 
                ?? throw new ArgumentNullException(nameof(value));
        }

        public bool IsValid
            => true;

        public bool IsInvalid
            => false;


        internal GaEuclideanVectorsFrame([NotNull] IGaScalarProcessor<T> scalarProcessor)
        {
            _vectorStoragesList = new List<IGaVectorStorage<T>>();

            ScalarProcessor = scalarProcessor;
        }

        internal GaEuclideanVectorsFrame([NotNull] IGaScalarProcessor<T> scalarProcessor, [NotNull] IEnumerable<IGaVectorStorage<T>> vectorStoragesList)
        {
            _vectorStoragesList = vectorStoragesList.ToList();

            ScalarProcessor = scalarProcessor;
        }

        
        public GaEuclideanVectorsFrame<T> AppendVector(IGaVectorStorage<T> vector)
        {
            _vectorStoragesList.Add(vector);

            return this;
        }
        
        public GaEuclideanVectorsFrame<T> AppendVectors(params IGaVectorStorage<T>[] vectorsList)
        {
            foreach (var vector in vectorsList)
                _vectorStoragesList.Add(vector);

            return this;
        }
        
        public GaEuclideanVectorsFrame<T> PrependVector(IGaVectorStorage<T> vector)
        {
            _vectorStoragesList.Insert(0, vector);

            return this;
        }
        
        public GaEuclideanVectorsFrame<T> InsertVector(int index, IGaVectorStorage<T> vector)
        {
            _vectorStoragesList.Insert(index, vector);

            return this;
        }

        public GaEuclideanVectorsFrame<T> GetSubFrame(int startIndex, int count)
        {
            return new(
                ScalarProcessor,
                _vectorStoragesList
                    .Skip(startIndex)
                    .Take(count)
            );
        }

        public GaEuclideanVectorsFrame<T> GetOrthogonalFrame(bool makeUnitVectors)
        {
            var vectorStoragesList = new List<IGaVectorStorage<T>>();

            var orthogonalVector = _vectorStoragesList[0];
            vectorStoragesList.Add(
                makeUnitVectors 
                    ? orthogonalVector.Divide(orthogonalVector.ENorm()).GetVectorPart()
                    : orthogonalVector
            );
            
            var mv1 = (IGaMultivectorStorage<T>)orthogonalVector;
            
            for (var i = 1; i < _vectorStoragesList.Count; i++)
            {
                var mv2 = mv1.Op(_vectorStoragesList[i]);
                
                orthogonalVector = 
                    mv1.GetReverse().EGp(mv2).GetVectorPart();
                
                vectorStoragesList.Add( 
                    makeUnitVectors
                        ? orthogonalVector.Divide(orthogonalVector.ENorm()).GetVectorPart()
                        : orthogonalVector
                    );
                
                mv1 = mv2;
            }

            return new GaEuclideanVectorsFrame<T>(ScalarProcessor, vectorStoragesList);
        }

        public GaEuclideanVectorsFrame<T> GetNegativeFrame()
        {
            return new(
                ScalarProcessor,
                _vectorStoragesList.Select(v => v.GetVectorPart(ScalarProcessor.Negative))
            );
        }

        public GaEuclideanVectorsFrame<T> GetSwappedPairsFrame()
        {
            var frame = new GaEuclideanVectorsFrame<T>(ScalarProcessor);

            //Swap each pair of two consecutive vectors in the frame
            for (var i = 0; i < _vectorStoragesList.Count - 1; i += 2)
            {
                frame.AppendVector(_vectorStoragesList[i + 1]);
                frame.AppendVector(_vectorStoragesList[i]);
            }

            if (_vectorStoragesList.Count % 2 == 1)
            {
                //To keep the same handedness we count the number of swaps and
                //negate the final vector if the number is odd

                var numberOfSwaps = (_vectorStoragesList.Count - 1) / 2;

                var lastVector = numberOfSwaps % 2 == 0
                    ? _vectorStoragesList[^1]
                    : _vectorStoragesList[^1].GetVectorPart(ScalarProcessor.Negative);

                frame.AppendVector(lastVector);
            }

            return frame;
        }

        public GaEuclideanVectorsFrame<T> GetRotatedFrame(IGaRotor<T> rotor)
        {
            return new(
                ScalarProcessor,
                _vectorStoragesList.Select(rotor.MapVector)
            );
        }

        public GaEuclideanSubspace<T> GetSubspace()
        {
            //TODO: Modify this to find the outer product of the basis from vectors _vectorStoragesList
            return GaEuclideanSubspace<T>.Create(
                ScalarProcessor.Op(_vectorStoragesList)
            );
        }

        public bool IsOrthonormal()
        {
            for (var i = 0; i < Count; i++)
            {
                var v1 = _vectorStoragesList[i];

                var dii = ScalarProcessor.Subtract(
                    v1.ESp(), 
                    ScalarProcessor.OneScalar
                );

                if (!ScalarProcessor.IsNearZero(dii))
                    return false;

                for (var j = i + 1; j < Count; j++)
                {
                    var dij = 
                        v1.ESp(_vectorStoragesList[j]);

                    if (!ScalarProcessor.IsNearZero(dij))
                        return false;
                }
            }

            return true;
        }

        public bool HasSameHandedness(GaEuclideanVectorsFrame<T> targetFrame)
        {
            var ps1 = GetSubspace();
            var ps2 = targetFrame.GetSubspace();
            var s = ps1.BladeStorage.Subtract(ps2.BladeStorage);

            //var s = GetPseudoScalar() - targetFrame.GetPseudoScalar();

            return s.IsZero();
        }

        public IEnumerable<GaEuclideanRotor<T>> GetRotorsToFrame(GaEuclideanVectorsFrame<T> targetFrame)
        {
            Debug.Assert(targetFrame.Count == Count);
            Debug.Assert(IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(HasSameHandedness(targetFrame));

            var inputFrame = new IGaVectorStorage<T>[Count];

            for (var i = 0; i < Count; i++)
                inputFrame[i] = _vectorStoragesList[i];
            
            for (var i = 0; i < Count - 1; i++)
            {
                var rotor = 
                    GaEuclideanRotor<T>.CreateSimpleRotor(inputFrame[i], targetFrame[i]);

                yield return rotor;

                for (var j = i + 1; j < Count; j++)
                    inputFrame[j] = rotor.MapVector(inputFrame[j]);
            }
        }

        public IEnumerable<GaEuclideanRotor<T>> GetRotorsToFrame(GaEuclideanVectorsFrame<T> targetFrame, params int[] basisRotationOrderList)
        {
            Debug.Assert(targetFrame.Count == Count);
            Debug.Assert(IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(HasSameHandedness(targetFrame));
            //Debug.Assert(GaPoTNumUtils.ValidateIndexPermutationList(basisRotationOrderList));

            var inputFrame = new IGaVectorStorage<T>[Count];

            for (var i = 0; i < Count; i++)
                inputFrame[i] = _vectorStoragesList[i];
            
            for (var i = 0; i < Count - 1; i++)
            {
                var vectorIndex = basisRotationOrderList[i];

                var rotor = 
                    GaEuclideanRotor<T>.CreateSimpleRotor(
                        inputFrame[vectorIndex], 
                        targetFrame[vectorIndex]
                    );

                yield return rotor;

                for (var j = i + 1; j < Count; j++)
                {
                    var vectorIndex1 = basisRotationOrderList[j];

                    inputFrame[vectorIndex1] = 
                        rotor.MapVector(inputFrame[vectorIndex1]);
                }
            }
        }

        public IEnumerable<T> GetAnglesToFrame(GaEuclideanVectorsFrame<T> targetFrame)
        {
            Debug.Assert(targetFrame.Count == Count);

            for (var i = 0; i < Count; i++)
            {
                var v1 = _vectorStoragesList[i];
                var v2 = targetFrame[i];

                yield return ScalarProcessor.ArcCos(
                    ScalarProcessor.Divide(
                        v1.ESp(v2),
                        ScalarProcessor.Sqrt(
                            ScalarProcessor.Times(v1.ESp(), v2.ESp())
                        )
                    )
                );
            }
        }

        public IEnumerable<GaEuclideanVectorsFrame<T>> GetFramePermutations()
        {
            var indexPermutationsList = 
                PermutationsUtils.GetIndexPermutations(Count);

            foreach (var indexPermutation in indexPermutationsList)
            {
                var frame = new GaEuclideanVectorsFrame<T>(ScalarProcessor);

                foreach (var index in indexPermutation)
                    frame.AppendVector(_vectorStoragesList[index]);

                yield return frame;
            }
        }

        public GaEuclideanVectorsFrame<T> GetProjectionOnFrame(GaEuclideanVectorsFrame<T> frame)
        {
            var ps = frame.GetSubspace();

            return new GaEuclideanVectorsFrame<T>(
                ScalarProcessor,
                _vectorStoragesList.Select(v => 
                    ps.Project(v).GetVectorPart()
                )
            );
        }

        public GaEuclideanVectorsFrame<T> Normalize()
        {
            return new(
                ScalarProcessor,
                _vectorStoragesList.Select(v => 
                    v.Divide(v.ENorm()).GetVectorPart()
                )
            );
        }

        public T[,] GetMatrix()
        {
            return GetMatrix(Count);
        }

        public T[,] GetMatrix(int rowsCount)
        {
            var colsCount = Count;
            var itemsArray = new T[rowsCount, colsCount];

            for (var j = 0; j < Count; j++)
            {
                var vector = _vectorStoragesList[j];

                foreach (var term in vector.GetTerms())
                {
                    var i = term.BasisBlade.Index;

                    itemsArray[i, j] = term.Scalar;
                }
            }
            
            return itemsArray;
        }

        public T[,] GetInnerProductsMatrix()
        {
            var ipm = new T[Count, Count];

            for (var i = 0; i < Count; i++)
            {
                var v1 = _vectorStoragesList[i];

                ipm[i, i] = v1.ESp(v1);

                for (var j = i + 1; j < Count; j++)
                {
                    var ip = v1.ESp(_vectorStoragesList[j]);

                    ipm[i, j] = ip;
                    ipm[j, i] = ip;
                }
            }

            return ipm;
        }

        public T[,] GetInnerAnglesMatrix()
        {
            var ipm = new T[Count, Count];

            for (var i = 0; i < Count; i++)
            {
                var v1 = _vectorStoragesList[i];

                for (var j = i + 1; j < Count; j++)
                {
                    var ip = v1.GetAngle(_vectorStoragesList[j]);

                    ipm[i, j] = ip;
                    ipm[j, i] = ip;
                }
            }

            return ipm;
        }

        public T[,] GetInnerAnglesInDegreesMatrix()
        {
            var ipm = new T[Count, Count];

            for (var i = 0; i < Count; i++)
            {
                var v1 = _vectorStoragesList[i];

                for (var j = i + 1; j < Count; j++)
                {
                    var ip = v1.GetAngle(_vectorStoragesList[j]);

                    ipm[i, j] = ip;
                    ipm[j, i] = ip;
                }
            }

            return ipm;
        }

        public IEnumerator<IGaVectorStorage<T>> GetEnumerator()
        {
            return _vectorStoragesList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}