using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Geometry.Multivectors;
using GeometricAlgebraFulcrumLib.Processing;
using GeometricAlgebraFulcrumLib.Processing.Generic;
using GeometricAlgebraFulcrumLib.Processing.Implementations.Float64;
using GeometricAlgebraFulcrumLib.Processing.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean
{
    public sealed class GaEuclideanSimpleRotorsSequence<T>
        : IGaEuclideanGeometry<T>, IGaRotor<T>, IReadOnlyList<GaEuclideanSimpleRotor<T>>
    {
        public static GaEuclideanSimpleRotorsSequence<T> CreateIdentity(IGaProcessor<T> processor)
        {
            return new GaEuclideanSimpleRotorsSequence<T>(processor);
        }

        public static GaEuclideanSimpleRotorsSequence<T> Create(IGaProcessor<T> processor, params GaEuclideanSimpleRotor<T>[] rotorsList)
        {
            return new GaEuclideanSimpleRotorsSequence<T>(processor, rotorsList);
        }

        public static GaEuclideanSimpleRotorsSequence<T> Create(IGaProcessor<T> processor, IEnumerable<GaEuclideanSimpleRotor<T>> rotorsList)
        {
            return new GaEuclideanSimpleRotorsSequence<T>(processor, rotorsList);
        }

        public static GaEuclideanSimpleRotorsSequence<T> CreateFromOrthonormalFrames(GaVectorsFrame<T> sourceFrame, GaVectorsFrame<T> targetFrame, bool fullRotorsFlag = false)
        {
            Debug.Assert(targetFrame.Count == sourceFrame.Count);
            Debug.Assert(sourceFrame.IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));

            var processor = sourceFrame.Processor;

            var rotorsSequence = new GaEuclideanSimpleRotorsSequence<T>(processor);

            var sourceFrameVectors = sourceFrame.ToArray();

            var n = fullRotorsFlag 
                ? sourceFrame.Count 
                : sourceFrame.Count - 1;
            
            for (var i = 0; i < n; i++)
            {
                var sourceVector = sourceFrameVectors[i];
                var targetVector = targetFrame[i];

                var rotor = 
                    GaEuclideanSimpleRotor<T>.Create(processor, sourceVector, targetVector);

                rotorsSequence.AppendRotor(rotor);

                for (var j = i + 1; j < sourceFrame.Count; j++)
                    sourceFrameVectors[j] = rotor.MapVector(sourceFrameVectors[j]);
            }

            return rotorsSequence;
        }

        public static GaEuclideanSimpleRotorsSequence<T> CreateFromOrthonormalFrames(GaVectorsFrame<T> sourceFrame, GaVectorsFrame<T> targetFrame, int[] sequenceArray)
        {
            Debug.Assert(targetFrame.Count == sourceFrame.Count);
            Debug.Assert(sourceFrame.IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));

            Debug.Assert(sequenceArray.Length == sourceFrame.Count - 1);
            Debug.Assert(sequenceArray.Min() >= 0);
            Debug.Assert(sequenceArray.Max() < sourceFrame.Count);
            Debug.Assert(sequenceArray.Distinct().Count() == sourceFrame.Count - 1);

            var processor = sourceFrame.Processor;

            var rotorsSequence = new GaEuclideanSimpleRotorsSequence<T>(processor);

            var sourceFrameVectors = sourceFrame.ToArray();
            
            for (var i = 0; i < sourceFrame.Count - 1; i++)
            {
                var vectorIndex = sequenceArray[i];

                var sourceVector = sourceFrameVectors[vectorIndex];
                var targetVector = targetFrame[vectorIndex];

                var rotor = GaEuclideanSimpleRotor<T>.Create(
                    processor,
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

        public static GaEuclideanSimpleRotorsSequence<T> CreateFromFrames(uint baseSpaceDimensions, GaVectorsFrame<T> sourceFrame, GaVectorsFrame<T> targetFrame)
        {
            Debug.Assert(targetFrame.Count == sourceFrame.Count);
            //Debug.Assert(IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));

            var processor = sourceFrame.Processor;

            var rotorsSequence = 
                new GaEuclideanSimpleRotorsSequence<T>(processor);

            var pseudoScalarSubspace = 
                GaSubspace<T>.CreateFromPseudoScalar(processor, baseSpaceDimensions);

            var sourceFrameVectors = new IGasVector<T>[sourceFrame.Count];
            var targetFrameVectors = new IGasVector<T>[targetFrame.Count];

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
                    GaEuclideanSimpleRotor<T>.Create(processor, sourceVector, targetVector);

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

        public static GaEuclideanSimpleRotorsSequence<double> CreateOrthogonalRotors(double[,] rotationMatrix)
        {
            var evdSolver = Matrix<double>.Build.DenseOfArray(rotationMatrix).Evd();

            var eigenValuesReal = evdSolver.EigenValues.Real();
            var eigenValuesImag = evdSolver.EigenValues.Imaginary();
            var eigenVectors = evdSolver.EigenVectors;

            //TODO: Complete this

            return new GaEuclideanSimpleRotorsSequence<double>(
                GaProcessorGenericOrthonormal<double>.CreateEuclidean(
                    GaScalarProcessorFloat64.DefaultProcessor,
                    63
                )
            );
        }


        private readonly List<GaEuclideanSimpleRotor<T>> _rotorsList
            = new();


        public int Count 
            => _rotorsList.Count;

        public uint VSpaceDimension 
            => Processor.VSpaceDimension;

        public ulong GaSpaceDimension
            => Processor.GaSpaceDimension;

        public ulong MaxBasisBladeId { get; }

        public uint GradesCount { get; }
        
        public IEnumerable<uint> Grades { get; }

        public IGaScalarProcessor<T> ScalarProcessor { get; }
        
        public IGasKVector<T> MappedPseudoScalar { get; }

        public IGaProcessor<T> Processor { get; }

        public IGasMultivector<T> Rotor { get; }

        public IGasMultivector<T> RotorReverse { get; }
        
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


        private GaEuclideanSimpleRotorsSequence([NotNull] IGaProcessor<T> processor)
        {
            Processor = processor;
        }

        private GaEuclideanSimpleRotorsSequence([NotNull] IGaProcessor<T> processor, IEnumerable<GaEuclideanSimpleRotor<T>> rotorsList)
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
                (v, i) => !targetFrame[i].Subtract(v).IsZero()
            ).Any();
        }

        public bool IsRotorsSequence()
        {
            return _rotorsList.All(r => r.Rotor.IsEuclideanRotor());
        }

        public bool IsSimpleRotorsSequence()
        {
            return _rotorsList.All(r => r.Rotor.IsSimpleEuclideanRotor());
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
            return new GaEuclideanSimpleRotorsSequence<T>(
                Processor,
                _rotorsList.Skip(startIndex).Take(count)
            );
        }

        public IEnumerable<IGasMultivector<T>> GetRotations(IGasMultivector<T> storage)
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
                v = v.GetRotatedVector(rotor);

                yield return v;
            }
        }

        public IEnumerable<GaVectorsFrame<T>> GetRotations(GaVectorsFrame<T> frame)
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
                GaVectorsFrame<T>.CreateBasisFrame(Processor, (uint) rowsCount);

            yield return f.GetMatrix(rowsCount);

            foreach (var rotor in _rotorsList)
                yield return f.GetRotatedFrame(rotor).GetMatrix(rowsCount);
        }

        public GaVector<T> Map(GaVector<T> vector)
        {
            return GaVector<T>.Create(
                Processor,
                MapVector(vector.Storage)
            );
        }
        

        public IGaOutermorphism<T> GetAdjoint()
        {
            return new GaEuclideanSimpleRotorsSequence<T>(
                Processor,
                _rotorsList
                    .Select(r => r.GetReverseRotor())
                    .Reverse()
            );
        }

        public IGasVector<T> MapBasisVector(int index)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<IGasVector<T>> GetMappedBasisVectors()
        {
            throw new NotImplementedException();
        }

        public IGasVector<T> MapBasisVector(ulong index)
        {
            throw new NotImplementedException();
        }

        public IGasBivector<T> MapBasisBivector(int index1, int index2)
        {
            throw new NotImplementedException();
        }

        public IGasBivector<T> MapBasisBivector(ulong index1, ulong index2)
        {
            throw new NotImplementedException();
        }

        public IGasKVector<T> MapBasisBlade(ulong id)
        {
            throw new NotImplementedException();
        }

        public IGasKVector<T> MapBasisBlade(uint grade, ulong index)
        {
            throw new NotImplementedException();
        }

        public IGasScalar<T> MapScalar(IGasScalar<T> storage)
        {
            throw new NotImplementedException();
        }

        public IGasKVector<T> MapTerm(IGasKVectorTerm<T> storage)
        {
            throw new NotImplementedException();
        }

        public IGasVector<T> MapVector(IGasVector<T> storage)
        {
            throw new NotImplementedException();
        }

        public IGasBivector<T> MapBivector(IGasBivector<T> storage)
        {
            throw new NotImplementedException();
        }

        public IGasKVector<T> MapKVector(IGasKVector<T> storage)
        {
            throw new NotImplementedException();
        }

        public IGasMultivector<T> MapMultivector(IGasGradedMultivector<T> storage)
        {
            throw new NotImplementedException();
        }

        public IGasMultivector<T> MapMultivector(IGasTermsMultivector<T> storage)
        {
            throw new NotImplementedException();
        }

        public IGasMultivector<T> MapMultivector(IGasMultivector<T> storage)
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
                    (current, rotor) => current.GetRotatedFrame(rotor)
                );
        }

        public GaRotor<T> GetFinalRotor()
        {
            var storage = _rotorsList
                .Skip(1)
                .Select(r => r.Rotor)
                .Aggregate(
                    _rotorsList[0].Rotor, 
                    (current, rotor) => rotor.EGp(current)
                );

            return GaRotor<T>.Create(Processor, storage);
        }

        public T[,] GetFinalMatrix(int rowsCount)
        {
            return Rotate(
                GaVectorsFrame<T>.CreateBasisFrame(Processor, (uint) rowsCount)
            ).GetMatrix(rowsCount);
        }

        public GaEuclideanSimpleRotorsSequence<T> Reverse()
        {
            var rotorsSequence = new GaEuclideanSimpleRotorsSequence<T>(Processor);

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