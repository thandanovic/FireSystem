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
        public virtual DbSet<FireSys.Entities.AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<FireSys.Entities.AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<FireSys.Entities.AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<FireSys.Entities.AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Status> Statusi { get; set; }
        public virtual DbSet<FireSys.Entities.EvidencijskaKartica> EvidencijskaKarticas { get; set; }
        public virtual DbSet<FireSys.Entities.EvidencijskaKarticaTip> EvidencijskaKarticaTips { get; set; }
        public virtual DbSet<FireSys.Entities.Hidrant> Hidrants { get; set; }
        public virtual DbSet<FireSys.Entities.HidrantTip> HidrantTips { get; set; }
        public virtual DbSet<FireSys.Entities.Instalacija> Instalacijas { get; set; }
        public virtual DbSet<FireSys.Entities.Ispravnost> Ispravnosts { get; set; }
        public virtual DbSet<FireSys.Entities.Klijent> Klijents { get; set; }
        public virtual DbSet<FireSys.Entities.Kompletnost> Kompletnosts { get; set; }
        public virtual DbSet<FireSys.Entities.Korisnik> Korisniks { get; set; }
        public virtual DbSet<FireSys.Entities.Lokacija> Lokacijas { get; set; }
        public virtual DbSet<FireSys.Entities.LokacijaVrsta> LokacijaVrstas { get; set; }
        public virtual DbSet<FireSys.Entities.Mjesto> Mjestoes { get; set; }
        public virtual DbSet<FireSys.Entities.PromjerMlaznice> PromjerMlaznices { get; set; }
        public virtual DbSet<FireSys.Entities.RadniNalog> RadniNalogs { get; set; }
        public virtual DbSet<FireSys.Entities.RadniNalogAparat> RadniNalogAparats { get; set; }
        public virtual DbSet<FireSys.Entities.RadniNalogHidrant> RadniNalogHidrants { get; set; }
        public virtual DbSet<FireSys.Entities.Regija> Regijas { get; set; }
        public virtual DbSet<FireSys.Entities.SistemskiPodaci> SistemskiPodacis { get; set; }
        public virtual DbSet<FireSys.Entities.VatrogasniAparat> VatrogasniAparats { get; set; }
        public virtual DbSet<FireSys.Entities.VatrogasniAparatTip> VatrogasniAparatTips { get; set; }
        public virtual DbSet<FireSys.Entities.VatrogasniAparatVrsta> VatrogasniAparatVrstas { get; set; }
        public virtual DbSet<FireSys.Entities.Zapisnik> Zapisniks { get; set; }
        public virtual DbSet<FireSys.Entities.ZapisnikAparat> ZapisnikAparats { get; set; }
        public virtual DbSet<FireSys.Entities.ZapisnikHidrant> ZapisnikHidrants { get; set; }
        public virtual DbSet<FireSys.Entities.ZapisnikTip> ZapisnikTips { get; set; }
        public virtual DbSet<FireSys.Entities.viewGridLokacijeList> viewGridLokacijeLists { get; set; }
        public virtual DbSet<FireSys.Entities.viewGridZapisniciList> viewGridZapisniciLists { get; set; }
        public virtual DbSet<FireSys.Entities.viewUsersFull> viewUsersFulls { get; set; }
        public virtual DbSet<FireSys.Entities.viewZapisniciRadniNalozi> viewZapisniciRadniNalozis { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FireSys.Entities.AspNetRole>()
                .HasMany(e => e.AspNetUserRoles)
                .WithRequired(e => e.AspNetRole)
                .HasForeignKey(e => e.RoleId);

            modelBuilder.Entity<FireSys.Entities.EvidencijskaKarticaTip>()
                .HasMany(e => e.EvidencijskaKarticas)
                .WithRequired(e => e.EvidencijskaKarticaTip)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FireSys.Entities.Hidrant>()
                .Property(e => e.HidrostatickiPritisak)
                .HasPrecision(4, 1);

            modelBuilder.Entity<FireSys.Entities.Hidrant>()
                .Property(e => e.HidrodinamickiPritisak)
                .HasPrecision(4, 1);

            modelBuilder.Entity<FireSys.Entities.Hidrant>()
                .HasMany(e => e.RadniNalogHidrants)
                .WithRequired(e => e.Hidrant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FireSys.Entities.Hidrant>()
                .HasMany(e => e.ZapisnikHidrants)
                .WithRequired(e => e.Hidrant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FireSys.Entities.Instalacija>()
                .HasMany(e => e.Hidrants)
                .WithRequired(e => e.Instalacija)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FireSys.Entities.Ispravnost>()
                .HasMany(e => e.ZapisnikAparats)
                .WithRequired(e => e.Ispravnost)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FireSys.Entities.Klijent>()
                .HasMany(e => e.Lokacijas)
                .WithRequired(e => e.Klijent)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FireSys.Entities.Kompletnost>()
                .HasMany(e => e.ZapisnikHidrants)
                .WithRequired(e => e.Kompletnost)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FireSys.Entities.Lokacija>()
                .HasMany(e => e.Hidrants)
                .WithRequired(e => e.Lokacija)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FireSys.Entities.Lokacija>()
                .HasMany(e => e.RadniNalogs)
                .WithRequired(e => e.Lokacija)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FireSys.Entities.Lokacija>()
                .HasMany(e => e.VatrogasniAparats)
                .WithRequired(e => e.Lokacija)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FireSys.Entities.Lokacija>()
                .HasMany(e => e.Zapisniks)
                .WithRequired(e => e.Lokacija)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FireSys.Entities.RadniNalog>()
                .HasMany(e => e.RadniNalogAparats)
                .WithRequired(e => e.RadniNalog)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FireSys.Entities.RadniNalog>()
                .HasMany(e => e.RadniNalogHidrants)
                .WithRequired(e => e.RadniNalog)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FireSys.Entities.RadniNalog>()
                .HasMany(e => e.Zapisniks)
                .WithOptional(e => e.RadniNalog)
                .HasForeignKey(e => e.IzRadnogNalogaId);

            modelBuilder.Entity<FireSys.Entities.RadniNalog>()
                .HasMany(e => e.Zapisniks1)
                .WithOptional(e => e.RadniNalog1)
                .HasForeignKey(e => e.KreiraniRadniNalogId);

            modelBuilder.Entity<FireSys.Entities.RadniNalog>()
                .HasMany(e => e.Zapisniks2)
                .WithOptional(e => e.RadniNalog2)
                .HasForeignKey(e => e.RadniNalogId);

            modelBuilder.Entity<FireSys.Entities.VatrogasniAparat>()
                .HasMany(e => e.RadniNalogAparats)
                .WithRequired(e => e.VatrogasniAparat)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FireSys.Entities.VatrogasniAparat>()
                .HasMany(e => e.ZapisnikAparats)
                .WithRequired(e => e.VatrogasniAparat)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FireSys.Entities.VatrogasniAparatTip>()
                .HasMany(e => e.VatrogasniAparats)
                .WithRequired(e => e.VatrogasniAparatTip)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FireSys.Entities.VatrogasniAparatVrsta>()
                .HasMany(e => e.VatrogasniAparats)
                .WithRequired(e => e.VatrogasniAparatVrsta)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FireSys.Entities.Zapisnik>()
                .HasMany(e => e.ZapisnikAparats)
                .WithRequired(e => e.Zapisnik)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FireSys.Entities.Zapisnik>()
                .HasMany(e => e.ZapisnikHidrants)
                .WithRequired(e => e.Zapisnik)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FireSys.Entities.ZapisnikTip>()
                .HasMany(e => e.Zapisniks)
                .WithRequired(e => e.ZapisnikTip)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FireSys.Entities.viewGridZapisniciList>()
                .Property(e => e.PregledIzvrsio)
                .IsUnicode(false);

            modelBuilder.Entity<FireSys.Entities.viewGridZapisniciList>()
                .Property(e => e.ZapisnikKreirao)
                .IsUnicode(false);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.RadniNalogs)
                .WithRequired(e => e.Status)
                .WillCascadeOnDelete(false);
        }
    }
}
