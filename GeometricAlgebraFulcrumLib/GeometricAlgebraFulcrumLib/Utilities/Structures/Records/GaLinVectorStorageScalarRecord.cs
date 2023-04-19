using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

public sealed record GaLinVectorStorageScalarRecord<T>(ILinVectorStorage<T> Storage, T Scalar) : 
    IGaScalarRecord<T>;