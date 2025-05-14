using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Records;

public sealed record XGaIdScalarRecord(IndexSet Id, double Scalar) :
    IXGaIdScalarRecord<double>;
