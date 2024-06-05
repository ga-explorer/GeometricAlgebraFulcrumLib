using GeometricAlgebraFulcrumLib.Utilities.Text.Files;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.GradedMultivectorsLib;

public class LibMainCodeComposer
{
    public LibCodeComposerSpecs Specs { get; }
        

    internal LibMainCodeComposer(LibCodeComposerSpecs specs)
    {
        Specs = specs;
    }

        
    public TextFilesComposer GenerateCode()
    {
        var codeComposer = new TextFilesComposer();

        codeComposer.DownFolder(Specs.GaSpaceName);

        for (var grade = 0; grade <= Specs.VSpaceDimensions; grade++)
            LibKVectorCodeComposer
                .Create(Specs, codeComposer, grade)
                .GenerateCode();

        LibMultivectorCodeComposer
            .Create(Specs, codeComposer)
            .GenerateCode();
            
        LibNormCodeComposer
            .Create(Specs, codeComposer)
            .GenerateCode();

        LibProjectionCodeComposer
            .Create(Specs, codeComposer)
            .GenerateCode();
            
        LibReflectionCodeComposer
            .Create(Specs, codeComposer)
            .GenerateCode();
            
        LibRotorMapCodeComposer
            .Create(Specs, codeComposer)
            .GenerateCode();
            
        LibVersorMapCodeComposer
            .Create(Specs, codeComposer)
            .GenerateCode();
            
        LibLaTeXCodeComposer
            .Create(Specs, codeComposer)
            .GenerateCode();

        LibProductCodeComposer.Create(
            Specs,
            codeComposer,
            "ScalarProduct",
            "Sp",
            (b1, b2) => b1.Sp(b2)
        ).GenerateCode();
            
        LibProductCodeComposer.Create(
            Specs,
            codeComposer,
            "OuterProduct",
            "Op",
            (b1, b2) => b1.Op(b2)
        ).GenerateCode();
            
        LibProductCodeComposer.Create(
            Specs,
            codeComposer,
            "LeftContractionProduct",
            "Lcp",
            (b1, b2) => b1.Lcp(b2)
        ).GenerateCode();
            
        LibProductCodeComposer.Create(
            Specs,
            codeComposer,
            "RightContractionProduct",
            "Rcp",
            (b1, b2) => b1.Rcp(b2)
        ).GenerateCode();
            
        LibProductCodeComposer.Create(
            Specs,
            codeComposer,
            "FatDotProduct",
            "Fdp",
            (b1, b2) => b1.Fdp(b2)
        ).GenerateCode();
            
        LibProductCodeComposer.Create(
            Specs,
            codeComposer,
            "CommutatorProduct",
            "Cp",
            (b1, b2) => b1.Cp(b2)
        ).GenerateCode();
            
        LibProductCodeComposer.Create(
            Specs,
            codeComposer,
            "AntiCommutatorProduct",
            "Acp",
            (b1, b2) => b1.Acp(b2)
        ).GenerateCode();

        LibProductCodeComposer.Create(
            Specs,
            codeComposer,
            "GeometricProduct",
            "Gp",
            (b1, b2) => b1.Gp(b2)
        ).GenerateCode();
            
        codeComposer.UpFolder();
            
        return codeComposer;
    }
}