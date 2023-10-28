namespace Infrastructure.API.ConvertorInterfaces;

public interface IJsonConvertor<out T>
{
    public T Convert(string json);
}