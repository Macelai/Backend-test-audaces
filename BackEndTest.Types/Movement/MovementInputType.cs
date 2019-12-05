using System;
using System.Collections.Generic;
using System.Text;
using GraphQL.Types;

namespace BackEndTest.Types.Movement
{
    public class MovementInputType : InputObjectGraphType
    {
        public MovementInputType()
        {
            Name = "MovementInputType";
            Description = "Dados do novo usuário";
            Field<IntGraphType>("id", description: "Id único");
            Field<IntGraphType>("amount", description: "Quantidade transferida em reais, sem formatação");
            Field<StringGraphType>("type", description: "Tipo da movimentação, 'IN' para entrada, 'OUT' para saída");
            Field<StringGraphType>("date",  description: "Data, formatado em 'yyyy-MM-dd HH:mm:ss.fff'");
            Field<StringGraphType>("description",  description: "Texto de descrição");
            Field<NonNullGraphType<IntGraphType>>("user",  description: "Id único do usuário que a fez");
        }
    }
}
