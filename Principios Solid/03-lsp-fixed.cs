public abstract class MedioDePago
{
    public abstract void Cobrar(decimal monto);
}

public interface IReembolsable
{
    void Devolver(decimal monto);
}

public class PagoEfectivo : MedioDePago, IReembolsable
{
    public override void Cobrar(decimal monto)
    {
        System.Console.WriteLine($"[EFECTIVO] Cobrados {monto:0.00} Bs");
    }
    public void Devolver(decimal monto)
    {
        System.Console.WriteLine($"[EFECTIVO] Devueltos {monto:0.00} Bs");
    }
}

public class PagoTarjeta : MedioDePago, IReembolsable
{
    public override void Cobrar(decimal monto)
    {
        System.Console.WriteLine($"[TARJETA] Cobrados {monto:0.00} Bs");
    }
    public void Devolver(decimal monto)
    {
        System.Console.WriteLine($"[TARJETA] Reversion de {monto:0.00} Bs solicitada al banco");
    }
}

public class PagoQR : MedioDePago
{
    public override void Cobrar(decimal monto)
    {
        System.Console.WriteLine($"[QR] Cobrados {monto:0.00} Bs");
    }
}

public static class Demo
{
    public static void Main()
    {
        var pagos = new List<MedioDePago> { new PagoEfectivo(), new PagoTarjeta(), new PagoQR() };
        decimal cantidad = 100;
        foreach (var pago in pagos)
        {
            pago.Cobrar(cantidad);
        }

        System.Console.WriteLine("--El cliente anula su compra: se devuelve donde SE PUEDE--");

        foreach(var pago in pagos)
        {
            if(pago is IReembolsable reembolsable)
            {
                reembolsable.Devolver(cantidad);
            }
            else
            {
                System.Console.WriteLine($"[{pago.GetType().Name}] No admite devolución: se emite nota de crédito");
            }
        }
    }
}