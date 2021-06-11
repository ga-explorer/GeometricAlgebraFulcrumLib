using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraLib.Frames;
using GeometricAlgebraLib.Multivectors;
using GeometricAlgebraLib.Multivectors.Terms;
using GeometricAlgebraLib.Storage;
using GeometricAlgebraLib.Storage.GuidedBinaryTraversal.Outermorphisms;
using GeometricAlgebraLib.Storage.GuidedBinaryTraversal.Products;

namespace GeometricAlgebraLib.Products
{
    public static class GaBilinearProductsUtils
    {
        public static IEnumerable<Tuple<T, IGaKVectorStorage<T>>> GetGbtOutermorphismScaledKVectors<T>(this GaMultivector<T> mv, IReadOnlyList<IGaVectorStorage<T>> basisVectorMappings)
        {
            return GaGbtMultivectorOutermorphismStack<T>.Create(basisVectorMappings, mv)
                .TraverseForScaledKVectors();
        }

        public static IEnumerable<GaTerm<T>> GetGbtOutermorphismIdScalarPairs<T>(this GaMultivector<T> mv, IReadOnlyList<IGaVectorStorage<T>> basisVectorMappings)
        {
            var scalarProcessor = mv.ScalarProcessor;
            var factorsList = 
                mv.GetGbtOutermorphismScaledKVectors(basisVectorMappings);

            foreach (var (scalingFactor, kVector) in factorsList)
            {
                if (scalarProcessor.IsZero(scalingFactor))
                    continue;

                foreach (var term in kVector.GetNotZeroTerms())
                    yield return term.GetCopy(scalingFactor);
            }
        }


        #region Euclidean bilinear products on multivectors using Guided Binary Traversal
        /// <summary>
        /// Compute the Outer Product terms of two multivectors using Guided Binary Traversal
        /// </summary>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<ulong, T>> GetGbtOpIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            var stack = GaGbtProductsStack2<T>.Create(mv1, mv2);

            return stack.GetOpIdScalarPairs();
        }

        /// <summary>
        /// Compute the Geometric Product terms of two multivectors on the given metric using Guided Binary Traversal
        /// </summary>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<ulong, T>> GetGbtEGpIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            var stack = GaGbtProductsStack2<T>.Create(mv1, mv2);

            return stack.GetEGpIdScalarPairs();
        }

        /// <summary>
        /// Compute the Left Contraction Product terms of two multivectors on the given metric using Guided Binary Traversal
        /// </summary>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<ulong, T>> GetGbtELcpIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            var stack = GaGbtProductsStack2<T>.Create(mv1, mv2);

            return stack.GetELcpIdScalarPairs();
        }

        /// <summary>
        /// Compute the Right Contraction Product terms of two multivectors on the given metric using Guided Binary Traversal
        /// </summary>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<ulong, T>> GetGbtERcpIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            var stack = GaGbtProductsStack2<T>.Create(mv1, mv2);

            return stack.GetERcpIdScalarPairs();
        }

        /// <summary>
        /// Compute the Scalar Product terms of two multivectors on the given metric using Guided Binary Traversal
        /// </summary>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<ulong, T>> GetGbtESpIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            var stack = GaGbtProductsStack2<T>.Create(mv1, mv2);

            return stack.GetESpIdScalarPairs();
        }

        /// <summary>
        /// Compute the Fat-Dot Product terms of two multivectors on the given metric using Guided Binary Traversal
        /// </summary>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<ulong, T>> GetGbtEFdpIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            var stack = GaGbtProductsStack2<T>.Create(mv1, mv2);

            return stack.GetEFdpIdScalarPairs();
        }

        /// <summary>
        /// Compute the Hestenes Inner Product terms of two multivectors on the given metric using Guided Binary Traversal
        /// </summary>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<ulong, T>> GetGbtEHipIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            var stack = GaGbtProductsStack2<T>.Create(mv1, mv2);

            return stack.GetEHipIdScalarPairs();
        }

        /// <summary>
        /// Compute the Anti-Commutator Product terms of two multivectors on the given metric using Guided Binary Traversal
        /// </summary>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<ulong, T>> GetGbtEAcpIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            var stack = GaGbtProductsStack2<T>.Create(mv1, mv2);

            return stack.GetEAcpIdScalarPairs();
        }

        /// <summary>
        /// Compute the Commutator Product terms of two multivectors on the Euclidean metric using Guided Binary Traversal
        /// </summary>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<ulong, T>> GetGbtECpIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            var stack = GaGbtProductsStack2<T>.Create(mv1, mv2);

            return stack.GetECpIdScalarPairs();
        }

        public static IEnumerable<KeyValuePair<ulong, T>> GetGbtENorm2IdScalarPairs<T>(this GaMultivector<T> mv)
        {
            var stack = GaGbtProductsStack2<T>.Create(mv, mv.Reverse());

            return stack.GetESpIdScalarPairs();
        }

        public static T ENorm2<T>(this GaMultivector<T> mv)
        {
            return mv.ScalarProcessor.Add(
                mv.GetGbtENorm2IdScalarPairs().Select(t => t.Value)
            );
        }

        public static T ENorm<T>(this GaMultivector<T> mv)
        {
            var scalarProcessor = mv.ScalarProcessor;

            return scalarProcessor.Sqrt(
                scalarProcessor.Add(
                    mv.GetGbtENorm2IdScalarPairs().Select(t => t.Value)
                )
            );
        }

        public static IEnumerable<GaTerm<T>> GetGptEInverseIdScalarPairs<T>(this GaMultivector<T> mv)
        {
            var mvReverse = mv.Reverse();

            var stack = GaGbtProductsStack2<T>.Create(mv, mvReverse);

            var scalarProcessor = mv.ScalarProcessor;

            var norm2 = scalarProcessor.Add(
                stack
                    .GetESpIdScalarPairs()
                    .Select(t => t.Value)
                );

            return (mvReverse /  norm2).Storage.GetTerms();
        }
        #endregion


        #region Orthogonal metric bilinear products on multivectors using Guided Binary Traversal
        /// <summary>
        /// Compute the Geometric Product terms of two multivectors on the given metric using Guided Binary Traversal
        /// </summary>
        /// <param name="metric"></param>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<ulong, T>> GetGbtGpIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2, GaOrthonormalBasesSignature metric)
        {
            var stack = GaGbtProductsStack2<T>.Create(mv1, mv2);

            return stack.GetGpIdScalarPairs(metric);
        }

        /// <summary>
        /// Compute the Left Contraction Product terms of two multivectors on the given metric using Guided Binary Traversal
        /// </summary>
        /// <param name="metric"></param>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<ulong, T>> GetGbtLcpIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2, GaOrthonormalBasesSignature metric)
        {
            var stack = GaGbtProductsStack2<T>.Create(mv1, mv2);

            return stack.GetLcpIdScalarPairs(metric);
        }

        /// <summary>
        /// Compute the Right Contraction Product terms of two multivectors on the given metric using Guided Binary Traversal
        /// </summary>
        /// <param name="metric"></param>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<ulong, T>> GetGbtRcpIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2, GaOrthonormalBasesSignature metric)
        {
            var stack = GaGbtProductsStack2<T>.Create(mv1, mv2);

            return stack.GetRcpIdScalarPairs(metric);
        }

        /// <summary>
        /// Compute the Scalar Product terms of two multivectors on the given metric using Guided Binary Traversal
        /// </summary>
        /// <param name="metric"></param>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<ulong, T>> GetGbtSpIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2, GaOrthonormalBasesSignature metric)
        {
            var stack = GaGbtProductsStack2<T>.Create(mv1, mv2);

            return stack.GetSpIdScalarPairs(metric);
        }

        /// <summary>
        /// Compute the Fat-Dot Product terms of two multivectors on the given metric using Guided Binary Traversal
        /// </summary>
        /// <param name="metric"></param>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<ulong, T>> GetGbtFdpIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2, GaOrthonormalBasesSignature metric)
        {
            var stack = GaGbtProductsStack2<T>.Create(mv1, mv2);

            return stack.GetFdpIdScalarPairs(metric);
        }

        /// <summary>
        /// Compute the Hestenes Inner Product terms of two multivectors on the given metric using Guided Binary Traversal
        /// </summary>
        /// <param name="metric"></param>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<ulong, T>> GetGbtHipIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2, GaOrthonormalBasesSignature metric)
        {
            var stack = GaGbtProductsStack2<T>.Create(mv1, mv2);

            return stack.GetHipIdScalarPairs(metric);
        }

        /// <summary>
        /// Compute the Anti-Commutator Product terms of two multivectors on the given metric using Guided Binary Traversal
        /// </summary>
        /// <param name="metric"></param>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<ulong, T>> GetGbtAcpIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2, GaOrthonormalBasesSignature metric)
        {
            var stack = GaGbtProductsStack2<T>.Create(mv1, mv2);

            return stack.GetAcpIdScalarPairs(metric);
        }

        /// <summary>
        /// Compute the Commutator Product terms of two multivectors on the given metric using Guided Binary Traversal
        /// </summary>
        /// <param name="metric"></param>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<ulong, T>> GetGbtCpIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2, GaOrthonormalBasesSignature metric)
        {
            var stack = GaGbtProductsStack2<T>.Create(mv1, mv2);

            return stack.GetCpIdScalarPairs(metric);
        }

        public static IEnumerable<KeyValuePair<ulong, T>> GetGbtNorm2IdScalarPairs<T>(this GaMultivector<T> mv, GaOrthonormalBasesSignature metric)
        {
            var stack = GaGbtProductsStack2<T>.Create(mv, mv.Reverse());

            return stack.GetSpIdScalarPairs(metric);
        }

        public static T Norm2<T>(this GaMultivector<T> mv, GaOrthonormalBasesSignature metric)
        {
            return mv.ScalarProcessor.Add(
                mv.GetGbtNorm2IdScalarPairs(metric).Select(t => t.Value)
            );
        }

        public static T Norm<T>(this GaMultivector<T> mv, GaOrthonormalBasesSignature metric)
        {
            var scalarProcessor = mv.ScalarProcessor;

            return scalarProcessor.Sqrt(scalarProcessor.Add(
                mv.GetGbtNorm2IdScalarPairs(metric).Select(t => t.Value)
            ));
        }

        public static IEnumerable<GaTerm<T>> GetGptInverseIdScalarPairs<T>(this GaMultivector<T> mv, GaOrthonormalBasesSignature metric)
        {
            var mvReverse = mv.Reverse();

            var stack = GaGbtProductsStack2<T>.Create(mv, mvReverse);

            var scalarProcessor = mv.ScalarProcessor;

            var norm2 = scalarProcessor.Add(
                stack
                    .GetSpIdScalarPairs(metric)
                    .Select(t => t.Value)
                );

            return (mvReverse / norm2).Storage.GetTerms();
        }
        #endregion


        //#region Non-Orthogonal metric bilinear products on multivectors using Guided Binary Traversal
        ///// <summary>
        ///// Compute the Geometric Product terms of two multivectors on the given metric using Guided Binary Traversal
        ///// </summary>
        ///// <param name="metric"></param>
        ///// <param name="mv1"></param>
        ///// <param name="mv2"></param>
        ///// <returns></returns>
        //public static IEnumerable<GaTerm<T>> GetGbtGpIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2, GaOmChangeOfBasis<T> metric)
        //{
        //    var orthoMv1 =
        //        metric.ForwardOutermorphism.MapMultivector(mv1);

        //    var orthoMv2 =
        //        metric.ForwardOutermorphism.MapMultivector(mv2);

        //    var orthoMv =
        //        GetGbtGpIdScalarPairs(orthoMv1, orthoMv2, metric.BaseFrameSignature).SumAsMultivectorStorageUniform(mv1.ScalarProcessor);

        //    return metric.BackwardOutermorphism.MapMultivector(orthoMv).MultivectorStorage.GetNotZeroTerms();
        //}

        ///// <summary>
        ///// Compute the Left Contraction Product terms of two multivectors on the given metric using Guided Binary Traversal
        ///// </summary>
        ///// <param name="metric"></param>
        ///// <param name="mv1"></param>
        ///// <param name="mv2"></param>
        ///// <returns></returns>
        //public static IEnumerable<GaTerm<T>> GetGbtLcpIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2, GaOmChangeOfBasis<T> metric)
        //{
        //    var orthoMv1 =
        //        metric.ForwardOutermorphism.MapMultivector(mv1);

        //    var orthoMv2 =
        //        metric.ForwardOutermorphism.MapMultivector(mv2);

        //    var orthoMv =
        //        GetGbtLcpTerms(orthoMv1, orthoMv2, metric.BaseFrameSignature).SumAsMultivectorStorageUniform(mv1.ScalarDomain);

        //    return metric.BackwardOutermorphism.MapMultivector(orthoMv).GetNotZeroTerms();
        //}

        ///// <summary>
        ///// Compute the Right Contraction Product terms of two multivectors on the given metric using Guided Binary Traversal
        ///// </summary>
        ///// <param name="metric"></param>
        ///// <param name="mv1"></param>
        ///// <param name="mv2"></param>
        ///// <returns></returns>
        //public static IEnumerable<GaTerm<T>> GetGbtRcpIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2, GaOmChangeOfBasis<T> metric)
        //{
        //    var orthoMv1 =
        //        metric.ForwardOutermorphism.MapMultivector(mv1);

        //    var orthoMv2 =
        //        metric.ForwardOutermorphism.MapMultivector(mv2);

        //    var orthoMv =
        //        GetGbtRcpTerms(orthoMv1, orthoMv2, metric.BaseFrameSignature).SumAsMultivectorStorageUniform(mv1.ScalarDomain);

        //    return metric.BackwardOutermorphism.MapMultivector(orthoMv).GetNotZeroTerms();
        //}

        ///// <summary>
        ///// Compute the Scalar Product terms of two multivectors on the given metric using Guided Binary Traversal
        ///// </summary>
        ///// <param name="metric"></param>
        ///// <param name="mv1"></param>
        ///// <param name="mv2"></param>
        ///// <returns></returns>
        //public static IEnumerable<GaTerm<T>> GetGbtSpIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2, GaOmChangeOfBasis<T> metric)
        //{
        //    var orthoMv1 =
        //        metric.ForwardOutermorphism.MapMultivector(mv1);

        //    var orthoMv2 =
        //        metric.ForwardOutermorphism.MapMultivector(mv2);

        //    var orthoMv =
        //        GetGbtSpTerms(orthoMv1, orthoMv2, metric.BaseFrameSignature).SumAsMultivectorStorageUniform(mv1.ScalarDomain);

        //    return metric.BackwardOutermorphism.MapMultivector(orthoMv).GetNotZeroTerms();
        //}

        ///// <summary>
        ///// Compute the Fat-Dot Product terms of two multivectors on the given metric using Guided Binary Traversal
        ///// </summary>
        ///// <param name="metric"></param>
        ///// <param name="mv1"></param>
        ///// <param name="mv2"></param>
        ///// <returns></returns>
        //public static IEnumerable<GaTerm<T>> GetGbtFdpIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2, GaOmChangeOfBasis<T> metric)
        //{
        //    var orthoMv1 =
        //        metric.ForwardOutermorphism.MapMultivector(mv1);

        //    var orthoMv2 =
        //        metric.ForwardOutermorphism.MapMultivector(mv2);

        //    var orthoMv =
        //        GetGbtFdpTerms(orthoMv1, orthoMv2, metric.BaseFrameSignature).SumAsMultivectorStorageUniform(mv1.ScalarDomain);

        //    return metric.BackwardOutermorphism.MapMultivector(orthoMv).GetNotZeroTerms();
        //}

        ///// <summary>
        ///// Compute the Hestenes Inner Product terms of two multivectors on the given metric using Guided Binary Traversal
        ///// </summary>
        ///// <param name="metric"></param>
        ///// <param name="mv1"></param>
        ///// <param name="mv2"></param>
        ///// <returns></returns>
        //public static IEnumerable<GaTerm<T>> GetGbtHipIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2, GaOmChangeOfBasis<T> metric)
        //{
        //    var orthoMv1 =
        //        metric.ForwardOutermorphism.MapMultivector(mv1);

        //    var orthoMv2 =
        //        metric.ForwardOutermorphism.MapMultivector(mv2);

        //    var orthoMv =
        //        GetGbtHipTerms(orthoMv1, orthoMv2, metric.BaseFrameSignature).SumAsMultivectorStorageUniform(mv1.ScalarDomain);

        //    return metric.BackwardOutermorphism.MapMultivector(orthoMv).GetNotZeroTerms();
        //}

        ///// <summary>
        ///// Compute the Anti-Commutator Product terms of two multivectors on the given metric using Guided Binary Traversal
        ///// </summary>
        ///// <param name="metric"></param>
        ///// <param name="mv1"></param>
        ///// <param name="mv2"></param>
        ///// <returns></returns>
        //public static IEnumerable<GaTerm<T>> GetGbtAcpIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2, GaOmChangeOfBasis<T> metric)
        //{
        //    var orthoMv1 =
        //        metric.ForwardOutermorphism.MapMultivector(mv1);

        //    var orthoMv2 =
        //        metric.ForwardOutermorphism.MapMultivector(mv2);

        //    var orthoMv =
        //        GetGbtAcpTerms(orthoMv1, orthoMv2, metric.BaseFrameSignature).SumAsMultivectorStorageUniform(mv1.ScalarDomain);

        //    return metric.BackwardOutermorphism.MapMultivector(orthoMv).GetNotZeroTerms();
        //}

        ///// <summary>
        ///// Compute the Commutator Product terms of two multivectors on the given metric using Guided Binary Traversal
        ///// </summary>
        ///// <param name="metric"></param>
        ///// <param name="mv1"></param>
        ///// <param name="mv2"></param>
        ///// <returns></returns>
        //public static IEnumerable<GaTerm<T>> GetGbtCpIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2, GaOmChangeOfBasis<T> metric)
        //{
        //    var orthoMv1 =
        //        metric.ForwardOutermorphism.MapMultivector(mv1);

        //    var orthoMv2 =
        //        metric.ForwardOutermorphism.MapMultivector(mv2);

        //    var orthoMv =
        //        GetGbtCpTerms(orthoMv1, orthoMv2, metric.BaseFrameSignature).SumAsMultivectorStorageUniform(mv1.ScalarDomain);

        //    return metric.BackwardOutermorphism.MapMultivector(orthoMv).GetNotZeroTerms();
        //}

        //public static IEnumerable<GaTerm<T>> GetGbtNorm2IdScalarPairs<T>(this GaMultivector<T> mv, GaOmChangeOfBasis<T> metric)
        //{
        //    var orthoMv =
        //        metric.ForwardOutermorphism.MapMultivector(mv);

        //    return GetGbtNorm2Terms(orthoMv, metric.BaseFrameSignature).SumAsMultivectorStorageUniform(mv.ScalarDomain);
        //}

        //public static T Norm2<T>(this GaMultivector<T> mv, GaOmChangeOfBasis<T> metric)
        //{
        //    return mv.GetGbtNorm2Terms(metric).Select(t => t.Scalar).Sum();
        //}

        //public static T Norm<T>(this GaMultivector<T> mv, GaOmChangeOfBasis<T> metric)
        //{
        //    return Math.Sqrt(
        //        mv.GetGbtNorm2Terms(metric).Select(t => t.Scalar).Sum()
        //    );
        //}

        //public static IEnumerable<GaTerm<T>> GetGbtInverseIdScalarPairs<T>(this GaMultivector<T> mv, GaOmChangeOfBasis<T> metric)
        //{
        //    var mvReverse = mv.GetReverse();

        //    var orthoMv =
        //        metric.ForwardOutermorphism.MapMultivector(mvReverse);

        //    var norm2 = 
        //        GetGbtNorm2Terms(orthoMv, metric.BaseFrameSignature)
        //            .Select(t => t.Scalar)
        //            .Sum();

        //    return mvReverse.GetScaledTerms(1 / norm2);
        //}
        //#endregion


        //public static IEnumerable<GaTerm<T>> GetGbtGpIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2, IGaNumMetric metric)
        //{
        //    if (metric is GaOmChangeOfBasis<T> nonOrthogonalMetric)
        //        return mv1.GetGbtGpTerms(mv2, nonOrthogonalMetric);

        //    var orthogonalMetric = (GaBasesSignature)metric;
        //    return mv1.GetGbtGpTerms(mv2, orthogonalMetric);
        //}

        //public static T GetGbtNorm2(this GaMultivector<T> mv, IGaNumMetric metric)
        //{
        //    if (metric is GaOmChangeOfBasis<T> nonOrthogonalMetric)
        //        return mv.GetGbtNorm2Terms(nonOrthogonalMetric).Select(t => t.Scalar).Sum();

        //    var orthogonalMetric = (GaBasesSignature)metric;
        //    return mv.GetGbtNorm2Terms(orthogonalMetric).Select(t => t.Scalar).Sum();
        //}


        //#region Euclidean bilinear products on multivectors using Simple Looping
        //public static IEnumerable<GaTerm<T>> GetLoopOpIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        //{
        //    foreach (var term1 in mv1.GetNotZeroTerms())
        //    {
        //        foreach (var term2 in mv2.GetNotZeroTerms())
        //        {
        //            if (!GaFrameUtils.IsNotZeroOp(term1.BasisBladeId, term2.BasisBladeId))
        //                continue;

        //            var value =
        //                term1.Scalar * term2.Scalar *
        //                (GaFrameUtils.IsNegativeEGp(term1.BasisBladeId, term2.BasisBladeId) ? -1.0d : 1.0d);

        //            yield return new GaTerm<T>(term1.BasisBladeId ^ term2.BasisBladeId, value);
        //        }
        //    }
        //}

        //public static IEnumerable<GaTerm<T>> GetLoopEGpIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        //{
        //    foreach (var term1 in mv1.GetNotZeroTerms())
        //    {
        //        foreach (var term2 in mv2.GetNotZeroTerms())
        //        {
        //            var value =
        //                term1.Scalar * term2.Scalar *
        //                (GaFrameUtils.IsNegativeEGp(term1.BasisBladeId, term2.BasisBladeId) ? -1.0d : 1.0d);

        //            yield return new GaTerm<T>(term1.BasisBladeId ^ term2.BasisBladeId, value);
        //        }
        //    }
        //}

        //public static IEnumerable<GaTerm<T>> GetLoopELcpIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        //{
        //    foreach (var term1 in mv1.GetNotZeroTerms())
        //    {
        //        foreach (var term2 in mv2.GetNotZeroTerms())
        //        {
        //            if (!GaFrameUtils.IsNotZeroELcp(term1.BasisBladeId, term2.BasisBladeId))
        //                continue;

        //            var value =
        //                term1.Scalar * term2.Scalar *
        //                (GaFrameUtils.IsNegativeEGp(term1.BasisBladeId, term2.BasisBladeId) ? -1.0d : 1.0d);

        //            yield return new GaTerm<T>(term1.BasisBladeId ^ term2.BasisBladeId, value);
        //        }
        //    }
        //}

        //public static IEnumerable<GaTerm<T>> GetLoopERcpIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        //{
        //    foreach (var term1 in mv1.GetNotZeroTerms())
        //    {
        //        foreach (var term2 in mv2.GetNotZeroTerms())
        //        {
        //            if (!GaFrameUtils.IsNotZeroERcp(term1.BasisBladeId, term2.BasisBladeId))
        //                continue;

        //            var value =
        //                term1.Scalar * term2.Scalar *
        //                (GaFrameUtils.IsNegativeEGp(term1.BasisBladeId, term2.BasisBladeId) ? -1.0d : 1.0d);

        //            yield return new GaTerm<T>(term1.BasisBladeId ^ term2.BasisBladeId, value);
        //        }
        //    }
        //}

        //public static IEnumerable<GaTerm<T>> GetLoopESpIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        //{
        //    foreach (var term1 in mv1.GetNotZeroTerms())
        //    {
        //        foreach (var term2 in mv2.GetNotZeroTerms())
        //        {
        //            if (!GaFrameUtils.IsNotZeroESp(term1.BasisBladeId, term2.BasisBladeId))
        //                continue;

        //            var value =
        //                term1.Scalar * term2.Scalar *
        //                (GaFrameUtils.IsNegativeEGp(term1.BasisBladeId, term2.BasisBladeId) ? -1.0d : 1.0d);

        //            yield return new GaTerm<T>(term1.BasisBladeId ^ term2.BasisBladeId, value);
        //        }
        //    }
        //}

        //public static IEnumerable<GaTerm<T>> GetLoopEFdpIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        //{
        //    foreach (var term1 in mv1.GetNotZeroTerms())
        //    {
        //        foreach (var term2 in mv2.GetNotZeroTerms())
        //        {
        //            if (!GaFrameUtils.IsNotZeroEFdp(term1.BasisBladeId, term2.BasisBladeId))
        //                continue;

        //            var value =
        //                term1.Scalar * term2.Scalar *
        //                (GaFrameUtils.IsNegativeEGp(term1.BasisBladeId, term2.BasisBladeId) ? -1.0d : 1.0d);

        //            yield return new GaTerm<T>(term1.BasisBladeId ^ term2.BasisBladeId, value);
        //        }
        //    }
        //}

        //public static IEnumerable<GaTerm<T>> GetLoopEHipIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        //{
        //    foreach (var term1 in mv1.GetNotZeroTerms())
        //    {
        //        foreach (var term2 in mv2.GetNotZeroTerms())
        //        {
        //            if (!GaFrameUtils.IsNotZeroEHip(term1.BasisBladeId, term2.BasisBladeId))
        //                continue;

        //            var value =
        //                term1.Scalar * term2.Scalar *
        //                (GaFrameUtils.IsNegativeEGp(term1.BasisBladeId, term2.BasisBladeId) ? -1.0d : 1.0d);

        //            yield return new GaTerm<T>(term1.BasisBladeId ^ term2.BasisBladeId, value);
        //        }
        //    }
        //}

        //public static IEnumerable<GaTerm<T>> GetLoopEAcpIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        //{
        //    foreach (var term1 in mv1.GetNotZeroTerms())
        //    {
        //        foreach (var term2 in mv2.GetNotZeroTerms())
        //        {
        //            if (!GaFrameUtils.IsNotZeroEAcp(term1.BasisBladeId, term2.BasisBladeId))
        //                continue;

        //            var value =
        //                term1.Scalar * term2.Scalar *
        //                (GaFrameUtils.IsNegativeEGp(term1.BasisBladeId, term2.BasisBladeId) ? -1.0d : 1.0d);

        //            yield return new GaTerm<T>(term1.BasisBladeId ^ term2.BasisBladeId, value);
        //        }
        //    }
        //}

        //public static IEnumerable<GaTerm<T>> GetLoopECpIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        //{
        //    foreach (var term1 in mv1.GetNotZeroTerms())
        //    {
        //        foreach (var term2 in mv2.GetNotZeroTerms())
        //        {
        //            if (!GaFrameUtils.IsNotZeroECp(term1.BasisBladeId, term2.BasisBladeId))
        //                continue;

        //            var value =
        //                term1.Scalar * term2.Scalar *
        //                (GaFrameUtils.IsNegativeEGp(term1.BasisBladeId, term2.BasisBladeId) ? -1.0d : 1.0d);

        //            yield return new GaTerm<T>(term1.BasisBladeId ^ term2.BasisBladeId, value);
        //        }
        //    }
        //}
        //#endregion


        //#region Orthogonal metric bilinear products on multivectors using Simple Looping
        //public static IEnumerable<GaTerm<T>> GetLoopGpIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2, GaBasesSignature metric)
        //{
        //    foreach (var term1 in mv1.GetNotZeroTerms())
        //    {
        //        foreach (var term2 in mv2.GetNotZeroTerms())
        //        {
        //            yield return metric.ScaledGp(term1.BasisBladeId, term2.BasisBladeId, term1.Scalar * term2.Scalar); 
        //        }
        //    }
        //}

        //public static IEnumerable<GaTerm<T>> GetLoopLcpIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2, GaBasesSignature metric)
        //{
        //    foreach (var term1 in mv1.GetNotZeroTerms())
        //    {
        //        foreach (var term2 in mv2.GetNotZeroTerms())
        //        {
        //            if (!GaFrameUtils.IsNotZeroELcp(term1.BasisBladeId, term2.BasisBladeId))
        //                continue;

        //            yield return metric.ScaledGp(term1.BasisBladeId, term2.BasisBladeId, term1.Scalar * term2.Scalar);
        //        }
        //    }
        //}

        //public static IEnumerable<GaTerm<T>> GetLoopRcpIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2, GaBasesSignature metric)
        //{
        //    foreach (var term1 in mv1.GetNotZeroTerms())
        //    {
        //        foreach (var term2 in mv2.GetNotZeroTerms())
        //        {
        //            if (!GaFrameUtils.IsNotZeroERcp(term1.BasisBladeId, term2.BasisBladeId))
        //                continue;

        //            yield return metric.ScaledGp(term1.BasisBladeId, term2.BasisBladeId, term1.Scalar * term2.Scalar);
        //        }
        //    }
        //}

        //public static IEnumerable<GaTerm<T>> GetLoopSpIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2, GaBasesSignature metric)
        //{
        //    foreach (var term1 in mv1.GetNotZeroTerms())
        //    {
        //        foreach (var term2 in mv2.GetNotZeroTerms())
        //        {
        //            if (!GaFrameUtils.IsNotZeroESp(term1.BasisBladeId, term2.BasisBladeId))
        //                continue;

        //            yield return metric.ScaledGp(term1.BasisBladeId, term2.BasisBladeId, term1.Scalar * term2.Scalar);
        //        }
        //    }
        //}

        //public static IEnumerable<GaTerm<T>> GetLoopFdpIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2, GaBasesSignature metric)
        //{
        //    foreach (var term1 in mv1.GetNotZeroTerms())
        //    {
        //        foreach (var term2 in mv2.GetNotZeroTerms())
        //        {
        //            if (!GaFrameUtils.IsNotZeroEFdp(term1.BasisBladeId, term2.BasisBladeId))
        //                continue;

        //            yield return metric.ScaledGp(term1.BasisBladeId, term2.BasisBladeId, term1.Scalar * term2.Scalar);
        //        }
        //    }
        //}

        //public static IEnumerable<GaTerm<T>> GetLoopHipIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2, GaBasesSignature metric)
        //{
        //    foreach (var term1 in mv1.GetNotZeroTerms())
        //    {
        //        foreach (var term2 in mv2.GetNotZeroTerms())
        //        {
        //            if (!GaFrameUtils.IsNotZeroEHip(term1.BasisBladeId, term2.BasisBladeId))
        //                continue;

        //            yield return metric.ScaledGp(term1.BasisBladeId, term2.BasisBladeId, term1.Scalar * term2.Scalar);
        //        }
        //    }
        //}

        //public static IEnumerable<GaTerm<T>> GetLoopAcpIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2, GaBasesSignature metric)
        //{
        //    foreach (var term1 in mv1.GetNotZeroTerms())
        //    {
        //        foreach (var term2 in mv2.GetNotZeroTerms())
        //        {
        //            if (!GaFrameUtils.IsNotZeroEAcp(term1.BasisBladeId, term2.BasisBladeId))
        //                continue;

        //            yield return metric.ScaledGp(term1.BasisBladeId, term2.BasisBladeId, term1.Scalar * term2.Scalar);
        //        }
        //    }
        //}

        //public static IEnumerable<GaTerm<T>> GetLoopCpIdScalarPairs<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2, GaBasesSignature metric)
        //{
        //    foreach (var term1 in mv1.GetNotZeroTerms())
        //    {
        //        foreach (var term2 in mv2.GetNotZeroTerms())
        //        {
        //            if (!GaFrameUtils.IsNotZeroECp(term1.BasisBladeId, term2.BasisBladeId))
        //                continue;

        //            yield return metric.ScaledGp(term1.BasisBladeId, term2.BasisBladeId, term1.Scalar * term2.Scalar);
        //        }
        //    }
        //}
        //#endregion


        //#region Euclidean bilinear products on Sparse Array Represntation Multivectors
        //public static GaNumSarMultivector Op<T>(this GaNumSarMultivector mv1, GaNumSarMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtOpTerms(mv2).SumAsSarMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumSarMultivector EGp<T>(this GaNumSarMultivector mv1, GaNumSarMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtEGpTerms(mv2).SumAsSarMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumSarMultivector ESp<T>(this GaNumSarMultivector mv1, GaNumSarMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtESpTerms(mv2).SumAsSarMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumSarMultivector ELcp<T>(this GaNumSarMultivector mv1, GaNumSarMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtELcpTerms(mv2).SumAsSarMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumSarMultivector ERcp<T>(this GaNumSarMultivector mv1, GaNumSarMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtERcpTerms(mv2).SumAsSarMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumSarMultivector EFdp<T>(this GaNumSarMultivector mv1, GaNumSarMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtEFdpTerms(mv2).SumAsSarMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumSarMultivector EHip<T>(this GaNumSarMultivector mv1, GaNumSarMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtEHipTerms(mv2).SumAsSarMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumSarMultivector EAcp<T>(this GaNumSarMultivector mv1, GaNumSarMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtEAcpTerms(mv2).SumAsSarMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumSarMultivector ECp<T>(this GaNumSarMultivector mv1, GaNumSarMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtECpTerms(mv2).SumAsSarMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumSarMultivector EInverse<T>(this GaNumSarMultivector mv)
        //{
        //    return mv.GetGptEInverseTerms().SumAsSarMultivector(mv.VSpaceDimension);
        //}
        //#endregion


        //#region Euclidean bilinear products on Dense Graded Represntation Multivectors
        //public static GaNumDgrMultivector Op<T>(this GaNumDgrMultivector mv1, GaNumDgrMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtOpTerms(mv2).SumAsDgrMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumDgrMultivector EGp<T>(this GaNumDgrMultivector mv1, GaNumDgrMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtEGpTerms(mv2).SumAsDgrMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumDgrMultivector ESp<T>(this GaNumDgrMultivector mv1, GaNumDgrMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtESpTerms(mv2).SumAsDgrMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumDgrMultivector ELcp<T>(this GaNumDgrMultivector mv1, GaNumDgrMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtELcpTerms(mv2).SumAsDgrMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumDgrMultivector ERcp<T>(this GaNumDgrMultivector mv1, GaNumDgrMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtERcpTerms(mv2).SumAsDgrMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumDgrMultivector EFdp<T>(this GaNumDgrMultivector mv1, GaNumDgrMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtEFdpTerms(mv2).SumAsDgrMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumDgrMultivector EHip<T>(this GaNumDgrMultivector mv1, GaNumDgrMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtEHipTerms(mv2).SumAsDgrMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumDgrMultivector EAcp<T>(this GaNumDgrMultivector mv1, GaNumDgrMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtEAcpTerms(mv2).SumAsDgrMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumDgrMultivector ECp<T>(this GaNumDgrMultivector mv1, GaNumDgrMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtECpTerms(mv2).SumAsDgrMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumDgrMultivector EInverse<T>(this GaNumDgrMultivector mv)
        //{
        //    return mv.GetGptEInverseTerms().SumAsDgrMultivector(mv.VSpaceDimension);
        //}
        //#endregion


        //#region Euclidean bilinear products on Dense Array Represntation Multivectors
        //public static GaNumDarMultivector Op<T>(this GaNumDarMultivector mv1, GaNumDarMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtOpTerms(mv2).SumAsDarMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumDarMultivector EGp<T>(this GaNumDarMultivector mv1, GaNumDarMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtEGpTerms(mv2).SumAsDarMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumDarMultivector ESp<T>(this GaNumDarMultivector mv1, GaNumDarMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtESpTerms(mv2).SumAsDarMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumDarMultivector ELcp<T>(this GaNumDarMultivector mv1, GaNumDarMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtELcpTerms(mv2).SumAsDarMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumDarMultivector ERcp<T>(this GaNumDarMultivector mv1, GaNumDarMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtERcpTerms(mv2).SumAsDarMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumDarMultivector EFdp<T>(this GaNumDarMultivector mv1, GaNumDarMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtEFdpTerms(mv2).SumAsDarMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumDarMultivector EHip<T>(this GaNumDarMultivector mv1, GaNumDarMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtEHipTerms(mv2).SumAsDarMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumDarMultivector EAcp<T>(this GaNumDarMultivector mv1, GaNumDarMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtEAcpTerms(mv2).SumAsDarMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumDarMultivector ECp<T>(this GaNumDarMultivector mv1, GaNumDarMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtECpTerms(mv2).SumAsDarMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumDarMultivector EInverse<T>(this GaNumDarMultivector mv)
        //{
        //    return mv.GetGptEInverseTerms().SumAsDarMultivector(mv.VSpaceDimension);
        //}
        //#endregion


        //#region Orthogonal metric bilinear products on Sparse Array Represntation Multivectors
        //public static GaNumSarMultivector Gp<T>(this GaNumSarMultivector mv1, GaNumSarMultivector mv2, GaBasesSignature metric)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtGpTerms(mv2, metric).SumAsSarMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumSarMultivector Sp<T>(this GaNumSarMultivector mv1, GaNumSarMultivector mv2, GaBasesSignature metric)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtSpTerms(mv2, metric).SumAsSarMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumSarMultivector Lcp<T>(this GaNumSarMultivector mv1, GaNumSarMultivector mv2, GaBasesSignature metric)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtLcpTerms(mv2, metric).SumAsSarMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumSarMultivector Rcp<T>(this GaNumSarMultivector mv1, GaNumSarMultivector mv2, GaBasesSignature metric)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtRcpTerms(mv2, metric).SumAsSarMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumSarMultivector Fdp<T>(this GaNumSarMultivector mv1, GaNumSarMultivector mv2, GaBasesSignature metric)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtFdpTerms(mv2, metric).SumAsSarMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumSarMultivector Hip<T>(this GaNumSarMultivector mv1, GaNumSarMultivector mv2, GaBasesSignature metric)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtHipTerms(mv2, metric).SumAsSarMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumSarMultivector Acp<T>(this GaNumSarMultivector mv1, GaNumSarMultivector mv2, GaBasesSignature metric)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtAcpTerms(mv2, metric).SumAsSarMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumSarMultivector Cp<T>(this GaNumSarMultivector mv1, GaNumSarMultivector mv2, GaBasesSignature metric)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtCpTerms(mv2, metric).SumAsSarMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumSarMultivector Inverse<T>(this GaNumSarMultivector mv, GaBasesSignature metric)
        //{
        //    return mv.GetGptInverseTerms(metric).SumAsSarMultivector(mv.VSpaceDimension);
        //}
        //#endregion


        //#region Orthogonal metric bilinear products on Dense Graded Represntation Multivectors
        //public static GaNumDgrMultivector Gp<T>(this GaNumDgrMultivector mv1, GaNumDgrMultivector mv2, GaBasesSignature metric)
        //{
        //    if (mv1.VSpaceDimension != mv2.VSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    var factory = new GaNumDgrMultivectorFactory(mv1.VSpaceDimension);

        //    factory.AddGbtGpTerms(mv1, mv2, metric);

        //    return factory.GetDgrMultivector();
        //}

        //public static GaNumDgrMultivector Sp<T>(this GaNumDgrMultivector mv1, GaNumDgrMultivector mv2, GaBasesSignature metric)
        //{
        //    if (mv1.VSpaceDimension != mv2.VSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    var factory = new GaNumDgrMultivectorFactory(mv1.VSpaceDimension);

        //    factory.AddGbtSpTerms(mv1, mv2, metric);

        //    return factory.GetDgrMultivector();
        //}

        //public static GaNumDgrMultivector Lcp<T>(this GaNumDgrMultivector mv1, GaNumDgrMultivector mv2, GaBasesSignature metric)
        //{
        //    if (mv1.VSpaceDimension != mv2.VSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    var factory = new GaNumDgrMultivectorFactory(mv1.VSpaceDimension);

        //    factory.AddGbtLcpTerms(mv1, mv2, metric);

        //    return factory.GetDgrMultivector();
        //}

        //public static GaNumDgrMultivector Rcp<T>(this GaNumDgrMultivector mv1, GaNumDgrMultivector mv2, GaBasesSignature metric)
        //{
        //    if (mv1.VSpaceDimension != mv2.VSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    var factory = new GaNumDgrMultivectorFactory(mv1.VSpaceDimension);

        //    factory.AddGbtRcpTerms(mv1, mv2, metric);

        //    return factory.GetDgrMultivector();
        //}

        //public static GaNumDgrMultivector Fdp<T>(this GaNumDgrMultivector mv1, GaNumDgrMultivector mv2, GaBasesSignature metric)
        //{
        //    if (mv1.VSpaceDimension != mv2.VSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    var factory = new GaNumDgrMultivectorFactory(mv1.VSpaceDimension);

        //    factory.AddGbtFdpTerms(mv1, mv2, metric);

        //    return factory.GetDgrMultivector();
        //}

        //public static GaNumDgrMultivector Hip<T>(this GaNumDgrMultivector mv1, GaNumDgrMultivector mv2, GaBasesSignature metric)
        //{
        //    if (mv1.VSpaceDimension != mv2.VSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    var factory = new GaNumDgrMultivectorFactory(mv1.VSpaceDimension);

        //    factory.AddGbtHipTerms(mv1, mv2, metric);

        //    return factory.GetDgrMultivector();
        //}

        //public static GaNumDgrMultivector Acp<T>(this GaNumDgrMultivector mv1, GaNumDgrMultivector mv2, GaBasesSignature metric)
        //{
        //    if (mv1.VSpaceDimension != mv2.VSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    var factory = new GaNumDgrMultivectorFactory(mv1.VSpaceDimension);

        //    factory.AddGbtAcpTerms(mv1, mv2, metric);

        //    return factory.GetDgrMultivector();
        //}

        //public static GaNumDgrMultivector Cp<T>(this GaNumDgrMultivector mv1, GaNumDgrMultivector mv2, GaBasesSignature metric)
        //{
        //    if (mv1.VSpaceDimension != mv2.VSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    var factory = new GaNumDgrMultivectorFactory(mv1.VSpaceDimension);

        //    factory.AddGbtCpTerms(mv1, mv2, metric);

        //    return factory.GetDgrMultivector();
        //}

        //public static GaNumDgrMultivector Inverse<T>(this GaNumDgrMultivector mv, GaBasesSignature metric)
        //{
        //    return mv.GetGptInverseTerms(metric).SumAsDgrMultivector(mv.VSpaceDimension);
        //}
        //#endregion


        //#region Orthogonal metric bilinear products on Dense Array Represntation Multivectors
        //public static GaNumDarMultivector Gp<T>(this GaNumDarMultivector mv1, GaNumDarMultivector mv2, GaBasesSignature metric)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtGpTerms(mv2, metric).SumAsDarMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumDarMultivector Sp<T>(this GaNumDarMultivector mv1, GaNumDarMultivector mv2, GaBasesSignature metric)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtSpTerms(mv2, metric).SumAsDarMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumDarMultivector Lcp<T>(this GaNumDarMultivector mv1, GaNumDarMultivector mv2, GaBasesSignature metric)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtLcpTerms(mv2, metric).SumAsDarMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumDarMultivector Rcp<T>(this GaNumDarMultivector mv1, GaNumDarMultivector mv2, GaBasesSignature metric)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtRcpTerms(mv2, metric).SumAsDarMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumDarMultivector Fdp<T>(this GaNumDarMultivector mv1, GaNumDarMultivector mv2, GaBasesSignature metric)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtFdpTerms(mv2, metric).SumAsDarMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumDarMultivector Hip<T>(this GaNumDarMultivector mv1, GaNumDarMultivector mv2, GaBasesSignature metric)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtHipTerms(mv2, metric).SumAsDarMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumDarMultivector Acp<T>(this GaNumDarMultivector mv1, GaNumDarMultivector mv2, GaBasesSignature metric)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtAcpTerms(mv2, metric).SumAsDarMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumDarMultivector Cp<T>(this GaNumDarMultivector mv1, GaNumDarMultivector mv2, GaBasesSignature metric)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return mv1.GetGbtCpTerms(mv2, metric).SumAsDarMultivector(mv1.VSpaceDimension);
        //}

        //public static GaNumDarMultivector Inverse<T>(this GaNumDarMultivector mv, GaBasesSignature metric)
        //{
        //    return mv.GetGptInverseTerms(metric).SumAsDarMultivector(mv.VSpaceDimension);
        //}
        //#endregion
    }
}
