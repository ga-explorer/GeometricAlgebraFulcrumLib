using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.Permutations;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Rotors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Geometry.Subspaces;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Frames
{
    public class VectorFrame<T> :
        IVectorFrame<T>
    {
        internal static VectorFrame<T> Create(IGeometricAlgebraProcessor<T> geometricProcessor, VectorFrameSpecs frameSpecs)
        {
            return new VectorFrame<T>(
                geometricProcessor,
                frameSpecs
            );
        }

        internal static VectorFrame<T> Create(IGeometricAlgebraProcessor<T> geometricProcessor, VectorFrameSpecs frameSpecs, IEnumerable<GaVector<T>> vectorList)
        {
            return new VectorFrame<T>(
                geometricProcessor,
                frameSpecs,
                vectorList.Select(v => v.VectorStorage).ToList()
            );
        }


        private readonly List<VectorStorage<T>> _vectorList;


        public int Count
            => _vectorList.Count;

        public IScalarAlgebraProcessor<T> ScalarProcessor
            => GeometricProcessor;

        public ILinearAlgebraProcessor<T> LinearProcessor
            => GeometricProcessor;

        public IGeometricAlgebraProcessor<T> GeometricProcessor { get; }

        public GaVector<T> this[int index]
        {
            get => _vectorList[index].CreateVector(GeometricProcessor);
            set => _vectorList[index] =
                value.VectorStorage
                ?? throw new ArgumentNullException(nameof(value));
        }

        public VectorFrameSpecs FrameSpecs { get; }


        private VectorFrame([NotNull] IGeometricAlgebraProcessor<T> geometricProcessor, VectorFrameSpecs frameSpecs)
        {
            _vectorList = new List<VectorStorage<T>>();

            GeometricProcessor = geometricProcessor;
            FrameSpecs = frameSpecs;
        }

        private VectorFrame([NotNull] IGeometricAlgebraProcessor<T> geometricProcessor, VectorFrameSpecs frameSpecs, [NotNull] List<VectorStorage<T>> vectorList)
        {
            _vectorList = vectorList;

            GeometricProcessor = geometricProcessor;
            FrameSpecs = frameSpecs;
        }


        public bool IsValid()
        {
            return true;
        }

        public VectorFrame<T> AppendVector(VectorStorage<T> vector)
        {
            _vectorList.Add(vector);

            return this;
        }

        public VectorFrame<T> AppendVectors(params VectorStorage<T>[] vectorsList)
        {
            foreach (var vector in vectorsList)
                _vectorList.Add(vector);

            return this;
        }

        public VectorFrame<T> PrependVector(VectorStorage<T> vector)
        {
            _vectorList.Insert(0, vector);

            return this;
        }

        public VectorFrame<T> InsertVector(int index, VectorStorage<T> vector)
        {
            _vectorList.Insert(index, vector);

            return this;
        }


        public VectorFrame<T> GetSubFrame(int startIndex, int count)
        {
            return new VectorFrame<T>(
                GeometricProcessor,
                FrameSpecs,
                _vectorList.Skip(startIndex).Take(count).ToList()
            );
        }

        public VectorFrame<T> GetNegativeFrame()
        {
            return new VectorFrame<T>(
                GeometricProcessor,
                FrameSpecs,
                _vectorList.Select(v => GeometricProcessor.Negative(v)).ToList()
            );
        }

        public VectorFrame<T> GetOrthonormalFrame()
        {
            if (FrameSpecs.Orthonormal == true)
                return FrameSpecs.UnitNormSquared == true
                    ? this
                    : GetUnitNormFrame();

            var orthogonalVector = _vectorList[0];
            var vectorStoragesList = new List<VectorStorage<T>>
            {
                GeometricProcessor.DivideByNorm(orthogonalVector)
            };

            var mv1 = (IMultivectorStorage<T>)orthogonalVector;

            for (var i = 1; i < _vectorList.Count; i++)
            {
                var mv2 =
                    GeometricProcessor.Op(mv1, _vectorList[i]);

                orthogonalVector =
                    GeometricProcessor.Gp(
                        GeometricProcessor.Reverse(mv1),
                        mv2
                    ).GetVectorPart();

                vectorStoragesList.Add(
                    GeometricProcessor.DivideByNorm(orthogonalVector)
                );

                mv1 = mv2;
            }

            var frameSpecs = new VectorFrameSpecs()
            {
                EqualNormSquared = true,
                EqualScalarProduct = null,
                LinearlyIndependent = true,
                Orthogonal = true,
                UnitNormSquared = true
            };

            return new VectorFrame<T>(
                GeometricProcessor,
                frameSpecs,
                vectorStoragesList
            );
        }

        public VectorFrame<T> GetOrthogonalFrame()
        {
            if (FrameSpecs.Orthogonal == true)
                return this;

            var orthogonalVector = _vectorList[0];
            var vectorStoragesList = new List<VectorStorage<T>>
            {
                orthogonalVector
            };

            var mv1 = (IMultivectorStorage<T>)orthogonalVector;

            for (var i = 1; i < _vectorList.Count; i++)
            {
                var mv2 = GeometricProcessor.Op(
                    mv1,
                    _vectorList[i]
                );

                orthogonalVector =
                    GeometricProcessor.Gp(
                        GeometricProcessor.Reverse(mv1),
                        mv2
                    ).GetVectorPart();

                vectorStoragesList.Add(orthogonalVector);

                mv1 = mv2;
            }

            var frameSpecs = new VectorFrameSpecs()
            {
                EqualNormSquared = null,
                EqualScalarProduct = null,
                LinearlyIndependent = FrameSpecs.LinearlyIndependent,
                Orthogonal = true,
                UnitNormSquared = null
            };

            return new VectorFrame<T>(
                GeometricProcessor,
                frameSpecs,
                vectorStoragesList
            );
        }

        public VectorFrame<T> GetSwappedPairsFrame()
        {
            var frame = new VectorFrame<T>(GeometricProcessor, FrameSpecs);

            //Swap each pair of two consecutive vectors in the frame
            for (var i = 0; i < _vectorList.Count - 1; i += 2)
            {
                frame.AppendVector(_vectorList[i + 1]);
                frame.AppendVector(_vectorList[i]);
            }

            if (_vectorList.Count % 2 == 1)
            {
                //To keep the same handedness we count the number of swaps and
                //negate the final vector if the number is odd

                var numberOfSwaps = (_vectorList.Count - 1) / 2;

                var lastVector = numberOfSwaps % 2 == 0
                    ? _vectorList[^1]
                    : GeometricProcessor.Negative(_vectorList[^1]);

                frame.AppendVector(lastVector);
            }

            return frame;
        }

        public Subspace<T> GetSubspace()
        {
            //TODO: Modify this to find the outer product of the basis from vectors _vectorStoragesList
            return Subspace<T>.Create(
                GeometricProcessor,
                GeometricProcessor.Op(_vectorList).CreateKVector(GeometricProcessor)
            );
        }

        public bool IsOrthonormal()
        {
            for (var i = 0; i < Count; i++)
            {
                var v1 = _vectorList[i];

                var dii = GeometricProcessor.Subtract(
                    GeometricProcessor.SpSquared(v1),
                    GeometricProcessor.ScalarOne
                );

                if (!GeometricProcessor.IsNearZero(dii))
                    return false;

                for (var j = i + 1; j < Count; j++)
                {
                    var dij = GeometricProcessor.Sp(v1, _vectorList[j]);

                    if (!GeometricProcessor.IsNearZero(dij))
                        return false;
                }
            }

            return true;
        }

        public bool HasSameHandedness(VectorFrame<T> targetFrame)
        {
            var ps1 = GetSubspace();
            var ps2 = targetFrame.GetSubspace();
            var s = ps1.GetBlade() - ps2.GetBlade();

            //var s = GetPseudoScalar() - targetFrame.GetPseudoScalar();

            return s.IsZero();
        }

        public IEnumerable<Rotor<T>> GetRotorsToFrame(VectorFrame<T> targetFrame)
        {
            Debug.Assert(targetFrame.Count == Count);
            Debug.Assert(IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(HasSameHandedness(targetFrame));

            var sourceFrame = new GaVector<T>[Count];

            for (var i = 0; i < Count; i++)
                sourceFrame[i] = _vectorList[i].CreateVector(GeometricProcessor);

            for (var i = 0; i < Count - 1; i++)
            {
                var rotor =
                    Rotor<T>.CreateEuclideanPureRotor(
                        sourceFrame[i],
                        targetFrame[i]
                    );

                yield return rotor;

                for (var j = i + 1; j < Count; j++)
                    sourceFrame[j] = rotor.OmMap(sourceFrame[j]);
            }
        }

        public IEnumerable<Rotor<T>> GetRotorsToFrame(VectorFrame<T> targetFrame, params int[] basisRotationOrderList)
        {
            Debug.Assert(targetFrame.Count == Count);
            Debug.Assert(IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(HasSameHandedness(targetFrame));
            //Debug.Assert(GeoPoTNumUtils.ValidateIndexPermutationList(basisRotationOrderList));

            var sourceFrame = new GaVector<T>[Count];

            for (var i = 0; i < Count; i++)
                sourceFrame[i] = _vectorList[i].CreateVector(GeometricProcessor);

            for (var i = 0; i < Count - 1; i++)
            {
                var vectorIndex = basisRotationOrderList[i];

                var rotor =
                    Rotor<T>.CreateEuclideanPureRotor(
                        sourceFrame[vectorIndex],
                        targetFrame[vectorIndex]
                    );

                yield return rotor;

                for (var j = i + 1; j < Count; j++)
                {
                    var vectorIndex1 = basisRotationOrderList[j];

                    sourceFrame[vectorIndex1] =
                        rotor.OmMap(sourceFrame[vectorIndex1]);
                }
            }
        }

        public IEnumerable<T> GetAnglesToFrame(VectorFrame<T> targetFrame)
        {
            Debug.Assert(targetFrame.Count == Count);

            for (var i = 0; i < Count; i++)
            {
                var v1 = this[i];
                var v2 = targetFrame[i];

                yield return (v1.ESp(v2) / (v1.ESpSquared() * v2.ESpSquared()).Sqrt()).ArcCos().ScalarValue;
            }
        }

        public IEnumerable<VectorFrame<T>> GetFramePermutations()
        {
            var indexPermutationsList =
                PermutationsUtils.GetIndexPermutations(Count);

            foreach (var indexPermutation in indexPermutationsList)
            {
                var frame = new VectorFrame<T>(GeometricProcessor, FrameSpecs);

                foreach (var index in indexPermutation)
                    frame.AppendVector(_vectorList[index]);

                yield return frame;
            }
        }

        public VectorFrame<T> GetProjectionOnFrame(VectorFrame<T> frame)
        {
            var ps = frame.GetSubspace();

            return new VectorFrame<T>(
                GeometricProcessor,
                VectorFrameSpecs.CreateUndefinedSpecs(),
                this.Select(v => ps.Project(v).VectorStorage).ToList()
            );
        }

        public VectorFrame<T> GetUnitNormFrame()
        {
            if (FrameSpecs.UnitNormSquared == true)
                return this;

            var frameSpecs = new VectorFrameSpecs()
            {
                EqualNormSquared = true,
                EqualScalarProduct = FrameSpecs.EqualScalarProduct,
                LinearlyIndependent = FrameSpecs.LinearlyIndependent,
                Orthogonal = FrameSpecs.Orthogonal,
                UnitNormSquared = true
            };

            return new VectorFrame<T>(
                GeometricProcessor,
                frameSpecs,
                _vectorList.Select(v => GeometricProcessor.DivideByENorm(v)).ToList()
            );
        }

        public T[,] GetArray()
        {
            return GetArray((int)GeometricProcessor.VSpaceDimension);
        }

        public T[,] GetArray(int rowsCount)
        {
            var colsCount = Count;
            var itemsArray =
                GeometricProcessor.CreateArrayZero2D(rowsCount, colsCount);

            for (var j = 0; j < Count; j++)
            {
                var vectorTerms =
                    _vectorList[j].GetLinVectorIndexScalarStorage()
                        .GetIndexScalarRecords()
                        .Where(pair => pair.Index < (ulong)rowsCount);

                foreach (var (index, scalar) in vectorTerms)
                    itemsArray[index, j] = scalar;
            }

            return itemsArray;
        }

        public T[,] GetInnerProductsArray()
        {
            var ipm = new T[Count, Count];

            for (var i = 0; i < Count; i++)
            {
                var v1 = _vectorList[i];

                ipm[i, i] = GeometricProcessor.SpSquared(v1);

                for (var j = i + 1; j < Count; j++)
                {
                    var ip = GeometricProcessor.Sp(v1, _vectorList[j]);

                    ipm[i, j] = ip;
                    ipm[j, i] = ip;
                }
            }

            return ipm;
        }

        public T[,] GetInnerAnglesArray()
        {
            var ipm = new T[Count, Count];

            for (var i = 0; i < Count; i++)
            {
                var v1 = this[i];

                for (var j = i + 1; j < Count; j++)
                {
                    var ip = v1.GetEuclideanAngle(this[j]).ScalarValue;

                    ipm[i, j] = ip;
                    ipm[j, i] = ip;
                }
            }

            return ipm;
        }

        public T[,] GetInnerAnglesInDegreesArray()
        {
            var ipm = new T[Count, Count];

            for (var i = 0; i < Count; i++)
            {
                var v1 = this[i];

                for (var j = i + 1; j < Count; j++)
                {
                    var ip = v1.GetEuclideanAngle(this[j]).ScalarValue;

                    ipm[i, j] = ip;
                    ipm[j, i] = ip;
                }
            }

            return ipm;
        }

        public IEnumerator<GaVector<T>> GetEnumerator()
        {
            return _vectorList.Select(
                v => v.CreateVector(GeometricProcessor)
            ).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            var composer = new LinearTextComposer();

            composer
                .AppendLine("Free Frame {")
                .IncreaseIndentation();

            foreach (var vector in _vectorList)
                composer.AppendAtNewLine(vector.ToString());

            composer
                .DecreaseIndentation()
                .AppendAtNewLine("}");

            return composer.ToString();
        }
    }
}