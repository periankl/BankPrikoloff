using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Domain.Migrations
{
    public partial class MigratonName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountStatus",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountStatus", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "AccountType",
                columns: table => new
                {
                    TypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountType", x => x.TypeID);
                });

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    ChatID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.ChatID);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    CurrencyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Course = table.Column<decimal>(type: "decimal(10,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.CurrencyID);
                });

            migrationBuilder.CreateTable(
                name: "DepositStatus",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepositStatus", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "DepositType",
                columns: table => new
                {
                    DepositTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    InterestRate = table.Column<decimal>(type: "decimal(2,1)", nullable: false),
                    MinAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    MinTerm = table.Column<int>(type: "int", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    Deleted_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepositType", x => x.DepositTypeID);
                });

            migrationBuilder.CreateTable(
                name: "DocumentType",
                columns: table => new
                {
                    TypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentType", x => x.TypeID);
                });

            migrationBuilder.CreateTable(
                name: "LoanStatus",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanStatus", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "LoanType",
                columns: table => new
                {
                    LoanTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    InterestRate = table.Column<decimal>(type: "decimal(2,1)", nullable: false),
                    MaxLoanAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanType", x => x.LoanTypeID);
                });

            migrationBuilder.CreateTable(
                name: "MessageStatus",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageStatus", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "OperationStatus",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationStatus", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "OperationType",
                columns: table => new
                {
                    TypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationType", x => x.TypeID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Type_of_card",
                columns: table => new
                {
                    TypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type_of_card", x => x.TypeID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ClientID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    Login = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    Password = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    FirstName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Patronomic = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false),
                    SeriesPasport = table.Column<int>(type: "int", nullable: false),
                    NumberPasport = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ChatID = table.Column<int>(type: "int", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    Deleted_by = table.Column<int>(type: "int", nullable: true),
                    Deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ClientID);
                    table.ForeignKey(
                        name: "Relationship3",
                        column: x => x.RoleID,
                        principalTable: "Role",
                        principalColumn: "RoleID");
                    table.ForeignKey(
                        name: "Relationship39",
                        column: x => x.ChatID,
                        principalTable: "Chat",
                        principalColumn: "ChatID");
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountID = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    ClientID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    TypeID = table.Column<int>(type: "int", nullable: false),
                    CurrencyID = table.Column<int>(type: "int", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountID);
                    table.ForeignKey(
                        name: "Relationship48",
                        column: x => x.ClientID,
                        principalTable: "Users",
                        principalColumn: "ClientID");
                    table.ForeignKey(
                        name: "Relationship49",
                        column: x => x.TypeID,
                        principalTable: "AccountType",
                        principalColumn: "TypeID");
                    table.ForeignKey(
                        name: "Relationship51",
                        column: x => x.CurrencyID,
                        principalTable: "Currency",
                        principalColumn: "CurrencyID");
                    table.ForeignKey(
                        name: "Relationship59",
                        column: x => x.StatusID,
                        principalTable: "AccountStatus",
                        principalColumn: "StatusID");
                });

            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    DocumentID = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    ClientID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    TypeID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Path = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    Deleted_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.DocumentID);
                    table.ForeignKey(
                        name: "Relationship27",
                        column: x => x.ClientID,
                        principalTable: "Users",
                        principalColumn: "ClientID");
                    table.ForeignKey(
                        name: "Relationship28",
                        column: x => x.TypeID,
                        principalTable: "DocumentType",
                        principalColumn: "TypeID");
                });

            migrationBuilder.CreateTable(
                name: "Tred",
                columns: table => new
                {
                    TredID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatID = table.Column<int>(type: "int", nullable: false),
                    OperatorID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false),
                    Closed_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tred", x => x.TredID);
                    table.ForeignKey(
                        name: "Relationship43",
                        column: x => x.ChatID,
                        principalTable: "Chat",
                        principalColumn: "ChatID");
                    table.ForeignKey(
                        name: "Relationship45",
                        column: x => x.OperatorID,
                        principalTable: "Users",
                        principalColumn: "ClientID");
                });

            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    CardID = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    TypeID = table.Column<int>(type: "int", nullable: false),
                    CurrencyID = table.Column<int>(type: "int", nullable: false),
                    AccountID = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    CardNumber = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: false),
                    ExpDate = table.Column<DateTime>(type: "date", nullable: false),
                    CVV = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: false),
                    OwnerName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Blocked = table.Column<bool>(type: "bit", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    Blocked_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.CardID);
                    table.ForeignKey(
                        name: "Relationship2",
                        column: x => x.TypeID,
                        principalTable: "Type_of_card",
                        principalColumn: "TypeID");
                    table.ForeignKey(
                        name: "Relationship4",
                        column: x => x.CurrencyID,
                        principalTable: "Currency",
                        principalColumn: "CurrencyID");
                    table.ForeignKey(
                        name: "Relationship50",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                });

            migrationBuilder.CreateTable(
                name: "Deposit",
                columns: table => new
                {
                    DepositID = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    DepositTypeID = table.Column<int>(type: "int", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false),
                    DocumentID = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    AccountID = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposit", x => x.DepositID);
                    table.ForeignKey(
                        name: "Relationship11",
                        column: x => x.StatusID,
                        principalTable: "DepositStatus",
                        principalColumn: "StatusID");
                    table.ForeignKey(
                        name: "Relationship37",
                        column: x => x.DocumentID,
                        principalTable: "Document",
                        principalColumn: "DocumentID");
                    table.ForeignKey(
                        name: "Relationship57",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                    table.ForeignKey(
                        name: "Relationship9",
                        column: x => x.DepositTypeID,
                        principalTable: "DepositType",
                        principalColumn: "DepositTypeID");
                });

            migrationBuilder.CreateTable(
                name: "Loan",
                columns: table => new
                {
                    LoanID = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    AccountID = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    LoanTypeID = table.Column<int>(type: "int", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false),
                    DocumentID = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    RemarningAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loan", x => x.LoanID);
                    table.ForeignKey(
                        name: "Relationship36",
                        column: x => x.DocumentID,
                        principalTable: "Document",
                        principalColumn: "DocumentID");
                    table.ForeignKey(
                        name: "Relationship58",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                    table.ForeignKey(
                        name: "Relationship7",
                        column: x => x.LoanTypeID,
                        principalTable: "LoanType",
                        principalColumn: "LoanTypeID");
                    table.ForeignKey(
                        name: "Relationship8",
                        column: x => x.StatusID,
                        principalTable: "LoanStatus",
                        principalColumn: "StatusID");
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    MessageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusID = table.Column<int>(type: "int", nullable: false),
                    TredID = table.Column<int>(type: "int", nullable: false),
                    ClientID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.MessageID);
                    table.ForeignKey(
                        name: "Relationship21",
                        column: x => x.StatusID,
                        principalTable: "MessageStatus",
                        principalColumn: "StatusID");
                    table.ForeignKey(
                        name: "Relationship44",
                        column: x => x.TredID,
                        principalTable: "Tred",
                        principalColumn: "TredID");
                    table.ForeignKey(
                        name: "Relationship46",
                        column: x => x.ClientID,
                        principalTable: "Users",
                        principalColumn: "ClientID");
                });

            migrationBuilder.CreateTable(
                name: "OperationHistory",
                columns: table => new
                {
                    OperationID = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    SenderAccountID = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    SenderCardID = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DestinationAccountID = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    DestinationCardID = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    TypeID = table.Column<int>(type: "int", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationHistory", x => x.OperationID);
                    table.ForeignKey(
                        name: "Relationship17",
                        column: x => x.SenderCardID,
                        principalTable: "Card",
                        principalColumn: "CardID");
                    table.ForeignKey(
                        name: "Relationship18",
                        column: x => x.TypeID,
                        principalTable: "OperationType",
                        principalColumn: "TypeID");
                    table.ForeignKey(
                        name: "Relationship19",
                        column: x => x.StatusID,
                        principalTable: "OperationStatus",
                        principalColumn: "StatusID");
                    table.ForeignKey(
                        name: "Relationship52",
                        column: x => x.SenderAccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                    table.ForeignKey(
                        name: "Relationship55",
                        column: x => x.DestinationAccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                    table.ForeignKey(
                        name: "Relationship56",
                        column: x => x.DestinationCardID,
                        principalTable: "Card",
                        principalColumn: "CardID");
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    FileID = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    FilePath = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    MessageID = table.Column<int>(type: "int", nullable: false),
                    ClientID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Upload_at = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.FileID);
                    table.ForeignKey(
                        name: "Relationship26",
                        column: x => x.MessageID,
                        principalTable: "Message",
                        principalColumn: "MessageID");
                    table.ForeignKey(
                        name: "Relationship47",
                        column: x => x.ClientID,
                        principalTable: "Users",
                        principalColumn: "ClientID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Relationship48",
                table: "Account",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship49",
                table: "Account",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship51",
                table: "Account",
                column: "CurrencyID");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship59",
                table: "Account",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship2",
                table: "Card",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship4",
                table: "Card",
                column: "CurrencyID");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship50",
                table: "Card",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship11",
                table: "Deposit",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship37",
                table: "Deposit",
                column: "DocumentID");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship57",
                table: "Deposit",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship9",
                table: "Deposit",
                column: "DepositTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship27",
                table: "Document",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship28",
                table: "Document",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship26",
                table: "Files",
                column: "MessageID");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship47",
                table: "Files",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship36",
                table: "Loan",
                column: "DocumentID");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship58",
                table: "Loan",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship7",
                table: "Loan",
                column: "LoanTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship8",
                table: "Loan",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship21",
                table: "Message",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship44",
                table: "Message",
                column: "TredID");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship46",
                table: "Message",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship17",
                table: "OperationHistory",
                column: "SenderCardID");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship18",
                table: "OperationHistory",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship19",
                table: "OperationHistory",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship52",
                table: "OperationHistory",
                column: "SenderAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship55",
                table: "OperationHistory",
                column: "DestinationAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship56",
                table: "OperationHistory",
                column: "DestinationCardID");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship43",
                table: "Tred",
                column: "ChatID");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship45",
                table: "Tred",
                column: "OperatorID");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship3",
                table: "Users",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship39",
                table: "Users",
                column: "ChatID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deposit");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Loan");

            migrationBuilder.DropTable(
                name: "OperationHistory");

            migrationBuilder.DropTable(
                name: "DepositStatus");

            migrationBuilder.DropTable(
                name: "DepositType");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Document");

            migrationBuilder.DropTable(
                name: "LoanType");

            migrationBuilder.DropTable(
                name: "LoanStatus");

            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.DropTable(
                name: "OperationType");

            migrationBuilder.DropTable(
                name: "OperationStatus");

            migrationBuilder.DropTable(
                name: "MessageStatus");

            migrationBuilder.DropTable(
                name: "Tred");

            migrationBuilder.DropTable(
                name: "DocumentType");

            migrationBuilder.DropTable(
                name: "Type_of_card");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "AccountType");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "AccountStatus");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Chat");
        }
    }
}