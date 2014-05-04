namespace KnowledgeRepresentationReasoning.Scenario
{
    using KnowledgeRepresentationReasoning.Expressions;
    using KnowledgeRepresentationReasoning.World;
    using System.Collections.Generic;

    public class ScenarioDescription
    {
        private List<ScenarioActionRecord> actions;
        private List<ScenarioObservationRecord> observations;

        public ScenarioDescription()
        {
            actions = new List<ScenarioActionRecord>();
            observations = new List<ScenarioObservationRecord>();
        }

        internal void addObservation(ILogicExpression expr, int time)
        {
            ScenarioObservationRecord OBS = new ScenarioObservationRecord(expr, time);
            observations.Add(OBS);
        }

        public void addACS(Action action, int time)
        {
            ScenarioActionRecord ACS = new ScenarioActionRecord(action, time);
            actions.Add(ACS);
        }

        List<ScenarioDescriptionRecord> GetRecords(int time)
        {
            List<ScenarioDescriptionRecord> records = new List<ScenarioDescriptionRecord>();
            foreach (ScenarioObservationRecord obs in observations)
            {
                if (obs.Time == time)
                {
                    records.Add(obs);
                }
            }

            foreach (ScenarioActionRecord acs in actions)
            {
                if (acs.Time == time)
                {
                    records.Add(acs);
                }
            }

            return records;
        }

        public bool CheckRecords(State state, Action action, int time)
        {
            foreach (ScenarioObservationRecord obs in observations)
            {
                if (!obs.checkState(state, time))
                {
                    return false;
                }
            }
            return true;
        }

        internal Action GetActionAtTime(int t)
        {
            throw new System.NotImplementedException();
        }

        internal List<ScenarioObservationRecord> GetNextObservationFromTime(int time)
        {
            throw new System.NotImplementedException();
        }
    }
}
