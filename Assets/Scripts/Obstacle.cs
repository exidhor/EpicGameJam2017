using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace EpicGameJam
{  
    [RequireComponent(typeof(Collider2D))]
    public class Obstacle : Movable
    {
        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag == "Player")
            {
                Debug.Log("End game");
            }

            else if (collider.tag == "EndLevel")
            {
                Destroy(this);
            }
        }
    }
}
