const clover = require('remote-pay-cloud');

let connector;
let listener;
let endListen;

const onEvent = (name, payload) => {};

const create = (call, callback) => {
  const config = new clover.WebSocketPairedCloverDeviceConfigurationBuilder(
    'RAID',
    'wss://10.247.3.111:12345',
    'POS',
    'Register1',
    null,
    code => console.log('onPairingCode', code),
    token => console.log('onPairingSuccess', token)
  );
  connector = clover.CloverConnectorFactoryBuilder.createICloverConnectorFactory(
    {
      [clover.CloverConnectorFactoryBuilder.FACTORY_VERSION]:
        clover.CloverConnectorFactoryBuilder.VERSION_12,
    }
  );
  listener = Object.assign(
    {},
    clover.remotepay.ICloverConnectorListener.prototype,
    {
      onAuthResponse: authResponse => onEvent('AuthResponse', authResponse),
      onCapturePreAuthResponse: capturePreAuthResponse =>
        onEvent('CapturePreAuthResponse', capturePreAuthResponse),
      onCloseoutResponse: closeoutResponse =>
        onEvent('CloseoutResponse', closeoutResponse),
      onConfirmPaymentRequest: confirmPaymentRequest =>
        onEvent('ConfirmPaymentRequest', confirmPaymentRequest),
      onCustomActivityResponse: customActivityResponse =>
        onEvent('CustomActivityResponse', customActivityResponse),
      onCustomerProvidedData: customerProvidedDataEvent =>
        onEvent('CustomerProvidedData', customerProvidedDataEvent),
      onDeviceActivityEnd: cloverDeviceEvent =>
        onEvent('DeviceActivityEnd', cloverDeviceEvent),
      onDeviceActivityStart: cloverDeviceEvent =>
        onEvent('DeviceActivityStart', cloverDeviceEvent),
      onDeviceConnected: () => onEvent('DeviceConnected', null),
      onDeviceDisconnected: () => onEvent('DeviceDisconnected', null),
      onDeviceError: cloverDeviceErrorEvent =>
        onEvent('DeviceError', cloverDeviceErrorEvent),
      onDeviceReady: merchantInfo => onEvent('DeviceReady', merchantInfo),
      onDisplayReceiptOptionsResponse: displayReceiptOptionsResponse =>
        onEvent('DisplayReceiptOptionsResponse', displayReceiptOptionsResponse),
      onInvalidStateTransitionResponse: invalidStateTransitionNotification =>
        onEvent(
          'InvalidStateTransitionResponse',
          invalidStateTransitionNotification
        ),
      onManualRefundResponse: manualRefundResponse =>
        onEvent('ManualRefundResponse', manualRefundResponse),
      onMessageFromActivity: messageFromActivity =>
        onEvent('MessageFromActivity', messageFromActivity),
      onPreAuthResponse: preAuthResponse =>
        onEvent('PreAuthResponse', preAuthResponse),
      onPrintJobStatusRequest: printJobStatusRequest =>
        onEvent('PrintJobStatusRequest', printJobStatusRequest),
      onPrintJobStatusResponse: printJobStatusResponse =>
        onEvent('PrintJobStatusResponse', printJobStatusResponse),
      onPrintManualRefundDeclineReceipt: printManualRefundDeclineReceiptMessage =>
        onEvent(
          'PrintManualRefundDeclineReceipt',
          printManualRefundDeclineReceiptMessage
        ),
      onPrintManualRefundReceipt: printManualRefundReceiptMessage =>
        onEvent('PrintManualRefundReceipt', printManualRefundReceiptMessage),
      onPrintPaymentDeclineReceipt: printPaymentDeclineReceiptMessage =>
        onEvent(
          'PrintPaymentDeclineReceipt',
          printPaymentDeclineReceiptMessage
        ),
      onPrintPaymentMerchantCopyReceipt: printPaymentMerchantCopyReceiptMessage =>
        onEvent(
          'PrintPaymentMerchantCopyReceipt',
          printPaymentMerchantCopyReceiptMessage
        ),
      onPrintPaymentReceipt: printPaymentReceiptMessage =>
        onEvent('PrintPaymentReceipt', printPaymentReceiptMessage),
      onPrintRefundPaymentReceipt: printRefundPaymentReceiptMessage =>
        onEvent('PrintRefundPaymentReceipt', printRefundPaymentReceiptMessage),
      onReadCardDataResponse: readCardDataResponse =>
        onEvent('ReadCardDataResponse', readCardDataResponse),
      onRefundPaymentResponse: refundPaymentResponse =>
        onEvent('RefundPaymentResponse', refundPaymentResponse),
      onResetDeviceResponse: resetDeviceResponse =>
        onEvent('ResetDeviceResponse', resetDeviceResponse),
      onRetrieveDeviceStatusResponse: retrieveDeviceStatusResponse =>
        onEvent('RetrieveDeviceStatusResponse', retrieveDeviceStatusResponse),
      onRetrievePaymentResponse: retrievePaymentResponse =>
        onEvent('RetrievePaymentResponse', retrievePaymentResponse),
      onRetrievePendingPaymentsResponse: retrievePendingPaymentsResponse =>
        onEvent(
          'RetrievePendingPaymentsResponse',
          retrievePendingPaymentsResponse
        ),
      onRetrievePrintersResponse: retrievePrintersResponse =>
        onEvent('RetrievePrintersResponse', retrievePrintersResponse),
      onSaleResponse: saleResponse => onEvent('SaleResponse', saleResponse),
      onTipAdded: tipAddedMessage => onEvent('TipAdded', tipAddedMessage),
      onTipAdjustAuthResponse: tipAdjustAuthResponse =>
        onEvent('TipAdjustAuthResponse', tipAdjustAuthResponse),
      onVaultCardResponse: vaultCardResponse =>
        onEvent('VaultCardResponse', vaultCardResponse),
      onVerifySignatureRequest: verifySignatureRequest =>
        onEvent('VerifySignatureRequest', verifySignatureRequest),
      onVoidPaymentRefundResponse: voidPaymentRefundResponse =>
        onEvent('VoidPaymentRefundResponse', voidPaymentRefundResponse),
      onVoidPaymentResponse: voidPaymentResponse =>
        onEvent('VoidPaymentResponse', voidPaymentResponse),
    }
  );
  callback(null, {});
};

const initialize = (call, callback) => {
  callback(null, {});
};

const dispose = (call, callback) => {
  callback(null, {});
};

const acceptPayment = (call, callback) => {
  callback(null, {});
};

const acceptSignature = (call, callback) => {
  callback(null, {});
};

const invokeInputOption = (call, callback) => {
  callback(null, {});
};

const rejectPayment = (call, callback) => {
  callback(null, {});
};

const rejectSignature = (call, callback) => {
  callback(null, {});
};

const resetDevice = (call, callback) => {
  callback(null, {});
};

const sale = (call, callback) => {
  callback(null, {});
};

const listen = call => {
  call.write({ name: '@@INIT', type: 'null', payload: 'null' });
  new Promise(resolve => {
    endListen = () => {
      call.end();
      resolve();
    };
  });
};

module.exports = {
  Create: create,
  Initialize: initialize,
  Dispose: dispose,
  AcceptPayment: acceptPayment,
  AcceptSignature: acceptSignature,
  InvokeInputOption: invokeInputOption,
  RejectPayment: rejectPayment,
  RejectSignature: rejectSignature,
  ResetDevice: resetDevice,
  Sale: sale,
  Listen: listen,
};
