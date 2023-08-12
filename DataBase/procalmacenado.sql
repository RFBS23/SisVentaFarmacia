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