using Ventas.Base;

namespace Ventas.ConFactory;

public class Venta
{
    public int Id { get; set; }
    public int NumeroTransaccion { get; set; }
    public DateTime Fecha { get; set; }
    public string Estado { get; set; } = "Pendiente";
    public int IdUsuario { get; set; }
    public List<DetalleVenta> Detalles { get; set; } = new();
    public string MedioDePago { get; set; } = "";


    public void Confirmar() => Estado = "Confirmada";
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

    public void Pagar(string medio, decimal monto)
    {
        FabricaDeMediosDePago.Crear(medio).Cobrar(monto);
        MedioDePago = medio;
        MarcarPagada();
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
        venta1.Pagar("efectivo", 150m);

        venta2.Confirmar();
        venta2.Pagar("tarjeta", 300m);

        venta3.Confirmar();
        venta3.Pagar("qr", 220m);

        Console.WriteLine($"Venta {venta1.Id} — Estado: {venta1.Estado} — Medio: {venta1.MedioDePago}");
        Console.WriteLine($"Venta {venta2.Id} — Estado: {venta2.Estado} — Medio: {venta2.MedioDePago}");
        Console.WriteLine($"Venta {venta3.Id} — Estado: {venta3.Estado} — Medio: {venta3.MedioDePago}");
    }
}
