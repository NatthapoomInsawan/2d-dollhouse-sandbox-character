
namespace DollhouseCharacter.Interfaces
{
    public interface IEventListener<T>
    {
        void OnEventRaised(T value);
    }
}
