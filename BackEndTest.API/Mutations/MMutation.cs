using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;
using BackEndTest.DataAccess.Repositories.Contracts;
using BackEndTest.Database.Models;
using BackEndTest.Types.User;
using BackEndTest.Types.Movement;


namespace BackEndTest.API.Mutations
{
    public class MMutation : ObjectGraphType
    {
        public MMutation(IUserRepository userRepository, IMovementRepository movementRepository)
        {
            Description = "Mutation raiz da interface BackEndTest para Audaces";

            Field<UserType>(
                "addUser",
                description: "Cria um novo usuário",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<UserInputType>> {Name = "user", Description = "Usuário como InputType"}),
                    resolve: context =>
                    {
                        var user = context.GetArgument<User>("user");
                        return userRepository.Add(user);
                    });
            
            Field<IntGraphType>(
                "removeUser",
                description: "Remove um usuário, dado seu Id",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id", Description = "Id do usuário"}),
                resolve: context => userRepository.RemoveById(context.GetArgument<int>("id")));  
            
            Field<UserType>(
                "updateUser",
                description: "Atualiza os dados de um usuário, dado seu Id",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<UserInputType>> {Name = "user", Description = "Usuário como InputType"}),
                    resolve: context =>
                    {
                        var user = context.GetArgument<User>("user");
                        return userRepository.Update(user);
                    });

            
            //--------------------------- movements ------------------------------------------

            Field<MovementType>(
                "addMovement",
                description: "Cria uma nova movimentação",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<MovementInputType>> {Name = "movement", Description = "Movimentação como InputType"}),
                    resolve: context =>
                    {
                        var movement = context.GetArgument<Movement>("movement");
                        return movementRepository.Add(movement);
                    });       
     
            Field<IntGraphType>(
                "removeMovement",
                description: "Remove uma movimentação, dado seu Id",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id", Description = "Id do usuário"}),
                resolve: context => movementRepository.RemoveById(context.GetArgument<int>("id")));  
            
            Field<MovementType>(
                "updateMovement",
                description: "Atualiza os dados de uma movimentação, dado seu Id",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<MovementInputType>> {Name = "movement", Description = "Movimentação como InputType"}),
                    resolve: context =>
                    {
                        var movement = context.GetArgument<Movement>("movement");
                        return movementRepository.Update(movement);
                    });
        }
    }
}
