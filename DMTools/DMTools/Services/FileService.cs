using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.Services
{
    public static class FileService
    {
        public static bool SaveFile(string path, string content, bool append = false)
        {
            var info = new FileInfo(path);
            if (!info.Directory.Exists) if (!DirectoryService.CreateDirectory(info.Directory.FullName)) return false;
            using (StreamWriter sw = new StreamWriter(path, append))
            {
                try
                {
                    sw.Write(content);
                    sw.Flush();
                    sw.Close();
                }
                catch (Exception)
                {
                    sw.Close();
                    return false;
                }
            }
            return true;
        }

        public static bool LoadFile(string path, out string content)
        {
            content = "";
            var info = new FileInfo(path);
            if (!info.Exists) return false;
            using (StreamReader sr = new StreamReader(path))
            {
                try
                {
                    content = sr.ReadToEnd();
                    sr.Close();
                }
                catch (Exception)
                {
                    sr.Close();
                    return false;
                }
            }
            return true;
        }

        internal static void LoadFile(object systemLayersFile, out string contentFile)
        {
            throw new NotImplementedException();
        }

        public static string GetFullFilePath(string filePath)
        {
            if (filePath == "") return "";
            var fileInfo = new FileInfo(filePath);
            return fileInfo.FullName;
        }
    }

    public static class DirectoryService
    {
        public static bool CreateDirectory(string path)
        {
            var info = new DirectoryInfo(path);
            if (info.Exists) return true;
            if (info.Parent == null) return false;
            if (!info.Parent.Exists) CreateDirectory(info.Parent.FullName);
            try
            {
                info.Create();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal static List<string> GetFileNames(string dirPath, string ext = "")
        {
            var dirInfo = new DirectoryInfo(dirPath);
            var files = dirInfo.GetFiles();
            var result = new List<string>();
            foreach (var file in files)
            {
                if (ext != "" && file.Extension != ext) continue;
                result.Add(file.Name.Replace(ext, ""));
            }
            return result;
        }
    }
}
