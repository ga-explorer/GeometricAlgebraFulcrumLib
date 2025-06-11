using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic;

public class LinRandomComposer<T> :
    ScalarRandomComposer<T>
{
    public int VSpaceDimensions { get; }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal LinRandomComposer(IScalarProcessor<T> scalarProcessor, int vSpaceDimensions)
        : base(scalarProcessor)
    {
        if (vSpaceDimensions is < 2 or > 31)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        VSpaceDimensions = vSpaceDimensions;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal LinRandomComposer(IScalarProcessor<T> scalarProcessor, int vSpaceDimensions, int seed)
        : base(scalarProcessor, seed)
    {
        if (vSpaceDimensions < 1)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        VSpaceDimensions = vSpaceDimensions;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal LinRandomComposer(IScalarProcessor<T> geometricProcessor, int vSpaceDimensions, Random randomGenerator)
        : base(geometricProcessor, randomGenerator)
    {
        if (vSpaceDimensions < 1)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        VSpaceDimensions = vSpaceDimensions;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetBasisVectorIndex()
    {
        return RandomGenerator.Next(VSpaceDimensions);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Dictionary<int, T> GetLinVectorIndexScalarDictionary()
    {
        return Enumerable
            .Range(0, VSpaceDimensions)
            .ToDictionary(
                index => index,
                _ => GetScalar().ScalarValue
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Dictionary<int, T> GetLinVectorIndexScalarDictionary(double minValue, double maxValue)
    {
        return Enumerable
            .Range(0, VSpaceDimensions)
            .ToDictionary(
                index => index,
                _ => GetScalar(minValue, maxValue).ScalarValue
            );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBasisVector GetLinBasisVector()
    {
        var id = GetBasisVectorIndex();

        return LinBasisVector.Positive(id);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBasisVector GetLinSignedBasisVector()
    {
        var idx = GetBasisVectorIndex();
        var sign = RandomGenerator.GetSign();

        return LinBasisVector.Create(idx, sign);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorTerm<T> GetLinVectorTerm()
    {
        var scalar = GetScalar().ScalarValue;
        var basis = GetLinBasisVector();

        return basis.ToTerm(ScalarProcessor, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorTerm<T> GetLinVectorTerm(int index)
    {
        var scalar = GetScalar().ScalarValue;

        return index.ToLinBasisVector().ToTerm(ScalarProcessor, scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector<T> GetLinVector(bool sparseVector = true)
    {
        if (sparseVector)
        {
            var termsCount = RandomGenerator.Next(VSpaceDimensions);

            var indexScalarDictionary =
                VSpaceDimensions
                    .GetRange()
                    .Shuffled(RandomGenerator)
                    .Take(termsCount)
                    .ToDictionary(
                        p => p,
                        _ => GetScalar().ScalarValue
                    );

            return ScalarProcessor.CreateLinVector(indexScalarDictionary);
        }

        var scalarArray =
            VSpaceDimensions
                .GetRange()
                .Select(_ => GetScalar().ScalarValue);

        return ScalarProcessor.CreateLinVector(scalarArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector<T> GetLinSparseVector(int termsCount)
    {
        if (termsCount > VSpaceDimensions)
            throw new ArgumentOutOfRangeException(nameof(termsCount));

        var indexScalarDictionary =
            VSpaceDimensions
                .GetRange()
                .Shuffled(RandomGenerator)
                .Take(termsCount)
                .ToDictionary(
                    p => p,
                    _ => GetScalar().ScalarValue
                );

        return ScalarProcessor.CreateLinVector(indexScalarDictionary);
    }
    
}