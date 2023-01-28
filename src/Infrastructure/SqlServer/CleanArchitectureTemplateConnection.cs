using CleanArchitectureTemplate.Core.Models.Configurations;

namespace CleanArchitectureTemplate.SqlServer;
public class CleanArchitectureTemplateConnection : Connection
{
    #region Overrides of Connection

    public override string ToString(string delimiter = ";")
    {
        return $"Data Source={Datasource}; Database={Database}; User Id={UserId}; Password={Password}; {AdditionalParameters}";
    }

    #endregion
}