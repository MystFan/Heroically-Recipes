namespace HeroicallyRecipes.Common.Providers
{
    using System;
    using System.Text;

    public class IdentifierProvider : IIdentifierProvider
    {
        private string Salt = ".12312313123";

        public IdentifierProvider(string salt = ".12312313123")
        {
            this.Salt = salt;
        }

        public int DecodeId(string urlId)
        {
            if(urlId.Length != 20)
            {
                return -1;
            }

            var base64EncodedBytes = Convert.FromBase64String(urlId);
            var bytesAsString = Encoding.UTF8.GetString(base64EncodedBytes);
            bytesAsString = bytesAsString.Replace(Salt, string.Empty);
            return int.Parse(bytesAsString);
        }

        public string EncodeId(int id)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(id + Salt);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}
