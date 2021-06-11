using System;
using System.Collections.Generic;
using System.Text;

namespace CodeComposerLib.Irony.Semantic.Expression.Basic
{
    /// <summary>
    /// This class represents a linear list of operands
    /// </summary>
    public sealed class OperandsList : PolyadicOperands
    {
        /// <summary>
        /// The list of operands
        /// </summary>
        public List<ILanguageExpressionAtomic> Operands { get; } = new List<ILanguageExpressionAtomic>();


        private OperandsList()
        {
        }


        /// <summary>
        /// Add a new operand to the list
        /// </summary>
        /// <param name="opExpr"></param>
        public void AddOperand(ILanguageExpressionAtomic opExpr)
        {
            Operands.Add(opExpr);
        }


        public void ChangeOperand(int opIndex, ILanguageExpressionAtomic opExpr)
        {
            if (Operands[opIndex].ExpressionType.IsSameType(opExpr.ExpressionType))
                Operands[opIndex] = opExpr;
            else
                throw new InvalidOperationException();
        }

        public override IEnumerable<ILanguageExpressionAtomic> RhsOperands => Operands;

        public override PolyadicOperands Duplicate()
        {
            var operands = new OperandsList();

            foreach (var operand in Operands)
                operands.Operands.Add(operand);

            return operands;
        }


        public override string ToString()
        {
            var s = new StringBuilder();

            s.Append("(");

            foreach (var expr in Operands)
            {
                s.Append(expr);
                s.Append(", ");
            }

            s.Length = s.Length - 2;

            s.Append(")");

            return s.ToString();
        }


        public static OperandsList Create()
        {
            return new OperandsList();
        }
    }
}
