# Proyecto: Cliente & Servidor gRPC para búsqueda de números primos
Este proyecto es un cliente gRPC que se comunica con un servidor para obtener rangos de números y buscar números primos dentro de esos rangos.
Server: Este proyecto es un servidor gRPC que proporciona rangos de números a los clientes para buscar números primos y recibe los resultados de la búsqueda.

# Cómo funciona


> El `Cliente` se conecta a un servidor gRPC en https://localhost:5001 y solicita rangos de números. Luego, busca números primos dentro de esos rangos y guarda los resultados. Cuando el servidor ya no tiene más rangos para proporcionar, el cliente envía los resultados de
> vuelta al servidor.
---
> El `Servidor` escucha en https://localhost:5001 y espera solicitudes de los clientes. Cuando recibe una solicitud, proporciona un rango de números al cliente. Cuando el cliente ha terminado de buscar números primos en ese rango, envía los resultados al servidor, que los
> guarda en un archivo.

# Estructura del código


`Client`

El código principal se encuentra en el archivo Program.cs. Aquí hay una descripción de las funciones más importantes:

* `Main`: Esta es la función principal del programa. Se conecta al servidor gRPC, solicita rangos de números, busca números primos y envía los resultados al servidor.

* `ServerCall`: Esta función solicita un rango de números al servidor. Si el servidor devuelve "-1,-1", significa que no hay más rangos disponibles y la función devuelve false. De lo contrario, busca números primos en el rango proporcionado y devuelve true.

* `ServerResult`: Esta función envía los resultados de la búsqueda de números primos al servidor.

* `BuscarPrimos`: Esta función busca números primos en un rango dado. Utiliza la función EsPrimo para verificar si cada número en el rango es primo.

* `EsPrimo`: Esta función verifica si un número dado es primo.

`Server`

El código principal se encuentra en el archivo GreeterService.cs. Aquí hay una descripción de las funciones más importantes:

* `SayHello`: Esta función es llamada por los clientes para solicitar un rango de números. Si no hay más rangos disponibles, devuelve "-1,-1". De lo contrario, devuelve el rango en el formato "valorInicial,valorFinal".

* `SaveResultPrimes`: Esta función es llamada por los clientes para enviar los resultados de la búsqueda de números primos. Guarda los resultados en el archivo "ResultSave.txt".

# Cómo usar
`Client:`

Asegúrate de que el servidor gRPC esté en ejecución y escuchando en https://localhost:5001.


> Ejecuta este programa. Se conectará al servidor y comenzará a solicitar rangos de números.
> 
> El programa buscará números primos en los rangos proporcionados y enviará los resultados al servidor.
> 
> Cuando el servidor ya no tenga más rangos para proporcionar, el programa terminará.

`Server:`

Ejecuta este programa. Comenzará a escuchar en https://localhost:5001.

> Los clientes pueden conectarse al servidor y solicitar rangos de números.
>
> Cuando un cliente ha terminado de buscar números primos en un rango, enviará los resultados al servidor.
>
> El servidor guardará los resultados en el archivo "ResultSave.txt".

# Requisitos
.NET Core 3.1 o superior
Un servidor gRPC en ejecución en https://localhost:5001
