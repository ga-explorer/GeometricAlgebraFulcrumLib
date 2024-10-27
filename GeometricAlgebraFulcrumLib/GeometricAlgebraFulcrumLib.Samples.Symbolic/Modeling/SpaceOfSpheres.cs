using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Spaces.Conformal;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Mathematica.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Samples.Symbolic.Modeling
{
    public static class SpaceOfSpheresSamples
    {
        public static void SphereEquation()
        {
            var sp = ScalarProcessorOfWolframExpr.Instance;
            var cga = CGaGeometricSpace<Expr>.Create5D(sp);
            
            // CGA null vector of sphere with radius r and center (Cx, Cy, Cz)
            var c = cga.EncodeIpnsRound.RealSphere("r", "Cx", "Cy", "Cz");
            
            // General CGA point (x, y, z)
            var p = cga.EncodeIpnsRound.Point("x", "y", "z");

            // Equation of sphere:
            var eq = sp.Times(-2, p.Sp(c));

            Console.WriteLine($"${cga.BasisSpecs.ToLaTeX(eq)}$");
            Console.WriteLine();
        }
    }
}
