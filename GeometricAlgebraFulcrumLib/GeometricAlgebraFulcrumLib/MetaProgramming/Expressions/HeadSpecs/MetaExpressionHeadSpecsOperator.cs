using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Composite;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.HeadSpecs
{
    public sealed record MetaExpressionHeadSpecsOperator : 
        IMetaExpressionHeadSpecsOperator
    {
        public static MetaExpressionHeadSpecsOperator Create(MetaContext context, string functionName, string opSymbol, int opPrecedence, MetaExpressionOperatorPosition opPosition, bool isLeftAssociative, bool isRightAssociative)
        {
            return new MetaExpressionHeadSpecsOperator(
                context,
                functionName,
                opSymbol,
                opPrecedence,
                opPosition,
                isLeftAssociative,
                isRightAssociative
            );
        }

        public static MetaExpressionHeadSpecsOperator CreateLeftAssociative(MetaContext context, string functionName, string opSymbol, int opPrecedence, MetaExpressionOperatorPosition opPosition)
        {
            return new MetaExpressionHeadSpecsOperator(
                context,
                functionName,
                opSymbol,
                opPrecedence,
                opPosition,
                true,
                false
            );
        }
        
        public static MetaExpressionHeadSpecsOperator CreateRightAssociative(MetaContext context, string functionName, string opSymbol, int opPrecedence, MetaExpressionOperatorPosition opPosition)
        {
            return new MetaExpressionHeadSpecsOperator(
                context,
                functionName,
                opSymbol,
                opPrecedence,
                opPosition,
                false,
                true
            );
        }

        public static MetaExpressionHeadSpecsOperator CreateAssociative(MetaContext context, string functionName, string opSymbol, int opPrecedence, MetaExpressionOperatorPosition opPosition)
        {
            return new MetaExpressionHeadSpecsOperator(
                context,
                functionName,
                opSymbol,
                opPrecedence,
                opPosition,
                true,
                true
            );
        }

        public static MetaExpressionHeadSpecsOperator CreateNonAssociative(MetaContext context, string functionName, string opSymbol, int opPrecedence, MetaExpressionOperatorPosition opPosition)
        {
            return new MetaExpressionHeadSpecsOperator(
                context,
                functionName,
                opSymbol,
                opPrecedence,
                opPosition,
                false,
                false
            );
        }


        public MetaContext Context { get; }

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

        public MetaExpressionOperatorPosition Position { get; }
        
        public bool IsLeftAssociative { get; }

        public bool IsRightAssociative { get; }

        public bool IsAssociative 
            => IsLeftAssociative || IsRightAssociative;

        public bool IsNonAssociative 
            => !IsLeftAssociative && !IsRightAssociative;

        public MetaExpressionFunctionAssociationKind AssociationKind 
            => IsLeftAssociative
                ? (IsRightAssociative 
                    ? MetaExpressionFunctionAssociationKind.LeftRight 
                    : MetaExpressionFunctionAssociationKind.Left)
                : (IsRightAssociative 
                    ? MetaExpressionFunctionAssociationKind.Right 
                    : MetaExpressionFunctionAssociationKind.None);


        private MetaExpressionHeadSpecsOperator([NotNull] MetaContext context, [NotNull] string functionName, [NotNull] string opSymbol, int opPrecedence, MetaExpressionOperatorPosition opPosition, bool isLeftAssociative, bool isRightAssociative)
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

        
        public IMetaExpressionFunction CreateFunction()
        {
            return new MetaExpressionFunction(this);
        }

        public IMetaExpressionFunction CreateFunction(IMetaExpression argument1)
        {
            return new MetaExpressionFunction(
                this, 
                new []
                {
                    argument1
                } 
            );
        }

        public IMetaExpressionFunction CreateFunction(IMetaExpression argument1, IMetaExpression argument2)
        {
            return new MetaExpressionFunction(
                this, 
                new []
                {
                    argument1,
                    argument2
                } 
            );
        }

        public IMetaExpressionFunction CreateFunction(IMetaExpression argument1, IMetaExpression argument2, IMetaExpression argument3)
        {
            return new MetaExpressionFunction(
                this, 
                new []
                {
                    argument1,
                    argument2,
                    argument3
                } 
            );
        }

        public IMetaExpressionFunction CreateFunction(params IMetaExpression[] argumentsList)
        {
            return new MetaExpressionFunction(
                this, 
                argumentsList
            );
        }

        public IMetaExpressionFunction CreateFunction(IEnumerable<IMetaExpression> argumentsList)
        {
            return new MetaExpressionFunction(
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
