
using CalculatorAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
namespace CalculatorAPI.Data
{
    public class CalculatorDbContext :DbContext
    {
        public CalculatorDbContext(DbContextOptions<CalculatorDbContext> options)
            : base(options) { }

        public DbSet<Calculation> Calculations { get; set; }
        public DbSet<Operation> Operations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Operation>()
                .HasKey(o => o.Id); //  explicitly set primary key


            var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
            v => v.ToUniversalTime(),   // store as UTC
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc) // read as UTC
        );

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())  // for created at
        {
            foreach (var property in entityType.GetProperties()
                         .Where(p => p.ClrType == typeof(DateTime)))
            {
                property.SetValueConverter(dateTimeConverter);
            }
}

modelBuilder.Entity<Calculation>().ToTable("calculations");
modelBuilder.Entity<Operation>().ToTable("operations");

modelBuilder.Entity<Calculation>(entity =>
            {
                entity.ToTable("calculations");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
        entity.Property(e => e.OperandA).HasColumnName("operand_a");
        entity.Property(e => e.OperandB).HasColumnName("operand_b");
        entity.Property(e => e.OperationId).HasColumnName("operation_id");
        entity.Property(e => e.Operator).HasColumnName("operator");
        entity.Property(e => e.Result).HasColumnName("result");
        entity.HasKey(e => e.Id);
                entity.Property(e => e.SessionId).HasColumnName("session_id");
                entity.Property(e => e.Id).HasColumnName("id");
    });

            modelBuilder.Entity<Operation>(entity =>
            {
                entity.ToTable("operations");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");       //  map to lowercase
    entity.Property(e => e.Name).HasColumnName("name");
    entity.Property(e => e.Symbol).HasColumnName("symbol");
});


        }




}





}

