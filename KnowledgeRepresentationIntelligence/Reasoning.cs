using log4net.Config;

[assembly: XmlConfigurator(Watch = true)]

namespace KnowledgeRepresentationReasoning
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        private ILog _logger { get; set; }

        private WorldDescription worldDescription { get; set; }

        private List<ScenarioDescription> scenarioDescriptionList { get; set; }

        public int TInf { get; set; }

        public Reasoning()
        {
            worldDescription = new WorldDescription();
            scenarioDescriptionList = new List<ScenarioDescription>();
            _logger = ServiceLocator.Current.GetInstance<ILog>();
            TInf = 100;
        }

        public void AddWorldDescriptionRecord(WorldDescriptionRecord record)
        {
            worldDescription.Descriptions.Add(new Tuple<WorldDescriptionRecordType, WorldDescriptionRecord>(record.Type, record));
        }

        public void RemoveWorldDescriptionRecord(WorldDescriptionRecord record)
        {
            var removeRecords = worldDescription.Descriptions.Where(t => t.Item2.Id == record.Id).ToList();
            for (int i = 0; i < removeRecords.Count; i++)
            {
                worldDescription.Descriptions.Remove(removeRecords[i]);
            }
        }

        public void UpdateWorldDescriptionRecord(WorldDescriptionRecord record)
        {
            throw new System.NotImplementedException();
        }

        public WorldDescription GetWorldDescription()
        {
            return worldDescription;
        }

        //public void AddScenarioDescriptionRecord(ScenarioDescriptionRecord record)
        //{
        //    if(record is ScenarioActionRecord)
        //    {
        //        ScenarioActionRecord add = record as ScenarioActionRecord;
        //        scenarioDescription.addACS(add.WorldAction, add.Time);
        //    }
        //    else if(record is ScenarioObservationRecord)
        //    {
        //        ScenarioObservationRecord add = record as ScenarioObservationRecord;
        //        scenarioDescription.addObservation(add.Expr, add.Time);
        //    }
        //}

        //public void RemoveScenarioDescriptionRecord(ScenarioDescriptionRecord record)
        //{
        //    if(record is ScenarioActionRecord)
        //    {
        //        ScenarioActionRecord remove = record as ScenarioActionRecord;
        //        var removeRecords = scenarioDescription.actions.Where(t => t.Id == remove.Id).ToList();
        //        for(int i = 0; i < removeRecords.Count; i++)
        //        {
        //            scenarioDescription.actions.Remove(removeRecords[i]);
        //        }
        //    }
        //    else if(record is ScenarioObservationRecord)
        //    {
        //        ScenarioObservationRecord remove = record as ScenarioObservationRecord;
        //        var removeRecords = scenarioDescription.observations.Where(t => t.Id == remove.Id).ToList();
        //        for(int i = 0; i < removeRecords.Count; i++)
        //        {
        //            scenarioDescription.observations.Remove(removeRecords[i]);
        //        }
        //    }
        //}

        //public void UpdateScenarioDescriptionRecord(ScenarioDescriptionRecord record)
        //{
        //    throw new System.NotImplementedException();
        //}

        public List<ScenarioDescription> GetScenarioDescriptionList()
        {
            return scenarioDescriptionList;
        }

        public QueryResult ExecuteQuery(Query query, ScenarioDescription scenarioDescription)
        {
            QueryResultsContainer queryResultsContainer = new QueryResultsContainer(query.questionType);

            //tree initialization
            Tree tree = new Tree(TInf);
            //add first level
            int numberOfImpossibleLeaf = 0;
            tree.AddFirstLevel(worldDescription, scenarioDescription, out numberOfImpossibleLeaf);

            queryResultsContainer.AddMany(QueryResult.False, numberOfImpossibleLeaf);

            if (tree.LastLevel.Count > 0 && CheckIfLeafIsPossible(tree.LastLevel[0], scenarioDescription))
            {
                QueryResult result = query.CheckCondition(tree.LastLevel[0]);
                if (result == QueryResult.True || result == QueryResult.False)
                {
                    queryResultsContainer.AddMany(result);
                }
                tree.LastLevel[0].SetQuery(query);
            }            
            
            //generate next level if query can't answer yet
            while (!queryResultsContainer.CanQuickAnswer() && tree.LastLevel.Count > 0)
            {
                int childsCount = tree.LastLevel.Count;
                for (int i = 0; i < childsCount; ++i)
                {
                    Vertex leaf = tree.LastLevel[i];
                    if (!CheckIfLeafIsPossible(leaf, scenarioDescription))
                    {
                        tree.DeleteChild(i);
                        queryResultsContainer.AddMany(QueryResult.False);
                        if (queryResultsContainer.CanQuickAnswer())
                        {
                            break;
                        }
                    }
                    else
                    {
                        tree.DeleteChild(i);
                        QueryResult queryInMiddleResult;
                        List<Vertex> nextLevel = leaf.GenerateChildsForLeaf(worldDescription, scenarioDescription, TInf, out queryInMiddleResult);
                        
                        queryResultsContainer.AddMany(queryInMiddleResult);
                        if (queryResultsContainer.CanQuickAnswer())
                        {
                            break;
                        }

                        foreach (var child in nextLevel)
                        {
                            if (!CheckIfLeafIsPossible(child, scenarioDescription))
                            {
                                queryResultsContainer.AddMany(QueryResult.False);
                                if (queryResultsContainer.CanQuickAnswer())
                                {
                                    break;
                                }
                            }
                            QueryResult result = query.CheckCondition(child);
                            if (result == QueryResult.True || result == QueryResult.False)
                            {
                                queryResultsContainer.AddMany(result);
                            }
                            else
                            {
                                tree.Add(child);
                            }
                        }
                    }
                }
            }

            return queryResultsContainer.CollectResults();
        }

        private bool CheckIfLeafIsPossible(Vertex leaf, ScenarioDescription scenarioDescription)
        {
            return leaf.IsPossible && leaf.ValidateActions() && worldDescription.Validate(leaf) && scenarioDescription.CheckIfLeafIsPossible(leaf);
        }

        public Task<QueryResult> ExecuteQueryAsync(Query query)
        {
            throw new System.NotImplementedException();
        }

        public static void Initialize()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new LoggingModule());
            builder.RegisterInstance(LogManager.GetLogger(typeof(Reasoning))).As<ILog>();
            builder.RegisterType<Tree>().As<ITree>();
            builder.RegisterType<SimpleLogicExpression>().As<ILogicExpression>();
            Container = builder.Build();
            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(Container));
        }

        public void AddScenarioDescriptionList(List<ScenarioDescription> scenarios)
        {
            scenarioDescriptionList.Concat(scenarios);
        }

        public void RemoveScenarioDescriptionList(List<ScenarioDescription> scenarios)
        {
            scenarioDescriptionList.RemoveAll(item => scenarios.Any(s => s.Name == item.Name));
        }
    }
}