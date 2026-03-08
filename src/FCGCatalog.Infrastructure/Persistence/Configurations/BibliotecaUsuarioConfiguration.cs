using FCGCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCGCatalog.Infrastructure.Persistence.Configurations;

public class BibliotecaUsuarioConfiguration : IEntityTypeConfiguration<BibliotecaUsuario>
{
	public void Configure(EntityTypeBuilder<BibliotecaUsuario> builder)
	{
		builder.ToTable("biblioteca_usuarios");

		// Chave primária
		builder.HasKey(b => b.Id);

		builder.Property(b => b.Id)
			.HasColumnName("id")
			.HasColumnType("uuid")
			.ValueGeneratedNever();

		// UsuarioId
		builder.Property(b => b.UsuarioId)
			.HasColumnName("usuario_id")
			.HasColumnType("uuid")
			.IsRequired();

		builder.HasIndex(b => b.UsuarioId)
			.HasDatabaseName("ix_biblioteca_usuario_usuario_id");

		// JogoId
		builder.Property(b => b.JogoId)
			.HasColumnName("jogo_id")
			.HasColumnType("uuid")
			.IsRequired();

		// DataCompra
		builder.Property(b => b.DataCompra)
			.HasColumnName("data_compra")
			.HasColumnType("timestamp with time zone")
			.IsRequired();

		// Relacionamento com Jogo
		builder.HasOne(b => b.Jogo)
			.WithMany()
			.HasForeignKey(b => b.JogoId)
			.OnDelete(DeleteBehavior.Restrict);

		// Índice único (um usuário não pode ter o mesmo jogo duas vezes)
		builder.HasIndex(b => new { b.UsuarioId, b.JogoId })
			.IsUnique()
			.HasDatabaseName("ux_biblioteca_usuario_jogo");
	}
}