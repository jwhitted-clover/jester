using System;
using System.Threading.Tasks;
using com.clover.remotepay.sdk;
using Grpc.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WindowsJester
{
    public static class Program
    {
        private readonly static object Lock = new object();
        public static ICloverConnector Connector { get; set; }
        public static Listener Listener { get; set; }
        public static TaskCompletionSource<bool> Running { get; } = new TaskCompletionSource<bool>();
        private static TaskCompletionSource<bool> Abort { get; } = new TaskCompletionSource<bool>();

        public static void Main(string[] args)
        {
            WriteLine(@"
     ____.                __                
    |    | ____   _______/  |_  ___________ 
    |    |/ __ \ /  ___/\   __\/ __ \_  __ \
/\__|    \  ___/ \___ \  |  | \  ___/|  | \/
\________|\___  >____  > |__|  \___  >__|   
 for Windows! \/     \/            \/       
".TrimStart('\r', '\n'), ConsoleColor.Cyan);
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings { Converters = { new StringEnumConverter() } };

            Console.CancelKeyPress += (s, e) =>
            {
                e.Cancel = true;
                Abort.TrySetResult(true);
            };

            const string GRPC_HOST = "localhost";
            const int GRPC_PORT = 12346;
            var sdkDriver = new SdkDriverImpl();
            var grpc = new Server
            {
                Services = { Jester.SdkDriver.BindService(sdkDriver) },
                Ports = { new ServerPort(GRPC_HOST, GRPC_PORT, ServerCredentials.Insecure) }
            };

            try
            {
                grpc.Start();
                WriteLine($"gRPC Server running on {GRPC_HOST}:{GRPC_PORT}", ConsoleColor.Green);
                Task.WaitAny(Running.Task, Abort.Task);
            }
            catch (Exception ex)
            {
                WriteLine("ERROR!", ex, ConsoleColor.Red);
            }
            finally
            {
                WriteLine("gRPC Server shutting down");
                grpc.KillAsync().Wait();
            }
        }

        public static void WriteLine(string message, ConsoleColor foreColor = ConsoleColor.Gray, ConsoleColor backColor = ConsoleColor.Black)
        {
            lock (Lock)
            {
                Console.ForegroundColor = foreColor;
                Console.BackgroundColor = backColor;
                Console.WriteLine(message);
                Console.ResetColor();
            }
        }

        public static void WriteLine(string message, object content, ConsoleColor foreColor = ConsoleColor.Gray, ConsoleColor backColor = ConsoleColor.Black)
        {
            lock (Lock)
            {
                Console.ForegroundColor = foreColor;
                Console.BackgroundColor = backColor;
                Console.Write(message);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine($" {JsonConvert.SerializeObject(content)}");
                Console.ResetColor();
            }
        }
    }
}
