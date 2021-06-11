using TextComposerLib.Files;

namespace TextComposerLib.Samples.Samples
{
    public static class FileComposerSamples
    {
        internal static string Task1()
        {
            var composer = new TextFilesComposer();

            composer
                .SelectFolder("Folder1")
                .SelectFile("File1-1.txt")
                .ActiveFileTextComposer
                .IncreaseIndentation()
                .AppendLine("Text Inside File1-1.txt");

            composer
                .SelectFile("File1-2.txt")
                .ActiveFileTextComposer
                .IncreaseIndentation()
                .AppendLine("Text Inside File1-2.txt")
                .AppendLine();

            composer
                .SelectFolder("Folder2")
                .SelectFile("File2-1.txt")
                .ActiveFileTextComposer
                .IncreaseIndentation()
                .AppendLine("Text Inside File2-1.txt")
                .AppendLine();

            composer
                .SelectFolder("Folder1")
                .SelectFile("File1-1.txt")
                .ActiveFileTextComposer
                .AppendLine("More Text Inside File1-1.txt")
                .AppendLine();

            return composer.ToString();
        }

    }
}
