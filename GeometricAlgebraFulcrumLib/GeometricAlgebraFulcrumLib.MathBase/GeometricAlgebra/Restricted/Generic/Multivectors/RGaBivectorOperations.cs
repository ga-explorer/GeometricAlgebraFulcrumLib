using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors
{
    public sealed partial class RGaBivector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBivector<T> operator -(RGaBivector<T> mv1)
        {
            return mv1.Negative();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBivector<T> operator +(RGaBivector<T> mv1, RGaBivector<T> mv2)
        {
            return mv1.Add(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBivector<T> operator -(RGaBivector<T> mv1, RGaBivector<T> mv2)
        {
            return mv1.Subtract(mv2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBivector<T> operator *(RGaBivector<T> mv1, IntegerSign mv2)
        {
            if (mv2.IsZero)
                return mv1.Processor.CreateZeroBivector();

            return mv2.IsPositive ? mv1 : mv1.Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBivector<T> operator *(IntegerSign mv1, RGaBivector<T> mv2)
        {
            if (mv1.IsZero)
                return mv2.Processor.CreateZeroBivector();

            return mv1.IsPositive ? mv2 : mv2.Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBivector<T> operator *(RGaBivector<T> mv1, int mv2)
        {
            return mv1.Times(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBivector<T> operator *(int mv1, RGaBivector<T> mv2)
        {
            return mv2.Times(
                mv2.ScalarProcessor.GetScalarFromNumber(mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBivector<T> operator *(RGaBivector<T> mv1, uint mv2)
        {
            return mv1.Times(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBivector<T> operator *(uint mv1, RGaBivector<T> mv2)
        {
            return mv2.Times(
                mv2.ScalarProcessor.GetScalarFromNumber(mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBivector<T> operator *(RGaBivector<T> mv1, long mv2)
        {
            return mv1.Times(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBivector<T> operator *(long mv1, RGaBivector<T> mv2)
        {
            return mv2.Times(
                mv2.ScalarProcessor.GetScalarFromNumber(mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBivector<T> operator *(RGaBivector<T> mv1, ulong mv2)
        {
            return mv1.Times(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBivector<T> operator *(ulong mv1, RGaBivector<T> mv2)
        {
            return mv2.Times(
                mv2.ScalarProcessor.GetScalarFromNumber(mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBivector<T> operator *(RGaBivector<T> mv1, float mv2)
        {
            return mv1.Times(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBivector<T> operator *(float mv1, RGaBivector<T> mv2)
        {
            return mv2.Times(
                mv2.ScalarProcessor.GetScalarFromNumber(mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBivector<T> operator *(RGaBivector<T> mv1, double mv2)
        {
            return mv1.Times(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBivector<T> operator *(double mv1, RGaBivector<T> mv2)
        {
            return mv2.Times(
                mv2.ScalarProcessor.GetScalarFromNumber(mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBivector<T> operator *(RGaBivector<T> mv1, T mv2)
        {
            return mv1.Times(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBivector<T> operator *(T mv1, RGaBivector<T> mv2)
        {
            return mv2.Times(mv1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBivector<T> operator *(RGaBivector<T> mv1, Scalar<T> mv2)
        {
            return mv1.Times(mv2.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBivector<T> operator *(Scalar<T> mv1, RGaBivector<T> mv2)
        {
            return mv2.Times(mv1.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBivector<T> operator *(RGaBivector<T> mv1, RGaScalar<T> mv2)
        {
            return mv1.Times(mv2.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBivector<T> operator *(RGaScalar<T> mv1, RGaBivector<T> mv2)
        {
            return mv2.Times(mv1.ScalarValue);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBivector<T> operator /(RGaBivector<T> mv1, IntegerSign mv2)
        {
            if (mv2.IsZero)
                throw new DivideByZeroException();

            return mv2.IsPositive ? mv1 : mv1.Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBivector<T> operator /(RGaBivector<T> mv1, int mv2)
        {
            return mv1.Divide(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBivector<T> operator /(RGaBivector<T> mv1, uint mv2)
        {
            return mv1.Divide(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBivector<T> operator /(RGaBivector<T> mv1, long mv2)
        {
            return mv1.Divide(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBivector<T> operator /(RGaBivector<T> mv1, ulong mv2)
        {
            return mv1.Divide(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBivector<T> operator /(RGaBivector<T> mv1, float mv2)
        {
            return mv1.Divide(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBivector<T> operator /(RGaBivector<T> mv1, double mv2)
        {
            return mv1.Divide(
                mv1.ScalarProcessor.GetScalarFromNumber(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBivector<T> operator /(RGaBivector<T> mv1, T mv2)
        {
            return mv1.Divide(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBivector<T> operator /(RGaBivector<T> mv1, Scalar<T> mv2)
        {
            return mv1.Divide(mv2.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBivector<T> operator /(RGaBivector<T> mv1, RGaScalar<T> mv2)
        {
            return mv1.Divide(mv2.ScalarValue);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Pair<RGaVector<T>> GetVectorBasis()
        {
            var closestVector = Processor.CreateVector(0);

            return GetVectorBasis(closestVector);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Pair<RGaVector<T>> GetVectorBasis(int closestBasisVectorIndex)
        {
            var closestVector = Processor.CreateVector(closestBasisVectorIndex);

            return GetVectorBasis(closestVector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Pair<RGaVector<T>> GetVectorBasis(RGaVector<T> closestVector)
        {
            var e1 = closestVector.Lcp(this).DivideByNorm();
            var e2 = e1.Lcp(this);

            Debug.Assert((e1.Op(e2) - this).IsNearZero());

            return new Pair<RGaVector<T>>(e1, e2);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBivector<T> MapScalars(Func<T, T> scalarMapping)
        {
            if (IsZero) return this;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<ulong, T>(
                        term.Key,
                        scalarMapping(term.Value)
                    )
                );

            return Processor
                .CreateComposer()
                .SetTerms(termList)
                .GetBivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Bivector MapScalars(RGaFloat64Processor processor, Func<T, double> scalarMapping)
        {
            if (IsZero) 
                return processor.CreateZeroBivector();

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<ulong, double>(
                        term.Key,
                        scalarMapping(term.Value)
                    )
                );

            return processor
                .CreateComposer()
                .SetTerms(termList)
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBivector<T1> MapScalars<T1>(RGaProcessor<T1> processor, Func<T, T1> scalarMapping)
        {
            if (IsZero) 
                return processor.CreateZeroBivector();

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<ulong, T1>(
                        term.Key,
                        scalarMapping(term.Value)
                    )
                );

            return processor
                .CreateComposer()
                .SetTerms(termList)
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBivector<T> MapScalars(Func<ulong, T, T> scalarMapping)
        {
            if (IsZero) return this;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<ulong, T>(
                        term.Key,
                        scalarMapping(term.Key, term.Value)
                    )
                );

            return Processor
                .CreateComposer()
                .SetTerms(termList)
                .GetBivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Bivector MapScalars(RGaFloat64Processor processor, Func<ulong, T, double> scalarMapping)
        {
            if (IsZero) 
                return processor.CreateZeroBivector();

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<ulong, double>(
                        term.Key,
                        scalarMapping(term.Key, term.Value)
                    )
                );

            return processor
                .CreateComposer()
                .SetTerms(termList)
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBivector<T1> MapScalars<T1>(RGaProcessor<T1> processor, Func<ulong, T, T1> scalarMapping)
        {
            if (IsZero) 
                return processor.CreateZeroBivector();

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<ulong, T1>(
                        term.Key,
                        scalarMapping(term.Key, term.Value)
                    )
                );

            return processor
                .CreateComposer()
                .SetTerms(termList)
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBivector<T> MapScalars(Func<int, int, T, T> scalarMapping)
        {
            if (IsZero) return this;

            var termList =
                IdScalarPairs
                    .Where(term => term.Key.Grade() == 1)
                    .Select(
                        term => new KeyValuePair<ulong, T>(
                            term.Key,
                            scalarMapping(term.Key.FirstOneBitPosition(), term.Key.LastOneBitPosition(), term.Value)
                        )
                    );

            return Processor
                .CreateComposer()
                .SetTerms(termList)
                .GetBivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBivector<T> MapBasisBivectors(Func<int, int, IPair<int>> basisMapping)
        {
            if (IsZero) return this;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<ulong, T>(
                        basisMapping(term.Key.FirstOneBitPosition(), term.Key.LastOneBitPosition()).IndexPairToBivectorId(),
                        term.Value
                    )
                );

            return Processor
                .CreateComposer()
                .AddTerms(termList)
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBivector<T> MapBasisBivectors(Func<int, int, T, IPair<int>> basisMapping)
        {
            if (IsZero) return this;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<ulong, T>(
                        basisMapping(term.Key.FirstOneBitPosition(), term.Key.LastOneBitPosition(), term.Value).IndexPairToBivectorId(),
                        term.Value
                    )
                );

            return Processor
                .CreateComposer()
                .AddTerms(termList)
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBivector<T> MapTerms(Func<int, int, T, KeyValuePair<IPair<int>, T>> termMapping)
        {
            if (IsZero) return this;

            var termList =
                IdScalarPairs.Select(
                    term =>
                    {
                        var (indexPair, scalar) = termMapping(
                            term.Key.FirstOneBitPosition(), 
                            term.Key.LastOneBitPosition(), 
                            term.Value
                        );

                        return new KeyValuePair<ulong, T>(
                            indexPair.IndexPairToBivectorId(),
                            scalar
                        );
                    }
                );

            return Processor
                .CreateComposer()
                .AddTerms(termList)
                .GetBivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBivector<T> Negative()
        {
            if (IsZero) return this;
            
            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<ulong, T>(
                        term.Key,
                        ScalarProcessor.Negative(term.Value)
                    )
                );

            return Processor
                .CreateComposer()
                .SetTerms(termList)
                .GetBivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBivector<T> Times(T scalarValue)
        {
            if (IsZero || ScalarProcessor.IsOne(scalarValue)) return this;

            if (ScalarProcessor.IsZero(scalarValue))
                return Processor.CreateZeroBivector();

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<ulong, T>(
                        term.Key,
                        ScalarProcessor.Times(term.Value, scalarValue)
                    )
                );

            return Processor
                .CreateComposer()
                .SetTerms(termList)
                .GetBivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBivector<T> Divide(T scalarValue)
        {
            if (IsZero || ScalarProcessor.IsOne(scalarValue)) return this;

            if (ScalarProcessor.IsZero(scalarValue))
                return Processor.CreateZeroBivector();

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<ulong, T>(
                        term.Key,
                        ScalarProcessor.Divide(term.Value, scalarValue)
                    )
                );

            return Processor
                .CreateComposer()
                .SetTerms(termList)
                .GetBivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBivector<T> DivideByENorm()
        {
            return Divide(ENorm().ScalarValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBivector<T> DivideByENormSquared()
        {
            return Divide(ENormSquared().ScalarValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBivector<T> DivideByNorm()
        {
            return Divide(Norm().ScalarValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBivector<T> DivideByNormSquared()
        {
            return Divide(NormSquared().ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBivector<T> Reverse()
        {
            return Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBivector<T> GradeInvolution()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBivector<T> CliffordConjugate()
        {
            return Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBivector<T> Conjugate()
        {
            return IsZero
                ? this
                : MapScalars((basisVector, scalar) =>
                    ScalarProcessor.Times(
                        Processor.ConjugateSign(basisVector),
                        scalar
                    )
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBivector<T> EInverse()
        {
            return Divide(
                ESpSquared().ScalarValue
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBivector<T> Inverse()
        {
            return Divide(
                SpSquared().ScalarValue
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBivector<T> PseudoInverse()
        {
            var kVectorConjugate = Conjugate();

            return kVectorConjugate.Divide(
                kVectorConjugate.Sp(this).ScalarValue
            );
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBivector<T> Add(RGaBivector<T> mv2)
        {
            if (IsZero)
                return mv2;

            if (mv2.IsZero)
                return this;

            return Processor
                .CreateComposer()
                .SetMultivector(this)
                .AddMultivector(mv2)
                .GetBivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaMultivector<T> Add(RGaMultivector<T> mv2)
        {
            if (mv2 is RGaBivector<T> bivector)
                return Add(bivector);

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
        public RGaBivector<T> Subtract(RGaBivector<T> mv2)
        {
            if (IsZero)
                return mv2;

            if (mv2.IsZero)
                return this;

            return Processor
                .CreateComposer()
                .SetMultivector(this)
                .SubtractMultivector(mv2)
                .GetBivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaMultivector<T> Subtract(RGaMultivector<T> mv2)
        {
            if (mv2 is RGaBivector<T> bivector)
                return Subtract(bivector);

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
        public RGaBivector<T> Op(RGaScalar<T> mv2)
        {
            return Times(mv2.ScalarValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaKVector<T> Op(RGaKVector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            if (mv2 is RGaScalar<T> scalar)
                return Times(scalar.ScalarValue);

            return Processor
                .CreateComposer()
                .AddOpTerms(this, mv2)
                .GetKVector(2 + mv2.Grade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaMultivector<T> Op(RGaMultivector<T> mv2)
        {
            if (mv2 is RGaScalar<T> scalar)
                return Times(scalar.ScalarValue);
            
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            if (mv2 is RGaKVector<T> kVector)
                return Processor
                    .CreateComposer()
                    .AddOpTerms(this, mv2)
                    .GetKVector(2 + kVector.Grade);

            return Processor
                .CreateComposer()
                .AddOpTerms(this, mv2)
                .GetSimpleMultivector();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBivector<T> EGp(RGaScalar<T> mv2)
        {
            return Times(mv2.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaMultivector<T> EGp(RGaMultivector<T> mv2)
        {
            if (mv2 is RGaScalar<T> scalar)
                return EGp(scalar);
            
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Processor
                .CreateComposer()
                .AddEGpTerms(this, mv2)
                .GetSimpleMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBivector<T> Gp(RGaScalar<T> mv2)
        {
            return Times(mv2.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaMultivector<T> Gp(RGaMultivector<T> mv2)
        {
            if (mv2 is RGaScalar<T> scalar)
                return Gp(scalar);
            
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return Processor
                .CreateComposer()
                .AddGpTerms(this, mv2)
                .GetSimpleMultivector();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> ELcp(RGaScalar<T> mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> ELcp(RGaVector<T> mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> ELcp(RGaBivector<T> mv2)
        {
            return ESp(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaKVector<T> ELcp(RGaHigherKVector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();
            
            return Processor
                .CreateComposer()
                .AddELcpTerms(this, mv2)
                .GetKVector(mv2.Grade - 2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaKVector<T> ELcp(RGaKVector<T> mv2)
        {
            if (mv2 is RGaScalar<T> or RGaVector<T>)
                return Processor.CreateZeroScalar();

            if (mv2 is RGaBivector<T> bv)
                return ESp(bv);

            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();
            
            return Processor.CreateComposer().AddELcpTerms(this, mv2).GetKVector(mv2.Grade - 2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaMultivector<T> ELcp(RGaGradedMultivector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();
            
            return Processor
                .CreateComposer()
                .AddELcpTerms(this, mv2)
                .GetSimpleMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaMultivector<T> ELcp(RGaMultivector<T> mv2)
        {
            if (mv2 is RGaScalar<T> or RGaVector<T>)
                return Processor.CreateZeroScalar();

            if (mv2 is RGaBivector<T> bv)
                return ESp(bv);

            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();
            
            if (mv2 is RGaHigherKVector<T> kv)
                return Processor.CreateComposer().AddELcpTerms(this, mv2).GetKVector(kv.Grade - 2);

            if (mv2 is RGaGradedMultivector<T> mv)
                return Processor.CreateComposer().AddELcpTerms(this, mv).GetSimpleMultivector();

            return Processor.CreateComposer().AddELcpTerms(this, mv2).GetSimpleMultivector();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> Lcp(RGaScalar<T> mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> Lcp(RGaVector<T> mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> Lcp(RGaBivector<T> mv2)
        {
            return Sp(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaKVector<T> Lcp(RGaHigherKVector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();
            
            return Processor
                .CreateComposer()
                .AddLcpTerms(this, mv2)
                .GetKVector(mv2.Grade - 2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaKVector<T> Lcp(RGaKVector<T> mv2)
        {
            if (mv2 is RGaScalar<T> or RGaVector<T>)
                return Processor.CreateZeroScalar();

            if (mv2 is RGaBivector<T> bv)
                return ESp(bv);

            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();
            
            return Processor.CreateComposer().AddLcpTerms(this, mv2).GetKVector(mv2.Grade - 2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaMultivector<T> Lcp(RGaGradedMultivector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();
            
            return Processor
                .CreateComposer()
                .AddLcpTerms(this, mv2)
                .GetSimpleMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaMultivector<T> Lcp(RGaMultivector<T> mv2)
        {
            if (mv2 is RGaScalar<T> or RGaVector<T>)
                return Processor.CreateZeroScalar();

            if (mv2 is RGaBivector<T> bv)
                return ESp(bv);

            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();
            
            if (mv2 is RGaHigherKVector<T> kv)
                return Processor.CreateComposer().AddLcpTerms(this, mv2).GetKVector(kv.Grade - 2);
            
            if (mv2 is RGaGradedMultivector<T> mv)
                return Processor.CreateComposer().AddLcpTerms(this, mv).GetSimpleMultivector();
            
            return Processor.CreateComposer().AddLcpTerms(this, mv2).GetSimpleMultivector();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBivector<T> ERcp(RGaScalar<T> mv2)
        {
            return Times(mv2.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaVector<T> ERcp(RGaVector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroVector();
            
            return Processor
                .CreateComposer()
                .AddERcpTerms(this, mv2)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> ERcp(RGaBivector<T> mv2)
        {
            return ESp(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaKVector<T> ERcp(RGaHigherKVector<T> mv2)
        {
            return Processor.CreateZeroScalar();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaKVector<T> ERcp(RGaKVector<T> mv2)
        {
            return mv2 switch
            {
                RGaScalar<T> s => ERcp(s),
                RGaVector<T> v => ERcp(v),
                RGaBivector<T> bv => ESp(bv),
                _ => Processor.CreateZeroScalar()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaMultivector<T> ERcp(RGaGradedMultivector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();
            
            return Processor
                .CreateComposer()
                .AddERcpTerms(this, mv2)
                .GetSimpleMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaMultivector<T> ERcp(RGaMultivector<T> mv2)
        {
            if (mv2 is RGaScalar<T> s)
                return ERcp(s);

            if (mv2 is RGaVector<T> v)
                return ERcp(v);

            if (mv2 is RGaBivector<T> bv)
                return ESp(bv);

            if (mv2 is RGaHigherKVector<T>)
                return Processor.CreateZeroScalar();

            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();
            
            if (mv2 is RGaGradedMultivector<T> mv)
                return Processor.CreateComposer().AddERcpTerms(this, mv).GetSimpleMultivector();

            return Processor.CreateComposer().AddERcpTerms(this, mv2).GetSimpleMultivector();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBivector<T> Rcp(RGaScalar<T> mv2)
        {
            return Times(mv2.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaVector<T> Rcp(RGaVector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroVector();
            
            return Processor
                .CreateComposer()
                .AddRcpTerms(this, mv2)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaScalar<T> Rcp(RGaBivector<T> mv2)
        {
            return Sp(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaKVector<T> Rcp(RGaHigherKVector<T> mv2)
        {
            return Processor.CreateZeroScalar();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaKVector<T> Rcp(RGaKVector<T> mv2)
        {
            return mv2 switch
            {
                RGaScalar<T> s => Rcp(s),
                RGaVector<T> v => Rcp(v),
                RGaBivector<T> bv => Sp(bv),
                _ => Processor.CreateZeroScalar()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaMultivector<T> Rcp(RGaGradedMultivector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();
            
            return Processor
                .CreateComposer()
                .AddRcpTerms(this, mv2)
                .GetSimpleMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaMultivector<T> Rcp(RGaMultivector<T> mv2)
        {
            if (mv2 is RGaScalar<T> s)
                return Rcp(s);

            if (mv2 is RGaVector<T> v)
                return Rcp(v);

            if (mv2 is RGaBivector<T> bv)
                return Sp(bv);

            if (mv2 is RGaHigherKVector<T>)
                return Processor.CreateZeroScalar();

            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();
            
            if (mv2 is RGaGradedMultivector<T> mv)
                return Processor.CreateComposer().AddRcpTerms(this, mv).GetSimpleMultivector();
            
            return Processor.CreateComposer().AddRcpTerms(this, mv2).GetSimpleMultivector();
        }

        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaScalar<T> ESp(RGaScalar<T> mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaScalar<T> ESp(RGaVector<T> mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaScalar<T> ESp(RGaBivector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();
            
            return ScalarProcessor
                .CreateScalarComposer()
                .AddESpTerms(this, mv2)
                .GetRGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaScalar<T> ESp(RGaHigherKVector<T> mv2)
        {
            return Processor.CreateZeroScalar();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaScalar<T> ESp(RGaKVector<T> mv2)
        {
            if (mv2 is not RGaBivector<T> bv)
                return Processor.CreateZeroScalar();
            
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return ScalarProcessor
                .CreateScalarComposer()
                .AddESpTerms(this, bv)
                .GetRGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaScalar<T> ESp(RGaGradedMultivector<T> mv2)
        {
            if (!mv2.TryGetKVector(2, out var kv))
                return Processor.CreateZeroScalar();

            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return ScalarProcessor
                .CreateScalarComposer()
                .AddESpTerms(this, (RGaBivector<T>)kv)
                .GetRGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaScalar<T> ESp(RGaUniformMultivector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return ScalarProcessor
                .CreateScalarComposer()
                .AddESpTerms(this, mv2)
                .GetRGaScalar(Processor);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaScalar<T> Sp(RGaScalar<T> mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaScalar<T> Sp(RGaVector<T> mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaScalar<T> Sp(RGaBivector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return ScalarProcessor
                .CreateScalarComposer()
                .AddSpTerms(this, mv2)
                .GetRGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaScalar<T> Sp(RGaHigherKVector<T> mv2)
        {
            return Processor.CreateZeroScalar();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaScalar<T> Sp(RGaKVector<T> mv2)
        {
            if (mv2 is not RGaBivector<T> bv)
                return Processor.CreateZeroScalar();

            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();
            
            return ScalarProcessor
                .CreateScalarComposer()
                .AddSpTerms(this, bv)
                .GetRGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaScalar<T> Sp(RGaGradedMultivector<T> mv2)
        {
            if (!mv2.TryGetKVector(2, out var kv))
                return Processor.CreateZeroScalar();

            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();
            
            return ScalarProcessor
                .CreateScalarComposer()
                .AddSpTerms(this, (RGaBivector<T>)kv)
                .GetRGaScalar(Processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaScalar<T> Sp(RGaUniformMultivector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.CreateZeroScalar();

            return ScalarProcessor
                .CreateScalarComposer()
                .AddSpTerms(this, mv2)
                .GetRGaScalar(Processor);
        }

    }
}
