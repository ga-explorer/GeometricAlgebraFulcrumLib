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
    public sealed class GeoPoTNumRotorsSequence : IReadOnlyList<GeoPoTNumMultivector>
    {
        public static GeoPoTNumRotorsSequence CreateIdentity()
        {
            return new GeoPoTNumRotorsSequence();

            //return new GeoPoTNumRotorsSequence().AppendRotor(
            //    GeoPoTNumMultivector.CreateZero().SetTerm(0, double.INT_ONE)
            //);
        }

        public static GeoPoTNumRotorsSequence Create(params GeoPoTNumMultivector[] rotorsList)
        {
            return new GeoPoTNumRotorsSequence(rotorsList);
        }

        public static GeoPoTNumRotorsSequence Create(IEnumerable<GeoPoTNumMultivector> rotorsList)
        {
            return new GeoPoTNumRotorsSequence(rotorsList);
        }

        public static GeoPoTNumRotorsSequence CreateFromOrthonormalFrames(GeoPoTNumFrame sourceFrame, GeoPoTNumFrame targetFrame, bool fullRotorsFlag = false)
        {
            Debug.Assert(targetFrame.Count == sourceFrame.Count);
            Debug.Assert(sourceFrame.IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));

            var rotorsSequence = new GeoPoTNumRotorsSequence();

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

        public static GeoPoTNumRotorsSequence CreateFromOrthonormalFrames(GeoPoTNumFrame sourceFrame, GeoPoTNumFrame targetFrame, int[] sequenceArray)
        {
            Debug.Assert(targetFrame.Count == sourceFrame.Count);
            Debug.Assert(sourceFrame.IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));

            Debug.Assert(sequenceArray.Length == sourceFrame.Count - 1);
            Debug.Assert(sequenceArray.Min() >= 0);
            Debug.Assert(sequenceArray.Max() < sourceFrame.Count);
            Debug.Assert(sequenceArray.Distinct().Count() == sourceFrame.Count - 1);

            var rotorsSequence = new GeoPoTNumRotorsSequence();

            var sourceFrameVectors = sourceFrame.ToArray();
            
            for (var i = 0; i < sourceFrame.Count - 1; i++)
            {
                var vectorIndex = sequenceArray[i];

                var sourceVector = sourceFrameVectors[vectorIndex];
                var targetVector = targetFrame[vectorIndex];

                var rotor = GeoPoTNumMultivector.CreateSimpleRotor(
                    sourceVector, 
                    targetVector
                );

                rotorsSequence.AppendRotor(rotor);

                for (var j = i + 1; j < sourceFrame.Count; j++)
                    sourceFrameVectors[j] = sourceFrameVectors[j].ApplyRotor(rotor);
            }

            return rotorsSequence;
        }

        public static GeoPoTNumRotorsSequence CreateFromFrames(int baseSpaceDimensions, GeoPoTNumFrame sourceFrame, GeoPoTNumFrame targetFrame)
        {
            Debug.Assert(targetFrame.Count == sourceFrame.Count);
            //Debug.Assert(IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));

            var rotorsSequence = new GeoPoTNumRotorsSequence();

            var pseudoScalar = 
                GeoPoTNumMultivector
                    .CreateZero()
                    .SetTerm((1 << baseSpaceDimensions) - 1, 1.0d);

            var sourceFrameVectors = new GeoPoTNumVector[sourceFrame.Count];
            var targetFrameVectors = new GeoPoTNumVector[targetFrame.Count];

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

        public static GeoPoTNumRotorsSequence CreateOrthogonalRotors(double[,] rotationMatrix)
        {
            var evdSolver = Matrix.Build.DenseOfArray(rotationMatrix).Evd();

            var eigenValuesReal = evdSolver.EigenValues.Real();
            var eigenValuesImag = evdSolver.EigenValues.Imaginary();
            var eigenVectors = evdSolver.EigenVectors;

            //TODO: Complete this

            return new GeoPoTNumRotorsSequence();
        }


        private readonly List<GeoPoTNumMultivector> _rotorsList
            = new List<GeoPoTNumMultivector>();


        public int Count 
            => _rotorsList.Count;
        
        public GeoPoTNumMultivector this[int index]
        {
            get => _rotorsList[index];
            set => _rotorsList[index] = value;
        }

        
        internal GeoPoTNumRotorsSequence()
        {
        }

        internal GeoPoTNumRotorsSequence(IEnumerable<GeoPoTNumMultivector> rotorsList)
        {
            _rotorsList.AddRange(rotorsList);
        }


        public bool ValidateRotation(GeoPoTNumFrame sourceFrame, GeoPoTNumFrame targetFrame)
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

        public GeoPoTNumMultivector GetRotor(int index)
        {
            return _rotorsList[index];
        }

        public GeoPoTNumRotorsSequence AppendRotor(GeoPoTNumMultivector rotor)
        {
            _rotorsList.Add(rotor);

            return this;
        }

        public GeoPoTNumRotorsSequence PrependRotor(GeoPoTNumMultivector rotor)
        {
            _rotorsList.Insert(0, rotor);

            return this;
        }

        public GeoPoTNumRotorsSequence InsertRotor(int index, GeoPoTNumMultivector rotor)
        {
            _rotorsList.Insert(index, rotor);

            return this;
        }

        public GeoPoTNumRotorsSequence GetSubSequence(int startIndex, int count)
        {
            return new GeoPoTNumRotorsSequence(
                _rotorsList.Skip(startIndex).Take(count)
            );
        }

        public IEnumerable<GeoPoTNumVector> GetRotations(GeoPoTNumVector vector)
        {
            var v = vector;

            yield return v;

            foreach (var rotor in _rotorsList)
            {
                v = v.ApplyRotor(rotor);

                yield return v;
            }
        }

        public IEnumerable<GeoPoTNumMultivector> GetRotations(GeoPoTNumMultivector multivector)
        {
            var mv = multivector;

            yield return mv;

            foreach (var rotor in _rotorsList)
            {
                mv = mv.ApplyRotor(rotor);

                yield return mv;
            }
        }

        public IEnumerable<GeoPoTNumFrame> GetRotations(GeoPoTNumFrame frame)
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
            var f = GeoPoTNumFrame.CreateBasisFrame(rowsCount);

            yield return f.GetMatrix(rowsCount);

            foreach (var rotor in _rotorsList)
                yield return f.ApplyRotor(rotor).GetMatrix(rowsCount);
        }

        public GeoPoTNumVector Rotate(GeoPoTNumVector vector)
        {
            return _rotorsList
                .Aggregate(
                    vector, 
                    (current, rotor) => current.ApplyRotor(rotor)
                );
        }

        public GeoPoTNumMultivector Rotate(GeoPoTNumMultivector multivector)
        {
            return _rotorsList
                .Aggregate(
                    multivector, 
                    (current, rotor) => current.ApplyRotor(rotor)
                );
        }

        public GeoPoTNumFrame Rotate(GeoPoTNumFrame frame)
        {
            return _rotorsList
                .Aggregate(
                    frame, 
                    (current, rotor) => current.ApplyRotor(rotor)
                );
        }

        public GeoPoTNumMultivector GetFinalRotor()
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
                GeoPoTNumFrame.CreateBasisFrame(rowsCount)
            ).GetMatrix(rowsCount);
        }

        public GeoPoTNumRotorsSequence Reverse()
        {
            var rotorsSequence = new GeoPoTNumRotorsSequence();

            foreach (var rotor in _rotorsList)
                rotorsSequence.PrependRotor(rotor.Reverse());

            return rotorsSequence;
        }

        public IEnumerator<GeoPoTNumMultivector> GetEnumerator()
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