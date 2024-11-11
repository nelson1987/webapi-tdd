namespace Application;

public sealed record Error(int Code, string Description)
{
    public static Error UserNotFound => new(100, "User not found");

    public static Error UserBadRequest => new(101, "User bad request");
    public static Error UserInternalServerError => new(102, "User internal server error");
}