using System.Collections;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Basis;

internal sealed record BasisBladeData
{
    private readonly BitArray _egpIsNegativeBitArray;
    private readonly BitVector32 _unaryOperationsIsNegativeBitVector;


    public ulong Id { get; }

    public uint Grade { get; }

    public ulong Index { get; }

    public Tuple<uint, ulong> GradeIndex 
        => new Tuple<uint, ulong>(Grade, Index);

    public bool GradeInvolutionIsNegative 
        => _unaryOperationsIsNegativeBitVector[1];

    public bool GradeInvolutionIsPositive 
        => !_unaryOperationsIsNegativeBitVector[1];
        
    public IntegerSign GradeInvolutionSign 
        => GradeInvolutionIsNegative 
            ? IntegerSign.Negative 
            : IntegerSign.Positive;

    public bool ReverseIsNegative 
        => _unaryOperationsIsNegativeBitVector[2];

    public bool ReverseIsPositive 
        => !_unaryOperationsIsNegativeBitVector[2];
        
    public IntegerSign ReverseSign 
        => ReverseIsNegative 
            ? IntegerSign.Negative 
            : IntegerSign.Positive;

    public bool CliffordConjugateIsNegative 
        => _unaryOperationsIsNegativeBitVector[4];

    public bool CliffordConjugateIsPositive
        => !_unaryOperationsIsNegativeBitVector[4];
        
    public IntegerSign CliffordConjugateSign 
        => CliffordConjugateIsNegative 
            ? IntegerSign.Negative 
            : IntegerSign.Positive;

    public bool EGpSquaredIsNegative 
        => _unaryOperationsIsNegativeBitVector[8];

    public bool EGpSquaredIsPositive
        => !_unaryOperationsIsNegativeBitVector[8];

    public IntegerSign EGpSquaredSign
        => EGpSquaredIsNegative 
            ? IntegerSign.Negative 
            : IntegerSign.Positive;


    internal BasisBladeData(ulong gaSpaceDimensions, ulong id, uint grade, ulong index)
    {
        if (gaSpaceDimensions > int.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(gaSpaceDimensions));

        Id = id;
        Grade = grade;
        Index = index;

        var egpIsNegativeBitArray = new BitArray((int) gaSpaceDimensions);

        for (var id2 = 0UL; id2 < gaSpaceDimensions; id2++)
            egpIsNegativeBitArray[(int) id2] = BasisBladeDataComputer.EGpIsNegative(id, id2);

        // This will not work because it has to be done gradually be increasing grade not by id
        //Parallel.For(
        //    0, 
        //    (int) gaSpaceDimensions,
        //    id2 =>
        //    {
        //        egpIsNegativeBitArray[id2] = BasisBladeDataComputer.EGpIsNegative(id, (ulong) id2);
        //    }
        //);

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
            : !BasisBladeDataComputer.EGpIsNegative(Id, id2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EGpSign(ulong id2)
    {
        return id2 < (ulong) _egpIsNegativeBitArray.Length
            ? _egpIsNegativeBitArray[(int) id2] 
                ? IntegerSign.Negative 
                : IntegerSign.Positive
            : BasisBladeDataComputer.EGpSign(Id, id2);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EGpReverseSign(ulong id2)
    {
        return EGpSign(id2) * BasisBladeDataLookup.ReverseSign(id2);
    }
}