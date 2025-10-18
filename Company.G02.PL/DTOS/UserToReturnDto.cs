namespace Company.G02.PL.DTOS
{
    public class UserToReturnDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }

        public string Email { get; set; }

        public IEnumerable<string>? Roles { get; set; }


    }
}
