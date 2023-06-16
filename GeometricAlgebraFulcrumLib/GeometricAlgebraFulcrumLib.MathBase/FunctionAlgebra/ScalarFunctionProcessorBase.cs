using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.FunctionAlgebra
{
    public abstract class ScalarFunctionProcessorBase<T> :
        ScalarProcessorContainer<T>,
        IScalarFunctionProcessor<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected ScalarFunctionProcessorBase(IScalarProcessor<T> scalarProcessor)
            : base(scalarProcessor)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Func<T, T> Negative(Func<T, T> f1)
        {
            return t => ScalarProcessor.Negative(f1(t));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Func<T, T> Add(Func<T, T> f1, T f2)
        {
            return t => ScalarProcessor.Add(f1(t), f2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Func<T, T> Add(T f1, Func<T, T> f2)
        {
            return t => ScalarProcessor.Add(f1, f2(t));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Func<T, T> Add(Func<T, T> f1, Func<T, T> f2)
        {
            return t => ScalarProcessor.Add(f1(t), f2(t));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Func<T, T> Subtract(Func<T, T> f1, T f2)
        {
            return t => ScalarProcessor.Subtract(f1(t), f2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Func<T, T> Subtract(T f1, Func<T, T> f2)
        {
            return t => ScalarProcessor.Subtract(f1, f2(t));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Func<T, T> Subtract(Func<T, T> f1, Func<T, T> f2)
        {
            return t => ScalarProcessor.Subtract(f1(t), f2(t));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Func<T, T> Times(Func<T, T> f1, T f2)
        {
            return t => ScalarProcessor.Times(f1(t), f2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Func<T, T> Times(T f1, Func<T, T> f2)
        {
            return t => ScalarProcessor.Times(f1, f2(t));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Func<T, T> Times(Func<T, T> f1, Func<T, T> f2)
        {
            return t => ScalarProcessor.Times(f1(t), f2(t));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Func<T, T> Divide(Func<T, T> f1, T f2)
        {
            return t => ScalarProcessor.Divide(f1(t), f2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Func<T, T> Divide(T f1, Func<T, T> f2)
        {
            return t => ScalarProcessor.Divide(f1, f2(t));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Func<T, T> Divide(Func<T, T> f1, Func<T, T> f2)
        {
            return t => ScalarProcessor.Divide(f1(t), f2(t));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Func<T, T> Compose(Func<T, T> f1, Func<T, T> f2)
        {
            return t => f2(f1(t));
        }

        public abstract T GetDerivativeValue(Func<T, T> scalarFunction, T t);

        public abstract T GetDerivativeValue(Func<T, T> scalarFunction, int order, T t);

        public abstract Func<T, T> GetDerivative(Func<T, T> scalarFunction);

        public abstract Func<T, T> GetDerivative(Func<T, T> scalarFunction, int order);
    }
}