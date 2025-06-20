# UserController Readme

## Descripción General
`UserController` es un controlador de API REST en el proyecto `WebApiColegios`. Su propósito principal es gestionar las operaciones CRUD relacionadas con los usuarios de la aplicación.

## Características Clave
- **Protección con JWT y políticas de autorización**: Algunas rutas están protegidas con el atributo `[Authorize]` y requieren autenticación a través del esquema JWT, específicamente con la política `IsAdmin`.
- **Uso de AutoMapper**: Se utiliza para mapear objetos entre entidades y DTOs (Data Transfer Objects).
- **Soporte para operaciones complejas**: El controlador incluye relaciones entre usuarios, roles, estados y contactos.

## Métodos Principales
### 1. **GetUser**
Obtiene un usuario específico por su ID.  
**Ruta:** `GET api/school/user/{id:int}`  
**Autorización requerida:** Sí, solo accesible para usuarios administradores.  
```c#
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
[HttpGet("{id:int}", Name = "GetUser")]
```
- Incluye datos relacionados como el rol, estado y contactos del usuario.  
- Devuelve un DTO con los datos del usuario solicitado.

### 2. **Post (CreateUser)**
Crea un nuevo usuario en el sistema.  
**Ruta:** `POST api/school/user`  
**Autorización requerida:** Sí, solo accesible para usuarios administradores.  
```c#
[HttpPost(Name = "CreateUser")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
```
- Verifica la validez del modelo y la existencia de `RoleID` y `StatusID` antes de guardar.  
- Permite incluir contactos relacionados con el usuario.  
- Devuelve una respuesta `201 Created` con la ruta del recurso recién creado.

### 3. **Put (Actualizar Usuario)**
Actualiza un usuario existente.  
**Ruta:** `PUT api/school/user/{id:int}`  
**Autorización requerida:** No.  
```c#
[HttpPut("{id:int}")]
```
- Actualiza la información básica de un usuario existente con datos proporcionados en el cuerpo de la solicitud.

### 4. **Delete (Eliminar Usuario)**
Elimina un usuario por su ID.  
**Ruta:** `DELETE api/school/user/{id:int}`  
**Autorización requerida:** No.  
```c#
[HttpDelete("{id:int}")]
```
- Elimina un usuario de la base de datos y devuelve una respuesta `204 No Content`.

## Resaltado: **Autorización**
El uso del atributo `[Authorize]` en varios métodos asegura que solo los usuarios autenticados y autorizados puedan acceder a ciertas operaciones.  
- **Esquema utilizado:** `JwtBearerDefaults.AuthenticationScheme`  
- **Política:** `IsAdmin` (requiere permisos de administrador).  

Ejemplo:  
```c#
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
[HttpGet("{id:int}", Name = "GetUser")]
```

## Tecnologías Utilizadas
- **Framework:** ASP.NET Core  
- **Autenticación:** JWT Bearer  
- **ORM:** Entity Framework Core  
- **Mapeo de Objetos:** AutoMapper  

## Estructura de Rutas
| Método | Ruta                            | Descripción                       | Autorización |
|--------|---------------------------------|-----------------------------------|--------------|
| GET    | api/school/user/{id:int}        | Obtiene un usuario por ID.        | Sí           |
| POST   | api/school/user                 | Crea un nuevo usuario.            | Sí           |
| PUT    | api/school/user/{id:int}        | Actualiza un usuario existente.   | No           |
| DELETE | api/school/user/{id:int}        | Elimina un usuario por ID.        | No           |

# AccountsController Readme

## Descripción General
`AccountsController` es un controlador de API REST que gestiona la autenticación y autorización de usuarios en la aplicación. Este controlador permite el registro, inicio de sesión, renovación de tokens, y la asignación o eliminación del rol de administrador.

## Métodos Principales
### 1. **Register**
Registra un nuevo usuario en el sistema.  
**Ruta:** `POST api/accounts/register`  
**Autorización requerida:** No.  
```c#
[HttpPost("register", Name = "registerUser")]
```
- **Entrada**: Credenciales del usuario (`UserCredentialsDTO`).  
- **Salida**: Un token de autenticación y la fecha de expiración.

### 2. **Login**
Inicia sesión de un usuario y genera un token de autenticación.  
**Ruta:** `POST api/accounts/login`  
**Autorización requerida:** No.  
```c#
[HttpPost("login", Name = "LoginUser")]
```
- **Entrada**: Credenciales del usuario (`UserCredentialsDTO`).  
- **Salida**: Un token JWT y la fecha de expiración.

### 3. **RenewToken**
Renueva el token de autenticación para usuarios autenticados.  
**Ruta:** `GET api/accounts/RenewToken`  
**Autorización requerida:** Sí.  
```c#
[HttpGet("RenewToken", Name = "renewToken")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
```
- Extrae el email del token JWT existente y genera un nuevo token.

### 4. **MakeAdmin**
Asigna el rol de administrador a un usuario.  
**Ruta:** `POST api/accounts/MakeAdmin`  
**Autorización requerida:** No.  
```c#
[HttpPost("MakeAdmin", Name = "makeAdmin")]
```
- **Entrada**: Email del usuario (`EditAdminDTO`).  
- Agrega un `Claim` con el valor `isAdmin = 1`.

### 5. **RemoveAdmin**
Revoca el rol de administrador de un usuario.  
**Ruta:** `POST api/accounts/RemoveAdmin`  
**Autorización requerida:** No.  
```c#
[HttpPost("RemoveAdmin", Name = "removeAdmin")]
```
- **Entrada**: Email del usuario (`EditAdminDTO`).  
- Elimina el `Claim` con el valor `isAdmin = 1`.

## Tecnologías Utilizadas
- **Framework:** ASP.NET Core  
- **Autenticación:** JWT Bearer  
- **Seguridad:** SymmetricSecurityKey y HMAC-SHA256 para la generación de tokens.

## Estructura de Rutas
| Método | Ruta                          | Descripción                           | Autorización |
|--------|-------------------------------|---------------------------------------|--------------|
| POST   | api/accounts/register         | Registra un nuevo usuario.            | No           |
| POST   | api/accounts/login            | Inicia sesión y genera un token JWT.  | No           |
| GET    | api/accounts/RenewToken       | Renueva el token de autenticación.    | Sí           |
| POST   | api/accounts/MakeAdmin        | Asigna el rol de administrador.       | No           |
| POST   | api/accounts/RemoveAdmin      | Revoca el rol de administrador.       | No           |

