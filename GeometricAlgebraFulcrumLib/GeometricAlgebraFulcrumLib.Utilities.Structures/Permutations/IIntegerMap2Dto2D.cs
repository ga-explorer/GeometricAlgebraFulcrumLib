﻿using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Permutations;

public interface IIntegerMap2Dto2D
{
    Pair<int> this[Pair<int> input] { get; }

    Pair<int> this[int input1, int input2] { get; }

    IEnumerable<Pair<int>> this[IEnumerable<Pair<int>> inputsList] { get; }
}