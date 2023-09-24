using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors
{
    public sealed partial class RGaFloat64UniformMultivector
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64UniformMultivector operator +(RGaFloat64UniformMultivector v1)
        {
            return v1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64UniformMultivector operator -(RGaFloat64UniformMultivector v1)
        {
            return v1.Negative();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64UniformMultivector operator +(RGaFloat64UniformMultivector v1, RGaFloat64Multivector v2)
        {
            return (RGaFloat64UniformMultivector)v1.Add(v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64UniformMultivector operator -(RGaFloat64UniformMultivector v1, RGaFloat64Multivector v2)
        {
            return (RGaFloat64UniformMultivector)v1.Subtract(v2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64UniformMultivector operator *(RGaFloat64UniformMultivector v1, double v2)
        {
            return v1.Times(v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64UniformMultivector operator *(double v1, RGaFloat64UniformMultivector v2)
        {
            return v2.Times(v1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64UniformMultivector operator *(RGaFloat64UniformMultivector v1, RGaFloat64Scalar v2)
        {
            return v1.Times(v2.ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64UniformMultivector operator *(RGaFloat64Scalar v1, RGaFloat64UniformMultivector v2)
        {
            return v2.Times(v1.ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64UniformMultivector operator /(RGaFloat64UniformMultivector v1, double v2)
        {
            return v1.Times(1d / v2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64UniformMultivector MapScalars(Func<double, double> scalarMapping)
        {
            if (IsZero)
                return this;

            var idScalarPairs =
                IdScalarPairs.Select(
                    term => new KeyValuePair<ulong, double>(
                        term.Key,
                        scalarMapping(term.Value)
                    )
                );

            return Processor
                .CreateComposer()
                .AddTerms(idScalarPairs)
                .GetUniformMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64UniformMultivector MapScalars(Func<ulong, double, double> scalarMapping)
        {
            if (IsZero)
                return this;

            var idScalarPairs =
                IdScalarPairs.Select(
                    term => new KeyValuePair<ulong, double>(
                        term.Key,
                        scalarMapping(term.Key, term.Value)
                    )
                );

            return Processor
                .CreateComposer()
                .AddTerms(idScalarPairs)
                .GetUniformMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64UniformMultivector MapBasisBlades(Func<ulong, ulong> basisMapping)
        {
            if (IsZero)
                return this;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<ulong, double>(
                        basisMapping(term.Key),
                        term.Value
                    )
                );

            return Processor
                .CreateComposer()
                .AddTerms(termList)
                .GetUniformMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64UniformMultivector MapBasisBlades(Func<ulong, double, ulong> basisMapping)
        {
            if (IsZero)
                return this;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<ulong, double>(
                        basisMapping(term.Key, term.Value),
                        term.Value
                    )
                );

            return Processor
                .CreateComposer()
                .SetTerms(termList)
                .GetUniformMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64UniformMultivector MapTerms(Func<ulong, double, KeyValuePair<ulong, double>> termMapping)
        {
            if (IsZero)
                return this;

            var termList =
                IdScalarPairs.Select(
                    term =>
                        termMapping(term.Key, term.Value)
                );

            return Processor
                .CreateComposer()
                .SetTerms(termList)
                .GetUniformMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64UniformMultivector Negative()
        {
            return MapScalars(s => -(s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64UniformMultivector Times(double scalar)
        {
            return MapScalars(s => s * scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64UniformMultivector Divide(double scalar)
        {
            return MapScalars(s => s / scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64UniformMultivector DivideByENorm()
        {
            return Divide(ENorm().ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64UniformMultivector DivideByENormSquared()
        {
            return Divide(ENormSquared().ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64UniformMultivector DivideByNorm()
        {
            return Divide(Norm().ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64UniformMultivector DivideByNormSquared()
        {
            return Divide(NormSquared().ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64UniformMultivector Reverse()
        {
            return MapScalars((basis, scalar) =>
                basis.Grade().ReverseIsNegativeOfGrade()
                    ? -scalar
                    : scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64UniformMultivector GradeInvolution()
        {
            return MapScalars((basis, scalar) =>
                basis.Grade().GradeInvolutionIsNegativeOfGrade()
                    ? -scalar
                    : scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64UniformMultivector CliffordConjugate()
        {
            return MapScalars((basis, scalar) =>
                basis.CliffordConjugateSignOfBasisBladeId().IsNegative
                    ? -scalar
                    : scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64UniformMultivector Conjugate()
        {
            return MapScalars((basis, scalar) =>
                Processor.ConjugateSign(basis) * scalar
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64UniformMultivector EInverse()
        {
            return Reverse().Divide(
                ENormSquared().ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64UniformMultivector Inverse()
        {
            return Reverse().Divide(
                NormSquared().ScalarValue()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64UniformMultivector PseudoInverse()
        {
            var kVectorConjugate = Conjugate();

            return kVectorConjugate.Divide(
                kVectorConjugate.Sp(this).ScalarValue()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Multivector EDual(int vSpaceDimensions)
        {
            var blade =
                Processor.CreatePseudoScalarEInverse(vSpaceDimensions);

            return ELcp(blade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Multivector EDual(RGaFloat64KVector blade)
        {
            return ELcp(blade.EInverse());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Multivector Dual(int vSpaceDimensions)
        {
            var blade =
                Processor.CreatePseudoScalarInverse(vSpaceDimensions);

            return Lcp(blade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Multivector Dual(RGaFloat64KVector blade)
        {
            return Lcp(blade.Inverse());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Multivector EUnDual(int vSpaceDimensions)
        {
            var blade =
                Processor.CreatePseudoScalarReverse(vSpaceDimensions);

            return ELcp(blade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Multivector EUnDual(RGaFloat64KVector blade)
        {
            return ELcp(blade.Reverse());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Multivector UnDual(int vSpaceDimensions)
        {
            var blade =
                Processor.CreatePseudoScalarReverse(vSpaceDimensions);

            //TODO: Should this be: 'return mv.Lcp(blade.Conjugate());'?
            return Lcp(blade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Multivector UnDual(RGaFloat64KVector blade)
        {
            //TODO: Should this be: 'return mv.Lcp(blade.Conjugate());'?
            return Lcp(blade.Reverse());
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector Add(RGaFloat64Multivector mv2)
        {
            return Processor
                .CreateComposer()
                .SetMultivector(this)
                .AddMultivector(mv2)
                .GetUniformMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector Subtract(RGaFloat64Multivector mv2)
        {
            return Processor
                .CreateComposer()
                .SetMultivector(this)
                .SubtractMultivector(mv2)
                .GetUniformMultivector();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector Op(RGaFloat64Multivector mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroUniformMultivector();

            return Processor
                .CreateComposer()
                .AddOpTerms(this, mv2)
                .GetUniformMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector EGp(RGaFloat64Multivector mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroUniformMultivector();

            return Processor
                .CreateComposer()
                .AddEGpTerms(this, mv2)
                .GetUniformMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector Gp(RGaFloat64Multivector mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroUniformMultivector();

            return Processor
                .CreateComposer()
                .AddGpTerms(this, mv2)
                .GetUniformMultivector();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector ELcp(RGaFloat64Multivector mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroUniformMultivector();

            return Processor
                .CreateComposer()
                .AddELcpTerms(this, mv2)
                .GetUniformMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector Lcp(RGaFloat64Multivector mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroUniformMultivector();

            return Processor
                .CreateComposer()
                .AddLcpTerms(this, mv2)
                .GetUniformMultivector();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector ERcp(RGaFloat64Multivector mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroUniformMultivector();

            return Processor
                .CreateComposer()
                .AddERcpTerms(this, mv2)
                .GetUniformMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector Rcp(RGaFloat64Multivector mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroUniformMultivector();

            return Processor
                .CreateComposer()
                .AddRcpTerms(this, mv2)
                .GetUniformMultivector();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Scalar ESp(RGaFloat64Scalar mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Float64ScalarComposer
                .Create()
                .AddESpTerms(this, mv2)
                .GetRGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Scalar ESp(RGaFloat64Vector mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Float64ScalarComposer
                .Create()
                .AddESpTerms(this, mv2)
                .GetRGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Scalar ESp(RGaFloat64Bivector mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Float64ScalarComposer
                .Create()
                .AddESpTerms(this, mv2)
                .GetRGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Scalar ESp(RGaFloat64HigherKVector mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Float64ScalarComposer
                .Create()
                .AddESpTerms(this, mv2)
                .GetRGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Scalar ESp(RGaFloat64GradedMultivector mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Float64ScalarComposer
                .Create()
                .AddESpTerms(this, mv2)
                .GetRGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Scalar ESp(RGaFloat64UniformMultivector mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Float64ScalarComposer
                .Create()
                .AddESpTerms(this, mv2)
                .GetRGaScalar(Processor);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Scalar Sp(RGaFloat64Scalar mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Float64ScalarComposer
                .Create()
                .AddSpTerms(this, mv2)
                .GetRGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Scalar Sp(RGaFloat64Vector mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Float64ScalarComposer
                .Create()
                .AddSpTerms(this, mv2)
                .GetRGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Scalar Sp(RGaFloat64Bivector mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Float64ScalarComposer
                .Create()
                .AddSpTerms(this, mv2)
                .GetRGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Scalar Sp(RGaFloat64HigherKVector mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Float64ScalarComposer
                .Create()
                .AddSpTerms(this, mv2)
                .GetRGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Scalar Sp(RGaFloat64GradedMultivector mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Float64ScalarComposer
                .Create()
                .AddSpTerms(this, mv2)
                .GetRGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Scalar Sp(RGaFloat64UniformMultivector mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Float64ScalarComposer
                .Create()
                .AddSpTerms(this, mv2)
                .GetRGaScalar(Processor);
        }
    }
}
