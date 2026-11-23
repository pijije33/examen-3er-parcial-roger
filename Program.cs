using System;
using System.Globalization;

class Program
{
    static void Main()
    {
        double monto = PedirPositivo("Monto del préstamo: ");
        double tasaAnual = PedirPositivo("Tasa de interés anual (%): ");
        int años = (int)PedirPositivo("Número de años: ");

        double mensual = CalcularPagoMensual(monto, tasaAnual, años);
        double total = CalcularPagoTotal(mensual, años);

        Console.WriteLine($"\nPago mensual: {mensual:C2}");
        Console.WriteLine($"Pago total: {total:C2}");
    }

    static double PedirPositivo(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);
            try
            {
                string entrada = (Console.ReadLine() ?? "").Trim().Replace(',', '.');
                double valor = double.Parse(entrada, CultureInfo.InvariantCulture);

                if (valor > 0)
                    return valor;

                Console.WriteLine("Error: el número debe ser mayor que 0.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: ingresa un número válido.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Error: el número es demasiado grande.");
            }
        }
    }

    static double CalcularPagoMensual(double monto, double tasaAnual, int años)
    {
        int numeroPagos = años * 12;
        double tasaMensual = tasaAnual / 1200.0;

        if (tasaMensual == 0)
            return monto / numeroPagos;

        return monto * tasaMensual * Math.Pow(1 + tasaMensual, numeroPagos)
               / (Math.Pow(1 + tasaMensual, numeroPagos) - 1);
    }

    static double CalcularPagoTotal(double pagoMensual, int años)
    {
        return pagoMensual * años * 12;
    }
}