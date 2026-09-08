| Principio | Ubicación | Razón |
|---|---|---|
| S (single responsibility) | `GestorDePedidos.ProcesarPedido` | El método calcula descuento, guarda en BD, imprime comprobante y envía correo. 4 operaciones sobrecargando el método. |
| O (open close) | `GestorDePedidos.ProcesarPedido` (switch `tipoCliente`) | Incapacidad de agregar nuevos tipos de descuento sin tocar el método de procesamiento de pedido. |
| L/I (liskov & interface segregation) | `IEmpleadoDeFerreteria` / `Vendedor` | La interfaz obliga a `Vendedor` a implementar métodos que no le corresponden (`AutorizarVentaAlPorMayor`, `AjustarPrecio`, `VerReporteDeCompras`)|
| D (dependency injection) | `GestorDePedidos.ProcesarPedido` | Instancia directamente `new BaseDeDatosMySql()` y `new CorreoSmtp()` estas podrían ser abstracciones en su lugar |