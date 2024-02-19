using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Grpc_Client;


namespace Grpc_Client
{
    internal class Program
    {
        static Greeter.GreeterClient client;
        static List<string> ListaResult = new List<string>();
        static async Task Main(string[] args)
        {
            Console.WriteLine("Presione cualquier tecla para comenzar con este cliente");
            Console.ReadLine();
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            client = new Greeter.GreeterClient(channel);

            while (true)
            {
                bool result = await ServerCall();
                if (!result) { break; }

            }
            Console.WriteLine("Cuando Termine el Servidor, Presione Cualquier Tecla para Enviar Los Resultados...");
            Console.ReadLine();

            for (int i = 0; i < ListaResult.Count; i++)
            {
                await ServerResult(i);
            }
            Console.WriteLine("Presione cualquier tecla para salir...");
            Console.ReadLine();
        }
        static async Task<bool> ServerCall()
        {
            var reply = await client.SayHelloAsync(new HelloRequest { Name = "" });
            if (reply.Message == "-1,-1")
            {

                return false;
            }
            var valores = reply.Message.Split(",");
            ulong valorInicio = Convert.ToUInt64(valores[0]);
            ulong valorFin = Convert.ToUInt64(valores[1]);
            Console.WriteLine($"Trabajando el cliente con el rango de valores: Inicio {valorInicio}; Fin {valorFin}");
            await BuscarPrimos(valorInicio, valorFin);
            return true;
        }
        static async Task ServerResult(int pos)
        {
            var reply = await client.SaveResultPrimesAsync(new ResultRequest { Info = ListaResult[pos] });
            Console.WriteLine(reply.Info);
        }
        static async Task BuscarPrimos(ulong valorInicio, ulong valorFin)
        {
            await Task.Yield();
            ulong contadorPrimos = 0;
            Console.WriteLine("Buscando números primos entre " + valorInicio + " y " + valorFin);
            DateTime inicio = DateTime.Now;
            for (ulong valorActual = valorInicio; valorActual <= valorFin; valorActual++)
            {
                if (EsPrimo(valorActual)) // Aquí es donde se hizo el cambio
                {
                    contadorPrimos++;
                }
            }
            DateTime fin = DateTime.Now;
            TimeSpan tiempoInvertido = fin.Subtract(inicio);
            ListaResult.Add($"{valorInicio},{valorFin},{contadorPrimos}");
            Console.WriteLine("");
            Console.WriteLine($"Respuesta Final Entre {valorInicio} y {valorFin} hay {contadorPrimos} números primos");
            Console.WriteLine($"Inicio El {inicio.ToString()}, Termino el {fin.ToString()}, Y Tardo: {tiempoInvertido}");
            Console.WriteLine("Fin de la busqueda de números primos...");
            Console.WriteLine("*******************");
            Console.WriteLine("");
        }


        static bool EsPrimo(ulong valor)
        {
            if (valor <= 1) return false;
            if (valor == 2) return true;
            if (valor % 2 == 0) return false;

            ulong limite = (ulong)Math.Sqrt(valor);
            for (ulong i = 3; i <= limite; i += 2)
            {
                if (valor % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }

}
