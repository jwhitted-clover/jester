using System;
using System.Linq;
using System.Threading.Tasks;
using Clover.Grpc;
using Grpc.Core;

namespace WindowsGrpcPayDisplayClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Run().Wait();
        }

        public static async Task Run()
        {
            var channel = new Channel("127.0.0.1:12346", ChannelCredentials.Insecure);
            var client = new PayDisplay.PayDisplayClient(channel);

            var readyStream = client.OnDeviceReady(new Empty()).ResponseStream;
            var resetStream = client.OnResetDeviceResponse(new Empty()).ResponseStream;
            var confirmStream = client.OnConfirmPaymentRequest(new Empty()).ResponseStream;
            var saleStream = client.OnSaleResponse(new Empty()).ResponseStream;
            var verifyStream = client.OnVerifySignatureRequest(new Empty()).ResponseStream;
            var activityStream = client.OnDeviceActivityStart(new Empty()).ResponseStream;

            var confirmTask = Task.Run(async () =>
             {
                 while (await confirmStream.MoveNext())
                 {
                     client.AcceptPayment(new AcceptPaymentRequest { Payment = confirmStream.Current.Payment });
                 }
             });

            var verifyTask = Task.Run(async () =>
            {
                while (await verifyStream.MoveNext())
                {
                    client.AcceptSignature(new AcceptSignatureRequest { Payment = verifyStream.Current.Payment });
                }
            });

            var receiptTask = Task.Run(async () =>
            {
                while(await activityStream.MoveNext())
                {
                    if (activityStream.Current.EventState == DeviceEventState.ReceiptOptions)
                    {
                        client.InvokeInputOption(new InvokeInputOptionRequest { InputOption = activityStream.Current.InputOptions.First() });
                    }
                }
            });

            client.Create(new CreateRequest());
            await readyStream.MoveNext();

            client.ResetDevice(new ResetDeviceRequest());
            await resetStream.MoveNext();

            client.Sale(new SaleRequest
            {
            
                ExternalId = ExternalId(),
                Amount = 123,
            });
            var sale = await saleStream.MoveNext();

            client.Dispose(new DisposeRequest());
        }

        private static Random random = new Random();
        private static string charset = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        private static string ExternalId() => string.Join("", Enumerable.Range(1, 32).Select(n => charset[random.Next() % charset.Length]));
    }
}
