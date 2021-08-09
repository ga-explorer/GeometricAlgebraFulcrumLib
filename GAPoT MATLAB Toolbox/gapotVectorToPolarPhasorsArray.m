% Create a sparse MATLAB array from a GAPoT vector' polar phasors
function sparseArray = gapotVectorToPolarPhasorsArray(mv, rowsCount)
    sparseMatrixData = mv.PolarPhasorsToMatlabArray(rowsCount);
    
    sparseArray = gapotSparseMatrixDataToArray(sparseMatrixData);
end
