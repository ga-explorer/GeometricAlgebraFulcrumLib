using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class ScalarFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalarFromObject<T>(this IScalarAlgebraProcessor<T> scalarProcessor, object valueObject)
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
        public static IEnumerable<T> GetScalarsFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<int> valuesList)
        {
            return valuesList.Select(scalarProcessor.GetScalarFromNumber);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetScalarsFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<uint> valuesList)
        {
            return valuesList.Select(scalarProcessor.GetScalarFromNumber);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetScalarsFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<long> valuesList)
        {
            return valuesList.Select(scalarProcessor.GetScalarFromNumber);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetScalarsFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<ulong> valuesList)
        {
            return valuesList.Select(scalarProcessor.GetScalarFromNumber);
        }
                
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetScalarsFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<float> valuesList)
        {
            return valuesList.Select(scalarProcessor.GetScalarFromNumber);
        }
                
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetScalarsFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<double> valuesList)
        {
            return valuesList.Select(scalarProcessor.GetScalarFromNumber);
        }
                
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetScalarsFrom<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<Scalar<T>> valuesList)
        {
            return valuesList.Select(value => value.ScalarValue);
        }
                
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetScalarValues<T>(this IEnumerable<Scalar<T>> valuesList)
        {
            return valuesList.Select(value => value.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetScalarsFromText<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<string> valuesList)
        {
            return valuesList.Select(scalarProcessor.GetScalarFromText);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetScalarsFromObjects<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<object> valuesList)
        {
            return valuesList.Select(scalarProcessor.GetScalarFromObject);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalarZero<T>(this IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return Scalar<T>.CreateZero(scalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalarOne<T>(this IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor, 
                scalarProcessor.ScalarOne
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalarMinusOne<T>(this IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor, 
                scalarProcessor.ScalarMinusOne
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, object valueObject)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.GetScalarFromObject(valueObject)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalar<T>(this object valueObject, IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.GetScalarFromObject(valueObject)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar)
        {
            return Scalar<T>.Create(scalarProcessor, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalar<T>(this T scalar, IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(scalarProcessor, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int scalar)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.GetScalarFromNumber(scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalar<T>(this int scalar, IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.GetScalarFromNumber(scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint scalar)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.GetScalarFromNumber(scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalar<T>(this uint scalar, IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.GetScalarFromNumber(scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, long scalar)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.GetScalarFromNumber(scalar)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalar<T>(this long scalar, IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.GetScalarFromNumber(scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ulong scalar)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.GetScalarFromNumber(scalar)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalar<T>(this ulong scalar, IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.GetScalarFromNumber(scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, float scalar)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.GetScalarFromNumber(scalar)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalar<T>(this float scalar, IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.GetScalarFromNumber(scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, double scalar)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.GetScalarFromNumber(scalar)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> CreateScalar<T>(this double scalar, IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.GetScalarFromNumber(scalar)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params T[] scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params int[] scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params uint[] scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params long[] scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params ulong[] scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params Scalar<T>[] scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars.Select(s => s.ScalarValue))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<T> scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<int> scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<uint> scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<long> scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<ulong> scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<float> scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<double> scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IEnumerable<T> scalars, IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IEnumerable<int> scalars, IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IEnumerable<uint> scalars, IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IEnumerable<long> scalars, IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IEnumerable<ulong> scalars, IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IEnumerable<float> scalars, IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> AddToScalar<T>(this IEnumerable<double> scalars, IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Add(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params T[] scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params int[] scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params uint[] scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params long[] scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params ulong[] scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params Scalar<T>[] scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars.Select(s => s.ScalarValue))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<T> scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<int> scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<uint> scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<long> scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<ulong> scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<float> scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<double> scalars)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IEnumerable<T> scalars, IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IEnumerable<int> scalars, IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IEnumerable<uint> scalars, IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IEnumerable<long> scalars, IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IEnumerable<ulong> scalars, IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IEnumerable<float> scalars, IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> TimesToScalar<T>(this IEnumerable<double> scalars, IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return Scalar<T>.Create(
                scalarProcessor,
                scalarProcessor.Times(scalars.Select(scalarProcessor.GetScalarFromNumber))
            );
        }    }
}