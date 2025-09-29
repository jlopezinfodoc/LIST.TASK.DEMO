# 📋 Task Management API

Una API REST completa para gestión de tareas construida con **ASP.NET Core 8**, siguiendo principios de **Clean Architecture** y mejores prácticas de desarrollo.

![.NET](https://img.shields.io/badge/.NET-8.0-purple)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core%208.0-blue)
![SQL Server](https://img.shields.io/badge/SQL%20Server-Database-red)
![AutoMapper](https://img.shields.io/badge/AutoMapper-12.0.1-green)
![Swagger](https://img.shields.io/badge/Swagger-OpenAPI-orange)

## 🚀 Características

- ✅ **CRUD Completo** - Crear, leer, actualizar y eliminar tareas
- ✅ **Filtros Avanzados** - Por estado, título, fechas con paginación
- ✅ **Clean Architecture** - Separación clara de responsabilidades
- ✅ **Repository Pattern** - Abstracción de acceso a datos
- ✅ **DTOs y AutoMapper** - Mapeo automático entre entidades
- ✅ **Global Exception Handling** - Manejo centralizado de errores
- ✅ **CORS Configurado** - Listo para aplicaciones front-end
- ✅ **Swagger UI** - Documentación interactiva de la API
- ✅ **Entity Framework** - Code-first con migraciones
- ✅ **Inyección de Dependencias** - Arquitectura desacoplada

## 🏗️ Arquitectura

```
┌─ Controllers/           # Controladores API (Endpoints)
├─ Servicios/            # Lógica de negocio
│  ├─ DTOs/             # Objetos de transferencia de datos
│  ├─ Interfaces/       # Contratos de servicios
│  ├─ Mappings/         # Perfiles de AutoMapper
│  └─ Exceptions/       # Excepciones personalizadas
├─ Infraestructura/     # Capa de datos
│  ├─ Persistencia/     # Contexto de Entity Framework
│  └─ Repository/       # Patrón Repository
├─ Dominio/             # Entidades del dominio
├─ Middlewares/         # Middleware personalizado
└─ Migrations/          # Migraciones de base de datos
```

## 🛠️ Tecnologías Utilizadas

### **Backend**
- **ASP.NET Core 8.0** - Framework principal
- **Entity Framework Core 8.0** - ORM para acceso a datos
- **SQL Server** - Base de datos relacional
- **AutoMapper 12.0.1** - Mapeo objeto-objeto
- **Swashbuckle (Swagger)** - Documentación API

### **Patrones y Principios**
- **Clean Architecture** - Separación de responsabilidades
- **Repository Pattern** - Abstracción de datos
- **Dependency Injection** - Inversión de control
- **SOLID Principles** - Diseño orientado a objetos
- **Exception Handling** - Manejo centralizado de errores

## 📋 Endpoints API

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| `GET` | `/api/tasks` | Obtener todas las tareas con filtros |
| `GET` | `/api/tasks/{id}` | Obtener tarea por ID |
| `POST` | `/api/tasks` | Crear nueva tarea |
| `PUT` | `/api/tasks/{id}` | Actualizar tarea completa |
| `PATCH` | `/api/tasks/{id}/complete` | Marcar tarea como completada |
| `DELETE` | `/api/tasks/{id}` | Eliminar tarea |

### **Filtros Disponibles**
- `isCompleted` - Filtrar por estado (true/false)
- `title` - Buscar en título de tarea
- `createdFrom` / `createdTo` - Rango de fechas
- `pageNumber` / `pageSize` - Paginación

## ⚡ Instalación y Configuración

### **Prerrequisitos**
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/sql-server) (LocalDB o instancia completa)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) o [VS Code](https://code.visualstudio.com/)

### **1. Clonar el Repositorio**
```bash
git clone https://github.com/jlopezinfodoc/LIST.TASK.DEMO.git
cd LIST.TASK.DEMO
```

### **2. Configurar Base de Datos**

Actualizar la cadena de conexión en `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=TU_SERVIDOR;Database=TaskDatabase;User Id=TU_USUARIO;Password=TU_PASSWORD;TrustServerCertificate=true;Encrypt=true;"
  }
}
```

### **3. Restaurar Dependencias**
```bash
dotnet restore
```

### **4. Ejecutar Migraciones**
```bash
# Instalar herramientas de EF (si no están instaladas)
dotnet tool install --global dotnet-ef

# Aplicar migraciones a la base de datos
dotnet ef database update
```

### **5. Ejecutar la Aplicación**
```bash
dotnet run
```

La API estará disponible en:
- **HTTP**: `http://localhost:5052`
- **HTTPS**: `https://localhost:7001`
- **Swagger UI**: `https://localhost:7001/swagger`

## 🧪 Pruebas

### **1. Swagger UI (Recomendado)**
Navega a `https://localhost:7001/swagger` para una interfaz interactiva.

### **2. Endpoints de Prueba**
```bash
# Verificar que la API funciona
GET /api/test/success

# Probar manejo de excepciones
GET /api/test/exception/notfound
```

### **3. Ejemplos con cURL**

**Crear una tarea:**
```bash
curl -X POST "https://localhost:7001/api/tasks" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Mi primera tarea",
    "description": "Descripción de prueba"
  }'
```

**Obtener todas las tareas:**
```bash
curl -X GET "https://localhost:7001/api/tasks"
```

**Filtrar tareas completadas:**
```bash
curl -X GET "https://localhost:7001/api/tasks?isCompleted=true"
```

### **4. Herramientas Recomendadas**
- **[Postman](https://www.postman.com/)** - Cliente API completo
- **[Thunder Client](https://marketplace.visualstudio.com/items?itemName=rangav.vscode-thunder-client)** - Extensión de VS Code
- **[Insomnia](https://insomnia.rest/)** - Cliente API alternativo

## 🌐 Integración con Frontend

### **Angular**
Revisa `ANGULAR_INTEGRATION.md` para ejemplos completos de integración.

```typescript
// Ejemplo básico
const apiUrl = 'http://localhost:5052/api/tasks';
this.http.get(apiUrl).subscribe(response => {
  console.log('Tareas:', response);
});
```

### **React**
```javascript
// Ejemplo con fetch
const response = await fetch('http://localhost:5052/api/tasks');
const data = await response.json();
console.log('Tareas:', data);
```

## 🚀 Despliegue

### **Opción 1: IIS (Windows)**
```bash
# Publicar la aplicación
dotnet publish -c Release -o ./publish

# Copiar archivos a IIS
# Configurar Application Pool para .NET 8
```

### **Opción 2: Docker**
```dockerfile
# Dockerfile incluido en el proyecto
docker build -t task-api .
docker run -p 8080:80 task-api
```

### **Opción 3: Azure App Service**
```bash
# Publicar directamente a Azure
dotnet publish -c Release
# Usar Azure CLI o Visual Studio para deployment
```

### **Variables de Entorno para Producción**
```bash
ASPNETCORE_ENVIRONMENT=Production
ConnectionStrings__DefaultConnection="tu_cadena_de_produccion"
```

## 📁 Estructura de Base de Datos

### **Tabla Tasks**
```sql
CREATE TABLE Tasks (
    Id int IDENTITY(1,1) PRIMARY KEY,
    Title nvarchar(200) NOT NULL,
    Description nvarchar(1000) NULL,
    IsCompleted bit NOT NULL DEFAULT 0,
    CreatedDate datetime2 NOT NULL DEFAULT GETDATE(),
    CompletedDate datetime2 NULL
);
```

## 🔧 Configuración CORS

La API está configurada para trabajar con aplicaciones frontend:

```csharp
// Desarrollo - Permisivo
app.UseCors("AllowAll");

// Producción - Específico
app.UseCors("AllowAngularApp");
```

Para más detalles, revisa `CORS_CONFIGURATION.md`.

## 📖 Documentación Adicional

- **[API_DOCUMENTATION.md](./API_DOCUMENTATION.md)** - Documentación completa de endpoints
- **[CORS_CONFIGURATION.md](./CORS_CONFIGURATION.md)** - Configuración CORS detallada
- **[ANGULAR_INTEGRATION.md](./ANGULAR_INTEGRATION.md)** - Integración con Angular
- **[MIDDLEWARE_DOCUMENTATION.md](./MIDDLEWARE_DOCUMENTATION.md)** - Middleware de excepciones

## 🤝 Contribuir

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/nueva-funcionalidad`)
3. Commit tus cambios (`git commit -am 'Agregar nueva funcionalidad'`)
4. Push a la rama (`git push origin feature/nueva-funcionalidad`)
5. Abre un Pull Request

## 📝 Licencia

Este proyecto está bajo la Licencia MIT. Ver el archivo [LICENSE](LICENSE) para más detalles.

## 👨‍💻 Autor

**jlopezinfodoc**
- GitHub: [@jlopezinfodoc](https://github.com/jlopezinfodoc)

## 🆘 Soporte

Si tienes problemas o preguntas:

1. **Revisa la documentación** en los archivos .md del proyecto
2. **Verifica los Issues** en GitHub
3. **Crea un nuevo Issue** con detalles del problema

---

⭐ **¡Si te gusta este proyecto, dale una estrella!** ⭐