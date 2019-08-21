using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clover.Grpc;
using Grpc.Core;

namespace ConsoleApp1
{
    public class PayDisplayImpl : PayDisplay.PayDisplayBase
    {
        public override Task<Empty> Print(PrintRequest request, ServerCallContext context)
        {
            return base.Print(request, context);
        }
    }
}
