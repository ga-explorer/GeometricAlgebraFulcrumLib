using System.Collections.Generic;
using System.IO;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.SignalProcessing;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using OfficeOpenXml;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class ExcelUtils
    {
        public static double[] ReadScalarColumn(this ExcelWorksheet workSheet, int rowIndex, int rowCount, int columnIndex)
        {
            var scalarArray = new double[rowCount];

            for (var i = 0; i < rowCount; i++)
            {
                scalarArray[i] = workSheet.Cells[rowIndex + i, columnIndex].GetValue<double>();
            }

            return scalarArray;
        }

        public static double[] ReadScalarColumn(this IScalarAlgebraProcessor<double> scalarProcessor, ExcelWorksheet workSheet, int rowIndex, int rowCount, int columnIndex)
        {
            return workSheet.ReadScalarColumn(rowIndex,rowCount,columnIndex);
        }

        public static Pair<double[]> ReadScalarColumnPair(this ExcelWorksheet workSheet, int firstRowIndex, int rowCount, int firstColIndex)
        {
            var columnData1 = workSheet.ReadScalarColumn(
                firstRowIndex,
                rowCount,
                firstColIndex
            );

            var columnData2 = workSheet.ReadScalarColumn(
                firstRowIndex,
                rowCount,
                firstColIndex + 1
            );

            return new Pair<double[]>(columnData1, columnData2);
        }

        public static Triplet<double[]> ReadScalarColumnTriplet(this ExcelWorksheet workSheet, int firstRowIndex, int rowCount, int firstColIndex)
        {
            var columnData1 = workSheet.ReadScalarColumn(
                firstRowIndex,
                rowCount,
                firstColIndex
            );

            var columnData2 = workSheet.ReadScalarColumn(
                firstRowIndex,
                rowCount,
                firstColIndex + 1
            );

            var columnData3 = workSheet.ReadScalarColumn(
                firstRowIndex,
                rowCount,
                firstColIndex + 2
            );

            return new Triplet<double[]>(columnData1, columnData2, columnData3);
        }
        
        public static Quad<double[]> ReadScalarColumnQuad(this ExcelWorksheet workSheet, int firstRowIndex, int rowCount, int firstColIndex)
        {
            var columnData1 = workSheet.ReadScalarColumn(
                firstRowIndex,
                rowCount,
                firstColIndex
            );

            var columnData2 = workSheet.ReadScalarColumn(
                firstRowIndex,
                rowCount,
                firstColIndex + 1
            );

            var columnData3 = workSheet.ReadScalarColumn(
                firstRowIndex,
                rowCount,
                firstColIndex + 2
            );
            
            var columnData4 = workSheet.ReadScalarColumn(
                firstRowIndex,
                rowCount,
                firstColIndex + 3
            );

            return new Quad<double[]>(columnData1, columnData2, columnData3, columnData4);
        }
        
        public static Quint<double[]> ReadScalarColumnQuint(this ExcelWorksheet workSheet, int firstRowIndex, int rowCount, int firstColIndex)
        {
            var columnData1 = workSheet.ReadScalarColumn(
                firstRowIndex,
                rowCount,
                firstColIndex
            );

            var columnData2 = workSheet.ReadScalarColumn(
                firstRowIndex,
                rowCount,
                firstColIndex + 1
            );

            var columnData3 = workSheet.ReadScalarColumn(
                firstRowIndex,
                rowCount,
                firstColIndex + 2
            );
            
            var columnData4 = workSheet.ReadScalarColumn(
                firstRowIndex,
                rowCount,
                firstColIndex + 3
            );
            
            var columnData5 = workSheet.ReadScalarColumn(
                firstRowIndex,
                rowCount,
                firstColIndex + 4
            );

            return new Quint<double[]>(columnData1, columnData2, columnData3, columnData4, columnData5);
        }
        
        public static Hexad<double[]> ReadScalarColumnHexad(this ExcelWorksheet workSheet, int firstRowIndex, int rowCount, int firstColIndex)
        {
            var columnData1 = workSheet.ReadScalarColumn(
                firstRowIndex,
                rowCount,
                firstColIndex
            );

            var columnData2 = workSheet.ReadScalarColumn(
                firstRowIndex,
                rowCount,
                firstColIndex + 1
            );

            var columnData3 = workSheet.ReadScalarColumn(
                firstRowIndex,
                rowCount,
                firstColIndex + 2
            );
            
            var columnData4 = workSheet.ReadScalarColumn(
                firstRowIndex,
                rowCount,
                firstColIndex + 3
            );
            
            var columnData5 = workSheet.ReadScalarColumn(
                firstRowIndex,
                rowCount,
                firstColIndex + 4
            );
            
            var columnData6 = workSheet.ReadScalarColumn(
                firstRowIndex,
                rowCount,
                firstColIndex + 5
            );

            return new Hexad<double[]>(columnData1, columnData2, columnData3, columnData4, columnData5, columnData6);
        }
        
        public static double[] ReadExcelScalarColumn(this string excelFilePath, int firstRowIndex, int rowCount, int firstColIndex)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = 
                new ExcelPackage(new FileInfo(excelFilePath));

            return package
                .Workbook
                .Worksheets[0]
                .ReadScalarColumn(firstRowIndex, rowCount, firstColIndex);
        }

        public static Pair<double[]> ReadExcelScalarColumnPair(this string excelFilePath, int firstRowIndex, int rowCount, int firstColIndex)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = 
                new ExcelPackage(new FileInfo(excelFilePath));

            return package
                .Workbook
                .Worksheets[0]
                .ReadScalarColumnPair(firstRowIndex, rowCount, firstColIndex);
        }

        public static Triplet<double[]> ReadExcelScalarColumnTriplet(this string excelFilePath, int firstRowIndex, int rowCount, int firstColIndex)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = 
                new ExcelPackage(new FileInfo(excelFilePath));

            return package
                .Workbook
                .Worksheets[0]
                .ReadScalarColumnTriplet(firstRowIndex, rowCount, firstColIndex);
        }
        
        public static Quad<double[]> ReadExcelScalarColumnQuad(this string excelFilePath, int firstRowIndex, int rowCount, int firstColIndex)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = 
                new ExcelPackage(new FileInfo(excelFilePath));

            return package
                .Workbook
                .Worksheets[0]
                .ReadScalarColumnQuad(firstRowIndex, rowCount, firstColIndex);
        }
        
        public static Quint<double[]> ReadExcelScalarColumnQuint(this string excelFilePath, int firstRowIndex, int rowCount, int firstColIndex)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = 
                new ExcelPackage(new FileInfo(excelFilePath));

            return package
                .Workbook
                .Worksheets[0]
                .ReadScalarColumnQuint(firstRowIndex, rowCount, firstColIndex);
        }
        
        public static Hexad<double[]> ReadExcelScalarColumnHexad(this string excelFilePath, int firstRowIndex, int rowCount, int firstColIndex)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = 
                new ExcelPackage(new FileInfo(excelFilePath));

            return package
                .Workbook
                .Worksheets[0]
                .ReadScalarColumnHexad(firstRowIndex, rowCount, firstColIndex);
        }


        public static GaVector<ScalarSignalFloat64> ReadVectorSignal(this IGeometricAlgebraProcessor<ScalarSignalFloat64> geometricProcessor, double samplingRate, ExcelWorksheet workSheet, int firstRowIndex, int rowCount, int firstColIndex, int colCount)
        {
            var vectorArray = new ScalarSignalFloat64[colCount];

            for (var j = 0; j < colCount; j++)
            {
                var columnVector = new double[rowCount];

                for (var i = 0; i < rowCount; i++)
                    columnVector[i] = workSheet.Cells[firstRowIndex + i, firstColIndex + j].GetValue<double>();

                vectorArray[j] = columnVector.CreateSignal(samplingRate);
            }

            return geometricProcessor.CreateVector(vectorArray);
        }

        public static GaVector<double>[] ReadVectors(this IGeometricAlgebraProcessor<double> geometricProcessor, ExcelWorksheet workSheet, int rowIndex, int rowCount, int columnIndex, int columnCount)
        {
            var vectorArray = new GaVector<double>[rowCount];

            for (var i = 0; i < rowCount; i++)
            {
                var scalarArray = new double[columnCount];

                for (var j = 0; j < columnCount; j++)
                    scalarArray[j] = workSheet.Cells[rowIndex + i, columnIndex + j].GetValue<double>();
                
                vectorArray[i] = geometricProcessor.CreateVector(scalarArray);
            }

            return vectorArray;
        }

        public static GaVector<double>[] ReadVectors(this IGeometricAlgebraProcessor<double> geometricProcessor, ExcelWorksheet workSheet, int rowIndex, int rowCount, params int[] columnIndexArray)
        {
            var columnCount = columnIndexArray.Length;
            var vectorArray = new GaVector<double>[rowCount];

            for (var i = 0; i < rowCount; i++)
            {
                var scalarArray = new double[columnCount];

                for (var j = 0; j < columnCount; j++)
                    scalarArray[j] = workSheet.Cells[rowIndex + i, columnIndexArray[j]].GetValue<double>();
                
                vectorArray[i] = geometricProcessor.CreateVector(scalarArray);
            }

            return vectorArray;
        }
        

        public static ExcelWorksheet WriteIndices(this ExcelWorksheet worksheet, int rowIndex, int columnIndex, int firstIndex, int indexCount, string columnName)
        {
            worksheet.Cells[2, columnIndex].Value = columnName;
            
            for (var index = 0; index < indexCount; index++)
            {
                worksheet.Cells[rowIndex, columnIndex].Value = firstIndex + index;

                rowIndex++;
            }

            return worksheet;
        }

        public static ExcelWorksheet WriteScalars(this ExcelWorksheet worksheet, int rowIndex, int columnIndex, IEnumerable<double> scalarData, string columnName)
        {
            worksheet.Cells[2, columnIndex].Value = columnName;
            
            foreach (var value in scalarData)
            {
                worksheet.Cells[rowIndex, columnIndex].Value = value;

                rowIndex++;
            }

            return worksheet;
        }
        
        public static ExcelWorksheet WriteRowVectors(this ExcelWorksheet worksheet, int rowIndex, int columnIndex, IEnumerable<double[]> vectorData, params string[] columnNames)
        {
            var columnCount = columnNames.Length;
            
            for (var i = 0; i < columnCount; i++)
                worksheet.Cells[2, columnIndex + i].Value = columnNames[i];
            
            foreach (var vector in vectorData)
            {
                for (var i = 0; i < columnCount; i++)
                    worksheet.Cells[rowIndex, columnIndex + i].Value = vector[i];

                rowIndex++;
            }

            return worksheet;
        }

        public static ExcelWorksheet WriteVectors(this ExcelWorksheet worksheet, int rowIndex, int columnIndex, IEnumerable<GaVector<double>> vectorData, string vectorName, params string[] columnNames)
        {
            var columnCount = columnNames.Length;
            
            var headCell = worksheet.Cells[1, columnIndex, 1, columnIndex + columnCount - 1];
            headCell.Value = vectorName;
            headCell.Merge = true;

            for (var i = 0; i < columnCount; i++)
                worksheet.Cells[2, columnIndex + i].Value = columnNames[i];

            foreach (var value in vectorData)
            {
                for (var i = 0; i < columnCount; i++)
                    worksheet.Cells[rowIndex, columnIndex + i].Value = value[i].ScalarValue;

                rowIndex++;
            }

            return worksheet;
        }
        
        public static ExcelWorksheet WriteBivectors(this ExcelWorksheet worksheet, int rowIndex, int columnIndex, IEnumerable<GaBivector<double>> bivectorData, string bivectorName, params string[] columnNames)
        {
            var columnCount = columnNames.Length;
            
            var headCell = worksheet.Cells[1, columnIndex, 1, columnIndex + columnCount - 1];
            headCell.Value = bivectorName;
            headCell.Merge = true;

            for (var i = 0; i < columnNames.Length; i++)
                worksheet.Cells[2, columnIndex + i].Value = columnNames[i];
            
            //rowIndex++;

            foreach (var value in bivectorData)
            {
                for (var i = 0; i < columnNames.Length; i++)
                    worksheet.Cells[rowIndex, columnIndex + i].Value = value[i].ScalarValue;

                rowIndex++;
            }

            return worksheet;
        }

        public static ExcelWorksheet WriteScalarSignal(this ExcelWorksheet worksheet, int rowIndex, int columnIndex, ScalarSignalFloat64 scalarSignal, string columnName)
        {
            return worksheet.WriteScalars(rowIndex, columnIndex, scalarSignal, columnName);
        }

        public static ExcelWorksheet WriteScalarSignal(this ExcelWorksheet worksheet, int rowIndex, int columnIndex, Scalar<ScalarSignalFloat64> scalarSignal, string columnName)
        {
            return worksheet.WriteScalars(rowIndex, columnIndex, scalarSignal.ScalarValue, columnName);
        }

        public static ExcelWorksheet WriteVectorSignal(this ExcelWorksheet worksheet, int rowIndex, int columnIndex, GaVector<ScalarSignalFloat64> vectorSignal, string vectorName, params string[] columnNames)
        {
            var columnCount = columnNames.Length;
            
            var headCell = worksheet.Cells[1, columnIndex, 1, columnIndex + columnCount - 1];
            headCell.Value = vectorName;
            headCell.Merge = true;
            
            for (var i = 0; i < columnCount; i++)
                worksheet.Cells[2, columnIndex + i].Value = columnNames[i];
            
            for (var i = 0; i < columnCount; i++)
            {
                var v = vectorSignal[i].ScalarValue;

                for (var r = 0; r < v.Count; r++) 
                    worksheet.Cells[rowIndex + r, columnIndex + i].Value = v[r];
            }

            return worksheet;
        }

        public static ExcelWorksheet WriteBivectorSignal(this ExcelWorksheet worksheet, int rowIndex, int columnIndex, GaBivector<ScalarSignalFloat64> bivectorSignal, string bivectorName, params string[] columnNames)
        {
            var columnCount = columnNames.Length;
            
            var headCell = worksheet.Cells[1, columnIndex, 1, columnIndex + columnCount - 1];
            headCell.Value = bivectorName;
            headCell.Merge = true;

            for (var i = 0; i < columnNames.Length; i++)
                worksheet.Cells[2, columnIndex + i].Value = columnNames[i];
            
            for (var i = 0; i < columnNames.Length; i++)
            {
                var v = bivectorSignal[i].ScalarValue;

                for (var r = 0; r < v.Count; r++) 
                    worksheet.Cells[rowIndex + r, columnIndex + i].Value = v[r];
            }

            return worksheet;
        }


    }
}
