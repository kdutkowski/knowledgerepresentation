using KnowledgeRepresentationReasoning.World;
using NUnit.Framework;

namespace KnowledgeRepresentationReasoning.Test.DocumentationExamples
{
    [TestFixture]
    public class Example1 : TestBase
    {
        #region | ACTIONS |
        private readonly WorldAction _drinkAction = new WorldAction
        {
            Id = "drink",
            Duration = 2
        };
        private readonly WorldAction _sleepAction = new WorldAction
        {
            Id = "sleep",
            Duration = 8
        };
        #endregion

        #region | FLUENTS |
        private Fluent _drunkFluent = new Fluent()
        {
            Name = "drunk"
        };
        private Fluent _hangoverFluent = new Fluent()
        {
            Name = "hangover"
        };

        [SetUp]
        public void SetUp()
        {
        }

        #region | EXAMPLE 1 |

        //Michał jest pracujacym studentem. W srode o godzinie 8.00 powinien pojawic sie w pracy zupełnie
        //trzezwy. Mimo to we wtorek postanowił pójsc do baru. Jesli Michał sie napije, stanie sie pijany. Jesli
        //pójdzie spac przestanie byc pijany, ale stanie sie skacowany, co równiez bedzie niedopuszczalne w jego
        //pracy.

        //4.1.2 Opis akcji
        //(drink, 2) causes drunk
        //(sleep, 8) causes !drunk
        //(sleep, 8) causes hangover if drunk

        //4.1.3 Scenariusze
        //Sc =(OBS;ACS)
        //OBS = f(!drunk; 0); (!hangover; 0); (!drunk; 10); (!hangover; 10)g
        //ACS = f((drink; 2); 0); ((sleep; 8); 2)g
        //Sc2 =(OBS2; ACS2)
        //OBS2 = f(!drunk; 0); (!hangover; 0); (!drunk; 10); (!hangover; 10)g
        //ACS2 = f((sleep; 8); 1)g

        //4.1.4 Kwerendy
        //1. ever executable Sc
        //2. ever executable Sc2

        #endregion
    }
}
