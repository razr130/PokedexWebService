namespace PokemonAPI.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PokedexModel : DbContext
    {
        public PokedexModel()
            : base("name=PokedexModel")
        {
        }

        public virtual DbSet<move> moves { get; set; }
        public virtual DbSet<pokedex> pokedexes { get; set; }
        public virtual DbSet<pokemon_type> pokemon_type { get; set; }
        public virtual DbSet<stat> stats { get; set; }
        public virtual DbSet<type> types { get; set; }

        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<move>()
                .Property(e => e.move_name)
                .IsUnicode(false);

            modelBuilder.Entity<move>()
                .Property(e => e.move_category)
                .IsUnicode(false);

            modelBuilder.Entity<move>()
                .Property(e => e.move_effect)
                .IsUnicode(false);

            modelBuilder.Entity<pokedex>()
                .Property(e => e.pokedex_id)
                .IsUnicode(false);

            modelBuilder.Entity<pokedex>()
                .Property(e => e.pokemon_name)
                .IsUnicode(false);

            modelBuilder.Entity<pokedex>()
                .Property(e => e.species)
                .IsUnicode(false);

            modelBuilder.Entity<pokedex>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<pokedex>()
                .Property(e => e.abilities)
                .IsUnicode(false);

            modelBuilder.Entity<pokedex>()
                .HasMany(e => e.stats)
                .WithOptional(e => e.pokedex)
                .WillCascadeOnDelete();

            modelBuilder.Entity<pokemon_type>()
                .Property(e => e.pokedex_id)
                .IsUnicode(false);

            modelBuilder.Entity<stat>()
                .Property(e => e.pokedex_id)
                .IsUnicode(false);

            modelBuilder.Entity<type>()
                .Property(e => e.type1)
                .IsUnicode(false);

            modelBuilder.Entity<type>()
                .HasMany(e => e.moves)
                .WithOptional(e => e.type)
                .HasForeignKey(e => e.move_type);
        }
    }
}
