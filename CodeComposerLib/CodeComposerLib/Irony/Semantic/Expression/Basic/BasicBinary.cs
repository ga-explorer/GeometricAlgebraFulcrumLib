using System;
using System.Collections.Generic;
using CodeComposerLib.Irony.Semantic.Operator;
using CodeComposerLib.Irony.Semantic.Type;

namespace CodeComposerLib.Irony.Semantic.Expression.Basic
{
    /// <summary>
    /// This class represents a basic binaty expression
    /// </summary>
    public sealed class BasicBinary : LanguageExpressionBasic
    {
        /// <summary>
        /// The first operand for the basic expression
        /// </summary>
        public ILanguageExpressionAtomic Operand1 { get; private set; }

        /// <summary>
        /// The second operand for the basic expression
        /// </summary>
        public ILanguageExpressionAtomic Operand2 { get; private set; }


        private BasicBinary(ILanguageType exprType, ILanguageOperator langOp, ILanguageExpressionAtomic operand1, ILanguageExpressionAtomic operand2)
            : base(exprType, langOp)
        {
            Operand1 = operand1;
            Operand2 = operand2;
        }


        public void ChangeOperand1(ILanguageExpressionAtomic operand)
        {
            if (operand.ExpressionType.IsSameType(Operand1.ExpressionType))
                Operand1 = operand;
            else
                throw new InvalidOperationException();
        }

        public void ChangeOperand2(ILanguageExpressionAtomic operand)
        {
            if (operand.ExpressionType.IsSameType(Operand2.ExpressionType))
                Operand2 = operand;
            else
                throw new InvalidOperationException();
        }

        public override IEnumerable<ILanguageExpressionAtomic> RhsOperands
        {
            get 
            {
                yield return Operand1;

                yield return Operand2;
            }
        }

        public override bool IsSimpleExpression => Operand1.IsSimpleExpression && Operand2.IsSimpleExpression;

        public override LanguageExpressionBasic Duplicate()
        {
            return new BasicBinary(ExpressionType, Operator, Operand1, Operand2);
        }


        public static BasicBinary Create(ILanguageType exprType, ILanguageOperator langOp, ILanguageExpressionAtomic operand1, ILanguageExpressionAtomic operand2)
        {
            return new BasicBinary(exprType, langOp, operand1, operand2);
        }

        public static BasicBinary CreatePrimitive(ILanguageType exprType, OperatorPrimitive langOp, ILanguageExpressionAtomic operand1, ILanguageExpressionAtomic operand2)
        {
            return new BasicBinary(exprType, langOp, operand1, operand2);
        }

        public static BasicBinary CreatePrimitive(ILanguageType exprType, string opName, ILanguageExpressionAtomic operand1, ILanguageExpressionAtomic operand2)
        {
            var langOp = exprType.RootAst.OperatorPrimitiveDictionary[opName];

            return new BasicBinary(exprType, langOp, operand1, operand2);
        }
    }
}
