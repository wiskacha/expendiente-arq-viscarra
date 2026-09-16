namespace Ventas.ConStrategy;

public class DetalleVenta
{
    public int Id { get; set; }

    // public Producto Producto { get; set; } = null!;

    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }

    public decimal Subtotal()
    {
        return Cantidad * PrecioUnitario;
    }
}

public interface IStratDeCalculo
{
    decimal Calcular(List<DetalleVenta> detalles);
}

public class CalculoRegular : IStratDeCalculo
{
    public decimal Calcular(List<DetalleVenta> detalles)
    {
        decimal subtotal = 0;
        foreach (var detalle in detalles)
        {
            subtotal += detalle.Subtotal();
        }
        return subtotal;
    }
}

public class CalculoMayorista : IStratDeCalculo
{
    public decimal Calcular(List<DetalleVenta> detalles)
    {
        decimal subtotal = 0;
        foreach (var detalle in detalles)
        {
            subtotal += detalle.Subtotal();
        }
        return subtotal * 0.90m;
    }
}

public class CalculoConCupon : IStratDeCalculo
{
    private readonly decimal _porcentaje;
    public CalculoConCupon(decimal porcentaje) => _porcentaje = porcentaje;
    public decimal Calcular(List<DetalleVenta> detalles)
    {
        decimal subtotal = 0;
        foreach (var detalle in detalles)
        {
            subtotal += detalle.Subtotal();
        }
        return subtotal * (1 - _porcentaje);
    }
}


public class Venta
{
    public List<DetalleVenta> Detalles { get; set; } = new();
    public IStratDeCalculo Strat { get; set; } = new CalculoRegular();
    public decimal CalcularTotal() => Strat.Calcular(Detalles);
}

public static class Demo
{
    public static void Main()
    {
        var detalles = new List<DetalleVenta> { new() { Cantidad = 3, PrecioUnitario = 100m } };

        var regular = new Venta { Detalles = detalles, Strat = new CalculoRegular() };
        Console.WriteLine($"[REGULAR] Total: {regular.CalcularTotal():C}");

        var mayorista = new Venta { Detalles = detalles, Strat = new CalculoMayorista() };
        Console.WriteLine($"[MAYORISTA] Total: {mayorista.CalcularTotal():C}");

        var conCupon = new Venta { Detalles = detalles, Strat = new CalculoConCupon(0.15m) };
        Console.WriteLine($"[CUPÓN] Total: {conCupon.CalcularTotal():C}");
    }
}