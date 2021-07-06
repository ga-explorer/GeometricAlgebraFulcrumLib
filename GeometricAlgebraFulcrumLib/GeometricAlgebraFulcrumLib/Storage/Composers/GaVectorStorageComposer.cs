using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Storage.Composers
{
    public class GaVectorStorageComposer<TScalar>
        : GaKVectorStorageComposer<TScalar>
    {
        public GaVectorStorageComposer(IGaScalarProcessor<TScalar> scalarProcessor) 
            : base(scalarProcessor, 1)
        {
        }

        public GaVectorStorageComposer(IGaScalarProcessor<TScalar> scalarProcessor, Dictionary<ulong, TScalar> indexScalarsDictionary) 
            : base(scalarProcessor, 1, indexScalarsDictionary)
        {
        }

        public GaVectorStorageComposer(IGaScalarProcessor<TScalar> scalarProcessor, IEnumerable<KeyValuePair<ulong, TScalar>> indexScalarPairs) 
            : base(scalarProcessor, 1, indexScalarPairs)
        {
        }

        public GaVectorStorageComposer(IGaScalarProcessor<TScalar> scalarProcessor, IEnumerable<Tuple<ulong, TScalar>> indexScalarTuples) 
            : base(scalarProcessor, 1, indexScalarTuples)
        {
        }
    }
}