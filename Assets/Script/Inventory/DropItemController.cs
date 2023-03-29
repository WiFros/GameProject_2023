using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemController : MonoBehaviour
{
   public IEnumerator DropItem(GameObject target,Transform playerTransform, int count,Slot dragSlot)
   {
      for (int i = 0; i < count; i++)
      {
         GameObject dropItem = Instantiate(target, playerTransform.position, Quaternion.identity);
         
         dragSlot.SetSlotCount(-1);
         yield return new WaitForSeconds(0.1f);
      }
   }
}
