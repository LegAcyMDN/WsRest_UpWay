using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WsRest_UpWay.Migrations
{
    /// <inheritdoc />
    public partial class CreationBDVelo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "upways");

            migrationBuilder.CreateTable(
                name: "AccessoireVelo",
                columns: table => new
                {
                    AccessoireId = table.Column<int>(type: "integer", nullable: false),
                    VeloId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessoireVelo", x => new { x.AccessoireId, x.VeloId });
                });

            migrationBuilder.CreateTable(
                name: "CaracteristiqueCaracteristique",
                columns: table => new
                {
                    CaracteristiqueId = table.Column<int>(type: "integer", nullable: false),
                    ListeCaracteristiquesCaracteristiqueId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaracteristiqueCaracteristique", x => new { x.CaracteristiqueId, x.ListeCaracteristiquesCaracteristiqueId });
                });

            migrationBuilder.CreateTable(
                name: "CaracteristiqueCategorie",
                columns: table => new
                {
                    CaracteristiqueId = table.Column<int>(type: "integer", nullable: false),
                    CategorieId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaracteristiqueCategorie", x => new { x.CaracteristiqueId, x.CategorieId });
                });

            migrationBuilder.CreateTable(
                name: "CaracteristiqueVelo",
                columns: table => new
                {
                    CaracteristiqueId = table.Column<int>(type: "integer", nullable: false),
                    VeloId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaracteristiqueVelo", x => new { x.CaracteristiqueId, x.VeloId });
                });

            migrationBuilder.CreateTable(
                name: "CategorieArticleCategorieArticle",
                columns: table => new
                {
                    CategorieArticleId = table.Column<int>(type: "integer", nullable: false),
                    ListeCategorieArticlesCategorieArticleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategorieArticleCategorieArticle", x => new { x.CategorieArticleId, x.ListeCategorieArticlesCategorieArticleId });
                });

            migrationBuilder.CreateTable(
                name: "MagasinVelo",
                columns: table => new
                {
                    MagasinId = table.Column<int>(type: "integer", nullable: false),
                    VeloId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MagasinVelo", x => new { x.MagasinId, x.VeloId });
                });

            migrationBuilder.CreateTable(
                name: "t_e_assurance_ass",
                schema: "upways",
                columns: table => new
                {
                    ass_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ass_titre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ass_description = table.Column<string>(type: "text", maxLength: 4096, nullable: true),
                    ass_prix = table.Column<decimal>(type: "numeric(4,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_assurance", x => x.ass_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_caracteristique_car",
                schema: "upways",
                columns: table => new
                {
                    car_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    car_libelle = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    car_image = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_caracteristique", x => x.car_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_caracteristiquevelo_cav",
                schema: "upways",
                columns: table => new
                {
                    cav_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cav_poids = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    cav_tubeselle = table.Column<int>(type: "integer", nullable: false),
                    cav_typesuspension = table.Column<string>(type: "character(20)", fixedLength: true, maxLength: 20, nullable: true),
                    cav_couleur = table.Column<string>(type: "character(10)", fixedLength: true, maxLength: 10, nullable: true),
                    cav_typecargo = table.Column<string>(type: "character(20)", fixedLength: true, maxLength: 20, nullable: true),
                    cav_etatbatterie = table.Column<string>(type: "character(10)", fixedLength: true, maxLength: 10, nullable: true),
                    cav_nombrecycle = table.Column<int>(type: "integer", nullable: true),
                    cav_materiau = table.Column<string>(type: "character(20)", fixedLength: true, maxLength: 20, nullable: true),
                    cav_fourche = table.Column<string>(type: "character(50)", fixedLength: true, maxLength: 50, nullable: true),
                    cav_debattement = table.Column<int>(type: "integer", nullable: true),
                    cav_amortisseur = table.Column<string>(type: "character(50)", fixedLength: true, maxLength: 50, nullable: true),
                    cav_debattementamortisseur = table.Column<int>(type: "integer", nullable: true),
                    cav_modeltransmission = table.Column<string>(type: "character(50)", fixedLength: true, maxLength: 50, nullable: true),
                    cav_nombrevitesse = table.Column<int>(type: "integer", nullable: true),
                    cav_freins = table.Column<string>(type: "character(30)", fixedLength: true, maxLength: 30, nullable: true),
                    cav_taillesroues = table.Column<int>(type: "integer", nullable: true),
                    cav_pneus = table.Column<string>(type: "character(100)", fixedLength: true, maxLength: 100, nullable: true),
                    cav_selletelescopique = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_caracteristiquevelo", x => x.cav_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_categorie_article_caa",
                schema: "upways",
                columns: table => new
                {
                    caa_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    caa_titre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    caa_contenue = table.Column<string>(type: "text", maxLength: 4096, nullable: true),
                    caa_image = table.Column<string>(type: "text", maxLength: 4096, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categorie_article", x => x.caa_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_categorie_cat",
                schema: "upways",
                columns: table => new
                {
                    cat_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cat_libelle = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categorie", x => x.cat_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_codereduction_cor",
                schema: "upways",
                columns: table => new
                {
                    cor_id = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    cor_actifreduction = table.Column<bool>(type: "boolean", nullable: true),
                    cor_reduction = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_codereduction", x => x.cor_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_compteclient_coc",
                schema: "upways",
                columns: table => new
                {
                    coc_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    coc_login = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    coc_mpd = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    coc_email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    coc_prenom = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    coc_nom = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    coc_datecreation = table.Column<DateOnly>(type: "date", nullable: true),
                    coc_remember_token = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    coc_two_factor_secret = table.Column<string>(type: "text", maxLength: 4096, nullable: true),
                    coc_two_factor_recovery_codes = table.Column<string>(type: "text", maxLength: 4096, nullable: true),
                    coc_two_factor_confirmed_at = table.Column<DateTime>(type: "date", nullable: true),
                    coc_usertype = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    coc_email_verified_at = table.Column<DateOnly>(type: "date", nullable: true),
                    coc_is_from_google = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_compteclient", x => x.coc_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_dpo_dpo",
                schema: "upways",
                columns: table => new
                {
                    dpo_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    clt_id = table.Column<int>(type: "integer", nullable: true),
                    dpo_typope = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    dpo_datreqdpo = table.Column<DateTime>(type: "char", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dpo", x => x.dpo_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_etatcommande_etc",
                schema: "upways",
                columns: table => new
                {
                    etc_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    etc_libelle = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_etatcommande", x => x.etc_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_magasin_mag",
                schema: "upways",
                columns: table => new
                {
                    mag_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mag_nom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    mag_horaire = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    mag_rue = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    mag_cp = table.Column<string>(type: "char(5)", fixedLength: true, maxLength: 5, nullable: true),
                    mag_ville = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_magasin", x => x.mag_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_marque_mar",
                schema: "upways",
                columns: table => new
                {
                    mar_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mar_nom = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_marque", x => x.mar_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_rapportinspection_ras",
                schema: "upways",
                columns: table => new
                {
                    ras_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ras_type = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ras_soustype = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ras_pointdinspection = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rapportinspection", x => x.ras_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_reparationvelo_rev",
                schema: "upways",
                columns: table => new
                {
                    rev_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rev_check = table.Column<bool>(type: "boolean", nullable: true),
                    rev_checkvalidation = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reparationvelo", x => x.rev_id);
                });

            migrationBuilder.CreateTable(
                name: "t_j_sedecompose_sed",
                schema: "upways",
                columns: table => new
                {
                    car_id = table.Column<int>(type: "integer", nullable: false),
                    soc_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sedecompose", x => new { x.car_id, x.soc_id });
                    table.ForeignKey(
                        name: "fk_sedecomp_caract_fi_caracter",
                        column: x => x.car_id,
                        principalSchema: "upways",
                        principalTable: "t_e_caracteristique_car",
                        principalColumn: "car_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_sedecomp_caracteri_caracter",
                        column: x => x.soc_id,
                        principalSchema: "upways",
                        principalTable: "t_e_caracteristique_car",
                        principalColumn: "car_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "appartient",
                schema: "upways",
                columns: table => new
                {
                    caa_id = table.Column<int>(type: "integer", nullable: false),
                    sca_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_appartient", x => new { x.caa_id, x.sca_id });
                    table.ForeignKey(
                        name: "fk_appartient_categorie",
                        column: x => x.sca_id,
                        principalSchema: "upways",
                        principalTable: "t_e_categorie_article_caa",
                        principalColumn: "caa_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_appartient_categorie_fi",
                        column: x => x.caa_id,
                        principalSchema: "upways",
                        principalTable: "t_e_categorie_article_caa",
                        principalColumn: "caa_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_article_art",
                schema: "upways",
                columns: table => new
                {
                    art_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    caa_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_article", x => x.art_id);
                    table.ForeignKey(
                        name: "fk_article",
                        column: x => x.caa_id,
                        principalSchema: "upways",
                        principalTable: "t_e_categorie_article_caa",
                        principalColumn: "caa_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_regrouper_reg",
                schema: "upways",
                columns: table => new
                {
                    car_id = table.Column<int>(type: "integer", nullable: false),
                    cat_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_regrouper", x => new { x.car_id, x.cat_id });
                    table.ForeignKey(
                        name: "fk_regroupe_regrouper_caracter",
                        column: x => x.car_id,
                        principalSchema: "upways",
                        principalTable: "t_e_caracteristique_car",
                        principalColumn: "car_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_regroupe_regrouper_categori",
                        column: x => x.cat_id,
                        principalSchema: "upways",
                        principalTable: "t_e_categorie_cat",
                        principalColumn: "cat_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_accessoire_acs",
                schema: "upways",
                columns: table => new
                {
                    acs_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mar_id = table.Column<int>(type: "integer", nullable: false),
                    cat_id = table.Column<int>(type: "integer", nullable: false),
                    acs_nom = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    acs_prix = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: true),
                    acs_description = table.Column<string>(type: "text", maxLength: 4096, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_accessoire", x => x.acs_id);
                    table.ForeignKey(
                        name: "fk_accessoi_classifie_categori",
                        column: x => x.cat_id,
                        principalSchema: "upways",
                        principalTable: "t_e_categorie_cat",
                        principalColumn: "cat_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_accessoi_distribue_marque",
                        column: x => x.mar_id,
                        principalSchema: "upways",
                        principalTable: "t_e_marque_mar",
                        principalColumn: "mar_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_moteur_mot",
                schema: "upways",
                columns: table => new
                {
                    mot_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mar_id = table.Column<int>(type: "integer", nullable: false),
                    mot_modele = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    mot_couple = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    mot_vitessemax = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_moteur", x => x.mot_id);
                    table.ForeignKey(
                        name: "fk_moteur_produire_marque",
                        column: x => x.mar_id,
                        principalSchema: "upways",
                        principalTable: "t_e_marque_mar",
                        principalColumn: "mar_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_contenu_article_coa",
                schema: "upways",
                columns: table => new
                {
                    coa_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    art_id = table.Column<int>(type: "integer", nullable: false),
                    coa_prioritecontenu = table.Column<int>(type: "integer", nullable: true),
                    coa_typecontenu = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    coa_contenu = table.Column<string>(type: "text", maxLength: 4096, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contenu_article", x => x.coa_id);
                    table.ForeignKey(
                        name: "fk_contenu_article_article",
                        column: x => x.art_id,
                        principalSchema: "upways",
                        principalTable: "t_e_article_art",
                        principalColumn: "art_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_photoaccessoire_pha",
                schema: "upways",
                columns: table => new
                {
                    pha_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    acs_id = table.Column<int>(type: "integer", nullable: false),
                    pha_url = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_photoaccessoire", x => x.pha_id);
                    table.ForeignKey(
                        name: "fk_photoacc_comprendr_accessoi",
                        column: x => x.acs_id,
                        principalSchema: "upways",
                        principalTable: "t_e_accessoire_acs",
                        principalColumn: "acs_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_velo_vel",
                schema: "upways",
                columns: table => new
                {
                    vel_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mar_id = table.Column<int>(type: "integer", nullable: true),
                    cat_id = table.Column<int>(type: "integer", nullable: false),
                    mot_id = table.Column<int>(type: "integer", nullable: true),
                    car_id = table.Column<int>(type: "integer", nullable: true),
                    vel_nom = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    vel_annee = table.Column<int>(type: "integer", precision: 4, scale: 0, nullable: true),
                    vel_taillemin = table.Column<string>(type: "character(15)", fixedLength: true, maxLength: 15, nullable: true),
                    vel_taillemax = table.Column<string>(type: "character(15)", fixedLength: true, maxLength: 15, nullable: true),
                    vel_kms = table.Column<string>(type: "character(15)", fixedLength: true, maxLength: 15, nullable: true),
                    vel_prixremise = table.Column<decimal>(type: "numeric(5,0)", precision: 5, scale: 0, nullable: true),
                    vel_prixneuf = table.Column<decimal>(type: "numeric(5,0)", precision: 5, scale: 0, nullable: true),
                    vel_pourcentagereduction = table.Column<decimal>(type: "numeric(3,0)", precision: 3, scale: 0, nullable: true),
                    vel_descriptif = table.Column<string>(type: "text", maxLength: 5000, nullable: true),
                    vel_quantite = table.Column<int>(type: "integer", precision: 3, scale: 0, nullable: true),
                    vel_positionmoteur = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    vel_capacitebatterie = table.Column<string>(type: "character(10)", fixedLength: true, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_velo", x => x.vel_id);
                    table.ForeignKey(
                        name: "fk_batterie_integrer_velo",
                        column: x => x.car_id,
                        principalSchema: "upways",
                        principalTable: "t_e_caracteristiquevelo_cav",
                        principalColumn: "cav_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_moteur_integrer_velo",
                        column: x => x.mot_id,
                        principalSchema: "upways",
                        principalTable: "t_e_moteur_mot",
                        principalColumn: "mot_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_velo_apparteni_categori",
                        column: x => x.cat_id,
                        principalSchema: "upways",
                        principalTable: "t_e_categorie_cat",
                        principalColumn: "cat_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_velo_fabriquer_marque",
                        column: x => x.mar_id,
                        principalSchema: "upways",
                        principalTable: "t_e_marque_mar",
                        principalColumn: "mar_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_velomodofier_vlm",
                schema: "upways",
                columns: table => new
                {
                    vlm_id = table.Column<int>(type: "integer", nullable: false),
                    vlm_idm = table.Column<int>(type: "integer", nullable: false),
                    mar_id = table.Column<int>(type: "integer", nullable: true),
                    cat_id = table.Column<int>(type: "integer", nullable: false),
                    mot_id = table.Column<int>(type: "integer", nullable: true),
                    car_id = table.Column<int>(type: "integer", nullable: true),
                    vlm_nom = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    vlm_annee = table.Column<decimal>(type: "numeric(4,0)", precision: 4, scale: 0, nullable: true),
                    vlm_taillemin = table.Column<string>(type: "character(15)", fixedLength: true, maxLength: 15, nullable: true),
                    vlm_taillemax = table.Column<string>(type: "character(15)", fixedLength: true, maxLength: 15, nullable: true),
                    vlm_kms = table.Column<string>(type: "character(15)", fixedLength: true, maxLength: 15, nullable: true),
                    vlm_prixremise = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    vlm_prixneuf = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    vlm_pourcentagereduction = table.Column<decimal>(type: "numeric(3,0)", precision: 3, scale: 0, nullable: true),
                    vlm_descriptif = table.Column<string>(type: "text", maxLength: 4096, nullable: true),
                    vlm_quantite = table.Column<decimal>(type: "numeric(3,0)", precision: 3, scale: 0, nullable: true),
                    vlm_positionmoteur = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    vlm_capacitebatterie = table.Column<string>(type: "character(10)", fixedLength: true, maxLength: 10, nullable: true),
                    vlm_ancienprix = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    modifier = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_velomodif", x => x.vlm_id);
                    table.ForeignKey(
                        name: "fk_batterie_integrer_velomodifier",
                        column: x => x.car_id,
                        principalSchema: "upways",
                        principalTable: "t_e_caracteristiquevelo_cav",
                        principalColumn: "cav_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_moteur_integrer_velomodifier",
                        column: x => x.mot_id,
                        principalSchema: "upways",
                        principalTable: "t_e_moteur_mot",
                        principalColumn: "mot_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_velomodifier_apparteni_categori",
                        column: x => x.cat_id,
                        principalSchema: "upways",
                        principalTable: "t_e_categorie_cat",
                        principalColumn: "cat_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_velomodifier_fabriquer_marque",
                        column: x => x.vlm_id,
                        principalSchema: "upways",
                        principalTable: "t_e_marque_mar",
                        principalColumn: "mar_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_alertevelo_alv",
                schema: "upways",
                columns: table => new
                {
                    alv_id = table.Column<int>(type: "integer", nullable: false),
                    cli_id = table.Column<int>(type: "integer", nullable: false),
                    vel_id = table.Column<int>(type: "integer", nullable: false),
                    alv_modification = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_alertevelo", x => new { x.alv_id, x.cli_id, x.vel_id });
                    table.ForeignKey(
                        name: "fk_alertevelo_client",
                        column: x => x.cli_id,
                        principalSchema: "upways",
                        principalTable: "t_e_compteclient_coc",
                        principalColumn: "coc_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_alertevelo_velo",
                        column: x => x.vel_id,
                        principalSchema: "upways",
                        principalTable: "t_e_velo_vel",
                        principalColumn: "vel_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_mentionvelo_mev",
                schema: "upways",
                columns: table => new
                {
                    mev_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    vel_id = table.Column<int>(type: "integer", nullable: false),
                    mev_libelle = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    mev_valeur = table.Column<string>(type: "text", maxLength: 4096, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mentionvelo", x => x.mev_id);
                    table.ForeignKey(
                        name: "fk_mentionv_mentionne_velo",
                        column: x => x.vel_id,
                        principalSchema: "upways",
                        principalTable: "t_e_velo_vel",
                        principalColumn: "vel_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_photovelo_phv",
                schema: "upways",
                columns: table => new
                {
                    phv_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    vel_id = table.Column<int>(type: "integer", nullable: false),
                    phv_url = table.Column<string>(type: "text", maxLength: 4096, nullable: true),
                    phv_bytea = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_photovelo", x => x.phv_id);
                    table.ForeignKey(
                        name: "fk_photovel_represent_velo",
                        column: x => x.vel_id,
                        principalSchema: "upways",
                        principalTable: "t_e_velo_vel",
                        principalColumn: "vel_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_testvelo_tev",
                schema: "upways",
                columns: table => new
                {
                    tev_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    vel_id = table.Column<int>(type: "integer", nullable: false),
                    mag_id = table.Column<int>(type: "integer", nullable: false),
                    tev_date = table.Column<DateTime>(type: "date", nullable: true),
                    tev_heure = table.Column<TimeOnly>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_testvelo", x => x.tev_id);
                    table.ForeignKey(
                        name: "fk_testvelo_essayer_velo",
                        column: x => x.vel_id,
                        principalSchema: "upways",
                        principalTable: "t_e_velo_vel",
                        principalColumn: "vel_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_testvelo_sederoule_magasin",
                        column: x => x.mag_id,
                        principalSchema: "upways",
                        principalTable: "t_e_magasin_mag",
                        principalColumn: "mag_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_caracteriser_car",
                schema: "upways",
                columns: table => new
                {
                    car_id = table.Column<int>(type: "integer", nullable: false),
                    vel_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_caracteriser", x => new { x.car_id, x.vel_id });
                    table.ForeignKey(
                        name: "fk_caracter_caracteri_caracter",
                        column: x => x.car_id,
                        principalSchema: "upways",
                        principalTable: "t_e_caracteristique_car",
                        principalColumn: "car_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_caracter_caracteri_velo",
                        column: x => x.vel_id,
                        principalSchema: "upways",
                        principalTable: "t_e_velo_vel",
                        principalColumn: "vel_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_equiper_equ",
                schema: "upways",
                columns: table => new
                {
                    acs_id = table.Column<int>(type: "integer", nullable: false),
                    vel_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_equiper", x => new { x.acs_id, x.vel_id });
                    table.ForeignKey(
                        name: "fk_equiper_equiper2_velo",
                        column: x => x.vel_id,
                        principalSchema: "upways",
                        principalTable: "t_e_velo_vel",
                        principalColumn: "vel_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_equiper_equiper_accessoi",
                        column: x => x.acs_id,
                        principalSchema: "upways",
                        principalTable: "t_e_accessoire_acs",
                        principalColumn: "acs_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_estdisponible_esd",
                schema: "upways",
                columns: table => new
                {
                    mag_id = table.Column<int>(type: "integer", nullable: false),
                    vel_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_estdisponible", x => new { x.mag_id, x.vel_id });
                    table.ForeignKey(
                        name: "fk_estdispo_estdispon_magasin",
                        column: x => x.mag_id,
                        principalSchema: "upways",
                        principalTable: "t_e_magasin_mag",
                        principalColumn: "mag_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_estdispo_estdispon_velo",
                        column: x => x.vel_id,
                        principalSchema: "upways",
                        principalTable: "t_e_velo_vel",
                        principalColumn: "vel_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_estrealise_esr",
                schema: "upways",
                columns: table => new
                {
                    vel_id = table.Column<int>(type: "integer", nullable: false),
                    ras_id = table.Column<int>(type: "integer", nullable: false),
                    esr_id = table.Column<int>(type: "integer", nullable: false),
                    esr_dateinspection = table.Column<string>(type: "text", fixedLength: true, nullable: true),
                    esr_commentaireinspection = table.Column<string>(type: "text", maxLength: 4096, nullable: true),
                    esr_historiqueinspection = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_estrealise", x => new { x.vel_id, x.ras_id, x.esr_id });
                    table.ForeignKey(
                        name: "fk_estreali_estrealis_rapporti",
                        column: x => x.ras_id,
                        principalSchema: "upways",
                        principalTable: "t_e_rapportinspection_ras",
                        principalColumn: "ras_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_estreali_estrealis_reparati",
                        column: x => x.esr_id,
                        principalSchema: "upways",
                        principalTable: "t_e_reparationvelo_rev",
                        principalColumn: "rev_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_estreali_estrealis_velo",
                        column: x => x.vel_id,
                        principalSchema: "upways",
                        principalTable: "t_e_velo_vel",
                        principalColumn: "vel_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_utilite_uti",
                schema: "upways",
                columns: table => new
                {
                    uti_id = table.Column<int>(type: "integer", nullable: false),
                    vel_id = table.Column<int>(type: "integer", nullable: false),
                    uti_valeur = table.Column<decimal>(type: "numeric(5,0)", precision: 5, scale: 0, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_utilite", x => new { x.uti_id, x.vel_id });
                    table.ForeignKey(
                        name: "fk_utilite_note_velo",
                        column: x => x.vel_id,
                        principalSchema: "upways",
                        principalTable: "t_e_velo_vel",
                        principalColumn: "vel_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_adresseexpedition_ade",
                schema: "upways",
                columns: table => new
                {
                    ade_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cli_id = table.Column<int>(type: "integer", nullable: false),
                    adf_id = table.Column<int>(type: "integer", nullable: true),
                    ade_pays = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ade_batopt = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ade_rue = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ade_cp = table.Column<string>(type: "char(5)", maxLength: 5, nullable: true),
                    ade_region = table.Column<string>(type: "character(20)", fixedLength: true, maxLength: 20, nullable: true),
                    ade_ville = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ade_telephone = table.Column<string>(type: "char(14)", maxLength: 14, nullable: true),
                    ade_donneessauv = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_adresseexpedition", x => x.ade_id);
                    table.ForeignKey(
                        name: "fk_adressee_modifier_comptecl",
                        column: x => x.cli_id,
                        principalSchema: "upways",
                        principalTable: "t_e_compteclient_coc",
                        principalColumn: "coc_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_adressefacturation_adf",
                schema: "upways",
                columns: table => new
                {
                    adf_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cli_id = table.Column<int>(type: "integer", nullable: false),
                    ade_id = table.Column<int>(type: "integer", nullable: true),
                    adf_pays = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    adf_batopt = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    adf_rue = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    adf_cp = table.Column<string>(type: "char(5)", maxLength: 5, nullable: true),
                    adf_region = table.Column<string>(type: "character(20)", fixedLength: true, maxLength: 20, nullable: true),
                    adf_ville = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    adf_telephone = table.Column<string>(type: "char(14)", maxLength: 14, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_adressefacturation", x => x.adf_id);
                    table.ForeignKey(
                        name: "fk_adressef_assimiler_adressee",
                        column: x => x.ade_id,
                        principalSchema: "upways",
                        principalTable: "t_e_adresseexpedition_ade",
                        principalColumn: "ade_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_adressef_rectifier_comptecl",
                        column: x => x.cli_id,
                        principalSchema: "upways",
                        principalTable: "t_e_compteclient_coc",
                        principalColumn: "coc_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_detailcommande_detcom",
                schema: "upways",
                columns: table => new
                {
                    detcom_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    retmag_id = table.Column<int>(type: "integer", nullable: true),
                    adf_id = table.Column<int>(type: "integer", nullable: true),
                    etacom_id = table.Column<int>(type: "integer", nullable: true),
                    cli_id = table.Column<int>(type: "integer", nullable: false),
                    pan_id = table.Column<int>(type: "integer", nullable: true),
                    detcom_moypai = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    detcom_modexp = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    detcom_datach = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_detailcommande", x => x.detcom_id);
                    table.ForeignKey(
                        name: "fk_detailco_afficher2_adressef",
                        column: x => x.adf_id,
                        principalSchema: "upways",
                        principalTable: "t_e_adressefacturation_adf",
                        principalColumn: "adf_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_detailco_indiquer_etatcomm",
                        column: x => x.etacom_id,
                        principalSchema: "upways",
                        principalTable: "t_e_etatcommande_etc",
                        principalColumn: "etc_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_detailco_visualise_comptecl",
                        column: x => x.cli_id,
                        principalSchema: "upways",
                        principalTable: "t_e_compteclient_coc",
                        principalColumn: "coc_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_panier_pan",
                schema: "upways",
                columns: table => new
                {
                    pan_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cli_id = table.Column<int>(type: "integer", nullable: true),
                    com_id = table.Column<int>(type: "integer", nullable: true),
                    pan_cookie = table.Column<string>(type: "text", maxLength: 255, nullable: true),
                    pan_prix = table.Column<decimal>(type: "numeric(11,2)", precision: 11, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_panier", x => x.pan_id);
                    table.ForeignKey(
                        name: "fk_panier_appartient_client",
                        column: x => x.cli_id,
                        principalSchema: "upways",
                        principalTable: "t_e_compteclient_coc",
                        principalColumn: "coc_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_panier_estlie2_detailco",
                        column: x => x.com_id,
                        principalSchema: "upways",
                        principalTable: "t_e_detailcommande_detcom",
                        principalColumn: "detcom_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_lignepanier_lignpan",
                schema: "upways",
                columns: table => new
                {
                    lignpan_id = table.Column<int>(type: "integer", nullable: false),
                    vel_id = table.Column<int>(type: "integer", nullable: false),
                    ass_id = table.Column<int>(type: "integer", nullable: false),
                    lignpan_quantpan = table.Column<decimal>(type: "numeric(2,0)", precision: 2, scale: 0, nullable: false),
                    lignpan_priquant = table.Column<decimal>(type: "numeric(11,2)", precision: 11, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_lignepanier", x => new { x.lignpan_id, x.vel_id });
                    table.ForeignKey(
                        name: "fk_lignepan_contenir_panier",
                        column: x => x.lignpan_id,
                        principalSchema: "upways",
                        principalTable: "t_e_panier_pan",
                        principalColumn: "pan_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_lignepan_estpresen_assuranc",
                        column: x => x.ass_id,
                        principalSchema: "upways",
                        principalTable: "t_e_assurance_ass",
                        principalColumn: "ass_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_lignepan_estvisibl_velo",
                        column: x => x.vel_id,
                        principalSchema: "upways",
                        principalTable: "t_e_velo_vel",
                        principalColumn: "vel_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_ajouteraccessoire_aja",
                schema: "upways",
                columns: table => new
                {
                    acs_id = table.Column<int>(type: "integer", nullable: false),
                    pan_id = table.Column<int>(type: "integer", nullable: false),
                    aja_quantite = table.Column<int>(type: "integer", precision: 2, scale: 0, nullable: true, defaultValueSql: "1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ajouteraccessoire", x => new { x.acs_id, x.pan_id });
                    table.ForeignKey(
                        name: "fk_ajoutera_ajouterac_accessoi",
                        column: x => x.acs_id,
                        principalSchema: "upways",
                        principalTable: "t_e_accessoire_acs",
                        principalColumn: "acs_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_ajoutera_ajouterac_panier",
                        column: x => x.pan_id,
                        principalSchema: "upways",
                        principalTable: "t_e_panier_pan",
                        principalColumn: "pan_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_marquage_velo_mal",
                schema: "upways",
                columns: table => new
                {
                    mal_code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    pan_id = table.Column<int>(type: "integer", nullable: false),
                    vel_id = table.Column<int>(type: "integer", nullable: false),
                    mal_prix = table.Column<decimal>(type: "numeric(2,2)", precision: 2, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_marquagevelo", x => x.mal_code);
                    table.ForeignKey(
                        name: "fk_marquage_estobliga_lignepan",
                        columns: x => new { x.vel_id, x.pan_id },
                        principalSchema: "upways",
                        principalTable: "t_e_lignepanier_lignpan",
                        principalColumns: new[] { "lignpan_id", "vel_id" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_informations_inf",
                schema: "upways",
                columns: table => new
                {
                    inf_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    red_id = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    retmag_id = table.Column<int>(type: "integer", nullable: true),
                    adexp_id = table.Column<int>(type: "integer", nullable: false),
                    pan_id = table.Column<int>(type: "integer", nullable: false),
                    inf_continf = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    inf_offmail = table.Column<bool>(type: "boolean", nullable: true),
                    inf_moodliv = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    inf_payinf = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    inf_rueinf = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_informations", x => x.inf_id);
                    table.ForeignKey(
                        name: "fk_informat_opter_adressee",
                        column: x => x.adexp_id,
                        principalSchema: "upways",
                        principalTable: "t_e_adresseexpedition_ade",
                        principalColumn: "ade_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_informat_regler_panier",
                        column: x => x.pan_id,
                        principalSchema: "upways",
                        principalTable: "t_e_panier_pan",
                        principalColumn: "pan_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_informat_utiliser_coderedu",
                        column: x => x.red_id,
                        principalSchema: "upways",
                        principalTable: "t_e_codereduction_cor",
                        principalColumn: "cor_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_retraitmagasin_rem",
                schema: "upways",
                columns: table => new
                {
                    rem_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    inf_id = table.Column<int>(type: "integer", nullable: true),
                    detcom_id = table.Column<int>(type: "integer", nullable: true),
                    mag_id = table.Column<int>(type: "integer", nullable: false),
                    rem_date = table.Column<DateOnly>(type: "date", nullable: true),
                    rem_heure = table.Column<TimeOnly>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_retraitmagasin", x => x.rem_id);
                    table.ForeignKey(
                        name: "fk_retraitm_choisir_informat",
                        column: x => x.inf_id,
                        principalSchema: "upways",
                        principalTable: "t_e_informations_inf",
                        principalColumn: "inf_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_retraitm_estfait_magasin",
                        column: x => x.mag_id,
                        principalSchema: "upways",
                        principalTable: "t_e_magasin_mag",
                        principalColumn: "mag_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_retraitm_monter2_detailco",
                        column: x => x.detcom_id,
                        principalSchema: "upways",
                        principalTable: "t_e_detailcommande_detcom",
                        principalColumn: "detcom_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_appartient_sca_id",
                schema: "upways",
                table: "appartient",
                column: "sca_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_accessoire_acs_categorieid",
                schema: "upways",
                table: "t_e_accessoire_acs",
                column: "cat_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_accessoire_acs_marqueid",
                schema: "upways",
                table: "t_e_accessoire_acs",
                column: "mar_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_adresseexpedition_ade_adressefactid",
                schema: "upways",
                table: "t_e_adresseexpedition_ade",
                column: "adf_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_adresseexpedition_ade_clientid",
                schema: "upways",
                table: "t_e_adresseexpedition_ade",
                column: "cli_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_adressefacturation_adf_adresseexpeid",
                schema: "upways",
                table: "t_e_adressefacturation_adf",
                column: "ade_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_adressefacturation_adf_clientid",
                schema: "upways",
                table: "t_e_adressefacturation_adf",
                column: "cli_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_alertevelo_alv_clientid",
                schema: "upways",
                table: "t_e_alertevelo_alv",
                column: "cli_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_alertevelo_alv_veloid",
                schema: "upways",
                table: "t_e_alertevelo_alv",
                column: "vel_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_article_art_caa_id",
                schema: "upways",
                table: "t_e_article_art",
                column: "caa_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_categorie_cat_libellecategorie",
                schema: "upways",
                table: "t_e_categorie_cat",
                column: "cat_libelle");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_compteclient_coc_email_unq",
                schema: "upways",
                table: "t_e_compteclient_coc",
                column: "coc_email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_t_e_compteclient_coc_pseudo_unq",
                schema: "upways",
                table: "t_e_compteclient_coc",
                column: "coc_login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_contenu_article_coa_art_id",
                schema: "upways",
                table: "t_e_contenu_article_coa",
                column: "art_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_detailcommande_detcom_adressefactid",
                schema: "upways",
                table: "t_e_detailcommande_detcom",
                column: "adf_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_detailcommande_detcom_clientid",
                schema: "upways",
                table: "t_e_detailcommande_detcom",
                column: "cli_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_detailcommande_detcom_etatcommandeid",
                schema: "upways",
                table: "t_e_detailcommande_detcom",
                column: "etacom_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_detailcommande_detcom_panierid",
                schema: "upways",
                table: "t_e_detailcommande_detcom",
                column: "pan_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_detailcommande_detcom_retraitmagasinid",
                schema: "upways",
                table: "t_e_detailcommande_detcom",
                column: "retmag_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_dpo_dpo_clientid",
                schema: "upways",
                table: "t_e_dpo_dpo",
                column: "clt_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_informations_inf_adresseexpeid",
                schema: "upways",
                table: "t_e_informations_inf",
                column: "adexp_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_informations_inf_panierid",
                schema: "upways",
                table: "t_e_informations_inf",
                column: "pan_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_informations_inf_reductionid",
                schema: "upways",
                table: "t_e_informations_inf",
                column: "red_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_informations_inf_retraitmagasinid",
                schema: "upways",
                table: "t_e_informations_inf",
                column: "retmag_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_linepanier_lignpan_assuranceid",
                schema: "upways",
                table: "t_e_lignepanier_lignpan",
                column: "ass_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_linepanier_lignpan_panierid",
                schema: "upways",
                table: "t_e_lignepanier_lignpan",
                column: "lignpan_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_linepanier_lignpan_veloid",
                schema: "upways",
                table: "t_e_lignepanier_lignpan",
                column: "vel_id");

            migrationBuilder.CreateIndex(
                name: "codemarquage_unq",
                schema: "upways",
                table: "t_e_marquage_velo_mal",
                column: "mal_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_marquage_velo_mal_vel_id_pan_id",
                schema: "upways",
                table: "t_e_marquage_velo_mal",
                columns: new[] { "vel_id", "pan_id" });

            migrationBuilder.CreateIndex(
                name: "ix_t_e_marquagevelo_idpanier_idvelo",
                schema: "upways",
                table: "t_e_marquage_velo_mal",
                columns: new[] { "pan_id", "vel_id" });

            migrationBuilder.CreateIndex(
                name: "nommarque_unq",
                schema: "upways",
                table: "t_e_marque_mar",
                column: "mar_nom",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_t_e_mentionvelo_mev_veloid",
                schema: "upways",
                table: "t_e_mentionvelo_mev",
                column: "vel_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_moteur_mot_marqueid",
                schema: "upways",
                table: "t_e_moteur_mot",
                column: "mar_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_panier_pan_cli_id",
                schema: "upways",
                table: "t_e_panier_pan",
                column: "cli_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_panier_pan_commandeid",
                schema: "upways",
                table: "t_e_panier_pan",
                column: "com_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_photoaccessoire_pha_accessoireid",
                schema: "upways",
                table: "t_e_photoaccessoire_pha",
                column: "acs_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_photovelo_phv_veloid",
                schema: "upways",
                table: "t_e_photovelo_phv",
                column: "vel_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_retraitmagasin_rem_commandeid",
                schema: "upways",
                table: "t_e_retraitmagasin_rem",
                column: "detcom_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_retraitmagasin_rem_informationid",
                schema: "upways",
                table: "t_e_retraitmagasin_rem",
                column: "inf_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_retraitmagasin_rem_magasinid",
                schema: "upways",
                table: "t_e_retraitmagasin_rem",
                column: "mag_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_testvelo_tev_idmagasin",
                schema: "upways",
                table: "t_e_testvelo_tev",
                column: "mag_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_testvelo_tev_idvelo",
                schema: "upways",
                table: "t_e_testvelo_tev",
                column: "vel_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_velo_vel_caracteristiqueveloid",
                schema: "upways",
                table: "t_e_velo_vel",
                column: "car_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_velo_vel_categorieid",
                schema: "upways",
                table: "t_e_velo_vel",
                column: "cat_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_velo_vel_marqueid",
                schema: "upways",
                table: "t_e_velo_vel",
                column: "mar_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_velo_vel_moteurid",
                schema: "upways",
                table: "t_e_velo_vel",
                column: "mot_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_velomodifier_vlm_caracteristiqueveloid",
                schema: "upways",
                table: "t_e_velomodofier_vlm",
                column: "car_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_velomodifier_vlm_categorieid",
                schema: "upways",
                table: "t_e_velomodofier_vlm",
                column: "cat_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_velomodifier_vlm_marqueid",
                schema: "upways",
                table: "t_e_velomodofier_vlm",
                column: "mar_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_velomodifier_vlm_moteurid",
                schema: "upways",
                table: "t_e_velomodofier_vlm",
                column: "mot_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_ajouteracessoire_aja_accessoireid",
                schema: "upways",
                table: "t_j_ajouteraccessoire_aja",
                column: "acs_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_ajouteracessoire_aja_panierid",
                schema: "upways",
                table: "t_j_ajouteraccessoire_aja",
                column: "pan_id");

            migrationBuilder.CreateIndex(
                name: "id_t_j_caracteriser_car_idvelo",
                schema: "upways",
                table: "t_j_caracteriser_car",
                column: "vel_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_j_caracteriser_car_idcaracteristique",
                schema: "upways",
                table: "t_j_caracteriser_car",
                column: "car_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_j__equiper_equ_acessoireid",
                schema: "upways",
                table: "t_j_equiper_equ",
                column: "acs_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_j_equiper_equ_veloid",
                schema: "upways",
                table: "t_j_equiper_equ",
                column: "vel_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_j_estdisponible_esd_magasinid",
                schema: "upways",
                table: "t_j_estdisponible_esd",
                column: "mag_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_estdisponible_esd_vel_id",
                schema: "upways",
                table: "t_j_estdisponible_esd",
                column: "vel_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_j_estrealise_esr_inspectionid",
                schema: "upways",
                table: "t_j_estrealise_esr",
                column: "ras_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_j_estrealise_esr_reparationid",
                schema: "upways",
                table: "t_j_estrealise_esr",
                column: "esr_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_j_estrealise_esr_veloid",
                schema: "upways",
                table: "t_j_estrealise_esr",
                column: "vel_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_j_regrouper_reg_caracteristiqueid",
                schema: "upways",
                table: "t_j_regrouper_reg",
                column: "car_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_j_regrouper_reg_categorieid",
                schema: "upways",
                table: "t_j_regrouper_reg",
                column: "cat_id");

            migrationBuilder.CreateIndex(
                name: "idx_t_j_sedecompose_sed_caracteristiqueid",
                schema: "upways",
                table: "t_j_sedecompose_sed",
                column: "car_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_j_sedecompose_sed_caracteristiqueid",
                schema: "upways",
                table: "t_j_sedecompose_sed",
                column: "car_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_j_sedecompose_sed_souscaracteristiqueid",
                schema: "upways",
                table: "t_j_sedecompose_sed",
                column: "soc_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_utilite_uti_veloid",
                schema: "upways",
                table: "t_j_utilite_uti",
                column: "vel_id");

            migrationBuilder.AddForeignKey(
                name: "fk_adressee_assimiler_adressef",
                schema: "upways",
                table: "t_e_adresseexpedition_ade",
                column: "adf_id",
                principalSchema: "upways",
                principalTable: "t_e_adressefacturation_adf",
                principalColumn: "adf_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_detailco_estlie_panier",
                schema: "upways",
                table: "t_e_detailcommande_detcom",
                column: "pan_id",
                principalSchema: "upways",
                principalTable: "t_e_panier_pan",
                principalColumn: "pan_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_detailco_monter_retraitm",
                schema: "upways",
                table: "t_e_detailcommande_detcom",
                column: "retmag_id",
                principalSchema: "upways",
                principalTable: "t_e_retraitmagasin_rem",
                principalColumn: "rem_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_informat_choisir2_retraitm",
                schema: "upways",
                table: "t_e_informations_inf",
                column: "retmag_id",
                principalSchema: "upways",
                principalTable: "t_e_retraitmagasin_rem",
                principalColumn: "rem_id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_adressee_assimiler_adressef",
                schema: "upways",
                table: "t_e_adresseexpedition_ade");

            migrationBuilder.DropForeignKey(
                name: "fk_detailco_afficher2_adressef",
                schema: "upways",
                table: "t_e_detailcommande_detcom");

            migrationBuilder.DropForeignKey(
                name: "fk_adressee_modifier_comptecl",
                schema: "upways",
                table: "t_e_adresseexpedition_ade");

            migrationBuilder.DropForeignKey(
                name: "fk_detailco_visualise_comptecl",
                schema: "upways",
                table: "t_e_detailcommande_detcom");

            migrationBuilder.DropForeignKey(
                name: "fk_panier_appartient_client",
                schema: "upways",
                table: "t_e_panier_pan");

            migrationBuilder.DropForeignKey(
                name: "fk_informat_opter_adressee",
                schema: "upways",
                table: "t_e_informations_inf");

            migrationBuilder.DropForeignKey(
                name: "fk_detailco_estlie_panier",
                schema: "upways",
                table: "t_e_detailcommande_detcom");

            migrationBuilder.DropForeignKey(
                name: "fk_informat_regler_panier",
                schema: "upways",
                table: "t_e_informations_inf");

            migrationBuilder.DropForeignKey(
                name: "fk_detailco_indiquer_etatcomm",
                schema: "upways",
                table: "t_e_detailcommande_detcom");

            migrationBuilder.DropForeignKey(
                name: "fk_detailco_monter_retraitm",
                schema: "upways",
                table: "t_e_detailcommande_detcom");

            migrationBuilder.DropForeignKey(
                name: "fk_informat_choisir2_retraitm",
                schema: "upways",
                table: "t_e_informations_inf");

            migrationBuilder.DropTable(
                name: "AccessoireVelo");

            migrationBuilder.DropTable(
                name: "appartient",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "CaracteristiqueCaracteristique");

            migrationBuilder.DropTable(
                name: "CaracteristiqueCategorie");

            migrationBuilder.DropTable(
                name: "CaracteristiqueVelo");

            migrationBuilder.DropTable(
                name: "CategorieArticleCategorieArticle");

            migrationBuilder.DropTable(
                name: "MagasinVelo");

            migrationBuilder.DropTable(
                name: "t_e_alertevelo_alv",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "t_e_contenu_article_coa",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "t_e_dpo_dpo",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "t_e_marquage_velo_mal",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "t_e_mentionvelo_mev",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "t_e_photoaccessoire_pha",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "t_e_photovelo_phv",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "t_e_testvelo_tev",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "t_e_velomodofier_vlm",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "t_j_ajouteraccessoire_aja",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "t_j_caracteriser_car",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "t_j_equiper_equ",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "t_j_estdisponible_esd",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "t_j_estrealise_esr",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "t_j_regrouper_reg",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "t_j_sedecompose_sed",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "t_j_utilite_uti",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "t_e_article_art",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "t_e_lignepanier_lignpan",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "t_e_accessoire_acs",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "t_e_rapportinspection_ras",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "t_e_reparationvelo_rev",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "t_e_caracteristique_car",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "t_e_categorie_article_caa",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "t_e_assurance_ass",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "t_e_velo_vel",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "t_e_caracteristiquevelo_cav",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "t_e_moteur_mot",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "t_e_categorie_cat",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "t_e_marque_mar",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "t_e_adressefacturation_adf",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "t_e_compteclient_coc",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "t_e_adresseexpedition_ade",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "t_e_panier_pan",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "t_e_etatcommande_etc",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "t_e_retraitmagasin_rem",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "t_e_informations_inf",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "t_e_magasin_mag",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "t_e_detailcommande_detcom",
                schema: "upways");

            migrationBuilder.DropTable(
                name: "t_e_codereduction_cor",
                schema: "upways");
        }
    }
}
