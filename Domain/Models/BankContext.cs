using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class BankContext : DbContext
    {
        public BankContext()
        {
        }

        public BankContext(DbContextOptions<BankContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<AccountStatus> AccountStatuses { get; set; } = null!;
        public virtual DbSet<AccountType> AccountTypes { get; set; } = null!;
        public virtual DbSet<Card> Cards { get; set; } = null!;
        public virtual DbSet<Chat> Chats { get; set; } = null!;
        public virtual DbSet<Currency> Currencies { get; set; } = null!;
        public virtual DbSet<Deposit> Deposits { get; set; } = null!;
        public virtual DbSet<DepositStatus> DepositStatuses { get; set; } = null!;
        public virtual DbSet<DepositType> DepositTypes { get; set; } = null!;
        public virtual DbSet<Document> Documents { get; set; } = null!;
        public virtual DbSet<DocumentType> DocumentTypes { get; set; } = null!;
        public virtual DbSet<File> Files { get; set; } = null!;
        public virtual DbSet<Loan> Loans { get; set; } = null!;
        public virtual DbSet<LoanStatus> LoanStatuses { get; set; } = null!;
        public virtual DbSet<LoanType> LoanTypes { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<MessageStatus> MessageStatuses { get; set; } = null!;
        public virtual DbSet<OperationHistory> OperationHistories { get; set; } = null!;
        public virtual DbSet<OperationStatus> OperationStatuses { get; set; } = null!;
        public virtual DbSet<OperationType> OperationTypes { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Tred> Treds { get; set; } = null!;
        public virtual DbSet<TypeOfCard> TypeOfCards { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.HasIndex(e => e.ClientId, "IX_Relationship48");

                entity.HasIndex(e => e.TypeId, "IX_Relationship49");

                entity.HasIndex(e => e.CurrencyId, "IX_Relationship51");

                entity.HasIndex(e => e.StatusId, "IX_Relationship59");

                entity.Property(e => e.AccountId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("AccountID");

                entity.Property(e => e.Balance).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.ClientId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ClientID");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_at");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship48");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship51");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship59");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship49");

            });

            modelBuilder.Entity<AccountStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId);

                entity.ToTable("AccountStatus");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(true);
            });

            modelBuilder.Entity<AccountType>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.ToTable("AccountType");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Card>(entity =>
            {
                entity.ToTable("Card");

                entity.HasIndex(e => e.TypeId, "IX_Relationship2");

                entity.HasIndex(e => e.CurrencyId, "IX_Relationship4");

                entity.HasIndex(e => e.AccountId, "IX_Relationship50");

                entity.Property(e => e.CardId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CardID");

                entity.Property(e => e.AccountId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("AccountID");

                entity.Property(e => e.Balance).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.BlockedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Blocked_at");

                entity.Property(e => e.CardNumber)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_at");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.Cvv)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CVV");

                entity.Property(e => e.ExpDate).HasColumnType("date");

                entity.Property(e => e.OwnerName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship50");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship4");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship2");
            });

            modelBuilder.Entity<Chat>(entity =>
            {
                entity.ToTable("Chat");

                entity.Property(e => e.ChatId).HasColumnName("ChatID");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_at");
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("Currency");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.Course).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.Name)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Deposit>(entity =>
            {
                entity.ToTable("Deposit");

                entity.HasIndex(e => e.StatusId, "IX_Relationship11");

                entity.HasIndex(e => e.DocumentId, "IX_Relationship37");

                entity.HasIndex(e => e.AccountId, "IX_Relationship57");

                entity.HasIndex(e => e.DepositTypeId, "IX_Relationship9");

                entity.Property(e => e.DepositId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("DepositID");

                entity.Property(e => e.AccountId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("AccountID");

                entity.Property(e => e.DepositTypeId).HasColumnName("DepositTypeID");

                entity.Property(e => e.DocumentId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("DocumentID");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Deposits)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship57");

                entity.HasOne(d => d.DepositType)
                    .WithMany(p => p.Deposits)
                    .HasForeignKey(d => d.DepositTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship9");

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.Deposits)
                    .HasForeignKey(d => d.DocumentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship37");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Deposits)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship11");
            });

            modelBuilder.Entity<DepositStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId);

                entity.ToTable("DepositStatus");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DepositType>(entity =>
            {
                entity.ToTable("DepositType");

                entity.Property(e => e.DepositTypeId).HasColumnName("DepositTypeID");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_at");

                entity.Property(e => e.InterestRate).HasColumnType("decimal(2, 1)");

                entity.Property(e => e.MinAmount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.ToTable("Document");

                entity.HasIndex(e => e.ClientId, "IX_Relationship27");

                entity.HasIndex(e => e.TypeId, "IX_Relationship28");

                entity.Property(e => e.DocumentId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("DocumentID");

                entity.Property(e => e.ClientId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ClientID");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_at");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Path)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship27");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship28");
            });

            modelBuilder.Entity<DocumentType>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.ToTable("DocumentType");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<File>(entity =>
            {
                entity.HasIndex(e => e.MessageId, "IX_Relationship26");

                entity.HasIndex(e => e.ClientId, "IX_Relationship47");

                entity.Property(e => e.FileId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FileID");

                entity.Property(e => e.ClientId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ClientID");

                entity.Property(e => e.FilePath)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.Property(e => e.UploadAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Upload_at");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Files)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship47");

                entity.HasOne(d => d.Message)
                    .WithMany(p => p.Files)
                    .HasForeignKey(d => d.MessageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship26");
            });

            modelBuilder.Entity<Loan>(entity =>
            {
                entity.ToTable("Loan");

                entity.HasIndex(e => e.DocumentId, "IX_Relationship36");

                entity.HasIndex(e => e.AccountId, "IX_Relationship58");

                entity.HasIndex(e => e.LoanTypeId, "IX_Relationship7");

                entity.HasIndex(e => e.StatusId, "IX_Relationship8");

                entity.Property(e => e.LoanId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("LoanID");

                entity.Property(e => e.AccountId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("AccountID");

                entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.DocumentId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("DocumentID");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.LoanTypeId).HasColumnName("LoanTypeID");

                entity.Property(e => e.RemarningAmount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Loans)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("Relationship58");

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.Loans)
                    .HasForeignKey(d => d.DocumentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship36");

                entity.HasOne(d => d.LoanType)
                    .WithMany(p => p.Loans)
                    .HasForeignKey(d => d.LoanTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship7");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Loans)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship8");
            });

            modelBuilder.Entity<LoanStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId);

                entity.ToTable("LoanStatus");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LoanType>(entity =>
            {
                entity.ToTable("LoanType");

                entity.Property(e => e.LoanTypeId).HasColumnName("LoanTypeID");

                entity.Property(e => e.InterestRate).HasColumnType("decimal(2, 1)");

                entity.Property(e => e.MaxLoanAmount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message");

                entity.HasIndex(e => e.StatusId, "IX_Relationship21");

                entity.HasIndex(e => e.TredId, "IX_Relationship44");

                entity.HasIndex(e => e.ClientId, "IX_Relationship46");

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.Property(e => e.ClientId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ClientID");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_at");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.TredId).HasColumnName("TredID");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(1024)
                    .IsUnicode(true);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship46");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship21");

                entity.HasOne(d => d.Tred)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.TredId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship44");
            });

            modelBuilder.Entity<MessageStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId);

                entity.ToTable("MessageStatus");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OperationHistory>(entity =>
            {
                entity.HasKey(e => e.OperationId);

                entity.ToTable("OperationHistory");

                entity.HasIndex(e => e.SenderCardId, "IX_Relationship17");

                entity.HasIndex(e => e.TypeId, "IX_Relationship18");

                entity.HasIndex(e => e.StatusId, "IX_Relationship19");

                entity.HasIndex(e => e.SenderAccountId, "IX_Relationship52");

                entity.HasIndex(e => e.DestinationAccountId, "IX_Relationship55");

                entity.HasIndex(e => e.DestinationCardId, "IX_Relationship56");

                entity.Property(e => e.OperationId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("OperationID");

                entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DestinationAccountId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("DestinationAccountID");

                entity.Property(e => e.DestinationCardId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("DestinationCardID");

                entity.Property(e => e.SenderAccountId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SenderAccountID");

                entity.Property(e => e.SenderCardId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SenderCardID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.DestinationAccount)
                    .WithMany(p => p.OperationHistoryDestinationAccounts)
                    .HasForeignKey(d => d.DestinationAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship55");

                entity.HasOne(d => d.DestinationCard)
                    .WithMany(p => p.OperationHistoryDestinationCards)
                    .HasForeignKey(d => d.DestinationCardId)
                    .HasConstraintName("Relationship56");

                entity.HasOne(d => d.SenderAccount)
                    .WithMany(p => p.OperationHistorySenderAccounts)
                    .HasForeignKey(d => d.SenderAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship52");

                entity.HasOne(d => d.SenderCard)
                    .WithMany(p => p.OperationHistorySenderCards)
                    .HasForeignKey(d => d.SenderCardId)
                    .HasConstraintName("Relationship17");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.OperationHistories)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship19");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.OperationHistories)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship18");
            });

            modelBuilder.Entity<OperationStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId);

                entity.ToTable("OperationStatus");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OperationType>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.ToTable("OperationType");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);


            });

            modelBuilder.Entity<Tred>(entity =>
            {
                entity.ToTable("Tred");

                entity.HasIndex(e => e.ChatId, "IX_Relationship43");

                entity.HasIndex(e => e.OperatorId, "IX_Relationship45");

                entity.Property(e => e.TredId).HasColumnName("TredID");

                entity.Property(e => e.ChatId).HasColumnName("ChatID");

                entity.Property(e => e.ClosedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Closed_at");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_at");

                entity.Property(e => e.OperatorId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("OperatorID");

                entity.HasOne(d => d.Chat)
                    .WithMany(p => p.Treds)
                    .HasForeignKey(d => d.ChatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship43");

                entity.HasOne(d => d.Operator)
                    .WithMany(p => p.Treds)
                    .HasForeignKey(d => d.OperatorId)
                    .HasConstraintName("Relationship45");
            });

            modelBuilder.Entity<TypeOfCard>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.ToTable("Type_of_card");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.TypeName)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.ClientId);

                entity.HasIndex(e => e.RoleId, "IX_Relationship3");

                entity.HasIndex(e => e.ChatId, "IX_Relationship39");

                entity.Property(e => e.ClientId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ClientID");

                entity.Property(e => e.ChatId).HasColumnName("ChatID");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_at");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_at");

                entity.Property(e => e.DeletedBy).HasColumnName("Deleted_by");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(true);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(true);

                entity.Property(e => e.Login)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Patronomic)
                    .HasMaxLength(50)
                    .IsUnicode(true);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_at");

                entity.HasOne(d => d.Chat)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.ChatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship39");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship3");

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}