using Domain;

namespace Infrastructure;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly ApplicationDbContext _context;

    public UsuarioRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task InsertAsync(Usuario usuario, CancellationToken cancellationToken)
    {
        await _context.AddAsync(usuario, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}