using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Composers;
using GeometricAlgebraFulcrumLib.Storage.Factories;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean
{
    public static class GaProductEucHipUtils
    {
        public static IGaStorageKVector<T> EHip<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv1, IGaStorageKVector<T> mv2)
        {
            var grade1 = mv1.Grade;
            var grade2 = mv2.Grade;
            if (grade1 == 0 || grade2 == 0)
                return scalarProcessor.CreateStorageZeroScalar();

            if (grade1 == grade2)
                return scalarProcessor.CreateStorageScalar(
                    scalarProcessor.ESp(mv1, mv2)
                );

            var grade = (uint) Math.Abs(grade2 - grade1);

            var composer = 
                new GaStorageComposerKVector<T>(scalarProcessor, grade);

            var indexScalarPairs1 = 
                mv1.IndexScalarDictionary;

            var indexScalarPairs2 = 
                mv2.IndexScalarDictionary;

            foreach (var (index1, scalar1) in indexScalarPairs1)
            {
                var id1 = GaBasisUtils.BasisBladeId(grade1, index1);

                foreach (var (index2, scalar2) in indexScalarPairs2)
                {
                    var id2 = GaBasisUtils.BasisBladeId(grade2, index2);

                    //This is correct because we eliminated the scalar case earlier
                    var signature = 
                        GaBasisUtils.EFdpSignature(id1, id2); 

                    if (signature == 0) 
                        continue;

                    var index = (id1 ^ id2).BasisBladeIndex();
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (signature > 0)
                        composer.AddTerm(index, scalar);
                    else
                        composer.SubtractTerm(index, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.GetKVector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> EHip<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            return scalarProcessor.BilinearProduct(mv1, mv2, GaBasisUtils.EHipSignature);
        }
    }
}