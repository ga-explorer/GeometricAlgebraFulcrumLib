using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.Differential.Functions.Interpolators;
using GeometricAlgebraFulcrumLib.MathBase.Signals;

namespace GeometricAlgebraFulcrumLib.MathBase.Differential.Functions
{
    public static class MathDf
    {
        public static DfConstant Zero
            => DfConstant.Zero;

        public static DfConstant One
            => DfConstant.One;

        public static DfConstant Pi
            => DfConstant.Pi;

        public static DfConstant E
            => DfConstant.E;

        public static DfConstant Degree
            => DfConstant.Degree;

        public static DfVar X 
            => DfVar.DefaultFunction;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DifferentialBasicFunction Number(double value)
        {
            return DfConstant.Create(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DifferentialFunction XPow(double powerValue)
        {
            return powerValue switch
            {
                0d => One,
                1d => X,
                _ => DfPowerScalar.Create(X, powerValue, false)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DifferentialFunction XPow(int powerValue, double scalarValue)
        {
            return scalarValue * XPow(powerValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfExp Exp(DifferentialFunction f)
        {
            return DfExp.Create(f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfSin Sin(DifferentialFunction f)
        {
            return DfSin.Create(f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfCos Cos(DifferentialFunction f)
        {
            return DfCos.Create(f);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Tuple<double, DifferentialFunction>> SeparateConstants(this IEnumerable<DifferentialFunction> argList)
        {
            return argList.Select(arg => arg switch
            {
                DfConstant argConstant => 
                    new Tuple<double, DifferentialFunction>(argConstant.Value, DfConstant.One),

                DfTimes argTimes => 
                    argTimes.SeparateConstant(),

                _ => 
                    new Tuple<double, DifferentialFunction>(1d, arg)
            });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<double> GetValues(this IEnumerable<double> xValues, DifferentialFunction f)
        {
            return xValues.Select(f.GetValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<double> GetValues(this DifferentialFunction f, IEnumerable<double> xValues)
        {
            return xValues.Select(f.GetValue);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarSignalFloat64 SampleFunction(this ScalarSignalFloat64 tSignal, DifferentialFunction f)
        {
            return tSignal.MapSamples(f.GetValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<ScalarSignalFloat64> SampleDerivatives2(this ScalarSignalFloat64 tSignal, DifferentialFunction f)
        {
            var fDt1 = f.GetDerivative1();
            var fDt2 = fDt1.GetDerivative1();
            
            return new Pair<ScalarSignalFloat64>(
                tSignal.MapSamples(fDt1.GetValue), 
                tSignal.MapSamples(fDt2.GetValue)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Triplet<ScalarSignalFloat64> SampleDerivatives3(this ScalarSignalFloat64 tSignal, DifferentialFunction f)
        {
            var fDt1 = f.GetDerivative1();
            var fDt2 = fDt1.GetDerivative1();
            var fDt3 = fDt2.GetDerivative1();
            
            return new Triplet<ScalarSignalFloat64>(
                tSignal.MapSamples(fDt1.GetValue), 
                tSignal.MapSamples(fDt2.GetValue),
                tSignal.MapSamples(fDt3.GetValue)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quad<ScalarSignalFloat64> SampleDerivatives4(this ScalarSignalFloat64 tSignal, DifferentialFunction f)
        {
            var fDt1 = f.GetDerivative1();
            var fDt2 = fDt1.GetDerivative1();
            var fDt3 = fDt2.GetDerivative1();
            var fDt4 = fDt3.GetDerivative1();
            
            return new Quad<ScalarSignalFloat64>(
                tSignal.MapSamples(fDt1.GetValue), 
                tSignal.MapSamples(fDt2.GetValue),
                tSignal.MapSamples(fDt3.GetValue),
                tSignal.MapSamples(fDt4.GetValue)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<ScalarSignalFloat64> SampleFunctionDerivatives1(this ScalarSignalFloat64 tSignal, DifferentialFunction f)
        {
            var fDt1 = f.GetDerivative1();
            
            return new Pair<ScalarSignalFloat64>(
                tSignal.MapSamples(f.GetValue), 
                tSignal.MapSamples(fDt1.GetValue)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Triplet<ScalarSignalFloat64> SampleFunctionDerivatives2(this ScalarSignalFloat64 tSignal, DifferentialFunction f)
        {
            var fDt1 = f.GetDerivative1();
            var fDt2 = fDt1.GetDerivative1();
            
            return new Triplet<ScalarSignalFloat64>(
                tSignal.MapSamples(f.GetValue), 
                tSignal.MapSamples(fDt1.GetValue),
                tSignal.MapSamples(fDt2.GetValue)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quad<ScalarSignalFloat64> SampleFunctionDerivatives3(this ScalarSignalFloat64 tSignal, DifferentialFunction f)
        {
            var fDt1 = f.GetDerivative1();
            var fDt2 = fDt1.GetDerivative1();
            var fDt3 = fDt2.GetDerivative1();
            
            return new Quad<ScalarSignalFloat64>(
                tSignal.MapSamples(f.GetValue), 
                tSignal.MapSamples(fDt1.GetValue), 
                tSignal.MapSamples(fDt2.GetValue),
                tSignal.MapSamples(fDt3.GetValue)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quint<ScalarSignalFloat64> SampleFunctionDerivatives4(this ScalarSignalFloat64 tSignal, DifferentialFunction f)
        {
            var fDt1 = f.GetDerivative1();
            var fDt2 = fDt1.GetDerivative1();
            var fDt3 = fDt2.GetDerivative1();
            var fDt4 = fDt3.GetDerivative1();
            
            return new Quint<ScalarSignalFloat64>(
                tSignal.MapSamples(f.GetValue), 
                tSignal.MapSamples(fDt1.GetValue), 
                tSignal.MapSamples(fDt2.GetValue),
                tSignal.MapSamples(fDt3.GetValue),
                tSignal.MapSamples(fDt4.GetValue)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfLinearSplineSignalInterpolator CreateLinearSplineInterpolator(this ScalarSignalFloat64 signal, DfLinearSplineSignalInterpolatorOptions options)
        {
            return DfLinearSplineSignalInterpolator.Create(signal, options);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfCatmullRomSplineSignalInterpolator CreateCatmullRomSplineInterpolator(this ScalarSignalFloat64 signal, DfCatmullRomSplineSignalInterpolatorOptions options)
        {
            return DfCatmullRomSplineSignalInterpolator.Create(signal, options);
        }

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static SmoothedCatmullRomSplineD0Function CreateSmoothedCatmullRomSplineD0Function(this ScalarSignalFloat64 signal, int bezierDegree, IReadOnlyList<int> smoothingFactors, CatmullRomSplineType curveType = CatmullRomSplineType.Centripetal)
        //{
        //    return SmoothedCatmullRomSplineD0Function.CreateSmoothedCatmullRomSplineD0Function(
        //        signal,
        //        bezierDegree,
        //        smoothingFactors,
        //        curveType
        //    );
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static DifferentialFunction CreateSmoothedCatmullRomSplineD1Function(this ScalarSignalFloat64 signal, int bezierDegree, IReadOnlyList<int> smoothingFactors, CatmullRomSplineType curveType = CatmullRomSplineType.Centripetal)
        //{
        //    return SmoothedCatmullRomSplineD1Function.CreateSmoothedCatmullRomSplineD1Function(
        //        signal,
        //        bezierDegree,
        //        smoothingFactors,
        //        curveType
        //    );
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static DifferentialFunction CreateSmoothedCatmullRomSplineD2Function(this ScalarSignalFloat64 signal, int bezierDegree, IReadOnlyList<int> smoothingFactors, CatmullRomSplineType curveType = CatmullRomSplineType.Centripetal)
        //{
        //    return SmoothedCatmullRomSplineD2Function.CreateSmoothedCatmullRomSplineD2Function(
        //        signal,
        //        bezierDegree,
        //        smoothingFactors,
        //        curveType
        //    );
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static DifferentialFunction CreateSmoothedCatmullRomSplineD3Function(this ScalarSignalFloat64 signal, int bezierDegree, IReadOnlyList<int> smoothingFactors, CatmullRomSplineType curveType = CatmullRomSplineType.Centripetal)
        //{
        //    return SmoothedCatmullRomSplineD3Function.CreateSmoothedCatmullRomSplineD3Function(
        //        signal,
        //        bezierDegree,
        //        smoothingFactors,
        //        curveType
        //    );
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static DifferentialFunction CreateSmoothedCatmullRomSplineD4Function(this ScalarSignalFloat64 signal, int bezierDegree, IReadOnlyList<int> smoothingFactors, CatmullRomSplineType curveType = CatmullRomSplineType.Centripetal)
        //{
        //    return SmoothedCatmullRomSplineD4Function.CreateSmoothedCatmullRomSplineD4Function(
        //        signal,
        //        bezierDegree,
        //        smoothingFactors,
        //        curveType
        //    );
        //}
    }
}
