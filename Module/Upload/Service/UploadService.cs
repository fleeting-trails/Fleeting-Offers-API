using FleetingOffers.Attributes;
using FleetingOffers.Modules.File;
using FleetingOffers.Settings;

namespace FleetingOffers.Module.Upload;

[ScopedService]
class UploadService {
    public bool UploadFiles () {
        return false;
    }

    public static List<CreateUploadDto> SaveToLocal(IEnumerable<IFormFile> files)
    {
        List<CreateUploadDto> FileEntries = [];
        foreach (var formFile in files)
        {
            if (formFile.Length > 0)
            {
                string fileName = $"{System.IO.Path.GetRandomFileName()}{System.IO.Path.GetExtension(formFile.FileName)}";
                var filePath = System.IO.Path.Combine(UploadSettings.StoragePath, fileName);

                CreateUploadDto fileDto = new CreateUploadDto(
                    Name: fileName, 
                    Location: filePath, 
                    Storage: UploadSettings.StorageType,
                    OriginalName: formFile.FileName
                );
                FileEntries.Add(fileDto);

                using (var stream = System.IO.File.Create(filePath))
                {
                    formFile.CopyTo(stream);
                }
            }
        }
        return FileEntries;
    }
}