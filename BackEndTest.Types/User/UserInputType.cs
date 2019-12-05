using System;
using System.Collections.Generic;
using System.Text;
using GraphQL.Types;

namespace BackEndTest.Types.User
{
    public class UserInputType : InputObjectGraphType
    {
        public UserInputType()
        {
            Name = "UserInputType";
            Description = "Dados do novo usuário";
            Field<IntGraphType>("id", description: "Id único");
            Field<NonNullGraphType<StringGraphType>>("name", description: "Nome completo");
            Field<IntGraphType>("salary", description: "Salário em reais sem formatação");
        }
    }
}
