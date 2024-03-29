﻿using System;
using System.Threading.Tasks;
using Clover.Grpc;
using Grpc.Core;
using sdk = com.clover.remotepay.sdk;
using transport = com.clover.remotepay.transport;

namespace MockGrpcPayDisplay
{
    public class PayDisplayImpl : PayDisplay.PayDisplayBase
    {
        #region Properties
        private sdk.ICloverConnector Connector { get; set; }
        private Listener Listener { get; set; }
        private StreamHandler<PingPacket, PingPacket> PingHandler { get; set; }
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
        #endregion


        #region Constructor
        public PayDisplayImpl()
        {
            Listener = new Listener();
            Listener.AuthResponse += (s, e) => AuthResponseHandler?.Invoke(e);
            Listener.CapturePreAuthResponse += (s, e) => CapturePreAuthResponseHandler?.Invoke(e);
            Listener.CloseoutResponse += (s, e) => CloseoutResponseHandler?.Invoke(e);
            Listener.ConfirmPaymentRequest += (s, e) => ConfirmPaymentRequestHandler?.Invoke(e);
            Listener.CustomActivityResponse += (s, e) => CustomActivityResponseHandler?.Invoke(e);
            Listener.CustomerProvidedData += (s, e) => CustomerProvidedDataHandler?.Invoke(e);
            Listener.DeviceActivityEnd += (s, e) => DeviceActivityEndHandler?.Invoke(e);
            Listener.DeviceActivityStart += (s, e) => DeviceActivityStartHandler?.Invoke(e);
            Listener.DeviceConnected += (s, e) => DeviceConnectedHandler?.Invoke(e);
            Listener.DeviceDisconnected += (s, e) => DeviceDisconnectedHandler?.Invoke(e);
            Listener.DeviceError += (s, e) => DeviceErrorHandler?.Invoke(e);
            Listener.DeviceReady += (s, e) => DeviceReadyHandler?.Invoke(e);
            Listener.DisplayReceiptOptionsResponse += (s, e) => DisplayReceiptOptionsResponseHandler?.Invoke(e);
            Listener.InvalidStateTransitionResponse += (s, e) => InvalidStateTransitionResponseHandler?.Invoke(e);
            Listener.ManualRefundResponse += (s, e) => ManualRefundResponseHandler?.Invoke(e);
            Listener.MessageFromActivity += (s, e) => MessageFromActivityHandler?.Invoke(e);
            Listener.PreAuthResponse += (s, e) => PreAuthResponseHandler?.Invoke(e);
            Listener.PrintJobStatusResponse += (s, e) => PrintJobStatusResponseHandler?.Invoke(e);
            Listener.PrintManualRefundDeclineReceipt += (s, e) => PrintManualRefundDeclineReceiptHandler?.Invoke(e);
            Listener.PrintManualRefundReceipt += (s, e) => PrintManualRefundReceiptHandler?.Invoke(e);
            Listener.PrintPaymentDeclineReceipt += (s, e) => PrintPaymentDeclineReceiptHandler?.Invoke(e);
            Listener.PrintPaymentMerchantCopyReceipt += (s, e) => PrintPaymentMerchantCopyReceiptHandler?.Invoke(e);
            Listener.PrintPaymentReceipt += (s, e) => PrintPaymentReceiptHandler?.Invoke(e);
            Listener.PrintRefundPaymentReceipt += (s, e) => PrintRefundPaymentReceiptHandler?.Invoke(e);
            Listener.ReadCardDataResponse += (s, e) => ReadCardDataResponseHandler?.Invoke(e);
            Listener.RefundPaymentResponse += (s, e) => RefundPaymentResponseHandler?.Invoke(e);
            Listener.ResetDeviceResponse += (s, e) => ResetDeviceResponseHandler?.Invoke(e);
            Listener.RetrieveDeviceStatusResponse += (s, e) => RetrieveDeviceStatusResponseHandler?.Invoke(e);
            Listener.RetrievePaymentResponse += (s, e) => RetrievePaymentResponseHandler?.Invoke(e);
            Listener.RetrievePendingPaymentsResponse += (s, e) => RetrievePendingPaymentsResponseHandler?.Invoke(e);
            Listener.RetrievePrintersResponse += (s, e) => RetrievePrintersResponseHandler?.Invoke(e);
            Listener.SaleResponse += (s, e) => SaleResponseHandler?.Invoke(e);
            Listener.TipAdded += (s, e) => TipAddedHandler?.Invoke(e);
            Listener.TipAdjustAuthResponse += (s, e) => TipAdjustAuthResponseHandler?.Invoke(e);
            Listener.VaultCardResponse += (s, e) => VaultCardResponseHandler?.Invoke(e);
            Listener.VerifySignatureRequest += (s, e) => VerifySignatureRequestHandler?.Invoke(e);
            Listener.VoidPaymentRefundResponse += (s, e) => VoidPaymentRefundResponseHandler?.Invoke(e);
            Listener.VoidPaymentResponse += (s, e) => VoidPaymentResponseHandler?.Invoke(e);
        }
        #endregion


        #region Connection
        public override Task<CreateResponse> Create(CreateRequest request, ServerCallContext context)
        {
            Program.WriteLine("Create");
            if (Connector == null)
            {
                Connector = sdk.CloverConnectorFactory.CreateUsbConnector("RAID", "POS", "Register1", true);
                Connector.AddCloverConnectorListener(Listener);
                Connector.InitializeConnection();
            }
            else
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

        public override Task<InitializeResponse> Initialize(InitializeRequest request, ServerCallContext context)
        {
            Program.WriteLine("Initialize");
            // NOTE: This is currently happening in `Create()`;
            // connector.InitializeConnection();
            return Task.FromResult(new InitializeResponse());
        }

        public override Task Ping(IAsyncStreamReader<PingPacket> requestStream, IServerStreamWriter<PingPacket> responseStream, ServerCallContext context)
        {
            Console.WriteLine("Ping");
            PingHandler = new StreamHandler<PingPacket, PingPacket>(responseStream, o => o);

            var clientPingTask = Task.Run(async () =>
            {
                while (await requestStream.MoveNext())
                {
                    if (PingHandler.Promise.Task.IsCompleted) return;
                    PingHandler.Invoke(new PingPacket
                    {
                        Id = requestStream.Current.Id,
                        Type = PingType.Pong,
                    });
                }
            });

            var serverPingTask = Task.Run(async () =>
            {
                int pingId = 0;
                while (!PingHandler.Promise.Task.IsCompleted)
                {
                    PingHandler.Invoke(new PingPacket
                    {
                        Id = ++pingId,
                        Type = PingType.Ping,
                    });
                    await Task.Delay(1000);
                }
            });

            return PingHandler.Promise.Task;
        }
        #endregion


        #region Connector
        public override Task<Empty> AcceptPayment(AcceptPaymentRequest request, ServerCallContext context)
        {
            Program.WriteLine("AcceptPayment");
            Connector.AcceptPayment(Translate.From(request.Payment));
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> AcceptSignature(AcceptSignatureRequest request, ServerCallContext context)
        {
            Program.WriteLine("AcceptSignature");
            Connector.AcceptSignature(Translate.From(request));
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> Auth(AuthRequest request, ServerCallContext context)
        {
            Program.WriteLine("Auth");
            Connector.Auth(Translate.From(request));
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> CapturePreAuth(CapturePreAuthRequest request, ServerCallContext context)
        {
            Program.WriteLine("CapturePreAuth");
            Connector.CapturePreAuth(Translate.From(request));
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> Closeout(CloseoutRequest request, ServerCallContext context)
        {
            Program.WriteLine("Closeout");
            Connector.Closeout(Translate.From(request));
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> DisplayPaymentReceiptOptions(DisplayPaymentReceiptOptionsRequest request, ServerCallContext context)
        {
            Program.WriteLine("DisplayPaymentReceiptOptions");
            Connector.DisplayPaymentReceiptOptions(Translate.From(request));
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> DisplayReceiptOptions(DisplayReceiptOptionsRequest request, ServerCallContext context)
        {
            Program.WriteLine("DisplayReceiptOptions");
            Connector.DisplayReceiptOptions(Translate.From(request));
            return Task.FromResult(new Empty());
        }
        
        public override Task<Empty> InvokeInputOption(InvokeInputOptionRequest request, ServerCallContext context)
        {
            Program.WriteLine("InvokeInputOption");
            Connector.InvokeInputOption(Translate.From(request.InputOption));
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> ManualRefund(ManualRefundRequest request, ServerCallContext context)
        {
            Program.WriteLine("ManualRefund");
            Connector.ManualRefund(Translate.From(request));
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> OpenCashDrawer(OpenCashDrawerRequest request, ServerCallContext context)
        {
            Program.WriteLine("OpenCashDrawer");
            Connector.OpenCashDrawer(Translate.From(request));
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> Print(PrintRequest request, ServerCallContext context)
        {
            Console.WriteLine("Print");
            Connector.Print(Translate.From(request));
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> PreAuth(PreAuthRequest request, ServerCallContext context)
        {
            Program.WriteLine("PreAuth");
            Connector.PreAuth(Translate.From(request));
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> ReadCardData(ReadCardDataRequest request, ServerCallContext context)
        {
            Program.WriteLine("ReadCardData");
            Connector.ReadCardData(Translate.From(request));
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> RefundPayment(RefundPaymentRequest request, ServerCallContext context)
        {
            Program.WriteLine("RefundPayment");
            Connector.RefundPayment(Translate.From(request));
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> RegisterForCustomerProvidedData(RegisterForCustomerProvidedDataRequest request, ServerCallContext context)
        {
            Program.WriteLine("RegisterForCustomerProvidedData");
            Connector.RegisterForCustomerProvidedData(Translate.From(request));
            return Task.FromResult(new Empty());
        }
        
        public override Task<Empty> RejectPayment(RejectPaymentRequest request, ServerCallContext context)

        {
            Program.WriteLine("RejectPayment");
            Connector.RejectPayment(Translate.From(request.Payment), Translate.From(request.Challenge));
            return Task.FromResult(new Empty());
        }
        
        public override Task<Empty> RejectSignature(RejectSignatureRequest request, ServerCallContext context)
        {
            Program.WriteLine("RejectSignature");
            Connector.RejectSignature(Translate.From(request));
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> RemoveDisplayOrder(RemoveDisplayOrderRequest request, ServerCallContext context)
        {
            Program.WriteLine("RemoveDisplayOrder");
            Connector.RemoveDisplayOrder(Translate.From(request.DisplayOrder));
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> ResetDevice(ResetDeviceRequest request, ServerCallContext context)
        {
            Program.WriteLine("ResetDevice");
            Connector.ResetDevice();
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> RetrieveDeviceStatus(RetrieveDeviceStatusRequest request, ServerCallContext context)
        {
            Program.WriteLine("RetrieveDeviceStatus");
            Connector.RetrieveDeviceStatus(Translate.From(request));
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> RetrievePayment(RetrievePaymentRequest request, ServerCallContext context)
        {
            Program.WriteLine("RetrievePayment");
            Connector.RetrievePayment(Translate.From(request));
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> RetrievePendingPayments(RetrievePendingPaymentsRequest request, ServerCallContext context)
        {
            Program.WriteLine("RetrievePendingPayments");
            Connector.RetrievePendingPayments();
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> RetrievePrinters(RetrievePrintersRequest request, ServerCallContext context)
        {
            Program.WriteLine("RetrievePrinters");
            Connector.RetrievePrinters(Translate.From(request));
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> RetrievePrintJobStatus(RetrievePrintJobStatusRequest request, ServerCallContext context)
        {
            Program.WriteLine("RetrievePrintJobStatus");
            Connector.RetrievePrintJobStatus(Translate.From(request));
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> Sale(SaleRequest request, ServerCallContext context)
        {
            Program.WriteLine("Sale");
            Connector.Sale(Translate.From(request));
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> SendMessageToActivity(SendMessageToActivityRequest request, ServerCallContext context)
        {
            Program.WriteLine("SendMessageToActivity");
            Connector.SendMessageToActivity(Translate.From(request));
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> SetCustomerInfo(SetCustomerInfoRequest request, ServerCallContext context)
        {
            Program.WriteLine("SetCustomerInfo");
            Connector.SetCustomerInfo(Translate.From(request));
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> ShowDisplayOrder(ShowDisplayOrderRequest request, ServerCallContext context)
        {
            Program.WriteLine("ShowDisplayOrder");
            Connector.ShowDisplayOrder(Translate.From(request.DisplayOrder));
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> ShowMessage(ShowMessageRequest request, ServerCallContext context)
        {
            Program.WriteLine("ShowMessage");
            Connector.ShowMessage(request.Message);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> ShowThankYouScreen(ShowThankYouScreenRequest request, ServerCallContext context)
        {
            Program.WriteLine("ShowThankYouScreen");
            Connector.ShowThankYouScreen();
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> ShowWelcomeScreen(ShowWelcomeScreenRequest request, ServerCallContext context)
        {
            Program.WriteLine("ShowWelcomeScreen");
            Connector.ShowWelcomeScreen();
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> StartCustomActivity(StartCustomActivityRequest request, ServerCallContext context)
        {
            Program.WriteLine("StartCustomActivity");
            Connector.StartCustomActivity(Translate.From(request));
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> TipAdjustAuth(TipAdjustAuthRequest request, ServerCallContext context)
        {
            Program.WriteLine("TipAdjustAuth");
            Connector.TipAdjustAuth(Translate.From(request));
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> VaultCard(VaultCardRequest request, ServerCallContext context)
        {
            Program.WriteLine("VaultCard");
            Connector.VaultCard(Translate.From(request));
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> VoidPayment(VoidPaymentRequest request, ServerCallContext context)
        {
            Program.WriteLine("VoidPayment");
            Connector.VoidPayment(Translate.From(request));
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> VoidPaymentRefund(VoidPaymentRefundRequest request, ServerCallContext context)
        {
            Program.WriteLine("VoidPaymentRefund");
            Connector.VoidPaymentRefund(Translate.From(request));
            return Task.FromResult(new Empty());
        }
        #endregion


        #region Listener
        public override Task OnAuthResponse(Empty request, IServerStreamWriter<AuthResponse> responseStream, ServerCallContext context)
        {
            AuthResponseHandler = new StreamHandler<sdk.AuthResponse, AuthResponse>(responseStream, o => Translate.From(o));
            return AuthResponseHandler.Promise.Task;
        }

        public override Task OnCapturePreAuthResponse(Empty request, IServerStreamWriter<CapturePreAuthResponse> responseStream, ServerCallContext context)
        {
            CapturePreAuthResponseHandler = new StreamHandler<sdk.CapturePreAuthResponse, CapturePreAuthResponse>(responseStream, o => Translate.From(o));
            return CapturePreAuthResponseHandler.Promise.Task;
        }

        public override Task OnCloseoutResponse(Empty request, IServerStreamWriter<CloseoutResponse> responseStream, ServerCallContext context)
        {
            CloseoutResponseHandler = new StreamHandler<sdk.CloseoutResponse, CloseoutResponse>(responseStream, o => Translate.From(o));
            return CloseoutResponseHandler.Promise.Task;
        }

        public override Task OnConfirmPaymentRequest(Empty request, IServerStreamWriter<ConfirmPaymentRequest> responseStream, ServerCallContext context)
        {
            ConfirmPaymentRequestHandler = new StreamHandler<sdk.ConfirmPaymentRequest, ConfirmPaymentRequest>(responseStream, o => Translate.From(o));
            return ConfirmPaymentRequestHandler.Promise.Task;
        }

        public override Task OnCustomActivityResponse(Empty request, IServerStreamWriter<CustomActivityResponse> responseStream, ServerCallContext context)
        {
            CustomActivityResponseHandler = new StreamHandler<sdk.CustomActivityResponse, CustomActivityResponse>(responseStream, o => Translate.From(o));
            return CustomActivityResponseHandler.Promise.Task;
        }

        public override Task OnCustomerProvidedData(Empty request, IServerStreamWriter<CustomerProvidedDataEvent> responseStream, ServerCallContext context)
        {
            CustomerProvidedDataHandler = new StreamHandler<sdk.CustomerProvidedDataEvent, CustomerProvidedDataEvent>(responseStream, o => Translate.From(o));
            return CustomerProvidedDataHandler.Promise.Task;
        }

        public override Task OnDeviceActivityEnd(Empty request, IServerStreamWriter<DeviceEvent> responseStream, ServerCallContext context)
        {
            DeviceActivityEndHandler = new StreamHandler<sdk.CloverDeviceEvent, DeviceEvent>(responseStream, o => Translate.From(o));
            return DeviceActivityEndHandler.Promise.Task;
        }

        public override Task OnDeviceActivityStart(Empty request, IServerStreamWriter<DeviceEvent> responseStream, ServerCallContext context)
        {
            DeviceActivityStartHandler = new StreamHandler<sdk.CloverDeviceEvent, DeviceEvent>(responseStream, o => Translate.From(o));
            return DeviceActivityStartHandler.Promise.Task;
        }

        public override Task OnDeviceConnected(Empty request, IServerStreamWriter<Empty> responseStream, ServerCallContext context)
        {
            DeviceConnectedHandler = new StreamHandler<EventArgs, Empty>(responseStream, o => new Empty());
            return DeviceConnectedHandler.Promise.Task;
        }

        public override Task OnDeviceDisconnected(Empty request, IServerStreamWriter<Empty> responseStream, ServerCallContext context)
        {
            DeviceDisconnectedHandler = new StreamHandler<EventArgs, Empty>(responseStream, o => new Empty());
            return DeviceDisconnectedHandler.Promise.Task;
        }

        public override Task OnDeviceError(Empty request, IServerStreamWriter<DeviceErrorEvent> responseStream, ServerCallContext context)
        {
            DeviceErrorHandler = new StreamHandler<sdk.CloverDeviceErrorEvent, DeviceErrorEvent>(responseStream, o => Translate.From(o));
            return DeviceErrorHandler.Promise.Task;
        }

        public override Task OnDeviceReady(Empty request, IServerStreamWriter<MerchantInfo> responseStream, ServerCallContext context)
        {
            DeviceReadyHandler = new StreamHandler<sdk.MerchantInfo, MerchantInfo>(responseStream, o => Translate.From(o));
            return DeviceReadyHandler.Promise.Task;
        }

        public override Task OnDisplayReceiptOptionsResponse(Empty request, IServerStreamWriter<DisplayReceiptOptionsResponse> responseStream, ServerCallContext context)
        {
            DisplayReceiptOptionsResponseHandler = new StreamHandler<sdk.DisplayReceiptOptionsResponse, DisplayReceiptOptionsResponse>(responseStream, o => Translate.From(o));
            return DisplayReceiptOptionsResponseHandler.Promise.Task;
        }

        public override Task OnInvalidStateTransitionResponse(Empty request, IServerStreamWriter<InvalidStateTransitionNotification> responseStream, ServerCallContext context)
        {
            InvalidStateTransitionResponseHandler = new StreamHandler<sdk.InvalidStateTransitionNotification, InvalidStateTransitionNotification>(responseStream, o => Translate.From(o));
            return InvalidStateTransitionResponseHandler.Promise.Task;
        }

        public override Task OnManualRefundResponse(Empty request, IServerStreamWriter<ManualRefundResponse> responseStream, ServerCallContext context)
        {
            ManualRefundResponseHandler = new StreamHandler<sdk.ManualRefundResponse, ManualRefundResponse>(responseStream, o => Translate.From(o));
            return ManualRefundResponseHandler.Promise.Task;
        }

        public override Task OnMessageFromActivity(Empty request, IServerStreamWriter<MessageFromActivity> responseStream, ServerCallContext context)
        {
            MessageFromActivityHandler = new StreamHandler<sdk.MessageFromActivity, MessageFromActivity>(responseStream, o => Translate.From(o));
            return MessageFromActivityHandler.Promise.Task;
        }

        public override Task OnPreAuthResponse(Empty request, IServerStreamWriter<PreAuthResponse> responseStream, ServerCallContext context)
        {
            PreAuthResponseHandler = new StreamHandler<sdk.PreAuthResponse, PreAuthResponse>(responseStream, o => Translate.From(o));
            return PreAuthResponseHandler.Promise.Task;
        }

        public override Task OnPrintJobStatusResponse(Empty request, IServerStreamWriter<PrintJobStatusResponse> responseStream, ServerCallContext context)
        {
            PrintJobStatusResponseHandler = new StreamHandler<sdk.PrintJobStatusResponse, PrintJobStatusResponse>(responseStream, o => Translate.From(o));
            return PrintJobStatusResponseHandler.Promise.Task;
        }

        public override Task OnPrintManualRefundDeclineReceipt(Empty request, IServerStreamWriter<PrintManualRefundDeclineReceiptMessage> responseStream, ServerCallContext context)
        {
            PrintManualRefundDeclineReceiptHandler = new StreamHandler<sdk.PrintManualRefundDeclineReceiptMessage, PrintManualRefundDeclineReceiptMessage>(responseStream, o => Translate.From(o));
            return PrintManualRefundDeclineReceiptHandler.Promise.Task;
        }

        public override Task OnPrintManualRefundReceipt(Empty request, IServerStreamWriter<PrintManualRefundReceiptMessage> responseStream, ServerCallContext context)
        {
            PrintManualRefundReceiptHandler = new StreamHandler<sdk.PrintManualRefundReceiptMessage, PrintManualRefundReceiptMessage>(responseStream, o => Translate.From(o));
            return PrintManualRefundReceiptHandler.Promise.Task;
        }

        public override Task OnPrintPaymentDeclineReceipt(Empty request, IServerStreamWriter<PrintPaymentDeclineReceiptMessage> responseStream, ServerCallContext context)
        {
            PrintPaymentDeclineReceiptHandler = new StreamHandler<sdk.PrintPaymentDeclineReceiptMessage, PrintPaymentDeclineReceiptMessage>(responseStream, o => Translate.From(o));
            return PrintPaymentDeclineReceiptHandler.Promise.Task;
        }

        public override Task OnPrintPaymentMerchantCopyReceipt(Empty request, IServerStreamWriter<PrintPaymentMerchantCopyReceiptMessage> responseStream, ServerCallContext context)
        {
            PrintPaymentMerchantCopyReceiptHandler = new StreamHandler<sdk.PrintPaymentMerchantCopyReceiptMessage, PrintPaymentMerchantCopyReceiptMessage>(responseStream, o => Translate.From(o));
            return PrintPaymentMerchantCopyReceiptHandler.Promise.Task;
        }

        public override Task OnPrintPaymentReceipt(Empty request, IServerStreamWriter<PrintPaymentReceiptMessage> responseStream, ServerCallContext context)
        {
            PrintPaymentReceiptHandler = new StreamHandler<sdk.PrintPaymentReceiptMessage, PrintPaymentReceiptMessage>(responseStream, o => Translate.From(o));
            return PrintPaymentReceiptHandler.Promise.Task;
        }

        public override Task OnPrintRefundPaymentReceipt(Empty request, IServerStreamWriter<PrintRefundPaymentReceiptMessage> responseStream, ServerCallContext context)
        {
            PrintRefundPaymentReceiptHandler = new StreamHandler<sdk.PrintRefundPaymentReceiptMessage, PrintRefundPaymentReceiptMessage>(responseStream, o => Translate.From(o));
            return PrintRefundPaymentReceiptHandler.Promise.Task;
        }

        public override Task OnReadCardDataResponse(Empty request, IServerStreamWriter<ReadCardDataResponse> responseStream, ServerCallContext context)
        {
            ReadCardDataResponseHandler = new StreamHandler<sdk.ReadCardDataResponse, ReadCardDataResponse>(responseStream, o => Translate.From(o));
            return ReadCardDataResponseHandler.Promise.Task;
        }

        public override Task OnRefundPaymentResponse(Empty request, IServerStreamWriter<RefundPaymentResponse> responseStream, ServerCallContext context)
        {
            RefundPaymentResponseHandler = new StreamHandler<sdk.RefundPaymentResponse, RefundPaymentResponse>(responseStream, o => Translate.From(o));
            return RefundPaymentResponseHandler.Promise.Task;
        }

        public override Task OnRetrieveDeviceStatusResponse(Empty request, IServerStreamWriter<RetrieveDeviceStatusResponse> responseStream, ServerCallContext context)
        {
            RetrieveDeviceStatusResponseHandler = new StreamHandler<sdk.RetrieveDeviceStatusResponse, RetrieveDeviceStatusResponse>(responseStream, o => Translate.From(o));
            return RetrieveDeviceStatusResponseHandler.Promise.Task;
        }

        public override Task OnRetrievePaymentResponse(Empty request, IServerStreamWriter<RetrievePaymentResponse> responseStream, ServerCallContext context)
        {
            RetrievePaymentResponseHandler = new StreamHandler<sdk.RetrievePaymentResponse, RetrievePaymentResponse>(responseStream, o => Translate.From(o));
            return RetrievePaymentResponseHandler.Promise.Task;
        }

        public override Task OnRetrievePendingPaymentsResponse(Empty request, IServerStreamWriter<RetrievePendingPaymentsResponse> responseStream, ServerCallContext context)
        {
            RetrievePendingPaymentsResponseHandler = new StreamHandler<sdk.RetrievePendingPaymentsResponse, RetrievePendingPaymentsResponse>(responseStream, o => Translate.From(o));
            return RetrievePendingPaymentsResponseHandler.Promise.Task;
        }

        public override Task OnRetrievePrintersResponse(Empty request, IServerStreamWriter<RetrievePrintersResponse> responseStream, ServerCallContext context)
        {
            RetrievePrintersResponseHandler = new StreamHandler<sdk.RetrievePrintersResponse, RetrievePrintersResponse>(responseStream, o => Translate.From(o));
            return RetrievePrintersResponseHandler.Promise.Task;
        }

        public override Task OnVerifySignatureRequest(Empty request, IServerStreamWriter<VerifySignatureRequest> responseStream, ServerCallContext context)
        {
            VerifySignatureRequestHandler = new StreamHandler<sdk.VerifySignatureRequest, VerifySignatureRequest>(responseStream, o => Translate.From(o));
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
            ResetDeviceResponseHandler = new StreamHandler<sdk.ResetDeviceResponse, ResetDeviceResponse>(responseStream, o => Translate.From(o));
            return ResetDeviceResponseHandler.Promise.Task;
        }

        public override Task OnTipAdded(Empty request, IServerStreamWriter<TipAddedMessage> responseStream, ServerCallContext context)
        {
            TipAddedHandler = new StreamHandler<transport.TipAddedMessage, TipAddedMessage>(responseStream, o => Translate.From(o));
            return TipAddedHandler.Promise.Task;
        }

        public override Task OnTipAdjustAuthResponse(Empty request, IServerStreamWriter<TipAdjustAuthResponse> responseStream, ServerCallContext context)
        {
            TipAdjustAuthResponseHandler = new StreamHandler<sdk.TipAdjustAuthResponse, TipAdjustAuthResponse>(responseStream, o => Translate.From(o));
            return TipAdjustAuthResponseHandler.Promise.Task;
        }

        public override Task OnVaultCardResponse(Empty request, IServerStreamWriter<VaultCardResponse> responseStream, ServerCallContext context)
        {
            VaultCardResponseHandler = new StreamHandler<sdk.VaultCardResponse, VaultCardResponse>(responseStream, o => Translate.From(o));
            return VaultCardResponseHandler.Promise.Task;
        }

        public override Task OnVoidPaymentRefundResponse(Empty request, IServerStreamWriter<VoidPaymentRefundResponse> responseStream, ServerCallContext context)
        {
            VoidPaymentRefundResponseHandler = new StreamHandler<sdk.VoidPaymentRefundResponse, VoidPaymentRefundResponse>(responseStream, o => Translate.From(o));
            return VoidPaymentRefundResponseHandler.Promise.Task;
        }

        public override Task OnVoidPaymentResponse(Empty request, IServerStreamWriter<VoidPaymentResponse> responseStream, ServerCallContext context)
        {
            VoidPaymentResponseHandler = new StreamHandler<sdk.VoidPaymentResponse, VoidPaymentResponse>(responseStream, o => Translate.From(o));
            return VoidPaymentResponseHandler.Promise.Task;
        }
        #endregion
    }
}
