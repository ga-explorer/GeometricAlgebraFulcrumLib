using System;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib;

namespace GeometricAlgebraLib.Frames
{
    public sealed class GaOrthonormalBasesSignature
    {
        public static GaOrthonormalBasesSignature CreateEuclidean(int positiveBasisCount)
        {
            return new(positiveBasisCount, 0, 0);
        }

        public static GaOrthonormalBasesSignature CreateConformal(int positiveBasisCount)
        {
            return new(positiveBasisCount + 1, 1, 0);
        }

        public static GaOrthonormalBasesSignature CreateProjective(int positiveBasisCount)
        {
            return new(positiveBasisCount, 0, 1);
        }

        public static GaOrthonormalBasesSignature Create(int positiveBasisCount, int negativeBasisCount)
        {
            return new(positiveBasisCount, negativeBasisCount, 0);
        }

        public static GaOrthonormalBasesSignature Create(int positiveBasisCount, int negativeBasisCount, int zeroBasisCount)
        {
            return new(positiveBasisCount, negativeBasisCount, zeroBasisCount);
        }


        private ulong PositiveBasisMask { get; }

        private ulong NegativeBasisMask { get; }

        private ulong ZeroBasisMask { get; }


        public int PositiveBasisCount { get; }

        public int NegativeBasisCount { get; }

        public int ZeroBasisCount { get; }

        public int VSpaceDimension { get; }

        public IReadOnlyList<int> BasisVectorSignatures { get; }


        private GaOrthonormalBasesSignature(int positiveBasisCount, int negativeBasisCount, int zeroBasisCount)
        {
            if (positiveBasisCount < 0 || negativeBasisCount < 0 || zeroBasisCount < 0)
                throw new ArgumentOutOfRangeException();

            if (positiveBasisCount + negativeBasisCount + zeroBasisCount < 2)
                throw new ArgumentOutOfRangeException();

            if (positiveBasisCount + negativeBasisCount + zeroBasisCount > 64)
                throw new ArgumentOutOfRangeException();

            PositiveBasisCount = positiveBasisCount;
            NegativeBasisCount = negativeBasisCount;
            ZeroBasisCount = zeroBasisCount;
            VSpaceDimension = PositiveBasisCount + NegativeBasisCount + ZeroBasisCount;

            PositiveBasisMask = PositiveBasisCount > 0 
                ? UnsignedLongBitUtils.CreateFullMask(PositiveBasisCount)
                : 0UL;

            NegativeBasisMask = NegativeBasisCount > 0
                ? UnsignedLongBitUtils.CreateFullMask(NegativeBasisCount) << PositiveBasisCount
                : 0UL ;

            ZeroBasisMask = ZeroBasisCount > 0
                ? UnsignedLongBitUtils.CreateFullMask(ZeroBasisCount) << (PositiveBasisCount + NegativeBasisCount)
                : 0UL;

            BasisVectorSignatures = Enumerable
                .Range(0, VSpaceDimension)
                .Select(GetBasisVectorSignatureFromIndex)
                .ToArray();
        }


        private int GetBasisVectorSignatureFromIndex(int index)
        {
            if (index < PositiveBasisCount)
                return 1;

            index -= PositiveBasisCount;

            if (index < NegativeBasisCount)
                return -1;

            return 0;
        }

        public int GetBasisBivectorSignature(int index1, int index2)
        {
            var id = (1UL << index1) | (1UL << index2);

            if ((id & ZeroBasisMask) != 0UL)
                return 0;

            var negativeBasisCount = 
                (id & NegativeBasisMask).BasisBladeGrade();

            return (negativeBasisCount & 1) == 0 ? 1 : -1;
        }

        public int GetBasisBivectorSignature(ulong index1, ulong index2)
        {
            var id = (1UL << (int)index1) | (1UL << (int)index2);

            if ((id & ZeroBasisMask) != 0UL)
                return 0;

            var negativeBasisCount = 
                (id & NegativeBasisMask).BasisBladeGrade();

            return (negativeBasisCount & 1) == 0 ? 1 : -1;
        }

        public int GetBasisBladeSignature(int grade, ulong index)
        {
            var id = GaFrameUtils.BasisBladeId(grade, index);

            if ((id & ZeroBasisMask) != 0UL)
                return 0;

            var negativeBasisCount = 
                (id & NegativeBasisMask).BasisBladeGrade();

            return (negativeBasisCount & 1) == 0 ? 1 : -1;
        }

        public int GetBasisBladeSignature(ulong id)
        {
            if ((id & ZeroBasisMask) != 0UL)
                return 0;

            var negativeBasisCount = 
                (id & NegativeBasisMask).BasisBladeGrade();

            return (negativeBasisCount & 1) == 0 ? 1 : -1;
        }


        public int GeometricProductSignature(ulong id)
        {
            if ((id & ZeroBasisMask) != 0UL)
                return 0;

            var negativeBasisCount = 
                (id & NegativeBasisMask).BasisBladeGrade();

            return ((negativeBasisCount & 1) == 0) 
                ? GaFrameUtils.EGpSignature(id, id)
                : -GaFrameUtils.EGpSignature(id, id);
        }

        public int GeometricProductSignature(ulong id1, ulong id2)
        {
            var commonBasisBladesId = id1 & id2;

            if ((commonBasisBladesId & ZeroBasisMask) != 0UL)
                return 0;

            var negativeBasisCount = 
                (commonBasisBladesId & NegativeBasisMask).BasisBladeGrade();

            return ((negativeBasisCount & 1) == 0) 
                ? GaFrameUtils.EGpSignature(id1, id2)
                : -GaFrameUtils.EGpSignature(id1, id2);
        }

        public GaGeometricProductOfBasesResult GeometricProduct(ulong id1, ulong id2)
        {
            var gpId = id1 ^ id2;
            var commonBasisBladesId = id1 & id2;

            if ((commonBasisBladesId & ZeroBasisMask) != 0UL)
                return new GaGeometricProductOfBasesResult(0, gpId);

            var negativeBasisCount = 
                (commonBasisBladesId & NegativeBasisMask).BasisBladeGrade();

            var signature = ((negativeBasisCount & 1) == 0) 
                ? GaFrameUtils.EGpSignature(id1, id2)
                : -GaFrameUtils.EGpSignature(id1, id2);

            return new GaGeometricProductOfBasesResult(signature, gpId);
        }
    }
}
