using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Frames;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Subspaces;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.LinearMaps.Rotors
{
    public sealed class XGaFloat64ScaledPureRotorsSequence : 
        XGaFloat64ScaledRotorBase, 
        IXGaFloat64OutermorphismSequence,
        IReadOnlyList<XGaFloat64ScaledPureRotor>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64ScaledPureRotorsSequence CreateIdentity(XGaFloat64Processor metric)
        {
            return new XGaFloat64ScaledPureRotorsSequence(
                new []{ XGaFloat64ScaledPureRotor.CreateIdentity(metric) }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64ScaledPureRotorsSequence Create(params XGaFloat64ScaledPureRotor[] rotorsList)
        {
            return new XGaFloat64ScaledPureRotorsSequence(rotorsList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64ScaledPureRotorsSequence Create(IEnumerable<XGaFloat64ScaledPureRotor> rotorsList)
        {
            return new XGaFloat64ScaledPureRotorsSequence(rotorsList.ToImmutableArray());
        }

        public static XGaFloat64ScaledPureRotorsSequence CreateFromOrthonormalEuclideanFrames(XGaFloat64VectorFrame sourceFrame, XGaFloat64VectorFrame targetFrame, bool fullScaledRotorsFlag = false)
        {
            Debug.Assert(targetFrame.Count == sourceFrame.Count);
            Debug.Assert(sourceFrame.IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));
            
            var rotorsSequence = 
                new List<XGaFloat64ScaledPureRotor>();

            var sourceFrameVectors = sourceFrame.ToArray();

            var n = fullScaledRotorsFlag 
                ? sourceFrame.Count 
                : sourceFrame.Count - 1;
            
            for (var i = 0; i < n; i++)
            {
                var sourceVector = sourceFrameVectors[i];
                var targetVector = targetFrame[i];

                var rotor = 
                    sourceVector.CreateScaledPureRotor(targetVector);

                rotorsSequence.Add(rotor);

                for (var j = i + 1; j < sourceFrame.Count; j++)
                    sourceFrameVectors[j] = rotor.OmMap(sourceFrameVectors[j]);
            }

            return new XGaFloat64ScaledPureRotorsSequence(rotorsSequence);
        }

        public static XGaFloat64ScaledPureRotorsSequence CreateFromOrthonormalEuclideanFrames(XGaFloat64VectorFrame sourceFrame, XGaFloat64VectorFrame targetFrame, int[] sequenceArray)
        {
            Debug.Assert(targetFrame.Count == sourceFrame.Count);
            Debug.Assert(sourceFrame.IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));

            Debug.Assert(sequenceArray.Length == sourceFrame.Count - 1);
            Debug.Assert(sequenceArray.Min() >= 0);
            Debug.Assert(sequenceArray.Max() < sourceFrame.Count);
            Debug.Assert(sequenceArray.Distinct().Count() == sourceFrame.Count - 1);
            
            var rotorsSequence = 
                new List<XGaFloat64ScaledPureRotor>();
            
            var sourceFrameVectors = sourceFrame.ToArray();
            
            for (var i = 0; i < sourceFrame.Count - 1; i++)
            {
                var vectorIndex = sequenceArray[i];

                var sourceVector = sourceFrameVectors[vectorIndex];
                var targetVector = targetFrame[vectorIndex];

                var rotor = sourceVector.CreateScaledPureRotor(targetVector);

                rotorsSequence.Add(rotor);

                for (var j = i + 1; j < sourceFrame.Count; j++)
                    sourceFrameVectors[j] = 
                        rotor.OmMap(sourceFrameVectors[j]);
            }

            return new XGaFloat64ScaledPureRotorsSequence(rotorsSequence);
        }

        public static XGaFloat64ScaledPureRotorsSequence CreateFromEuclideanFrames(int baseSpaceDimensions, XGaFloat64VectorFrame sourceFrame, XGaFloat64VectorFrame targetFrame)
        {
            Debug.Assert(targetFrame.Count == sourceFrame.Count);
            //Debug.Assert(IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));
            
            var rotorsSequence = 
                new List<XGaFloat64ScaledPureRotor>();

            var metric = sourceFrame.Processor;

            var pseudoScalarSubspace = 
                metric.CreatePseudoScalarSubspace(baseSpaceDimensions);

            var sourceFrameVectors = new XGaFloat64Vector[sourceFrame.Count];
            var targetFrameVectors = new XGaFloat64Vector[targetFrame.Count];

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
                    sourceVector.CreateScaledPureRotor(targetVector);

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

            return new XGaFloat64ScaledPureRotorsSequence(rotorsSequence);
        }

        //public static PureScaledRotorsSequence<double> CreateOrthogonalScaledRotors(double[,] rotationMatrix)
        //{
        //    var evdSolver = Matrix<double>.Build.DenseOfArray(rotationMatrix).Evd();

        //    var eigenValuesReal = evdSolver.EigenValues.Real();
        //    var eigenValuesImag = evdSolver.EigenValues.Imaginary();
        //    var eigenVectors = evdSolver.EigenVectors;

        //    //TODO: Complete this

        //    return new PureScaledRotorsSequence<double>(
        //        ScalarAlgebraFloat64Processor.DefaultProcessor.CreateGeometricAlgebraEuclideanProcessor(63)
        //    );
        //}


        private readonly IReadOnlyList<XGaFloat64ScaledPureRotor> _rotorsList;


        public int Count 
            => _rotorsList.Count;

        public XGaFloat64ScaledPureRotor this[int index]
        {
            get => _rotorsList[index];
            //set => _rotorsList[index] = value ?? throw new ArgumentNullException(nameof(value));
        }

        
        private XGaFloat64ScaledPureRotorsSequence(IReadOnlyList<XGaFloat64ScaledPureRotor> rotorsList)
            : base(rotorsList[0].Processor)
        {
            _rotorsList = rotorsList;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return _rotorsList.All(rotor => rotor.IsValid());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector GetMultivector()
        {
            return _rotorsList
                .Select(r => r.Multivector)
                .Gp();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector GetMultivectorReverse()
        {
            return _rotorsList
                .Select(r => r.MultivectorReverse)
                .Reverse()
                .Gp();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector GetMultivectorInverse()
        {
            return _rotorsList
                .Select(r => r.MultivectorReverse)
                .Reverse()
                .Gp();
        }
        
        public bool ValidateRotation(XGaFloat64VectorFrame sourceFrame, XGaFloat64VectorFrame targetFrame)
        {
            if (sourceFrame.Count != targetFrame.Count)
                return false;

            var rotatedFrame = Rotate(sourceFrame);

            return !rotatedFrame.Select(
                (v, i) => !(targetFrame[i] - v).IsZero
            ).Any();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64ScaledPureRotor GetScaledRotor(int index)
        {
            return _rotorsList[index];
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64ScaledPureRotorsSequence GetSubSequence(int startIndex, int count)
        {
            return new XGaFloat64ScaledPureRotorsSequence(
                _rotorsList.Skip(startIndex).Take(count).ToImmutableArray()
            );
        }

        public IEnumerable<XGaFloat64Multivector> GetRotations(XGaFloat64Multivector storage)
        {
            var v = storage;

            yield return v;

            foreach (var rotor in _rotorsList)
            {
                v = rotor.Map(v);

                yield return v;
            }
        }

        public IEnumerable<XGaFloat64VectorFrame> GetRotations(XGaFloat64VectorFrame frame)
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
        public override double GetScalingFactor()
        {
            return _rotorsList
                .Select(r => r.GetScalingFactor())
                .Aggregate(1d, (d, d1) => d * d1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IXGaFloat64ScaledRotor GetScaledRotorInverse()
        {
            return new XGaFloat64ScaledPureRotorsSequence(
                _rotorsList
                    .Select(r => r.GetPureScaledRotorInverse())
                    .Reverse()
                    .ToImmutableArray()
            );
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Vector OmMap(XGaFloat64Vector mv)
        {
            return _rotorsList
                .Aggregate(
                    mv, 
                    (bv, rotor) => rotor.OmMap(bv)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Bivector OmMap(XGaFloat64Bivector mv)
        {
            return _rotorsList
                .Aggregate(
                    mv, 
                    (bv, rotor) => rotor.OmMap(bv)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64HigherKVector OmMap(XGaFloat64HigherKVector mv)
        {
            return _rotorsList
                .Aggregate(
                    mv, 
                    (kv, rotor) => rotor.OmMap(kv)
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector OmMap(XGaFloat64Multivector mv)
        {
            return _rotorsList
                .Aggregate(
                    mv, 
                    (current, rotor) => rotor.OmMap(current)
                );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64VectorFrame Rotate(XGaFloat64VectorFrame frame)
        {
            return _rotorsList
                .Aggregate(
                    frame, 
                    (current, rotor) => rotor.OmMap(current)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64ScaledRotor GetFinalScaledRotor()
        {
            var storage = _rotorsList
                .Skip(1)
                .Select(r => r.Multivector)
                .Aggregate(
                    _rotorsList[0].GetMultivector(), 
                    (current, rotor) => 
                        rotor.Gp(current)
                );

            return XGaFloat64ScaledRotor.Create(storage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[,] GetFinalScaledRotorArray(int rowsCount)
        {
            return Rotate(
                Processor.CreateFreeFrameOfBasis(rowsCount)
            ).GetArray(rowsCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<XGaFloat64ScaledPureRotor> GetEnumerator()
        {
            return _rotorsList.GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IXGaFloat64Outermorphism> GetLeafOutermorphisms()
        {
            return _rotorsList;
        }
    }
}