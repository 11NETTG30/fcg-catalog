using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCGCatalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedJogos : Migration
    {
        private static readonly DateTime DataCriacao = new(2026, 3, 22, 0, 0, 0, DateTimeKind.Utc);

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "jogos",
                columns: new[] { "id", "titulo", "descricao", "preco", "data_lancamento", "ativo", "data_criacao" },
                values: new object[,]
                {
                    {
                        new Guid("10000000-0000-0000-0000-000000000001"),
                        "The Last of Us Part I",
                        "Em um mundo pós-apocalíptico devastado por um fungo parasita, Joel conduz Ellie por uma América em ruínas. Uma jornada brutal sobre sobrevivência, amor paternal e sacrifício.",
                        249.90m,
                        new DateTime(2022, 9, 2, 0, 0, 0, DateTimeKind.Utc),
                        true,
                        DataCriacao
                    },
                    {
                        new Guid("10000000-0000-0000-0000-000000000002"),
                        "Red Dead Redemption 2",
                        "Prelúdio épico ao clássico western. Viva a vida de Arthur Morgan na última era dos foragidos, enfrentando a inevitável chegada da modernização ao velho oeste americano.",
                        199.90m,
                        new DateTime(2018, 10, 26, 0, 0, 0, DateTimeKind.Utc),
                        true,
                        DataCriacao
                    },
                    {
                        new Guid("10000000-0000-0000-0000-000000000003"),
                        "God of War Ragnarök",
                        "Kratos e Atreus enfrentam o apocalipse nórdico. Uma aventura épica pelos nove reinos com batalhas deslumbrantes, narrativa emocional profunda e confronto com deuses escandinavos.",
                        299.90m,
                        new DateTime(2022, 11, 9, 0, 0, 0, DateTimeKind.Utc),
                        true,
                        DataCriacao
                    },
                    {
                        new Guid("10000000-0000-0000-0000-000000000004"),
                        "Cyberpunk 2077",
                        "Night City, 2077. Como V, mercenário à beira da morte com um chip de memória de uma lenda, navegue por uma metrópole cyberpunk corrompida em busca de imortalidade e identidade.",
                        179.90m,
                        new DateTime(2020, 12, 10, 0, 0, 0, DateTimeKind.Utc),
                        true,
                        DataCriacao
                    },
                    {
                        new Guid("10000000-0000-0000-0000-000000000005"),
                        "Elden Ring",
                        "No Intermundo, o Anel do Ancião foi destruído. Como Sem-brilho, explore um vasto mundo aberto repleto de desafios épicos, criado por Hidetaka Miyazaki e George R.R. Martin.",
                        249.90m,
                        new DateTime(2022, 2, 25, 0, 0, 0, DateTimeKind.Utc),
                        true,
                        DataCriacao
                    },
                    {
                        new Guid("10000000-0000-0000-0000-000000000006"),
                        "Grand Theft Auto V",
                        "Los Santos, cidade de sonhos e crimes. Controle três criminosos em missões eletrizantes, explorando um mundo aberto monumental com liberdade total de ação e caos.",
                        79.90m,
                        new DateTime(2013, 9, 17, 0, 0, 0, DateTimeKind.Utc),
                        true,
                        DataCriacao
                    },
                    {
                        new Guid("10000000-0000-0000-0000-000000000007"),
                        "EA Sports FC 24",
                        "A nova era do futebol virtual. Com tecnologia HyperMotionV, simule a experiência do futebol real com os melhores clubes e seleções do mundo em campo.",
                        299.90m,
                        new DateTime(2023, 9, 29, 0, 0, 0, DateTimeKind.Utc),
                        true,
                        DataCriacao
                    },
                    {
                        new Guid("10000000-0000-0000-0000-000000000008"),
                        "Hogwarts Legacy",
                        "1890, mundo bruxo. Explore Hogwarts, Hogsmeade e arredores em um RPG de ação ambientado séculos antes de Harry Potter. Domine a magia e descubra uma antiga conspiração.",
                        299.90m,
                        new DateTime(2023, 2, 10, 0, 0, 0, DateTimeKind.Utc),
                        true,
                        DataCriacao
                    },
                    {
                        new Guid("10000000-0000-0000-0000-000000000009"),
                        "Marvel's Spider-Man: Miles Morales",
                        "Miles Morales assume o manto do Homem-Aranha enquanto Peter Parker viaja. Defenda o Harlem contra o Tinkerer e a Roxxon, descobrindo seus poderes únicos e identidade como herói.",
                        249.90m,
                        new DateTime(2020, 11, 12, 0, 0, 0, DateTimeKind.Utc),
                        true,
                        DataCriacao
                    },
                    {
                        new Guid("10000000-0000-0000-0000-000000000010"),
                        "Horizon Forbidden West",
                        "Aloy parte para o Oeste Proibido, terras dominadas por máquinas corruptas e tribos hostis, para descobrir a origem de uma praga mortal que ameaça toda a vida na Terra.",
                        299.90m,
                        new DateTime(2022, 2, 18, 0, 0, 0, DateTimeKind.Utc),
                        true,
                        DataCriacao
                    },
                    {
                        new Guid("10000000-0000-0000-0000-000000000011"),
                        "Batman: Arkham Knight",
                        "Gotham sob ataque. Scarecrow reúne os maiores vilões da cidade e revela o Cavaleiro de Arkham. Pilote o Batmóvel e enfrente a maior ameaça já enfrentada pelo Homem-Morcego.",
                        69.90m,
                        new DateTime(2015, 6, 23, 0, 0, 0, DateTimeKind.Utc),
                        true,
                        DataCriacao
                    },
                    {
                        new Guid("10000000-0000-0000-0000-000000000012"),
                        "The Witcher 3: Wild Hunt",
                        "Geralt de Rívia busca sua filha adotiva Ciri, perseguida pela Caçada Selvagem. RPG colossal com mais de 100 horas de conteúdo, escolhas morais e mundo extraordinariamente detalhado.",
                        149.90m,
                        new DateTime(2015, 5, 19, 0, 0, 0, DateTimeKind.Utc),
                        true,
                        DataCriacao
                    },
                    {
                        new Guid("10000000-0000-0000-0000-000000000013"),
                        "Minecraft",
                        "Construa, explore e sobreviva em um mundo gerado proceduralmente de blocos infinitos. Do modo criativo ao survival, é o jogo sandbox mais popular e vendido da história dos games.",
                        129.90m,
                        new DateTime(2011, 11, 18, 0, 0, 0, DateTimeKind.Utc),
                        true,
                        DataCriacao
                    },
                    {
                        new Guid("10000000-0000-0000-0000-000000000014"),
                        "Among Us",
                        "Você é tripulante ou impostor? Complete tarefas enquanto tenta descobrir quem está sabotando a missão — ou engane todos para vencer como impostor nesta experiência social.",
                        19.90m,
                        new DateTime(2018, 6, 15, 0, 0, 0, DateTimeKind.Utc),
                        true,
                        DataCriacao
                    },
                    {
                        new Guid("10000000-0000-0000-0000-000000000015"),
                        "Stardew Valley",
                        "Herde a fazenda do seu avô e comece uma nova vida no campo. Plante, cultive, cuide de animais, explore minas e construa relacionamentos na charmosa cidade de Pelican Town.",
                        39.90m,
                        new DateTime(2016, 2, 26, 0, 0, 0, DateTimeKind.Utc),
                        true,
                        DataCriacao
                    },
                    {
                        new Guid("10000000-0000-0000-0000-000000000016"),
                        "Hollow Knight",
                        "Descubra os segredos do enorme reino subterrâneo de insetos de Hallownest. Um metroidvania desafiador e atmosférico com arte artesanal e trilha sonora cativante.",
                        49.90m,
                        new DateTime(2017, 2, 24, 0, 0, 0, DateTimeKind.Utc),
                        true,
                        DataCriacao
                    },
                    {
                        new Guid("10000000-0000-0000-0000-000000000017"),
                        "Hades",
                        "Zagreus, filho do Deus do Submundo, tenta escapar do Inferno contra a vontade do pai. Roguelite frenético com narrativa dinâmica e incontáveis combinações de habilidades divinas.",
                        64.90m,
                        new DateTime(2020, 9, 17, 0, 0, 0, DateTimeKind.Utc),
                        true,
                        DataCriacao
                    },
                    {
                        new Guid("10000000-0000-0000-0000-000000000018"),
                        "It Takes Two",
                        "Josef e Cody, casal em crise, são transformados em bonecos pela filha e devem cooperar para se tornarem humanos novamente. Coletânea criativa de mecânicas cooperativas únicas.",
                        149.90m,
                        new DateTime(2021, 3, 26, 0, 0, 0, DateTimeKind.Utc),
                        true,
                        DataCriacao
                    },
                    {
                        new Guid("10000000-0000-0000-0000-000000000019"),
                        "Control",
                        "Jesse Faden assume o Bureau of Control após uma invasão paranormal. Explore o Oldest House, edifício brutalist e sobrenatural, usando poderes telequinéticos e combate intenso.",
                        119.90m,
                        new DateTime(2019, 8, 27, 0, 0, 0, DateTimeKind.Utc),
                        true,
                        DataCriacao
                    },
                    {
                        new Guid("10000000-0000-0000-0000-000000000020"),
                        "Sekiro: Shadows Die Twice",
                        "No Japão feudal sobrenatural, Wolf busca resgatar seu jovem senhor sequestrado e se vingar. Um desafio implacável de precisão, postura e maestria no combate de espadas.",
                        199.90m,
                        new DateTime(2019, 3, 22, 0, 0, 0, DateTimeKind.Utc),
                        true,
                        DataCriacao
                    },
                    {
                        new Guid("10000000-0000-0000-0000-000000000021"),
                        "Death Stranding",
                        "Sam Porter Bridges percorre uma América devastada entregando cargas e reconectando comunidades isoladas. Experiência única de Hideo Kojima sobre conexão humana e esperança.",
                        149.90m,
                        new DateTime(2019, 11, 8, 0, 0, 0, DateTimeKind.Utc),
                        true,
                        DataCriacao
                    },
                    {
                        new Guid("10000000-0000-0000-0000-000000000022"),
                        "Returnal",
                        "Selene Vassos aterrissa em um planeta alienígena e fica presa em um loop temporal mortal. Roguelite de tiro em terceira pessoa com mecânicas bullet-hell e narrativa psicológica.",
                        299.90m,
                        new DateTime(2021, 4, 30, 0, 0, 0, DateTimeKind.Utc),
                        true,
                        DataCriacao
                    },
                    {
                        new Guid("10000000-0000-0000-0000-000000000023"),
                        "Forza Horizon 5",
                        "O Festival Horizon chega ao México. Explore um mapa vasto e variado com mais de 500 carros licenciados, corridas, desafios e o festival de automobilismo mais bonito dos games.",
                        299.90m,
                        new DateTime(2021, 11, 9, 0, 0, 0, DateTimeKind.Utc),
                        true,
                        DataCriacao
                    },
                    {
                        new Guid("10000000-0000-0000-0000-000000000024"),
                        "Halo Infinite",
                        "Master Chief retorna para enfrentar os Banidos na ringworld Zeta Halo. O retorno às raízes da série com campanha de mundo semiaberto e multiplayer gratuito.",
                        249.90m,
                        new DateTime(2021, 12, 8, 0, 0, 0, DateTimeKind.Utc),
                        true,
                        DataCriacao
                    },
                    {
                        new Guid("10000000-0000-0000-0000-000000000025"),
                        "Alan Wake 2",
                        "O escritor Alan Wake está preso em um pesadelo sombrio há 13 anos enquanto a detetive Saga Anderson investiga assassinatos rituais em Bright Falls. Horror psicológico narrativo de tirar o fôlego.",
                        249.90m,
                        new DateTime(2023, 10, 27, 0, 0, 0, DateTimeKind.Utc),
                        true,
                        DataCriacao
                    }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM jogos
                WHERE id IN (
                    '10000000-0000-0000-0000-000000000001',
                    '10000000-0000-0000-0000-000000000002',
                    '10000000-0000-0000-0000-000000000003',
                    '10000000-0000-0000-0000-000000000004',
                    '10000000-0000-0000-0000-000000000005',
                    '10000000-0000-0000-0000-000000000006',
                    '10000000-0000-0000-0000-000000000007',
                    '10000000-0000-0000-0000-000000000008',
                    '10000000-0000-0000-0000-000000000009',
                    '10000000-0000-0000-0000-000000000010',
                    '10000000-0000-0000-0000-000000000011',
                    '10000000-0000-0000-0000-000000000012',
                    '10000000-0000-0000-0000-000000000013',
                    '10000000-0000-0000-0000-000000000014',
                    '10000000-0000-0000-0000-000000000015',
                    '10000000-0000-0000-0000-000000000016',
                    '10000000-0000-0000-0000-000000000017',
                    '10000000-0000-0000-0000-000000000018',
                    '10000000-0000-0000-0000-000000000019',
                    '10000000-0000-0000-0000-000000000020',
                    '10000000-0000-0000-0000-000000000021',
                    '10000000-0000-0000-0000-000000000022',
                    '10000000-0000-0000-0000-000000000023',
                    '10000000-0000-0000-0000-000000000024',
                    '10000000-0000-0000-0000-000000000025'
                )
            ");
        }
    }
}
