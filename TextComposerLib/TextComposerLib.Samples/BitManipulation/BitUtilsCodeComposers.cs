using DataStructuresLib;
using DataStructuresLib.BitManipulation;
using TextComposerLib.Text.Linear;

namespace TextComposerLib.Samples.BitManipulation
{
    public static class BitUtilsCodeComposers
    {
        
        private static void GenerateUInt64FirstOneBitPositionCases(LinearTextComposer composer, int pos1, int pos2)
        {
            if (pos2 == pos1 + 1)
            {
                composer.AppendAtNewLine(
                    $"return (bitPattern & {1UL << pos1}UL) != 0 ? {pos1} : {pos2};"
                );
            }
            else
            {
                var posMid = (pos1 + pos2 - 1) / 2;
                var mask = UInt64BitUtils.CreateMask(pos1, posMid);

                composer.AppendAtNewLine($"if ((bitPattern & {mask}UL) != 0)");
                composer.AppendAtNewLine("{");
                composer.IncreaseIndentation();
                composer.AppendAtNewLine($"// Found ones in positions {pos1}-{posMid}");
                GenerateUInt64FirstOneBitPositionCases(composer, pos1, posMid);
                composer.DecreaseIndentation();
                composer.AppendAtNewLine("}");
                composer.AppendLineAtNewLine();
                composer.AppendAtNewLine($"// Found ones in positions {posMid + 1}-{pos2}");
                GenerateUInt64FirstOneBitPositionCases(composer, posMid + 1, pos2);
            }
        }

        private static void GenerateUInt64LastOneBitPositionCases(LinearTextComposer composer, int pos1, int pos2)
        {
            if (pos2 == pos1 + 1)
            {
                composer.AppendAtNewLine(
                    $"return (bitPattern & {1UL << pos2}UL) != 0 ? {pos2} : {pos1};"
                );
            }
            else
            {
                var posMid = (pos1 + pos2 - 1) / 2;
                var mask = UInt64BitUtils.CreateMask(posMid + 1, pos2);

                composer.AppendAtNewLine($"if ((bitPattern & {mask}UL) != 0)");
                composer.AppendAtNewLine("{");
                composer.IncreaseIndentation();
                composer.AppendAtNewLine($"// Found ones in positions {posMid + 1}-{pos2}");
                GenerateUInt64LastOneBitPositionCases(composer, posMid + 1, pos2);
                composer.DecreaseIndentation();
                composer.AppendAtNewLine("}");
                composer.AppendLineAtNewLine();
                composer.AppendAtNewLine($"// Found ones in positions {pos1}-{posMid}");
                GenerateUInt64LastOneBitPositionCases(composer, pos1, posMid);
            }
        }

        private static void GenerateUInt32FirstOneBitPositionCases(LinearTextComposer composer, int pos1, int pos2)
        {
            if (pos2 == pos1 + 1)
            {
                composer.AppendAtNewLine(
                    $"return (bitPattern & {1U << pos1}U) != 0 ? {pos1} : {pos2};"
                );
            }
            else
            {
                var posMid = (pos1 + pos2 - 1) / 2;
                var mask = Int32BitUtils.CreateMask(pos1, posMid);

                composer.AppendAtNewLine($"if ((bitPattern & {mask}U) != 0)");
                composer.AppendAtNewLine("{");
                composer.IncreaseIndentation();
                composer.AppendAtNewLine($"// Found ones in positions {pos1}-{posMid}");
                GenerateUInt32FirstOneBitPositionCases(composer, pos1, posMid);
                composer.DecreaseIndentation();
                composer.AppendAtNewLine("}");
                composer.AppendLineAtNewLine();
                composer.AppendAtNewLine($"// Found ones in positions {posMid + 1}-{pos2}");
                GenerateUInt32FirstOneBitPositionCases(composer, posMid + 1, pos2);
            }
        }

        private static void GenerateUInt32LastOneBitPositionCases(LinearTextComposer composer, int pos1, int pos2)
        {
            if (pos2 == pos1 + 1)
            {
                composer.AppendAtNewLine(
                    $"return (bitPattern & {1UL << pos2}U) != 0 ? {pos2} : {pos1};"
                );
            }
            else
            {
                var posMid = (pos1 + pos2 - 1) / 2;
                var mask = Int32BitUtils.CreateMask(posMid + 1, pos2);

                composer.AppendAtNewLine($"if ((bitPattern & {mask}U) != 0)");
                composer.AppendAtNewLine("{");
                composer.IncreaseIndentation();
                composer.AppendAtNewLine($"// Found ones in positions {posMid + 1}-{pos2}");
                GenerateUInt32LastOneBitPositionCases(composer, posMid + 1, pos2);
                composer.DecreaseIndentation();
                composer.AppendAtNewLine("}");
                composer.AppendLineAtNewLine();
                composer.AppendAtNewLine($"// Found ones in positions {pos1}-{posMid}");
                GenerateUInt32LastOneBitPositionCases(composer, pos1, posMid);
            }
        }


        public static string GenerateUInt64FirstOneBitPosition()
        {
            var composer = new LinearTextComposer();

            composer.AppendAtNewLine("if (bitPattern == 0UL) return -1;");
            composer.AppendLineAtNewLine();

            GenerateUInt64FirstOneBitPositionCases(composer, 0, 63);

            return composer.ToString();
        }

        public static string GenerateUInt32FirstOneBitPosition()
        {
            var composer = new LinearTextComposer();

            composer.AppendAtNewLine("if (bitPattern == 0U) return -1;");
            composer.AppendLineAtNewLine();

            GenerateUInt32FirstOneBitPositionCases(composer, 0, 31);

            return composer.ToString();
        }

        public static string GenerateUInt64LastOneBitPosition()
        {
            var composer = new LinearTextComposer();

            composer.AppendAtNewLine("if (bitPattern == 0UL) return -1;");
            composer.AppendLineAtNewLine();

            GenerateUInt64LastOneBitPositionCases(composer, 0, 63);

            return composer.ToString();
        }

        public static string GenerateUInt32LastOneBitPosition()
        {
            var composer = new LinearTextComposer();

            composer.AppendAtNewLine("if (bitPattern == 0U) return -1;");
            composer.AppendLineAtNewLine();

            GenerateUInt32LastOneBitPositionCases(composer, 0, 31);

            return composer.ToString();
        }
    }
}
