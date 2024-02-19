using System;
using System.Linq;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.MetaProgramming.Applications.Matlab.GradedMultivectorsLib.Combinations;
using GeometricAlgebraFulcrumLib.MetaProgramming.Applications.Matlab.GradedMultivectorsLib.Types;
using TextComposerLib;
using TextComposerLib.Files;
using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.Matlab.GradedMultivectorsLib;

public class LibUnilinearBasisToScalarMappingCodeComposer :
    LibSubCodeComposer
{
    public static LibUnilinearBasisToScalarMappingCodeComposer Create(
        LibCodeComposerSpecs specs,
        TextFilesComposer codeFilesComposer,
        string mappingFunctionName,
        Func<RGaBasisBlade, IRGaSignedBasisBlade> basisMapFunc
    )
    {
        return new LibUnilinearBasisToScalarMappingCodeComposer(specs, codeFilesComposer)
        {
            MappingFunctionName = mappingFunctionName,
            BasisMapFunc = basisMapFunc
        };
    }


    public string MappingFunctionName { get; init; }


    public Func<RGaBasisBlade, IRGaSignedBasisBlade> BasisMapFunc { get; init; }


    private LibUnilinearBasisToScalarMappingCodeComposer(LibCodeComposerSpecs specs, TextFilesComposer codeFilesComposer)
        : base(specs, codeFilesComposer)
    {
    }


    private void GetUnilinearOperationCode(LibUnilinearCombination termTable)
    {
        if (termTable.OutputType is not LibTypeKVector { Grade: 0 })
            throw new InvalidOperationException();

        var scalarCode =
            termTable
                .GetIdTermListPairs()
                .OrderBy(p => 
                    termTable.OutputType.GetScalarIndex(p.Key)
                ).Select(p =>
                    {
                        var lhsIdCode = GradedMultivectorLibUtils.GetBasisBladeCode(
                            p.Key,
                            id => termTable.OutputType.GetScalarName("outMv", id)
                        );

                        var rhsCode = p.Value.GetRhsCode(
                            id => termTable.InputType.GetScalarName("inMv", id)
                        );

                        return $"{lhsIdCode} = {rhsCode};";
                    }
                ).Concatenate(Environment.NewLine);
        
        var initCode = @$"
arguments
    inMv ({termTable.InputType.ScalarCount},:) double
end

sampleCount = size(inMv, 2);

outMv = zeros([{termTable.OutputType.ScalarCount}, sampleCount], 'double');
".Trim();

        var functionName = 
            $"{MappingFunctionName}{termTable.InputType.TypeSymbol}";

        CodeFilesComposer.InitializeFile($"{functionName}.m");

        var codeComposer = CodeFilesComposer.ActiveFileTextComposer;

        codeComposer
            .AppendLine(Specs.GetFileHeaderText())
            .AppendLine($"function outMv = {functionName}(inMv)")
            .IncreaseIndentation()
            .AppendLine(initCode)
            .AppendLine()
            .AppendLine(scalarCode)
            .DecreaseIndentation()
            .AppendLine("end")
            .AppendLine();

        CodeFilesComposer.ActiveFileComposer.FinalizeText(
            code => code.RemoveRepeatedEmptyLines()
        );
    }


    public override TextFilesComposer GenerateCode()
    {
        foreach (var inType1 in Types)
        {
            var termTable = 
                LibUnilinearCombination.Create(
                    inType1,
                    BasisMapFunc
                ).SelectOutputType(Types);

            GetUnilinearOperationCode(termTable);
        }

        return CodeFilesComposer;
    }
}