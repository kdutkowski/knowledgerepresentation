using KnowledgeRepresentationReasoning.World;
using KnowledgeRepresentationReasoning.World.Records;
using System.Collections.Generic;
using System.Windows.Controls;

namespace KnowledgeRepresentationInterface
{
    public static class Switcher
    {
        public static MainWindow pageSwitcher;

        public static void Switch(UserControl newPage)
        {
            pageSwitcher.Navigate(newPage);
        }

        public static void Switch(UserControl newPage, int tInf, List<Fluent> fluents, List<WorldAction> actions,
                                  List<WorldDescriptionRecord> statements)
        {
            pageSwitcher.Navigate(newPage, tInf, fluents, actions, statements);
        }

    }
}
