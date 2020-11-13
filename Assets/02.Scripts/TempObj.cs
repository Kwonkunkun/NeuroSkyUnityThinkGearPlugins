using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempObj : MonoBehaviour
{
    //회전한다. meditation값에 따라서 회전값이 변화한다.
    private void Update()
    {
        transform.Rotate(new Vector3(0, DisplayData.instance.Meditation, 0));       
    }
}
