using GraphQL.Types;
using BackEndTest.DataAccess.Repositories.Contracts;
using BackEndTest.Types.Movement;

namespace BackEndTest.Types.User
{
    public class UserType : ObjectGraphType<Database.Models.User>
    {
        public UserType(IMovementRepository movementRepository)
        {
            Field(x => x.Id).Description("Id único");
            Field(x => x.Name).Description("Nome completo");
            Field(x => x.Salary).Description("Salário em reais sem formatação");
        }
    }
}