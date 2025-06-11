using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeometricAlgebraFulcrumLib.Matlab.Structures;
using GeometricAlgebraFulcrumLib.Matlab.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Combibnations;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra;

public static class GaTextUtils
{
    
    public static string GetBasisVectorText(this ulong index)
    {
        return GetBasisBladeText(
            [index]
        );
    }

    
    public static string GetBasisBladeText(this ulong id)
    {
        return GetBasisBladeText(
            id.GetSetBitPositions()
        );
    }

    
    public static string GetBasisBladeText(uint grade, ulong index)
    {
        //return $"<Grade:{grade}, Index:{index}>";
        return GetBasisBladeText(
            index.IndexToCombinadic((int) grade)
        );
    }

    //public static string GetBasisBladeText(this BasisBlade basisBlade)
    //{
    //    return GetBasisBladeText(basisBlade.Id);
    //}
    
    
    public static string GetBasisBladeText(this IEnumerable<int> indexList)
    {
        return indexList.Select(i => i.ToString()).ConcatenateText(", ", "<", ">");
    }

    
    public static string GetBasisBladeText(this IEnumerable<ulong> indexList)
    {
        return indexList.Select(i => i.ToString()).ConcatenateText(", ", "<", ">");
    }

    
    public static string GetScalarText(this double scalar)
    {
        return scalar.ToString("G");
    }

    
    public static string GetTermText(ulong id, double scalar)
    {
        return new StringBuilder()
            .Append($"{scalar:G} ")
            .Append(GetBasisBladeText(id))
            .ToString();
    }

    
    public static string GetTermText(uint grade, int index, double scalar)
    {
        return GetTermText(grade, (ulong) index, scalar);
    }

    
    public static string GetTermText(uint grade, ulong index, double scalar)
    {
        return new StringBuilder()
            .Append($"{GetScalarText(scalar)} ")
            .Append(GetBasisBladeText(grade, index))
            .ToString();
    }

    
    public static string GetTermText(this KeyValuePair<ulong, double> idScalarPair)
    {
        return GetTermText(
            idScalarPair.Key, 
            idScalarPair.Value
        );
    }

    //public static string GetTermText(this GradeIndexScalarRecord gradeIndexScalarTuple)
    //{
    //    return GetTermText(
    //        gradeIndexScalarTuple.Grade, 
    //        gradeIndexScalarTuple.Index,
    //        gradeIndexScalarTuple.Scalar
    //    );
    //}

    //public static string GetTermText(BasisBlade basisBlade, double scalar)
    //{
    //    return GetTermText(
    //        basisBlade.Id,
    //        scalar
    //    );
    //}
        
    
    public static string GetTermsText(this IEnumerable<KeyValuePair<ulong, double>> idScalarPairs)
    {
        return idScalarPairs
            .Select(GetTermText)
            .ConcatenateText(", ");
    }

    //public static string GetTermsText(this IEnumerable<IndexScalarRecord> idScalarTuples)
    //{
    //    return idScalarTuples
    //        .Select(GetTermText)
    //        .ConcatenateText(", ");
    //}

    //public static string GetTermsText(this IEnumerable<GradeIndexScalarRecord> gradeIndexScalarTuples)
    //{
    //    return gradeIndexScalarTuples
    //        .Select(GetTermText)
    //        .ConcatenateText(", ");
    //}

    
    public static string GetTermsText(uint grade, IEnumerable<KeyValuePair<ulong, double>> indexScalarPairs)
    {
        return indexScalarPairs
            .Select(indexScalarPair => GetTermText(grade, indexScalarPair.Key, indexScalarPair.Value))
            .ConcatenateText(", ");
    }

    //public static string GetTermsText(uint grade, IEnumerable<IndexScalarRecord> indexScalarTuples)
    //{
    //    return indexScalarTuples
    //        .Select(indexScalarPair => GetTermText(grade, indexScalarPair.Index, indexScalarPair.Scalar))
    //        .ConcatenateText(", ");
    //}
        
    public static string GetArrayText(this IReadOnlyList<double> array)
    {
        var composer = new StringBuilder();

        var scalarsCount = array.Count;

        for (var i = 0; i < scalarsCount; i++)
        {
            if (i > 0)
                composer.Append(", ");

            composer.Append(GetScalarText(array[i]));
        }

        return composer.ToString();
    }

    //public static string GetArrayText(this ILinVectorStorage array)
    //{
    //    return GetArrayText(array.ToArray());
    //}

    public static string GetArrayText(this double[,] array)
    {
        var composer = new StringBuilder();

        var rowsCount = array.GetLength(0);
        var colsCount = array.GetLength(1);

        for (var i = 0; i < rowsCount; i++)
        {
            if (i > 0)
                composer.AppendLine();

            for (var j = 0; j < colsCount; j++)
            {
                if (j > 0)
                    composer.Append(", ");

                composer.Append(GetScalarText(array[i, j]));
            }
        }

        return composer.ToString();
    }

    //public static string GetArrayText(this ILinMatrixStorage matrixStorage)
    //{
    //    return GetArrayText(matrixStorage.ToArray());
    //}
        

    //public static string GetArrayText(this ITextComposer composer, ILinVectorStorage vectorStorage)
    //{
    //    return composer.GetArrayText(vectorStorage.ToArray());
    //}
        
    //public static string GetArrayText(this ITextComposer composer, ILinMatrixStorage matrixStorage)
    //{
    //    return composer.GetArrayText(matrixStorage.ToArray());
    //}
}