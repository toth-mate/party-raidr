namespace PartyRaidR.Backend.Assemblers
{
    public abstract class Assembler<TModel, TDto>
    {
        public abstract TModel ConvertToModel(TDto dto);
        public abstract TDto ConvertToDto(TModel model);
    }
}
