using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using GAPoTNumLib.Text;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GAPoTNumLib.GAPoT
{
    public sealed class GaPoTNumRotorsSequence : IReadOnlyList<GaPoTNumMultivector>
    {
        public static GaPoTNumRotorsSequence CreateIdentity()
        {
            return new GaPoTNumRotorsSequence();

            //return new GaPoTNumRotorsSequence().AppendRotor(
            //    GaPoTNumMultivector.CreateZero().SetTerm(0, double.INT_ONE)
            //);
        }

        public static GaPoTNumRotorsSequence Create(params GaPoTNumMultivector[] rotorsList)
        {
            return new GaPoTNumRotorsSequence(rotorsList);
        }

        public static GaPoTNumRotorsSequence Create(IEnumerable<GaPoTNumMultivector> rotorsList)
        {
            return new GaPoTNumRotorsSequence(rotorsList);
        }

        public static GaPoTNumRotorsSequence CreateFromOrthonormalFrames(GaPoTNumFrame sourceFrame, GaPoTNumFrame targetFrame, bool fullRotorsFlag = false)
        {
            Debug.Assert(targetFrame.Count == sourceFrame.Count);
            Debug.Assert(sourceFrame.IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));

            var rotorsSequence = new GaPoTNumRotorsSequence();

            var sourceFrameVectors = sourceFrame.ToArray();

            var n = fullRotorsFlag 
                ? sourceFrame.Count 
                : sourceFrame.Count - 1;
            
            for (var i = 0; i < n; i++)
            {
                var sourceVector = sourceFrameVectors[i];
                var targetVector = targetFrame[i];

                var rotor = 
                    sourceVector.GetRotorToVector(targetVector);

                rotorsSequence.AppendRotor(rotor);

                for (var j = i + 1; j < sourceFrame.Count; j++)
                    sourceFrameVectors[j] = sourceFrameVectors[j].ApplyRotor(rotor);
            }

            return rotorsSequence;
        }

        public static GaPoTNumRotorsSequence CreateFromOrthonormalFrames(GaPoTNumFrame sourceFrame, GaPoTNumFrame targetFrame, int[] sequenceArray)
        {
            Debug.Assert(targetFrame.Count == sourceFrame.Count);
            Debug.Assert(sourceFrame.IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));

            Debug.Assert(sequenceArray.Length == sourceFrame.Count - 1);
            Debug.Assert(sequenceArray.Min() >= 0);
            Debug.Assert(sequenceArray.Max() < sourceFrame.Count);
            Debug.Assert(sequenceArray.Distinct().Count() == sourceFrame.Count - 1);

            var rotorsSequence = new GaPoTNumRotorsSequence();

            var sourceFrameVectors = sourceFrame.ToArray();
            
            for (var i = 0; i < sourceFrame.Count - 1; i++)
            {
                var vectorIndex = sequenceArray[i];

                var sourceVector = sourceFrameVectors[vectorIndex];
                var targetVector = targetFrame[vectorIndex];

                var rotor = GaPoTNumMultivector.CreateSimpleRotor(
                    sourceVector, 
                    targetVector
                );

                rotorsSequence.AppendRotor(rotor);

                for (var j = i + 1; j < sourceFrame.Count; j++)
                    sourceFrameVectors[j] = sourceFrameVectors[j].ApplyRotor(rotor);
            }

            return rotorsSequence;
        }

        public static GaPoTNumRotorsSequence CreateFromFrames(int baseSpaceDimensions, GaPoTNumFrame sourceFrame, GaPoTNumFrame targetFrame)
        {
            Debug.Assert(targetFrame.Count == sourceFrame.Count);
            //Debug.Assert(IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));

            var rotorsSequence = new GaPoTNumRotorsSequence();

            var pseudoScalar = 
                GaPoTNumMultivector
                    .CreateZero()
                    .SetTerm((1 << baseSpaceDimensions) - 1, 1.0d);

            var sourceFrameVectors = new GaPoTNumVector[sourceFrame.Count];
            var targetFrameVectors = new GaPoTNumVector[targetFrame.Count];

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
                    sourceVector.GetRotorToVector(targetVector);

                rotorsSequence.AppendRotor(rotor);

                pseudoScalar = targetVector.ToMultivector().Lcp(pseudoScalar.Inverse());

                for (var j = i + 1; j < sourceFrame.Count; j++)
                {
                    sourceFrameVectors[j] = 
                        sourceFrameVectors[j].ApplyRotor(rotor).GetProjectionOnBlade(pseudoScalar);

                    targetFrameVectors[j] =
                        targetFrameVectors[j].GetProjectionOnBlade(pseudoScalar);
                }
            }

            return rotorsSequence;
        }

        public static GaPoTNumRotorsSequence CreateOrthogonalRotors(double[,] rotationMatrix)
        {
            var evdSolver = Matrix.Build.DenseOfArray(rotationMatrix).Evd();

            var eigenValuesReal = evdSolver.EigenValues.Real();
            var eigenValuesImag = evdSolver.EigenValues.Imaginary();
            var eigenVectors = evdSolver.EigenVectors;

            //TODO: Complete this

            return new GaPoTNumRotorsSequence();
        }


        private readonly List<GaPoTNumMultivector> _rotorsList
            = new List<GaPoTNumMultivector>();


        public int Count 
            => _rotorsList.Count;
        
        public GaPoTNumMultivector this[int index]
        {
            get => _rotorsList[index];
            set => _rotorsList[index] = value;
        }

        
        internal GaPoTNumRotorsSequence()
        {
        }

        internal GaPoTNumRotorsSequence(IEnumerable<GaPoTNumMultivector> rotorsList)
        {
            _rotorsList.AddRange(rotorsList);
        }


        public bool ValidateRotation(GaPoTNumFrame sourceFrame, GaPoTNumFrame targetFrame)
        {
            if (sourceFrame.Count != targetFrame.Count)
                return false;

            var rotatedFrame = Rotate(sourceFrame);

            return !rotatedFrame.Select(
                (v, i) => !(targetFrame[i] - v).IsZero()
            ).Any();
        }

        public bool IsRotorsSequence()
        {
            return _rotorsList.All(r => r.IsRotor());
        }

        public bool IsSimpleRotorsSequence()
        {
            return _rotorsList.All(r => r.IsSimpleRotor());
        }

        public GaPoTNumMultivector GetRotor(int index)
        {
            return _rotorsList[index];
        }

        public GaPoTNumRotorsSequence AppendRotor(GaPoTNumMultivector rotor)
        {
            _rotorsList.Add(rotor);

            return this;
        }

        public GaPoTNumRotorsSequence PrependRotor(GaPoTNumMultivector rotor)
        {
            _rotorsList.Insert(0, rotor);

            return this;
        }

        public GaPoTNumRotorsSequence InsertRotor(int index, GaPoTNumMultivector rotor)
        {
            _rotorsList.Insert(index, rotor);

            return this;
        }

        public GaPoTNumRotorsSequence GetSubSequence(int startIndex, int count)
        {
            return new GaPoTNumRotorsSequence(
                _rotorsList.Skip(startIndex).Take(count)
            );
        }

        public IEnumerable<GaPoTNumVector> GetRotations(GaPoTNumVector vector)
        {
            var v = vector;

            yield return v;

            foreach (var rotor in _rotorsList)
            {
                v = v.ApplyRotor(rotor);

                yield return v;
            }
        }

        public IEnumerable<GaPoTNumMultivector> GetRotations(GaPoTNumMultivector multivector)
        {
            var mv = multivector;

            yield return mv;

            foreach (var rotor in _rotorsList)
            {
                mv = mv.ApplyRotor(rotor);

                yield return mv;
            }
        }

        public IEnumerable<GaPoTNumFrame> GetRotations(GaPoTNumFrame frame)
        {
            var f = frame;

            yield return f;

            foreach (var rotor in _rotorsList)
            {
                f = f.ApplyRotor(rotor);

                yield return f;
            }
        }

        public IEnumerable<double[,]> GetRotationMatrices(int rowsCount)
        {
            var f = GaPoTNumFrame.CreateBasisFrame(rowsCount);

            yield return f.GetMatrix(rowsCount);

            foreach (var rotor in _rotorsList)
                yield return f.ApplyRotor(rotor).GetMatrix(rowsCount);
        }

        public GaPoTNumVector Rotate(GaPoTNumVector vector)
        {
            return _rotorsList
                .Aggregate(
                    vector, 
                    (current, rotor) => current.ApplyRotor(rotor)
                );
        }

        public GaPoTNumMultivector Rotate(GaPoTNumMultivector multivector)
        {
            return _rotorsList
                .Aggregate(
                    multivector, 
                    (current, rotor) => current.ApplyRotor(rotor)
                );
        }

        public GaPoTNumFrame Rotate(GaPoTNumFrame frame)
        {
            return _rotorsList
                .Aggregate(
                    frame, 
                    (current, rotor) => current.ApplyRotor(rotor)
                );
        }

        public GaPoTNumMultivector GetFinalRotor()
        {
            return _rotorsList
                .Skip(1)
                .Aggregate(
                    _rotorsList[0], 
                    (current, rotor) => rotor.Gp(current)
                );
        }

        public double[,] GetFinalMatrix(int rowsCount)
        {
            return Rotate(
                GaPoTNumFrame.CreateBasisFrame(rowsCount)
            ).GetMatrix(rowsCount);
        }

        public GaPoTNumRotorsSequence Reverse()
        {
            var rotorsSequence = new GaPoTNumRotorsSequence();

            foreach (var rotor in _rotorsList)
                rotorsSequence.PrependRotor(rotor.Reverse());

            return rotorsSequence;
        }

        public IEnumerator<GaPoTNumMultivector> GetEnumerator()
        {
            return _rotorsList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public string RotorsToText()
        {
            return _rotorsList
                .Select((r, i) => $"R{i + 1} = {r.TermsToText()}")
                .Concatenate(Environment.NewLine);
        }

        public string ToLaTeXEquationsArrays(string rotorsName, string basisName)
        {
            var textComposer = new StringBuilder();

            for (var i = 0; i < Count; i++)
            {
                var rotorEquation = this[i].ToLaTeXEquationsArray(
                    $"{rotorsName}_{{{i + 1}}}", 
                    basisName
                );

                textComposer.AppendLine(rotorEquation);
                textComposer.AppendLine();
            }

            return textComposer.ToString();
        }

        public override string ToString()
        {
            return RotorsToText();
        }
    }
}