WebAppUsers

Descripción del Proyecto Este proyecto fue creado con la finalidad de mostrar los beneficios de la arquitectura hexagonal ademas de mostrar que aunque estoy en capacidad de realizar tareas desde proyectos con una estructura mas simple tambien estoy en capacidad de trabajar bajo arquitecturas mas complejas.

WebAppUsers es un sistema de gestión de usuarios diseñado para registrar, editar y eliminar usuarios, adicionalmente tambien contiene una funcionalidad la cual realiza la consulta de una api de noticias la cual contiene una serie de datos de noticias y clima con datos como title,description, humedad, temperatura y otros adicional a esto realiza el guardado de esa informacion y realiza una consulta para mostrar los datos consultados. La aplicación sigue los principios de la arquitectura hexagonal, utiliza .NET Core 8, ASP.NET Core Web API (para el manejo de servicios), SQL Server como base de datos y Entity Framework para el acceso a datos.

Tecnologías Utilizadas .NET Core 8 ASP.NET Core Web API SQL Server Entity Framework Core AutoMapper Docker Docker Compose

Estructura del Proyecto El proyecto está estructurado siguiendo una arquitectura hexagonal, lo que facilita la separación de preocupaciones y promueve la alta cohesión y bajo acoplamiento entre los componentes del sistema. La estructura del proyecto es la siguiente:

API: Contiene los controladores y la configuración de la aplicación. Application: Contiene los casos de uso de la aplicación. Core: Contiene las entidades, interfaces y DTOs. Infrastructure: Contiene la implementación de repositorios y la configuración de la base de datos.

Beneficios de la Arquitectura Hexagonal

Separación de Preocupaciones: La arquitectura hexagonal permite separar claramente la lógica de negocio de la infraestructura y los mecanismos de entrega (interfaces de usuario, APIs).
Alta Cohesión y Bajo Acoplamiento: Los componentes están altamente cohesionados y tienen bajo acoplamiento, lo que facilita el mantenimiento y la escalabilidad.
Facilidad para el Testing: Al separar la lógica de negocio de los detalles de implementación, es más sencillo escribir pruebas unitarias y de integración.
Flexibilidad y Extensibilidad: Es fácil cambiar o añadir nuevas funcionalidades sin afectar otras partes del sistema, gracias a la clara definición de las interfaces.
Reutilización de Código: La lógica de negocio puede ser reutilizada por diferentes mecanismos de entrega (por ejemplo, una API y una interfaz de usuario).
Instalación y Ejecución

Requisitos Previos .NET Core 8 SDK Docker Docker Compose Configuración del Proyecto

Clonar el repositorio: https://github.com/jeison20/WebApiUsers.git

Licencia Este proyecto está licenciado bajo la Licencia MIT. Ver el archivo LICENSE para más detalles.
