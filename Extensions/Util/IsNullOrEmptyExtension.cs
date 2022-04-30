namespace CarSee.Extensions.Util
{
    public static class IsNullOrEmptyExtension
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return str == null || str == "";
        }
    }
}