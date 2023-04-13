using AutoMapper;

namespace OrderService.Application.Mapping;

public static class ObjectMapper
{
    private static readonly Lazy<IMapper> _lazy = new Lazy<IMapper>(() =>
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile<CustomMapping>(); });
        return config.CreateMapper();
    });

    public static IMapper Mapper => _lazy.Value;
}