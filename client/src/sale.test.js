const createDriver = require('./client');
const externalId = require('./externalId');

describe('Sale', () => {
  it('should', async () => {
    jest.setTimeout(120000);

    const driver = createDriver();

    const onEvent = driver.Listen({});

    const waitForEvent = eventName =>
      new Promise(resolve => {
        console.log("waiting for " + eventName);
        const test =
          eventName instanceof Function
            ? eventName
            : ({ name }) => name === eventName;
        const handler = ({ type, name, payload }) => {
          const evt = { type, name, payload: JSON.parse(payload) };
          if (test(evt)) {
            console.log("got it! " + eventName);
            onEvent.off('data', handler);
            resolve(evt);
          }
        };
        onEvent.on('data', handler);
      });

    await waitForEvent('@@INIT');

    await driver.Create({});

    const deviceReady = waitForEvent('DeviceReady');
    await driver.Initialize({});
    await deviceReady;

    await driver.ResetDevice({});

    const acceptPayment = new Promise(async resolve => {
      const { payload } = await waitForEvent('ConfirmPaymentRequest');
      await driver.AcceptPayment({ payload: JSON.stringify(payload.payment) });
      resolve(payload);
    });

    const acceptSignature = new Promise(async resolve => {
      const { payload } = await waitForEvent('VerifySignatureRequest');
      await driver.AcceptSignature({
        payload: JSON.stringify(payload),
      });
      resolve(payload);
    });

    const noReceipt = new Promise(async resolve => {
      const receiptOptions = ({ name, payload }) =>
        name === 'DeviceActivityStart' &&
        payload.eventState === 'RECEIPT_OPTIONS';
      await waitForEvent(receiptOptions);
      const inputOption = { keyPress: 'BUTTON_1', description: 'No receipt' };
      driver.InvokeInputOption({ payload: JSON.stringify(inputOption) });
    });

    const saleResponse = new Promise(async resolve => {
      const { payload } = await waitForEvent('SaleResponse');
      resolve(payload);
    });

    const saleRequest = {
      externalId: externalId(),
      amount: 200,
    };
    await driver.Sale({ payload: JSON.stringify(saleRequest) });

    const sale = await saleResponse;

    expect(sale.success).toBe(true);

    await driver.Dispose({});
  });
});
