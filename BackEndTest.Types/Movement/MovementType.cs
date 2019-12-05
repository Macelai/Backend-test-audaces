using System;
using System.Collections.Generic;
using System.Text;
using GraphQL.Types;

namespace BackEndTest.Types.Movement
{
    public class MovementType : ObjectGraphType<Database.Models.Movement>
    {
        public MovementType()
        {
            Field(x => x.Id).Description("Id único");
            Field(x => x.Amount).Description("Quantidade transferida em reais, sem formatação");
            Field(x => x.Type).Description("Tipo da movimentação, 'IN' para entrada, 'OUT' para saída");
            Field(x => x.Date).Description("Data, formatado em 'yyyy-MM-dd HH:mm:ss.fff'");
            Field(x => x.Description).Description("Texto de descrição");
            Field(x => x.User).Description("Id único do usuário que a fez");
            //Field<UserType>("payments",
        }
    }
}
