using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Composite;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;

namespace GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.HeadSpecs
{
    public readonly struct SymbolicHeadSpecsFunction : 
        ISymbolicHeadSpecsFunction
    {
        public static SymbolicHeadSpecsFunction Create(SymbolicContext context, string functionName, bool isLeftAssociative, bool isRightAssociative)
        {
            return new SymbolicHeadSpecsFunction(
                context, 
                functionName, 
                isLeftAssociative, 
                isRightAssociative
            );
        }

        public static SymbolicHeadSpecsFunction CreateLeftAssociative(SymbolicContext context, string functionName)
        {
            return new SymbolicHeadSpecsFunction(
                context, 
                functionName, 
                true, 
                false
            );
        }

        public static SymbolicHeadSpecsFunction CreateRightAssociative(SymbolicContext context, string functionName)
        {
            return new SymbolicHeadSpecsFunction(
                context, 
                functionName, 
                false, 
                true
            );
        }

        public static SymbolicHeadSpecsFunction CreateAssociative(SymbolicContext context, string functionName)
        {
            return new SymbolicHeadSpecsFunction(
                context, 
                functionName, 
                true, 
                true
            );
        }

        public static SymbolicHeadSpecsFunction CreateNonAssociative(SymbolicContext context, string functionName)
        {
            return new SymbolicHeadSpecsFunction(
                context, 
                functionName, 
                false, 
                false
            );
        }


        public SymbolicContext Context { get; }

        public string FunctionName { get; }

        public int Precedence 
            => 0;

        public string HeadText 
            => FunctionName;

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
            => true;

        public bool IsOperator 
            => false;

        public bool IsArrayAccess 
            => false;

        public bool IsLeftAssociative { get; }

        public bool IsRightAssociative { get; }

        public bool IsAssociative 
            => IsLeftAssociative || IsRightAssociative;

        public bool IsNonAssociative 
            => !IsLeftAssociative && !IsRightAssociative;

        public SymbolicFunctionAssociationKind AssociationKind 
            => IsLeftAssociative
            ? (IsRightAssociative 
                ? SymbolicFunctionAssociationKind.LeftRight 
                : SymbolicFunctionAssociationKind.Left)
            : (IsRightAssociative 
                ? SymbolicFunctionAssociationKind.Right 
                : SymbolicFunctionAssociationKind.None);


        private SymbolicHeadSpecsFunction([NotNull] SymbolicContext context, [NotNull] string functionName, bool isLeftAssociative, bool isRightAssociative)
        {
            if (string.IsNullOrEmpty(functionName))
                throw new ArgumentNullException(nameof(functionName), @"Function name not initialized");

            Context = context;            
            FunctionName = functionName;
            IsLeftAssociative = isLeftAssociative;
            IsRightAssociative = isRightAssociative;
        }


        public ISymbolicFunction CreateFunction()
        {
            return new SymbolicFunction(this);
        }

        public ISymbolicFunction CreateFunction(ISymbolicExpression argument1)
        {
            return new SymbolicFunction(
                this, 
                new []
                {
                    argument1
                } 
            );
        }

        public ISymbolicFunction CreateFunction(ISymbolicExpression argument1, ISymbolicExpression argument2)
        {
            return new SymbolicFunction(
                this, 
                new []
                {
                    argument1,
                    argument2
                } 
            );
        }

        public ISymbolicFunction CreateFunction(ISymbolicExpression argument1, ISymbolicExpression argument2, ISymbolicExpression argument3)
        {
            return new SymbolicFunction(
                this, 
                new []
                {
                    argument1,
                    argument2,
                    argument3
                } 
            );
        }

        public ISymbolicFunction CreateFunction(params ISymbolicExpression[] argumentsList)
        {
            return new SymbolicFunction(
                this, 
                argumentsList
            );
        }

        public ISymbolicFunction CreateFunction(IEnumerable<ISymbolicExpression> argumentsList)
        {
            return new SymbolicFunction(
                this, 
                argumentsList
            );
        }


        public override string ToString()
        {
            return FunctionName;
        }
    }
}
