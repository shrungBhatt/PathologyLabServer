using OPSMBackend.DataEntities;
using OPSMBackend.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Services.Reagent
{
    public interface IReagentService
    {
        IEnumerable<Reagents> GetReagents();
        Reagents GetReagent(int id);
        void InsertReagent(Reagents reagent);
        void UpdateReagent(Reagents reagent);
        void DeleteReagent(int id);

        IEnumerable<TestReagentRelation> GetTestReagentRelations();
        TestReagentRelation GetTestReagentRelation(int id);
        void InsertTestReagentRelation(TestReagentRelation testReagentRelation);
        void UpdateTestReagentRelation(TestReagentRelation testReagentRelation);
        void DeleteTestReagentRelation(int id);

        DealersResponseModel GetDealers();
        void InsertDealer(Dealers dealer);
        void UpdateDealer(Dealers dealer);
        void DeleteDealer(int id);

        ReagentEntryResponseModel GetReagentEntries();
        void InsertReagentEntry(ReagentBillEntries reagentEntry);
        void UpdateReagentEntry(ReagentBillEntries reagentEntry);
        void DeleteReagentEntry(int id);
    }
}
