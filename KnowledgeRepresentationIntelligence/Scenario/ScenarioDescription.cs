using System.Linq;

namespace KnowledgeRepresentationReasoning.Scenario
{
    using KnowledgeRepresentationReasoning.Expressions;
    using KnowledgeRepresentationReasoning.World;
    using System.Collections.Generic;

    public class ScenarioDescription
    {
        public string Name { get; set; }
        public List<ScenarioActionRecord> Actions { get; set; }
        public List<ScenarioObservationRecord> Observations { get; set; }

        public ScenarioDescription()
        {
            Actions = new List<ScenarioActionRecord>();
            Observations = new List<ScenarioObservationRecord>();
        }

        public ScenarioDescription(string name)
        {
            Name = name;
            Actions = new List<ScenarioActionRecord>();
            Observations = new List<ScenarioObservationRecord>();
        }

        public void addObservation(ILogicExpression expr, int time)
        {
            ScenarioObservationRecord OBS = new ScenarioObservationRecord(expr, time);
            Observations.Add(OBS);
        }

        public void addACS(WorldAction worldAction, int time)
        {
            ScenarioActionRecord ACS = new ScenarioActionRecord(worldAction, time);
            Actions.Add(ACS);
        }

        List<ScenarioDescriptionRecord> GetRecords(int time)
        {
            List<ScenarioDescriptionRecord> records = new List<ScenarioDescriptionRecord>();
            foreach (ScenarioObservationRecord obs in Observations)
            {
                if (obs.Time == time)
                {
                    records.Add(obs);
                }
            }

            foreach (ScenarioActionRecord acs in Actions)
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
            foreach (ScenarioObservationRecord obs in Observations)
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
                return Actions.Find(action => action.Time == t).WorldAction;
            }
            catch (System.NullReferenceException)
            {
                return null;
            }
        }

        /// <summary>
        /// Method returns many observations as one in form: "(ob1) && (ob2)..."
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        internal ScenarioObservationRecord GetObservationFromTime(int time)
        {
            ScenarioObservationRecord observation = new ScenarioObservationRecord(new SimpleLogicExpression(""), time);
            foreach (ScenarioObservationRecord obs in this.Observations)
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
            foreach (ScenarioObservationRecord sor in this.Observations)
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
            foreach (ScenarioActionRecord sor in this.Actions)
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
            foreach (var action in Actions)
                result += action.ToString() + "\n";
            foreach (var observarion in Observations)
                result += observarion.ToString() + "\n";
            return result;
        }

        public override bool Equals(object obj)
        {
            if(obj is ScenarioDescription)
            {
                var scenarioDescription = obj as ScenarioDescription;
                if(scenarioDescription.Name.Equals(this.Name))
                    return true;
            }
            return false;
        }
    }
}
