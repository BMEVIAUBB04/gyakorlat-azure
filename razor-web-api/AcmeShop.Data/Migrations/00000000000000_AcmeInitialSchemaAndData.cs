using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AcmeShop.Data.Migrations
{
    public partial class AcmeInitialSchemaAndData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Afa",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kulcs = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Afa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FizetesMod",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mod = table.Column<string>(maxLength: 20, nullable: true),
                    Hatarido = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FizetesMod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kategoria",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nev = table.Column<string>(maxLength: 50, nullable: true),
                    SzuloKategoriaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kategoria_Kategoria_SzuloKategoriaId",
                        column: x => x.SzuloKategoriaId,
                        principalTable: "Kategoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Statusz",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nev = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statusz", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SzamlaKiallito",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nev = table.Column<string>(maxLength: 50, nullable: true),
                    Iranyitoszam = table.Column<string>(unicode: false, fixedLength: true, maxLength: 4, nullable: true),
                    Varos = table.Column<string>(maxLength: 50, nullable: true),
                    Utca = table.Column<string>(maxLength: 50, nullable: true),
                    Adoszam = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Szamlaszam = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SzamlaKiallito", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Termek",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nev = table.Column<string>(maxLength: 50, nullable: true),
                    NettoAr = table.Column<double>(nullable: true),
                    Raktarkeszlet = table.Column<int>(nullable: true),
                    AfaId = table.Column<int>(nullable: true),
                    KategoriaId = table.Column<int>(nullable: true),
                    Leiras = table.Column<string>(type: "xml", nullable: true),
                    Kep = table.Column<byte[]>(type: "image", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Termek", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Termek_Afa_AfaId",
                        column: x => x.AfaId,
                        principalTable: "Afa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Termek_Kategoria_KategoriaId",
                        column: x => x.KategoriaId,
                        principalTable: "Kategoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Megrendeles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<DateTime>(nullable: true),
                    Hatarido = table.Column<DateTime>(nullable: true),
                    TelephelyId = table.Column<int>(nullable: true),
                    StatuszId = table.Column<int>(nullable: true),
                    FizetesModId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Megrendeles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Megrendeles_FizetesMod_FizetesModId",
                        column: x => x.FizetesModId,
                        principalTable: "FizetesMod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Megrendeles_Statusz_StatuszId",
                        column: x => x.StatuszId,
                        principalTable: "Statusz",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MegrendelesTetel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mennyiseg = table.Column<int>(nullable: true),
                    NettoAr = table.Column<double>(nullable: true),
                    MegrendelesId = table.Column<int>(nullable: true),
                    TermekId = table.Column<int>(nullable: true),
                    StatuszId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MegrendelesTetel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MegrendelesTetel_Megrendeles_MegrendelesId",
                        column: x => x.MegrendelesId,
                        principalTable: "Megrendeles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MegrendelesTetel_Statusz_StatuszId",
                        column: x => x.StatuszId,
                        principalTable: "Statusz",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MegrendelesTetel_Termek_TermekId",
                        column: x => x.TermekId,
                        principalTable: "Termek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Szamla",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MegrendeloNev = table.Column<string>(maxLength: 50, nullable: true),
                    MegrendeloIranyitoszam = table.Column<string>(unicode: false, fixedLength: true, maxLength: 4, nullable: true),
                    MegrendeloVaros = table.Column<string>(maxLength: 50, nullable: true),
                    MegrendeloUtca = table.Column<string>(maxLength: 50, nullable: true),
                    NyomtatottPeldanyszam = table.Column<int>(nullable: true),
                    Sztorno = table.Column<bool>(nullable: true),
                    FizetesiMod = table.Column<string>(maxLength: 20, nullable: true),
                    KiallitasDatum = table.Column<DateTime>(nullable: true),
                    TeljesitesDatum = table.Column<DateTime>(nullable: true),
                    FizetesiHatarido = table.Column<DateTime>(nullable: true),
                    KiallitoId = table.Column<int>(nullable: true),
                    MegrendelesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Szamla", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Szamla_SzamlaKiallito_KiallitoId",
                        column: x => x.KiallitoId,
                        principalTable: "SzamlaKiallito",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Szamla_Megrendeles_MegrendelesId",
                        column: x => x.MegrendelesId,
                        principalTable: "Megrendeles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SzamlaTetel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nev = table.Column<string>(maxLength: 50, nullable: true),
                    Mennyiseg = table.Column<int>(nullable: true),
                    NettoAr = table.Column<double>(nullable: true),
                    Afakulcs = table.Column<int>(nullable: true),
                    SzamlaId = table.Column<int>(nullable: true),
                    MegrendelesTetelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SzamlaTetel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SzamlaTetel_MegrendelesTetel_MegrendelesTetelId",
                        column: x => x.MegrendelesTetelId,
                        principalTable: "MegrendelesTetel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SzamlaTetel_Szamla_SzamlaId",
                        column: x => x.SzamlaId,
                        principalTable: "Szamla",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vevo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nev = table.Column<string>(maxLength: 50, nullable: true),
                    Szamlaszam = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Login = table.Column<string>(maxLength: 50, nullable: true),
                    Jelszo = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    KozpontiTelephelyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vevo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Telephely",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Iranyitoszam = table.Column<string>(unicode: false, fixedLength: true, maxLength: 4, nullable: true),
                    Varos = table.Column<string>(maxLength: 50, nullable: true),
                    Utca = table.Column<string>(maxLength: 50, nullable: true),
                    Telefonszam = table.Column<string>(unicode: false, maxLength: 15, nullable: true),
                    Fax = table.Column<string>(unicode: false, maxLength: 15, nullable: true),
                    VevoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telephely", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Telephely_Vevo_VevoId",
                        column: x => x.VevoId,
                        principalTable: "Vevo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Afa",
                columns: new[] { "Id", "Kulcs" },
                values: new object[,]
                {
                    { 1, 0 },
                    { 2, 15 },
                    { 3, 20 }
                });

            migrationBuilder.InsertData(
                table: "FizetesMod",
                columns: new[] { "Id", "Hatarido", "Mod" },
                values: new object[,]
                {
                    { 1, 0, "Készpénz" },
                    { 2, 8, "Átutalás 8" },
                    { 3, 15, "Átutalás 15" },
                    { 4, 30, "Átutalás 30" },
                    { 5, 0, "Kártya" },
                    { 6, 0, "Utánvét" }
                });

            migrationBuilder.InsertData(
                table: "Kategoria",
                columns: new[] { "Id", "Nev", "SzuloKategoriaId" },
                values: new object[,]
                {
                    { 2, "Játszóház", null },
                    { 1, "Játék", null }
                });

            migrationBuilder.InsertData(
                table: "Statusz",
                columns: new[] { "Id", "Nev" },
                values: new object[,]
                {
                    { 1, "Rögzítve" },
                    { 2, "Várakozik" },
                    { 3, "Csomagolva" },
                    { 4, "Szállítás alatt" },
                    { 5, "Kiszállítva" }
                });

            migrationBuilder.InsertData(
                table: "SzamlaKiallito",
                columns: new[] { "Id", "Adoszam", "Iranyitoszam", "Nev", "Szamlaszam", "Utca", "Varos" },
                values: new object[,]
                {
                    { 1, "15684995-2-32", "1119", "Regio Játék Áruház Kft", "259476332-15689799-10020065", "Nándorfejérvári u. 23", "Budapest" },
                    { 2, "68797867-1-32", "1119", "Regio Játék Áruház Zrt", "259476332-15689799-10020065", "Nándorfejérvári u. 23", "Budapest" }
                });

            migrationBuilder.InsertData(
                table: "Vevo",
                columns: new[] { "Id", "Email", "Jelszo", "KozpontiTelephelyId", "Login", "Nev", "Szamlaszam" },
                values: new object[,]
                {
                    { 2, "hajdunagyk@hotmail.com", "gandalf67j", null, "katinka", "Hajdú-Nagy Katalin", "54255831-15615432-25015126" },
                    { 1, "puskasnorbert@freemail.hu", "huti9haj1s", null, "pnorbert", "Puskás Norbert", "16489665-05899845-10000038" },
                    { 3, "janos.grosz@gmail.com", "jag7guFs", null, "jano", "Grosz János", "25894467-12005362-59815126" }
                });

            migrationBuilder.InsertData(
                table: "Kategoria",
                columns: new[] { "Id", "Nev", "SzuloKategoriaId" },
                values: new object[,]
                {
                    { 3, "Bébijáték", 1 },
                    { 4, "Építőjáték", 1 },
                    { 5, "Fajáték", 1 },
                    { 6, "Plüss figurák", 1 },
                    { 7, "Közlekedési eszközök", 1 }
                });

            migrationBuilder.InsertData(
                table: "Telephely",
                columns: new[] { "Id", "Fax", "Iranyitoszam", "Telefonszam", "Utca", "Varos", "VevoId" },
                values: new object[,]
                {
                    { 2, "061-457-11-04", "1051", "061-457-11-03", "Hercegprímás u. 22.", "Budapest", 1 },
                    { 1, null, "1114", "061-569-23-99", "Baranyai u. 16.", "Budapest", 2 },
                    { 3, "0646-319-168", "3000", "0646-319-169", "Vörösmarty tér. 5.", "Hatvan", 2 },
                    { 4, "0623-200-155", "2045", "0623-200-156", "Határ u. 17.", "Törökbálint", 3 }
                });

            migrationBuilder.InsertData(
                table: "Termek",
                columns: new[] { "Id", "AfaId", "KategoriaId", "Kep", "Leiras", "NettoAr", "Nev", "Raktarkeszlet" },
                values: new object[] { 10, 2, 2, null, null, 800.0, "Egy óra gyerekfelügyelet", 0 });

            migrationBuilder.InsertData(
                table: "Kategoria",
                columns: new[] { "Id", "Nev", "SzuloKategoriaId" },
                values: new object[,]
                {
                    { 8, "0-6 hónapos kor", 3 },
                    { 21, "Triciklik", 7 },
                    { 20, "Motorok", 7 },
                    { 19, "Bébi taxik", 7 },
                    { 18, "Ügyességi játékok", 5 },
                    { 16, "Készségfejlesztő játékok", 5 },
                    { 17, "Logikai játékok", 5 },
                    { 14, "Építő elemek", 4 },
                    { 13, "LEGO", 4 },
                    { 11, "DUPLO", 4 },
                    { 10, "18-24 hónapos kor", 3 },
                    { 9, "6-18 hónapos kor", 3 },
                    { 15, "Építő kockák", 5 }
                });

            migrationBuilder.InsertData(
                table: "Megrendeles",
                columns: new[] { "Id", "Datum", "FizetesModId", "Hatarido", "StatuszId", "TelephelyId" },
                values: new object[,]
                {
                    { 3, new DateTime(2008, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2008, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1 },
                    { 2, new DateTime(2008, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2008, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 2 },
                    { 4, new DateTime(2008, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(2008, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 2 },
                    { 1, new DateTime(2008, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2008, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 3 }
                });

            migrationBuilder.InsertData(
                table: "Szamla",
                columns: new[] { "Id", "FizetesiHatarido", "FizetesiMod", "KiallitasDatum", "KiallitoId", "MegrendelesId", "MegrendeloIranyitoszam", "MegrendeloNev", "MegrendeloUtca", "MegrendeloVaros", "NyomtatottPeldanyszam", "Sztorno", "TeljesitesDatum" },
                values: new object[,]
                {
                    { 2, new DateTime(2008, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Átutalás 8", new DateTime(2008, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, "1051", "Puskás Norbert", "Hercegprímás u. 22.", "Budapest", 2, false, new DateTime(2008, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 1, new DateTime(2008, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Készpénz", new DateTime(2008, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "3000", "Hajdú-Nagy Katalin", "Vörösmarty tér. 5.", "Hatvan", 2, false, new DateTime(2008, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Termek",
                columns: new[] { "Id", "AfaId", "KategoriaId", "Kep", "Leiras", "NettoAr", "Nev", "Raktarkeszlet" },
                values: new object[,]
                {
                    { 1, 3, 8, null, @"<?xml version=""1.0"" encoding=""utf-16""?>
                <termek>
                	<termek_meret>
                		<mertekegyseg>cm</mertekegyseg>
                		<szelesseg>150</szelesseg>
                		<magassag>50</magassag>
                		<melyseg>150</melyseg>
                	</termek_meret>
                	<csomag_parameterek>
                		<csomag_darabszam>1</csomag_darabszam>
                		<csomag_meret>
                			<mertekegyseg>cm</mertekegyseg>
                			<szelesseg>150</szelesseg>
                			<magassag>20</magassag>
                			<melyseg>20</melyseg>
                		</csomag_meret>
                	</csomag_parameterek>
                	<leiras>
                		Elemmel mukodik, a csomag nem tartalmay elemet.
                	</leiras>
                	<ajanlott_kor>0-18 hónap</ajanlott_kor>
                </termek>", 7488.0, "Activity playgim", 21 },
                    { 2, 3, 8, null, @"<?xml version=""1.0"" encoding=""utf-16""?>
                <termek>
                	<termek_meret>
                		<mertekegyseg>cm</mertekegyseg>
                		<szelesseg>15</szelesseg>
                		<magassag>2</magassag>
                		<melyseg>15</melyseg>
                	</termek_meret>
                	<csomag_parameterek>
                		<csomag_darabszam>1</csomag_darabszam>
                		<csomag_meret>
                			<mertekegyseg>cm</mertekegyseg>
                			<szelesseg>15</szelesseg>
                			<magassag>2</magassag>
                			<melyseg>15</melyseg>
                		</csomag_meret>
                	</csomag_parameterek>
                	<leiras>
                		Tiszta pamut oldalak, élénk színek, vastag kontúrok.
                		Ez a mini világ termék a babák életkori sajátosságainak megfelelően fejleszti a látást, tapintást. Motiválja a babát, hogy megtanulja környezete felismerését.
                		Felerősíthető a gyerekágyra, járókára vagy a babakocsira.
                	</leiras>
                	<ajanlott_kor>0-18 hónap</ajanlott_kor>
                </termek>", 1738.0, "Színes bébikönyv", 58 },
                    { 3, 3, 9, null, @"<?xml version=""1.0"" encoding=""utf-16""?>
                <termek>
                	<termek_meret>
                		<mertekegyseg>cm</mertekegyseg>
                		<szelesseg>20</szelesseg>
                		<magassag>12</magassag>
                		<melyseg>35</melyseg>
                	</termek_meret>
                	<csomag_parameterek>
                		<csomag_darabszam>1</csomag_darabszam>
                		<csomag_meret>
                			<mertekegyseg>cm</mertekegyseg>
                			<szelesseg>40</szelesseg>
                			<magassag>25</magassag>
                			<melyseg>50</melyseg>
                		</csomag_meret>
                	</csomag_parameterek>
                	<leiras>
                		9-36 hónaposan a zajok és a zene izgatja a gyermeki fantáziát. A gombok különböző hangélményekkel lepik meg a gyermeket a dallamok és csengetések segítségével. A 3 gomb megnyomásával vidám képmotívumok kezdenek forogni.
                	</leiras>
                	<ajanlott_kor>9-36 hónap</ajanlott_kor>
                </termek>", 3725.0, "Zenélő bébitelefon", 18 },
                    { 4, 3, 10, null, null, 8356.0, "Fisher Price kalapáló", 58 },
                    { 9, 3, 11, null, null, 6399.0, "Lego Duplo Ásógép", 26 },
                    { 8, 3, 13, null, @"<?xml version=""1.0"" encoding=""utf-16""?>
                <termek>
                	<csomag_parameterek>
                		<csomag_darabszam>1</csomag_darabszam>
                		<csomag_meret>
                			<mertekegyseg>cm</mertekegyseg>
                			<szelesseg>80</szelesseg>
                			<magassag>20</magassag>
                			<melyseg>40</melyseg>
                		</csomag_meret>
                	</csomag_parameterek>
                	<leiras>
                		Elemek száma: 695 db.
                	</leiras>
                	<ajanlott_kor>5-12 év</ajanlott_kor>
                </termek>", 27563.0, "Lego City kikötője", 12 },
                    { 5, 3, 14, null, null, 4325.0, "Mega Bloks 24 db-os", 47 },
                    { 6, 3, 14, null, null, 1854.0, "Maxi Blocks 56 db-os", 36 },
                    { 7, 3, 14, null, null, 4362.0, "Building Blocks 80 db-os", 25 }
                });

            migrationBuilder.InsertData(
                table: "MegrendelesTetel",
                columns: new[] { "Id", "MegrendelesId", "Mennyiseg", "NettoAr", "StatuszId", "TermekId" },
                values: new object[,]
                {
                    { 4, 2, 2, 7488.0, 5, 1 },
                    { 13, 4, 25, 7488.0, 3, 1 },
                    { 3, 1, 5, 1738.0, 5, 2 },
                    { 9, 3, 5, 1738.0, 1, 2 },
                    { 11, 4, 12, 1738.0, 3, 2 },
                    { 5, 2, 3, 3725.0, 5, 3 },
                    { 10, 4, 23, 3725.0, 3, 3 },
                    { 1, 1, 2, 8356.0, 5, 4 },
                    { 8, 3, 2, 6399.0, 3, 9 },
                    { 12, 4, 10, 27563.0, 3, 8 },
                    { 2, 1, 1, 1854.0, 5, 6 },
                    { 7, 3, 6, 1854.0, 2, 6 },
                    { 6, 3, 1, 4362.0, 3, 7 }
                });

            migrationBuilder.InsertData(
                table: "SzamlaTetel",
                columns: new[] { "Id", "Afakulcs", "MegrendelesTetelId", "Mennyiseg", "NettoAr", "Nev", "SzamlaId" },
                values: new object[,]
                {
                    { 4, 20, 4, 2, 7488.0, "Activity playgim", 2 },
                    { 3, 20, 3, 5, 1738.0, "Színes bébikönyv", 1 },
                    { 5, 20, 5, 3, 3725.0, "Zenélő bébitelefon", 2 },
                    { 1, 20, 1, 2, 8356.0, "Fisher Price kalapáló", 1 },
                    { 2, 20, 2, 1, 1854.0, "Maxi Blocks 56 db-os", 1 }
                });

            migrationBuilder.UpdateData(
                table: "Vevo",
                keyColumn: "Id",
                keyValue: 1,
                column: "KozpontiTelephelyId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Vevo",
                keyColumn: "Id",
                keyValue: 2,
                column: "KozpontiTelephelyId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Vevo",
                keyColumn: "Id",
                keyValue: 3,
                column: "KozpontiTelephelyId",
                value: 4);

            migrationBuilder.CreateIndex(
                name: "IX_Kategoria_SzuloKategoriaId",
                table: "Kategoria",
                column: "SzuloKategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Megrendeles_FizetesModId",
                table: "Megrendeles",
                column: "FizetesModId");

            migrationBuilder.CreateIndex(
                name: "IX_Megrendeles_StatuszId",
                table: "Megrendeles",
                column: "StatuszId");

            migrationBuilder.CreateIndex(
                name: "IX_Megrendeles_TelephelyId",
                table: "Megrendeles",
                column: "TelephelyId");

            migrationBuilder.CreateIndex(
                name: "IX_MegrendelesTetel_MegrendelesId",
                table: "MegrendelesTetel",
                column: "MegrendelesId");

            migrationBuilder.CreateIndex(
                name: "IX_MegrendelesTetel_StatuszId",
                table: "MegrendelesTetel",
                column: "StatuszId");

            migrationBuilder.CreateIndex(
                name: "IX_MegrendelesTetel_TermekId",
                table: "MegrendelesTetel",
                column: "TermekId");

            migrationBuilder.CreateIndex(
                name: "IX_Szamla_KiallitoId",
                table: "Szamla",
                column: "KiallitoId");

            migrationBuilder.CreateIndex(
                name: "IX_Szamla_MegrendelesId",
                table: "Szamla",
                column: "MegrendelesId");

            migrationBuilder.CreateIndex(
                name: "IX_SzamlaTetel_MegrendelesTetelId",
                table: "SzamlaTetel",
                column: "MegrendelesTetelId");

            migrationBuilder.CreateIndex(
                name: "IX_SzamlaTetel_SzamlaId",
                table: "SzamlaTetel",
                column: "SzamlaId");

            migrationBuilder.CreateIndex(
                name: "IX_Telephely_VevoId",
                table: "Telephely",
                column: "VevoId");

            migrationBuilder.CreateIndex(
                name: "IX_Termek_AfaId",
                table: "Termek",
                column: "AfaId");

            migrationBuilder.CreateIndex(
                name: "IX_Termek_KategoriaId",
                table: "Termek",
                column: "KategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Vevo_KozpontiTelephelyId",
                table: "Vevo",
                column: "KozpontiTelephelyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Megrendeles_Telephely_TelephelyId",
                table: "Megrendeles",
                column: "TelephelyId",
                principalTable: "Telephely",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vevo_Telephely_KozpontiTelephelyId",
                table: "Vevo",
                column: "KozpontiTelephelyId",
                principalTable: "Telephely",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vevo_Telephely_KozpontiTelephelyId",
                table: "Vevo");

            migrationBuilder.DropTable(
                name: "SzamlaTetel");

            migrationBuilder.DropTable(
                name: "MegrendelesTetel");

            migrationBuilder.DropTable(
                name: "Szamla");

            migrationBuilder.DropTable(
                name: "Termek");

            migrationBuilder.DropTable(
                name: "SzamlaKiallito");

            migrationBuilder.DropTable(
                name: "Megrendeles");

            migrationBuilder.DropTable(
                name: "Afa");

            migrationBuilder.DropTable(
                name: "Kategoria");

            migrationBuilder.DropTable(
                name: "FizetesMod");

            migrationBuilder.DropTable(
                name: "Statusz");

            migrationBuilder.DropTable(
                name: "Telephely");

            migrationBuilder.DropTable(
                name: "Vevo");
        }
    }
}
