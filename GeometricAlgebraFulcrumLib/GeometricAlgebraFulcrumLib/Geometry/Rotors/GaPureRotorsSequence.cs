using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Space;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Geometry.Frames;
using GeometricAlgebraFulcrumLib.Geometry.Subspaces;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Geometry.Rotors
{
    public sealed class GaPureRotorsSequence<T>
        : IGaGeometry<T>, IGaRotor<T>, IReadOnlyList<GaPureRotor<T>>
    {
        public static GaPureRotorsSequence<T> CreateIdentity(IGaProcessor<T> processor)
        {
            return new GaPureRotorsSequence<T>(processor);
        }

        public static GaPureRotorsSequence<T> Create(IGaProcessor<T> processor, params GaPureRotor<T>[] rotorsList)
        {
            return new GaPureRotorsSequence<T>(processor, rotorsList);
        }

        public static GaPureRotorsSequence<T> Create(IGaProcessor<T> processor, IEnumerable<GaPureRotor<T>> rotorsList)
        {
            return new GaPureRotorsSequence<T>(processor, rotorsList);
        }

        public static GaPureRotorsSequence<T> CreateFromOrthonormalFrames(GaVectorsFrame<T> sourceFrame, GaVectorsFrame<T> targetFrame, bool fullRotorsFlag = false)
        {
            Debug.Assert(targetFrame.Count == sourceFrame.Count);
            Debug.Assert(sourceFrame.IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));

            var processor = sourceFrame.Processor;

            var rotorsSequence = new GaPureRotorsSequence<T>(processor);

            var sourceFrameVectors = sourceFrame.ToArray();

            var n = fullRotorsFlag 
                ? sourceFrame.Count 
                : sourceFrame.Count - 1;
            
            for (var i = 0; i < n; i++)
            {
                var sourceVector = sourceFrameVectors[i];
                var targetVector = targetFrame[i];

                var rotor = 
                    processor.CreateEuclideanRotor(sourceVector, targetVector);

                rotorsSequence.AppendRotor(rotor);

                for (var j = i + 1; j < sourceFrame.Count; j++)
                    sourceFrameVectors[j] = rotor.MapVector(sourceFrameVectors[j]);
            }

            return rotorsSequence;
        }

        public static GaPureRotorsSequence<T> CreateFromOrthonormalFrames(GaVectorsFrame<T> sourceFrame, GaVectorsFrame<T> targetFrame, int[] sequenceArray)
        {
            Debug.Assert(targetFrame.Count == sourceFrame.Count);
            Debug.Assert(sourceFrame.IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));

            Debug.Assert(sequenceArray.Length == sourceFrame.Count - 1);
            Debug.Assert(sequenceArray.Min() >= 0);
            Debug.Assert(sequenceArray.Max() < sourceFrame.Count);
            Debug.Assert(sequenceArray.Distinct().Count() == sourceFrame.Count - 1);

            var processor = sourceFrame.Processor;

            var rotorsSequence = new GaPureRotorsSequence<T>(processor);

            var sourceFrameVectors = sourceFrame.ToArray();
            
            for (var i = 0; i < sourceFrame.Count - 1; i++)
            {
                var vectorIndex = sequenceArray[i];

                var sourceVector = sourceFrameVectors[vectorIndex];
                var targetVector = targetFrame[vectorIndex];

                var rotor = processor.CreateEuclideanRotor(
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

        public static GaPureRotorsSequence<T> CreateFromFrames(uint baseSpaceDimensions, GaVectorsFrame<T> sourceFrame, GaVectorsFrame<T> targetFrame)
        {
            Debug.Assert(targetFrame.Count == sourceFrame.Count);
            //Debug.Assert(IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));

            var processor = sourceFrame.Processor;

            var rotorsSequence = 
                new GaPureRotorsSequence<T>(processor);

            var pseudoScalarSubspace = 
                GaSubspace<T>.CreateFromPseudoScalar(processor, baseSpaceDimensions);

            var sourceFrameVectors = new IGaVectorStorage<T>[sourceFrame.Count];
            var targetFrameVectors = new IGaVectorStorage<T>[targetFrame.Count];

            for (var i = 0; i < sourceFrame.Count; i++)
            {
                sourceFrameVectors[i] = sourceFrame[i];
                targetFrameVectors[i] = targetFrame[i];
            }
            
            for (var i = 0U; i < sourceFrame.Count - 1; i++)
            {
                var sourceVector = sourceFrameVectors[i];
                var targetVector = targetFrameVectors[i];

                var rotor = 
                    processor.CreateEuclideanRotor(sourceVector, targetVector);

                rotorsSequence.AppendRotor(rotor);

                pseudoScalarSubspace = 
                    processor.CreateSubspace(
                        pseudoScalarSubspace
                            .Complement(targetVector)
                            .GetKVectorPart(baseSpaceDimensions - i - 1)
                    );

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

        public static GaPureRotorsSequence<double> CreateOrthogonalRotors(double[,] rotationMatrix)
        {
            var evdSolver = Matrix<double>.Build.DenseOfArray(rotationMatrix).Evd();

            var eigenValuesReal = evdSolver.EigenValues.Real();
            var eigenValuesImag = evdSolver.EigenValues.Imaginary();
            var eigenVectors = evdSolver.EigenVectors;

            //TODO: Complete this

            return new GaPureRotorsSequence<double>(
                Float64ScalarProcessor.DefaultProcessor.CreateGaEuclideanProcessor(63)
            );
        }


        private readonly List<GaPureRotor<T>> _rotorsList
            = new();


        public int Count 
            => _rotorsList.Count;

        public IGaSpace Space => Processor;

        public uint VSpaceDimension 
            => Processor.VSpaceDimension;

        public ulong GaSpaceDimension
            => Processor.GaSpaceDimension;

        public ulong MaxBasisBladeId 
            => Processor.MaxBasisBladeId;

        public uint GradesCount 
            => Processor.GradesCount;

        public IEnumerable<uint> Grades 
            => Processor.Grades;

        public ILaProcessor<T> ScalarsGridProcessor 
            => Processor;
        
        public IGaKVectorStorage<T> MappedPseudoScalar { get; }

        public IGaProcessor<T> Processor { get; }

        public IGaMultivectorStorage<T> Multivector { get; }

        public IGaMultivectorStorage<T> MultivectorReverse { get; }
        
        public GaPureRotor<T> this[int index]
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


        private GaPureRotorsSequence([NotNull] IGaProcessor<T> processor)
        {
            Processor = processor;
        }

        private GaPureRotorsSequence([NotNull] IGaProcessor<T> processor, IEnumerable<GaPureRotor<T>> rotorsList)
        {
            Processor = processor;

            _rotorsList.AddRange(rotorsList);
        }


        public bool ValidateRotation(GaVectorsFrame<T> sourceFrame, GaVectorsFrame<T> targetFrame)
        {
            if (sourceFrame.Count != targetFrame.Count)
                return false;

            var rotatedFrame = Rotate(sourceFrame);

            return !rotatedFrame.Select(
                (v, i) => !Processor.IsZero(Processor.Subtract(targetFrame[i], v))
            ).Any();
        }

        public bool IsRotorsSequence()
        {
            return _rotorsList.All(r => Processor.IsEuclideanRotor(r.Multivector));
        }

        public bool IsSimpleRotorsSequence()
        {
            return _rotorsList.All(r => Processor.IsSimpleEuclideanRotor(r.Multivector));
        }

        public GaPureRotor<T> GetRotor(int index)
        {
            return _rotorsList[index];
        }

        public GaPureRotorsSequence<T> AppendRotor(GaPureRotor<T> rotor)
        {
            _rotorsList.Add(rotor);

            return this;
        }

        public GaPureRotorsSequence<T> PrependRotor(GaPureRotor<T> rotor)
        {
            _rotorsList.Insert(0, rotor);

            return this;
        }

        public GaPureRotorsSequence<T> InsertRotor(int index, GaPureRotor<T> rotor)
        {
            _rotorsList.Insert(index, rotor);

            return this;
        }

        public GaPureRotorsSequence<T> GetSubSequence(int startIndex, int count)
        {
            return new GaPureRotorsSequence<T>(
                Processor,
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

        public IEnumerable<GaVector<T>> GetRotations(GaVector<T> vector)
        {
            var v = vector;

            yield return v;

            foreach (var rotor in _rotorsList)
            {
                v = rotor.MapVector(v);

                yield return v;
            }
        }

        public IEnumerable<GaVectorsFrame<T>> GetRotations(GaVectorsFrame<T> frame)
        {
            var f = frame;

            yield return f;

            foreach (var rotor in _rotorsList)
            {
                f = rotor.MapVectorsFrame(f);

                yield return f;
            }
        }

        public IEnumerable<T[,]> GetRotationMatrices(int rowsCount)
        {
            var f = 
                Processor.CreateBasisVectorsFrame((uint) rowsCount);

            yield return f.GetMatrix(rowsCount);

            foreach (var rotor in _rotorsList)
                yield return rotor.MapVectorsFrame(f).GetMatrix(rowsCount);
        }

        public GaVector<T> Map(GaVector<T> vector)
        {
            return Processor.CreateGaVector(
                MapVector(vector.VectorStorage)
            );
        }
        

        public IGaOutermorphism<T> GetAdjoint()
        {
            return new GaPureRotorsSequence<T>(
                Processor,
                _rotorsList
                    .Select(r => r.GetReverseRotor())
                    .Reverse()
            );
        }

        public IGaVectorStorage<T> MapBasisVector(int index)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<IGaVectorStorage<T>> GetMappedBasisVectors()
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

        public IGaKVectorStorage<T> MapBasisBlade(uint grade, ulong index)
        {
            throw new NotImplementedException();
        }

        public IGaScalarStorage<T> MapScalar(IGaScalarStorage<T> storage)
        {
            throw new NotImplementedException();
        }

        public IGaKVectorStorage<T> MapTerm(IGaKVectorStorage<T> storage)
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

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorSparseStorage<T> storage)
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

        public GaVectorsFrame<T> Rotate(GaVectorsFrame<T> frame)
        {
            return _rotorsList
                .Aggregate(
                    frame, 
                    (current, rotor) => rotor.MapVectorsFrame(current)
                );
        }

        public GaRotor<T> GetFinalRotor()
        {
            var storage = _rotorsList
                .Skip(1)
                .Select(r => r.Multivector)
                .Aggregate(
                    _rotorsList[0].Multivector, 
                    (current, rotor) => 
                        Processor.EGp(rotor, current)
                );

            return GaRotor<T>.Create(Processor, storage);
        }

        public T[,] GetFinalMatrix(int rowsCount)
        {
            return Rotate(
                Processor.CreateBasisVectorsFrame((uint) rowsCount)
            ).GetMatrix(rowsCount);
        }

        public GaPureRotorsSequence<T> Reverse()
        {
            var rotorsSequence = new GaPureRotorsSequence<T>(Processor);

            foreach (var rotor in _rotorsList)
                rotorsSequence.PrependRotor(rotor.GetReverseRotor());

            return rotorsSequence;
        }

        public IEnumerator<GaPureRotor<T>> GetEnumerator()
        {
            return _rotorsList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}