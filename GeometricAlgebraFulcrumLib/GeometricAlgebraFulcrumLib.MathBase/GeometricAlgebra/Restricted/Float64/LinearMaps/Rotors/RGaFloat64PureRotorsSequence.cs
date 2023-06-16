using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Frames;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Subspaces;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.SpaceND;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.LinearMaps.Rotors
{
    public sealed class RGaFloat64PureRotorsSequence : 
        RGaFloat64RotorBase, 
        IRGaFloat64OutermorphismSequence,
        IReadOnlyList<RGaFloat64PureRotor>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64PureRotorsSequence CreateIdentity(RGaFloat64Processor metric)
        {
            return new RGaFloat64PureRotorsSequence(
                new[]{ metric.CreateIdentityRotor() }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64PureRotorsSequence Create(params RGaFloat64PureRotor[] rotorsList)
        {
            return new RGaFloat64PureRotorsSequence(rotorsList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64PureRotorsSequence Create(IEnumerable<RGaFloat64PureRotor> rotorsList)
        {
            return new RGaFloat64PureRotorsSequence(rotorsList.ToImmutableArray());
        }

        public static RGaFloat64PureRotorsSequence CreateFromOrthonormalEuclideanFrames(RGaFloat64VectorFrame sourceFrame, RGaFloat64VectorFrame targetFrame, bool fullRotorsFlag = false)
        {
            Debug.Assert(targetFrame.Count == sourceFrame.Count);
            Debug.Assert(sourceFrame.IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));
            
            var rotorsSequence = new List<RGaFloat64PureRotor>();

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

            return new RGaFloat64PureRotorsSequence(rotorsSequence);
        }

        public static RGaFloat64PureRotorsSequence CreateFromOrthonormalEuclideanFrames(RGaFloat64VectorFrame sourceFrame, RGaFloat64VectorFrame targetFrame, int[] sequenceArray)
        {
            Debug.Assert(targetFrame.Count == sourceFrame.Count);
            Debug.Assert(sourceFrame.IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));

            Debug.Assert(sequenceArray.Length == sourceFrame.Count - 1);
            Debug.Assert(sequenceArray.Min() >= 0);
            Debug.Assert(sequenceArray.Max() < sourceFrame.Count);
            Debug.Assert(sequenceArray.Distinct().Count() == sourceFrame.Count - 1);
            
            var rotorsSequence = new List<RGaFloat64PureRotor>();

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

            return new RGaFloat64PureRotorsSequence(rotorsSequence);
        }

        public static RGaFloat64PureRotorsSequence CreateFromEuclideanFrames(int baseSpaceDimensions, RGaFloat64VectorFrame sourceFrame, RGaFloat64VectorFrame targetFrame)
        {
            Debug.Assert(targetFrame.Count == sourceFrame.Count);
            //Debug.Assert(IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));

            var metric = sourceFrame.Processor;

            var rotorsSequence = new List<RGaFloat64PureRotor>();

            var pseudoScalarSubspace = 
                metric.CreatePseudoScalarSubspace(baseSpaceDimensions);

            var sourceFrameVectors = new RGaFloat64Vector[sourceFrame.Count];
            var targetFrameVectors = new RGaFloat64Vector[targetFrame.Count];

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

            return new RGaFloat64PureRotorsSequence(rotorsSequence);
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


        private readonly IReadOnlyList<RGaFloat64PureRotor> _rotorsList;


        public int Count 
            => _rotorsList.Count;

        public RGaFloat64PureRotor this[int index]
        {
            get => _rotorsList[index];
            //set => _rotorsList[index] = value ?? throw new ArgumentNullException(nameof(value));
        }


        private RGaFloat64PureRotorsSequence(IReadOnlyList<RGaFloat64PureRotor> rotorsList)
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
        public override RGaFloat64Multivector GetMultivector()
        {
            return _rotorsList
                .Select(r => r.Multivector)
                .Gp();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector GetMultivectorReverse()
        {
            return _rotorsList
                .Select(r => r.Multivector)
                .Reverse()
                .Gp();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector GetMultivectorInverse()
        {
            return GetMultivectorReverse();
        }
        
        public bool ValidateRotation(RGaFloat64VectorFrame sourceFrame, RGaFloat64VectorFrame targetFrame)
        {
            if (sourceFrame.Count != targetFrame.Count)
                return false;

            var rotatedFrame = Rotate(sourceFrame);

            return !rotatedFrame.Select(
                (v, i) => !(targetFrame[i] - v).IsZero
            ).Any();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64PureRotor GetRotor(int index)
        {
            return _rotorsList[index];
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64PureRotorsSequence GetSubSequence(int startIndex, int count)
        {
            return new RGaFloat64PureRotorsSequence(
                _rotorsList.Skip(startIndex).Take(count).ToImmutableArray()
            );
        }

        public IEnumerable<RGaFloat64Multivector> GetRotations(RGaFloat64Multivector mv)
        {
            var v = mv;

            yield return v;

            foreach (var rotor in _rotorsList)
            {
                v = rotor.Map(v);

                yield return v;
            }
        }

        public IEnumerable<RGaFloat64VectorFrame> GetRotations(RGaFloat64VectorFrame frame)
        {
            var f = frame;

            yield return f;

            foreach (var rotor in _rotorsList)
            {
                f = rotor.OmMap(f);

                yield return f;
            }
        }

        public IEnumerable<double[,]> GetRotationMatrices(int rowsCount)
        {
            var f = 
                Processor.CreateFreeFrameOfBasis(rowsCount);

            yield return f.GetArray(rowsCount);

            foreach (var rotor in _rotorsList)
                yield return rotor.OmMap(f).GetArray(rowsCount);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IRGaFloat64Rotor GetRotorInverse()
        {
            return new RGaFloat64PureRotorsSequence(
                _rotorsList
                    .Select(r => r.GetPureRotorInverse())
                    .Reverse()
                    .ToImmutableArray()
            );
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Vector OmMap(RGaFloat64Vector mv)
        {
            return _rotorsList
                .Aggregate(
                    mv, 
                    (bv, rotor) => rotor.OmMap(bv)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Bivector OmMap(RGaFloat64Bivector mv)
        {
            return _rotorsList
                .Aggregate(
                    mv, 
                    (bv, rotor) => rotor.OmMap(bv)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64HigherKVector OmMap(RGaFloat64HigherKVector mv)
        {
            return _rotorsList
                .Aggregate(
                    mv, 
                    (kv, rotor) => rotor.OmMap(kv)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64KVector OmMap(RGaFloat64KVector mv)
        {
            return _rotorsList
                .Aggregate(
                    mv, 
                    (kv, rotor) => rotor.OmMap(kv)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector OmMap(RGaFloat64Multivector mv)
        {
            return _rotorsList
                .Aggregate(
                    mv, 
                    (current, rotor) => rotor.OmMap(current)
                );
        }

        public override LinFloat64UnilinearMap GetVectorMapPart(int vSpaceDimensions)
        {
            throw new NotImplementedException();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64VectorFrame Rotate(RGaFloat64VectorFrame frame)
        {
            return _rotorsList
                .Aggregate(
                    frame, 
                    (current, rotor) => rotor.OmMap(current)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Rotor GetFinalRotor()
        {
            var storage = _rotorsList
                .Skip(1)
                .Select(r => r.Multivector)
                .Aggregate(
                    _rotorsList[0].Multivector, 
                    (current, rotor) => 
                        rotor.Gp(current)
                );

            return RGaFloat64Rotor.Create(storage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[,] GetFinalRotorArray(int rowsCount)
        {
            return Rotate(
                Processor.CreateFreeFrameOfBasis(rowsCount)
            ).GetArray(rowsCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<RGaFloat64PureRotor> GetEnumerator()
        {
            return _rotorsList.GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IRGaFloat64Outermorphism> GetLeafOutermorphisms()
        {
            return _rotorsList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double GetScalingFactor()
        {
            return 1d;
        }
    }
}