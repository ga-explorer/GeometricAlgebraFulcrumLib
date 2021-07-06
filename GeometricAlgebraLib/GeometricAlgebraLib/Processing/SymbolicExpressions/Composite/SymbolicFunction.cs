using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using CodeComposerLib.SyntaxTree.Expressions;
using GeometricAlgebraLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraLib.Processing.SymbolicExpressions.HeadSpecs;

namespace GeometricAlgebraLib.Processing.SymbolicExpressions.Composite
{
    public sealed class SymbolicFunction :
        SymbolicExpressionCompositeBase, ISymbolicFunction
    {
        public static SymbolicFunction Create(ISymbolicHeadSpecsFunction headSpecs)
        {
            return new SymbolicFunction(
                headSpecs
            );
        }
        
        public static SymbolicFunction Create(ISymbolicHeadSpecsFunction headSpecs, params ISymbolicExpression[] argumentsList)
        {
            return new SymbolicFunction(
                headSpecs,
                argumentsList
            );
        }
        
        public static SymbolicFunction Create(ISymbolicHeadSpecsFunction headSpecs, IEnumerable<ISymbolicExpression> argumentsList)
        {
            return new SymbolicFunction(
                headSpecs,
                argumentsList
            );
        }

        public static SymbolicFunction Create(SymbolicContext context, string functionName, bool isLeftAssociative, bool isRightAssociative)
        {
            return new SymbolicFunction(
                SymbolicHeadSpecsFunction.Create(context, functionName, isLeftAssociative, isRightAssociative)
            );
        }

        public static SymbolicFunction CreateLeftAssociative(SymbolicContext context, string functionName, bool isLeftAssociative, bool isRightAssociative, params ISymbolicExpression[] argumentsList)
        {
            return new SymbolicFunction(
                SymbolicHeadSpecsFunction.Create(context, functionName, isLeftAssociative, isRightAssociative),
                argumentsList
            );
        }

        public static SymbolicFunction CreateLeftAssociative(SymbolicContext context, string functionName, bool isLeftAssociative, bool isRightAssociative, IEnumerable<ISymbolicExpression> argumentsList)
        {
            return new SymbolicFunction(
                SymbolicHeadSpecsFunction.Create(context, functionName, isLeftAssociative, isRightAssociative),
                argumentsList
            );
        }

        public static SymbolicFunction CreateLeftAssociative(SymbolicContext context, string functionName)
        {
            return new SymbolicFunction(
                SymbolicHeadSpecsFunction.CreateLeftAssociative(context, functionName)
            );
        }

        public static SymbolicFunction CreateLeftAssociative(SymbolicContext context, string functionName, params ISymbolicExpression[] argumentsList)
        {
            return new SymbolicFunction(
                SymbolicHeadSpecsFunction.CreateLeftAssociative(context, functionName),
                argumentsList
            );
        }

        public static SymbolicFunction CreateLeftAssociative(SymbolicContext context, string functionName, IEnumerable<ISymbolicExpression> argumentsList)
        {
            return new SymbolicFunction(
                SymbolicHeadSpecsFunction.CreateLeftAssociative(context, functionName),
                argumentsList
            );
        }

        public static SymbolicFunction CreateRightAssociative(SymbolicContext context, string functionName)
        {
            return new SymbolicFunction(
                SymbolicHeadSpecsFunction.CreateRightAssociative(context, functionName)
            );
        }

        public static SymbolicFunction CreateRightAssociative(SymbolicContext context, string functionName, params ISymbolicExpression[] argumentsList)
        {
            return new SymbolicFunction(
                SymbolicHeadSpecsFunction.CreateRightAssociative(context, functionName),
                argumentsList
            );
        }

        public static SymbolicFunction CreateRightAssociative(SymbolicContext context, string functionName, IEnumerable<ISymbolicExpression> argumentsList)
        {
            return new SymbolicFunction(
                SymbolicHeadSpecsFunction.CreateRightAssociative(context, functionName),
                argumentsList
            );
        }

        public static SymbolicFunction CreateAssociative(SymbolicContext context, string functionName)
        {
            return new SymbolicFunction(
                SymbolicHeadSpecsFunction.CreateAssociative(context, functionName)
            );
        }

        public static SymbolicFunction CreateAssociative(SymbolicContext context, string functionName, params ISymbolicExpression[] argumentsList)
        {
            return new SymbolicFunction(
                SymbolicHeadSpecsFunction.CreateAssociative(context, functionName),
                argumentsList
            );
        }

        public static SymbolicFunction CreateAssociative(SymbolicContext context, string functionName, IEnumerable<ISymbolicExpression> argumentsList)
        {
            return new SymbolicFunction(
                SymbolicHeadSpecsFunction.CreateAssociative(context, functionName),
                argumentsList
            );
        }

        public static SymbolicFunction CreateNonAssociative(SymbolicContext context, string functionName)
        {
            return new SymbolicFunction(
                SymbolicHeadSpecsFunction.CreateNonAssociative(context, functionName)
            );
        }

        public static SymbolicFunction CreateNonAssociative(SymbolicContext context, string functionName, params ISymbolicExpression[] argumentsList)
        {
            return new SymbolicFunction(
                SymbolicHeadSpecsFunction.CreateNonAssociative(context, functionName),
                argumentsList
            );
        }

        public static SymbolicFunction CreateNonAssociative(SymbolicContext context, string functionName, IEnumerable<ISymbolicExpression> argumentsList)
        {
            return new SymbolicFunction(
                SymbolicHeadSpecsFunction.CreateNonAssociative(context, functionName),
                argumentsList
            );
        }


        public override ISymbolicHeadSpecs HeadSpecs 
            => FunctionHeadSpecs;

        public override ISymbolicHeadSpecsComposite CompositeHeadSpecs 
            => FunctionHeadSpecs;
        
        public ISymbolicHeadSpecsFunction FunctionHeadSpecs { get; }

        public ISymbolicHeadSpecsOperator OperatorHeadSpecs 
            => FunctionHeadSpecs as ISymbolicHeadSpecsOperator;

        public bool IsLeftAssociative 
            => FunctionHeadSpecs.IsLeftAssociative;

        public bool IsRightAssociative 
            => FunctionHeadSpecs.IsRightAssociative;

        public bool IsAssociative 
            => FunctionHeadSpecs.IsAssociative;

        public bool IsNonAssociative 
            => FunctionHeadSpecs.IsNonAssociative;

        public SymbolicFunctionAssociationKind AssociationKind
            => FunctionHeadSpecs.AssociationKind;

        public string FunctionName 
            => FunctionHeadSpecs.FunctionName;

        public override string HeadText 
            => FunctionHeadSpecs.FunctionName;

        public override bool IsFunction 
            => true;

        public override bool IsArrayAccess 
            => false;

        public override bool IsOperator 
            => FunctionHeadSpecs.IsOperator;


        internal SymbolicFunction([NotNull] ISymbolicHeadSpecsFunction headSpecs)
        {
            FunctionHeadSpecs = headSpecs;
        }

        internal SymbolicFunction([NotNull] ISymbolicHeadSpecsFunction headSpecs, IEnumerable<ISymbolicExpression> argumentsList)
            : base(argumentsList)
        {
            FunctionHeadSpecs = headSpecs;
        }


        public override SymbolicExpressionCompositeBase CreateCopy()
        {
            return ArgumentsList.Count == 0
                ? new SymbolicFunction(FunctionHeadSpecs)
                : new SymbolicFunction(FunctionHeadSpecs, ArgumentsList);
        }

        public override SymbolicExpressionCompositeBase GetEmptyExpressionCopy()
        {
            return new SymbolicFunction(FunctionHeadSpecs);
        }

        public override SteExpression ToSimpleTextExpression()
        {
            if (ArgumentsList.Count == 0)
                return SteExpression.CreateFunction(FunctionHeadSpecs.FunctionName);

            var argumentsList = 
                ArgumentsList.Select(expr => expr.ToSimpleTextExpression());

            if (!IsOperator)
                return SteExpression.CreateFunction(
                    FunctionHeadSpecs.FunctionName,
                    argumentsList
                );

            var headSpecs = OperatorHeadSpecs;
                
            var opAssociation = headSpecs.AssociationKind switch
            {
                SymbolicFunctionAssociationKind.Left => SteOperatorAssociation.Left,
                SymbolicFunctionAssociationKind.Right => SteOperatorAssociation.Right,
                SymbolicFunctionAssociationKind.LeftRight => SteOperatorAssociation.LeftRight,
                _ => SteOperatorAssociation.None
            };
                
            var opPosition = headSpecs.Position switch
            {
                SymbolicOperatorPosition.Prefix => SteOperatorPosition.Prefix,
                SymbolicOperatorPosition.Infix => SteOperatorPosition.Infix,
                _ => SteOperatorPosition.Suffix
            };

            return SteExpression.CreateOperator(
                new SteOperatorSpecs(headSpecs.SymbolText, headSpecs.Precedence, opPosition, opAssociation),
                argumentsList
            );
        }
        

        public override string ToString()
        {
            var composer = new StringBuilder();

            composer.Append(FunctionHeadSpecs.FunctionName)
                .Append("[");

            if (ArgumentsList.Count > 0)
            {
                composer.Append(ArgumentsList[0]);

                for (var i = 1; i < ArgumentsList.Count; i++)
                    composer
                        .Append(", ")
                        .Append(ArgumentsList[i]);
            }

            composer.Append("]");

            return composer.ToString();
        }
    }
}