using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class SafeEntrance : MonoBehaviour
{
    private Hunter hunter;

    private void Awake()
    {
        hunter = FindObjectOfType<Hunter>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player ply = other.GetComponent<Player>();
            ply.isSafe = !ply.isSafe;
            ply.isLost = !ply.isSafe;

            if (ply.isSafe)
            {
                ply.mst.NewSoundtrack("Audio/safe");
                hunter.cState = "Strolling";
                hunter.aud[1].Stop();

            } else if (ply.isLost)
            {
                ply.mst.NewSoundtrack("Audio/lost");
                hunter.aud[0].Play();
            }

        }
    }
}
