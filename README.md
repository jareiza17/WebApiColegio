# 📚 WebApiColegios

API RESTful desarrollada en ASP.NET Core para la gestión de estudiantes, materias y sus inscripciones.

> 🔧 Proyecto técnico realizado como parte de una prueba para evaluar arquitectura por capas, buenas prácticas, SOLID, Entity Framework, validaciones y pruebas unitarias.

---

## 🚀 Tecnologías usadas

- ✅ ASP.NET Core 6+
- ✅ Entity Framework Core
- ✅ SQL Server Express
- ✅ AutoMapper
- ✅ xUnit + Moq (para pruebas)
- ✅ Swagger para documentación

---

## 📦 Estructura por capas

- **WebApiColegios**: capa de presentación (API)
- **DomainLayer**: servicios, reglas de negocio e interfaces
- **DataAccessLayer**: implementación de repositorios (EF Core)
- **ModelsLayer**: entidades del dominio

---

## 📄 Requisitos

- .NET 6 SDK o superior
- SQL Server Express instalado
- Visual Studio 2022 o terminal .NET CLI

---

## 🛠️ Cómo ejecutar

1. Clona el repositorio

```bash
git clone https://github.com/jareiza17/WebApiColegio.git
cd WebApiColegios
```

2. Verifica tu cadena de conexión en `appsettings.json`:

```json
"ConnectionStrings": {
  "defaultConnection": "Server=.;Database=WebApiColegiosDB;Trusted_Connection=True;TrustServerCertificate=True"
}
```

3. Aplica las migraciones y crea la base de datos:

```bash
Update-Database
```

4. Ejecuta el proyecto:

```bash
dotnet run
```

5. Accede a Swagger:

```
https://localhost:<puerto>/swagger
```

---

## 🔁 Endpoints principales

| Acción                       | Ruta                          | Método |
|-----------------------------|-------------------------------|--------|
| Listar estudiantes          | `/api/school/students`        | GET    |
| Crear estudiante            | `/api/school/students`        | POST   |
| Editar estudiante           | `/api/school/students/{id}`   | PUT    |
| Eliminar estudiante         | `/api/school/students/{id}`   | DELETE |
| Listar materias             | `/api/school/subjects`        | GET    |
| Crear materia               | `/api/school/subjects`        | POST   |
| Inscribir materias          | `/api/school/enrollments`     | POST   |
| Ver materias inscritas      | `/api/school/enrollments/{id}`| GET    |

---

## ✅ Validaciones implementadas

- Un estudiante **no puede inscribirse a más de 3 materias con más de 4 créditos**
- No se permiten **inscripciones duplicadas**
- Validación de existencia de estudiantes y materias antes de operar

---

## 🧪 Pruebas unitarias

Se implementaron pruebas con `xUnit` y `Moq` para el controlador de inscripciones:

- ✅ Inscripción exitosa
- ✅ Error por materias > 4 créditos
- ✅ Consulta de materias inscritas

Para ejecutar:

```bash
dotnet test
```

---

## 👨‍💻 Autor

**Jaime Felipe Areiza**  
Desarrollador .NET y analista de datos

---

## 📬 Contacto

- GitHub: [@jareiza17](https://github.com/jareiza17)
- Email: (agrega si deseas)

---

## 🏁 Licencia

Este proyecto fue creado para fines académicos y de evaluación técnica. Puedes usarlo como referencia.