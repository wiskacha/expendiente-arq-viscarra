namespace Ventas.ConBuilder;

public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; init; } = "";
    public decimal Precio { get; init; }
    public int StockActual { get; set; }
    public int StockMinimo { get; init; }
    public int IdCategoria { get; init; }
    public string UnidadDeMedida { get; init; } = "";

    public void Describir()
    {
        Console.WriteLine($"[PRODUCTO] {Nombre} — {Precio:C} — stock: {StockActual}/{StockMinimo} " + $"({UnidadDeMedida}) — categoría {IdCategoria}");
    }

    public void ActualizarStock(int cantidad)
    {
        StockActual += cantidad;
    }

    public bool EstaBajoMinimo()
    {
        return StockActual < StockMinimo;
    }
}

public class ArmadorDeProducto
{
    private int _id;
    private string _nombre = "";
    private decimal _precio;
    private int _stockActual;
    private int _stockMinimo;
    private int _idCategoria;
    private string _unidadDeMedida = "unidad";

    public ArmadorDeProducto ConId(int id) { _id = id; return this; }
    public ArmadorDeProducto ConNombre(string nombre) { _nombre = nombre; return this; }
    public ArmadorDeProducto ConPrecio(decimal precio) { _precio = precio; return this; }
    public ArmadorDeProducto ConStockActual(int cantidad) { _stockActual = cantidad; return this; }
    public ArmadorDeProducto ConStockMinimo(int cantidad) { _stockMinimo = cantidad; return this; }
    public ArmadorDeProducto ConCategoria(int idCategoria) { _idCategoria = idCategoria; return this; }
    public ArmadorDeProducto ConUnidadDeMedida(string unidad) { _unidadDeMedida = unidad; return this; }

    public Producto Registrar()
    {
        if (_id <= 0) throw new InvalidOperationException("Falta el id del producto.");
        if (string.IsNullOrWhiteSpace(_nombre)) throw new InvalidOperationException("Falta el nombre del producto.");
        if (_precio <= 0) throw new InvalidOperationException("El precio debe ser mayor a cero.");
        if (_stockMinimo <= 0) throw new InvalidOperationException("Falta el stock mínimo del producto.");
        if (_idCategoria <= 0) throw new InvalidOperationException("Falta asignar una categoría.");
        if (string.IsNullOrWhiteSpace(_unidadDeMedida)) throw new InvalidOperationException("Falta definir la unidad de medida.");

        return new Producto
        {
            Id = _id,
            Nombre = _nombre,
            Precio = _precio,
            StockActual = _stockActual,
            StockMinimo = _stockMinimo,
            IdCategoria = _idCategoria,
            UnidadDeMedida = _unidadDeMedida
        };
    }
}

public static class Demo
{
    public static void Main()
    {
        var producto = new ArmadorDeProducto()
            .ConId(5)
            .ConNombre("Cuaderno A4")
            .ConPrecio(15.50m)
            .ConStockActual(50)
            .ConStockMinimo(10)
            .ConCategoria(3)
            .ConUnidadDeMedida("unidad")
            .Registrar();
        producto.Describir();

        try
        {
            new ArmadorDeProducto().ConId(4).ConStockActual(20).Registrar();
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine($"[CATCH] 🛑 {e.Message}");
        }
    }
}