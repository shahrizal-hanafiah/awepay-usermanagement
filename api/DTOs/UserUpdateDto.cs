namespace API.DTOs
{
    public class UserUpdateDto
    {
        public string FullName { get; set; }
        public string KnownAs { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}