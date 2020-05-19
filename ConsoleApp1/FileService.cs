using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.IO;
using ConsoleApp1.MyExceptions;
using ConsoleApp1.Components;


namespace ConsoleApp1
{
    public class FileService
    {
        List<Row> Data = new List<Row>();
        System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");

        public List<Row> ReturnData(string path)
        {
            Data.Clear();
            BackDir();
            DirectoryInfo dir = new DirectoryInfo(path);
            if (dir.GetDirectories().Count() > 0)
                DirectoryLoad(dir);
            if (dir.GetFiles().Count() > 0)
                FileLoad(dir);
            return Data;
        }

        public void BackDir()
        {
            Row row = new Row();
            row.Add(new Cell() { Value = ".." });
            row.Add(new Cell() { Value = "UP--DIR" });
            row.Add(new Cell() { Value = DateTime.Now.ToString("MMMM dd HH:mm", ci) });
            row.FileType = (int)FileType.BackDir;
            row.FileInterface = "/";

            Data.Add(row);
        }

        private void DirectoryLoad(DirectoryInfo dir)
        {
            foreach (DirectoryInfo info in dir.GetDirectories())
            {
                Row row = new Row();
                row.Add(new Cell() { Value = info.Name });
                row.Add(new Cell() { Value = "DIR" });
                row.Add(new Cell() { Value = info.LastWriteTimeUtc.ToString("MMM dd HH:mm", ci) });
                row.FileType = (int)FileType.Directory;
                row.FileInterface = "/";

                Data.Add(row);
            }
        }

        private void FileLoad(DirectoryInfo dir)
        {
            foreach (FileInfo info in dir.GetFiles())
            {
                Row row = new Row();
                row.Add(new Cell() { Value = info.Name });
                row.Add(new Cell() { Value = (info.Length / (1024 * 1024)).ToString() });
                row.Add(new Cell() { Value = info.LastWriteTime.ToString("MMM dd HH:mm", ci) });
                row.FileType = (int)FileType.File;
                row.FileInterface = " ";

                Data.Add(row);
            }
        }

        public List<Row> ReturnDrives()
        {
            foreach (DriveInfo info in DriveInfo.GetDrives())
            {
                if (info.IsReady)
                {
                    Row row = new Row();
                    row.Add(new Cell() { Value = info.Name });
                    row.Add(new Cell() { Value = (info.TotalSize / (1024 * 1024)).ToString() });
                    row.Add(new Cell() { Value = "" });
                    row.FileType = (int)FileType.Drive;
                    row.FileInterface = " ";

                    Data.Add(row);
                }
            }

            return Data;
        }

        public void CreateDirectory(string path)
        {
            foreach (char item in Path.GetInvalidFileNameChars())
            {
                if (Path.GetDirectoryName(path).Contains(item))
                    throw new UnsupportedSymbolException();
            }
            Directory.CreateDirectory(path);
        }

        public void Delete(string path, bool deleteSub)
        {
            if (Directory.Exists(path))
            {
                if ((Directory.GetFiles(path).Length != 0 || Directory.GetDirectories(path).Length != 0) && !deleteSub)
                    throw new DeleteFullDirException();

                else
                    Directory.Delete(path, deleteSub);
            }

            else if (File.Exists(path))
                File.Delete(path);

            else
                throw new Exception("Deleting failed unknown file name");

        }

        public void CopyFile(string filePath, string destinationPath)
        {
             if (File.Exists(destinationPath) || Directory.Exists(destinationPath))
                 throw new FileExistsException();

             if (File.Exists(filePath))
                 File.Copy(filePath, destinationPath);

             else if (Directory.Exists(filePath))
                 CopyDir(filePath, destinationPath);

             else
                 throw new ArgumentException("Copying failed unknown file name");
        }
        
        private void CopyDir(string sourceDir, string destDir)
        {
            Console.WriteLine("Copying directory " + sourceDir);
            Directory.CreateDirectory(destDir);
            foreach (string path in Directory.GetFiles(sourceDir))
            {
                File.Copy(path, Path.Combine(destDir, Path.GetFileName(path)));
                Console.WriteLine("File " + path + " copied.");
            }
            foreach (string path in Directory.GetDirectories(sourceDir))
            {
            string[] segments = path.Split('\\');
        
                CopyDir(path, Path.Combine(destDir, segments[segments.Length - 1]));
            }
        }

        public void Move(string sourcePath, string destinationPath)
        {
            if (!Directory.Exists(sourcePath) && !File.Exists(sourcePath))
                throw new ArgumentException("Moving failed unknown file name");

            if (Directory.Exists(destinationPath) || File.Exists(destinationPath))
                throw new FileExistsException();

            if (Path.GetPathRoot(sourcePath) == Path.GetPathRoot(destinationPath))
            {
                if (Directory.Exists(sourcePath))
                    Directory.Move(sourcePath, destinationPath);

                else
                    File.Move(sourcePath, destinationPath);
            }

            else
            {
                this.CopyFile(sourcePath, destinationPath);
                this.Delete(sourcePath, true);
            }
        }
    }
}
