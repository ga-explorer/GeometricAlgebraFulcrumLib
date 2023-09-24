using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors
{
    public sealed partial class XGaHigherKVector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaHigherKVector<T> operator -(XGaHigherKVector<T> mv1)
        {
            return mv1.Negative();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaHigherKVector<T> operator *(XGaHigherKVector<T> mv1, IntegerSign mv2)
        {
            if (mv2.IsZero)
                return mv1.Processor.CreateZeroHigherKVector(mv1.Grade);

            return mv2.IsPositive ? mv1 : mv1.Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaHigherKVector<T> operator *(IntegerSign mv1, XGaHigherKVector<T> mv2)
        {
            if (mv1.IsZero)
                return mv2.Processor.CreateZeroHigherKVector(mv2.Grade);

            return mv1.IsPositive ? mv2 : mv2.Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaHigherKVector<T> operator *(XGaHigherKVector<T> mv1, int mv2)
        {
            return mv1.Times(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaHigherKVector<T> operator *(int mv1, XGaHigherKVector<T> mv2)
        {
            return mv2.Times(
                mv2.ScalarProcessor.GetScalarFromNumber(mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaHigherKVector<T> operator *(XGaHigherKVector<T> mv1, uint mv2)
        {
            return mv1.Times(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaHigherKVector<T> operator *(uint mv1, XGaHigherKVector<T> mv2)
        {
            return mv2.Times(
                mv2.ScalarProcessor.GetScalarFromNumber(mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaHigherKVector<T> operator *(XGaHigherKVector<T> mv1, long mv2)
        {
            return mv1.Times(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaHigherKVector<T> operator *(long mv1, XGaHigherKVector<T> mv2)
        {
            return mv2.Times(
                mv2.ScalarProcessor.GetScalarFromNumber(mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaHigherKVector<T> operator *(XGaHigherKVector<T> mv1, ulong mv2)
        {
            return mv1.Times(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaHigherKVector<T> operator *(ulong mv1, XGaHigherKVector<T> mv2)
        {
            return mv2.Times(
                mv2.ScalarProcessor.GetScalarFromNumber(mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaHigherKVector<T> operator *(XGaHigherKVector<T> mv1, float mv2)
        {
            return mv1.Times(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaHigherKVector<T> operator *(float mv1, XGaHigherKVector<T> mv2)
        {
            return mv2.Times(
                mv2.ScalarProcessor.GetScalarFromNumber(mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaHigherKVector<T> operator *(XGaHigherKVector<T> mv1, double mv2)
        {
            return mv1.Times(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaHigherKVector<T> operator *(double mv1, XGaHigherKVector<T> mv2)
        {
            return mv2.Times(
                mv2.ScalarProcessor.GetScalarFromNumber(mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaHigherKVector<T> operator *(XGaHigherKVector<T> mv1, T mv2)
        {
            return mv1.Times(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaHigherKVector<T> operator *(T mv1, XGaHigherKVector<T> mv2)
        {
            return mv2.Times(mv1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaHigherKVector<T> operator *(XGaHigherKVector<T> mv1, Scalar<T> mv2)
        {
            return mv1.Times(mv2.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaHigherKVector<T> operator *(Scalar<T> mv1, XGaHigherKVector<T> mv2)
        {
            return mv2.Times(mv1.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaHigherKVector<T> operator *(XGaHigherKVector<T> mv1, XGaScalar<T> mv2)
        {
            return mv1.Times(mv2.ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaHigherKVector<T> operator *(XGaScalar<T> mv1, XGaHigherKVector<T> mv2)
        {
            return mv2.Times(mv1.ScalarValue());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaHigherKVector<T> operator /(XGaHigherKVector<T> mv1, IntegerSign mv2)
        {
            if (mv2.IsZero)
                throw new DivideByZeroException();

            return mv2.IsPositive ? mv1 : mv1.Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaHigherKVector<T> operator /(XGaHigherKVector<T> mv1, int mv2)
        {
            return mv1.Divide(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaHigherKVector<T> operator /(XGaHigherKVector<T> mv1, uint mv2)
        {
            return mv1.Divide(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaHigherKVector<T> operator /(XGaHigherKVector<T> mv1, long mv2)
        {
            return mv1.Divide(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaHigherKVector<T> operator /(XGaHigherKVector<T> mv1, ulong mv2)
        {
            return mv1.Divide(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaHigherKVector<T> operator /(XGaHigherKVector<T> mv1, float mv2)
        {
            return mv1.Divide(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaHigherKVector<T> operator /(XGaHigherKVector<T> mv1, double mv2)
        {
            return mv1.Divide(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaHigherKVector<T> operator /(XGaHigherKVector<T> mv1, T mv2)
        {
            return mv1.Divide(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaHigherKVector<T> operator /(XGaHigherKVector<T> mv1, Scalar<T> mv2)
        {
            return mv1.Divide(mv2.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaHigherKVector<T> operator /(XGaHigherKVector<T> mv1, XGaScalar<T> mv2)
        {
            return mv1.Divide(mv2.ScalarValue());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaHigherKVector<T> MapScalars(Func<T, T> scalarMapping)
        {
            if (IsZero) return this;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IIndexSet, T>(
                        term.Key,
                        scalarMapping(term.Value)
                    )
                );

            return Processor
                .CreateComposer()
                .SetTerms(termList)
                .GetHigherKVector(Grade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64HigherKVector MapScalars(XGaFloat64Processor processor, Func<T, double> scalarMapping)
        {
            if (IsZero) 
                return processor.CreateZeroHigherKVector(Grade);

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IIndexSet, double>(
                        term.Key,
                        scalarMapping(term.Value)
                    )
                );

            return processor
                .CreateComposer()
                .SetTerms(termList)
                .GetHigherKVector(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaHigherKVector<T1> MapScalars<T1>(XGaProcessor<T1> processor, Func<T, T1> scalarMapping)
        {
            if (IsZero) 
                return processor.CreateZeroHigherKVector(Grade);

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IIndexSet, T1>(
                        term.Key,
                        scalarMapping(term.Value)
                    )
                );

            return processor
                .CreateComposer()
                .SetTerms(termList)
                .GetHigherKVector(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaHigherKVector<T> MapScalars(Func<IIndexSet, T, T> scalarMapping)
        {
            if (IsZero) return this;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IIndexSet, T>(
                        term.Key,
                        scalarMapping(term.Key, term.Value)
                    )
                );

            return Processor
                .CreateComposer()
                .SetTerms(termList)
                .GetHigherKVector(Grade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64HigherKVector MapScalars(XGaFloat64Processor processor, Func<IIndexSet, T, double> scalarMapping)
        {
            if (IsZero) 
                return processor.CreateZeroHigherKVector(Grade);

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IIndexSet, double>(
                        term.Key,
                        scalarMapping(term.Key, term.Value)
                    )
                );

            return processor
                .CreateComposer()
                .SetTerms(termList)
                .GetHigherKVector(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaHigherKVector<T1> MapScalars<T1>(XGaProcessor<T1> processor, Func<IIndexSet, T, T1> scalarMapping)
        {
            if (IsZero) 
                return processor.CreateZeroHigherKVector(Grade);

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IIndexSet, T1>(
                        term.Key,
                        scalarMapping(term.Key, term.Value)
                    )
                );

            return processor
                .CreateComposer()
                .SetTerms(termList)
                .GetHigherKVector(Grade);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaHigherKVector<T> Negative()
        {
            if (IsZero) return this;
            
            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IIndexSet, T>(
                        term.Key,
                        ScalarProcessor.Negative(term.Value)
                    )
                );

            return Processor
                .CreateComposer()
                .SetTerms(termList)
                .GetHigherKVector(Grade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaHigherKVector<T> Times(T scalarValue)
        {
            if (IsZero || ScalarProcessor.IsOne(scalarValue)) return this;

            if (ScalarProcessor.IsZero(scalarValue))
                return Processor.CreateZeroHigherKVector(Grade);

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IIndexSet, T>(
                        term.Key,
                        ScalarProcessor.Times(term.Value, scalarValue)
                    )
                );

            return Processor
                .CreateComposer()
                .SetTerms(termList)
                .GetHigherKVector(Grade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaHigherKVector<T> Divide(T scalarValue)
        {
            if (IsZero || ScalarProcessor.IsOne(scalarValue)) return this;

            if (ScalarProcessor.IsZero(scalarValue))
                return Processor.CreateZeroHigherKVector(Grade);

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IIndexSet, T>(
                        term.Key,
                        ScalarProcessor.Divide(term.Value, scalarValue)
                    )
                );

            return Processor
                .CreateComposer()
                .SetTerms(termList)
                .GetHigherKVector(Grade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaHigherKVector<T> DivideByENorm()
        {
            return Divide(ENorm().ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaHigherKVector<T> DivideByENormSquared()
        {
            return Divide(ENormSquared().ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaHigherKVector<T> DivideByNorm()
        {
            return Divide(Norm().ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaHigherKVector<T> DivideByNormSquared()
        {
            return Divide(NormSquared().ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaHigherKVector<T> Reverse()
        {
            return IsZero || Grade.ReverseIsPositiveOfGrade()
                ? this
                : Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaHigherKVector<T> GradeInvolution()
        {
            return IsZero || Grade.GradeInvolutionIsPositiveOfGrade()
                ? this
                : Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaHigherKVector<T> CliffordConjugate()
        {
            return IsZero || Grade.CliffordConjugateIsPositiveOfGrade()
                ? this
                : Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaHigherKVector<T> Conjugate()
        {
            return IsZero
                ? this
                : MapScalars((basisKVector, scalar) =>
                    ScalarProcessor.Times(
                        Processor.ConjugateSign(basisKVector),
                        scalar
                    )
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaHigherKVector<T> EInverse()
        {
            return Divide(
                ESpSquared().ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaHigherKVector<T> Inverse()
        {
            return Divide(
                SpSquared().ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaHigherKVector<T> PseudoInverse()
        {
            var kVectorConjugate = Conjugate();

            return kVectorConjugate.Divide(
                kVectorConjugate.Sp(this).ScalarValue()
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Add(XGaMultivector<T> mv2)
        {
            if (IsZero)
                return mv2;

            if (mv2.IsZero)
                return this;

            return Processor
                .CreateComposer()
                .SetMultivector(this)
                .AddMultivector(mv2)
                .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Subtract(XGaMultivector<T> mv2)
        {
            if (IsZero)
                return mv2;

            if (mv2.IsZero)
                return this;

            return Processor
                .CreateComposer()
                .SetMultivector(this)
                .SubtractMultivector(mv2)
                .GetSimpleMultivector();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaHigherKVector<T> Op(XGaScalar<T> mv2)
        {
            return Times(mv2.ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> Op(XGaKVector<T> mv2)
        {
            if (mv2 is XGaScalar<T> scalar)
                return Times(scalar.ScalarValue());

            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();
            
            return Processor
                .CreateComposer()
                .AddOpTerms(this, mv2)
                .GetKVector(Grade + mv2.Grade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Op(XGaMultivector<T> mv2)
        {
            if (mv2 is XGaScalar<T> scalar)
                return Times(scalar.ScalarValue());

            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            if (mv2 is XGaKVector<T> kVector)
                return Processor
                    .CreateComposer()
                    .AddOpTerms(this, mv2)
                    .GetKVector(Grade + kVector.Grade);
            
            return Processor
                .CreateComposer()
                .AddOpTerms(this, mv2)
                .GetSimpleMultivector();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaHigherKVector<T> EGp(XGaScalar<T> mv2)
        {
            return Times(mv2.ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> EGp(XGaMultivector<T> mv2)
        {
            if (mv2 is XGaScalar<T> scalar)
                return EGp(scalar);

            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Processor
                .CreateComposer()
                .AddEGpTerms(this, mv2)
                .GetSimpleMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaHigherKVector<T> Gp(XGaScalar<T> mv2)
        {
            return Times(mv2.ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Gp(XGaMultivector<T> mv2)
        {
            if (mv2 is XGaScalar<T> scalar)
                return Gp(scalar);

            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Processor
                .CreateComposer()
                .AddGpTerms(this, mv2)
                .GetSimpleMultivector();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> ELcp(XGaScalar<T> mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> ELcp(XGaVector<T> mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> ELcp(XGaBivector<T> mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVector<T> ELcp(XGaHigherKVector<T> mv2)
        {
            if (Grade > mv2.Grade)
                return Processor.CreateZeroScalar();

            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Processor
                .CreateComposer()
                .AddELcpTerms(this, mv2)
                .GetKVector(mv2.Grade - Grade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> ELcp(XGaKVector<T> mv2)
        {
            if (Grade > mv2.Grade)
                return Processor.CreateZeroScalar();

            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Processor
                .CreateComposer()
                .AddELcpTerms(this, mv2)
                .GetKVector(mv2.Grade - Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> ELcp(XGaGradedMultivector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Processor
                .CreateComposer()
                .AddELcpTerms(this, mv2)
                .GetSimpleMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> ELcp(XGaUniformMultivector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Processor
                .CreateComposer()
                .AddELcpTerms(this, mv2)
                .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> ELcp(XGaMultivector<T> mv2)
        {
            return mv2 switch
            {
                XGaScalar<T> mv => ELcp(mv),
                XGaVector<T> mv => ELcp(mv),
                XGaBivector<T> mv => ELcp(mv),
                XGaHigherKVector<T> mv => ELcp(mv),
                XGaGradedMultivector<T> mv => ELcp(mv),
                XGaUniformMultivector<T> mv => ELcp(mv),
                _ => throw new InvalidOperationException()
            };
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> Lcp(XGaScalar<T> mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> Lcp(XGaVector<T> mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> Lcp(XGaBivector<T> mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVector<T> Lcp(XGaHigherKVector<T> mv2)
        {
            if (Grade > mv2.Grade)
                return Processor.CreateZeroScalar();

            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Processor
                .CreateComposer()
                .AddLcpTerms(this, mv2)
                .GetKVector(mv2.Grade - Grade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> Lcp(XGaKVector<T> mv2)
        {
            if (Grade > mv2.Grade)
                return Processor.CreateZeroScalar();

            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Processor
                .CreateComposer()
                .AddLcpTerms(this, mv2)
                .GetKVector(mv2.Grade - Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> Lcp(XGaGradedMultivector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Processor
                .CreateComposer()
                .AddLcpTerms(this, mv2)
                .GetSimpleMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> Lcp(XGaUniformMultivector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Processor
                .CreateComposer()
                .AddLcpTerms(this, mv2)
                .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Lcp(XGaMultivector<T> mv2)
        {
            return mv2 switch
            {
                XGaScalar<T> mv => Lcp(mv),
                XGaVector<T> mv => Lcp(mv),
                XGaBivector<T> mv => Lcp(mv),
                XGaHigherKVector<T> mv => Lcp(mv),
                XGaGradedMultivector<T> mv => Lcp(mv),
                XGaUniformMultivector<T> mv => Lcp(mv),
                _ => throw new InvalidOperationException()
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaHigherKVector<T> ERcp(XGaScalar<T> mv2)
        {
            return Times(mv2.ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVector<T> ERcp(XGaVector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Processor
                .CreateComposer()
                .AddERcpTerms(this, mv2)
                .GetKVector(Grade - 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVector<T> ERcp(XGaBivector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Processor
                .CreateComposer()
                .AddERcpTerms(this, mv2)
                .GetKVector(Grade - 2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVector<T> ERcp(XGaHigherKVector<T> mv2)
        {
            if (Grade < mv2.Grade)
                return Processor.CreateZeroScalar();

            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Processor
                .CreateComposer()
                .AddERcpTerms(this, mv2)
                .GetKVector(Grade - mv2.Grade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> ERcp(XGaKVector<T> mv2)
        {
            if (Grade < mv2.Grade)
                return Processor.CreateZeroScalar();

            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Processor
                .CreateComposer()
                .AddERcpTerms(this, mv2)
                .GetKVector(Grade - mv2.Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> ERcp(XGaGradedMultivector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Processor
                .CreateComposer()
                .AddERcpTerms(this, mv2)
                .GetSimpleMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> ERcp(XGaUniformMultivector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Processor
                .CreateComposer()
                .AddERcpTerms(this, mv2)
                .GetSimpleMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> ERcp(XGaMultivector<T> mv2)
        {
            return mv2 switch
            {
                XGaScalar<T> mv => ERcp(mv),
                XGaVector<T> mv => ERcp(mv),
                XGaBivector<T> mv => ERcp(mv),
                XGaHigherKVector<T> mv => ERcp(mv),
                XGaGradedMultivector<T> mv => ERcp(mv),
                XGaUniformMultivector<T> mv => ERcp(mv),
                _ => throw new InvalidOperationException()
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaHigherKVector<T> Rcp(XGaScalar<T> mv2)
        {
            return Times(mv2.ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVector<T> Rcp(XGaVector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Processor
                .CreateComposer()
                .AddRcpTerms(this, mv2)
                .GetKVector(Grade - 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVector<T> Rcp(XGaBivector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Processor
                .CreateComposer()
                .AddRcpTerms(this, mv2)
                .GetKVector(Grade - 2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVector<T> Rcp(XGaHigherKVector<T> mv2)
        {
            if (Grade < mv2.Grade)
                return Processor.CreateZeroScalar();

            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Processor
                .CreateComposer()
                .AddRcpTerms(this, mv2)
                .GetKVector(Grade - mv2.Grade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> Rcp(XGaKVector<T> mv2)
        {
            if (Grade < mv2.Grade)
                return Processor.CreateZeroScalar();

            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Processor
                .CreateComposer()
                .AddRcpTerms(this, mv2)
                .GetKVector(Grade - mv2.Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> Rcp(XGaGradedMultivector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Processor
                .CreateComposer()
                .AddRcpTerms(this, mv2)
                .GetSimpleMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> Rcp(XGaUniformMultivector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Processor
                .CreateComposer()
                .AddRcpTerms(this, mv2)
                .GetSimpleMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Rcp(XGaMultivector<T> mv2)
        {
            return mv2 switch
            {
                XGaScalar<T> mv => Rcp(mv),
                XGaVector<T> mv => Rcp(mv),
                XGaBivector<T> mv => Rcp(mv),
                XGaHigherKVector<T> mv => Rcp(mv),
                XGaGradedMultivector<T> mv => Rcp(mv),
                XGaUniformMultivector<T> mv => Rcp(mv),
                _ => throw new InvalidOperationException()
            };
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ESp(XGaScalar<T> mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ESp(XGaVector<T> mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ESp(XGaBivector<T> mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ESp(XGaHigherKVector<T> mv2)
        {
            if (Grade != mv2.Grade)
                return Processor.CreateZeroScalar();

            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return ScalarProcessor
                .CreateScalarComposer()
                .AddESpTerms(this, mv2)
                .GetXGaScalar(Processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ESp(XGaKVector<T> mv2)
        {
            if (Grade != mv2.Grade)
                return Processor.CreateZeroScalar();

            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return ScalarProcessor
                .CreateScalarComposer()
                .AddESpTerms(this, mv2)
                .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ESp(XGaGradedMultivector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return ScalarProcessor
                .CreateScalarComposer()
                .AddESpTerms(this, mv2)
                .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ESp(XGaUniformMultivector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return ScalarProcessor
                .CreateScalarComposer()
                .AddESpTerms(this, mv2)
                .GetXGaScalar(Processor);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Sp(XGaScalar<T> mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Sp(XGaVector<T> mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Sp(XGaBivector<T> mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Sp(XGaHigherKVector<T> mv2)
        {
            if (Grade != mv2.Grade)
                return Processor.CreateZeroScalar();

            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return ScalarProcessor
                .CreateScalarComposer()
                .AddSpTerms(this, mv2)
                .GetXGaScalar(Processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Sp(XGaKVector<T> mv2)
        {
            if (Grade != mv2.Grade)
                return Processor.CreateZeroScalar();

            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return ScalarProcessor
                .CreateScalarComposer()
                .AddSpTerms(this, mv2)
                .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Sp(XGaGradedMultivector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return ScalarProcessor
                .CreateScalarComposer()
                .AddSpTerms(this, mv2)
                .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Sp(XGaUniformMultivector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return ScalarProcessor
                .CreateScalarComposer()
                .AddSpTerms(this, mv2)
                .GetXGaScalar(Processor);
        }
    }
}
