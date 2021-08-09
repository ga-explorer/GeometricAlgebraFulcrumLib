% Create a sparse MATLAB array from a GAPoT vector' terms
function sparseArray = gapotVectorToTermsArray(mv, rowsCount)
    sparseMatrixData = mv.TermsToMatlabArray(rowsCount);
    
    sparseArray = gapotSparseMatrixDataToArray(sparseMatrixData);
end
