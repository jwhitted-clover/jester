using System;
using System.Linq;
using System.Threading.Tasks;
using com.clover.remotepay.sdk;
using Newtonsoft.Json;

namespace WindowsUsbPayDisplayClient
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Run().Wait();
        }

        private static async Task Run()
        {
            var connector = CloverConnectorFactory.CreateUsbConnector("RAID", "POS", "Register1", false);
            var listener = new Listener(connector);
            connector.AddCloverConnectorListener(listener);

            listener.DeviceReadyPromise = new TaskCompletionSource<MerchantInfo>();
            connector.InitializeConnection();
            await listener.DeviceReadyPromise.Task;

            listener.ResetDeviceResponsePromise = new TaskCompletionSource<ResetDeviceResponse>();
            connector.ResetDevice();
            await listener.ResetDeviceResponsePromise.Task;

            listener.SaleResponsePromise = new TaskCompletionSource<SaleResponse>();
            connector.Sale(new SaleRequest
            {
                ExternalId = ExternalIDUtil.GenerateRandomString(32),
                Amount = 123
            });
            var sale = await listener.SaleResponsePromise.Task;

            await Task.Delay(100);

            connector.Dispose();
        }
    }

    public class Listener : DefaultCloverConnectorListener
    {
        public ICloverConnector Connector { get; set; }
        public TaskCompletionSource<MerchantInfo> DeviceReadyPromise { get; set; }
        public TaskCompletionSource<SaleResponse> SaleResponsePromise { get; set; }
        public TaskCompletionSource<ResetDeviceResponse> ResetDeviceResponsePromise { get; set; }

        public Listener(ICloverConnector connector) : base(connector)
        {
            Connector = connector;
        }

        public override void OnDeviceReady(MerchantInfo merchantInfo)
        {
            DeviceReadyPromise?.TrySetResult(merchantInfo);
        }

        public override void OnResetDeviceResponse(ResetDeviceResponse response)
        {
            ResetDeviceResponsePromise?.TrySetResult(response);
        }

        public override void OnConfirmPaymentRequest(ConfirmPaymentRequest request)
        {
            Connector.AcceptPayment(request.Payment);
        }

        public override void OnVerifySignatureRequest(VerifySignatureRequest request)
        {
            Connector.AcceptSignature(request);
        }

        public override void OnSaleResponse(SaleResponse response)
        {
            SaleResponsePromise?.TrySetResult(response);
        }

        public override void OnDeviceActivityStart(CloverDeviceEvent deviceEvent)
        {
            if(deviceEvent.EventState == CloverDeviceEvent.DeviceEventState.RECEIPT_OPTIONS)
            {
                Connector.InvokeInputOption(deviceEvent.InputOptions.First());
            }
        }
    }
}
