// PARCIAL 1 · VARIANTE B — Ferretería "El Tornillo"
// Sistema de pedidos de materiales. El código FUNCIONA, pero su diseño tiene
// 4 violaciones de principios SOLID. Tu trabajo: encontrarlas y curar dos.

namespace Parcial1.Ferreteria;


// Sección descuentos SERGIO VISCARRA
public interface IDescuento
{
    string Nombre { get; }
    decimal Calcular(decimal total);
}

public class DescuentoClienteParticular : IDescuento
{
    public string Nombre => "particular";
    public decimal Calcular(decimal total) => 0;
}

public class DescuentoClienteContratista : IDescuento
{
    public string Nombre => "contratista";
    public decimal Calcular(decimal total) => total * 0.15m;
}

public class DescuentoClienteConstructora : IDescuento
{
    public string Nombre => "constructora";
    public decimal Calcular(decimal total) => total * 0.25m;
}

public class CalculadoraDeDescuento
{
    public decimal Calcular(IDescuento descuento, decimal total)
    {
        return descuento.Calcular(total);
    }
}
// fin sección Descuentos

// herramientas: Impresora SERGIO VISCARRA
public class ImpresoraDeComprobante
{
    public void Imprimir(string cliente, string tipoCliente, string material, int cantidad, decimal totalFinal)
    {
        Console.WriteLine("----- COMPROBANTE -----");
        Console.WriteLine($"{cantidad} x {material}");
        Console.WriteLine($"Cliente: {cliente} ({tipoCliente})");
        Console.WriteLine($"TOTAL: {totalFinal:0.00} Bs");
    }
}

//herramientas: calculadoraTotales SERGIO VISCARRA
public class CalculadoraDeTotales
{
    public decimal Calcular(int cantidad, decimal precioUnitario)
    {
        decimal total = cantidad * precioUnitario;
        return total;
    }
}


//Nuevo gestorDePedidos enflaquecido SERGIO VISCARRA
public class GestorDePedidos
{
    private readonly CalculadoraDeTotales _calculadoraT;
    private readonly CalculadoraDeDescuento _calculadoraD;
    private readonly ImpresoraDeComprobante _impresora;
    private readonly BaseDeDatosMySql _baseDeDatos;
    private readonly CorreoSmtp _correo;
    private readonly List<IDescuento> _descuentosDisponibles;

    public GestorDePedidos(CalculadoraDeTotales calculadoraT, CalculadoraDeDescuento calculadoraD,
        ImpresoraDeComprobante impresora, BaseDeDatosMySql baseDeDatos, CorreoSmtp correo,
        List<IDescuento> descuentosDisponibles)
    {
        _calculadoraT = calculadoraT;
        _calculadoraD = calculadoraD;
        _impresora = impresora;
        _baseDeDatos = baseDeDatos;
        _correo = correo;
        _descuentosDisponibles = descuentosDisponibles;
    }

    public void ProcesarPedido(string cliente, string tipoCliente, string material, int cantidad, decimal precioUnitario)
    {
        decimal total = _calculadoraT.Calcular(cantidad, precioUnitario);

        IDescuento descuento = new DescuentoClienteParticular();
        foreach (var d in _descuentosDisponibles)
        {
            if (d.Nombre == tipoCliente)
            {
                descuento = d;
                break;
            }
        }
        decimal totalFinal = total - _calculadoraD.Calcular(descuento, total);

        _baseDeDatos.GuardarPedido(cliente, material, cantidad, totalFinal);
        _impresora.Imprimir(cliente, tipoCliente, material, cantidad, totalFinal);
        _correo.Enviar($"Su pedido de {material} fue registrado, {cliente}");
    }
}

public class BaseDeDatosMySql
{
    public void GuardarPedido(string cliente, string material, int cantidad, decimal total)
        => Console.WriteLine($"[MYSQL] INSERT INTO pedidos VALUES ('{cliente}', '{material}', {cantidad}, {total})");
}

public class CorreoSmtp
{
    public void Enviar(string mensaje)
        => Console.WriteLine($"[SMTP] {mensaje}");
}

public static class Demo
{
    public static void Main()
    {
        var descuentos = new List<IDescuento>
        {
            new DescuentoClienteParticular(),
            new DescuentoClienteContratista(),
            new DescuentoClienteConstructora()
        };

        var gestor = new GestorDePedidos(
            new CalculadoraDeTotales(),
            new CalculadoraDeDescuento(),
            new ImpresoraDeComprobante(),
            new BaseDeDatosMySql(),
            new CorreoSmtp(),
            descuentos);

        // Input sin cambios respecto al original SERGIO VISCARRA
        gestor.ProcesarPedido("Marco", "contratista", "Cemento 50kg", 10, 62.00m);
    }
}
