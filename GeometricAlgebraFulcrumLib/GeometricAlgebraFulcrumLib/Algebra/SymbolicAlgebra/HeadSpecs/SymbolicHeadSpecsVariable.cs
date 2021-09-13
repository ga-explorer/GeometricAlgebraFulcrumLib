using System;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Context;

namespace GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.HeadSpecs
{
    public sealed record SymbolicHeadSpecsVariable : 
        ISymbolicHeadSpecsVariable
    {
        public static SymbolicHeadSpecsVariable Create(SymbolicContext context, string variableName)
        {
            return new SymbolicHeadSpecsVariable(
                context,
                variableName
            );
        }


        public string VariableName { get; }

        public SymbolicContext Context { get; }

        public string HeadText 
            => VariableName;

        public bool IsNumber 
            => false;

        public bool IsSymbolicNumber 
            => false;

        public bool IsLiteralNumber 
            => false;

        public bool IsSymbolicNumberOrVariable 
            => true;

        public bool IsVariable 
            => true;

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


        private SymbolicHeadSpecsVariable([NotNull] SymbolicContext context, [NotNull] string variableName)
        {
            if (string.IsNullOrEmpty(variableName))
                throw new ArgumentNullException(nameof(variableName), @"Variable name not initialized");

            Context = context;
            VariableName = variableName;
        }


        public override string ToString()
        {
            return VariableName;
        }
    }
}
