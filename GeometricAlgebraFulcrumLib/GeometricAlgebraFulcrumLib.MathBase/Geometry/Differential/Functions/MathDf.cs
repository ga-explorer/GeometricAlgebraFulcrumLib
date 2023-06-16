using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.SignalAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Differential.Functions
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
        public static Float64Signal SampleFunction(this Float64Signal tSignal, DifferentialFunction f)
        {
            return tSignal.MapSamples(f.GetValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<Float64Signal> SampleDerivatives2(this Float64Signal tSignal, DifferentialFunction f)
        {
            var fDt1 = f.GetDerivative1();
            var fDt2 = fDt1.GetDerivative1();
            
            return new Pair<Float64Signal>(
                tSignal.MapSamples(fDt1.GetValue), 
                tSignal.MapSamples(fDt2.GetValue)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Triplet<Float64Signal> SampleDerivatives3(this Float64Signal tSignal, DifferentialFunction f)
        {
            var fDt1 = f.GetDerivative1();
            var fDt2 = fDt1.GetDerivative1();
            var fDt3 = fDt2.GetDerivative1();
            
            return new Triplet<Float64Signal>(
                tSignal.MapSamples(fDt1.GetValue), 
                tSignal.MapSamples(fDt2.GetValue),
                tSignal.MapSamples(fDt3.GetValue)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quad<Float64Signal> SampleDerivatives4(this Float64Signal tSignal, DifferentialFunction f)
        {
            var fDt1 = f.GetDerivative1();
            var fDt2 = fDt1.GetDerivative1();
            var fDt3 = fDt2.GetDerivative1();
            var fDt4 = fDt3.GetDerivative1();
            
            return new Quad<Float64Signal>(
                tSignal.MapSamples(fDt1.GetValue), 
                tSignal.MapSamples(fDt2.GetValue),
                tSignal.MapSamples(fDt3.GetValue),
                tSignal.MapSamples(fDt4.GetValue)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<Float64Signal> SampleFunctionDerivatives1(this Float64Signal tSignal, DifferentialFunction f)
        {
            var fDt1 = f.GetDerivative1();
            
            return new Pair<Float64Signal>(
                tSignal.MapSamples(f.GetValue), 
                tSignal.MapSamples(fDt1.GetValue)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Triplet<Float64Signal> SampleFunctionDerivatives2(this Float64Signal tSignal, DifferentialFunction f)
        {
            var fDt1 = f.GetDerivative1();
            var fDt2 = fDt1.GetDerivative1();
            
            return new Triplet<Float64Signal>(
                tSignal.MapSamples(f.GetValue), 
                tSignal.MapSamples(fDt1.GetValue),
                tSignal.MapSamples(fDt2.GetValue)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quad<Float64Signal> SampleFunctionDerivatives3(this Float64Signal tSignal, DifferentialFunction f)
        {
            var fDt1 = f.GetDerivative1();
            var fDt2 = fDt1.GetDerivative1();
            var fDt3 = fDt2.GetDerivative1();
            
            return new Quad<Float64Signal>(
                tSignal.MapSamples(f.GetValue), 
                tSignal.MapSamples(fDt1.GetValue), 
                tSignal.MapSamples(fDt2.GetValue),
                tSignal.MapSamples(fDt3.GetValue)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quint<Float64Signal> SampleFunctionDerivatives4(this Float64Signal tSignal, DifferentialFunction f)
        {
            var fDt1 = f.GetDerivative1();
            var fDt2 = fDt1.GetDerivative1();
            var fDt3 = fDt2.GetDerivative1();
            var fDt4 = fDt3.GetDerivative1();
            
            return new Quint<Float64Signal>(
                tSignal.MapSamples(f.GetValue), 
                tSignal.MapSamples(fDt1.GetValue), 
                tSignal.MapSamples(fDt2.GetValue),
                tSignal.MapSamples(fDt3.GetValue),
                tSignal.MapSamples(fDt4.GetValue)
            );
        }

    }
}
