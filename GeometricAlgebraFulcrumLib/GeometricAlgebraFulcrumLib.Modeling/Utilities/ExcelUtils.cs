using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64.Interpolators;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Modeling.Signals.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using OfficeOpenXml;

namespace GeometricAlgebraFulcrumLib.Modeling.Utilities;

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

    public static double[] ReadScalarColumn(this ExcelWorksheet workSheet, int rowIndex, int rowCount, int rowStep, int columnIndex)
    {
        var scalarArray = new double[rowCount];

        for (var i = 0; i < rowCount; i++)
        {
            scalarArray[i] = workSheet.Cells[rowIndex + i * rowStep, columnIndex].GetValue<double>();
        }

        return scalarArray;
    }

    public static double[] ReadScalarColumn(this IScalarProcessor<double> scalarProcessor, ExcelWorksheet workSheet, int rowIndex, int rowCount, int columnIndex)
    {
        return workSheet.ReadScalarColumn(rowIndex, rowCount, columnIndex);
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


    public static XGaVector<double>[] ReadVectors(this XGaProcessor<double> geometricProcessor, ExcelWorksheet workSheet, int rowIndex, int rowCount, int columnIndex, int columnCount)
    {
        var vectorArray = new XGaVector<double>[rowCount];

        for (var i = 0; i < rowCount; i++)
        {
            var scalarArray = new double[columnCount];

            for (var j = 0; j < columnCount; j++)
                scalarArray[j] = workSheet.Cells[rowIndex + i, columnIndex + j].GetValue<double>();

            vectorArray[i] = geometricProcessor.Vector(scalarArray);
        }

        return vectorArray;
    }

    public static XGaVector<double>[] ReadVectors(this XGaProcessor<double> geometricProcessor, ExcelWorksheet workSheet, int rowIndex, int rowCount, params int[] columnIndexArray)
    {
        var columnCount = columnIndexArray.Length;
        var vectorArray = new XGaVector<double>[rowCount];

        for (var i = 0; i < rowCount; i++)
        {
            var scalarArray = new double[columnCount];

            for (var j = 0; j < columnCount; j++)
                scalarArray[j] = workSheet.Cells[rowIndex + i, columnIndexArray[j]].GetValue<double>();

            vectorArray[i] = geometricProcessor.Vector(scalarArray);
        }

        return vectorArray;
    }


    public static XGaVector<Float64SampledTimeSignal> ReadVectorSignal(this XGaProcessor<Float64SampledTimeSignal> geometricProcessor, double samplingRate, ExcelWorksheet workSheet, int firstRowIndex, int rowCount, int firstColIndex, int colCount)
    {
        var vectorArray = new Float64SampledTimeSignal[colCount];

        for (var j = 0; j < colCount; j++)
        {
            var columnVector = new double[rowCount];

            for (var i = 0; i < rowCount; i++)
                columnVector[i] = workSheet.Cells[firstRowIndex + i, firstColIndex + j].GetValue<double>();

            vectorArray[j] = columnVector.CreateSignal(samplingRate);
        }

        return geometricProcessor.Vector(vectorArray);
    }

    public static XGaFloat64Vector[] ReadVectors(this XGaFloat64Processor geometricProcessor, ExcelWorksheet workSheet, int rowIndex, int rowCount, int columnIndex, int columnCount)
    {
        var vectorArray = new XGaFloat64Vector[rowCount];

        for (var i = 0; i < rowCount; i++)
        {
            var scalarArray = new double[columnCount];

            for (var j = 0; j < columnCount; j++)
                scalarArray[j] = workSheet.Cells[rowIndex + i, columnIndex + j].GetValue<double>();

            vectorArray[i] = geometricProcessor.Vector(scalarArray);
        }

        return vectorArray;
    }

    public static XGaFloat64Vector[] ReadVectors(this XGaFloat64Processor geometricProcessor, ExcelWorksheet workSheet, int rowIndex, int rowCount, params int[] columnIndexArray)
    {
        var columnCount = columnIndexArray.Length;
        var vectorArray = new XGaFloat64Vector[rowCount];

        for (var i = 0; i < rowCount; i++)
        {
            var scalarArray = new double[columnCount];

            for (var j = 0; j < columnCount; j++)
                scalarArray[j] = workSheet.Cells[rowIndex + i, columnIndexArray[j]].GetValue<double>();

            vectorArray[i] = geometricProcessor.Vector(scalarArray);
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

    public static ExcelWorksheet WriteVectors(this ExcelWorksheet worksheet, int rowIndex, int columnIndex, IEnumerable<XGaVector<double>> vectorData, string vectorName, params string[] columnNames)
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
                worksheet.Cells[rowIndex, columnIndex + i].Value = value.Scalar(i).ScalarValue;

            rowIndex++;
        }

        return worksheet;
    }

    public static ExcelWorksheet WriteBivectors(this ExcelWorksheet worksheet, int rowIndex, int columnIndex, IEnumerable<XGaFloat64Bivector> bivectorData, string bivectorName, params string[] columnNames)
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
                worksheet.Cells[rowIndex, columnIndex + i].Value = value.Scalar(i);

            rowIndex++;
        }

        return worksheet;
    }

    public static ExcelWorksheet WriteScalarSignal(this ExcelWorksheet worksheet, int rowIndex, int columnIndex, Float64SampledTimeSignal scalarSignal, string columnName)
    {
        return worksheet.WriteScalars(rowIndex, columnIndex, scalarSignal, columnName);
    }

    public static ExcelWorksheet WriteScalarSignal(this ExcelWorksheet worksheet, int rowIndex, int columnIndex, Scalar<Float64SampledTimeSignal> scalarSignal, string columnName)
    {
        return worksheet.WriteScalars(rowIndex, columnIndex, scalarSignal.ScalarValue, columnName);
    }

    public static ExcelWorksheet WriteScalarSignal(this ExcelWorksheet worksheet, int rowIndex, int columnIndex, IScalar<Float64SampledTimeSignal> scalarSignal, string columnName)
    {
        return worksheet.WriteScalars(rowIndex, columnIndex, scalarSignal.ScalarValue, columnName);
    }

    public static ExcelWorksheet WriteVectorSignal(this ExcelWorksheet worksheet, int rowIndex, int columnIndex, XGaVector<Float64SampledTimeSignal> vectorSignal, string vectorName, params string[] columnNames)
    {
        var columnCount = columnNames.Length;

        var headCell = worksheet.Cells[1, columnIndex, 1, columnIndex + columnCount - 1];
        headCell.Value = vectorName;
        headCell.Merge = true;

        for (var i = 0; i < columnCount; i++)
            worksheet.Cells[2, columnIndex + i].Value = columnNames[i];

        for (var i = 0; i < columnCount; i++)
        {
            var v = vectorSignal.Scalar(i).ScalarValue;

            for (var r = 0; r < v.Count; r++)
                worksheet.Cells[rowIndex + r, columnIndex + i].Value = v[r];
        }

        return worksheet;
    }

    public static ExcelWorksheet WriteBivectorSignal(this ExcelWorksheet worksheet, int rowIndex, int columnIndex, XGaBivector<Float64SampledTimeSignal> bivectorSignal, string bivectorName, params string[] columnNames)
    {
        var columnCount = columnNames.Length;

        var headCell = worksheet.Cells[1, columnIndex, 1, columnIndex + columnCount - 1];
        headCell.Value = bivectorName;
        headCell.Merge = true;

        for (var i = 0; i < columnNames.Length; i++)
            worksheet.Cells[2, columnIndex + i].Value = columnNames[i];

        for (var i = 0; i < columnNames.Length; i++)
        {
            var id = i.BasisBivectorIndexToId();

            var v = bivectorSignal.GetBasisBladeScalar(id).ScalarValue;

            for (var r = 0; r < v.Count; r++)
                worksheet.Cells[rowIndex + r, columnIndex + i].Value = v[r];
        }

        return worksheet;
    }


    public static DifferentialSignalInterpolatorFunction GetDifferentialInterpolatorOfColumn(this ExcelWorksheet workSheet, int firstRow, int columnIndex, Float64SamplingSpecs samplingSpecs, int sampleStep, DfSignalInterpolatorOptions interpolatorOptions)
    {
        var signal =
            workSheet
                .ReadScalarColumn(
                    firstRow,
                    samplingSpecs.SampleCount,
                    sampleStep,
                    columnIndex
                ).CreateSignal(samplingSpecs.SamplingRate);

        // Remove DC component (running average)
        signal -= signal.GetRunningAverageSignal();

        return signal.GetDifferentialInterpolator(interpolatorOptions);
    }

}