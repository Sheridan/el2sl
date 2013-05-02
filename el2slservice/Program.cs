
using System;
using System.ServiceProcess;

namespace Bng.EL2SL.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //System.Console.WriteLine(args.Length);
            if (args.Length == 0)
            {
                ServiceBase serviceToRun = new Service();
                ServiceBase.Run(serviceToRun);
            }
            switch (args[0].Trim())
            {
                case "-c":
                {
                    System.Console.WriteLine("starting in console mode");
                    SendersManager sm = new SendersManager();
                    sm.Start();
                    /*for (int i = 0; i < 10000; i++ )
                        System.Console.WriteLine(i);
                    sm.Stop();*/
                    System.Windows.Forms.Application.Run();
                }
                break;
                case "-h":
                default:
                {
                    System.Console.WriteLine("-h This help");
                    System.Console.WriteLine("-c Run in console");
                }
                break;
            }
        }
    }
}