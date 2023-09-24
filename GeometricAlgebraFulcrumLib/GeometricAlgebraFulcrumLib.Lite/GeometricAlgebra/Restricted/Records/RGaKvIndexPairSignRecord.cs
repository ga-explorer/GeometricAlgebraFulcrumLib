﻿using DataStructuresLib.Basic;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;

public sealed record RGaKvIndexPairSignRecord(ulong KvIndex1, ulong KvIndex2, IntegerSign Sign) :
    IRGaKvIndexPairSignRecord;