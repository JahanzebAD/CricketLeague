public class LandingPageTWO : Singleton<LandingPageTWO>
{
	protected void Start()
	{
	}

	public void tapped()
	{
		CONTROLLER.CurrentMenu = "landingpage";
		CONTROLLER.loadingScreenVisited = false;
		Singleton<GameModeTWO>.instance.showMe();
		Singleton<GameModeTWO>.instance.utilSelected(3);
	}
}
