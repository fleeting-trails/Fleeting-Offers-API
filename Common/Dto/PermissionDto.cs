using FleetingOffers.Common.Enum;
using FleetingOffers.Module.User;

namespace FleetingOffers.Common.Dto;

public class PermissionModuleObjectDto : Dictionary<string, bool> {};
public class PermissionModuleDto : Dictionary<APP_MODULE, PermissionModuleObjectDto> {};
public class PermissionDto : Dictionary<USER_ROLE, PermissionModuleDto> {};