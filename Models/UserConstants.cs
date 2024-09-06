namespace ProjetWebAPI.Models
{
    public class UserConstants
    {
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel() { Username = "thomas", EmailAddress = "thomas.professor@email.com", Password = "Professor", GivenName = "Thomas", Surname = "Thomas", Role = "Professor" },
            new UserModel() { Username = "julien", EmailAddress = "julien.student@email.com", Password = "Student", GivenName = "Julien", Surname = "Julien", Role = "Student" },
            new UserModel() { Username = "romain", EmailAddress = "romain.student@email.com", Password = "Student", GivenName = "Romain", Surname = "Romain", Role = "Student" },
            new UserModel() { Username = "adrien", EmailAddress = "adrien.student@email.com", Password = "Student", GivenName = "Adrien", Surname = "Adrien", Role = "Student" },
        };
    }
}
