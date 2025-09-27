# Middleware de Manejo Global de Excepciones

## Descripción
El `GlobalExceptionMiddleware` captura todas las excepciones no controladas en la aplicación y las convierte en respuestas HTTP consistentes usando la clase `ApiResponse<T>`.

## Características

### ✅ Excepciones Personalizadas Manejadas:
- **TaskNotFoundException** → 404 Not Found
- **TaskValidationException** → 400 Bad Request  
- **TaskAlreadyCompletedException** → 409 Conflict

### ✅ Excepciones del Sistema Manejadas:
- **ArgumentException** → 400 Bad Request
- **KeyNotFoundException** → 404 Not Found
- **UnauthorizedAccessException** → 401 Unauthorized
- **InvalidOperationException** → 400 Bad Request
- **TimeoutException** → 408 Request Timeout
- **Exception (genérica)** → 500 Internal Server Error

## Uso

### Registro en Program.cs:
```csharp
app.UseGlobalExceptionHandling(); // Debe ser uno de los primeros middlewares
```

### Ejemplo de Respuesta de Error:
```json
{
  "data": null,
  "statusCode": 404,
  "message": "Tarea con ID 999 no fue encontrada",
  "success": false
}
```

## Endpoints de Prueba

Para probar el middleware, puedes usar el `TestController`:

### Probar diferentes excepciones:
- `GET /api/test/exception/notfound` → TaskNotFoundException
- `GET /api/test/exception/validation` → TaskValidationException  
- `GET /api/test/exception/completed` → TaskAlreadyCompletedException
- `GET /api/test/exception/argument` → ArgumentException
- `GET /api/test/exception/generic` → Exception genérica

### Probar funcionamiento normal:
- `GET /api/test/success` → Respuesta exitosa

## Ventajas

1. **Centralización**: Todas las excepciones se manejan en un solo lugar
2. **Consistencia**: Respuestas uniformes usando ApiResponse<T>
3. **Logging**: Todas las excepciones se registran automáticamente
4. **Separación de responsabilidades**: Los servicios pueden lanzar excepciones sin preocuparse por el formato de respuesta
5. **Mantenibilidad**: Fácil agregar nuevos tipos de excepciones

## Implementación en Servicios

Los servicios ahora pueden lanzar excepciones directamente:

```csharp
public async Task<ApiResponse<TaskDto>> GetTaskByIdAsync(int id)
{
    var task = await _taskRepository.GetByIdAsync(id);
    if (task == null)
    {
        throw new TaskNotFoundException(id); // El middleware se encarga del resto
    }
    
    // Lógica normal continúa...
}
```