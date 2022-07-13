namespace LibraryManagement.Helpers
{
    public static class ImageHelper
    {
        public static string UPLOAD_FOLDER = "img/books";
        public static string NOTFOUND_NAME = "notfound.png";
        public static void UploadImage<T>(ref T entity, IWebHostEnvironment env, bool deleteOldImage = true) where T : IImageContainable
        {
            // If the formfile is not added, set it to notfound.
            if (entity.FormFile == null)
            {
                entity.ImagePath = ImageHelper.NOTFOUND_NAME;
                return;
            }

            // Remove the image of the entity before adding a new one.
            if (deleteOldImage)
            {
                ImageHelper.RemoveImage(ref entity, env);
            }

            // Set folder and file names.
            string uploadFolder = Path.Combine(env.WebRootPath, ImageHelper.UPLOAD_FOLDER);
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + entity.FormFile.FileName;
            string fp = Path.Combine(uploadFolder, uniqueFileName);

            // Copy the formfile contents to the filepath.
            using (var stream = new FileStream(fp, FileMode.Create))
            {
                entity.FormFile.CopyTo(stream);
            }

            // Set the imagepath to the new unique name.
            entity.ImagePath = uniqueFileName;
        }

        public static void RemoveImage<T> (ref T entity, IWebHostEnvironment env) where T : IImageContainable
        {
            // Don't delete the notfound image.
            if (entity.ImagePath?.ToLower() == ImageHelper.NOTFOUND_NAME)
            {
                return;
            }

            string folder = Path.Combine(env.WebRootPath, ImageHelper.UPLOAD_FOLDER);
            string fp = Path.Combine(folder, entity.ImagePath);

            // Delete the image if it exists.
            if (File.Exists(fp))
            {
                File.Delete(fp);
            }

            entity.ImagePath = ImageHelper.NOTFOUND_NAME;
        }

    }

    public interface IImageContainable
    {
        public string ImagePath { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
