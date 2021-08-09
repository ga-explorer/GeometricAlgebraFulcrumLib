using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Binary;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Unary;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Generic
{
    public abstract class GaProcessorGenericBase<T> :
        IGaProcessor<T>
    {
        public IGaScalarProcessor<T> ScalarProcessor { get; }

        public bool IsNumeric 
            => ScalarProcessor.IsNumeric;

        public bool IsSymbolic 
            => ScalarProcessor.IsSymbolic;

        public T ZeroScalar 
            => ScalarProcessor.ZeroScalar;

        public T OneScalar 
            => ScalarProcessor.OneScalar;

        public T MinusOneScalar 
            => ScalarProcessor.MinusOneScalar;

        public T PiScalar 
            => ScalarProcessor.PiScalar;

        public abstract uint VSpaceDimension { get; }

        public ulong GaSpaceDimension 
            => 1UL << (int) VSpaceDimension;

        public ulong MaxBasisBladeId 
            => (1UL << (int) VSpaceDimension) - 1UL;

        public uint GradesCount 
            => VSpaceDimension + 1;

        public IEnumerable<uint> Grades 
            => GradesCount.GetRange();

        public abstract IGaSignature Signature { get; }
        
        public abstract bool IsOrthonormal { get; }

        public abstract bool IsChangeOfBasis { get; }

        public abstract IGaStorageKVector<T> PseudoScalar { get; }

        public abstract IGaStorageKVector<T> PseudoScalarInverse { get; }

        public abstract IGaStorageKVector<T> PseudoScalarReverse { get; }


        protected GaProcessorGenericBase([NotNull] IGaScalarProcessor<T> scalarProcessor)
        {
            ScalarProcessor = scalarProcessor;
        }


        public T Add(T scalar1, T scalar2)
        {
            return ScalarProcessor.Add(scalar1, scalar2);
        }

        public T Add(params T[] scalarsList)
        {
            return ScalarProcessor.Add(scalarsList);
        }

        public T Add(IEnumerable<T> scalarsList)
        {
            return ScalarProcessor.Add(scalarsList);
        }

        public T Subtract(T scalar1, T scalar2)
        {
            return ScalarProcessor.Subtract(scalar1, scalar2);
        }

        public T Times(T scalar1, T scalar2)
        {
            return ScalarProcessor.Times(scalar1, scalar2);
        }

        public T Times(params T[] scalarsList)
        {
            return ScalarProcessor.Times(scalarsList);
        }

        public T Times(IEnumerable<T> scalarsList)
        {
            return ScalarProcessor.Times(scalarsList);
        }

        public T NegativeTimes(T scalar1, T scalar2)
        {
            return ScalarProcessor.NegativeTimes(scalar1, scalar2);
        }

        public T NegativeTimes(params T[] scalarsList)
        {
            return ScalarProcessor.NegativeTimes(scalarsList);
        }

        public T NegativeTimes(IEnumerable<T> scalarsList)
        {
            return ScalarProcessor.NegativeTimes(scalarsList);
        }

        public T Divide(T scalar1, T scalar2)
        {
            return ScalarProcessor.Divide(scalar1, scalar2);
        }

        public T NegativeDivide(T scalar1, T scalar2)
        {
            return ScalarProcessor.NegativeDivide(scalar1, scalar2);
        }

        public T Positive(T scalar)
        {
            return ScalarProcessor.Positive(scalar);
        }

        public T Negative(T scalar)
        {
            return ScalarProcessor.Negative(scalar);
        }

        public T Inverse(T scalar)
        {
            return ScalarProcessor.Inverse(scalar);
        }

        public T Abs(T scalar)
        {
            return ScalarProcessor.Abs(scalar);
        }

        public T Sqrt(T scalar)
        {
            return ScalarProcessor.Sqrt(scalar);
        }

        public T SqrtOfAbs(T scalar)
        {
            return ScalarProcessor.SqrtOfAbs(scalar);
        }

        public T Exp(T scalar)
        {
            return ScalarProcessor.Exp(scalar);
        }

        public T Log(T scalar)
        {
            return ScalarProcessor.Log(scalar);
        }

        public T Log2(T scalar)
        {
            return ScalarProcessor.Log2(scalar);
        }

        public T Log10(T scalar)
        {
            return ScalarProcessor.Log10(scalar);
        }

        public T Log(T scalar, T baseScalar)
        {
            return ScalarProcessor.Log(scalar);
        }

        public T Cos(T scalar)
        {
            return ScalarProcessor.Cos(scalar);
        }

        public T Sin(T scalar)
        {
            return ScalarProcessor.Sin(scalar);
        }

        public T Tan(T scalar)
        {
            return ScalarProcessor.Tan(scalar);
        }

        public T ArcCos(T scalar)
        {
            return ScalarProcessor.ArcCos(scalar);
        }

        public T ArcSin(T scalar)
        {
            return ScalarProcessor.ArcSin(scalar);
        }

        public T ArcTan(T scalar)
        {
            return ScalarProcessor.ArcTan(scalar);
        }

        public T ArcTan2(T scalarX, T scalarY)
        {
            return ScalarProcessor.ArcTan2(scalarX, scalarY);
        }

        public T Cosh(T scalar)
        {
            return ScalarProcessor.Cosh(scalar);
        }

        public T Sinh(T scalar)
        {
            return ScalarProcessor.Sinh(scalar);
        }

        public T Tanh(T scalar)
        {
            return ScalarProcessor.Tanh(scalar);
        }

        public bool IsValid(T scalar)
        {
            return ScalarProcessor.IsValid(scalar);
        }

        public bool IsZero(T scalar)
        {
            return ScalarProcessor.IsZero(scalar);
        }

        public bool IsZero(T scalar, bool nearZeroFlag)
        {
            return ScalarProcessor.IsZero(scalar, nearZeroFlag);
        }

        public bool IsNearZero(T scalar)
        {
            return ScalarProcessor.IsNearZero(scalar);
        }

        public bool IsPositive(T scalar)
        {
            return ScalarProcessor.IsPositive(scalar);
        }

        public bool IsNegative(T scalar)
        {
            return ScalarProcessor.IsNegative(scalar);
        }

        public bool IsNotNearPositive(T scalar)
        {
            return ScalarProcessor.IsNotNearPositive(scalar);
        }

        public bool IsNotNearNegative(T scalar)
        {
            return ScalarProcessor.IsNotNearNegative(scalar);
        }

        public T TextToScalar(string text)
        {
            return ScalarProcessor.TextToScalar(text);
        }

        public T IntegerToScalar(int value)
        {
            return ScalarProcessor.IntegerToScalar(value);
        }

        public T Float64ToScalar(double value)
        {
            return ScalarProcessor.Float64ToScalar(value);
        }

        public T GetRandomScalar(System.Random randomGenerator, double minValue, double maxValue)
        {
            return ScalarProcessor.GetRandomScalar(randomGenerator, minValue, maxValue);
        }

        public string ToText(T scalar)
        {
            return ScalarProcessor.ToText(scalar);
        }

        public virtual IGaStorageMultivector<T> Normalize(IGaStorageMultivector<T> mv1)
        {
            return ScalarProcessor.Divide(
                mv1,
                ScalarProcessor.SqrtOfAbs(NormSquared(mv1))
            );
        }


        public abstract T Sp(IGaStorageMultivector<T> mv1);

        public abstract T Sp(IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2);

        public abstract IGaStorageMultivector<T> Lcp(IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2);

        public abstract IGaStorageMultivector<T> Rcp(IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2);

        public abstract IGaStorageMultivector<T> Hip(IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2);

        public abstract IGaStorageMultivector<T> Fdp(IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2);

        public abstract IGaStorageMultivector<T> Acp(IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2);

        public abstract IGaStorageMultivector<T> Cp(IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2);

        public abstract T NormSquared(IGaStorageMultivector<T> mv1);

        public T Norm(IGaStorageMultivector<T> mv1)
        {
            return ScalarProcessor.SqrtOfAbs(NormSquared(mv1));
        }

        public IGaStorageMultivector<T> Dual(IGaStorageMultivector<T> mv1)
        {
            return Lcp(mv1, PseudoScalarInverse);
        }

        public IGaStorageMultivector<T> UnDual(IGaStorageMultivector<T> mv1)
        {
            return Lcp(mv1, PseudoScalar);
        }
        
        public IGaStorageMultivector<T> BladeInverse(IGaStorageMultivector<T> mv1)
        {
            var bladeSpSquared = Sp(mv1);

            return ScalarProcessor.Divide(mv1, bladeSpSquared);
        }

        public IGaStorageMultivector<T> VersorInverse(IGaStorageMultivector<T> mv1)
        {
            var versorSpReverse = NormSquared(mv1);

            return ScalarProcessor.Divide(
                ScalarProcessor.Reverse(mv1), 
                versorSpReverse
            );
        }

    }
}