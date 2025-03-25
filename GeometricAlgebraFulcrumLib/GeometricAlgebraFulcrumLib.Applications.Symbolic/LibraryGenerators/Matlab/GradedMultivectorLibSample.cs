using System.Text;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.Matlab.GradedMultivectorsLib;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.Matlab;

public static class GradedMultivectorLibSample
{
    private static IEnumerable<Triplet<int>> GetMainSignatures(int maxVSpaceDimensions)
    {
        for (var p = 0; p <= maxVSpaceDimensions; p++)
        for (var q = 0; q <= maxVSpaceDimensions; q++)
        for (var r = 0; r <= maxVSpaceDimensions; r++)
        {
            var n = p + q + r;

            if (n < 2 || n > maxVSpaceDimensions) continue;

            yield return new Triplet<int>(p, q, r);
        }

        //for (var n = 2; n <= maxVSpaceDimensions; n++)
        //{
        //    // Euclidean GA and Elliptic PGA 
        //    yield return new Triplet<int>(n, 0, 0);

        //    if (n < 3) continue;

        //    // Conformal GA and Hyperbolic PGA
        //    yield return new Triplet<int>(n - 1, 1, 0);

        //    // Euclidean PGA
        //    yield return new Triplet<int>(n - 1, 0, 1);
        //}

        //for (var n = 2; n <= maxVSpaceDimensions / 2; n++)
        //{
        //    // Mother Algebra
        //    yield return new Triplet<int>(n, n, 0);
        //}

        // Conic GA(5,3)
        yield return new Triplet<int>(5, 3, 0);

        // VGA
        for (var p = maxVSpaceDimensions + 1; p <= 12; p++)
            yield return new Triplet<int>(p, 0, 0);
    }

    private static IEnumerable<Triplet<int>> GetFullSignatures(int maxVSpaceDimensions)
    {
        for (var p = 2; p <= maxVSpaceDimensions; p++)
            for (var q = 0; q <= maxVSpaceDimensions; q++)
                for (var r = 0; r <= maxVSpaceDimensions; r++)
                {
                    var n = p + q + r;

                    if (n < 2 || n > maxVSpaceDimensions) continue;

                    yield return new Triplet<int>(p, q, r);
                }
    }

    private static string GetSpaceName(int p, int q, int r)
    {
        if (p > 9 || q > 9 || r > 9)
        {
            if (r != 0)
                return $"GaP{p}Q{q}R{r}";

            if (q != 0)
                return $"GaP{p}Q{q}";

            return $"GaP{p}";
        }

        if (r != 0)
            return $"Ga{p}{q}{r}";

        if (q != 0)
            return $"Ga{p}{q}";

        return $"Ga{p}";
    }

    private static IEnumerable<LibCodeComposerSpecs> GetCodeComposerSpecs(int maxVSpaceDimensions)
    {
        foreach (var (p, q, r) in GetMainSignatures(maxVSpaceDimensions))
        {
            var vSpaceDimensions = p + q + r;
            var metric = RGaFloat64Processor.Create(q, r);
            var spaceName = GetSpaceName(p, q, r);

            yield return new LibCodeComposerSpecs(
                vSpaceDimensions,
                metric,
                spaceName
            )
            {
                LaTeXBasisSymbol = "e"
            };
        }
    }

    public static void GenerateCode()
    {
        // Max recommended dimensions for code generation is 6

        foreach (var codeSpecs in GetCodeComposerSpecs(5))
        {
            Console.Write(codeSpecs.GaSpaceName + " .. ");

            var composer = codeSpecs.CreateMainCodeComposer();

            var codeFilesComposer =
                composer.GenerateCode();
            
            codeFilesComposer.FilesEncoding = Encoding.ASCII;

            codeFilesComposer.SaveToFolder(
                @"D:\Projects\Study\Matlab Programming\GA-FuL Toolbox"
            );

            Console.WriteLine("done.");
            Console.WriteLine();
        }
    }


    //private static IReadOnlyList<string> GetLaTeXBasisVectorSubscripts(string gaName, int vSpaceDimensions)
    //{
    //    var textArray = new string[vSpaceDimensions];

    //    if (gaName == "EGa")
    //    {
    //        for (var i = 0; i < vSpaceDimensions; i++)
    //            textArray[i] = (i + 1).ToString();
    //    }
    //    else if (gaName == "PGa")
    //    {
    //        for (var i = 0; i < vSpaceDimensions - 1; i++)
    //            textArray[i] = (i + 1).ToString();

    //        textArray[vSpaceDimensions - 1] = "0";
    //    }
    //    else if (gaName == "CGa")
    //    {
    //        for (var i = 0; i < vSpaceDimensions - 2; i++)
    //            textArray[i] = (i + 1).ToString();

    //        textArray[vSpaceDimensions - 2] = "o";
    //        textArray[vSpaceDimensions - 1] = @"\infty";
    //    }

    //    return textArray;
    //}

    //private static double[,] GetLaTeXBasisVectorMap(string gaName, int vSpaceDimensions)
    //{
    //    // If linearly independent basis F = <f1, f2, f3> is related to
    //    // orthonormal basis E = <e1, e2, e3> via matrix M (F = M E), then
    //    // the scalars of a multivector expressed using E (Ae) are related
    //    // to the scalars of the same multivectors expressed using basis F
    //    // (Af) using the inverse transpose of M: Af = inv(transpose(M)) Ae
    //    // Thus if M is an orthogonal matrix (or as a special case, a permutation)
    //    // they are related using M itself: Af = M Ae.

    //    var vectorMapArray = new double[vSpaceDimensions, vSpaceDimensions];

    //    if (gaName == "EGa")
    //    {
    //        for (var i = 0; i < vSpaceDimensions; i++)
    //            vectorMapArray[i, i] = 1d;
    //    }
    //    else if (gaName == "PGa")
    //    {
    //        for (var i = 0; i < vSpaceDimensions - 1; i++)
    //            vectorMapArray[i, i + 1] = 1;

    //        vectorMapArray[vSpaceDimensions - 1, 0] = 1d;
    //    }
    //    else if (gaName == "CGa")
    //    {
    //        for (var i = 0; i < vSpaceDimensions - 2; i++)
    //            vectorMapArray[i, i + 2] = 1d;

    //        vectorMapArray[vSpaceDimensions - 2, 0] = 1d;
    //        vectorMapArray[vSpaceDimensions - 1, 0] = 0.5d;

    //        vectorMapArray[vSpaceDimensions - 2, 1] = 1d;
    //        vectorMapArray[vSpaceDimensions - 1, 1] = -0.5d;
    //    }
    //    else
    //        throw new InvalidOperationException();

    //    return vectorMapArray;
    //}

    //public static void GenerateCGa5D()
    //{
    //    var gaNameArray = new string[]
    //    {
    //        "EGa",
    //        "PGa",
    //        "CGa"
    //    };

    //    var metricArray = new RGaFloat64Processor[]
    //    {
    //        RGaFloat64Processor.Euclidean,
    //        RGaFloat64Processor.Projective,
    //        RGaFloat64Processor.Conformal
    //    };

    //    var vSpaceDimensionsRangeArray = new Int32Range1D[]
    //    {
    //        Int32Range1D.Create(2, 6),
    //        Int32Range1D.Create(3, 6),
    //        Int32Range1D.Create(4, 6),
    //    };

    //    for (var i = 0; i < gaNameArray.Length; i++)
    //    {
    //        var gaName = gaNameArray[i];
    //        var metric = metricArray[i];
    //        var vSpaceDimensionsRange = vSpaceDimensionsRangeArray[i];

    //        foreach (var vSpaceDimensions in vSpaceDimensionsRange)
    //        {
    //            var spaceName = 
    //                $"{gaName}{vSpaceDimensions}D";

    //            Console.Write(spaceName + "...");

    //            var composerSpecs = new LibCodeComposerSpecs(
    //                vSpaceDimensions,
    //                metric,
    //                spaceName,
    //                string.Empty)
    //            {
    //                LaTeXBasisSymbol = "e",
    //                //LaTeXBasisVectorSubscripts = GetLaTeXBasisVectorSubscripts(gaName, vSpaceDimensions),
    //                //LaTeXBasisVectorMap = GetLaTeXBasisVectorMap(gaName, vSpaceDimensions)
    //            };

    //            var composer = composerSpecs.CreateMainCodeComposer();

    //            var codeFilesComposer = 
    //                composer.GenerateCode();

    //            codeFilesComposer.SaveToFolder(
    //                @"D:\Projects\Code Gen"
    //            );

    //            //File.WriteAllText(
    //            //    @$"D:\Projects\{spaceName}.cs", 
    //            //    code
    //            //);

    //            Console.WriteLine("done.");
    //            Console.WriteLine();
    //        }
    //    }
    //}
}