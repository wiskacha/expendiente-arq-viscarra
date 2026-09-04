public abstract class MedioDePago
{
    public abstract void Cobrar(decimal monto);
    public abstract void Devolver(decimal monto);
}

public class PagoEfectivo : MedioDePago
{
    public override void Cobrar(decimal monto)
    {
        System.Console.WriteLine($"[EFECTIVO] Cobrados {monto:0.00} Bs");
    }
    public override void Devolver(decimal monto)
    {
        System.Console.WriteLine($"[EFECTIVO] Devueltos {monto:0.00} Bs");
    }
}

public class PagoTarjeta : MedioDePago
{
    public override void Cobrar(decimal monto)
    {
        System.Console.WriteLine($"[TARJETA] Cobrados {monto:0.00} Bs");
    }
    public override void Devolver(decimal monto)
    {
        System.Console.WriteLine($"[TARJETA] Reversion de {monto:0.00} Bs solicitada al banco");
    }
}

public class PagoQr : MedioDePago
{
    public override void Cobrar(decimal monto)
    {
        System.Console.WriteLine($"[QR] Cobrados {monto:0.00} Bs");
    }
    public override void Devolver(decimal monto)
    {
        throw new NotSupportedException("El pago por QR no admite devoluciones");
    }
}

public static class Demo
{
    public static void Main()
    {
        var pagos = new List<MedioDePago> { new PagoEfectivo(), new PagoTarjeta(), new PagoQr() };

        foreach (var pago in pagos)
        {
            pago.Cobrar(100);
        }
        System.Console.WriteLine("-- EL cliente anula su compra: hay que devolver TODO --");

        foreach (var pago in pagos)
        {
            try
            {
                pago.Devolver(100);
            }
            catch (NotSupportedException ex)
            {
                System.Console.WriteLine($" EXPLOTÓ: {ex.Message}");
            }
        }
    }
}