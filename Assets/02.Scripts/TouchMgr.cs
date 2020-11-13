using GoogleARCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMgr : MonoBehaviour
{
    #region 싱글톤
    private static TouchMgr m_instance; // 싱글톤이 할당될 static 변수
    public bool isGameover { get; private set; } // 게임 오버 상태
    public int hp = 5;

    public static TouchMgr instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아 할당
                m_instance = FindObjectOfType<TouchMgr>();
            }

            // 싱글톤 오브젝트를 반환
            return m_instance;
        }
    }
    private void Awake()
    {
        // 씬에 싱글톤 오브젝트가 된 다른 GameManager 오브젝트가 있다면
        if (instance != this)
        {
            // 자신을 파괴
            Destroy(gameObject);
        }
    }
    #endregion 

    private Camera ARCamera;
    public GameObject placeObject;
    private bool isExist = false;

    void Start()
    {
        ARCamera = GameObject.Find("First Person Camera").GetComponent<Camera>();
    }

    void Update()
    {
        if (isExist == false)
        {
            if (Input.touchCount == 0)
                return;

            Touch touch = Input.GetTouch(0);

            TrackableHit hit;

            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon | TrackableHitFlags.FeaturePointWithSurfaceNormal;

            if (touch.phase == TouchPhase.Began
                && Frame.Raycast(touch.position.x
                                , touch.position.y
                                , raycastFilter
                                , out hit))
            {
                var anchor = hit.Trackable.CreateAnchor(hit.Pose);

                GameObject obj = Instantiate(placeObject
                                            , hit.Pose.position
                                            , Quaternion.identity
                                            , anchor.transform);

                var rot = Quaternion.LookRotation(ARCamera.transform.position
                                                    - hit.Pose.position);

                obj.transform.rotation = Quaternion.Euler(ARCamera.transform.position.x
                                                            , rot.eulerAngles.y
                                                            , ARCamera.transform.position.z);

                isExist = true;
            }
        }
    }
}