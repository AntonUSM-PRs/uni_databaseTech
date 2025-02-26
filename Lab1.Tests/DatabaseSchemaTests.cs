using Lab1.DataLayer;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace TestProject1;

[UsesVerify]
public class DatabaseSchemaTests
{
    [Fact]
    public async Task Test1()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .Build();
        var connectionString = config.GetValue<string>("ConnectionStrings:MainDatabase");
        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync();
        var tables = await DatabaseSchemaHelper.GetTables(connection);
        await Verify(tables);
    }


    [Fact]
    public void JoinableDbPropertyListTest()
    {
        var list = new JoinableDbPropertyList(new[] { "a", "b", "c" });
        var result = list.Prefix("t");
        var str = $"{result}";
        Assert.Equal("t.[a], t.[b], t.[c]", str);
    }
}