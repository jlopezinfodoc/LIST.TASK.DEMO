# 🤝 Guía de Contribución

¡Gracias por tu interés en contribuir al proyecto **Task Management API**! Esta guía te ayudará a comenzar.

## 🚀 Cómo Contribuir

### 1. **Fork del Repositorio**
- Haz fork del repositorio en GitHub
- Clona tu fork localmente:
```bash
git clone https://github.com/TU_USUARIO/LIST.TASK.DEMO.git
cd LIST.TASK.DEMO
```

### 2. **Configurar el Entorno**
- Instala [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Configura SQL Server (LocalDB o instancia completa)
- Ejecuta las migraciones: `dotnet ef database update`

### 3. **Crear una Rama**
```bash
git checkout -b feature/nombre-de-tu-feature
# o
git checkout -b bugfix/descripcion-del-bug
```

### 4. **Realizar Cambios**
- Sigue las convenciones de código del proyecto
- Agrega tests si es necesario
- Actualiza la documentación si es relevante

### 5. **Probar los Cambios**
```bash
# Compilar
dotnet build

# Ejecutar
dotnet run

# Probar endpoints en Swagger
# https://localhost:7001/swagger
```

### 6. **Commit y Push**
```bash
git add .
git commit -m "tipo: descripción breve del cambio"
git push origin nombre-de-tu-rama
```

### 7. **Crear Pull Request**
- Ve a tu fork en GitHub
- Crea un Pull Request hacia la rama `dev`
- Describe claramente qué cambios realizaste

## 📝 Convenciones de Código

### **Nombres**
- **Clases**: PascalCase (`TaskService`, `TaskController`)
- **Métodos**: PascalCase (`GetTasksAsync`, `CreateTask`)
- **Variables**: camelCase (`taskDto`, `isCompleted`)
- **Constantes**: UPPER_CASE (`MAX_PAGE_SIZE`)

### **Estructura de Archivos**
```
Controllers/     # Controladores API
Servicios/       # Lógica de negocio
├─ DTOs/        # Data Transfer Objects
├─ Interfaces/  # Contratos de servicios
└─ Mappings/    # Perfiles de AutoMapper
Infraestructura/ # Capa de datos
├─ Repository/  # Patrón Repository
└─ Persistencia/ # Contexto EF
Dominio/        # Entidades del dominio
```

### **Estilo de Código**
- Usar `async/await` para operaciones asíncronas
- Manejar excepciones apropiadamente
- Documentar métodos públicos con XML comments
- Seguir principios SOLID

## 🐛 Reportar Bugs

### **Antes de Reportar**
1. Verifica que no exista un issue similar
2. Asegúrate de estar usando la última versión
3. Prueba en un entorno limpio

### **Formato del Issue**
```markdown
**Descripción del Bug**
Descripción clara y concisa del problema.

**Pasos para Reproducir**
1. Ir a '...'
2. Hacer clic en '...'
3. Introducir '...'
4. Ver error

**Comportamiento Esperado**
Qué debería haber pasado.

**Screenshots**
Si aplica, agrega screenshots.

**Entorno**
- OS: [ej. Windows 11]
- .NET Version: [ej. 8.0]
- SQL Server Version: [ej. 2022]
```

## ✨ Sugerir Funcionalidades

### **Formato de Solicitud**
```markdown
**¿Tu solicitud está relacionada con un problema?**
Descripción clara del problema. Ej. "Me frustra cuando [...]"

**Describe la solución que te gustaría**
Descripción clara de lo que quieres que pase.

**Describe alternativas consideradas**
Descripción de soluciones alternativas.

**Contexto adicional**
Cualquier otra información relevante.
```

## 🧪 Testing

### **Ejecutar Tests**
```bash
# Cuando se implementen tests unitarios
dotnet test
```

### **Tipos de Tests**
- **Unit Tests**: Probar lógica de servicios
- **Integration Tests**: Probar endpoints completos
- **Repository Tests**: Probar acceso a datos

### **Agregar Tests**
- Crea tests para nueva funcionalidad
- Usa `xUnit` como framework de testing
- Mock dependencias con `Moq`

## 📋 Tipos de Contribuciones

### **🐛 Bug Fixes**
- Corrección de errores existentes
- Mejoras de rendimiento
- Correcciones de seguridad

### **✨ Features**
- Nueva funcionalidad
- Mejoras de API existente
- Integración con nuevas tecnologías

### **📖 Documentación**
- Mejorar README
- Agregar ejemplos de código
- Corregir typos

### **🎨 Refactoring**
- Mejorar estructura de código
- Aplicar patrones de diseño
- Optimizaciones

## 🔄 Proceso de Review

### **Criterios de Aceptación**
- ✅ El código compila sin errores
- ✅ Sigue las convenciones del proyecto
- ✅ Incluye tests si es necesario
- ✅ La documentación está actualizada
- ✅ No rompe funcionalidad existente

### **Timeline**
- **Review inicial**: 2-3 días hábiles
- **Feedback**: Respuesta en 1-2 días
- **Merge**: Una vez aprobado el PR

## 🏷️ Convenciones de Commit

Usa [Conventional Commits](https://www.conventionalcommits.org/):

```
tipo(alcance): descripción

feat: agregar endpoint para estadísticas
fix: corregir validación en CreateTaskDto
docs: actualizar README con ejemplos
style: aplicar formato de código
refactor: reorganizar estructura de servicios
test: agregar tests para TaskService
chore: actualizar dependencias
```

## 🌟 Reconocimientos

Todos los contribuidores serán reconocidos en el proyecto. ¡Tu contribución importa!

## 💬 Comunicación

- **Issues**: Para bugs y solicitudes de funcionalidades
- **Discussions**: Para preguntas generales
- **Pull Requests**: Para contribuciones de código

## 📚 Recursos Útiles

- [Documentación de ASP.NET Core](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)
- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [REST API Best Practices](https://docs.microsoft.com/azure/architecture/best-practices/api-design)

---

¡Gracias por contribuir! 🎉