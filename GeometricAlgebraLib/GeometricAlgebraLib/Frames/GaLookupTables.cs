using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataStructuresLib;
using DataStructuresLib.Combinations;

namespace GeometricAlgebraLib.Frames
{
    public static class GaLookupTables
    {
        internal static int MaxVSpaceDimension { get; } = 13;

        internal static int MaxGaSpaceDimension { get; } = 1 << MaxVSpaceDimension;

        internal static int MaxBasisBladeId { get; } = (1 << MaxVSpaceDimension) - 1;

        /// <summary>
        /// ID to grade lookup table
        /// </summary>
        internal static int[] IdToGradeTable;

        /// <summary>
        /// ID to index lookup table
        /// </summary>
        internal static ulong[] IdToIndexTable;

        /// <summary>
        /// ID to 'is reverse sign = -1' lookup table
        /// </summary>
        internal static BitArray IsNegativeReverseTable;

        /// <summary>
        /// ID to 'is grade inverse sign = -1' lookup table
        /// </summary>
        internal static BitArray IsNegativeGradeInvTable;

        /// <summary>
        /// ID to 'is clifford conjugate sign = -1' lookup table
        /// </summary>
        internal static BitArray IsNegativeCliffConjTable;

        /// <summary>
        /// (grade, index) to ID lookup table
        /// </summary>
        internal static List<ulong[]> GradeIndexToIdTable;

        /// <summary>
        /// (grade, index) to largest one-bit position ID lookup table
        /// </summary>
        internal static List<int[]> GradeIndexToMaxBasisVectorIndexTable;

        /// <summary>
        /// Is basis blades EGP Negative lookup tables
        /// </summary>
        internal static BitArray[] IsNegativeEgpLookupTables;

        /// <summary>
        /// Is (basis blade by basis blade) EGP Negative lookup tables
        /// </summary>
        internal static BitArray[] IsNegativeVectorEgpLookupTables;

        /// <summary>
        /// An index lookup table used to compute the outer product of a vector with a k-vector
        /// </summary>
        internal static int[][][][] VectorKVectorOpIndexLookupTables;


        static GaLookupTables()
        {
            ComputeIsNegativeVectorEgpLookupTables();

            ComputeIsNegativeEgpLookupTables();

            ComputeGaLookupTables();

            ComputeVectorKVectorOpIndexLookupTables();
        }


        private static void ComputeIsNegativeEgpLookupTables()
        {
            IsNegativeEgpLookupTables = new BitArray[MaxGaSpaceDimension];

            for (var id1 = 0; id1 < MaxGaSpaceDimension; id1++)
            {
                var bitArray = new BitArray(MaxGaSpaceDimension);

                var n = (ulong)id1;
                Parallel.For(
                    0, 
                    MaxGaSpaceDimension, 
                    id2 => { bitArray[id2] = GaFrameUtils.ComputeIsNegativeEGp(n, (ulong)id2); }
                );

                IsNegativeEgpLookupTables[id1] = bitArray;
            }
        }

        private static void ComputeIsNegativeVectorEgpLookupTables()
        {
            IsNegativeVectorEgpLookupTables = new BitArray[MaxGaSpaceDimension];

            for (var index1 = 0; index1 < MaxVSpaceDimension; index1++)
            {
                var bitArray = new BitArray(MaxGaSpaceDimension);

                var id1 = 1UL << index1;
                Parallel.For(
                    0,
                    MaxGaSpaceDimension,
                    id2 => { bitArray[id2] = GaFrameUtils.ComputeIsNegativeEGp(id1, (ulong)id2); }
                );

                IsNegativeVectorEgpLookupTables[index1] = bitArray;
            }
        }

        private static void ComputeGaLookupTables()
        {
            var gradeCount = new ulong[MaxVSpaceDimension + 1];

            //Initialize all tables
            IdToGradeTable = new int[MaxGaSpaceDimension];
            IdToIndexTable = new ulong[MaxGaSpaceDimension];
            IsNegativeReverseTable = new BitArray(MaxGaSpaceDimension);
            IsNegativeGradeInvTable = new BitArray(MaxGaSpaceDimension);
            IsNegativeCliffConjTable = new BitArray(MaxGaSpaceDimension);
            GradeIndexToIdTable = new List<ulong[]>(MaxVSpaceDimension);
            GradeIndexToMaxBasisVectorIndexTable = new List<int[]>(MaxVSpaceDimension);
            
            for (var id = 0; id <= MaxBasisBladeId; id++)
            {
                var grade = id.CountOnes();

                IdToGradeTable[id] = grade;

                //Calculate grade inversion sign
                if (grade.GradeHasNegativeGradeInvolution())
                    IsNegativeGradeInvTable.Set(id, true);

                //Calculate reversion sign
                if (grade.GradeHasNegativeReverse())
                    IsNegativeReverseTable.Set(id, true);

                //Calculate Clifford conjugate sign
                if (grade.GradeHasNegativeCliffordConjugate())
                    IsNegativeCliffConjTable.Set(id, true);

                //Calculate index of basis blade ID
                IdToIndexTable[id] = gradeCount[grade];

                gradeCount[grade] += 1;
            }

            //Calculate inverse index table: (grade, index) to ID table
            gradeCount = new ulong[MaxVSpaceDimension + 1];

            for (var id = 0; id <= MaxBasisBladeId; id++)
            {
                var grade = IdToGradeTable[id];
                var index = gradeCount[grade];

                gradeCount[grade] += 1;

                if (gradeCount[grade] == 1)
                {
                    GradeIndexToIdTable.Add(
                        new ulong[MaxVSpaceDimension.GetBinomialCoefficient(grade)]
                    );

                    GradeIndexToMaxBasisVectorIndexTable.Add(
                        new int[MaxVSpaceDimension.GetBinomialCoefficient(grade)]
                    );
                }

                GradeIndexToIdTable[grade][index] = (ulong)id;
                GradeIndexToMaxBasisVectorIndexTable[grade][index] = id.LastOneBitPosition();
            }
        }

        private static void ComputeVectorKVectorOpIndexLookupTables()
        {
            const int maxDim = 15;
            const int minVSpaceDim = 12;

            var maxVSpaceDim =
                GaFrameUtils.MaxVSpaceDimension < maxDim
                    ? GaFrameUtils.MaxVSpaceDimension
                    : maxDim;

            //var maxGaSpaceDim = (1 << maxVSpaceDim);

            VectorKVectorOpIndexLookupTables = new int[maxVSpaceDim - minVSpaceDim + 1][][][];

            for (var vSpaceDim = minVSpaceDim; vSpaceDim <= maxVSpaceDim; vSpaceDim++)
            {
                var vectorKVectorOpIndexLookupTable = new int[vSpaceDim - 1][][];

                for (var grade = 1; grade < vSpaceDim; grade++)
                {
                    var lookupTable = new int[vSpaceDim.GetBinomialCoefficient(grade + 1)][];

                    var resultIdsList =
                        GaFrameUtils.BasisBladeIDsOfGrade(vSpaceDim, grade + 1);

                    var lookupTableIndex = 0;
                    foreach (var id in resultIdsList)
                    {
                        var indexList1 = id.PatternToPositions().ToArray();
                        var lookupTableItems = new List<int>(2 * indexList1.Length);

                        foreach (var index1 in indexList1)
                        {
                            var id1 = 1 << index1;
                            var id2 = (ulong)((int)id ^ id1);
                            var index2 = (int)id2.BasisBladeIndex();

                            lookupTableItems.Add(index1);
                            lookupTableItems.Add(index2);
                        }

                        lookupTable[lookupTableIndex] = lookupTableItems.ToArray();

                        lookupTableIndex++;
                    }

                    vectorKVectorOpIndexLookupTable[grade - 1] = lookupTable;
                }

                VectorKVectorOpIndexLookupTables[vSpaceDim - minVSpaceDim] = vectorKVectorOpIndexLookupTable;
            }
        }
    }
}
