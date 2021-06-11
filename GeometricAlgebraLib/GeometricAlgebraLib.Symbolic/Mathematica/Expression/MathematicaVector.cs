using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraLib.Symbolic.Mathematica.ExprFactory;
using Wolfram.NETLink;

namespace GeometricAlgebraLib.Symbolic.Mathematica.Expression
{
    public sealed class MathematicaVector : MathematicaExpression, ISymbolicVector
    {
        public static MathematicaVector CreateZero(MathematicaInterface parentCas, int size)
        {
            var mathExpr = parentCas[Mfs.ConstantArray[0.ToExpr(), size.ToExpr()]];

            return new MathematicaVector(parentCas, mathExpr);
        }

        public static MathematicaVector CreateFullVector(MathematicaInterface parentCas, params MathematicaScalar[] scalarsList)
        {
            var mathExpr = parentCas[Mfs.List[scalarsList.Select(scalar => scalar.Expression as object).ToArray()]];

            return new MathematicaVector(parentCas, mathExpr);
        }

        public static MathematicaVector CreateFullVector(MathematicaInterface parentCas, IEnumerable<MathematicaScalar> scalarsList)
        {
            var mathExpr = parentCas[Mfs.List[scalarsList.Select(scalar => scalar.Expression as object).ToArray()]];

            return new MathematicaVector(parentCas, mathExpr);
        }

        public static MathematicaVector Create(MathematicaScalar scalar, int size)
        {
            var mathExpr = scalar.CasInterface[Mfs.ConstantArray[scalar.Expression, size.ToExpr()]];

            return new MathematicaVector(scalar.CasInterface, mathExpr);
        }

        public new static MathematicaVector Create(MathematicaInterface parentCas, Expr mathExpr)
        {
            return new MathematicaVector(parentCas, mathExpr);
        }

        public new static MathematicaVector Create(MathematicaInterface parentCas, string mathExprText)
        {
            var mathExpr = parentCas.Connection.EvaluateToExpr(mathExprText);

            return new MathematicaVector(parentCas, mathExpr);
        }


        public static MathematicaVector operator -(MathematicaVector vector1)
        {
            var e = vector1.CasInterface[Mfs.Minus[vector1.Expression]];

            return new MathematicaVector(vector1.CasInterface, e);
        }

        public static MathematicaVector operator +(MathematicaVector vector1, MathematicaVector vector2)
        {
            var e = vector1.CasInterface[Mfs.Plus[vector1.Expression, vector2.Expression]];

            return new MathematicaVector(vector1.CasInterface, e);
        }

        public static MathematicaVector operator -(MathematicaVector vector1, MathematicaVector vector2)
        {
            var e = vector1.CasInterface[Mfs.Subtract[vector1.Expression, vector2.Expression]];

            return new MathematicaVector(vector1.CasInterface, e);
        }

        public static MathematicaScalar operator *(MathematicaVector vector1, MathematicaVector vector2)
        {
            var e = vector1.CasInterface[Mfs.Dot[vector1.Expression, vector2.Expression]];

            return MathematicaScalar.Create(vector1.CasInterface, e);
        }

        public static MathematicaVector operator *(MathematicaVector vector1, MathematicaScalar scalar2)
        {
            var e = vector1.CasInterface[Mfs.Times[vector1.Expression, scalar2.Expression]];

            return new MathematicaVector(vector1.CasInterface, e);
        }

        public static MathematicaVector operator *(MathematicaScalar scalar1, MathematicaVector vector2)
        {
            var e = vector2.CasInterface[Mfs.Times[scalar1.Expression, vector2.Expression]];

            return new MathematicaVector(vector2.CasInterface, e);
        }

        public static MathematicaVector operator /(MathematicaVector vector1, MathematicaScalar scalar2)
        {
            var e = vector1.CasInterface[Mfs.Divide[vector1.Expression, scalar2.Expression]];

            return new MathematicaVector(vector1.CasInterface, e);
        }


        private MathematicaVector(MathematicaInterface parentCas, Expr mathExpr)
            : base(parentCas, mathExpr)
        {
        }


        public int Size
        {
            get
            {
                if (IsFullVector())
                    return Expression.Args.Length;

                var dimensions = CasConnection.EvaluateToExpr(Mfs.Dimensions[Expression]);

                return Int32.Parse(dimensions.Args[0].ToString());
            }
        }

        public MathematicaScalar this[int index] => MathematicaScalar.Create(
            CasInterface, 
            IsFullVector() 
                ? Expression.Args[index] 
                : CasInterface[Mfs.Part[Expression, (index + 1).ToExpr()]]
            );

        public bool IsFullVector()
        {
            return Expression.ListQ();

            //return
            //    this.MathExpr.Head.ToString() == FunctionNames.List &&
            //    this.Evaluator.EvaluateFunctionIsTrue(FunctionNames.VectorQ, this.MathExpr);
        }

        public bool IsSparseVector()
        {
            return Expression.Head.ToString() == Mfs.SparseArray.ToString();

            //return
            //    this.MathExpr.Head.ToString() == FunctionNames.SparseArray &&
            //    this.Evaluator.EvaluateFunctionIsTrue(FunctionNames.VectorQ, this.MathExpr);
        }


        public ISymbolicVector Times(ISymbolicMatrix m)
        {
            var e = CasInterface[Mfs.Dot[Expression, m.ToMathematicaMatrix().Expression]];

            return Create(CasInterface, e);
        }

        /// <summary>
        /// Euclidean norm of a vector
        /// </summary>
        /// <returns></returns>
        public MathematicaScalar Norm()
        {
            var e = CasInterface[Mfs.Norm[Expression]];

            return MathematicaScalar.Create(CasInterface, e);
        }

        /// <summary>
        /// Euclidean norm squared of a vector
        /// </summary>
        /// <returns></returns>
        public MathematicaScalar Norm2()
        {
            var e = CasInterface[Mfs.Dot[Expression, Expression]];

            return MathematicaScalar.Create(CasInterface, e);
        }


        public MathematicaVector ToMathematicaVector()
        {
            return this;
        }

        public MathematicaVector ToMathematicaFullVector()
        {
            if (IsFullVector())
                return this;

            var e = CasInterface[Mfs.Normal[Expression]];

            return new MathematicaVector(CasInterface, e);
        }

        public MathematicaVector ToMathematicaSparseVector()
        {
            if (IsSparseVector())
                return this;

            var e = CasInterface[Mfs.SparseArray[Expression]];

            return new MathematicaVector(CasInterface, e);
        }

        public IEnumerator<MathematicaScalar> GetEnumerator()
        {
            for (var i = 0; i < Size; i++)
                yield return this[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (var i = 0; i < Size; i++)
                yield return this[i];
        }
    }
}
