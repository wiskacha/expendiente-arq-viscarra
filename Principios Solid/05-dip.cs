public class BaseDeDatosSqlServer
{
    public List<decimal> LeerVentasDelDia()
    {
        System.Console.WriteLine("[SQL SERVER] SELECT total FROM ventas WHERE fecha = HOY");
        return new List<decimal> { 150.00m, 89.50m, 230.00m };
    }
}

public class CorreoSmtp
{
    public void Enviar(string mensaje)
    {
        System.Console.WriteLine($"[SMTP] Enviando correo: \"{mensaje}\"");
    }
}

public class GeneradorDeReporteDeVentas
{
    public void Generar()
    {
        var baseDeDatos = new BaseDeDatosSqlServer();
        var correo = new CorreoSmtp();

        decimal total = 0;
        foreach (var venta in baseDeDatos.LeerVentasDelDia())
        {
            total += venta;
        }
        string mensaje = "Ventas del día: ";
        correo.Enviar($"{mensaje} {total:0.00} Bs");
    }
}

public static class Demo
{
    public static void Main()
    {
        new GeneradorDeReporteDeVentas().Generar();
    }
}

