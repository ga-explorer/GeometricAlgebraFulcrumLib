using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GeometricAlgebraLib.Processors.Multivectors;
using GeometricAlgebraLib.Processors.Scalars;
using GeometricAlgebraLib.Storage;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraLib.Geometry.Euclidean
{
    public sealed class GaEuclideanSimpleRotorsSequence<T>
        : IGaEuclideanGeometry<T>, IGaRotor<T>, IReadOnlyList<GaEuclideanSimpleRotor<T>>
    {
        public static GaEuclideanSimpleRotorsSequence<T> CreateIdentity(IGaScalarProcessor<T> scalarProcessor)
        {
            return new(scalarProcessor);
        }

        public static GaEuclideanSimpleRotorsSequence<T> Create(params GaEuclideanSimpleRotor<T>[] rotorsList)
        {
            return new(rotorsList[0].ScalarProcessor, rotorsList);
        }

        public static GaEuclideanSimpleRotorsSequence<T> Create(IGaScalarProcessor<T> scalarProcessor, IEnumerable<GaEuclideanSimpleRotor<T>> rotorsList)
        {
            return new(scalarProcessor, rotorsList);
        }

        public static GaEuclideanSimpleRotorsSequence<T> CreateFromOrthonormalFrames(GaEuclideanVectorsFrame<T> sourceFrame, GaEuclideanVectorsFrame<T> targetFrame, bool fullRotorsFlag = false)
        {
            Debug.Assert(targetFrame.Count == sourceFrame.Count);
            Debug.Assert(sourceFrame.IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));

            var scalarProcessor = sourceFrame.ScalarProcessor;

            var rotorsSequence = new GaEuclideanSimpleRotorsSequence<T>(scalarProcessor);

            var sourceFrameVectors = sourceFrame.ToArray();

            var n = fullRotorsFlag 
                ? sourceFrame.Count 
                : sourceFrame.Count - 1;
            
            for (var i = 0; i < n; i++)
            {
                var sourceVector = sourceFrameVectors[i];
                var targetVector = targetFrame[i];

                var rotor = 
                    GaEuclideanSimpleRotor<T>.Create(sourceVector, targetVector);

                rotorsSequence.AppendRotor(rotor);

                for (var j = i + 1; j < sourceFrame.Count; j++)
                    sourceFrameVectors[j] = rotor.MapVector(sourceFrameVectors[j]);
            }

            return rotorsSequence;
        }

        public static GaEuclideanSimpleRotorsSequence<T> CreateFromOrthonormalFrames(GaEuclideanVectorsFrame<T> sourceFrame, GaEuclideanVectorsFrame<T> targetFrame, int[] sequenceArray)
        {
            Debug.Assert(targetFrame.Count == sourceFrame.Count);
            Debug.Assert(sourceFrame.IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));

            Debug.Assert(sequenceArray.Length == sourceFrame.Count - 1);
            Debug.Assert(sequenceArray.Min() >= 0);
            Debug.Assert(sequenceArray.Max() < sourceFrame.Count);
            Debug.Assert(sequenceArray.Distinct().Count() == sourceFrame.Count - 1);

            var scalarProcessor = sourceFrame.ScalarProcessor;

            var rotorsSequence = new GaEuclideanSimpleRotorsSequence<T>(scalarProcessor);

            var sourceFrameVectors = sourceFrame.ToArray();
            
            for (var i = 0; i < sourceFrame.Count - 1; i++)
            {
                var vectorIndex = sequenceArray[i];

                var sourceVector = sourceFrameVectors[vectorIndex];
                var targetVector = targetFrame[vectorIndex];

                var rotor = GaEuclideanSimpleRotor<T>.Create(
                    sourceVector, 
                    targetVector
                );

                rotorsSequence.AppendRotor(rotor);

                for (var j = i + 1; j < sourceFrame.Count; j++)
                    sourceFrameVectors[j] = 
                        rotor.MapVector(sourceFrameVectors[j]);
            }

            return rotorsSequence;
        }

        public static GaEuclideanSimpleRotorsSequence<T> CreateFromFrames(int baseSpaceDimensions, GaEuclideanVectorsFrame<T> sourceFrame, GaEuclideanVectorsFrame<T> targetFrame)
        {
            Debug.Assert(targetFrame.Count == sourceFrame.Count);
            //Debug.Assert(IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));

            var scalarProcessor = sourceFrame.ScalarProcessor;

            var rotorsSequence = 
                new GaEuclideanSimpleRotorsSequence<T>(scalarProcessor);

            var pseudoScalarSubspace = 
                GaEuclideanSubspace<T>.CreateFromPseudoScalar(scalarProcessor, baseSpaceDimensions);

            var sourceFrameVectors = new IGaVectorStorage<T>[sourceFrame.Count];
            var targetFrameVectors = new IGaVectorStorage<T>[targetFrame.Count];

            for (var i = 0; i < sourceFrame.Count; i++)
            {
                sourceFrameVectors[i] = sourceFrame[i];
                targetFrameVectors[i] = targetFrame[i];
            }
            
            for (var i = 0; i < sourceFrame.Count - 1; i++)
            {
                var sourceVector = sourceFrameVectors[i];
                var targetVector = targetFrameVectors[i];

                var rotor = 
                    GaEuclideanSimpleRotor<T>.Create(sourceVector, targetVector);

                rotorsSequence.AppendRotor(rotor);

                pseudoScalarSubspace = 
                    pseudoScalarSubspace.GetOrthogonalComplementSubspace(targetVector);

                for (var j = i + 1; j < sourceFrame.Count; j++)
                {
                    sourceFrameVectors[j] =
                        pseudoScalarSubspace
                            .Project(rotor.MapVector(sourceFrameVectors[j]))
                            .GetVectorPart();

                    targetFrameVectors[j] =
                        pseudoScalarSubspace
                            .Project(targetFrameVectors[j])
                            .GetVectorPart();
                }
            }

            return rotorsSequence;
        }

        public static GaEuclideanSimpleRotorsSequence<double> CreateOrthogonalRotors(double[,] rotationMatrix)
        {
            var evdSolver = Matrix<double>.Build.DenseOfArray(rotationMatrix).Evd();

            var eigenValuesReal = evdSolver.EigenValues.Real();
            var eigenValuesImag = evdSolver.EigenValues.Imaginary();
            var eigenVectors = evdSolver.EigenVectors;

            //TODO: Complete this

            return new GaEuclideanSimpleRotorsSequence<double>(
                GaScalarProcessorFloat64.DefaultProcessor

            );
        }


        private readonly List<GaEuclideanSimpleRotor<T>> _rotorsList
            = new();


        public int Count 
            => _rotorsList.Count;

        public IGaScalarProcessor<T> ScalarProcessor { get; }

        public IGaMultivectorStorage<T> Storage { get; }

        public IGaMultivectorStorage<T> StorageReverse { get; }
        
        public GaEuclideanSimpleRotor<T> this[int index]
        {
            get => _rotorsList[index];
            set => _rotorsList[index] = 
                value 
                ?? throw new ArgumentNullException(nameof(value));
        }

        public bool IsValid
            => _rotorsList.All(rotor => rotor.IsValid);

        public bool IsInvalid 
            => !IsValid;


        private GaEuclideanSimpleRotorsSequence([NotNull] IGaScalarProcessor<T> scalarProcessor)
        {
            ScalarProcessor = scalarProcessor;
        }

        private GaEuclideanSimpleRotorsSequence([NotNull] IGaScalarProcessor<T> scalarProcessor, IEnumerable<GaEuclideanSimpleRotor<T>> rotorsList)
        {
            ScalarProcessor = scalarProcessor;

            _rotorsList.AddRange(rotorsList);
        }


        public bool ValidateRotation(GaEuclideanVectorsFrame<T> sourceFrame, GaEuclideanVectorsFrame<T> targetFrame)
        {
            if (sourceFrame.Count != targetFrame.Count)
                return false;

            var rotatedFrame = Rotate(sourceFrame);

            return !rotatedFrame.Select(
                (v, i) => !targetFrame[i].Subtract(v).IsZero()
            ).Any();
        }

        public bool IsRotorsSequence()
        {
            return _rotorsList.All(r => r.Storage.IsEuclideanRotor());
        }

        public bool IsSimpleRotorsSequence()
        {
            return _rotorsList.All(r => r.Storage.IsSimpleEuclideanRotor());
        }

        public GaEuclideanSimpleRotor<T> GetRotor(int index)
        {
            return _rotorsList[index];
        }

        public GaEuclideanSimpleRotorsSequence<T> AppendRotor(GaEuclideanSimpleRotor<T> rotor)
        {
            _rotorsList.Add(rotor);

            return this;
        }

        public GaEuclideanSimpleRotorsSequence<T> PrependRotor(GaEuclideanSimpleRotor<T> rotor)
        {
            _rotorsList.Insert(0, rotor);

            return this;
        }

        public GaEuclideanSimpleRotorsSequence<T> InsertRotor(int index, GaEuclideanSimpleRotor<T> rotor)
        {
            _rotorsList.Insert(index, rotor);

            return this;
        }

        public GaEuclideanSimpleRotorsSequence<T> GetSubSequence(int startIndex, int count)
        {
            return new(
                ScalarProcessor,
                _rotorsList.Skip(startIndex).Take(count)
            );
        }

        public IEnumerable<IGaMultivectorStorage<T>> GetRotations(IGaMultivectorStorage<T> storage)
        {
            var v = storage;

            yield return v;

            foreach (var rotor in _rotorsList)
            {
                v = rotor.MapMultivector(v);

                yield return v;
            }
        }

        public IEnumerable<GaEuclideanVector<T>> GetRotations(GaEuclideanVector<T> vector)
        {
            var v = vector;

            yield return v;

            foreach (var rotor in _rotorsList)
            {
                v = v.GetRotatedVector(rotor);

                yield return v;
            }
        }

        public IEnumerable<GaEuclideanVectorsFrame<T>> GetRotations(GaEuclideanVectorsFrame<T> frame)
        {
            var f = frame;

            yield return f;

            foreach (var rotor in _rotorsList)
            {
                f = f.GetRotatedFrame(rotor);

                yield return f;
            }
        }

        public IEnumerable<T[,]> GetRotationMatrices(int rowsCount)
        {
            var f = 
                GaEuclideanVectorsFrame<T>.CreateBasisFrame(ScalarProcessor, rowsCount);

            yield return f.GetMatrix(rowsCount);

            foreach (var rotor in _rotorsList)
                yield return f.GetRotatedFrame(rotor).GetMatrix(rowsCount);
        }

        public GaEuclideanVector<T> Map(GaEuclideanVector<T> vector)
        {
            return GaEuclideanVector<T>.Create(
                MapVector(vector.Storage)
            );
        }
        

        public IGaVectorsLinearMap<T> GetAdjoint()
        {
            return new GaEuclideanSimpleRotorsSequence<T>(
                ScalarProcessor,
                _rotorsList
                    .Select(r => r.GetReverseRotor())
                    .Reverse()
            );
        }

        public IGaVectorStorage<T> MapBasisVector(int index)
        {
            throw new NotImplementedException();
        }

        public IGaVectorStorage<T> MapBasisVector(ulong index)
        {
            throw new NotImplementedException();
        }

        public IGaBivectorStorage<T> MapBasisBivector(int index1, int index2)
        {
            throw new NotImplementedException();
        }

        public IGaBivectorStorage<T> MapBasisBivector(ulong index1, ulong index2)
        {
            throw new NotImplementedException();
        }

        public IGaKVectorStorage<T> MapBasisBlade(ulong id)
        {
            throw new NotImplementedException();
        }

        public IGaKVectorStorage<T> MapBasisBlade(int grade, ulong index)
        {
            throw new NotImplementedException();
        }

        public IGaScalarStorage<T> MapScalar(IGaScalarStorage<T> storage)
        {
            throw new NotImplementedException();
        }

        public IGaKVectorStorage<T> MapTerm(IGaKVectorTermStorage<T> storage)
        {
            throw new NotImplementedException();
        }

        public IGaVectorStorage<T> MapVector(IGaVectorStorage<T> storage)
        {
            throw new NotImplementedException();
        }

        public IGaBivectorStorage<T> MapBivector(IGaBivectorStorage<T> storage)
        {
            throw new NotImplementedException();
        }

        public IGaKVectorStorage<T> MapKVector(IGaKVectorStorage<T> storage)
        {
            throw new NotImplementedException();
        }

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorGradedStorage<T> storage)
        {
            throw new NotImplementedException();
        }

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorTermsStorage<T> storage)
        {
            throw new NotImplementedException();
        }

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorStorage<T> storage)
        {
            return _rotorsList
                .Aggregate(
                    storage, 
                    (current, rotor) => rotor.MapMultivector(current)
                );
        }

        public GaEuclideanVectorsFrame<T> Rotate(GaEuclideanVectorsFrame<T> frame)
        {
            return _rotorsList
                .Aggregate(
                    frame, 
                    (current, rotor) => current.GetRotatedFrame(rotor)
                );
        }

        public GaEuclideanRotor<T> GetFinalRotor()
        {
            var storage = _rotorsList
                .Skip(1)
                .Select(r => r.Storage)
                .Aggregate(
                    _rotorsList[0].Storage, 
                    (current, rotor) => rotor.EGp(current)
                );

            return GaEuclideanRotor<T>.Create(storage);
        }

        public T[,] GetFinalMatrix(int rowsCount)
        {
            return Rotate(
                GaEuclideanVectorsFrame<T>.CreateBasisFrame(ScalarProcessor, rowsCount)
            ).GetMatrix(rowsCount);
        }

        public GaEuclideanSimpleRotorsSequence<T> Reverse()
        {
            var rotorsSequence = new GaEuclideanSimpleRotorsSequence<T>(ScalarProcessor);

            foreach (var rotor in _rotorsList)
                rotorsSequence.PrependRotor(rotor.GetReverseRotor());

            return rotorsSequence;
        }

        public IEnumerator<GaEuclideanSimpleRotor<T>> GetEnumerator()
        {
            return _rotorsList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}