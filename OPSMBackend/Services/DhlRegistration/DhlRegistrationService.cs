using OPSMBackend.DataEntities;
using OPSMBackend.Models.Response;
using OPSMBackend.Repositories;
using OPSMBackend.Services.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OPSMBackend.Services.DhlRegistration
{
    public class DhlRegistrationService : IDhlRegistrationService
    {

        private readonly IRepository<HdlRegistration> _dhlRegistrationRepository;
        private readonly IRepository<RegistrationCategories> _cateogoriesRepository;
        private readonly IRepository<RegistrationTypes> _registrationTypesRepository;
        private readonly IRepository<TestTitles> _testTitlesRepository;
        private readonly IRepository<SpecializedLabRateList> _specializedLabRateListRepository;
        private readonly IRepository<MonthlyRateList> _monthlyRateListRepository;
        private readonly IRepository<ReferringRateList> _referringRateListRepository;
        private readonly IRepository<TestGroups> _testGroupsRepository;
        private readonly IUtil _util;

        public DhlRegistrationService(IRepository<HdlRegistration> dhlRegistrationRepository,
            IRepository<RegistrationCategories> cateogoriesRepository,
            IRepository<RegistrationTypes> registrationTypesRepository,
            IRepository<TestTitles> testTitlesRepository,
            IRepository<SpecializedLabRateList> specializedLabRateListRepository,
            IRepository<MonthlyRateList> monthlyRateListRepository,
            IRepository<ReferringRateList> referringRateListRepository,
            IRepository<TestGroups> testGroupsRepository,
            IUtil util)
        {
            _dhlRegistrationRepository = dhlRegistrationRepository;
            _cateogoriesRepository = cateogoriesRepository;
            _registrationTypesRepository = registrationTypesRepository;
            _testTitlesRepository = testTitlesRepository;
            _specializedLabRateListRepository = specializedLabRateListRepository;
            _monthlyRateListRepository = monthlyRateListRepository;
            _referringRateListRepository = referringRateListRepository;
            _testGroupsRepository = testGroupsRepository;
            _util = util;
        }

        public void DeleteDhlRegistration(int id)
        {
            _dhlRegistrationRepository.Delete(_dhlRegistrationRepository.Get(id));
        }

        public void DeleteRateList(int id)
        {
            var hdl = _dhlRegistrationRepository.Get(id);
            if (hdl != null)
            {
                if (hdl.RegistrationTypeId == 3)
                {
                    var specializedList = _specializedLabRateListRepository.GetAll().ToList().FindAll(x => x.HdlId == hdl.Id);
                    if (specializedList != null)
                    {
                        foreach (var item in specializedList)
                        {
                            _specializedLabRateListRepository.Delete(item);
                        }
                    }
                }
                else
                {
                    if (hdl.RegistrationCategoryId == 1)
                    {
                        var monthlyList = _monthlyRateListRepository.GetAll().ToList().FindAll(x => x.HdlId == hdl.Id);
                        if (monthlyList != null)
                        {
                            foreach (var item in monthlyList)
                            {
                                _monthlyRateListRepository.Delete(item);
                            }
                        }
                    }
                    else if (hdl.RegistrationCategoryId == 2)
                    {
                        var referredList = _referringRateListRepository.GetAll().ToList().FindAll(x => x.HdlId == hdl.Id);
                        if (referredList != null)
                        {
                            foreach (var item in referredList)
                            {
                                _referringRateListRepository.Delete(item);
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("The registration category is invalid. It can be either Monthly or referring");
                    }
                }
            }
            else
            {
                throw new Exception("This registration does not exist");
            }
        }

        public DhlRegistrationResponseModel GetDhlRegistrations()
        {
            var types = _registrationTypesRepository.GetAll().ToList();
            var categories = _cateogoriesRepository.GetAll().ToList();
            var registrations = _dhlRegistrationRepository.GetAll().ToList();

            var responseModel = new DhlRegistrationResponseModel
            {
                HdlRegistrations = registrations,
                RegistrationTypes = types,
                Categories = categories
            };

            return responseModel;
        }

        public RateListResponseModel GetRateLists()
        {
            var hdlRegistrations = _dhlRegistrationRepository.GetAll().ToList();
            var rateLists = new List<RateListModel>();
            var specializedRateList = _specializedLabRateListRepository.GetAll().ToList();
            var groups = _testGroupsRepository.GetAll().ToList();
            var testTitls = _testTitlesRepository.GetAll().ToList();
            var monthlyRateList = _monthlyRateListRepository.GetAll().ToList();
            var referredRateList = _referringRateListRepository.GetAll().ToList();
            foreach (var hdl in hdlRegistrations)
            {
                var rateListModel = new RateListModel();
                rateListModel.HdlRegistration = hdl;
                if (hdl.RegistrationTypeId == 3)
                {
                    rateListModel.SpecializedLabRateLists.AddRange(specializedRateList.FindAll(x => x.HdlId == hdl.Id));
                }
                else
                {
                    if (hdl.RegistrationCategoryId == 1)
                    {
                        rateListModel.MonthlyRateLists.AddRange(monthlyRateList.FindAll(x => x.HdlId == hdl.Id));
                    }
                    else if (hdl.RegistrationCategoryId == 2)
                    {
                        rateListModel.ReferredRateLists.AddRange(referredRateList.FindAll(x => x.HdlId == hdl.Id));
                    }
                    else
                    {
                        throw new Exception("The registration category is invalid. It can be either Monthly or referring");
                    }
                }

                rateLists.Add(rateListModel);
            }

            return new RateListResponseModel { RateListModels = rateLists, HdlRegistrations = hdlRegistrations, TestTitles = testTitls };
        }

        public void InsertDhlRegistration(HdlRegistration hdlRegistration)
        {
            _dhlRegistrationRepository.Insert(hdlRegistration);
        }

        public void InsertRateList(RateListModel rateList)
        {
            if (rateList.HdlRegistration != null)
            {
                if (rateList.HdlRegistration.RegistrationTypeId == 3)
                {
                    //Add the specialized charges rate list
                    foreach (var item in rateList.SpecializedLabRateLists.ToList())
                    {
                        item.TestTitle = null;
                        item.Hdl = null;
                        _specializedLabRateListRepository.Insert(item);
                    }
                }
                else
                {
                    if (rateList.HdlRegistration.RegistrationCategoryId == 1)
                    {
                        //Add the monthly rate list
                        foreach (var item in rateList.MonthlyRateLists.ToList())
                        {
                            item.TestTitle = null;
                            item.Hdl = null;
                            _monthlyRateListRepository.Insert(item);
                        }
                    }
                    else if (rateList.HdlRegistration.RegistrationCategoryId == 2)
                    {
                        //Add the referring rate list
                        foreach (var item in rateList.ReferredRateLists.ToList())
                        {
                            item.TestTitle = null;
                            item.Hdl = null;
                            _referringRateListRepository.Insert(item);
                        }
                    }
                    else
                    {
                        throw new Exception("The registration category is invalid. It can be either Monthly or referring");
                    }
                }
            }
            else
            {
                throw new InvalidDataException("The Doctor/Hospital/Laboratory does not exist. Please select one in order to add its rate list");
            }
        }

        public void UpdateDhlRegistration(HdlRegistration hdlRegistration)
        {
            var registrationFromDb = _dhlRegistrationRepository.Get(hdlRegistration.Id);
            if (registrationFromDb != null)
            {
                _util.CopyProperties(hdlRegistration, registrationFromDb);

                _dhlRegistrationRepository.Update(registrationFromDb);
            }
            else
            {
                throw new Exception("This registration does not exist");
            }
        }

        public void UpdateRateList(RateListModel rateList)
        {
            if (rateList.HdlRegistration != null)
            {
                if (rateList.HdlRegistration.RegistrationTypeId == 3)
                {
                    //Add the specialized charges rate list
                    foreach (var item in rateList.SpecializedLabRateLists.ToList())
                    {
                        var objFromDb = _specializedLabRateListRepository.Get(item.Id);
                        if (objFromDb != null)
                        {
                            _util.CopyProperties(item, objFromDb);
                            _specializedLabRateListRepository.Update(objFromDb);
                        }
                        else
                        {
                            Program.Logger.Warn("This specialized rate list item does not exist");
                        }
                    }
                }
                else
                {
                    if (rateList.HdlRegistration.RegistrationCategoryId == 1)
                    {
                        //Add the monthly rate list
                        foreach (var item in rateList.MonthlyRateLists.ToList())
                        {
                            var objFromDb = _monthlyRateListRepository.Get(item.Id);
                            if (objFromDb != null)
                            {
                                _util.CopyProperties(item, objFromDb);
                                _monthlyRateListRepository.Update(objFromDb);
                            }
                            else
                            {
                                Program.Logger.Warn("This monthly rate list item does not exist");
                            }
                        }
                    }
                    else if (rateList.HdlRegistration.RegistrationCategoryId == 2)
                    {
                        //Add the referring rate list
                        foreach (var item in rateList.ReferredRateLists.ToList())
                        {
                            var objFromDb = _referringRateListRepository.Get(item.Id);
                            if (objFromDb != null)
                            {
                                _util.CopyProperties(item, objFromDb);
                                _referringRateListRepository.Update(objFromDb);
                            }
                            else
                            {
                                Program.Logger.Warn("This referring rate list item does not exist");
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("The registration category is invalid. It can be either Monthly or referring");
                    }
                }
            }
            else
            {
                throw new InvalidDataException("The Doctor/Hospital/Laboratory does not exist. Please select one in order to add its rate list");
            }
        }
    }
}
