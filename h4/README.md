# C4 — Sistema de Ventas
 
## Nivel 1 — Contexto
 
```mermaid
flowchart TB
    vendedor["👤 Vendedor<br>(registra ventas)"]
    admin["👤 Administrador<br>(ajusta stock y categorías)"]
 
    sistema["🛒 SISTEMA DE VENTAS<br>Registra ventas, controla stock<br>y avisa cuando algo se agota"]
    correo["📧 Servicio de notificaciones/whatsapp<br>(externo)"]
    pasarela["💳 Pasarela de pagos<br>(externa)"]
 
    vendedor -->|"registra ventas"| sistema
    admin -->|"gestiona productos y stock"| sistema
    sistema -->|"envía comprobantes y alertas"| correo
    correo -->|"entrega el aviso"| admin
    sistema -->|"cobra el medio de pago elegido"| pasarela
```

## Nivel 2 — Contenedores
 
```mermaid
flowchart TB
    vendedor["👤 Vendedor"]
    admin["👤 Administrador"]
 
    subgraph sistema["🛒 SISTEMA DE VENTAS"]
        webapp["🌐 Aplicación web<br>C# / ASP.NET<br>Pantallas de venta, stock y reportes"]
        api["⚙️ Lógica de negocio<br>C#<br>Ventas, productos, stock<br>Factory: elige el medio de pago"]
        bd[("🗄️ Base de datos<br>SQL<br>Productos, ventas, movimientos")]
        avisos["🛎️ Servicio de avisos<br>C#<br>Observer: publica stock-bajo<br>a los suscriptores"]
    end
 
    correo["📧 Servicio de notificaciones/whatsapp (externo)"]
    pasarela["💳 Pasarela de pagos (externa)"]
 
    vendedor --> webapp
    admin --> webapp
    webapp --> api
    api --> bd
    api -->|"publica evento stock-bajo"| avisos
    avisos --> correo
    api -->|"cobra vía medio de pago creado"| pasarela
```