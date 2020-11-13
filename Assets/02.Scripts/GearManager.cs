using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GearManager : MonoBehaviour
{
	#region 싱글톤

	private static GearManager m_instance; // 싱글톤이 할당될 static 변수
	public bool isGameover { get; private set; } // 게임 오버 상태
	public int hp = 5;

	public static GearManager instance
	{
		get
		{
			// 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
			if (m_instance == null)
			{
				// 씬에서 GameManager 오브젝트를 찾아 할당
				m_instance = FindObjectOfType<GearManager>();
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

	public Text testTxt;
	public Texture2D[] signalIcons;
	ThinkGearController controller;

	private float indexSignalIcons = 1;
	private bool enableAnimation = false;
	private float animationInterval = 0.06f;

	private int Raw = 0;
	private int PoorSignal = 200;
	private int Attention = 0;
	private int Meditation = 0;
	private int Blink = 0;
	private float Delta = 0.0f;
	private float Theta = 0.0f;
	private float LowAlpha = 0.0f;
	private float HighAlpha = 0.0f;
	private float LowBeta = 0.0f;
	private float HighBeta = 0.0f;
	private float LowGamma = 0.0f;
	private float HighGamma = 0.0f;

	private int Algo_Attention = 0;
	private int Algo_Meditation = 0;
	private float Algo_Delta = 0.0f;
	private float Algo_Theta = 0.0f;
	private float Algo_Alpha = 0.0f;
	private float Algo_Beta = 0.0f;
	private float Algo_Gamma = 0.0f;

	void Start()
    {
		controller = GameObject.Find("ThinkGear").GetComponent<ThinkGearController>();

		controller.UpdateRawdataEvent += OnUpdateRaw;
		controller.UpdatePoorSignalEvent += OnUpdatePoorSignal;
		controller.UpdateAttentionEvent += OnUpdateAttention;
		controller.UpdateMeditationEvent += OnUpdateMeditation;

		controller.UpdateDeltaEvent += OnUpdateDelta;
		controller.UpdateThetaEvent += OnUpdateTheta;

		controller.UpdateHighAlphaEvent += OnUpdateHighAlpha;
		controller.UpdateHighBetaEvent += OnUpdateHighBeta;
		controller.UpdateHighGammaEvent += OnUpdateHighGamma;

		controller.UpdateLowAlphaEvent += OnUpdateLowAlpha;
		controller.UpdateLowBetaEvent += OnUpdateLowBeta;
		controller.UpdateLowGammaEvent += OnUpdateLowGamma;

		controller.UpdateBlinkEvent += OnUpdateBlink;

		controller.Algo_UpdateAttentionEvent += OnAlgo_UpdateAttentionEvent;
		controller.Algo_UpdateMeditationEvent += OnAlgo_UpdateMeditationEvent;
		controller.Algo_UpdateDeltaEvent += OnAlgo_UpdateDeltaEvent;
		controller.Algo_UpdateThetaEvent += OnAlgo_UpdateThetaEvent;
		controller.Algo_UpdateAlphaEvent += OnAlgo_UpdateAlphaEvent;
		controller.Algo_UpdateBetaEvent += OnAlgo_UpdateBetaEvent;
		controller.Algo_UpdateGammaEvent += OnAlgo_UpdateGammaEvent;
	}

    #region 뉴로스카이

    void OnAlgo_UpdateAttentionEvent(int value)
	{
		Algo_Attention = value;
	}
	void OnAlgo_UpdateMeditationEvent(int value)
	{
		Algo_Meditation = value;

	}
	void OnAlgo_UpdateDeltaEvent(float value)
	{
		Algo_Delta = value;
	}
	void OnAlgo_UpdateThetaEvent(float value)
	{
		Algo_Theta = value;
	}
	void OnAlgo_UpdateAlphaEvent(float value)
	{
		Algo_Alpha = value;
	}
	void OnAlgo_UpdateBetaEvent(float value)
	{
		Algo_Beta = value;
	}
	void OnAlgo_UpdateGammaEvent(float value)
	{
		Algo_Gamma = value;
	}
	void OnUpdatePoorSignal(int value)
	{
		PoorSignal = value;
		if (value == 0)
		{
			indexSignalIcons = 0;
			enableAnimation = false;
		}
		else if (value == 200)
		{
			indexSignalIcons = 1;
			enableAnimation = false;
		}
		else if (!enableAnimation)
		{
			indexSignalIcons = 2;
			enableAnimation = true;
		}
	}
	void OnUpdateRaw(int value)
	{
		Raw = value;
	}
	void OnUpdateAttention(int value)
	{
		Attention = value;
	}
	void OnUpdateMeditation(int value)
	{
		Meditation = value;

	}
	void OnUpdateDelta(float value)
	{
		Delta = value;
	}
	void OnUpdateTheta(float value)
	{
		Theta = value;
	}
	void OnUpdateHighAlpha(float value)
	{
		HighAlpha = value;
	}
	void OnUpdateHighBeta(float value)
	{
		HighBeta = value;
	}
	void OnUpdateHighGamma(float value)
	{
		HighGamma = value;
	}
	void OnUpdateLowAlpha(float value)
	{
		LowAlpha = value;
	}
	void OnUpdateLowBeta(float value)
	{
		LowBeta = value;
	}
	void OnUpdateLowGamma(float value)
	{
		LowGamma = value;
	}

	void OnUpdateBlink(int value)
	{
		Blink = value;
	}
	#endregion

	void FixedUpdate()
	{
		if (enableAnimation)
		{
			if (indexSignalIcons >= 4.8)
			{
				indexSignalIcons = 2;
			}
			indexSignalIcons += animationInterval;
		}
		testTxt.text = Meditation.ToString();
	}

	private void OnGUI()
	{
		GUILayout.BeginHorizontal();
		GUILayout.Label("Demo App");
		GUILayout.Space(Screen.width - 250);
		GUILayout.Label(signalIcons[(int)indexSignalIcons]);
		GUILayout.EndHorizontal();
	}

	public void OnConnectDevice()
	{
		UnityThinkGear.Init(true);
		UnityThinkGear.StartStream();
	}

	public void OnInitDevice()
	{
		UnityThinkGear.Init(true);
	}

	public void OnQuit()
	{
		Application.Quit();
	}

}
