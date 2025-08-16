namespace Wallet.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Wallet.Domain.Entities;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    #region Tables
    public DbSet<User> Users { get; set; }
    public DbSet<UserAddress> UserAddresses { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<TransactionBankInfo> TransactionBankInfos { get; set; }
    public DbSet<Province> Provinces { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<Neighborhood> Neighborhoods { get; set; }
    public DbSet<Outbox> Outboxes { get; set; }
    public DbSet<Agreement> Agreements { get; set; }
    public DbSet<Parameter> Parameters { get; set; }
    public DbSet<Blacklist> Blacklists { get; set; }
    public DbSet<Receipt> Receipts { get; set; }
    public DbSet<SavedTransfer> SavedTransfers { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Tüm entity’ler için Id primary key
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var idProp = entityType.FindProperty("Id");
            if (idProp != null)
            {
                entityType.SetPrimaryKey(idProp);
                modelBuilder.Entity(entityType.ClrType)
                    .Property("Id")
                    .HasDefaultValueSql("gen_random_uuid()");
            }
        }

        #region User
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(11);
        });
        #endregion

        #region UserAddress
        modelBuilder.Entity<UserAddress>(entity =>
        {
            entity.HasOne(e => e.User)
                  .WithMany(u => u.Addresses)
                  .HasForeignKey(e => e.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
        #endregion

        #region Wallet
        modelBuilder.Entity<Wallet>(entity =>
        {
            entity.HasOne(e => e.User)
                  .WithMany(u => u.Wallets)
                  .HasForeignKey(e => e.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.WalletNumber).IsUnique();

            entity.Property(e => e.PayableBalance).HasPrecision(18, 2);
            entity.Property(e => e.TransferableBalance).HasPrecision(18, 2);
            entity.Property(e => e.WalletNumber).HasDefaultValueSql("nextval('wallet_number_sequence')");
        });
        #endregion

        #region Transaction
        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasOne(e => e.Wallet)
                  .WithMany(w => w.Transactions)
                  .HasForeignKey(e => e.WalletId);

            entity.HasOne(e => e.TransactionBankInfo)
                  .WithMany()
                  .HasForeignKey(e => e.TransactionBankInfoId);

            entity.HasOne(e => e.Receipt)
                  .WithMany()
                  .HasForeignKey(e => e.ReceiptId);

            entity.Property(e => e.PayableInitialBalance).HasPrecision(18, 2);
            entity.Property(e => e.PayableAmount).HasPrecision(18, 2);
            entity.Property(e => e.TransferableInitialBalance).HasPrecision(18, 2);
            entity.Property(e => e.TransferableAmount).HasPrecision(18, 2);
        });
        #endregion

        #region TransactionBankInfo
        modelBuilder.Entity<TransactionBankInfo>(entity =>
        {
            entity.Property(e => e.PayeeIban).HasMaxLength(26);
            entity.Property(e => e.BankCommissionAmount).HasPrecision(18, 2);
        });
        #endregion

        #region Province, District, Neighborhood
        modelBuilder.Entity<Province>(entity =>
        {
            entity.Property(e => e.Name).IsRequired();
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.Property(e => e.Name).IsRequired();
        });

        modelBuilder.Entity<Neighborhood>(entity =>
        {
            entity.Property(e => e.Name).IsRequired();
        });
        #endregion

        #region Outbox
        modelBuilder.Entity<Outbox>(entity =>
        {
            entity.Property(e => e.Type).IsRequired(false);
        });
        #endregion

        #region Agreement
        modelBuilder.Entity<Agreement>(entity =>
        {
            entity.Property(e => e.Version).HasMaxLength(20);
        });
        #endregion

        #region Parameter
        modelBuilder.Entity<Parameter>(entity =>
        {
            entity.Property(e => e.ParamType).HasMaxLength(20);
        });
        #endregion

        #region Blacklist
        modelBuilder.Entity<Blacklist>(entity =>
        {
            entity.HasOne(e => e.User)
                  .WithMany(u => u.Blacklists)
                  .HasForeignKey(e => e.UserId);

            entity.Property(e => e.Channel).HasMaxLength(100);
        });
        #endregion

        #region Receipt
        modelBuilder.Entity<Receipt>(entity =>
        {
            entity.HasOne(e => e.Transaction)
                  .WithOne(t => t.Receipt)
                  .HasForeignKey<Receipt>(e => e.TransactionId);

            entity.Property(e => e.NumberPrefix).HasMaxLength(3);
            entity.Property(e => e.Number).HasMaxLength(9);
        });
        #endregion

        #region SavedTransfer
        modelBuilder.Entity<SavedTransfer>(entity =>
        {
            entity.HasOne(e => e.Wallet)
                  .WithMany(w => w.SavedTransfers)
                  .HasForeignKey(e => e.WalletId);

            entity.Property(e => e.Identifier).HasMaxLength(26);
            entity.Property(e => e.DisplayName).HasMaxLength(50);
            entity.Property(e => e.RecipientFullName).HasMaxLength(200);
        });
        #endregion
    }
}
