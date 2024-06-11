using System;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Markdown.Tables;

namespace GeometricAlgebraFulcrumLib.Samples.Modeling.Geometry.PGa;

public static class PGaSamples
{
    /// <summary>
    /// Display PGA basis blades
    /// </summary>
    public static void PGaBasisExample()
    {
        for (var n = 3; n <= 10; n++)
        {
            var pga = 
                PGaGeometricSpace<double>.Create(
                    ScalarProcessorOfFloat64.Instance, 
                    n + 1
                );
            
            //var ega = CGaFloat64GeometricSpace.Create(n + 2);
            var cga = CGaFloat64GeometricSpace.Create(n + 2);

            //var egaBasisBlades =
            //    ega.GetBasisBladesVGa().ToArray();

            var pgaBasisBlades1 =
                pga.GetBasisBladesPGa().ToArray();

            var pgaBasisBlades2 =
                cga.GetBasisBladesPGa().ToArray();

            for (var id = 0; id < (1 << n); id++)
            {
                var basisText1 = pgaBasisBlades1[id].ToLaTeX();
                var basisText2 = pgaBasisBlades1[id].PGaDual().PGaUnDual().ToLaTeX();

                Debug.Assert(basisText1 == basisText2);

                var basisDualText1 = pgaBasisBlades1[id].PGaDual().ToLaTeX();
                var basisDualText2 = pgaBasisBlades2[id].PGaDual().ToLaTeX();

                Debug.Assert(basisDualText1 == basisDualText2);
                
                //var basisUnDualText1 = pgaBasisBlades1[id].PGaUnDual().ToLaTeX();
                //var basisUnDualText2 = pgaBasisBlades2[id].PGaUnDual().ToLaTeX();

                //Debug.Assert(basisUnDualText1 == basisUnDualText2);
            }

            Console.WriteLine("PGA (= CGAo) Basis Blades:");

            var mdTableComposer = new MarkdownTable();

            mdTableComposer.AddColumn("basis", "PGA Basis Blade");
            mdTableComposer.AddColumn("pga-dual1", "CGA-PGA Dual");
            mdTableComposer.AddColumn("pga-dual2", "PGA Dual");
            mdTableComposer.AddColumn("pga-dual3", "PGA Dual-UnDual");

            mdTableComposer[0].AddRange(
                pgaBasisBlades1.Select(kv =>
                    $"${kv.ToLaTeX()}$"
                )
            );

            mdTableComposer[1].AddRange(
                pgaBasisBlades2.Select(kv =>
                    $"${kv.PGaDual().ToLaTeX()}$"
                )
            );

            mdTableComposer[2].AddRange(
                pgaBasisBlades1.Select(kv =>
                    $"${kv.PGaDual().ToLaTeX()}$"
                )
            );

            mdTableComposer[3].AddRange(
                pgaBasisBlades1.Select(kv =>
                    $"${kv.PGaDual().PGaUnDual().ToLaTeX()}$"
                )
            );

            Console.WriteLine(mdTableComposer.ToString());

            Console.WriteLine();
        }
    }

    public static void Example1()
    {
        var cga = CGaFloat64GeometricSpace.Space5D;

        var plane1 = cga.DefineFlatPlane(
            LinFloat64Vector3D.Create(1, 2, -1),
            LinFloat64Vector3D.Create(1, -1, 1)
        );


    }
}