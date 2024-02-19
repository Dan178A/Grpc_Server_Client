# Proyecto: Cliente gRPC para búsqueda de números primos
Este proyecto es un cliente gRPC que se comunica con un servidor para obtener rangos de números y buscar números primos dentro de esos rangos.

# Cómo funciona
El cliente se conecta a un servidor gRPC en https://localhost:5001 y solicita rangos de números. Luego, busca números primos dentro de esos rangos y guarda los resultados. Cuando el servidor ya no tiene más rangos para proporcionar, el cliente envía los resultados de vuelta al servidor.

Estructura del código
El código principal se encuentra en el archivo Program.cs. Aquí hay una descripción de las funciones más importantes:

* Main: Esta es la función principal del programa. Se conecta al servidor gRPC, solicita rangos de números, busca números primos y envía los resultados al servidor.

* ServerCall: Esta función solicita un rango de números al servidor. Si el servidor devuelve "-1,-1", significa que no hay más rangos disponibles y la función devuelve false. De lo contrario, busca números primos en el rango proporcionado y devuelve true.

* ServerResult: Esta función envía los resultados de la búsqueda de números primos al servidor.

* BuscarPrimos: Esta función busca números primos en un rango dado. Utiliza la función EsPrimo para verificar si cada número en el rango es primo.

* EsPrimo: Esta función verifica si un número dado es primo.

# Cómo usar
Asegúrate de que el servidor gRPC esté en ejecución y escuchando en https://localhost:5001.

> Ejecuta este programa. Se conectará al servidor y comenzará a solicitar rangos de números.
> El programa buscará números primos en los rangos proporcionados y enviará los resultados al servidor.
> Cuando el servidor ya no tenga más rangos para proporcionar, el programa terminará.

# Requisitos
.NET Core 3.1 o superior
Un servidor gRPC en ejecución en https://localhost:5001
