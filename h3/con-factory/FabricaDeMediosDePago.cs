namespace Ventas.ConFactory;

public static class FabricaDeMediosDePago
{
    public static IMedioDePago Crear(string medio) => medio switch
    {
        "efectivo" => new PagoEfectivo(),
        "tarjeta" => new PagoTarjeta(),
        "qr" => new PagoQR(),
        _ => throw new ArgumentException($"Medio de pago desconocido: {medio}")
    };
}