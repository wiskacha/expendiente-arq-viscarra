namespace Ventas.ConFactory;

public class PagoEfectivo : IMedioDePago
{
    public void Cobrar(decimal monto)
        => Console.WriteLine($"[EFECTIVO] Cobrando {monto:C}");
}

public class PagoTarjeta : IMedioDePago
{
    public void Cobrar(decimal monto)
        => Console.WriteLine($"[TARJETA] Cobrando {monto:C}");
}

public class PagoQR : IMedioDePago
{
    public void Cobrar(decimal monto)
        => Console.WriteLine($"[QR] Cobrando {monto:C}");
}