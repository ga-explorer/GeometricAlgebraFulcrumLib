﻿using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.Structures;
using GeometricAlgebraFulcrumLib.Matlab.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Combibnations;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Basis;

public static class BasisBladeDataLookup
{
    private static BasisBladeData[] BasisBladeDataArray { get; }

    private static BasisBladeData[][] BasisBladeDataGradedArray { get; }


    public static uint MaxVSpaceDimension { get; } 
        = 13;

    public static ulong MaxGaSpaceDimension { get; } 
        = 1U << (int) MaxVSpaceDimension;

    public static ulong MaxBasisBladeId { get; } 
        = (1U << (int) MaxVSpaceDimension) - 1;
    
    /// <summary>
    /// An index lookup table used to compute the outer product of a vector with a k-vector
    /// </summary>
    internal static int[][][][] VectorKVectorOpIndexLookupTables;


    static BasisBladeDataLookup()
    {
        Console.WriteLine($"Initializing GA lookup tables (max. of {MaxVSpaceDimension} dimensions) ..");
        var t1 = DateTime.Now;
        
        var dataSize = 0L;

        BasisBladeDataArray = new BasisBladeData[MaxGaSpaceDimension];
        BasisBladeDataGradedArray = new BasisBladeData[MaxVSpaceDimension + 1][];

        dataSize = BasisBladeDataArray.SizeInBytes();

        GenerateBasisBladeData();
            
        GenerateVectorKVectorOpIndexLookupTables();

        dataSize += BasisBladeDataGradedArray.SizeInBytes();

        dataSize += VectorKVectorOpIndexLookupTables.SizeInBytes();

        var t2 = DateTime.Now;
        Console.WriteLine($"GA lookup tables finished in: {(t2 - t1).TotalSeconds:G} seconds");
        Console.WriteLine($"GA lookup tables size: {dataSize:N0} bytes");
        Console.WriteLine();
    }
        

    private static void GenerateBasisBladeData()
    {
        for (var grade = 0U; grade <= MaxVSpaceDimension; grade++)
        {
            var kvSpaceDimensions = 
                MaxVSpaceDimension.GetBinomialCoefficient(grade);

            BasisBladeDataGradedArray[grade] = new BasisBladeData[kvSpaceDimensions];
        }

        var indexCountArray = new int[MaxVSpaceDimension + 1];

        for (var id = 0UL; id < MaxGaSpaceDimension; id++)
        {
            var grade = (uint) BitOperations.PopCount((uint) id);
            var index = (ulong) indexCountArray[grade];
            indexCountArray[grade]++;

            var basisBlade = 
                new BasisBladeData(MaxGaSpaceDimension, id, grade, index);

            BasisBladeDataArray[id] = basisBlade;
            BasisBladeDataGradedArray[grade][index] = basisBlade;
        }
    }
        
    private static void GenerateVectorKVectorOpIndexLookupTables()
    {
        const uint maxDim = 15U;
        const uint minDim = 12U;

        var maxVSpaceDim =
            MaxVSpaceDimension < maxDim
                ? MaxVSpaceDimension
                : maxDim;
        
        var minVSpaceDim =
            MaxVSpaceDimension < minDim
                ? MaxVSpaceDimension
                : minDim;

        //var maxGeoSpaceDim = (1 << maxVSpaceDim);

        VectorKVectorOpIndexLookupTables = new int[maxVSpaceDim - minVSpaceDim + 1][][][];

        for (var vSpaceDimensions = minVSpaceDim; vSpaceDimensions <= maxVSpaceDim; vSpaceDimensions++)
        {
            var vectorKVectorOpIndexLookupTable = new int[vSpaceDimensions - 1][][];

            for (var grade = 1U; grade < vSpaceDimensions; grade++)
            {
                var lookupTable = new int[vSpaceDimensions.GetBinomialCoefficient(grade + 1)][];

                var g = grade + 1;
                var resultIdsList =
                    vSpaceDimensions
                        .GetBinomialCoefficient(g)
                        .GetRange()
                        .Select(index => BasisBladeId(g, index));
                //vSpaceDimensions.BasisBladeIDsOfGrade(grade + 1);

                var lookupTableIndex = 0;
                foreach (var id in resultIdsList)
                {
                    var indexList1 = id.GetSetBitPositions().ToArray();
                    var lookupTableItems = new List<int>(2 * indexList1.Length);

                    foreach (var index1 in indexList1)
                    {
                        var id1 = 1 << index1;
                        var id2 = (ulong)((int)id ^ id1);
                        var index2 = (int) BasisBladeIndex(id2);

                        lookupTableItems.Add(index1);
                        lookupTableItems.Add(index2);
                    }

                    lookupTable[lookupTableIndex] = lookupTableItems.ToArray();

                    lookupTableIndex++;
                }

                vectorKVectorOpIndexLookupTable[grade - 1] = lookupTable;
            }

            VectorKVectorOpIndexLookupTables[vSpaceDimensions - minVSpaceDim] = vectorKVectorOpIndexLookupTable;
        }
    }


    
    public static bool ContainsBasisBladeData(ulong id)
    {
        return id < MaxGaSpaceDimension;
    }
        
    
    public static bool ContainsBasisBladeData(uint grade, ulong index)
    {
        return grade <= MaxVSpaceDimension &&
               index < (ulong) BasisBladeDataGradedArray[grade].Length;
    }
        
    
    public static bool ContainsBasisBladeDataOfGrade(uint grade)
    {
        return grade <= MaxVSpaceDimension;
    }
        
    
    public static bool GradeInvolutionIsPositive(ulong id)
    {
        return id < (ulong) BasisBladeDataArray.Length
            ? BasisBladeDataArray[id].GradeInvolutionIsPositive
            : ((uint) BitOperations.PopCount(id)).GradeInvolutionIsNegativeOfGrade();
    }

    
    public static bool GradeInvolutionIsNegative(ulong id)
    {
        return id < (ulong) BasisBladeDataArray.Length
            ? BasisBladeDataArray[id].GradeInvolutionIsNegative
            : ((uint) BitOperations.PopCount(id)).GradeInvolutionIsNegativeOfGrade();
    }
        
    
    public static IntegerSign GradeInvolutionSign(ulong id)
    {
        return id < (ulong) BasisBladeDataArray.Length
            ? BasisBladeDataArray[id].GradeInvolutionSign
            : ((uint) BitOperations.PopCount(id)).GradeInvolutionSignOfGrade();
    }

    
    public static bool ReverseIsPositive(ulong id)
    {
        return id < (ulong) BasisBladeDataArray.Length
            ? BasisBladeDataArray[id].ReverseIsPositive
            : ((uint) BitOperations.PopCount(id)).ReverseIsNegativeOfGrade();
    }

    
    public static bool ReverseIsNegative(ulong id)
    {
        return id < (ulong) BasisBladeDataArray.Length
            ? BasisBladeDataArray[id].ReverseIsNegative
            : ((uint) BitOperations.PopCount(id)).ReverseIsNegativeOfGrade();
    }
        
    
    public static IntegerSign ReverseSign(ulong id)
    {
        return id < (ulong) BasisBladeDataArray.Length
            ? BasisBladeDataArray[id].ReverseSign
            : ((uint) BitOperations.PopCount(id)).ReverseSignOfGrade();
    }
        
    
    public static bool CliffordConjugateIsPositive(ulong id)
    {
        return id < (ulong) BasisBladeDataArray.Length
            ? BasisBladeDataArray[id].CliffordConjugateIsPositive
            : ((uint) BitOperations.PopCount(id)).CliffordConjugateIsNegativeOfGrade();
    }

    
    public static bool CliffordConjugateIsNegative(ulong id)
    {
        return id < (ulong) BasisBladeDataArray.Length
            ? BasisBladeDataArray[id].CliffordConjugateIsNegative
            : ((uint) BitOperations.PopCount(id)).CliffordConjugateIsNegativeOfGrade();
    }
        
    
    public static IntegerSign CliffordConjugateSign(ulong id)
    {
        return id < (ulong) BasisBladeDataArray.Length
            ? BasisBladeDataArray[id].CliffordConjugateSign
            : ((uint) BitOperations.PopCount(id)).CliffordConjugateSignOfGrade();
    }

    
    public static uint BasisBladeGrade(ulong id)
    {
        return id < (ulong) BasisBladeDataArray.Length
            ? BasisBladeDataArray[id].Grade
            : (uint) BitOperations.PopCount(id);
    }
        
    
    public static ulong BasisBladeIndex(ulong id)
    {
        return id < (ulong) BasisBladeDataArray.Length
            ? BasisBladeDataArray[id].Index
            : id.CombinadicPatternToIndex();
    }
        
    
    public static Tuple<uint, ulong> BasisBladeGradeIndex(ulong id)
    {
        return id < (ulong) BasisBladeDataArray.Length
            ? BasisBladeDataArray[id].GradeIndex
            : new Tuple<uint, ulong>(
                (uint) BitOperations.PopCount(id), 
                id.CombinadicPatternToIndex()
            );
    }

    
    public static ulong BasisBladeId(uint grade, ulong index)
    {
        if (grade > BasisBladeDataGradedArray.Length) 
            return index.IndexToCombinadicPattern((int) grade);
            
        var table = 
            BasisBladeDataGradedArray[(int) grade];

        return index < (ulong) table.Length 
            ? table[index].Id 
            : index.IndexToCombinadicPattern((int) grade);
    }

    
    public static bool EGpIsNegative(ulong id1, ulong id2)
    {
        return ContainsBasisBladeData(id1) 
            ? BasisBladeDataArray[id1].EGpIsNegative(id2) 
            : BasisBladeDataComputer.EGpIsNegative(id1, id2);
    }

    
    public static bool EGpIsPositive(ulong id1, ulong id2)
    {
        return ContainsBasisBladeData(id1) 
            ? BasisBladeDataArray[id1].EGpIsPositive(id2) 
            : !BasisBladeDataComputer.EGpIsNegative(id1, id2);
    }
        
    
    public static IntegerSign EGpSign(ulong id1, ulong id2)
    {
        return ContainsBasisBladeData(id1) 
            ? BasisBladeDataArray[id1].EGpSign(id2) 
            : BasisBladeDataComputer.EGpSign(id1, id2);
    }
        
    
    public static IntegerSign EGpSquaredSign(ulong id)
    {
        return ContainsBasisBladeData(id) 
            ? BasisBladeDataArray[id].EGpSquaredSign 
            : BasisBladeDataComputer.EGpSign(id, id);
    }
        
    
    public static IntegerSign EGpReverseSign(ulong id1, ulong id2)
    {
        return ContainsBasisBladeData(id1) 
            ? BasisBladeDataArray[id1].EGpReverseSign(id2) 
            : BasisBladeDataComputer.EGpReverseSign(id1, id2);
    }

}