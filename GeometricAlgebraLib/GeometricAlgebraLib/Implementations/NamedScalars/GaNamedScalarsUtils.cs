using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraLib.Storage;

namespace GeometricAlgebraLib.Implementations.NamedScalars
{
    public static class GaNamedScalarsUtils
    {
        public static void SetIsOutput<TScalar>(this IGaMultivectorStorage<IGaNamedScalar<TScalar>> multivector, bool isOutput)
        {
            var namedScalarsList = 
                multivector
                    .GetScalars()
                    .Where(s => s.IsVariable)
                    .Select(s => (GaNamedScalarVariable<TScalar>) s);

            foreach (var namedScalar in namedScalarsList)
                namedScalar.IsOutput = isOutput;
        }

        public static void SetFinalScalarNamesById<TScalar>(this IGaMultivectorStorage<IGaNamedScalar<TScalar>> multivector, Func<ulong, string> namingFunc)
        {
            var idScalarPairs = 
                multivector
                    .GetIdScalarPairs()
                    .Where(s => !s.Value.IsConstant);

            foreach (var (id, scalar) in idScalarPairs)
                scalar.FinalScalarName = namingFunc(id);
        }

        public static void SetFinalScalarNamesByGradeIndex<TScalar>(this IGaMultivectorStorage<IGaNamedScalar<TScalar>> multivector, Func<int, ulong, string> namingFunc)
        {
            var indexScalarTuples = 
                multivector
                    .GetGradeIndexScalarTuples()
                    .Where(s => !s.Item3.IsConstant);

            foreach (var (grade, index, scalar) in indexScalarTuples) 
                scalar.FinalScalarName = namingFunc(grade, index);
        }
        
        public static void SetFinalScalarNamesByIndex<TScalar>(this IGaKVectorStorage<IGaNamedScalar<TScalar>> kVector, Func<ulong, string> namingFunc)
        {
            var indexScalarPairs = 
                kVector
                    .GetIndexScalarPairs()
                    .Where(s => !s.Value.IsConstant);

            foreach (var (index, scalar) in indexScalarPairs)
                scalar.FinalScalarName = namingFunc(index);
        }
        
        public static void SetFinalScalarNamesByOrder<TScalar>(this IEnumerable<IGaNamedScalar<TScalar>> namedScalars, Func<int, string> namingFunc)
        {
            var namedScalarsList = 
                namedScalars.Where(scalar => !scalar.IsConstant);

            var index = 0;
            foreach (var namedScalar in namedScalarsList)
            {
                namedScalar.FinalScalarName = namingFunc(index);

                index++;
            }
        }

        public static void SetIntermediateFinalScalarNamesByOrder<TScalar>(this GaNamedScalarsCollection<TScalar> namedScalarsCollection, Func<int, string> namingFunc)
        {
            var namedScalarsList = 
                namedScalarsCollection.IntermediateVariableNamedScalars;

            var index = 0;
            foreach (var namedScalar in namedScalarsList)
            {
                namedScalar.FinalScalarName = namingFunc(index);

                index++;
            }
        }
    }
}
