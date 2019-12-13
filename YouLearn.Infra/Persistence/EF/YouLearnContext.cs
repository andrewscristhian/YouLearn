using Microsoft.EntityFrameworkCore;
using prmToolkit.NotificationPattern;
using YouLearn.Domain.Entities;
using YouLearn.Domain.ValueObjects;
using YouLearn.Infra.Persistence.EF.Map;
using YouLearn.Shared;

namespace YouLearn.Infra.Persistence.EF
{
    public class YouLearnContext : DbContext
    {
        public DbSet<Canal> Canais { get; set; }
        public DbSet<PlayList> PlayLists { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        //public DbSet<Favorito> Favoritos { get; set; }

        // Configurar acesso ao BD
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Settings.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            //Ignorar essas classes pois sao objetos de valor, evitando assim que o EF crie-os como tabela
            modelBuilder.Ignore<Notification>();
            modelBuilder.Ignore<Nome>();
            modelBuilder.Ignore<Email>();

            //Aplicar configuracoes
            modelBuilder.ApplyConfiguration(new MapCanal());
            modelBuilder.ApplyConfiguration(new MapPlayList());
            modelBuilder.ApplyConfiguration(new MapVideo());
            modelBuilder.ApplyConfiguration(new MapUsuario());
            //modelBuilder.ApplyConfiguration(new MapFavorito());

            base.OnModelCreating(modelBuilder);
        }
    }
}
