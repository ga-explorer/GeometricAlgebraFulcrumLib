using System;
using System.IO;
using DataStructuresLib.Basic;

namespace DataStructuresLib.Files
{
    public static class FilesUtils
    {
        public static Triplet<string> GetFilePathParts(this string filePath)
        {
            var folderPath = Path.GetDirectoryName(filePath) ?? string.Empty;
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            var fileExtension = Path.GetExtension(filePath);

            return new Triplet<string>(
                folderPath,
                fileName,
                fileExtension
            );
        }
        
        public static string GetFilePath(this string folderPath, string fileName)
        {
            return Path.Combine(folderPath, fileName);
        }

        public static string GetFilePath(this string folderPath, string fileName, string fileExtension)
        {
            return $"{Path.Combine(folderPath, fileName)}.{fileExtension}";
        }

        public static string GetTempFilePath(this string folderPath, string fileExtension)
        {
            var fileName = Path.GetTempFileName();

            return $"{Path.Combine(folderPath, fileName)}.{fileExtension}";
        }
        
        public static string GetTempFilePath(this string fileExtension)
        {
            var folderPath = Path.GetTempPath();
            var fileName = Path.GetTempFileName();

            return $"{Path.Combine(folderPath, fileName)}.{fileExtension}";
        }


        public static bool IsFileLocked(this string filePath)
        {
            return new FileInfo(filePath).IsFileLocked();
        }

        /// <summary>
        /// https://stackoverflow.com/questions/876473/is-there-a-way-to-check-if-a-file-is-in-use
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool IsFileLocked(this FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                stream?.Close();
            }

            //file is not locked
            return false;
        }

        public static bool WaitForFileUnlock(this string filePath, double timeoutInMilliseconds)
        {
            var file = new FileInfo(filePath);

            var time = DateTime.Now;
            while (IsFileLocked(file))
            {
                if ((DateTime.Now - time).TotalMilliseconds > timeoutInMilliseconds)
                    return false;
            }

            return true;
        }

        public static bool TryDeleteFile(this string filePath, double timeoutInMilliseconds = 100d)
        {
            try
            {
                if (!File.Exists(filePath))
                    return false;

                if (filePath.IsFileLocked())
                    filePath.WaitForFileUnlock(timeoutInMilliseconds);

                File.Delete(filePath);

                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }

        /// <summary>
        /// Read a byte array from a given binary stream
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="rewindStream"></param>
        /// <returns></returns>
        public static byte[] GetByteArray(this Stream stream, bool rewindStream = false)
        {
            using var ms = new MemoryStream();

            var streamPosition = stream.Position;
            if (rewindStream && streamPosition > 0)
            {
                stream.Position = 0;

                stream.CopyTo(ms);

                stream.Position = streamPosition;
            }
            else
                stream.CopyTo(ms);

            return ms.ToArray();
        }

    }
}
