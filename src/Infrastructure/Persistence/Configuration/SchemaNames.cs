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

    public static string User = "User";
    public static string Roles = "Roles";
    public static string RoleClaims = "RoleClaims";
    public static string UserRoles = "UserRoles";
    public static string UserClaims = "UserClaims";
    public static string UserLogins = "UserLogins";
    public static string UserTokens = "UserTokens";

    public static string Product = "Product";
    public static string Brand = "Brand";
    public static string Cash = "Cash";
    public static string Order = "Order";

}
