namespace MediCue.Services.Mapper
{
    public interface IWebMapper <TWebModel,TDto>
    {
        TDto MapToDTO(TWebModel webModel);
        TWebModel MapToWebModel(TDto dto);
    }
}
