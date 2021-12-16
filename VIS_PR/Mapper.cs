namespace VIS_PR
{
    public interface  IMapper<in T>
    {
        void Insert(T item);
        void Update(T item);
        void Delete(T item);
        bool Find(int id);
        void LoadAll();

    }
}