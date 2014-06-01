using KnowledgeRepresentationReasoning.World;
using NUnit.Framework;

namespace KnowledgeRepresentationReasoning.Test.DocumentationExamples
{
    [TestFixture]
    public class Example3 : TestBase
    {
        #region | ACTIONS |
        private readonly WorldAction _goesBillAction = new WorldAction
        {
            Id = "goesBill",
            Duration = 2
        };
        private readonly WorldAction _runsMaxAction = new WorldAction
        {
            Id = "runsMax",
            Duration = 2
        };
        private readonly WorldAction _whistlesBillAction = new WorldAction
        {
            Id = "whistlesBill",
            Duration = 1
        };
        private readonly WorldAction _barksMaxAction = new WorldAction
        {
            Id = "barksMax",
            Duration = 1
        };
        #endregion

        #region | FLUENTS |
        private Fluent _runMaxFluent = new Fluent()
        {
            Name = "runMax"
        };
        private Fluent _barkMaxrFluent = new Fluent()
        {
            Name = "barkMax"
        };
        #endregion

        [SetUp]
        public void SetUp()
        {
        }

        #region | EXAMPLE 3 |
        //Mamy Billa i psa Maxa. Jesli Bill idzie, to Max biegnie przez jakis czas. Jesli Bill gwizdze, Max szczeka
        //przez jakis czas. Jesli Bill zatrzymuje sie, Max równiez. Jesli Bill przestaje gwizdac, to Max przestaje
        //szczekac.

        //Opis akcji
        //(goes Bill; 2) causes run Max
        //(goes Bill; 2) invokes (runs Max; 2) after 0
        //(runs Max; 2) causes !run Max
        //(whistles Bill; 1) causes bark Max
        //(whistles Bill; 1) invokes (barks Max; 1) after 0
        //(barks Max; 1) causes !bark Max

        //Scenariusz
        //Sc =(OBS; ACS)
        //OBS = (!run Max; 0); (!bark Max; 0)
        //ACS = ((goes Bill; 2); 1); ((whistles Bill; 1); 5); ((goes Bill; 2); 7)

        //Kwerendy
        //1. performing runs Max at 8 when Sc
        //2. performing runs Max when Sc
        //3. performing at 8 when Sc

        #endregion
    }
}
