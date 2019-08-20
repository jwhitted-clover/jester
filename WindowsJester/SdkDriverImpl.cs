using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using Clover.RemotePay;
using sdk = com.clover.remotepay.sdk;
using Grpc.Core;
using Jester;
using payments = com.clover.sdk.v3.payments;
using transport = com.clover.remotepay.transport;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace WindowsJester
{
    public class SdkDriverImpl : SdkDriver.SdkDriverBase
    {
        public TaskCompletionSource<int> EventPromise { get; set; }
        public IServerStreamWriter<Event> EventResponseStream { get; set; }
        private Task EventWriteTask { get; set; } = Task.FromResult(0);
        private readonly object EventLock = new object();

        public override Task<Empty> AcceptPayment(Request request, ServerCallContext context)
        {
            Program.WriteLine("SdkDriverImpl.AcceptPayment", request);
            var payment = JsonConvert.DeserializeObject<payments.Payment>(request.Payload);
            Program.Connector.AcceptPayment(payment);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> AcceptSignature(Request request, ServerCallContext context)
        {
            Program.WriteLine("SdkDriverImpl.AcceptSignature", request);
            var signature = JsonConvert.DeserializeObject<sdk.VerifySignatureRequest>(request.Payload);
            Program.Connector.AcceptSignature(signature);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> Create(CreateRequest request, ServerCallContext context)
        {
            Program.WriteLine("SdkDriverImpl.Create", request);

            var listener = new Listener();
            listener.Event += (s, e) => OnEvent(e);

            var connector = sdk.CloverConnectorFactory.CreateUsbConnector("RAID", "POS", "Register1", false);
            connector.AddCloverConnectorListener(listener);

            Program.Connector = connector;
            Program.Listener = listener;

            return Task.FromResult(new Empty());
        }

        public override Task<Empty> Dispose(DisposeRequest request, ServerCallContext context)
        {
            Program.WriteLine("SdkDriverImpl.Dispose", request, ConsoleColor.DarkRed);

            EventPromise?.TrySetResult(0);
            EventPromise = null;
            EventResponseStream = null;

            Program.Connector.RemoveCloverConnectorListener(Program.Listener);
            Program.Connector.Dispose();
            Program.Connector = null;
            Program.Listener = null;

            return Task.FromResult(new Empty());
        }

        public override Task<Empty> Initialize(InitializeRequest request, ServerCallContext context)
        {
            Program.WriteLine("SdkDriverImpl.Initialize", request);
            Program.Connector.InitializeConnection();
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> InvokeInputOption(Request request, ServerCallContext context)
        {
            var option = JsonConvert.DeserializeObject<transport.InputOption>(request.Payload);
            Program.Connector.InvokeInputOption(option);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> RejectPayment(RejectPaymentRequest request, ServerCallContext context)
        {
            Program.WriteLine("SdkDriverImpl.RejectPayment", request);
            var payment = JsonConvert.DeserializeObject<payments.Payment>(request.Payment);
            var challenge = JsonConvert.DeserializeObject<transport.Challenge>(request.Challenge);
            Program.Connector.RejectPayment(payment, challenge);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> RejectSignature(Request request, ServerCallContext context)
        {
            Program.WriteLine("SdkDriverImpl.RejectSignature", request);
            var signature = JsonConvert.DeserializeObject<sdk.VerifySignatureRequest>(request.Payload);
            Program.Connector.RejectSignature(signature);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> ResetDevice(Empty request, ServerCallContext context)
        {
            Program.WriteLine("SdkDriverImpl.ResetDevice");
            Program.Connector.ResetDevice();
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> Sale(Request request, ServerCallContext context)
        {
            Program.WriteLine("SdkDriverImpl.Sale", request);
            var sale = JsonConvert.DeserializeObject<sdk.SaleRequest>(request.Payload);
            Program.Connector.Sale(sale);
            return Task.FromResult(new Empty());
        }

        public override Task Listen(Empty request, IServerStreamWriter<Event> responseStream, ServerCallContext context)
        {
            Program.WriteLine("SdkDriverImpl.Listen", request);
            EventPromise = new TaskCompletionSource<int>();
            EventResponseStream = responseStream;
            responseStream.WriteAsync(new Event { Name = "@@INIT", Type = "null", Payload = "null" });
            return EventPromise.Task;
            // return Task.FromResult(0);
        }

        private void OnEvent(Event @event)
        {
            lock (EventLock)
            {
                var oldTask = EventWriteTask;
                EventWriteTask = Task.Run(() =>
                {
                    oldTask.Wait();
                    EventResponseStream?.WriteAsync(@event).Wait();
                });
            }
        }
    }
}
