namespace ProjetWebAPI.Models
{
    public class UserConstants
    {
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel() { Username = "thomas", EmailAddress = "thomas.professor@email.com", Password = "Professor", GivenName = "Thomas", Surname = "Thomas", Roles = "Professor" },
            new UserModel() { Username = "julien", EmailAddress = "julien.student@email.com", Password = "Student", GivenName = "Julien", Surname = "Julien", Roles = "Student" },
            new UserModel() { Username = "romain", EmailAddress = "romain.student@email.com", Password = "Student", GivenName = "Romain", Surname = "Romain", Roles = "Student" },
            new UserModel() { Username = "adrien", EmailAddress = "adrien.student@email.com", Password = "Student", GivenName = "Adrien", Surname = "Adrien", Roles = "Student" },
        };
    }
}
