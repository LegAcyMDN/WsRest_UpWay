using Microsoft.AspNetCore.Mvc;
using WsRest_UpWay.Models.EntityFramework;

namespace WsRest_UpWay.Models.Repository;

public interface IDataVelo : IDataRepository<Velo>
{
    Task<ActionResult<IEnumerable<Velo>>> GetByFiltresAsync(
        int? taille, int? categorie, int? cara, int? marque, int? annee,
        int? kilomMin, int? kilomMax, string? posmot, int? motmar,
        string? couplemot, string? capbat, decimal? poids,
        decimal? prixMin, decimal? prixMax, int page = 0);

    Task<ActionResult<IEnumerable<PhotoVelo>>> GetPhotosByIdAsync(int id);

    Task<ActionResult<IEnumerable<MentionVelo>>> GetMentionByIdAsync(int id);

    Task<ActionResult<IEnumerable<Caracteristique>>> GetCaracteristiquesVeloAsync(int id);
}