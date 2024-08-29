public class EtudiantService
{
    private readonly DbContext _context;

    public EtudiantService(DbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Etudiant>> GetAll()
    {
        return await _context.Etudiants.ToListAsync();
    }

    public async Task<Etudiant> GetById(int id)
    {
        return await _context.Etudiants.FindAsync(id);
    }

    public async Task Add(Etudiant etudiant)
    {
        _context.Etudiants.Add(etudiant);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Etudiant etudiant)
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
