using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Permutations;
using GeometricAlgebraFulcrumLib.Geometry.Euclidean;
using GeometricAlgebraFulcrumLib.Processing;
using GeometricAlgebraFulcrumLib.Processing.Products;
using GeometricAlgebraFulcrumLib.Processing.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Geometry
{
    public sealed class GaVectorsFrame<T> : 
        IReadOnlyList<IGasVector<T>>, 
        IGaEuclideanGeometry<T>
    {
        public static GaVectorsFrame<T> CreateEmptyFrame(IGaProcessor<T> processor)
        {
            return new GaVectorsFrame<T>(processor);
        }
        
        public static GaVectorsFrame<T> Create(IGaProcessor<T> processor, params IGasVector<T>[] vectorsList)
        {
            return new GaVectorsFrame<T>(processor, vectorsList);
        }
        
        public static GaVectorsFrame<T> Create(IGaProcessor<T> processor, IEnumerable<IGasVector<T>> vectorsList)
        {
            return new GaVectorsFrame<T>(processor, vectorsList);
        }

        public static GaVectorsFrame<T> CreateBasisFrame(IGaProcessor<T> processor, uint vSpaceDimension)
        {
            var vectorsList = 
                vSpaceDimension
                    .GetRange()
                    .Select(
                        index => processor.CreateVector(index, 
                            processor.OneScalar
                        )
                    );

            return new GaVectorsFrame<T>(processor, vectorsList);
        }


        private readonly List<IGasVector<T>> _vectorStoragesList;


        public int Count 
            => _vectorStoragesList.Count;

        public uint VSpaceDimension 
            => Processor.VSpaceDimension;

        public ulong GaSpaceDimension
            => Processor.GaSpaceDimension;

        public IGaProcessor<T> Processor { get; }

        public IGasVector<T> this[int index]
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


        internal GaVectorsFrame([NotNull] IGaProcessor<T> processor)
        {
            _vectorStoragesList = new List<IGasVector<T>>();

            Processor = processor;
        }

        internal GaVectorsFrame([NotNull] IGaProcessor<T> processor, [NotNull] IEnumerable<IGasVector<T>> vectorStoragesList)
        {
            _vectorStoragesList = vectorStoragesList.ToList();

            Processor = processor;
        }

        
        public GaVectorsFrame<T> AppendVector(IGasVector<T> vector)
        {
            _vectorStoragesList.Add(vector);

            return this;
        }
        
        public GaVectorsFrame<T> AppendVectors(params IGasVector<T>[] vectorsList)
        {
            foreach (var vector in vectorsList)
                _vectorStoragesList.Add(vector);

            return this;
        }
        
        public GaVectorsFrame<T> PrependVector(IGasVector<T> vector)
        {
            _vectorStoragesList.Insert(0, vector);

            return this;
        }
        
        public GaVectorsFrame<T> InsertVector(int index, IGasVector<T> vector)
        {
            _vectorStoragesList.Insert(index, vector);

            return this;
        }

        public GaVectorsFrame<T> GetSubFrame(int startIndex, int count)
        {
            return new GaVectorsFrame<T>(
                Processor,
                _vectorStoragesList
                    .Skip(startIndex)
                    .Take(count)
            );
        }

        public GaVectorsFrame<T> GetOrthogonalFrame(bool makeUnitVectors)
        {
            var vectorStoragesList = new List<IGasVector<T>>();

            var orthogonalVector = _vectorStoragesList[0];
            vectorStoragesList.Add(
                makeUnitVectors 
                    ? orthogonalVector.Divide(orthogonalVector.ENorm()).GetVectorPart()
                    : orthogonalVector
            );
            
            var mv1 = (IGasMultivector<T>)orthogonalVector;
            
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

            return new GaVectorsFrame<T>(Processor, vectorStoragesList);
        }

        public GaVectorsFrame<T> GetNegativeFrame()
        {
            return new GaVectorsFrame<T>(
                Processor,
                _vectorStoragesList.Select(v => v.GetVectorPart(Processor.Negative))
            );
        }

        public GaVectorsFrame<T> GetSwappedPairsFrame()
        {
            var frame = new GaVectorsFrame<T>(Processor);

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
                    : _vectorStoragesList[^1].GetVectorPart(Processor.Negative);

                frame.AppendVector(lastVector);
            }

            return frame;
        }

        public GaVectorsFrame<T> GetRotatedFrame(IGaRotor<T> rotor)
        {
            return new GaVectorsFrame<T>(
                Processor,
                _vectorStoragesList.Select(rotor.MapVector)
            );
        }

        public GaSubspace<T> GetSubspace()
        {
            //TODO: Modify this to find the outer product of the basis from vectors _vectorStoragesList
            return GaSubspace<T>.Create(
                Processor,
                Processor.Op(_vectorStoragesList)
            );
        }

        public bool IsOrthonormal()
        {
            for (var i = 0; i < Count; i++)
            {
                var v1 = _vectorStoragesList[i];

                var dii = Processor.Subtract(
                    v1.ESp(), 
                    Processor.OneScalar
                );

                if (!Processor.IsNearZero(dii))
                    return false;

                for (var j = i + 1; j < Count; j++)
                {
                    var dij = 
                        v1.ESp(_vectorStoragesList[j]);

                    if (!Processor.IsNearZero(dij))
                        return false;
                }
            }

            return true;
        }

        public bool HasSameHandedness(GaVectorsFrame<T> targetFrame)
        {
            var ps1 = GetSubspace();
            var ps2 = targetFrame.GetSubspace();
            var s = ps1.Blade.Subtract(ps2.Blade);

            //var s = GetPseudoScalar() - targetFrame.GetPseudoScalar();

            return s.IsZero();
        }

        public IEnumerable<GaRotor<T>> GetRotorsToFrame(GaVectorsFrame<T> targetFrame)
        {
            Debug.Assert(targetFrame.Count == Count);
            Debug.Assert(IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(HasSameHandedness(targetFrame));

            var inputFrame = new IGasVector<T>[Count];

            for (var i = 0; i < Count; i++)
                inputFrame[i] = _vectorStoragesList[i];
            
            for (var i = 0; i < Count - 1; i++)
            {
                var rotor = 
                    GaRotor<T>.CreateSimpleRotor(
                        Processor, 
                        inputFrame[i], 
                        targetFrame[i]
                    );

                yield return rotor;

                for (var j = i + 1; j < Count; j++)
                    inputFrame[j] = rotor.MapVector(inputFrame[j]);
            }
        }

        public IEnumerable<GaRotor<T>> GetRotorsToFrame(GaVectorsFrame<T> targetFrame, params int[] basisRotationOrderList)
        {
            Debug.Assert(targetFrame.Count == Count);
            Debug.Assert(IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(HasSameHandedness(targetFrame));
            //Debug.Assert(GaPoTNumUtils.ValidateIndexPermutationList(basisRotationOrderList));

            var inputFrame = new IGasVector<T>[Count];

            for (var i = 0; i < Count; i++)
                inputFrame[i] = _vectorStoragesList[i];
            
            for (var i = 0; i < Count - 1; i++)
            {
                var vectorIndex = basisRotationOrderList[i];

                var rotor = 
                    GaRotor<T>.CreateSimpleRotor(
                        Processor,
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

        public IEnumerable<T> GetAnglesToFrame(GaVectorsFrame<T> targetFrame)
        {
            Debug.Assert(targetFrame.Count == Count);

            for (var i = 0; i < Count; i++)
            {
                var v1 = _vectorStoragesList[i];
                var v2 = targetFrame[i];

                yield return Processor.ArcCos(
                    Processor.Divide(
                        v1.ESp(v2),
                        Processor.Sqrt(
                            Processor.Times(v1.ESp(), v2.ESp())
                        )
                    )
                );
            }
        }

        public IEnumerable<GaVectorsFrame<T>> GetFramePermutations()
        {
            var indexPermutationsList = 
                PermutationsUtils.GetIndexPermutations(Count);

            foreach (var indexPermutation in indexPermutationsList)
            {
                var frame = new GaVectorsFrame<T>(Processor);

                foreach (var index in indexPermutation)
                    frame.AppendVector(_vectorStoragesList[index]);

                yield return frame;
            }
        }

        public GaVectorsFrame<T> GetProjectionOnFrame(GaVectorsFrame<T> frame)
        {
            var ps = frame.GetSubspace();

            return new GaVectorsFrame<T>(
                Processor,
                _vectorStoragesList.Select(v => 
                    ps.Project(v).GetVectorPart()
                )
            );
        }

        public GaVectorsFrame<T> Normalize()
        {
            return new GaVectorsFrame<T>(
                Processor,
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

        public IEnumerator<IGasVector<T>> GetEnumerator()
        {
            return _vectorStoragesList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}