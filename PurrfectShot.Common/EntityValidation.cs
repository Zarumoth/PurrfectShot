namespace PurrfectShot.Web.Common
{
    public static class EntityValidation
    {
        public static class Cat
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 100;
            public const int BreedMinLength = 2;
            public const int BreedMaxLength = 100;
            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 1000;
        }

        public static class Photo
        {
            public const int CaptionMinLength = 10;
            public const int CaptionMaxLength = 1000;
            public const int FilePathMaxLength = 500;
        }

        public static class Vote
        {
            public const int MinStarVoteValue = 1;
            public const int MaxStarVoteValue = 5;
            public const int VoterNameMinLength = 2;
            public const int VoterNameMaxLength = 50;
        }
    }
}
