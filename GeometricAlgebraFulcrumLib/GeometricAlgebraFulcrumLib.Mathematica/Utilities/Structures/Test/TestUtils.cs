using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures.Expression;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures.Test;

public static class TestUtils
{
    public static readonly LinearTextComposer Log = new LinearTextComposer();

    public static readonly MathematicaInterface Cas = MathematicaInterface.Create();


    static TestUtils()
    {
        Log.AddStopWatchHeader();
    }


    public static void AddTestStartingMessage(string text)
    {
        Log.AppendLineAtNewLine("".PadLeft(80, '='));
        Log.AppendLine(text);
        Log.AppendLine();
    }

    public static void AddTestCompletionMessage(string text)
    {
        Log.AppendLineAtNewLine(text);
        Log.AppendLine("".PadLeft(80, '='));
    }

    public static string DescribeScalar(MathematicaScalar scalar)
    {
        return scalar + " = (" + scalar.N() + ")";
    }

    public static string DescribeScalar(MathematicaScalar scalar, int percision)
    {
        return scalar + " = (" + scalar.N(percision) + ")";
    }

    public static string DescribeVector(MathematicaVector vector)
    {
        return vector.ToString();
    }

    public static string DescribeMatrix(MathematicaMatrix matrix)
    {
        return matrix.ToString();
    }


    public static void AddTest(string message, MathematicaScalar result)
    {
        Log.AppendAtNewLine(message);
        Log.AppendLine(DescribeScalar(result));
        Log.AppendLine();
    }

    public static void AddTest(string message, MathematicaVector result)
    {
        Log.AppendAtNewLine(message);
        Log.AppendLine(DescribeVector(result));
        Log.AppendLine();
    }

    public static void AddTest(string message, MathematicaMatrix result)
    {
        Log.AppendAtNewLine(message);
        Log.AppendLine(DescribeMatrix(result));
        Log.AppendLine();
    }

    public static void AddTest<T>(string message, T result)
    {
        Log.AppendAtNewLine(message);
        Log.AppendLine(result.ToString());
        Log.AppendLine();
    }


}