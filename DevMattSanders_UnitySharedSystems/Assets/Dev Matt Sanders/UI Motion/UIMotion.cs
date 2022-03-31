using System.Collections;
using System.Collections.Generic;
using DevMattSanders._CoreSystems;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
using Sirenix.OdinInspector.Editor;
#endif
[ExecuteInEditMode]
public class UIMotion : MonoBehaviour
{
    //public bool editingMotionStates = false;
    // public static UnityEvent UpdateUiMotions = new UnityEvent();
    // public static List<UiMotion> allUIMotions = new List<UiMotion>();
    public enum MoveType
    {
        none,
        UIMove,
        StandardMove
    }

    #region Starting State

    [HideInInspector]
    public bool initedStartingState = false;

    public void InitStartingState()
    {
        startingState = new UIMotionState(this);
        initedStartingState = true;
    }

    [GUIColor("StartBoxColour")] [HorizontalGroup("H")] [HideLabel] [SerializeField] [ReadOnly] private string StartingTitle = "On Start";


    [GUIColor("StartBoxColour")]
    [ShowIf("ShowEditStartingStateButton")]
    [HorizontalGroup("H", width: 40), Button(name: "Edit")]
    public void EditStartingState()
    {
        foreach (UIMotionState next in motionStates)
        {
            //    Debug.Log("HERE");
            next.gameCondition.SetMet(false);
        }

        //  if(!Application.isPlaying) UpdateUiMotions.Invoke();
    }

    [HideLabel] public UIMotionState startingState = null;

    Color StartBoxColour()
    {
        if (startingState.editingThisState)
        {
            return new Color(0.8f, 1, 0.8f, 1);
        }
        else
        {
            return new Color(1, 0.8f, 0.8f, 1);
        }
    }

    bool ShowEditStartingStateButton()
    {
        if (startingState.editingThisState)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    #endregion

    [HideInPlayMode]
    [InlineButton("AddCanvasGroup", label: "Set")]
    public CanvasGroup canvasGroup;
    private void AddCanvasGroup()
    {
        CanvasGroup cg = GetComponent<CanvasGroup>();

        if (cg == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        else
        {
            canvasGroup = cg;
        }
    }

    [OnValueChanged("SetUiMotions")]
    [ListDrawerSettings(Expanded = true, HideAddButton = true)]
    public List<UIMotionState> motionStates = new List<UIMotionState>();

    [HideInPlayMode]
    [Button]
    private void NewMotionState()
    {
        motionStates.Add(new UIMotionState(this));
        Debug.Log("Added new motion state");
        motionStates[motionStates.Count - 1].SetCurrentValues();
    }

    public void SetUiMotions()
    {
        foreach (UIMotionState next in motionStates)
        {
            next.uiMotion = this;
        }
    }
    /*
    UIMotionDatabase _m;

    UIMotionDatabase m
    {
        get
        {
            Debug.Log("Here 2");
            if (_m == null) _m = UIMotionDatabase.instance;

            return _m;
        }
    }
    */
#if UNITY_EDITOR
    public static List<UIMotion> uiMotions = new List<UIMotion>();
#endif
    public void OnEnable()
    {
#if UNITY_EDITOR
        if (Application.isPlaying == false)
        {

           // Debug.Log("Add");

            // m.AddUIMotion(this);
            uiMotions.Add(this);
            // UpdateUiMotions.AddListener(UIMotionHeartbeatUpdate);
        }
#endif
    }
  //  [Button]
   // public void Add()
   // {
        // m.AddUIMotion(this);
      //  uiMotions.Add(this);
   // }

    public void OnDisable()
    {
#if UNITY_EDITOR
        if (Application.isPlaying == false)
        {
            //   Debug.Log("Remove");

            //  m.RemoveUIMotion(this);
            uiMotions.Remove(this);
            //   UpdateUiMotions.RemoveListener(UIMotionHeartbeatUpdate);
        }
#endif
    }

    public void Awake()
    {
        if (!Application.isPlaying) return;

        startingState.QuickEnterState();
    }

    public void Start()
    {   
        foreach (UIMotionState next in motionStates)
        {
            next.gameCondition.AddListener(CheckStates);
        }

        CheckStates(false);
    }

    public void OnDestroy()
    {


        foreach (UIMotionState next in motionStates)
        {
            next.gameCondition.RemoveListener(CheckStates);//.OnMetChanged.RemoveListener(StateChanged);
        }
    }

    public void UIMotionHeartbeatUpdate() //This will be a condition instead
    {
        if (Application.isPlaying) return;

        if (initedStartingState == false)
        {
            InitStartingState();
        }
       
        CheckStates(false);
    }

    public void CheckStates(bool value)
    {
        UIMotionState stateToEnter = null;
        foreach (UIMotionState next in motionStates)
        {
            if (next.gameCondition.conditionMet == true)
            {
                stateToEnter = next;
                break;
            }
        }


        bool applicationPlaying = Application.isPlaying;

        foreach (UIMotionState motionState in motionStates)
        {

            if (motionState != stateToEnter)
            {
                if (applicationPlaying)
                {
                    motionState.ExitState();
                }
                else
                {

                    motionState.editingThisState = false;
                }
            }
        }

        if (stateToEnter != null)
        {
            if (applicationPlaying)
            {
                startingState.ExitState();
                stateToEnter.EnterState();
            }
            else
            {
                startingState.editingThisState = false;
                if (stateToEnter.editingThisState == false)
                {
                    stateToEnter.QuickEnterState();
                    stateToEnter.editingThisState = true;
                }
                if (stateToEnter.editingThisState)
                {
           //         stateToEnter.SetCurrentValues();
                }
            }
        }
        else
        {
            if (applicationPlaying)
            {
                startingState.EnterState();
            }
            else
            {
                if (startingState.editingThisState == false)
                {
                    startingState.QuickEnterState();
                    startingState.editingThisState = true;
                }
                // if (startingState.editingThisState)
                // {
            //    startingState.SetCurrentValues();
                // }
            }
        }
    }
}

[System.Serializable]
public class UIMotionState
{

#if UNITY_EDITOR
    #region Odin Vars

    private bool ShowStampingButton()
    {
        if (uiMotion == null) return false;
        if (editingThisState) return true;

        return false;
    }

    private Color ButtonColor()
    {
        if (uiMotion == null)
        {
            return Color.gray;
        }

        if (editingThisState)
        {
            return new Color(0.8f, 1, 0.8f, 1);
        }
        else
        {
            return new Color(1, 0.8f, 0.8f, 1);
        }
    }
    private bool ShowState()
    {
        if (uiMotion != null)
        {
            if (uiMotion.startingState == this) return false;
        }

        return true;
    }
    private bool ShowEditButton()
    {
        if (uiMotion == null)
        {
            return false;
        }
        if (uiMotion.startingState == this) return false;


        if (editingThisState) return false;

        return true;
    }
    private bool ShowStopButton()
    {
        if (uiMotion == null)
        {
            return false;
        }
        if (uiMotion.startingState == this) return false;


        if (editingThisState) return true;

        return false;
    }

    #endregion
#endif

    public UIMotionState(UIMotion _uiMotion)
    {
        uiMotion = _uiMotion;
    }

    public UIMotionState()
    {
        editingThisState = false;
    }

    [ReadOnly] public UIMotion uiMotion;
     [ReadOnly] [SerializeField] public bool editingThisState;


    [HorizontalGroup("H")]
    [VerticalGroup("H/L")]
    [GUIColor("ButtonColor")]
    [BoxGroup("H/L/B", showLabel: false)]
    [ShowIf("ShowState")]
    public GameCondition gameCondition;

    [HorizontalGroup("H/L/B/O")] [LabelWidth(40)] public float delay = 0f;
    [HorizontalGroup("H/L/B/O")] [LabelWidth(40)] public float duration = 0.5f;
    [HorizontalGroup("H/L/B/O")] [HideLabel] public Ease ease = Ease.InOutSine;


    [HideInInspector] [FoldoutGroup("Manual value")] [SerializeField] private Vector2 anchoredPosition;
    [HideInInspector] [FoldoutGroup("Manual value")] [SerializeField] private Vector2 sizeDelta;
    [HideInInspector] [FoldoutGroup("Manual value")] [SerializeField] private Vector2 anchorMin;
    [HideInInspector] [FoldoutGroup("Manual value")] [SerializeField] private Vector2 anchorMax;
    [HideInInspector] [FoldoutGroup("Manual value")] [SerializeField] private Vector3 eulerAngles;
    [HideInInspector] [FoldoutGroup("Manual value")] [SerializeField] private Vector3 localScale = Vector3.one;
    [HideInInspector] [FoldoutGroup("Manual value")] [SerializeField] private float alpha = 1;

    public Tween positionTween, sizeDeltaTween, anchorMinTween, anchorMaxTween, eulerAnglesTween, localScaleTween, alphaTween;

    [HorizontalGroup("H", Width = 50)]

    [VerticalGroup("H/R")]
    [ShowIf("ShowEditButton")]
    [Button]
    private void Edit()
    {
        foreach (UIMotionState next in uiMotion.motionStates)
        {
            next.gameCondition.SetMet(false);
        }

        gameCondition.SetMet(true);
    }
    [VerticalGroup("H/R")]
    [ShowIf("ShowStopButton")]
    [Button]
    private void Stop()
    {
        gameCondition.SetMet(false);
    }

    
  
    [VerticalGroup("H/R")]
    [ShowIf("ShowStampingButton")]
    [Button("Stamp")]
    public void SetCurrentValues()
    {
        if (Application.isPlaying) return;
       
        if (uiMotion == null) { Debug.Log("UI Motion Null!"); return; }

       // RectTransform rect = uiMotion.transform as RectTransform;
        if (RectTransform == null) return;

        anchoredPosition = RectTransform.anchoredPosition;
        sizeDelta = RectTransform.sizeDelta;
        anchorMin = RectTransform.anchorMin;
        anchorMax = RectTransform.anchorMax;

        eulerAngles = RectTransform.localEulerAngles;
        localScale = RectTransform.localScale;

        if (uiMotion.canvasGroup) alpha = uiMotion.canvasGroup.alpha;
    }

    public void QuickEnterState()
    {
        if (uiMotion == null) { Debug.Log("UI Motion Null!"); return; }

       // RectTransform rect = uiMotion.transform as RectTransform;
        if (RectTransform == null) return;

        RectTransform.anchoredPosition = anchoredPosition;

        RectTransform.sizeDelta = sizeDelta;

        RectTransform.anchorMin = anchorMin;
        RectTransform.anchorMax = anchorMax;

        RectTransform.localEulerAngles = eulerAngles;
        RectTransform.localScale = localScale;

        if (uiMotion.canvasGroup) uiMotion.canvasGroup.alpha = alpha;

        KillTweens();
    }

    private bool inState = false;
    public void EnterState()
    {
        if (inState) return;
        inState = true;
        
        if (RectTransform == null) return;
        if (RectTransform.anchoredPosition != anchoredPosition) positionTween = RectTransform.DOAnchorPos(anchoredPosition, duration).SetDelay(delay).SetEase(ease);
        if (RectTransform.sizeDelta != sizeDelta) sizeDeltaTween = RectTransform.DOSizeDelta(sizeDelta, duration).SetDelay(delay).SetEase(ease);
        if (RectTransform.anchorMin != anchorMin) anchorMinTween = RectTransform.DOAnchorMin(anchorMin, duration).SetDelay(delay).SetEase(ease);
        if (RectTransform.anchorMax != anchorMax) anchorMaxTween = RectTransform.DOAnchorMax(anchorMax, duration).SetDelay(delay).SetEase(ease);
        if (RectTransform.eulerAngles != eulerAngles) eulerAnglesTween = RectTransform.DOLocalRotate(eulerAngles, duration, RotateMode.FastBeyond360).SetDelay(delay).SetEase(ease);
        if (RectTransform.localScale != localScale) localScaleTween = RectTransform.DOScale(localScale, duration).SetDelay(delay).SetEase(ease);
        if (uiMotion.canvasGroup != null) { if (uiMotion.canvasGroup.alpha != alpha) alphaTween = uiMotion.canvasGroup.DOFade(alpha, duration).SetDelay(delay).SetEase(ease); }
    }

    public void ExitState()
    {
        if (inState == false) return;
        inState = false;
        KillTweens();
    }

    public void KillTweens()
    {
        if (positionTween != null) positionTween.Kill();
        if (sizeDeltaTween != null) sizeDeltaTween.Kill();
        if (anchorMinTween != null) anchorMinTween.Kill();
        if (anchorMaxTween != null) anchorMaxTween.Kill();
        if (eulerAnglesTween != null) eulerAnglesTween.Kill();
        if (localScaleTween != null) localScaleTween.Kill();
        if (alphaTween != null) alphaTween.Kill();
    }

    RectTransform _rectTransform;

    RectTransform RectTransform
    {
        get
        {
            if(_rectTransform == null && uiMotion != null)
            {
                _rectTransform = uiMotion.transform as RectTransform;
            }

            return _rectTransform;
        }
    }
}
