using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class Recahed : MonoBehaviour
    {
        public int Index;
        private Hunter hunter;

        private void Awake()
        {
            hunter = FindObjectOfType<Hunter>().GetComponent<Hunter>();
        }

        int Generate()
        {
           return Random.Range(0, hunter.moveSpots.Length);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Hunter") && hunter.cState == "Strolling")
            {
                int n;
                do{
                    n = Generate();
                } while (n == Index);
                hunter.randomSpot = n;
            }
        }
    }
}