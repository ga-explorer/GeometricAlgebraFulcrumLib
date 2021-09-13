using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Context;

namespace GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.HeadSpecs
{
    public sealed record SymbolicHeadSpecsNumberFloat32 : 
        ISymbolicHeadSpecsNumber
    {
        public static SymbolicHeadSpecsNumberFloat32 Create(SymbolicContext context, float number)
        {
            return new SymbolicHeadSpecsNumberFloat32(
                context,
                number
            );
        }

        
        public SymbolicContext Context { get; }


        public float NumberFloat32Value { get; }

        public double NumberFloat64Value 
            => NumberFloat32Value;

        public string NumberText 
            => NumberFloat32Value.ToString("G");

        public string HeadText 
            => NumberFloat32Value.ToString("G");

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


        private SymbolicHeadSpecsNumberFloat32([NotNull] SymbolicContext context, float numberValue)
        {
            Context = context;
            NumberFloat32Value = numberValue;
        }


        public override string ToString()
        {
            return NumberText;
        }
    }
}