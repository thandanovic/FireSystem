namespace FireSys.DB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using FireSys.Entities;

    public partial class FireSysModel : DbContext
    {
        public FireSysModel()
            : base("name=FireSysModel")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<C__RefactorLog> C__RefactorLog { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<EvidencijskaKartica> EvidencijskaKarticas { get; set; }
        public virtual DbSet<EvidencijskaKarticaTip> EvidencijskaKarticaTips { get; set; }
        public virtual DbSet<Hidrant> Hidrants { get; set; }
        public virtual DbSet<HidrantTip> HidrantTips { get; set; }
        public virtual DbSet<Instalacija> Instalacijas { get; set; }
        public virtual DbSet<Ispravnost> Ispravnosts { get; set; }
        public virtual DbSet<Klijent> Klijents { get; set; }
        public virtual DbSet<Kompletnost> Kompletnosts { get; set; }
        public virtual DbSet<Korisnik> Korisniks { get; set; }
        public virtual DbSet<Lokacija> Lokacijas { get; set; }
        public virtual DbSet<LokacijaVrsta> LokacijaVrstas { get; set; }
        public virtual DbSet<Mjesto> Mjestoes { get; set; }
        public virtual DbSet<PromjerMlaznice> PromjerMlaznices { get; set; }
        public virtual DbSet<RadniNalog> RadniNalogs { get; set; }
        public virtual DbSet<RadniNalogAparat> RadniNalogAparats { get; set; }
        public virtual DbSet<RadniNalogHidrant> RadniNalogHidrants { get; set; }
        public virtual DbSet<Regija> Regijas { get; set; }
        public virtual DbSet<SistemskiPodaci> SistemskiPodacis { get; set; }
        public virtual DbSet<VatrogasniAparat> VatrogasniAparats { get; set; }
        public virtual DbSet<VatrogasniAparatTip> VatrogasniAparatTips { get; set; }
        public virtual DbSet<VatrogasniAparatVrsta> VatrogasniAparatVrstas { get; set; }
        public virtual DbSet<Zapisnik> Zapisniks { get; set; }
        public virtual DbSet<ZapisnikAparat> ZapisnikAparats { get; set; }
        public virtual DbSet<ZapisnikHidrant> ZapisnikHidrants { get; set; }
        public virtual DbSet<ZapisnikTip> ZapisnikTips { get; set; }
        public virtual DbSet<viewGridLokacijeList> viewGridLokacijeLists { get; set; }
        public virtual DbSet<viewGridZapisniciList> viewGridZapisniciLists { get; set; }
        public virtual DbSet<viewUsersFull> viewUsersFulls { get; set; }
        public virtual DbSet<viewZapisniciRadniNalozi> viewZapisniciRadniNalozis { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUserRoles)
                .WithRequired(e => e.AspNetRole)
                .HasForeignKey(e => e.RoleId);

            modelBuilder.Entity<EvidencijskaKarticaTip>()
                .HasMany(e => e.EvidencijskaKarticas)
                .WithRequired(e => e.EvidencijskaKarticaTip)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Hidrant>()
                .Property(e => e.HidrostatickiPritisak)
                .HasPrecision(4, 1);

            modelBuilder.Entity<Hidrant>()
                .Property(e => e.HidrodinamickiPritisak)
                .HasPrecision(4, 1);

            modelBuilder.Entity<Hidrant>()
                .HasMany(e => e.RadniNalogHidrants)
                .WithRequired(e => e.Hidrant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Hidrant>()
                .HasMany(e => e.ZapisnikHidrants)
                .WithRequired(e => e.Hidrant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Instalacija>()
                .HasMany(e => e.Hidrants)
                .WithRequired(e => e.Instalacija)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ispravnost>()
                .HasMany(e => e.ZapisnikAparats)
                .WithRequired(e => e.Ispravnost)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Klijent>()
                .HasMany(e => e.Lokacijas)
                .WithRequired(e => e.Klijent)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kompletnost>()
                .HasMany(e => e.ZapisnikHidrants)
                .WithRequired(e => e.Kompletnost)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Lokacija>()
                .HasMany(e => e.Hidrants)
                .WithRequired(e => e.Lokacija)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Lokacija>()
                .HasMany(e => e.RadniNalogs)
                .WithRequired(e => e.Lokacija)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Lokacija>()
                .HasMany(e => e.VatrogasniAparats)
                .WithRequired(e => e.Lokacija)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Lokacija>()
                .HasMany(e => e.Zapisniks)
                .WithRequired(e => e.Lokacija)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RadniNalog>()
                .HasMany(e => e.RadniNalogAparats)
                .WithRequired(e => e.RadniNalog)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RadniNalog>()
                .HasMany(e => e.RadniNalogHidrants)
                .WithRequired(e => e.RadniNalog)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RadniNalog>()
                .HasMany(e => e.Zapisniks)
                .WithOptional(e => e.RadniNalog)
                .HasForeignKey(e => e.IzRadnogNalogaId);

            modelBuilder.Entity<RadniNalog>()
                .HasMany(e => e.Zapisniks1)
                .WithOptional(e => e.RadniNalog1)
                .HasForeignKey(e => e.KreiraniRadniNalogId);

            modelBuilder.Entity<RadniNalog>()
                .HasMany(e => e.Zapisniks2)
                .WithOptional(e => e.RadniNalog2)
                .HasForeignKey(e => e.RadniNalogId);

            modelBuilder.Entity<VatrogasniAparat>()
                .HasMany(e => e.RadniNalogAparats)
                .WithRequired(e => e.VatrogasniAparat)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VatrogasniAparat>()
                .HasMany(e => e.ZapisnikAparats)
                .WithRequired(e => e.VatrogasniAparat)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VatrogasniAparatTip>()
                .HasMany(e => e.VatrogasniAparats)
                .WithRequired(e => e.VatrogasniAparatTip)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VatrogasniAparatVrsta>()
                .HasMany(e => e.VatrogasniAparats)
                .WithRequired(e => e.VatrogasniAparatVrsta)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Zapisnik>()
                .HasMany(e => e.ZapisnikAparats)
                .WithRequired(e => e.Zapisnik)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Zapisnik>()
                .HasMany(e => e.ZapisnikHidrants)
                .WithRequired(e => e.Zapisnik)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ZapisnikTip>()
                .HasMany(e => e.Zapisniks)
                .WithRequired(e => e.ZapisnikTip)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<viewGridZapisniciList>()
                .Property(e => e.PregledIzvrsio)
                .IsUnicode(false);

            modelBuilder.Entity<viewGridZapisniciList>()
                .Property(e => e.ZapisnikKreirao)
                .IsUnicode(false);
        }
    }
}
