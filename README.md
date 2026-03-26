# 💳 Sistema de Procesamiento de Pagos con Azure Service Bus

Proyecto desarrollado como parte del módulo de Azure en el máster de Desarrollo y Arquitecturas Cloud.

## 📌 Descripción

Este proyecto simula el funcionamiento de un sistema de pagos electrónicos, permitiendo gestionar transacciones desde su creación hasta su procesamiento final.

El objetivo principal es trabajar con arquitecturas desacopladas y procesamiento asíncrono utilizando servicios cloud.

El sistema permite:
- Crear transacciones desde una interfaz web
- Enviarlas a una cola para su procesamiento
- Procesarlas en segundo plano
- Gestionar errores mediante Dead Letter Queue (DLQ)
- Publicar eventos para otros sistemas (auditoría y notificaciones)

---

## 🧠 Arquitectura

El flujo de la aplicación sigue un enfoque basado en eventos:

1. El usuario crea una transacción desde la web
2. La transacción se guarda en base de datos con estado **Pendiente**
3. Se envía un mensaje a una cola de Azure Service Bus
4. Un consumidor procesa la transacción:
   - Si es válida → se marca como **Exitosa**
   - Si falla → se marca como **Fallida** y se envía a la **DLQ**
5. Se publica un evento en un Topic
6. Las suscripciones reciben los eventos:
   - Auditoría → recibe todas
   - Notificaciones → solo exitosas

---

## ⚙️ Lógica de negocio (simulación)

Para la demo se ha aplicado una regla sencilla:

- ✅ Transacciones con importe menor o igual a 5000 → **Exitosa**
- ❌ Transacciones con importe mayor a 5000 → **Fallida (DLQ)**

Esto permite probar tanto el flujo correcto como la gestión de errores.

---

## 🛠️ Tecnologías utilizadas

- ASP.NET Core MVC (.NET 8)
- Entity Framework Core
- Azure Service Bus
  - Colas
  - Topics
  - Subscriptions
  - Dead Letter Queue (DLQ)
- Azure SQL Database
- Bootstrap + CSS personalizado

---

## 🗃️ Modelo de datos

### 🔹 Transacción
Representa una operación de pago.

- TransaccionId
- Monto
- TipoTransaccion
- CuentaDestino
- Estado (Pendiente, Exitosa, Fallida)
- FechaCreacion
- FechaProcesamiento
- FechaNotificacion

### 🔹 EventoTransaccion
Registra eventos del sistema.

- EventoId
- TransaccionId
- TipoEvento
- Descripcion
- FechaEvento

### 🔹 Notificacion
Simula el envío de notificaciones al cliente.

- NotificacionId
- TransaccionId
- EmailCliente
- EstadoNotificacion
- FechaEnvio

---

## 🚀 Funcionalidades principales

✔️ Creación de transacciones desde formulario  
✔️ Envío de mensajes a cola (Service Bus)  
✔️ Procesamiento manual desde interfaz  
✔️ Gestión de estados (Pendiente, Exitosa, Fallida)  
✔️ Registro de eventos  
✔️ Simulación de notificaciones  
✔️ Uso de DLQ para errores  
✔️ Visualización en panel de control  

---

## 🎥 Demo

Se incluye un vídeo donde se muestra:

- Creación de transacciones
- Procesamiento desde la cola
- Ejemplo de transacción exitosa
- Ejemplo de transacción fallida (DLQ)
- Visualización de eventos y notificaciones

---

## ☁️ Despliegue

La aplicación está desplegada en Azure utilizando:

- Azure App Service
- Azure SQL Database
- Azure Service Bus

---

## 📚 Aprendizajes

Este proyecto ha servido para profundizar en:

- Arquitecturas desacopladas
- Procesamiento asíncrono
- Sistemas orientados a eventos
- Gestión de errores en mensajería (DLQ)
- Integración de servicios cloud

---

## 📎 Autor

Proyecto desarrollado por **Sofía** como parte de su formación en cloud computing.
