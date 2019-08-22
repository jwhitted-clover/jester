const grpc = require('grpc');
const protoLoader = require('@grpc/proto-loader');

const PROTO_PATH = __dirname + '../../../proto/SdkDriver.proto';

const promisify = fn => (...args) =>
  new Promise(resolve => {
    fn(...args, (err, response) => {
      if (err) throw new Error(err);
      resolve(response);
    });
  });

module.exports = () => {
  const packageDefinition = protoLoader.loadSync(PROTO_PATH, {
    keepCase: true,
    longs: String,
    enums: String,
    defaults: String,
    oneofs: true,
  });
  const proto = grpc.loadPackageDefinition(packageDefinition).Jester;

  const GRPC_HOST = 'localhost';
  const GRPC_PORT = 12346;
  const client = new proto.SdkDriver(
    `${GRPC_HOST}:${GRPC_PORT}`,
    grpc.credentials.createInsecure()
  );

  client.Create = promisify(client.Create.bind(client));
  client.Initialize = promisify(client.Initialize.bind(client));
  client.Dispose = promisify(client.Dispose.bind(client));
  client.AcceptPayment = promisify(client.AcceptPayment.bind(client));
  client.AcceptSignature = promisify(client.AcceptSignature.bind(client));
  client.Auth = promisify(client.Auth.bind(client));
  client.CapturePreAuth = promisify(client.CapturePreAuth.bind(client));
  client.Closeout = promisify(client.Closeout.bind(client));
  client.DisplayPaymentReceiptOptions = promisify(
    client.DisplayPaymentReceiptOptions.bind(client)
  );
  client.DisplayReceiptOptions = promisify(
    client.DisplayReceiptOptions.bind(client)
  );
  client.InvokeInputOption = promisify(client.InvokeInputOption.bind(client));
  client.ManualRefund = promisify(client.ManualRefund.bind(client));
  client.OpenCashDrawer = promisify(client.OpenCashDrawer.bind(client));
  client.PreAuth = promisify(client.PreAuth.bind(client));
  client.Print = promisify(client.Print.bind(client));
  client.ReadCardData = promisify(client.ReadCardData.bind(client));
  client.RefundPayment = promisify(client.RefundPayment.bind(client));
  client.RegisterForCustomerProvidedData = promisify(
    client.RegisterForCustomerProvidedData.bind(client)
  );
  client.RejectPayment = promisify(client.RejectPayment.bind(client));
  client.RejectSignature = promisify(client.RejectSignature.bind(client));
  client.RemoveDisplayOrder = promisify(client.RemoveDisplayOrder.bind(client));
  client.ResetDevice = promisify(client.ResetDevice.bind(client));
  client.RetrieveDeviceStatus = promisify(
    client.RetrieveDeviceStatus.bind(client)
  );
  client.RetrievePayment = promisify(client.RetrievePayment.bind(client));
  client.RetrievePendingPayments = promisify(
    client.RetrievePendingPayments.bind(client)
  );
  client.RetrievePrinters = promisify(client.RetrievePrinters.bind(client));
  client.RetrievePrintJobStatus = promisify(
    client.RetrievePrintJobStatus.bind(client)
  );
  client.Sale = promisify(client.Sale.bind(client));
  client.SendMessageToActivity = promisify(
    client.SendMessageToActivity.bind(client)
  );
  client.SetCustomerInfo = promisify(client.SetCustomerInfo.bind(client));
  client.ShowDisplayOrder = promisify(client.ShowDisplayOrder.bind(client));
  client.ShowMessage = promisify(client.ShowMessage.bind(client));
  client.ShowThankYouScreen = promisify(client.ShowThankYouScreen.bind(client));
  client.ShowWelcomeScreen = promisify(client.ShowWelcomeScreen.bind(client));
  client.StartCustomActivity = promisify(
    client.StartCustomActivity.bind(client)
  );
  client.TipAdjustAuth = promisify(client.TipAdjustAuth.bind(client));
  client.VaultCard = promisify(client.VaultCard.bind(client));
  client.VoidPayment = promisify(client.VoidPayment.bind(client));
  client.VoidPaymentRefund = promisify(client.VoidPaymentRefund.bind(client));

  return client;
};
