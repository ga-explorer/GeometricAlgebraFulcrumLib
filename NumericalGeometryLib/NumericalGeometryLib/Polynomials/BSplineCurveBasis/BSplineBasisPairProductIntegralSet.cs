using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.Polynomials.CurveBasis;

namespace NumericalGeometryLib.Polynomials.BSplineCurveBasis
{
    public sealed class BSplineBasisPairProductIntegralSet :
        IPolynomialPairProductIntegralSet
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BSplineBasisPairProductIntegralSet Create(BSplineBasisPairProductSet bernsteinBasisPairProductSet)
        {
            return new BSplineBasisPairProductIntegralSet(bernsteinBasisPairProductSet);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BSplineBasisPairProductIntegralSet Create(BSplineKnotVector knotVector, int degree)
        {
            var bernsteinBasisPairProductSet = BSplineBasisPairProductSet.Create(knotVector, degree);
            return new BSplineBasisPairProductIntegralSet(bernsteinBasisPairProductSet);
        }



        public BSplineBasisPairProductSet BasisPairProductSet { get; }

        public BSplineBasisSet BasisSet2 { get; }
        
        public int Degree 
            => 1 + BasisPairProductSet.Degree;
        
        public BSplineKnotVector KnotVector 
            => BasisSet2.KnotVector;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private BSplineBasisPairProductIntegralSet([NotNull] BSplineBasisPairProductSet basisPairProductSet)
        {
            var degree = basisPairProductSet.Degree / 2;

            var valueMultiplicityList =
                basisPairProductSet.KnotVector.GetKnotValueMultiplicityList().ToArray();

            var knotVector = BSplineKnotVector.Create();

            var valueMultiplicityListCount = valueMultiplicityList.Length;
            for (var i = 0; i < valueMultiplicityListCount; i++)
            {
                var (value, multiplicity) = valueMultiplicityList[i];

                if (i == 0 || i == valueMultiplicityListCount - 1)
                    knotVector.AppendKnot(value, multiplicity + 1);
                else
                    knotVector.AppendKnot(value, multiplicity);
            }
            
            BasisSet2 = knotVector.CreateBSplineBasisSet(2 * degree + 1);

            BasisPairProductSet = basisPairProductSet;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetValueAt1(int index1, int index2)
        {
            return 0d;
            //return BasisPairProductSet.GetBinomialConstant(index1, index2) / Degree;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetValueAt1(int index1, int index2, double termScalar)
        {
            return 0;
            //return termScalar * BasisPairProductSet.GetBinomialConstant(index1, index2) / Degree;
        }
        
        public double GetValueAt1(double[,] termScalarsList)
        {
            var m = (Degree - 1) / 2;
            var value = 0d;

            for (var i = 0; i <= m; i++)
            for (var j = 0; j <= m; j++)
            {
                value += GetValueAt1(i, j, termScalarsList[i, j]);
            }

            return value;
        }

        public double[,] GetValuesAt1()
        {
            var m = (Degree - 1) / 2;
            var valueArray = new double[m + 1, m + 1];

            for (var i = 0; i <= m; i++)
            {
                valueArray[i, i] = GetValueAt1(i, i);

                for (var j = i + 1; j <= m; j++)
                {
                    var value = GetValueAt1(i, j);

                    valueArray[i, j] = value;
                    valueArray[j, i] = value;
                }
            }

            return valueArray;
        }


        public double GetValue(int index1, int index2, double parameterValue)
        {
            return 0;
            //var m = Degree;
            //var cij = BasisPairProductSet.GetBinomialConstant(index1, index2) / Degree;

            //var value = 0d;

            //var k0 = index1 + index2 + 1;
            //for (var k = k0; k <= m; k++)
            //{
            //    value += BasisSet2.GetValue(k, parameterValue, cij);
            //}

            //return value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetValue(int index1, int index2, double parameterValue, double termScalar)
        {
            return termScalar * GetValue(index1, index2, parameterValue);
        }

        public double GetValue(double parameterValue, double[,] termScalarsList)
        {
            var m = (Degree - 1) / 2;
            var value = 0d;

            for (var i = 0; i <= m; i++)
            for (var j = 0; j <= m; j++)
            {
                value += GetValue(i, j, parameterValue, termScalarsList[i, j]);
            }

            return value;
        }

        public double[,] GetValues(double parameterValue)
        {
            var m = (Degree - 1) / 2;
            var valueArray = new double[m + 1, m + 1];

            for (var i = 0; i <= m; i++)
            for (var j = 0; j <= m; j++)
            {
                valueArray[i, j] = GetValue(i, j, parameterValue);
            }

            return valueArray;
        }
    }
}