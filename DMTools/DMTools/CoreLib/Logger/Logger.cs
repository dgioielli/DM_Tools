using DMTools.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.CoreLib.Logger
{
    public class Logger
    {
        #region Variáveis e propriedades

        public const string EXT_logger = ".brnlog";

        private static Logger m_instance = new Logger();

        private List<ILog> m_logs = new List<ILog>();
        private string m_dirPath = "";
        private string m_logName = "";
        private string m_trackerId = "";

        #endregion

        #region Construtoras e instanciadores

        private Logger()
        { }

        public static Logger getInstance()
        { if (m_instance == null) m_instance = new Logger(); return m_instance; }

        #endregion

        #region Funções

        public void addLog(ILog newLog) => m_logs.Add(newLog);

        public void startNewLog()
        {
            DateTime start = DateTime.Now;
            m_logName = $"{Environment.UserName}_{start.Year}_{start.Month}_{start.Day}_{start.Hour}_{start.Minute}_{start.Second}";
            m_trackerId = ((new Random().Next(10000, 100000) % 9999) + 1).ToString("0000");
        }

        public void setDirPath(string dirPath) => m_dirPath = dirPath;

        public string getTracker()
        {
            if (m_trackerId == "") startNewLog();
            return m_trackerId;
        }

        public void saveLogs()
        {
            if (m_logName == "")
                return;
            var path = $"{m_dirPath}{m_logName}{EXT_logger}";
            var content = "";
            m_logs.ForEach(x => content = $"{content}{x.getStringLog()}\n");
            FileService.SaveFile(path, content);
            m_logs.Clear();
            joinLogs();
        }

        public void runAction(Action func, Action<string> msgErr, string commandName)
        {
            startNewLog();
            var start = DateTime.Now;
            CommandLog.newLogCommand($"Inicio comando - {commandName}");
            try
            {
                func();
            }
            catch (Exception ex)
            {
                ErrorLog.newLogError($"Erro : {ex.Message}");
                msgErr(m_trackerId);
            }
            InfoLog.newLogInfo($"Fim comando - {commandName} - {DateTime.Now.Subtract(start).TotalMinutes:n2} minutes");
            saveLogs();
        }

        public void runAction<T>(Action<T> func, Action<string> msgErr, string commandName, T param1)
        {
            startNewLog();
            var start = DateTime.Now;
            CommandLog.newLogCommand($"Inicio comando - {commandName}");
            try
            {
                func(param1);
            }
            catch (Exception ex)
            {
                ErrorLog.newLogError($"Erro : {ex.Message}");
                msgErr(m_trackerId);
            }
            InfoLog.newLogInfo($"Fim comando - {commandName} - {DateTime.Now.Subtract(start).TotalMinutes:n2} minutes");
            saveLogs();
        }

        public void runAction<T, U>(Action<T, U> func, Action<string> msgErr, string commandName, T param1, U param2)
        {
            startNewLog();
            var start = DateTime.Now;
            CommandLog.newLogCommand($"Inicio comando - {commandName}");
            try
            {
                func(param1, param2);
            }
            catch (Exception ex)
            {
                ErrorLog.newLogError($"Erro : {ex.Message}");
                msgErr(m_trackerId);
            }
            InfoLog.newLogInfo($"Fim comando - {commandName} - {DateTime.Now.Subtract(start).TotalMinutes:n2} minutes");
            saveLogs();
        }

        public void joinLogs()
        {
            try
            {
                var dir = new DirectoryInfo(m_dirPath);
                var files = new List<FileInfo>();
                var inicio = DateTime.Now;
                var pathBase = $"{Environment.UserName}_{inicio.Year}_{inicio.Month}_{inicio.Day}";
                foreach (var item in dir.GetFiles())
                {
                    if (item.Name.Contains(pathBase) && item.FullName.Contains(EXT_logger))
                        files.Add(item);
                }
                joinLogs(files, pathBase);
            }
            catch (Exception)
            {
                return;
            }
        }

        private void joinLogs(List<FileInfo> files, string pathBase)
        {
            var path = $"{m_dirPath}{pathBase}{EXT_logger}";
            if (!File.Exists(path))
                File.Create(pathBase).Close();
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                try
                {
                    var linhas = new List<string>();
                    for (int i = 0; i < files.Count; i++)
                    {
                        if (files[i].FullName == path.Replace("////", "//"))
                            continue;
                        using (StreamReader sr = new StreamReader(files[i].FullName))
                        {
                            try
                            {
                                var linha = sr.ReadLine();
                                while (linha != null && linha != "")
                                {
                                    linhas.Add(linha);
                                    linha = sr.ReadLine();
                                }
                                sr.Close();
                            }
                            catch (Exception)
                            {
                                sr.Close();
                            }
                        }
                        files[i].Delete();
                    }
                    linhas.ForEach(x => sw.WriteLine(x));
                    sw.Close();
                }
                catch (Exception)
                {
                    sw.Close();
                }
            }

        }

        #endregion
    }
}
