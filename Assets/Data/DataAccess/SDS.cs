using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

    public class SDS
    {
        /*######################################################################################*/
        //      MDS data copy (JsonIgnore so we don't save it to the save game file)
        /*######################################################################################*/
        [JsonIgnore]
        public List<Sector> Sectors = new List<Sector>();
        [JsonIgnore]
        public List<Station> Stations = new List<Station>();
        [JsonIgnore]
        public List<Shop> Shops = new List<Shop>();
        [JsonIgnore]
        public List<Inventory> Inventories = new List<Inventory>();
        [JsonIgnore]
        public List<Item> Items = new List<Item>();
        [JsonIgnore]
        public List<MetaData> MetaData = new List<MetaData>();
        [JsonIgnore]
        public List<Jumpgate> Jumpgates = new List<Jumpgate>();
        [JsonIgnore]
        public List<MetaDataToEntity> MetaDataToEntity = new List<MetaDataToEntity>();
        //[JsonIgnore]
        //public GameStateCollection gsc = new GameStateCollection();
        /*######################################################################################*/


        /*######################################################################################*/
        //      SDS specific data
        /*######################################################################################*/
        public List<InventoryItem> InventoryItems = new List<InventoryItem>();      //Generated on new, loaded from save

        public PlayerInfo PlayerInfo = new PlayerInfo();                            //Generated on new, loaded from save
        public GameState CurrentGameState = new GameState();                        //Generated on new, loaded from save
        /*######################################################################################*/


        /// <summary>
        /// After the MDS is loaded, and before a new game is created or
        /// an existing game is loaded, transfer the MDS data to the SDS 
        /// object, and then trash the MDS object at the instantiation
        /// source (we don't need to keep it around)
        /// </summary>
        /// <param name="Source"></param>
        public void TransferFromMDS(MDS Source)
        {
            this.Sectors = Source.Sectors;
            this.Stations = Source.Stations;
            this.Shops = Source.Shops;
            this.Inventories = Source.Inventories;
            this.Items = Source.Items;
            this.MetaData = Source.MetaData;
            this.Jumpgates = Source.Jumpgates;
            this.MetaDataToEntity = Source.MetaDataToEntity;
        }

        /*######################################################################################*/
        //   List Searching
        /*######################################################################################*/

        public List<Inventory> FindByInventoryType(Enums.Entity_Type Inventory_Type)
        {
            List<Inventory> foundItem = null;

            var obj = from src in Inventories
                      where src.Entity_Type == Inventory_Type
                      select src;

            foundItem = obj.ToList();
            return foundItem;
        }

        public List<Item> FindByItemGroup(string Item_Group)
        {
            List<Item> foundItem = null;

            var obj = from src in Items
                      where src.Item_Group == Item_Group
                      select src;

            if (obj.Count() > 0) { foundItem = obj.ToList(); }
            return foundItem;
        }

        public string GetItemMetaValue(int Item_ID, string MetaDataKey)
        {
            string foundItem = null;

            var obj = (from src in MetaDataToEntity
                       where src.ID == Item_ID && src.MetaData_Key.Equals(MetaDataKey)
                       select src).First();

            foundItem = obj.Value;
            return foundItem;
        }

    }

