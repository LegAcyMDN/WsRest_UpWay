﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Models.DataManager
{
    public class MarqueManager : IDataRepository<Marque>
    {
        readonly S215UpWayContext? upwaysDbContext;

        public MarqueManager() { }

        public MarqueManager(S215UpWayContext context)
        {
            upwaysDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<Marque>>> GetAllAsync(int page)
        {
            return await upwaysDbContext.Marques.ToListAsync();
        }

        public async Task<ActionResult<Marque>> GetByIdAsync(int id)
        {
            return await upwaysDbContext.Marques.FirstOrDefaultAsync(u => u.MarqueId == id);
        }

        public async Task<ActionResult<Marque>> GetByStringAsync(string nom)
        {
            return await upwaysDbContext.Marques.FirstOrDefaultAsync(u => u.NomMarque.ToUpper() == nom.ToUpper());
        }

        public async Task AddAsync(Marque mar)
        {
            await upwaysDbContext.Marques.AddAsync(mar);
            await upwaysDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Marque marToUpdate, Marque mar)
        {
            upwaysDbContext.Entry(marToUpdate).State = EntityState.Modified;
            marToUpdate.MarqueId = mar.MarqueId;
            marToUpdate.NomMarque = mar.NomMarque;
            await upwaysDbContext.SaveChangesAsync();

        }

        public async Task DeleteAsync(Marque mar)
        {
            upwaysDbContext.Marques.Remove(mar);
            await upwaysDbContext.SaveChangesAsync();
        }
    }
}
