﻿using System.Collections.Generic;
using KnowledgeRepresentationReasoning.Queries;
using KnowledgeRepresentationReasoning.World;
using KnowledgeRepresentationReasoning.World.Records;

namespace KnowledgeRepresentationInterface
{
    using KnowledgeRepresentationReasoning.Scenario;

    public static class Switcher
    {
        public static MainWindow pageSwitcher;

        public static void NextPage(List<ScenarioDescription> savedScenarios)
        {
            pageSwitcher.NextPage(savedScenarios);
        }

        public static void NextPage(int tInf, List<Fluent> fluents, List<WorldAction> action, List<WorldDescriptionRecord> statements)
        {
            pageSwitcher.NextPage(tInf, fluents, action, statements);
        }

        public static void PrevPage()
        {
            pageSwitcher.PrevPage();
        }

        public static void PrevPage(bool removeScenarios, bool removeEnvironment)
        {
            pageSwitcher.PrevPage(removeScenarios, removeEnvironment);
        }

        public static QueryResult ExecuteQuery(Query query, ScenarioDescription scenarioDescription)
        {
            return pageSwitcher.ExecuteQuery(query, scenarioDescription);
        }
    }
}