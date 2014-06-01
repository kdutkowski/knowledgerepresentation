using KnowledgeRepresentationReasoning.World;
using NUnit.Framework;

namespace KnowledgeRepresentationReasoning.Test.DocumentationExamples
{
    [TestFixture]
    public class Example2 : TestBase
    {
        #region | ACTIONS |
        private readonly WorldAction _makingPancAction = new WorldAction
        {
            Id = "makingPanc",
            Duration = 1
        };
        private readonly WorldAction _makingCakeAction = new WorldAction
        {
            Id = "makingCake",
            Duration = 1
        };
        private readonly WorldAction _buyEggsAction = new WorldAction
        {
            Id = "buyEggs",
            Duration = 2
        };
        #endregion

        #region | FLUENTS |
        private Fluent _eggsFluent = new Fluent()
        {
            Name = "eggs"
        };
        #endregion
        [SetUp]
        public void SetUp()
        {
        }

        #region | EXAMPLE 2 |

        //Mick i Sarah sa para, wiec maja wspólne produkty spozywcze, ale posiłki zwykle jadaja oddzielnie.
        //Pewnego dnia Sarah chce zrobic ciasto, a Mick nalesniki. Nie moga byc one robione w tym samym czasie
        //ze wzgledu koniecznosc uzycia miksera do przygotowania obu. Ponadto, zrobienie jednego lub drugiego
        //dania zuzywa cały zapas jajek dostepnych w mieszkaniu, wiec trzeba je potem dokupic.

        //4.2.2 Opis akcji
        //(making panc; 1) causes !eggs if eggs
        //(making cake; 1) causes !eggs if eggs
        //(buy eggs; 2) causes eggs

        //4.2.3 Scenariusz
        //Sc =(OBS; ACS)
        //OBS = f(eggs; 0)g
        //ACS = f((making panc; 1); 0); ((making cake; 1); 2)g

        //4.2.4 Kwerendy
        //1. eggs at 0 when Sc
        //2. eggs at 2 when Sc


        #endregion
    }
}
