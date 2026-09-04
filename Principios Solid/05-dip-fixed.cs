public interface IOrigenDeVentas
{
    List<decimal> LeerVentasDelDia();
}

public interface ICanalDeAviso
{
    void Enviar(string mensaje);
}

public class BaseDeDatosSqlServer : IOrigenDeVentas
{
    public List<decimal> LeerVentasDelDia()
    {
        System.Console.WriteLine("[SQL SERVER] SELECT total FROM ventas WHERE fecha = HOY");
        return new List<decimal> { 150.00m, 89.50m, 230.00m };
    }
}

public class VentasDePrueba : IOrigenDeVentas
{
    public List<decimal> LeerVentasDelDia()
    {
        System.Console.WriteLine("[PRUEBA] Datos falseados");
        return new List<decimal> { 10.00m, 20.00m };
    }
}

public class CorreoSmtp : ICanalDeAviso
{
    public void Enviar(string mensaje)
    {
        System.Console.WriteLine($"[SMTP] Enviando correo: \"{mensaje}\"");
    }
}

public class AvisoWhatsapp : ICanalDeAviso
{
    public void Enviar(string mensaje)
    {
        System.Console.WriteLine($"[Whatsapp] Enviando mensaje: \"{mensaje}\"");
    }
}

public class GeneradorDeReporteDeVentas
{
    private readonly IOrigenDeVentas _origen;
    private readonly ICanalDeAviso _canal;

    public GeneradorDeReporteDeVentas(IOrigenDeVentas origen, ICanalDeAviso canal)
    {
        _origen = origen;
        _canal = canal;
    }

    public void Generar()
    {
        decimal total = 0;
        foreach (var venta in _origen.LeerVentasDelDia())
        {
            total += venta;
        }
        _canal.Enviar($"Ventas del día: {total:0.00} Bs");
    }
}

public static class Demo
{
    public static void Main()
    {
        System.Console.WriteLine("--Producción SQL Server + correo ---");
        new GeneradorDeReporteDeVentas(new BaseDeDatosSqlServer(), new CorreoSmtp()).Generar();

        System.Console.WriteLine("--Producción SQL Server + Whatsapp--");
        new GeneradorDeReporteDeVentas(new BaseDeDatosSqlServer(), new AvisoWhatsapp()).Generar();

        System.Console.WriteLine("--Datos de prueba + Whatsapp--");
        new GeneradorDeReporteDeVentas(new VentasDePrueba(), new AvisoWhatsapp()).Generar();
    }
}