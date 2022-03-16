using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseSkip : MonoBehaviour
{

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(3);
        }
    }
}
