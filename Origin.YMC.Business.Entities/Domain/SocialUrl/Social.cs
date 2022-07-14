using Origin.YMC.Business.Entities.Domain.SocialUrl.Enums;


namespace Origin.YMC.Business.Entities.Domain.SocialUrl
{
    public class Social : EntityBase
    {
        public string TitleAr { get; set; }
        public string TitleEn { get; set; }
        public string Url { set; get; }
        public SocialType Type { set; get; }
    }
}
