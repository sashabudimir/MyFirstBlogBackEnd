namespace MyFirstBlog.Dtos;

public record CreatePostDto
{
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}