using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Records;

public sealed record XGaIdScalarRecord(IndexSet Id, double Scalar) :
    IXGaIdScalarRecord<double>;

public sealed record XGaIdScalarRecord<T>(IndexSet Id, T Scalar) :
    IXGaIdScalarRecord<T>;