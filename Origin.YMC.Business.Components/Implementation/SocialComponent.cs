using Origin.YMC.Business.Components.Interfaces;
using Origin.YMC.Business.Entities.Domain.SocialUrl;
using Origin.YMC.Business.Entities.Domain.SocialUrl.ViewModels;
using Origin.YMC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Origin.YMC.Business.Components.Implementation
{
    public class SocialComponent : ISocialComponent
    {
        private readonly IRepository<Social> _socialRepository;
        private readonly ILogComponent _logComponent;

        public SocialComponent(
            IRepository<Social> socialRepository,
            ILogComponent logComponent)
        {
            _socialRepository = socialRepository;
            _logComponent = logComponent;

        }

        public void Add(Social model)
        {
            _socialRepository.Add(model);
            _socialRepository.UnitOfWork.SaveChanges();
        }

        public void Create(SocialViewModels model)
        {
            try
            {

                var social = new Social
                {
                    TitleAr = model.TitleAr,
                    TitleEn = model.TitleEn,
                    Url = model.Url,
                    Type = model.Type,
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    LastUpdatedAt = DateTime.Now,
                    CreatedBy = Guid.NewGuid(),
                    LastUpdatedBy = Guid.NewGuid()
                };
                Add(social);

            }
            catch (Exception ex)
            {

                _logComponent.LogError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw;
            }
            finally
            {


            }
        }

        public List<SocialViewModels> GetList(string query)
        {
            return _socialRepository.Query()
                 .Where(x => x.IsActive)
                 .Where(x => string.IsNullOrEmpty(query))
                 .Select(c => new SocialViewModels
                 {
                     Id = c.Id,
                     TitleAr = c.TitleAr,
                     TitleEn = c.TitleEn,
                     Url = c.Url,
                     Type = c.Type,
                     CreatedBy = c.CreatedBy,
                     CreationDate = c.CreationDate,
                     IsActive = c.IsActive,
                     LastUpdatedAt = c.LastUpdatedAt,
                     LastUpdatedBy = c.LastUpdatedBy
                 }).ToList();
        }

        public SocialViewModels Get(Guid id)
        {
            var md = _socialRepository.Query().FirstOrDefault(c => c.Id == id);
            return new SocialViewModels
            {
                Id = md.Id,
                TitleAr = md.TitleAr,
                TitleEn = md.TitleEn,
                Url = md.Url,
                Type = md.Type,
                CreatedBy = md.CreatedBy,
                CreationDate = md.CreationDate,
                IsActive = md.IsActive,
                LastUpdatedAt = md.LastUpdatedAt,
                LastUpdatedBy = md.LastUpdatedBy
            };
        }

        public ListSocialViewModels GetPageData(string searchValue, int requestStart, int requestLenght, string orderColumsName, int orderColumsDirect)
        {
            var socials = _socialRepository.Query();

            var filteredData = String.IsNullOrWhiteSpace(searchValue) ?
                                   socials :
                                    socials.Where(x =>
                                       x.TitleAr.ToLower().Contains(searchValue)
                                    || x.TitleEn.ToLower().Contains(searchValue)
                                    || x.Url.ToLower().Contains(searchValue));

            switch (orderColumsName.ToLower())
            {
                case "TitleAr":
                    filteredData = orderColumsDirect == 0 ? filteredData.OrderBy(c => c.TitleAr) : filteredData.OrderByDescending(c => c.TitleAr);
                    break;
                case "TitleEn":
                    filteredData = orderColumsDirect == 0 ? filteredData.OrderBy(c => c.TitleEn) : filteredData.OrderByDescending(c => c.TitleEn);
                    break;
                case "Url":
                    filteredData = orderColumsDirect == 0 ? filteredData.OrderBy(c => c.Url) : filteredData.OrderByDescending(c => c.Url);
                    break;
                default:
                    filteredData = orderColumsDirect == 0 ? filteredData.OrderByDescending(c => c.CreationDate) : filteredData.OrderBy(c => c.CreationDate);
                    break;
            }

            var dataPage = filteredData.Skip(requestStart).Take(requestLenght);

            var viewModel = new ListSocialViewModels();
            viewModel.TotalDatalenght = socials.Count();
            viewModel.FilterDatalenght = filteredData.Count();
            viewModel.Items = dataPage.Select(c => new SocialViewModels
            {
                Id = c.Id,
                TitleAr = c.TitleAr,
                TitleEn = c.TitleEn,
                Url = c.Url,
                Type = c.Type,
                CreatedBy = c.CreatedBy,
                CreationDate = c.CreationDate,
                IsActive = c.IsActive,
                LastUpdatedAt = c.LastUpdatedAt,
                LastUpdatedBy = c.LastUpdatedBy
            }).ToList();
            return viewModel;
        }

        public void Update(SocialViewModels social)
        {
            //Edit
            var oldSocial = _socialRepository.Get(social.Id);

            oldSocial.TitleAr = social.TitleAr;
            oldSocial.TitleEn = social.TitleEn;
            oldSocial.Url = social.Url;
            oldSocial.Type = social.Type;
            oldSocial.LastUpdatedAt = DateTime.Now;


            _socialRepository.Update(oldSocial);
            _socialRepository.UnitOfWork.SaveChanges();
        }

        public void Deactive_Activate(Guid id)
        {
            //Edit
            var oldSocial = _socialRepository.Get(id);
            if (oldSocial.IsActive)
                oldSocial.IsActive = false;
            else
                oldSocial.IsActive = true;

            _socialRepository.Update(oldSocial);
            _socialRepository.UnitOfWork.SaveChanges();
        }
    }
}
