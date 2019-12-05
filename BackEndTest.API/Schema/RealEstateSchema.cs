using GraphQL;
using BackEndTest.API.Mutations;
using BackEndTest.API.Queries;

namespace BackEndTest.API.Schema
{
    public class BackEndTestSchema : GraphQL.Types.Schema
    {
        public BackEndTestSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<QQuery>();
            Mutation = resolver.Resolve<MMutation>();
        }
    }
}
