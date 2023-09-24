﻿using DataStructuresLib.IndexSets;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Records;

public interface IXGaIdPairRecord
{
    /// <summary>
    /// The First Basis Blade ID
    /// </summary>
    IIndexSet Id1 { get; }

    /// <summary>
    /// The Second Basis Blade ID
    /// </summary>
    IIndexSet Id2 { get; }
}