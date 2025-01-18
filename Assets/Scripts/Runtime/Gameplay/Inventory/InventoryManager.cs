namespace Runtime.Gameplay.Inventory
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        private Inventory inventory;
        public Inventory Inventory => inventory ?? new Inventory();
    }
}