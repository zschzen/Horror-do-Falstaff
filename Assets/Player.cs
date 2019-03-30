using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isSafe = true;
    public bool isLost;
    public bool isHaunted;

    public Maestria mst;

    private void Awake()
    {
        Transform mtr = transform.Find("Maestro");
        mst = mtr.GetComponent<Maestria>();
    }

    private void Start()
    {
        mst.NewSoundtrack("Audio/safe");
    }
}
