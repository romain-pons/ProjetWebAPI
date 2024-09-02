using ProjetWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using ProjetWebAPI.Data;


public class EtudiantService
{
    private readonly AppDbContext _context;

    public EtudiantService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Etudiants>> GetAll()
    {
        return await _context.Etudiants.ToListAsync();
    }

    public async Task<Etudiants> GetById(int id)
    {
        return await _context.Etudiants.FindAsync(id);
    }

    public async Task Add(Etudiants etudiant)
    {
        _context.Etudiants.Add(etudiant);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Etudiants etudiant)
    {
        _context.Entry(etudiant).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var etudiant = await _context.Etudiants.FindAsync(id);
        if (etudiant != null)
        {
            _context.Etudiants.Remove(etudiant);
            await _context.SaveChangesAsync();
        }
    }
}
