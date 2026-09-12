using Ventas.Base;

namespace Ventas.ConSingleton;

public class Venta
{
    public int Id { get; set; }
    public int NumeroTransaccion { get; set; }
    public DateTime Fecha { get; set; }
    public string Estado { get; set; } = "Pendiente";
    public int IdUsuario { get; set; }
    public List<DetalleVenta> Detalles { get; set; } = new();

    public void Confirmar()
    {
        Estado = "Confirmada";
        NumeroTransaccion = NumeradorDeTransaccion.Instancia.SiguienteNumero();
    }
    public void MarcarPagada() => Estado = "Pagada";
    public void MarcarEntregada() => Estado = "Entregada";
    public void Anular() => Estado = "Anulada";

    public decimal CalcularTotal()
    {
        decimal total = 0;
        foreach (var detalle in Detalles)
            total += detalle.Subtotal();
        return total;
    }
}

public static class Demo
{
    public static void Main()
    {
        var venta1 = new Venta { Id = 1, IdUsuario = 10 };
        var venta2 = new Venta { Id = 2, IdUsuario = 20 };
        var venta3 = new Venta { Id = 3, IdUsuario = 10 };

        venta1.Confirmar();
        venta2.Confirmar();
        venta3.Confirmar();

        Console.WriteLine($"Venta {venta1.Id} — N° Transacción: {venta1.NumeroTransaccion}");
        Console.WriteLine($"Venta {venta2.Id} — N° Transacción: {venta2.NumeroTransaccion}");
        Console.WriteLine($"Venta {venta3.Id} — N° Transacción: {venta3.NumeroTransaccion}");

        Console.WriteLine("Misma instancia del numerador para las tres ventas: sin duplicados.");
    }
}