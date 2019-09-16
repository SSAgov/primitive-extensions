using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSAx.PrimitiveExtensions;
using Xunit;
using System.IO;

namespace SSAx.PrimitiveExtensions.Tests
{
    public class FileSystemTests
    {

        [Fact]
        public void CreateFolderPathIfNotExist_GivenFolderName_ExpectTrue()
        {
            string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string folderPath = Path.Combine(userFolder, "MyTestFolder");
            bool r = FileSystemHelper.CreateFolderPathIfNotExist(folderPath);
            Assert.True(r);
        }

        [Fact]
        public void WriteToFile_GivenTexttowrite_foldername_filename_ExpectTrue()
        {
            string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string folderPath = Path.Combine(userFolder, "MyTestFolder");
            string fileName = "mytestfile.txt";
            string filepath = Path.Combine(folderPath, fileName);
            FileSystemHelper.WriteToFile("This is test", folderPath, fileName);
            string r = File.ReadAllText(filepath, Encoding.UTF8);
            Assert.Equal("This is test", r);

        }

        [Fact]
        public void CreateFolderPathIfNotExist_GivenFoldername_ExpectTrue()
        {
            string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string folderPath = Path.Combine(userFolder, "MyTestFolder");
            FileSystemHelper.CreateFolderPathIfNotExist(folderPath);
            bool s= Directory.Exists(folderPath);
            Assert.True(s);
        }

    }
}
