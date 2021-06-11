using System;
using System.IO;

namespace DataStructuresLib.Files
{
    public static class FilesUtils
    {
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
                if (stream != null)
                    stream.Close();
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
    }
}
