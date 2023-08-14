create database sistemafarmacia
go

use sistemafarmacia
go

create table categorias(
	idcategoria int identity primary key,
	descripcion varchar(100),
	estado bit default 1,
	fecharegistro datetime default getdate()
)
go

insert into categorias (descripcion) values
('Cuidado Personal'),
('Nutricion'),
('Zona Fitnes'),
('Dermocosmetica')
select * from categorias

create table marcas(
	idmarca int identity primary key,
	descripcion varchar(100),
	estado bit default 1,
	fecharegistro datetime default getdate()
)
go
insert into marcas (descripcion) values
('Head & Shoulders'),
('Pantene'),
('Ensure'),
('Geriaplus'),
('Yaqua'),
('Electrolight'),
('BioDerma'),
('Eucerin')
select * from marcas

create table productos(
	idproducto int identity primary key,
	idmarca int,
	idcategoria int,
	nombre varchar(100),
	Descripcion varchar(500),
	precio decimal(10,2) default 0,
	stock int,
	rutaimg varchar(100),
	nombreimg varchar(100),
	estado bit default 1,
	fecharegistro datetime default getdate(),
	constraint fk_idmarca_prod foreign key (idmarca) references marcas (idmarca),
	constraint fk_idcategoria_prod foreign key (idcategoria) references categorias (idcategoria)
)
go

select * from productos

create table clientes(
	idcliente int identity primary key,
	nombres varchar(50),
	apellidos varchar(50),
	correo varchar(50),
	clave varchar(50),
	restablecer bit default 0, /*cambiar contraseña*/
	fecharegistro datetime default getdate()
)
go

create table carrito(
	idcarrito int identity primary key,
	idcliente int,
	idproducto int,
	cantidad int,
	constraint fk_idcliente_car foreign key (idcliente) references clientes (idcliente),
	constraint fk_idproducto_car foreign key (idproducto) references productos (idproducto)
)
go

create table ventas(
	idventa int identity primary key,
	idcliente int,
	totalproducto int,
	montototal decimal(10,2),
	contacto varchar(50),
	iddistrito varchar(10),
	telefono varchar(50),
	direccion varchar(30),
	idtransaccion varchar(50), /*paypal id de codigo*/
	fechaventa datetime default getdate(),
	constraint ck_telefono_ven check (telefono LIKE ('[9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')),
	constraint fk_idcliente_ven foreign key (idcliente) references clientes (idcliente)
)
go

create table detallesVentas(
	iddetventa int identity primary key,
	idventa int,
	idproducto int,
	cantidad int,
	total decimal(10,2),
	constraint fk_idventa_detVen foreign key (idventa) references ventas (idventa),
	constraint fk_idproducto_detVen foreign key (idproducto) references productos (idproducto)
)
go

create table usuarios(
	idusuario int identity primary key,
	nombreusuario varchar(100),
	correo varchar(50),
	clave varchar(90),
	restablecer bit default 1,
	estado bit default 1,
	fecharegistro datetime default getdate()
)
go
/*
* clave. 12345
*/
insert into usuarios(nombreusuario, correo, clave) values
('alfredopepino', 'alfredo@hotmail', '5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5'),
('karolinaCD', 'karoCD@gmail', '5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5')
select idusuario, nombreusuario, correo, clave, restablecer, estado from usuarios

update usuarios set estado = 0 where idusuario = 1


--datos de ubigeo
create table departamentos(
	iddepartamento varchar(2) not null, /*omitimos el id por que se escribira*/
	descripcion varchar(45)
)
go

insert into departamentos(iddepartamento, descripcion) values
('01', 'Lima'),
('02', 'Ica')
select * from departamentos

create table provincias (
	idprovincia varchar(4) not null,
	provincia varchar(45) not null,
	iddepartamento varchar(20) not null
)
go

insert into provincias (idprovincia, provincia, iddepartamento) values
-- lima
('001', 'lima', '01'),
-- ica
('002', 'chincha', '02'),
('002', 'pisco', '02')
select * from provincias

create table distritos (
	iddistrito varchar(6) not null,
	descripcion varchar(45) not null,
	idprovincia varchar(4) not null,
	iddepartamento varchar(2) not null
)
go

insert into distritos (iddistrito, descripcion, idprovincia, iddepartamento) values
-- lima
('0001', 'miraflores', '001', '01'),
-- ica
('0002', 'chincha alta', '002', '02'),
('0002', 'san clemente', '002', '02')
select * from distritos
go
