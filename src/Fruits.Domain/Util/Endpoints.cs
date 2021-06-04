namespace Fruits.Domain.Util
{
    public static class Endpoints
    {
        public static class Route
        {
            public const string POST = "";
            public const string PUT = "";
            public const string ALL = "";
            public const string DELETE = "{id}";
            public const string LIST = "list";

            public const string ID = "{id}";
            public const string NAME = "name/{name}";

            public const string LOGIN = "login";
            public const string REGISTER = "register";
        }

        public static class Recursos
        {
            public const string Account = "api/account";
            public const string Authentication = "api/authentication";
            public const string Fruit = "api/fruit";
            public const string Store = "api/store";
        }
    }
}
