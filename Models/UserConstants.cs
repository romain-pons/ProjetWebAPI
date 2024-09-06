namespace ProjetWebAPI.Models
{
    public class UserConstants
    {
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel() { Username = "thomas", EmailAddress = "contact@tbdc.com", Password = "MyPass_w0rd", GivenName = "Thomas", Surname = "Thomas", Role = "Administrator" },
            new UserModel() { Username = "julien", EmailAddress = "julien@edu.igensia.com", Password = "MyPass_w0rd", GivenName = "Julien", Surname = "Julien", Role = "Seller" },
            new UserModel() { Username = "romain", EmailAddress = "romain@edu.igensia.com", Password = "MyPass_w0rd", GivenName = "Romain", Surname = "Romain", Role = "Seller" },
            new UserModel() { Username = "adrien", EmailAddress = "adrien@edu.igensia.com", Password = "MyPass_w0rd", GivenName = "Adrien", Surname = "Adrien", Role = "Seller" }
        };
    }
}
