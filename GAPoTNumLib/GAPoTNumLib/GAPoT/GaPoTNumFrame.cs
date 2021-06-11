using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using GAPoTNumLib.Text;

namespace GAPoTNumLib.GAPoT
{
    public sealed class GaPoTNumFrame : IReadOnlyList<GaPoTNumVector>
    {
        public static GaPoTNumFrame CreateEmptyFrame()
        {
            return new GaPoTNumFrame();
        }
        
        public static GaPoTNumFrame Create(params GaPoTNumVector[] vectorsList)
        {
            return new GaPoTNumFrame(vectorsList);
        }
        
        public static GaPoTNumFrame Create(IEnumerable<GaPoTNumVector> vectorsList)
        {
            return new GaPoTNumFrame(vectorsList);
        }

        public static GaPoTNumFrame CreateFromRows(double[,] matrix)
        {
            var rowsCount = matrix.GetLength(0);
            var colsCount = matrix.GetLength(1);

            var frame = new GaPoTNumFrame();

            for (var i = 0; i < rowsCount; i++)
            {
                var vector = new GaPoTNumVector();

                for (var j = 0; j < colsCount; j++)
                {
                    var value = matrix[i, j];

                    if (value == 0)
                        continue;

                    vector.AddTerm(j + 1, value);
                }

                frame.AppendVector(vector);
            }

            return frame;
        }

        public static GaPoTNumFrame CreateFromColumns(double[,] matrix)
        {
            var rowsCount = matrix.GetLength(0);
            var colsCount = matrix.GetLength(1);

            var frame = new GaPoTNumFrame();

            for (var j = 0; j < colsCount; j++)
            {
                var vector = new GaPoTNumVector();

                for (var i = 0; i < rowsCount; i++)
                {
                    var value = matrix[i, j];

                    if (value == 0)
                        continue;

                    vector.AddTerm(i + 1, value);
                }

                frame.AppendVector(vector);
            }

            return frame;
        }

        public static GaPoTNumFrame CreateBasisFrame(int vectorsCount)
        {
            var frame = new GaPoTNumFrame();

            for (var i = 0; i < vectorsCount; i++)
            {
                var vector = new GaPoTNumVector().AddTerm(i + 1, 1.0d);
                
                frame.AppendVector(vector);
            }
            
            return frame;
        }

        /// <summary>
        /// See the paper "Generalized Clarke Components for Polyphase Networks", 1969
        /// </summary>
        /// <param name="vectorsCount"></param>
        /// <returns></returns>
        private static GaPoTNumFrame CreateClarkeFrameOdd(int vectorsCount)
        {
            var frameVectorsArray = new GaPoTNumVector[vectorsCount];
            
            var m = vectorsCount;
            var s = Math.Sqrt(2.0d / m);

            //m is odd, fill all columns except the last
            var n = (m - 1) / 2;
            for (var k = 0; k < n; k++)
            {
                var vectorIndex1 = 2 * k;
                var vectorIndex2 = 2 * k + 1;
                
                frameVectorsArray[vectorIndex1] = new GaPoTNumVector();
                frameVectorsArray[vectorIndex2] = new GaPoTNumVector();
                
                frameVectorsArray[vectorIndex1].SetTerm(1, s);
                
                for (var i = 1; i < m; i++)
                {
                    var angle = 2 * Math.PI * (k + 1) * i / m;
                    var cosAngle = s * Math.Cos(angle);
                    var sinAngle = s * Math.Sin(angle);
                    
                    frameVectorsArray[vectorIndex1].SetTerm(i + 1, cosAngle);
                    frameVectorsArray[vectorIndex2].SetTerm(i + 1, sinAngle);
                }
            }

            //Fill the last column
            frameVectorsArray[m - 1] = new GaPoTNumVector();

            var v = 1.0d / Math.Sqrt(m);
            for (var i = 0; i < m; i++) 
                frameVectorsArray[m - 1].SetTerm(i + 1, v);

            return new GaPoTNumFrame(frameVectorsArray);
        }

        /// <summary>
        /// See the paper "Generalized Clarke Components for Polyphase Networks", 1969
        /// </summary>
        /// <param name="vectorsCount"></param>
        /// <returns></returns>
        private static GaPoTNumFrame CreateClarkeFrameEven(int vectorsCount)
        {
            var frameVectorsArray = new GaPoTNumVector[vectorsCount];
            
            var m = vectorsCount;
            var s = Math.Sqrt(2.0d / m);

            //m is even, fill all columns except the last two
            var n = (m - 1) / 2;
            for (var k = 0; k < n; k++)
            {
                var vectorIndex1 = 2 * k;
                var vectorIndex2 = 2 * k + 1;
                
                frameVectorsArray[vectorIndex1] = new GaPoTNumVector();
                frameVectorsArray[vectorIndex2] = new GaPoTNumVector();
                
                frameVectorsArray[vectorIndex1].SetTerm(1, s);
                
                for (var i = 1; i < m; i++)
                {
                    var angle = 2.0d * Math.PI * (k + 1) * i / m;
                    var cosAngle = s * Math.Cos(angle);
                    var sinAngle = s * Math.Sin(angle);
                    
                    frameVectorsArray[vectorIndex1].SetTerm(i + 1, cosAngle);
                    frameVectorsArray[vectorIndex2].SetTerm(i + 1, sinAngle);
                }
            }

            //Fill the last column
            frameVectorsArray[m - 2] = new GaPoTNumVector();
            frameVectorsArray[m - 1] = new GaPoTNumVector();

            var v0 = 1.0d / Math.Sqrt(m);
            var v1 = -v0;

            for (var i = 0; i < m; i++)
            {
                frameVectorsArray[m - 2].SetTerm(i + 1, i % 2 == 0 ? v0 : v1);
                frameVectorsArray[m - 1].SetTerm(i + 1, v0);
            }
            
            return new GaPoTNumFrame(frameVectorsArray);
        }

        /// <summary>
        /// See the paper "Generalized Clarke Components for Polyphase Networks", 1969
        /// </summary>
        /// <param name="vectorsCount"></param>
        /// <returns></returns>
        public static GaPoTNumFrame CreateClarkeFrame(int vectorsCount)
        {
            return vectorsCount % 2 == 0 
                ? CreateClarkeFrameEven(vectorsCount) 
                : CreateClarkeFrameOdd(vectorsCount);
        }

        public static GaPoTNumFrame CreateKirchhoffFrame(int vectorsCount)
        {
            return CreateKirchhoffFrame(vectorsCount, 0);
        }

        public static GaPoTNumFrame CreateKirchhoffFrame(int vectorsCount, int refVectorIndex)
        {
            var uFrame = CreateBasisFrame(vectorsCount);
            var eFrame = CreateEmptyFrame();

            var refVector = uFrame[refVectorIndex];
            for (var i = 0; i < vectorsCount; i++)
            {
                if (i == refVectorIndex) 
                    continue;

                eFrame.AppendVector(uFrame[i] - refVector);
            }

            return eFrame;
        }

        public static GaPoTNumFrame CreateGramSchmidtFrame(int vectorsCount)
        {
            return CreateGramSchmidtFrame(vectorsCount, out _);
        }

        public static GaPoTNumFrame CreateGramSchmidtFrame(int vectorsCount, out GaPoTNumFrame kirchhoffFrame)
        {
            return CreateGramSchmidtFrame(vectorsCount, 0, out kirchhoffFrame);
        }

        public static GaPoTNumFrame CreateGramSchmidtFrame(int vectorsCount, int refVectorIndex)
        {
            return CreateGramSchmidtFrame(vectorsCount, refVectorIndex, out _);
        }

        public static GaPoTNumFrame CreateGramSchmidtFrame(int vectorsCount, int refVectorIndex, out GaPoTNumFrame kirchhoffFrame)
        {
            kirchhoffFrame = CreateKirchhoffFrame(vectorsCount, refVectorIndex);

            var uPseudoScalar = new GaPoTNumMultivector()
                .SetTerm(
                    (1 << vectorsCount) - 1, 
                    1.0d
                );
            
            var cFrame = kirchhoffFrame.GetOrthogonalFrame(true);
            
            cFrame.AppendVector(
                -GaPoTNumUtils
                    .OuterProduct(cFrame)
                    .Gp(uPseudoScalar.CliffordConjugate())
                    .GetVectorPart()
            );

            Debug.Assert(
                cFrame.IsOrthonormal()
            );

            Debug.Assert(
                CreateBasisFrame(vectorsCount).HasSameHandedness(cFrame)
            );

            return cFrame;
        }

        public static GaPoTNumFrame CreateHyperVectorsFrame3D()
        {
            var frame = new GaPoTNumFrame();

            var v1 = new GaPoTNumVector(
                1.0d,
                0.0d
            );

            var v2 = new GaPoTNumVector(
                -0.5d,
                0.5d * Math.Sqrt(3.0d)
            );

            var v3 = new GaPoTNumVector(
                -0.5d,
                -0.5d * Math.Sqrt(3.0d)
            );

            frame.AppendVectors(v1, v2, v3);

            return frame;
        }

        public static GaPoTNumFrame CreateHyperVectorsFrame4D()
        {
            var c = Math.Sqrt(0.75d);

            var frame = new GaPoTNumFrame();

            var v1 = new GaPoTNumVector(
                c * Math.Sqrt(8.0d / 9.0d),
                0.0d,
                c / 3.0d
            );

            var v2 = new GaPoTNumVector(
                c * -Math.Sqrt(2.0d / 9.0d),
                c * Math.Sqrt(2.0d / 3.0d),
                c / 3.0d
            );

            var v3 = new GaPoTNumVector(
                -c * Math.Sqrt(2.0d / 9.0d),
                c * -Math.Sqrt(2.0d / 3.0d),
                c / 3.0d
            );

            var v4 = new GaPoTNumVector(
                0.0d,
                0.0d,
                -c
            );

            frame.AppendVectors(v1, v2, v3, v4);

            return frame;
        }

        /// <summary>
        /// See paper: Generalized space vector transformation, 2000
        /// </summary>
        /// <param name="vectorsCount"></param>
        /// <returns></returns>
        public static GaPoTNumFrame CreateHyperVectorsFrame(int vectorsCount)
        {
            var n = vectorsCount;
            var fbdMatrix = new double[n - 1, n];

            for (var i = 0; i < n - 1; i++)
            {
                var k1 = (double)(n - i - 1);
                var k2 = (double)(n - i);

                var c1 = Math.Sqrt(k1 / k2);
                var c2 = -1.0d / Math.Sqrt(k1 * k2);

                fbdMatrix[i, i] = c1;

                for (var j = i + 1; j < n; j++)
                    fbdMatrix[i, j] = c2;
            }

            return CreateFromColumns(fbdMatrix);
        }



        private readonly List<GaPoTNumVector> _vectorsList
            = new List<GaPoTNumVector>();


        public int Count 
            => _vectorsList.Count;
        
        public GaPoTNumVector this[int index]
        {
            get => _vectorsList[index];
            set => _vectorsList[index] = value;
        }


        internal GaPoTNumFrame()
        {
        }

        internal GaPoTNumFrame(IEnumerable<GaPoTNumVector> vectorsList)
        {
            _vectorsList.AddRange(vectorsList);
        }
        
        
        public GaPoTNumFrame AppendVector(GaPoTNumVector vector)
        {
            _vectorsList.Add(vector);

            return this;
        }
        
        public GaPoTNumFrame AppendVectors(params GaPoTNumVector[] vectorsList)
        {
            foreach (var vector in vectorsList)
                _vectorsList.Add(vector);

            return this;
        }
        
        public GaPoTNumFrame PrependVector(GaPoTNumVector vector)
        {
            _vectorsList.Insert(0, vector);

            return this;
        }
        
        public GaPoTNumFrame InsertVector(int index, GaPoTNumVector vector)
        {
            _vectorsList.Insert(index, vector);

            return this;
        }

        public GaPoTNumFrame GetSubFrame(int startIndex, int count)
        {
            return new GaPoTNumFrame(
                _vectorsList
                    .Skip(startIndex)
                    .Take(count)
            );
        }

        public GaPoTNumFrame GetOrthogonalFrame(bool makeUnitVectors)
        {
            return new GaPoTNumFrame(
                _vectorsList.ApplyGramSchmidt(makeUnitVectors)
            );
        }

        public GaPoTNumFrame GetNegativeFrame()
        {
            return new GaPoTNumFrame(
                _vectorsList.Select(v => -v)
            );
        }

        public GaPoTNumFrame GetSwappedPairsFrame()
        {
            var frame = new GaPoTNumFrame();

            //Swap each pair of two consecutive vectors in the frame
            for (var i = 0; i < _vectorsList.Count - 1; i += 2)
            {
                frame.AppendVector(_vectorsList[i + 1]);
                frame.AppendVector(_vectorsList[i]);
            }

            if (_vectorsList.Count % 2 == 1)
            {
                //To keep the same handedness we count the number of swaps and
                //negate the final vector if the number is odd

                var numberOfSwaps = (_vectorsList.Count - 1) / 2;

                var lastVector = numberOfSwaps % 2 == 0
                    ? _vectorsList[_vectorsList.Count - 1]
                    : -_vectorsList[_vectorsList.Count - 1];

                frame.AppendVector(lastVector);
            }

            return frame;
        }

        public GaPoTNumFrame ApplyRotor(GaPoTNumMultivector rotor)
        {
            var r1 = rotor;
            var r2 = rotor.Reverse();

            return new GaPoTNumFrame(
                _vectorsList.Select(v => r1.Gp(v.ToMultivector()).Gp(r2).GetVectorPart())
            );
        }

        public GaPoTNumMultivector GetPseudoScalar()
        {
            return GaPoTNumUtils.OuterProduct(_vectorsList);
        }

        public bool IsOrthonormal()
        {
            for (var i = 0; i < Count; i++)
            {
                var v1 = _vectorsList[i];

                var dii = v1.DotProduct(v1) - 1.0d;

                if (!dii.IsNearZero()) 
                    return false;

                for (var j = i + 1; j < Count; j++)
                {
                    var dij = v1.DotProduct(_vectorsList[j]);

                    if (!dij.IsNearZero())
                        return false;
                }
            }

            return true;
        }

        public bool HasSameHandedness(GaPoTNumFrame targetFrame)
        {
            var ps1 = GetPseudoScalar();
            var ps2 = targetFrame.GetPseudoScalar();
            var s = ps1 - ps2;

            //var s = GetPseudoScalar() - targetFrame.GetPseudoScalar();

            return s.IsZero();
        }

        public double[,] GetMatrix()
        {
            return GetMatrix(Count);
        }

        public double[,] GetMatrix(int rowsCount)
        {
            var colsCount = Count;
            var itemsArray = new double[rowsCount, colsCount];

            for (var j = 0; j < Count; j++)
            {
                var vector = _vectorsList[j];

                foreach (var term in vector.GetTerms())
                {
                    var i = term.TermId - 1;

                    itemsArray[i, j] = term.Value;
                }
            }
            
            return itemsArray;
        }

        public double[,] GetInnerProductsMatrix()
        {
            var ipm = new double[Count, Count];

            for (var i = 0; i < Count; i++)
            {
                var v1 = _vectorsList[i];

                ipm[i, i] = v1.DotProduct(v1);

                for (var j = i + 1; j < Count; j++)
                {
                    var ip = v1.DotProduct(_vectorsList[j]);

                    ipm[i, j] = ip;
                    ipm[j, i] = ip;
                }
            }

            return ipm;
        }

        public double[,] GetInnerAnglesMatrix()
        {
            var ipm = new double[Count, Count];

            for (var i = 0; i < Count; i++)
            {
                var v1 = _vectorsList[i];

                for (var j = i + 1; j < Count; j++)
                {
                    var ip = v1.GetAngle(_vectorsList[j]).RadiansToDegrees();

                    ipm[i, j] = ip;
                    ipm[j, i] = ip;
                }
            }

            return ipm;
        }

        public double[,] GetInnerAnglesInDegreesMatrix()
        {
            var ipm = new double[Count, Count];

            for (var i = 0; i < Count; i++)
            {
                var v1 = _vectorsList[i];

                for (var j = i + 1; j < Count; j++)
                {
                    var ip = v1.GetAngle(_vectorsList[j]).RadiansToDegrees();

                    ipm[i, j] = ip;
                    ipm[j, i] = ip;
                }
            }

            return ipm;
        }

        /// <summary>
        /// Find a sequence of simple rotors to transform this frame to another
        /// Both frames must be orthonormal with the same handedness and size
        /// </summary>
        /// <param name="targetFrame"></param>
        /// <returns></returns>
        public IEnumerable<GaPoTNumMultivector> GetRotorsToFrame(GaPoTNumFrame targetFrame)
        {
            Debug.Assert(targetFrame.Count == Count);
            Debug.Assert(IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(HasSameHandedness(targetFrame));

            var inputFrame = new GaPoTNumVector[Count];

            for (var i = 0; i < Count; i++)
                inputFrame[i] = _vectorsList[i];
            
            for (var i = 0; i < Count - 1; i++)
            {
                var rotor = 
                    inputFrame[i].GetRotorToVector(targetFrame[i]);

                yield return rotor;

                for (var j = i + 1; j < Count; j++)
                    inputFrame[j] = inputFrame[j].ApplyRotor(rotor);
            }
        }

        public IEnumerable<GaPoTNumMultivector> GetRotorsToFrame(GaPoTNumFrame targetFrame, params int[] basisRotationOrderList)
        {
            Debug.Assert(targetFrame.Count == Count);
            Debug.Assert(IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(HasSameHandedness(targetFrame));
            Debug.Assert(GaPoTNumUtils.ValidateIndexPermutationList(basisRotationOrderList));

            var inputFrame = new GaPoTNumVector[Count];

            for (var i = 0; i < Count; i++)
                inputFrame[i] = _vectorsList[i];
            
            for (var i = 0; i < Count - 1; i++)
            {
                var vectorIndex = basisRotationOrderList[i];

                var rotor = 
                    inputFrame[vectorIndex].GetRotorToVector(targetFrame[vectorIndex]);

                yield return rotor;

                for (var j = i + 1; j < Count; j++)
                {
                    var vectorIndex1 = basisRotationOrderList[j];

                    inputFrame[vectorIndex1] = inputFrame[vectorIndex1].ApplyRotor(rotor);
                }
            }
        }

        public IEnumerable<double> GetAnglesToFrame(GaPoTNumFrame targetFrame)
        {
            Debug.Assert(targetFrame.Count == Count);

            for (var i = 0; i < Count; i++)
                yield return _vectorsList[i].GetAngle(targetFrame[i]);
        }

        public IEnumerable<GaPoTNumFrame> GetFramePermutations()
        {
            var indexPermutationsList = 
                GaPoTNumUtils.GetIndexPermutations(Count);

            foreach (var indexPermutation in indexPermutationsList)
            {
                var frame = new GaPoTNumFrame();

                foreach (var index in indexPermutation)
                    frame.AppendVector(_vectorsList[index]);

                yield return frame;
            }
        }

        public GaPoTNumFrame GetProjectionOnFrame(GaPoTNumFrame frame)
        {
            var ps = frame.GetPseudoScalar();

            return new GaPoTNumFrame(
                _vectorsList.Select(v => v.GetProjectionOnBlade(ps))
            );
        }

        public GaPoTNumFrame Normalize()
        {
            return new GaPoTNumFrame(_vectorsList.Select(v => v / v.Norm()));
        }

        public string ToLaTeXEquationsArray(string vectorName, string basisName)
        {
            var textComposer = new StringBuilder();

            textComposer.AppendLine(@"\begin{eqnarray*}");

            for (var i = 0; i < _vectorsList.Count; i++)
            {
                var vector = _vectorsList[i];

                var termLaTeX = vector
                    .TermsToLaTeX()
                    .Replace(@"\sigma_", $"{basisName}_");

                var line = $@"{vectorName}_{i + 1} & = & {termLaTeX}";

                if (i < _vectorsList.Count - 1)
                    line += @"\\";

                textComposer.AppendLine(line);
            }

            textComposer.AppendLine(@"\end{eqnarray*}");

            return textComposer.ToString();
        }

        public IEnumerator<GaPoTNumVector> GetEnumerator()
        {
            return _vectorsList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public string VectorsToText()
        {
            return _vectorsList
                .Select((r, i) => $"e{i + 1} = {r.TermsToText()}")
                .Concatenate(Environment.NewLine);
        }

        public override string ToString()
        {
            return VectorsToText();
        }
    }
}