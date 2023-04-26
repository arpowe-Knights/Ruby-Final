using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour

{
    void OnTriggerStay2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            controller.ChangeHealth(-1);
        }
    }
}

//IEnumerator OnTriggerExit2D(Collider2D other)
//{

//    {
 //       RubyController controller = other.GetComponent<RubyController>();

 //       while (isOnFire == true)
///{
  //          controller.ChangeHealth(-1);
 //           yield return new WaitForSeconds(2);
 //           isOnFire = false;
  //      }
  //  }
//}
//}