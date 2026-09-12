namespace Ventas.Modelo;

public class DetalleVenta
{
    public int Id { get; set; }
    public Producto Producto { get; set; } = null!;
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }

    public decimal Subtotal()
    {
        return Cantidad * PrecioUnitario;
    }
}