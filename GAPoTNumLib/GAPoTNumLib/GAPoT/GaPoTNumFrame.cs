using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using GAPoTNumLib.Text;

namespace GAPoTNumLib.GAPoT
{
    public sealed class GeoPoTNumFrame : IReadOnlyList<GeoPoTNumVector>
    {
        public static GeoPoTNumFrame CreateEmptyFrame()
        {
            return new GeoPoTNumFrame();
        }
        
        public static GeoPoTNumFrame Create(params GeoPoTNumVector[] vectorsList)
        {
            return new GeoPoTNumFrame(vectorsList);
        }
        
        public static GeoPoTNumFrame Create(IEnumerable<GeoPoTNumVector> vectorsList)
        {
            return new GeoPoTNumFrame(vectorsList);
        }

        public static GeoPoTNumFrame CreateFromRows(double[,] matrix)
        {
            var rowsCount = matrix.GetLength(0);
            var colsCount = matrix.GetLength(1);

            var frame = new GeoPoTNumFrame();

            for (var i = 0; i < rowsCount; i++)
            {
                var vector = new GeoPoTNumVector();

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

        public static GeoPoTNumFrame CreateFromColumns(double[,] matrix)
        {
            var rowsCount = matrix.GetLength(0);
            var colsCount = matrix.GetLength(1);

            var frame = new GeoPoTNumFrame();

            for (var j = 0; j < colsCount; j++)
            {
                var vector = new GeoPoTNumVector();

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

        public static GeoPoTNumFrame CreateBasisFrame(int vectorsCount)
        {
            var frame = new GeoPoTNumFrame();

            for (var i = 0; i < vectorsCount; i++)
            {
                var vector = new GeoPoTNumVector().AddTerm(i + 1, 1.0d);
                
                frame.AppendVector(vector);
            }
            
            return frame;
        }

        /// <summary>
        /// See the paper "Generalized Clarke Components for Polyphase Networks", 1969
        /// </summary>
        /// <param name="vectorsCount"></param>
        /// <returns></returns>
        private static GeoPoTNumFrame CreateClarkeFrameOdd(int vectorsCount)
        {
            var frameVectorsArray = new GeoPoTNumVector[vectorsCount];
            
            var m = vectorsCount;
            var s = Math.Sqrt(2.0d / m);

            //m is odd, fill all columns except the last
            var n = (m - 1) / 2;
            for (var k = 0; k < n; k++)
            {
                var vectorIndex1 = 2 * k;
                var vectorIndex2 = 2 * k + 1;
                
                frameVectorsArray[vectorIndex1] = new GeoPoTNumVector();
                frameVectorsArray[vectorIndex2] = new GeoPoTNumVector();
                
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
            frameVectorsArray[m - 1] = new GeoPoTNumVector();

            var v = 1.0d / Math.Sqrt(m);
            for (var i = 0; i < m; i++) 
                frameVectorsArray[m - 1].SetTerm(i + 1, v);

            return new GeoPoTNumFrame(frameVectorsArray);
        }

        /// <summary>
        /// See the paper "Generalized Clarke Components for Polyphase Networks", 1969
        /// </summary>
        /// <param name="vectorsCount"></param>
        /// <returns></returns>
        private static GeoPoTNumFrame CreateClarkeFrameEven(int vectorsCount)
        {
            var frameVectorsArray = new GeoPoTNumVector[vectorsCount];
            
            var m = vectorsCount;
            var s = Math.Sqrt(2.0d / m);

            //m is even, fill all columns except the last two
            var n = (m - 1) / 2;
            for (var k = 0; k < n; k++)
            {
                var vectorIndex1 = 2 * k;
                var vectorIndex2 = 2 * k + 1;
                
                frameVectorsArray[vectorIndex1] = new GeoPoTNumVector();
                frameVectorsArray[vectorIndex2] = new GeoPoTNumVector();
                
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
            frameVectorsArray[m - 2] = new GeoPoTNumVector();
            frameVectorsArray[m - 1] = new GeoPoTNumVector();

            var v0 = 1.0d / Math.Sqrt(m);
            var v1 = -v0;

            for (var i = 0; i < m; i++)
            {
                frameVectorsArray[m - 2].SetTerm(i + 1, i % 2 == 0 ? v0 : v1);
                frameVectorsArray[m - 1].SetTerm(i + 1, v0);
            }
            
            return new GeoPoTNumFrame(frameVectorsArray);
        }

        /// <summary>
        /// See the paper "Generalized Clarke Components for Polyphase Networks", 1969
        /// </summary>
        /// <param name="vectorsCount"></param>
        /// <returns></returns>
        public static GeoPoTNumFrame CreateClarkeFrame(int vectorsCount)
        {
            return vectorsCount % 2 == 0 
                ? CreateClarkeFrameEven(vectorsCount) 
                : CreateClarkeFrameOdd(vectorsCount);
        }

        public static GeoPoTNumFrame CreateKirchhoffFrame(int vectorsCount)
        {
            return CreateKirchhoffFrame(vectorsCount, 0);
        }

        public static GeoPoTNumFrame CreateKirchhoffFrame(int vectorsCount, int refVectorIndex)
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

        public static GeoPoTNumFrame CreateGramSchmidtFrame(int vectorsCount)
        {
            return CreateGramSchmidtFrame(vectorsCount, out _);
        }

        public static GeoPoTNumFrame CreateGramSchmidtFrame(int vectorsCount, out GeoPoTNumFrame kirchhoffFrame)
        {
            return CreateGramSchmidtFrame(vectorsCount, 0, out kirchhoffFrame);
        }

        public static GeoPoTNumFrame CreateGramSchmidtFrame(int vectorsCount, int refVectorIndex)
        {
            return CreateGramSchmidtFrame(vectorsCount, refVectorIndex, out _);
        }

        public static GeoPoTNumFrame CreateGramSchmidtFrame(int vectorsCount, int refVectorIndex, out GeoPoTNumFrame kirchhoffFrame)
        {
            kirchhoffFrame = CreateKirchhoffFrame(vectorsCount, refVectorIndex);

            var uPseudoScalar = new GeoPoTNumMultivector()
                .SetTerm(
                    (1 << vectorsCount) - 1, 
                    1.0d
                );
            
            var cFrame = kirchhoffFrame.GetOrthogonalFrame(true);
            
            cFrame.AppendVector(
                -GeoPoTNumUtils
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

        public static GeoPoTNumFrame CreateHyperFreeFrame3D()
        {
            var frame = new GeoPoTNumFrame();

            var v1 = new GeoPoTNumVector(
                1.0d,
                0.0d
            );

            var v2 = new GeoPoTNumVector(
                -0.5d,
                0.5d * Math.Sqrt(3.0d)
            );

            var v3 = new GeoPoTNumVector(
                -0.5d,
                -0.5d * Math.Sqrt(3.0d)
            );

            frame.AppendVectors(v1, v2, v3);

            return frame;
        }

        public static GeoPoTNumFrame CreateHyperFreeFrame4D()
        {
            var c = Math.Sqrt(0.75d);

            var frame = new GeoPoTNumFrame();

            var v1 = new GeoPoTNumVector(
                c * Math.Sqrt(8.0d / 9.0d),
                0.0d,
                c / 3.0d
            );

            var v2 = new GeoPoTNumVector(
                c * -Math.Sqrt(2.0d / 9.0d),
                c * Math.Sqrt(2.0d / 3.0d),
                c / 3.0d
            );

            var v3 = new GeoPoTNumVector(
                -c * Math.Sqrt(2.0d / 9.0d),
                c * -Math.Sqrt(2.0d / 3.0d),
                c / 3.0d
            );

            var v4 = new GeoPoTNumVector(
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
        public static GeoPoTNumFrame CreateHyperFreeFrame(int vectorsCount)
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



        private readonly List<GeoPoTNumVector> _vectorsList
            = new List<GeoPoTNumVector>();


        public int Count 
            => _vectorsList.Count;
        
        public GeoPoTNumVector this[int index]
        {
            get => _vectorsList[index];
            set => _vectorsList[index] = value;
        }


        internal GeoPoTNumFrame()
        {
        }

        internal GeoPoTNumFrame(IEnumerable<GeoPoTNumVector> vectorsList)
        {
            _vectorsList.AddRange(vectorsList);
        }
        
        
        public GeoPoTNumFrame AppendVector(GeoPoTNumVector vector)
        {
            _vectorsList.Add(vector);

            return this;
        }
        
        public GeoPoTNumFrame AppendVectors(params GeoPoTNumVector[] vectorsList)
        {
            foreach (var vector in vectorsList)
                _vectorsList.Add(vector);

            return this;
        }
        
        public GeoPoTNumFrame PrependVector(GeoPoTNumVector vector)
        {
            _vectorsList.Insert(0, vector);

            return this;
        }
        
        public GeoPoTNumFrame InsertVector(int index, GeoPoTNumVector vector)
        {
            _vectorsList.Insert(index, vector);

            return this;
        }

        public GeoPoTNumFrame GetSubFrame(int startIndex, int count)
        {
            return new GeoPoTNumFrame(
                _vectorsList
                    .Skip(startIndex)
                    .Take(count)
            );
        }

        public GeoPoTNumFrame GetOrthogonalFrame(bool makeUnitVectors)
        {
            return new GeoPoTNumFrame(
                _vectorsList.ApplyGramSchmidt(makeUnitVectors)
            );
        }

        public GeoPoTNumFrame GetNegativeFrame()
        {
            return new GeoPoTNumFrame(
                _vectorsList.Select(v => -v)
            );
        }

        public GeoPoTNumFrame GetSwappedPairsFrame()
        {
            var frame = new GeoPoTNumFrame();

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

        public GeoPoTNumFrame ApplyRotor(GeoPoTNumMultivector rotor)
        {
            var r1 = rotor;
            var r2 = rotor.Reverse();

            return new GeoPoTNumFrame(
                _vectorsList.Select(v => r1.Gp(v.ToMultivector()).Gp(r2).GetVectorPart())
            );
        }

        public GeoPoTNumMultivector GetPseudoScalar()
        {
            return GeoPoTNumUtils.OuterProduct(_vectorsList);
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

        public bool HasSameHandedness(GeoPoTNumFrame targetFrame)
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
        public IEnumerable<GeoPoTNumMultivector> GetRotorsToFrame(GeoPoTNumFrame targetFrame)
        {
            Debug.Assert(targetFrame.Count == Count);
            Debug.Assert(IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(HasSameHandedness(targetFrame));

            var inputFrame = new GeoPoTNumVector[Count];

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

        public IEnumerable<GeoPoTNumMultivector> GetRotorsToFrame(GeoPoTNumFrame targetFrame, params int[] basisRotationOrderList)
        {
            Debug.Assert(targetFrame.Count == Count);
            Debug.Assert(IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(HasSameHandedness(targetFrame));
            Debug.Assert(GeoPoTNumUtils.ValidateIndexPermutationList(basisRotationOrderList));

            var inputFrame = new GeoPoTNumVector[Count];

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

        public IEnumerable<double> GetAnglesToFrame(GeoPoTNumFrame targetFrame)
        {
            Debug.Assert(targetFrame.Count == Count);

            for (var i = 0; i < Count; i++)
                yield return _vectorsList[i].GetAngle(targetFrame[i]);
        }

        public IEnumerable<GeoPoTNumFrame> GetFramePermutations()
        {
            var indexPermutationsList = 
                GeoPoTNumUtils.GetIndexPermutations(Count);

            foreach (var indexPermutation in indexPermutationsList)
            {
                var frame = new GeoPoTNumFrame();

                foreach (var index in indexPermutation)
                    frame.AppendVector(_vectorsList[index]);

                yield return frame;
            }
        }

        public GeoPoTNumFrame GetProjectionOnFrame(GeoPoTNumFrame frame)
        {
            var ps = frame.GetPseudoScalar();

            return new GeoPoTNumFrame(
                _vectorsList.Select(v => v.GetProjectionOnBlade(ps))
            );
        }

        public GeoPoTNumFrame Normalize()
        {
            return new GeoPoTNumFrame(_vectorsList.Select(v => v / v.Norm()));
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

        public IEnumerator<GeoPoTNumVector> GetEnumerator()
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