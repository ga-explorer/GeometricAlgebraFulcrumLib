﻿using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Records;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

public sealed record GaGradeLinVectorStorageScalarRecord<T>(uint Grade, ILinVectorStorage<T> Storage, T Scalar) : 
    IGaGradeScalarRecord<T>;