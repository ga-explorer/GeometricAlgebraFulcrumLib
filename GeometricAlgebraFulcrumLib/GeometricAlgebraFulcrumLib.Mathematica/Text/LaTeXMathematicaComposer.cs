using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
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
            : base(ScalarProcessorExpr.DefaultProcessor)
        {
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string GetAngleText(Float64PlanarAngle angle)
        {
            return $@"{GetScalarText(angle.Degrees.ToExpr())} \degree";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string GetScalarText(Expr scalar)
        {
            if (scalar is null)
                return "0";

            return MathematicaUtils.Cas.Connection.EvaluateToString(
                Mfs.EToString[Mfs.TeXForm[Mfs.HoldForm[scalar]]]
            );
        }

    }
}