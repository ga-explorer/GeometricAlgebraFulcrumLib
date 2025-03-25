using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Modeling.Signals.Composers;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Angles;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Basic;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Normalized;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64
{
    public abstract class Float64ScalarSignal(Float64ScalarRange timeRange, bool isPeriodic) :
        Float64Trajectory<double>(timeRange, isPeriodic)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteZero()
        {
            return Float64ScalarConstantZeroSignal.FiniteInstance;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteZero(Float64ScalarRange timeRange)
        {
            return Float64ScalarConstantZeroSignal.FiniteInstance.MapTimeRangeTo(timeRange);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteZero(double timeMin, double timeMax)
        {
            return Float64ScalarConstantZeroSignal.FiniteInstance.MapTimeRangeTo(timeMin, timeMax);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteOne()
        {
            return Float64ScalarConstantOneSignal.FiniteInstance;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteOne(Float64ScalarRange timeRange)
        {
            return Float64ScalarConstantZeroSignal.FiniteInstance.MapTimeRangeTo(timeRange);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteOne(double timeMin, double timeMax)
        {
            return Float64ScalarConstantOneSignal.FiniteInstance.MapTimeRangeTo(timeMin, timeMax);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteConstant(double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.FiniteInstance;

            if (value.IsOne()) return 
                Float64ScalarConstantOneSignal.FiniteInstance;

            return Float64ScalarConstantOneSignal.FiniteInstance.ScaleValueBy(value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteConstant(Float64ScalarRange timeRange, double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.FiniteInstance.MapTimeRangeTo(timeRange);

            if (value.IsOne()) return 
                Float64ScalarConstantOneSignal.FiniteInstance.MapTimeRangeTo(timeRange);

            return Float64ScalarConstantOneSignal.FiniteInstance.ScaleValueBy(value).MapTimeRangeTo(timeRange);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteConstant(double timeMin, double timeMax, double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.FiniteInstance.MapTimeRangeTo(timeMin, timeMax);

            if (value.IsOne()) return 
                Float64ScalarConstantOneSignal.FiniteInstance.MapTimeRangeTo(timeMin, timeMax);

            return Float64ScalarConstantOneSignal.FiniteInstance.ScaleValueBy(value).MapTimeRangeTo(timeMin, timeMax);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteSharpStep()
        {
            return Float64ScalarSharpStepSignal.FiniteInstance;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteSharpStep(double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.FiniteInstance;

            if (value.IsOne())
                return Float64ScalarSharpStepSignal.FiniteInstance;

            return Float64ScalarSharpStepSignal.FiniteInstance.MapValueRangeTo(-value, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteSharpStep(double value1, double value2)
        {
            if (value1.IsZero() && value2.IsZero()) 
                return Float64ScalarConstantZeroSignal.FiniteInstance;

            if (value1.IsMinusOne() && value2.IsOne())
                return Float64ScalarSharpStepSignal.FiniteInstance;

            return Float64ScalarSharpStepSignal.FiniteInstance.MapValueRangeTo(value1, value2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteSharpStep(Float64ScalarRange timeRange, double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.FiniteInstance.MapTimeRangeTo(timeRange);

            if (value.IsOne()) return 
                Float64ScalarSharpStepSignal.FiniteInstance.MapTimeRangeTo(timeRange);

            return Float64ScalarSharpStepSignal.FiniteInstance.ScaleValueBy(value).MapTimeRangeTo(timeRange);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteSharpStep(Float64ScalarRange timeRange, double value1, double value2)
        {
            if (value1.IsZero() && value2.IsZero()) 
                return Float64ScalarConstantZeroSignal.FiniteInstance.MapTimeRangeTo(timeRange);

            if (value1.IsMinusOne() && value2.IsOne()) return 
                Float64ScalarSharpStepSignal.FiniteInstance.MapTimeRangeTo(timeRange);

            return Float64ScalarSharpStepSignal.FiniteInstance.MapValueRangeTo(value1, value2).MapTimeRangeTo(timeRange);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteSharpStep(double timeMin, double timeMax, double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.FiniteInstance.MapTimeRangeTo(timeMin, timeMax);

            if (value.IsOne()) return 
                Float64ScalarSharpStepSignal.FiniteInstance.MapTimeRangeTo(timeMin, timeMax);

            return Float64ScalarSharpStepSignal.FiniteInstance.ScaleValueBy(value).MapTimeRangeTo(timeMin, timeMax);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteSharpStep(double timeMin, double timeMax, double value1, double value2)
        {
            if (value1.IsZero() && value2.IsZero()) 
                return Float64ScalarConstantZeroSignal.FiniteInstance.MapTimeRangeTo(timeMin, timeMax);

            if (value1.IsMinusOne() && value2.IsOne()) return 
                Float64ScalarSharpStepSignal.FiniteInstance.MapTimeRangeTo(timeMin, timeMax);

            return Float64ScalarSharpStepSignal.FiniteInstance.MapValueRangeTo(value1, value2).MapTimeRangeTo(timeMin, timeMax);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteHalfSinStep()
        {
            return Float64ScalarHalfSinStepSignal.FiniteInstance;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteHalfSinStep(double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.FiniteInstance;

            if (value.IsOne())
                return Float64ScalarHalfSinStepSignal.FiniteInstance;

            return Float64ScalarHalfSinStepSignal.FiniteInstance.MapValueRangeTo(-value, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteHalfSinStep(double value1, double value2)
        {
            if (value1.IsZero() && value2.IsZero()) 
                return Float64ScalarConstantZeroSignal.FiniteInstance;

            if (value1.IsMinusOne() && value2.IsOne())
                return Float64ScalarHalfSinStepSignal.FiniteInstance;

            return Float64ScalarHalfSinStepSignal.FiniteInstance.MapValueRangeTo(value1, value2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteHalfSinStep(Float64ScalarRange timeRange, double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.FiniteInstance.MapTimeRangeTo(timeRange);

            if (value.IsOne()) return 
                Float64ScalarHalfSinStepSignal.FiniteInstance.MapTimeRangeTo(timeRange);

            return Float64ScalarHalfSinStepSignal.FiniteInstance.ScaleValueBy(value).MapTimeRangeTo(timeRange);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteHalfSinStep(Float64ScalarRange timeRange, double value1, double value2)
        {
            if (value1.IsZero() && value2.IsZero()) 
                return Float64ScalarConstantZeroSignal.FiniteInstance.MapTimeRangeTo(timeRange);

            if (value1.IsMinusOne() && value2.IsOne()) return 
                Float64ScalarHalfSinStepSignal.FiniteInstance.MapTimeRangeTo(timeRange);

            return Float64ScalarHalfSinStepSignal.FiniteInstance.MapValueRangeTo(value1, value2).MapTimeRangeTo(timeRange);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteHalfSinStep(double timeMin, double timeMax, double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.FiniteInstance.MapTimeRangeTo(timeMin, timeMax);

            if (value.IsOne()) return 
                Float64ScalarHalfSinStepSignal.FiniteInstance.MapTimeRangeTo(timeMin, timeMax);

            return Float64ScalarHalfSinStepSignal.FiniteInstance.ScaleValueBy(value).MapTimeRangeTo(timeMin, timeMax);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteHalfSinStep(double timeMin, double timeMax, double value1, double value2)
        {
            if (value1.IsZero() && value2.IsZero()) 
                return Float64ScalarConstantZeroSignal.FiniteInstance.MapTimeRangeTo(timeMin, timeMax);

            if (value1.IsMinusOne() && value2.IsOne()) return 
                Float64ScalarHalfSinStepSignal.FiniteInstance.MapTimeRangeTo(timeMin, timeMax);

            return Float64ScalarHalfSinStepSignal.FiniteInstance.MapValueRangeTo(value1, value2).MapTimeRangeTo(timeMin, timeMax);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteSmoothStep()
        {
            return Float64ScalarSmoothStepSignal.FiniteInstance;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteSmoothStep(double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.FiniteInstance;

            if (value.IsOne())
                return Float64ScalarSmoothStepSignal.FiniteInstance;

            return Float64ScalarSmoothStepSignal.FiniteInstance.MapValueRangeTo(-value, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteSmoothStep(double value1, double value2)
        {
            if (value1.IsZero() && value2.IsZero()) 
                return Float64ScalarConstantZeroSignal.FiniteInstance;

            if (value1.IsMinusOne() && value2.IsOne())
                return Float64ScalarSmoothStepSignal.FiniteInstance;

            return Float64ScalarSmoothStepSignal.FiniteInstance.MapValueRangeTo(value1, value2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteSmoothStep(Float64ScalarRange timeRange, double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.FiniteInstance.MapTimeRangeTo(timeRange);

            if (value.IsOne()) return 
                Float64ScalarSmoothStepSignal.FiniteInstance.MapTimeRangeTo(timeRange);

            return Float64ScalarSmoothStepSignal.FiniteInstance.ScaleValueBy(value).MapTimeRangeTo(timeRange);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteSmoothStep(Float64ScalarRange timeRange, double value1, double value2)
        {
            if (value1.IsZero() && value2.IsZero()) 
                return Float64ScalarConstantZeroSignal.FiniteInstance.MapTimeRangeTo(timeRange);

            if (value1.IsMinusOne() && value2.IsOne()) return 
                Float64ScalarSmoothStepSignal.FiniteInstance.MapTimeRangeTo(timeRange);

            return Float64ScalarSmoothStepSignal.FiniteInstance.MapValueRangeTo(value1, value2).MapTimeRangeTo(timeRange);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteSmoothStep(double timeMin, double timeMax, double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.FiniteInstance.MapTimeRangeTo(timeMin, timeMax);

            if (value.IsOne()) return 
                Float64ScalarSmoothStepSignal.FiniteInstance.MapTimeRangeTo(timeMin, timeMax);

            return Float64ScalarSmoothStepSignal.FiniteInstance.ScaleValueBy(value).MapTimeRangeTo(timeMin, timeMax);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteSmoothStep(double timeMin, double timeMax, double value1, double value2)
        {
            if (value1.IsZero() && value2.IsZero()) 
                return Float64ScalarConstantZeroSignal.FiniteInstance.MapTimeRangeTo(timeMin, timeMax);

            if (value1.IsMinusOne() && value2.IsOne()) return 
                Float64ScalarSmoothStepSignal.FiniteInstance.MapTimeRangeTo(timeMin, timeMax);

            return Float64ScalarSmoothStepSignal.FiniteInstance.MapValueRangeTo(value1, value2).MapTimeRangeTo(timeMin, timeMax);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteRamp()
        {
            return Float64ScalarRampSignal.FiniteInstance;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteRamp(double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.FiniteInstance;

            if (value.IsOne())
                return Float64ScalarRampSignal.FiniteInstance;

            return Float64ScalarRampSignal.FiniteInstance.MapValueRangeTo(-value, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteRamp(double value1, double value2)
        {
            if (value1.IsZero() && value2.IsZero()) 
                return Float64ScalarConstantZeroSignal.FiniteInstance;

            if (value1.IsMinusOne() && value2.IsOne())
                return Float64ScalarRampSignal.FiniteInstance;

            return Float64ScalarRampSignal.FiniteInstance.MapValueRangeTo(value1, value2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteRamp(Float64ScalarRange timeRange, double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.FiniteInstance.MapTimeRangeTo(timeRange);

            if (value.IsOne()) return 
                Float64ScalarRampSignal.FiniteInstance.MapTimeRangeTo(timeRange);

            return Float64ScalarRampSignal.FiniteInstance.ScaleValueBy(value).MapTimeRangeTo(timeRange);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteRamp(Float64ScalarRange timeRange, double value1, double value2)
        {
            if (value1.IsZero() && value2.IsZero()) 
                return Float64ScalarConstantZeroSignal.FiniteInstance.MapTimeRangeTo(timeRange);

            if (value1.IsMinusOne() && value2.IsOne()) return 
                Float64ScalarRampSignal.FiniteInstance.MapTimeRangeTo(timeRange);

            return Float64ScalarRampSignal.FiniteInstance.MapValueRangeTo(value1, value2).MapTimeRangeTo(timeRange);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteRamp(double timeMin, double timeMax, double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.FiniteInstance.MapTimeRangeTo(timeMin, timeMax);

            if (value.IsOne()) return 
                Float64ScalarRampSignal.FiniteInstance.MapTimeRangeTo(timeMin, timeMax);

            return Float64ScalarRampSignal.FiniteInstance.ScaleValueBy(value).MapTimeRangeTo(timeMin, timeMax);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteRamp(double timeMin, double timeMax, double value1, double value2)
        {
            if (value1.IsZero() && value2.IsZero()) 
                return Float64ScalarConstantZeroSignal.FiniteInstance.MapTimeRangeTo(timeMin, timeMax);

            if (value1.IsMinusOne() && value2.IsOne()) return 
                Float64ScalarRampSignal.FiniteInstance.MapTimeRangeTo(timeMin, timeMax);

            return Float64ScalarRampSignal.FiniteInstance.MapValueRangeTo(value1, value2).MapTimeRangeTo(timeMin, timeMax);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteSin()
        {
            return Float64ScalarSinSignal.FiniteInstance;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteSin(double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.FiniteInstance;

            if (value.IsOne())
                return Float64ScalarSinSignal.FiniteInstance;

            return Float64ScalarSinSignal.FiniteInstance.MapValueRangeTo(-value, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteSin(double value1, double value2)
        {
            if (value1.IsZero() && value2.IsZero()) 
                return Float64ScalarConstantZeroSignal.FiniteInstance;

            if (value1.IsMinusOne() && value2.IsOne())
                return Float64ScalarSinSignal.FiniteInstance;

            return Float64ScalarSinSignal.FiniteInstance.MapValueRangeTo(value1, value2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteSin(Float64ScalarRange timeRange, double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.FiniteInstance.MapTimeRangeTo(timeRange);

            if (value.IsOne()) return 
                Float64ScalarSinSignal.FiniteInstance.MapTimeRangeTo(timeRange);

            return Float64ScalarSinSignal.FiniteInstance.ScaleValueBy(value).MapTimeRangeTo(timeRange);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteSin(Float64ScalarRange timeRange, double value1, double value2)
        {
            if (value1.IsZero() && value2.IsZero()) 
                return Float64ScalarConstantZeroSignal.FiniteInstance.MapTimeRangeTo(timeRange);

            if (value1.IsMinusOne() && value2.IsOne()) return 
                Float64ScalarSinSignal.FiniteInstance.MapTimeRangeTo(timeRange);

            return Float64ScalarSinSignal.FiniteInstance.MapValueRangeTo(value1, value2).MapTimeRangeTo(timeRange);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteSin(double timeMin, double timeMax, double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.FiniteInstance.MapTimeRangeTo(timeMin, timeMax);

            if (value.IsOne()) return 
                Float64ScalarSinSignal.FiniteInstance.MapTimeRangeTo(timeMin, timeMax);

            return Float64ScalarSinSignal.FiniteInstance.ScaleValueBy(value).MapTimeRangeTo(timeMin, timeMax);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteSin(double timeMin, double timeMax, double value1, double value2)
        {
            if (value1.IsZero() && value2.IsZero()) 
                return Float64ScalarConstantZeroSignal.FiniteInstance.MapTimeRangeTo(timeMin, timeMax);

            if (value1.IsMinusOne() && value2.IsOne()) return 
                Float64ScalarSinSignal.FiniteInstance.MapTimeRangeTo(timeMin, timeMax);

            return Float64ScalarSinSignal.FiniteInstance.MapValueRangeTo(value1, value2).MapTimeRangeTo(timeMin, timeMax);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteCos()
        {
            return Float64ScalarCosSignal.FiniteInstance;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteCos(double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.FiniteInstance;

            if (value.IsOne())
                return Float64ScalarCosSignal.FiniteInstance;

            return Float64ScalarCosSignal.FiniteInstance.MapValueRangeTo(-value, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteCos(double value1, double value2)
        {
            if (value1.IsZero() && value2.IsZero()) 
                return Float64ScalarConstantZeroSignal.FiniteInstance;

            if (value1.IsMinusOne() && value2.IsOne())
                return Float64ScalarCosSignal.FiniteInstance;

            return Float64ScalarCosSignal.FiniteInstance.MapValueRangeTo(value1, value2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteCos(Float64ScalarRange timeRange, double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.FiniteInstance.MapTimeRangeTo(timeRange);

            if (value.IsOne()) return 
                Float64ScalarCosSignal.FiniteInstance.MapTimeRangeTo(timeRange);

            return Float64ScalarCosSignal.FiniteInstance.ScaleValueBy(value).MapTimeRangeTo(timeRange);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteCos(Float64ScalarRange timeRange, double value1, double value2)
        {
            if (value1.IsZero() && value2.IsZero()) 
                return Float64ScalarConstantZeroSignal.FiniteInstance.MapTimeRangeTo(timeRange);

            if (value1.IsMinusOne() && value2.IsOne()) return 
                Float64ScalarCosSignal.FiniteInstance.MapTimeRangeTo(timeRange);

            return Float64ScalarCosSignal.FiniteInstance.MapValueRangeTo(value1, value2).MapTimeRangeTo(timeRange);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteCos(double timeMin, double timeMax, double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.FiniteInstance.MapTimeRangeTo(timeMin, timeMax);

            if (value.IsOne()) return 
                Float64ScalarCosSignal.FiniteInstance.MapTimeRangeTo(timeMin, timeMax);

            return Float64ScalarCosSignal.FiniteInstance.ScaleValueBy(value).MapTimeRangeTo(timeMin, timeMax);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteCos(double timeMin, double timeMax, double value1, double value2)
        {
            if (value1.IsZero() && value2.IsZero()) 
                return Float64ScalarConstantZeroSignal.FiniteInstance.MapTimeRangeTo(timeMin, timeMax);

            if (value1.IsMinusOne() && value2.IsOne()) return 
                Float64ScalarCosSignal.FiniteInstance.MapTimeRangeTo(timeMin, timeMax);

            return Float64ScalarCosSignal.FiniteInstance.MapValueRangeTo(value1, value2).MapTimeRangeTo(timeMin, timeMax);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteSinWave(Float64ScalarRange timeRange, double value, int cycleCount)
        {
            return Float64ScalarSinSignal
                .FiniteInstance
                .Repeat(cycleCount)
                .MapTimeRangeTo(timeRange)
                .MapValueRangeTo(-value, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteSinWave(Float64ScalarRange timeRange, double value1, double value2, int cycleCount)
        {
            return Float64ScalarSinSignal
                .FiniteInstance
                .Repeat(cycleCount)
                .MapTimeRangeTo(timeRange)
                .MapValueRangeTo(value1, value2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteSinWave(double timeMin, double timeMax, double value, int cycleCount)
        {
            return Float64ScalarSinSignal
                .FiniteInstance
                .Repeat(cycleCount)
                .MapTimeRangeTo(timeMin, timeMax)
                .MapValueRangeTo(-value, -value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteSinWave(double timeMin, double timeMax, double value1, double value2, int cycleCount)
        {
            return Float64ScalarSinSignal
                .FiniteInstance
                .Repeat(cycleCount)
                .MapTimeRangeTo(timeMin, timeMax)
                .MapValueRangeTo(value1, value2);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteCosWave(Float64ScalarRange timeRange, double value, int cycleCount)
        {
            return Float64ScalarCosSignal
                .FiniteInstance
                .Repeat(cycleCount)
                .MapTimeRangeTo(timeRange)
                .MapValueRangeTo(-value, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteCosWave(Float64ScalarRange timeRange, double value1, double value2, int cycleCount)
        {
            return Float64ScalarCosSignal
                .FiniteInstance
                .Repeat(cycleCount)
                .MapTimeRangeTo(timeRange)
                .MapValueRangeTo(value1, value2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteCosWave(double timeMin, double timeMax, double value, int cycleCount)
        {
            return Float64ScalarCosSignal
                .FiniteInstance
                .Repeat(cycleCount)
                .MapTimeRangeTo(timeMin, timeMax)
                .MapValueRangeTo(-value, -value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FiniteCosWave(double timeMin, double timeMax, double value1, double value2, int cycleCount)
        {
            return Float64ScalarCosSignal
                .FiniteInstance
                .Repeat(cycleCount)
                .MapTimeRangeTo(timeMin, timeMax)
                .MapValueRangeTo(value1, value2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarComputedSignal FiniteComputed(double timeMin, double timeMax, Func<double, double> getValueFunc)
        {
            return Float64ScalarComputedSignal.Finite(timeMin, timeMax, getValueFunc);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarComputedSignal FiniteComputed(Float64ScalarRange timeRange, Func<double, double> getValueFunc)
        {
            return Float64ScalarComputedSignal.Finite(timeRange, getValueFunc);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarComputedSignal FiniteComputed(Float64ScalarRange timeRange, Func<double, double> getValueFunc, Func<double, double> getDerivative1ValueFunc)
        {
            return Float64ScalarComputedSignal.Finite(timeRange, getValueFunc, getDerivative1ValueFunc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarComputedSignal FiniteComputed(Float64ScalarRange timeRange, Func<double, double> getValueFunc, Func<double, double> getDerivative1ValueFunc, Func<double, double> getDerivative2ValueFunc)
        {
            return Float64ScalarComputedSignal.Finite(
                timeRange, 
                getValueFunc, 
                getDerivative1ValueFunc, 
                getDerivative2ValueFunc
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarComputedSignal CreateComputed(Float64ScalarRange timeRange, bool isPeriodic, Func<double, double> getValueFunc)
        {
            return isPeriodic
                ? Float64ScalarComputedSignal.Periodic(timeRange, getValueFunc)
                : Float64ScalarComputedSignal.Finite(timeRange, getValueFunc);
        }


        public static Float64ScalarTriangleSignal FiniteTriangle()
        {
            return Float64ScalarTriangleSignal.FiniteSymmetric;
        }

        public static Float64ScalarTriangleSignal FiniteTriangle(double vertexRelativeTime)
        {
            return Float64ScalarTriangleSignal.Finite(vertexRelativeTime);
        }
        

        public static Float64ScalarSharpRectangleSignal FiniteSharpRectangle()
        {
            return Float64ScalarSharpRectangleSignal.FiniteInstance;
        }

        public static Float64ScalarSignal FiniteSmoothRectangle()
        {
            return Float64ScalarSmoothRectangleSignal.FiniteInstance;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicZero()
        {
            return Float64ScalarConstantZeroSignal.PeriodicInstance;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicZero(Float64ScalarRange timeRange)
        {
            return Float64ScalarConstantZeroSignal.PeriodicInstance.MapTimeRangeTo(timeRange);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicZero(double timeMin, double timeMax)
        {
            return Float64ScalarConstantZeroSignal.PeriodicInstance.MapTimeRangeTo(timeMin, timeMax);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicOne()
        {
            return Float64ScalarConstantOneSignal.PeriodicInstance;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicOne(Float64ScalarRange timeRange)
        {
            return Float64ScalarConstantZeroSignal.PeriodicInstance.MapTimeRangeTo(timeRange);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicOne(double timeMin, double timeMax)
        {
            return Float64ScalarConstantOneSignal.PeriodicInstance.MapTimeRangeTo(timeMin, timeMax);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicConstant(double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.PeriodicInstance;

            if (value.IsOne()) return 
                Float64ScalarConstantOneSignal.PeriodicInstance;

            return Float64ScalarConstantOneSignal.PeriodicInstance.ScaleValueBy(value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicConstant(Float64ScalarRange timeRange, double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.PeriodicInstance.MapTimeRangeTo(timeRange);

            if (value.IsOne()) return 
                Float64ScalarConstantOneSignal.PeriodicInstance.MapTimeRangeTo(timeRange);

            return Float64ScalarConstantOneSignal.PeriodicInstance.ScaleValueBy(value).MapTimeRangeTo(timeRange);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicConstant(double timeMin, double timeMax, double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.PeriodicInstance.MapTimeRangeTo(timeMin, timeMax);

            if (value.IsOne()) return 
                Float64ScalarConstantOneSignal.PeriodicInstance.MapTimeRangeTo(timeMin, timeMax);

            return Float64ScalarConstantOneSignal.PeriodicInstance.ScaleValueBy(value).MapTimeRangeTo(timeMin, timeMax);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal CreateConstant(Float64ScalarRange timeRange, bool isPeriodic, double value)
        {
            return isPeriodic 
                ? PeriodicConstant(timeRange, value) 
                : FiniteConstant(timeRange, value);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicSharpStep()
        {
            return Float64ScalarSharpStepSignal.PeriodicInstance;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicSharpStep(double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.PeriodicInstance;

            if (value.IsOne())
                return Float64ScalarSharpStepSignal.PeriodicInstance;

            return Float64ScalarSharpStepSignal.PeriodicInstance.MapValueRangeTo(-value, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicSharpStep(double value1, double value2)
        {
            if (value1.IsZero() && value2.IsZero()) 
                return Float64ScalarConstantZeroSignal.PeriodicInstance;

            if (value1.IsMinusOne() && value2.IsOne())
                return Float64ScalarSharpStepSignal.PeriodicInstance;

            return Float64ScalarSharpStepSignal.PeriodicInstance.MapValueRangeTo(value1, value2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicSharpStep(Float64ScalarRange timeRange, double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.PeriodicInstance.MapTimeRangeTo(timeRange);

            if (value.IsOne()) return 
                Float64ScalarSharpStepSignal.PeriodicInstance.MapTimeRangeTo(timeRange);

            return Float64ScalarSharpStepSignal.PeriodicInstance.ScaleValueBy(value).MapTimeRangeTo(timeRange);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicSharpStep(Float64ScalarRange timeRange, double value1, double value2)
        {
            if (value1.IsZero() && value2.IsZero()) 
                return Float64ScalarConstantZeroSignal.PeriodicInstance.MapTimeRangeTo(timeRange);

            if (value1.IsMinusOne() && value2.IsOne()) return 
                Float64ScalarSharpStepSignal.PeriodicInstance.MapTimeRangeTo(timeRange);

            return Float64ScalarSharpStepSignal.PeriodicInstance.MapValueRangeTo(value1, value2).MapTimeRangeTo(timeRange);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicSharpStep(double timeMin, double timeMax, double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.PeriodicInstance.MapTimeRangeTo(timeMin, timeMax);

            if (value.IsOne()) return 
                Float64ScalarSharpStepSignal.PeriodicInstance.MapTimeRangeTo(timeMin, timeMax);

            return Float64ScalarSharpStepSignal.PeriodicInstance.ScaleValueBy(value).MapTimeRangeTo(timeMin, timeMax);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicSharpStep(double timeMin, double timeMax, double value1, double value2)
        {
            if (value1.IsZero() && value2.IsZero()) 
                return Float64ScalarConstantZeroSignal.PeriodicInstance.MapTimeRangeTo(timeMin, timeMax);

            if (value1.IsMinusOne() && value2.IsOne()) return 
                Float64ScalarSharpStepSignal.PeriodicInstance.MapTimeRangeTo(timeMin, timeMax);

            return Float64ScalarSharpStepSignal.PeriodicInstance.MapValueRangeTo(value1, value2).MapTimeRangeTo(timeMin, timeMax);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicHalfSinStep()
        {
            return Float64ScalarHalfSinStepSignal.PeriodicInstance;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicHalfSinStep(double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.PeriodicInstance;

            if (value.IsOne())
                return Float64ScalarHalfSinStepSignal.PeriodicInstance;

            return Float64ScalarHalfSinStepSignal.PeriodicInstance.MapValueRangeTo(-value, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicHalfSinStep(double value1, double value2)
        {
            if (value1.IsZero() && value2.IsZero()) 
                return Float64ScalarConstantZeroSignal.PeriodicInstance;

            if (value1.IsMinusOne() && value2.IsOne())
                return Float64ScalarHalfSinStepSignal.PeriodicInstance;

            return Float64ScalarHalfSinStepSignal.PeriodicInstance.MapValueRangeTo(value1, value2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicHalfSinStep(Float64ScalarRange timeRange, double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.PeriodicInstance.MapTimeRangeTo(timeRange);

            if (value.IsOne()) return 
                Float64ScalarHalfSinStepSignal.PeriodicInstance.MapTimeRangeTo(timeRange);

            return Float64ScalarHalfSinStepSignal.PeriodicInstance.ScaleValueBy(value).MapTimeRangeTo(timeRange);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicHalfSinStep(Float64ScalarRange timeRange, double value1, double value2)
        {
            if (value1.IsZero() && value2.IsZero()) 
                return Float64ScalarConstantZeroSignal.PeriodicInstance.MapTimeRangeTo(timeRange);

            if (value1.IsMinusOne() && value2.IsOne()) return 
                Float64ScalarHalfSinStepSignal.PeriodicInstance.MapTimeRangeTo(timeRange);

            return Float64ScalarHalfSinStepSignal.PeriodicInstance.MapValueRangeTo(value1, value2).MapTimeRangeTo(timeRange);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicHalfSinStep(double timeMin, double timeMax, double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.PeriodicInstance.MapTimeRangeTo(timeMin, timeMax);

            if (value.IsOne()) return 
                Float64ScalarHalfSinStepSignal.PeriodicInstance.MapTimeRangeTo(timeMin, timeMax);

            return Float64ScalarHalfSinStepSignal.PeriodicInstance.ScaleValueBy(value).MapTimeRangeTo(timeMin, timeMax);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicHalfSinStep(double timeMin, double timeMax, double value1, double value2)
        {
            if (value1.IsZero() && value2.IsZero()) 
                return Float64ScalarConstantZeroSignal.PeriodicInstance.MapTimeRangeTo(timeMin, timeMax);

            if (value1.IsMinusOne() && value2.IsOne()) return 
                Float64ScalarHalfSinStepSignal.PeriodicInstance.MapTimeRangeTo(timeMin, timeMax);

            return Float64ScalarHalfSinStepSignal.PeriodicInstance.MapValueRangeTo(value1, value2).MapTimeRangeTo(timeMin, timeMax);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicSmoothStep()
        {
            return Float64ScalarSmoothStepSignal.PeriodicInstance;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicSmoothStep(double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.PeriodicInstance;

            if (value.IsOne())
                return Float64ScalarSmoothStepSignal.PeriodicInstance;

            return Float64ScalarSmoothStepSignal.PeriodicInstance.MapValueRangeTo(-value, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicSmoothStep(double value1, double value2)
        {
            if (value1.IsZero() && value2.IsZero()) 
                return Float64ScalarConstantZeroSignal.PeriodicInstance;

            if (value1.IsMinusOne() && value2.IsOne())
                return Float64ScalarSmoothStepSignal.PeriodicInstance;

            return Float64ScalarSmoothStepSignal.PeriodicInstance.MapValueRangeTo(value1, value2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicSmoothStep(Float64ScalarRange timeRange, double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.PeriodicInstance.MapTimeRangeTo(timeRange);

            if (value.IsOne()) return 
                Float64ScalarSmoothStepSignal.PeriodicInstance.MapTimeRangeTo(timeRange);

            return Float64ScalarSmoothStepSignal.PeriodicInstance.ScaleValueBy(value).MapTimeRangeTo(timeRange);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicSmoothStep(Float64ScalarRange timeRange, double value1, double value2)
        {
            if (value1.IsZero() && value2.IsZero()) 
                return Float64ScalarConstantZeroSignal.PeriodicInstance.MapTimeRangeTo(timeRange);

            if (value1.IsMinusOne() && value2.IsOne()) return 
                Float64ScalarSmoothStepSignal.PeriodicInstance.MapTimeRangeTo(timeRange);

            return Float64ScalarSmoothStepSignal.PeriodicInstance.MapValueRangeTo(value1, value2).MapTimeRangeTo(timeRange);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicSmoothStep(double timeMin, double timeMax, double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.PeriodicInstance.MapTimeRangeTo(timeMin, timeMax);

            if (value.IsOne()) return 
                Float64ScalarSmoothStepSignal.PeriodicInstance.MapTimeRangeTo(timeMin, timeMax);

            return Float64ScalarSmoothStepSignal.PeriodicInstance.ScaleValueBy(value).MapTimeRangeTo(timeMin, timeMax);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicSmoothStep(double timeMin, double timeMax, double value1, double value2)
        {
            if (value1.IsZero() && value2.IsZero()) 
                return Float64ScalarConstantZeroSignal.PeriodicInstance.MapTimeRangeTo(timeMin, timeMax);

            if (value1.IsMinusOne() && value2.IsOne()) return 
                Float64ScalarSmoothStepSignal.PeriodicInstance.MapTimeRangeTo(timeMin, timeMax);

            return Float64ScalarSmoothStepSignal.PeriodicInstance.MapValueRangeTo(value1, value2).MapTimeRangeTo(timeMin, timeMax);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicRamp()
        {
            return Float64ScalarRampSignal.PeriodicInstance;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicRamp(double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.PeriodicInstance;

            if (value.IsOne())
                return Float64ScalarRampSignal.PeriodicInstance;

            return Float64ScalarRampSignal.PeriodicInstance.MapValueRangeTo(-value, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicRamp(double value1, double value2)
        {
            if (value1.IsZero() && value2.IsZero()) 
                return Float64ScalarConstantZeroSignal.PeriodicInstance;

            if (value1.IsMinusOne() && value2.IsOne())
                return Float64ScalarRampSignal.PeriodicInstance;

            return Float64ScalarRampSignal.PeriodicInstance.MapValueRangeTo(value1, value2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicRamp(Float64ScalarRange timeRange, double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.PeriodicInstance.MapTimeRangeTo(timeRange);

            if (value.IsOne()) return 
                Float64ScalarRampSignal.PeriodicInstance.MapTimeRangeTo(timeRange);

            return Float64ScalarRampSignal.PeriodicInstance.ScaleValueBy(value).MapTimeRangeTo(timeRange);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicRamp(Float64ScalarRange timeRange, double value1, double value2)
        {
            if (value1.IsZero() && value2.IsZero()) 
                return Float64ScalarConstantZeroSignal.PeriodicInstance.MapTimeRangeTo(timeRange);

            if (value1.IsMinusOne() && value2.IsOne()) return 
                Float64ScalarRampSignal.PeriodicInstance.MapTimeRangeTo(timeRange);

            return Float64ScalarRampSignal.PeriodicInstance.MapValueRangeTo(value1, value2).MapTimeRangeTo(timeRange);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicRamp(double timeMin, double timeMax, double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.PeriodicInstance.MapTimeRangeTo(timeMin, timeMax);

            if (value.IsOne()) return 
                Float64ScalarRampSignal.PeriodicInstance.MapTimeRangeTo(timeMin, timeMax);

            return Float64ScalarRampSignal.PeriodicInstance.ScaleValueBy(value).MapTimeRangeTo(timeMin, timeMax);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicRamp(double timeMin, double timeMax, double value1, double value2)
        {
            if (value1.IsZero() && value2.IsZero()) 
                return Float64ScalarConstantZeroSignal.PeriodicInstance.MapTimeRangeTo(timeMin, timeMax);

            if (value1.IsMinusOne() && value2.IsOne()) return 
                Float64ScalarRampSignal.PeriodicInstance.MapTimeRangeTo(timeMin, timeMax);

            return Float64ScalarRampSignal.PeriodicInstance.MapValueRangeTo(value1, value2).MapTimeRangeTo(timeMin, timeMax);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicSin()
        {
            return Float64ScalarSinSignal.PeriodicInstance;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicSin(double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.PeriodicInstance;

            if (value.IsOne())
                return Float64ScalarSinSignal.PeriodicInstance;

            return Float64ScalarSinSignal.PeriodicInstance.MapValueRangeTo(-value, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicSin(double value1, double value2)
        {
            if (value1.IsZero() && value2.IsZero()) 
                return Float64ScalarConstantZeroSignal.PeriodicInstance;

            if (value1.IsMinusOne() && value2.IsOne())
                return Float64ScalarSinSignal.PeriodicInstance;

            return Float64ScalarSinSignal.PeriodicInstance.MapValueRangeTo(value1, value2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicSin(Float64ScalarRange timeRange, double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.PeriodicInstance.MapTimeRangeTo(timeRange);

            if (value.IsOne()) return 
                Float64ScalarSinSignal.PeriodicInstance.MapTimeRangeTo(timeRange);

            return Float64ScalarSinSignal.PeriodicInstance.ScaleValueBy(value).MapTimeRangeTo(timeRange);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicSin(Float64ScalarRange timeRange, double value1, double value2)
        {
            if (value1.IsZero() && value2.IsZero()) 
                return Float64ScalarConstantZeroSignal.PeriodicInstance.MapTimeRangeTo(timeRange);

            if (value1.IsMinusOne() && value2.IsOne()) return 
                Float64ScalarSinSignal.PeriodicInstance.MapTimeRangeTo(timeRange);

            return Float64ScalarSinSignal.PeriodicInstance.MapValueRangeTo(value1, value2).MapTimeRangeTo(timeRange);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicSin(double timeMin, double timeMax, double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.PeriodicInstance.MapTimeRangeTo(timeMin, timeMax);

            if (value.IsOne()) return 
                Float64ScalarSinSignal.PeriodicInstance.MapTimeRangeTo(timeMin, timeMax);

            return Float64ScalarSinSignal.PeriodicInstance.ScaleValueBy(value).MapTimeRangeTo(timeMin, timeMax);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicSin(double timeMin, double timeMax, double value1, double value2)
        {
            if (value1.IsZero() && value2.IsZero()) 
                return Float64ScalarConstantZeroSignal.PeriodicInstance.MapTimeRangeTo(timeMin, timeMax);

            if (value1.IsMinusOne() && value2.IsOne()) return 
                Float64ScalarSinSignal.PeriodicInstance.MapTimeRangeTo(timeMin, timeMax);

            return Float64ScalarSinSignal.PeriodicInstance.MapValueRangeTo(value1, value2).MapTimeRangeTo(timeMin, timeMax);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicCos()
        {
            return Float64ScalarCosSignal.PeriodicInstance;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicCos(double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.PeriodicInstance;

            if (value.IsOne())
                return Float64ScalarCosSignal.PeriodicInstance;

            return Float64ScalarCosSignal.PeriodicInstance.MapValueRangeTo(-value, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicCos(double value1, double value2)
        {
            if (value1.IsZero() && value2.IsZero()) 
                return Float64ScalarConstantZeroSignal.PeriodicInstance;

            if (value1.IsMinusOne() && value2.IsOne())
                return Float64ScalarCosSignal.PeriodicInstance;

            return Float64ScalarCosSignal.PeriodicInstance.MapValueRangeTo(value1, value2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicCos(Float64ScalarRange timeRange, double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.PeriodicInstance.MapTimeRangeTo(timeRange);

            if (value.IsOne()) return 
                Float64ScalarCosSignal.PeriodicInstance.MapTimeRangeTo(timeRange);

            return Float64ScalarCosSignal.PeriodicInstance.ScaleValueBy(value).MapTimeRangeTo(timeRange);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicCos(Float64ScalarRange timeRange, double value1, double value2)
        {
            if (value1.IsZero() && value2.IsZero()) 
                return Float64ScalarConstantZeroSignal.PeriodicInstance.MapTimeRangeTo(timeRange);

            if (value1.IsMinusOne() && value2.IsOne()) return 
                Float64ScalarCosSignal.PeriodicInstance.MapTimeRangeTo(timeRange);

            return Float64ScalarCosSignal.PeriodicInstance.MapValueRangeTo(value1, value2).MapTimeRangeTo(timeRange);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicCos(double timeMin, double timeMax, double value)
        {
            if (value.IsZero()) 
                return Float64ScalarConstantZeroSignal.PeriodicInstance.MapTimeRangeTo(timeMin, timeMax);

            if (value.IsOne()) return 
                Float64ScalarCosSignal.PeriodicInstance.MapTimeRangeTo(timeMin, timeMax);

            return Float64ScalarCosSignal.PeriodicInstance.ScaleValueBy(value).MapTimeRangeTo(timeMin, timeMax);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicCos(double timeMin, double timeMax, double value1, double value2)
        {
            if (value1.IsZero() && value2.IsZero()) 
                return Float64ScalarConstantZeroSignal.PeriodicInstance.MapTimeRangeTo(timeMin, timeMax);

            if (value1.IsMinusOne() && value2.IsOne()) return 
                Float64ScalarCosSignal.PeriodicInstance.MapTimeRangeTo(timeMin, timeMax);

            return Float64ScalarCosSignal.PeriodicInstance.MapValueRangeTo(value1, value2).MapTimeRangeTo(timeMin, timeMax);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicSinWave(Float64ScalarRange timeRange, double value, int cycleCount)
        {
            return Float64ScalarSinSignal
                .PeriodicInstance
                .Repeat(cycleCount)
                .MapTimeRangeTo(timeRange)
                .MapValueRangeTo(-value, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicSinWave(Float64ScalarRange timeRange, double value1, double value2, int cycleCount)
        {
            return Float64ScalarSinSignal
                .PeriodicInstance
                .Repeat(cycleCount)
                .MapTimeRangeTo(timeRange)
                .MapValueRangeTo(value1, value2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicSinWave(double timeMin, double timeMax, double value, int cycleCount)
        {
            return Float64ScalarSinSignal
                .PeriodicInstance
                .Repeat(cycleCount)
                .MapTimeRangeTo(timeMin, timeMax)
                .MapValueRangeTo(-value, -value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicSinWave(double timeMin, double timeMax, double value1, double value2, int cycleCount)
        {
            return Float64ScalarSinSignal
                .PeriodicInstance
                .Repeat(cycleCount)
                .MapTimeRangeTo(timeMin, timeMax)
                .MapValueRangeTo(value1, value2);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicCosWave(Float64ScalarRange timeRange, double value, int cycleCount)
        {
            return Float64ScalarCosSignal
                .PeriodicInstance
                .Repeat(cycleCount)
                .MapTimeRangeTo(timeRange)
                .MapValueRangeTo(-value, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicCosWave(Float64ScalarRange timeRange, double value1, double value2, int cycleCount)
        {
            return Float64ScalarCosSignal
                .PeriodicInstance
                .Repeat(cycleCount)
                .MapTimeRangeTo(timeRange)
                .MapValueRangeTo(value1, value2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicCosWave(double timeMin, double timeMax, double value, int cycleCount)
        {
            return Float64ScalarCosSignal
                .PeriodicInstance
                .Repeat(cycleCount)
                .MapTimeRangeTo(timeMin, timeMax)
                .MapValueRangeTo(-value, -value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicCosWave(double timeMin, double timeMax, double value1, double value2, int cycleCount)
        {
            return Float64ScalarCosSignal
                .PeriodicInstance
                .Repeat(cycleCount)
                .MapTimeRangeTo(timeMin, timeMax)
                .MapValueRangeTo(value1, value2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarComputedSignal PeriodicComputed(double timeMin, double timeMax, Func<double, double> getValueFunc)
        {
            return Float64ScalarComputedSignal.Periodic(timeMin, timeMax, getValueFunc);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarComputedSignal PeriodicComputed(Float64ScalarRange timeRange, Func<double, double> getValueFunc)
        {
            return Float64ScalarComputedSignal.Periodic(timeRange, getValueFunc);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarComputedSignal PeriodicComputed(Float64ScalarRange timeRange, Func<double, double> getValueFunc, Func<double, double> getDerivative1ValueFunc)
        {
            return Float64ScalarComputedSignal.Periodic(timeRange, getValueFunc, getDerivative1ValueFunc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarComputedSignal PeriodicComputed(Float64ScalarRange timeRange, Func<double, double> getValueFunc, Func<double, double> getDerivative1ValueFunc, Func<double, double> getDerivative2ValueFunc)
        {
            return Float64ScalarComputedSignal.Periodic(
                timeRange, 
                getValueFunc, 
                getDerivative1ValueFunc, 
                getDerivative2ValueFunc
            );
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarTriangleSignal PeriodicTriangle()
        {
            return Float64ScalarTriangleSignal.PeriodicSymmetric;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarTriangleSignal PeriodicTriangle(double vertexRelativeTime)
        {
            return Float64ScalarTriangleSignal.Periodic(vertexRelativeTime);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSharpRectangleSignal PeriodicSharpRectangle()
        {
            return Float64ScalarSharpRectangleSignal.PeriodicInstance;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal PeriodicSmoothRectangle()
        {
            return Float64ScalarSmoothRectangleSignal.PeriodicInstance;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal operator +(Float64ScalarSignal s1)
        {
            return s1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal operator -(Float64ScalarSignal s1)
        {
            return s1.NegativeValue();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal operator +(Float64ScalarSignal s1, double s2)
        {
            return s1.OffsetValueBy(s2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal operator +(double s1, Float64ScalarSignal s2)
        {
            return s2.OffsetValueBy(s1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal operator +(Float64ScalarSignal s1, Float64ScalarSignal s2)
        {
            return s1.Plus(s2);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal operator -(Float64ScalarSignal s1, double s2)
        {
            return s1.OffsetValueBy(-s2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal operator -(double s1, Float64ScalarSignal s2)
        {
            return s2.NegativeValue().OffsetValueBy(s1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal operator -(Float64ScalarSignal s1, Float64ScalarSignal s2)
        {
            return s1.Plus(s2.NegativeValue());
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal operator *(Float64ScalarSignal s1, double s2)
        {
            return s1.ScaleValueBy(s2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal operator *(double s1, Float64ScalarSignal s2)
        {
            return s2.ScaleValueBy(s1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal operator *(Float64ScalarSignal s1, Float64ScalarSignal s2)
        {
            return s1.Times(s2);
        }


        private Float64ScalarRange? _valueRange;

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual Float64ScalarRange FindValueRange()
        {
            return TimeRange.FindValueRange(GetValue);
        }

        public virtual Float64ScalarRange FindValueRange(double minTime, double maxTime)
        {
            if (minTime <= TimeRange.MinValue && maxTime >= TimeRange.MaxValue)
                return GetValueRange();

            if (maxTime <= TimeRange.MinValue && minTime >= TimeRange.MaxValue)
                return GetValueRange();

            if (minTime < maxTime)
                return Float64ScalarRange.Create(minTime, maxTime).FindValueRange(GetValue);

            if (minTime > maxTime)
                return Float64ScalarRange.Create(maxTime, minTime).FindValueRange(GetValue);

            var v = GetValue(minTime);
            return Float64ScalarRange.Create(v, v);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64ScalarRange GetValueRange()
        {
            _valueRange ??= FindValueRange();

            return _valueRange.Value;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IFloat64Trajectory ToFinite()
        {
            return ToFiniteSignal();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IFloat64Trajectory ToPeriodic()
        {
            return ToPeriodicSignal();
        }

        public abstract Float64ScalarSignal ToFiniteSignal();

        public abstract Float64ScalarSignal ToPeriodicSignal();


        public double GetDerivative1ValueNumerical(double t)
        {
            if (IsFinite && !TimeRange.Contains(t)) return 0d;

            var epsilon = Math.Pow(2, -39); // Near 1.82e-12

            t = this.ClampTime(t);

            if (t < MinTime + 16 * epsilon)
                return (GetValue(t + epsilon) - GetValue(t)) / epsilon;

            if (t > MaxTime - 16 * epsilon)
                return (GetValue(t) - GetValue(t - epsilon)) / epsilon;

            return MathNet.Numerics.Differentiate.Derivative(
                GetValue, 
                t, 
                1
            );
        }

        public double GetDerivative2ValueNumerical(double t)
        {
            if (IsFinite && !TimeRange.Contains(t)) return 0d;

            var epsilon = Math.Pow(2, -39); // Near 1.82e-12

            t = this.ClampTime(t);
            
            if (t < MinTime + 16 * epsilon)
                return (GetDerivative1Value(t + epsilon) - GetDerivative1Value(t)) / epsilon;

            if (t > MaxTime - 16 * epsilon)
                return (GetDerivative1Value(t) - GetDerivative1Value(t - epsilon)) / epsilon;
            
            return MathNet.Numerics.Differentiate.Derivative(
                GetDerivative1Value, 
                t, 
                1
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual double GetDerivative1Value(double t)
        {
            return GetDerivative1ValueNumerical(t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual double GetDerivative2Value(double t)
        {
            return GetDerivative2ValueNumerical(t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64ScalarSignal GetDerivative1ErrorSignal()
        {
            return Float64ScalarComputedSignal.Finite(
                TimeRange,
                t => GetDerivative1Value(t) - GetDerivative1ValueNumerical(t)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64ScalarSignal GetDerivative2ErrorSignal()
        {
            return Float64ScalarComputedSignal.Finite(
                TimeRange,
                t => GetDerivative2Value(t) - GetDerivative2ValueNumerical(t)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64SampledTimeSignal GetSampledSignal(int sampleCount)
        {
            var sampleList = Float64ScalarSignalSampleList.Create(this, sampleCount);

            return sampleList.CreateSignal(sampleList.SamplingSpecs.SamplingRate);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64ScalarSignal RadiansToDegrees()
        {
            return this.ToFloat64ScalarSignal(v => v.RadiansToDegrees());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64ScalarSignal DegreesToRadians()
        {
            return this.ToFloat64ScalarSignal(v => v.DegreesToRadians());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64PolarAngleTimeSignal RadiansToPolarAngle()
        {
            return LinFloat64PolarAngleTimeSignal.CreateFromRadians(this);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64PolarAngleTimeSignal DegreesToPolarAngle()
        {
            return LinFloat64PolarAngleTimeSignal.CreateFromDegrees(this);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64DirectedAngleTimeSignal RadiansToDirectedAngle()
        {
            return LinFloat64DirectedAngleTimeSignal.CreateFromRadians(this);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64DirectedAngleTimeSignal DegreesToDirectedAngle()
        {
            return LinFloat64DirectedAngleTimeSignal.CreateFromDegrees(this);
        }

    }
}

