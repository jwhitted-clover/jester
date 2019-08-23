using System.Threading.Tasks;
using Clover.Grpc;
using sdk = com.clover.remotepay.sdk;
using transport = com.clover.remotepay.transport;
using Grpc.Core;
using System;

namespace MockGrpcPayDisplay
{
    public class PayDisplayImpl : PayDisplay.PayDisplayBase
    {
        private sdk.ICloverConnector connector;
        private Listener listener;
        private StreamHandler<sdk.AuthResponse, AuthResponse> AuthResponseHandler { get; set; }
        private StreamHandler<sdk.CapturePreAuthResponse, CapturePreAuthResponse> CapturePreAuthResponseHandler { get; set; }
        private StreamHandler<sdk.CloseoutResponse, CloseoutResponse> CloseoutResponseHandler { get; set; }
        private StreamHandler<sdk.ConfirmPaymentRequest, ConfirmPaymentRequest> ConfirmPaymentRequestHandler { get; set; }
        private StreamHandler<sdk.CustomActivityResponse, CustomActivityResponse> CustomActivityResponseHandler { get; set; }
        private StreamHandler<sdk.CustomerProvidedDataEvent, CustomerProvidedDataEvent> CustomerProvidedDataHandler { get; set; }
        private StreamHandler<sdk.CloverDeviceEvent, DeviceEvent> DeviceActivityEndHandler { get; set; }
        private StreamHandler<sdk.CloverDeviceEvent, DeviceEvent> DeviceActivityStartHandler { get; set; }
        private StreamHandler<EventArgs, Empty> DeviceConnectedHandler { get; set; }
        private StreamHandler<EventArgs, Empty> DeviceDisconnectedHandler { get; set; }
        private StreamHandler<sdk.CloverDeviceErrorEvent, DeviceErrorEvent> DeviceErrorHandler { get; set; }
        private StreamHandler<sdk.MerchantInfo, MerchantInfo> DeviceReadyHandler { get; set; }
        private StreamHandler<sdk.DisplayReceiptOptionsResponse, DisplayReceiptOptionsResponse> DisplayReceiptOptionsResponseHandler { get; set; }
        private StreamHandler<sdk.InvalidStateTransitionNotification, InvalidStateTransitionNotification> InvalidStateTransitionResponseHandler { get; set; }
        private StreamHandler<sdk.ManualRefundResponse, ManualRefundResponse> ManualRefundResponseHandler { get; set; }
        private StreamHandler<sdk.MessageFromActivity, MessageFromActivity> MessageFromActivityHandler { get; set; }
        private StreamHandler<sdk.PreAuthResponse, PreAuthResponse> PreAuthResponseHandler { get; set; }
        private StreamHandler<sdk.PrintJobStatusResponse, PrintJobStatusResponse> PrintJobStatusResponseHandler { get; set; }
        private StreamHandler<sdk.PrintManualRefundDeclineReceiptMessage, PrintManualRefundDeclineReceiptMessage> PrintManualRefundDeclineReceiptHandler { get; set; }
        private StreamHandler<sdk.PrintManualRefundReceiptMessage, PrintManualRefundReceiptMessage> PrintManualRefundReceiptHandler { get; set; }
        private StreamHandler<sdk.PrintPaymentDeclineReceiptMessage, PrintPaymentDeclineReceiptMessage> PrintPaymentDeclineReceiptHandler { get; set; }
        private StreamHandler<sdk.PrintPaymentMerchantCopyReceiptMessage, PrintPaymentMerchantCopyReceiptMessage> PrintPaymentMerchantCopyReceiptHandler { get; set; }
        private StreamHandler<sdk.PrintPaymentReceiptMessage, PrintPaymentReceiptMessage> PrintPaymentReceiptHandler { get; set; }
        private StreamHandler<sdk.PrintRefundPaymentReceiptMessage, PrintRefundPaymentReceiptMessage> PrintRefundPaymentReceiptHandler { get; set; }
        private StreamHandler<sdk.ReadCardDataResponse, ReadCardDataResponse> ReadCardDataResponseHandler { get; set; }
        private StreamHandler<sdk.RefundPaymentResponse, RefundPaymentResponse> RefundPaymentResponseHandler { get; set; }
        private StreamHandler<sdk.ResetDeviceResponse, ResetDeviceResponse> ResetDeviceResponseHandler { get; set; }
        private StreamHandler<sdk.RetrieveDeviceStatusResponse, RetrieveDeviceStatusResponse> RetrieveDeviceStatusResponseHandler { get; set; }
        private StreamHandler<sdk.RetrievePaymentResponse, RetrievePaymentResponse> RetrievePaymentResponseHandler { get; set; }
        private StreamHandler<sdk.RetrievePendingPaymentsResponse, RetrievePendingPaymentsResponse> RetrievePendingPaymentsResponseHandler { get; set; }
        private StreamHandler<sdk.RetrievePrintersResponse, RetrievePrintersResponse> RetrievePrintersResponseHandler { get; set; }
        private StreamHandler<sdk.SaleResponse, SaleResponse> SaleResponseHandler { get; set; }
        private StreamHandler<transport.TipAddedMessage, TipAddedMessage> TipAddedHandler { get; set; }
        private StreamHandler<sdk.TipAdjustAuthResponse, TipAdjustAuthResponse> TipAdjustAuthResponseHandler { get; set; }
        private StreamHandler<sdk.VaultCardResponse, VaultCardResponse> VaultCardResponseHandler { get; set; }
        private StreamHandler<sdk.VerifySignatureRequest, VerifySignatureRequest> VerifySignatureRequestHandler { get; set; }
        private StreamHandler<sdk.VoidPaymentRefundResponse, VoidPaymentRefundResponse> VoidPaymentRefundResponseHandler { get; set; }
        private StreamHandler<sdk.VoidPaymentResponse, VoidPaymentResponse> VoidPaymentResponseHandler { get; set; }


        #region Constructor
        public PayDisplayImpl()
        {
            listener = new Listener();
            listener.AuthResponse += (s, e) => AuthResponseHandler?.Invoke(e);
            listener.CapturePreAuthResponse += (s, e) => CapturePreAuthResponseHandler?.Invoke(e);
            listener.CloseoutResponse += (s, e) => CloseoutResponseHandler?.Invoke(e);
            listener.ConfirmPaymentRequest += (s, e) => ConfirmPaymentRequestHandler?.Invoke(e);
            listener.CustomActivityResponse += (s, e) => CustomActivityResponseHandler?.Invoke(e);
            listener.CustomerProvidedData += (s, e) => CustomerProvidedDataHandler?.Invoke(e);
            listener.DeviceActivityEnd += (s, e) => DeviceActivityEndHandler?.Invoke(e);
            listener.DeviceActivityStart += (s, e) => DeviceActivityStartHandler?.Invoke(e);
            listener.DeviceConnected += (s, e) => DeviceConnectedHandler?.Invoke(e);
            listener.DeviceDisconnected += (s, e) => DeviceDisconnectedHandler?.Invoke(e);
            listener.DeviceError += (s, e) => DeviceErrorHandler?.Invoke(e);
            listener.DeviceReady += (s, e) => DeviceReadyHandler?.Invoke(e);
            listener.DisplayReceiptOptionsResponse += (s, e) => DisplayReceiptOptionsResponseHandler?.Invoke(e);
            listener.InvalidStateTransitionResponse += (s, e) => InvalidStateTransitionResponseHandler?.Invoke(e);
            listener.ManualRefundResponse += (s, e) => ManualRefundResponseHandler?.Invoke(e);
            listener.MessageFromActivity += (s, e) => MessageFromActivityHandler?.Invoke(e);
            listener.PreAuthResponse += (s, e) => PreAuthResponseHandler?.Invoke(e);
            listener.PrintJobStatusResponse += (s, e) => PrintJobStatusResponseHandler?.Invoke(e);
            listener.PrintManualRefundDeclineReceipt += (s, e) => PrintManualRefundDeclineReceiptHandler?.Invoke(e);
            listener.PrintManualRefundReceipt += (s, e) => PrintManualRefundReceiptHandler?.Invoke(e);
            listener.PrintPaymentDeclineReceipt += (s, e) => PrintPaymentDeclineReceiptHandler?.Invoke(e);
            listener.PrintPaymentMerchantCopyReceipt += (s, e) => PrintPaymentMerchantCopyReceiptHandler?.Invoke(e);
            listener.PrintPaymentReceipt += (s, e) => PrintPaymentReceiptHandler?.Invoke(e);
            listener.PrintRefundPaymentReceipt += (s, e) => PrintRefundPaymentReceiptHandler?.Invoke(e);
            listener.ReadCardDataResponse += (s, e) => ReadCardDataResponseHandler?.Invoke(e);
            listener.RefundPaymentResponse += (s, e) => RefundPaymentResponseHandler?.Invoke(e);
            listener.ResetDeviceResponse += (s, e) => ResetDeviceResponseHandler?.Invoke(e);
            listener.RetrieveDeviceStatusResponse += (s, e) => RetrieveDeviceStatusResponseHandler?.Invoke(e);
            listener.RetrievePaymentResponse += (s, e) => RetrievePaymentResponseHandler?.Invoke(e);
            listener.RetrievePendingPaymentsResponse += (s, e) => RetrievePendingPaymentsResponseHandler?.Invoke(e);
            listener.RetrievePrintersResponse += (s, e) => RetrievePrintersResponseHandler?.Invoke(e);
            listener.SaleResponse += (s, e) => SaleResponseHandler?.Invoke(e);
            listener.TipAdded += (s, e) => TipAddedHandler?.Invoke(e);
            listener.TipAdjustAuthResponse += (s, e) => TipAdjustAuthResponseHandler?.Invoke(e);
            listener.VaultCardResponse += (s, e) => VaultCardResponseHandler?.Invoke(e);
            listener.VerifySignatureRequest += (s, e) => VerifySignatureRequestHandler?.Invoke(e);
            listener.VoidPaymentRefundResponse += (s, e) => VoidPaymentRefundResponseHandler?.Invoke(e);
            listener.VoidPaymentResponse += (s, e) => VoidPaymentResponseHandler?.Invoke(e);
        }
        #endregion


        #region Connection
        public override Task<CreateResponse> Create(CreateRequest request, ServerCallContext context)
        {
            Program.WriteLine("Create");
            if (connector == null)
            {
                connector = sdk.CloverConnectorFactory.CreateUsbConnector("RAID", "POS", "Register1", true);
                connector.AddCloverConnectorListener(listener);
                connector.InitializeConnection();
            } else
            {
                DeviceReadyHandler.Reinvoke();
            }
            return Task.FromResult(new CreateResponse());
        }

        public override Task<DisposeResponse> Dispose(DisposeRequest request, ServerCallContext context)
        {
            Program.WriteLine("Dispose");

            AuthResponseHandler?.Promise.TrySetResult(0);
            AuthResponseHandler = null;
            CapturePreAuthResponseHandler?.Promise.TrySetResult(0);
            CapturePreAuthResponseHandler = null;
            CloseoutResponseHandler?.Promise.TrySetResult(0);
            CloseoutResponseHandler = null;
            ConfirmPaymentRequestHandler?.Promise.TrySetResult(0);
            ConfirmPaymentRequestHandler = null;
            CustomActivityResponseHandler?.Promise.TrySetResult(0);
            CustomActivityResponseHandler = null;
            CustomerProvidedDataHandler?.Promise.TrySetResult(0);
            CustomerProvidedDataHandler = null;
            DeviceActivityEndHandler?.Promise.TrySetResult(0);
            DeviceActivityEndHandler = null;
            DeviceActivityStartHandler?.Promise.TrySetResult(0);
            DeviceActivityStartHandler = null;
            DeviceConnectedHandler?.Promise.TrySetResult(0);
            DeviceConnectedHandler = null;
            DeviceDisconnectedHandler?.Promise.TrySetResult(0);
            DeviceDisconnectedHandler = null;
            DeviceErrorHandler?.Promise.TrySetResult(0);
            DeviceErrorHandler = null;
            DeviceReadyHandler?.Promise.TrySetResult(0);
            DeviceReadyHandler = null;
            DisplayReceiptOptionsResponseHandler?.Promise.TrySetResult(0);
            DisplayReceiptOptionsResponseHandler = null;
            InvalidStateTransitionResponseHandler?.Promise.TrySetResult(0);
            InvalidStateTransitionResponseHandler = null;
            ManualRefundResponseHandler?.Promise.TrySetResult(0);
            ManualRefundResponseHandler = null;
            MessageFromActivityHandler?.Promise.TrySetResult(0);
            MessageFromActivityHandler = null;
            PreAuthResponseHandler?.Promise.TrySetResult(0);
            PreAuthResponseHandler = null;
            PrintJobStatusResponseHandler?.Promise.TrySetResult(0);
            PrintJobStatusResponseHandler = null;
            PrintManualRefundDeclineReceiptHandler?.Promise.TrySetResult(0);
            PrintManualRefundDeclineReceiptHandler = null;
            PrintManualRefundReceiptHandler?.Promise.TrySetResult(0);
            PrintManualRefundReceiptHandler = null;
            PrintPaymentDeclineReceiptHandler?.Promise.TrySetResult(0);
            PrintPaymentDeclineReceiptHandler = null;
            PrintPaymentMerchantCopyReceiptHandler?.Promise.TrySetResult(0);
            PrintPaymentMerchantCopyReceiptHandler = null;
            PrintPaymentReceiptHandler?.Promise.TrySetResult(0);
            PrintPaymentReceiptHandler = null;
            PrintRefundPaymentReceiptHandler?.Promise.TrySetResult(0);
            PrintRefundPaymentReceiptHandler = null;
            ReadCardDataResponseHandler?.Promise.TrySetResult(0);
            ReadCardDataResponseHandler = null;
            RefundPaymentResponseHandler?.Promise.TrySetResult(0);
            RefundPaymentResponseHandler = null;
            ResetDeviceResponseHandler?.Promise.TrySetResult(0);
            ResetDeviceResponseHandler = null;
            RetrieveDeviceStatusResponseHandler?.Promise.TrySetResult(0);
            RetrieveDeviceStatusResponseHandler = null;
            RetrievePaymentResponseHandler?.Promise.TrySetResult(0);
            RetrievePaymentResponseHandler = null;
            RetrievePendingPaymentsResponseHandler?.Promise.TrySetResult(0);
            RetrievePendingPaymentsResponseHandler = null;
            RetrievePrintersResponseHandler?.Promise.TrySetResult(0);
            RetrievePrintersResponseHandler = null;
            SaleResponseHandler?.Promise.TrySetResult(0);
            SaleResponseHandler = null;
            TipAddedHandler?.Promise.TrySetResult(0);
            TipAddedHandler = null;
            TipAdjustAuthResponseHandler?.Promise.TrySetResult(0);
            TipAdjustAuthResponseHandler = null;
            VaultCardResponseHandler?.Promise.TrySetResult(0);
            VaultCardResponseHandler = null;
            VerifySignatureRequestHandler?.Promise.TrySetResult(0);
            VerifySignatureRequestHandler = null;
            VoidPaymentRefundResponseHandler?.Promise.TrySetResult(0);
            VoidPaymentRefundResponseHandler = null;
            VoidPaymentResponseHandler?.Promise.TrySetResult(0);
            VoidPaymentResponseHandler = null;

            //connector.RemoveCloverConnectorListener(listener);
            //connector.Dispose();

            //connector = null;
            //listener = null;

            return Task.FromResult(new DisposeResponse());
        }
        #endregion


        #region Connector
        public override Task<Empty> AcceptPayment(AcceptPaymentRequest request, ServerCallContext context)
        {
            Program.WriteLine("AcceptPayment");
            connector.AcceptPayment(Translate.From(request.Payment));
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> AcceptSignature(AcceptSignatureRequest request, ServerCallContext context)
        {
            Program.WriteLine("AcceptSignature");
            connector.AcceptSignature(Translate.From(request));
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> InvokeInputOption(InvokeInputOptionRequest request, ServerCallContext context)
        {
            Program.WriteLine("InvokeInputOption");
            connector.InvokeInputOption(Translate.From(request.InputOption));
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> RejectPayment(RejectPaymentRequest request, ServerCallContext context)
        {
            Program.WriteLine("RejectPayment");
            connector.RejectPayment(Translate.From(request.Payment), Translate.From(request.Challenge));
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> RejectSignature(RejectSignatureRequest request, ServerCallContext context)
        {
            Program.WriteLine("RejectSignature");
            connector.RejectSignature(Translate.From(request));
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> ResetDevice(ResetDeviceRequest request, ServerCallContext context)
        {
            Program.WriteLine("ResetDevice");
            connector.ResetDevice();
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> Sale(SaleRequest request, ServerCallContext context)
        {
            Program.WriteLine("Sale");
            connector.Sale(Translate.From(request));
            return Task.FromResult(new Empty());
        }
        #endregion


        #region Listener
        public override Task OnDeviceActivityStart(Empty request, IServerStreamWriter<DeviceEvent> responseStream, ServerCallContext context)
        {
            DeviceActivityStartHandler = new StreamHandler<sdk.CloverDeviceEvent, DeviceEvent>(
                responseStream,
                o => Translate.From(o)
            );
            return DeviceActivityStartHandler.Promise.Task;
        }

        public override Task OnDeviceReady(Empty request, IServerStreamWriter<MerchantInfo> responseStream, ServerCallContext context)
        {
            DeviceReadyHandler = new StreamHandler<sdk.MerchantInfo, MerchantInfo>(
                responseStream,
                o => Translate.From(o)
            );
            return DeviceReadyHandler.Promise.Task;
        }

        public override Task OnConfirmPaymentRequest(Empty request, IServerStreamWriter<ConfirmPaymentRequest> responseStream, ServerCallContext context)
        {
            ConfirmPaymentRequestHandler = new StreamHandler<sdk.ConfirmPaymentRequest, ConfirmPaymentRequest>(
                responseStream,
                o => Translate.From(o)
            );
            return ConfirmPaymentRequestHandler.Promise.Task;
        }

        public override Task OnVerifySignatureRequest(Empty request, IServerStreamWriter<VerifySignatureRequest> responseStream, ServerCallContext context)
        {
            VerifySignatureRequestHandler = new StreamHandler<sdk.VerifySignatureRequest, VerifySignatureRequest>(
                responseStream,
                o => Translate.From(o)
            );
            return VerifySignatureRequestHandler.Promise.Task;
        }

        public override Task OnSaleResponse(Empty request, IServerStreamWriter<SaleResponse> responseStream, ServerCallContext context)
        {
            SaleResponseHandler = new StreamHandler<sdk.SaleResponse, SaleResponse>(
                responseStream,
                o => Translate.From(o)
            );
            return SaleResponseHandler.Promise.Task;
        }

        public override Task OnResetDeviceResponse(Empty request, IServerStreamWriter<ResetDeviceResponse> responseStream, ServerCallContext context)
        {
            ResetDeviceResponseHandler = new StreamHandler<sdk.ResetDeviceResponse, ResetDeviceResponse>(
                responseStream,
                o => Translate.From(o)
            );
            return ResetDeviceResponseHandler.Promise.Task;
        }
        #endregion
    }

    public class StreamHandler<TSdk, TGrpc>
    {
        private static TSdk Cache { get; set; }

        private readonly object Lock = new object();
        private Task writeTask = Task.FromResult(0);

        public TaskCompletionSource<int> Promise { get; set; } = new TaskCompletionSource<int>();
        public IServerStreamWriter<TGrpc> Stream { get; set; }
        public Func<TSdk, TGrpc> ToGrpc { get; set; }

        public StreamHandler(IServerStreamWriter<TGrpc> stream, Func<TSdk, TGrpc> toGrpc)
        {
            Stream = stream;
            ToGrpc = toGrpc;
        }

        public void Invoke(TSdk obj)
        {
            lock(Lock)
            {
                var oldTask = writeTask;
                writeTask = Task.Run(() =>
                {
                    oldTask.Wait();
                    Cache = obj;
                    Stream.WriteAsync(ToGrpc(obj)).Wait();
                });
            }
        }

        public void Reinvoke() => Invoke(Cache);
    }
}
