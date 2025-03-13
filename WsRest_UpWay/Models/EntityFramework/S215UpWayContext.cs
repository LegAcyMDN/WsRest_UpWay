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
            entity.HasKey(e => e.AccessoireId).HasName("pk_accessoire");

            entity.Property(e => e.AccessoireId).HasDefaultValueSql("nextval('accessoire_idaccessoire_seq'::regclass)");

            entity.HasOne(d => d.AccessoireCategorie).WithMany(p => p.ListeAccessoires)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_accessoi_classifie_categori");

            entity.HasOne(d => d.AccessoireMarque).WithMany(p => p.ListeAccessoires)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_accessoi_distribue_marque");

            entity.HasMany(d => d.ListeVelos).WithMany(p => p.ListeAccessoires)
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
            entity.HasKey(e => e.AdresseExpeId).HasName("pk_adresseexpedition");

            entity.Property(e => e.AdresseExpeId).HasDefaultValueSql("nextval('adresseexpedition_idadresseexp_seq'::regclass)");
            entity.Property(e => e.RegionExpedition).IsFixedLength();

            entity.HasOne(d => d.AdresseExpeFact).WithMany(p => p.ListeAdresseExpe)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_adressee_assimiler_adressef");

            entity.HasOne(d => d.AdresseExpeClient).WithMany(p => p.ListeAdresseExpe)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_adressee_modifier_comptecl");
        });

        modelBuilder.Entity<Adressefacturation>(entity =>
        {
            entity.HasKey(e => e.AdresseFactId).HasName("pk_adressefacturation");

            entity.Property(e => e.AdresseFactId).HasDefaultValueSql("nextval('adressefacturation_idadressefact_seq'::regclass)");
            entity.Property(e => e.RegionFacturation).IsFixedLength();

            entity.HasOne(d => d.AdresseFactExpe).WithMany(p => p.ListeAdresseFact)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_adressef_assimiler_adressee");

            entity.HasOne(d => d.AdresseFactClient).WithMany(p => p.ListeAdresseFact)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_adressef_rectifier_comptecl");
        });

        modelBuilder.Entity<Ajouteraccessoire>(entity =>
        {
            entity.HasKey(e => new { e.AccessoireId, e.PanierId }).HasName("pk_ajouteraccessoire");

            entity.Property(e => e.QuantiteAccessoire).HasDefaultValueSql("1");

            entity.HasOne(d => d.AjoutDAccessoire).WithMany(p => p.ListeAjoutAccessoires)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_ajoutera_ajouterac_accessoi");

            entity.HasOne(d => d.AjoutDAccessoirePanier).WithMany(p => p.ListeAjouterAccessoires)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_ajoutera_ajouterac_panier");
        });

        modelBuilder.Entity<Alertevelo>(entity =>
        {
            entity.HasKey(e => new { e.AlerteId, e.ClientId, e.VeloId }).HasName("pk_alertevelo");

            entity.Property(e => e.AlerteId).HasDefaultValueSql("nextval('alertevelo_idalerte_seq'::regclass)");

            entity.HasOne(d => d.AlerteClient).WithMany(p => p.ListeAlerteVelos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_alertevelo_client");

            entity.HasOne(d => d.AlerteVelo).WithMany(p => p.ListeAlerteVelos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_alertevelo_velo");
        });

        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.ArticleId).HasName("pk_article");

            entity.Property(e => e.ArticleId).HasDefaultValueSql("nextval('article_idarticle_seq'::regclass)");

            entity.HasOne(d => d.ArticleCategorieArt).WithMany(p => p.ListeArticles)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_article");
        });

        modelBuilder.Entity<Assurance>(entity =>
        {
            entity.HasKey(e => e.AssuranceId).HasName("pk_assurance");

            entity.Property(e => e.AssuranceId).HasDefaultValueSql("nextval('assurance_idassurance_seq'::regclass)");
        });

        modelBuilder.Entity<Caracteristique>(entity =>
        {
            entity.HasKey(e => e.CaracteristiqueId).HasName("pk_caracteristique");

            entity.Property(e => e.CaracteristiqueId).HasDefaultValueSql("nextval('caracteristique_idcaracteristique_seq'::regclass)");

            entity.HasMany(d => d.ListeSousCaracteristiques).WithMany(p => p.ListeCaracteristiques)
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

            entity.HasMany(d => d.ListeCaracteristiques).WithMany(p => p.ListeSousCaracteristiques)
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

            entity.HasMany(d => d.ListeCategories).WithMany(p => p.ListeCaracteristiques)
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

            entity.HasMany(d => d.ListeVelos).WithMany(p => p.ListeCaracteristiques)
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
            entity.HasKey(e => e.CaracteristiqueVeloId).HasName("pk_caracteristiquevelo");

            entity.Property(e => e.CaracteristiqueVeloId).HasDefaultValueSql("nextval('caracteristiquevelo_idcaracteristiquevelo_seq'::regclass)");
            entity.Property(e => e.Amortisseur).IsFixedLength();
            entity.Property(e => e.Couleur).IsFixedLength();
            entity.Property(e => e.EtatBatterie).IsFixedLength();
            entity.Property(e => e.Fourche).IsFixedLength();
            entity.Property(e => e.Freins).IsFixedLength();
            entity.Property(e => e.Materiau).IsFixedLength();
            entity.Property(e => e.ModelTransmission).IsFixedLength();
            entity.Property(e => e.Pneus).IsFixedLength();
            entity.Property(e => e.TypeCargo).IsFixedLength();
            entity.Property(e => e.TypeSuspension).IsFixedLength();
        });

        modelBuilder.Entity<Categorie>(entity =>
        {
            entity.HasKey(e => e.Idcategorie).HasName("pk_categorie");

            entity.Property(e => e.Idcategorie).HasDefaultValueSql("nextval('categorie_idcategorie_seq'::regclass)");
        });

        modelBuilder.Entity<CategorieArticle>(entity =>
        {
            entity.HasKey(e => e.CategorieArticleId).HasName("pk_categorie_article");

            entity.Property(e => e.CategorieArticleId).HasDefaultValueSql("nextval('categorie_article_idcategorie_article_seq'::regclass)");

            entity.HasMany(d => d.ListeSousCategorieArticles).WithMany(p => p.ListeCategorieArticles)
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

            entity.HasMany(d => d.ListeCategorieArticles).WithMany(p => p.ListeSousCategorieArticles)
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
            entity.HasKey(e => e.ReductionId).HasName("pk_codereduction");
        });

        modelBuilder.Entity<Compteclient>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("pk_compteclient");

            entity.Property(e => e.ClientId).HasDefaultValueSql("nextval('compteclient_idclient_seq'::regclass)");
        });

        modelBuilder.Entity<ContenuArticle>(entity =>
        {
            entity.HasKey(e => e.ContenueId).HasName("pk_contenu_article");

            entity.Property(e => e.ContenueId).HasDefaultValueSql("nextval('contenu_article_idcontenue_seq'::regclass)");

            entity.HasOne(d => d.ContenuArticleArt).WithMany(p => p.ListeContenuArticles)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_contenu_article_article");
        });

        modelBuilder.Entity<Detailcommande>(entity =>
        {
            entity.HasKey(e => e.CommandeId).HasName("pk_detailcommande");

            entity.Property(e => e.CommandeId).HasDefaultValueSql("nextval('detailcommande_idcommande_seq'::regclass)");

            entity.HasOne(d => d.DetailComAdresseFact).WithMany(p => p.ListeDetailCommande)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_detailco_afficher2_adressef");

            entity.HasOne(d => d.DetailCommandeClient).WithMany(p => p.ListeDetailCommandes)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_detailco_visualise_comptecl");

            entity.HasOne(d => d.DetailCommandeEtat).WithMany(p => p.ListeDetailCommandes)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_detailco_indiquer_etatcomm");

            entity.HasOne(d => d.DetailCommandePanier).WithMany(p => p.ListeDetailCommandes)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_detailco_estlie_panier");

            entity.HasOne(d => d.DetailComRetraitMagasin).WithMany(p => p.ListeDetailCommandes)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_detailco_monter_retraitm");
        });

        modelBuilder.Entity<Dpo>(entity =>
        {
            entity.HasKey(e => e.DpoId).HasName("pk_dpo");

            entity.Property(e => e.DpoId).HasDefaultValueSql("nextval('dpo_iddpo_seq'::regclass)");
        });

        modelBuilder.Entity<Estrealise>(entity =>
        {
            entity.HasKey(e => new { e.VeloId, e.InspectionId, e.ReparationId }).HasName("pk_estrealise");

            entity.Property(e => e.DateInspection).IsFixedLength();

            entity.HasOne(d => d.EstRealiseRapportInspection).WithMany(p => p.ListeEstRealises)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_estreali_estrealis_rapporti");

            entity.HasOne(d => d.EstRealiseReparationVelo).WithMany(p => p.ListeEstRealises)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_estreali_estrealis_reparati");

            entity.HasOne(d => d.EstRealiseVelo).WithMany(p => p.ListeEstRealises)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_estreali_estrealis_velo");
        });

        modelBuilder.Entity<Etatcommande>(entity =>
        {
            entity.HasKey(e => e.EtatCommandeId).HasName("pk_etatcommande");

            entity.Property(e => e.EtatCommandeId).HasDefaultValueSql("nextval('etatcommande_idetatcommande_seq'::regclass)");
        });

        modelBuilder.Entity<Information>(entity =>
        {
            entity.HasKey(e => e.InformationId).HasName("pk_informations");

            entity.Property(e => e.InformationId).HasDefaultValueSql("nextval('informations_idinformations_seq'::regclass)");

            entity.HasOne(d => d.InformationAdresseExpe).WithMany(p => p.ListeInformations)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_informat_opter_adressee");

            entity.HasOne(d => d.InformationPanier).WithMany(p => p.ListeInformations)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_informat_regler_panier");

            entity.HasOne(d => d.InformationCodeReduction).WithMany(p => p.ListeInformations)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_informat_utiliser_coderedu");

            entity.HasOne(d => d.InformationRetraitMagasin).WithMany(p => p.ListeInformations)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_informat_choisir2_retraitm");
        });

        modelBuilder.Entity<Lignepanier>(entity =>
        {
            entity.HasKey(e => new { e.PanierId, e.VeloId }).HasName("pk_lignepanier");

            entity.HasOne(d => d.LignePanierAssurance).WithMany(p => p.ListeLignePaniers)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_lignepan_estpresen_assuranc");

            entity.HasOne(d => d.LignePanierPanier).WithMany(p => p.ListeLignePaniers)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_lignepan_contenir_panier");

            entity.HasOne(d => d.LignePanierVelo).WithMany(p => p.ListeLignePaniers)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_lignepan_estvisibl_velo");
        });

        modelBuilder.Entity<Magasin>(entity =>
        {
            entity.HasKey(e => e.MagasinId).HasName("pk_magasin");

            entity.Property(e => e.MagasinId).HasDefaultValueSql("nextval('magasin_idmagasin_seq'::regclass)");
            entity.Property(e => e.CPMagasin).IsFixedLength();

            entity.HasMany(d => d.ListeVelos).WithMany(p => p.ListeMagasins)
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
            entity.HasKey(e => e.CodeMarquage).HasName("pk_marquagevelo");

            entity.HasOne(d => d.MarquageVeloLignePanier).WithMany(p => p.ListeMarquageVelos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_marquage_estobliga_lignepan");
        });

        modelBuilder.Entity<Marque>(entity =>
        {
            entity.HasKey(e => e.MarqueId).HasName("pk_marque");

            entity.Property(e => e.MarqueId).HasDefaultValueSql("nextval('marque_idmarque_seq'::regclass)");
        });

        modelBuilder.Entity<Mentionvelo>(entity =>
        {
            entity.HasKey(e => e.MentionId).HasName("pk_mentionvelo");

            entity.Property(e => e.MentionId).HasDefaultValueSql("nextval('mentionvelo_idmention_seq'::regclass)");

            entity.HasOne(d => d.MentionVeloVelo).WithMany(p => p.ListeMentionVelos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_mentionv_mentionne_velo");
        });

        modelBuilder.Entity<Moteur>(entity =>
        {
            entity.HasKey(e => e.MoteurId).HasName("pk_moteur");

            entity.Property(e => e.MoteurId).HasDefaultValueSql("nextval('moteur_idmoteur_seq'::regclass)");

            entity.HasOne(d => d.MoteurMarque).WithMany(p => p.ListeMoteurs)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_moteur_produire_marque");
        });

        modelBuilder.Entity<Panier>(entity =>
        {
            entity.HasKey(e => e.PanierId).HasName("pk_panier");

            entity.Property(e => e.PanierId).HasDefaultValueSql("nextval('panier_idpanier_seq'::regclass)");

            entity.HasOne(d => d.PanierClient).WithMany(p => p.ListePaniers)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_panier_appartient_client");

            entity.HasOne(d => d.PanierDetailCommande).WithMany(p => p.ListePaniers)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_panier_estlie2_detailco");
        });

        modelBuilder.Entity<Photoaccessoire>(entity =>
        {
            entity.HasKey(e => e.PhotoAcessoireId).HasName("pk_photoaccessoire");

            entity.Property(e => e.PhotoAcessoireId).HasDefaultValueSql("nextval('photoaccessoire_idphotoaccessoire_seq'::regclass)");

            entity.HasOne(d => d.PhotoAccessoireAccessoire).WithMany(p => p.ListePhotoAccessoires)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_photoacc_comprendr_accessoi");
        });

        modelBuilder.Entity<Photovelo>(entity =>
        {
            entity.HasKey(e => e.PhotoVeloId).HasName("pk_photovelo");

            entity.Property(e => e.PhotoVeloId).HasDefaultValueSql("nextval('photovelo_idphotovelo_seq'::regclass)");

            entity.HasOne(d => d.PhotoVeloVelo).WithMany(p => p.ListePhotoVelos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_photovel_represent_velo");
        });

        modelBuilder.Entity<Rapportinspection>(entity =>
        {
            entity.HasKey(e => e.InspectionId).HasName("pk_rapportinspection");

            entity.Property(e => e.InspectionId).HasDefaultValueSql("nextval('rapportinspection_idinspection_seq'::regclass)");
        });

        modelBuilder.Entity<Reparationvelo>(entity =>
        {
            entity.HasKey(e => e.ReparationId).HasName("pk_reparationvelo");

            entity.Property(e => e.ReparationId).HasDefaultValueSql("nextval('reparationvelo_idreparation_seq'::regclass)");
        });

        modelBuilder.Entity<Retraitmagasin>(entity =>
        {
            entity.HasKey(e => e.RetraitMagasinId).HasName("pk_retraitmagasin");

            entity.Property(e => e.RetraitMagasinId).HasDefaultValueSql("nextval('retraitmagasin_idretraitmagasin_seq'::regclass)");

            entity.HasOne(d => d.RetraitMagasinDetailCom).WithMany(p => p.ListeRetraitMagasins)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_retraitm_monter2_detailco");

            entity.HasOne(d => d.RetraitMagasinInformation).WithMany(p => p.ListeRetraitMagasins)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_retraitm_choisir_informat");

            entity.HasOne(d => d.RetraitMagasinMagasin).WithMany(p => p.ListeRetraitMagasins)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_retraitm_estfait_magasin");
        });

        modelBuilder.Entity<Testvelo>(entity =>
        {
            entity.HasKey(e => e.TestId).HasName("pk_testvelo");

            entity.Property(e => e.TestId).HasDefaultValueSql("nextval('testvelo_idtest_seq'::regclass)");

            entity.HasOne(d => d.TestVeloMagasin).WithMany(p => p.ListeTestVelos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_testvelo_sederoule_magasin");

            entity.HasOne(d => d.TestVeloVelo).WithMany(p => p.ListeTestVelos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_testvelo_essayer_velo");
        });

        modelBuilder.Entity<Utilite>(entity =>
        {
            entity.HasKey(e => new { e.UtiliteId, e.VeloId }).HasName("pk_utilite");

            entity.Property(e => e.UtiliteId).HasDefaultValueSql("nextval('utilite_idutilite_seq'::regclass)");

            entity.HasOne(d => d.UtiliteVelo).WithMany(p => p.ListeUtilites)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_utilite_note_velo");
        });

        modelBuilder.Entity<Vadresse>(entity =>
        {
            entity.ToView("vadresse", "upways");

            entity.Property(e => e.Regionfact).IsFixedLength();
        });

        modelBuilder.Entity<Vcaracteristiquevelo>(entity =>
        {
            entity.ToView("vcaracteristiquevelo", "upways");

            entity.Property(e => e.Amortisseur).IsFixedLength();
            entity.Property(e => e.Capacitebatterie).IsFixedLength();
            entity.Property(e => e.Couleur).IsFixedLength();
            entity.Property(e => e.Etatbatterie).IsFixedLength();
            entity.Property(e => e.Fourche).IsFixedLength();
            entity.Property(e => e.Freins).IsFixedLength();
            entity.Property(e => e.Materiau).IsFixedLength();
            entity.Property(e => e.Modeltransmission).IsFixedLength();
            entity.Property(e => e.Nombrekms).IsFixedLength();
            entity.Property(e => e.Pneus).IsFixedLength();
            entity.Property(e => e.Taillemax).IsFixedLength();
            entity.Property(e => e.Taillemin).IsFixedLength();
            entity.Property(e => e.Typecargo).IsFixedLength();
            entity.Property(e => e.Typesuspension).IsFixedLength();
        });

        modelBuilder.Entity<Vcommande>(entity =>
        {
            entity.ToView("vcommande", "upways");
        });

        modelBuilder.Entity<Vdpo>(entity =>
        {
            entity.ToView("vdpo", "upways");
        });

        modelBuilder.Entity<Velo>(entity =>
        {
            entity.HasKey(e => e.VeloId).HasName("pk_velo");

            entity.Property(e => e.VeloId).HasDefaultValueSql("nextval('velo_idvelo_seq'::regclass)");
            entity.Property(e => e.CapaciteBatterie).IsFixedLength();
            entity.Property(e => e.NombreKms).IsFixedLength();
            entity.Property(e => e.TailleMax).IsFixedLength();
            entity.Property(e => e.TailleMin).IsFixedLength();

            entity.HasOne(d => d.VeloCaracteristiqueVelo).WithMany(p => p.ListeVelos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_batterie_integrer_velo");

            entity.HasOne(d => d.VeloCategorie).WithMany(p => p.ListeVelos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_velo_apparteni_categori");

            entity.HasOne(d => d.VeloMarque).WithMany(p => p.ListeVelos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_velo_fabriquer_marque");

            entity.HasOne(d => d.VeloMoteur).WithMany(p => p.ListeVelos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_moteur_integrer_velo");
        });

        modelBuilder.Entity<Velomodifier>(entity =>
        {
            entity.HasKey(e => e.VelomId).HasName("pk_velomodif");

            entity.Property(e => e.VelomId).HasDefaultValueSql("nextval('velomodifier_idvelom_seq'::regclass)");
            entity.Property(e => e.CapaciteBatterie).IsFixedLength();
            entity.Property(e => e.NombreKms).IsFixedLength();
            entity.Property(e => e.TailleMax).IsFixedLength();
            entity.Property(e => e.TailleMin).IsFixedLength();

            entity.HasOne(d => d.VeloModifCaracteristiqueVelo).WithMany(p => p.ListeVeloModifiers)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_batterie_integrer_velomodifier");

            entity.HasOne(d => d.VeloModifierCategorie).WithMany(p => p.ListeVeloModifiers)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_velomodifier_apparteni_categori");

            entity.HasOne(d => d.VeloModifierMarque).WithMany(p => p.ListeVeloModifiers)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_velomodifier_fabriquer_marque");

            entity.HasOne(d => d.VeloModifierMoteur).WithMany(p => p.ListeVeloModifiers)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_moteur_integrer_velomodifier");
        });

        modelBuilder.Entity<Vvelo>(entity =>
        {
            entity.ToView("vvelo", "upways");

            entity.Property(e => e.Nombrekms).IsFixedLength();
            entity.Property(e => e.Taillemax).IsFixedLength();
            entity.Property(e => e.Taillemin).IsFixedLength();
        });

        modelBuilder.Entity<Vventenombreacessoire>(entity =>
        {
            entity.ToView("vventenombreacessoire", "upways");
        });

        modelBuilder.Entity<Vventenombrevelo>(entity =>
        {
            entity.ToView("vventenombrevelo", "upways");

            entity.Property(e => e.Taillemax).IsFixedLength();
            entity.Property(e => e.Taillemin).IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
