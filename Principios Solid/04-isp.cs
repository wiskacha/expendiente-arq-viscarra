public interface IEmpladoDeTienda
{
    void RegistrarVenta(string producto);
    void AjustarPrecio(string producto, decimal nuevoPrecio);
    void AnularVenta(int numeroDeVenta);
    void VerReporteDeCaja();
}

public class Administrador : IEmpladoDeTienda
{
    public void RegistrarVenta(string producto)
    {
        System.Console.WriteLine($"[ADMIN] Vende {producto}");
    }
    public void AjustarPrecio(string producto, decimal nuevoPrecio)
    {
        System.Console.WriteLine($"[ADMIN] {producto} ahora cuesta {nuevoPrecio:0.00} Bs");
    }
    public void AnularVenta(int numeroDeVenta)
    {
        System.Console.WriteLine($"[ADMIN] Anuló la venta {numeroDeVenta}");
    }
        public void VerReporteDeCaja()
    {
        System.Console.WriteLine($"[ADMIN] Reporte de caja del día");
    }
}

public class Cajero : IEmpladoDeTienda
{
    public void RegistrarVenta(string producto)
    {
        System.Console.WriteLine($"[CAJERO] Vende {producto}");
    }

    public void AjustarPrecio(string producto, decimal nuevoPrecio)
    {
        throw new NotSupportedException("Un cajero no ajusta precios.");
    }
    public void AnularVenta(int numeroDeVenta)
    {
        throw new NotSupportedException("Un cajero no anula ventas.");
    }
    public void VerReporteDeCaja()
    {
        throw new NotSupportedException("Un cajero no ve reportes.");
    }
}

public static class Demo
{
    public static void Main()
    {
        var cajero = new Cajero();
        cajero.RegistrarVenta("Cuaderno");

        try
        {
            cajero.AjustarPrecio("Cuaderno", 15);
        }
        catch (NotSupportedException ex)
        {
            System.Console.WriteLine($"EXPLOTÓ: {ex.Message}");
        }
    }
}