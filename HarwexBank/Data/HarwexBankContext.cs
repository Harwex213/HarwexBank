using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Configuration;

#nullable disable

namespace HarwexBank
{
    // Migrations
    // ----------
    // Add-Migration [migration name]
    // Update-Database
    // ----------
    
    // Database First
    // ----------
    // Scaffold-DbContext "[connection string]" Microsoft.EntityFrameworkCore.SqlServer
    // ----------
    
    public sealed class HarwexBankContext : DbContext
    {
        public HarwexBankContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
            
            CreateDefaultData();
        }

        #region DbSet Init

        // User & UserType tables.
        public DbSet<UserModel> Users { get; set; }
        public DbSet<UserTypeModel> UserTypes { get; set; }
        
        // Data belongs to User:
        // Credit & CreditType tables.
        public DbSet<IssuedCreditModel> IssuedCredits { get; set; }
        public DbSet<CreditTypeModel> CreditTypes { get; set; }
        
        // Account & CurrencyType tables.
        public DbSet<AccountModel> Accounts { get; set; }
        public DbSet<CurrencyTypeModel> CurrencyTypes { get; set; }
        
        // Data belongs to Account:
        // Card & CardType tables.
        public DbSet<CardModel> Cards { get; set; }
        public DbSet<CardTypeModel> CardTypes { get; set; }
        
        // Operations data:
        // Operation baseclass.
        // Child classes: CreditRepayment; TransferToAccount;
        public DbSet<JournalModel> Journal { get; set; }
        public DbSet<NotificationModel> Notifications { get; set; }
        public DbSet<OperationModel> Operations { get; set; }
        public DbSet<CreditRepaymentModel> CreditRepayments { get; set; }
        public DbSet<TransferToAccountModel> TransferToAccounts { get; set; }

        #endregion
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");
            
            modelBuilder.Entity<UserModel>(UserConfigure);
            modelBuilder.Entity<UserTypeModel>(UserTypeConfigure);
            
            modelBuilder.Entity<IssuedCreditModel>(IssuedCreditConfigure);
            modelBuilder.Entity<CreditTypeModel>(CreditTypeConfigure);
            
            modelBuilder.Entity<AccountModel>(AccountConfigure);
            modelBuilder.Entity<CurrencyTypeModel>(CurrencyTypeConfigure);

            modelBuilder.Entity<CardModel>(CardConfigure);
            modelBuilder.Entity<CardTypeModel>(CardTypeConfigure);

            modelBuilder.Entity<JournalModel>(JournalConfigure);
            modelBuilder.Entity<NotificationModel>(NotificationConfigure);
            modelBuilder.Entity<OperationModel>(OperationConfigure);
            modelBuilder.Entity<CreditRepaymentModel>(CreditRepaymentConfigure);
            modelBuilder.Entity<TransferToAccountModel>(TransferToAccountConfigure);
        }

        #region Configures

        private void UserConfigure(EntityTypeBuilder<UserModel> entity)
        {
            entity.ToTable("USER");
            
            // Columns.
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);
            
            entity.Property(e => e.Patronymic).HasMaxLength(50);

            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(50);
            
            entity.Property(e => e.Passport)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Login).HasMaxLength(50);
            
            entity.Property(e => e.Password).HasMaxLength(50);
            
            // References.
            entity.HasAlternateKey(e => e.Login);
            
            entity.HasOne(d => d.UserTypeModelNavigation)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.UserType)
                .HasPrincipalKey(t=> t.Name)
                .HasConstraintName("USER_TYPE_FK");
        }
        private void UserTypeConfigure(EntityTypeBuilder<UserTypeModel> entity)
        {
            entity.ToTable("USER_TYPE");
            
            // Columns.
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
        private void AccountConfigure(EntityTypeBuilder<AccountModel> entity)
        {
            entity.ToTable("ACCOUNT");

            // Columns.
            entity.Property(e => e.Amount).HasColumnType("money");

            entity.Property(e => e.RegistrationDate).HasColumnType("date");

            // References.
            entity.HasOne(d => d.CurrencyTypeModelNavigation)
                .WithMany(p => p.Accounts)
                .HasForeignKey(d => d.CurrencyType)
                .HasPrincipalKey(t=> t.Name)
                .HasConstraintName("ACCOUNT_CURRENCY_FK");

            entity.HasOne(d => d.UserModelAccount)
                .WithMany(p => p.Accounts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("ACCOUNT_USER_FK");
        }
        private void CurrencyTypeConfigure(EntityTypeBuilder<CurrencyTypeModel> entity)
        {
            entity.ToTable("CURRENCY_TYPE");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
        private void CardConfigure(EntityTypeBuilder<CardModel> entity)
        {
            entity.ToTable("CARD");

            // Columns.
            entity.Property(e => e.Cvv)
                .IsRequired()
                .HasMaxLength(3)
                .IsFixedLength(true);

            entity.Property(e => e.OwnerName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Number).HasMaxLength(16);

            entity.Property(e => e.TimeFrame)
                .IsRequired()
                .HasMaxLength(5)
                .IsFixedLength(true);

            // References.
            entity.HasOne(d => d.BankAccountModel)
                .WithMany(p => p.Cards)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("CARD_USER_FK");

            entity.HasOne(d => d.CardTypeModelNavigation)
                .WithMany(p => p.Cards)
                .HasForeignKey(d => d.CardType)
                .HasPrincipalKey(t=> t.Name)
                .HasConstraintName("CARD_TYPE_FK");
        }
        private void CardTypeConfigure(EntityTypeBuilder<CardTypeModel> entity)
        {
            entity.ToTable("CARD_TYPE");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
        private void IssuedCreditConfigure(EntityTypeBuilder<IssuedCreditModel> entity)
        {
            entity.ToTable("ISSUED_CREDITS");

            // Columns.
            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.RepaidAmount).HasColumnType("money");

            entity.Property(e => e.DateIn).HasColumnType("date");

            // References.
            entity.HasOne(d => d.CreditTypeModelNavigation)
                .WithMany(p => p.IssuedCredits)
                .HasForeignKey(d => d.CreditType)
                .HasPrincipalKey(t=> t.Name)
                .HasConstraintName("ISSUED_CREDITS_TYPE_FK");

            entity.HasOne(d => d.UserModelAccount)
                .WithMany(p => p.IssuedCredits)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("ISSUED_CREDITS_USER_FK");
        }
        private void CreditTypeConfigure(EntityTypeBuilder<CreditTypeModel> entity)
        {
            entity.ToTable("CREDIT_TYPE");

            // Columns.
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
        private void JournalConfigure(EntityTypeBuilder<JournalModel> entity)
        {
            entity.ToTable("JOURNAL");

            entity.Property(e => e.Date).IsRequired();
            // References.
            entity.HasOne(d => d.UserIdNavigation)
                .WithMany(p => p.Journal)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("JOURNAL_TO_USER_RECEIVER_FK");
        }
        private void NotificationConfigure(EntityTypeBuilder<NotificationModel> entity)
        {
            entity.ToTable("NOTIFICATIONS");
            
            entity.Property(e => e.Message)
                .IsRequired()
                .HasMaxLength(50);
        }
        private void OperationConfigure(EntityTypeBuilder<OperationModel> entity)
        {
            entity.ToTable("OPERATION");

            // Columns.
            entity.Property(e => e.Amount).HasColumnType("money");

            entity.Property(e => e.Date).HasColumnType("date");
            
            // References.
            entity.HasOne(d => d.BankAccountModelInitiatorNavigation)
                .WithMany(p => p.Operations)
                .HasForeignKey(d => d.BankAccountInitiator)
                .HasConstraintName("OPERATION_ACCOUNT_INIT_FK");
        }
        private void CreditRepaymentConfigure(EntityTypeBuilder<CreditRepaymentModel> entity)
        {
            entity.ToTable("CREDIT_REPAYMENT");
            
            // References.
            entity.HasOne(d => d.SelectedCreditModelNavigation)
                .WithMany(p => p.CreditRepayments)
                .HasForeignKey(d => d.SelectedCredit)
                .HasConstraintName("CREDIT_REPAYMENT_SELECTED_CREDIT_FK");
        }
        private void TransferToAccountConfigure(EntityTypeBuilder<TransferToAccountModel> entity)
        {
            entity.ToTable("TRANSFER_TO_ACCOUNT");
            
            // References.
            entity.HasOne(d => d.BankAccountModelReceiverNavigation)
                .WithMany(p => p.TransferToAccounts)
                .HasForeignKey(d => d.BankAccountReceiver)
                .HasConstraintName("TRANSFER_TO_ACCOUNT_RECEIVER_FK");
        }

        #endregion

        private void CreateDefaultData()
        {
            UserTypes.AddRange(new List<UserTypeModel>
            {
                new() { Name = "admin"},
                new() { Name = "worker"},
                new() { Name = "client"}
            });

            var userOleg = new UserModel
            {
                FirstName = "Oleg",
                LastName = "Kaportsev",
                Patronymic = "Andreevich",
                Address = "Vitebsk",
                Passport = "BM1234576",
                UserType = "client",
                Login = "oleg",
                Password = "1111",
                IsBlocked = false
            };
            var userSergey = new UserModel
            {
                FirstName = "Sergey",
                LastName = "Turov",
                Address = "Vitebsk",
                Passport = "BM1234576",
                UserType = "client",
                Login = "sergey",
                Password = "1111",
                IsBlocked = false
            };
            var userIgor = new UserModel
            {
                FirstName = "Igor",
                LastName = "Skvortsoff",
                Address = "Vitebsk",
                Passport = "BM1234576",
                UserType = "worker",
                Login = "igor",
                Password = "1111",
                IsBlocked = false
            };
            
            Users.AddRange(new List<UserModel>
            {
                userOleg, userSergey, userIgor
            });
            
            SaveChanges();

            CurrencyTypes.AddRange(new List<CurrencyTypeModel>
            {
                new() { Name = "RUB" },
                new() { Name = "USD" },
                new() { Name = "BYN" }
            });
            
            Accounts.AddRange(new List<AccountModel>
            {
                new()
                {
                    UserId = userOleg.Id,
                    CurrencyType = "BYN",
                    RegistrationDate = DateTime.Today,
                    Amount = 74812m,
                    IsFrozen = true
                },
                new()
                {
                    UserId = userOleg.Id,
                    CurrencyType = "USD",
                    RegistrationDate = DateTime.Today,
                    Amount = 9994451m,
                    IsFrozen = false
                },
                new()
                {
                    UserId = userSergey.Id,
                    CurrencyType = "RUB",
                    RegistrationDate = DateTime.Today,
                    Amount = 897556m,
                    IsFrozen = false
                }
            });
            
            SaveChanges();
            
            CardTypes.AddRange(new List<CardTypeModel>
            {
                new() { Name = "Visa Standard" },
                new() { Name = "Visa Classic" },
                new() { Name = "Visa Gold" }
            });
            
            Cards.AddRange(new List<CardModel>
            {
                new()
                {
                    AccountId = 1,
                    CardType = "Visa Standard",
                    Number = "6700110010504715",
                    OwnerName = "ALEH KAPORTSAU",
                    TimeFrame = "08/22",
                    Cvv = "999"
                },
                new()
                {
                    AccountId = 2,
                    CardType = "Visa Gold",
                    Number = "6700110010509999",
                    OwnerName = "ALEH KAPORTSAU",
                    TimeFrame = "08/23",
                    Cvv = "000"
                },
                new()
                {
                    AccountId = 3,
                    CardType = "Visa Gold",
                    Number = "6700110010507777",
                    OwnerName = "SERGEY TUROV",
                    TimeFrame = "08/23",
                    Cvv = "000"
                }
            });
            
            SaveChanges();
            
            CreditTypes.AddRange(new List<CreditTypeModel>
            {
                new() {Name = "Студенческий", Rate = 0.13m },
                new() {Name = "Потребительский", Rate = 0.25m},
                new() {Name = "Корпоративный", Rate = 0.1m}
            });
            
            IssuedCredits.AddRange(new List<IssuedCreditModel>
            {
                new()
                {
                    UserId = userOleg.Id,
                    CreditType = "Корпоративный",
                    DateIn = DateTime.Today,
                    Term = 5,
                    Amount = 20000m,
                    IsApproved = true,
                    IsRepaid = false
                },
                new()
                {
                    UserId = userOleg.Id,
                    CreditType = "Студенческий",
                    DateIn = DateTime.Today,
                    Term = 2,
                    Amount = 4000m,
                    IsApproved = false,
                    IsRepaid = false
                }
            });
            
            SaveChanges();
        }
    }
}
