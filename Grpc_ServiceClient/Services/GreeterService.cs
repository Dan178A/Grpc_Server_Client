using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc_ServiceClient;

namespace Grpc_ServiceClient
{
    public class GreeterService : Greeter.GreeterBase
    {
        ulong valorInicial = 0;
        ulong valorFinal = 0;
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }


        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            ulong valorInicial;
            ulong valorFinal;

            try
            {
                if (!File.Exists("valorOficial.txt"))
                {
                    File.WriteAllText("valorOficial.txt", "0");
                }

                var textoleido = File.ReadAllText("valorOficial.txt");
                valorInicial = Convert.ToUInt64(textoleido);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer el archivo: {ex.Message}");
                return Task.FromResult(new HelloReply
                {
                    Message = "-1,-1"
                });
            }

            if (valorInicial > 8000000)
            {
                return Task.FromResult(new HelloReply
                {
                    Message = "-1,-1"
                });
            }
            else
            {
                valorFinal = valorInicial + 100000 - 1;
                try
                {
                    File.WriteAllText("valorOficial.txt", (valorFinal + 1).ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al escribir en el archivo: {ex.Message}");
                    return Task.FromResult(new HelloReply
                    {
                        Message = "-1,-1"
                    });
                }
            }

            return Task.FromResult(new HelloReply
            {
                Message = valorInicial.ToString() + " , " + valorFinal.ToString()
            });
        }
        public override Task<ResultReply> SaveResultPrimes(ResultRequest request, ServerCallContext context)
        {
            File.AppendAllText("ResultSave.txt", request.Info + Environment.NewLine);  
            return Task.FromResult(new ResultReply
            {
                Info = "Guardado" + DateTime.Now.ToString()
            });
        }
    }

}
