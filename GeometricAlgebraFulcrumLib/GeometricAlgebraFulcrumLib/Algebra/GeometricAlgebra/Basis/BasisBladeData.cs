using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis
{
    internal sealed record BasisBladeData
    {
        private readonly BitArray _egpIsNegativeBitArray;
        private readonly BitVector32 _unaryOperationsIsNegativeBitVector;


        public ulong Id { get; }

        public uint Grade { get; }

        public ulong Index { get; }

        public GradeIndexRecord GradeIndex 
            => new GradeIndexRecord(Grade, Index);

        public bool GradeInvolutionIsNegative 
            => _unaryOperationsIsNegativeBitVector[1];

        public bool GradeInvolutionIsPositive 
            => !_unaryOperationsIsNegativeBitVector[1];
        
        public int GradeInvolutionSign 
            => GradeInvolutionIsNegative ? -1 : 1;

        public bool ReverseIsNegative 
            => _unaryOperationsIsNegativeBitVector[2];

        public bool ReverseIsPositive 
            => !_unaryOperationsIsNegativeBitVector[2];
        
        public int ReverseSign 
            => ReverseIsNegative ? -1 : 1;

        public bool CliffordConjugateIsNegative 
            => _unaryOperationsIsNegativeBitVector[4];

        public bool CliffordConjugateIsPositive
            => !_unaryOperationsIsNegativeBitVector[4];
        
        public int CliffordConjugateSign 
            => CliffordConjugateIsNegative ? -1 : 1;

        public bool EGpSquaredIsNegative 
            => _unaryOperationsIsNegativeBitVector[8];

        public bool EGpSquaredIsPositive
            => !_unaryOperationsIsNegativeBitVector[8];

        public int EGpSquaredSign
            => EGpSquaredIsNegative ? -1 : 1;


        internal BasisBladeData(ulong gaSpaceDimension, ulong id, uint grade, ulong index)
        {
            if (gaSpaceDimension > int.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(gaSpaceDimension));

            Id = id;
            Grade = grade;
            Index = index;

            var egpIsNegativeBitArray = new BitArray((int) gaSpaceDimension);

            //for (var id2 = 0UL; id2 < gaSpaceDimension; id2++)
            //    egpIsNegativeBitArray[(int) id2] = BasisBladeDataComputer.EGpIsNegative(id, id2);

            Parallel.For(
                0, 
                (int) gaSpaceDimension,
                id2 =>
                {
                    egpIsNegativeBitArray[id2] = BasisBladeDataComputer.EGpIsNegative(id, (ulong) id2);
                }
            );

            _egpIsNegativeBitArray = egpIsNegativeBitArray;

            _unaryOperationsIsNegativeBitVector = new BitVector32
            {
                [1] = grade.GradeInvolutionIsNegativeOfGrade(),
                [2] = grade.ReverseIsNegativeOfGrade(),
                [4] = grade.CliffordConjugateIsNegativeOfGrade(),
                [8] = _egpIsNegativeBitArray[(int) id]
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool EGpIsNegative(ulong id2)
        {
            return id2 < (ulong) _egpIsNegativeBitArray.Length
                ? _egpIsNegativeBitArray[(int) id2]
                : BasisBladeDataComputer.EGpIsNegative(Id, id2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool EGpIsPositive(ulong id2)
        {
            return id2 < (ulong) _egpIsNegativeBitArray.Length
                ? !_egpIsNegativeBitArray[(int) id2]
                : BasisBladeDataComputer.EGpIsPositive(Id, id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int EGpSign(ulong id2)
        {
            return id2 < (ulong) _egpIsNegativeBitArray.Length
                ? _egpIsNegativeBitArray[(int) id2] ? -1 : 1
                : BasisBladeDataComputer.EGpSign(Id, id2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int EGpReverseSign(ulong id2)
        {
            return EGpSign(id2) * BasisBladeDataLookup.ReverseSign(id2);
        }
    }
}
