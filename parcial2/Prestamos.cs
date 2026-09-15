public interface IObservadorVencimiento
{
    void Notificar(Prestamo prestamo);
}

public class Prestamo
{
    public string Socio { get; set; }
    public string Libro { get; set; }
    public DateTime FechaVencimiento { get; set; }

}

public class AvisoCorreoSocio : IObservadorVencimiento
{
    public void Notificar(Prestamo prestamo) =>
        System.Console.WriteLine($"[CORREO] Enviando correo electrónico a: {prestamo.Socio}");
}

public class RegistroMorosidad : IObservadorVencimiento
{
    public void Notificar(Prestamo prestamo) =>
        System.Console.WriteLine($"[REGISTRO] Registrando vencimiento en el registro de morosidad socio: {prestamo.Socio} libro: {prestamo.Libro}");
}

public class AvisoPantalla : IObservadorVencimiento
{
    public void Notificar(Prestamo prestamo) =>
        System.Console.WriteLine($"[PANTALLA] ¡ATENCIÓN! {prestamo.Socio} DEBE {prestamo.Libro} ");
}

public class AvisoSistemaMunicipal : IObservadorVencimiento
{
    public void Notificar(Prestamo prestamo) =>
        System.Console.WriteLine($"[MUNICIPIO] Enviando datos del vencimiento de {prestamo.Socio} al sistema Municipal de multas");
}


public class ModuloPrestamos
{
    private readonly List<IObservadorVencimiento> _interesados = new();
    public void Suscribir(IObservadorVencimiento interesado) => _interesados.Add(interesado);

    public void MarcarVencido(Prestamo prestamo)
    {
        foreach (var interesado in _interesados)
        {
            interesado.Notificar(prestamo);
        }
    }
}

public class Demo
{
    public static void Main()
    {
        var modulo = new ModuloPrestamos();
        modulo.Suscribir(new AvisoCorreoSocio());
        modulo.Suscribir(new RegistroMorosidad());
        modulo.Suscribir(new AvisoPantalla());
        modulo.Suscribir(new AvisoSistemaMunicipal());

        var prestamo = new Prestamo { Socio = "Juana Perez", Libro = "Cien años de soledad", FechaVencimiento = DateTime.Today };
        modulo.MarcarVencido(prestamo);
    }
}

// Solucion: VISCARRA VARGAS SERGIO DANIEL.