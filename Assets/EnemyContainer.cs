using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContainer : MonoBehaviour
{
   private Building target;
   private int targetCount;

   public float MoveSpeed = 1;

   public float Life = 1;

   public void TakeDamage()
   {
      Life -= 0.1f;

      if (Life <= 0)
      {
         Die();
      }
   }

   private void Die()
   {
      EnemyManager.Instance.Die(this);
   }

   public void OnTriggerEnter(Collider collision)
   {
      if (collision.gameObject.tag == "Arrow")
      {
         TakeDamage();
      }

      if (collision.gameObject.tag == "GameOver")
      {
         Debug.LogError("GameOver!");
      }

      if (collision.gameObject.tag == "EnvironmentObject")
      {
         Building building = collision.gameObject.GetComponentInParent<Building>();
         if (building != null)
         {
            if (building.Level > 0)
            {
               target = building;
               targetCount++;
            }
         }

      }
   }

   public void OnTriggerExit(Collider collision)
   {
      if (collision.gameObject.tag == "EnvironmentObject")
      {
         Building building = collision.gameObject.GetComponentInParent<Building>();
         if (building != null)
         {
            targetCount = Mathf.Max(0, targetCount - 1);
         }
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
