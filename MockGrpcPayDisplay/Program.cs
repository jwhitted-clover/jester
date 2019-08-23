using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clover.Grpc;
using Grpc.Core;
using Newtonsoft.Json;

namespace MockGrpcPayDisplay
{
    public static class Program
    {
        private static readonly object Lock = new object();
        private static TaskCompletionSource<bool> Abort { get; } = new TaskCompletionSource<bool>();

        public static void Main(string[] args)
        {
            WriteHeader();

            Console.CancelKeyPress += Console_CancelKeyPress;

            const string GRPC_HOST = "localhost";
            const int GRPC_PORT = 12346;
            var payDisplay = new PayDisplayImpl();
            var grpc = new Server
            {
                Services = { PayDisplay.BindService(payDisplay) },
                Ports = { new ServerPort(GRPC_HOST, GRPC_PORT, ServerCredentials.Insecure) }
            };

            try
            {
                grpc.Start();
                WriteLine($"gRPC Server running on {GRPC_HOST}:{GRPC_PORT}", ConsoleColor.Green);
                Task.WaitAny(Abort.Task);
            }
            catch(Exception ex)
            {
                WriteLine("ERROR!", ex, ConsoleColor.Red);
            }
            finally
            {
                WriteLine("gRPC Server shutting down");
                grpc.KillAsync().Wait();
            }
        }

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
            Abort.TrySetResult(true);
        }

        private static void WriteHeader()
        {
            WriteLine(@"
  _____            _____  _           _             
 |  __ \  M O C K |  __ \(_) g R P C | |            
 | |__) |_ _ _   _| |  | |_ ___ _ __ | | __ _ _   _ 
 |  ___/ _` | | | | |  | | / __| '_ \| |/ _` | | | |
 | |  | (_| | |_| | |__| | \__ \ |_) | | (_| | |_| |
 |_|   \__,_|\__, |_____/|_|___/ .__/|_|\__,_|\__, |
              __/ |            | |             __/ |
             |___/             |_|            |___/ 
".TrimStart('\r', '\n'), ConsoleColor.Cyan, ConsoleColor.Black);

            var left = Console.CursorLeft;
            var top = Console.CursorTop;

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkCyan;

            Console.SetCursorPosition(9, 1);
            Console.Write(" M O C K ");

            Console.SetCursorPosition(28, 1);
            Console.Write(" g R P C ");

            Console.ResetColor();
            Console.SetCursorPosition(left, top);
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
