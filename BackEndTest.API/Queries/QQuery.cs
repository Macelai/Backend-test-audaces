using System.Collections.Generic;
using GraphQL.Types;
using BackEndTest.Types.User;
using BackEndTest.Types.Movement;
using BackEndTest.DataAccess.Repositories.Contracts;

namespace BackEndTest.API.Queries
{
    public class QQuery : ObjectGraphType
    {
        public QQuery(IUserRepository userRepository, IMovementRepository movementRepository)
        {
            Description = "Query raiz da interface BackEndTest para Audaces";
            
            Field<ListGraphType<UserType>>(
                "users",
                description: "Busca todos os usuários",
                resolve: context => userRepository.GetAll());

            Field<UserType>(
                "userById",
                description: "Busca um usuário pelo seu Id",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id", Description = "Id do usuário" }),
                resolve: context => userRepository.GetById(context.GetArgument<int>("id")));


            // ------------------------------------- movements -----------------------------------
            Field<ListGraphType<MovementType>>(
                "movements",
                description: "Busca todas as movimentações",
                resolve: context => movementRepository.GetAll());

            Field<ListGraphType<MovementType>>(
                "movementByUserId",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "user", Description = "Id do usuário"  }),
                description: "Busca todas as movimentações de um determinado usuário, dado seu Id",
                resolve: context => movementRepository.GetAllMovementForUserId(context.GetArgument<int>("user")));
            
            Field<IntGraphType>(
                "balanceBetweenDates",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "start", Description = "Data de inicio" },
                           new QueryArgument<NonNullGraphType<StringGraphType>>{ Name = "end", Description = "Data do fim" }),
                description: "Busca o saldo global, entre duas datas",
                resolve: context => movementRepository.balanceBetweenDates(context.GetArgument<string>("start"), context.GetArgument<string>("end")));

            Field<IntGraphType>(
                "balanceBetweenDatesByUser",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "start", Description = "Data de inicio" },
                           new QueryArgument<NonNullGraphType<StringGraphType>>{ Name = "end", Description = "Data do fim" },
                           new QueryArgument<NonNullGraphType<IntGraphType>>{ Name = "userId", Description = "Id do usuário" }),
                description: "Busca o saldo do usuário, entre duas datas",
                resolve: context => movementRepository.balanceBetweenDatesByUser(context.GetArgument<string>("start"), context.GetArgument<string>("end"), 
                                                                                 context.GetArgument<int>("userId")));
        }
    }
}