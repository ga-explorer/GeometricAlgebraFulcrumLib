﻿using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Records;

public sealed record RGaIdBivectorRecord<T>(ulong Id, RGaBivector<T> Bivector) :
    IRGaIdBivectorRecord<T>;