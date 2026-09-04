public interface IDescuento
{
    decimal Aplicar(decimal total);
}

public interface IRepositorioDeVentas
{
    void Guardar(Venta venta, decimal totalFinal);
}

public interface INotificadorDeStock
{
    void AvisarStockBajo(string producto, int stockActual);
}

public class Producto
{
    public string Nombre { get; }
    public decimal Precio { get; }
    public int Stock { get; private set; }
    public int StockMinimo { get; }

    public Producto(string nombre, decimal precio, int stock, int stockMinimo)
    {
        Nombre = nombre;
        Precio = precio;
        Stock = stock;
        StockMinimo = stockMinimo;
    }

    public void Descontar(int cantidad)
    {
        Stock -= cantidad;
    }

    public bool EstaBajoMinimo()
    {
        return Stock <= StockMinimo;
    }
}

public class Venta
{
    public string Cliente { get; }
    public List<(Producto producto, int cantidad)> Detalles { get; } = new();
    public Venta(string cliente)
    {
        Cliente = cliente;
    }
    public decimal TotalBruto()
    {
        decimal total = 0;
        foreach (var (producto, cantidad) in Detalles)
        {
            total += producto.Precio * cantidad;
        }
        return total;
    }
}

public class SinDescuento : IDescuento
{
    public decimal Aplicar(decimal total)
    {
        return total;
    }
}

public class DescuentoConvenio : IDescuento
{
    public decimal Aplicar(decimal total)
    {
        return total * 0.90m;
    }
}

public class RepositorioEnConsola : IRepositorioDeVentas
{
    public void Guardar(Venta venta, decimal totalFinal)
    {
        System.Console.WriteLine($"[BD] Venta de {venta.Cliente} por {totalFinal:0.00} Bs guardada");
    }
}

public class AvisoAlEncargado : INotificadorDeStock
{
    public void AvisarStockBajo(string producto, int stockActual)
    {
        System.Console.WriteLine($"[AVISO] {producto} quedó con stock {stockActual}: ¡reponer!");
    }
}

public class ServicioDeVentas
{
    private readonly IRepositorioDeVentas _repositorio;
    private readonly INotificadorDeStock _notificador; 

    public ServicioDeVentas(IRepositorioDeVentas repositorio, INotificadorDeStock notificador)
    {
        _repositorio = repositorio;
        _notificador = notificador;
    }

    public void Registrar(Venta venta, IDescuento descuento)
    {
        decimal totalFinal = descuento.Aplicar(venta.TotalBruto());

        _repositorio.Guardar(venta, totalFinal);

        foreach(var (producto, cantidad) in venta.Detalles)
        {
            producto.Descontar(cantidad);
            if (producto.EstaBajoMinimo())
            {
                _notificador.AvisarStockBajo(producto.Nombre, producto.Stock);
            }
        }
    }
}

public static class Demo
{
    public static void Main()
    {
        var cuaderno = new Producto("Cuaderno", 12.50m, stock: 6, stockMinimo: 5);
        var venta = new Venta("Noelia");
        venta.Detalles.Add((cuaderno, 2));

        var servicio = new ServicioDeVentas(new RepositorioEnConsola(), new AvisoAlEncargado());
        servicio.Registrar(venta, new DescuentoConvenio());
    }
}