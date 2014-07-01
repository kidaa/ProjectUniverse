using System;

    public class InventoryItem
    {
         #region DEFINITIONS

        private int _id;
        private int _inventory_id;
        private int _item_id;
        private int _quantity;
        private string _buy_sell;

        private bool _is_dirty;

        #endregion

        #region PROPERTIES

        public int ID { get { return _id; } set { _id = value; _is_dirty = true; } }
        public int Inventory_ID { get { return _inventory_id; } set { _inventory_id = value; _is_dirty = true; } }
        public int Item_ID { get { return _item_id; } set { _item_id = value; _is_dirty = true; } }
        public int Quantity { get { return _quantity; } set { _quantity = value; _is_dirty = true; } }
        public string Buy_Sell { get { return _buy_sell; } set { _buy_sell = value; _is_dirty = true; } }

        public bool Is_Dirty { get { return _is_dirty; } set { _is_dirty = value; } }

        #endregion

        #region CONSTRUCTORS

        public InventoryItem()
        {
            NewInventoryItem(0, 0, 0, 0, string.Empty);
        }

        public InventoryItem(int ID,
            int Inventory_ID,
            int Item_ID,
            int Quantity,
            string Buy_Sell)
        {
            NewInventoryItem(ID, Inventory_ID, Item_ID, Quantity, Buy_Sell);
        }

        #endregion

        #region PRIVATE METHODS

        private void NewInventoryItem(int ID,
            int Inventory_ID,
            int Item_ID,
            int Quantity,
            string Buy_Sell)
        {
            this.ID = ID;
            this.Inventory_ID = Inventory_ID;
            this.Item_ID = Item_ID;
            this.Quantity = Quantity;
            this.Buy_Sell = Buy_Sell;
        }

        #endregion

        #region PUBLIC METHODS

        #endregion
    }

