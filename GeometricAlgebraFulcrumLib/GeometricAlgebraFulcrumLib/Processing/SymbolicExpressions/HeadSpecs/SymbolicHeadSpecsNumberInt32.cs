using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;

namespace GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.HeadSpecs
{
    public sealed record SymbolicHeadSpecsNumberInt32 : 
        ISymbolicHeadSpecsNumber
    {
        public static SymbolicHeadSpecsNumberInt32 Create(SymbolicContext context, int number)
        {
            return new SymbolicHeadSpecsNumberInt32(
                context,
                number
            );
        }

        
        public SymbolicContext Context { get; }

        public double NumberValue 
            => NumberValueInt32;

        public int NumberValueInt32 { get; }

        public string NumberText 
            => NumberValueInt32.ToString();

        public string HeadText 
            => NumberValueInt32.ToString();

        public bool IsNumber 
            => true;

        public bool IsSymbolicNumber 
            => false;

        public bool IsLiteralNumber 
            => true;

        public bool IsSymbolicNumberOrVariable 
            => false;

        public bool IsVariable 
            => false;

        public bool IsAtomic 
            => true;

        public bool IsComposite 
            => false;

        public bool IsFunction 
            => false;

        public bool IsOperator 
            => false;

        public bool IsArrayAccess 
            => false;


        private SymbolicHeadSpecsNumberInt32([NotNull] SymbolicContext context, int numberValue)
        {
            Context = context;
            NumberValueInt32 = numberValue;
        }


        public override string ToString()
        {
            return NumberText;
        }
    }
}