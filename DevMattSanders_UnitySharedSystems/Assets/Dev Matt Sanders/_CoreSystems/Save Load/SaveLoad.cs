namespace DevMattSanders._CoreSystems
{
	public class SaveLoad : GlobalScriptable
	{
		public static SaveLoad instance;
		public string fileName = "";

//	public BoolVariable loadingGame;
//	public State loadingScreen;

//	public VoidEvent E_loadLastUser;

		public override void SoSetStartingValue()
		{
			base.SoSetStartingValue();
			instance = this;
		}
		/*
    public override void SoStart()
	{
		base.SoStart();

		instance = this;
		E_loadLastUser.Register(LoadLastUser);
	}

	public override void SoEnd()
	{
		base.SoEnd();

		E_loadLastUser.Unregister(LoadLastUser);
	}

	//  public override void ScriptableStart()
	// {
	//     LoadLastUser();
	// }

	private void LoadLastUser()
	{
		//    Load("");
	}
	*/

	}
}
