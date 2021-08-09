using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Storage.Terms;
using GeometricAlgebraFulcrumLib.Structures.Even;

namespace GeometricAlgebraFulcrumLib.Storage.Composers
{
    public interface IGaStorageComposerMultivector<T>
        : IGaStorageComposer<T>
    {
        T this[ulong id] { get; set; }

        T this[uint grade, ulong index] { get; set; }

        IGaStorageComposerMultivector<T> Clear();

        IGaStorageComposerMultivector<T> SetScalar(T scalar);

        IGaStorageComposerMultivector<T> SetTerm(ulong id, T scalar);

        IGaStorageComposerMultivector<T> SetTerm(uint grade, ulong index, T scalar);

        IGaStorageComposerMultivector<T> SetTerm(GaTerm<T> term);

        IGaStorageComposerMultivector<T> SetTerms(IEnumerable<Tuple<ulong, T>> termsList);

        IGaStorageComposerMultivector<T> SetTerms(IEnumerable<Tuple<uint, ulong, T>> termsList);

        IGaStorageComposerMultivector<T> SetTerms(IEnumerable<KeyValuePair<ulong, T>> termsList);

        IGaStorageComposerMultivector<T> SetTerms(IEnumerable<GaTerm<T>> termsList);


        IGaStorageComposerMultivector<T> SetTermsToNegative();

        IGaStorageComposerMultivector<T> SetTermsToNegative(IEnumerable<ulong> idsList);

        IGaStorageComposerMultivector<T> SetTermsToNegative(IEnumerable<Tuple<uint, ulong>> indicesList);


        IGaStorageComposerMultivector<T> SetComputedTerms(IEnumerable<ulong> idsList, Func<ulong, T> mappingFunc);

        IGaStorageComposerMultivector<T> SetComputedTerms(IEnumerable<Tuple<uint, ulong>> idsList, Func<ulong, T> mappingFunc);

        IGaStorageComposerMultivector<T> SetComputedTerms(IEnumerable<ulong> idsList, Func<uint, ulong, T> mappingFunc);

        IGaStorageComposerMultivector<T> SetComputedTerms(IEnumerable<Tuple<uint, ulong>> idsList, Func<uint, ulong, T> mappingFunc);

        IGaStorageComposerMultivector<T> SetComputedTerms(IEnumerable<ulong> idsList, Func<ulong, T, T> mappingFunc);

        IGaStorageComposerMultivector<T> SetComputedTerms(IEnumerable<Tuple<uint, ulong>> idsList, Func<ulong, T, T> mappingFunc);

        IGaStorageComposerMultivector<T> SetComputedTerms(IEnumerable<ulong> idsList, Func<uint, ulong, T, T> mappingFunc);

        IGaStorageComposerMultivector<T> SetComputedTerms(IEnumerable<Tuple<uint, ulong>> idsList, Func<uint, ulong, T, T> mappingFunc);


        IGaStorageComposerMultivector<T> SetLeftScaledTerms(T scalingFactor, IEnumerable<Tuple<ulong, T>> termsList);

        IGaStorageComposerMultivector<T> SetLeftScaledTerms(T scalingFactor, IEnumerable<Tuple<uint, ulong, T>> termsList);

        IGaStorageComposerMultivector<T> SetLeftScaledTerms(T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> termsList);

        IGaStorageComposerMultivector<T> SetLeftScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList);


        IGaStorageComposerMultivector<T> SetRightScaledTerms(T scalingFactor, IEnumerable<Tuple<ulong, T>> termsList);

        IGaStorageComposerMultivector<T> SetRightScaledTerms(T scalingFactor, IEnumerable<Tuple<uint, ulong, T>> termsList);

        IGaStorageComposerMultivector<T> SetRightScaledTerms(T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> termsList);

        IGaStorageComposerMultivector<T> SetRightScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList);


        IGaStorageComposerMultivector<T> LeftScaleTerms(T scalingFactor);

        IGaStorageComposerMultivector<T> LeftScaleTerms(IEnumerable<ulong> indexList, T scalingFactor);

        IGaStorageComposerMultivector<T> LeftScaleTerms(IEnumerable<Tuple<uint, ulong>> indexList, T scalingFactor);


        IGaStorageComposerMultivector<T> RightScaleTerms(T scalingFactor);

        IGaStorageComposerMultivector<T> RightScaleTerms(IEnumerable<ulong> indexList, T scalingFactor);

        IGaStorageComposerMultivector<T> RightScaleTerms(IEnumerable<Tuple<uint, ulong>> indexList, T scalingFactor);


        IGaStorageComposerMultivector<T> AddScalar(T scalar);

        IGaStorageComposerMultivector<T> AddTerm(ulong id, T value);

        IGaStorageComposerMultivector<T> AddTerm(uint grade, ulong index, T value);

        IGaStorageComposerMultivector<T> AddTerm(GaTerm<T> term);


        IGaStorageComposerMultivector<T> AddTerms(IEnumerable<T> scalarsList);

        IGaStorageComposerMultivector<T> AddTerms(IEnumerable<Tuple<ulong, T>> idScalarTuples);

        IGaStorageComposerMultivector<T> AddTerms(IEnumerable<Tuple<uint, ulong, T>> gradeIndexScalarTuples);

        IGaStorageComposerMultivector<T> AddTerms(IEnumerable<KeyValuePair<ulong, T>> idScalarPairs);

        IGaStorageComposerMultivector<T> AddTerms(IEnumerable<GaTerm<T>> termsList);


        IGaStorageComposerMultivector<T> AddComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T> mappingFunc);

        IGaStorageComposerMultivector<T> AddComputedTerms(IEnumerable<Tuple<uint, ulong>> indexList, Func<ulong, T> mappingFunc);

        IGaStorageComposerMultivector<T> AddComputedTerms(IEnumerable<ulong> indexList, Func<uint, ulong, T> mappingFunc);

        IGaStorageComposerMultivector<T> AddComputedTerms(IEnumerable<Tuple<uint, ulong>> indexList, Func<uint, ulong, T> mappingFunc);

        IGaStorageComposerMultivector<T> AddComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T, T> mappingFunc);

        IGaStorageComposerMultivector<T> AddComputedTerms(IEnumerable<Tuple<uint, ulong>> indexList, Func<ulong, T, T> mappingFunc);

        IGaStorageComposerMultivector<T> AddComputedTerms(IEnumerable<ulong> indexList, Func<uint, ulong, T, T> mappingFunc);

        IGaStorageComposerMultivector<T> AddComputedTerms(IEnumerable<Tuple<uint, ulong>> indexList, Func<uint, ulong, T, T> mappingFunc);


        IGaStorageComposerMultivector<T> AddLeftScaledTerms(T scalingFactor, IEnumerable<Tuple<ulong, T>> termsList);

        IGaStorageComposerMultivector<T> AddLeftScaledTerms(T scalingFactor, IEnumerable<Tuple<uint, ulong, T>> termsList);

        IGaStorageComposerMultivector<T> AddLeftScaledTerms(T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> termsList);

        IGaStorageComposerMultivector<T> AddLeftScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList);


        IGaStorageComposerMultivector<T> AddRightScaledTerms(T scalingFactor, IEnumerable<Tuple<ulong, T>> termsList);

        IGaStorageComposerMultivector<T> AddRightScaledTerms(T scalingFactor, IEnumerable<Tuple<uint, ulong, T>> termsList);

        IGaStorageComposerMultivector<T> AddRightScaledTerms(T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> termsList);

        IGaStorageComposerMultivector<T> AddRightScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList);


        IGaStorageComposerMultivector<T> SubtractScalar(T scalar);

        IGaStorageComposerMultivector<T> SubtractTerm(ulong id, T value);

        IGaStorageComposerMultivector<T> SubtractTerm(uint grade, ulong index, T value);

        IGaStorageComposerMultivector<T> SubtractTerm(GaTerm<T> term);


        IGaStorageComposerMultivector<T> SubtractTerms(IEnumerable<T> scalarsList);

        IGaStorageComposerMultivector<T> SubtractTerms(IEnumerable<Tuple<ulong, T>> termsList);

        IGaStorageComposerMultivector<T> SubtractTerms(IEnumerable<Tuple<uint, ulong, T>> termsList);

        IGaStorageComposerMultivector<T> SubtractTerms(IEnumerable<KeyValuePair<ulong, T>> termsList);

        IGaStorageComposerMultivector<T> SubtractTerms(IEnumerable<GaTerm<T>> termsList);


        IGaStorageComposerMultivector<T> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T> mappingFunc);

        IGaStorageComposerMultivector<T> SubtractComputedTerms(IEnumerable<Tuple<uint, ulong>> indexList, Func<ulong, T> mappingFunc);

        IGaStorageComposerMultivector<T> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<uint, ulong, T> mappingFunc);

        IGaStorageComposerMultivector<T> SubtractComputedTerms(IEnumerable<Tuple<uint, ulong>> indexList, Func<uint, ulong, T> mappingFunc);

        IGaStorageComposerMultivector<T> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<ulong, T, T> mappingFunc);

        IGaStorageComposerMultivector<T> SubtractComputedTerms(IEnumerable<Tuple<uint, ulong>> indexList, Func<ulong, T, T> mappingFunc);

        IGaStorageComposerMultivector<T> SubtractComputedTerms(IEnumerable<ulong> indexList, Func<uint, ulong, T, T> mappingFunc);

        IGaStorageComposerMultivector<T> SubtractComputedTerms(IEnumerable<Tuple<uint, ulong>> indexList, Func<uint, ulong, T, T> mappingFunc);


        IGaStorageComposerMultivector<T> SubtractLeftScaledTerms(T scalingFactor, IEnumerable<Tuple<ulong, T>> termsList);

        IGaStorageComposerMultivector<T> SubtractLeftScaledTerms(T scalingFactor, IEnumerable<Tuple<uint, ulong, T>> termsList);

        IGaStorageComposerMultivector<T> SubtractLeftScaledTerms(T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> termsList);

        IGaStorageComposerMultivector<T> SubtractLeftScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList);


        IGaStorageComposerMultivector<T> SubtractRightScaledTerms(T scalingFactor, IEnumerable<Tuple<ulong, T>> termsList);

        IGaStorageComposerMultivector<T> SubtractRightScaledTerms(T scalingFactor, IEnumerable<Tuple<uint, ulong, T>> termsList);

        IGaStorageComposerMultivector<T> SubtractRightScaledTerms(T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> termsList);

        IGaStorageComposerMultivector<T> SubtractRightScaledTerms(T scalingFactor, IEnumerable<GaTerm<T>> termsList);


        IGaStorageComposerMultivector<T> AddKVector(uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs);

        IGaStorageComposerMultivector<T> AddKVector(uint grade, IEnumerable<Tuple<ulong, T>> indexScalarPairs);

        IGaStorageComposerMultivector<T> AddKVector(IGaStorageKVector<T> storage);

        IGaStorageComposerMultivector<T> AddKVectors(IEnumerable<KeyValuePair<uint, Dictionary<ulong, T>>> storagesList);

        IGaStorageComposerMultivector<T> AddKVectors(IEnumerable<KeyValuePair<uint, IGaEvenDictionary<T>>> storagesList);

        IGaStorageComposerMultivector<T> AddKVectors(IEnumerable<IGaStorageKVector<T>> storagesList);

        IGaStorageComposerMultivector<T> AddLeftScaledKVector(T scalingFactor, uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs);

        IGaStorageComposerMultivector<T> AddLeftScaledKVector(T scalingFactor, uint grade, IEnumerable<Tuple<ulong, T>> indexScalarPairs);

        IGaStorageComposerMultivector<T> AddLeftScaledKVector(T scalingFactor, GaStorageKVectorBase<T> storage);

        IGaStorageComposerMultivector<T> AddRightScaledKVector(T scalingFactor, uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs);

        IGaStorageComposerMultivector<T> AddRightScaledKVector(T scalingFactor, uint grade, IEnumerable<Tuple<ulong, T>> indexScalarPairs);

        IGaStorageComposerMultivector<T> AddRightScaledKVector(T scalingFactor, GaStorageKVectorBase<T> storage);


        IGaStorageComposerMultivector<T> SubtractKVector(uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs);

        IGaStorageComposerMultivector<T> SubtractKVector(uint grade, IEnumerable<Tuple<ulong, T>> indexScalarPairs);

        IGaStorageComposerMultivector<T> SubtractKVector(GaStorageKVectorBase<T> storage);

        IGaStorageComposerMultivector<T> SubtractLeftScaledKVector(T scalingFactor, uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs);

        IGaStorageComposerMultivector<T> SubtractLeftScaledKVector(T scalingFactor, uint grade, IEnumerable<Tuple<ulong, T>> indexScalarPairs);

        IGaStorageComposerMultivector<T> SubtractLeftScaledKVector(T scalingFactor, GaStorageKVectorBase<T> storage);

        IGaStorageComposerMultivector<T> SubtractRightScaledKVector(T scalingFactor, uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs);

        IGaStorageComposerMultivector<T> SubtractRightScaledKVector(T scalingFactor, uint grade, IEnumerable<Tuple<ulong, T>> indexScalarPairs);

        IGaStorageComposerMultivector<T> SubtractRightScaledKVector(T scalingFactor, GaStorageKVectorBase<T> storage);
        
        
        bool RemoveTerm(ulong id);

        bool RemoveTerm(uint grade, ulong index);

        IGaStorageComposerMultivector<T> RemoveTerms(params ulong[] indexList);

        IGaStorageComposerMultivector<T> RemoveZeroTerms();

        IGaStorageComposerMultivector<T> RemoveNearZeroTerms();

        IGaStorageComposerMultivector<T> RemoveZeroTerms(bool nearZeroFlag);

        IGaStorageComposerMultivector<T> RemoveKVector(uint grade);

        /// <summary>
        /// Set some terms using the given k-vector data
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalarValuesList"></param>
        /// <returns></returns>
        IGaStorageComposerMultivector<T> SetKVector(uint grade, IReadOnlyList<T> scalarValuesList);

        /// <summary>
        /// Set some terms using the given k-vector data scaled by a scaling factor
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalingFactor"></param>
        /// <param name="scalarValuesList"></param>
        /// <returns></returns>
        IGaStorageComposerMultivector<T> SetKVector(uint grade, T scalingFactor, IReadOnlyList<T> scalarValuesList);

        /// <summary>
        /// Set some terms using the given k-vector data
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="indexScalarPairs"></param>
        /// <returns></returns>
        IGaStorageComposerMultivector<T> SetKVector(uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs);

        /// <summary>
        /// Set some terms using the given k-vector data scaled by a scaling factor
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalingFactor"></param>
        /// <param name="indexScalarPairs"></param>
        /// <returns></returns>
        IGaStorageComposerMultivector<T> SetKVector(uint grade, T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs);

        /// <summary>
        /// Set some terms using the given k-vector data
        /// </summary>
        /// <param name="kVector"></param>
        /// <returns></returns>
        IGaStorageComposerMultivector<T> SetKVector(IGaStorageKVector<T> kVector);

        /// <summary>
        /// Set some terms using the given k-vector data
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="indexScalarsDictionary"></param>
        /// <returns></returns>
        IGaStorageComposerMultivector<T> SetKVectorStorage(uint grade, Dictionary<ulong, T> indexScalarsDictionary);

        /// <summary>
        /// Set some terms using the given k-vector data scaled by a scaling factor
        /// </summary>
        /// <param name="scalingFactor"></param>
        /// <param name="kVector"></param>
        /// <returns></returns>
        IGaStorageComposerMultivector<T> SetKVector(T scalingFactor, IGaStorageKVector<T> kVector);

        IGaStorageComposerMultivector<T> SetKVectors(IEnumerable<KeyValuePair<uint, Dictionary<ulong, T>>> storagesList);

        IGaStorageComposerMultivector<T> SetKVectors(IEnumerable<KeyValuePair<uint, IGaEvenDictionary<T>>> storagesList);


        /// <summary>
        /// Set some terms using the given k-vectors data
        /// </summary>
        /// <param name="kVectorsList"></param>
        /// <returns></returns>
        IGaStorageComposerMultivector<T> SetKVectors(IEnumerable<IGaStorageKVector<T>> kVectorsList);

        /// <summary>
        /// Set some terms using the given k-vectors data scaled by a scaling factor
        /// </summary>
        /// <param name="scaledKVectorsList"></param>
        /// <returns></returns>
        IGaStorageComposerMultivector<T> SetKVectors(IEnumerable<Tuple<T, IGaStorageKVector<T>>> scaledKVectorsList);



        /// <summary>
        /// Set some terms using the given k-vector data
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalarValuesList"></param>
        /// <returns></returns>
        IGaStorageComposerMultivector<T> AddKVector(uint grade, IReadOnlyList<T> scalarValuesList);

        /// <summary>
        /// Set some terms using the given k-vector data scaled by a scaling factor
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalingFactor"></param>
        /// <param name="scalarValuesList"></param>
        /// <returns></returns>
        IGaStorageComposerMultivector<T> AddKVector(uint grade, T scalingFactor, IReadOnlyList<T> scalarValuesList);

        /// <summary>
        /// Set some terms using the given k-vector data scaled by a scaling factor
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalingFactor"></param>
        /// <param name="indexScalarPairs"></param>
        /// <returns></returns>
        IGaStorageComposerMultivector<T> AddLeftScaledKVector(uint grade, T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs);

        /// <summary>
        /// Set some terms using the given k-vector data scaled by a scaling factor
        /// </summary>
        /// <param name="scalingFactor"></param>
        /// <param name="kVector"></param>
        /// <returns></returns>
        IGaStorageComposerMultivector<T> AddKVector(T scalingFactor, IGaStorageKVector<T> kVector);

        /// <summary>
        /// Set some terms using the given k-vectors data scaled by a scaling factor
        /// </summary>
        /// <param name="scaledKVectorsList"></param>
        /// <returns></returns>
        IGaStorageComposerMultivector<T> AddLeftScaledKVectors(IEnumerable<Tuple<T, IGaStorageKVector<T>>> scaledKVectorsList);


        /// <summary>
        /// Set some terms using the given k-vector data
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalarValuesList"></param>
        /// <returns></returns>
        IGaStorageComposerMultivector<T> SubtractKVector(uint grade, IReadOnlyList<T> scalarValuesList);

        /// <summary>
        /// Set some terms using the given k-vector data scaled by a scaling factor
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalingFactor"></param>
        /// <param name="scalarValuesList"></param>
        /// <returns></returns>
        IGaStorageComposerMultivector<T> SubtractKVector(uint grade, T scalingFactor, IReadOnlyList<T> scalarValuesList);

        /// <summary>
        /// Set some terms using the given k-vector data scaled by a scaling factor
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalingFactor"></param>
        /// <param name="indexScalarPairs"></param>
        /// <returns></returns>
        IGaStorageComposerMultivector<T> SubtractLeftScaledKVector(uint grade, T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs);

        /// <summary>
        /// Set some terms using the given k-vector data
        /// </summary>
        /// <param name="kVector"></param>
        /// <returns></returns>
        IGaStorageComposerMultivector<T> SubtractKVector(IGaStorageKVector<T> kVector);

        /// <summary>
        /// Set some terms using the given k-vector data scaled by a scaling factor
        /// </summary>
        /// <param name="scalingFactor"></param>
        /// <param name="kVector"></param>
        /// <returns></returns>
        IGaStorageComposerMultivector<T> SubtractKVector(T scalingFactor, IGaStorageKVector<T> kVector);

        IGaStorageComposerMultivector<T> SubtractKVectors(IEnumerable<KeyValuePair<uint, IGaEvenDictionary<T>>> storagesList);

        IGaStorageComposerMultivector<T> SubtractKVectors(IEnumerable<KeyValuePair<uint, Dictionary<ulong, T>>> storagesList);

        /// <summary>
        /// Set some terms using the given k-vectors data
        /// </summary>
        /// <param name="kVectorsList"></param>
        /// <returns></returns>
        IGaStorageComposerMultivector<T> SubtractKVectors(IEnumerable<IGaStorageKVector<T>> kVectorsList);

        /// <summary>
        /// Set some terms using the given k-vectors data scaled by a scaling factor
        /// </summary>
        /// <param name="scaledKVectorsList"></param>
        /// <returns></returns>
        IGaStorageComposerMultivector<T> SubtractLeftScaledKVectors(IEnumerable<Tuple<T, IGaStorageKVector<T>>> scaledKVectorsList);


        /// <summary>
        /// Remove the given terms if possible, else set to zero
        /// </summary>
        /// <param name="idsList"></param>
        IGaStorageComposerMultivector<T> RemoveTerms(IEnumerable<ulong> idsList);

        /// <summary>
        /// Remove the given terms if possible, else set to zero
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="indexList"></param>
        IGaStorageComposerMultivector<T> RemoveTerms(uint grade, IEnumerable<ulong> indexList);

        /// <summary>
        /// Remove the given terms if possible, else set to zero
        /// </summary>
        /// <param name="grade"></param>
        IGaStorageComposerMultivector<T> RemoveTermsOfGrade(uint grade);


        /// <summary>
        /// Remove the given term if zero
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nearZeroFlag"></param>
        /// <returns></returns>
        IGaStorageComposerMultivector<T> RemoveTermIfZero(ulong id, bool nearZeroFlag = false);

        /// <summary>
        /// Remove the given term if zero
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        /// <param name="nearZeroFlag"></param>
        /// <returns></returns>
        IGaStorageComposerMultivector<T> RemoveTermIfZero(uint grade, ulong index, bool nearZeroFlag = false);

        /// <summary>
        /// Remove all terms from storage where their scalar values is near zero
        /// </summary>
        IGaStorageComposerMultivector<T> RemoveZeroTermsOfGrade(uint grade, bool nearZeroFlag = false);
    }
}