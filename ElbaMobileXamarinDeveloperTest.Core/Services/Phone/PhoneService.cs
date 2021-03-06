﻿using PhoneNumbers;

namespace ElbaMobileXamarinDeveloperTest.Core.Services.Phone
{
    public class PhoneService : IPhoneService
    {
        public string FormatNormalizedPhone(string phone)
        {
            long.TryParse(phone, out long longPhone);
            return longPhone.ToString("+# (###) ###-####");
        }

        public string Normalize(string phone) => PhoneNumberUtil.Normalize(phone);
    }
}
