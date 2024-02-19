﻿using System.Runtime.CompilerServices;
using System.Text;
using DataStructuresLib;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic;

namespace GeometricAlgebraFulcrumLib.MathBase.Text;

public class TextComposer<T>
    : ITextComposer<T>
{
    public IScalarProcessor<T> ScalarProcessor { get; }
        
    public int RoundingDecimals { get; set; }
        = 15;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TextComposer(IScalarProcessor<T> scalarProcessor)
    {
        ScalarProcessor = scalarProcessor;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetBasisVectorText(int index)
    {
        return index.GetBasisVectorText();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetBasisBladeText(ulong id)
    {
        return id.GetBasisBladeText();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetBasisBladeText(uint grade, ulong index)
    {
        return TextComposerUtils.GetBasisBladeText(grade, index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetBasisBladeText(IIndexSet id)
    {
        return id.GetBasisBladeText();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetBasisBladeText(RGaBasisBlade basisBlade)
    {
        return basisBlade.GetBasisBladeText();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetBasisBladeText(XGaBasisBlade basisBlade)
    {
        return basisBlade.GetBasisBladeText();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetBasisBladeText(IEnumerable<int> indexList)
    {
        return indexList.GetBasisBladeText();

        //var composer = new StringBuilder();

        //composer.Append('<');

        //var firstItemFlag = true;
        //foreach (var index in indexList)
        //{
        //    if (firstItemFlag)
        //        firstItemFlag = false;
        //    else
        //        composer.Append(", ");

        //    composer.Append(index + 1);
        //}

        //composer.Append('>');

        //return composer.ToString();
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetScalarText(Scalar<T> scalar)
    {
        return GetScalarText(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual string GetAngleText(Float64PlanarAngle angle)
    {
        return $"{GetScalarText(ScalarProcessor.GetScalarFromNumber(angle.Degrees))} degrees";
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual string GetAngleText(PlanarAngle<T> angle)
    {
        return $"{GetScalarText(angle.Degrees)} degrees";
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual string GetScalarText(double scalar)
    {
        return Math.Round(scalar, RoundingDecimals).ToString("G");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual string GetScalarText(T scalar)
    {
        return ScalarProcessor.ToText(scalar);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetVectorTermText(int index, double scalar)
    {
        return new StringBuilder()
            .Append($"'{GetScalarText(scalar)}'")
            .Append(GetBasisVectorText(index))
            .ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetVectorTermText(int index, T scalar)
    {
        return new StringBuilder()
            .Append($"'{GetScalarText(scalar)}'")
            .Append(GetBasisVectorText(index))
            .ToString();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetTermText(ulong id, double scalar)
    {
        return new StringBuilder()
            .Append($"'{GetScalarText(scalar)}'")
            .Append(GetBasisBladeText(id))
            .ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetTermText(ulong id, T scalar)
    {
        return new StringBuilder()
            .Append($"'{GetScalarText(scalar)}'")
            .Append(GetBasisBladeText(id))
            .ToString();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetTermText(uint grade, int index, double scalar)
    {
        return GetTermText(grade, (ulong) index, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetTermText(uint grade, int index, T scalar)
    {
        return GetTermText(grade, (ulong) index, scalar);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetTermText(IIndexSet id, double scalar)
    {
        return new StringBuilder()
            .Append($"'{GetScalarText(scalar)}'")
            .Append(GetBasisBladeText(id))
            .ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetTermText(IIndexSet id, T scalar)
    {
        return new StringBuilder()
            .Append($"'{GetScalarText(scalar)}'")
            .Append(GetBasisBladeText(id))
            .ToString();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetTermText(uint grade, ulong index, double scalar)
    {
        return new StringBuilder()
            .Append($"'{GetScalarText(scalar)}'")
            .Append(GetBasisBladeText(grade, index))
            .ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetTermText(uint grade, ulong index, T scalar)
    {
        return new StringBuilder()
            .Append($"'{GetScalarText(scalar)}'")
            .Append(GetBasisBladeText(grade, index))
            .ToString();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetVectorTermText(KeyValuePair<int, double> term)
    {
        return GetVectorTermText(term.Key, term.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetVectorTermText(KeyValuePair<int, T> term)
    {
        return GetVectorTermText(term.Key, term.Value);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetTermText(KeyValuePair<ulong, double> term)
    {
        return GetTermText(term.Key, term.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetTermText(KeyValuePair<ulong, T> term)
    {
        return GetTermText(term.Key, term.Value);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetTermText(KeyValuePair<IIndexSet, double> term)
    {
        return GetTermText(term.Key, term.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetTermText(KeyValuePair<IIndexSet, T> term)
    {
        return GetTermText(term.Key, term.Value);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetTermText(RGaBasisBlade basisBlade, double scalar)
    {
        return GetTermText(
            basisBlade.Id,
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetTermText(RGaBasisBlade basisBlade, T scalar)
    {
        return GetTermText(
            basisBlade.Id,
            scalar
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetTermText(XGaBasisBlade basisBlade, double scalar)
    {
        return GetTermText(
            basisBlade.Id,
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetTermText(XGaBasisBlade basisBlade, T scalar)
    {
        return GetTermText(
            basisBlade.Id,
            scalar
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetVectorTermsText(IEnumerable<KeyValuePair<int, double>> idScalarTuples)
    {
        return idScalarTuples
            .Select(GetVectorTermText)
            .ConcatenateText(", ");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetVectorTermsText(IEnumerable<KeyValuePair<int, T>> idScalarTuples)
    {
        return idScalarTuples
            .Select(GetVectorTermText)
            .ConcatenateText(", ");
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetTermsText(IEnumerable<KeyValuePair<ulong, double>> idScalarTuples)
    {
        return idScalarTuples
            .Select(GetTermText)
            .ConcatenateText(", ");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetTermsText(IEnumerable<KeyValuePair<ulong, T>> idScalarTuples)
    {
        return idScalarTuples
            .Select(GetTermText)
            .ConcatenateText(", ");
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetTermsText(IEnumerable<KeyValuePair<IIndexSet, double>> gradeIndexScalarTuples)
    {
        return gradeIndexScalarTuples
            .Select(GetTermText)
            .ConcatenateText(", ");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetTermsText(IEnumerable<KeyValuePair<IIndexSet, T>> gradeIndexScalarTuples)
    {
        return gradeIndexScalarTuples
            .Select(GetTermText)
            .ConcatenateText(", ");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetTermsText(uint grade, IEnumerable<RGaKvIndexScalarRecord<T>> indexScalarTuples)
    {
        return indexScalarTuples
            .Select(indexScalarPair => GetTermText(grade, indexScalarPair.KvIndex, indexScalarPair.Scalar))
            .ConcatenateText(", ");
    }
        
    public string GetArrayText(IReadOnlyList<double> array)
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

    public string GetArrayText(IReadOnlyList<T> array)
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
        
    public virtual string GetArrayText(double[,] array)
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

    public virtual string GetArrayText(T[,] array)
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
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetVectorText(Float64Vector v)
    {
        return GetVectorTermsText(
            v.IndexScalarPairs.OrderBy(p => p.Key)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetVectorText(LinVector<T> v)
    {
        return GetVectorTermsText(
            v.IndexScalarPairs.OrderBy(p => p.Key)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetMultivectorText(RGaFloat64Multivector mv)
    {
        return GetTermsText(
            mv
                .IdScalarPairs
                .OrderByGradeIndex()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetMultivectorText(RGaMultivector<T> mv)
    {
        return GetTermsText(
            mv
                .IdScalarPairs
                .OrderByGradeIndex()
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetMultivectorText(XGaFloat64Multivector mv)
    {
        return GetTermsText(
            mv
                .IdScalarPairs
                .OrderByGradeIndex()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetMultivectorText(XGaMultivector<T> mv)
    {
        return GetTermsText(
            mv
                .IdScalarPairs
                .OrderByGradeIndex()
        );
    }
}