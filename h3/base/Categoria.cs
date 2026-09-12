namespace Ventas.Modelo;

public class Categoria
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    private readonly List<Producto> _productos = new();

    public void Agregar(Producto producto)
    {
        _productos.Add(producto);
    }

    public List<Producto> ListarProductos()
    {
        return _productos;
    }
}