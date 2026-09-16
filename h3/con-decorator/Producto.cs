using System.Reflection.Metadata;

namespace Ventas.ConDecorator;

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
public interface ICalculoDePrecio
{
    decimal Calcular();
}

public class PrecioBase : ICalculoDePrecio
{
    private readonly Producto _producto;
    public PrecioBase(Producto producto) => _producto = producto;

    public decimal Calcular() => _producto.Precio;
}

public class DescuentoPorMayoreo : ICalculoDePrecio
{
    private readonly ICalculoDePrecio _base;
    public DescuentoPorMayoreo(ICalculoDePrecio calculo) => _base = calculo;

    public decimal Calcular() => _base.Calcular() * 0.90m;
}

public class RecargoPorAlcohol : ICalculoDePrecio
{
    private readonly ICalculoDePrecio _base;
    private readonly decimal _recargoFijo;
    public RecargoPorAlcohol(ICalculoDePrecio calculo, decimal recargoFijo)
    {
        _base = calculo;
        _recargoFijo = recargoFijo;
    }
    public decimal Calcular() => _base.Calcular() + _recargoFijo;
}

public static class Demo
{
    public static void Main()
    {
        var producto = new Producto { Nombre = "Paceña", Precio = 100m };

        ICalculoDePrecio precioRegular = new PrecioBase(producto);
        System.Console.WriteLine($"[SIN DESCUENTOS] Precio : {precioRegular.Calcular():C}");

        ICalculoDePrecio soloDescuento = new DescuentoPorMayoreo(new PrecioBase(producto));
        System.Console.WriteLine($"[SOLO DESCUENTO] Precio: {soloDescuento.Calcular():C}");

        ICalculoDePrecio descuentoYRecargo = new DescuentoPorMayoreo(new RecargoPorAlcohol(new PrecioBase(producto), 5m));
        System.Console.WriteLine($"[DESCUENTO + RECARGO] Precio: {descuentoYRecargo.Calcular():C}");

    }
}

