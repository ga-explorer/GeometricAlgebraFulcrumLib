using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Storage.Composers
{
    public class GaStorageComposerVector<T>
        : GaStorageComposerKVector<T>
    {
        internal GaStorageComposerVector(IGaScalarProcessor<T> scalarProcessor) 
            : base(scalarProcessor, 1)
        {
        }

        internal GaStorageComposerVector(IGaScalarProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, T> indexScalarsDictionary) 
            : base(scalarProcessor, 1, indexScalarsDictionary)
        {
        }

        internal GaStorageComposerVector(IGaScalarProcessor<T> scalarProcessor, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs) 
            : base(scalarProcessor, 1, indexScalarPairs)
        {
        }

        internal GaStorageComposerVector(IGaScalarProcessor<T> scalarProcessor, IEnumerable<Tuple<ulong, T>> indexScalarTuples) 
            : base(scalarProcessor, 1, indexScalarTuples)
        {
        }
    }
}