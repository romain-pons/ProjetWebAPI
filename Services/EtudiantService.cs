using ProjetWebAPI.Models;
using Microsoft.EntityFrameworkCore;


public class EtudiantService
{
    private readonly DbContext _context;

    public EtudiantService(DbContext context)
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
