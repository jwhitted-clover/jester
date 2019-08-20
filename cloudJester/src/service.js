const clover = require('remote-pay-cloud');

let connector;

const create = (call, callback) => {
  const configBuilder = new clover.WebSocketPairedCloverDeviceConfigurationBuilder();
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
  // call.end();
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
