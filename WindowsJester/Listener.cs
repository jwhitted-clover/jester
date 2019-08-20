using System;
using com.clover.remotepay.sdk;
using com.clover.remotepay.transport;
using Jester;
using Newtonsoft.Json;

namespace WindowsJester
{
    public class Listener : ICloverConnectorListener
    {
        private static Event EventFrom(string name)
        {
            return new Event
            {
                Name = name,
                Type = "null",
                Payload = "null",
            };
        }

        private static Event EventFrom(string name, object payload)
        {
            return new Event
            {
                Name = name,
                Type = payload.GetType().Name,
                Payload = JsonConvert.SerializeObject(payload),
            };
        }

        public event EventHandler<Event> Event;

        public void OnAuthResponse(AuthResponse response) => Event?.Invoke(this, EventFrom("AuthResponse", response));
        public void OnCapturePreAuthResponse(CapturePreAuthResponse response) => Event?.Invoke(this, EventFrom("CapturePreAuthResponse", response));
        public void OnCloseoutResponse(CloseoutResponse response) => Event?.Invoke(this, EventFrom("CloseoutResponse", response));
        public void OnConfirmPaymentRequest(ConfirmPaymentRequest request) => Event?.Invoke(this, EventFrom("ConfirmPaymentRequest", request));
        public void OnCustomActivityResponse(CustomActivityResponse response) => Event?.Invoke(this, EventFrom("CustomActivityResponse", response));
        public void OnCustomerProvidedData(CustomerProvidedDataEvent response) => Event?.Invoke(this, EventFrom("CustomerProvidedData", response));
        public void OnDeviceActivityEnd(CloverDeviceEvent deviceEvent) => Event?.Invoke(this, EventFrom("DeviceActivityEnd", deviceEvent));
        public void OnDeviceActivityStart(CloverDeviceEvent deviceEvent) => Event?.Invoke(this, EventFrom("DeviceActivityStart", deviceEvent));
        public void OnDeviceConnected() => Event?.Invoke(this, EventFrom("DeviceConnected"));
        public void OnDeviceDisconnected() => Event?.Invoke(this, EventFrom("DeviceDisconnected"));
        public void OnDeviceError(CloverDeviceErrorEvent deviceErrorEvent) => Event?.Invoke(this, EventFrom("DeviceError", deviceErrorEvent));
        public void OnDeviceReady(MerchantInfo merchantInfo) => Event?.Invoke(this, EventFrom("DeviceReady", merchantInfo));
        public void OnDisplayReceiptOptionsResponse(DisplayReceiptOptionsResponse response) => Event?.Invoke(this, EventFrom("DisplayReceiptOptionsResponse", response));
        public void OnInvalidStateTransitionResponse(InvalidStateTransitionNotification message) => Event?.Invoke(this, EventFrom("InvalidStateTransitionResponse", message));
        public void OnManualRefundResponse(ManualRefundResponse response) => Event?.Invoke(this, EventFrom("ManualRefundResponse", response));
        public void OnMessageFromActivity(MessageFromActivity response) => Event?.Invoke(this, EventFrom("MessageFromActivity", response));
        public void OnPreAuthResponse(PreAuthResponse response) => Event?.Invoke(this, EventFrom("PreAuthResponse", response));
        public void OnPrintJobStatusRequest(PrintJobStatusRequest request) => throw new NotImplementedException();
        public void OnPrintJobStatusResponse(PrintJobStatusResponse response) => Event?.Invoke(this, EventFrom("PrintJobStatusResponse", response));
        public void OnPrintManualRefundDeclineReceipt(PrintManualRefundDeclineReceiptMessage message) => Event?.Invoke(this, EventFrom("PrintManualRefundDeclineReceipt", message));
        public void OnPrintManualRefundReceipt(PrintManualRefundReceiptMessage message) => Event?.Invoke(this, EventFrom("PrintManualRefundReceipt", message));
        public void OnPrintPaymentDeclineReceipt(PrintPaymentDeclineReceiptMessage message) => Event?.Invoke(this, EventFrom("PrintPaymentDeclineReceipt", message));
        public void OnPrintPaymentMerchantCopyReceipt(PrintPaymentMerchantCopyReceiptMessage message) => Event?.Invoke(this, EventFrom("PrintPaymentMerchantCopyReceipt", message));
        public void OnPrintPaymentReceipt(PrintPaymentReceiptMessage message) => Event?.Invoke(this, EventFrom("PrintPaymentReceipt", message));
        public void OnPrintRefundPaymentReceipt(PrintRefundPaymentReceiptMessage message) => Event?.Invoke(this, EventFrom("PrintRefundPaymentReceipt", message));
        public void OnReadCardDataResponse(ReadCardDataResponse response) => Event?.Invoke(this, EventFrom("ReadCardDataResponse", response));
        public void OnRefundPaymentResponse(RefundPaymentResponse response) => Event?.Invoke(this, EventFrom("RefundPaymentResponse", response));
        public void OnResetDeviceResponse(ResetDeviceResponse response) => Event?.Invoke(this, EventFrom("ResetDeviceResponse", response));
        public void OnRetrieveDeviceStatusResponse(RetrieveDeviceStatusResponse response) => Event?.Invoke(this, EventFrom("RetrieveDeviceStatusResponse", response));
        public void OnRetrievePaymentResponse(RetrievePaymentResponse response) => Event?.Invoke(this, EventFrom("RetrievePaymentResponse", response));
        public void OnRetrievePendingPaymentsResponse(RetrievePendingPaymentsResponse response) => Event?.Invoke(this, EventFrom("RetrievePendingPaymentsResponse", response));
        public void OnRetrievePrintersResponse(RetrievePrintersResponse response) => Event?.Invoke(this, EventFrom("RetrievePrintersResponse", response));
        public void OnSaleResponse(SaleResponse response) => Event?.Invoke(this, EventFrom("SaleResponse", response));
        public void OnTipAdded(TipAddedMessage message) => Event?.Invoke(this, EventFrom("TipAdded", message));
        public void OnTipAdjustAuthResponse(TipAdjustAuthResponse response) => Event?.Invoke(this, EventFrom("TipAdjustAuthResponse", response));
        public void OnVaultCardResponse(VaultCardResponse response) => Event?.Invoke(this, EventFrom("VaultCardResponse", response));
        public void OnVerifySignatureRequest(VerifySignatureRequest request) => Event?.Invoke(this, EventFrom("VerifySignatureRequest", request));
        public void OnVoidPaymentRefundResponse(VoidPaymentRefundResponse response) => Event?.Invoke(this, EventFrom("VoidPaymentRefundResponse", response));
        public void OnVoidPaymentResponse(VoidPaymentResponse response) => Event?.Invoke(this, EventFrom("VoidPaymentResponse", response));
    }
}
