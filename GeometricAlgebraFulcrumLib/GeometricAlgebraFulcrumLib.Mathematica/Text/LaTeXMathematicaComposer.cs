using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica.ExprFactory;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using GeometricAlgebraFulcrumLib.Text;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Mathematica.Text
{
    public sealed class LaTeXMathematicaComposer
        : LaTeXComposer<Expr>
    {
        public static LaTeXMathematicaComposer DefaultComposer { get; }
            = new LaTeXMathematicaComposer();


        public LaTeXMathematicaComposer() 
            : base(ScalarAlgebraMathematicaProcessor.DefaultProcessor)
        {
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string GetAngleText(PlanarAngle angle)
        {
            return $"{GetScalarText(angle.Degrees.ToExpr())} Degree";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string GetScalarText(Expr scalar)
        {
            return MathematicaUtils.Cas.Connection.EvaluateToString(
                Mfs.EToString[Mfs.TeXForm[Mfs.HoldForm[scalar]]]
            );
        }

    }
}