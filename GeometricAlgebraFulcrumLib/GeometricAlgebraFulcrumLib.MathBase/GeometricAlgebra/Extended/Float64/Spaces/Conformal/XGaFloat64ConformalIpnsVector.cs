using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Spaces.Conformal
{
    public abstract class XGaFloat64ConformalIpnsVector :
        XGaFloat64ConformalBlade
    {
        protected bool AssumeUnitWeight { get; private set; }

        public override XGaFloat64KVector Blade 
            => Vector;

        public XGaFloat64Vector Vector { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected XGaFloat64ConformalIpnsVector(XGaFloat64ConformalSpace space, XGaFloat64Vector vector)
            : base(space)
        {
            Debug.Assert(
                vector.VSpaceDimensions <= space.VSpaceDimensions
            );

            Vector = vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected XGaFloat64ConformalIpnsVector(XGaFloat64ConformalSpace space, XGaFloat64Vector vector, bool assumeUnitWeight)
            : base(space)
        {
            Debug.Assert(
                vector.VSpaceDimensions <= space.VSpaceDimensions
            );

            AssumeUnitWeight = assumeUnitWeight;
            Vector = vector;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar Square()
        {
            return Vector.SpSquared();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar Weight()
        {
            return AssumeUnitWeight
                ? Processor.CreateOneScalar()
                : -Space.InfinityBasisVector.Sp(Vector);
        }

        public XGaFloat64Vector GetUnitWeightVector()
        {
            if (AssumeUnitWeight)
                return Vector;

            var weight = Weight().ScalarValue;

            if (weight.IsZero())
                return Vector;

            if (!weight.IsOne()) 
                return Vector.Divide(weight);

            AssumeUnitWeight = true;

            return Vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasUnitWeight()
        {
            return AssumeUnitWeight || 
                   (AssumeUnitWeight = (Weight().ScalarValue - 1d).IsZero());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasZeroWeight()
        {
            return !AssumeUnitWeight && 
                   Weight().ScalarValue.IsZero();
        }
    }
}