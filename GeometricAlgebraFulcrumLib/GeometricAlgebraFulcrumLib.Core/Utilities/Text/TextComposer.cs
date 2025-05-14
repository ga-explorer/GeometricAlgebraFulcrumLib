using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Core.ComplexAlgebra;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Core.Utilities.Text;

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
    public string GetBasisBladeText(uint grade, ulong index)
    {
        return TextComposerUtils.GetBasisBladeText(grade, index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetBasisBladeText(IndexSet id)
    {
        return id.GetBasisBladeText();
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
    public string GetScalarText(IScalar<T> scalar)
    {
        return GetScalarText(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual string GetAngleText(LinFloat64Angle angle)
    {
        return $"{GetScalarText(ScalarProcessor.ScalarFromNumber(angle.DegreesValue).ScalarValue)} degrees";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual string GetAngleText(LinAngle<T> angle)
    {
        return $"{GetScalarText(angle.DegreesValue)} degrees";
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

    public virtual string GetNumberText(ComplexNumber<T> number)
    {
        if (number.Imaginary.IsZero())
            return GetScalarText(number.RealValue);

        var imagText = GetScalarText(number.ImaginaryValue);

        if (number.Real.IsZero())
            return $"({imagText}) i";

        var realText = GetScalarText(number.RealValue);

        return $"({realText}) + ({imagText}) i";
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
    public string GetTermText(uint grade, int index, double scalar)
    {
        return GetTermText(grade, (ulong)index, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetTermText(uint grade, int index, T scalar)
    {
        return GetTermText(grade, (ulong)index, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetTermText(IndexSet id, double scalar)
    {
        return new StringBuilder()
            .Append($"'{GetScalarText(scalar)}'")
            .Append(GetBasisBladeText(id))
            .ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetTermText(IndexSet id, T scalar)
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
    public string GetTermText(KeyValuePair<IndexSet, double> term)
    {
        return GetTermText(term.Key, term.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetTermText(KeyValuePair<IndexSet, T> term)
    {
        return GetTermText(term.Key, term.Value);
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
    public string GetTermsText(IEnumerable<KeyValuePair<IndexSet, double>> gradeIndexScalarTuples)
    {
        return gradeIndexScalarTuples
            .Select(GetTermText)
            .ConcatenateText(", ");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetTermsText(IEnumerable<KeyValuePair<IndexSet, T>> gradeIndexScalarTuples)
    {
        return gradeIndexScalarTuples
            .Select(GetTermText)
            .ConcatenateText(", ");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetTermsText(uint grade, IEnumerable<Tuple<ulong, T>> indexScalarTuples)
    {
        return indexScalarTuples
            .Select(indexScalarPair => GetTermText(grade, indexScalarPair.Item1, indexScalarPair.Item2))
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
    public string GetVectorText(LinFloat64Vector v)
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