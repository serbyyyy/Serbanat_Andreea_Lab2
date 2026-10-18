using Microsoft.EntityFrameworkCore;

public class Serbanat_Andreea_Lab2Context(DbContextOptions<Serbanat_Andreea_Lab2Context> options) : DbContext(options)
{
    public DbSet<Serbanat_Andreea_Lab2.Models.Book> Book { get; set; } = default!;

    public DbSet<Serbanat_Andreea_Lab2.Models.Publisher> Publisher { get; set; } = default!;

    public DbSet<Serbanat_Andreea_Lab2.Models.Authors> Authors { get; set; } = default!;


}
