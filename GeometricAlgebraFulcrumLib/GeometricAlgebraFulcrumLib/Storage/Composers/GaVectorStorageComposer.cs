using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Storage.Composers
{
    public class GaVectorStorageComposer<T>
        : GaKVectorStorageComposer<T>
    {
        public GaVectorStorageComposer(IGaScalarProcessor<T> scalarProcessor) 
            : base(scalarProcessor, 1)
        {
        }

        public GaVectorStorageComposer(IGaScalarProcessor<T> scalarProcessor, Dictionary<ulong, T> indexScalarsDictionary) 
            : base(scalarProcessor, 1, indexScalarsDictionary)
        {
        }

        public GaVectorStorageComposer(IGaScalarProcessor<T> scalarProcessor, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs) 
            : base(scalarProcessor, 1, indexScalarPairs)
        {
        }

        public GaVectorStorageComposer(IGaScalarProcessor<T> scalarProcessor, IEnumerable<Tuple<ulong, T>> indexScalarTuples) 
            : base(scalarProcessor, 1, indexScalarTuples)
        {
        }
    }
}