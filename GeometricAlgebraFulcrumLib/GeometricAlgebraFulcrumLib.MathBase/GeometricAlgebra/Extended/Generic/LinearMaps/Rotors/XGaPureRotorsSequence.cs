using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Frames;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Subspaces;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Rotors
{
    public sealed class XGaPureRotorsSequence<T> : 
        XGaRotorBase<T>, 
        IXGaOutermorphismSequence<T>,
        IReadOnlyList<XGaPureRotor<T>>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaPureRotorsSequence<T> CreateIdentity(XGaProcessor<T> metric)
        {
            return new XGaPureRotorsSequence<T>(
                new[]{ metric.CreateIdentityRotor() }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaPureRotorsSequence<T> Create(params XGaPureRotor<T>[] rotorsList)
        {
            return new XGaPureRotorsSequence<T>(rotorsList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaPureRotorsSequence<T> Create(IEnumerable<XGaPureRotor<T>> rotorsList)
        {
            return new XGaPureRotorsSequence<T>(rotorsList.ToImmutableArray());
        }

        public static XGaPureRotorsSequence<T> CreateFromOrthonormalEuclideanFrames(XGaVectorFrame<T> sourceFrame, XGaVectorFrame<T> targetFrame, bool fullRotorsFlag = false)
        {
            Debug.Assert(targetFrame.Count == sourceFrame.Count);
            Debug.Assert(sourceFrame.IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));
            
            var rotorsSequence = new List<XGaPureRotor<T>>();

            var sourceFrameVectors = sourceFrame.ToArray();

            var n = fullRotorsFlag 
                ? sourceFrame.Count 
                : sourceFrame.Count - 1;
            
            for (var i = 0; i < n; i++)
            {
                var sourceVector = sourceFrameVectors[i];
                var targetVector = targetFrame[i];

                var rotor = 
                    sourceVector.CreatePureRotor(targetVector);

                rotorsSequence.Add(rotor);

                for (var j = i + 1; j < sourceFrame.Count; j++)
                    sourceFrameVectors[j] = rotor.OmMap(sourceFrameVectors[j]);
            }

            return new XGaPureRotorsSequence<T>(rotorsSequence);
        }

        public static XGaPureRotorsSequence<T> CreateFromOrthonormalEuclideanFrames(XGaVectorFrame<T> sourceFrame, XGaVectorFrame<T> targetFrame, int[] sequenceArray)
        {
            Debug.Assert(targetFrame.Count == sourceFrame.Count);
            Debug.Assert(sourceFrame.IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));

            Debug.Assert(sequenceArray.Length == sourceFrame.Count - 1);
            Debug.Assert(sequenceArray.Min() >= 0);
            Debug.Assert(sequenceArray.Max() < sourceFrame.Count);
            Debug.Assert(sequenceArray.Distinct().Count() == sourceFrame.Count - 1);
            
            var rotorsSequence = new List<XGaPureRotor<T>>();

            var sourceFrameVectors = sourceFrame.ToArray();
            
            for (var i = 0; i < sourceFrame.Count - 1; i++)
            {
                var vectorIndex = sequenceArray[i];

                var sourceVector = sourceFrameVectors[vectorIndex];
                var targetVector = targetFrame[vectorIndex];

                var rotor = sourceVector.CreatePureRotor(targetVector);

                rotorsSequence.Add(rotor);

                for (var j = i + 1; j < sourceFrame.Count; j++)
                    sourceFrameVectors[j] = 
                        rotor.OmMap(sourceFrameVectors[j]);
            }

            return new XGaPureRotorsSequence<T>(rotorsSequence);
        }

        public static XGaPureRotorsSequence<T> CreateFromEuclideanFrames(int baseSpaceDimensions, XGaVectorFrame<T> sourceFrame, XGaVectorFrame<T> targetFrame)
        {
            Debug.Assert(targetFrame.Count == sourceFrame.Count);
            //Debug.Assert(IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));

            var processor = sourceFrame.Processor;

            var rotorsSequence = new List<XGaPureRotor<T>>();

            var pseudoScalarSubspace = 
                processor.CreatePseudoScalarSubspace(baseSpaceDimensions);

            var sourceFrameVectors = new XGaVector<T>[sourceFrame.Count];
            var targetFrameVectors = new XGaVector<T>[targetFrame.Count];

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
                    sourceVector.CreatePureRotor(targetVector);

                rotorsSequence.Add(rotor);

                pseudoScalarSubspace = 
                    pseudoScalarSubspace.Complement(targetVector).ToSubspace();

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

            return new XGaPureRotorsSequence<T>(rotorsSequence);
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


        private readonly IReadOnlyList<XGaPureRotor<T>> _rotorsList;


        public int Count 
            => _rotorsList.Count;

        public XGaPureRotor<T> this[int index]
        {
            get => _rotorsList[index];
            //set => _rotorsList[index] = value ?? throw new ArgumentNullException(nameof(value));
        }


        private XGaPureRotorsSequence(IReadOnlyList<XGaPureRotor<T>> rotorsList)
            : base(rotorsList[0].Processor)
        {
            Debug.Assert(rotorsList.Count > 0);

            _rotorsList = rotorsList;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return _rotorsList.All(rotor => rotor.IsValid());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> GetMultivector()
        {
            return _rotorsList
                .Select(r => r.Multivector)
                .Gp();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> GetMultivectorReverse()
        {
            return _rotorsList
                .Select(r => r.Multivector)
                .Reverse()
                .Gp();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> GetMultivectorInverse()
        {
            return GetMultivectorReverse();
        }
        
        public bool ValidateRotation(XGaVectorFrame<T> sourceFrame, XGaVectorFrame<T> targetFrame)
        {
            if (sourceFrame.Count != targetFrame.Count)
                return false;

            var rotatedFrame = Rotate(sourceFrame);

            return !rotatedFrame.Select(
                (v, i) => !(targetFrame[i] - v).IsZero
            ).Any();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaPureRotor<T> GetRotor(int index)
        {
            return _rotorsList[index];
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaPureRotorsSequence<T> GetSubSequence(int startIndex, int count)
        {
            return new XGaPureRotorsSequence<T>(
                _rotorsList.Skip(startIndex).Take(count).ToImmutableArray()
            );
        }

        public IEnumerable<XGaMultivector<T>> GetRotations(XGaMultivector<T> mv)
        {
            var v = mv;

            yield return v;

            foreach (var rotor in _rotorsList)
            {
                v = rotor.Map(v);

                yield return v;
            }
        }

        public IEnumerable<XGaVectorFrame<T>> GetRotations(XGaVectorFrame<T> frame)
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
                Processor.CreateFreeFrameOfBasis(rowsCount);

            yield return f.GetArray(rowsCount);

            foreach (var rotor in _rotorsList)
                yield return rotor.OmMap(f).GetArray(rowsCount);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IXGaRotor<T> GetRotorInverse()
        {
            return new XGaPureRotorsSequence<T>(
                _rotorsList
                    .Select(r => r.GetPureRotorInverse())
                    .Reverse()
                    .ToImmutableArray()
            );
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> OmMap(XGaVector<T> mv)
        {
            return _rotorsList
                .Aggregate(
                    mv, 
                    (bv, rotor) => rotor.OmMap(bv)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> OmMap(XGaBivector<T> mv)
        {
            return _rotorsList
                .Aggregate(
                    mv, 
                    (bv, rotor) => rotor.OmMap(bv)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaHigherKVector<T> OmMap(XGaHigherKVector<T> mv)
        {
            return _rotorsList
                .Aggregate(
                    mv, 
                    (kv, rotor) => rotor.OmMap(kv)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> OmMap(XGaKVector<T> mv)
        {
            return _rotorsList
                .Aggregate(
                    mv, 
                    (kv, rotor) => rotor.OmMap(kv)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> OmMap(XGaMultivector<T> mv)
        {
            return _rotorsList
                .Aggregate(
                    mv, 
                    (current, rotor) => rotor.OmMap(current)
                );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVectorFrame<T> Rotate(XGaVectorFrame<T> frame)
        {
            return _rotorsList
                .Aggregate(
                    frame, 
                    (current, rotor) => rotor.OmMap(current)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaRotor<T> GetFinalRotor()
        {
            var storage = _rotorsList
                .Skip(1)
                .Select(r => r.Multivector)
                .Aggregate(
                    _rotorsList[0].Multivector, 
                    (current, rotor) => 
                        rotor.Gp(current)
                );

            return XGaRotor<T>.Create(storage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T[,] GetFinalRotorArray(int rowsCount)
        {
            return Rotate(
                Processor.CreateFreeFrameOfBasis(rowsCount)
            ).GetArray(rowsCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<XGaPureRotor<T>> GetEnumerator()
        {
            return _rotorsList.GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IXGaOutermorphism<T>> GetLeafOutermorphisms()
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