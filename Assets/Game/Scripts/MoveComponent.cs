using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    private bool isJumping;

    private bool isGrounded;

    [SerializeField]
    private float jumpHeight;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (Input.GetKeyDown(KeyCode.Space))
	        StartCoroutine(JumpCoroutine(new Vector2(0, 0), 0));

        // StartCoroutine(JumpCoroutine(new Vector2(transform.position.x, transform.position.y + jumpHeight), 0));
    }

    void Jump(Vector2 _destination, float _time)
    {

    }

    IEnumerator JumpCoroutine(Vector2 _destination, float _time)
    {
        Vector2 start_pos = transform.position;

        float timer = 0f;
        while (timer <= 3f)
        {
            float height = Mathf.Sin(Mathf.PI * timer) * jumpHeight;
            Vector2 pos = Vector2.Lerp(start_pos, _destination, timer) + ((Vector2)transform.up * height);
            transform.position = new Vector3(pos.x, pos.y, 3);
            timer += Time.deltaTime
                ;
            yield return null;
        }
    }
}
