using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 1.0f;
    private Rigidbody playerRigidbody;

    #region Private Constants
    private Vector2 movement;
    private Rigidbody2D rb;
    private float t = 0.0f;
    private bool moving = false;
    #endregion


    #region Private Methods
    private void Move(Vector2 vector)
    {
        if (true)
        {


            rb.velocity = vector * moveSpeed;
            moving = true;
            t = 0.0f;

        }
    }
	#endregion

	[SerializeField]
	private float moveTime = 0.5f;                              // 1칸 이동에 소요되는 시간

	public Vector3 MoveDirection { set; get; } = Vector3.zero;  // 이동 방향
	public bool IsMove { set; get; } = false;           // 현재 이동중인지

	private IEnumerator Start()
	{
		playerRigidbody = GetComponent<Rigidbody>();
		while (true)
		{
			if (MoveDirection != Vector3.zero && IsMove == false)
			{
				Vector3 end = transform.position + MoveDirection;

				yield return StartCoroutine(GridSmoothMovement(end));
			}

			yield return null;
		}
	}

	private IEnumerator GridSmoothMovement(Vector3 end)
	{
		Vector3 start = transform.position;
		float current = 0;
		float percent = 0;

		IsMove = true;

		while (percent < 1)
		{
			current += Time.deltaTime;
			percent = current / moveTime;

			transform.position = Vector3.Lerp(start, end, percent);

			yield return null;
		}

		IsMove = false;
	}
}