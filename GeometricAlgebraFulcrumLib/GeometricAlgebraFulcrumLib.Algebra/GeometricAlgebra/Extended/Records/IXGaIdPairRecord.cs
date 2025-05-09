﻿using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Records;

public interface IXGaIdPairRecord
{
    /// <summary>
    /// The First Basis Blade ID
    /// </summary>
    IndexSet Id1 { get; }

    /// <summary>
    /// The Second Basis Blade ID
    /// </summary>
    IndexSet Id2 { get; }
}