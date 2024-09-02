using ProjetWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using ProjetWebAPI.Data;

public class CourService
{
    private readonly AppDbContext _context;

    public CourService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cours>> GetAll()
    {
        return await _context.Cours.Include(c => c.Prof).ToListAsync();
    }

    public async Task<Cours> GetById(int id)
    {
        return await _context.Cours.Include(c => c.Prof).FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task Add(Cours cours)
    {
        _context.Cours.Add(cours);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Cours cours)
    {
        _context.Entry(cours).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var cours = await _context.Cours.FindAsync(id);
        if (cours != null)
        {
            _context.Cours.Remove(cours);
            await _context.SaveChangesAsync();
        }
    }
}
