% Only used for some internal operations in the toolbox
function sparseArray = gapotSparseMatrixDataToArray(sparseMatrixData)
    iArray = int32(sparseMatrixData.RowIndicesArray);
    jArray = int32(sparseMatrixData.ColumnIndicesArray);
    
    vArray = double(sparseMatrixData.ValuesArray);
    
    m = double(sparseMatrixData.RowsCount);
    n = double(sparseMatrixData.ColumnsCount);
    
    sparseArray = sparse(iArray, jArray, vArray, m, n);
end