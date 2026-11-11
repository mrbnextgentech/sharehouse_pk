using Gharshare.DataAccessLayer.Data.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.DataAccessLayer.Data.Infrastructure.IRepository
{
    public interface IUnitofWork
    {
        IPropertyRepository Property { get; }
        IPersonListingRepository PersonListing { get; }
        IPersonRepository Person { get; }
        IEmploymentRepository Employment { get; }
        IListingEmploymentRepository ListingEmployment { get; }
        ILifestyleRepository Lifestyle { get; }
        IListingLifestyleRepository ListingLifestyle { get; }
        IListingPropertyTypeRepository ListingPropertyType { get; }
        IFurnishingRepository Furnishing { get; }
        IRoomFurnishingRepository RoomFurnishing { get; }
        IEmailTemplateTypeRepository EmailTemplateType { get; }
        IEmailTemplateRepository EmailTemplate { get; }
        IShortlistRepository Shortlist { get; }        
        IPropertyTypeRepository PropertyType { get; }
        IUserNotificationRepository UserNotification { get; }
        IUserNotificationTypeRepository UserNotificationType { get; }
        IPropertyImagesRepository PropertyImages { get; }
        IPlanRepository Plan { get; }
        IServiceRepository Service { get; }
        IPlanServiceRepository PlanService { get; }
        IUserPlanRepository UserPlan { get; }
        IUserPaymentRepository UserPayment { get; }
        ISitesettingRepository Sitesetting { get; }
        ITestimonialRepository Testimonial { get; }
        IMessageRepository Message { get; }
        IUserRepository User { get; }
        IMeetingRepository Meeting { get; }
        IStateRepository State { get; }
        ICityRepository City { get; }
        ISuburbRepository Suburb { get; }
        IRoomRepository Room { get; }
        IStaticContentRepository StaticContent { get; }         
        IPropertyInspectionRepository PropertyInspection { get; }         
        IEnquiryRepository Enquiry { get; }
        IUserBankAccountRepository UserBankAccount { get; }
        IPropertyFeatureRepository PropertyFeature { get; }
        IFeatureRepository Feature { get; }
        IAcceptingRepository Accepting { get; }
        IPropertyAcceptingRepository PropertyAccepting { get; }
        IPropertiesViewedRepository PropertiesViewed { get; }         
        IErrorLogRepository ErrorLog { get; }
        IDemoRequestRepository DemoRequest { get; }        
        void Save();
    }
}
