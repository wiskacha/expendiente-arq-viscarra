| Principio | Ubicación | Razón |
|---|---|---|
| S (single responsibility) | `GestorDePedidos.ProcesarPedido` | El método calcula descuento, guarda en BD, imprime comprobante y envía correo. 4 operaciones sobrecargando el método. |
