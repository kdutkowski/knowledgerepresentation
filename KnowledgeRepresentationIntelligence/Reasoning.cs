using log4net.Config;

[assembly: XmlConfigurator(Watch = true)]
namespace KnowledgeRepresentationReasoning
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Autofac;
    using Autofac.Extras.CommonServiceLocator;
    using KnowledgeRepresentationReasoning.Expressions;
    using KnowledgeRepresentationReasoning.Helpers;
    using KnowledgeRepresentationReasoning.Logic;
    using KnowledgeRepresentationReasoning.Queries;
    using KnowledgeRepresentationReasoning.Scenario;
    using KnowledgeRepresentationReasoning.World;
    using KnowledgeRepresentationReasoning.World.Records;
    using log4net;
    using Microsoft.Practices.ServiceLocation;

    public class Reasoning : IReasoning
    {
        private static IContainer Container { get; set; }

        private ILog Logger { get; set; }

        private WorldDescription WorldDescription { get; set; }

        public int Inf { get; set; }

        public Reasoning()
        {
            WorldDescription = new WorldDescription();
            Logger = ServiceLocator.Current.GetInstance<ILog>();
            Inf = 100;
        }

        public void AddWorldDescriptionRecord(WorldDescriptionRecord record)
        {
            WorldDescription.Descriptions.Add(
                new Tuple<WorldDescriptionRecordType, WorldDescriptionRecord>(record.Type, record));
        }

        public void RemoveWorldDescriptionRecord(WorldDescriptionRecord record)
        {
            WorldDescription.Descriptions.RemoveAll(t => t.Item2.Id == record.Id);
        }

        public WorldDescription GetWorldDescription()
        {
            return WorldDescription;
        }

        public QueryResult ExecuteQuery(Query query, ScenarioDescription scenarioDescription)
        {
            var queryResultsContainer = new QueryResultsContainer(query.questionType);

            var tree = new Tree(Inf);
            int numberOfImpossibleLeaf;
            bool worldCanStart = tree.AddFirstLevel(WorldDescription, scenarioDescription,
                out numberOfImpossibleLeaf);
            queryResultsContainer.AddMany(QueryResult.False, numberOfImpossibleLeaf);

            if (worldCanStart == false)
            {
                return QueryResult.False;
            }

            //generate next level if query can't be answered yet
            while (!queryResultsContainer.CanQuickAnswer() && tree.LastLevel.Count > 0)
            {
                var newLevel = new List<Vertex>();
                foreach (Vertex leaf in tree.LastLevel)
                {
                    List<Vertex> nextLevel = leaf.GenerateChildsForLeaf(WorldDescription, scenarioDescription, Inf);

                    //koniec sciezki
                    if (!leaf.IsActive)
                    {
                        QueryResult result = query.CheckCondition(leaf);
                        queryResultsContainer.AddOne(result);
                        continue;
                    }
                    foreach (var child in nextLevel)
                    {
                        if (!child.IsPossible)
                        {
                            queryResultsContainer.AddOne(QueryResult.False);
                        }
                        else
                        {
                            QueryResult result = query.CheckCondition(child);
                            if (result == QueryResult.True || result == QueryResult.False)
                            {
                                queryResultsContainer.AddOne(result);
                            }
                            else
                            {
                                newLevel.Add(child);
                            }
                        }
                    }
                }
                tree.LastLevel = newLevel;
            }

            return queryResultsContainer.CollectResults();
        }

        public Task<QueryResult> ExecuteQueryAsync(Query query)
        {
            throw new NotImplementedException();
        }

        public static void Initialize()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new LoggingModule());
            builder.RegisterInstance(LogManager.GetLogger(typeof (Reasoning))).As<ILog>();
            builder.RegisterType<Tree>().As<ITree>();
            builder.RegisterType<SimpleLogicExpression>().As<ILogicExpression>();
            Container = builder.Build();
            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(Container));
        }
    }
}