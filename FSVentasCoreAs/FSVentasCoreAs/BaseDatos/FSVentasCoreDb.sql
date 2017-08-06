use FSVentasCore2017Db;



create table TipoUsuarios 
(
    TipoId int identity(1,1) primary key,
	Nombre varchar(70),
);

Create table Usuarios
(
    UsuarioId int identity(1,1) primary key,
	Nombres varchar(70),
	Contraseña varchar(50),
	TipoId int ,
	CONSTRAINT fk_TipoUsuarios FOREIGN KEY (TipoId) REFERENCES TipoUsuarios (TipoId),
	
);
CREATE TABLE Empleados (
    EmpleadoId   INT           IDENTITY (1, 1) primary key,
    Nombre     VARCHAR (70),  
    Sexo        VARCHAR (10),  
    Cedula      VARCHAR (13),  
    ProvinciaId INT ,         
    MunicipioId INT       ,
    DistritoId INT          ,
    SectorId   INT           ,
    Direccion   VARCHAR (100) ,
    Telefono    VARCHAR (13)  ,
    Celular    VARCHAR (13) ,
    Fecha      DATETIME      ,
   
);




create table Provincias
(
    ProvinciaId int identity(1,1) primary key,
	Nombre varchar(70),
);

create table Municipios
(
    MunicipioId int identity(1,1) primary key,
	Nombre varchar(70),
	ProvinciaId int,
	CiudadId int,
);

create table Ciudades
(
    CiudadId int identity(1,1) primary key,
	Nombre varchar(70),
	ProvinciaId int,
);

create table Proveedores
(
    ProveedorId int identity(1,1) primary key,
	Nombre varchar(70),
	MarcaId int,
	ProvinciaId int,
	CiudadId int,
	MunicipioId int,
	Direccion varchar(100),
	Telefono varchar(13),
	Fax varchar(13),
	Correo varchar(100),
	Fecha Datetime,
	CONSTRAINT fk_Marcas FOREIGN KEY (MarcaId) REFERENCES Marcas (MarcaId),
	CONSTRAINT fk_Provincias FOREIGN KEY (ProvinciaId) REFERENCES Provincias (ProvinciaId),
	CONSTRAINT fk_Ciudades FOREIGN KEY (CiudadId) REFERENCES Ciudades (CiudadId),
	CONSTRAINT fk_Municipios FOREIGN KEY (MunicipioId) REFERENCES Municipios (MunicipioId),
	

);

create table Clientes
(
   ClienteId int identity(1,1) primary key,
   Nombre  varchar(70),
   Sexo    varchar(10),
   Cedula  varchar(13),
   ProvinciaId  int,
   CiudadId int,
   MunicipioId int,
   Direccion varchar(100),
   Telefono varchar(13),
   Celular  varchar(13),
   Fecha datetime,
   CONSTRAINT fk_Provincias FOREIGN KEY (ProvinciaId) REFERENCES Provincias (ProvinciaId),
   CONSTRAINT fk_Ciudades   FOREIGN KEY (CiudadId) REFERENCES Ciudades (CiudadId),
   CONSTRAINT fk_Municipios FOREIGN KEY (MunicipioId) REFERENCES Municipios (MunicipioId),
);



create table Marcas 
(
   MarcaId int identity (1,1) primary key,
   Nombre varchar(70),
);

create table Categorias
(
   CategoriaId int identity (1,1) primary key,
   Nombre varchar(70),
);

Create table Articulos
(
   ArticuloId int identity (1,1) primary key,
   Nombre  varchar(70),
   Descripcion varchar(200),
   MarcaId int,
   ProveedorId int,
   CategoriaId int,
   Cantidad int,
   Descuento float,
   PrecioCompra money,
   Precio money,
   Importe float,
   Fecha datetime,
   CONSTRAINT fk_Marcas FOREIGN KEY (MarcaId) REFERENCES Marcas(MarcaId),
   CONSTRAINT fk_Proveedores FOREIGN KEY (ProveedorId) REFERENCES Proveedores (ProveedorId),
   CONSTRAINT fk_Categorias FOREIGN KEY (CategoriaId) REFERENCES Categorias (CategoriaId),
);


create table Ventas
(
   VentaId int identity (1,1) primary key,
   ClienteId int,
   Clientes varchar(max),
   Fecha Datetime,
   Cantidad  int,
   Empleado varchar(max),
   CONSTRAINT fk_Usuarios FOREIGN KEY (UsuarioId) REFERENCES Usuarios (UsuarioId),
   CONSTRAINT fk_Articulos FOREIGN KEY (ArticuloId) REFERENCES Articulos (ArticuloId),
   CONSTRAINT fk_Usuarios FOREIGN KEY (UsuarioId) REFERENCES Usuarios (UsuarioId)
   
);

create table VentasDetalles
(
   Id int identity (1,1) primary key,
   VentaId int,
   ArticuloId int,
   Articulo nvarchar(Max),
   Cantidad int,
   Precio Money,
   Descuento float,
   SubTotal Money,
  
);



create table CotizacionesDetalles
(
    Id int identity (1,1) primary key,
	Descripcion varchar(100),
	Cantidad int,
	Precio money,
	CotizacionId int,
	ArticuloId int,
   CONSTRAINT fk_CotizacionesDetalles FOREIGN KEY (CotizacionId) REFERENCES Contizaciones (CotizacionId),
   CONSTRAINT fk_Articulos FOREIGN KEY (ArticuloId) REFERENCES Articulos (ArticuloId),

);
create table Cotizaciones
(
    CotizacionId int identity(1,1) primary key,
	NombreCliente varchar(70),
	ArticuloId int,
	Fecha datetime,
	Precio money,
);






