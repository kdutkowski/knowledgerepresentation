using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowledgeRepresentationReasoning.World.Records;

namespace KnowledgeRepresentationInterface.Views.EnvironmentControls
{
    interface IEnvControl
    {
        WorldDescriptionRecord getWorldDescriptionRecord();
        void CleanValues();
    }
}
