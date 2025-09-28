# Configuración CORS - Task Management API

## ¿Qué es CORS?

**CORS** (Cross-Origin Resource Sharing) es un mecanismo de seguridad implementado por los navegadores web que restringe las solicitudes HTTP realizadas desde un origen (dominio, protocolo o puerto) hacia otro origen diferente.

## El Problema

Cuando una aplicación Angular (generalmente en `http://localhost:4200`) intenta consumir una API que se ejecuta en un puerto diferente (como `http://localhost:5052`), el navegador bloquea estas solicitudes por seguridad.

### Síntomas del problema CORS:
- ❌ **405 Method Not Allowed** en solicitudes OPTIONS
- ❌ **CORS policy** errors en la consola del navegador
- ❌ Las solicitudes funcionan desde **Postman/Thunder Client** pero no desde el **navegador**
- ❌ **Preflight requests** fallando

## Solución Implementada

Se han configurado **dos políticas CORS** en la API:

### 1. **Política para Desarrollo** (`AllowAll`)
```csharp
options.AddPolicy("AllowAll", policy =>
{
    policy.AllowAnyOrigin()      // Permite cualquier origen
          .AllowAnyMethod()      // Permite cualquier método HTTP
          .AllowAnyHeader();     // Permite cualquier header
});
```

### 2. **Política para Producción** (`AllowAngularApp`)
```csharp
options.AddPolicy("AllowAngularApp", policy =>
{
    policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
          .AllowAnyMethod()
          .AllowAnyHeader()
          .AllowCredentials();
});
```

## Configuración por Ambiente

- **Desarrollo**: Usa la política `AllowAll` (más permisiva)
- **Producción**: Usa la política `AllowAngularApp` (más segura)

## Puertos Comunes de Angular

Si tu aplicación Angular se ejecuta en un puerto diferente, agrega el origen correspondiente:

```csharp
policy.WithOrigins(
    "http://localhost:4200",   // Angular CLI default
    "https://localhost:4200",  // Angular CLI HTTPS
    "http://localhost:3000",   // Create React App / Next.js
    "http://localhost:8080",   // Vue CLI default
    "http://localhost:5173"    // Vite default
)
```

## Orden de Middlewares Importante

```csharp
app.UseGlobalExceptionHandling(); // 1. Manejo de excepciones
app.UseCors("AllowAll");          // 2. CORS (ANTES de UseAuthorization)
app.UseHttpsRedirection();        // 3. HTTPS redirect
app.UseAuthorization();           // 4. Autorización
app.MapControllers();             // 5. Controllers
```

## Verificar la Configuración

### 1. **Reiniciar la API**
```bash
dotnet run
```

### 2. **Verificar Headers en el Navegador**
Deberías ver estos headers en las respuestas:
```
Access-Control-Allow-Origin: *
Access-Control-Allow-Methods: GET,POST,PUT,DELETE,OPTIONS
Access-Control-Allow-Headers: *
```

### 3. **Probar desde Angular**
```typescript
import { HttpClient } from '@angular/common/http';

constructor(private http: HttpClient) {}

getTasks() {
  return this.http.get('http://localhost:5052/api/tasks');
}
```

## Solución de Problemas

### Si sigue sin funcionar:

1. **Verificar el puerto de Angular:**
   ```bash
   ng serve --port 4200
   ```

2. **Limpiar caché del navegador:**
   - Ctrl+Shift+R (hard refresh)
   - O abrir en ventana incógnita

3. **Verificar la URL exacta:**
   - Asegúrate de usar el puerto correcto de la API
   - Verifica si es HTTP o HTTPS

4. **Agregar más orígenes si es necesario:**
   ```csharp
   policy.WithOrigins(
       "http://localhost:4200",
       "http://localhost:4201", // Si Angular usa otro puerto
       "https://localhost:4200"
   )
   ```

## Headers de CORS Explicados

| Header | Descripción |
|--------|-------------|
| `Access-Control-Allow-Origin` | Especifica qué orígenes pueden acceder |
| `Access-Control-Allow-Methods` | Métodos HTTP permitidos |
| `Access-Control-Allow-Headers` | Headers permitidos en las solicitudes |
| `Access-Control-Allow-Credentials` | Permite envío de cookies/credenciales |

## Configuración Específica para Angular

Si necesitas una configuración más específica para Angular, puedes usar:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .WithMethods("GET", "POST", "PUT", "DELETE", "PATCH")
              .WithHeaders("Content-Type", "Authorization", "Accept")
              .AllowCredentials();
    });
});
```

## Comandos Útiles

### Verificar que Angular esté corriendo:
```bash
ng serve --open
```

### Verificar que la API esté corriendo:
```bash
dotnet run --urls "http://localhost:5052"
```

### Probar un endpoint específico desde Angular:
```bash
curl -X GET "http://localhost:5052/api/tasks" \
  -H "Origin: http://localhost:4200"
```

¡Con esta configuración, tu aplicación Angular debería poder comunicarse sin problemas con la API!