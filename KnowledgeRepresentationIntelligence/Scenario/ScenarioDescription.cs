namespace KnowledgeRepresentationReasoning.Scenario
{
    using KnowledgeRepresentationReasoning.Expressions;
    using KnowledgeRepresentationReasoning.World;
    using System.Collections.Generic;

    public class ScenarioDescription
    {
        public List<ScenarioActionRecord> actions { get; set; }
        public List<ScenarioObservationRecord> observations { get; set; }

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

        public void addACS(WorldAction worldAction, int time)
        {
            ScenarioActionRecord ACS = new ScenarioActionRecord(worldAction, time);
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

        public bool CheckRecords(State state, WorldAction worldAction, int time)
        {
            foreach (ScenarioObservationRecord obs in observations)
            {
                if (!obs.CheckState(state, time))
                {
                    return false;
                }
            }
            return true;
        }

        internal WorldAction GetActionAtTime(int t)
        {
            foreach (ScenarioActionRecord sar in this.actions)
            {
                if (sar.Time.Equals(t)) return sar.WorldAction;

            }

            return null;



            //throw new System.NotImplementedException();
        }

        /// <summary>
        /// Methods returns many observations as one in form: "(ob1) && (ob2)...", null if no observations found.
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        internal ScenarioObservationRecord GetObservationFromTime(int time)
        {
            //List<ScenarioObservationRecord> matches = new List<ScenarioObservationRecord>(observations.Count);
            foreach (ScenarioObservationRecord sor in this.observations)
            {
                if (sor.Time.Equals(time)) return sor;
                
            }

            return null;

            //throw new System.NotImplementedException();

            
        }

        internal bool CheckIfLeafIsPossible(Logic.Vertex leaf)
        {
            return CheckRecords(leaf.State, null, leaf.Time);
        }

        /// <summary>
        /// Methods return time from neariest observation.
        /// </summary>
        /// <param name="actualTime"></param>
        /// <returns></returns>
        internal int GetNextObservationTime(int actualTime)
        {
            int result = int.MaxValue;
            foreach (ScenarioObservationRecord sor in this.observations)
            {
                if (sor.Time>=actualTime)
                    if(sor.Time<result)result=sor.Time;

            }
            if (result == int.MaxValue) result = -1;
            return result;
           
            //throw new System.NotImplementedException();

        }

        /// <summary>
        /// Methods returns neariest action in or after actualTime, or -1 if no action is found
        /// </summary>
        /// <param name="actualTime"></param>
        /// <returns></returns>
        internal int GetNextActionTime(int actualTime)
        {
            int result = int.MaxValue;
            foreach (ScenarioObservationRecord sor in this.observations)
            {
                if (sor.Time >= actualTime)
                    if (sor.Time < result) result = sor.Time;

            }

            if(result==int.MaxValue)result=-1;
            return result;

            //throw new System.NotImplementedException();

        }
    }
}
