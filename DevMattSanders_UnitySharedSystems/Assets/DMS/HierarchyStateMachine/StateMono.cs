using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace DMS.HierarchyStateMachine
{
    [ExecuteInEditMode]
    public class StateMono : MonoBehaviour
    {
        [HideIf("state")]
        [InlineButton("CreateStateSO", "Create")]
        public string newStateName;

        [OnValueChanged("StateChanged")]
        [InlineEditor]
        public State state;

        public StateMono parentState = null;

        //[HideIf("hierarchyStateMachine")] 
        public HSMMono hierarchyStateMachine;

        // public event Action<StateMono> enteredState;
        /*
    public void EnteredState(StateMono state)
    {
        enteredState?.Invoke(state);
    }
    */
        /*
    public void OnApplicationQuit()
    {
        if (state)
        {
            state.stateActiveAsSecondary = false;
        }
    }
    */
#if UNITY_EDITOR
    private void OnTransformChildrenChanged()
    {
        StateChanged();
        hierarchyStateMachine.SetNoState();
    }

#endif
        public HierarchyHighlighter highlighter;
    
        public void UpdateColorState()
        {
#if UNITY_EDITOR
        if (state == null)
        {
            AddHightlighter();
        }
        else
        {
            if (state.stateActive)
            {
                AddHightlighter();
            }
            else
            {
                RemoveHightlighter();
            }
        }
#endif
        }

        private void RemoveHightlighter()
        {
            if (highlighter)
            {
                DestroyImmediate(highlighter);
                highlighter = null;
            }      
        }

        private void AddHightlighter()
        {
            if (!highlighter)
            {
                highlighter = gameObject.AddComponent<HierarchyHighlighter>();
            }
     
            highlighter.TextStyle = FontStyle.Bold;
            if (state == null)
            {
                highlighter.Background_Color = Color.red;
            }
            else
            {
                highlighter.Background_Color = Color.white;
            }
        }

        /*
    public void OnEnable()
    {
#if UNITY_EDITOR
        if (state == null)
        {
            //Debug.Log("NO STATE ASSIGNED " + this.name);
            return;
        }
        if (state.stateMono != null && state.stateMono != this) { Debug.Log("STATE MONO TWICE ASSIGNED " + state.stateMono.name + " " + this.name); return; }
        state.stateMono = this;
#endif
    }
    */

        public void InitState(HSMMono hsm)
        {
            hierarchyStateMachine = hsm;

            if(state == null)
            {
                return;
            }
            //  Debug.Log("Herer");
            parentState = null;

            //FIND PARENT STATE
            if (transform.parent != null)
            {
                StateMono parentCheck = transform.parent.GetComponent<StateMono>();
                if (parentCheck != null)
                {
                    parentState = parentCheck;
                }
            }
        }

        /*
    public void InitState(HSMMono hsm)
    {
        hierarchyStateMachine = hsm;
       // Debug.Log("STATE INITED");
        if (state == null) 
        { 
           // Debug.Log("NO STATE ASSIGNED " + this.name);
            return;
        }
        if(state.stateMono != null && state.stateMono != this) { Debug.Log("STATE MONO TWICE ASSIGNED " + state.stateMono.name + " " + this.name); return; }


        state.stateMono = this;
        state.stateActiveAsSecondary = false;
        gameObject.name = state.name;
        
        //FIND PARENT STATE
        if (transform.parent != null)
        {
            StateMono parentCheck = transform.parent.GetComponent<StateMono>();
            if (parentCheck != null)
            {
                parentState = parentCheck;
            }
        }      
    }

   

    public void EnterState()
    {

        state.EnterState();
    }
    */

    
#if UNITY_EDITOR

    public void CreateStateSO()
    {
        State myState = ScriptableObject.CreateInstance<State>();

        if (string.IsNullOrEmpty(newStateName))
        {
            newStateName = "New State ";
        }

        myState.name = newStateName;

        AssetDatabase.CreateAsset(myState, "Assets/" + newStateName + ".asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        //Selection.activeObject = myState;

        state = myState;
        newStateName = "";

        StateChanged();
    }


    private void StateChanged()
    {
        if (state)
        {
            gameObject.name = state.name;
            if(hierarchyStateMachine == null)
            {
                hierarchyStateMachine = gameObject.GetComponentInParent<HSMMono>();
            }
            hierarchyStateMachine.InitStates();
           // hierarchyStateMachine.UpdateHSMData();
           // state.stateMono = this;
        }
    }
#endif


    }
}