public class CalculadoraDeDescuento
{
    public decimal Calcular(string tipoCliente, decimal total)
    {
        switch (tipoCliente)
        {
            case "normal":
                return 0;
            case "frecuente":
                return total * 0.05m;
            case "mayorista":
                return total * 0.15m;
            default:
                return 0;
        }
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
        System.Console.WriteLine($"normal:      {calcualdora.Calcular("normal", 200):0.00} Bs");
        System.Console.WriteLine($"frecuente:   {calcualdora.Calcular("frecuente", 200):0.00} Bs");
        System.Console.WriteLine($"mayorista:   {calcualdora.Calcular("mayorista", 200):0.00} Bs");
        System.Console.WriteLine($"convenio:    ...no existe.");
    }
}
