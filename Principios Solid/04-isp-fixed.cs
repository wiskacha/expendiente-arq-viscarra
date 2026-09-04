public interface IVendedor
{
    void RegistrarVenta(string producto);
}

public interface IGestorDePrecios
{
    void AjustarPrecio(string producto, decimal nuevoPrecio);
}

public interface ISupervisorDeVentas
{
    void AnularVenta(int numeroDeVenta);
    void VerReporteDeCaja();
}

public class Cajero : IVendedor
{
    public void RegistrarVenta(string producto)
    {
        System.Console.WriteLine($"[CAJERO] Vende {producto}");
    }
}

public class Administrador : IVendedor, IGestorDePrecios, ISupervisorDeVentas
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

public static class Demo
{
    public static void Main()
    {
        var cajero = new Cajero();
        var admin = new Administrador();

        cajero.RegistrarVenta("Cuaderno");
        admin.AjustarPrecio("Cuaderno" , 15);
        admin.VerReporteDeCaja();
        
    }
}
