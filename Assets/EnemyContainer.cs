using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContainer : MonoBehaviour
{
   private Building target;
   private int targetCount;

   public float MoveSpeed = 1;

   public void TakeDamage()
   {

   }

   public void OnTriggerEnter(Collider collision)
   {
      if (collision.gameObject.tag == "Building")
      {
         Building building = collision.gameObject.GetComponentInParent<Building>();
         if (building.Level > 0)
         {
               target = building;
               targetCount++;
         }
      }
   }

   public void OnCollisionExit(Collider collision)
   {
      if (collision.gameObject.tag == "Building")
      {
         targetCount = Mathf.Max(0, targetCount - 1);
      }

      if (targetCount <= 0)
      {
         target = null;
      }
   }

   public void Update()
   {
      if (target != null && target.Level > 0)
      {
         target.TakeDamage();
      }
      else
      {
         transform.position = new Vector3(transform.position.x, 0, transform.position.z - Time.deltaTime * MoveSpeed);
      }
   }
}
