﻿namespace PromoManagemntApp.Domain.Constants
{
    public static class UserRolesConstants
    {
        public const string Admin = nameof(Admin);

        public const string Manager = nameof(Manager);

        public const string FinanceSpecialist = nameof(FinanceSpecialist);

        public static readonly string[] AllowedRoles = { Admin, Manager, FinanceSpecialist };
    }
}
