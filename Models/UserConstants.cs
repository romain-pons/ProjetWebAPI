namespace ProjetWebAPI.Models
{
    public class UserConstants
    {
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel() { Username = "jason_admin", EmailAddress = "jason.admin@email.com", Password = "MyPass_w0rd", GivenName = "Jason", Surname = "Bryant", Role = "Administrator" },
            new UserModel() { Username = "julien", EmailAddress = "julien.admin@email.com", Password = "MyPass_w0rd", GivenName = "Julien", Surname = "Bryant", Role = "Administrator" },
            new UserModel() { Username = "romain", EmailAddress = "romain.admin@email.com", Password = "MyPass_w0rd", GivenName = "Romain", Surname = "Bryant", Role = "Administrator" },
            new UserModel() { Username = "adrien", EmailAddress = "adrien.admin@email.com", Password = "MyPass_w0rd", GivenName = "Adrien", Surname = "Bryant", Role = "Administrator" },
            new UserModel() { Username = "elyse", EmailAddress = "elyse.seller@email.com", Password = "MyPass_w0rd", GivenName = "Elyse", Surname = "Lambert", Role = "Seller" },
        };
    }
}
