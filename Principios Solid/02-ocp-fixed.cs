public interface IDescuento
{
    string Nombre { get; }
    decimal Calcular(decimal total);
}

public class DescuentoClienteNormal : IDescuento
{
    public string Nombre => "normal";
    public decimal Calcular(decimal total) => 0;
}

public class DescuentoClienteFrecuente : IDescuento
{
    public string Nombre => "frecuente";
    public decimal Calcular(decimal total) => total * 0.05m;
}

public class DescuentoClienteMayorista : IDescuento
{
    public string Nombre => "mayorista";
    public decimal Calcular(decimal total) => total * 0.15m;
}

public class DescuentoConvenio : IDescuento
{
    public string Nombre => "convenio";
    public decimal Calcular(decimal total) => total * 0.10m;
}

public class CalculadoraDeDescuento
{
    public decimal Calcular(IDescuento descuento, decimal total)
    {
        return descuento.Calcular(total);
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
        var calcualdora = new CalculadoraDeDescuento();
        var tipos = new List<IDescuento>
        {
            new DescuentoClienteNormal(),
            new DescuentoClienteFrecuente(),
            new DescuentoClienteMayorista(),
            new DescuentoConvenio()
        };

        foreach(var descuento in tipos)
        {
            System.Console.WriteLine($"{descuento.Nombre}: {calcualdora.Calcular(descuento, 200):0.00} Bs");
        }
    }
}