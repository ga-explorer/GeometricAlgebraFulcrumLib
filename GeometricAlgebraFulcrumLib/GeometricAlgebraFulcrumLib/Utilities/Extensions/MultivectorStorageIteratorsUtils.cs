using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.GuidedBinaryTraversal.Outermorphisms;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.GuidedBinaryTraversal.Products;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class MultivectorStorageIteratorsUtils
    {
        public static IEnumerable<GradeLinVectorStorageScalarRecord<T>> GetGbtOutermorphismScaledKVectors<T>(this Multivector<T> mv, IReadOnlyList<VectorStorage<T>> basisVectorMappings)
        {
            return GeoGbtMultivectorOutermorphismStack<T>
                .Create(basisVectorMappings, mv)
                .TraverseForScaledKVectors();
        }

        public static IEnumerable<IndexScalarRecord<T>> GetGbtOutermorphismIdScalarRecords<T>(this Multivector<T> mv, IReadOnlyList<VectorStorage<T>> basisVectorMappings)
        {
            var scalarProcessor = mv.GeometricProcessor;
            var factorsList = 
                mv.GetGbtOutermorphismScaledKVectors(basisVectorMappings);

            foreach (var (grade, indexScalarList, scalingFactor) in factorsList)
            {
                if (scalarProcessor.IsZero(scalingFactor))
                    continue;

                var indexScalarRecords = 
                    indexScalarList
                        .FilterByScalar(scalarProcessor.IsNotZero)
                        .GetIndexScalarRecords()
                        .Select(
                            indexScalarRecord => new IndexScalarRecord<T>(
                                indexScalarRecord.Index.BasisBladeIndexToId(grade),
                                scalarProcessor.Times(scalingFactor, indexScalarRecord.Scalar)
                            )
                        );

                foreach (var idScalarRecord in indexScalarRecords)
                    yield return idScalarRecord;
            }
        }


        #region Euclidean bilinear products on multivectors using Guided Binary Traversal
        /// <summary>
        /// Compute the Outer Product terms of two multivectors using Guided Binary Traversal
        /// </summary>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<IndexScalarRecord<T>> GetGbtOpIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2)
        {
            var stack = GeoGbtProductsStack2<T>.Create(mv1, mv2);

            return stack.GetOpIdScalarRecords();
        }

        /// <summary>
        /// Compute the Geometric Product terms of two multivectors on the given basisSet using Guided Binary Traversal
        /// </summary>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<IndexScalarRecord<T>> GetGbtEGpIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2)
        {
            var stack = GeoGbtProductsStack2<T>.Create(mv1, mv2);

            return stack.GetEGpIdScalarRecords();
        }

        /// <summary>
        /// Compute the Left Contraction Product terms of two multivectors on the given basisSet using Guided Binary Traversal
        /// </summary>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<IndexScalarRecord<T>> GetGbtELcpIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2)
        {
            var stack = GeoGbtProductsStack2<T>.Create(mv1, mv2);

            return stack.GetELcpIdScalarRecords();
        }

        /// <summary>
        /// Compute the Right Contraction Product terms of two multivectors on the given basisSet using Guided Binary Traversal
        /// </summary>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<IndexScalarRecord<T>> GetGbtERcpIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2)
        {
            var stack = GeoGbtProductsStack2<T>.Create(mv1, mv2);

            return stack.GetERcpIdScalarRecords();
        }

        /// <summary>
        /// Compute the Scalar Product terms of two multivectors on the given basisSet using Guided Binary Traversal
        /// </summary>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<IndexScalarRecord<T>> GetGbtESpIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2)
        {
            var stack = GeoGbtProductsStack2<T>.Create(mv1, mv2);

            return stack.GetESpIdScalarRecords();
        }

        /// <summary>
        /// Compute the Fat-Dot Product terms of two multivectors on the given basisSet using Guided Binary Traversal
        /// </summary>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<IndexScalarRecord<T>> GetGbtEFdpIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2)
        {
            var stack = GeoGbtProductsStack2<T>.Create(mv1, mv2);

            return stack.GetEFdpIdScalarRecords();
        }

        /// <summary>
        /// Compute the Hestenes Inner Product terms of two multivectors on the given basisSet using Guided Binary Traversal
        /// </summary>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<IndexScalarRecord<T>> GetGbtEHipIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2)
        {
            var stack = GeoGbtProductsStack2<T>.Create(mv1, mv2);

            return stack.GetEHipIdScalarRecords();
        }

        /// <summary>
        /// Compute the Anti-Commutator Product terms of two multivectors on the given basisSet using Guided Binary Traversal
        /// </summary>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<IndexScalarRecord<T>> GetGbtEAcpIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2)
        {
            var stack = GeoGbtProductsStack2<T>.Create(mv1, mv2);

            return stack.GetEAcpIdScalarRecords();
        }

        /// <summary>
        /// Compute the Commutator Product terms of two multivectors on the Euclidean basisSet using Guided Binary Traversal
        /// </summary>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<IndexScalarRecord<T>> GetGbtECpIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2)
        {
            var stack = GeoGbtProductsStack2<T>.Create(mv1, mv2);

            return stack.GetECpIdScalarRecords();
        }

        public static IEnumerable<IndexScalarRecord<T>> GetGbtENorm2IdScalarPairs<T>(this Multivector<T> mv)
        {
            var stack = GeoGbtProductsStack2<T>.Create(mv, mv.Reverse());

            return stack.GetESpIdScalarRecords();
        }

        public static T ENorm2<T>(this Multivector<T> mv)
        {
            return mv.GeometricProcessor.Add(
                mv.GetGbtENorm2IdScalarPairs().Select(t => t.Scalar)
            );
        }

        public static T ENorm<T>(this Multivector<T> mv)
        {
            var scalarProcessor = mv.GeometricProcessor;

            return scalarProcessor.Sqrt(
                scalarProcessor.Add(
                    mv.GetGbtENorm2IdScalarPairs().Select(t => t.Scalar)
                )
            );
        }

        public static IEnumerable<BasisTerm<T>> GetGptEInverseIdScalarPairs<T>(this Multivector<T> mv)
        {
            var mvReverse = mv.Reverse();

            var stack = GeoGbtProductsStack2<T>.Create(mv, mvReverse);

            var scalarProcessor = mv.GeometricProcessor;

            var norm2 = scalarProcessor.Add(
                stack
                    .GetESpIdScalarRecords()
                    .Select(t => t.Scalar)
                );

            return (mvReverse /  norm2).MultivectorStorage.GetTerms();
        }
        #endregion


        #region Orthogonal basisSet bilinear products on multivectors using Guided Binary Traversal
        /// <summary>
        /// Compute the Geometric Product terms of two multivectors on the given basisSet using Guided Binary Traversal
        /// </summary>
        /// <param name="basisSet"></param>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<IndexScalarRecord<T>> GetGbtGpIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2, GeometricAlgebraBasisSet basisSet)
        {
            var stack = GeoGbtProductsStack2<T>.Create(mv1, mv2);

            return stack.GetGpIdScalarRecords(basisSet);
        }

        /// <summary>
        /// Compute the Left Contraction Product terms of two multivectors on the given basisSet using Guided Binary Traversal
        /// </summary>
        /// <param name="basisSet"></param>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<IndexScalarRecord<T>> GetGbtLcpIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2, GeometricAlgebraBasisSet basisSet)
        {
            var stack = GeoGbtProductsStack2<T>.Create(mv1, mv2);

            return stack.GetLcpIdScalarRecords(basisSet);
        }

        /// <summary>
        /// Compute the Right Contraction Product terms of two multivectors on the given basisSet using Guided Binary Traversal
        /// </summary>
        /// <param name="basisSet"></param>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<IndexScalarRecord<T>> GetGbtRcpIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2, GeometricAlgebraBasisSet basisSet)
        {
            var stack = GeoGbtProductsStack2<T>.Create(mv1, mv2);

            return stack.GetRcpIdScalarRecords(basisSet);
        }

        /// <summary>
        /// Compute the Scalar Product terms of two multivectors on the given basisSet using Guided Binary Traversal
        /// </summary>
        /// <param name="basisSet"></param>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<IndexScalarRecord<T>> GetGbtSpIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2, GeometricAlgebraBasisSet basisSet)
        {
            var stack = GeoGbtProductsStack2<T>.Create(mv1, mv2);

            return stack.GetSpIdScalarRecords(basisSet);
        }

        /// <summary>
        /// Compute the Fat-Dot Product terms of two multivectors on the given basisSet using Guided Binary Traversal
        /// </summary>
        /// <param name="basisSet"></param>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<IndexScalarRecord<T>> GetGbtFdpIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2, GeometricAlgebraBasisSet basisSet)
        {
            var stack = GeoGbtProductsStack2<T>.Create(mv1, mv2);

            return stack.GetFdpIdScalarRecords(basisSet);
        }

        /// <summary>
        /// Compute the Hestenes Inner Product terms of two multivectors on the given basisSet using Guided Binary Traversal
        /// </summary>
        /// <param name="basisSet"></param>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<IndexScalarRecord<T>> GetGbtHipIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2, GeometricAlgebraBasisSet basisSet)
        {
            var stack = GeoGbtProductsStack2<T>.Create(mv1, mv2);

            return stack.GetHipIdScalarRecords(basisSet);
        }

        /// <summary>
        /// Compute the Anti-Commutator Product terms of two multivectors on the given basisSet using Guided Binary Traversal
        /// </summary>
        /// <param name="basisSet"></param>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<IndexScalarRecord<T>> GetGbtAcpIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2, GeometricAlgebraBasisSet basisSet)
        {
            var stack = GeoGbtProductsStack2<T>.Create(mv1, mv2);

            return stack.GetAcpIdScalarRecords(basisSet);
        }

        /// <summary>
        /// Compute the Commutator Product terms of two multivectors on the given basisSet using Guided Binary Traversal
        /// </summary>
        /// <param name="basisSet"></param>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<IndexScalarRecord<T>> GetGbtCpIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2, GeometricAlgebraBasisSet basisSet)
        {
            var stack = GeoGbtProductsStack2<T>.Create(mv1, mv2);

            return stack.GetCpIdScalarRecords(basisSet);
        }

        public static IEnumerable<IndexScalarRecord<T>> GetGbtNorm2IdScalarPairs<T>(this Multivector<T> mv, GeometricAlgebraBasisSet basisSet)
        {
            var stack = GeoGbtProductsStack2<T>.Create(mv, mv.Reverse());

            return stack.GetSpIdScalarRecords(basisSet);
        }

        public static T Norm2<T>(this Multivector<T> mv, GeometricAlgebraBasisSet basisSet)
        {
            return mv.GeometricProcessor.Add(
                mv.GetGbtNorm2IdScalarPairs(basisSet).Select(t => t.Scalar)
            );
        }

        public static T Norm<T>(this Multivector<T> mv, GeometricAlgebraBasisSet basisSet)
        {
            var scalarProcessor = mv.GeometricProcessor;

            return scalarProcessor.Sqrt(scalarProcessor.Add(
                mv.GetGbtNorm2IdScalarPairs(basisSet).Select(t => t.Scalar)
            ));
        }

        public static IEnumerable<BasisTerm<T>> GetGptInverseIdScalarPairs<T>(this Multivector<T> mv, GeometricAlgebraBasisSet basisSet)
        {
            var mvReverse = mv.Reverse();

            var stack = GeoGbtProductsStack2<T>.Create(mv, mvReverse);

            var scalarProcessor = mv.GeometricProcessor;

            var norm2 = scalarProcessor.Add(
                stack
                    .GetSpIdScalarRecords(basisSet)
                    .Select(t => t.Scalar)
                );

            return (mvReverse / norm2).MultivectorStorage.GetTerms();
        }
        #endregion


        //#region Non-Orthogonal basisSet bilinear products on multivectors using Guided Binary Traversal
        ///// <summary>
        ///// Compute the Geometric Product terms of two multivectors on the given basisSet using Guided Binary Traversal
        ///// </summary>
        ///// <param name="basisSet"></param>
        ///// <param name="mv1"></param>
        ///// <param name="mv2"></param>
        ///// <returns></returns>
        //public static IEnumerable<GeoTerm<T>> GetGbtGpIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2, GeoOmChangeOfBasis<T> basisSet)
        //{
        //    var orthoMv1 =
        //        basisSet.ForwardOutermorphism.MapMultivector(mv1);

        //    var orthoMv2 =
        //        basisSet.ForwardOutermorphism.MapMultivector(mv2);

        //    var orthoMv =
        //        GetGbtGpIdScalarPairs(orthoMv1, orthoMv2, basisSet.BaseFrameSignature).SumAsMultivectorStorageUniform(mv1.ScalarProcessor);

        //    return basisSet.BackwardOutermorphism.MapMultivector(orthoMv).MultivectorStorage.GetNotZeroTerms();
        //}

        ///// <summary>
        ///// Compute the Left Contraction Product terms of two multivectors on the given basisSet using Guided Binary Traversal
        ///// </summary>
        ///// <param name="basisSet"></param>
        ///// <param name="mv1"></param>
        ///// <param name="mv2"></param>
        ///// <returns></returns>
        //public static IEnumerable<GeoTerm<T>> GetGbtLcpIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2, GeoOmChangeOfBasis<T> basisSet)
        //{
        //    var orthoMv1 =
        //        basisSet.ForwardOutermorphism.MapMultivector(mv1);

        //    var orthoMv2 =
        //        basisSet.ForwardOutermorphism.MapMultivector(mv2);

        //    var orthoMv =
        //        GetGbtLcpTerms(orthoMv1, orthoMv2, basisSet.BaseFrameSignature).SumAsMultivectorStorageUniform(mv1.ScalarDomain);

        //    return basisSet.BackwardOutermorphism.MapMultivector(orthoMv).GetNotZeroTerms();
        //}

        ///// <summary>
        ///// Compute the Right Contraction Product terms of two multivectors on the given basisSet using Guided Binary Traversal
        ///// </summary>
        ///// <param name="basisSet"></param>
        ///// <param name="mv1"></param>
        ///// <param name="mv2"></param>
        ///// <returns></returns>
        //public static IEnumerable<GeoTerm<T>> GetGbtRcpIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2, GeoOmChangeOfBasis<T> basisSet)
        //{
        //    var orthoMv1 =
        //        basisSet.ForwardOutermorphism.MapMultivector(mv1);

        //    var orthoMv2 =
        //        basisSet.ForwardOutermorphism.MapMultivector(mv2);

        //    var orthoMv =
        //        GetGbtRcpTerms(orthoMv1, orthoMv2, basisSet.BaseFrameSignature).SumAsMultivectorStorageUniform(mv1.ScalarDomain);

        //    return basisSet.BackwardOutermorphism.MapMultivector(orthoMv).GetNotZeroTerms();
        //}

        ///// <summary>
        ///// Compute the Scalar Product terms of two multivectors on the given basisSet using Guided Binary Traversal
        ///// </summary>
        ///// <param name="basisSet"></param>
        ///// <param name="mv1"></param>
        ///// <param name="mv2"></param>
        ///// <returns></returns>
        //public static IEnumerable<GeoTerm<T>> GetGbtSpIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2, GeoOmChangeOfBasis<T> basisSet)
        //{
        //    var orthoMv1 =
        //        basisSet.ForwardOutermorphism.MapMultivector(mv1);

        //    var orthoMv2 =
        //        basisSet.ForwardOutermorphism.MapMultivector(mv2);

        //    var orthoMv =
        //        GetGbtSpTerms(orthoMv1, orthoMv2, basisSet.BaseFrameSignature).SumAsMultivectorStorageUniform(mv1.ScalarDomain);

        //    return basisSet.BackwardOutermorphism.MapMultivector(orthoMv).GetNotZeroTerms();
        //}

        ///// <summary>
        ///// Compute the Fat-Dot Product terms of two multivectors on the given basisSet using Guided Binary Traversal
        ///// </summary>
        ///// <param name="basisSet"></param>
        ///// <param name="mv1"></param>
        ///// <param name="mv2"></param>
        ///// <returns></returns>
        //public static IEnumerable<GeoTerm<T>> GetGbtFdpIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2, GeoOmChangeOfBasis<T> basisSet)
        //{
        //    var orthoMv1 =
        //        basisSet.ForwardOutermorphism.MapMultivector(mv1);

        //    var orthoMv2 =
        //        basisSet.ForwardOutermorphism.MapMultivector(mv2);

        //    var orthoMv =
        //        GetGbtFdpTerms(orthoMv1, orthoMv2, basisSet.BaseFrameSignature).SumAsMultivectorStorageUniform(mv1.ScalarDomain);

        //    return basisSet.BackwardOutermorphism.MapMultivector(orthoMv).GetNotZeroTerms();
        //}

        ///// <summary>
        ///// Compute the Hestenes Inner Product terms of two multivectors on the given basisSet using Guided Binary Traversal
        ///// </summary>
        ///// <param name="basisSet"></param>
        ///// <param name="mv1"></param>
        ///// <param name="mv2"></param>
        ///// <returns></returns>
        //public static IEnumerable<GeoTerm<T>> GetGbtHipIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2, GeoOmChangeOfBasis<T> basisSet)
        //{
        //    var orthoMv1 =
        //        basisSet.ForwardOutermorphism.MapMultivector(mv1);

        //    var orthoMv2 =
        //        basisSet.ForwardOutermorphism.MapMultivector(mv2);

        //    var orthoMv =
        //        GetGbtHipTerms(orthoMv1, orthoMv2, basisSet.BaseFrameSignature).SumAsMultivectorStorageUniform(mv1.ScalarDomain);

        //    return basisSet.BackwardOutermorphism.MapMultivector(orthoMv).GetNotZeroTerms();
        //}

        ///// <summary>
        ///// Compute the Anti-Commutator Product terms of two multivectors on the given basisSet using Guided Binary Traversal
        ///// </summary>
        ///// <param name="basisSet"></param>
        ///// <param name="mv1"></param>
        ///// <param name="mv2"></param>
        ///// <returns></returns>
        //public static IEnumerable<GeoTerm<T>> GetGbtAcpIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2, GeoOmChangeOfBasis<T> basisSet)
        //{
        //    var orthoMv1 =
        //        basisSet.ForwardOutermorphism.MapMultivector(mv1);

        //    var orthoMv2 =
        //        basisSet.ForwardOutermorphism.MapMultivector(mv2);

        //    var orthoMv =
        //        GetGbtAcpTerms(orthoMv1, orthoMv2, basisSet.BaseFrameSignature).SumAsMultivectorStorageUniform(mv1.ScalarDomain);

        //    return basisSet.BackwardOutermorphism.MapMultivector(orthoMv).GetNotZeroTerms();
        //}

        ///// <summary>
        ///// Compute the Commutator Product terms of two multivectors on the given basisSet using Guided Binary Traversal
        ///// </summary>
        ///// <param name="basisSet"></param>
        ///// <param name="mv1"></param>
        ///// <param name="mv2"></param>
        ///// <returns></returns>
        //public static IEnumerable<GeoTerm<T>> GetGbtCpIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2, GeoOmChangeOfBasis<T> basisSet)
        //{
        //    var orthoMv1 =
        //        basisSet.ForwardOutermorphism.MapMultivector(mv1);

        //    var orthoMv2 =
        //        basisSet.ForwardOutermorphism.MapMultivector(mv2);

        //    var orthoMv =
        //        GetGbtCpTerms(orthoMv1, orthoMv2, basisSet.BaseFrameSignature).SumAsMultivectorStorageUniform(mv1.ScalarDomain);

        //    return basisSet.BackwardOutermorphism.MapMultivector(orthoMv).GetNotZeroTerms();
        //}

        //public static IEnumerable<GeoTerm<T>> GetGbtNorm2IdScalarPairs<T>(this Multivector<T> mv, GeoOmChangeOfBasis<T> basisSet)
        //{
        //    var orthoMv =
        //        basisSet.ForwardOutermorphism.MapMultivector(mv);

        //    return GetGbtNorm2Terms(orthoMv, basisSet.BaseFrameSignature).SumAsMultivectorStorageUniform(mv.ScalarDomain);
        //}

        //public static T Norm2<T>(this Multivector<T> mv, GeoOmChangeOfBasis<T> basisSet)
        //{
        //    return mv.GetGbtNorm2Terms(basisSet).Select(t => t.Scalar).Sum();
        //}

        //public static T Norm<T>(this Multivector<T> mv, GeoOmChangeOfBasis<T> basisSet)
        //{
        //    return Math.Sqrt(
        //        mv.GetGbtNorm2Terms(basisSet).Select(t => t.Scalar).Sum()
        //    );
        //}

        //public static IEnumerable<GeoTerm<T>> GetGbtInverseIdScalarPairs<T>(this Multivector<T> mv, GeoOmChangeOfBasis<T> basisSet)
        //{
        //    var mvReverse = mv.GetReverse();

        //    var orthoMv =
        //        basisSet.ForwardOutermorphism.MapMultivector(mvReverse);

        //    var norm2 = 
        //        GetGbtNorm2Terms(orthoMv, basisSet.BaseFrameSignature)
        //            .Select(t => t.Scalar)
        //            .Sum();

        //    return mvReverse.GetScaledTerms(1 / norm2);
        //}
        //#endregion


        //public static IEnumerable<GeoTerm<T>> GetGbtGpIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2, IGeoNumMetric basisSet)
        //{
        //    if (basisSet is GeoOmChangeOfBasis<T> nonOrthogonalMetric)
        //        return mv1.GetGbtGpTerms(mv2, nonOrthogonalMetric);

        //    var orthogonalMetric = (GeoBasesSignature)basisSet;
        //    return mv1.GetGbtGpTerms(mv2, orthogonalMetric);
        //}

        //public static T GetGbtNorm2(this Multivector<T> mv, IGeoNumMetric basisSet)
        //{
        //    if (basisSet is GeoOmChangeOfBasis<T> nonOrthogonalMetric)
        //        return mv.GetGbtNorm2Terms(nonOrthogonalMetric).Select(t => t.Scalar).Sum();

        //    var orthogonalMetric = (GeoBasesSignature)basisSet;
        //    return mv.GetGbtNorm2Terms(orthogonalMetric).Select(t => t.Scalar).Sum();
        //}


        //#region Euclidean bilinear products on multivectors using Simple Looping
        //public static IEnumerable<GeoTerm<T>> GetLoopOpIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2)
        //{
        //    foreach (var term1 in mv1.GetNotZeroTerms())
        //    {
        //        foreach (var term2 in mv2.GetNotZeroTerms())
        //        {
        //            if (!GeoFrameUtils.IsNotZeroOp(term1.BasisBladeId, term2.BasisBladeId))
        //                continue;

        //            var value =
        //                term1.Scalar * term2.Scalar *
        //                (GeoFrameUtils.IsNegativeEGp(term1.BasisBladeId, term2.BasisBladeId) ? -1.0d : 1.0d);

        //            yield return new GeoTerm<T>(term1.BasisBladeId ^ term2.BasisBladeId, value);
        //        }
        //    }
        //}

        //public static IEnumerable<GeoTerm<T>> GetLoopEGpIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2)
        //{
        //    foreach (var term1 in mv1.GetNotZeroTerms())
        //    {
        //        foreach (var term2 in mv2.GetNotZeroTerms())
        //        {
        //            var value =
        //                term1.Scalar * term2.Scalar *
        //                (GeoFrameUtils.IsNegativeEGp(term1.BasisBladeId, term2.BasisBladeId) ? -1.0d : 1.0d);

        //            yield return new GeoTerm<T>(term1.BasisBladeId ^ term2.BasisBladeId, value);
        //        }
        //    }
        //}

        //public static IEnumerable<GeoTerm<T>> GetLoopELcpIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2)
        //{
        //    foreach (var term1 in mv1.GetNotZeroTerms())
        //    {
        //        foreach (var term2 in mv2.GetNotZeroTerms())
        //        {
        //            if (!GeoFrameUtils.IsNotZeroELcp(term1.BasisBladeId, term2.BasisBladeId))
        //                continue;

        //            var value =
        //                term1.Scalar * term2.Scalar *
        //                (GeoFrameUtils.IsNegativeEGp(term1.BasisBladeId, term2.BasisBladeId) ? -1.0d : 1.0d);

        //            yield return new GeoTerm<T>(term1.BasisBladeId ^ term2.BasisBladeId, value);
        //        }
        //    }
        //}

        //public static IEnumerable<GeoTerm<T>> GetLoopERcpIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2)
        //{
        //    foreach (var term1 in mv1.GetNotZeroTerms())
        //    {
        //        foreach (var term2 in mv2.GetNotZeroTerms())
        //        {
        //            if (!GeoFrameUtils.IsNotZeroERcp(term1.BasisBladeId, term2.BasisBladeId))
        //                continue;

        //            var value =
        //                term1.Scalar * term2.Scalar *
        //                (GeoFrameUtils.IsNegativeEGp(term1.BasisBladeId, term2.BasisBladeId) ? -1.0d : 1.0d);

        //            yield return new GeoTerm<T>(term1.BasisBladeId ^ term2.BasisBladeId, value);
        //        }
        //    }
        //}

        //public static IEnumerable<GeoTerm<T>> GetLoopESpIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2)
        //{
        //    foreach (var term1 in mv1.GetNotZeroTerms())
        //    {
        //        foreach (var term2 in mv2.GetNotZeroTerms())
        //        {
        //            if (!GeoFrameUtils.IsNotZeroESp(term1.BasisBladeId, term2.BasisBladeId))
        //                continue;

        //            var value =
        //                term1.Scalar * term2.Scalar *
        //                (GeoFrameUtils.IsNegativeEGp(term1.BasisBladeId, term2.BasisBladeId) ? -1.0d : 1.0d);

        //            yield return new GeoTerm<T>(term1.BasisBladeId ^ term2.BasisBladeId, value);
        //        }
        //    }
        //}

        //public static IEnumerable<GeoTerm<T>> GetLoopEFdpIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2)
        //{
        //    foreach (var term1 in mv1.GetNotZeroTerms())
        //    {
        //        foreach (var term2 in mv2.GetNotZeroTerms())
        //        {
        //            if (!GeoFrameUtils.IsNotZeroEFdp(term1.BasisBladeId, term2.BasisBladeId))
        //                continue;

        //            var value =
        //                term1.Scalar * term2.Scalar *
        //                (GeoFrameUtils.IsNegativeEGp(term1.BasisBladeId, term2.BasisBladeId) ? -1.0d : 1.0d);

        //            yield return new GeoTerm<T>(term1.BasisBladeId ^ term2.BasisBladeId, value);
        //        }
        //    }
        //}

        //public static IEnumerable<GeoTerm<T>> GetLoopEHipIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2)
        //{
        //    foreach (var term1 in mv1.GetNotZeroTerms())
        //    {
        //        foreach (var term2 in mv2.GetNotZeroTerms())
        //        {
        //            if (!GeoFrameUtils.IsNotZeroEHip(term1.BasisBladeId, term2.BasisBladeId))
        //                continue;

        //            var value =
        //                term1.Scalar * term2.Scalar *
        //                (GeoFrameUtils.IsNegativeEGp(term1.BasisBladeId, term2.BasisBladeId) ? -1.0d : 1.0d);

        //            yield return new GeoTerm<T>(term1.BasisBladeId ^ term2.BasisBladeId, value);
        //        }
        //    }
        //}

        //public static IEnumerable<GeoTerm<T>> GetLoopEAcpIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2)
        //{
        //    foreach (var term1 in mv1.GetNotZeroTerms())
        //    {
        //        foreach (var term2 in mv2.GetNotZeroTerms())
        //        {
        //            if (!GeoFrameUtils.IsNotZeroEAcp(term1.BasisBladeId, term2.BasisBladeId))
        //                continue;

        //            var value =
        //                term1.Scalar * term2.Scalar *
        //                (GeoFrameUtils.IsNegativeEGp(term1.BasisBladeId, term2.BasisBladeId) ? -1.0d : 1.0d);

        //            yield return new GeoTerm<T>(term1.BasisBladeId ^ term2.BasisBladeId, value);
        //        }
        //    }
        //}

        //public static IEnumerable<GeoTerm<T>> GetLoopECpIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2)
        //{
        //    foreach (var term1 in mv1.GetNotZeroTerms())
        //    {
        //        foreach (var term2 in mv2.GetNotZeroTerms())
        //        {
        //            if (!GeoFrameUtils.IsNotZeroECp(term1.BasisBladeId, term2.BasisBladeId))
        //                continue;

        //            var value =
        //                term1.Scalar * term2.Scalar *
        //                (GeoFrameUtils.IsNegativeEGp(term1.BasisBladeId, term2.BasisBladeId) ? -1.0d : 1.0d);

        //            yield return new GeoTerm<T>(term1.BasisBladeId ^ term2.BasisBladeId, value);
        //        }
        //    }
        //}
        //#endregion


        //#region Orthogonal basisSet bilinear products on multivectors using Simple Looping
        //public static IEnumerable<GeoTerm<T>> GetLoopGpIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2, GeoBasesSignature basisSet)
        //{
        //    foreach (var term1 in mv1.GetNotZeroTerms())
        //    {
        //        foreach (var term2 in mv2.GetNotZeroTerms())
        //        {
        //            yield return basisSet.ScaledGp(term1.BasisBladeId, term2.BasisBladeId, term1.Scalar * term2.Scalar); 
        //        }
        //    }
        //}

        //public static IEnumerable<GeoTerm<T>> GetLoopLcpIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2, GeoBasesSignature basisSet)
        //{
        //    foreach (var term1 in mv1.GetNotZeroTerms())
        //    {
        //        foreach (var term2 in mv2.GetNotZeroTerms())
        //        {
        //            if (!GeoFrameUtils.IsNotZeroELcp(term1.BasisBladeId, term2.BasisBladeId))
        //                continue;

        //            yield return basisSet.ScaledGp(term1.BasisBladeId, term2.BasisBladeId, term1.Scalar * term2.Scalar);
        //        }
        //    }
        //}

        //public static IEnumerable<GeoTerm<T>> GetLoopRcpIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2, GeoBasesSignature basisSet)
        //{
        //    foreach (var term1 in mv1.GetNotZeroTerms())
        //    {
        //        foreach (var term2 in mv2.GetNotZeroTerms())
        //        {
        //            if (!GeoFrameUtils.IsNotZeroERcp(term1.BasisBladeId, term2.BasisBladeId))
        //                continue;

        //            yield return basisSet.ScaledGp(term1.BasisBladeId, term2.BasisBladeId, term1.Scalar * term2.Scalar);
        //        }
        //    }
        //}

        //public static IEnumerable<GeoTerm<T>> GetLoopSpIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2, GeoBasesSignature basisSet)
        //{
        //    foreach (var term1 in mv1.GetNotZeroTerms())
        //    {
        //        foreach (var term2 in mv2.GetNotZeroTerms())
        //        {
        //            if (!GeoFrameUtils.IsNotZeroESp(term1.BasisBladeId, term2.BasisBladeId))
        //                continue;

        //            yield return basisSet.ScaledGp(term1.BasisBladeId, term2.BasisBladeId, term1.Scalar * term2.Scalar);
        //        }
        //    }
        //}

        //public static IEnumerable<GeoTerm<T>> GetLoopFdpIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2, GeoBasesSignature basisSet)
        //{
        //    foreach (var term1 in mv1.GetNotZeroTerms())
        //    {
        //        foreach (var term2 in mv2.GetNotZeroTerms())
        //        {
        //            if (!GeoFrameUtils.IsNotZeroEFdp(term1.BasisBladeId, term2.BasisBladeId))
        //                continue;

        //            yield return basisSet.ScaledGp(term1.BasisBladeId, term2.BasisBladeId, term1.Scalar * term2.Scalar);
        //        }
        //    }
        //}

        //public static IEnumerable<GeoTerm<T>> GetLoopHipIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2, GeoBasesSignature basisSet)
        //{
        //    foreach (var term1 in mv1.GetNotZeroTerms())
        //    {
        //        foreach (var term2 in mv2.GetNotZeroTerms())
        //        {
        //            if (!GeoFrameUtils.IsNotZeroEHip(term1.BasisBladeId, term2.BasisBladeId))
        //                continue;

        //            yield return basisSet.ScaledGp(term1.BasisBladeId, term2.BasisBladeId, term1.Scalar * term2.Scalar);
        //        }
        //    }
        //}

        //public static IEnumerable<GeoTerm<T>> GetLoopAcpIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2, GeoBasesSignature basisSet)
        //{
        //    foreach (var term1 in mv1.GetNotZeroTerms())
        //    {
        //        foreach (var term2 in mv2.GetNotZeroTerms())
        //        {
        //            if (!GeoFrameUtils.IsNotZeroEAcp(term1.BasisBladeId, term2.BasisBladeId))
        //                continue;

        //            yield return basisSet.ScaledGp(term1.BasisBladeId, term2.BasisBladeId, term1.Scalar * term2.Scalar);
        //        }
        //    }
        //}

        //public static IEnumerable<GeoTerm<T>> GetLoopCpIdScalarPairs<T>(this Multivector<T> mv1, Multivector<T> mv2, GeoBasesSignature basisSet)
        //{
        //    foreach (var term1 in mv1.GetNotZeroTerms())
        //    {
        //        foreach (var term2 in mv2.GetNotZeroTerms())
        //        {
        //            if (!GeoFrameUtils.IsNotZeroECp(term1.BasisBladeId, term2.BasisBladeId))
        //                continue;

        //            yield return basisSet.ScaledGp(term1.BasisBladeId, term2.BasisBladeId, term1.Scalar * term2.Scalar);
        //        }
        //    }
        //}
        //#endregion


        //#region Euclidean bilinear products on Sparse Array Represntation Multivectors
        //public static GeoNumSarMultivector Op<T>(this GeoNumSarMultivector mv1, GeoNumSarMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtOpTerms(mv2).SumAsSarMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumSarMultivector EGp<T>(this GeoNumSarMultivector mv1, GeoNumSarMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtEGpTerms(mv2).SumAsSarMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumSarMultivector ESp<T>(this GeoNumSarMultivector mv1, GeoNumSarMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtESpTerms(mv2).SumAsSarMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumSarMultivector ELcp<T>(this GeoNumSarMultivector mv1, GeoNumSarMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtELcpTerms(mv2).SumAsSarMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumSarMultivector ERcp<T>(this GeoNumSarMultivector mv1, GeoNumSarMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtERcpTerms(mv2).SumAsSarMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumSarMultivector EFdp<T>(this GeoNumSarMultivector mv1, GeoNumSarMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtEFdpTerms(mv2).SumAsSarMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumSarMultivector EHip<T>(this GeoNumSarMultivector mv1, GeoNumSarMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtEHipTerms(mv2).SumAsSarMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumSarMultivector EAcp<T>(this GeoNumSarMultivector mv1, GeoNumSarMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtEAcpTerms(mv2).SumAsSarMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumSarMultivector ECp<T>(this GeoNumSarMultivector mv1, GeoNumSarMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtECpTerms(mv2).SumAsSarMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumSarMultivector EInverse<T>(this GeoNumSarMultivector mv)
        //{
        //    return mv.GetGptEInverseTerms().SumAsSarMultivector(mv.VSpaceDimension);
        //}
        //#endregion


        //#region Euclidean bilinear products on Dense Graded Represntation Multivectors
        //public static GeoNumDgrMultivector Op<T>(this GeoNumDgrMultivector mv1, GeoNumDgrMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtOpTerms(mv2).SumAsDgrMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumDgrMultivector EGp<T>(this GeoNumDgrMultivector mv1, GeoNumDgrMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtEGpTerms(mv2).SumAsDgrMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumDgrMultivector ESp<T>(this GeoNumDgrMultivector mv1, GeoNumDgrMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtESpTerms(mv2).SumAsDgrMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumDgrMultivector ELcp<T>(this GeoNumDgrMultivector mv1, GeoNumDgrMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtELcpTerms(mv2).SumAsDgrMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumDgrMultivector ERcp<T>(this GeoNumDgrMultivector mv1, GeoNumDgrMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtERcpTerms(mv2).SumAsDgrMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumDgrMultivector EFdp<T>(this GeoNumDgrMultivector mv1, GeoNumDgrMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtEFdpTerms(mv2).SumAsDgrMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumDgrMultivector EHip<T>(this GeoNumDgrMultivector mv1, GeoNumDgrMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtEHipTerms(mv2).SumAsDgrMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumDgrMultivector EAcp<T>(this GeoNumDgrMultivector mv1, GeoNumDgrMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtEAcpTerms(mv2).SumAsDgrMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumDgrMultivector ECp<T>(this GeoNumDgrMultivector mv1, GeoNumDgrMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtECpTerms(mv2).SumAsDgrMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumDgrMultivector EInverse<T>(this GeoNumDgrMultivector mv)
        //{
        //    return mv.GetGptEInverseTerms().SumAsDgrMultivector(mv.VSpaceDimension);
        //}
        //#endregion


        //#region Euclidean bilinear products on Dense Array Represntation Multivectors
        //public static GeoNumDarMultivector Op<T>(this GeoNumDarMultivector mv1, GeoNumDarMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtOpTerms(mv2).SumAsDarMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumDarMultivector EGp<T>(this GeoNumDarMultivector mv1, GeoNumDarMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtEGpTerms(mv2).SumAsDarMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumDarMultivector ESp<T>(this GeoNumDarMultivector mv1, GeoNumDarMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtESpTerms(mv2).SumAsDarMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumDarMultivector ELcp<T>(this GeoNumDarMultivector mv1, GeoNumDarMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtELcpTerms(mv2).SumAsDarMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumDarMultivector ERcp<T>(this GeoNumDarMultivector mv1, GeoNumDarMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtERcpTerms(mv2).SumAsDarMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumDarMultivector EFdp<T>(this GeoNumDarMultivector mv1, GeoNumDarMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtEFdpTerms(mv2).SumAsDarMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumDarMultivector EHip<T>(this GeoNumDarMultivector mv1, GeoNumDarMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtEHipTerms(mv2).SumAsDarMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumDarMultivector EAcp<T>(this GeoNumDarMultivector mv1, GeoNumDarMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtEAcpTerms(mv2).SumAsDarMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumDarMultivector ECp<T>(this GeoNumDarMultivector mv1, GeoNumDarMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtECpTerms(mv2).SumAsDarMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumDarMultivector EInverse<T>(this GeoNumDarMultivector mv)
        //{
        //    return mv.GetGptEInverseTerms().SumAsDarMultivector(mv.VSpaceDimension);
        //}
        //#endregion


        //#region Orthogonal basisSet bilinear products on Sparse Array Represntation Multivectors
        //public static GeoNumSarMultivector Gp<T>(this GeoNumSarMultivector mv1, GeoNumSarMultivector mv2, GeoBasesSignature basisSet)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtGpTerms(mv2, basisSet).SumAsSarMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumSarMultivector Sp<T>(this GeoNumSarMultivector mv1, GeoNumSarMultivector mv2, GeoBasesSignature basisSet)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtSpTerms(mv2, basisSet).SumAsSarMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumSarMultivector Lcp<T>(this GeoNumSarMultivector mv1, GeoNumSarMultivector mv2, GeoBasesSignature basisSet)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtLcpTerms(mv2, basisSet).SumAsSarMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumSarMultivector Rcp<T>(this GeoNumSarMultivector mv1, GeoNumSarMultivector mv2, GeoBasesSignature basisSet)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtRcpTerms(mv2, basisSet).SumAsSarMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumSarMultivector Fdp<T>(this GeoNumSarMultivector mv1, GeoNumSarMultivector mv2, GeoBasesSignature basisSet)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtFdpTerms(mv2, basisSet).SumAsSarMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumSarMultivector Hip<T>(this GeoNumSarMultivector mv1, GeoNumSarMultivector mv2, GeoBasesSignature basisSet)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtHipTerms(mv2, basisSet).SumAsSarMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumSarMultivector Acp<T>(this GeoNumSarMultivector mv1, GeoNumSarMultivector mv2, GeoBasesSignature basisSet)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtAcpTerms(mv2, basisSet).SumAsSarMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumSarMultivector Cp<T>(this GeoNumSarMultivector mv1, GeoNumSarMultivector mv2, GeoBasesSignature basisSet)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtCpTerms(mv2, basisSet).SumAsSarMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumSarMultivector Inverse<T>(this GeoNumSarMultivector mv, GeoBasesSignature basisSet)
        //{
        //    return mv.GetGptInverseTerms(basisSet).SumAsSarMultivector(mv.VSpaceDimension);
        //}
        //#endregion


        //#region Orthogonal basisSet bilinear products on Dense Graded Represntation Multivectors
        //public static GeoNumDgrMultivector Gp<T>(this GeoNumDgrMultivector mv1, GeoNumDgrMultivector mv2, GeoBasesSignature basisSet)
        //{
        //    if (mv1.VSpaceDimension != mv2.VSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    var factory = new GeoNumDgrMultivectorFactory(mv1.VSpaceDimension);

        //    factory.AddGbtGpTerms(mv1, mv2, basisSet);

        //    return factory.GetDgrMultivector();
        //}

        //public static GeoNumDgrMultivector Sp<T>(this GeoNumDgrMultivector mv1, GeoNumDgrMultivector mv2, GeoBasesSignature basisSet)
        //{
        //    if (mv1.VSpaceDimension != mv2.VSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    var factory = new GeoNumDgrMultivectorFactory(mv1.VSpaceDimension);

        //    factory.AddGbtSpTerms(mv1, mv2, basisSet);

        //    return factory.GetDgrMultivector();
        //}

        //public static GeoNumDgrMultivector Lcp<T>(this GeoNumDgrMultivector mv1, GeoNumDgrMultivector mv2, GeoBasesSignature basisSet)
        //{
        //    if (mv1.VSpaceDimension != mv2.VSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    var factory = new GeoNumDgrMultivectorFactory(mv1.VSpaceDimension);

        //    factory.AddGbtLcpTerms(mv1, mv2, basisSet);

        //    return factory.GetDgrMultivector();
        //}

        //public static GeoNumDgrMultivector Rcp<T>(this GeoNumDgrMultivector mv1, GeoNumDgrMultivector mv2, GeoBasesSignature basisSet)
        //{
        //    if (mv1.VSpaceDimension != mv2.VSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    var factory = new GeoNumDgrMultivectorFactory(mv1.VSpaceDimension);

        //    factory.AddGbtRcpTerms(mv1, mv2, basisSet);

        //    return factory.GetDgrMultivector();
        //}

        //public static GeoNumDgrMultivector Fdp<T>(this GeoNumDgrMultivector mv1, GeoNumDgrMultivector mv2, GeoBasesSignature basisSet)
        //{
        //    if (mv1.VSpaceDimension != mv2.VSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    var factory = new GeoNumDgrMultivectorFactory(mv1.VSpaceDimension);

        //    factory.AddGbtFdpTerms(mv1, mv2, basisSet);

        //    return factory.GetDgrMultivector();
        //}

        //public static GeoNumDgrMultivector Hip<T>(this GeoNumDgrMultivector mv1, GeoNumDgrMultivector mv2, GeoBasesSignature basisSet)
        //{
        //    if (mv1.VSpaceDimension != mv2.VSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    var factory = new GeoNumDgrMultivectorFactory(mv1.VSpaceDimension);

        //    factory.AddGbtHipTerms(mv1, mv2, basisSet);

        //    return factory.GetDgrMultivector();
        //}

        //public static GeoNumDgrMultivector Acp<T>(this GeoNumDgrMultivector mv1, GeoNumDgrMultivector mv2, GeoBasesSignature basisSet)
        //{
        //    if (mv1.VSpaceDimension != mv2.VSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    var factory = new GeoNumDgrMultivectorFactory(mv1.VSpaceDimension);

        //    factory.AddGbtAcpTerms(mv1, mv2, basisSet);

        //    return factory.GetDgrMultivector();
        //}

        //public static GeoNumDgrMultivector Cp<T>(this GeoNumDgrMultivector mv1, GeoNumDgrMultivector mv2, GeoBasesSignature basisSet)
        //{
        //    if (mv1.VSpaceDimension != mv2.VSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    var factory = new GeoNumDgrMultivectorFactory(mv1.VSpaceDimension);

        //    factory.AddGbtCpTerms(mv1, mv2, basisSet);

        //    return factory.GetDgrMultivector();
        //}

        //public static GeoNumDgrMultivector Inverse<T>(this GeoNumDgrMultivector mv, GeoBasesSignature basisSet)
        //{
        //    return mv.GetGptInverseTerms(basisSet).SumAsDgrMultivector(mv.VSpaceDimension);
        //}
        //#endregion


        //#region Orthogonal basisSet bilinear products on Dense Array Represntation Multivectors
        //public static GeoNumDarMultivector Gp<T>(this GeoNumDarMultivector mv1, GeoNumDarMultivector mv2, GeoBasesSignature basisSet)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtGpTerms(mv2, basisSet).SumAsDarMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumDarMultivector Sp<T>(this GeoNumDarMultivector mv1, GeoNumDarMultivector mv2, GeoBasesSignature basisSet)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtSpTerms(mv2, basisSet).SumAsDarMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumDarMultivector Lcp<T>(this GeoNumDarMultivector mv1, GeoNumDarMultivector mv2, GeoBasesSignature basisSet)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtLcpTerms(mv2, basisSet).SumAsDarMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumDarMultivector Rcp<T>(this GeoNumDarMultivector mv1, GeoNumDarMultivector mv2, GeoBasesSignature basisSet)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtRcpTerms(mv2, basisSet).SumAsDarMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumDarMultivector Fdp<T>(this GeoNumDarMultivector mv1, GeoNumDarMultivector mv2, GeoBasesSignature basisSet)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtFdpTerms(mv2, basisSet).SumAsDarMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumDarMultivector Hip<T>(this GeoNumDarMultivector mv1, GeoNumDarMultivector mv2, GeoBasesSignature basisSet)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtHipTerms(mv2, basisSet).SumAsDarMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumDarMultivector Acp<T>(this GeoNumDarMultivector mv1, GeoNumDarMultivector mv2, GeoBasesSignature basisSet)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtAcpTerms(mv2, basisSet).SumAsDarMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumDarMultivector Cp<T>(this GeoNumDarMultivector mv1, GeoNumDarMultivector mv2, GeoBasesSignature basisSet)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GeoNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtCpTerms(mv2, basisSet).SumAsDarMultivector(mv1.VSpaceDimension);
        //}

        //public static GeoNumDarMultivector Inverse<T>(this GeoNumDarMultivector mv, GeoBasesSignature basisSet)
        //{
        //    return mv.GetGptInverseTerms(basisSet).SumAsDarMultivector(mv.VSpaceDimension);
        //}
        //#endregion
    }
}
