namespace Engine.Models
{
    public class Trader : LivingEntity
    {
        public int ID { get; }

        public Trader(int id, string name) : base(name, 15, 15, 18, 15, 15, 15, 9999, 9999, 9999)
        {
            ID = id;
        }
    }
}