namespace Ventas.ConObserver;

public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public decimal Precio { get; set; }
    public int StockActual { get; set; }
    public int StockMinimo { get; set; }
    public int IdCategoria { get; set; }

    private readonly List<IObservadorDeStock> _observadores = new();

    public void Suscribir(IObservadorDeStock observador) => _observadores.Add(observador);

    public void ActualizarStock(int cantidad)
    {
        StockActual += cantidad;
        System.Console.WriteLine($"El stock actual del producto {Nombre} es de: {StockActual}");
        if (EstaBajoMinimo())
            foreach (var observador in _observadores)
                observador.Notificar(Nombre);
    }
    public bool EstaBajoMinimo() => StockActual < StockMinimo;
}

public interface IObservadorDeStock
{
    void Notificar(string nombreProducto);
}

public class ModuloDeCompras : IObservadorDeStock
{
    public void Notificar(string nombreProducto)
        => System.Console.WriteLine($"[COMPRAS] Orden de reposición para {nombreProducto}");
}

public class PanelDeAdministracion : IObservadorDeStock
{
    public void Notificar(string nombreProducto)
        => System.Console.WriteLine($"[PANEL] Alerta de stock bajo en: {nombreProducto}");
}

public class AvisoWhatsapp : IObservadorDeStock
{
    public void Notificar(string nombreProducto)
        => System.Console.WriteLine($"[Whatsapp] To: Gerente : el producto {nombreProducto} está en stock mínimo");
}


public static class Demo
{
    public static void Main()
    {
        var producto = new Producto { Nombre = "Cuaderno A6", StockActual = 15, StockMinimo = 10 };

        producto.Suscribir(new ModuloDeCompras());
        producto.Suscribir(new PanelDeAdministracion());
        producto.Suscribir(new AvisoWhatsapp());

        producto.ActualizarStock(-8);

    }
}