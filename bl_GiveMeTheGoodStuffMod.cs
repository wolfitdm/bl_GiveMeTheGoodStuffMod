using Den.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static Mono.Security.X509.X520;
using static UnityEngine.Rendering.VolumeComponent;

namespace BitchLand//must have this namespace
{
	class bl_GiveMeTheGoodStuffModDoWork 
	{
		public bl_GiveMeTheGoodStuffModDoWork() 
		{ 
		}
		
		public void OnEnable()
		{
			doWork();
		}

        public void OnDisable()
        {

        }

		public void OnStart()
		{
            doWork();
		}

        private List<GameObject> getPrefabsByName(string prefab)
        {
			List<GameObject> Prefabs = null;

            if (prefab == null)
			{
				Prefabs = Main.Instance.AllPrefabs;
				return Prefabs;
			}

            switch (prefab)
			{
				case "Any": {
						Prefabs = Main.Instance.Prefabs_Any;
				}
				break;

                case "Shoes":
                    {
                        Prefabs = Main.Instance.Prefabs_Shoes;
                    }
                    break;

                case "Pants":
                    {
                        Prefabs = Main.Instance.Prefabs_Pants;
                    }
                    break;

                case "Top":
                    {
                        Prefabs = Main.Instance.Prefabs_Top;
                    }
                    break;

                case "UnderwearTop":
                    {
                        Prefabs = Main.Instance.Prefabs_UnderwearTop;
                    }
                    break;

                case "UnderwearLower":
                    {
                        Prefabs = Main.Instance.Prefabs_UnderwearLower;
                    }
                    break;

                case "Garter":
                    {
                        Prefabs = Main.Instance.Prefabs_Garter;
                    }
                    break;

                case "Socks":
                    {
                        Prefabs = Main.Instance.Prefabs_Socks;
                    }
                    break;

                case "Hat":
                    {
                        Prefabs = Main.Instance.Prefabs_Hat;
                    }
                    break;

                case "Hair":
                    {
                        Prefabs = Main.Instance.Prefabs_Hair;
                    }
                    break;

                case "MaleHair":
                    {
                        Prefabs = Main.Instance.Prefabs_MaleHair;
                    }
                    break;

                case "Bodies":
                    {
                        Prefabs = Main.Instance.Prefabs_Bodies;
                    }
                    break;

                case "Heads":
                    {
                        Prefabs = Main.Instance.Prefabs_Heads;
                    }
                    break;

                case "Beards":
                    {
                        Prefabs = Main.Instance.Prefabs_Beards;
                    }
                    break;

                case "ProstSuit1":
                    {
                        Prefabs = Main.Instance.Prefabs_ProstSuit1;
                    }
                    break;

                case "ProstSuit2":
                    {
                        Prefabs = Main.Instance.Prefabs_ProstSuit2;
                    }
                    break;

                case "Weapons":
                    {
                        Prefabs = null;
                    }
                    break;

                default:
				{
						Prefabs = Main.Instance.AllPrefabs;
				}
				break;
			}

			return Prefabs;
        }

        private List<Weapon> getPrefabsByName2(string prefab)
        {
            List<Weapon> Prefabs = null;
            if (prefab == null)
            {
                Prefabs = Main.Instance.Prefabs_Weapons;
                return Prefabs;
            }

            switch (prefab)
            {
                case "Weapons":
                    {
                        Prefabs = Main.Instance.Prefabs_Weapons;
                    }
                    break;

                default:
                    {
                        Prefabs = Main.Instance.Prefabs_Weapons;
                    }
                    break;
            }

            return Prefabs;
        }

        private GameObject getItemByName(string prefab, string name)
		{
            List<GameObject> Prefabs = getPrefabsByName(prefab);
            if (Prefabs == null)
			{
				return null;
			}
			int length = Prefabs.Count;
			for (int i = 0; i < length; i++)
			{
				if (Prefabs[i].IsNull())
				{
					continue;
				}

				if (Prefabs[i].name == name)
				{
					return Prefabs[i];
				}
			}
			return null;
		}

        private Weapon getWeaponByName(string prefab, string name)
        {
            List<Weapon> Prefabs = getPrefabsByName2(prefab);
            if (Prefabs == null)
            {
                return null;
            }
            int length = Prefabs.Count;
            for (int i = 0; i < length; i++)
            {
                if (Prefabs[i].IsNull())
                {
                    continue;
                }

                if (Prefabs[i].name == name)
                {
                    return Prefabs[i];
                }
            }
            return null;
        }

        private Int_Storage addAllItemsByPrefab(string prefab, Int_Storage bag)
		{
            if (bag == null)
            {
                return null;
            }

            List<GameObject> Prefabs = getPrefabsByName(prefab);

            if (Prefabs == null)
            {
                return bag;
            }

            int length = Prefabs.Count;
            for (int i = 0; i < length; i++)
            {
                if (Prefabs[i].IsNull())
                {
                    continue;
                }

                GameObject item = Prefabs[i];
                
                if (bag.HasItem(item))
                {
                    continue;
                }

                bag.AddItem(item);
            }

            return bag;
        }

        private WeaponSystem addAllWeaponsByPrefab(string prefab, WeaponSystem bag)
        {
            if (bag == null)
            {
                return null;
            }

            List<Weapon> Prefabs = getPrefabsByName2(prefab);

            if (Prefabs == null)
            {
                return bag;
            }

            int length = Prefabs.Count;
            bool[] haveItems = new bool[length];
            for (int i = 0; i < length; i++)
            {
                haveItems[i] = false;
            }

            for (int i = 0; i < length; i++)
            {
                if (Prefabs[i].IsNull())
                {
                    continue;
                }

                if (haveItems[i])
                    continue;

                if (bag.CurrentWeapon != null)
                {
                    bag.DropAllWeapons();
                    i--;
                    continue;
                }

                //bag.PickupWeapon(Main.Spawn(weapon));
                Weapon item = Prefabs[i];
          //      if (!item.playerWeapon)
          //          continue;
                GameObject weapon = Main.Spawn(item.gameObject);
                //weapon.transform.position = weapon.transform.position + new Vector3(0.5f, 0.4f, 0.0f);
                bag.PickupWeapon(weapon);
                haveItems[i] = true;
            }

            return bag;
        }


        private WeaponSystem addRandomWeaponByPrefab(string prefab, WeaponSystem bag)
        {
            if (bag == null)
            {
                return null;
            }

            List<Weapon> Prefabs = getPrefabsByName2(prefab);

            if (Prefabs == null)
            {
                return bag;
            }

            int length = Prefabs.Count;
            int i = UnityEngine.Random.Range(0, length);
            Weapon item = Prefabs[i];
            GameObject weapon = Main.Spawn(item.gameObject);
            bag.DropAllWeapons();
            bag.PickupWeapon(weapon);
            return bag;
        }

        private WeaponSystem addWeaponByPrefabAndName(string prefab, string name, WeaponSystem bag)
        {
            if (bag == null)
            {
                return null;
            }

            if (name == null)
            {
                return null;
            }

            List<Weapon> Prefabs = getPrefabsByName2(prefab);

            if (Prefabs == null)
            {
                return bag;
            }

            int length = Prefabs.Count;
            for (int i = 0; i < length; i++)
            {
                if (Prefabs[i].IsNull())
                {
                    continue;
                }

                if (Prefabs[i].name != name)
                    continue;

                //bag.PickupWeapon(Main.Spawn(weapon));
                Weapon item = Prefabs[i];
                //      if (!item.playerWeapon)
                //          continue;
                GameObject weapon = Main.Spawn(item.gameObject);
                //weapon.transform.position = weapon.transform.position + new Vector3(0.5f, 0.4f, 0.0f);
                bag.DropAllWeapons();
                bag.PickupWeapon(weapon);
            }
            return bag;
        }


        private void showItemsInLogByPrefab(string prefab)
        {
            string prefabName = null;
            
            if(prefab == null) {
                prefabName = "AllPrefabs";
            } else
            {
                prefabName= prefab; 
            }

            List<GameObject> Prefabs = getPrefabsByName(prefab);
            
            if (Prefabs == null)
            {
                return;
            }

            string itemi = "-------------------------------------------------------------------" + prefabName + "-------------------------------------------------------------------";
            Main.Instance.GameplayMenu.ShowNotification(itemi);
            Debug.Log((object)itemi);

            int length = Prefabs.Count;
            for (int i = 0; i < length; i++)
            {
                if (Prefabs[i].IsNull())
                {
                    continue;
                }
                string item = prefabName + "[" + i.ToString() + "]: name: " + Prefabs[i].name;
                Main.Instance.GameplayMenu.ShowNotification(item);
                Debug.Log((object)item);

            }
        }

        private void showWeaponsInLogByPrefab(string prefab)
        {
            string prefabName = null;

            if (prefab == null)
            {
                prefabName = "Weapons";
            }
            else
            {
                prefabName = prefab;
            }

            List<Weapon> Prefabs = getPrefabsByName2(prefab);

            if (Prefabs == null)
            {
                return;
            }

            string itemi = "-------------------------------------------------------------------" + prefabName + "-------------------------------------------------------------------";
            Main.Instance.GameplayMenu.ShowNotification(itemi);
            Debug.Log((object)itemi);

            int length = Prefabs.Count;
            for (int i = 0; i < length; i++)
            {
                if (Prefabs[i].IsNull())
                {
                    continue;
                }
                string item = prefabName + "[" + i.ToString() + "]: name: " + Prefabs[i].name;
                Main.Instance.GameplayMenu.ShowNotification(item);
                Debug.Log((object)item);

            }
        }

        private void showAllItemsInLog()
        {
            showItemsInLogByPrefab("ProstSuit1");
            showItemsInLogByPrefab("ProstSuit2");
            showWeaponsInLogByPrefab("Weapons");
            showItemsInLogByPrefab(null);
            showItemsInLogByPrefab("Any");
            showItemsInLogByPrefab("Shoes");
            showItemsInLogByPrefab("Pants");
            showItemsInLogByPrefab("Top");
            showItemsInLogByPrefab("UnderwearTop");
            showItemsInLogByPrefab("UnderwearLower");
            showItemsInLogByPrefab("Garter");
            showItemsInLogByPrefab("Socks");
            showItemsInLogByPrefab("Hat");
            showItemsInLogByPrefab("Hair");
            showItemsInLogByPrefab("MaleHair");
            showItemsInLogByPrefab("Bodies");
            showItemsInLogByPrefab("Heads");
            showItemsInLogByPrefab("Beards");
        }

        public void doWork()
        {
            if (Main.Instance.AllPrefabs == null)
            {
                return;
            }

            if (Main.Instance.Player.CurrentBackpack == null)
            {
                GameObject backpack2 = getItemByName(null, "backpack2");
                if (backpack2 == null)
                {
                    backpack2 = getItemByName(null, "backpack");
                }
                if (backpack2 == null)
                {
                    return;
                }
                Main.Instance.Player.DressClothe(Main.Spawn(backpack2));
            }

            if (Main.Instance.Player.CurrentBackpack != null)
            {
                Int_Storage bag = Main.Instance.Player.CurrentBackpack.ThisStorage;
                bag = addAllItemsByPrefab(null, bag);
                bag = addAllItemsByPrefab("Prost_Suit1", bag);
                bag = addAllItemsByPrefab("Prost_Suit2", bag);
                bag = addAllItemsByPrefab("Any", bag);
                bag = addAllItemsByPrefab("Shoes", bag);
                bag = addAllItemsByPrefab("Pants", bag);
                bag = addAllItemsByPrefab("Top", bag);
                bag = addAllItemsByPrefab("UnderwearTop", bag);
                bag = addAllItemsByPrefab("UnderwearLower", bag);
                bag = addAllItemsByPrefab("Garter", bag);
                bag = addAllItemsByPrefab("Socks", bag);
                bag = addAllItemsByPrefab("Hat", bag);
                Main.Instance.Player.CurrentBackpack.ThisStorage = bag;

                WeaponSystem bag2 = Main.Instance.Player.WeaponInv;
                //bag2 = addAllWeaponsByPrefab(null, bag2);
                bag2 = addWeaponByPrefabAndName(null, "Assault Rifle 2", bag2);
                bag2 = addWeaponByPrefabAndName(null, "Pistol", bag2);
                bag2 = addWeaponByPrefabAndName(null, "M79 Grenade Launcher", bag2);
                bag2 = addWeaponByPrefabAndName(null, "Pickaxe", bag2);
                bag2 = addWeaponByPrefabAndName(null, "crowbar", bag2);
                bag2 = addWeaponByPrefabAndName(null, "Shovel", bag2);
                Main.Instance.Player.WeaponInv = bag2;
                Main.Instance.Player.Money += 100000;
            }
            //showAllItemsInLog();
            showItemsInLogByPrefab("ProstSuit1");
            showItemsInLogByPrefab("ProstSuit2");
        }


        public static bl_GiveMeTheGoodStuffModDoWork Instance = new bl_GiveMeTheGoodStuffModDoWork();
    }

    class bl_AllStatsToMaxModDoWork
    {
        public bl_AllStatsToMaxModDoWork()
        {
        }

        public void OnEnable()
        {
            doWork();
        }

        public void OnDisable()
        {

        }

        public void doWork()
        {
            string allStatsToMaxModString = "bl_AllStatsToMaxModDoWork.doWork() Set all skills to max";
            Main.Instance.GameplayMenu.ShowNotification(allStatsToMaxModString);
            Debug.Log((object)allStatsToMaxModString);
            int add = 300;
            Main.Instance.Player.SexSkills = add;
            Main.Instance.Player.WorkSkills = add;
            Main.Instance.Player.ArmySkills = add;
            Main.Instance.Player.Money += add;
            int workMax = Main.Instance.Player.WorkXpThisLvlMax;
            int sexMax = Main.Instance.Player.SexXpThisLvlMax;
            int armyMax = Main.Instance.Player.ArmyXpThisLvlMax;
            workMax = workMax >= 0 ? workMax : add;
            sexMax = sexMax >= 0 ? sexMax : add;
            armyMax = armyMax >= 0 ? armyMax : add;
            Main.Instance.Player.SexXpThisLvl = sexMax;
            Main.Instance.Player.WorkXpThisLvl = workMax;
            Main.Instance.Player.ArmyXpThisLvl = armyMax;
            Main.Instance.Player.Hunger = 75;
            Main.Instance.Player.Energy = 75;
            Main.Instance.Player.Toilet = 75;
            Main.Instance.Player.Favor = 75;
        }

        public static bl_AllStatsToMaxModDoWork Instance = new bl_AllStatsToMaxModDoWork();
    }

    public class Mod//must have this class name
	{
		public static bl_GiveMeTheGoodStuffMod ThisMod;

        public static void Init() //must have this name, and MUST be static
        {
            ThisMod = Main.Instance.gameObject.AddComponent<bl_GiveMeTheGoodStuffMod>();
        }

		public static void EnableMod(bool value)
		{
			if(value)
			{//mod was enabled in the settings
                bl_AllStatsToMaxModDoWork.Instance.OnEnable();
				bl_GiveMeTheGoodStuffModDoWork.Instance.OnEnable();
            }
			else
			{
                bl_AllStatsToMaxModDoWork.Instance.OnDisable();
				bl_GiveMeTheGoodStuffModDoWork.Instance.OnDisable();
			}
		}
	}

	public class bl_GiveMeTheGoodStuffMod : MonoBehaviour
	{
		public void Start()
		{
            bl_AllStatsToMaxModDoWork.Instance.OnEnable();
            bl_GiveMeTheGoodStuffModDoWork.Instance.OnStart();
        }

	}
}

