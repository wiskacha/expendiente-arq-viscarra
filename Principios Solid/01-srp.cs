namespace Srp1; 
public class GestorDeVentas
{
    public void RegistrarVenta(string cliente, List<(string producto, int cantidad, decimal precio)> detalles)
    {
        //Calcular el total
        decimal total = 0;
        foreach (var detalle in detalles)
        {
            total += detalle.cantidad * detalle.precio;
        }

        //Guarda en la base de datos
        Console.WriteLine($"[BD] INSERT INTO ventas (cliente, total) VALUES ('{cliente}', {total})");

        //Imprime el ticket
        Console.WriteLine("-------Ticket--------");
        foreach (var detalle in detalles)
        {
            Console.WriteLine($"{detalle.cantidad} x {detalle.producto} {detalle.cantidad * detalle.precio:0.00} Bs");
        }
        Console.WriteLine($"TOTAL: {total:0.00} Bs");

        //Notifica al cliente
        Console.WriteLine($"[CORREO] Enviando comprobante a {cliente}...");
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
        gestor.RegistrarVenta(
            "Noelia",
            new List<(string, int, decimal)>
            {
                ("Cuaderno", 3, 12.50m),
                ("Boligrafo", 5, 2.00m)
            }
        );
    }
}