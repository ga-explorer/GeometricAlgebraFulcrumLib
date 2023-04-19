using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars
{
    public static class ScalarFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalarFromObject<T>(this IScalarProcessor<T> scalarProcessor, object valueObject)
        {
            return valueObject switch
            {
                int v => scalarProcessor.GetScalarFromNumber(v),
                uint v => scalarProcessor.GetScalarFromNumber(v),
                long v => scalarProcessor.GetScalarFromNumber(v),
                ulong v => scalarProcessor.GetScalarFromNumber(v),
                float v => scalarProcessor.GetScalarFromNumber(v),
                double v => scalarProcessor.GetScalarFromNumber(v),
                string v => scalarProcessor.GetScalarFromText(v),
                T v => v,
                Scalar<T> v => v.ScalarValue,
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetScalarsFromNumbers<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<int> valuesList)
        {
            return valuesList.Select(scalarProcessor.GetScalarFromNumber);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetScalarsFromNumbers<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<uint> valuesList)
        {
            return valuesList.Select(scalarProcessor.GetScalarFromNumber);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetScalarsFromNumbers<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<long> valuesList)
        {
            return valuesList.Select(scalarProcessor.GetScalarFromNumber);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetScalarsFromNumbers<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<ulong> valuesList)
        {
            return valuesList.Select(scalarProcessor.GetScalarFromNumber);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetScalarsFromNumbers<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<float> valuesList)
        {
            return valuesList.Select(scalarProcessor.GetScalarFromNumber);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetScalarsFromNumbers<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<double> valuesList)
        {
            return valuesList.Select(scalarProcessor.GetScalarFromNumber);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetScalarsFrom<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<Scalar<T>> valuesList)
        {
            return valuesList.Select(value => value.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetScalarValues<T>(this IEnumerable<Scalar<T>> valuesList)
        {
            return valuesList.Select(value => value.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetScalarsFromText<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<string> valuesList)
        {
            return valuesList.Select(scalarProcessor.GetScalarFromText);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetScalarsFromObjects<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<object> valuesList)
        {
            return valuesList.Select(scalarProcessor.GetScalarFromObject);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalarZero<T>(this IScalarProcessor<T> scalarProcessor)
        {
            return Scalar<T>.CreateZero(scalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalarOne<T>(this IScalarProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.ScalarOne
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalarMinusOne<T>(this IScalarProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.ScalarMinusOne
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalar<T>(this IScalarProcessor<T> scalarProcessor, string valueText)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.GetScalarFromText(valueText)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalar<T>(this IScalarProcessor<T> scalarProcessor, object valueObject)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.GetScalarFromObject(valueObject)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalar<T>(this object valueObject, IScalarProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.GetScalarFromObject(valueObject)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalar<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
        {
            return Scalar<T>.Create(scalarProcessor, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalar<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalingFactor, T scalar)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalingFactor, scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalar<T>(this IScalarProcessor<T> scalarProcessor, int scalingFactor, T scalar)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalingFactor, scalar)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalar<T>(this int scalar, IScalarProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor, 
                scalarProcessor.GetScalarFromNumber(scalar)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalar<T>(this double scalar, IScalarProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor, 
                scalarProcessor.GetScalarFromNumber(scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalar<T>(this string scalar, IScalarProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor, 
                scalarProcessor.GetScalarFromText(scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalar<T>(this T scalar, IScalarProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(scalarProcessor, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalar<T>(this IScalarProcessor<T> scalarProcessor, int scalar)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.GetScalarFromNumber(scalar)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalar<T>(this IScalarProcessor<T> scalarProcessor, uint scalar)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.GetScalarFromNumber(scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalar<T>(this uint scalar, IScalarProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.GetScalarFromNumber(scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalar<T>(this IScalarProcessor<T> scalarProcessor, long scalar)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.GetScalarFromNumber(scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalar<T>(this long scalar, IScalarProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.GetScalarFromNumber(scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalar<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.GetScalarFromNumber(scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalar<T>(this ulong scalar, IScalarProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.GetScalarFromNumber(scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalar<T>(this IScalarProcessor<T> scalarProcessor, float scalar)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.GetScalarFromNumber(scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalar<T>(this float scalar, IScalarProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.GetScalarFromNumber(scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalar<T>(this IScalarProcessor<T> scalarProcessor, double scalar)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.GetScalarFromNumber(scalar)
            );
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IScalarProcessor<T> scalarProcessor, params T[] scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IScalarProcessor<T> scalarProcessor, params int[] scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IScalarProcessor<T> scalarProcessor, params uint[] scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IScalarProcessor<T> scalarProcessor, params long[] scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IScalarProcessor<T> scalarProcessor, params ulong[] scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IScalarProcessor<T> scalarProcessor, params Scalar<T>[] scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars.Select(s => s.ScalarValue))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<T> scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<int> scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<uint> scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<long> scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<ulong> scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<float> scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<double> scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IEnumerable<T> scalars, IScalarProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IEnumerable<int> scalars, IScalarProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IEnumerable<uint> scalars, IScalarProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IEnumerable<long> scalars, IScalarProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IEnumerable<ulong> scalars, IScalarProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IEnumerable<float> scalars, IScalarProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IEnumerable<double> scalars, IScalarProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T scalar2)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalar1, scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IScalarProcessor<T> scalarProcessor, params T[] scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IScalarProcessor<T> scalarProcessor, params int[] scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IScalarProcessor<T> scalarProcessor, params uint[] scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IScalarProcessor<T> scalarProcessor, params long[] scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IScalarProcessor<T> scalarProcessor, params ulong[] scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IScalarProcessor<T> scalarProcessor, params Scalar<T>[] scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars.Select(s => s.ScalarValue))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<T> scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<int> scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<uint> scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<long> scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<ulong> scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<float> scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<double> scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IEnumerable<T> scalars, IScalarProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IEnumerable<int> scalars, IScalarProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IEnumerable<uint> scalars, IScalarProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IEnumerable<long> scalars, IScalarProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IEnumerable<ulong> scalars, IScalarProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IEnumerable<float> scalars, IScalarProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IEnumerable<double> scalars, IScalarProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }
    }
}