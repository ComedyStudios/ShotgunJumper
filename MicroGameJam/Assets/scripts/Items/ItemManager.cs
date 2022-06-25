using System;
using System.Collections;
using System.Collections.Generic;
using GameMechanics;
using MiniGameJam;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class ItemManager: MonoBehaviour
{
    public List<DropRates> monsterItems = new List<DropRates>();
    public List<DropRates> chestItems = new List<DropRates>();
    public GameObject itemPrefab;
    public static ItemManager instance; 
    private void Awake()
    {
        instance = this;
        List<DropRates> sortedArray = new List<DropRates>();
        if (monsterItems.Count > 1)
        {
            monsterItems = SortArray(monsterItems, 0, monsterItems.Count-1);
        }
    }

    //Drops Random Items at specified location 
    public void DropRandomItem(Vector3 position, List<DropRates> items)
    {
        float sum = 0; 
        foreach (var item in items)
        {
            sum += item.weight;
        }

        
        float randomNumber = Random.Range(0, sum);
        for (int i = items.Count-1; i>=0 ; i--)
        {
            if (randomNumber <= items[i].weight)
            {
                var obj = Instantiate(itemPrefab, position, Quaternion.identity);
                obj.GetComponent<ItemPickup>().item = items[i].item;
                break;
            }
            randomNumber -= items[i].weight;
        }
    }

    public void DropSetItem(Item item, Vector3 position)
    {
        var obj = Instantiate(itemPrefab, position, Quaternion.identity);
        obj.GetComponent<ItemPickup>().item = item;
    }

    public List<DropRates> SortArray(List<DropRates> array, int leftIndex, int rightIndex)
    {
        var i = leftIndex;
        var j = rightIndex;
        var pivot = array[leftIndex].weight;
        
        while (i <= j)
        {
            while (array[i].weight < pivot)
            {
                i++;
            }
        
            while (array[j].weight > pivot)
            {
                j--;
            }

            if (i <= j)
            {
                (array[i], array[j]) = (array[j], array[i]);
                i++;
                j--;
            }
        }
        if (leftIndex < j)
            SortArray(array, leftIndex, j);
        
        if (i < rightIndex)
            SortArray(array, i, rightIndex);

        return array;
    }
    
    public IEnumerator IncreaseSpeed(float speedIncrease, float increaseDuration)
    {
        PlayerMovement.playerInstance.speed *= speedIncrease + 1;
        yield return new WaitForSeconds(increaseDuration);
        PlayerMovement.playerInstance.speed /= speedIncrease + 1;

    }
    public IEnumerator IncreaseDamage(float damageIncrease, float increaseDuration)
    {
        Debug.Log(AttackScript.Instance.currentWeapon.damage);
        AttackScript.Instance.currentWeapon.damage *= damageIncrease + 1;
        Debug.Log(AttackScript.Instance.currentWeapon.damage);
        yield return new WaitForSeconds(increaseDuration);
        AttackScript.Instance.currentWeapon.damage /= damageIncrease + 1;
        Debug.Log(AttackScript.Instance.currentWeapon.damage);
    }
}
