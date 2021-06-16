using GeometricAlgebraLib.Implementations.NamedScalars;
using GeometricAlgebraLib.Symbolic.Processors;
using Wolfram.NETLink;

namespace GeometricAlgebraLib.Symbolic.NamedScalars
{
    public sealed class GaNamedMathematicaExprCollection :
        GaNamedScalarsCollection<Expr>
    {
        public GaNamedMathematicaExprCollection() 
            : base(GaScalarProcessorMathematicaExpr.DefaultProcessor, "tmpVar")
        {
            //NamedScalarProcessor = new GaScalarProcessorNamedScalar<TScalar>(this);
        }

        public GaNamedMathematicaExprCollection(string defaultSymbolName) 
            : base(GaScalarProcessorMathematicaExpr.DefaultProcessor, defaultSymbolName)
        {
            //NamedScalarProcessor = new GaScalarProcessorNamedScalar<TScalar>(this);
        }

        
    }
}
