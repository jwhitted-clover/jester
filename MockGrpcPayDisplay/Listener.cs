using System;
using com.clover.remotepay.sdk;
using com.clover.remotepay.transport;

namespace MockGrpcPayDisplay
{
    public class Listener : ICloverConnectorListener
    {
        public event EventHandler<AuthResponse> AuthResponse;
        public event EventHandler<CapturePreAuthResponse> CapturePreAuthResponse;
        public event EventHandler<CloseoutResponse> CloseoutResponse;
        public event EventHandler<ConfirmPaymentRequest> ConfirmPaymentRequest;
        public event EventHandler<CustomActivityResponse> CustomActivityResponse;
        public event EventHandler<CustomerProvidedDataEvent> CustomerProvidedData;
        public event EventHandler<CloverDeviceEvent> DeviceActivityEnd;
        public event EventHandler<CloverDeviceEvent> DeviceActivityStart;
        public event EventHandler DeviceConnected;
        public event EventHandler DeviceDisconnected;
        public event EventHandler<CloverDeviceErrorEvent> DeviceError;
        public event EventHandler<MerchantInfo> DeviceReady;
        public event EventHandler<DisplayReceiptOptionsResponse> DisplayReceiptOptionsResponse;
        public event EventHandler<InvalidStateTransitionNotification> InvalidStateTransitionResponse;
        public event EventHandler<ManualRefundResponse> ManualRefundResponse;
        public event EventHandler<MessageFromActivity> MessageFromActivity;
        public event EventHandler<PreAuthResponse> PreAuthResponse;
        public event EventHandler<PrintJobStatusResponse> PrintJobStatusResponse;
        public event EventHandler<PrintManualRefundDeclineReceiptMessage> PrintManualRefundDeclineReceipt;
        public event EventHandler<PrintManualRefundReceiptMessage> PrintManualRefundReceipt;
        public event EventHandler<PrintPaymentDeclineReceiptMessage> PrintPaymentDeclineReceipt;
        public event EventHandler<PrintPaymentMerchantCopyReceiptMessage> PrintPaymentMerchantCopyReceipt;
        public event EventHandler<PrintPaymentReceiptMessage> PrintPaymentReceipt;
        public event EventHandler<PrintRefundPaymentReceiptMessage> PrintRefundPaymentReceipt;
        public event EventHandler<ReadCardDataResponse> ReadCardDataResponse;
        public event EventHandler<RefundPaymentResponse> RefundPaymentResponse;
        public event EventHandler<ResetDeviceResponse> ResetDeviceResponse;
        public event EventHandler<RetrieveDeviceStatusResponse> RetrieveDeviceStatusResponse;
        public event EventHandler<RetrievePaymentResponse> RetrievePaymentResponse;
        public event EventHandler<RetrievePendingPaymentsResponse> RetrievePendingPaymentsResponse;
        public event EventHandler<RetrievePrintersResponse> RetrievePrintersResponse;
        public event EventHandler<SaleResponse> SaleResponse;
        public event EventHandler<TipAddedMessage> TipAdded;
        public event EventHandler<TipAdjustAuthResponse> TipAdjustAuthResponse;
        public event EventHandler<VaultCardResponse> VaultCardResponse;
        public event EventHandler<VerifySignatureRequest> VerifySignatureRequest;
        public event EventHandler<VoidPaymentRefundResponse> VoidPaymentRefundResponse;
        public event EventHandler<VoidPaymentResponse> VoidPaymentResponse;

        public void OnAuthResponse(AuthResponse response) => AuthResponse?.Invoke(this, response);
        public void OnCapturePreAuthResponse(CapturePreAuthResponse response) => CapturePreAuthResponse?.Invoke(this, response);
        public void OnCloseoutResponse(CloseoutResponse response) => CloseoutResponse?.Invoke(this, response);
        public void OnConfirmPaymentRequest(ConfirmPaymentRequest request) => ConfirmPaymentRequest?.Invoke(this, request);
        public void OnCustomActivityResponse(CustomActivityResponse response) => CustomActivityResponse?.Invoke(this, response);
        public void OnCustomerProvidedData(CustomerProvidedDataEvent response) => CustomerProvidedData?.Invoke(this, response);
        public void OnDeviceActivityEnd(CloverDeviceEvent deviceEvent) => DeviceActivityEnd?.Invoke(this, deviceEvent);
        public void OnDeviceActivityStart(CloverDeviceEvent deviceEvent) => DeviceActivityStart?.Invoke(this, deviceEvent);
        public void OnDeviceConnected() => DeviceConnected?.Invoke(this, EventArgs.Empty);
        public void OnDeviceDisconnected() => DeviceDisconnected?.Invoke(this, EventArgs.Empty);
        public void OnDeviceError(CloverDeviceErrorEvent deviceErrorEvent) => DeviceError?.Invoke(this, deviceErrorEvent);
        public void OnDeviceReady(MerchantInfo merchantInfo) => DeviceReady?.Invoke(this, merchantInfo);
        public void OnDisplayReceiptOptionsResponse(DisplayReceiptOptionsResponse response) => DisplayReceiptOptionsResponse?.Invoke(this, response);
        public void OnInvalidStateTransitionResponse(InvalidStateTransitionNotification message) => InvalidStateTransitionResponse?.Invoke(this, message);
        public void OnManualRefundResponse(ManualRefundResponse response) => ManualRefundResponse?.Invoke(this, response);
        public void OnMessageFromActivity(MessageFromActivity response) => MessageFromActivity?.Invoke(this, response);
        public void OnPreAuthResponse(PreAuthResponse response) => PreAuthResponse?.Invoke(this, response);
        public void OnPrintJobStatusRequest(PrintJobStatusRequest request) => throw new NotImplementedException();
        public void OnPrintJobStatusResponse(PrintJobStatusResponse response) => PrintJobStatusResponse?.Invoke(this, response);
        public void OnPrintManualRefundDeclineReceipt(PrintManualRefundDeclineReceiptMessage message) => PrintManualRefundDeclineReceipt?.Invoke(this, message);
        public void OnPrintManualRefundReceipt(PrintManualRefundReceiptMessage message) => PrintManualRefundReceipt?.Invoke(this, message);
        public void OnPrintPaymentDeclineReceipt(PrintPaymentDeclineReceiptMessage message) => PrintPaymentDeclineReceipt?.Invoke(this, message);
        public void OnPrintPaymentMerchantCopyReceipt(PrintPaymentMerchantCopyReceiptMessage message) => PrintPaymentMerchantCopyReceipt?.Invoke(this, message);
        public void OnPrintPaymentReceipt(PrintPaymentReceiptMessage message) => PrintPaymentReceipt?.Invoke(this, message);
        public void OnPrintRefundPaymentReceipt(PrintRefundPaymentReceiptMessage message) => PrintRefundPaymentReceipt?.Invoke(this, message);
        public void OnReadCardDataResponse(ReadCardDataResponse response) => ReadCardDataResponse?.Invoke(this, response);
        public void OnRefundPaymentResponse(RefundPaymentResponse response) => RefundPaymentResponse?.Invoke(this, response);
        public void OnResetDeviceResponse(ResetDeviceResponse response) => ResetDeviceResponse?.Invoke(this, response);
        public void OnRetrieveDeviceStatusResponse(RetrieveDeviceStatusResponse response) => RetrieveDeviceStatusResponse?.Invoke(this, response);
        public void OnRetrievePaymentResponse(RetrievePaymentResponse response) => RetrievePaymentResponse?.Invoke(this, response);
        public void OnRetrievePendingPaymentsResponse(RetrievePendingPaymentsResponse response) => RetrievePendingPaymentsResponse?.Invoke(this, response);
        public void OnRetrievePrintersResponse(RetrievePrintersResponse response) => RetrievePrintersResponse?.Invoke(this, response);
        public void OnSaleResponse(SaleResponse response) => SaleResponse?.Invoke(this, response);
        public void OnTipAdded(TipAddedMessage message) => TipAdded?.Invoke(this, message);
        public void OnTipAdjustAuthResponse(TipAdjustAuthResponse response) => TipAdjustAuthResponse?.Invoke(this, response);
        public void OnVaultCardResponse(VaultCardResponse response) => VaultCardResponse?.Invoke(this, response);
        public void OnVerifySignatureRequest(VerifySignatureRequest request) => VerifySignatureRequest?.Invoke(this, request);
        public void OnVoidPaymentRefundResponse(VoidPaymentRefundResponse response) => VoidPaymentRefundResponse?.Invoke(this, response);
        public void OnVoidPaymentResponse(VoidPaymentResponse response) => VoidPaymentResponse?.Invoke(this, response);
    }
}
