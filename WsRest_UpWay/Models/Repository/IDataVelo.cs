using Microsoft.AspNetCore.Mvc;
using WsRest_UpWay.Models.EntityFramework;

namespace WsRest_UpWay.Models.Repository;

public interface IDataVelo : IDataRepository<Velo>
{
    Task<ActionResult<IEnumerable<Velo>>> GetByFiltresAsync(string? taille, int? categorie, int? cara, int? marque,
        int? annee, string? kilom, string? posmot, string? motmar, string? couplemot, string? capbat, string? posbat,
        string? batamo, string? posbag, decimal? poids, int page = 0);

    Task<ActionResult<IEnumerable<PhotoVelo>>> GetPhotosByIdAsync(int id);

    Task<ActionResult<IEnumerable<MentionVelo>>> GetMentionByIdAsync(int id);

    Task<ActionResult<IEnumerable<Caracteristique>>> GetCaracteristiquesVeloAsync(int id);
}