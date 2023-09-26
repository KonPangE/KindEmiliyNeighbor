using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [System.Serializable]
    public class RandomItem
    {
        public GameObject Prefab;
       
        [Range(0f, 100f)] public float Value;
       
        [HideInInspector] public double itemweight;
    }
    public class ItemSpawn : MonoBehaviour
    {
        public RandomItem[] items;
        public double accumulatedwight; 
        public System.Random random = new System.Random();
        public GameObject itemspawnpoint;


        private void Awake()
        {
            CalculateWeight();
        }


    public void SpawnItem()
    {
            RandomItem randomItem = items[GetRandomIndex()];
            GameObject item = Instantiate(randomItem.Prefab,itemspawnpoint.transform.position, randomItem.Prefab.transform.rotation);
           

    }

       
        private int GetRandomIndex()
        {
            double r = random.NextDouble() * accumulatedwight;

            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].itemweight >= r)
                    return i;
            }
            return 0;
        }

        
        void CalculateWeight()
        {
            accumulatedwight = 0f;
            foreach (RandomItem item in items)
            {
                accumulatedwight += item.Value;
                item.itemweight = accumulatedwight;
            }
        }



    }


