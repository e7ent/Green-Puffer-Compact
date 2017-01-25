namespace GreenPuffer.Accounts
{
    class Users
    {
        private static User localUser;
        public static User LocalUser
        {
            get
            {
                if (localUser == null)
                {
                    localUser = new User();
                }
                return localUser;
            }
        }
    }
}
