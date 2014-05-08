using KnowledgeRepresentationReasoning.Queries;
using KnowledgeRepresentationReasoning.World;
using KnowledgeRepresentationReasoning.World.Records;
using System.Collections.Generic;

namespace KnowledgeRepresentationInterface
{
    public static class Switcher
    {
        public static MainWindow pageSwitcher;

        public static void NextPage()
        {
            pageSwitcher.NextPage();
        }

        public static void NextPage(int tInf, List<Fluent> fluents, List<WorldAction> action, List<WorldDescriptionRecord> statements)
        {
            pageSwitcher.NextPage(tInf, fluents, action, statements);
        }

        public static void PrevPage()
        {
            pageSwitcher.PrevPage();
        }

        public static QueryResult ExecuteQuery(Query query)
        {
            return pageSwitcher.ExecuteQuery(query);
        }

    }
}
