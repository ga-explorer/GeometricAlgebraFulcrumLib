﻿namespace DataStructuresLib.Permutations
{
    public interface IIndexMap2DTo1D
        : IIntegerMap2Dto1D
    {
        int IndexCount1 { get; }

        int IndexCount2 { get; }
    }
}