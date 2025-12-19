using Den.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
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

            int length = 2;
            for (int i = 0; i < length; i++)
            {
                if (Prefabs[i].IsNull())
                {
                    continue;
                }

                Weapon item = Prefabs[i];
                //bag.PickupWeapon(Main.Spawn(weapon));
                GameObject weapon = Main.Spawn(item.gameObject);
                Main.Instance.Player.WeaponInv.DropAllWeapons();
                Main.Instance.Player.WeaponInv.PickupWeapon(weapon);
                //weapon.transform.position = weapon.transform.position + new Vector3(0.0f, 3f, 0.0f);
                //bag.DropAllWeapons();
                //bag.PickupWeapon(weapon);
                //bag.startingWeaponIndex = 0;
                //bag.DropAllWeapons();
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
                bag2 = addAllWeaponsByPrefab(null, bag2);
                Main.Instance.Player.WeaponInv = bag2;
                Main.Instance.Player.Favor = 100000;
                Main.Instance.Player.CantBeHit = true;
                Main.Instance.Player.Fertility = 1f;
                Main.Instance.Player.StoryModeFertility = 1f;
                Main.Instance.Player.Energy = 100f;
                Main.Instance.Player.Hunger = 100f;
                Main.Instance.Player.NoEnergyLoss = true;
                Main.Instance.Player.Money += 10000;
            }
            //showAllItemsInLog();
        }


        public static bl_GiveMeTheGoodStuffModDoWork Instance = new bl_GiveMeTheGoodStuffModDoWork();
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
				bl_GiveMeTheGoodStuffModDoWork.Instance.OnEnable();
            }
			else
			{
				bl_GiveMeTheGoodStuffModDoWork.Instance.OnDisable();
			}
		}
	}

	public class bl_GiveMeTheGoodStuffMod : MonoBehaviour
	{
		public void Start()
		{
            bl_GiveMeTheGoodStuffModDoWork.Instance.OnStart();
        }

	}
}

