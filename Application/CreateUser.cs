using Domain;
using FluentValidation;

namespace Application;

public class CreateUser
{
    public record Command(string Email, string Password);

    public record Response(int Id, string Email, string Password);

    public interface IHandler
    {
        Task<Result<Response>> HandleAsync(Command command, CancellationToken cancellationToken);
    }

    public class Handler : IHandler
    {
        private readonly IUsuarioRepository _repository;
        private readonly IValidator<Command> _validator;

        public Handler(IUsuarioRepository repository, IValidator<Command> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Result<Response>> HandleAsync(Command command, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(command, cancellationToken);
                if (!validationResult.IsValid) return Result<Response>.Failure(Error.UserBadRequest);

                var entity = command.ToEntity();
                await _repository.InsertAsync(entity, cancellationToken);
                var response = entity.ToResponse();

                return Result<Response>.Success(response);
            }
            catch (Exception ex)
            {
                return Result<Response>.Failure(Error.UserInternalServerError);
            }
        }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}

public static class UsuarioExtensions
{
    public static Usuario ToEntity(this CreateUser.Command command)
    {
        return new Usuario(command.Email, command.Password);
    }

    public static CreateUser.Response ToResponse(this Usuario entity)
    {
        return new CreateUser.Response(entity.Id, entity.Email, entity.Password);
    }
}