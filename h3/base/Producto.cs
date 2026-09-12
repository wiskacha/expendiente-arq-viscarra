namespace Ventas.Base;

public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public decimal Precio { get; set; }
    public int StockActual { get; set; }
    public int StockMinimo { get; set; }
    public int IdCategoria { get; set; }

    public void ActualizarStock(int cantidad)
    {
        StockActual += cantidad;
    }

    public bool EstaBajoMinimo()
    {
        return StockActual < StockMinimo;
    }
}