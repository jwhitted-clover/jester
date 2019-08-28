using System;
using System.Threading.Tasks;
using Grpc.Core;

namespace MockGrpcPayDisplay
{
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
            lock (Lock)
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
