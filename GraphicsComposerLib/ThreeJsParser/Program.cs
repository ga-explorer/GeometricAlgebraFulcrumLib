using System;
using System.Collections.Generic;
using System.IO;
using Esprima;
using Esprima.Utils;
using Newtonsoft.Json;

namespace ThreeJsParser
{
    public class CSharpCodeComposer
    {
        public 
    }

    class Program
    {
        static void Main(string[] args)
        {
            var parserOptions = new ParserOptions()
            {
                Comment = true,
                Tokens = true, 
                Tolerant = true,
                ErrorHandler = new CollectingErrorHandler()
            };

            const string filesPath = @"D:\Projects\Study\Three.js\three.js-dev\build";
            var sourceFile = Path.Combine(filesPath, "three.js");
            var jsonFile = sourceFile + ".json";

            var code = File.ReadAllText(sourceFile);
            //var scanner = new Scanner(code, parserOptions);

            //var tokens = new List<Token>();
            //Token token;

            //do
            //{
            //    scanner.ScanComments();
            //    token = scanner.Lex();
            //    tokens.Add(token);
            //} while (token.Type != TokenType.EOF);

            //File.WriteAllText(
            //    jsonFile, 
            //    JsonConvert.SerializeObject(tokens, Formatting.Indented)
            //);

            var parser = new JavaScriptParser(code, parserOptions);
            var program = parser.ParseScript();

            using (var textWriter = File.CreateText(jsonFile))
            {
                program.WriteJson(textWriter, "   ");
            }

            Console.WriteLine();
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
