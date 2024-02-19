using System.Text;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Basis;
using TextComposerLib.Files;
using TextComposerLib.Text;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.Matlab.GradedMultivectorsLib;

public class LibMainCodeComposer
{
    public LibCodeComposerSpecs Specs { get; }
    

    internal LibMainCodeComposer(LibCodeComposerSpecs specs)
    {
        Specs = specs;
    }

    private string GetCreateDataArrayCode()
    {
        var switchCodeComposer = new LinearTextComposer();

        for (var g = 0; g <= Specs.VSpaceDimensions; g++)
            switchCodeComposer
                .AppendLine($"case {g}")
                .AppendLine($"    scalarCount = {Specs.KVectorTypes[g].KvSpaceDimensions};");

        switchCodeComposer
            .AppendLine($"otherwise")
            .AppendLine($"    scalarCount = {Specs.MultivectorType.GaSpaceDimensions};");
        
        return $@"
function outMvData = CreateDataArray(grade, sampleCount)
    arguments
        grade (1, 1) int32
        sampleCount (1, 1) uint32
    end

    switch grade
{switchCodeComposer.ToString().PrefixTextLines("        ").TrimEnd()}
    end

    outMvData = zeros(scalarCount, sampleCount, 'double');
end
".Trim();
    }

    private string GetEncodeScalarCode()
    {
        var methodCode = @$"
{Specs.GetFileHeaderText()}
function outMv = EncodeScalar(scalarArray)
    arguments
        scalarArray (1,:) {{mustBeNumeric}}
    end
    
    %EncodeScalar Create a scalar multivector from scalar coefficients
    %   Examples: 
    %   s = ga3.EncodeScalar(1.2);
    %
    %   This creates a multi-sample multivector containing 5 sampled scalars
    %       sSignal = ga3.EncodeScalar([1.2, -1.4, 2.5, 0.25, -1.3]);
    
    outMv = {Specs.GetQualifiedMultivectorClassName()}(0, double(scalarArray));
end
".Trim();

        return methodCode;
    }

    private string GetEncodeVectorCode()
    {
        var methodCode = @$"
{Specs.GetFileHeaderText()}
function outMv = EncodeVector(scalarArray)
    arguments
        scalarArray {{mustBeNumeric}}
    end
    
    %EncodeVector Create a vector from scalar coefficients
    %   Examples: 
    %   The following 3 calls are equivalent:
    %       v = ga3.EncodeVector([1.2, -3.1, 2.4]);
    %       v = ga3.EncodeVector([1.2; -3.1; 2.4]);
    %
    %   This creates a multi-sample multivector containing 2 sampled vectors
    %       vSignal = ga3.EncodeVector([1.2; -3.1; 2.4, 0; -1.4; 1.2]);
    
    size1 = size(scalarArray);
    
    if (length(size1) == 2 && size1(1) == 1 && size1(2) == {Specs.VSpaceDimensions})
        outMv = {Specs.GetQualifiedMultivectorClassName()}(1, double(scalarArray'));
        return;
    end

    if (length(size1) ~= 2 || size1(1) ~= {Specs.VSpaceDimensions})
        error('Incorrect input size');
    end
    
    outMv = {Specs.GetQualifiedMultivectorClassName()}(1, double(scalarArray));
end
".Trim();

        return methodCode;
    }

    private string GetEncodeEGaVectorCode()
    {
        var methodCode = @$"
{Specs.GetFileHeaderText()}
function outMv = EncodeEGaVector(scalarArray)
    arguments
        scalarArray {{mustBeNumeric}}
    end
    
    %EncodeEGaVector Create a EGA vector from the input Euclidean coordinates
    %   Examples: 
    %   The following calls are equivalent:
    %       v = ga41.EncodeEGaVector([1.2, -3.1, 2.4]);
    %       v = ga41.EncodeEGaVector([1.2; -3.1; 2.4]);
    %
    %   This creates a multi-sample multivector containing 2 sampled EGA vectors
    %       vSignal = ga41.EncodeEGaVector([1.2; -3.1; 2.4, 0; -1.4; 1.2]);
    
    size1 = size(scalarArray);
    
    if (length(size1) == 2 && size1(1) == 1 && size1(2) == {Specs.VSpaceDimensions - 2})
        outMv = {Specs.GetQualifiedMultivectorClassName()}(1, [0; 0; double(scalarArray')]);

        return;
    end

    if (length(size1) ~= 2 || size1(1) ~= {Specs.VSpaceDimensions - 2})
        error('Incorrect input size');
    end
    
    outMv = {Specs.GetQualifiedMultivectorClassName()}(1, [zeros(2, size1(2)); x]);
end
".Trim();

        return methodCode;
    }

    private string GetEncodeCGaPointCode()
    {
        var methodCode = @$"
{Specs.GetFileHeaderText()}
function outMv = EncodeCGaPoint(scalarArray)
    arguments
        scalarArray {{mustBeNumeric}}
    end
    
    %EncodeCGaPoint Create a null CGA vector from the Euclidean coordinates of the point
    %   Examples: 
    %   The following calls are equivalent:
    %       v = ga41.EncodeCGaPoint([1.2, -3.1, 2.4]);
    %       v = ga41.EncodeCGaPoint([1.2; -3.1; 2.4]);
    %
    %   This creates a multi-sample multivector containing 2 sampled CGA null vectors
    %       vSignal = ga41.EncodeCGaPoint([1.2; -3.1; 2.4, 0; -1.4; 1.2]);
    
    size1 = size(scalarArray);
    
    if (length(size1) == 2 && size1(1) == 1 && size1(2) == {Specs.VSpaceDimensions - 2})
        x = double(scalarArray');
        x2 = sum(x .* x);
        en = (1 + x2) * 0.5;
        ep = (1 - x2) * 0.5;

        outMv = {Specs.GetQualifiedMultivectorClassName()}(1, [en; ep; x]);

        return;
    end

    if (length(size1) ~= 2 || size1(1) ~= {Specs.VSpaceDimensions - 2})
        error('Incorrect input size');
    end
    
    x = double(scalarArray);
    x2 = sum(x .* x);
    en = (1 + x2) * 0.5;
    ep = (1 - x2) * 0.5;

    outMv = {Specs.GetQualifiedMultivectorClassName()}(1, [en; ep; x]);
end
".Trim();

        return methodCode;
    }
    
    private string GetEncodeCGaTranslatorCode()
    {
        var spaceName = Specs.GaSpaceName.ToLower();

        var methodCode = @$"
{Specs.GetFileHeaderText()}
function outMv = EncodeCGaTranslator(scalarArray)
    arguments
        scalarArray {{mustBeNumeric}}
    end
    
    %EncodeCGaTranslator Create a CGA translator from the input Euclidean vectors
    %   Examples: 
    %   The following calls are equivalent:
    %       v = ga41.EncodeCGaTranslator([1.2, -3.1, 2.4]);
    %       v = ga41.EncodeCGaTranslator([1.2; -3.1; 2.4]);
    %
    %   This creates a multi-sample multivector containing 2 sampled CGA translators
    %       vSignal = ga41.EncodeCGaTranslator([1.2; -3.1; 2.4, 0; -1.4; 1.2]);
    
    t = {spaceName}.EncodeEGaVector(scalarArray);
    outMv = {spaceName}.EncodeScalar(1) + t.op({spaceName}.ei()).op({spaceName}.EncodeScalar(-0.5));
end
".Trim();

        return methodCode;
    }
    
    private string GetCGaEnBasisBlade()
    {
        return $@"
function outMv = en(sampleCount)
    if (nargin == 0)
        sampleCount = 1;
    end

    outMvData = zeros({Specs.VSpaceDimensions}, uint32(sampleCount));
    outMvData(1,:) = 1;

    outMv = {Specs.GetQualifiedMultivectorClassName()}(1, outMvData);
end
".Trim();
    }
    
    private string GetCGaEpBasisBlade()
    {
        return $@"
function outMv = ep(sampleCount)
    if (nargin == 0)
        sampleCount = 1;
    end

    outMvData = zeros({Specs.VSpaceDimensions}, uint32(sampleCount));
    outMvData(2,:) = 1;

    outMv = {Specs.GetQualifiedMultivectorClassName()}(1, outMvData);
end
".Trim();
    }
    
    private string GetCGaEoBasisBlade()
    {
        return $@"
function outMv = eo(sampleCount)
    if (nargin == 0)
        sampleCount = 1;
    end

    outMvData = zeros({Specs.VSpaceDimensions}, uint32(sampleCount));
    outMvData(1,:) = 0.5;
    outMvData(2,:) = 0.5;

    outMv = {Specs.GetQualifiedMultivectorClassName()}(1, outMvData);
end
".Trim();
    }
    
    private string GetCGaEiBasisBlade()
    {
        return $@"
function outMv = ei(sampleCount)
    if (nargin == 0)
        sampleCount = 1;
    end

    outMvData = zeros({Specs.VSpaceDimensions}, uint32(sampleCount));
    outMvData(1,:) = 1;
    outMvData(2,:) = -1;

    outMv = {Specs.GetQualifiedMultivectorClassName()}(1, outMvData);
end
".Trim();
    }
    
    private string GetCGaEoiBasisBlade()
    {
        var bivectorSpaceDimensions = 
            Specs.KVectorTypes[2].KvSpaceDimensions;

        return $@"
function outMv = eoi(sampleCount)
    if (nargin == 0)
        sampleCount = 1;
    end

    outMvData = zeros({bivectorSpaceDimensions}, uint32(sampleCount));
    outMvData(1,:) = -1;

    outMv = {Specs.GetQualifiedMultivectorClassName()}(2, outMvData);
end
".Trim();
    }
    
    private string GetCGaIeBasisBlade()
    {
        var kvSpaceDimensions = 
            Specs.KVectorTypes[Specs.VSpaceDimensions - 2].KvSpaceDimensions;

        return $@"
function outMv = Ie(sampleCount)
    if (nargin == 0)
        sampleCount = 1;
    end

    outMvData = zeros({kvSpaceDimensions}, uint32(sampleCount));
    outMvData({kvSpaceDimensions},:) = 1;

    outMv = {Specs.GetQualifiedMultivectorClassName()}({Specs.VSpaceDimensions - 2}, outMvData);
end
".Trim();
    }
    
    private string GetCGaIcBasisBlade()
    {
        return $@"
function outMv = Ic(sampleCount)
    if (nargin == 0)
        sampleCount = 1;
    end

    outMvData = zeros(1, uint32(sampleCount));
    outMvData(1,:) = 1;

    outMv = {Specs.GetQualifiedMultivectorClassName()}({Specs.VSpaceDimensions}, outMvData);
end
".Trim();
    }
    
    private string GetSpaceZeroScalar()
    {
        return $@"
function outMv = zero(sampleCount)
    if (nargin == 0)
        sampleCount = 1;
    end

    outMvData = zeros(1, uint32(sampleCount));
    
    outMv = {Specs.GetQualifiedMultivectorClassName()}(0, outMvData);
end
".Trim();
    }
    
    private string GetSpaceOneScalar()
    {
        return $@"
function outMv = one(sampleCount)
    if (nargin == 0)
        sampleCount = 1;
    end

    outMvData = zeros(1, uint32(sampleCount));

    outMvData(1,:) = 1;

    outMv = {Specs.GetQualifiedMultivectorClassName()}(0, outMvData);
end
".Trim();
    }

    private string GetSpaceIBasisBlade()
    {
        return $@"
function outMv = I(sampleCount)
    if (nargin == 0)
        sampleCount = 1;
    end

    outMvData = zeros(1, uint32(sampleCount));
    outMvData(1,:) = 1;

    outMv = {Specs.GetQualifiedMultivectorClassName()}({Specs.VSpaceDimensions}, outMvData);
end
".Trim();
    }
    
    private string GetSpaceIinvBasisBlade()
    {
        var signText = 
            Specs.Metric.CreateBasisPseudoScalarInverse(Specs.VSpaceDimensions).Sign.IsPositive
                ? "1" : "-1";

        return $@"
function outMv = Iinv(sampleCount)
    if (nargin == 0)
        sampleCount = 1;
    end

    outMvData = zeros(1, uint32(sampleCount));
    outMvData(1,:) = {signText};

    outMv = {Specs.GetQualifiedMultivectorClassName()}({Specs.VSpaceDimensions}, outMvData);
end
".Trim();
    }
    
    private string GetSpaceIrevBasisBlade()
    {
        var signText = 
            Specs.Metric.CreateBasisPseudoScalarReverse(Specs.VSpaceDimensions).Sign.IsPositive
            ? "1" : "-1";

        return $@"
function outMv = Irev(sampleCount)
    if (nargin == 0)
        sampleCount = 1;
    end

    outMvData = zeros(1, uint32(sampleCount));
    outMvData(1,:) = {signText};

    outMv = {Specs.GetQualifiedMultivectorClassName()}({Specs.VSpaceDimensions}, outMvData);
end
".Trim();
    }

    private TextFilesComposer GenerateCGaCode(TextFilesComposer codeFilesComposer)
    {
        codeFilesComposer
            .InitializeFile("EncodeEGaVector.m")
            .ActiveFileTextComposer
            .AppendLine(GetEncodeEGaVectorCode());

        codeFilesComposer
            .InitializeFile("EncodeCGaPoint.m")
            .ActiveFileTextComposer
            .AppendLine(GetEncodeCGaPointCode());
        
        codeFilesComposer
            .InitializeFile("EncodeCGaTranslator.m")
            .ActiveFileTextComposer
            .AppendLine(GetEncodeCGaTranslatorCode());

        codeFilesComposer
            .InitializeFile("en.m")
            .ActiveFileTextComposer
            .AppendLine(GetCGaEnBasisBlade());
        
        codeFilesComposer
            .InitializeFile("ep.m")
            .ActiveFileTextComposer
            .AppendLine(GetCGaEpBasisBlade());
        
        codeFilesComposer
            .InitializeFile("eo.m")
            .ActiveFileTextComposer
            .AppendLine(GetCGaEoBasisBlade());
        
        codeFilesComposer
            .InitializeFile("ei.m")
            .ActiveFileTextComposer
            .AppendLine(GetCGaEiBasisBlade());
        
        codeFilesComposer
            .InitializeFile("eoi.m")
            .ActiveFileTextComposer
            .AppendLine(GetCGaEoiBasisBlade());
        
        codeFilesComposer
            .InitializeFile("Ie.m")
            .ActiveFileTextComposer
            .AppendLine(GetCGaIeBasisBlade());
        
        codeFilesComposer
            .InitializeFile("Ic.m")
            .ActiveFileTextComposer
            .AppendLine(GetCGaIcBasisBlade());

        return codeFilesComposer;
    }

    public TextFilesComposer GenerateCode()
    {
        var codeFilesComposer = new TextFilesComposer
        {
            FilesEncoding = Encoding.ASCII
        };

        codeFilesComposer.DownFolder("+" + Specs.GaSpaceName.ToLower());
        
        codeFilesComposer
            .InitializeFile(
                "CreateDataArray.m", 
                GetCreateDataArrayCode()
            ).InitializeFile(
                "EncodeScalar.m", 
                GetEncodeScalarCode()
            ).InitializeFile(
                "EncodeVector.m", 
                GetEncodeVectorCode()
            ).InitializeFile(
                "zero.m",
                GetSpaceZeroScalar()
            ).InitializeFile(
                "one.m", 
                GetSpaceOneScalar()
            ).InitializeFile(
                "I.m",
                GetSpaceIBasisBlade()
            ).InitializeFile(
                "Irev.m", 
                GetSpaceIrevBasisBlade()
            );

        if (!Specs.Metric.IsDegenerate)
            codeFilesComposer.InitializeFile(
                "Iinv.m", 
                GetSpaceIinvBasisBlade()
                );

        if (Specs.Metric.IsConformal)
            GenerateCGaCode(codeFilesComposer);

        codeFilesComposer.DownFolder("@" + Specs.MultivectorType.ClassName);

        //for (var grade = 0; grade <= Specs.VSpaceDimensions; grade++)
        //    LibKVectorCodeComposer
        //        .Create(Specs, codeComposer, grade)
        //        .GenerateCode();

        LibMultivectorCodeComposer
            .Create(Specs, codeFilesComposer)
            .GenerateCode();

        codeFilesComposer.DownFolder("private");

        //LibProjectionCodeComposer
        //    .Create(Specs, codeComposer)
        //    .GenerateCode();

        //LibReflectionCodeComposer
        //    .Create(Specs, codeComposer)
        //    .GenerateCode();

        //LibRotorMapCodeComposer
        //    .Create(Specs, codeComposer)
        //    .GenerateCode();

        //LibVersorMapCodeComposer
        //    .Create(Specs, codeComposer)
        //    .GenerateCode();

        //LibLaTeXCodeComposer
        //    .Create(Specs, codeComposer)
        //    .GenerateCode();
        
        LibUnilinearBasisToBasisMappingCodeComposer.Create(
            Specs,
            codeFilesComposer,
            "negative",
            b1 => b1.Negative()
        ).GenerateCode();

        LibUnilinearBasisToBasisMappingCodeComposer.Create(
            Specs,
            codeFilesComposer,
            "reverse",
            b1 => b1.Reverse()
        ).GenerateCode();
        
        LibUnilinearBasisToBasisMappingCodeComposer.Create(
            Specs,
            codeFilesComposer,
            "gradeInvolution",
            b1 => b1.GradeInvolution()
        ).GenerateCode();
        
        LibUnilinearBasisToBasisMappingCodeComposer.Create(
            Specs,
            codeFilesComposer,
            "cliffordConjugate",
            b1 => b1.CliffordConjugate()
        ).GenerateCode();
        
        LibUnilinearBasisToBasisMappingCodeComposer.Create(
            Specs,
            codeFilesComposer,
            "conjugate",
            b1 => b1.Conjugate()
        ).GenerateCode();

        if (!Specs.Metric.IsDegenerate)
        {
            var basisPseudoScalarInverse =
                Specs.GetBasisPseudoScalarInverse();

            LibUnilinearBasisToBasisMappingCodeComposer.Create(
                Specs,
                codeFilesComposer,
                "dual",
                b1 => b1.Lcp(basisPseudoScalarInverse)
            ).GenerateCode();
        }
        
        var basisPseudoScalar = 
            Specs.GetBasisPseudoScalar();
        
        LibUnilinearBasisToBasisMappingCodeComposer.Create(
            Specs,
            codeFilesComposer,
            "unDual",
            b1 => b1.Lcp(basisPseudoScalar)
        ).GenerateCode();
        

        LibBilinearBasisToScalarMappingCodeComposer.Create(
            Specs,
            codeFilesComposer,
            "normSquared",
            true,
            (b1, b2) => b1.Sp(b2.Reverse())
        ).GenerateCode();
        
        LibBilinearBasisToScalarMappingCodeComposer.Create(
            Specs,
            codeFilesComposer,
            "pseudoNormSquared",
            true,
            (b1, b2) => b1.Sp(b2.Conjugate())
        ).GenerateCode();


        LibBilinearBasisToBasisMappingCodeComposer.Create(
            Specs,
            codeFilesComposer,
            "sp",
            false,
            (b1, b2) => b1.Sp(b2)
        ).GenerateCode();

        LibBilinearBasisToBasisMappingCodeComposer.Create(
            Specs,
            codeFilesComposer,
            "op",
            false,
            (b1, b2) => b1.Op(b2)
        ).GenerateCode();

        LibBilinearBasisToBasisMappingCodeComposer.Create(
            Specs,
            codeFilesComposer,
            "lcp",
            false,
            (b1, b2) => b1.Lcp(b2)
        ).GenerateCode();

        LibBilinearBasisToBasisMappingCodeComposer.Create(
            Specs,
            codeFilesComposer,
            "rcp",
            false,
            (b1, b2) => b1.Rcp(b2)
        ).GenerateCode();

        LibBilinearBasisToBasisMappingCodeComposer.Create(
            Specs,
            codeFilesComposer,
            "fdp",
            false,
            (b1, b2) => b1.Fdp(b2)
        ).GenerateCode();

        LibBilinearBasisToBasisMappingCodeComposer.Create(
            Specs,
            codeFilesComposer,
            "cp",
            false,
            (b1, b2) => b1.Cp(b2)
        ).GenerateCode();

        LibBilinearBasisToBasisMappingCodeComposer.Create(
            Specs,
            codeFilesComposer,
            "acp",
            false,
            (b1, b2) => b1.Acp(b2)
        ).GenerateCode();

        LibBilinearBasisToBasisMappingCodeComposer.Create(
            Specs,
            codeFilesComposer,
            "gp",
            false,
            (b1, b2) => b1.Gp(b2)
        ).GenerateCode();

        codeFilesComposer.UpFolder();

        codeFilesComposer.UpFolder();

        codeFilesComposer.UpFolder();

        return codeFilesComposer;
    }
}