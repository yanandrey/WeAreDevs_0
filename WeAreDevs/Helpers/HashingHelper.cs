namespace WeAreDevs.Helpers
{
    public static class HashingHelper
    {
        public static string CriarHash(string senha)
        {
            var hash = BCrypt.Net.BCrypt.EnhancedHashPassword(senha);
            return hash;
        }

        public static bool ValidarHash(string senha, string hash)
        {
            var valido = BCrypt.Net.BCrypt.EnhancedVerify(senha, hash);
            return valido;
        }
    }
}