using System;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;

namespace GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.HeadSpecs
{
    public sealed record SymbolicHeadSpecsArrayAccess : 
        ISymbolicHeadSpecsComposite
    {
        public static SymbolicHeadSpecsArrayAccess Create(SymbolicContext context, string arrayName)
        {
            return new SymbolicHeadSpecsArrayAccess(context, arrayName);
        }


        public string ArrayName { get; }

        public SymbolicContext Context { get; }

        public string HeadText 
            => ArrayName;

        public bool IsNumber 
            => false;

        public bool IsSymbolicNumber 
            => false;

        public bool IsLiteralNumber 
            => false;

        public bool IsSymbolicNumberOrVariable 
            => false;

        public bool IsVariable 
            => false;

        public bool IsAtomic 
            => false;

        public bool IsComposite 
            => true;

        public bool IsFunction 
            => false;

        public bool IsOperator 
            => false;

        public bool IsArrayAccess
            => true;


        private SymbolicHeadSpecsArrayAccess([NotNull] SymbolicContext context, [NotNull] string arrayName)
        {
            if (string.IsNullOrEmpty(arrayName))
                throw new ArgumentNullException(nameof(arrayName), @"Array name not initialized");

            Context = context; 
            ArrayName = arrayName;
        }


        public override string ToString()
        {
            return ArrayName;
        }
    }
}
