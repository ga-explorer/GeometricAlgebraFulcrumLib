% Create a sparse MATLAB column array from a GAPoT vector's parts
% Each vector part's terms are put in a separate colum in the array
% rowsCount indicate the max. number of allowed terms per vector part
function sparseArray = gapotPartsTermsToArray(mv, rowsCount, partLengthsArray)
    sparseMatrixData = mv.PartsTermsToMatlabArray(rowsCount, int32(partLengthsArray(:)));
    
    sparseArray = gapotSparseMatrixDataToArray(sparseMatrixData);
end
