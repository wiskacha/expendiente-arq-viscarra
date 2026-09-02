using System.IO.Compression;

namespace Srp1.Fixed;

public class DetalleDeVenta
{
    public string Producto { get; }
    public int Cantidad { get; }
    public decimal PrecioUnitario { get; }

    public DetalleDeVenta(string producto, int cantidad, decimal precioUnitario)
    {
        Producto = producto;
        Cantidad = cantidad;
        PrecioUnitario = precioUnitario;
    }

    public decimal Subtotal()
    {
        return Cantidad * PrecioUnitario;
    }
}

public class Venta
{
    public string Cliente { get; }
    public List<DetalleDeVenta> Detalles { get; } = new();

    public Venta(string cliente)
    {
        Cliente = cliente;
    }
}

//Responsabilidad 1:
public class CalculadoraDeTotales
{
    public decimal Calcular(Venta venta)
    {
        decimal total = 0;
        foreach (var detalle in venta.Detalles)
        {
            total += detalle.Subtotal();
        }
        return total;
    }
}

//Responsabilidad 2 PERSISTENCIA
public class RepositorioDeVentas
{
    public void Guardar(Venta venta, decimal total)
    {
        Console.WriteLine($"[BD] INSERT INTO ventas (cliente, total) VALUES ('{venta.Cliente}', {total})");
    }
}

//Responsabilidad 3 PRESENTACIÓN
public class ImpresoraDeTickets
{
    public void Imprimir(Venta venta, decimal total)
    {
        Console.WriteLine("------TICKET------");
        foreach (var detalle in venta.Detalles)
        {
            Console.WriteLine($"{detalle.Cantidad} x {detalle.Producto} {detalle.Subtotal():0.00} Bs");
        }
        Console.WriteLine($"TOTAL: {total:0.00} Bs");
    }
}

//Responsabilidad 4 COMUNICACIONES
public class NotificadorDeVentas
{
    public void Notificar(Venta venta)
    {
        Console.WriteLine($"[CORREO] Enviando comprobante a {venta.Cliente}...");
    }
}

//Nuevo Gestor
public class GestorDeVentas
{
    private readonly CalculadoraDeTotales _calculadora = new();
    private readonly RepositorioDeVentas _repositorio = new();
    private readonly ImpresoraDeTickets _impresora = new();
    private readonly NotificadorDeVentas _notificador = new();

    public void RegistrarVenta(Venta venta)
    {
        decimal total = _calculadora.Calcular(venta);
        _repositorio.Guardar(venta, total);
        _impresora.Imprimir(venta, total);
        _notificador.Notificar(venta);
    }
}

public static class Demo
{
    public static void Main()
    {
        Correr();
    }
    public static void Correr()
    {
        var gestor = new GestorDeVentas();
        var cliente = "Noelia";
        var venta = new Venta(cliente);
        venta.Detalles.Add(new DetalleDeVenta("Cuaderno", 3, 12.50m));
        venta.Detalles.Add(new DetalleDeVenta("Bolígrafo", 5, 2.00m));

        gestor.RegistrarVenta(venta);
    }
}

