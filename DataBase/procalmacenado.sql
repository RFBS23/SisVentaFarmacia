use sistemafarmacia
go

select * from usuarios;
go

create procedure spu_registrarusuario_user (
	@nombreusuario varchar(100),
	@correo varchar(50),
	@clave varchar(90),
	@estado bit,
	@mensaje varchar(60) output,
	@resultado int output
)
as
begin
	set @resultado = 0
	if not exists (select * from usuarios where correo = @correo)
	begin
		insert into usuarios(nombreusuario, correo, clave, estado) values
			(@nombreusuario, @correo, @clave, @estado)
			set @resultado = SCOPE_IDENTITY()
		end
		else
		set @mensaje = 'El correo ya se encuentra registrado'
end
go

create procedure spu_editarusuario_user (
	@idusuario int,
	@nombreusuario varchar(100),
	@correo varchar(50),
	@estado bit,
	@mensaje varchar(60) output,
	@resultado bit output
)
as
begin
	set @resultado = 0
	if not exists (select * from usuarios where correo = @correo and idusuario!= @idusuario)
	begin
		update top (1) usuarios set
		nombreusuario = @nombreusuario,
		correo = @correo,
		estado = @estado
		where idusuario = @idusuario
		set @resultado = 1
	end
	else
	set @mensaje = 'El correo ya se encuentra registrado'
end
go

--categoria
create proc spu_registrarcategoria_categoria(
	@descripcion varchar(100),
	@estado bit,
	@mensaje varchar(60) output,
	@resultado int output
)
as
begin
	set @resultado = 0
	if not exists (select * from categorias where descripcion = @descripcion)
	begin
		insert into categorias(descripcion, estado) values (@descripcion, @estado)
		set @resultado = SCOPE_IDENTITY()
	end
	else
	set @mensaje = 'La Categoria ya existe'
end
go

create proc spu_editarcategoria_categoria(
	@idcategoria int,
	@descripcion varchar(100),
	@estado bit,
	@mensaje varchar(100) output,
	@resultado int output
)
as
begin
	set @resultado = 0
	if not exists (select * from categorias where descripcion = @descripcion and idcategoria != @idcategoria)
	begin
		update top (1) categorias set
		descripcion = @descripcion,
		estado = @estado
		where idcategoria = @idcategoria
		set @resultado = 1
	end
	else
	set @mensaje = 'La categoria ya existe'
end
go

create proc spu_eliminarcategoria_categoria(
	@idcategoria int,
	@mensaje varchar(60) output,
	@resultado bit output
)
as
begin
	set @resultado = 0
	if not exists (select * from productos p inner join categorias c on c.idcategoria = p.idcategoria
					where p.idcategoria = @idcategoria)
	begin
		delete top (1) from categorias where idcategoria = @idcategoria
		set @resultado = 1
	end
	else
	set @mensaje = 'La Categoria Tiene un Producto Relacionado'
end
go

select * from categorias;

-- marcas
create proc spu_registrarmarca_marca(
	@descripcion varchar(100),
	@estado bit,
	@mensaje varchar(60) output,
	@resultado int output
)
as
begin
	set @resultado = 0
	if not exists (select * from marcas where descripcion = @descripcion)
	begin
		insert into marcas (descripcion, estado) values (@descripcion, @estado)
		set @resultado = SCOPE_IDENTITY()
	end
	else
	set @mensaje = 'La marca ya existe'
end
go

create proc spu_editarmarca_marca(
	@idmarca int,
	@descripcion varchar(100),
	@estado bit,
	@mensaje varchar(60) output,
	@resultado bit output
)
as
begin
	set @resultado = 0
	if not exists (select * from marcas where descripcion = @descripcion and idmarca != @idmarca)
	begin
		update top (1) marcas set
		descripcion = @descripcion,
		estado = @estado
		where idmarca = @idmarca
		set @resultado = 1
	end
	else
		set @mensaje = 'La marca ya existe'
end
go

create proc spu_eliminarmarca(
	@idmarca int,
	@mensaje varchar(60) output,
	@resultado bit output
)
as
begin
	set @resultado = 0
	if not exists (select * from productos p inner join marcas m on m.idmarca = p.idmarca where p.idmarca = @idmarca)
	begin
		delete top(1) from marcas where idmarca = @idmarca
		set @resultado = 1
	end
	else
	set @mensaje = 'La marca esta realciona con un producto'
end
go

select * from marcas
go

-- productos
create proc spu_registrar_productos(
	@idmarca varchar(100),
	@idcategoria varchar(100),
	@nombre varchar(100),
	@Descripcion varchar(500),	
	@precio decimal(10,2),
	@stock int,
	@estado bit,
	@mensaje varchar(60) output,
	@resultado int output
)
as
begin
	set @resultado = 0
	if not exists (select * from productos where nombre = @nombre)
	begin
		insert into productos(idmarca, idcategoria, nombre, Descripcion, precio, stock, estado) values
		(@idmarca, @idcategoria, @nombre, @Descripcion, @precio, @stock, @estado)
		set @resultado = SCOPE_IDENTITY()
	end
	else
	set @mensaje = 'El producto ya existe'
end
go

create proc spu_editar_productos(
	@idproducto int,
	@idmarca varchar(100),
	@idcategoria varchar(100),
	@nombre varchar(100),
	@Descripcion varchar(500),	
	@precio decimal(10,2),
	@stock int,
	@estado bit,
	@mensaje varchar(60) output,
	@resultado int output
)
as
begin
	set @resultado = 0
	if not exists (select * from productos where nombre = @nombre and idproducto != @idproducto)
	begin
		update productos set
		idmarca = @idmarca,
		idcategoria = @idcategoria,
		nombre = @nombre,
		Descripcion = @Descripcion,
		precio = @precio,
		stock = @stock,
		estado = @estado
		where idproducto = @idproducto
		set @resultado = 1
	end
	else
	set @mensaje = 'El producto ya existe'
end
go

create proc spu_eliminar_producto(
	@idproducto int,
	@mensaje varchar(60) output,
	@resultado bit output
)
as
begin
	set @resultado = 0
	if not exists (select * from detallesVentas dv inner join productos p on p.idproducto = dv.idproducto where p.idproducto = @idproducto)
	begin
		delete top (1) from productos where idproducto = @idproducto
		set @resultado = 1
	end
	else
	set @mensaje = 'El producto se encuentra relacionado a una venta'
end
go

select p.idproducto, p.nombre, p.Descripcion, m.idmarca, m.descripcion[desmarca], c.idcategoria, c.descripcion[descategoria], p.precio, p.stock, p.rutaimg, p.nombreimg, p.estado from productos p
inner join marcas m on m.idmarca = p.idmarca
inner join categorias c on c.idcategoria = p.idcategoria

-- obtener total de clientes y ventas y productos
select * from clientes

-- reportes
create proc spu_reporte
as
begin
	select
		(select count(*) from clientes) [TotalCliente],
		(select isnull(sum(cantidad), 0) from detallesVentas) [TotalVenta],
		(select count(*) from productos) [TotalProducto]
end
go
exec spu_reporte
go

-- reporte de venta
create proc spu_reporteVentas(
	@fechainicio varchar(10),
	@fechafin varchar(10),
	@idtransaccion varchar(50)
)
as
begin
	set dateformat dmy;

	select convert(char(10),v.fechaventa,103)[FechaVenta], CONCAT(c.nombres, '', c.apellidos)[Clientes], p.nombre [Producto], p.precio, dv.cantidad, dv.total, v.idtransaccion
	from detallesVentas dv
	inner join productos p on p.idproducto = dv.idproducto
	inner join ventas v on v.idventa = dv.idventa
	inner join clientes c on c.idcliente = v.idcliente

	where convert(date, v.fechaventa) between @fechainicio and @fechafin
	and v.idtransaccion = iif(@idtransaccion = '', v.idtransaccion, @idtransaccion)
end
go