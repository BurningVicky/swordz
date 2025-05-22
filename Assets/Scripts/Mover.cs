using UnityEngine;

public class Mover : MonoBehaviour
{

      [SerializeField]float moveSpeed = 20f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     PrintInstructions();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer(){
        float xValue = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float yValue = 0f;
        float zValue = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed; 
        transform.Translate(xValue, yValue, zValue);
    }
    void PrintInstructions(){
        Debug.Log("Welcome to the game!");
        Debug.Log("Move with WASD or Arrow keys");
        Debug.Log("Don't bump into walls or other players");
    }
}

