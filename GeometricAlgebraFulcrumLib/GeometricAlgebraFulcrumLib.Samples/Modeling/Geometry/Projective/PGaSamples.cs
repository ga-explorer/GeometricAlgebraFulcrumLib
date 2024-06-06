using System;
using System.Linq;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Markdown.Tables;

namespace GeometricAlgebraFulcrumLib.Samples.Modeling.Geometry.Projective;

public static class PGaSamples
{
    /// <summary>
    /// Display PGA basis blades
    /// </summary>
    public static void PGaBasisExample()
    {
        for (var n = 3; n <= 9; n++)
        {
            var ega = RGaConformalSpace.Create(n + 2);
            var cga = RGaConformalSpace.Create(n + 1);

            var egaBasisBlades =
                ega.GetBasisBladesEGa().ToArray();

            var pgaBasisBlades =
                cga.GetBasisBladesPGa().ToArray();

            Console.WriteLine("PGA (= CGAo) Basis Blades:");

            var mdTableComposer = new MarkdownTable();

            mdTableComposer.AddColumn("basis", "PGA Basis Blade");
            mdTableComposer.AddColumn("pga-dual1", "PGA Dual");
            mdTableComposer.AddColumn("pga-dual2", "Classical Un-Dual");
            mdTableComposer.AddColumn("pga-dual3", "PGA Dual-Dual");

            mdTableComposer[0].AddRange(
                pgaBasisBlades.Select(kv =>
                    $"${kv.ToLaTeX()}$"
                )
            );

            mdTableComposer[1].AddRange(
                pgaBasisBlades.Select(kv =>
                    $"${kv.PGaDual().ToLaTeX()}$"
                )
            );

            var sign = n.IsEven() ? 1 : -1;

            mdTableComposer[2].AddRange(
                egaBasisBlades.Select(kv =>
                    $"${(sign * kv.EGaDual()).ToLaTeX()}$"
                )
            );

            mdTableComposer[3].AddRange(
                pgaBasisBlades.Select(kv =>
                    $"${kv.PGaDual().PGaDual().ToLaTeX()}$"
                )
            );

            Console.WriteLine(mdTableComposer.ToString());

            Console.WriteLine();
        }
    }

    public static void Example1()
    {
        var cga = RGaConformalSpace.Space5D;

        var plane1 = cga.DefineFlatPlane(
            LinFloat64Vector3D.Create(1, 2, -1),
            LinFloat64Vector3D.Create(1, -1, 1)
        );


    }
}