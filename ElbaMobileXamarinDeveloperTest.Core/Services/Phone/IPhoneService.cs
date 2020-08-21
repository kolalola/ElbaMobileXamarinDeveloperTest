namespace ElbaMobileXamarinDeveloperTest.Core.Services.Phone
{
    public interface IPhoneService
    {
        string FormatNormalizedPhone(string phone);

        string Normalize(string phone);
    }
}
