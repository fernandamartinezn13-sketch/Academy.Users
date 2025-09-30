namespace Academy.Users.Domain.Entities;

public class User
{
    private User()
    {
    }

    private User(
        string firstName,
        string lastName,
        string email,
        string address,
        string phoneNumber,
        string passwordHash,
        DateTime creationDate,
        bool status)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Address = address;
        PhoneNumber = phoneNumber;
        PasswordHash = passwordHash;
        CreationDate = creationDate;
        Status = status;
    }

    public Guid Id { get; private set; }
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string Address { get; private set; } = null!;
    public string PhoneNumber { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public DateTime CreationDate { get; private set; }
    public bool Status { get; private set; }

    public static User Create(
        string firstName,
        string lastName,
        string email,
        string address,
        string phoneNumber,
        string passwordHash,
        DateTime creationDate,
        bool status)
    {
        return new User(firstName, lastName, email, address, phoneNumber, passwordHash, creationDate, status);
    }
}
