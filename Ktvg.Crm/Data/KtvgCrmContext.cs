using Microsoft.EntityFrameworkCore;

public class KtvgCrmContext : DbContext
{
    public KtvgCrmContext(DbContextOptions<KtvgCrmContext> options)
        : base(options)
    {
    }

    public DbSet<Ktvg.Crm.Models.ContactProject> ContactProject { get; set; } = default!;

    public DbSet<Ktvg.Crm.Models.ContactPurpose> ContactPurpose { get; set; } = default!;

    public DbSet<Ktvg.Crm.Models.Customer> Customer { get; set; } = default!;

    public DbSet<Ktvg.Crm.Models.Employee> Employee { get; set; } = default!;

    public DbSet<Ktvg.Crm.Models.LoginHistory> LoginHistory { get; set; } = default!;

    public DbSet<Ktvg.Crm.Models.MessageLog> MessageLog { get; set; } = default!;

    public DbSet<Ktvg.Crm.Models.ZaloOAuth> ZaloOAuth { get; set; } = default!;

    public DbSet<Ktvg.Crm.Models.ContactHistory> ContactHistory { get; set; } = default!;
}