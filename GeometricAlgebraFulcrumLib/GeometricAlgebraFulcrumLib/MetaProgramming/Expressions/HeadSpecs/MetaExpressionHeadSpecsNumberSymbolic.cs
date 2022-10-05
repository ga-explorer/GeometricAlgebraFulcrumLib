using System;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.HeadSpecs
{
    public sealed record MetaExpressionHeadSpecsNumberSymbolic : 
        IMetaExpressionHeadSpecsNumber
    {
        public static MetaExpressionHeadSpecsNumberSymbolic Create(MetaContext context, double number)
        {
            return new MetaExpressionHeadSpecsNumberSymbolic(
                context,
                number.ToString("G"),
                number
            );
        }
        
        public static MetaExpressionHeadSpecsNumberSymbolic Create(MetaContext context, string numberText, double numberValue)
        {
            return new MetaExpressionHeadSpecsNumberSymbolic(
                context,
                numberText,
                numberValue
            );
        }

        
        public MetaContext Context { get; }

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


        private MetaExpressionHeadSpecsNumberSymbolic([NotNull] MetaContext context, [NotNull] string numberText, double numberValue)
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
