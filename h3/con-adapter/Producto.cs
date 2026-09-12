namespace Ventas.ConAdapter;

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

public class CatalogoProveedorA
{
    public string NombreProducto(string sku) => sku == "SKU-01" ? "Coca Cola" : "Fanta";
    public decimal PrecioEnDecimales(string sku) => sku == "SKU-01" ? 19.5m : 18.5m;
    public string CodigoCategoria(string sku) => "REFRE";
}

public class CatalogoProveedorB
{
    public string NombreProducto(string codigo) => codigo == "CBN-43" ? "Pepsi" : "Paceña";
    public string PrecioEnTexto(string codigo) => codigo == "CBN-43" ? "16,40" : "12,60";
    public string CodigoCategoria(string sku) => "Bebidas";
}

public interface IProductoSolicitable
{
    Producto Solicitar(string codigo);
}

public class AdaptadorProveedorA : IProductoSolicitable
{
    private readonly CatalogoProveedorA _catalogo = new();

    public Producto Solicitar(string sku) => new Producto
    {
        Nombre = _catalogo.NombreProducto(sku),
        Precio = _catalogo.PrecioEnDecimales(sku),
        IdCategoria = _catalogo.CodigoCategoria(sku) == "REFRE" ? 1 : 0
    };
}

public class AdaptadorProveedorB : IProductoSolicitable
{
    private readonly CatalogoProveedorB _catalogo = new();

    public Producto Solicitar(string codigo) => new Producto
    {
        Nombre = _catalogo.NombreProducto(codigo),
        Precio = decimal.Parse(_catalogo.PrecioEnTexto(codigo).Replace(",", ".")),
        IdCategoria = _catalogo.CodigoCategoria(codigo) == "Bebidas" ? 1 : 0
    };
}

public class ModuloDePedido
{
    private readonly IProductoSolicitable _adaptador;
    public ModuloDePedido(IProductoSolicitable adaptador) => _adaptador = adaptador;

    public void SolicitarYMostrar(string codigo)
    {
        var producto = _adaptador.Solicitar(codigo);
        Console.WriteLine($"[PEDIDO] {producto.Nombre} — {producto.Precio:C} — categoría {producto.IdCategoria}");
    }
}

public static class Demo
{
    public static void Main()
    {
        var desdeA = new ModuloDePedido(new AdaptadorProveedorA());
        desdeA.SolicitarYMostrar("SKU-01");

        var desdeB = new ModuloDePedido(new AdaptadorProveedorB());
        desdeB.SolicitarYMostrar("CBN-43");
    }
}