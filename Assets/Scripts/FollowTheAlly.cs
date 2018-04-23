using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTheAlly : MonoBehaviour
{

    public GameObject ally;

    void Update()
    {
        if (ally != null)
            transform.position = ally.transform.position;
    }
}
