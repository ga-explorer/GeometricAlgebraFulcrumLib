using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using GAPoTNumLib.Interop.MATLAB;
using GAPoTNumLib.Structures;
using Irony.Parsing;
using Matrix = MathNet.Numerics.LinearAlgebra.Complex.Matrix;
using Vector = MathNet.Numerics.LinearAlgebra.Complex.Vector;

namespace GAPoTNumLib.GAPoT
{
    public static class GeoPoTNumUtils
    {
        public static double Epsilon { get; internal set; } 
            = Math.Pow(2, -25);

        public static bool IsNearZero(this double d)
        {
            return Math.Abs(d) <= Epsilon;
        }

        public static bool IsNearZero(this double d, double epsilon)
        {
            return Math.Abs(d) <= epsilon;
        }

        public static bool IsNearEqual(this double d1, double d2)
        {
            return Math.Abs(d2 - d1) <= Epsilon;
        }

        public static IEnumerable<IList<int>> GetIndexPermutations(int count)
        {
            var indicesArray = Enumerable.Range(0, count).ToArray();

            var list = new List<IList<int>>();

            return GetIndexPermutations(indicesArray, 0, indicesArray.Length - 1, list);
        }

        private static IEnumerable<IList<int>> GetIndexPermutations(int[] indicesArray, int start, int end, IList<IList<int>> list)
        {
            if (start == end)
            {
                // We have one of our possible n! solutions,
                // add it to the list.
                list.Add(new List<int>(indicesArray));
            }
            else
            {
                for (var i = start; i <= end; i++)
                {
                    Swap(ref indicesArray[start], ref indicesArray[i]);

                    GetIndexPermutations(indicesArray, start + 1, end, list);

                    Swap(ref indicesArray[start], ref indicesArray[i]);
                }
            }

            return list;
        }

        private static void Swap(ref int a, ref int b)
        {
            var temp = a;
            a = b;
            b = temp;
        }


        public static double DegreesToRadians(this double angle)
        {
            return angle * Math.PI / 180.0d;
        }

        public static double RadiansToDegrees(this double angle)
        {
            return angle * 180.0d / Math.PI;
        }

        internal static Tuple<int, int> ValidateBiversorTermIDs(int id1, int id2)
        {
            Debug.Assert(id1 == id2 || (id1 > 0 && id2 > 0));

            if (id1 == id2)
                return new Tuple<int, int>(0, 0);

            return id1 < id2 
                ? new Tuple<int, int>(id1, id2) 
                : new Tuple<int, int>(id2, id1);
        }

        public static bool ValidateIndexPermutationList(params int[] inputList)
        {
            var orderedList =
                inputList.Distinct().OrderBy(i => i).ToArray();

            return
                orderedList.Length == inputList.Length &&
                orderedList[0] == 0 &&
                orderedList[orderedList.Length - 1] != (orderedList.Length - 1);
        }


        /// <summary>
        /// Compute if the Euclidean Geometric Product of two basis blades is -1.
        /// This method is slow but can be used for GAs with dimension more than 16
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool ComputeIsNegativeEGp(int id1, int id2)
        {
            if (id1 == 0 || id2 == 0) return false;

            var flag = false;
            var id = id1;

            //Find largest 1-bit of ID1 and create a bit mask
            var initMask1 = 1;
            while (initMask1 <= id1)
                initMask1 <<= 1;

            initMask1 >>= 1;

            var mask2 = 1;
            while (mask2 <= id2)
            {
                //If the current bit in ID2 is one:
                if ((id2 & mask2) != 0)
                {
                    //Count number of swaps, each new swap inverts the final sign
                    var mask1 = initMask1;

                    while (mask1 > mask2)
                    {
                        if ((id & mask1) != 0)
                            flag = !flag;

                        mask1 >>= 1;
                    }
                }

                //Invert the corresponding bit in ID1
                id = id ^ mask2;

                mask2 <<= 1;
            }

            return flag;
        }
        
        
        /// <summary>
        /// Compute if the Euclidean Geometric Product of two basis blades is -1.
        /// This method is slow but can be used for GAs with dimension more than 16
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool ComputeIsNegativeEGp(ulong id1, ulong id2)
        {
            if (id1 == 0UL || id2 == 0UL) return false;

            var flag = false;
            var id = id1;

            //Find largest 1-bit of ID1 and create a bit mask
            var initMask1 = 1UL;
            while (initMask1 <= id1)
                initMask1 <<= 1;

            initMask1 >>= 1;

            var mask2 = 1UL;
            while (mask2 <= id2)
            {
                //If the current bit in ID2 is one:
                if ((id2 & mask2) != 0)
                {
                    //Count number of swaps, each new swap inverts the final sign
                    var mask1 = initMask1;

                    while (mask1 > mask2)
                    {
                        if ((id & mask1) != 0)
                            flag = !flag;

                        mask1 >>= 1;
                    }
                }

                //Invert the corresponding bit in ID1
                id = id ^ mask2;

                mask2 <<= 1;
            }

            return flag;
        }

        /// <summary>
        /// True if the outer product of the given euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool IsNonZeroOp(int id1, int id2)
        {
            return (id1 & id2) == 0;
        }

        /// <summary>
        /// True if the scalar product of the given euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool IsNonZeroESp(int id1, int id2)
        {
            return id1 == id2;
        }

        /// <summary>
        /// True if the left contraction product of the given euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool IsNonZeroELcp(int id1, int id2)
        {
            return (id1 & ~id2) == 0;
        }

        /// <summary>
        /// True if the right contraction product of the given euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool IsNonZeroERcp(int id1, int id2)
        {
            return (~id1 & id2) == 0;
        }

        /// <summary>
        /// True if the fat-dot product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool IsNonZeroEFdp(int id1, int id2)
        {
            return (id1 & ~id2) == 0 || (id2 & ~id1) == 0;
        }

        /// <summary>
        /// True if the Hestenes inner product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool IsNonZeroEHip(int id1, int id2)
        {
            return id1 != 0 && id2 != 0 && ((id1 & ~id2) == 0 || (id2 & ~id1) == 0);
        }

        /// <summary>
        /// True if the anti-commutator product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool IsNonZeroEAcp(int id1, int id2)
        {
            //A acp B = (AB + BA) / 2
            return ComputeIsNegativeEGp(id1, id2) == ComputeIsNegativeEGp(id2, id1);
        }

        /// <summary>
        /// True if the commutator product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool IsNonZeroECp(int id1, int id2)
        {
            //A cp B = (AB - BA) / 2
            return ComputeIsNegativeEGp(id1, id2) != ComputeIsNegativeEGp(id2, id1);
        }

        //public static int ReverseBits(this int bitsPattern, int bitsCount)
        //{
        //    Debug.Assert(
        //        bitsCount > 0 && 
        //        bitsPattern < (1 << bitsCount)
        //    );

        //    var result = 0;

        //    var i = bitsCount - 1;
        //    while (bitsPattern != 0)
        //    {
        //        if ((bitsPattern & 1) != 0)
        //            result |= (1 << i);

        //        i--;
        //        bitsPattern >>= 1;
        //    }

        //    return result;
        //}
        
        /// <summary>
        /// Test if the reverse of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: ++--
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public static bool GradeHasNegativeReverse(this int grade)
        {
            return grade % 4 > 1;

            //return ((grade * (grade - 1)) & 2) != 0;
        }

        /// <summary>
        /// Test if the reverse of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: ++--
        /// </summary>
        /// <param name="idsPattern"></param>
        /// <returns></returns>
        public static bool BasisBladeHasNegativeReverse(this int idsPattern)
        {
            var grade = idsPattern.CountOnes();
            
            return grade % 4 > 1;

            //return ((grade * (grade - 1)) & 2) != 0;
        }

        public static bool GradeHasNegativeGradeInv(this int grade)
        {
            return (grade & 1) != 0;
        }

        public static bool BasisBladeHasNegativeGradeInv(this int idsPattern)
        {
            var grade = idsPattern.CountOnes();
            
            return (grade & 1) != 0;
        }

        public static bool GradeHasNegativeCliffConj(this int grade)
        {
            var v = grade % 4;
            return v == 1 || v == 2;

            //return ((grade * (grade + 1)) & 2) != 0;
        }

        public static bool BasisBladeHasNegativeCliffConj(this int idsPattern)
        {
            var grade = idsPattern.CountOnes();
            
            var v = grade % 4;
            return v == 1 || v == 2;

            //return ((grade * (grade + 1)) & 2) != 0;
        }


        private static GeoPoTNumVector GeoPoTNumParseVector(IronyParsingResults parsingResults, ParseTreeNode rootNode)
        {
            if (rootNode.ToString() != "spVector")
                throw new SyntaxErrorException(parsingResults.ToString());

            var vector = new GeoPoTNumVector();

            var vectorNode = rootNode;
            foreach (var vectorElementNode in vectorNode.ChildNodes)
            {
                if (vectorElementNode.ToString() == "spTerm")
                {
                    //Term Form
                    var value = double.Parse(vectorElementNode.ChildNodes[0].FindTokenAndGetText());
                    var id = int.Parse(vectorElementNode.ChildNodes[1].FindTokenAndGetText());

                    if (id < 0)
                        throw new SyntaxErrorException(parsingResults.ToString());

                    vector.AddTerm(id, value);
                }
                else if (vectorElementNode.ToString() == "spPolarPhasor")
                {
                    //Polar Phasor Form
                    var magnitude = double.Parse(vectorElementNode.ChildNodes[1].FindTokenAndGetText());
                    var phase = double.Parse(vectorElementNode.ChildNodes[2].FindTokenAndGetText());
                    var id1 = int.Parse(vectorElementNode.ChildNodes[3].FindTokenAndGetText());
                    var id2 = int.Parse(vectorElementNode.ChildNodes[4].FindTokenAndGetText());

                    if (id1 < 0 || id2 != id1 + 1)
                        throw new SyntaxErrorException(parsingResults.ToString());

                    //Convert phase from degrees to radians
                    phase = phase.DegreesToRadians();

                    vector.AddPolarPhasor(id1, magnitude, phase);
                }
                else if (vectorElementNode.ToString() == "spRectPhasor")
                {
                    //Rectangular Phasor Form
                    var xValue = double.Parse(vectorElementNode.ChildNodes[1].FindTokenAndGetText());
                    var yValue = double.Parse(vectorElementNode.ChildNodes[2].FindTokenAndGetText());
                    var id1 = int.Parse(vectorElementNode.ChildNodes[3].FindTokenAndGetText());
                    var id2 = int.Parse(vectorElementNode.ChildNodes[4].FindTokenAndGetText());

                    if (id1 < 0 || id2 != id1 + 1)
                        throw new SyntaxErrorException(parsingResults.ToString());

                    vector.AddRectPhasor(id1, xValue, yValue);
                }
                else
                {
                    throw new SyntaxErrorException(parsingResults.ToString());
                }
            }

            return vector;
        }

        public static GeoPoTNumVector GeoPoTNumParseVector(this string sourceText)
        {
            var parsingResults = new IronyParsingResults(
                new GeoPoTNumVectorConstructorGrammar(), 
                sourceText
            );

            if (parsingResults.ContainsErrorMessages || !parsingResults.ContainsParseTreeRoot)
                throw new SyntaxErrorException(parsingResults.ToString());

            return GeoPoTNumParseVector(parsingResults, parsingResults.ParseTreeRoot);
        }


        private static GeoPoTNumBiversor GeoPoTNumParseBiversor(IronyParsingResults parsingResults, ParseTreeNode rootNode)
        {
            if (rootNode.ToString() != "biversor")
                throw new SyntaxErrorException(parsingResults.ToString());

            var biversor = new GeoPoTNumBiversor();

            var vectorNode = rootNode;
            foreach (var vectorElementNode in vectorNode.ChildNodes)
            {
                if (vectorElementNode.ToString() == "biversorTerm0")
                {
                    //Scalar term
                    var value = double.Parse(vectorElementNode.ChildNodes[0].FindTokenAndGetText());

                    biversor.AddTerm(1, 1, value);
                }
                else if (vectorElementNode.ToString() == "biversorTerm2")
                {
                    //Biversor term
                    var value = double.Parse(vectorElementNode.ChildNodes[0].FindTokenAndGetText());
                    var id1 = int.Parse(vectorElementNode.ChildNodes[1].FindTokenAndGetText());
                    var id2 = int.Parse(vectorElementNode.ChildNodes[2].FindTokenAndGetText());

                    if (id1 < 0 || id2 < 0)
                        throw new SyntaxErrorException(parsingResults.ToString());

                    biversor.AddTerm(id1, id2, value);
                }
                else
                {
                    throw new SyntaxErrorException(parsingResults.ToString());
                }
            }

            return biversor;
        }

        public static GeoPoTNumBiversor GeoPoTNumParseBiversor(this string sourceText)
        {
            var parsingResults = new IronyParsingResults(
                new GeoPoTNumBiversorConstructorGrammar(), 
                sourceText
            );

            if (parsingResults.ContainsErrorMessages || !parsingResults.ContainsParseTreeRoot)
                throw new SyntaxErrorException(parsingResults.ToString());

            return GeoPoTNumParseBiversor(parsingResults, parsingResults.ParseTreeRoot);
        }


        public static GeoPoTNumVector[] Negative(this IEnumerable<GeoPoTNumVector> vectorsList)
        {
            return vectorsList.Select(v => v.Negative()).ToArray();
        }

        public static GeoPoTNumVector[] Inverse(this IEnumerable<GeoPoTNumVector> vectorsList)
        {
            return vectorsList.Select(v => v.Inverse()).ToArray();
        }

        public static GeoPoTNumVector[] Reverse(this IEnumerable<GeoPoTNumVector> vectorsList)
        {
            return vectorsList.Select(v => v.Reverse()).ToArray();
        }

        public static double[] Norm(this IEnumerable<GeoPoTNumVector> vectorsList)
        {
            return vectorsList.Select(v => v.Norm()).ToArray();
        }

        public static double[] Norm2(this IEnumerable<GeoPoTNumVector> vectorsList)
        {
            return vectorsList.Select(v => v.Norm2()).ToArray();
        }

        public static GeoPoTNumVector[] Add(this GeoPoTNumVector[] vectorsList1, GeoPoTNumVector[] vectorsList2)
        {
            if (vectorsList1.Length != vectorsList2.Length)
                throw new InvalidOperationException();

            var results = new GeoPoTNumVector[vectorsList1.Length];

            for (var i = 0; i < vectorsList1.Length; i++)
                results[i] = vectorsList1[i].Add(vectorsList2[i]);

            return results;
        }

        public static GeoPoTNumVector[] Subtract(this GeoPoTNumVector[] vectorsList1, GeoPoTNumVector[] vectorsList2)
        {
            if (vectorsList1.Length != vectorsList2.Length)
                throw new InvalidOperationException();

            var results = new GeoPoTNumVector[vectorsList1.Length];

            for (var i = 0; i < vectorsList1.Length; i++)
                results[i] = vectorsList1[i].Subtract(vectorsList2[i]);

            return results;
        }

        public static GeoPoTNumBiversor[] Gp(this GeoPoTNumVector[] vectorsList1, GeoPoTNumVector[] vectorsList2)
        {
            if (vectorsList1.Length != vectorsList2.Length)
                throw new InvalidOperationException();

            var results = new GeoPoTNumBiversor[vectorsList1.Length];

            for (var i = 0; i < vectorsList1.Length; i++)
                results[i] = vectorsList1[i].Gp(vectorsList2[i]);

            return results;
        }


        public static GeoPoTNumMultivector OuterProduct(params GeoPoTNumVector[] vectorsList)
        {
            return vectorsList
                .Skip(1)
                .Aggregate(
                    vectorsList[0].ToMultivector(), 
                    (current, mv) => current.Op(mv.ToMultivector())
                );
        }

        public static GeoPoTNumMultivector OuterProduct(IReadOnlyList<GeoPoTNumVector> vectorsList)
        {
            return vectorsList
                .Skip(1)
                .Aggregate(
                    vectorsList[0].ToMultivector(), 
                    (current, mv) => current.Op(mv.ToMultivector())
                );
        }

        public static GeoPoTNumMultivector OuterProduct(params GeoPoTNumMultivector[] multivectorsList)
        {
            return multivectorsList
                .Skip(1)
                .Aggregate(
                    multivectorsList[0], 
                    (current, mv) => current.Op(mv)
                );
        }

        //public static IEnumerable<GeoPoTNumVector> ApplyGramSchmidt(GeoPoTNumVector[] vectorsArray)
        //{
        //    var v1 = vectorsArray[0];
        //    yield return (v1 / v1.Norm());
            
        //    var mv1 = vectorsArray[0].ToMultivector();
            
        //    for (var i = 1; i < vectorsArray.Length; i++)
        //    {
        //        var mv2 = mv1.Op(vectorsArray[i]);
                
        //        var orthogonalVector = mv1.Reverse().Gp(mv2).GetVectorPart();
        //        //var orthogonalVector = mv2.Gp(mv1.Reverse()).GetVectorPart();
                
        //        yield return (orthogonalVector / orthogonalVector.Norm());
                
        //        mv1 = mv2;
        //    }
        //}

        public static IEnumerable<GeoPoTNumVector> ApplyGramSchmidt(this IReadOnlyList<GeoPoTNumVector> vectorsArray, bool makeUnitVectors)
        {
            var v1 = vectorsArray[0];
            yield return makeUnitVectors 
                ? (v1 / v1.Norm()) 
                : v1;
            
            var mv1 = vectorsArray[0].ToMultivector();
            
            for (var i = 1; i < vectorsArray.Count; i++)
            {
                var mv2 = mv1.Op(vectorsArray[i]);
                
                var orthogonalVector = mv1.Reverse().Gp(mv2).GetVectorPart();
                //var orthogonalVector = mv2.Gp(mv1.Reverse()).GetVectorPart();
                
                yield return makeUnitVectors
                    ? (orthogonalVector / orthogonalVector.Norm())
                    : orthogonalVector;
                
                mv1 = mv2;
            }
        }

        public static Complex[,] ToComplexArray(this double[,] realArray)
        {
            var rowsCount = realArray.GetLength(0);
            var colsCount = realArray.GetLength(1);

            var complexArray = new Complex[rowsCount, colsCount];

            for (var i = 0; i < rowsCount; i++)
            for (var j = 0; j < colsCount; j++)
                complexArray[i, j] = new Complex(realArray[i, j], 0.0d);

            return complexArray;
        }

        public static void EigenDecomposition(this Matrix matrix, out Complex[] values, out Vector[] vectors)
        {
            var sysExpr = matrix.Evd();

            values = sysExpr.EigenValues.ToArray();

            vectors = new Vector[sysExpr.EigenVectors.ColumnCount];

            for (var i = 0; i < vectors.Length; i++)
                vectors[i] = (Vector)sysExpr.EigenVectors.Column(i);
        }

        public static GeoNumMatlabSparseMatrixData PolarPhasorsToMatlabArray(this IEnumerable<GeoPoTNumPolarPhasor> phasorsList, int rowsCount)
        {
            var termsArray = 
                phasorsList
                    .OrderBy(t => t.Id)
                    .ToArray();

            var result = GeoNumMatlabSparseMatrixData.CreateMatrix(
                rowsCount, 
                2,
                termsArray.Length * 2
            );

            var sparseIndex = 0;
            foreach (var term in termsArray)
            {
                var row = (term.Id - 1) / 2 + 1;

                result.SetItem(sparseIndex, row, 1, term.Magnitude);
                result.SetItem(sparseIndex + 1, row, 2, term.Phase);

                sparseIndex += 2;
            }

            return result;
        }

        public static GeoNumMatlabSparseMatrixData RectPhasorsToMatlabArray(this IEnumerable<GeoPoTNumRectPhasor> phasorsList, int rowsCount)
        {
            var termsArray = 
                phasorsList
                    .OrderBy(t => t.Id)
                    .ToArray();

            var result = GeoNumMatlabSparseMatrixData.CreateMatrix(
                rowsCount, 
                2,
                termsArray.Length * 2
            );

            var sparseIndex = 0;
            foreach (var term in termsArray)
            {
                var row = (term.Id - 1) / 2 + 1;

                result.SetItem(sparseIndex, row, 1, term.XValue);
                result.SetItem(sparseIndex + 1, row, 2, term.YValue);

                sparseIndex += 2;
            }

            return result;
        }

        public static GeoPoTNumVector TermsToVector(this IEnumerable<GeoPoTNumVectorTerm> termsList)
        {
            return new GeoPoTNumVector(termsList);
        }

        public static GeoPoTNumBiversor TermsToBiversor(this IEnumerable<GeoPoTNumBiversorTerm> termsList)
        {
            return new GeoPoTNumBiversor(termsList);
        }

        public static GeoPoTNumMultivector TermsToMultivector(this IEnumerable<GeoPoTNumMultivectorTerm> termsList)
        {
            return new GeoPoTNumMultivector(termsList);
        }
        
        public static IEnumerable<GeoPoTNumMultivectorTerm> OrderByGrade(this IEnumerable<GeoPoTNumMultivectorTerm> termsList)
        {
            var termsArray = termsList.ToArray();
            var bitsCount = termsArray.Max(t => t.IDsPattern).LastOneBitPosition() + 1;

            if (bitsCount == 0)
                return termsArray;

            return termsArray
                .Where(t => !t.Value.IsNearZero())
                .OrderBy(t => t.GetGrade())
                .ThenByDescending(t => t.IDsPattern.ReverseBits(bitsCount));
        }
        
        public static IEnumerable<GeoPoTNumMultivectorTerm> OrderById(this IEnumerable<GeoPoTNumMultivectorTerm> termsList)
        {
            return termsList
                .Where(t => !t.Value.IsNearZero())
                .OrderBy(t => t.IDsPattern);
        }

        public static GeoNumMatlabSparseMatrixData TermsToMatlabArray(this IEnumerable<GeoPoTNumVectorTerm> termsList, int rowsCount)
        {
            var termsArray = 
                termsList
                    .OrderBy(t => t.TermId)
                    .ToArray();

            var result = GeoNumMatlabSparseMatrixData.CreateColumnMatrix(
                rowsCount, 
                termsArray.Length
            );

            var sparseIndex = 0;
            foreach (var term in termsArray)
            {
                result.SetItem(sparseIndex, term.TermId, term.Value);

                sparseIndex++;
            }

            return result;
        }

        public static GeoNumMatlabSparseMatrixData TermsToMatlabArray(this GeoPoTNumVector[] vectorsList, int rowsCount)
        {
            var columnsCount = vectorsList.Length;

            var termsList = 
                vectorsList
                    .Select(v => v.GetTerms()
                        .OrderBy(t => t.TermId)
                        .ToArray()
                    ).ToArray();

            var result = GeoNumMatlabSparseMatrixData.CreateMatrix(
                rowsCount,
                columnsCount,
                termsList.Sum(t => t.Length)
            );

            var sparseIndex = 0;
            for (var j = 0; j < columnsCount; j++)
            {
                var termsArray = 
                    termsList[j];

                foreach (var term in termsArray)
                {
                    result.SetItem(sparseIndex, term.TermId, j + 1, term.Value);

                    sparseIndex++;
                }
            }

            return result;
        }

        public static GeoNumMatlabSparseMatrixData TermsToMatlabArray(this IEnumerable<GeoPoTNumBiversorTerm> termsList, int rowsCount)
        {
            var termsArray = termsList
                .OrderBy(t => t.TermId1)
                .ThenBy(t => t.TermId2)
                .ToArray();

            var result = GeoNumMatlabSparseMatrixData.CreateSquareMatrix(
                rowsCount, 
                termsArray.Length
            );

            var sparseIndex = 0;
            foreach (var term in termsArray)
            {
                result.SetItem(
                    sparseIndex,
                    term.TermId1 + 1,
                    term.TermId2 + 1,
                    term.Value
                );

                sparseIndex++;
            }

            return result;
        }
    }
}
