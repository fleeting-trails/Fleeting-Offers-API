using FleetingOffers.Module;

namespace FleetingOffers.Modules.File;

public record CreateUploadDto(
    string Name,
    string Location,
    UPLOAD_STORAGE_TYPE? Storage,
    string? OriginalName
);


public record UpdateUploadDto  (
    string? Name,
    string? Location,
    UPLOAD_STORAGE_TYPE? Storage,
    bool? IsUsed
);



