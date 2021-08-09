using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Numbers;

namespace GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.HeadSpecs
{
    public sealed record SymbolicHeadSpecsNumberRational : 
        ISymbolicHeadSpecsNumber
    {
        public static SymbolicHeadSpecsNumberRational Create(SymbolicContext context, int numerator, int denominator)
        {
            return new SymbolicHeadSpecsNumberRational(
                context,
                numerator,
                denominator
            );
        }

        
        public SymbolicContext Context { get; }

        public int Numerator { get; }

        public int Denominator { get; }

        public double NumberValue 
            => ((double) Numerator) / Denominator;

        public string NumberText
            => SymbolicNumber.GetRationalNumberText(Numerator, Denominator);

        public string HeadText 
            => NumberValue.ToString("G");

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


        private SymbolicHeadSpecsNumberRational([NotNull] SymbolicContext context, int numerator, int denominator)
        {
            Context = context;
            Numerator = numerator;
            Denominator = denominator;
        }


        public override string ToString()
        {
            return NumberText;
        }
    }
}