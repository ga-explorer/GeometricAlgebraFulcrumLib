using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.Numbers;
using GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Context;

namespace GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.HeadSpecs
{
    public sealed record SymbolicHeadSpecsNumberRational : 
        ISymbolicHeadSpecsNumber
    {
        public static SymbolicHeadSpecsNumberRational Create(SymbolicContext context, long numerator, long denominator)
        {
            return new SymbolicHeadSpecsNumberRational(
                context,
                numerator,
                denominator
            );
        }

        
        public SymbolicContext Context { get; }

        public long Numerator { get; }

        public long Denominator { get; }

        public double NumberFloat64Value 
            => Numerator / (double) Denominator;

        public string NumberText
            => SymbolicNumber.GetRationalNumberText(Numerator, Denominator);

        public string HeadText 
            => NumberFloat64Value.ToString("G");

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


        private SymbolicHeadSpecsNumberRational([NotNull] SymbolicContext context, long numerator, long denominator)
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