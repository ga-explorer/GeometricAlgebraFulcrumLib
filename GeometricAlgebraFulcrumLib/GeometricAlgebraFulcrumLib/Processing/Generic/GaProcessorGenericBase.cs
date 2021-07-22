using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Generic
{
    public abstract class GaProcessorGenericBase<T> :
        IGaProcessor<T>
    {
        public IGaScalarProcessor<T> ScalarProcessor { get; }

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

        public abstract IGasKVector<T> PseudoScalar { get; }

        public abstract IGasKVector<T> PseudoScalarInverse { get; }

        public abstract IGasKVector<T> PseudoScalarReverse { get; }


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

        public virtual IGasMultivector<T> Normalize(IGasMultivector<T> mv1)
        {
            return mv1.Divide(
                ScalarProcessor.SqrtOfAbs(NormSquared(mv1))
            );
        }


        public abstract T Sp(IGasMultivector<T> mv1);

        public abstract T Sp(IGasMultivector<T> mv1, IGasMultivector<T> mv2);

        public abstract IGasMultivector<T> Lcp(IGasMultivector<T> mv1, IGasMultivector<T> mv2);

        public abstract IGasMultivector<T> Rcp(IGasMultivector<T> mv1, IGasMultivector<T> mv2);

        public abstract IGasMultivector<T> Hip(IGasMultivector<T> mv1, IGasMultivector<T> mv2);

        public abstract IGasMultivector<T> Fdp(IGasMultivector<T> mv1, IGasMultivector<T> mv2);

        public abstract IGasMultivector<T> Acp(IGasMultivector<T> mv1, IGasMultivector<T> mv2);

        public abstract IGasMultivector<T> Cp(IGasMultivector<T> mv1, IGasMultivector<T> mv2);

        public abstract T NormSquared(IGasMultivector<T> mv1);

        public T Norm(IGasMultivector<T> mv1)
        {
            return ScalarProcessor.SqrtOfAbs(NormSquared(mv1));
        }

        public IGasMultivector<T> Dual(IGasMultivector<T> mv1)
        {
            return Lcp(mv1, PseudoScalarInverse);
        }

        public IGasMultivector<T> UnDual(IGasMultivector<T> mv1)
        {
            return Lcp(mv1, PseudoScalar);
        }
        
        public IGasMultivector<T> BladeInverse(IGasMultivector<T> mv1)
        {
            var bladeSpSquared = Sp(mv1);

            return mv1.Divide(bladeSpSquared);
        }

        public IGasMultivector<T> VersorInverse(IGasMultivector<T> mv1)
        {
            var versorSpReverse = NormSquared(mv1);

            return mv1.GetReverse().Divide(versorSpReverse);
        }

    }
}