using System;


    public class Item
    {
        #region DEFINITIONS

        private int _id;
        private string _name;
        private string _description;
        private string _atlas_avatar;
        private string _item_group;
        private string _category;
        private string _subcategory;
        private int _rarity;
        private int _weight_per_unit;
        private int _base_cost;

        private bool _is_dirty;

        #endregion

        #region PROPERTIES

        public int ID { get { return _id; } set { _id = value; _is_dirty = true; } }
        public string Name { get { return _name; } set { _name = value; _is_dirty = true; } }
        public string Description { get { return _description; } set { _description = value; _is_dirty = true; } }
        public string Atlas_Avatar { get { return _atlas_avatar; } set { _atlas_avatar = value; _is_dirty = true; } }
        public string Item_Group { get { return _item_group; } set { _item_group = value; _is_dirty = true; } }
        public string Category { get { return _category; } set { _category = value; _is_dirty = true; } }
        public string Subcategory { get { return _subcategory; } set { _subcategory = value; _is_dirty = true; } }
        public int Rarity { get { return _rarity; } set { _rarity = value; _is_dirty = true; } }
        public int Weight_Per_Unit { get { return _weight_per_unit; } set { _weight_per_unit = value; _is_dirty = true; } }
        public int Base_Cost { get { return _base_cost; } set { _base_cost = value; _is_dirty = true; } }

        public bool Is_Dirty { get { return _is_dirty; } set { _is_dirty = value; } }

        #endregion

        #region CONSTRUCTORS

        public Item()
        {
            NewItem(0,string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, 0);
        }

        public Item(int ID,
            string Name,
            string Description,
            string Atlas_Avatar,
            string Item_Group,
            string Category,
            string Subcategory,
            int Rarity,
            int Weight_Per_Unit,
            int Base_Cost)
        {
            NewItem(ID, Name, Description, Atlas_Avatar, Item_Group, Category, Subcategory, Rarity, Weight_Per_Unit, Base_Cost);
        }

        #endregion

        #region PRIVATE METHODS

        private void NewItem(int ID,
            string Name,
            string Description,
            string Atlas_Avatar,
            string Item_Group,
            string Category,
            string Subcategory,
            int Rarity,
            int Weight_Per_Unit,
            int Base_Cost)
        {
            this.ID = ID;
            this.Name = Name;
            this.Description = Description;
            this.Item_Group = Item_Group;
            this.Category = Category;
            this.Subcategory = Subcategory;
            this.Rarity = Rarity;
            this.Weight_Per_Unit = Weight_Per_Unit;
            this.Base_Cost = Base_Cost;
        }

        #endregion

        #region PUBLIC METHODS

        #endregion
    }

