using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Basis;

namespace GeometricAlgebraFulcrumLib.Algebra.Signatures
{
    public sealed class GaSignatureLookup :
        IGaSignatureLookup
    {
        public static int MaxVSpaceDimension 
            => 10;


        private readonly SortedDictionary<int, int> _dataDictionary
            = new SortedDictionary<int, int>();


        public uint VSpaceDimension { get; }

        public ulong GaSpaceDimension 
            => 1UL << (int) VSpaceDimension;

        public ulong MaxBasisBladeId 
            => (1UL << (int) VSpaceDimension) - 1UL;

        public uint GradesCount 
            => VSpaceDimension + 1;

        public IEnumerable<uint> Grades 
            => GradesCount.GetRange();

        public uint SignatureId 
            => BaseSignature.SignatureId;
        
        public IGaSignatureComputed BaseSignature { get; }
        
        public uint PositiveCount 
            => BaseSignature.PositiveCount;

        public uint NegativeCount 
            => BaseSignature.NegativeCount;

        public uint ZeroCount 
            => BaseSignature.ZeroCount;

        public bool IsEuclidean { get; }

        public bool IsProjective { get; }
        
        public bool IsConformal { get; }

        public bool IsMotherAlgebra { get; }


        internal GaSignatureLookup([NotNull] IGaSignatureComputed baseSignature)
        {
            if (baseSignature.VSpaceDimension > MaxVSpaceDimension)
                throw new ArgumentOutOfRangeException();

            BaseSignature = baseSignature;
            IsEuclidean = baseSignature.IsEuclidean;
            IsProjective = baseSignature.IsProjective;
            IsConformal = baseSignature.IsConformal;
            IsMotherAlgebra = baseSignature.IsMotherAlgebra;
            VSpaceDimension = baseSignature.VSpaceDimension;

            Initialize(baseSignature);
        }


        private void Initialize(IGaSignature baseSignature)
        {
            for (var id1 = 0UL; id1 < GaSpaceDimension; id1++)
            {
                for (var id2 = 0UL; id2 < GaSpaceDimension; id2++)
                {
                    var gpSignature = 
                        1 + baseSignature.GpSignature(id1, id2);

                    var signature = gpSignature;

                    signature |= 
                        (1 + baseSignature.OpSignature(id1, id2)) << 2;
                    
                    signature |= 
                        (GaBasisUtils.IsNonZeroESp(id1, id2) ? gpSignature : 1) << 4;
                    
                    signature |= 
                        (GaBasisUtils.IsNonZeroELcp(id1, id2) ? gpSignature : 1) << 6;
                    
                    signature |= 
                        (GaBasisUtils.IsNonZeroERcp(id1, id2) ? gpSignature : 1) << 8;
                    
                    signature |= 
                        (GaBasisUtils.IsNonZeroEFdp(id1, id2) ? gpSignature : 1) << 10;
                    
                    signature |= 
                        (GaBasisUtils.IsNonZeroEHip(id1, id2) ? gpSignature : 1) << 12;
                    
                    signature |= 
                        (GaBasisUtils.IsNonZeroEAcp(id1, id2) ? gpSignature : 1) << 14;
                    
                    signature |= 
                        (GaBasisUtils.IsNonZeroECp(id1, id2) ? gpSignature : 1) << 16;

                    var key = (int) (id1 | (id2 << (int) VSpaceDimension));

                    _dataDictionary.Add(key, signature);
                }
            }
        }


        public int GetBasisVectorSignature(int index)
        {
            var id = 1 << index;
            var key = id | (id << (int) VSpaceDimension);

            return (_dataDictionary[key] & 3) - 1;
        }

        public int GetBasisVectorSignature(ulong index)
        {
            var id = 1 << (int) index;
            var key = id | (id << (int) VSpaceDimension);

            return (_dataDictionary[key] & 3) - 1;
        }

        public int GetBasisBivectorSignature(int index1, int index2)
        {
            var id = (1 << index1) ^ (1 << index2);
            var key = id | (id << (int) VSpaceDimension);

            return (_dataDictionary[key] & 3) - 1;
            //return BaseSignature.GetBasisBivectorSignature(index1, index2);
        }

        public int GetBasisBivectorSignature(ulong index1, ulong index2)
        {
            var id = (1 << (int) index1) ^ (1 << (int) index2);
            var key = id | (id << (int) VSpaceDimension);

            return (_dataDictionary[key] & 3) - 1;
            //return BaseSignature.GetBasisBivectorSignature(index1, index2);
        }

        public int GetBasisBladeSignature(uint grade, ulong index)
        {
            var id = GaBasisUtils.BasisBladeId(grade, index);
            var key = (int) (id | (id << (int) VSpaceDimension));

            return (_dataDictionary[key] & 3) - 1;
        }

        public int GetBasisBladeSignature(ulong id)
        {
            var key = (int) (id | (id << (int) VSpaceDimension));

            return (_dataDictionary[key] & 3) - 1;
        }

        public int GetBasisBladeSignature(IGaBasisBlade basisBlade)
        {
            var id = basisBlade.Id;
            var key = (int) (id | (id << (int) VSpaceDimension));

            return (_dataDictionary[key] & 3) - 1;
        }

        public int GpSignature(ulong id)
        {
            var key = (int) (id | (id << (int) VSpaceDimension));

            return (_dataDictionary[key] & 3) - 1;
        }


        public int GpSignature(ulong id1, ulong id2)
        {
            var key = (int) (id1 | (id2 << (int) VSpaceDimension));

            return (_dataDictionary[key] & 3) - 1;
        }

        public int GpReverseSignature(ulong id1, ulong id2)
        {
            var key = (int) (id1 | (id2 << (int) VSpaceDimension));

            var signature = (_dataDictionary[key] & 3) - 1;

            return id2.BasisBladeIdHasNegativeReverse() 
                ? -signature 
                : signature;
        }

        public int OpSignature(ulong id1, ulong id2)
        {
            var key = (int) (id1 | (id2 << (int) VSpaceDimension));

            return ((_dataDictionary[key] >> 2) & 3) - 1;
        }

        public int SpSignature(ulong id)
        {
            var key = (int) (id | (id << (int) VSpaceDimension));

            return ((_dataDictionary[key] >> 4) & 3) - 1;
        }

        public int SpSignature(ulong id1, ulong id2)
        {
            var key = (int) (id1 | (id2 << (int) VSpaceDimension));

            return ((_dataDictionary[key] >> 4) & 3) - 1;
        }

        public int NormSquaredSignature(ulong id)
        {
            var key = (int) (id | (id << (int) VSpaceDimension));

            var signature = ((_dataDictionary[key] >> 4) & 3) - 1;

            return id.BasisBladeIdHasNegativeReverse() 
                ? -signature 
                : signature;
        }

        public int LcpSignature(ulong id1, ulong id2)
        {
            var key = (int) (id1 | (id2 << (int) VSpaceDimension));

            return ((_dataDictionary[key] >> 6) & 3) - 1;
        }

        public int RcpSignature(ulong id1, ulong id2)
        {
            var key = (int) (id1 | (id2 << (int) VSpaceDimension));

            return ((_dataDictionary[key] >> 8) & 3) - 1;
        }

        public int FdpSignature(ulong id1, ulong id2)
        {
            var key = (int) (id1 | (id2 << (int) VSpaceDimension));

            return ((_dataDictionary[key] >> 10) & 3) - 1;
        }

        public int HipSignature(ulong id1, ulong id2)
        {
            var key = (int) (id1 | (id2 << (int) VSpaceDimension));

            return ((_dataDictionary[key] >> 12) & 3) - 1;
        }

        public int AcpSignature(ulong id1, ulong id2)
        {
            var key = (int) (id1 | (id2 << (int) VSpaceDimension));

            return ((_dataDictionary[key] >> 14) & 3) - 1;
        }

        public int CpSignature(ulong id1, ulong id2)
        {
            var key = (int) (id1 | (id2 << (int) VSpaceDimension));

            return ((_dataDictionary[key] >> 16) & 3) - 1;
        }



    }
}