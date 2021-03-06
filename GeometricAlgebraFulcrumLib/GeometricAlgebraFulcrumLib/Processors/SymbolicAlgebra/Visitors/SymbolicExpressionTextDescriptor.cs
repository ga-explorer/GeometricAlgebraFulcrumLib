using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra;
using Microsoft.CSharp.RuntimeBinder;

namespace GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Visitors
{
    public sealed class SymbolicExpressionTextDescriptor :
        ISymbolicExpressionDynamicVisitor<string>
    {
        public static SymbolicExpressionTextDescriptor DefaultDescriptor { get; }
            = new SymbolicExpressionTextDescriptor();


        public bool UseExceptions 
            => true;

        public bool IgnoreNullElements 
            => false;


        private SymbolicExpressionTextDescriptor()
        {
        }


        public string Fallback(ISymbolicExpression item, RuntimeBinderException excException)
        {
            throw new System.NotImplementedException();
        }


        //public string Visit(ISymbolicNumber expr)
        //{
        //    return expr.NumberHeadSpecs.IsLiteral
        //        ? expr.NumberText
        //        : ;
        //}
    }
}