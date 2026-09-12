namespace Ventas.Modelo;

public class Venta
{
    public int Id { get; set; }
    public int NumeroTransaccion { get; set; }
    public DateTime Fecha { get; set; }
    public string Estado { get; set; } = "Pendiente";
    public int IdUsuario { get; set; }
    public List<DetalleDeVenta> Detalles { get; set; } = new();

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
}