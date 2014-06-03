using KnowledgeRepresentationReasoning.Logic;

namespace KnowledgeRepresentationReasoning.Queries
 {
     using KnowledgeRepresentationReasoning.Scenario;

     public class ExecutableScenarioQuery : Query
     {
         public ExecutableScenarioQuery(QuestionType questionType, ScenarioDescription scenario)
             : base(QueryType.ExecutableScenario, questionType){}

         public ExecutableScenarioQuery(QuestionType questionType)
             : base(QueryType.ExecutableScenario, questionType){}

         public override QueryResult CheckCondition(Vertex v)
         {
             _logger.Info("Checking executable scenario" + " with parameters:\nstate: " + v.ActualState + "\naction: " + v.ActualWorldAction);

             var result = QueryResult.Undefined;

             if (!v.IsPossible)
             {
                 result = QueryResult.False;
             }
             else if (!v.IsActive)
             {
                 result = QueryResult.True;
             }

             string logResult = "Executable: " + result;

             if (QueryResult.Undefined == result)
             {
                 _logger.Warn(logResult);
             }
             else
             {
                 _logger.Info(logResult);
             }

             return result;
         }

         public override string ToString()
         {
             return "Executable Scenario Query";
         }
     }
 }
