using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraLib.SymbolicExpressions.Composite;
using GeometricAlgebraLib.SymbolicExpressions.Context;

namespace GeometricAlgebraLib.SymbolicExpressions.HeadSpecs
{
    public readonly struct SymbolicHeadSpecsOperator : 
        ISymbolicHeadSpecsOperator
    {
        public static SymbolicHeadSpecsOperator Create(SymbolicContext context, string functionName, string opSymbol, int opPrecedence, SymbolicOperatorPosition opPosition, bool isLeftAssociative, bool isRightAssociative)
        {
            return new SymbolicHeadSpecsOperator(
                context,
                functionName,
                opSymbol,
                opPrecedence,
                opPosition,
                isLeftAssociative,
                isRightAssociative
            );
        }

        public static SymbolicHeadSpecsOperator CreateLeftAssociative(SymbolicContext context, string functionName, string opSymbol, int opPrecedence, SymbolicOperatorPosition opPosition)
        {
            return new SymbolicHeadSpecsOperator(
                context,
                functionName,
                opSymbol,
                opPrecedence,
                opPosition,
                true,
                false
            );
        }
        
        public static SymbolicHeadSpecsOperator CreateRightAssociative(SymbolicContext context, string functionName, string opSymbol, int opPrecedence, SymbolicOperatorPosition opPosition)
        {
            return new SymbolicHeadSpecsOperator(
                context,
                functionName,
                opSymbol,
                opPrecedence,
                opPosition,
                false,
                true
            );
        }

        public static SymbolicHeadSpecsOperator CreateAssociative(SymbolicContext context, string functionName, string opSymbol, int opPrecedence, SymbolicOperatorPosition opPosition)
        {
            return new SymbolicHeadSpecsOperator(
                context,
                functionName,
                opSymbol,
                opPrecedence,
                opPosition,
                true,
                true
            );
        }

        public static SymbolicHeadSpecsOperator CreateNonAssociative(SymbolicContext context, string functionName, string opSymbol, int opPrecedence, SymbolicOperatorPosition opPosition)
        {
            return new SymbolicHeadSpecsOperator(
                context,
                functionName,
                opSymbol,
                opPrecedence,
                opPosition,
                false,
                false
            );
        }


        public SymbolicContext Context { get; }

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
            => true;

        public bool IsArrayAccess 
            => false;

        public string FunctionName { get; }

        public int Precedence { get; }

        public string SymbolText { get; }

        public SymbolicOperatorPosition Position { get; }
        
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


        private SymbolicHeadSpecsOperator([NotNull] SymbolicContext context, [NotNull] string functionName, [NotNull] string opSymbol, int opPrecedence, SymbolicOperatorPosition opPosition, bool isLeftAssociative, bool isRightAssociative)
        {
            if (string.IsNullOrEmpty(opSymbol))
                throw new ArgumentNullException(nameof(opSymbol), @"Operator symbol not initialized");

            Context = context;
            FunctionName = functionName;
            Precedence = opPrecedence;
            IsLeftAssociative = isLeftAssociative;
            IsRightAssociative = isRightAssociative;
            SymbolText = opSymbol;
            Position = opPosition;
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
            return SymbolText;
        }
    }
}
