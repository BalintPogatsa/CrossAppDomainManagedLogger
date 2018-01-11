using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ManagedTools
{
    public class FileLogger
    {
        public static void WriteLine(string message)
        {
            Console.WriteLine(message);
            WriteLog(message);
        }

        public static void WriteLine(string format, params object[] args)
        {
            Console.WriteLine(format, args);
            WriteLog(string.Format(format, args));
        }

        public static void WriteLog(string message)
        {
            using (var writer = File.AppendText("log.txt"))
            {
                writer.WriteLine("{0}: {1}", DateTime.Now.ToLongTimeString(), message);
            }
        }
    }
}
