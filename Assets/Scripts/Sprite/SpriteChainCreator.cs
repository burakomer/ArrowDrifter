using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChainCreator : MonoBehaviour
{
    private void Start()
    {
        SpriteJoint[] joints = GetComponentsInChildren<SpriteJoint>();

        for (int i = 0; i < joints.Length; i++)
        {
            // If not last
            if (i != joints.Length - 1)
            {
                joints[i].next = joints[i + 1];
            }
            // If not first
            if(i != 0)
            {
                joints[i].previous = joints[i - 1];
            }
        }
    }
}
