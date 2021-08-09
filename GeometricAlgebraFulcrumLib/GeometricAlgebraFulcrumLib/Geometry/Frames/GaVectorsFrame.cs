using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.Permutations;
using GeometricAlgebraFulcrumLib.Algebra.Matrices;
using GeometricAlgebraFulcrumLib.Geometry.Rotors;
using GeometricAlgebraFulcrumLib.Geometry.Subspaces;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Binary;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Unary;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Utils;

namespace GeometricAlgebraFulcrumLib.Geometry.Frames
{
    public sealed class GaVectorsFrame<T> : 
        IReadOnlyList<IGaStorageVector<T>>, 
        IGaGeometry<T>
    {
        private readonly List<IGaStorageVector<T>> _vectorStoragesList;


        public int Count 
            => _vectorStoragesList.Count;

        public uint VSpaceDimension 
            => Processor.VSpaceDimension;

        public ulong GaSpaceDimension
            => Processor.GaSpaceDimension;

        public IGaProcessor<T> Processor { get; }

        public IGaStorageVector<T> this[int index]
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

        public GaVectorsFrameKind FrameKind { get; }


        internal GaVectorsFrame([NotNull] IGaProcessor<T> processor, GaVectorsFrameKind frameKind)
        {
            _vectorStoragesList = new List<IGaStorageVector<T>>();

            Processor = processor;
            FrameKind = frameKind;
        }

        internal GaVectorsFrame([NotNull] IGaProcessor<T> processor, GaVectorsFrameKind frameKind, [NotNull] IEnumerable<IGaStorageVector<T>> vectorStoragesList)
        {
            _vectorStoragesList = vectorStoragesList.ToList();

            Processor = processor;
            FrameKind = frameKind;
        }

        
        public GaVectorsFrame<T> AppendVector(IGaStorageVector<T> vector)
        {
            _vectorStoragesList.Add(vector);

            return this;
        }
        
        public GaVectorsFrame<T> AppendVectors(params IGaStorageVector<T>[] vectorsList)
        {
            foreach (var vector in vectorsList)
                _vectorStoragesList.Add(vector);

            return this;
        }
        
        public GaVectorsFrame<T> PrependVector(IGaStorageVector<T> vector)
        {
            _vectorStoragesList.Insert(0, vector);

            return this;
        }
        
        public GaVectorsFrame<T> InsertVector(int index, IGaStorageVector<T> vector)
        {
            _vectorStoragesList.Insert(index, vector);

            return this;
        }


        public GaVectorsFrame<T> GetSubFrame(int startIndex, int count)
        {
            return new GaVectorsFrame<T>(
                Processor,
                FrameKind,
                _vectorStoragesList
                    .Skip(startIndex)
                    .Take(count)
            );
        }

        public GaVectorsFrame<T> GetNegativeFrame()
        {
            return new GaVectorsFrame<T>(
                Processor,
                FrameKind,
                _vectorStoragesList.Select(v => v.GetVectorPart(Processor.Negative))
            );
        }

        public GaVectorsFrame<T> GetOrthogonalFrame(bool makeUnitVectors)
        {
            if (FrameKind == GaVectorsFrameKind.OrthogonalUnitVectors)
                return this;

            if (FrameKind == GaVectorsFrameKind.Orthogonal && !makeUnitVectors)
                return this;

            var orthogonalVector = _vectorStoragesList[0];
            var vectorStoragesList = new List<IGaStorageVector<T>>
            {
                makeUnitVectors
                    ? Processor.Divide(
                        orthogonalVector, 
                        Processor.ENorm(orthogonalVector)
                    )
                    : orthogonalVector
            };

            var mv1 = (IGaStorageMultivector<T>) orthogonalVector;
            
            for (var i = 1; i < _vectorStoragesList.Count; i++)
            {
                var mv2 = 
                    Processor.Op(mv1, _vectorStoragesList[i]);
                
                orthogonalVector = 
                    Processor.EGp(Processor.Reverse(mv1), mv2).GetVectorPart();
                
                vectorStoragesList.Add( 
                    makeUnitVectors
                        ? Processor.Divide(
                            orthogonalVector, 
                            Processor.ENorm(orthogonalVector)
                        )
                        : orthogonalVector
                    );
                
                mv1 = mv2;
            }

            return new GaVectorsFrame<T>(
                Processor, 
                makeUnitVectors 
                    ? GaVectorsFrameKind.OrthogonalUnitVectors 
                    : GaVectorsFrameKind.Orthogonal,
                vectorStoragesList
            );
        }

        public GaVectorsFrame<T> GetSwappedPairsFrame()
        {
            var frame = new GaVectorsFrame<T>(Processor, FrameKind);

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
                    Processor.ESp(v1), 
                    Processor.OneScalar
                );

                if (!Processor.IsNearZero(dii))
                    return false;

                for (var j = i + 1; j < Count; j++)
                {
                    var dij = 
                        Processor.ESp(v1, _vectorStoragesList[j]);

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
            var s = Processor.Subtract(ps1.Blade, ps2.Blade);

            //var s = GetPseudoScalar() - targetFrame.GetPseudoScalar();

            return Processor.IsZero(s);
        }

        public IEnumerable<GaRotor<T>> GetRotorsToFrame(GaVectorsFrame<T> targetFrame)
        {
            Debug.Assert(targetFrame.Count == Count);
            Debug.Assert(IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(HasSameHandedness(targetFrame));

            var inputFrame = new IGaStorageVector<T>[Count];

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

            var inputFrame = new IGaStorageVector<T>[Count];

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
                        Processor.ESp(v1, v2),
                        Processor.Sqrt(
                            Processor.Times(Processor.ESp(v1), Processor.ESp(v2))
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
                var frame = new GaVectorsFrame<T>(Processor, FrameKind);

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
                GaVectorsFrameKind.Undefined, 
                _vectorStoragesList.Select(v => 
                    ps.Project(v).GetVectorPart()
                )
            );
        }

        public GaVectorsFrame<T> Normalize()
        {
            if (FrameKind.IsUnitVectors())
                return this;

            var frameKind = FrameKind == GaVectorsFrameKind.LinearlyIndependent
                ? GaVectorsFrameKind.UnitVectors
                : GaVectorsFrameKind.OrthogonalUnitVectors;

            return new GaVectorsFrame<T>(
                Processor,
                frameKind,
                _vectorStoragesList.Select(v => Processor.DivideByENorm(v))
            );
        }

        public T[,] GetMatrix()
        {
            return GetMatrix((int) VSpaceDimension);
        }

        public T[,] GetMatrix(int rowsCount)
        {
            var colsCount = Count;
            var itemsArray = 
                Processor.CreateZeroMatrix(rowsCount, colsCount);

            for (var j = 0; j < Count; j++)
            {
                var vectorTerms = 
                    _vectorStoragesList[j]
                        .IndexScalarDictionary
                        .Where(pair => pair.Key < (ulong) rowsCount);

                foreach (var (index, scalar) in vectorTerms)
                    itemsArray[index, j] = scalar;
            }
            
            return itemsArray;
        }

        public T[,] GetInnerProductsMatrix()
        {
            var ipm = new T[Count, Count];

            for (var i = 0; i < Count; i++)
            {
                var v1 = _vectorStoragesList[i];

                ipm[i, i] = Processor.Sp(v1);

                for (var j = i + 1; j < Count; j++)
                {
                    var ip = Processor.Sp(v1, _vectorStoragesList[j]);

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
                    var ip = Processor.GetAngle(v1, _vectorStoragesList[j]);

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
                    var ip = Processor.GetAngle(v1, _vectorStoragesList[j]);

                    ipm[i, j] = ip;
                    ipm[j, i] = ip;
                }
            }

            return ipm;
        }

        public IEnumerator<IGaStorageVector<T>> GetEnumerator()
        {
            return _vectorStoragesList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}