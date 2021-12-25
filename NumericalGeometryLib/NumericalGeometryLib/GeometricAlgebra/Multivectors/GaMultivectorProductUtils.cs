using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.GeometricAlgebra.Basis;
using NumericalGeometryLib.GeometricAlgebra.GuidedBinaryTraversal;
using NumericalGeometryLib.GeometricAlgebra.Structures;

namespace NumericalGeometryLib.GeometricAlgebra.Multivectors
{
    public static class GaMultivectorProductUtils
    {
        private static GaBasisBladeDataLookup Lookup 
            => GaBasisBladeDataLookup.Default;


        /// <summary>
        /// True if the outer product of the given euclidean basis blades is zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroOp(ulong id1, ulong id2)
        {
            return (id1 & id2) != 0UL;
        }

        /// <summary>
        /// True if the outer product of the given euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroOp(ulong id1, ulong id2)
        {
            return (id1 & id2) == 0UL;
        }

        /// <summary>
        /// True if the Euclidean geometric product of the given euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroEGp(ulong id1, ulong id2)
        {
            return false;
        }

        /// <summary>
        /// True if the Euclidean geometric product of the given euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroEGp(ulong id1, ulong id2)
        {
            return true;
        }

        /// <summary>
        /// True if the scalar product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroESp(ulong id1, ulong id2)
        {
            return id1 != id2;
        }

        /// <summary>
        /// True if the scalar product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroESp(ulong id1, ulong id2)
        {
            return id1 == id2;
        }

        /// <summary>
        /// True if the left contraction product of the given Euclidean basis blades is zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroELcp(ulong id1, ulong id2)
        {
            return (id1 & ~id2) != 0UL;
        }

        /// <summary>
        /// True if the left contraction product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroELcp(ulong id1, ulong id2)
        {
            return (id1 & ~id2) == 0UL;
        }

        /// <summary>
        /// True if the right contraction product of the given Euclidean basis blades is zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroERcp(ulong id1, ulong id2)
        {
            return (id2 & ~id1) != 0UL;
        }

        /// <summary>
        /// True if the right contraction product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroERcp(ulong id1, ulong id2)
        {
            return (id2 & ~id1) == 0UL;
        }

        /// <summary>
        /// True if the fat-dot product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroEFdp(ulong id1, ulong id2)
        {
            return (id1 & ~id2) != 0UL && (id2 & ~id1) != 0UL;
        }

        /// <summary>
        /// True if the fat-dot product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroEFdp(ulong id1, ulong id2)
        {
            return (id1 & ~id2) == 0UL || (id2 & ~id1) == 0UL;
        }

        /// <summary>
        /// True if the Hestenes inner product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroEHip(ulong id1, ulong id2)
        {
            return id1 == 0UL || 
                   id2 == 0UL || 
                   ((id1 & ~id2) != 0UL && (id2 & ~id1) != 0UL);
        }

        /// <summary>
        /// True if the Hestenes inner product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroEHip(ulong id1, ulong id2)
        {
            return id1 != 0UL && 
                   id2 != 0UL && 
                   ((id1 & ~id2) == 0UL || (id2 & ~id1) == 0UL);
        }

        /// <summary>
        /// True if the anti-commutator product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroEAcp(ulong id1, ulong id2)
        {
            //A acp B = (AB + BA) / 2
            return Lookup.IsNegativeEGp(id1, id2) != 
                   Lookup.IsNegativeEGp(id2, id1);
        }

        /// <summary>
        /// True if the anti-commutator product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroEAcp(ulong id1, ulong id2)
        {
            //A acp B = (AB + BA) / 2
            return Lookup.IsNegativeEGp(id1, id2) == 
                   Lookup.IsNegativeEGp(id2, id1);
        }

        /// <summary>
        /// True if the commutator product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroECp(ulong id1, ulong id2)
        {
            //A cp B = (AB - BA) / 2
            return Lookup.IsNegativeEGp(id1, id2) == 
                   Lookup.IsNegativeEGp(id2, id1);
        }

        /// <summary>
        /// True if the commutator product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroECp(ulong id1, ulong id2)
        {
            //A cp B = (AB - BA) / 2
            return Lookup.IsNegativeEGp(id1, id2) != 
                   Lookup.IsNegativeEGp(id2, id1);
        }


        public static IEnumerable<double> GetESpSquaredScalars(this GaBasisSet basisSet, GaMultivectorSparseList mvList1)
        {
            foreach (var (id, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.ESpSquaredSignature(id);

                yield return signature * scalar1 * scalar1;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<double> GetENormSquaredScalars(this GaBasisSet basisSet, GaMultivectorSparseList mvList1)
        {
            return mvList1.StoredNumbers.Select(scalar => scalar * scalar);
        }
        
        public static IEnumerable<double> GetESpScalars(this GaBasisSet basisSet, GaMultivectorSparseList mvList1, GaMultivectorSparseList mvList2)
        {
            if (mvList1.Count <= mvList2.Count)
            {
                foreach (var (id, scalar1) in mvList1.StoredIdNumberPairs)
                {
                    var signature = basisSet.ESpSquaredSignature(id);

                    yield return signature * scalar1 * mvList2[id];
                }
            }
            else
            {
                foreach (var (id, scalar2) in mvList2.StoredIdNumberPairs)
                {
                    var signature = basisSet.ESpSquaredSignature(id);

                    yield return signature * mvList1[id] * scalar2;
                }
            }
        }

        public static IEnumerable<GaIdScalarRecord> GetOpIdScalarRecords(this GaBasisSet basisSet, GaMultivectorSparseList mvList1, GaTerm mv2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.OpSignature(id1, mv2.Id);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    id1 ^ mv2.Id,
                    signature * scalar1 * mv2.Scalar
                );
            }
        }
        
        public static IEnumerable<GaIdScalarRecord> GetOpIdScalarRecords(this GaBasisSet basisSet, GaTerm mv1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.OpSignature(mv1.Id, id2);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    mv1.Id ^ id2,
                    signature * mv1.Scalar * scalar2
                );
            }
        }

        public static IEnumerable<GaIdScalarRecord> GetOpIdScalarRecords(this GaBasisSet basisSet, GaMultivectorSparseList mvList1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.OpSignature(id1, id2);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    id1 ^ id2,
                    signature * scalar1 * scalar2
                );
            }
        }
        
        public static IEnumerable<GaIdScalarRecord> GetELcpIdScalarRecords(this GaBasisSet basisSet, GaMultivectorSparseList mvList1, GaTerm mv2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.ELcpSignature(id1, mv2.Id);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    id1 ^ mv2.Id,
                    signature * scalar1 * mv2.Scalar
                );
            }
        }
        
        public static IEnumerable<GaIdScalarRecord> GetELcpIdScalarRecords(this GaBasisSet basisSet, GaTerm mv1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.ELcpSignature(mv1.Id, id2);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    mv1.Id ^ id2,
                    signature * mv1.Scalar * scalar2
                );
            }
        }

        public static IEnumerable<GaIdScalarRecord> GetELcpIdScalarRecords(this GaBasisSet basisSet, GaMultivectorSparseList mvList1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.ELcpSignature(id1, id2);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    id1 ^ id2,
                    signature * scalar1 * scalar2
                );
            }
        }
        
        public static IEnumerable<GaIdScalarRecord> GetERcpIdScalarRecords(this GaBasisSet basisSet, GaMultivectorSparseList mvList1, GaTerm mv2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.ERcpSignature(id1, mv2.Id);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    id1 ^ mv2.Id,
                    signature * scalar1 * mv2.Scalar
                );
            }
        }
        
        public static IEnumerable<GaIdScalarRecord> GetERcpIdScalarRecords(this GaBasisSet basisSet, GaTerm mv1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.ERcpSignature(mv1.Id, id2);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    mv1.Id ^ id2,
                    signature * mv1.Scalar * scalar2
                );
            }
        }

        public static IEnumerable<GaIdScalarRecord> GetERcpIdScalarRecords(this GaBasisSet basisSet, GaMultivectorSparseList mvList1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.ERcpSignature(id1, id2);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    id1 ^ id2,
                    signature * scalar1 * scalar2
                );
            }
        }
        
        public static IEnumerable<GaIdScalarRecord> GetEFdpIdScalarRecords(this GaBasisSet basisSet, GaMultivectorSparseList mvList1, GaTerm mv2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.EFdpSignature(id1, mv2.Id);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    id1 ^ mv2.Id,
                    signature * scalar1 * mv2.Scalar
                );
            }
        }
        
        public static IEnumerable<GaIdScalarRecord> GetEFdpIdScalarRecords(this GaBasisSet basisSet, GaTerm mv1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.EFdpSignature(mv1.Id, id2);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    mv1.Id ^ id2,
                    signature * mv1.Scalar * scalar2
                );
            }
        }

        public static IEnumerable<GaIdScalarRecord> GetEFdpIdScalarRecords(this GaBasisSet basisSet, GaMultivectorSparseList mvList1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.EFdpSignature(id1, id2);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    id1 ^ id2,
                    signature * scalar1 * scalar2
                );
            }
        }
        
        public static IEnumerable<GaIdScalarRecord> GetEHipIdScalarRecords(this GaBasisSet basisSet, GaMultivectorSparseList mvList1, GaTerm mv2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.EHipSignature(id1, mv2.Id);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    id1 ^ mv2.Id,
                    signature * scalar1 * mv2.Scalar
                );
            }
        }
        
        public static IEnumerable<GaIdScalarRecord> GetEHipIdScalarRecords(this GaBasisSet basisSet, GaTerm mv1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.EHipSignature(mv1.Id, id2);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    mv1.Id ^ id2,
                    signature * mv1.Scalar * scalar2
                );
            }
        }

        public static IEnumerable<GaIdScalarRecord> GetEHipIdScalarRecords(this GaBasisSet basisSet, GaMultivectorSparseList mvList1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.EHipSignature(id1, id2);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    id1 ^ id2,
                    signature * scalar1 * scalar2
                );
            }
        }
        
        public static IEnumerable<GaIdScalarRecord> GetEAcpIdScalarRecords(this GaBasisSet basisSet, GaMultivectorSparseList mvList1, GaTerm mv2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.EAcpSignature(id1, mv2.Id);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    id1 ^ mv2.Id,
                    signature * scalar1 * mv2.Scalar
                );
            }
        }
        
        public static IEnumerable<GaIdScalarRecord> GetEAcpIdScalarRecords(this GaBasisSet basisSet, GaTerm mv1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.EAcpSignature(mv1.Id, id2);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    mv1.Id ^ id2,
                    signature * mv1.Scalar * scalar2
                );
            }
        }

        public static IEnumerable<GaIdScalarRecord> GetEAcpIdScalarRecords(this GaBasisSet basisSet, GaMultivectorSparseList mvList1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.EAcpSignature(id1, id2);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    id1 ^ id2,
                    signature * scalar1 * scalar2
                );
            }
        }
        
        public static IEnumerable<GaIdScalarRecord> GetECpIdScalarRecords(this GaBasisSet basisSet, GaMultivectorSparseList mvList1, GaTerm mv2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.ECpSignature(id1, mv2.Id);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    id1 ^ mv2.Id,
                    signature * scalar1 * mv2.Scalar
                );
            }
        }
        
        public static IEnumerable<GaIdScalarRecord> GetECpIdScalarRecords(this GaBasisSet basisSet, GaTerm mv1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.ECpSignature(mv1.Id, id2);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    mv1.Id ^ id2,
                    signature * mv1.Scalar * scalar2
                );
            }
        }

        public static IEnumerable<GaIdScalarRecord> GetECpIdScalarRecords(this GaBasisSet basisSet, GaMultivectorSparseList mvList1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.ECpSignature(id1, id2);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    id1 ^ id2,
                    signature * scalar1 * scalar2
                );
            }
        }
        
        public static IEnumerable<GaIdScalarRecord> GetEGpIdScalarRecords(this GaBasisSet basisSet, GaMultivectorSparseList mvList1, GaTerm mv2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.EGpSignature(id1, mv2.Id);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    id1 ^ mv2.Id,
                    signature * scalar1 * mv2.Scalar
                );
            }
        }
        
        public static IEnumerable<GaIdScalarRecord> GetEGpIdScalarRecords(this GaBasisSet basisSet, GaTerm mv1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.EGpSignature(mv1.Id, id2);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    mv1.Id ^ id2,
                    signature * mv1.Scalar * scalar2
                );
            }
        }

        public static IEnumerable<GaIdScalarRecord> GetEGpIdScalarRecords(this GaBasisSet basisSet, GaMultivectorSparseList mvSparseList1, GaMultivectorSparseList mvSparseList2)
        {
            foreach (var (id1, scalar1) in mvSparseList1.StoredIdNumberPairs)
            foreach (var (id2, scalar2) in mvSparseList2.StoredIdNumberPairs)
            {
                var signature = basisSet.EGpSignature(id1, id2);

                yield return new GaIdScalarRecord(
                    id1 ^ id2,
                    signature * scalar1 * scalar2
                );
            }
        }
        
        public static IEnumerable<GaIdScalarRecord> GetEGpReverseIdScalarRecords(this GaBasisSet basisSet, GaMultivectorSparseList mvList1, GaTerm mv2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.EGpReverseSignature(id1, mv2.Id);

                yield return new GaIdScalarRecord(
                    id1 ^ mv2.Id,
                    signature * scalar1 * mv2.Scalar
                );
            }
        }
        
        public static IEnumerable<GaIdScalarRecord> GetEGpReverseIdScalarRecords(this GaBasisSet basisSet, GaTerm mv1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.EGpReverseSignature(mv1.Id, id2);

                yield return new GaIdScalarRecord(
                    mv1.Id ^ id2,
                    signature * mv1.Scalar * scalar2
                );
            }
        }

        public static IEnumerable<GaIdScalarRecord> GetEGpReverseIdScalarRecords(this GaBasisSet basisSet, GaMultivectorSparseList mvList1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.EGpReverseSignature(id1, id2);

                yield return new GaIdScalarRecord(
                    id1 ^ id2,
                    signature * scalar1 * scalar2
                );
            }
        }


        public static IEnumerable<double> GetSpSquaredScalars(this GaBasisSet basisSet, GaMultivectorSparseList mvList1)
        {
            foreach (var (id, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.SpSquaredSignature(id);

                if (signature == 0) continue;

                yield return signature * scalar1 * scalar1;
            }
        }
        
        public static IEnumerable<double> GetNormSquaredScalars(this GaBasisSet basisSet, GaMultivectorSparseList mvList1)
        {
            foreach (var (id, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.NormSquaredSignature(id);

                if (signature == 0) continue;

                yield return signature * scalar1 * scalar1;
            }
        }

        public static IEnumerable<double> GetSpScalars(this GaBasisSet basisSet, GaMultivectorSparseList mvList1, GaMultivectorSparseList mvList2)
        {
            if (mvList1.Count <= mvList2.Count)
            {
                foreach (var (id, scalar1) in mvList1.StoredIdNumberPairs)
                {
                    var signature = basisSet.SpSquaredSignature(id);

                    if (signature == 0) continue;

                    yield return signature * scalar1 * mvList2[id];
                }
            }
            else
            {
                foreach (var (id, scalar2) in mvList2.StoredIdNumberPairs)
                {
                    var signature = basisSet.SpSquaredSignature(id);

                    if (signature == 0) continue;

                    yield return signature * mvList1[id] * scalar2;
                }
            }
        }
        
        public static IEnumerable<GaIdScalarRecord> GetLcpIdScalarRecords(this GaBasisSet basisSet, GaMultivectorSparseList mvList1, GaTerm mv2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.LcpSignature(id1, mv2.Id);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    id1 ^ mv2.Id,
                    signature * scalar1 * mv2.Scalar
                );
            }
        }
        
        public static IEnumerable<GaIdScalarRecord> GetLcpIdScalarRecords(this GaBasisSet basisSet, GaTerm mv1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.LcpSignature(mv1.Id, id2);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    mv1.Id ^ id2,
                    signature * mv1.Scalar * scalar2
                );
            }
        }

        public static IEnumerable<GaIdScalarRecord> GetLcpIdScalarRecords(this GaBasisSet basisSet, GaMultivectorSparseList mvList1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.LcpSignature(id1, id2);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    id1 ^ id2,
                    signature * scalar1 * scalar2
                );
            }
        }
        
        public static IEnumerable<GaIdScalarRecord> GetRcpIdScalarRecords(this GaBasisSet basisSet, GaMultivectorSparseList mvList1, GaTerm mv2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.RcpSignature(id1, mv2.Id);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    id1 ^ mv2.Id,
                    signature * scalar1 * mv2.Scalar
                );
            }
        }
        
        public static IEnumerable<GaIdScalarRecord> GetRcpIdScalarRecords(this GaBasisSet basisSet, GaTerm mv1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.RcpSignature(mv1.Id, id2);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    mv1.Id ^ id2,
                    signature * mv1.Scalar * scalar2
                );
            }
        }

        public static IEnumerable<GaIdScalarRecord> GetRcpIdScalarRecords(this GaBasisSet basisSet, GaMultivectorSparseList mvList1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.RcpSignature(id1, id2);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    id1 ^ id2,
                    signature * scalar1 * scalar2
                );
            }
        }
        
        public static IEnumerable<GaIdScalarRecord> GetFdpIdScalarRecords(this GaBasisSet basisSet, GaMultivectorSparseList mvList1, GaTerm mv2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.FdpSignature(id1, mv2.Id);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    id1 ^ mv2.Id,
                    signature * scalar1 * mv2.Scalar
                );
            }
        }
        
        public static IEnumerable<GaIdScalarRecord> GetFdpIdScalarRecords(this GaBasisSet basisSet, GaTerm mv1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.FdpSignature(mv1.Id, id2);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    mv1.Id ^ id2,
                    signature * mv1.Scalar * scalar2
                );
            }
        }

        public static IEnumerable<GaIdScalarRecord> GetFdpIdScalarRecords(this GaBasisSet basisSet, GaMultivectorSparseList mvList1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.FdpSignature(id1, id2);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    id1 ^ id2,
                    signature * scalar1 * scalar2
                );
            }
        }
        
        public static IEnumerable<GaIdScalarRecord> GetHipIdScalarRecords(this GaBasisSet basisSet, GaMultivectorSparseList mvList1, GaTerm mv2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.HipSignature(id1, mv2.Id);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    id1 ^ mv2.Id,
                    signature * scalar1 * mv2.Scalar
                );
            }
        }
        
        public static IEnumerable<GaIdScalarRecord> GetHipIdScalarRecords(this GaBasisSet basisSet, GaTerm mv1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.HipSignature(mv1.Id, id2);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    mv1.Id ^ id2,
                    signature * mv1.Scalar * scalar2
                );
            }
        }

        public static IEnumerable<GaIdScalarRecord> GetHipIdScalarRecords(this GaBasisSet basisSet, GaMultivectorSparseList mvList1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.HipSignature(id1, id2);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    id1 ^ id2,
                    signature * scalar1 * scalar2
                );
            }
        }
        
        public static IEnumerable<GaIdScalarRecord> GetAcpIdScalarRecords(this GaBasisSet basisSet, GaMultivectorSparseList mvList1, GaTerm mv2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.AcpSignature(id1, mv2.Id);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    id1 ^ mv2.Id,
                    signature * scalar1 * mv2.Scalar
                );
            }
        }
        
        public static IEnumerable<GaIdScalarRecord> GetAcpIdScalarRecords(this GaBasisSet basisSet, GaTerm mv1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.AcpSignature(mv1.Id, id2);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    mv1.Id ^ id2,
                    signature * mv1.Scalar * scalar2
                );
            }
        }

        public static IEnumerable<GaIdScalarRecord> GetAcpIdScalarRecords(this GaBasisSet basisSet, GaMultivectorSparseList mvList1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.AcpSignature(id1, id2);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    id1 ^ id2,
                    signature * scalar1 * scalar2
                );
            }
        }
        
        public static IEnumerable<GaIdScalarRecord> GetCpIdScalarRecords(this GaBasisSet basisSet, GaMultivectorSparseList mvList1, GaTerm mv2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.CpSignature(id1, mv2.Id);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    id1 ^ mv2.Id,
                    signature * scalar1 * mv2.Scalar
                );
            }
        }
        
        public static IEnumerable<GaIdScalarRecord> GetCpIdScalarRecords(this GaBasisSet basisSet, GaTerm mv1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.CpSignature(mv1.Id, id2);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    mv1.Id ^ id2,
                    signature * mv1.Scalar * scalar2
                );
            }
        }

        public static IEnumerable<GaIdScalarRecord> GetCpIdScalarRecords(this GaBasisSet basisSet, GaMultivectorSparseList mvList1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.CpSignature(id1, id2);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    id1 ^ id2,
                    signature * scalar1 * scalar2
                );
            }
        }
        
        public static IEnumerable<GaIdScalarRecord> GetGpIdScalarRecords(this GaBasisSet basisSet, GaMultivectorSparseList mvList1, GaTerm mv2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.GpSignature(id1, mv2.Id);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    id1 ^ mv2.Id,
                    signature * scalar1 * mv2.Scalar
                );
            }
        }
        
        public static IEnumerable<GaIdScalarRecord> GetGpIdScalarRecords(this GaBasisSet basisSet, GaTerm mv1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.GpSignature(mv1.Id, id2);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    mv1.Id ^ id2,
                    signature * mv1.Scalar * scalar2
                );
            }
        }

        public static IEnumerable<GaIdScalarRecord> GetGpIdScalarRecords(this GaBasisSet basisSet, GaMultivectorSparseList mvSparseList1, GaMultivectorSparseList mvSparseList2)
        {
            foreach (var (id1, scalar1) in mvSparseList1.StoredIdNumberPairs)
            foreach (var (id2, scalar2) in mvSparseList2.StoredIdNumberPairs)
            {
                var signature = basisSet.GpSignature(id1, id2);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    id1 ^ id2,
                    signature * scalar1 * scalar2
                );
            }
        }
        
        public static IEnumerable<GaIdScalarRecord> GetGpReverseIdScalarRecords(this GaBasisSet basisSet, GaMultivectorSparseList mvList1, GaTerm mv2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.GpReverseSignature(id1, mv2.Id);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    id1 ^ mv2.Id,
                    signature * scalar1 * mv2.Scalar
                );
            }
        }
        
        public static IEnumerable<GaIdScalarRecord> GetGpReverseIdScalarRecords(this GaBasisSet basisSet, GaTerm mv1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.GpReverseSignature(mv1.Id, id2);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    mv1.Id ^ id2,
                    signature * mv1.Scalar * scalar2
                );
            }
        }

        public static IEnumerable<GaIdScalarRecord> GetGpReverseIdScalarRecords(this GaBasisSet basisSet, GaMultivectorSparseList mvList1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.GpReverseSignature(id1, id2);

                if (signature == 0) continue;

                yield return new GaIdScalarRecord(
                    id1 ^ id2,
                    signature * scalar1 * scalar2
                );
            }
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaIdScalarRecord> GetOpIdScalarRecords(this GaBasisSet basisSet, GaMultivectorBinaryTrie mvBinaryTrie1, GaMultivectorBinaryTrie mvBinaryTrie2)
        {
            return basisSet
                .CreateGbtProductsStack(mvBinaryTrie1, mvBinaryTrie2)
                .GetOpIdScalarRecords();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<double> GetESpScalars(this GaBasisSet basisSet, GaMultivectorBinaryTrie mvBinaryTrie1, GaMultivectorBinaryTrie mvBinaryTrie2)
        {
            return basisSet
                .CreateGbtProductsStack(mvBinaryTrie1, mvBinaryTrie2)
                .GetESpScalars();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaIdScalarRecord> GetELcpIdScalarRecords(this GaBasisSet basisSet, GaMultivectorBinaryTrie mvBinaryTrie1, GaMultivectorBinaryTrie mvBinaryTrie2)
        {
            return basisSet
                .CreateGbtProductsStack(mvBinaryTrie1, mvBinaryTrie2)
                .GetELcpIdScalarRecords();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaIdScalarRecord> GetERcpIdScalarRecords(this GaBasisSet basisSet, GaMultivectorBinaryTrie mvBinaryTrie1, GaMultivectorBinaryTrie mvBinaryTrie2)
        {
            return basisSet
                .CreateGbtProductsStack(mvBinaryTrie1, mvBinaryTrie2)
                .GetERcpIdScalarRecords();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaIdScalarRecord> GetEFdpIdScalarRecords(this GaBasisSet basisSet, GaMultivectorBinaryTrie mvBinaryTrie1, GaMultivectorBinaryTrie mvBinaryTrie2)
        {
            return basisSet
                .CreateGbtProductsStack(mvBinaryTrie1, mvBinaryTrie2)
                .GetEFdpIdScalarRecords();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaIdScalarRecord> GetEHipIdScalarRecords(this GaBasisSet basisSet, GaMultivectorBinaryTrie mvBinaryTrie1, GaMultivectorBinaryTrie mvBinaryTrie2)
        {
            return basisSet
                .CreateGbtProductsStack(mvBinaryTrie1, mvBinaryTrie2)
                .GetEHipIdScalarRecords();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaIdScalarRecord> GetEAcpIdScalarRecords(this GaBasisSet basisSet, GaMultivectorBinaryTrie mvBinaryTrie1, GaMultivectorBinaryTrie mvBinaryTrie2)
        {
            return basisSet
                .CreateGbtProductsStack(mvBinaryTrie1, mvBinaryTrie2)
                .GetEAcpIdScalarRecords();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaIdScalarRecord> GetECpIdScalarRecords(this GaBasisSet basisSet, GaMultivectorBinaryTrie mvBinaryTrie1, GaMultivectorBinaryTrie mvBinaryTrie2)
        {
            return basisSet
                .CreateGbtProductsStack(mvBinaryTrie1, mvBinaryTrie2)
                .GetECpIdScalarRecords();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaIdScalarRecord> GetEGpIdScalarRecords(this GaBasisSet basisSet, GaMultivectorBinaryTrie mvBinaryTrie1, GaMultivectorBinaryTrie mvBinaryTrie2)
        {
            return basisSet
                .CreateGbtProductsStack(mvBinaryTrie1, mvBinaryTrie2)
                .GetEGpIdScalarRecords();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<double> GetSpScalars(this GaBasisSet basisSet, GaMultivectorBinaryTrie mvBinaryTrie1, GaMultivectorBinaryTrie mvBinaryTrie2)
        {
            return basisSet
                .CreateGbtProductsStack(mvBinaryTrie1, mvBinaryTrie2)
                .GetSpScalars();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaIdScalarRecord> GetLcpIdScalarRecords(this GaBasisSet basisSet, GaMultivectorBinaryTrie mvBinaryTrie1, GaMultivectorBinaryTrie mvBinaryTrie2)
        {
            return basisSet
                .CreateGbtProductsStack(mvBinaryTrie1, mvBinaryTrie2)
                .GetLcpIdScalarRecords();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaIdScalarRecord> GetRcpIdScalarRecords(this GaBasisSet basisSet, GaMultivectorBinaryTrie mvBinaryTrie1, GaMultivectorBinaryTrie mvBinaryTrie2)
        {
            return basisSet
                .CreateGbtProductsStack(mvBinaryTrie1, mvBinaryTrie2)
                .GetRcpIdScalarRecords();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaIdScalarRecord> GetFdpIdScalarRecords(this GaBasisSet basisSet, GaMultivectorBinaryTrie mvBinaryTrie1, GaMultivectorBinaryTrie mvBinaryTrie2)
        {
            return basisSet
                .CreateGbtProductsStack(mvBinaryTrie1, mvBinaryTrie2)
                .GetFdpIdScalarRecords();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaIdScalarRecord> GetHipIdScalarRecords(this GaBasisSet basisSet, GaMultivectorBinaryTrie mvBinaryTrie1, GaMultivectorBinaryTrie mvBinaryTrie2)
        {
            return basisSet
                .CreateGbtProductsStack(mvBinaryTrie1, mvBinaryTrie2)
                .GetHipIdScalarRecords();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaIdScalarRecord> GetAcpIdScalarRecords(this GaBasisSet basisSet, GaMultivectorBinaryTrie mvBinaryTrie1, GaMultivectorBinaryTrie mvBinaryTrie2)
        {
            return basisSet
                .CreateGbtProductsStack(mvBinaryTrie1, mvBinaryTrie2)
                .GetAcpIdScalarRecords();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaIdScalarRecord> GetCpIdScalarRecords(this GaBasisSet basisSet, GaMultivectorBinaryTrie mvBinaryTrie1, GaMultivectorBinaryTrie mvBinaryTrie2)
        {
            return basisSet
                .CreateGbtProductsStack(mvBinaryTrie1, mvBinaryTrie2)
                .GetCpIdScalarRecords();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaIdScalarRecord> GetGpIdScalarRecords(this GaBasisSet basisSet, GaMultivectorBinaryTrie mvBinaryTrie1, GaMultivectorBinaryTrie mvBinaryTrie2)
        {
            return basisSet
                .CreateGbtProductsStack(mvBinaryTrie1, mvBinaryTrie2)
                .GetGpIdScalarRecords();
        }
    }
}