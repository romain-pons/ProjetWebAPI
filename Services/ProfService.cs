public class ProfService
{
    private readonly DbContext _context;

    public ProfService(DbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Prof>> GetAll()
    {
        return await _context.Profs.ToListAsync();
    }

    public async Task<Prof> GetById(int id)
    {
        return await _context.Profs.FindAsync(id);
    }

    public async Task Add(Prof prof)
    {
        _context.Profs.Add(prof);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Prof prof)
    {
        _context.Entry(prof).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var prof = await _context.Profs.FindAsync(id);
        if (prof != null)
        {
            _context.Profs.Remove(prof);
            await _context.SaveChangesAsync();
        }
    }
}
