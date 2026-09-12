namespace Ventas.Base;

public class Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string Rol { get; set; } = "";

    public Venta RegistrarVenta()
    {
        return new Venta { IdUsuario = Id, Fecha = DateTime.Now };
    }

    public void AjustarStock(Producto producto, int cantidad)
    {
        producto.ActualizarStock(cantidad);
    }
}