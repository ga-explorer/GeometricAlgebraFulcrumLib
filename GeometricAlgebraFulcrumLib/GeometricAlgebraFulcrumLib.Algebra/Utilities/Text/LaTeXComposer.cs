using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Utilities.Structures;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Algebra.ComplexAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Algebra.Utilities.Text;

public class LaTeXComposer<T>
    : ITextComposer<T>
{
    public string BasisName { get; set; }
        = @"\boldsymbol{e}";

    public IScalarProcessor<T> ScalarProcessor { get; }

    public LaTeXComposerBasisFormat BasisFormat { get; set; }
        = LaTeXComposerBasisFormat.CommaSeparated;

    public int ScalarDecimals { get; set; }
        = 7;

    public double ZeroEpsilon { get; set; }
        = 1e-12d;


    public LaTeXComposer(IScalarProcessor<T> scalarProcessor)
    {
        ScalarProcessor = scalarProcessor;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetBasisBladeText(RGaBasisBlade basisBlade)
    {
        return GetBasisBladeText(basisBlade.Id);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetBasisBladeText(XGaBasisBlade basisBlade)
    {
        return GetBasisBladeText(basisBlade.Id);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual string GetAngleText(LinFloat64Angle angle)
    {
        var angleText = GetScalarText(angle.DegreesValue);

        return $"{angleText}^{{\\circ}}";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual string GetAngleText(LinAngle<T> angle)
    {
        var angleText = GetScalarText(angle.DegreesValue);

        return $"{angleText}^{{\\circ}}";
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
    public virtual string GetScalarText(double scalar)
    {
        if (scalar.IsNearZero(ZeroEpsilon))
            return "0";

        var valueText = scalar.ToString("G");

        if (!valueText.Contains("E"))
            return Math.Round(scalar, ScalarDecimals).ToString("G");

        var ePosition = valueText.IndexOf('E');

        var magnitude = double.Parse(valueText[..ePosition]);
        var magnitudeText = Math.Round(magnitude, ScalarDecimals).ToString("G");
        var exponentText = valueText[(ePosition + 1)..];

        return $@"{magnitudeText} \times 10^{{{exponentText}}}";
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
            return $@"\left( {imagText} \right) \mathbf{{i}}";

        var realText = GetScalarText(number.RealValue);

        return $@"\left( {realText} \right) + \left( {imagText} \right) \mathbf{{i}}";
    }

    public string GetTermText(uint grade, int index, double scalar)
    {
        return GetTermText(
            ((ulong)index).BasisBladeIndexToId(grade),
            ScalarProcessor.ScalarFromNumber(scalar).ScalarValue
        );
    }

    public string GetTermText(uint grade, int index, T scalar)
    {
        return GetTermText(
            ((ulong)index).BasisBladeIndexToId(grade),
            scalar
        );
    }

    public string GetTermText(IIndexSet id, double scalar)
    {
        return GetTermText(
            id,
            ScalarProcessor.ScalarFromNumber(scalar).ScalarValue
        );
    }

    public string GetTermText(IIndexSet id, T scalar)
    {
        var valueText = GetScalarText(scalar);

        if (id.IsEmptySet)
            return $@"\left( {valueText} \right)";

        var basisText = GetBasisBladeText(id);

        return $@"\left( {valueText} \right) {basisText}";
    }

    public string GetTermText(uint grade, ulong index, double scalar)
    {
        return GetTermText(
            index.BasisBladeIndexToId(grade),
            ScalarProcessor.ScalarFromNumber(scalar).ScalarValue
        );
    }

    public string GetTermText(uint grade, ulong index, T scalar)
    {
        return GetTermText(
            index.BasisBladeIndexToId(grade),
            scalar
        );
    }

    public string GetTermText(KeyValuePair<ulong, double> term)
    {
        return GetTermText(
            term.Key,
            ScalarProcessor.ScalarFromNumber(term.Value).ScalarValue
        );
    }

    public string GetTermText(KeyValuePair<ulong, T> term)
    {
        return GetTermText(term.Key, term.Value);
    }

    public string GetTermText(KeyValuePair<IIndexSet, double> term)
    {
        return GetTermText(term.Key, term.Value);
    }

    public string GetTermText(KeyValuePair<IIndexSet, T> term)
    {
        return GetTermText(term.Key, term.Value);
    }

    public string GetTermText(RGaBasisBlade basisBlade, double scalar)
    {
        return GetTermText(
            basisBlade.Id,
            ScalarProcessor.ScalarFromNumber(scalar).ScalarValue
        );
    }

    public string GetTermText(RGaBasisBlade basisBlade, T scalar)
    {
        return GetTermText(basisBlade.Id, scalar);
    }

    public string GetTermText(XGaBasisBlade basisBlade, double scalar)
    {
        return GetTermText(basisBlade.Id, scalar);
    }

    public string GetTermText(XGaBasisBlade basisBlade, T scalar)
    {
        return GetTermText(basisBlade.Id, scalar);
    }

    public string GetVectorTermsText(IEnumerable<KeyValuePair<int, double>> idScalarTuples)
    {
        return idScalarTuples
            .Select(GetVectorTermText)
            .ConcatenateText(" + ");
    }

    public string GetVectorTermsText(IEnumerable<KeyValuePair<int, T>> idScalarTuples)
    {
        return idScalarTuples
            .Select(GetVectorTermText)
            .ConcatenateText(" + ");
    }

    public string GetTermsText(IEnumerable<KeyValuePair<ulong, double>> idScalarTuples)
    {
        return idScalarTuples
            .Select(GetTermText)
            .ConcatenateText(" + ");
    }

    public string GetTermsText(IEnumerable<KeyValuePair<ulong, T>> idScalarTuples)
    {
        return idScalarTuples
            .Select(GetTermText)
            .ConcatenateText(" + ");
    }

    public string GetTermsText(IEnumerable<KeyValuePair<IIndexSet, double>> idScalarTuples)
    {
        return idScalarTuples
            .Select(GetTermText)
            .ConcatenateText(" + ");
    }

    public string GetTermsText(IEnumerable<KeyValuePair<IIndexSet, T>> idScalarTuples)
    {
        return idScalarTuples
            .Select(GetTermText)
            .ConcatenateText(" + ");
    }

    public string GetArrayText(IReadOnlyList<double> array)
    {
        var colsCount = array.Count;

        var textComposer = new StringBuilder();

        var ccc = string.Concat(
            Enumerable.Repeat("c", array.Count)
        );

        textComposer
            .AppendLine(@"\left(\begin{array}{" + ccc + "}");

        for (var j = 0; j < colsCount; j++)
        {
            textComposer.Append(GetScalarText(array[j]));

            if (j < colsCount - 1)
                textComposer.Append(@" & ");
        }

        textComposer
            .AppendLine()
            .AppendLine(@"\end{array}\right)");

        return textComposer.ToString();
    }

    public string GetArrayText(IReadOnlyList<T> array)
    {
        var colsCount = array.Count;

        var textComposer = new StringBuilder();

        var ccc = string.Concat(
            Enumerable.Repeat("c", array.Count)
        );

        textComposer
            .AppendLine(@"\left(\begin{array}{" + ccc + "}");

        for (var j = 0; j < colsCount; j++)
        {
            textComposer.Append(GetScalarText(array[j]));

            if (j < colsCount - 1)
                textComposer.Append(@" & ");
        }

        textComposer
            .AppendLine()
            .AppendLine(@"\end{array}\right)");

        return textComposer.ToString();
    }

    public string GetArrayText(double[,] array)
    {
        var rowsCount = array.GetLength(0);
        var colsCount = array.GetLength(1);

        var textComposer = new StringBuilder();

        var ccc = string.Concat(
            Enumerable.Repeat("c", array.GetLength(1))
        );

        textComposer
            .AppendLine(@"\left(\begin{array}{" + ccc + "}");

        for (var i = 0; i < rowsCount; i++)
        {
            for (var j = 0; j < colsCount; j++)
            {
                textComposer.Append(GetScalarText(array[i, j]));

                if (j < colsCount - 1)
                    textComposer.Append(@" & ");
            }

            if (i < rowsCount - 1)
                textComposer.AppendLine(@" \\");
        }

        textComposer
            .AppendLine()
            .AppendLine(@"\end{array}\right)");

        return textComposer.ToString();
    }

    public string GetArrayText(T[,] array)
    {
        var rowsCount = array.GetLength(0);
        var colsCount = array.GetLength(1);

        var textComposer = new StringBuilder();

        var ccc = string.Concat(
            Enumerable.Repeat("c", array.GetLength(1))
        );

        textComposer
            .AppendLine(@"\left(\begin{array}{" + ccc + "}");

        for (var i = 0; i < rowsCount; i++)
        {
            for (var j = 0; j < colsCount; j++)
            {
                textComposer.Append(GetScalarText(array[i, j]));

                if (j < colsCount - 1)
                    textComposer.Append(@" & ");
            }

            if (i < rowsCount - 1)
                textComposer.AppendLine(@" \\");
        }

        textComposer
            .AppendLine()
            .AppendLine(@"\end{array}\right)");

        return textComposer.ToString();
    }

    public string GetVectorText(LinFloat64Vector v)
    {
        return GetVectorTermsText(
            v.IndexScalarPairs.OrderBy(p => p.Key)
        );
    }

    public string GetVectorText(LinVector<T> v)
    {
        return GetVectorTermsText(
            v.IndexScalarPairs.OrderBy(p => p.Key)
        );
    }

    public string GetMultivectorText(RGaFloat64Multivector mv)
    {
        return GetTermsText(
            mv
                .IdScalarPairs
                .OrderByGradeIndex()
        );
    }

    public string GetMultivectorText(RGaMultivector<T> mv)
    {
        return GetTermsText(
            mv
                .IdScalarPairs
                .OrderByGradeIndex()
        );
    }

    public string GetMultivectorText(XGaFloat64Multivector mv)
    {
        return GetTermsText(
            mv
                .IdScalarPairs
                .OrderByGradeIndex()
        );
    }

    public string GetMultivectorText(XGaMultivector<T> mv)
    {
        return GetTermsText(
            mv
                .IdScalarPairs
                .OrderByGradeIndex()
        );
    }

    public string GetBasisVectorText(int index)
    {
        return GetBasisBladeText(new[] { index });
    }

    public string GetBasisBladeText(ulong id)
    {
        return GetBasisBladeText(
            id.PatternToPositions()
        );
    }

    public string GetBasisBladeText(uint grade, ulong index)
    {
        return GetBasisBladeText(
            index.IndexToCombinadic((int)grade)
        );
    }

    public string GetBasisBladeText(IIndexSet id)
    {
        return GetBasisBladeText((IEnumerable<int>)id);
    }

    public string GetBasisBladeText(IEnumerable<int> indexList)
    {
        if (BasisFormat == LaTeXComposerBasisFormat.OuterProduct)
            return indexList
                .Select(i => $"{BasisName}_{{{i + 1}}}")
                .Concatenate(@" \wedge ");

        var basisSubscript =
            BasisFormat == LaTeXComposerBasisFormat.CommaSeparated
                ? indexList.Select(i => i + 1).Concatenate(",")
                : indexList.Select(i => i + 1).Concatenate();

        return $"{BasisName}_{{{basisSubscript}}}";
    }

    public string GetVectorTermText(KeyValuePair<int, double> term)
    {
        var (index, value) = term;

        var valueText = GetScalarText(value);
        var basisText = GetBasisVectorText(index);

        return $@"\left( {valueText} \right) {basisText}";
    }

    public string GetVectorTermText(KeyValuePair<int, T> term)
    {
        var (index, value) = term;

        var valueText = GetScalarText(value);
        var basisText = GetBasisVectorText(index);

        return $@"\left( {valueText} \right) {basisText}";
    }

    public string GetVectorTermText(int index, double value)
    {
        var valueText = GetScalarText(value);
        var basisText = GetBasisVectorText(index);

        return $@"\left( {valueText} \right) {basisText}";
    }

    public string GetVectorTermText(int index, T value)
    {
        var valueText = GetScalarText(value);
        var basisText = GetBasisVectorText(index);

        return $@"\left( {valueText} \right) {basisText}";
    }

    public string GetTermText(ulong id, double value)
    {
        var valueText = GetScalarText(value);

        if (id == 0)
            return $@"\left( {valueText} \right)";

        var basisText = GetBasisBladeText(id);

        return $@"\left( {valueText} \right) {basisText}";
    }

    public string GetTermText(ulong id, T value)
    {
        var valueText = GetScalarText(value);

        if (id == 0)
            return $@"\left( {valueText} \right)";

        var basisText = GetBasisBladeText(id);

        return $@"\left( {valueText} \right) {basisText}";
    }

    public string GetArrayDisplayEquationText(double[,] array)
    {
        var textComposer = new StringBuilder();

        var code = GetArrayText(array).Trim();

        return textComposer
            .AppendLine(@"\[")
            .AppendLine(code)
            .AppendLine(@"\]")
            .AppendLine()
            .ToString();
    }

    public string GetArrayDisplayEquationText(T[,] array)
    {
        var textComposer = new StringBuilder();

        var code = GetArrayText(array).Trim();

        return textComposer
            .AppendLine(@"\[")
            .AppendLine(code)
            .AppendLine(@"\]")
            .AppendLine()
            .ToString();
    }

    //public string GetArrayDisplayEquationText(ILinMatrixStorage<T> array)
    //{
    //    var textComposer = new StringBuilder();

    //    var code = GetArrayText(array.ToArray()).Trim();

    //    return textComposer
    //        .AppendLine(@"\[")
    //        .AppendLine(code)
    //        .AppendLine(@"\]")
    //        .AppendLine()
    //        .ToString();
    //}

    //public string GetTermsEquationsArrayText(string rightHandSide, IEnumerable<BasisTerm<T>> termsList)
    //{
    //    var textComposer = new StringBuilder();

    //    textComposer.AppendLine(@"\begin{eqnarray*}");

    //    var termsArray = 
    //        termsList.OrderByGradeIndex().ToArray();

    //    var j = 0;
    //    foreach (var term in termsArray)
    //    {
    //        var termCode = GetTermText(term);

    //        var line = j == 0
    //            ? $@"{rightHandSide.Trim()} & = & {termCode}"
    //            : $@" & + & {termCode}";

    //        if (j < termsArray.Length - 1)
    //            line += @"\\";

    //        textComposer.AppendLine(line);

    //        j++;
    //    }

    //    textComposer.AppendLine(@"\end{eqnarray*}");

    //    return textComposer.ToString();
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LaTeXComposer<T> ConsoleWriteLine()
    {
        Console.WriteLine();

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LaTeXComposer<T> ConsoleWriteLine(string vText)
    {
        Console.WriteLine(vText);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LaTeXComposer<T> ConsoleWriteLine(double v, string vText)
    {
        Console.WriteLine(@$"${vText} = {GetScalarText(v)}$");

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LaTeXComposer<T> ConsoleWriteLine(T v, string vText)
    {
        Console.WriteLine(@$"${vText} = {GetScalarText(v)}$");

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LaTeXComposer<T> ConsoleWriteLine(Scalar<T> v, string vText)
    {
        Console.WriteLine(@$"${vText} = {GetScalarText(v)}$");

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LaTeXComposer<T> ConsoleWriteLine(IScalar<T> v, string vText)
    {
        Console.WriteLine(@$"${vText} = {GetScalarText(v)}$");

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LaTeXComposer<T> ConsoleWriteLine(RGaFloat64Multivector v, string vText)
    {
        Console.WriteLine(@$"${vText} = {GetMultivectorText(v)}$");

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LaTeXComposer<T> ConsoleWriteLine(RGaScalar<T> v, string vText)
    {
        Console.WriteLine(@$"${vText} = {GetMultivectorText(v)}$");

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LaTeXComposer<T> ConsoleWriteLine(RGaMultivector<T> v, string vText)
    {
        Console.WriteLine(@$"${vText} = {GetMultivectorText(v)}$");

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LaTeXComposer<T> ConsoleWriteLine(XGaFloat64Multivector v, string vText)
    {
        Console.WriteLine(@$"${vText} = {GetMultivectorText(v)}$");

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LaTeXComposer<T> ConsoleWriteLine(XGaMultivector<T> v, string vText)
    {
        Console.WriteLine(@$"${vText} = {GetMultivectorText(v)}$");

        return this;
    }
}