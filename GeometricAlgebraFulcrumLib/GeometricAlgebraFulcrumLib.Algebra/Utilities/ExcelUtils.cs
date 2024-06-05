using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Differential.Functions.Interpolators;
using GeometricAlgebraFulcrumLib.Core.Algebra.Signals;
using GeometricAlgebraFulcrumLib.Core.Algebra.Signals.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using OfficeOpenXml;

namespace GeometricAlgebraFulcrumLib.Algebra.Utilities;

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


    public static RGaVector<Float64Signal> ReadVectorSignal(this RGaProcessor<Float64Signal> geometricProcessor, double samplingRate, ExcelWorksheet workSheet, int firstRowIndex, int rowCount, int firstColIndex, int colCount)
    {
        var vectorArray = new Float64Signal[colCount];

        for (var j = 0; j < colCount; j++)
        {
            var columnVector = new double[rowCount];

            for (var i = 0; i < rowCount; i++)
                columnVector[i] = workSheet.Cells[firstRowIndex + i, firstColIndex + j].GetValue<double>();

            vectorArray[j] = columnVector.CreateSignal(samplingRate);
        }

        return geometricProcessor.Vector(vectorArray);
    }

    public static RGaVector<double>[] ReadVectors(this RGaProcessor<double> geometricProcessor, ExcelWorksheet workSheet, int rowIndex, int rowCount, int columnIndex, int columnCount)
    {
        var vectorArray = new RGaVector<double>[rowCount];

        for (var i = 0; i < rowCount; i++)
        {
            var scalarArray = new double[columnCount];

            for (var j = 0; j < columnCount; j++)
                scalarArray[j] = workSheet.Cells[rowIndex + i, columnIndex + j].GetValue<double>();

            vectorArray[i] = geometricProcessor.Vector(scalarArray);
        }

        return vectorArray;
    }

    public static RGaVector<double>[] ReadVectors(this RGaProcessor<double> geometricProcessor, ExcelWorksheet workSheet, int rowIndex, int rowCount, params int[] columnIndexArray)
    {
        var columnCount = columnIndexArray.Length;
        var vectorArray = new RGaVector<double>[rowCount];

        for (var i = 0; i < rowCount; i++)
        {
            var scalarArray = new double[columnCount];

            for (var j = 0; j < columnCount; j++)
                scalarArray[j] = workSheet.Cells[rowIndex + i, columnIndexArray[j]].GetValue<double>();

            vectorArray[i] = geometricProcessor.Vector(scalarArray);
        }

        return vectorArray;
    }


    public static XGaVector<Float64Signal> ReadVectorSignal(this XGaProcessor<Float64Signal> geometricProcessor, double samplingRate, ExcelWorksheet workSheet, int firstRowIndex, int rowCount, int firstColIndex, int colCount)
    {
        var vectorArray = new Float64Signal[colCount];

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

    public static ExcelWorksheet WriteVectors(this ExcelWorksheet worksheet, int rowIndex, int columnIndex, IEnumerable<RGaVector<double>> vectorData, string vectorName, params string[] columnNames)
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

    public static ExcelWorksheet WriteBivectors(this ExcelWorksheet worksheet, int rowIndex, int columnIndex, IEnumerable<RGaFloat64Bivector> bivectorData, string bivectorName, params string[] columnNames)
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

    public static ExcelWorksheet WriteScalarSignal(this ExcelWorksheet worksheet, int rowIndex, int columnIndex, Float64Signal scalarSignal, string columnName)
    {
        return worksheet.WriteScalars(rowIndex, columnIndex, scalarSignal, columnName);
    }

    public static ExcelWorksheet WriteScalarSignal(this ExcelWorksheet worksheet, int rowIndex, int columnIndex, Scalar<Float64Signal> scalarSignal, string columnName)
    {
        return worksheet.WriteScalars(rowIndex, columnIndex, scalarSignal.ScalarValue, columnName);
    }

    public static ExcelWorksheet WriteScalarSignal(this ExcelWorksheet worksheet, int rowIndex, int columnIndex, IScalar<Float64Signal> scalarSignal, string columnName)
    {
        return worksheet.WriteScalars(rowIndex, columnIndex, scalarSignal.ScalarValue, columnName);
    }

    public static ExcelWorksheet WriteVectorSignal(this ExcelWorksheet worksheet, int rowIndex, int columnIndex, RGaVector<Float64Signal> vectorSignal, string vectorName, params string[] columnNames)
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

    public static ExcelWorksheet WriteVectorSignal(this ExcelWorksheet worksheet, int rowIndex, int columnIndex, XGaVector<Float64Signal> vectorSignal, string vectorName, params string[] columnNames)
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

    public static ExcelWorksheet WriteBivectorSignal(this ExcelWorksheet worksheet, int rowIndex, int columnIndex, RGaBivector<Float64Signal> bivectorSignal, string bivectorName, params string[] columnNames)
    {
        var columnCount = columnNames.Length;

        var headCell = worksheet.Cells[1, columnIndex, 1, columnIndex + columnCount - 1];
        headCell.Value = bivectorName;
        headCell.Merge = true;

        for (var i = 0; i < columnNames.Length; i++)
            worksheet.Cells[2, columnIndex + i].Value = columnNames[i];

        for (var i = 0; i < columnNames.Length; i++)
        {
            var v = bivectorSignal.Scalar(i).ScalarValue;

            for (var r = 0; r < v.Count; r++)
                worksheet.Cells[rowIndex + r, columnIndex + i].Value = v[r];
        }

        return worksheet;
    }

    public static ExcelWorksheet WriteBivectorSignal(this ExcelWorksheet worksheet, int rowIndex, int columnIndex, XGaBivector<Float64Signal> bivectorSignal, string bivectorName, params string[] columnNames)
    {
        var columnCount = columnNames.Length;

        var headCell = worksheet.Cells[1, columnIndex, 1, columnIndex + columnCount - 1];
        headCell.Value = bivectorName;
        headCell.Merge = true;

        for (var i = 0; i < columnNames.Length; i++)
            worksheet.Cells[2, columnIndex + i].Value = columnNames[i];

        for (var i = 0; i < columnNames.Length; i++)
        {
            var id = i.BasisBivectorIndexToId().BitPatternToIndexSet();

            var v = bivectorSignal.GetBasisBladeScalar(id).ScalarValue;

            for (var r = 0; r < v.Count; r++)
                worksheet.Cells[rowIndex + r, columnIndex + i].Value = v[r];
        }

        return worksheet;
    }


    public static DifferentialSignalInterpolatorFunction GetDifferentialInterpolatorOfColumn(this ExcelWorksheet workSheet, int firstRow, int columnIndex, Float64SignalSamplingSpecs samplingSpecs, int sampleStep, DfSignalInterpolatorOptions interpolatorOptions)
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