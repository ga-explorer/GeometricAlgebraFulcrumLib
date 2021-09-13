using System;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Context;

namespace GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.HeadSpecs
{
    public sealed record SymbolicHeadSpecsNumberSymbolic : 
        ISymbolicHeadSpecsNumber
    {
        public static SymbolicHeadSpecsNumberSymbolic Create(SymbolicContext context, double number)
        {
            return new SymbolicHeadSpecsNumberSymbolic(
                context,
                number.ToString("G"),
                number
            );
        }
        
        public static SymbolicHeadSpecsNumberSymbolic Create(SymbolicContext context, string numberText, double numberValue)
        {
            return new SymbolicHeadSpecsNumberSymbolic(
                context,
                numberText,
                numberValue
            );
        }

        
        public SymbolicContext Context { get; }

        public double NumberFloat64Value { get; }

        public string NumberText { get; }

        public string HeadText 
            => NumberText;

        public bool IsNumber 
            => true;

        public bool IsSymbolicNumber 
            => true;

        public bool IsLiteralNumber 
            => false;

        public bool IsSymbolicNumberOrVariable 
            => true;

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


        private SymbolicHeadSpecsNumberSymbolic([NotNull] SymbolicContext context, [NotNull] string numberText, double numberValue)
        {
            if (string.IsNullOrEmpty(numberText))
                throw new ArgumentNullException(nameof(numberText), @"Number value not initialized");

            Context = context;
            NumberText = numberText;
            NumberFloat64Value = numberValue;
        }


        public override string ToString()
        {
            return NumberText;
        }
    }
}
