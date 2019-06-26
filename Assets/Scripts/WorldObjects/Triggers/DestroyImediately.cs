using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyImediately : MonoBehaviour
{
    public void DestroyThis()
    {
        Destroy(this.gameObject);
    }
}
