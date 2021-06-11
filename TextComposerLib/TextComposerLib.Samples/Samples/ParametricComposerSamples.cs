using System;
using TextComposerLib.Text.Parametric;
using TextComposerLib.Text.Structured;

namespace TextComposerLib.Samples.Samples
{
    public static class ParametricComposerSamples
    {
        internal static string Task1()
        {
            var composer = new ParametricTextComposer("<", ">")
            {
                AlignMultiLineParameterValues = true
            };

            composer.SetTemplateText(
@"public sealed class <class_name> : <base_class_name>
{
    public <class_name>()
    {
        <constructor_code>
    }

    <class_code>
}"
            );

            composer["class_name"] = "MyClass";
            composer["base_class_name"] = "BaseClass";
            composer["constructor_code"] =
@"//You can add more constructor code here
//Note the correct handling of indentation
//for this multi-line parameter value when
//using AlignMultiLineParameterValues = true";
            composer["class_code"] = @"//You can add more class code here";

            return composer.GenerateText();


            ////Code Piece 1
            //composer["class_name"] = "MyClass";
            //composer["base_class_name"] = "BaseClass";
            //composer["constructor_code"] = @"//You can add more constructor code here";
            //composer["class_code"] = @"//You can add more class code here";

            //return composer.GenerateText();


            ////Code Piece 2
            //composer.SetParametersValues(
            //    "class_name", "MyClass",
            //    "base_class_name", "BaseClass",
            //    "constructor_code", @"//You can add more constructor code here",
            //    "class_code", @"//You can add more class code here"
            //    );

            //return composer.GenerateText();


            ////Code Piece 3
            //return  
            //    composer.GenerateText(
            //        "class_name", "MyClass",
            //        "base_class_name", "BaseClass",
            //        "constructor_code", @"//You can add more constructor code here",
            //        "class_code", @"//You can add more class code here"
            //        );


            ////Code Piece 4
            //return 
            //    composer.GenerateUsing(
            //        "MyClass",
            //        "BaseClass",
            //        @"//You can add more constructor code here",
            //        @"//You can add more class code here"
            //        );
        }

        internal static string Task2()
        {
            var composerCollection = new ParametricTextComposerCollection();

            composerCollection.Parse(
@"
delimiters # #

//This template can be used to declare properties inside C# classes
begin declare_property
public #type# #name# { get; private set; }

end declare_property

//This template can be used to declare C# classes
begin declare_class
public class #name#
{
    #properties#

    public #name#(#params#)
    {
        #code#
    }
}
end declare_class
"
);

            var declarePropertyComposer = composerCollection["declare_property"];

            var propertiesComposer = new ListTextComposer
            {
                declarePropertyComposer.GenerateUsing("double", "X"),
                declarePropertyComposer.GenerateUsing("double", "Y"),
                declarePropertyComposer.GenerateUsing("double", "Z")
            };

            var paramsComposer = new ListTextComposer(", ")
            {
                ActiveItemPrefix = "double "
            };

            paramsComposer.AddRange("x", "y", "z");

            var codeComposer = new ListTextComposer(Environment.NewLine)
            {
                ActiveItemSuffix = ";"
            };

            codeComposer.AddRange("X = x", "Y = y", "Z = z");

            return
                composerCollection["declare_class"].GenerateText(
                    "name", "Point3D",
                    "properties", propertiesComposer,
                    "params", paramsComposer,
                    "code", codeComposer
                    );
        }

    }
}
