using UnityEngine;

public class BroomAndFlyingManager : MonoBehaviour
{
    public GameObject Broom;

    private void Start()
    {
        Broom.SetActive(false);
    }

    private void Update()
    {
        if (Broom.activeSelf)
        {
            // Activate the FlyingNavi script when the Broom is active
            GameObject.Find("Controller (left)").GetComponent<FlyingNavi>().enabled = true;
            GameObject.Find("Controller (right)").GetComponent<FlyingNavi>().enabled = true;
          
        }
        else
        {
            // Deactivate the FlyingNavi script when the Broom is not active
            GameObject.Find("Controller (left)").GetComponent<FlyingNavi>().enabled = false;
            GameObject.Find("Controller (right)").GetComponent<FlyingNavi>().enabled = false;
        }
    }
}

