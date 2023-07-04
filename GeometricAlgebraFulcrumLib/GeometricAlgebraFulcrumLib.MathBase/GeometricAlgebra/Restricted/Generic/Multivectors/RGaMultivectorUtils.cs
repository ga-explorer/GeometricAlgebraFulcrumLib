using System.Runtime.CompilerServices;
using DataStructuresLib.Combinations;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors
{
    public static class RGaMultivectorUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaRandomComposer<T> CreateRGaRandomComposer<T>(this RGaProcessor<T> processor, int vSpaceDimensions)
        {
            return new RGaRandomComposer<T>(processor, vSpaceDimensions);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaRandomComposer<T> CreateRGaRandomComposer<T>(this RGaProcessor<T> processor, int vSpaceDimensions, int seed)
        {
            return new RGaRandomComposer<T>(processor, vSpaceDimensions, seed);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetVSpaceDimensions<T>(this IEnumerable<RGaMultivector<T>> mvList)
        {
            return mvList.Max(mv => mv.VSpaceDimensions);
        }

        public static T[] KVectorToArray<T>(this RGaKVector<T> kVector, int vSpaceDimensions)
        {
            if (vSpaceDimensions < kVector.VSpaceDimensions)
                throw new ArgumentException(nameof(vSpaceDimensions));

            var kvSpaceDimensions =
                (int)vSpaceDimensions.GetBinomialCoefficient(kVector.Grade);

            var array = kVector.ScalarProcessor.CreateArrayZero1D(kvSpaceDimensions);

            foreach (var (index, scalar) in kVector.GetKVectorArrayItems())
                array[index] = scalar;

            return array;
        }

        public static T[] MultivectorToArray<T>(this RGaMultivector<T> kVector, int vSpaceDimensions)
        {
            if (vSpaceDimensions > 31 || vSpaceDimensions < kVector.VSpaceDimensions)
                throw new ArgumentException(nameof(vSpaceDimensions));

            var gaSpaceDimensions =
                1 << vSpaceDimensions;

            var array = kVector.ScalarProcessor.CreateArrayZero1D(gaSpaceDimensions);

            foreach (var (index, scalar) in kVector.GetMultivectorArrayItems())
                array[index] = scalar;

            return array;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> GetVectorTermScalar<T>(this RGaMultivector<T> mv, int index)
        {
            var id = index.BasisVectorIndexToId();

            return mv.GetTermScalar(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> GetTermScalar<T>(this RGaMultivector<T> mv, int index1, int index2)
        {
            var basisBlade = mv.Processor.Gp(
                index1.BasisVectorIndexToId(), 
                index2.BasisVectorIndexToId()
            );

            if (basisBlade.IsZero)
                return Scalar<T>.CreateZero(mv.ScalarProcessor);

            var scalar = mv.GetTermScalar(basisBlade.Id);

            return basisBlade.IsNegative
                ? -scalar : scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> GetTermScalar<T>(this RGaMultivector<T> mv, params int[] indexList)
        {
            var basisBlade = mv.Processor.Gp(indexList);

            if (basisBlade.IsZero)
                return Scalar<T>.CreateZero(mv.ScalarProcessor);

            var scalar = mv.GetTermScalar(basisBlade.Id);

            return basisBlade.IsNegative
                ? -scalar : scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> GetTermScalar<T>(this RGaMultivector<T> mv, IReadOnlyList<int> indexList)
        {
            var basisBlade = mv.Processor.Gp(indexList);

            if (basisBlade.IsZero)
                return Scalar<T>.CreateZero(mv.ScalarProcessor);

            var scalar = mv.GetTermScalar(basisBlade.Id);

            return basisBlade.IsNegative
                ? -scalar : scalar;
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivector<T> GetPart<T>(this RGaMultivector<T> mv, Func<ulong, bool> filterFunc)
        {
            return mv switch
            {
                RGaScalar<T> s => s.GetPart(filterFunc),
                RGaVector<T> v => v.GetPart(filterFunc),
                RGaBivector<T> bv => bv.GetPart(filterFunc),
                RGaHigherKVector<T> kv => kv.GetPart(filterFunc),
                RGaGradedMultivector<T> mv1 => mv1.GetPart(filterFunc),
                RGaUniformMultivector<T> mv1 => mv1.GetPart(filterFunc),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivector<T> GetPart<T>(this RGaMultivector<T> mv, Func<T, bool> filterFunc)
        {
            return mv switch
            {
                RGaScalar<T> s => s.GetPart(filterFunc),
                RGaVector<T> v => v.GetPart(filterFunc),
                RGaBivector<T> bv => bv.GetPart(filterFunc),
                RGaHigherKVector<T> kv => kv.GetPart(filterFunc),
                RGaGradedMultivector<T> mv1 => mv1.GetPart(filterFunc),
                RGaUniformMultivector<T> mv1 => mv1.GetPart(filterFunc),
                _ => throw new InvalidOperationException()
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivector<T> GetPart<T>(this RGaMultivector<T> mv, Func<ulong, T, bool> filterFunc)
        {
            return mv switch
            {
                RGaScalar<T> s => s.GetPart(filterFunc),
                RGaVector<T> v => v.GetPart(filterFunc),
                RGaBivector<T> bv => bv.GetPart(filterFunc),
                RGaHigherKVector<T> kv => kv.GetPart(filterFunc),
                RGaGradedMultivector<T> mv1 => mv1.GetPart(filterFunc),
                RGaUniformMultivector<T> mv1 => mv1.GetPart(filterFunc),
                _ => throw new InvalidOperationException()
            };
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<RGaScalar<T>, RGaBivector<T>> GetScalarBivectorParts<T>(this RGaMultivector<T> mv)
        {
            return new Tuple<RGaScalar<T>, RGaBivector<T>>(
                mv.GetScalarPart(),
                mv.GetBivectorPart()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<RGaMultivector<T>, RGaMultivector<T>> GetEvenOddParts<T>(this RGaMultivector<T> mv)
        {
            return new Tuple<RGaMultivector<T>, RGaMultivector<T>>(
                mv.GetEvenPart(),
                mv.GetOddPart()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<RGaMultivector<T>, RGaMultivector<T>> GetEvenOddParts<T>(this RGaMultivector<T> mv, int maxGrade)
        {
            return new Tuple<RGaMultivector<T>, RGaMultivector<T>>(
                mv.GetEvenPart(maxGrade),
                mv.GetOddPart(maxGrade)
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivector<T> MapScalars<T>(this RGaMultivector<T> mv, Func<T, T> scalarMapping)
        {
            return mv switch
            {
                RGaScalar<T> s => s.MapScalar(scalarMapping),
                RGaVector<T> v => v.MapScalars(scalarMapping),
                RGaBivector<T> bv => bv.MapScalars(scalarMapping),
                RGaHigherKVector<T> kv => kv.MapScalars(scalarMapping),
                RGaGradedMultivector<T> mv1 => mv1.MapScalars(scalarMapping),
                RGaUniformMultivector<T> mv1 => mv1.MapScalars(scalarMapping),
                _ => throw new InvalidOperationException()
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Multivector MapScalars<T>(this RGaMultivector<T> mv, RGaFloat64Processor processor, Func<T, double> scalarMapping)
        {
            return mv switch
            {
                RGaScalar<T> s => s.MapScalar(processor, scalarMapping),
                RGaVector<T> v => v.MapScalars(processor, scalarMapping),
                RGaBivector<T> bv => bv.MapScalars(processor, scalarMapping),
                RGaHigherKVector<T> kv => kv.MapScalars(processor, scalarMapping),
                RGaGradedMultivector<T> mv1 => mv1.MapScalars(processor, scalarMapping),
                RGaUniformMultivector<T> mv1 => mv1.MapScalars(processor, scalarMapping),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivector<T2> MapScalars<T1, T2>(this RGaMultivector<T1> mv, RGaProcessor<T2> processor, Func<T1, T2> scalarMapping)
        {
            return mv switch
            {
                RGaScalar<T1> s => s.MapScalar(processor, scalarMapping),
                RGaVector<T1> v => v.MapScalars(processor, scalarMapping),
                RGaBivector<T1> bv => bv.MapScalars(processor, scalarMapping),
                RGaHigherKVector<T1> kv => kv.MapScalars(processor, scalarMapping),
                RGaGradedMultivector<T1> mv1 => mv1.MapScalars(processor, scalarMapping),
                RGaUniformMultivector<T1> mv1 => mv1.MapScalars(processor, scalarMapping),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivector<T> MapScalars<T>(this RGaMultivector<T> mv, Func<ulong, T, T> scalarMapping)
        {
            return mv switch
            {
                RGaScalar<T> s => s.MapScalar(scalarMapping),
                RGaVector<T> v => v.MapScalars(scalarMapping),
                RGaBivector<T> bv => bv.MapScalars(scalarMapping),
                RGaHigherKVector<T> kv => kv.MapScalars(scalarMapping),
                RGaGradedMultivector<T> mv1 => mv1.MapScalars(scalarMapping),
                RGaUniformMultivector<T> mv1 => mv1.MapScalars(scalarMapping),
                _ => throw new InvalidOperationException()
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Multivector MapScalars<T>(this RGaMultivector<T> mv, RGaFloat64Processor processor, Func<ulong, T, double> scalarMapping)
        {
            return mv switch
            {
                RGaScalar<T> s => s.MapScalar(processor, scalarMapping),
                RGaVector<T> v => v.MapScalars(processor, scalarMapping),
                RGaBivector<T> bv => bv.MapScalars(processor, scalarMapping),
                RGaHigherKVector<T> kv => kv.MapScalars(processor, scalarMapping),
                RGaGradedMultivector<T> mv1 => mv1.MapScalars(processor, scalarMapping),
                RGaUniformMultivector<T> mv1 => mv1.MapScalars(processor, scalarMapping),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivector<T2> MapScalars<T1, T2>(this RGaMultivector<T1> mv, RGaProcessor<T2> processor, Func<ulong, T1, T2> scalarMapping)
        {
            return mv switch
            {
                RGaScalar<T1> s => s.MapScalar(processor, scalarMapping),
                RGaVector<T1> v => v.MapScalars(processor, scalarMapping),
                RGaBivector<T1> bv => bv.MapScalars(processor, scalarMapping),
                RGaHigherKVector<T1> kv => kv.MapScalars(processor, scalarMapping),
                RGaGradedMultivector<T1> mv1 => mv1.MapScalars(processor, scalarMapping),
                RGaUniformMultivector<T1> mv1 => mv1.MapScalars(processor, scalarMapping),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivector<T> MapBasisBlades<T>(this RGaMultivector<T> mv, Func<ulong, ulong> basisMapping)
        {
            var termList =
                mv.IdScalarPairs.Select(
                    term => new KeyValuePair<ulong, T>(
                        basisMapping(term.Key),
                        term.Value
                    )
                );

            return mv.Processor
                .CreateComposer()
                .AddTerms(termList)
                .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivector<T> MapBasisBlades<T>(this RGaMultivector<T> mv, Func<ulong, T, ulong> basisMapping)
        {
            var termList =
                mv.IdScalarPairs.Select(
                    term => new KeyValuePair<ulong, T>(
                        basisMapping(term.Key, term.Value),
                        term.Value
                    )
                );

            return mv.Processor
                .CreateComposer()
                .AddTerms(termList)
                .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivector<T> MapTerms<T>(this RGaMultivector<T> mv, Func<ulong, T, KeyValuePair<ulong, T>> termMapping)
        {
            var termList =
                mv.IdScalarPairs.Select(
                    term =>
                        termMapping(term.Key, term.Value)
                );

            return mv.Processor
                .CreateComposer()
                .AddTerms(termList)
                .GetSimpleMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivector<T> Negative<T>(this RGaMultivector<T> mv)
        {
            return mv switch
            {
                RGaScalar<T> mv1 => mv1.Negative(),
                RGaVector<T> mv1 => mv1.Negative(),
                RGaBivector<T> mv1 => mv1.Negative(),
                RGaHigherKVector<T> mv1 => mv1.Negative(),
                RGaGradedMultivector<T> mv1 => mv1.Negative(),
                RGaUniformMultivector<T> mv1 => mv1.Negative(),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivector<T> Reverse<T>(this RGaMultivector<T> mv)
        {
            return mv switch
            {
                RGaScalar<T> => mv,
                RGaVector<T> => mv,
                RGaBivector<T> mv1 => mv1.Negative(),
                RGaHigherKVector<T> mv1 => mv1.Grade.ReverseIsNegativeOfGrade() ? mv1.Negative() : mv1,
                RGaGradedMultivector<T> mv1 => mv1.Reverse(),
                RGaUniformMultivector<T> mv1 => mv1.Reverse(),
                _ => throw new InvalidOperationException()
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivector<T> GradeInvolution<T>(this RGaMultivector<T> mv)
        {
            return mv switch
            {
                RGaScalar<T> => mv,
                RGaVector<T> mv1 => mv1.Negative(),
                RGaBivector<T> mv1 => mv1,
                RGaHigherKVector<T> mv1 => mv1.Grade.GradeInvolutionIsNegativeOfGrade() ? mv1.Negative() : mv1,
                RGaGradedMultivector<T> mv1 => mv1.GradeInvolution(),
                RGaUniformMultivector<T> mv1 => mv1.GradeInvolution(),
                _ => throw new InvalidOperationException()
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivector<T> CliffordConjugate<T>(this RGaMultivector<T> mv)
        {
            return mv switch
            {
                RGaScalar<T> => mv,
                RGaVector<T> mv1 => mv1.Negative(),
                RGaBivector<T> mv1 => mv1.Negative(),
                RGaHigherKVector<T> mv1 => mv1.Grade.CliffordConjugateIsNegativeOfGrade() ? mv1.Negative() : mv1,
                RGaGradedMultivector<T> mv1 => mv1.CliffordConjugate(),
                RGaUniformMultivector<T> mv1 => mv1.CliffordConjugate(),
                _ => throw new InvalidOperationException()
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivector<T> Conjugate<T>(this RGaMultivector<T> mv)
        {
            return mv switch
            {
                RGaScalar<T> => mv,
                RGaVector<T> mv1 => mv1.Conjugate(),
                RGaBivector<T> mv1 => mv1.Conjugate(),
                RGaHigherKVector<T> mv1 => mv1.Conjugate(),
                RGaGradedMultivector<T> mv1 => mv1.Conjugate(),
                RGaUniformMultivector<T> mv1 => mv1.Conjugate(),
                _ => throw new InvalidOperationException()
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivector<T> Times<T>(this RGaMultivector<T> mv, T scalarValue)
        {
            return mv switch
            {
                RGaScalar<T> mv1 => mv1.Times(scalarValue),
                RGaVector<T> mv1 => mv1.Times(scalarValue),
                RGaBivector<T> mv1 => mv1.Times(scalarValue),
                RGaHigherKVector<T> mv1 => mv1.Times(scalarValue),
                RGaGradedMultivector<T> mv1 => mv1.Times(scalarValue),
                RGaUniformMultivector<T> mv1 => mv1.Times(scalarValue),
                _ => throw new InvalidOperationException()
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivector<T> Divide<T>(this RGaMultivector<T> mv, T scalarValue)
        {
            return mv switch
            {
                RGaScalar<T> mv1 => mv1.Divide(scalarValue),
                RGaVector<T> mv1 => mv1.Divide(scalarValue),
                RGaBivector<T> mv1 => mv1.Divide(scalarValue),
                RGaHigherKVector<T> mv1 => mv1.Divide(scalarValue),
                RGaGradedMultivector<T> mv1 => mv1.Divide(scalarValue),
                RGaUniformMultivector<T> mv1 => mv1.Divide(scalarValue),
                _ => throw new InvalidOperationException()
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivector<T> DivideByENorm<T>(this RGaMultivector<T> mv)
        {
            return mv switch
            {
                RGaScalar<T> mv1 => mv1.DivideByENorm(),
                RGaVector<T> mv1 => mv1.DivideByENorm(),
                RGaBivector<T> mv1 => mv1.DivideByENorm(),
                RGaHigherKVector<T> mv1 => mv1.DivideByENorm(),
                RGaGradedMultivector<T> mv1 => mv1.DivideByENorm(),
                RGaUniformMultivector<T> mv1 => mv1.DivideByENorm(),
                _ => throw new InvalidOperationException()
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivector<T> DivideByENormSquared<T>(this RGaMultivector<T> mv)
        {
            return mv switch
            {
                RGaScalar<T> mv1 => mv1.DivideByENormSquared(),
                RGaVector<T> mv1 => mv1.DivideByENormSquared(),
                RGaBivector<T> mv1 => mv1.DivideByENormSquared(),
                RGaHigherKVector<T> mv1 => mv1.DivideByENormSquared(),
                RGaGradedMultivector<T> mv1 => mv1.DivideByENormSquared(),
                RGaUniformMultivector<T> mv1 => mv1.DivideByENormSquared(),
                _ => throw new InvalidOperationException()
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivector<T> DivideByNorm<T>(this RGaMultivector<T> mv)
        {
            return mv switch
            {
                RGaScalar<T> mv1 => mv1.DivideByNorm(),
                RGaVector<T> mv1 => mv1.DivideByNorm(),
                RGaBivector<T> mv1 => mv1.DivideByNorm(),
                RGaHigherKVector<T> mv1 => mv1.DivideByNorm(),
                RGaGradedMultivector<T> mv1 => mv1.DivideByNorm(),
                RGaUniformMultivector<T> mv1 => mv1.DivideByNorm(),
                _ => throw new InvalidOperationException()
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivector<T> DivideByNormSquared<T>(this RGaMultivector<T> mv)
        {
            return mv switch
            {
                RGaScalar<T> mv1 => mv1.DivideByNormSquared(),
                RGaVector<T> mv1 => mv1.DivideByNormSquared(),
                RGaBivector<T> mv1 => mv1.DivideByNormSquared(),
                RGaHigherKVector<T> mv1 => mv1.DivideByNormSquared(),
                RGaGradedMultivector<T> mv1 => mv1.DivideByNormSquared(),
                RGaUniformMultivector<T> mv1 => mv1.DivideByNormSquared(),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivector<T> EInverse<T>(this RGaMultivector<T> mv)
        {
            return mv switch
            {
                RGaScalar<T> mv1 => mv1.EInverse(),
                RGaVector<T> mv1 => mv1.EInverse(),
                RGaBivector<T> mv1 => mv1.EInverse(),
                RGaHigherKVector<T> mv1 => mv1.EInverse(),
                RGaGradedMultivector<T> mv1 => mv1.EInverse(),
                RGaUniformMultivector<T> mv1 => mv1.EInverse(),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivector<T> Inverse<T>(this RGaMultivector<T> mv)
        {
            return mv switch
            {
                RGaScalar<T> mv1 => mv1.Inverse(),
                RGaVector<T> mv1 => mv1.Inverse(),
                RGaBivector<T> mv1 => mv1.Inverse(),
                RGaHigherKVector<T> mv1 => mv1.Inverse(),
                RGaGradedMultivector<T> mv1 => mv1.Inverse(),
                RGaUniformMultivector<T> mv1 => mv1.Inverse(),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivector<T> EDual<T>(this RGaMultivector<T> mv, int vSpaceDimensions)
        {
            return mv switch
            {
                RGaScalar<T> mv1 => mv1.EDual(vSpaceDimensions),
                RGaVector<T> mv1 => mv1.EDual(vSpaceDimensions),
                RGaBivector<T> mv1 => mv1.EDual(vSpaceDimensions),
                RGaHigherKVector<T> mv1 => mv1.EDual(vSpaceDimensions),
                RGaGradedMultivector<T> mv1 => mv1.EDual(vSpaceDimensions),
                RGaUniformMultivector<T> mv1 => mv1.EDual(vSpaceDimensions),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivector<T> EDual<T>(this RGaMultivector<T> mv, RGaKVector<T> blade)
        {
            return mv switch
            {
                RGaScalar<T> mv1 => mv1.EDual(blade),
                RGaVector<T> mv1 => mv1.EDual(blade),
                RGaBivector<T> mv1 => mv1.EDual(blade),
                RGaHigherKVector<T> mv1 => mv1.EDual(blade),
                RGaGradedMultivector<T> mv1 => mv1.EDual(blade),
                RGaUniformMultivector<T> mv1 => mv1.EDual(blade),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivector<T> Dual<T>(this RGaMultivector<T> mv, int vSpaceDimensions)
        {
            return mv switch
            {
                RGaScalar<T> mv1 => mv1.Dual(vSpaceDimensions),
                RGaVector<T> mv1 => mv1.Dual(vSpaceDimensions),
                RGaBivector<T> mv1 => mv1.Dual(vSpaceDimensions),
                RGaHigherKVector<T> mv1 => mv1.Dual(vSpaceDimensions),
                RGaGradedMultivector<T> mv1 => mv1.Dual(vSpaceDimensions),
                RGaUniformMultivector<T> mv1 => mv1.Dual(vSpaceDimensions),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivector<T> Dual<T>(this RGaMultivector<T> mv, RGaKVector<T> blade)
        {
            return mv switch
            {
                RGaScalar<T> mv1 => mv1.Dual(blade),
                RGaVector<T> mv1 => mv1.Dual(blade),
                RGaBivector<T> mv1 => mv1.Dual(blade),
                RGaHigherKVector<T> mv1 => mv1.Dual(blade),
                RGaGradedMultivector<T> mv1 => mv1.Dual(blade),
                RGaUniformMultivector<T> mv1 => mv1.Dual(blade),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivector<T> EUnDual<T>(this RGaMultivector<T> mv, int vSpaceDimensions)
        {
            return mv switch
            {
                RGaScalar<T> mv1 => mv1.EUnDual(vSpaceDimensions),
                RGaVector<T> mv1 => mv1.EUnDual(vSpaceDimensions),
                RGaBivector<T> mv1 => mv1.EUnDual(vSpaceDimensions),
                RGaHigherKVector<T> mv1 => mv1.EUnDual(vSpaceDimensions),
                RGaGradedMultivector<T> mv1 => mv1.EUnDual(vSpaceDimensions),
                RGaUniformMultivector<T> mv1 => mv1.EUnDual(vSpaceDimensions),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivector<T> EUnDual<T>(this RGaMultivector<T> mv, RGaKVector<T> blade)
        {
            return mv switch
            {
                RGaScalar<T> mv1 => mv1.EUnDual(blade),
                RGaVector<T> mv1 => mv1.EUnDual(blade),
                RGaBivector<T> mv1 => mv1.EUnDual(blade),
                RGaHigherKVector<T> mv1 => mv1.EUnDual(blade),
                RGaGradedMultivector<T> mv1 => mv1.EUnDual(blade),
                RGaUniformMultivector<T> mv1 => mv1.EUnDual(blade),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivector<T> UnDual<T>(this RGaMultivector<T> mv, int vSpaceDimensions)
        {
            return mv switch
            {
                RGaScalar<T> mv1 => mv1.UnDual(vSpaceDimensions),
                RGaVector<T> mv1 => mv1.UnDual(vSpaceDimensions),
                RGaBivector<T> mv1 => mv1.UnDual(vSpaceDimensions),
                RGaHigherKVector<T> mv1 => mv1.UnDual(vSpaceDimensions),
                RGaGradedMultivector<T> mv1 => mv1.UnDual(vSpaceDimensions),
                RGaUniformMultivector<T> mv1 => mv1.UnDual(vSpaceDimensions),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivector<T> UnDual<T>(this RGaMultivector<T> mv, RGaKVector<T> blade)
        {
            return mv switch
            {
                RGaScalar<T> mv1 => mv1.UnDual(blade),
                RGaVector<T> mv1 => mv1.UnDual(blade),
                RGaBivector<T> mv1 => mv1.UnDual(blade),
                RGaHigherKVector<T> mv1 => mv1.UnDual(blade),
                RGaGradedMultivector<T> mv1 => mv1.UnDual(blade),
                RGaUniformMultivector<T> mv1 => mv1.UnDual(blade),
                _ => throw new InvalidOperationException()
            };
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivector<T> Op<T>(this IEnumerable<RGaMultivector<T>> mvList)
        {
            return mvList.Skip(1).Aggregate(
                mvList.First(),
                (current, mv) => current.Op(mv)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivector<T> EGp<T>(this IEnumerable<RGaMultivector<T>> mvList)
        {
            return mvList.Skip(1).Aggregate(
                mvList.First(),
                (current, mv) => current.EGp(mv)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaMultivector<T> Gp<T>(this IEnumerable<RGaMultivector<T>> mvList)
        {
            return mvList.Skip(1).Aggregate(
                mvList.First(),
                (current, mv) => current.Gp(mv)
            );
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector2D GetVectorPartAsTuple2D(this RGaMultivector<double> mv)
        {
            return Float64Vector2D.Create((Float64Scalar)mv.GetTermScalar(1).ScalarValue,
                (Float64Scalar)mv.GetTermScalar(2).ScalarValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D GetVectorPartAsTuple3D(this RGaMultivector<double> mv)
        {
            return Float64Vector3D.Create(mv.GetTermScalar(1).ScalarValue,
                mv.GetTermScalar(2).ScalarValue,
                mv.GetTermScalar(4).ScalarValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector4D GetVectorPartAsTuple4D(this RGaMultivector<double> mv)
        {
            return Float64Vector4D.Create(mv.GetTermScalar(1).ScalarValue,
                mv.GetTermScalar(2).ScalarValue,
                mv.GetTermScalar(4).ScalarValue,
                mv.GetTermScalar(8).ScalarValue);
        }

        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] MultivectorToArray1D<T>(this RGaMultivector<T> mv, int arraySize)
        {
            var vSpaceDimensions = mv.VSpaceDimensions;

            if (vSpaceDimensions > 31)
                throw new InvalidOperationException();

            var gaSpaceDimensions = 1UL << vSpaceDimensions;

            if ((ulong) arraySize < gaSpaceDimensions)
                throw new InvalidOperationException();

            var array = mv
                .ScalarProcessor
                .CreateArrayZero1D(arraySize);

            foreach (var (id, scalar) in mv.IdScalarPairs)
                array[id] = scalar;

            return array;
        }
        
        public static T[,] ScalarPlusBivectorToArray2D<T>(this RGaMultivector<T> mv)
        {
            var array = mv.GetBivectorPart().BivectorToArray2D();
            var scalar = mv.GetScalarTermScalar().ScalarValue;
            var metric = mv.Metric;
            var scalarProcessor = mv.ScalarProcessor;

            var arraySize = array.GetLength(0);
            for (var i = 0; i < arraySize; i++)
            {
                var signature = metric.Signature(i);

                if (signature.IsZero) continue;

                array[i, i] = signature.IsPositive
                    ? scalar
                    : scalarProcessor.Negative(scalar);
            }
        
            return array;
        }

        public static T[,] ScalarPlusBivectorToArray2D<T>(this RGaMultivector<T> mv, int arraySize)
        {
            var array = mv.GetBivectorPart().BivectorToArray2D(arraySize);
            var scalar = mv.GetScalarTermScalar().ScalarValue;
            var metric = mv.Metric;
            var scalarProcessor = mv.ScalarProcessor;

            for (var i = 0; i < arraySize; i++)
            {
                var signature = metric.Signature(i);

                if (signature.IsZero) continue;

                array[i, i] = signature.IsPositive
                    ? scalar
                    : scalarProcessor.Negative(scalar);
            }
        
            return array;
        }
    }
}
