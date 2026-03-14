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
            public const int VoterNameMinLength = 2;   //Not used after implementing user authentication, but left for potential future use if we want to allow guest voting
            public const int VoterNameMaxLength = 50;  //Not used after implementing user authentication, but left for potential future use if we want to allow guest voting
        }

        public static class SeedConstants
        {
            public const string adminUserId = "38058665-8726-41fa-be91-41de9acd0f72";
            public const string adminRoleId = "b91f0f0c-99a3-4315-9c87-6cdcc81d1a6e";
            public const string adminEmail = "admin@purrfect.com";
        }
    }
}
