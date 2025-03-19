using System.Threading.Tasks;
using AutoMapper;
using FleetingOffers.Attributes;
using FleetingOffers.Settings;

namespace FleetingOffers.Module.Upload;

[ScopedService]
public class UploadService
{
    private readonly Dictionary<UPLOAD_STORAGE_TYPE, Func<IEnumerable<IFormFile>, Task<List<UploadDto>>>> UploadWorkerMapping = new() {
        { UPLOAD_STORAGE_TYPE.LOCAL, SaveToLocalAsync }
    };
    private readonly IMapper _mapper;
    private readonly AppDbContext _dbContext;
    public UploadService(
        IMapper mapper,
        AppDbContext dbContext
    )
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }
    public async Task<List<UploadDto>> UploadFilesAsync(IEnumerable<IFormFile> files)
    {
        if (!UploadWorkerMapping.ContainsKey(UploadSettings.StorageType))
        {
            throw new Exception("Storage type not supported");
        }
        var dtos = await UploadWorkerMapping[UploadSettings.StorageType](files);
        foreach (var dto in dtos)
        {
            // Save the file to the database
            _dbContext.Uploads.Add(_mapper.Map<UploadEntity>(dto));
        }
        _dbContext.SaveChanges();
        return dtos;
    }

    public static async Task<List<UploadDto>> SaveToLocalAsync(IEnumerable<IFormFile> files)
    {
        List<UploadDto> fileEntries = new();
        List<Task> copyTasks = new(); // ✅ Move outside the loop to ensure all tasks are awaited

        foreach (var formFile in files)
        {
            if (formFile.Length > 0)
            {
                string fileName = $"{Path.GetRandomFileName()}{Path.GetExtension(formFile.FileName)}";
                var filePath = Path.Combine(UploadSettings.StoragePath, fileName);

                UploadDto fileDto = new UploadDto() { 
                    Name = fileName, 
                    URL = filePath, 
                    Storage = UPLOAD_STORAGE_TYPE.LOCAL, 
                    OriginalName = formFile.FileName 
                };
                fileEntries.Add(fileDto);

                // ✅ Use "await using" to keep stream open until CopyToAsync completes
                copyTasks.Add(Task.Run(async () =>
                {
                    await using var stream = File.Create(filePath);
                    await formFile.CopyToAsync(stream);
                }));
            }
        }

        await Task.WhenAll(copyTasks); // ✅ Ensure all files are written before returning
        return fileEntries;
    }

    #region Helpers

    public static Boolean IsAllUploadedFileExtensionsValid(IEnumerable<IFormFile> files)
    {
        Boolean isValid = true;
        foreach (var file in files)
        {
            if (!UploadSettings.AllowedExtensions.Contains(System.IO.Path.GetExtension(file.FileName)))
            {
                isValid = false;
            }
        }
        return isValid;
    }
    #endregion
}