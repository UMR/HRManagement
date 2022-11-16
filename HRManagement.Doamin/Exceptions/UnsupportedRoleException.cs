namespace HRManagement.Domain.Exceptions
{
    public class UnsupportedRoleException : Exception
    {
        public UnsupportedRoleException(string code)
            : base($"Colour \"{code}\" is unsupported.")
        {
        }
    }
}
