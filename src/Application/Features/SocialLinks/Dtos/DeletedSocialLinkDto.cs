namespace Application.Features.SocialLinks.Dtos
{
    public class DeletedSocialLinkDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string GithubUrl { get; set; }
    }
}
