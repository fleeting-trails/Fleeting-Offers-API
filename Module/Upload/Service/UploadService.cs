using System.Threading.Tasks;
using FleetingOffers.Attributes;
using FleetingOffers.Modules.File;
using FleetingOffers.Settings;

namespace FleetingOffers.Module.Upload;

[ScopedService]
class UploadService {
    private readonly Dictionary<UPLOAD_STORAGE_TYPE, Func<IEnumerable<IFormFile>, Task<List<CreateUploadDto>>>> UploadWorkerMapping = new() {
        { UPLOAD_STORAGE_TYPE.LOCAL, SaveToLocalAsync }
    };
    public bool UploadFiles (IEnumerable<IFormFile> files) {
        if (!UploadWorkerMapping.ContainsKey(UploadSettings.StorageType)) {
            throw new Exception("Storage type not supported");
        }
        UploadWorkerMapping[UploadSettings.StorageType](files);
        return false;
    }

    public static async Task<List<CreateUploadDto>> SaveToLocalAsync(IEnumerable<IFormFile> files)
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

                List<Task> copyTasks = new();
                using (var stream = System.IO.File.Create(filePath))
                {
                    copyTasks.Add(formFile.CopyToAsync(stream));
                }

                await Task.WhenAll(copyTasks);
            }
        }
        return FileEntries;
    }
}