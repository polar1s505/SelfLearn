using TaskManager.Models;

namespace TaskManager.Interfaces
{
    public interface ICommand<T>
    {
        public T Execute(User user);
    }
}
