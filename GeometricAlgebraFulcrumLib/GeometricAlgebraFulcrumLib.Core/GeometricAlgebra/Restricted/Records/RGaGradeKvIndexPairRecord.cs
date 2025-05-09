﻿namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Records;

public sealed record RGaGradeKvIndexPairRecord(uint Grade, ulong KvIndex1, ulong KvIndex2) :
    IRGaGradeKvIndexPairRecord;