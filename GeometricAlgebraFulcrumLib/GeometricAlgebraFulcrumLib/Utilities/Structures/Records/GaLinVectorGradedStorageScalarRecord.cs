using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Records;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

public sealed record GaLinVectorGradedStorageScalarRecord<T>(ILinVectorGradedStorage<T> Storage, T Scalar) : 
    IGaScalarRecord<T>;