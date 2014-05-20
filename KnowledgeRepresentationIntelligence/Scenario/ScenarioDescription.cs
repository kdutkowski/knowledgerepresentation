using System.Linq;

namespace KnowledgeRepresentationReasoning.Scenario
{
    using KnowledgeRepresentationReasoning.Expressions;
    using KnowledgeRepresentationReasoning.World;
    using System.Collections.Generic;

    public class ScenarioDescription
    {
        public string Name { get; set; }
        public List<ScenarioActionRecord> actions { get; set; }
        public List<ScenarioObservationRecord> observations { get; set; }

        public ScenarioDescription()
        {
            actions = new List<ScenarioActionRecord>();
            observations = new List<ScenarioObservationRecord>();
        }

        public ScenarioDescription(string name)
        {
            Name = name;
            actions = new List<ScenarioActionRecord>();
            observations = new List<ScenarioObservationRecord>();
        }

        public void addObservation(ILogicExpression expr, int time)
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
            try
            {
                return actions.Find(action => action.Time == t).WorldAction;
            }
            catch (System.ArgumentNullException)
            {
                return null;
            }
        }

        /// <summary>
        /// Now: Returns first observation found, TO DO: 
        /// Method returns many observations as one in form: "(ob1) && (ob2)...".
        /// Returns null if no observations found.
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        internal ScenarioObservationRecord GetObservationFromTime(int time)
        {
            ScenarioObservationRecord observation = new ScenarioObservationRecord(new SimpleLogicExpression(""), time);
            foreach (ScenarioObservationRecord obs in this.observations)
            {
                if (obs.Time.Equals(time))
                {
                    observation.Expr.AddExpression(obs.Expr);
                }
            }
            return observation;
        }

        internal bool CheckIfLeafIsPossible(Logic.Vertex leaf)
        {
            return CheckRecords(leaf.ActualState, null, leaf.Time);
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
                if (sor.Time >= actualTime)
                {
                    if (sor.Time < result)
                    {
                        result = sor.Time;
                    }
                }

            }
            if (result == int.MaxValue)
            {
                result = -1;
            }

            return result;
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
                {
                    if (sor.Time < result)
                    {
                        result = sor.Time;
                    }
                }
            }

            if (result == int.MaxValue)
            {
                result = -1;
            }

            return result;
        }

        public override string ToString()
        {
            var result = string.Empty;
            foreach (var action in actions)
                result += action.ToString() + "\n";
            foreach (var observarion in observations)
                result += observarion.ToString() + "\n";
            return result;
        }
    }
}
