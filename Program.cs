using System.ComponentModel.Design;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Hw_24_09
{
     class Program
    {
        
        [DllImport("kernel32.dll")]
        public static extern bool Beep(int frequency, int duration);

        
        [DllImport("user32.dll")]
        public static extern bool MessageBeep(uint uType);

        // ДОП 4
        public static void Menu()
        {
            while (true)
            {
                Console.WriteLine("1 : Notepad");
                Console.WriteLine("2 : calculate");
                Console.WriteLine("3 : Paint");
                Console.WriteLine("0 : exit");


                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        StartApp("notepad.exe");
                        break;
                    case "2":
                        StartApp("calc.exe");
                        break;
                    case "3":
                        StartApp("mspaint.exe");
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор, попробуйте снова.");
                        break;
                }
            }
        }

        static void Main(string[] args)
        {

            Menu();

            //ДОП 2
            Beep(1000, 500);
            Thread.Sleep(1000);

            Beep(1500, 500);
            Thread.Sleep(1000);

            Beep(2000, 500);
            Thread.Sleep(1000);

            MessageBeep(0xFFFFFFFF);


            //Main
            string childProccesPath = "notepad.exe";
            Process childProcces = new Process();
            childProcces.StartInfo.FileName = childProccesPath;
            childProcces.StartInfo.UseShellExecute = false;
            childProcces.StartInfo.RedirectStandardOutput = true;
            childProcces.StartInfo.RedirectStandardError = true;
            childProcces.StartInfo.RedirectStandardError = false;
            childProcces.StartInfo.CreateNoWindow = true;
            childProcces.EnableRaisingEvents = true;
            childProcces.Exited += (sender, eventArgs) =>
            {
                int exitCode = childProcces.ExitCode;
                Console.WriteLine($"Дочерний процес завершен с исходным кодом: {exitCode}");
                childProcces.Dispose();
            };
            childProcces.Start();
            Console.WriteLine("приложение запущено");
            childProcces.WaitForExit();

            
        }
        static void StartApp(string value)
        {
            try
            {
                Process.Start(value);
                Console.WriteLine($"{value} Started");
            }
            catch(Exception ex)
            {
                throw new Exception($"{value} is {ex.Message}");
            }
        }
    }
}
