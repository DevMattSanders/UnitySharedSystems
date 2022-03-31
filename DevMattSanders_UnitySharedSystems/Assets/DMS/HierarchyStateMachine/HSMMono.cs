using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
#endif

namespace DMS.HierarchyStateMachine
{
    [ExecuteInEditMode]
    public class HSMMono : MonoBehaviour
    {
        public static UnityEngine.Events.UnityEvent AllMonoReadFromHSM = new UnityEngine.Events.UnityEvent();

        public HSMMono()
        {
            AllMonoReadFromHSM.AddListener(ReadFromHSM);
            // EditorApplication.hierarchyChanged += HierarchyChanged;
        }



        //  public void HierarchyChanged()
        // {
        // Debug.Log("Hierarchy Changed");
        //    UpdateHSMData();
        // }

        public HSM hsm;

        [Button]
        public void ReadFromHSM()
        {
            //  Debug.Log("Listerers: " + AllMonoReadFromHSM.GetPersistentEventCount());
            //Only reads data of states that currently exists on the mono HSM;
            //Will not add new states to the mono hsm
            foreach(StateMono next in stateMonos)
            {
                next.UpdateColorState();
            }
        }

        private void OnEnable()
        {
            InitStates();
            ReadFromHSM();
        }

        //   [Button]
        private void UpdateHSMData()
        {
            if(hsm == null)
            {
                Debug.Log("HSM null!");
                return;
            }

            // InitStates();
            hsm.ReadFromHSMMono(this);     
        }

    

        //  public List<StateMono> stateMonos = new List<StateMono>();
        public StateMono[] stateMonos;
        /*
    private void FindAllChildHSMs()
    {
        // StateMono[] stateMonos = 
        stateMonos = GetComponentsInChildren<StateMono>();

        foreach (StateMono next in stateMonos)
        {
            next.InitState(this);

            if (next.state != null)
            {
                if (gameStates.ContainsKey(next.state.name))
                {
                    // Debug.Log("Duplicate state assignemnets! " + next.state.name);
                    continue;
                }
                gameStates.Add(next.state.name, next);

            }
        }
    }
    */

        // public static UnityEngine.Events.UnityEvent CallSetNoState = new UnityEngine.Events.UnityEvent();
        // public HSMMono()
        // {
        //     CallSetNoState.AddListener(SetNoState);
        // }

        //public bool debugging;
    

        //  public StateMono firstState;
        //   public StateMono currentState;

        // [HideInInspector] public List<StateMono> currentStates = new List<StateMono>();
        // [HideInInspector] public List<StateMono> newCurrentStates = new List<StateMono>();
        //  [HideInInspector] public List<StateMono> newEnterStates = new List<StateMono>();

        //  public Dictionary<string, StateMono> gameStates = new Dictionary<string, StateMono>();

        // public Color activeStateColour;

        [Button]
        public void InitStates()
        {
            // gameStates.Clear();


            // StateMono[] stateMonos = GetComponentsInChildren<StateMono>();
            stateMonos = GetComponentsInChildren<StateMono>();
            foreach (StateMono next in stateMonos)
            {
                next.InitState(this);
                /*
            if (next.state != null)
            {
                if (gameStates.ContainsKey(next.state.name))
                {
                    continue;
                }
                gameStates.Add(next.state.name, next);

            }
            */
            }

            UpdateHSMData();
        }

        public void SetNoState()
        {
            if (hsm) hsm.SetNoState();
        }
        /*
    [Button]
    public void SetNoState()
    {
        ChangeState(null);

        foreach (StateMono next in gameStates.Values)
        {
           // Debug.Log("Quitting state on exit all 2");
            if (next.state.stateActive)
            {
              //  Debug.Log("Quitting state on exit all 3");
                ExitState(next);
            }
        }
    }
    */

        // private void Start()
        //{

        // Debug.Log("----------- HSM START ------------------");
        //SetNoState();
    	
        //    InitStates();

        // queueStates.Clear();
        // statesThisFrame.Clear();
        // statesToExit.Clear();

        // if (firstState != null)
        // {
        //     firstState.hierarchyStateMachine = this;
        //     firstState.EnterState();
        //  }
        //   }
        /*
    public void SetActiveByGameobject(GameObject go)
    {
        if (!Application.isPlaying)
        {
            InitStates();

            StateMono toSet = null;
            foreach (StateMono next in gameStates.Values)
            {
                if (next.gameObject == go)
                {
                    toSet = next;
                    break;
                }
            }

            if (toSet != null)
            {
                ChangeState(toSet);
            }
        }
    }
    */

        /*
    public List<StateMono> queueStates = new List<StateMono>();
    public List<StateMono> statesThisFrame = new List<StateMono>();

    public List<StateMono> statesToExit = new List<StateMono>();


    public void ChangeState(StateMono gameState)
    {
        
        if (!Application.isPlaying)
        {
            statesThisFrame.Clear();
            queueStates.Clear();

            InitStates();
        }

        if (gameState == null)
        {
            EnterStateByName("");
        }
        else
        {

            if (!statesThisFrame.Contains(gameState))
            {
                statesThisFrame.Add(gameState);

                if (queueStates.Count == 0)
                {

                    queueStates.Add(gameState);

                    while (queueStates.Count > 0)
                    {
                        EnterStateByName(queueStates[0].state.name);

                        queueStates.RemoveAt(0);
                    }
                }
                else
                {
                    queueStates.Add(gameState);
                }
            }

            if (queueStates.Count == 0)
            {
                if (statesThisFrame.Count > 0)
                {
                    statesThisFrame.Clear();
                }
            }
        }
    }

    private void EnterStateByName(string stateName)
    {
        bool newStateNull = string.IsNullOrEmpty(stateName);
        if (gameStates.ContainsKey(stateName) || newStateNull)           
        {
            //   gameStateNames.Add(stateName);
            StateMono nextState = null;
            if (newStateNull == false)
            {
                nextState = gameStates[stateName];
            }

            if (currentState != nextState)
            {
                newCurrentStates.Clear();
                if (!newStateNull)
                {
                    newCurrentStates.Add(nextState);
                    StateMono loopState = nextState;
                    while (true)
                    {
                        if (loopState.parentState != null)
                        {
                            newCurrentStates.Add(loopState.parentState);
                            loopState = loopState.parentState;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                newEnterStates.Clear();

                foreach (StateMono next in newCurrentStates)
                {
                    if (!currentStates.Contains(next))
                    {
                        newEnterStates.Add(next);
                    }
                }

                statesToExit.Clear();

                foreach (StateMono next in currentStates)
                {
                    if (!newCurrentStates.Contains(next))
                    {
                        statesToExit.Add(next);
                    }
                }

                for (int i = newEnterStates.Count - 1; i >= 0; i--)
                    {
                        EnterState(newEnterStates[i]);
                    }

                    currentStates.Clear();
                    currentStates.AddRange(newCurrentStates);
                
                    currentState = nextState;

                    foreach (StateMono next in statesToExit)
                    {
                        ExitState(next);
                    }
            }
        }
       else
        {
            Debug.Log("State does not exist! (" + stateName + ")");
        }
    }

    private void EnterState(StateMono state)
    {
        if(state == currentState)
        {
            state.state.stateActiveAsPrimary = true;
            state.state.stateActiveAsSecondary = false;
        }
        else
        {
            state.state.stateActiveAsPrimary = false;
            state.state.stateActiveAsSecondary = true;
        }

        state.state.stateActive = true;

        if (debugging)
        {
            Scripts.Utility.HierarchyHighlighter highlighter = state.gameObject.AddComponent<Scripts.Utility.HierarchyHighlighter>();
            highlighter.TextStyle = FontStyle.Bold;
            highlighter.Background_Color = activeStateColour;
        }
        state.EnteredState(state);
    }

    private void ExitState(StateMono state)
    {
       if(state.state.stateActiveAsPrimary) state.state.stateActiveAsPrimary = false;
        if (state.state.stateActiveAsSecondary) state.state.stateActiveAsSecondary = false;

        if (state.state.stateActive) state.state.stateActive = false;

        if (debugging)
        {
            DestroyImmediate(state.gameObject.GetComponent<Scripts.Utility.HierarchyHighlighter>());
        }
       // state.ExitedState(state);
    }
    */
    }
}