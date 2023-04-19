﻿//using System;
//using NumericalGeometryLib.Calculus.AutoDiff;
//using NUnit.Framework;
//using Xunit;
//using static GeometricAlgebraFulcrumLib.UnitTests.AutoDiff.Utils;
//using static NumericalGeometryLib.Calculus.AutoDiff.TermBuilder;

//namespace GeometricAlgebraFulcrumLib.UnitTests.AutoDiff
//{
//    public class BasicEvaluationTests
//    {
//        private static readonly Variable[] NoVars = new Variable[0];
//        private static readonly double[] NoVals = new double[0];

//        [Fact]
//        public void TestZero()
//        {
//            var zero = Constant(0);
//            var value = zero.Evaluate(NoVars, NoVals);
//            Assert.AreEqual(0, value);
//        }

//        [Fact]
//        public void TestConstant()
//        {
//            var constant = Constant(5);
//            var value = constant.Evaluate(NoVars, NoVals);
//            Assert.AreEqual(5, value);
//        }

//        [Fact]
//        public void TestSumTwoConsts()
//        {
//            var c1 = Constant(5);
//            var c2 = Constant(7);
//            var sum = c1 + c2;
//            var value = sum.Evaluate(NoVars, NoVals);
//            Assert.AreEqual(12, value);
//        }

//        [Fact]
//        public void TestSumConstVar()
//        {
//            var c = Constant(5);
//            var v = new Variable();
//            var sum = c + v;
//            var value = sum.Evaluate(Vec(v), NumVec(7));
//            Assert.AreEqual(12, value);
//        }

//        [Fact]
//        public void TestDiffConst()
//        {
//            var c1 = Constant(12);
//            var c2 = Constant(5);
//            var diff = c1 - c2;
//            var value = diff.Evaluate(NoVars, NoVals);
//            Assert.AreEqual(7, value);
//        }

//        [Fact]
//        public void TestDiffVar()
//        {
//            var c = Constant(12);
//            var v = new Variable();
//            var diff = c - v;
//            var value = diff.Evaluate(Vec(v), NumVec(5));
//            Assert.AreEqual(7, value);
//        }

//        [Fact]
//        public void TestProdVar()
//        {
//            var v1 = new Variable();
//            var v2 = new Variable();
//            var prod = v1 * v2;
//            var value = prod.Evaluate(Vec(v1, v2), NumVec(3, -5));
//            Assert.AreEqual(-15, value);
//        }

//        [Fact]
//        public void TestConstPower()
//        {
//            var c = Constant(3);
//            var pow = Power(c, 3);
//            var value = pow.Evaluate(NoVars, NoVals);
//            Assert.AreEqual(27, value);
//        }

//        [Fact]
//        public void TestTermPower()
//        {
//            var baseTerm = Constant(3);
//            var expTerm = Constant(4);
//            var pow = Power(baseTerm, expTerm);
//            var value = pow.Evaluate(NoVars, NoVals);
//            Assert.AreEqual(Math.Pow(3, 4), value);
//        }

//        [Fact]
//        public void TestSquareDiff()
//        {
//            var v = new Variable();
//            var sqDiff = Power(v - 5, 2);
//            var r1 = sqDiff.Evaluate(Vec(v), NumVec(3));
//            var r2 = sqDiff.Evaluate(Vec(v), NumVec(5));

//            Assert.AreEqual(4, r1);
//            Assert.AreEqual(0, r2);
//        }

//        [Fact]
//        public void WeighedSquareDiff()
//        {
//            var v = Vec(new Variable(), new Variable(), new Variable());
//            var sqDiff = Sum(
//                12 * Power(v[0] - 5, 2),
//                3 * Power(v[1] - 4, 2),
//                2 * Power(v[2] + 3, 2));

//            var r1 = sqDiff.Evaluate(v, NumVec(5, 4, -3));
//            var r2 = sqDiff.Evaluate(v, NumVec(3, 4, -3));
//            var r3 = sqDiff.Evaluate(v, NumVec(4, 4, 0));

//            Assert.AreEqual(0, r1);
//            Assert.AreEqual(48, r2);
//            Assert.AreEqual(30, r3);
//        }

//        [Fact]
//        public void TestUnaryFuncSimple()
//        {
//            var v = new Variable();

//            Func<double, double> eval = x => x * x;
//            Func<double, double> diff = x => 2 * x;

//            var term = new UnaryFunc(eval, diff, v);

//            var y1 = term.Evaluate(Vec(v), NumVec(1));
//            var y2 = term.Evaluate(Vec(v), NumVec(2));
//            var y3 = term.Evaluate(Vec(v), NumVec(3));

//            Assert.AreEqual(1.0, y1);
//            Assert.AreEqual(4.0, y2);
//            Assert.AreEqual(9.0, y3);
//        }

//        [Fact]
//        public void TestUnaryFuncComplex()
//        {
//            var v = Vec(new Variable(), new Variable());

//            var square = UnaryFunc.Factory(x => x * x, x => 2 * x);

//            // f(x, y) = x^2 + 2 * y^2
//            var term = square(v[0]) +  2 * square(v[1]);

//            var y1 = term.Evaluate(v, NumVec(1, 0));  // 1 + 0 = 1
//            var y2 = term.Evaluate(v, NumVec(0, 1));  // 0 + 2 = 2
//            var y3 = term.Evaluate(v, NumVec(2, 1));  // 4 + 2 = 6

//            Assert.AreEqual(1, y1);
//            Assert.AreEqual(2, y2);
//            Assert.AreEqual(6, y3);
//        }

//        [Fact]
//        public void TestBinaryFuncSimple()
//        {
//            var v = Vec(new Variable(), new Variable());
//            var func = BinaryFunc.Factory(
//                (x, y) => x * x - x * y, 
//                (x, y) => Tuple.Create(2 * x - y, -x));

//            var term = func(v[0], v[1]);

//            var y1 = term.Evaluate(v, NumVec(1, 0)); // 1 - 0 = 1
//            var y2 = term.Evaluate(v, NumVec(0, 1)); // 0 - 0 = 0
//            var y3 = term.Evaluate(v, NumVec(1, 2)); // 1 - 2 = -1

//            Assert.AreEqual(1.0, y1);
//            Assert.AreEqual(0.0, y2);
//            Assert.AreEqual(-1.0, y3);
//        }

//        [Fact]
//        public void TestBinaryFuncComplex()
//        {
//            var v = Vec(new Variable(), new Variable());
//            var func = BinaryFunc.Factory(
//                (x, y) => x * x - x * y,
//                (x, y) => Tuple.Create(2 * x - y, -x));

//            // f(x, y) = x² - xy - y² + xy = x² - y²
//            var term = func(v[0], v[1]) - func(v[1], v[0]);

//            var y1 = term.Evaluate(v, NumVec(1, 0)); // 1 - 0 = 1
//            var y2 = term.Evaluate(v, NumVec(0, 1)); // 0 - 1 = -1
//            var y3 = term.Evaluate(v, NumVec(2, 1)); // 4 - 1 = 3

//            Assert.AreEqual(1.0, y1);
//            Assert.AreEqual(-1.0, y2);
//            Assert.AreEqual(3.0, y3);
//        }
//    }
//}
