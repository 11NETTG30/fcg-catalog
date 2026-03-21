using FCGCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCGCatalog.Infrastructure.Persistence.Configurations;

public class JogoConfiguration : IEntityTypeConfiguration<Jogo>
{
	public void Configure(EntityTypeBuilder<Jogo> builder)
	{
		builder.ToTable("jogos");

		// Chave primária
		builder.HasKey(j => j.Id);

		builder.Property(j => j.Id)
			.HasColumnName("id")
			.HasColumnType("uuid")
			.ValueGeneratedNever();

		// Titulo
		builder.Property(j => j.Titulo)
			.HasColumnName("titulo")
			.HasMaxLength(200)
			.IsRequired();

		builder.HasIndex(j => j.Titulo)
			.IsUnique()
			.HasDatabaseName("ux_jogos_titulo");

		// Descricao
		builder.Property(j => j.Descricao)
			.HasColumnName("descricao")
			.HasMaxLength(1000);

		// Preco (Value Object)
		builder.OwnsOne(j => j.Preco, preco =>
		{
			preco.Property(p => p.Valor)
				.HasColumnName("preco")
				.HasColumnType("numeric(10,2)")
				.IsRequired();
		});

		builder.Navigation(j => j.Preco).IsRequired();

		// DataLancamento
		builder.Property(j => j.DataLancamento)
			.HasColumnName("data_lancamento")
			.HasColumnType("timestamp with time zone");

		// Ativo
		builder.Property(j => j.Ativo)
			.HasColumnName("ativo")
			.HasDefaultValue(true)
			.IsRequired();

		// DataCriacao
		builder.Property(j => j.DataCriacao)
			.HasColumnName("data_criacao")
			.HasColumnType("timestamp with time zone")
			.IsRequired()
			.ValueGeneratedNever();

		// DataAtualizacao
		builder.Property(j => j.DataAtualizacao)
			.HasColumnName("data_atualizacao")
			.HasColumnType("timestamp with time zone");
	}
}
