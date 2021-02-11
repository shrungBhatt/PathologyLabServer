using OPSMBackend.DataEntities;
using OPSMBackend.Models.Response;
using OPSMBackend.Repositories;
using OPSMBackend.Services.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Services.Reagent
{
    public class ReagentService : IReagentService
    {
        readonly IRepository<Reagents> _reagentsRepository;
        readonly IRepository<TestReagentRelation> _testReagentRelationRepository;
        readonly IRepository<OtherTests> _otherTestsRepository;
        readonly IRepository<Dealers> _dealersRepository;
        readonly IRepository<ReagentBillEntries> _reagentEntriesRepository;
        readonly IUtil _util;


        public ReagentService(IRepository<Reagents> reagentsRepository,
                              IRepository<TestReagentRelation> testReagentRelationRepository,
                              IRepository<OtherTests> otherTestsRepository,
                              IRepository<Dealers> dealersRepository,
                              IRepository<ReagentBillEntries> reagentEntriesRepository,
                              IUtil util)
        {
            _reagentsRepository = reagentsRepository;
            _testReagentRelationRepository = testReagentRelationRepository;
            _otherTestsRepository = otherTestsRepository;
            _dealersRepository = dealersRepository;
            _reagentEntriesRepository = reagentEntriesRepository;
            _util = util;
        }

        public void DeleteDealer(int id)
        {
            _dealersRepository.Delete(_dealersRepository.Get(id));
        }

        public void DeleteReagentEntry(int id)
        {
            _reagentEntriesRepository.Delete(_reagentEntriesRepository.Get(id));
        }

        public void DeleteReagent(int id)
        {
            _reagentsRepository.Delete(_reagentsRepository.Get(id));
        }

        public void DeleteTestReagentRelation(int id)
        {
            _testReagentRelationRepository.Delete(_testReagentRelationRepository.Get(id));
        }

        public DealersResponseModel GetDealers()
        {
            return new DealersResponseModel { Dealers = _dealersRepository.GetAll().ToList() };
        }

        public Reagents GetReagent(int id)
        {
            return _reagentsRepository.Get(id);
        }

        public ReagentEntryResponseModel GetReagentEntries()
        {
            var reagents = _reagentsRepository.GetAll().ToList();
            var dealers = _dealersRepository.GetAll().ToList();
            var reagentEntries = _reagentEntriesRepository.GetAll().ToList();

            return new ReagentEntryResponseModel { Dealers = dealers, ReagentEntries = reagentEntries, Reagents = reagents };
        }

        public IEnumerable<Reagents> GetReagents()
        {
            return _reagentsRepository.GetAll().ToList();
        }

        public TestReagentRelation GetTestReagentRelation(int id)
        {
            return _testReagentRelationRepository.Get(id);
        }

        public IEnumerable<TestReagentRelation> GetTestReagentRelations()
        {
            _ = _reagentsRepository.GetAll().ToList();
            _ = _otherTestsRepository.GetAll().ToList();

            var reagentsTestRelations = _testReagentRelationRepository.GetAll().ToList();

            return reagentsTestRelations;
        }

        public void InsertDealer(Dealers dealer)
        {
            _dealersRepository.Insert(dealer);
        }

        public void InsertReagent(Reagents reagent)
        {
            _reagentsRepository.Insert(reagent);
        }

        public void InsertReagentEntry(ReagentBillEntries reagentEntry)
        {
            _reagentEntriesRepository.Insert(reagentEntry);
        }

        public void InsertTestReagentRelation(TestReagentRelation testReagentRelation)
        {
            _testReagentRelationRepository.Insert(testReagentRelation);
        }

        public void UpdateDealer(Dealers dealer)
        {
            var dealerFromDb = _dealersRepository.Get(dealer.Id);
            if (dealerFromDb != null)
            {
                _util.CopyProperties(dealer, dealerFromDb);
                _dealersRepository.Update(dealerFromDb);
            }
            else
            {
                throw new Exception("This dealer does not exist");
            }
        }

        public void UpdateReagent(Reagents reagent)
        {
            var reagentFromDb = _reagentsRepository.Get(reagent.Id);
            if (reagentFromDb != null)
            {
                reagentFromDb.ModifiedBy = reagent.ModifiedBy;
                reagentFromDb.ModifiedDate = reagent.ModifiedDate;
                reagentFromDb.AdditionalNotes = reagent.AdditionalNotes;
                reagentFromDb.Name = reagent.Name;
                reagentFromDb.UnitInStock = reagent.UnitInStock;
                reagentFromDb.ReorderLevel = reagent.ReorderLevel;
                reagentFromDb.UnitPrice = reagent.UnitPrice;
                reagentFromDb.PackingSize = reagent.PackingSize;
                reagentFromDb.PurchaseDate = reagent.PurchaseDate;
                reagentFromDb.ExpiryDate = reagent.ExpiryDate;

                _reagentsRepository.Update(reagentFromDb);
            }
            else
            {
                throw new Exception("Reagent not found");
            }
        }

        public void UpdateReagentEntry(ReagentBillEntries reagentEntry)
        {
            var reagentEntryFromDb = _reagentEntriesRepository.Get(reagentEntry.Id);
            if (reagentEntryFromDb != null)
            {
                _util.CopyProperties(reagentEntry, reagentEntryFromDb);
                _reagentEntriesRepository.Update(reagentEntryFromDb);
            }
            else
            {
                throw new Exception("This reagent entry does not exist");
            }
        }

        public void UpdateTestReagentRelation(TestReagentRelation testReagentRelation)
        {
            var relationFromDb = _testReagentRelationRepository.Get(testReagentRelation.Id);
            if (relationFromDb != null)
            {
                relationFromDb.ModifiedBy = testReagentRelation.ModifiedBy;
                relationFromDb.ModifiedDate = testReagentRelation.ModifiedDate;
                relationFromDb.ReagentId = testReagentRelation.ReagentId;
                relationFromDb.OtherTestId = testReagentRelation.OtherTestId;
                relationFromDb.QtyPerTest = testReagentRelation.QtyPerTest;
                relationFromDb.Unit = testReagentRelation.Unit;

                _testReagentRelationRepository.Update(relationFromDb);
            }
            else
            {
                throw new Exception("Test reagent relation does not exist");
            }
        }


    }
}
