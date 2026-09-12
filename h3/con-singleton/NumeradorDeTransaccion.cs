namespace Ventas.ConSingleton;

public class NumeradorDeTransaccion
{
    private static NumeradorDeTransaccion? _instancia;
    private int _ultimoNumero = 0;

    public static NumeradorDeTransaccion Instancia
    {
        get
        {
            if (_instancia == null)
            {
                _instancia = new NumeradorDeTransaccion();
            }
            return _instancia;
        }
    }

    private NumeradorDeTransaccion() { }

    public int SiguienteNumero()
    {
        _ultimoNumero++;
        return _ultimoNumero;
    }
}