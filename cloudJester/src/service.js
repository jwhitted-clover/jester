const clover = require('remote-pay-cloud');
const CloverWebSocketInterface = require("remote-pay-cloud").CloverWebSocketInterface;
const WebSocket = require('ws');

let connector;
let mainStream;

const create = (call, callback) => {
  const onPairingCode = (pairingCode) => {
    const pairingCodeMessage = `Please enter pairing code ${pairingCode} on the device`;

    console.log(`    >  ${pairingCodeMessage}`);
  };
  const onPairingSuccess = (authTokenFromPairing) => {
    console.log(`    > Got Pairing Auth Token: ${authTokenFromPairing}`);

  };

  const configBuilder = new clover.WebSocketPairedCloverDeviceConfigurationBuilder(
  "test",
  "wss://10.247.2.250:12345/remote_pay",
  "posName",
  "serialNo",
  undefined,
  onPairingCode,
  onPairingSuccess);
  configBuilder.setWebSocketFactoryFunction((endpoint) => {
    let webSocketOverrides = {
      createWebSocket: function (endpoint) {
        // To support self-signed certificates you must pass rejectUnauthorized = false.
        // https://github.com/websockets/ws/blob/master/examples/ssl.js
        let sslOptions = {
          rejectUnauthorized: false
        };
        // Use the ws library by default.
        return new WebSocket(endpoint, sslOptions);
      }
    };

    return Object.assign(new CloverWebSocketInterface(endpoint), webSocketOverrides);
  });

  let builderConfiguration = {};
  builderConfiguration[clover.CloverConnectorFactoryBuilder.FACTORY_VERSION] = clover.CloverConnectorFactoryBuilder.VERSION_12;
  let cloverConnectorFactory = clover.CloverConnectorFactoryBuilder.createICloverConnectorFactory(builderConfiguration);
  connector = cloverConnectorFactory.createICloverConnector(configBuilder.build());
  connector.addCloverConnectorListener(buildCloverConnectionListener(call, callback));

  callback(null, {});
};

function buildCloverConnectionListener(call, callback) {

  return Object.assign({}, clover.remotepay.ICloverConnectorListener.prototype, {

    onSaleResponse: function (response) {
      console.log({message: "Sale response received"});

      mainStream.write({ name: 'SaleResponse', type: 'null', payload: response });
      if (!response.getIsSale()) {
        console.log({error: "Response is not a sale!"});
      }
    },

    // See https://docs.clover.com/build/working-with-challenges/
    onConfirmPaymentRequest: function (request) {
      console.log({message: "Automatically accepting payment"});
      mainStream.write({name: "ConfirmPaymentRequest", type: 'null', payload: request });
    },

    // See https://docs.clover.com/build/working-with-challenges/
    onVerifySignatureRequest: function (request) {
      console.log({message: "Automatically accepting signature"});
      mainStream.write({name: "VerifySignatureRequest", type: 'null', payload: request });
    },

    onDeviceActivityStart: function (cloverDeviceEvent) {
      console.log({message: "Device Ready to process requests!"});
      mainStream.write({ name: 'DeviceActivityStart', type: 'null', payload: cloverDeviceEvent });
    },

    onDeviceReady: function (merchantInfo) {
      console.log({message: "Device Ready to process requests!"});
      mainStream.write({ name: 'DeviceReady', type: 'null', payload: merchantInfo });
    },

    onDeviceError: function (cloverDeviceErrorEvent) {
    },

    onDeviceDisconnected: function () {
      console.log({message: "Disconnected"});
    }

  });

}

const initialize = (call, callback) => {
  connector.initializeConnection();
  callback(null, {});
};
const dispose = (call, callback) => {
  callback(null, {});
};

const acceptPayment = (call, callback) => {
  let payment = new clover.payments.Payment();
  payment = Object.assign(payment, JSON.parse(call.request.payload));
  connector.acceptPayment(payment);
  console.log("Accepted payment");
  callback(null, {});
};
const acceptSignature = (call, callback) => {
  let payload = JSON.parse(call.request.payload);

  let verifySignatureRequest = new clover.remotepay.VerifySignatureRequest();
  let payment = new clover.payments.Payment();
  payment = Object.assign(payment, payload.payment);
  verifySignatureRequest = Object.assign(verifySignatureRequest, payload);
  verifySignatureRequest.payment = payment;
  connector.acceptSignature(verifySignatureRequest);
  console.log("Accepted payment");
  callback(null, {});
};
const invokeInputOption = (call, callback) => {
  let inputOption = new clover.remotepay.InputOption();
  inputOption = Object.assign(inputOption, JSON.parse(call.request.payload));
  connector.invokeInputOption(inputOption);
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
  let saleRequest = new clover.remotepay.SaleRequest();
  saleRequest = Object.assign(saleRequest, JSON.parse(call.request.payload));
  connector.sale(saleRequest);
  callback(null, {});
};

const listen = call => {
  call.write({ name: '@@INIT', type: 'null', payload: 'null' });
  mainStream = call;
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
