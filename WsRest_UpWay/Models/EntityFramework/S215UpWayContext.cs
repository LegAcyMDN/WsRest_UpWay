using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class S215UpWayContext : DbContext
{
    public S215UpWayContext()
    {
    }

    public S215UpWayContext(DbContextOptions<S215UpWayContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Accessoire> Accessoires { get; set; }

    public virtual DbSet<Adresseexpedition> Adresseexpeditions { get; set; }

    public virtual DbSet<Adressefacturation> Adressefacturations { get; set; }

    public virtual DbSet<Ajouteraccessoire> Ajouteraccessoires { get; set; }

    public virtual DbSet<Alertevelo> Alertevelos { get; set; }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<Assurance> Assurances { get; set; }

    public virtual DbSet<Caracteristique> Caracteristiques { get; set; }

    public virtual DbSet<Caracteristiquevelo> Caracteristiquevelos { get; set; }

    public virtual DbSet<Categorie> Categories { get; set; }

    public virtual DbSet<CategorieArticle> CategorieArticles { get; set; }

    public virtual DbSet<Codereduction> Codereductions { get; set; }

    public virtual DbSet<Compteclient> Compteclients { get; set; }

    public virtual DbSet<ContenuArticle> ContenuArticles { get; set; }

    public virtual DbSet<Detailcommande> Detailcommandes { get; set; }

    public virtual DbSet<Dpo> Dpos { get; set; }

    public virtual DbSet<Estrealise> Estrealises { get; set; }

    public virtual DbSet<Etatcommande> Etatcommandes { get; set; }

    public virtual DbSet<Information> Informations { get; set; }

    public virtual DbSet<Lignepanier> Lignepaniers { get; set; }

    public virtual DbSet<Magasin> Magasins { get; set; }

    public virtual DbSet<Marquagevelo> Marquagevelos { get; set; }

    public virtual DbSet<Marque> Marques { get; set; }

    public virtual DbSet<Mentionvelo> Mentionvelos { get; set; }

    public virtual DbSet<Moteur> Moteurs { get; set; }

    public virtual DbSet<Panier> Paniers { get; set; }

    public virtual DbSet<Photoaccessoire> Photoaccessoires { get; set; }

    public virtual DbSet<Photovelo> Photovelos { get; set; }

    public virtual DbSet<Rapportinspection> Rapportinspections { get; set; }

    public virtual DbSet<Reparationvelo> Reparationvelos { get; set; }

    public virtual DbSet<Retraitmagasin> Retraitmagasins { get; set; }

    public virtual DbSet<Testvelo> Testvelos { get; set; }

    public virtual DbSet<Utilite> Utilites { get; set; }

    public virtual DbSet<Vadresse> Vadresses { get; set; }

    public virtual DbSet<Vcaracteristiquevelo> Vcaracteristiquevelos { get; set; }

    public virtual DbSet<Vcommande> Vcommandes { get; set; }

    public virtual DbSet<Vdpo> Vdpos { get; set; }

    public virtual DbSet<Velo> Velos { get; set; }

    public virtual DbSet<Velomodifier> Velomodifiers { get; set; }

    public virtual DbSet<Vvelo> Vvelos { get; set; }

    public virtual DbSet<Vventenombreacessoire> Vventenombreacessoires { get; set; }

    public virtual DbSet<Vventenombrevelo> Vventenombrevelos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=51.83.36.122;port=5432;Database=s215_UpWay;uid=s215;password=e932ef57-561f-40cb-bbe6-e4952926895253769175fbbafe508f4cd113d35cb1976e371119952198f3a26478d6b7d1b61e;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Accessoire>(entity =>
        {
            entity.HasKey(e => e.Idaccessoire).HasName("pk_accessoire");

            entity.ToTable("accessoire", "upways");

            entity.HasIndex(e => e.Idcategorie, "idx_accessoire_idcategorie");

            entity.HasIndex(e => e.Idmarque, "idx_accessoire_idmarque");

            entity.Property(e => e.Idaccessoire)
                .HasDefaultValueSql("nextval('accessoire_idaccessoire_seq'::regclass)")
                .HasColumnName("idaccessoire");
            entity.Property(e => e.Descriptionaccessoire)
                .HasMaxLength(4096)
                .HasColumnName("descriptionaccessoire");
            entity.Property(e => e.Idcategorie).HasColumnName("idcategorie");
            entity.Property(e => e.Idmarque).HasColumnName("idmarque");
            entity.Property(e => e.Nomaccessoire)
                .HasMaxLength(100)
                .HasColumnName("nomaccessoire");
            entity.Property(e => e.Prixaccessoire).HasColumnName("prixaccessoire");

            entity.HasOne(d => d.IdcategorieNavigation).WithMany(p => p.Accessoires)
                .HasForeignKey(d => d.Idcategorie)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_accessoi_classifie_categori");

            entity.HasOne(d => d.IdmarqueNavigation).WithMany(p => p.Accessoires)
                .HasForeignKey(d => d.Idmarque)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_accessoi_distribue_marque");

            entity.HasMany(d => d.Idvelos).WithMany(p => p.Idaccessoires)
                .UsingEntity<Dictionary<string, object>>(
                    "Equiper",
                    r => r.HasOne<Velo>().WithMany()
                        .HasForeignKey("Idvelo")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_equiper_equiper2_velo"),
                    l => l.HasOne<Accessoire>().WithMany()
                        .HasForeignKey("Idaccessoire")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_equiper_equiper_accessoi"),
                    j =>
                    {
                        j.HasKey("Idaccessoire", "Idvelo").HasName("pk_equiper");
                        j.ToTable("equiper", "upways");
                        j.HasIndex(new[] { "Idaccessoire" }, "idx_equiper_idacessoire");
                        j.HasIndex(new[] { "Idvelo" }, "idx_equiper_idvelo");
                        j.IndexerProperty<int>("Idaccessoire").HasColumnName("idaccessoire");
                        j.IndexerProperty<int>("Idvelo").HasColumnName("idvelo");
                    });
        });

        modelBuilder.Entity<Adresseexpedition>(entity =>
        {
            entity.HasKey(e => e.Idadresseexp).HasName("pk_adresseexpedition");

            entity.ToTable("adresseexpedition", "upways");

            entity.HasIndex(e => e.Idadressefact, "idx_adresseexp_idadressefact");

            entity.HasIndex(e => e.Idclient, "idx_adresseexp_idclient");

            entity.Property(e => e.Idadresseexp)
                .HasDefaultValueSql("nextval('adresseexpedition_idadresseexp_seq'::regclass)")
                .HasColumnName("idadresseexp");
            entity.Property(e => e.Batimentexpeoption)
                .HasMaxLength(100)
                .HasColumnName("batimentexpeoption");
            entity.Property(e => e.Cpexpe)
                .HasMaxLength(10)
                .HasColumnName("cpexpe");
            entity.Property(e => e.Donneessauvegardees).HasColumnName("donneessauvegardees");
            entity.Property(e => e.Idadressefact).HasColumnName("idadressefact");
            entity.Property(e => e.Idclient).HasColumnName("idclient");
            entity.Property(e => e.Paysexpe)
                .HasMaxLength(50)
                .HasColumnName("paysexpe");
            entity.Property(e => e.Regionexpe)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("regionexpe");
            entity.Property(e => e.Rueexpe)
                .HasMaxLength(200)
                .HasColumnName("rueexpe");
            entity.Property(e => e.Telephoneexpe)
                .HasMaxLength(14)
                .HasColumnName("telephoneexpe");
            entity.Property(e => e.Villeexpe)
                .HasMaxLength(100)
                .HasColumnName("villeexpe");

            entity.HasOne(d => d.IdadressefactNavigation).WithMany(p => p.Adresseexpeditions)
                .HasForeignKey(d => d.Idadressefact)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_adressee_assimiler_adressef");

            entity.HasOne(d => d.IdclientNavigation).WithMany(p => p.Adresseexpeditions)
                .HasForeignKey(d => d.Idclient)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_adressee_modifier_comptecl");
        });

        modelBuilder.Entity<Adressefacturation>(entity =>
        {
            entity.HasKey(e => e.Idadressefact).HasName("pk_adressefacturation");

            entity.ToTable("adressefacturation", "upways");

            entity.HasIndex(e => e.Idadresseexp, "idx_adressefact_idadresseexp");

            entity.HasIndex(e => e.Idclient, "idx_adressefact_idclient");

            entity.Property(e => e.Idadressefact)
                .HasDefaultValueSql("nextval('adressefacturation_idadressefact_seq'::regclass)")
                .HasColumnName("idadressefact");
            entity.Property(e => e.Appartementfact)
                .HasMaxLength(100)
                .HasColumnName("appartementfact");
            entity.Property(e => e.Cpfact)
                .HasMaxLength(10)
                .HasColumnName("cpfact");
            entity.Property(e => e.Idadresseexp).HasColumnName("idadresseexp");
            entity.Property(e => e.Idclient).HasColumnName("idclient");
            entity.Property(e => e.Paysfact)
                .HasMaxLength(50)
                .HasColumnName("paysfact");
            entity.Property(e => e.Regionfact)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("regionfact");
            entity.Property(e => e.Ruefact)
                .HasMaxLength(200)
                .HasColumnName("ruefact");
            entity.Property(e => e.Telephonefact)
                .HasMaxLength(14)
                .HasColumnName("telephonefact");
            entity.Property(e => e.Villefact)
                .HasMaxLength(100)
                .HasColumnName("villefact");

            entity.HasOne(d => d.IdadresseexpNavigation).WithMany(p => p.Adressefacturations)
                .HasForeignKey(d => d.Idadresseexp)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_adressef_assimiler_adressee");

            entity.HasOne(d => d.IdclientNavigation).WithMany(p => p.Adressefacturations)
                .HasForeignKey(d => d.Idclient)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_adressef_rectifier_comptecl");
        });

        modelBuilder.Entity<Ajouteraccessoire>(entity =>
        {
            entity.HasKey(e => new { e.Idaccessoire, e.Idpanier }).HasName("pk_ajouteraccessoire");

            entity.ToTable("ajouteraccessoire", "upways");

            entity.HasIndex(e => e.Idaccessoire, "idx_ajouteracessoire_idaccessoire");

            entity.HasIndex(e => e.Idpanier, "idx_ajouteracessoire_idpanier");

            entity.Property(e => e.Idaccessoire).HasColumnName("idaccessoire");
            entity.Property(e => e.Idpanier).HasColumnName("idpanier");
            entity.Property(e => e.Quantiteaccessoire)
                .HasPrecision(2)
                .HasDefaultValueSql("1")
                .HasColumnName("quantiteaccessoire");

            entity.HasOne(d => d.IdaccessoireNavigation).WithMany(p => p.Ajouteraccessoires)
                .HasForeignKey(d => d.Idaccessoire)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_ajoutera_ajouterac_accessoi");

            entity.HasOne(d => d.IdpanierNavigation).WithMany(p => p.Ajouteraccessoires)
                .HasForeignKey(d => d.Idpanier)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_ajoutera_ajouterac_panier");
        });

        modelBuilder.Entity<Alertevelo>(entity =>
        {
            entity.HasKey(e => new { e.Idalerte, e.Idclient, e.Idvelo }).HasName("pk_alertevelo");

            entity.ToTable("alertevelo", "upways");

            entity.HasIndex(e => e.Idclient, "idx_alerte_idclient");

            entity.HasIndex(e => e.Idvelo, "idx_alerte_idvelo");

            entity.Property(e => e.Idalerte)
                .HasDefaultValueSql("nextval('alertevelo_idalerte_seq'::regclass)")
                .HasColumnName("idalerte");
            entity.Property(e => e.Idclient).HasColumnName("idclient");
            entity.Property(e => e.Idvelo).HasColumnName("idvelo");
            entity.Property(e => e.Modifalerte)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifalerte");

            entity.HasOne(d => d.IdclientNavigation).WithMany(p => p.Alertevelos)
                .HasForeignKey(d => d.Idclient)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_alertevelo_client");

            entity.HasOne(d => d.IdveloNavigation).WithMany(p => p.Alertevelos)
                .HasForeignKey(d => d.Idvelo)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_alertevelo_velo");
        });

        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.Idarticle).HasName("pk_article");

            entity.ToTable("article", "upways");

            entity.Property(e => e.Idarticle)
                .HasDefaultValueSql("nextval('article_idarticle_seq'::regclass)")
                .HasColumnName("idarticle");
            entity.Property(e => e.IdcategorieArticle).HasColumnName("idcategorie_article");

            entity.HasOne(d => d.IdcategorieArticleNavigation).WithMany(p => p.Articles)
                .HasForeignKey(d => d.IdcategorieArticle)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_article");
        });

        modelBuilder.Entity<Assurance>(entity =>
        {
            entity.HasKey(e => e.Idassurance).HasName("pk_assurance");

            entity.ToTable("assurance", "upways");

            entity.Property(e => e.Idassurance)
                .HasDefaultValueSql("nextval('assurance_idassurance_seq'::regclass)")
                .HasColumnName("idassurance");
            entity.Property(e => e.Descriptionassurance)
                .HasMaxLength(4096)
                .HasColumnName("descriptionassurance");
            entity.Property(e => e.Prixassurance)
                .HasPrecision(4, 2)
                .HasColumnName("prixassurance");
            entity.Property(e => e.Titreassurance)
                .HasMaxLength(50)
                .HasColumnName("titreassurance");
        });

        modelBuilder.Entity<Caracteristique>(entity =>
        {
            entity.HasKey(e => e.Idcaracteristique).HasName("pk_caracteristique");

            entity.ToTable("caracteristique", "upways");

            entity.Property(e => e.Idcaracteristique)
                .HasDefaultValueSql("nextval('caracteristique_idcaracteristique_seq'::regclass)")
                .HasColumnName("idcaracteristique");
            entity.Property(e => e.Imagecaracteristique)
                .HasMaxLength(200)
                .HasColumnName("imagecaracteristique");
            entity.Property(e => e.Libellecaracteristique)
                .HasMaxLength(100)
                .HasColumnName("libellecaracteristique");

            entity.HasMany(d => d.CarIdcaracteristiques).WithMany(p => p.Idcaracteristiques)
                .UsingEntity<Dictionary<string, object>>(
                    "Sedecompose",
                    r => r.HasOne<Caracteristique>().WithMany()
                        .HasForeignKey("CarIdcaracteristique")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_sedecomp_caracteri_caracter"),
                    l => l.HasOne<Caracteristique>().WithMany()
                        .HasForeignKey("Idcaracteristique")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_sedecomp_caract_fi_caracter"),
                    j =>
                    {
                        j.HasKey("Idcaracteristique", "CarIdcaracteristique").HasName("pk_sedecompose");
                        j.ToTable("sedecompose", "upways");
                        j.HasIndex(new[] { "CarIdcaracteristique" }, "idx_sedecompose_car_idcaracteristique");
                        j.HasIndex(new[] { "Idcaracteristique" }, "idx_sedecompose_idcaracteristique");
                        j.IndexerProperty<int>("Idcaracteristique").HasColumnName("idcaracteristique");
                        j.IndexerProperty<int>("CarIdcaracteristique").HasColumnName("car_idcaracteristique");
                    });

            entity.HasMany(d => d.Idcaracteristiques).WithMany(p => p.CarIdcaracteristiques)
                .UsingEntity<Dictionary<string, object>>(
                    "Sedecompose",
                    r => r.HasOne<Caracteristique>().WithMany()
                        .HasForeignKey("Idcaracteristique")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_sedecomp_caract_fi_caracter"),
                    l => l.HasOne<Caracteristique>().WithMany()
                        .HasForeignKey("CarIdcaracteristique")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_sedecomp_caracteri_caracter"),
                    j =>
                    {
                        j.HasKey("Idcaracteristique", "CarIdcaracteristique").HasName("pk_sedecompose");
                        j.ToTable("sedecompose", "upways");
                        j.HasIndex(new[] { "CarIdcaracteristique" }, "idx_sedecompose_car_idcaracteristique");
                        j.HasIndex(new[] { "Idcaracteristique" }, "idx_sedecompose_idcaracteristique");
                        j.IndexerProperty<int>("Idcaracteristique").HasColumnName("idcaracteristique");
                        j.IndexerProperty<int>("CarIdcaracteristique").HasColumnName("car_idcaracteristique");
                    });

            entity.HasMany(d => d.Idcategories).WithMany(p => p.Idcaracteristiques)
                .UsingEntity<Dictionary<string, object>>(
                    "Regrouper",
                    r => r.HasOne<Categorie>().WithMany()
                        .HasForeignKey("Idcategorie")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_regroupe_regrouper_categori"),
                    l => l.HasOne<Caracteristique>().WithMany()
                        .HasForeignKey("Idcaracteristique")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_regroupe_regrouper_caracter"),
                    j =>
                    {
                        j.HasKey("Idcaracteristique", "Idcategorie").HasName("pk_regrouper");
                        j.ToTable("regrouper", "upways");
                        j.HasIndex(new[] { "Idcaracteristique" }, "idx_regrouper_idcaracteristique");
                        j.HasIndex(new[] { "Idcategorie" }, "idx_regrouper_idcategorie");
                        j.IndexerProperty<int>("Idcaracteristique").HasColumnName("idcaracteristique");
                        j.IndexerProperty<int>("Idcategorie").HasColumnName("idcategorie");
                    });

            entity.HasMany(d => d.Idvelos).WithMany(p => p.Idcaracteristiques)
                .UsingEntity<Dictionary<string, object>>(
                    "Caracteriser",
                    r => r.HasOne<Velo>().WithMany()
                        .HasForeignKey("Idvelo")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_caracter_caracteri_velo"),
                    l => l.HasOne<Caracteristique>().WithMany()
                        .HasForeignKey("Idcaracteristique")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_caracter_caracteri_caracter"),
                    j =>
                    {
                        j.HasKey("Idcaracteristique", "Idvelo").HasName("pk_caracteriser");
                        j.ToTable("caracteriser", "upways");
                        j.HasIndex(new[] { "Idcaracteristique" }, "idx_caracteriser_idcaracteristique");
                        j.HasIndex(new[] { "Idvelo" }, "idx_caracteriser_idvelo");
                        j.IndexerProperty<int>("Idcaracteristique").HasColumnName("idcaracteristique");
                        j.IndexerProperty<int>("Idvelo").HasColumnName("idvelo");
                    });
        });

        modelBuilder.Entity<Caracteristiquevelo>(entity =>
        {
            entity.HasKey(e => e.Idcaracteristiquevelo).HasName("pk_caracteristiquevelo");

            entity.ToTable("caracteristiquevelo", "upways");

            entity.Property(e => e.Idcaracteristiquevelo)
                .HasDefaultValueSql("nextval('caracteristiquevelo_idcaracteristiquevelo_seq'::regclass)")
                .HasColumnName("idcaracteristiquevelo");
            entity.Property(e => e.Amortisseur)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("amortisseur");
            entity.Property(e => e.Couleur)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("couleur");
            entity.Property(e => e.Debattement).HasColumnName("debattement");
            entity.Property(e => e.Debattementamortisseur).HasColumnName("debattementamortisseur");
            entity.Property(e => e.Etatbatterie)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("etatbatterie");
            entity.Property(e => e.Fourche)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("fourche");
            entity.Property(e => e.Freins)
                .HasMaxLength(30)
                .IsFixedLength()
                .HasColumnName("freins");
            entity.Property(e => e.Materiau)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("materiau");
            entity.Property(e => e.Modeltransmission)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("modeltransmission");
            entity.Property(e => e.Nombrecycle).HasColumnName("nombrecycle");
            entity.Property(e => e.Nombrevitesse).HasColumnName("nombrevitesse");
            entity.Property(e => e.Pneus)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("pneus");
            entity.Property(e => e.Poids)
                .HasPrecision(5, 2)
                .HasColumnName("poids");
            entity.Property(e => e.Selletelescopique).HasColumnName("selletelescopique");
            entity.Property(e => e.Taillesroues).HasColumnName("taillesroues");
            entity.Property(e => e.Tubeselle).HasColumnName("tubeselle");
            entity.Property(e => e.Typecargo)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("typecargo");
            entity.Property(e => e.Typesuspension)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("typesuspension");
        });

        modelBuilder.Entity<Categorie>(entity =>
        {
            entity.HasKey(e => e.Idcategorie).HasName("pk_categorie");

            entity.ToTable("categorie", "upways");

            entity.HasIndex(e => e.Libellecategorie, "idx_catgegorie_libellecategorie");

            entity.Property(e => e.Idcategorie)
                .HasDefaultValueSql("nextval('categorie_idcategorie_seq'::regclass)")
                .HasColumnName("idcategorie");
            entity.Property(e => e.Libellecategorie)
                .HasMaxLength(100)
                .HasColumnName("libellecategorie");
        });

        modelBuilder.Entity<CategorieArticle>(entity =>
        {
            entity.HasKey(e => e.IdcategorieArticle).HasName("pk_categorie_article");

            entity.ToTable("categorie_article", "upways");

            entity.Property(e => e.IdcategorieArticle)
                .HasDefaultValueSql("nextval('categorie_article_idcategorie_article_seq'::regclass)")
                .HasColumnName("idcategorie_article");
            entity.Property(e => e.ContenuecategorieArticle)
                .HasMaxLength(4096)
                .HasColumnName("contenuecategorie_article");
            entity.Property(e => e.Imagecategorie)
                .HasMaxLength(4096)
                .HasColumnName("imagecategorie");
            entity.Property(e => e.TitrecategorieArticle)
                .HasMaxLength(100)
                .HasColumnName("titrecategorie_article");

            entity.HasMany(d => d.CatIdcategorieArticles).WithMany(p => p.IdcategorieArticles)
                .UsingEntity<Dictionary<string, object>>(
                    "Appartient",
                    r => r.HasOne<CategorieArticle>().WithMany()
                        .HasForeignKey("CatIdcategorieArticle")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_appartient_categorie"),
                    l => l.HasOne<CategorieArticle>().WithMany()
                        .HasForeignKey("IdcategorieArticle")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_appartient_categorie_fi"),
                    j =>
                    {
                        j.HasKey("IdcategorieArticle", "CatIdcategorieArticle").HasName("pk_appartient");
                        j.ToTable("appartient", "upways");
                        j.IndexerProperty<int>("IdcategorieArticle").HasColumnName("idcategorie_article");
                        j.IndexerProperty<int>("CatIdcategorieArticle").HasColumnName("cat_idcategorie_article");
                    });

            entity.HasMany(d => d.IdcategorieArticles).WithMany(p => p.CatIdcategorieArticles)
                .UsingEntity<Dictionary<string, object>>(
                    "Appartient",
                    r => r.HasOne<CategorieArticle>().WithMany()
                        .HasForeignKey("IdcategorieArticle")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_appartient_categorie_fi"),
                    l => l.HasOne<CategorieArticle>().WithMany()
                        .HasForeignKey("CatIdcategorieArticle")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_appartient_categorie"),
                    j =>
                    {
                        j.HasKey("IdcategorieArticle", "CatIdcategorieArticle").HasName("pk_appartient");
                        j.ToTable("appartient", "upways");
                        j.IndexerProperty<int>("IdcategorieArticle").HasColumnName("idcategorie_article");
                        j.IndexerProperty<int>("CatIdcategorieArticle").HasColumnName("cat_idcategorie_article");
                    });
        });

        modelBuilder.Entity<Codereduction>(entity =>
        {
            entity.HasKey(e => e.Idreduction).HasName("pk_codereduction");

            entity.ToTable("codereduction", "upways");

            entity.Property(e => e.Idreduction)
                .HasMaxLength(20)
                .HasColumnName("idreduction");
            entity.Property(e => e.Actifreduction).HasColumnName("actifreduction");
            entity.Property(e => e.Reduction).HasColumnName("reduction");
        });

        modelBuilder.Entity<Compteclient>(entity =>
        {
            entity.HasKey(e => e.Idclient).HasName("pk_compteclient");

            entity.ToTable("compteclient", "upways");

            entity.HasIndex(e => e.Emailclient, "email_unq").IsUnique();

            entity.HasIndex(e => e.Loginclient, "pseudo_unq").IsUnique();

            entity.Property(e => e.Idclient)
                .HasDefaultValueSql("nextval('compteclient_idclient_seq'::regclass)")
                .HasColumnName("idclient");
            entity.Property(e => e.Datecreation).HasColumnName("datecreation");
            entity.Property(e => e.EmailVerifiedAt).HasColumnName("email_verified_at");
            entity.Property(e => e.Emailclient)
                .HasMaxLength(200)
                .HasColumnName("emailclient");
            entity.Property(e => e.IsFromGoogle).HasColumnName("is_from_google");
            entity.Property(e => e.Loginclient)
                .HasMaxLength(20)
                .HasColumnName("loginclient");
            entity.Property(e => e.Motdepasseclient)
                .HasMaxLength(64)
                .HasColumnName("motdepasseclient");
            entity.Property(e => e.Nomclient)
                .HasMaxLength(30)
                .HasColumnName("nomclient");
            entity.Property(e => e.Prenomclient)
                .HasMaxLength(20)
                .HasColumnName("prenomclient");
            entity.Property(e => e.RememberToken)
                .HasMaxLength(100)
                .HasColumnName("remember_token");
            entity.Property(e => e.TwoFactorConfirmedAt)
                .HasMaxLength(4096)
                .HasColumnName("two_factor_confirmed_at");
            entity.Property(e => e.TwoFactorRecoveryCodes)
                .HasMaxLength(4096)
                .HasColumnName("two_factor_recovery_codes");
            entity.Property(e => e.TwoFactorSecret)
                .HasMaxLength(4096)
                .HasColumnName("two_factor_secret");
            entity.Property(e => e.Usertype)
                .HasMaxLength(20)
                .HasColumnName("usertype");
        });

        modelBuilder.Entity<ContenuArticle>(entity =>
        {
            entity.HasKey(e => e.Idcontenue).HasName("pk_contenu_article");

            entity.ToTable("contenu_article", "upways");

            entity.Property(e => e.Idcontenue)
                .HasDefaultValueSql("nextval('contenu_article_idcontenue_seq'::regclass)")
                .HasColumnName("idcontenue");
            entity.Property(e => e.Contenu)
                .HasMaxLength(4096)
                .HasColumnName("contenu");
            entity.Property(e => e.Idarticle).HasColumnName("idarticle");
            entity.Property(e => e.Prioritecontenu).HasColumnName("prioritecontenu");
            entity.Property(e => e.Typecontenu)
                .HasMaxLength(64)
                .HasColumnName("typecontenu");

            entity.HasOne(d => d.IdarticleNavigation).WithMany(p => p.ContenuArticles)
                .HasForeignKey(d => d.Idarticle)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_contenu_article_article");
        });

        modelBuilder.Entity<Detailcommande>(entity =>
        {
            entity.HasKey(e => e.Idcommande).HasName("pk_detailcommande");

            entity.ToTable("detailcommande", "upways");

            entity.HasIndex(e => e.Idadressefact, "idx_detailcommande_idadressefact");

            entity.HasIndex(e => e.Idclient, "idx_detailcommande_idclient");

            entity.HasIndex(e => e.Idetatcommande, "idx_detailcommande_idetatcommande");

            entity.HasIndex(e => e.Idpanier, "idx_detailcommande_idpanier");

            entity.HasIndex(e => e.Idretraitmagasin, "idx_detailcommande_idretraitmagasin");

            entity.Property(e => e.Idcommande)
                .HasDefaultValueSql("nextval('detailcommande_idcommande_seq'::regclass)")
                .HasColumnName("idcommande");
            entity.Property(e => e.Dateachat).HasColumnName("dateachat");
            entity.Property(e => e.Idadressefact).HasColumnName("idadressefact");
            entity.Property(e => e.Idclient).HasColumnName("idclient");
            entity.Property(e => e.Idetatcommande).HasColumnName("idetatcommande");
            entity.Property(e => e.Idpanier).HasColumnName("idpanier");
            entity.Property(e => e.Idretraitmagasin).HasColumnName("idretraitmagasin");
            entity.Property(e => e.Modeexpedition)
                .HasMaxLength(20)
                .HasColumnName("modeexpedition");
            entity.Property(e => e.Moyenpaiement)
                .HasMaxLength(10)
                .HasColumnName("moyenpaiement");

            entity.HasOne(d => d.IdadressefactNavigation).WithMany(p => p.Detailcommandes)
                .HasForeignKey(d => d.Idadressefact)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_detailco_afficher2_adressef");

            entity.HasOne(d => d.IdclientNavigation).WithMany(p => p.Detailcommandes)
                .HasForeignKey(d => d.Idclient)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_detailco_visualise_comptecl");

            entity.HasOne(d => d.IdetatcommandeNavigation).WithMany(p => p.Detailcommandes)
                .HasForeignKey(d => d.Idetatcommande)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_detailco_indiquer_etatcomm");

            entity.HasOne(d => d.IdpanierNavigation).WithMany(p => p.Detailcommandes)
                .HasForeignKey(d => d.Idpanier)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_detailco_estlie_panier");

            entity.HasOne(d => d.IdretraitmagasinNavigation).WithMany(p => p.Detailcommandes)
                .HasForeignKey(d => d.Idretraitmagasin)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_detailco_monter_retraitm");
        });

        modelBuilder.Entity<Dpo>(entity =>
        {
            entity.HasKey(e => e.Iddpo).HasName("pk_dpo");

            entity.ToTable("dpo", "upways");

            entity.HasIndex(e => e.Idclient, "idx_dpo_idclient");

            entity.Property(e => e.Iddpo)
                .HasDefaultValueSql("nextval('dpo_iddpo_seq'::regclass)")
                .HasColumnName("iddpo");
            entity.Property(e => e.Daterequetedpo).HasColumnName("daterequetedpo");
            entity.Property(e => e.Idclient).HasColumnName("idclient");
            entity.Property(e => e.Typeoperation)
                .HasMaxLength(20)
                .HasColumnName("typeoperation");
        });

        modelBuilder.Entity<Estrealise>(entity =>
        {
            entity.HasKey(e => new { e.Idvelo, e.Idinspection, e.Idreparation }).HasName("pk_estrealise");

            entity.ToTable("estrealise", "upways");

            entity.HasIndex(e => e.Idinspection, "idx_estrealise_idinspection");

            entity.HasIndex(e => e.Idreparation, "idx_estrealise_idreparation");

            entity.HasIndex(e => e.Idvelo, "idx_estrealise_idvelo");

            entity.Property(e => e.Idvelo).HasColumnName("idvelo");
            entity.Property(e => e.Idinspection).HasColumnName("idinspection");
            entity.Property(e => e.Idreparation).HasColumnName("idreparation");
            entity.Property(e => e.Commentaireinspection)
                .HasMaxLength(4096)
                .HasColumnName("commentaireinspection");
            entity.Property(e => e.Dateinspection)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("dateinspection");
            entity.Property(e => e.Historiqueinspection)
                .HasMaxLength(100)
                .HasColumnName("historiqueinspection");

            entity.HasOne(d => d.IdinspectionNavigation).WithMany(p => p.Estrealises)
                .HasForeignKey(d => d.Idinspection)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_estreali_estrealis_rapporti");

            entity.HasOne(d => d.IdreparationNavigation).WithMany(p => p.Estrealises)
                .HasForeignKey(d => d.Idreparation)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_estreali_estrealis_reparati");

            entity.HasOne(d => d.IdveloNavigation).WithMany(p => p.Estrealises)
                .HasForeignKey(d => d.Idvelo)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_estreali_estrealis_velo");
        });

        modelBuilder.Entity<Etatcommande>(entity =>
        {
            entity.HasKey(e => e.Idetatcommande).HasName("pk_etatcommande");

            entity.ToTable("etatcommande", "upways");

            entity.Property(e => e.Idetatcommande)
                .HasDefaultValueSql("nextval('etatcommande_idetatcommande_seq'::regclass)")
                .HasColumnName("idetatcommande");
            entity.Property(e => e.Libelleetat)
                .HasMaxLength(20)
                .HasColumnName("libelleetat");
        });

        modelBuilder.Entity<Information>(entity =>
        {
            entity.HasKey(e => e.Idinformations).HasName("pk_informations");

            entity.ToTable("informations", "upways");

            entity.HasIndex(e => e.Idadresseexp, "idx_informations_idadresseexp");

            entity.HasIndex(e => e.Idpanier, "idx_informations_idpanier");

            entity.HasIndex(e => e.Idreduction, "idx_informations_idreduction");

            entity.HasIndex(e => e.Idretraitmagasin, "idx_informations_idretraitmagasin");

            entity.Property(e => e.Idinformations)
                .HasDefaultValueSql("nextval('informations_idinformations_seq'::regclass)")
                .HasColumnName("idinformations");
            entity.Property(e => e.Contactinformations)
                .HasMaxLength(100)
                .HasColumnName("contactinformations");
            entity.Property(e => e.Idadresseexp).HasColumnName("idadresseexp");
            entity.Property(e => e.Idpanier).HasColumnName("idpanier");
            entity.Property(e => e.Idreduction)
                .HasMaxLength(20)
                .HasColumnName("idreduction");
            entity.Property(e => e.Idretraitmagasin).HasColumnName("idretraitmagasin");
            entity.Property(e => e.Informationpays)
                .HasMaxLength(20)
                .HasColumnName("informationpays");
            entity.Property(e => e.Informationrue)
                .HasMaxLength(200)
                .HasColumnName("informationrue");
            entity.Property(e => e.Modelivraison)
                .HasMaxLength(30)
                .HasColumnName("modelivraison");
            entity.Property(e => e.Offreemail).HasColumnName("offreemail");

            entity.HasOne(d => d.IdadresseexpNavigation).WithMany(p => p.Information)
                .HasForeignKey(d => d.Idadresseexp)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_informat_opter_adressee");

            entity.HasOne(d => d.IdpanierNavigation).WithMany(p => p.Information)
                .HasForeignKey(d => d.Idpanier)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_informat_regler_panier");

            entity.HasOne(d => d.IdreductionNavigation).WithMany(p => p.Information)
                .HasForeignKey(d => d.Idreduction)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_informat_utiliser_coderedu");

            entity.HasOne(d => d.IdretraitmagasinNavigation).WithMany(p => p.Information)
                .HasForeignKey(d => d.Idretraitmagasin)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_informat_choisir2_retraitm");
        });

        modelBuilder.Entity<Lignepanier>(entity =>
        {
            entity.HasKey(e => new { e.Idpanier, e.Idvelo }).HasName("pk_lignepanier");

            entity.ToTable("lignepanier", "upways");

            entity.HasIndex(e => e.Idassurance, "idx_linepanier_idassurance");

            entity.HasIndex(e => e.Idpanier, "idx_linepanier_idpanier");

            entity.HasIndex(e => e.Idvelo, "idx_linepanier_idvelo");

            entity.Property(e => e.Idpanier).HasColumnName("idpanier");
            entity.Property(e => e.Idvelo).HasColumnName("idvelo");
            entity.Property(e => e.Idassurance).HasColumnName("idassurance");
            entity.Property(e => e.Prixquantite)
                .HasPrecision(11, 2)
                .HasColumnName("prixquantite");
            entity.Property(e => e.Quantitepanier)
                .HasPrecision(2)
                .HasColumnName("quantitepanier");

            entity.HasOne(d => d.IdassuranceNavigation).WithMany(p => p.Lignepaniers)
                .HasForeignKey(d => d.Idassurance)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_lignepan_estpresen_assuranc");

            entity.HasOne(d => d.IdpanierNavigation).WithMany(p => p.Lignepaniers)
                .HasForeignKey(d => d.Idpanier)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_lignepan_contenir_panier");

            entity.HasOne(d => d.IdveloNavigation).WithMany(p => p.Lignepaniers)
                .HasForeignKey(d => d.Idvelo)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_lignepan_estvisibl_velo");
        });

        modelBuilder.Entity<Magasin>(entity =>
        {
            entity.HasKey(e => e.Idmagasin).HasName("pk_magasin");

            entity.ToTable("magasin", "upways");

            entity.Property(e => e.Idmagasin)
                .HasDefaultValueSql("nextval('magasin_idmagasin_seq'::regclass)")
                .HasColumnName("idmagasin");
            entity.Property(e => e.Cpmagasin)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("cpmagasin");
            entity.Property(e => e.Horairemagasin)
                .HasMaxLength(200)
                .HasColumnName("horairemagasin");
            entity.Property(e => e.Nommagasin)
                .HasMaxLength(50)
                .HasColumnName("nommagasin");
            entity.Property(e => e.Ruemagasin)
                .HasMaxLength(100)
                .HasColumnName("ruemagasin");
            entity.Property(e => e.Villemagasin)
                .HasMaxLength(50)
                .HasColumnName("villemagasin");

            entity.HasMany(d => d.Idvelos).WithMany(p => p.Idmagasins)
                .UsingEntity<Dictionary<string, object>>(
                    "Estdisponible",
                    r => r.HasOne<Velo>().WithMany()
                        .HasForeignKey("Idvelo")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_estdispo_estdispon_velo"),
                    l => l.HasOne<Magasin>().WithMany()
                        .HasForeignKey("Idmagasin")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_estdispo_estdispon_magasin"),
                    j =>
                    {
                        j.HasKey("Idmagasin", "Idvelo").HasName("pk_estdisponible");
                        j.ToTable("estdisponible", "upways");
                        j.HasIndex(new[] { "Idmagasin" }, "idx_estdisponible_idmagasin");
                        j.IndexerProperty<int>("Idmagasin").HasColumnName("idmagasin");
                        j.IndexerProperty<int>("Idvelo").HasColumnName("idvelo");
                    });
        });

        modelBuilder.Entity<Marquagevelo>(entity =>
        {
            entity.HasKey(e => e.Codemarquage).HasName("pk_marquagevelo");

            entity.ToTable("marquagevelo", "upways");

            entity.HasIndex(e => e.Codemarquage, "codemarquage_unq").IsUnique();

            entity.HasIndex(e => new { e.Idpanier, e.Idvelo }, "idx_marquagevelo_idpanier_idvelo");

            entity.Property(e => e.Codemarquage)
                .HasMaxLength(10)
                .HasColumnName("codemarquage");
            entity.Property(e => e.Idpanier).HasColumnName("idpanier");
            entity.Property(e => e.Idvelo).HasColumnName("idvelo");
            entity.Property(e => e.Prixmarquage)
                .HasPrecision(4, 2)
                .HasColumnName("prixmarquage");

            entity.HasOne(d => d.Lignepanier).WithMany(p => p.Marquagevelos)
                .HasForeignKey(d => new { d.Idpanier, d.Idvelo })
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_marquage_estobliga_lignepan");
        });

        modelBuilder.Entity<Marque>(entity =>
        {
            entity.HasKey(e => e.Idmarque).HasName("pk_marque");

            entity.ToTable("marque", "upways");

            entity.HasIndex(e => e.Nommarque, "nommarque_unq").IsUnique();

            entity.Property(e => e.Idmarque)
                .HasDefaultValueSql("nextval('marque_idmarque_seq'::regclass)")
                .HasColumnName("idmarque");
            entity.Property(e => e.Nommarque)
                .HasMaxLength(100)
                .HasColumnName("nommarque");
        });

        modelBuilder.Entity<Mentionvelo>(entity =>
        {
            entity.HasKey(e => e.Idmention).HasName("pk_mentionvelo");

            entity.ToTable("mentionvelo", "upways");

            entity.HasIndex(e => e.Idvelo, "idx_mentionvelo_idvelo");

            entity.Property(e => e.Idmention)
                .HasDefaultValueSql("nextval('mentionvelo_idmention_seq'::regclass)")
                .HasColumnName("idmention");
            entity.Property(e => e.Idvelo).HasColumnName("idvelo");
            entity.Property(e => e.Libellemention)
                .HasMaxLength(50)
                .HasColumnName("libellemention");
            entity.Property(e => e.Valeurmention)
                .HasMaxLength(4096)
                .HasColumnName("valeurmention");

            entity.HasOne(d => d.IdveloNavigation).WithMany(p => p.Mentionvelos)
                .HasForeignKey(d => d.Idvelo)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_mentionv_mentionne_velo");
        });

        modelBuilder.Entity<Moteur>(entity =>
        {
            entity.HasKey(e => e.Idmoteur).HasName("pk_moteur");

            entity.ToTable("moteur", "upways");

            entity.HasIndex(e => e.Idmarque, "idx_moteur_idmarque");

            entity.Property(e => e.Idmoteur)
                .HasDefaultValueSql("nextval('moteur_idmoteur_seq'::regclass)")
                .HasColumnName("idmoteur");
            entity.Property(e => e.Couplemoteur)
                .HasMaxLength(10)
                .HasColumnName("couplemoteur");
            entity.Property(e => e.Idmarque).HasColumnName("idmarque");
            entity.Property(e => e.Modelemoteur)
                .HasMaxLength(50)
                .HasColumnName("modelemoteur");
            entity.Property(e => e.Vitessemaximal)
                .HasMaxLength(10)
                .HasColumnName("vitessemaximal");

            entity.HasOne(d => d.IdmarqueNavigation).WithMany(p => p.Moteurs)
                .HasForeignKey(d => d.Idmarque)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_moteur_produire_marque");
        });

        modelBuilder.Entity<Panier>(entity =>
        {
            entity.HasKey(e => e.Idpanier).HasName("pk_panier");

            entity.ToTable("panier", "upways");

            entity.HasIndex(e => e.Idcommande, "idx_panier_idcommannde");

            entity.Property(e => e.Idpanier)
                .HasDefaultValueSql("nextval('panier_idpanier_seq'::regclass)")
                .HasColumnName("idpanier");
            entity.Property(e => e.Cookie)
                .HasMaxLength(255)
                .HasColumnName("cookie");
            entity.Property(e => e.Idclient).HasColumnName("idclient");
            entity.Property(e => e.Idcommande).HasColumnName("idcommande");
            entity.Property(e => e.Prixpanier)
                .HasPrecision(11, 2)
                .HasColumnName("prixpanier");

            entity.HasOne(d => d.IdclientNavigation).WithMany(p => p.Paniers)
                .HasForeignKey(d => d.Idclient)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_panier_appartient_client");

            entity.HasOne(d => d.IdcommandeNavigation).WithMany(p => p.Paniers)
                .HasForeignKey(d => d.Idcommande)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_panier_estlie2_detailco");
        });

        modelBuilder.Entity<Photoaccessoire>(entity =>
        {
            entity.HasKey(e => e.Idphotoaccessoire).HasName("pk_photoaccessoire");

            entity.ToTable("photoaccessoire", "upways");

            entity.HasIndex(e => e.Idaccessoire, "idx_photoaccessoire_idaccessoire");

            entity.Property(e => e.Idphotoaccessoire)
                .HasDefaultValueSql("nextval('photoaccessoire_idphotoaccessoire_seq'::regclass)")
                .HasColumnName("idphotoaccessoire");
            entity.Property(e => e.Idaccessoire).HasColumnName("idaccessoire");
            entity.Property(e => e.Urlphotoaccessoire).HasColumnName("urlphotoaccessoire");

            entity.HasOne(d => d.IdaccessoireNavigation).WithMany(p => p.Photoaccessoires)
                .HasForeignKey(d => d.Idaccessoire)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_photoacc_comprendr_accessoi");
        });

        modelBuilder.Entity<Photovelo>(entity =>
        {
            entity.HasKey(e => e.Idphotovelo).HasName("pk_photovelo");

            entity.ToTable("photovelo", "upways");

            entity.HasIndex(e => e.Idvelo, "idx_photovelo_idvelo");

            entity.Property(e => e.Idphotovelo)
                .HasDefaultValueSql("nextval('photovelo_idphotovelo_seq'::regclass)")
                .HasColumnName("idphotovelo");
            entity.Property(e => e.Idvelo).HasColumnName("idvelo");
            entity.Property(e => e.Photobytea).HasColumnName("photobytea");
            entity.Property(e => e.Urlphotovelo)
                .HasMaxLength(4096)
                .HasColumnName("urlphotovelo");

            entity.HasOne(d => d.IdveloNavigation).WithMany(p => p.Photovelos)
                .HasForeignKey(d => d.Idvelo)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_photovel_represent_velo");
        });

        modelBuilder.Entity<Rapportinspection>(entity =>
        {
            entity.HasKey(e => e.Idinspection).HasName("pk_rapportinspection");

            entity.ToTable("rapportinspection", "upways");

            entity.Property(e => e.Idinspection)
                .HasDefaultValueSql("nextval('rapportinspection_idinspection_seq'::regclass)")
                .HasColumnName("idinspection");
            entity.Property(e => e.Pointdinspection)
                .HasMaxLength(200)
                .HasColumnName("pointdinspection");
            entity.Property(e => e.Soustypeinspection)
                .HasMaxLength(200)
                .HasColumnName("soustypeinspection");
            entity.Property(e => e.Typeinspection)
                .HasMaxLength(200)
                .HasColumnName("typeinspection");
        });

        modelBuilder.Entity<Reparationvelo>(entity =>
        {
            entity.HasKey(e => e.Idreparation).HasName("pk_reparationvelo");

            entity.ToTable("reparationvelo", "upways");

            entity.Property(e => e.Idreparation)
                .HasDefaultValueSql("nextval('reparationvelo_idreparation_seq'::regclass)")
                .HasColumnName("idreparation");
            entity.Property(e => e.Checkreparation).HasColumnName("checkreparation");
            entity.Property(e => e.Checkvalidation).HasColumnName("checkvalidation");
        });

        modelBuilder.Entity<Retraitmagasin>(entity =>
        {
            entity.HasKey(e => e.Idretraitmagasin).HasName("pk_retraitmagasin");

            entity.ToTable("retraitmagasin", "upways");

            entity.HasIndex(e => e.Idcommande, "idx_retraitmagasin_idcommande");

            entity.HasIndex(e => e.Idinformations, "idx_retraitmagasin_idinformations");

            entity.HasIndex(e => e.Idmagasin, "idx_retraitmagasin_idmagasin");

            entity.Property(e => e.Idretraitmagasin)
                .HasDefaultValueSql("nextval('retraitmagasin_idretraitmagasin_seq'::regclass)")
                .HasColumnName("idretraitmagasin");
            entity.Property(e => e.Dateretrait).HasColumnName("dateretrait");
            entity.Property(e => e.Heureretrait).HasColumnName("heureretrait");
            entity.Property(e => e.Idcommande).HasColumnName("idcommande");
            entity.Property(e => e.Idinformations).HasColumnName("idinformations");
            entity.Property(e => e.Idmagasin).HasColumnName("idmagasin");

            entity.HasOne(d => d.IdcommandeNavigation).WithMany(p => p.Retraitmagasins)
                .HasForeignKey(d => d.Idcommande)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_retraitm_monter2_detailco");

            entity.HasOne(d => d.IdinformationsNavigation).WithMany(p => p.Retraitmagasins)
                .HasForeignKey(d => d.Idinformations)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_retraitm_choisir_informat");

            entity.HasOne(d => d.IdmagasinNavigation).WithMany(p => p.Retraitmagasins)
                .HasForeignKey(d => d.Idmagasin)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_retraitm_estfait_magasin");
        });

        modelBuilder.Entity<Testvelo>(entity =>
        {
            entity.HasKey(e => e.Idtest).HasName("pk_testvelo");

            entity.ToTable("testvelo", "upways");

            entity.HasIndex(e => e.Idmagasin, "idx_testvelo_idmagasin");

            entity.HasIndex(e => e.Idvelo, "idx_testvelo_idvelo");

            entity.Property(e => e.Idtest)
                .HasDefaultValueSql("nextval('testvelo_idtest_seq'::regclass)")
                .HasColumnName("idtest");
            entity.Property(e => e.Datetest).HasColumnName("datetest");
            entity.Property(e => e.Heuretest).HasColumnName("heuretest");
            entity.Property(e => e.Idmagasin).HasColumnName("idmagasin");
            entity.Property(e => e.Idvelo).HasColumnName("idvelo");

            entity.HasOne(d => d.IdmagasinNavigation).WithMany(p => p.Testvelos)
                .HasForeignKey(d => d.Idmagasin)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_testvelo_sederoule_magasin");

            entity.HasOne(d => d.IdveloNavigation).WithMany(p => p.Testvelos)
                .HasForeignKey(d => d.Idvelo)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_testvelo_essayer_velo");
        });

        modelBuilder.Entity<Utilite>(entity =>
        {
            entity.HasKey(e => new { e.Idutilite, e.Idvelo }).HasName("pk_utilite");

            entity.ToTable("utilite", "upways");

            entity.HasIndex(e => e.Idvelo, "idx_utilite_idvelo");

            entity.Property(e => e.Idutilite)
                .HasDefaultValueSql("nextval('utilite_idutilite_seq'::regclass)")
                .HasColumnName("idutilite");
            entity.Property(e => e.Idvelo).HasColumnName("idvelo");
            entity.Property(e => e.Valeurutilite).HasColumnName("valeurutilite");

            entity.HasOne(d => d.IdveloNavigation).WithMany(p => p.Utilites)
                .HasForeignKey(d => d.Idvelo)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_utilite_note_velo");
        });

        modelBuilder.Entity<Vadresse>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vadresse", "upways");

            entity.Property(e => e.Idadressefact).HasColumnName("idadressefact");
            entity.Property(e => e.Paysfact)
                .HasMaxLength(50)
                .HasColumnName("paysfact");
            entity.Property(e => e.Regionfact)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("regionfact");
            entity.Property(e => e.Villefact)
                .HasMaxLength(100)
                .HasColumnName("villefact");
        });

        modelBuilder.Entity<Vcaracteristiquevelo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vcaracteristiquevelo", "upways");

            entity.Property(e => e.Amortisseur)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("amortisseur");
            entity.Property(e => e.Capacitebatterie)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("capacitebatterie");
            entity.Property(e => e.Caracteristique).HasColumnName("caracteristique");
            entity.Property(e => e.Couleur)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("couleur");
            entity.Property(e => e.Couplemoteur)
                .HasMaxLength(10)
                .HasColumnName("couplemoteur");
            entity.Property(e => e.Debattement).HasColumnName("debattement");
            entity.Property(e => e.Debattementamortisseur).HasColumnName("debattementamortisseur");
            entity.Property(e => e.Etatbatterie)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("etatbatterie");
            entity.Property(e => e.Fourche)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("fourche");
            entity.Property(e => e.Freins)
                .HasMaxLength(30)
                .IsFixedLength()
                .HasColumnName("freins");
            entity.Property(e => e.Idvelo).HasColumnName("idvelo");
            entity.Property(e => e.Libellecategorie)
                .HasMaxLength(100)
                .HasColumnName("libellecategorie");
            entity.Property(e => e.Materiau)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("materiau");
            entity.Property(e => e.Modelemoteur)
                .HasMaxLength(50)
                .HasColumnName("modelemoteur");
            entity.Property(e => e.Modeltransmission)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("modeltransmission");
            entity.Property(e => e.Nombrecycle).HasColumnName("nombrecycle");
            entity.Property(e => e.Nombrekms)
                .HasMaxLength(15)
                .IsFixedLength()
                .HasColumnName("nombrekms");
            entity.Property(e => e.Nombrevitesse).HasColumnName("nombrevitesse");
            entity.Property(e => e.Nommarque)
                .HasMaxLength(100)
                .HasColumnName("nommarque");
            entity.Property(e => e.Nomvelo)
                .HasMaxLength(200)
                .HasColumnName("nomvelo");
            entity.Property(e => e.Pneus)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("pneus");
            entity.Property(e => e.Poids)
                .HasPrecision(5, 2)
                .HasColumnName("poids");
            entity.Property(e => e.Positionmoteur)
                .HasMaxLength(20)
                .HasColumnName("positionmoteur");
            entity.Property(e => e.Selletelescopique).HasColumnName("selletelescopique");
            entity.Property(e => e.Taillemax)
                .HasMaxLength(15)
                .IsFixedLength()
                .HasColumnName("taillemax");
            entity.Property(e => e.Taillemin)
                .HasMaxLength(15)
                .IsFixedLength()
                .HasColumnName("taillemin");
            entity.Property(e => e.Taillesroues).HasColumnName("taillesroues");
            entity.Property(e => e.Tubeselle).HasColumnName("tubeselle");
            entity.Property(e => e.Typecargo)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("typecargo");
            entity.Property(e => e.Typesuspension)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("typesuspension");
            entity.Property(e => e.Vitessemaximal)
                .HasMaxLength(10)
                .HasColumnName("vitessemaximal");
        });

        modelBuilder.Entity<Vcommande>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vcommande", "upways");

            entity.Property(e => e.Idclient).HasColumnName("idclient");
            entity.Property(e => e.Idcommande).HasColumnName("idcommande");
            entity.Property(e => e.Idpanier).HasColumnName("idpanier");
            entity.Property(e => e.Idvelo).HasColumnName("idvelo");
            entity.Property(e => e.Prixpanier)
                .HasPrecision(11, 2)
                .HasColumnName("prixpanier");
            entity.Property(e => e.Titreassurance)
                .HasMaxLength(50)
                .HasColumnName("titreassurance");
        });

        modelBuilder.Entity<Vdpo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vdpo", "upways");

            entity.Property(e => e.Daterequetedpo).HasColumnName("daterequetedpo");
            entity.Property(e => e.Idclient).HasColumnName("idclient");
            entity.Property(e => e.Iddpo).HasColumnName("iddpo");
            entity.Property(e => e.Loginclient)
                .HasMaxLength(20)
                .HasColumnName("loginclient");
            entity.Property(e => e.Typeoperation)
                .HasMaxLength(20)
                .HasColumnName("typeoperation");
        });

        modelBuilder.Entity<Velo>(entity =>
        {
            entity.HasKey(e => e.Idvelo).HasName("pk_velo");

            entity.ToTable("velo", "upways");

            entity.HasIndex(e => e.Idcaracteristiquevelo, "idx_velo_idcaracteristiquevelo");

            entity.HasIndex(e => e.Idcategorie, "idx_velo_idcategorie");

            entity.HasIndex(e => e.Idmarque, "idx_velo_idmarque");

            entity.HasIndex(e => e.Idmoteur, "idx_velo_idmoteur");

            entity.Property(e => e.Idvelo)
                .HasDefaultValueSql("nextval('velo_idvelo_seq'::regclass)")
                .HasColumnName("idvelo");
            entity.Property(e => e.Anneevelo)
                .HasPrecision(4)
                .HasColumnName("anneevelo");
            entity.Property(e => e.Capacitebatterie)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("capacitebatterie");
            entity.Property(e => e.Descriptifvelo)
                .HasMaxLength(5000)
                .HasColumnName("descriptifvelo");
            entity.Property(e => e.Idcaracteristiquevelo).HasColumnName("idcaracteristiquevelo");
            entity.Property(e => e.Idcategorie).HasColumnName("idcategorie");
            entity.Property(e => e.Idmarque).HasColumnName("idmarque");
            entity.Property(e => e.Idmoteur).HasColumnName("idmoteur");
            entity.Property(e => e.Nombrekms)
                .HasMaxLength(15)
                .IsFixedLength()
                .HasColumnName("nombrekms");
            entity.Property(e => e.Nomvelo)
                .HasMaxLength(200)
                .HasColumnName("nomvelo");
            entity.Property(e => e.Positionmoteur)
                .HasMaxLength(20)
                .HasColumnName("positionmoteur");
            entity.Property(e => e.Pourcentagereduction)
                .HasPrecision(3)
                .HasColumnName("pourcentagereduction");
            entity.Property(e => e.Prixneuf)
                .HasPrecision(5)
                .HasColumnName("prixneuf");
            entity.Property(e => e.Prixremise)
                .HasPrecision(5)
                .HasColumnName("prixremise");
            entity.Property(e => e.Quantitevelo)
                .HasPrecision(3)
                .HasColumnName("quantitevelo");
            entity.Property(e => e.Taillemax)
                .HasMaxLength(15)
                .IsFixedLength()
                .HasColumnName("taillemax");
            entity.Property(e => e.Taillemin)
                .HasMaxLength(15)
                .IsFixedLength()
                .HasColumnName("taillemin");

            entity.HasOne(d => d.IdcaracteristiqueveloNavigation).WithMany(p => p.Velos)
                .HasForeignKey(d => d.Idcaracteristiquevelo)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_batterie_integrer_velo");

            entity.HasOne(d => d.IdcategorieNavigation).WithMany(p => p.Velos)
                .HasForeignKey(d => d.Idcategorie)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_velo_apparteni_categori");

            entity.HasOne(d => d.IdmarqueNavigation).WithMany(p => p.Velos)
                .HasForeignKey(d => d.Idmarque)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_velo_fabriquer_marque");

            entity.HasOne(d => d.IdmoteurNavigation).WithMany(p => p.Velos)
                .HasForeignKey(d => d.Idmoteur)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_moteur_integrer_velo");
        });

        modelBuilder.Entity<Velomodifier>(entity =>
        {
            entity.HasKey(e => e.Idvelom).HasName("pk_velomodif");

            entity.ToTable("velomodifier", "upways");

            entity.HasIndex(e => e.Idcaracteristiquevelo, "idx_velomodifier_idcaracteristiquevelomodifier");

            entity.HasIndex(e => e.Idcategorie, "idx_velomodifier_idcategorie");

            entity.HasIndex(e => e.Idmarque, "idx_velomodifier_idmarque");

            entity.HasIndex(e => e.Idmoteur, "idx_velomodifier_idmoteur");

            entity.Property(e => e.Idvelom)
                .HasDefaultValueSql("nextval('velomodifier_idvelom_seq'::regclass)")
                .HasColumnName("idvelom");
            entity.Property(e => e.Ancienprix)
                .HasPrecision(5)
                .HasColumnName("ancienprix");
            entity.Property(e => e.Anneevelo)
                .HasPrecision(4)
                .HasColumnName("anneevelo");
            entity.Property(e => e.Capacitebatterie)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("capacitebatterie");
            entity.Property(e => e.Descriptifvelo)
                .HasMaxLength(5000)
                .HasColumnName("descriptifvelo");
            entity.Property(e => e.Idcaracteristiquevelo).HasColumnName("idcaracteristiquevelo");
            entity.Property(e => e.Idcategorie).HasColumnName("idcategorie");
            entity.Property(e => e.Idmarque).HasColumnName("idmarque");
            entity.Property(e => e.Idmoteur).HasColumnName("idmoteur");
            entity.Property(e => e.Idvelomodif).HasColumnName("idvelomodif");
            entity.Property(e => e.Modifier)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifier");
            entity.Property(e => e.Nombrekms)
                .HasMaxLength(15)
                .IsFixedLength()
                .HasColumnName("nombrekms");
            entity.Property(e => e.Nomvelo)
                .HasMaxLength(200)
                .HasColumnName("nomvelo");
            entity.Property(e => e.Positionmoteur)
                .HasMaxLength(20)
                .HasColumnName("positionmoteur");
            entity.Property(e => e.Pourcentagereduction)
                .HasPrecision(3)
                .HasColumnName("pourcentagereduction");
            entity.Property(e => e.Prixneuf)
                .HasPrecision(5)
                .HasColumnName("prixneuf");
            entity.Property(e => e.Prixremise)
                .HasPrecision(5)
                .HasColumnName("prixremise");
            entity.Property(e => e.Quantitevelo)
                .HasPrecision(3)
                .HasColumnName("quantitevelo");
            entity.Property(e => e.Taillemax)
                .HasMaxLength(15)
                .IsFixedLength()
                .HasColumnName("taillemax");
            entity.Property(e => e.Taillemin)
                .HasMaxLength(15)
                .IsFixedLength()
                .HasColumnName("taillemin");

            entity.HasOne(d => d.IdcaracteristiqueveloNavigation).WithMany(p => p.Velomodifiers)
                .HasForeignKey(d => d.Idcaracteristiquevelo)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_batterie_integrer_velomodifier");

            entity.HasOne(d => d.IdcategorieNavigation).WithMany(p => p.Velomodifiers)
                .HasForeignKey(d => d.Idcategorie)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_velomodifier_apparteni_categori");

            entity.HasOne(d => d.IdmarqueNavigation).WithMany(p => p.Velomodifiers)
                .HasForeignKey(d => d.Idmarque)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_velomodifier_fabriquer_marque");

            entity.HasOne(d => d.IdmoteurNavigation).WithMany(p => p.Velomodifiers)
                .HasForeignKey(d => d.Idmoteur)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_moteur_integrer_velomodifier");
        });

        modelBuilder.Entity<Vvelo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vvelo", "upways");

            entity.Property(e => e.Anneevelo)
                .HasPrecision(4)
                .HasColumnName("anneevelo");
            entity.Property(e => e.Descriptifvelo)
                .HasMaxLength(5000)
                .HasColumnName("descriptifvelo");
            entity.Property(e => e.Libellecategorie)
                .HasMaxLength(100)
                .HasColumnName("libellecategorie");
            entity.Property(e => e.Libellemention)
                .HasMaxLength(50)
                .HasColumnName("libellemention");
            entity.Property(e => e.Nombrekms)
                .HasMaxLength(15)
                .IsFixedLength()
                .HasColumnName("nombrekms");
            entity.Property(e => e.Nommarque)
                .HasMaxLength(100)
                .HasColumnName("nommarque");
            entity.Property(e => e.Nomvelo)
                .HasMaxLength(200)
                .HasColumnName("nomvelo");
            entity.Property(e => e.Pourcentagereduction)
                .HasPrecision(3)
                .HasColumnName("pourcentagereduction");
            entity.Property(e => e.Prixneuf)
                .HasPrecision(5)
                .HasColumnName("prixneuf");
            entity.Property(e => e.Prixremise)
                .HasPrecision(5)
                .HasColumnName("prixremise");
            entity.Property(e => e.Taillemax)
                .HasMaxLength(15)
                .IsFixedLength()
                .HasColumnName("taillemax");
            entity.Property(e => e.Taillemin)
                .HasMaxLength(15)
                .IsFixedLength()
                .HasColumnName("taillemin");
        });

        modelBuilder.Entity<Vventenombreacessoire>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vventenombreacessoire", "upways");

            entity.Property(e => e.Dateachat).HasColumnName("dateachat");
            entity.Property(e => e.Idaccessoire).HasColumnName("idaccessoire");
            entity.Property(e => e.Idadressefact).HasColumnName("idadressefact");
            entity.Property(e => e.Libellecategorie)
                .HasMaxLength(100)
                .HasColumnName("libellecategorie");
            entity.Property(e => e.Nomaccessoire)
                .HasMaxLength(100)
                .HasColumnName("nomaccessoire");
            entity.Property(e => e.Nommarque)
                .HasMaxLength(100)
                .HasColumnName("nommarque");
            entity.Property(e => e.Prixpanier)
                .HasPrecision(11, 2)
                .HasColumnName("prixpanier");
        });

        modelBuilder.Entity<Vventenombrevelo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vventenombrevelo", "upways");

            entity.Property(e => e.Dateachat).HasColumnName("dateachat");
            entity.Property(e => e.Idadressefact).HasColumnName("idadressefact");
            entity.Property(e => e.Idvelo).HasColumnName("idvelo");
            entity.Property(e => e.Libellecategorie)
                .HasMaxLength(100)
                .HasColumnName("libellecategorie");
            entity.Property(e => e.Modelemoteur)
                .HasMaxLength(50)
                .HasColumnName("modelemoteur");
            entity.Property(e => e.Nommarque)
                .HasMaxLength(100)
                .HasColumnName("nommarque");
            entity.Property(e => e.Nomvelo)
                .HasMaxLength(200)
                .HasColumnName("nomvelo");
            entity.Property(e => e.Prixpanier)
                .HasPrecision(11, 2)
                .HasColumnName("prixpanier");
            entity.Property(e => e.Taillemax)
                .HasMaxLength(15)
                .IsFixedLength()
                .HasColumnName("taillemax");
            entity.Property(e => e.Taillemin)
                .HasMaxLength(15)
                .IsFixedLength()
                .HasColumnName("taillemin");
            entity.Property(e => e.Titreassurance)
                .HasMaxLength(50)
                .HasColumnName("titreassurance");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
