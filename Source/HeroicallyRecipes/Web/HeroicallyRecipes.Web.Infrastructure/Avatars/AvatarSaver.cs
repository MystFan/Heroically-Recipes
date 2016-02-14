namespace HeroicallyRecipes.Web.Infrastructure.Avatars
{
    using System;
    using System.IO;
    using System.Web;
    using System.Web.Hosting;

    public class AvatarSaver
    {
        public string SaveAvatar(HttpPostedFileBase avatarFile, string username)
        { 
            var filename = avatarFile.FileName;
            var filePathOriginal = HostingEnvironment.MapPath("/images/Avatars");

            Guid salt = Guid.NewGuid();
            string folder = username + salt.ToString().Substring(0, 10);
            string pathToSave = HostingEnvironment.MapPath("/images/Avatars/" + folder + "/");

            var directory = new DirectoryInfo(pathToSave);
            if (directory.Exists == false)
            {
                directory.Create();
            }

            string savedFileName = Path.Combine(pathToSave, filename);
            
            avatarFile.SaveAs(savedFileName);
            return "/images/Avatars/" + folder + "/" + filename;
        }
    }
}
