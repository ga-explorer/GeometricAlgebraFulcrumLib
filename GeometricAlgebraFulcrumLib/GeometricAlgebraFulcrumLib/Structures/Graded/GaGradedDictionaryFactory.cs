using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Collections;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Structures.Even;

namespace GeometricAlgebraFulcrumLib.Structures.Graded
{
    public static class GaGradedDictionaryFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGradedDictionary<T> CreateGradedDictionarySingleZeroGrade<T>(this T value)
        {
            return new GaGradedDictionarySingleZeroGrade<T>(
                value.CreateEvenDictionarySingleZeroKey()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGradedDictionary<T> CreateGradedDictionarySingleKey<T>(this T value, uint grade, ulong key)
        {
            var evenDictionary = 
                value.CreateEvenDictionarySingleKey(key);

            return grade == 0UL
                ? new GaGradedDictionarySingleZeroGrade<T>(evenDictionary)
                : new GaGradedDictionarySingleGrade<T>(grade, evenDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGradedDictionary<T> CreateGradedDictionarySingleKey<T>(this uint grade, ulong key, T value)
        {
            var evenDictionary = 
                value.CreateEvenDictionarySingleKey(key);

            return grade == 0UL
                ? new GaGradedDictionarySingleZeroGrade<T>(evenDictionary)
                : new GaGradedDictionarySingleGrade<T>(grade, evenDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGradedDictionary<T> CreateGradedDictionarySingleKey<T>(this IGaEvenDictionary<T> evenDictionary, uint grade)
        {
            if (evenDictionary.IsEmpty())
                return GaGradedDictionaryEmpty<T>.DefaultDictionary;

            return grade == 0UL
                ? new GaGradedDictionarySingleZeroGrade<T>(evenDictionary)
                : new GaGradedDictionarySingleGrade<T>(grade, evenDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGradedDictionary<T> CreateGradedDictionarySingleKey<T>(this uint grade, IGaEvenDictionary<T> evenDictionary)
        {
            if (evenDictionary.IsEmpty())
                return GaGradedDictionaryEmpty<T>.DefaultDictionary;

            return grade == 0UL
                ? new GaGradedDictionarySingleZeroGrade<T>(evenDictionary)
                : new GaGradedDictionarySingleGrade<T>(grade, evenDictionary);
        }


        public static IGaGradedDictionary<T> CreateGradedDictionary<T>(this Dictionary<uint, Dictionary<ulong, T>> gradeKeyValueDictionary)
        {
            if (gradeKeyValueDictionary.Count == 0 || gradeKeyValueDictionary.Values.All(d => d.IsNullOrEmpty()))
                return GaGradedDictionaryEmpty<T>.DefaultDictionary;

            if (gradeKeyValueDictionary.Count != 1)
            {
                return new GaGradedDictionary<T>(
                    gradeKeyValueDictionary
                        .CopyToDictionary(
                            dict => dict.CreateEvenDictionary()
                        )
                );
            }

            var (grade, evenDictionary) = 
                gradeKeyValueDictionary.First();

            var keyValueDictionary =
                evenDictionary.CreateEvenDictionary();

            return grade == 0
                ? new GaGradedDictionarySingleZeroGrade<T>(keyValueDictionary)
                : new GaGradedDictionarySingleGrade<T>(grade, keyValueDictionary);
        }

        public static IGaGradedDictionary<T> CreateGradedDictionary<T>(this Dictionary<uint, IGaEvenDictionary<T>> gradeKeyValueDictionary)
        {
            if (gradeKeyValueDictionary.Count == 0 || gradeKeyValueDictionary.Values.All(d => d.IsEmpty()))
                return GaGradedDictionaryEmpty<T>.DefaultDictionary;

            if (gradeKeyValueDictionary.Count != 1) 
                return new GaGradedDictionary<T>(gradeKeyValueDictionary);

            var (grade, evenDictionary) = 
                gradeKeyValueDictionary.First();

            return grade == 0
                ? new GaGradedDictionarySingleZeroGrade<T>(evenDictionary)
                : new GaGradedDictionarySingleGrade<T>(grade, evenDictionary);
        }
    }
}