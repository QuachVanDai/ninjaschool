using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUi : NCKHMonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.localScale = transform.parent.localScale;
    }
}
