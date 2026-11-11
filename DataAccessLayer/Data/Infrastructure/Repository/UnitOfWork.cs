using Gharshare.DataAccessLayer;
using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.DataAccessLayer.Data.Infrastructure.Repository
{
    public class UnitOfWork : IUnitofWork
    {
        public IPropertyRepository Property { get; private set; }
        public IPersonListingRepository PersonListing { get; private set; }
        public IPersonRepository Person { get; private set; }
        public IEmploymentRepository Employment { get; private set; }
        public IListingEmploymentRepository ListingEmployment { get; private set; }
        public ILifestyleRepository Lifestyle { get; private set; }
        public IListingLifestyleRepository ListingLifestyle { get; private set; }
        public IListingPropertyTypeRepository ListingPropertyType { get; private set; }
        public IFurnishingRepository Furnishing { get; private set; }
        public IRoomFurnishingRepository RoomFurnishing { get; private set; }
        public IEmailTemplateTypeRepository EmailTemplateType { get; private set; }
        public IEmailTemplateRepository EmailTemplate { get; private set; }
        public IShortlistRepository Shortlist { get; private set; }
        public IPropertyTypeRepository PropertyType { get; private set; }
        public IUserNotificationRepository UserNotification { get; private set; }
        public IUserNotificationTypeRepository UserNotificationType { get; private set; }
        public IPropertyImagesRepository PropertyImages { get; private set; }
        public IPlanRepository Plan { get; private set; }
        public IPlanServiceRepository PlanService { get; private set; }
        public IServiceRepository Service { get; private set; }
        public IUserPlanRepository UserPlan { get; private set; }
        public IUserPaymentRepository UserPayment { get; private set; }
        public ISitesettingRepository Sitesetting { get; private set; }
        public ITestimonialRepository Testimonial { get; private set; }
        public IMessageRepository Message { get; private set; }
        public IUserRepository User { get; private set; }
        public IMeetingRepository Meeting { get; private set; }
        public IStateRepository State { get; private set; }
        public ICityRepository City { get; private set; }
        public ISuburbRepository Suburb { get; private set; }
        public IRoomRepository Room { get; private set; }
        public IStaticContentRepository StaticContent { get; private set; }         
        public IEnquiryRepository Enquiry { get; private set; }
        public IUserBankAccountRepository UserBankAccount { get; private set; }
        public IFeatureRepository Feature { get; private set; }
        public IPropertyFeatureRepository PropertyFeature { get; private set; }
        public IAcceptingRepository Accepting { get; private set; }
        public IPropertyAcceptingRepository PropertyAccepting { get; private set; }
        public IPropertiesViewedRepository PropertiesViewed { get; private set; }
        public IErrorLogRepository ErrorLog { get; }
        public IDemoRequestRepository DemoRequest { get; }
        public IPropertyInspectionRepository PropertyInspection { get; private set; }

        private readonly ApplicationDBContext _dbContext;

        public UnitOfWork(ApplicationDBContext context)
        {
            _dbContext = context;
            Property = new PropertyRepository(context);
            PersonListing=new PersonListingRepository(context);
            Person = new PersonRepository(context);
            Employment = new EmploymentRepository(context);
            ListingEmployment = new ListingEmploymentRepository(context);            
            Lifestyle = new LifestyleRepository(context);
            ListingLifestyle = new ListingLifestyleRepository(context);
            ListingPropertyType = new ListingPropertyTypeRepository(context);
            Shortlist = new ShortlistRepository(context);
            Furnishing = new FurnishingRepository(context);
            RoomFurnishing = new RoomFurnishingRepository(context);
            EmailTemplateType=new EmailTemplateTypeRepository(context);
            EmailTemplate = new EmailTemplateRepository(context);
            PropertyType = new PropertyTypeRepository(context);             
            PropertyImages = new PropertyImagesRepository(context);
            PropertyInspection = new PropertyInspectionRepository(context);
            Plan = new PlanRepository(context);
            Service = new ServiceRepository(context);
            PlanService = new PlanServiceRepository(context);
            UserPlan = new UserPlanRepository(context);            
            UserPayment = new UserPaymentRepository(context);
            Sitesetting = new SitesettingRepository(context);
            Testimonial = new TestimonialRepository(context);
            Message = new MessageRepository(context);
            User = new UserRepository(context);
            Meeting = new MeetingRepository(context);
            State = new StateRepository(context);
            City = new CityRepository(context);
            Suburb = new SuburbRepository(context);
            Room = new RoomRepository(context);
            StaticContent = new StaticContentRepository(context);             
            Enquiry = new EnquiryRepository(context);
            UserBankAccount = new UserBankAccountRepository(context);
            Feature = new FeatureRepository(context);
            PropertyFeature = new PropertyFeatureRepository(context);
            Accepting = new AcceptingRepository(context);
            PropertyAccepting = new PropertyAcceptingRepository(context);
            PropertiesViewed = new PropertiesViewedRepository(context);
            ErrorLog = new ErrorLogRepository(context);
            DemoRequest = new DemoRequestRepository(context);
            UserNotificationType = new UserNotificationTypeRepository(context);
            UserNotification = new UserNotificationRepository(context);
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
