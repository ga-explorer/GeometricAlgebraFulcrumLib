% Create a sparse MATLAB array from a GAPoT vector' rectangular phasors
function sparseArray = gapotVectorToRectPhasorsArray(mv, rowsCount)
    sparseMatrixData = mv.RectPhasorsToMatlabArray(rowsCount);
    
    sparseArray = gapotSparseMatrixDataToArray(sparseMatrixData);
end
