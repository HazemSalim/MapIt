
namespace MapIt.Lib
{
    public static class AppEnums
    {
        public enum AdminPages
        {
            AdminUsers = 1,
            Brokers,
            CommAds,
            ContentPages,
            Countries,
            Currencies,
            GeneralSettings,
            GenNotifs,
            Newsletter,
            Offers,
            Packages,
            PaymentMethods,
            ProPages,
            Properties,
            PropertiesSettings,
            Requests,
            SerPages,
            Services,
            Sliders,
            Users
        }

        public enum AdPlaces
        {
            PropertiesList = 1,
            ServicesCategories,
            ServicesList,
        }

        public enum ContentPages
        {
            AboutUs = 1,
            FAQ,
            TermsCond,
            Policy,
            TechSupport
        }

        public enum PagePlaces
        {
            WebsiteHeader = 1,
            WebsiteFooter,
            MobMenu,
            MobApp
        }

        public enum PaymentMethods
        {
            Free = 1,
            MyFatoorah,
            Cash,
            Other2
        }

        public enum PaymentStatus
        {
            NotPaid = 1,
            Paid
        }

        public enum NotifTypes
        {
            General = 1,
            Offer,
            Alert,
            Property,
            Service,
            Message
        }
    }
}
