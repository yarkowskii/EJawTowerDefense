using UnityEngine;
using System.Collections;

public class CameraPan : MonoBehaviour
{
	public float dragSpeed = 10;                            //Camera drag speed;
	public Vector2 sizeX = new Vector2(-15, -10);           //Default bound size;
	public Vector2 sizeY = new Vector2(15, 10);


	public enum DragAxes                                    //Allowed drag axes;
	{
		XY,
		X,
		y
	}
	public DragAxes dragAxes = DragAxes.XY;

	public enum CameraPosition
	{
		SaveCurrent,
		WithNextLevel
	}

	public CameraPosition cameraPosition = CameraPosition.SaveCurrent;

	public Bounds bounds;                                   //Camera bounds;

	public Vector3 defaultPosition;
	private Vector3 dragOrigin, touchPos, moveDir;
	private float mapX, mapY;
	private float minX, maxX, minY, maxY;
	private float vertExtent, horzExtent;
	private bool canStopping;


	void Awake()
	{
		gameObject.tag = "MainCamera"; //Assign default tag
		Application.targetFrameRate = 60;
	}

	void Start()
	{

		//Positioning the camera
		if (defaultPosition != Vector3.zero)
			transform.position = new Vector3(defaultPosition.x, defaultPosition.y, transform.position.z);

		//Calculating bound;
		mapX = bounds.size.x;
		mapY = bounds.size.y;

		bounds.SetMinMax(sizeX, sizeY);

		vertExtent = Camera.main.GetComponent<Camera>().orthographicSize;
		horzExtent = vertExtent * Screen.width / Screen.height;

		minX = horzExtent - (mapX / 2.0F - bounds.center.x);
		maxX = (mapX / 2.0F + bounds.center.x) - horzExtent;

		minY = vertExtent - (mapY / 2.0F - bounds.center.y);
		maxY = (mapY / 2.0F + bounds.center.y) - vertExtent;
	}

	void Update()
	{
		DragCam();

		//Clamp camera movement with bound
		var v3 = transform.position;



		float oldY = v3.y;
		v3.x = Mathf.Clamp(v3.x, minX, maxX);
		v3.y = Mathf.Clamp(v3.y, minY, maxY);
		transform.position = v3;


	}


	//Camera movement
	void DragCam()
	{
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
		if (Input.GetMouseButtonDown(0))
		{
			dragOrigin = Input.mousePosition;
			return;
		}
		if (!Input.GetMouseButton(0)) return;
		touchPos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);


		switch (dragAxes)
		{
			case DragAxes.XY:
				moveDir = new Vector3(touchPos.x, touchPos.y, 0);
				break;
			case DragAxes.X:
				moveDir = new Vector3(touchPos.x, 0, 0);
				break;
			case DragAxes.y:
				moveDir = new Vector3(0, touchPos.y, 0);
				break;
			default:
				break;
		}
		moveDir *= dragSpeed * 15f * Time.deltaTime;
		transform.position -= moveDir;
#endif

#if UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
		{
			canStopping = false;
			touchPos = Input.GetTouch(0).deltaPosition;

			switch (dragAxes)
			{
				case DragAxes.XY:
					moveDir = new Vector3(touchPos.x, touchPos.y, 0);
					break;
				case DragAxes.X:
					moveDir = new Vector3(touchPos.x, 0, 0);
					break;
				case DragAxes.y:
					moveDir = new Vector3(0, touchPos.y, 0);
					break;
				default:
					break;
			}
			Vector3 newPos = transform.position - moveDir;
			transform.position = Vector3.Lerp(transform.position, newPos, dragSpeed * Time.deltaTime);
		}
		else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
		{
			canStopping = true;
			touchPos = Input.GetTouch(0).deltaPosition;

			switch (dragAxes)
			{
				case DragAxes.XY:
					moveDir = new Vector3(touchPos.x, touchPos.y, 0);
					break;
				case DragAxes.X:
					moveDir = new Vector3(touchPos.x, 0, 0);
					break;
				case DragAxes.y:
					moveDir = new Vector3(0, touchPos.y, 0);
					break;
				default:
					break;
			}
			StartCoroutine(Stoping(moveDir));
		}
#endif
	}

	IEnumerator Stoping(Vector3 vec)
	{


		//Vector3 startVec = vec;
		Debug.Log("Finger release");

		while (Mathf.Abs(vec.y) > .05f && canStopping)
		{

			Debug.Log("Stopping scrolling");
			vec = Vector3.Lerp(vec, Vector3.zero, 0.08f);
			transform.position -= vec * dragSpeed * Time.deltaTime;
			if (transform.position.y < minY || transform.position.y > maxY)
				canStopping = false;
			yield return null;
		}
	}

	//Draw bounds in scene view
	void OnDrawGizmos()
	{
		bounds.SetMinMax(sizeX, sizeY);
		Gizmos.DrawWireCube(bounds.center, bounds.size);
	}
}
