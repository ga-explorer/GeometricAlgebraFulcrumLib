using GeometricAlgebraFulcrumLib.Mathematica.Mathematica.ExprFactory;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using GeometricAlgebraFulcrumLib.Utilities.Composers;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Mathematica.Text
{
    public sealed class MathematicaLaTeXComposer
        : LaTeXComposer<Expr>
    {
        public static MathematicaLaTeXComposer DefaultComposer { get; }
            = new MathematicaLaTeXComposer();


        public MathematicaLaTeXComposer() 
            : base(MathematicaScalarProcessor.DefaultProcessor)
        {
        }

        public override string GetScalarText(Expr scalar)
        {
            return GaSymbolicUtils.Cas.Connection.EvaluateToString(
                Mfs.EToString[Mfs.TeXForm[scalar]]
            );
        }
    }
}