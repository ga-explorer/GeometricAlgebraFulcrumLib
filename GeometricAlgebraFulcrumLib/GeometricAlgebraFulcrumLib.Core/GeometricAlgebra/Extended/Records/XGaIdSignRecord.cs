using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Records;

public sealed record XGaIdSignRecord(IndexSet Id, IntegerSign Sign) :
    IXGaIdSignRecord;