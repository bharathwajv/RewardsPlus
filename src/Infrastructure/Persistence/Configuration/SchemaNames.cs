namespace RewardsPlus.Infrastructure.Persistence.Configuration;

internal static class SchemaNames
{
    // TODO: figure out how to capitalize these only for Oracle
    public static string Auditing = "Auditing"; // "AUDITING";
    public static string Catalog = "Catalog"; // "CATALOG";
    public static string Identity = "Identity"; // "IDENTITY";
    public static string MultiTenancy = "MultiTenancy"; // "MULTITENANCY";
    public static string Application = "Application";
}
internal static class TableNames
{
    public static string AuditTrails = "AuditTrails";
    public static string Tenants = "Tenants";

    public static string Users = "Users";
    public static string Roles = "Roles";
    public static string RoleClaims = "RoleClaims";
    public static string UserRoles = "UserRoles";
    public static string UserClaims = "UserClaims";
    public static string UserLogins = "UserLogins";
    public static string UserTokens = "UserTokens";

    public static string Products = "Products";
    public static string Brands = "Brands";
    public static string Cash = "Cash";
    public static string Orders = "Orders";
    public static string GiftingInfo = "GiftingInfo";
}
