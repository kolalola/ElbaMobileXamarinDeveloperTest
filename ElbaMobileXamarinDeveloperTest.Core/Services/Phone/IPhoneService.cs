using System;
using System.Collections.Generic;
using System.Text;

namespace ElbaMobileXamarinDeveloperTest.Core.Services.Phone
{
    public interface IPhoneService
    {
        string FormatNormalizedPhone(string phone);

        string Normalize(string phone);
    }
}
