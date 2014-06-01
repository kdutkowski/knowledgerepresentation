using KnowledgeRepresentationReasoning.World;
using NUnit.Framework;

namespace KnowledgeRepresentationReasoning.Test.DocumentationExamples
{
    [TestFixture]
    public class Example4 : TestBase
    {
        #region | ACTIONS |
        private readonly WorldAction _clicksButtonAction = new WorldAction
        {
            Id = "clicksButton",
            Duration = 1
        };
        private readonly WorldAction _switchesOnComputerAction = new WorldAction
        {
            Id = "switchesOnComputer",
            Duration = 2
        };
        private readonly WorldAction _disconnectsPowerAction = new WorldAction
        {
            Id = "disconnectsPower",
            Duration = 1
        };
        #endregion

        #region | FLUENTS |
        private Fluent _connectPowerComputerFluent = new Fluent()
        {
            Name = "connectPowerComputer"
        };
        private Fluent _onComputerFluent = new Fluent()
        {
            Name = "onComputer"
        };
        #endregion

        [SetUp]
        public void SetUp()
        {
        }

        #region | EXAMPLE 4 |
        //Mamy Billa oraz komputer. Bill moze nacisnac przycisk Włacz lub odłaczyc komputer od zasilania.
        //Poczatkowo komputer jest wyłaczony i podłaczony do zasilania. Jezeli zostanie nacisniety jego przycisk
        //Włacz oraz komputer jest podłaczony do zasilania, to komputer właczy sie. Odłaczenie komputera od
        //pradu powoduje, ze komputer bedzie odłaczony od zasilania oraz wyłaczony.

        //4.4.2 Opis akcji
        //(clicks button on; 1) invokes (switches on computer; 2) after 0 if connect power computer
        //(switches on computer; 2) causes on computer
        //(disconnects power; 1) causes !connect power computer
        //(disconnects power; 1) causes !on computer

        //4.4.3 Scenariusz
        //Sc =(OBS; ACS)
        //OBS = f(!on computer; 0); (connect power computer; 0)g
        //ACS = f(clicks button on; 1); 1); ((disconnects power; 1); 4); ((clicks button on; 1); 5)g

        //4.4.4 Kwerendy
        //1. on computer at 7 when Sc
        //2. accesible on computer when Sc


        #endregion
    }
}
