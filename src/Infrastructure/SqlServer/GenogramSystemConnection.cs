using GenogramSystem.Core.Models.Configurations;

namespace GenogramSystem.SqlServer;
public class GenogramSystemConnection : Connection
{
    #region Overrides of Connection

    public override string ToString(string delimiter = ";")
    {
        return $"Data Source={Datasource}; Database={Database}; User Id={UserId}; Password={Password}; {AdditionalParameters}";
    }

    #endregion
}