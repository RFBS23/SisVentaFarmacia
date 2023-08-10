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