namespace Ventas.ConFactory;

public interface IMedioDePago
{
    void Cobrar(decimal monto);
}