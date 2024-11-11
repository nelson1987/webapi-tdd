namespace Domain;

public interface IUsuarioRepository
{
    Task InsertAsync(Usuario usuario, CancellationToken cancellationToken);
}