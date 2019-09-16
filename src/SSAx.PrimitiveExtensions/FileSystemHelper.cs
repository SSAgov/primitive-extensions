using System.IO;

namespace SSAx.PrimitiveExtensions
{
    /// <summary>
    /// Provides static methods that aid in performing file system related operations.
    /// </summary>
    public static class FileSystemHelper
    {
        
        /// <summary>
        /// Adding .txt to the filename
        /// </summary>
        /// <param name="textToWrite"></param>
        /// <param name="folderPath"></param>
        /// <param name="fileName"></param>
        public static void WriteToTextFile(this string textToWrite, string folderPath, string fileName)
        {
            CreateFolderPathIfNotExist(folderPath);
            if (!fileName.EndsWith(".txt"))
            {
                fileName += ".txt";
            }
            textToWrite.WriteToFile(folderPath, fileName);
        }


        /// <summary>
        /// WriteToFile
        /// </summary>
        /// <param name="textToWrite"></param>
        /// <param name="folderPath"></param>
        /// <param name="fileName"></param>
        public static void WriteToFile(this string textToWrite, string folderPath, string fileName)
        {
            CreateFolderPathIfNotExist(folderPath);
            if (!folderPath.EndsWith(@"\"))
            {
                folderPath += @"\";
            }
            StreamWriter w = new StreamWriter(folderPath + fileName);
            w.Write(textToWrite);
            w.Flush();
            w.Close();
        }


        public static bool CreateFolderPathIfNotExist(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            return Directory.Exists(folderPath);
        }

    }
}
