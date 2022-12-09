using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Frames;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Geometry.Subspaces;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Rotors
{
    public sealed class PureRotorsSequence<T> : 
        RotorBase<T>, 
        IOutermorphismSequence<T>,
        IReadOnlyList<PureRotor<T>>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PureRotorsSequence<T> CreateIdentity(IGeometricAlgebraProcessor<T> processor)
        {
            return new PureRotorsSequence<T>(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PureRotorsSequence<T> Create(IGeometricAlgebraProcessor<T> processor, params PureRotor<T>[] rotorsList)
        {
            return new PureRotorsSequence<T>(processor, rotorsList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PureRotorsSequence<T> Create(IGeometricAlgebraProcessor<T> processor, IEnumerable<PureRotor<T>> rotorsList)
        {
            return new PureRotorsSequence<T>(processor, rotorsList);
        }

        public static PureRotorsSequence<T> CreateFromOrthonormalEuclideanFrames(VectorFrame<T> sourceFrame, VectorFrame<T> targetFrame, bool fullRotorsFlag = false)
        {
            Debug.Assert(targetFrame.Count == sourceFrame.Count);
            Debug.Assert(sourceFrame.IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));

            var processor = (IGeometricAlgebraEuclideanProcessor<T>) sourceFrame.GeometricProcessor;

            var rotorsSequence = new PureRotorsSequence<T>(processor);

            var sourceFrameVectors = sourceFrame.ToArray();

            var n = fullRotorsFlag 
                ? sourceFrame.Count 
                : sourceFrame.Count - 1;
            
            for (var i = 0; i < n; i++)
            {
                var sourceVector = sourceFrameVectors[i];
                var targetVector = targetFrame[i];

                var rotor = 
                    processor.CreatePureRotor(sourceVector, targetVector);

                rotorsSequence.AppendRotor(rotor);

                for (var j = i + 1; j < sourceFrame.Count; j++)
                    sourceFrameVectors[j] = rotor.OmMap(sourceFrameVectors[j]);
            }

            return rotorsSequence;
        }

        public static PureRotorsSequence<T> CreateFromOrthonormalEuclideanFrames(VectorFrame<T> sourceFrame, VectorFrame<T> targetFrame, int[] sequenceArray)
        {
            Debug.Assert(targetFrame.Count == sourceFrame.Count);
            Debug.Assert(sourceFrame.IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));

            Debug.Assert(sequenceArray.Length == sourceFrame.Count - 1);
            Debug.Assert(sequenceArray.Min() >= 0);
            Debug.Assert(sequenceArray.Max() < sourceFrame.Count);
            Debug.Assert(sequenceArray.Distinct().Count() == sourceFrame.Count - 1);

            var processor = (IGeometricAlgebraEuclideanProcessor<T>) sourceFrame.GeometricProcessor;

            var rotorsSequence = new PureRotorsSequence<T>(processor);

            var sourceFrameVectors = sourceFrame.ToArray();
            
            for (var i = 0; i < sourceFrame.Count - 1; i++)
            {
                var vectorIndex = sequenceArray[i];

                var sourceVector = sourceFrameVectors[vectorIndex];
                var targetVector = targetFrame[vectorIndex];

                var rotor = processor.CreatePureRotor(
                    sourceVector, 
                    targetVector
                );

                rotorsSequence.AppendRotor(rotor);

                for (var j = i + 1; j < sourceFrame.Count; j++)
                    sourceFrameVectors[j] = 
                        rotor.OmMap(sourceFrameVectors[j]);
            }

            return rotorsSequence;
        }

        public static PureRotorsSequence<T> CreateFromEuclideanFrames(uint baseSpaceDimensions, VectorFrame<T> sourceFrame, VectorFrame<T> targetFrame)
        {
            Debug.Assert(targetFrame.Count == sourceFrame.Count);
            //Debug.Assert(IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));

            var processor = (IGeometricAlgebraEuclideanProcessor<T>) sourceFrame.GeometricProcessor;

            var rotorsSequence = 
                new PureRotorsSequence<T>(processor);

            var pseudoScalarSubspace = 
                Subspace<T>.CreateFromPseudoScalar(processor, baseSpaceDimensions);

            var sourceFrameVectors = new GaVector<T>[sourceFrame.Count];
            var targetFrameVectors = new GaVector<T>[targetFrame.Count];

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
                    processor.CreatePureRotor(sourceVector, targetVector);

                rotorsSequence.AppendRotor(rotor);

                pseudoScalarSubspace = 
                    pseudoScalarSubspace.Complement(targetVector).GetSubspace();

                for (var j = i + 1; j < sourceFrame.Count; j++)
                {
                    sourceFrameVectors[j] =
                        pseudoScalarSubspace
                            .Project(rotor.OmMap(sourceFrameVectors[j]));

                    targetFrameVectors[j] =
                        pseudoScalarSubspace
                            .Project(targetFrameVectors[j]);
                }
            }

            return rotorsSequence;
        }

        //public static PureRotorsSequence<double> CreateOrthogonalRotors(double[,] rotationMatrix)
        //{
        //    var evdSolver = Matrix<double>.Build.DenseOfArray(rotationMatrix).Evd();

        //    var eigenValuesReal = evdSolver.EigenValues.Real();
        //    var eigenValuesImag = evdSolver.EigenValues.Imaginary();
        //    var eigenVectors = evdSolver.EigenVectors;

        //    //TODO: Complete this

        //    return new PureRotorsSequence<double>(
        //        ScalarAlgebraFloat64Processor.DefaultProcessor.CreateGeometricAlgebraEuclideanProcessor(63)
        //    );
        //}


        private readonly List<PureRotor<T>> _rotorsList
            = new List<PureRotor<T>>();


        public int Count 
            => _rotorsList.Count;

        public PureRotor<T> this[int index]
        {
            get => _rotorsList[index];
            set => _rotorsList[index] = 
                value 
                ?? throw new ArgumentNullException(nameof(value));
        }


        private PureRotorsSequence([NotNull] IGeometricAlgebraProcessor<T> processor)
            : base(processor)
        {
        }

        private PureRotorsSequence([NotNull] IGeometricAlgebraProcessor<T> processor, IEnumerable<PureRotor<T>> rotorsList)
            : base(processor)
        {
            _rotorsList.AddRange(rotorsList);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return _rotorsList.All(rotor => rotor.IsValid());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaMultivector<T> GetMultivector()
        {
            return GeometricProcessor.CreateMultivector(
                GetMultivectorStorage()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaMultivector<T> GetMultivectorReverse()
        {
            return GeometricProcessor.CreateMultivector(
                GetMultivectorStorageReverse()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaMultivector<T> GetMultivectorInverse()
        {
            return GeometricProcessor.CreateMultivector(
                GetMultivectorStorageReverse()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> GetMultivectorStorage()
        {
            return _rotorsList
                .Select(r => r.Multivector.MultivectorStorage)
                .Aggregate(
                    (IMultivectorStorage<T>) GeometricProcessor.CreateKVectorStorageBasisScalar(),
                    GeometricProcessor.Gp
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> GetMultivectorStorageReverse()
        {
            return _rotorsList
                .Select(r => r.MultivectorReverse.MultivectorStorage)
                .Reverse()
                .Aggregate(
                    (IMultivectorStorage<T>) GeometricProcessor.CreateKVectorStorageBasisScalar(),
                    GeometricProcessor.Gp
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> GetMultivectorStorageInverse()
        {
            return GetMultivectorStorageReverse();
        }

        public bool ValidateRotation(VectorFrame<T> sourceFrame, VectorFrame<T> targetFrame)
        {
            if (sourceFrame.Count != targetFrame.Count)
                return false;

            var rotatedFrame = Rotate(sourceFrame);

            return !rotatedFrame.Select(
                (v, i) => !(targetFrame[i] - v).IsZero()
            ).Any();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PureRotor<T> GetRotor(int index)
        {
            return _rotorsList[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PureRotorsSequence<T> AppendRotor([NotNull] PureRotor<T> rotor)
        {
            _rotorsList.Add(rotor);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PureRotorsSequence<T> PrependRotor([NotNull] PureRotor<T> rotor)
        {
            _rotorsList.Insert(0, rotor);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PureRotorsSequence<T> InsertRotor(int index, [NotNull] PureRotor<T> rotor)
        {
            _rotorsList.Insert(index, rotor);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PureRotorsSequence<T> GetSubSequence(int startIndex, int count)
        {
            return new PureRotorsSequence<T>(
                GeometricProcessor,
                _rotorsList.Skip(startIndex).Take(count)
            );
        }

        public IEnumerable<IMultivectorStorage<T>> GetRotations([NotNull] IMultivectorStorage<T> storage)
        {
            var v = storage;

            yield return v;

            foreach (var rotor in _rotorsList)
            {
                v = rotor.Map(v);

                yield return v;
            }
        }

        public IEnumerable<VectorFrame<T>> GetRotations(VectorFrame<T> frame)
        {
            var f = frame;

            yield return f;

            foreach (var rotor in _rotorsList)
            {
                f = rotor.OmMap(f);

                yield return f;
            }
        }

        public IEnumerable<T[,]> GetRotationMatrices(int rowsCount)
        {
            var f = 
                GeometricProcessor.CreateFreeFrameOfBasis((uint) rowsCount);

            yield return f.GetArray(rowsCount);

            foreach (var rotor in _rotorsList)
                yield return rotor.OmMap(f).GetArray(rowsCount);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IRotor<T> GetRotorInverse()
        {
            return new PureRotorsSequence<T>(
                GeometricProcessor,
                _rotorsList
                    .Select(r => r.GetPureRotorInverse())
                    .Reverse()
            );
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaVector<T> OmMap(GaVector<T> mv)
        {
            return _rotorsList
                .Aggregate(
                    mv, 
                    (bv, rotor) => rotor.OmMap(bv)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaBivector<T> OmMap(GaBivector<T> mv)
        {
            return _rotorsList
                .Aggregate(
                    mv, 
                    (bv, rotor) => rotor.OmMap(bv)
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaKVector<T> OmMap(GaKVector<T> mv)
        {
            return _rotorsList
                .Aggregate(
                    mv, 
                    (kv, rotor) => rotor.OmMap(kv)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaMultivector<T> OmMap(GaMultivector<T> mv)
        {
            return _rotorsList
                .Aggregate(
                    mv, 
                    (current, rotor) => rotor.OmMap(current)
                );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorFrame<T> Rotate(VectorFrame<T> frame)
        {
            return _rotorsList
                .Aggregate(
                    frame, 
                    (current, rotor) => rotor.OmMap(current)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Rotor<T> GetFinalRotor()
        {
            var storage = _rotorsList
                .Skip(1)
                .Select(r => r.Multivector)
                .Aggregate(
                    _rotorsList[0].Multivector, 
                    (current, rotor) => 
                        rotor.Gp(current)
                );

            return Rotor<T>.Create(storage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T[,] GetFinalRotorArray(int rowsCount)
        {
            return Rotate(
                GeometricProcessor.CreateFreeFrameOfBasis((uint) rowsCount)
            ).GetArray(rowsCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<PureRotor<T>> GetEnumerator()
        {
            return _rotorsList.GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IGaOutermorphism<T>> GetLeafOutermorphisms()
        {
            return _rotorsList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Scalar<T> GetScalingFactor()
        {
            return ScalarProcessor.CreateScalarOne();
        }
    }
}