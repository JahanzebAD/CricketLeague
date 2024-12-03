using System;
using System.Collections;
using System.Collections.Generic;
using Beebyte.Obfuscator;
using CodeStage.AntiCheat.ObscuredTypes;
using DG.Tweening;
using GreedyGame.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class GroundController : Singleton<GroundController>
{
	public Camera sixDistanceCamera;

	public Image BG;

	private Transform bowlingSpotTransform;

	private Transform mainUmpireTransform;

	private Transform batsmanTransform;

	private Transform runnerTransform;

	private Transform ballTimingOriginTransform;

	private Transform fielder10Transform;

	private Transform wicketKeeperTransform;

	private Transform bowlerTransform;

	private bool canSwipeNow;

	public Transform BallHitEffect;

	private Transform mainCameraTransform;

	private Transform umpireCameraTransform;

	private Transform introCameraTransform;

	private Transform replayCameraTransform;

	private Transform rightSideCamTransform;

	private Transform leftSideCamTransform;

	private Transform ultraMotionCamTransform;

	private Transform closeUpCamTransform;

	private float DEG2RAD = (float)Math.PI / 180f;

	private float RAD2DEG = 180f / (float)Math.PI;

	private int randomBowler;

	private bool playIntro = true;

	private string battingBy;

	private string bowlingBy;

	private bool IsFullTossBall;

	private float FullBallLength;

	private float FullTossDisplacementFromCrease;

	private float FulltossCalculationVal1;

	private float FulltossCalculationVal2;

	private float AngleBtwX;

	private float temp;

	private float AngleBtwForFindingStraightLine;

	private float Hypotenuse;

	private float xDiff;

	private float zDiff;

	private float creaseLineAdjacentLength;

	private float creaseLineThetaBtwAdjAndHyp;

	private float creaseLineHypotenuse;

	private float creaseLineOppositeLength;

	private float FullTossTriggerLength;

	private float SwingOffsetDisplacement;

	private int FullTossChance;

	private Vector3 UltraEdgeCamPosition;

	private Vector3 UltraEdgeCamRotation;

	private Vector3 sideCamPos;

	private Vector3 sideCamRot;

	public bool canShowDRS;

	public bool canShowHotspot;

	private bool showImpact = true;

	private bool umpireDecision;

	private bool isImpactedDuringReplay = true;

	private bool impacting;

	public bool impactOffSideWithAttemptedShot;

	private GameObject impactBall;

	public int drsCount;

	private bool pitching;

	private bool hitting;

	private bool isOutByDrs = true;

	private bool isUmpiresCall;

	private bool canAiGoForDRS;

	public bool bDRSPitchingOutsideLeg;

	public int drsCalledByBattingTeam = -1;

	public int drsCalledByUser = -1;

	public GameObject ball;

	private Transform ballTransform;

	private GameObject ballRayCastReferenceGO;

	private Transform ballRayCastReferenceGOTransform;

	private GameObject ballOriginGO;

	private GameObject ballTimingOrigin;

	private Vector3 ballInitPosition;

	private float ballRadius = 0.05f;

	private int ballNoOfBounce;

	private float ballProjectileAngle = 270f;

	private float ballProjectileAnglePerSecond;

	private float ballProjectileHeight = 2.15f;

	private float horizontalSpeed = 22f;

	public float ballAngle;

	private float AIBallAngle;

	private string ballStatus = string.Empty;

	private float ballBatMeetingHeight;

	private float ballTimingFirstBounceDistance;

	private GameObject ballTimingFirstBounce;

	private GameObject ballCatchingSpot;

	private Transform ballCatchingSpotTransform;

	private float ballPreCatchingDistance;

	public bool pauseTheBall;

	private string ballResult = string.Empty;

	private GameObject ninjaSlice;

	private TrailRenderer ballTrail;

	private Gradient ballTrialColorGradient;

	private Material ballTrailMaterial;

	private bool applyBallFiction;

	public int currentBallNoOfRuns;

	private float timeBetweenBalls;

	private float stayStartTime;

	private GameObject ballSpotAtCreaseLine;

	private GameObject ballSpotAtStump;

	private float shortestBallPickupDistance;

	public bool ballReleased;

	private bool hattrickBall;

	private int fielderStopCount;

	private List<bool> fielderNearToPitch = new List<bool>();

	private float creaseEdge = 8.73f;

	private GameObject batEdgeGO;

	private Transform batTopEdgeTransform;

	private Transform batShadowHolderTransform;

	private GameObject batsmanRightLegEdge;

	private GameObject batsmanLeftLegEdge;

	private List<float> userFielderScanList = new List<float>();

	private List<float> userFielderScanListRefined = new List<float>();

	private Vector3 tempSpot1;

	private Vector3 tempSpot2;

	private bool CancelRun;

	private bool callCancelRunFunc;

	private int cancelRunDirectionFactor = 1;

	private bool moveMainUmpire = true;

	private float[,] cancelRunArray = new float[10, 10];

	private float[,] saveRunAgainArray = new float[10, 10];

	private int saveRunAgainCount;

	private int cancelRunCount;

	private int saveCount;

	private int cancelCount;

	private GameObject batsmanLeftLegEdgePoint;

	private GameObject batsmanLeftShoeBackEdge;

	private GameObject batsmanRightShoeBackEdge;

	private bool keeperCaughtDiffCatch;

	public bool canShowFCLPowers;

	private bool ballPickedByFielder;

	private List<bool> fielderSetChasePoint = new List<bool>();

	private List<GameObject> getSlipArray = new List<GameObject>();

	private List<GameObject> aiFielderScanArray = new List<GameObject>();

	private bool setfrictionRation;

	private float speedReduceFactor;

	private bool hasReachedFirstBounce;

	private bool topEdge;

	private bool isLbwOut;

	private GameObject throwToGO;

	private bool isFielderthrown;

	private bool canKeeperCollectBall;

	public bool edgeCatch;

	private bool saveEdgeCatch;

	private bool aiCancelRun;

	private float throwingFirstBounceDistance;

	public GameObject batsman;

	public Animation batsmanAnimationComponent;

	private Animation BowlerAnimationComponent;

	private Animation WicketKeeperAnimationComponent;

	private Animation MainUmpireAnimationComponent;

	private Animation SideUmpireAnimationComponent;

	private Animation Fielder10AnimationComponent;

	public Animation Stump1AnimationComponent;

	public Animation Stump2AnimationComponent;

	private Animation RunnerAnimationComponent;

	private Animation StrikerAnimationComponent;

	private Animation NonStrikerAnimationComponent;

	private Animation[] FielderAnimationComponent = new Animation[10];

	private Renderer fielder10SkinRendererComponent;

	public Renderer BatsmanSkinRendererComponent;

	private Renderer RunnerSkinRendererComponent;

	public Renderer MainUmpireSkinRendererComponent;

	public Renderer SideUmpireSkinRendererComponent;

	private Renderer BowlerSkinRendererComponent;

	private Renderer WicketKeeperSkinRendererComponent;

	private Renderer Fielder10BallSkinRendererComponent;

	private Renderer BallSkinRendererComponent;

	private Renderer WicketKeeperBallSkinRendererComponent;

	private Renderer BowlerBallSkinRendererComponent;

	private Renderer DigitalScreenRendererComponent;

	private Renderer[] FielderSkinRendererComponent = new Renderer[11];

	public Collider BatColliderComponent;

	public GameObject batsmanSkin;

	private GameObject batCollider;

	private GameObject batCollider2;

	private GameObject leftLowerLegObject;

	private GameObject rightLowerLegObject;

	private GameObject leftUpperLegObject;

	private GameObject rightUpperLegObject;

	private GameObject stump1Collider;

	private GameObject stump2Collider;

	private GameObject boardCollider;

	private GameObject RHBatsmanMaxBowlLimitGO;

	private GameObject RHBatsmanMinBowlLimitGO;

	private GameObject LHBatsmanMaxBowlLimitGO;

	private GameObject LHBatsmanMinBowlLimitGO;

	private bool lbwAppeal;

	private bool LBW;

	private float batsmanInitXPos;

	private bool umpireOriginalDecision;

	private Vector3 RHBatsmanInitPosition;

	private Vector3 LHBatsmanInitPosition;

	private GameObject LHBatsmanInitSpot;

	public string shotPlayed = string.Empty;

	private bool squareLegGlance;

	private bool squareCutDrive;

	public string batsmanAnimation = string.Empty;

	private float wantedAnimationSpeed;

	private GameObject shotActivationMinLimit;

	private GameObject shotActivationMaxLimit;

	private bool canMakeShot;

	private bool batsmanTriggeredShot;

	private bool batsmanMadeShot;

	private bool batsmanCanMoveLeftRight;

	private bool batsmanOnLeftRightMovement;

	private float batsmanLeftRightMovementDuring = 2f;

	private GameObject RHBatsmanBackwardLimit;

	private GameObject RHBatsmanForwardLimit;

	private GameObject LHBatsmanBackwardLimit;

	private GameObject LHBatsmanForwardLimit;

	private GameObject RHBMaxWideLimit;

	private GameObject RHBMinWideLimit;

	private GameObject LHBMaxWideLimit;

	private GameObject LHBMinWideLimit;

	public string currentBatsmanHand = "right";

	private float animationFPS = 25f;

	private float animationFPSDivide = 0.04f;

	private float optimalShotTime;

	private float ballReleasedTime;

	private float batReachingTimeForOptimalShotLength;

	private float optimalShotActivationTime;

	private GameObject wicketKeeper;

	public GameObject wicketKeeperSkin;

	private GameObject wicketKeeperBall;

	private Vector3 wicketKeeperInitPosition4RHBFast;

	private Vector3 wicketKeeperInitPosition4LHBFast;

	private Vector3 wicketKeeperInitPosition4RHBSpin;

	private Vector3 wicketKeeperInitPosition4LHBSpin;

	private GameObject WKRefPoint;

	private GameObject fielder10Ref;

	private GameObject wicketKeeperStraightBallStumping;

	private GameObject wicketKeeperLegSideBallStumping;

	private GameObject wicketKeeperOffSideBallStumping;

	private float distanceBtwBallAndCollectingPlayerWhileThrowing;

	private string postBattingWicketKeeperDirection = string.Empty;

	private GameObject bowler;

	public GameObject bowlerSkin;

	private GameObject bowlerBall;

	private float spinValue;

	private GameObject hideBowlingInterfaceSpot;

	private bool hideBowlingInterface;

	private bool userBowlerCanMoveBowlingSpot;

	private bool userBowlingSpotSelected;

	private GameObject userBowlingMinLimit;

	private GameObject userBowlingMaxLimit;

	private GameObject userBowlingFullTossTrigger;

	public GameObject fielder10;

	private GameObject fielder10Skin;

	private GameObject fielder10Ball;

	private GameObject bowlingSpotGO;

	private GameObject bowlingSpotModel;

	private GameObject bowlingSpotFullTossGO;

	private GameObject TempBallStartPoint;

	private Renderer bowlingSpotRenderer;

	private Renderer bowlingSpotFullTossRenderer;

	private GameObject fielder10FastInit;

	private GameObject fielder10SpinInit;

	private float FullTossY;

	private float FullTossX;

	private float ballSpotLength;

	private float ballSpotHeight;

	private float ballHeightAtStump;

	private float bowlerRunningSpeed = 5f;

	public string currentBowlerType = "fast";

	private int currentBowlerSpinType = 2;

	public string currentBowlerHand = "right";

	private float bowlerRunupTime = 4.84f;

	private bool canActivateBowler;

	private string fielder10Action = string.Empty;

	private GameObject fielderStraightBallStumping;

	private GameObject fielderLegSideBallStumping;

	private GameObject fielderOffSideBallStumping;

	private string postBattingStumpFielderDirection;

	private int noOfFielders = 9;

	public GameObject[] fielder = new GameObject[10];

	private Transform[] fielderTransform = new Transform[10];

	private GameObject[] FielderBallReleasePointGOBJ = new GameObject[10];

	private Transform[] FielderBallReleasePointGOTransform = new Transform[10];

	private GameObject[] fielderBall = new GameObject[10];

	private GameObject[] fielderRef = new GameObject[10];

	public GameObject[] fielderSkin = new GameObject[11];

	private Vector3[] fielderInitPosition = new Vector3[10];

	public Vector3[] fieldRestriction1FielderPosition = new Vector3[10];

	public Vector3[] fieldRestriction2FielderPosition = new Vector3[10];

	public Vector3[] fieldRestriction3FielderPosition = new Vector3[10];

	public Vector3[] fieldRestriction4FielderPosition = new Vector3[10];

	public Vector3[] fieldRestriction5FielderPosition = new Vector3[10];

	public Vector3[] fieldRestriction6FielderPosition = new Vector3[10];

	public Vector3[] fieldRestriction7FielderPosition = new Vector3[10];

	public Vector3[] fieldRestriction8FielderPosition = new Vector3[10];

	public Vector3[] fieldRestriction9FielderPosition = new Vector3[10];

	public Vector3[] fieldRestriction10FielderPosition = new Vector3[10];

	public Vector3[] fieldRestriction11FielderPosition = new Vector3[10];

	public Vector3[] fieldRestriction12FielderPosition = new Vector3[10];

	public Vector3[] fieldRestriction13FielderPosition = new Vector3[10];

	public Vector3[] fieldRestriction14FielderPosition = new Vector3[10];

	public Vector3[] fieldRestriction15FielderPosition = new Vector3[10];

	public Vector3[] fieldRestriction16FielderPosition = new Vector3[10];

	public Vector3[] fieldRestriction17FielderPosition = new Vector3[10];

	public Vector3[] fieldRestriction18FielderPosition = new Vector3[10];

	public Vector3[] fieldRestriction19FielderPosition = new Vector3[10];

	public Vector3[] fieldRestriction20FielderPosition = new Vector3[10];

	public Vector3[] fieldRestriction21FielderPosition = new Vector3[10];

	public Vector3[] fieldRestriction22FielderPosition = new Vector3[10];

	public Vector3[] fieldRestriction23FielderPosition = new Vector3[10];

	public Vector3[] fieldRestriction24FielderPosition = new Vector3[10];

	public Vector3[] fieldRestriction25FielderPosition = new Vector3[10];

	private float[] fielderAngle = new float[10];

	private float[] fielderDistance = new float[10];

	private float[] fielderBallDiffInAngle = new float[10];

	private GameObject[] fielderChasePoint = new GameObject[10];

	private bool stopTheFielders;

	private float fielderThrowElapsedTime;

	private GameObject slipFielderSpotForRHBSpin;

	private GameObject slipFielder2SpotForRHBSpin;

	public float delayMakeFieldersAppeal;

	public GameObject runner;

	public GameObject runnerSkin;

	private GameObject RHBNonStrikerRunningSpot;

	private GameObject RHBStrikerRunningSpot;

	private GameObject runnerStrikerRunningSpot;

	private GameObject runnerNonStrikerRunningSpot;

	private GameObject nonStrikerNearCreaseSpot;

	private GameObject strikerNearCreaseSpot;

	private float batsmanRunningAngle;

	private float runnerRunningAngle;

	private Vector3 runnerInitPosition;

	private GameObject nonStrikerReachSpot;

	private GameObject strikerReachSpot;

	public Transform mainUmpire;

	public GameObject sideUmpire;

	public GameObject mainUmpireSkin;

	public GameObject sideUmpireSkin;

	private Vector3 mainUmpireInitPosition;

	private GameObject umpireLeftSideSpot;

	private GameObject umpireRightSideSpot;

	public int action = -10;

	private GameObject stump1;

	private GameObject stump2;

	private GameObject stump1Crease;

	private GameObject stump2Crease;

	private GameObject stump1Spot;

	private GameObject stump2Spot;

	private float outOfPitchZPos = 10.5f;

	private float groundRadius = 68.5f;

	private float groundBannerRadius = 70f;

	public bool ballOnboundaryLine;

	public float speedThresholdDuringBoundary = 0.5f;

	private int nearestFielderIndex = -1;

	private float boundaryHeight;

	private string goToBoundaryAnim = string.Empty;

	private GameObject UIGO;

	private float powerFactor;

	public float controlFactor;

	private float agilityFactor;

	[SerializeField]
	private GameObject sideUmpireCameraSpot;

	public Camera mainCamera;

	private Vector3 mainCameraZoomOutPosition = new Vector3(0f, 6.8f, -60f);

	private Vector3 fastBowlerZoomOutPosition = new Vector3(0f, 6.8f, -68f);

	private Vector3 mainCameraZoomInPosition = new Vector3(0f, 6.8f, -45f);

	private Vector3 mainCameraInitRotation = new Vector3(7.5f, 0f, 0f);

	public GameObject groundCenterPoint;

	private Transform groundCenterPointTransform;

	private GameObject groundGO;

	public GameObject digitalScreen;

	private Vector3 digitalScreenScale;

	private GameObject hideableObjects;

	private bool upArrowKeyDown;

	private bool downArrowKeyDown;

	private bool leftArrowKeyDown;

	private bool rightArrowKeyDown;

	private bool powerKeyDown;

	private bool powerShot;

	private bool takeRun;

	private bool canTakeRun;

	public Camera rightSideCamera;

	public Camera leftSideCamera;

	public Camera previewCamera;

	public Camera umpireCamera;

	public Camera introCamera;

	public Camera ultraMotionCamera;

	public Camera closeUpCamera;

	public Camera replayCamera;

	private Camera UICam;

	public Camera rightSideBoundaryCamera;

	public Camera leftSideBoundaryCamera;

	private GameObject introCameraPivot;

	private bool sideCameraSelected;

	private ReplaySmoothFollow replayCameraScript;

	private float replayCameraRotationAngleInBoundary;

	private float zoomCameraToBowlerStartTime;

	public bool limitReplayCameraHeight;

	public Camera veteranCamera;

	public Camera zombieCamera;

	public Camera fireCamera;

	public Camera googlyCamera;

	private GameObject fireCameraPivot;

	private GameObject umpireCameraPivot;

	public bool showShadows = true;

	private GameObject[] FielderShadowRefGO = new GameObject[9];

	private List<GameObject> ShadowsArray = new List<GameObject>();

	private List<Transform> ShadowsArrayTransform = new List<Transform>();

	private List<GameObject> ShadowRefArray = new List<GameObject>();

	private List<Transform> ShadowRefArrayTransform = new List<Transform>();

	private GameObject bowlerShadowRef;

	private Transform bowlerShadowRefTransform;

	private Transform batsmanRefPoint;

	private bool batsmanConfidenceLevel;

	private bool ballInline;

	private bool mouseDownD;

	private bool bowlerIsWaiting;

	public string bowlerSide = "right";

	private float swingProjectileAnglePerSecond;

	private float swingProjectileAngle;

	private float swingValue;

	private bool swingingBall;

	public bool slipShot;

	private bool ballToFineLeg;

	private int mainUmpireIndex;

	private int sideUmpireIndex;

	private bool fielderAppealForRunOut;

	public bool replayMode;

	private float savedBallAngle;

	private float savedHorizontalSpeed;

	private float savedBallProjectileAngle;

	private float savedBallProjectileHeight;

	private float savedBallTimingFirstBounceDistance;

	private float savedBallProjectileAnglePerSecond;

	private bool savedSlipShot;

	private bool savedBallToFineLeg;

	private string savedShotPlayed;

	private float savedShotExecutedZPosition;

	private Vector3 savedBatsmanShotExecutedPosition;

	private float bowlerRunupStartTime;

	private bool savedLeftArrowDownTime;

	private bool savedRightArrowDownTime;

	private List<float> savedLeftArrowKeyDownArray = new List<float>();

	private List<float> savedLeftArrowKeyUpArray = new List<float>();

	private List<float> savedRightArrowKeyDownArray = new List<float>();

	private List<float> savedRightArrowKeyUpArray = new List<float>();

	private float[] slipFielderExtraAction = new float[5];

	private List<bool> slipFielderDoingWarmUpAction = new List<bool>();

	private float[] slipFielderWarmUpAnimationSpeed = new float[5];

	private bool savedLBW;

	private bool savedLbwAppeal;

	private List<float> savedTakeRunTimingArray = new List<float>();

	private float ballConnectedTiming;

	private bool savedPowerShotStatus;

	private float savedThrowingFirstBounceDistance;

	private float savedThrowLength;

	private string savedSummary = string.Empty;

	private GameObject replayController;

	private Transform replayControllerTransform;

	private string replayActionStatus = string.Empty;

	private float ballSpinningSpeedInX;

	private float ballSpinningSpeedInZ;

	private float savedBallSpinningSpeedInX;

	private float savedBallSpinningSpeedInZ;

	private float savedFirstBounceBallSpinningSpeedInZ;

	private float savedBallConnectedSpinningSpeedInZ;

	private bool savedRunOutAppeal;

	private int savedPickedUpFielderIndex = -1;

	public static bool SOMatchStart;

	public static bool QPMatchStart;

	private bool AIHitInGap;

	private bool savedIsRunOut;

	private int savedCurrentBallNoOfRuns;

	private float savedBallRayCastConnectedZposition;

	private Collider savedPadCollider;

	private float savedBallAngleAfterHittingPads;

	private Vector3 savedBallConnectedPosition;

	private string savedThrowAction = string.Empty;

	private int savedCelebrationAnimationIndex;

	private string savedThrowTo;

	private string savedStumpAnimationToPlay;

	private string pickingupAnimationToPlay = string.Empty;

	private bool afterReplayUpdateRunForRunoutFailedAttempt;

	private GameObject fielder10FocusGObjToCollectTheBall;

	private int canBe4or6 = 6;

	private string[] TeamNames = new string[14];

	private GameObject CamPivotGO;

	private int StrikerBurntIndex = -1;

	private int NonStrikerBurntIndex = -1;

	private int AshCounter = -7;

	private bool AshCounterActive;

	private Shader shaderDiffuse;

	private Shader shaderTransparentDiffuse;

	private Shader shaderTransparentSoftEdge;

	private Shader shaderSpriteCutout;

	private Shader shaderSpriteVertexColored;

	private GameObject outfieldGO;

	private GameObject pitchAndLogoGO;

	private GameObject crowdGO;

	private GameObject stadiumGO;

	private GameObject FCL_Ground_Shade;

	private GameObject fourLineDiffuseGO;

	private GameObject fourLineDiffuseInstanceGO;

	private GameObject stumpStick1;

	private GameObject stumpStick2;

	private GameObject stumpStick3;

	private GameObject stumpStick4;

	private GameObject stumpStick5;

	private float batsmanStep = 0.3f;

	private Vector2 prevMousePos;

	private bool WicketKeeperReachedToStump;

	public bool CanShowCountDown;

	private float BallHitTime;

	private int diff;

	private bool StopKeeper;

	private GameObject shadowHolder;

	private bool isBowled;

	private string traceStr = string.Empty;

	private bool rayCastOn = true;

	private Collider ballSphereCollider;

	private bool canShowPartnerShip = true;

	private float initRotationSpeed = 360f;

	private float rotationSpeed = 360f;

	private float zoomSpeed = 1.5f;

	private float zoomMin = 0.5f;

	private float zoomMax = 1f;

	private string animationStatus = "idle";

	private float freezingStartTime;

	private float freezingDuring = 0.5f;

	private float scaleDuringFreeze;

	public GameObject tutorialArrow;

	private string battingTeam;

	private string bowlingTeam;

	private bool newInning = true;

	private string preShader;

	private string stumpPreShader;

	private bool earthIsBurnt;

	private bool playedOnce;

	private bool stumpBlown;

	private float wicketKeeperAdjacentLength;

	private float wicketKeeperHypotenuse;

	private float wicketKeeperOppositeLength;

	private float wicketKeeperThetaBtwAdjAndHyp;

	private bool wicketKeeperIsActive;

	private string wicketKeeperStatus = string.Empty;

	private bool wicketKeeperCatchingAnimationSelected;

	private float wicketKeeperCatchingFrame;

	private string wicketKeeperAnimationClip;

	private float wicketKeeprMaxCatchingDistance = 1.75f;

	private bool wideBallChecked;

	private bool wideBall;

	private float shotExecutionTime;

	private float shotCompletionDuration = 0.5f;

	private bool computerBatsmanNewRunAttempt;

	private bool showingPyschOutEffect;

	private int pyschOutEffectCount;

	private float topDownViewStartTime;

	private float topDownViewZoomingSecs = 0.8f;

	private bool cameraToKeeper;

	private bool ballOverTheFence;

	private float boundaryFenceHeight = 1.5f;

	private string boundaryAction = string.Empty;

	private bool ballBoundaryReflection;

	private bool runOut;

	private bool takingRun;

	private bool isRunOut;

	private int umpireRunDirection;

	private float strikerSpeed;

	private float nonStrikerSpeed;

	private string strikerStatus;

	private string nonStrikerStatus;

	private GameObject striker;

	private GameObject nonStriker;

	private float strikerRunningAngle;

	private float nonStrikerRunningAngle;

	private float introRotationSpeed = 5f;

	private float bowlingSpeed;

	private bool touchDeviceShotInput;

	private float fielderSpeed = 7f;

	private List<int> activeFielderNumber = new List<int>();

	private List<string> activeFielderAction = new List<string>();

	private float batsmanWaitSeconds = 0.2f;

	private float bowlerWaitSeconds = 0.5f;

	private float currentBallStartTime;

	private float gamePausedTimeScale;

	private bool isFreeHit;

	private bool isJokerFreeHit;

	private bool ballHitTheBall;

	private bool overStepBall;

	private bool isNoBallSet;

	private string noBallRunUpdateStatus = string.Empty;

	private bool isSlowMotionSetForNoBall;

	public bool lineFreeHit;

	private int runsScoredInLineNoBall;

	private float bowlerHeelPosForNoBall;

	public string lastBowledBall = "lineball";

	private float noBallActionWaitTime;

	private GameObject fielderToCatchTheBall;

	private GameObject celebrationBatsmanGO;

	public GameObject previewPanel;

	private bool isStumped;

	private bool stumped;

	private bool savedIsStumped;

	private bool wideWithStumpingSignalShown;

	public bool isnextball;

	private bool isEnhanced;

	private bool tightRunoutCall;

	private bool veryTightRunoutCall;

	private int stumpingReturnAnimationType;

	private string savedWicketKeeperAnimationClip;

	private float savedWicketKeeperCatchingFrame;

	private bool savedKeeperCaughtDiffCatch;

	private float savedBatsmanReturnToCreaseSpeed;

	private bool thirdUmpireRunoutReplaySkipped;

	private int savedReturnToCreaseAnimationId;

	private int BowlerAnimNumber = 1;

	private Animator anim;

	private GameObject halfBatsman;

	[Header("Ball texture")]
	public Texture2D ballTextureRed;

	public Texture2D ballTextureWhite;

	public Renderer ballTextureRenderer;

	public Renderer[] dummyBallTextureRenderer;

	public Renderer bowlerBallTexture;

	public Texture2D[] digitalBoardContent = new Texture2D[4];

	private Color32[] teamUniformColor = new Color32[16];

	private Vector3[] teamUniformGreyScale = new Vector3[16];

	private Color32[] teamSkinColor = new Color32[16];

	private Color32[] teamStripColor = new Color32[16];

	public Texture2D[] umpireTexture = new Texture2D[3];

	public Material[] umpireMaterial = new Material[2];

	private float nextPitchDistance;

	public GameObject resumeGO;

	public Text three;

	public Text two;

	public Text one;

	private Vector3 brightJerseyGreyScale = new Vector3(0f, 2.9f, -1.08f);

	private Vector3 darkJerseyGreyScale = new Vector3(0.45f, -5.45f, 1.13f);

	public float shotAngle;

	public bool mainCameraOnTopDownView;

	private bool warmUpOnce;

	private int savedJerseyIndex;

	private int jerseyIndexToChange = 1;

	public Text shotNameText;

	public GameObject[] hotspotReference;

	private int targetToWin;

	public Shader noLight;

	public Shader oneLight;

	public GameObject[] lights;

	private bool canAutoActivateBowler;

	private int batsmanOutIndex;

	private bool activateBowlerForReplay;

	public Transform edgeRefs;

	[NonSerialized]
	public bool noBall;

	[NonSerialized]
	public bool freeHit;

	private float savedSixDistance;

	private bool hardcoded;

	private float animval;

	public GameObject broadCastCamera;

	public bool gamePaused;

	public bool fieldRestriction = true;

	private Vector3 tempShadowPosition;

	private float shadowY = -0.1f;

	private int CheckCount;

	public string UmpireInitialDecision = string.Empty;

	private int umpireChance = 50;

	private int edgeChance = 50;

	private int aiReviewChance = 50;

	private Vector3[] path1 = new Vector3[2];

	public GameObject IrCam;

	public GameObject impactImg;

	public GameObject waveImg;

	public GameObject SideCam;

	public GameObject bat;

	public GameObject ultraEdgeImpact;

	public GameObject UltraEdgeCam;

	private Tweener ballTween;

	private Tweener waveTween;

	public bool ShowNotOutAnim;

	public bool ballTimeSaved;

	private bool positionSaved;

	private bool waveTweenPlayed;

	private float ultraEdgeImpactPosition;

	public bool customBallMovement;

	public bool changedBallMovement;

	private bool ballPlaced;

	private bool isEdged;

	private bool edgePositionSaved;

	private float safeDistance = 0.1f;

	private float edgeDistance = 0.025f;

	private float deviation = 0.13f;

	public GameObject[] ShadowsToDisable;

	private Vector3 savedEdgePosition;

	public float elapsedTime;

	public bool UltraEdgeCutscenePlaying;

	public bool umpireAnimationPlayed;

	public bool UserCanAskReview;

	public bool AiCanAskReview;

	private Vector3[] DefaultHotspotPositions = new Vector3[4];

	private Vector3[] DefaultBallPath = new Vector3[4];

	public int validBall;

	public int canCountBall;

	public int runsScored;

	public int extraRun;

	public int batsmanID;

	public int isWicket;

	public int wicketType;

	public int bowlerID;

	public int catcherID;

	public int batsmanOut;

	public bool isBoundary;

	public bool animStarted;

	public GameObject[] ballPath1;

	public float ballSpeed = 1f;

	public float travelTime = 0.08f;

	private bool DRSHardcode;

	private Vector3 savedEdgeTransform;

	public GameObject SnickoMeter;

	private Vector3 snickoPosition;

	public GameObject referencePath;

	public GameObject startingPoint;

	public GameObject endingPoint;

	private float timeToTravel;

	private bool canRecord = true;

	private bool shotExecuted;

	private bool ballPausedAtImpact;

	public float StartingFrame = 0.45f;

	public float EndingFrame = 0.5f;

	public float midFrame;

	public Image snickoStatus;

	public Sprite snickoEdged;

	public Sprite snickoNotEdged;

	private float battingTimingMeter;

	private float firstBounceMultiplier = 1f;

	private float horizontalSpeedMultiplier = 1f;

	private bool perfectShot;

	private bool mistimedShot;

	private bool updateBattingTimingMeterNeedle;

	public void MaxPowerUp()
	{
		agilityFactor = 0.2f;
		controlFactor = 0.2f;
		powerFactor = 0.2f;
		isEnhanced = false;
	}

	public void enhancedMode()
	{
		agilityFactor = (float)ObscuredPrefs.GetInt("agilityGrade") * 0.02f;
		controlFactor = (float)ObscuredPrefs.GetInt("controlGrade") * 0.02f;
		powerFactor = (float)ObscuredPrefs.GetInt("powerGrade") * 0.02f;
		isEnhanced = true;
	}

	private void InitializeEnvironment()
	{
		if (CONTROLLER.PlayModeSelected == 2 || CONTROLLER.PlayModeSelected == 7)
		{
			GameObject gameObject;
			Renderer component;
			for (int i = 1; i <= 9; i++)
			{
				gameObject = fielderSkin[i];
				component = gameObject.GetComponent<Renderer>();
				component.materials[0].shader = oneLight;
			}
			gameObject = fielderSkin[10];
			component = gameObject.GetComponent<Renderer>();
			component.materials[1].shader = oneLight;
			WicketKeeperSkinRendererComponent.materials[1].shader = oneLight;
			BowlerSkinRendererComponent.materials[1].shader = oneLight;
			BatsmanSkinRendererComponent.materials[1].shader = oneLight;
			RunnerSkinRendererComponent.materials[1].shader = oneLight;
			lights[0].SetActive(value: true);
			lights[1].SetActive(value: true);
		}
		else
		{
			GameObject gameObject;
			Renderer component;
			for (int j = 1; j <= 9; j++)
			{
				gameObject = fielderSkin[j];
				component = gameObject.GetComponent<Renderer>();
				component.materials[0].shader = noLight;
			}
			gameObject = fielderSkin[10];
			component = gameObject.GetComponent<Renderer>();
			component.materials[1].shader = noLight;
			WicketKeeperSkinRendererComponent.materials[1].shader = noLight;
			BowlerSkinRendererComponent.materials[1].shader = noLight;
			BatsmanSkinRendererComponent.materials[1].shader = noLight;
			RunnerSkinRendererComponent.materials[1].shader = noLight;
			lights[0].SetActive(value: false);
			lights[1].SetActive(value: false);
		}
	}

	public void SaveJerseyColor()
	{
		BatsmanSkinRendererComponent.materials[1].SetColor("_Color", teamUniformColor[jerseyIndexToChange]);
		BatsmanSkinRendererComponent.materials[1].SetColor("_PatternColor", teamStripColor[jerseyIndexToChange]);
		BatsmanSkinRendererComponent.materials[2].SetColor("_Color", teamSkinColor[jerseyIndexToChange]);
		BatsmanSkinRendererComponent.materials[1].SetVector("_GrayScale", teamUniformGreyScale[jerseyIndexToChange]);
	}

	public void RevertJerseyColor()
	{
		BatsmanSkinRendererComponent.materials[1].SetColor("_Color", teamUniformColor[CONTROLLER.BattingTeamIndex]);
		BatsmanSkinRendererComponent.materials[1].SetColor("_PatternColor", teamStripColor[CONTROLLER.BattingTeamIndex]);
		BatsmanSkinRendererComponent.materials[2].SetColor("_Color", teamSkinColor[CONTROLLER.BattingTeamIndex]);
		BatsmanSkinRendererComponent.materials[1].SetVector("_GrayScale", teamUniformGreyScale[CONTROLLER.BattingTeamIndex]);
	}

	public void Awake()
	{
		if (CONTROLLER.PlayModeSelected == 4 || CONTROLLER.PlayModeSelected == 5)
		{
			SOMatchStart = true;
			Server_Connection.instance.Get_ArcadeXPRank();
		}
		if (CONTROLLER.PlayModeSelected < 4)
		{
			QPMatchStart = true;
			//Server_Connection.instance.Get_XPRank();
		}
		CONTROLLER.pageName = "Ground";
		CONTROLLER.fromPreloader = false;
		Screen.sleepTimeout = -1;
		PlayerPrefs.SetInt("EncrytionSuccess", 1);
		BowlerAnimNumber = 1;
		TeamNames[0] = "Australia";
		TeamNames[1] = "Bangladesh";
		TeamNames[2] = "Canada";
		TeamNames[3] = "England";
		TeamNames[4] = "India";
		TeamNames[5] = "Ireland";
		TeamNames[6] = "Kenya";
		TeamNames[7] = "Netherlands";
		TeamNames[8] = "NewZealand";
		TeamNames[9] = "Pakistan";
		TeamNames[10] = "SouthAfrica";
		TeamNames[11] = "SriLanka";
		TeamNames[12] = "WestIndies";
		TeamNames[13] = "Zimbabwe";
		Renderer[] array = dummyBallTextureRenderer;
		foreach (Renderer renderer in array)
		{
			renderer.material.mainTexture = ballTextureWhite;
		}
		if (CONTROLLER.PlayModeSelected == 6)
		{
			//Singleton<AdIntegrate>.instance.HideAd();
		}
		bowlerBallTexture.material.mainTexture = ballTextureWhite;
		ballTextureRenderer.material.mainTexture = ballTextureWhite;
		if (CONTROLLER.PlayModeSelected != 2 && CONTROLLER.PlayModeSelected != 7)
		{
			ref Color32 reference = ref teamUniformColor[0];
			reference = new Color32(12, 59, 166, byte.MaxValue);
			ref Color32 reference2 = ref teamUniformColor[1];
			reference2 = new Color32(188, 163, 1, byte.MaxValue);
			ref Color32 reference3 = ref teamUniformColor[2];
			reference3 = new Color32(38, 179, 118, byte.MaxValue);
			ref Color32 reference4 = ref teamUniformColor[3];
			reference4 = new Color32(51, 87, 162, byte.MaxValue);
			ref Color32 reference5 = ref teamUniformColor[4];
			reference5 = new Color32(34, 118, 185, byte.MaxValue);
			ref Color32 reference6 = ref teamUniformColor[5];
			reference6 = new Color32(53, 156, 45, byte.MaxValue);
			ref Color32 reference7 = ref teamUniformColor[6];
			reference7 = new Color32(15, 101, 64, byte.MaxValue);
			ref Color32 reference8 = ref teamUniformColor[7];
			reference8 = new Color32(180, 44, 0, byte.MaxValue);
			ref Color32 reference9 = ref teamUniformColor[8];
			reference9 = new Color32(32, 32, 32, byte.MaxValue);
			ref Color32 reference10 = ref teamUniformColor[9];
			reference10 = new Color32(35, 73, 75, byte.MaxValue);
			ref Color32 reference11 = ref teamUniformColor[10];
			reference11 = new Color32(45, 164, 208, byte.MaxValue);
			ref Color32 reference12 = ref teamUniformColor[12];
			reference12 = new Color32(21, 77, 45, byte.MaxValue);
			ref Color32 reference13 = ref teamUniformColor[11];
			reference13 = new Color32(18, 59, 115, byte.MaxValue);
			ref Color32 reference14 = ref teamUniformColor[13];
			reference14 = new Color32(54, 58, 60, byte.MaxValue);
			ref Color32 reference15 = ref teamUniformColor[14];
			reference15 = new Color32(137, 0, 41, byte.MaxValue);
			ref Color32 reference16 = ref teamUniformColor[15];
			reference16 = new Color32(178, 0, 0, byte.MaxValue);
			ref Vector3 reference17 = ref teamUniformGreyScale[0];
			reference17 = darkJerseyGreyScale;
			ref Vector3 reference18 = ref teamUniformGreyScale[1];
			reference18 = brightJerseyGreyScale;
			ref Vector3 reference19 = ref teamUniformGreyScale[2];
			reference19 = darkJerseyGreyScale;
			ref Vector3 reference20 = ref teamUniformGreyScale[3];
			reference20 = darkJerseyGreyScale;
			ref Vector3 reference21 = ref teamUniformGreyScale[4];
			reference21 = brightJerseyGreyScale;
			ref Vector3 reference22 = ref teamUniformGreyScale[5];
			reference22 = brightJerseyGreyScale;
			ref Vector3 reference23 = ref teamUniformGreyScale[6];
			reference23 = darkJerseyGreyScale;
			ref Vector3 reference24 = ref teamUniformGreyScale[7];
			reference24 = brightJerseyGreyScale;
			ref Vector3 reference25 = ref teamUniformGreyScale[8];
			reference25 = brightJerseyGreyScale;
			ref Vector3 reference26 = ref teamUniformGreyScale[9];
			reference26 = brightJerseyGreyScale;
			ref Vector3 reference27 = ref teamUniformGreyScale[10];
			reference27 = brightJerseyGreyScale;
			ref Vector3 reference28 = ref teamUniformGreyScale[11];
			reference28 = brightJerseyGreyScale;
			ref Vector3 reference29 = ref teamUniformGreyScale[12];
			reference29 = brightJerseyGreyScale;
			ref Vector3 reference30 = ref teamUniformGreyScale[13];
			reference30 = brightJerseyGreyScale;
			ref Vector3 reference31 = ref teamUniformGreyScale[14];
			reference31 = new Vector3(0f, 1.8f, -1.08f);
			ref Vector3 reference32 = ref teamUniformGreyScale[15];
			reference32 = new Vector3(0f, 2.3f, -1.08f);
			ref Color32 reference33 = ref teamStripColor[0];
			reference33 = new Color32(217, 0, 12, byte.MaxValue);
			ref Color32 reference34 = ref teamStripColor[1];
			reference34 = new Color32(37, 46, 0, byte.MaxValue);
			ref Color32 reference35 = ref teamStripColor[2];
			reference35 = new Color32(180, 16, 16, byte.MaxValue);
			ref Color32 reference36 = ref teamStripColor[3];
			reference36 = new Color32(214, 3, 5, byte.MaxValue);
			ref Color32 reference37 = ref teamStripColor[4];
			reference37 = new Color32(161, 60, 21, byte.MaxValue);
			ref Color32 reference38 = ref teamStripColor[5];
			reference38 = new Color32(5, 23, 89, byte.MaxValue);
			ref Color32 reference39 = ref teamStripColor[6];
			reference39 = new Color32(180, 16, 16, byte.MaxValue);
			ref Color32 reference40 = ref teamStripColor[7];
			reference40 = new Color32(8, 18, 53, byte.MaxValue);
			ref Color32 reference41 = ref teamStripColor[8];
			reference41 = new Color32(212, 212, 212, byte.MaxValue);
			ref Color32 reference42 = ref teamStripColor[9];
			reference42 = new Color32(180, 155, 0, byte.MaxValue);
			ref Color32 reference43 = ref teamStripColor[10];
			reference43 = new Color32(7, 32, 49, byte.MaxValue);
			ref Color32 reference44 = ref teamStripColor[12];
			reference44 = new Color32(218, 122, 0, byte.MaxValue);
			ref Color32 reference45 = ref teamStripColor[11];
			reference45 = new Color32(142, 126, 9, byte.MaxValue);
			ref Color32 reference46 = ref teamStripColor[13];
			reference46 = new Color32(169, 0, 18, byte.MaxValue);
			ref Color32 reference47 = ref teamStripColor[14];
			reference47 = new Color32(183, 182, 11, byte.MaxValue);
			ref Color32 reference48 = ref teamStripColor[15];
			reference48 = new Color32(188, 140, 0, byte.MaxValue);
			ref Color32 reference49 = ref teamSkinColor[0];
			reference49 = new Color32(180, 180, 180, 1);
			ref Color32 reference50 = ref teamSkinColor[1];
			reference50 = new Color32(200, 200, 200, 1);
			ref Color32 reference51 = ref teamSkinColor[2];
			reference51 = new Color32(120, 120, 120, 1);
			ref Color32 reference52 = ref teamSkinColor[3];
			reference52 = new Color32(176, 176, 176, 1);
			ref Color32 reference53 = ref teamSkinColor[4];
			reference53 = new Color32(160, 160, 160, 1);
			ref Color32 reference54 = ref teamSkinColor[5];
			reference54 = new Color32(200, 200, 200, 1);
			ref Color32 reference55 = ref teamSkinColor[6];
			reference55 = new Color32(80, 80, 80, 1);
			ref Color32 reference56 = ref teamSkinColor[7];
			reference56 = new Color32(176, 176, 176, 1);
			ref Color32 reference57 = ref teamSkinColor[8];
			reference57 = new Color32(200, 200, 200, 1);
			ref Color32 reference58 = ref teamSkinColor[9];
			reference58 = new Color32(140, 140, 140, 1);
			ref Color32 reference59 = ref teamSkinColor[10];
			reference59 = new Color32(176, 176, 176, 1);
			ref Color32 reference60 = ref teamSkinColor[12];
			reference60 = new Color32(200, 200, 200, 1);
			ref Color32 reference61 = ref teamSkinColor[11];
			reference61 = new Color32(110, 110, 110, 1);
			ref Color32 reference62 = ref teamSkinColor[13];
			reference62 = new Color32(140, 140, 140, 1);
			ref Color32 reference63 = ref teamSkinColor[14];
			reference63 = new Color32(95, 95, 95, 1);
			ref Color32 reference64 = ref teamSkinColor[15];
			reference64 = new Color32(200, 200, 200, 1);
		}
		else if (CONTROLLER.PlayModeSelected == 7)
		{
			Renderer[] array2 = dummyBallTextureRenderer;
			foreach (Renderer renderer2 in array2)
			{
				renderer2.material.mainTexture = ballTextureRed;
			}
			bowlerBallTexture.material.mainTexture = ballTextureRed;
			ballTextureRenderer.material.mainTexture = ballTextureRed;
			for (int k = 0; k < 16; k++)
			{
				ref Color32 reference65 = ref teamUniformColor[k];
				reference65 = new Color32(151, 151, 151, byte.MaxValue);
				ref Color32 reference66 = ref teamStripColor[k];
				reference66 = new Color32(137, 137, 137, byte.MaxValue);
			}
			ref Color32 reference67 = ref teamSkinColor[0];
			reference67 = new Color32(180, 180, 180, 1);
			ref Color32 reference68 = ref teamSkinColor[1];
			reference68 = new Color32(200, 200, 200, 1);
			ref Color32 reference69 = ref teamSkinColor[2];
			reference69 = new Color32(120, 120, 120, 1);
			ref Color32 reference70 = ref teamSkinColor[3];
			reference70 = new Color32(176, 176, 176, 1);
			ref Color32 reference71 = ref teamSkinColor[4];
			reference71 = new Color32(160, 160, 160, 1);
			ref Color32 reference72 = ref teamSkinColor[5];
			reference72 = new Color32(200, 200, 200, 1);
			ref Color32 reference73 = ref teamSkinColor[6];
			reference73 = new Color32(80, 80, 80, 1);
			ref Color32 reference74 = ref teamSkinColor[7];
			reference74 = new Color32(176, 176, 176, 1);
			ref Color32 reference75 = ref teamSkinColor[8];
			reference75 = new Color32(200, 200, 200, 1);
			ref Color32 reference76 = ref teamSkinColor[9];
			reference76 = new Color32(140, 140, 140, 1);
			ref Color32 reference77 = ref teamSkinColor[10];
			reference77 = new Color32(176, 176, 176, 1);
			ref Color32 reference78 = ref teamSkinColor[12];
			reference78 = new Color32(200, 200, 200, 1);
			ref Color32 reference79 = ref teamSkinColor[11];
			reference79 = new Color32(110, 110, 110, 1);
			ref Color32 reference80 = ref teamSkinColor[13];
			reference80 = new Color32(140, 140, 140, 1);
			ref Color32 reference81 = ref teamSkinColor[14];
			reference81 = new Color32(95, 95, 95, 1);
			ref Color32 reference82 = ref teamSkinColor[15];
			reference82 = new Color32(200, 200, 200, 1);
		}
		else
		{
			if (CONTROLLER.tournamentType == "NPL")
			{
				ref Color32 reference83 = ref teamUniformColor[0];
				reference83 = new Color32(77, 0, 3, byte.MaxValue);
				ref Color32 reference84 = ref teamUniformColor[1];
				reference84 = new Color32(113, 99, 8, byte.MaxValue);
				ref Color32 reference85 = ref teamUniformColor[2];
				reference85 = new Color32(88, 89, 0, byte.MaxValue);
				ref Color32 reference86 = ref teamUniformColor[3];
				reference86 = new Color32(5, 20, 48, byte.MaxValue);
				ref Color32 reference87 = ref teamUniformColor[4];
				reference87 = new Color32(103, 49, 10, byte.MaxValue);
				ref Color32 reference88 = ref teamUniformColor[5];
				reference88 = new Color32(0, 29, 63, byte.MaxValue);
				ref Color32 reference89 = ref teamUniformColor[6];
				reference89 = new Color32(87, 27, 3, byte.MaxValue);
				ref Color32 reference90 = ref teamUniformColor[7];
				reference90 = new Color32(51, 0, 43, byte.MaxValue);
				ref Color32 reference91 = ref teamUniformColor[8];
				reference91 = new Color32(8, 61, 125, byte.MaxValue);
				ref Color32 reference92 = ref teamUniformColor[9];
				reference92 = new Color32(23, 14, 53, byte.MaxValue);
				ref Color32 reference93 = ref teamStripColor[0];
				reference93 = new Color32(72, 56, 7, byte.MaxValue);
				ref Color32 reference94 = ref teamStripColor[1];
				reference94 = new Color32(37, 46, 0, byte.MaxValue);
				ref Color32 reference95 = ref teamStripColor[2];
				reference95 = new Color32(0, 43, 87, byte.MaxValue);
				ref Color32 reference96 = ref teamStripColor[3];
				reference96 = new Color32(72, 1, 0, byte.MaxValue);
				ref Color32 reference97 = ref teamStripColor[4];
				reference97 = new Color32(36, 26, 22, byte.MaxValue);
				ref Color32 reference98 = ref teamStripColor[5];
				reference98 = new Color32(75, 61, 18, byte.MaxValue);
				ref Color32 reference99 = ref teamStripColor[6];
				reference99 = new Color32(6, 0, 63, byte.MaxValue);
				ref Color32 reference100 = ref teamStripColor[7];
				reference100 = new Color32(75, 67, 0, byte.MaxValue);
				ref Color32 reference101 = ref teamStripColor[8];
				reference101 = new Color32(79, 60, 0, byte.MaxValue);
				ref Color32 reference102 = ref teamStripColor[9];
				reference102 = new Color32(94, 58, 11, byte.MaxValue);
			}
			else if (CONTROLLER.tournamentType == "AUS")
			{
				ref Color32 reference103 = ref teamUniformColor[0];
				reference103 = new Color32(23, 67, 169, byte.MaxValue);
				ref Color32 reference104 = ref teamUniformColor[1];
				reference104 = new Color32(22, 128, 136, byte.MaxValue);
				ref Color32 reference105 = ref teamUniformColor[2];
				reference105 = new Color32(55, 21, 109, byte.MaxValue);
				ref Color32 reference106 = ref teamUniformColor[3];
				reference106 = new Color32(142, 23, 24, byte.MaxValue);
				ref Color32 reference107 = ref teamUniformColor[4];
				reference107 = new Color32(23, 129, 65, byte.MaxValue);
				ref Color32 reference108 = ref teamUniformColor[5];
				reference108 = new Color32(241, 84, 29, byte.MaxValue);
				ref Color32 reference109 = ref teamUniformColor[6];
				reference109 = new Color32(244, 20, 148, byte.MaxValue);
				ref Color32 reference110 = ref teamUniformColor[7];
				reference110 = new Color32(139, 195, 74, byte.MaxValue);
				ref Color32 reference111 = ref teamStripColor[0];
				reference111 = new Color32(7, 14, 23, byte.MaxValue);
				ref Color32 reference112 = ref teamStripColor[1];
				reference112 = new Color32(7, 21, 23, byte.MaxValue);
				ref Color32 reference113 = ref teamStripColor[2];
				reference113 = new Color32(112, 155, 23, byte.MaxValue);
				ref Color32 reference114 = ref teamStripColor[3];
				reference114 = new Color32(20, 7, 22, byte.MaxValue);
				ref Color32 reference115 = ref teamStripColor[4];
				reference115 = new Color32(8, 28, 9, byte.MaxValue);
				ref Color32 reference116 = ref teamStripColor[5];
				reference116 = new Color32(17, 10, 6, byte.MaxValue);
				ref Color32 reference117 = ref teamStripColor[6];
				reference117 = new Color32(21, 7, 23, byte.MaxValue);
				ref Color32 reference118 = ref teamStripColor[7];
				reference118 = new Color32(10, 17, 5, byte.MaxValue);
			}
			else if (CONTROLLER.tournamentType == "PAK")
			{
				ref Color32 reference119 = ref teamUniformColor[0];
				reference119 = new Color32(216, 26, 17, byte.MaxValue);
				ref Color32 reference120 = ref teamUniformColor[1];
				reference120 = new Color32(23, 26, 141, byte.MaxValue);
				ref Color32 reference121 = ref teamUniformColor[2];
				reference121 = new Color32(92, 240, 31, byte.MaxValue);
				ref Color32 reference122 = ref teamUniformColor[3];
				reference122 = new Color32(167, 149, 23, byte.MaxValue);
				ref Color32 reference123 = ref teamUniformColor[4];
				reference123 = new Color32(18, 12, 50, byte.MaxValue);
				ref Color32 reference124 = ref teamStripColor[0];
				reference124 = new Color32(206, 185, 19, byte.MaxValue);
				ref Color32 reference125 = ref teamStripColor[1];
				reference125 = new Color32(237, 120, 50, byte.MaxValue);
				ref Color32 reference126 = ref teamStripColor[2];
				reference126 = new Color32(6, 22, 9, byte.MaxValue);
				ref Color32 reference127 = ref teamStripColor[3];
				reference127 = new Color32(8, 9, 29, byte.MaxValue);
				ref Color32 reference128 = ref teamStripColor[4];
				reference128 = new Color32(195, 158, 20, byte.MaxValue);
			}
			ref Color32 reference129 = ref teamSkinColor[0];
			reference129 = new Color32(160, 160, 160, 1);
			ref Color32 reference130 = ref teamSkinColor[1];
			reference130 = new Color32(160, 160, 160, 1);
			ref Color32 reference131 = ref teamSkinColor[2];
			reference131 = new Color32(160, 160, 160, 1);
			ref Color32 reference132 = ref teamSkinColor[3];
			reference132 = new Color32(160, 160, 160, 1);
			ref Color32 reference133 = ref teamSkinColor[4];
			reference133 = new Color32(160, 160, 160, 1);
			ref Color32 reference134 = ref teamSkinColor[5];
			reference134 = new Color32(160, 160, 160, 1);
			ref Color32 reference135 = ref teamSkinColor[6];
			reference135 = new Color32(160, 160, 160, 1);
			ref Color32 reference136 = ref teamSkinColor[7];
			reference136 = new Color32(160, 160, 160, 1);
			ref Color32 reference137 = ref teamSkinColor[8];
			reference137 = new Color32(160, 160, 160, 1);
			ref Color32 reference138 = ref teamSkinColor[9];
			reference138 = new Color32(160, 160, 160, 1);
		}
		for (int l = 0; l < 4; l++)
		{
			ref Vector3 reference139 = ref DefaultHotspotPositions[l];
			reference139 = hotspotReference[l].transform.localPosition;
		}
		for (int m = 0; m < 4; m++)
		{
			ref Vector3 reference140 = ref DefaultBallPath[m];
			reference140 = ballPath1[m].transform.localPosition;
		}
		snickoPosition = SnickoMeter.transform.localPosition;
		UltraEdgeCamPosition = IrCam.transform.position;
		UltraEdgeCamRotation = IrCam.transform.eulerAngles;
		sideCamPos = SideCam.transform.position;
		sideCamRot = SideCam.transform.eulerAngles;
		savedEdgeTransform = edgeRefs.localPosition;
		umpireCameraTransform = umpireCamera.transform;
		introCameraTransform = introCamera.transform;
		replayCameraTransform = replayCamera.transform;
		rightSideCamTransform = rightSideCamera.transform;
		leftSideCamTransform = leftSideCamera.transform;
		ultraMotionCamTransform = ultraMotionCamera.transform;
		closeUpCamTransform = closeUpCamera.transform;
		groundGO = GameObject.Find("Blitz");
		hideableObjects = GameObject.Find("Blitz/HideableObjects");
		pitchAndLogoGO = GameObject.Find("Blitz/Pitch_collections");
		introCameraPivot = GameObject.Find("IntroCameraPivot");
		replayCameraScript = replayCamera.GetComponent<ReplaySmoothFollow>();
		replayCamera.enabled = false;
		UIGO = GameObject.Find("MainCamera");
		UICam = UIGO.GetComponent("Camera") as Camera;
		mainCamera = Camera.main;
		mainCameraTransform = mainCamera.transform;
		umpireCameraPivot = new GameObject("umpireCameraPivot");
		ball = GameObject.Find("Ball");
		ballTransform = ball.transform;
		ballRayCastReferenceGO = GameObject.Find("BallRayCastReference");
		ballRayCastReferenceGOTransform = ballRayCastReferenceGO.transform;
		fielder10FocusGObjToCollectTheBall = new GameObject("Fielder10FocusGObjToCollectTheBall");
		fielder10FocusGObjToCollectTheBall.transform.position = new Vector3(0f, 0f, 0f);
		ballSphereCollider = ball.GetComponent("SphereCollider") as SphereCollider;
		ballOriginGO = GameObject.Find("BallOrigin");
		bowlingSpotGO = GameObject.Find("BowlingSpot");
		bowlingSpotFullTossGO = GameObject.Find("BowlingSpotFullToss");
		TempBallStartPoint = GameObject.Find("TempBallStartPoint");
		bowlingSpotFullTossGO.SetActive(value: false);
		bowlingSpotModel = GameObject.Find("BowlingSpotModel");
		bowlingSpotRenderer = bowlingSpotModel.GetComponent<Renderer>();
		bowlingSpotTransform = bowlingSpotGO.transform;
		ballTimingOrigin = GameObject.Find("BallTimingOrigin");
		ballTimingOriginTransform = ballTimingOrigin.transform;
		batCollider = GameObject.Find("BatCollider");
		batCollider2 = GameObject.Find("BatCollider2");
		leftLowerLegObject = GameObject.Find("LeftLowerLeg");
		rightLowerLegObject = GameObject.Find("RightLowerLeg");
		leftUpperLegObject = GameObject.Find("LeftUpperLeg");
		rightUpperLegObject = GameObject.Find("RightUpperLeg");
		ninjaSlice = GameObject.Find("NinjaSlice");
		stump1Collider = GameObject.Find("Stump1Collider");
		stump2Collider = GameObject.Find("Stump2Collider");
		boardCollider = GameObject.Find("Board");
		ballTimingFirstBounce = GameObject.Find("BallTimingFirstBounce");
		ballCatchingSpot = GameObject.Find("BallCatchingSpot");
		ballCatchingSpotTransform = ballCatchingSpot.transform;
		ballTrail = ball.gameObject.GetComponent<TrailRenderer>();
		ballSpotAtCreaseLine = GameObject.Find("BallSpotAtCreaseLine");
		ballSpotAtStump = GameObject.Find("BallSpotAtStump");
		batsmanTransform = batsman.transform;
		batsmanAnimationComponent = batsman.GetComponent<Animation>();
		RHBatsmanMaxBowlLimitGO = GameObject.Find("RHBatsmanMaxBowlLimit");
		RHBatsmanMinBowlLimitGO = GameObject.Find("RHBatsmanMinBowlLimit");
		LHBatsmanMaxBowlLimitGO = GameObject.Find("LHBatsmanMaxBowlLimit");
		LHBatsmanMinBowlLimitGO = GameObject.Find("LHBatsmanMinBowlLimit");
		RHBatsmanInitPosition = batsmanTransform.position;
		LHBatsmanInitSpot = GameObject.Find("LHBatsmanInitSpot");
		LHBatsmanInitPosition = LHBatsmanInitSpot.transform.position;
		shotActivationMinLimit = GameObject.Find("ShotActivationMinLimit");
		shotActivationMaxLimit = GameObject.Find("ShotActivationMaxLimit");
		RHBatsmanBackwardLimit = GameObject.Find("RHBatsmanBackwardLimit");
		RHBatsmanForwardLimit = GameObject.Find("RHBatsmanForwardLimit");
		LHBatsmanBackwardLimit = GameObject.Find("LHBatsmanBackwardLimit");
		LHBatsmanForwardLimit = GameObject.Find("LHBatsmanForwardLimit");
		RHBMaxWideLimit = GameObject.Find("RHBMaxWideLimit");
		RHBMinWideLimit = GameObject.Find("RHBMinWideLimit");
		LHBMaxWideLimit = GameObject.Find("LHBMaxWideLimit");
		LHBMinWideLimit = GameObject.Find("LHBMinWideLimit");
		bowler = GameObject.Find("/Bowler");
		bowlerSkin = GameObject.Find("/Bowler/Bowler");
		bowlerBall = GameObject.Find("/Bowler/Sphere");
		groundGO = GameObject.Find("Blitz");
		hideableObjects = GameObject.Find("Blitz/HideableObjects");
		pitchAndLogoGO = GameObject.Find("Blitz/Pitch_collections");
		introCameraPivot = GameObject.Find("IntroCameraPivot");
		replayCameraScript = replayCamera.GetComponent<ReplaySmoothFollow>();
		replayCamera.enabled = false;
		UIGO = GameObject.Find("MainCamera");
		UICam = UIGO.GetComponent("Camera") as Camera;
		mainCamera = Camera.main;
		mainCameraTransform = mainCamera.transform;
		umpireCameraPivot = new GameObject("umpireCameraPivot");
		ball = GameObject.Find("Ball");
		ballTransform = ball.transform;
		impactBall = GameObject.Find("ImpactBall");
		impactBall.SetActive(value: false);
		ballRayCastReferenceGO = GameObject.Find("BallRayCastReference");
		ballRayCastReferenceGOTransform = ballRayCastReferenceGO.transform;
		fielder10FocusGObjToCollectTheBall = new GameObject("Fielder10FocusGObjToCollectTheBall");
		fielder10FocusGObjToCollectTheBall.transform.position = new Vector3(0f, 0f, 0f);
		ballSphereCollider = ball.GetComponent("SphereCollider") as SphereCollider;
		ballOriginGO = GameObject.Find("BallOrigin");
		bowlingSpotGO = GameObject.Find("BowlingSpot");
		bowlingSpotModel = GameObject.Find("BowlingSpotModel");
		bowlingSpotTransform = bowlingSpotGO.transform;
		ballTimingOrigin = GameObject.Find("BallTimingOrigin");
		ballTimingOriginTransform = ballTimingOrigin.transform;
		batCollider = GameObject.Find("BatCollider");
		leftLowerLegObject = GameObject.Find("LeftLowerLeg");
		rightLowerLegObject = GameObject.Find("RightLowerLeg");
		leftUpperLegObject = GameObject.Find("LeftUpperLeg");
		rightUpperLegObject = GameObject.Find("RightUpperLeg");
		ninjaSlice = GameObject.Find("NinjaSlice");
		stump1Collider = GameObject.Find("Stump1Collider");
		stump2Collider = GameObject.Find("Stump2Collider");
		boardCollider = GameObject.Find("Board");
		ballTimingFirstBounce = GameObject.Find("BallTimingFirstBounce");
		ballCatchingSpot = GameObject.Find("BallCatchingSpot");
		ballCatchingSpotTransform = ballCatchingSpot.transform;
		ballTrail = ball.gameObject.GetComponent<TrailRenderer>();
		ballTrialColorGradient = ballTrail.colorGradient;
		ballTrailMaterial = ballTrail.material;
		ballSpotAtCreaseLine = GameObject.Find("BallSpotAtCreaseLine");
		ballSpotAtStump = GameObject.Find("BallSpotAtStump");
		batsmanTransform = batsman.transform;
		batsmanAnimationComponent = batsman.GetComponent<Animation>();
		RHBatsmanMaxBowlLimitGO = GameObject.Find("RHBatsmanMaxBowlLimit");
		RHBatsmanMinBowlLimitGO = GameObject.Find("RHBatsmanMinBowlLimit");
		LHBatsmanMaxBowlLimitGO = GameObject.Find("LHBatsmanMaxBowlLimit");
		LHBatsmanMinBowlLimitGO = GameObject.Find("LHBatsmanMinBowlLimit");
		RHBatsmanInitPosition = batsmanTransform.position;
		LHBatsmanInitSpot = GameObject.Find("LHBatsmanInitSpot");
		LHBatsmanInitPosition = LHBatsmanInitSpot.transform.position;
		shotActivationMinLimit = GameObject.Find("ShotActivationMinLimit");
		shotActivationMaxLimit = GameObject.Find("ShotActivationMaxLimit");
		RHBatsmanBackwardLimit = GameObject.Find("RHBatsmanBackwardLimit");
		RHBatsmanForwardLimit = GameObject.Find("RHBatsmanForwardLimit");
		LHBatsmanBackwardLimit = GameObject.Find("LHBatsmanBackwardLimit");
		LHBatsmanForwardLimit = GameObject.Find("LHBatsmanForwardLimit");
		RHBMaxWideLimit = GameObject.Find("RHBMaxWideLimit");
		RHBMinWideLimit = GameObject.Find("RHBMinWideLimit");
		LHBMaxWideLimit = GameObject.Find("LHBMaxWideLimit");
		LHBMinWideLimit = GameObject.Find("LHBMinWideLimit");
		bowler = GameObject.Find("/Bowler");
		bowlerSkin = GameObject.Find("/Bowler/Bowler");
		bowlerBall = GameObject.Find("/Bowler/Sphere");
		batsmanLeftLegEdge = GameObject.Find("Batsman/Armature/Bone/food_ik_l/food_l/heal_ik_l/LeftShoeEdge");
		batsmanRightLegEdge = GameObject.Find("Batsman/Armature/Bone/food_ik_r/food_r/heal_ik_r/RightShoeEdge");
		batEdgeGO = GameObject.Find("Batsman/Armature/Bone/hand_r/hip_001/BatEdge");
		batTopEdgeTransform = GameObject.Find("Batsman/Armature/Bone/hand_r/hip_001/BatTopEdge").transform;
		batShadowHolderTransform = GameObject.Find("ShadowHolder/BatShadowHolder").transform;
		batsmanLeftLegEdgePoint = GameObject.Find("Batsman/Armature/Bone/hip/fight_l/shin_l/LeftLegEdgePoint");
		batsmanLeftShoeBackEdge = GameObject.Find("Batsman/Armature/Bone/food_ik_l/food_l/heal_ik_l/LeftShoeBackEdge");
		batsmanRightShoeBackEdge = GameObject.Find("Batsman/Armature/Bone/food_ik_r/food_r/heal_ik_r/RightShoeBackEdge");
		hideBowlingInterfaceSpot = GameObject.Find("HideBowlingInterface");
		userBowlingMinLimit = GameObject.Find("UserBowlingMinLimit");
		userBowlingMaxLimit = GameObject.Find("UserBowlingMaxLimit");
		userBowlingFullTossTrigger = GameObject.Find("UserBowlingMinFullTossTrigger");
		fielder10 = GameObject.Find("/Fielders/Fielder10");
		fielder10Transform = fielder10.transform;
		fielder10Skin = GameObject.Find("/Fielders/Fielder10/Fielder");
		fielder10Ball = GameObject.Find("/Fielders/Fielder10/Sphere");
		fielder10FastInit = GameObject.Find("Fielder10FastInit");
		fielder10SpinInit = GameObject.Find("Fielder10SpinInit");
		setFieldersPosition();
		fielderSkin[10] = GameObject.Find("/Fielders/Fielder10/Fielder");
		FielderSkinRendererComponent[10] = fielderSkin[10].GetComponent<Renderer>();
		slipFielderSpotForRHBSpin = GameObject.Find("SlipFielderSpotForRHBSpin");
		slipFielder2SpotForRHBSpin = GameObject.Find("SlipFielder2SpotForRHBSpin");
		wicketKeeper = GameObject.Find("WicketKeeper");
		wicketKeeperTransform = wicketKeeper.transform;
		wicketKeeperSkin = GameObject.Find("WicketKeeper/Armature/Wicket_keeper_");
		wicketKeeperBall = GameObject.Find("WicketKeeper/Armature/Sphere");
		wicketKeeperInitPosition4RHBFast = GameObject.Find("WicketKeeperInitPos4RHBFast").transform.position;
		wicketKeeperInitPosition4LHBFast = GameObject.Find("WicketKeeperInitPos4LHBFast").transform.position;
		wicketKeeperInitPosition4RHBSpin = GameObject.Find("WicketKeeperInitPos4RHBSpin").transform.position;
		wicketKeeperInitPosition4LHBSpin = GameObject.Find("WicketKeeperInitPos4LHBSpin").transform.position;
		wicketKeeperStraightBallStumping = GameObject.Find("WicketKeeperStraightBallStumpingPos");
		wicketKeeperLegSideBallStumping = GameObject.Find("WicketKeeperLegSideBallStumpingPos");
		wicketKeeperOffSideBallStumping = GameObject.Find("WicketKeeperOffSideBallStumpingPos");
		WKRefPoint = GameObject.Find("WicketKeeper/Armature/WKRefPoint");
		fielder10Ref = GameObject.Find("/Fielders/Fielder10/Ref");
		fielderStraightBallStumping = GameObject.Find("FielderStraightBallStumpingPos");
		fielderLegSideBallStumping = GameObject.Find("FielderLegSideBallStumpingPos");
		fielderOffSideBallStumping = GameObject.Find("FielderOffSideBallStumpingPos");
		runnerTransform = runner.transform;
		RHBNonStrikerRunningSpot = GameObject.Find("RHBNonStickerRunningSpot");
		RHBStrikerRunningSpot = GameObject.Find("RHBStickerRunningSpot");
		runnerStrikerRunningSpot = GameObject.Find("RunnerStickerRunningSpot");
		runnerNonStrikerRunningSpot = GameObject.Find("RunnerNonStickerRunningSpot");
		nonStrikerNearCreaseSpot = GameObject.Find("NonStickerNearCreaseSpot");
		strikerNearCreaseSpot = GameObject.Find("StickerNearCreaseSpot");
		runnerInitPosition = runnerTransform.position;
		nonStrikerReachSpot = GameObject.Find("NonStickerReachSpot");
		strikerReachSpot = GameObject.Find("StickerReachSpot");
		groundCenterPoint = GameObject.Find("GroundCenterPoint");
		groundCenterPointTransform = groundCenterPoint.transform;
		stump1 = GameObject.Find("Stump1");
		stump2 = GameObject.Find("Stump2");
		stump1Crease = GameObject.Find("Stump1Crease");
		stump2Crease = GameObject.Find("Stump2Crease");
		stump1Spot = GameObject.Find("Stump1Spot");
		stump2Spot = GameObject.Find("Stump2Spot");
		digitalScreenScale = digitalScreen.transform.localScale;
		mainUmpireTransform = mainUmpire;
		mainUmpireSkin = GameObject.Find("MainUmpire/Umpire");
		sideUmpireSkin = GameObject.Find("SideUmpire/Umpire");
		mainUmpireInitPosition = mainUmpireTransform.position;
		umpireLeftSideSpot = GameObject.Find("UmpireLeftSideSpot");
		umpireRightSideSpot = GameObject.Find("UmpireRightSideSpot");
		replayController = GameObject.Find("ReplayController");
		replayControllerTransform = replayController.transform;
		replayController = GameObject.Find("ReplayController");
		replayControllerTransform = replayController.transform;
		UIGO = GameObject.Find("MainCamera");
		HideBowlingSpot();
		ShowFullTossSpot(_Value: false);
		ballInitPosition = ballTransform.position;
		shadowHolder = GameObject.Find("ShadowHolder");
		batsmanAnimationComponent = batsman.GetComponent<Animation>();
		BowlerAnimationComponent = bowler.GetComponent<Animation>();
		WicketKeeperAnimationComponent = wicketKeeper.GetComponent<Animation>();
		MainUmpireAnimationComponent = mainUmpire.GetComponent<Animation>();
		SideUmpireAnimationComponent = sideUmpire.GetComponent<Animation>();
		Fielder10AnimationComponent = fielder10.GetComponent<Animation>();
		Stump1AnimationComponent = stump1.GetComponent<Animation>();
		Stump2AnimationComponent = stump2.GetComponent<Animation>();
		RunnerAnimationComponent = runner.GetComponent<Animation>();
		fielder10SkinRendererComponent = fielder10Skin.GetComponent<Renderer>();
		BatsmanSkinRendererComponent = batsmanSkin.GetComponent<Renderer>();
		RunnerSkinRendererComponent = runnerSkin.GetComponent<Renderer>();
		BowlerSkinRendererComponent = bowlerSkin.GetComponent<Renderer>();
		WicketKeeperSkinRendererComponent = wicketKeeperSkin.GetComponent<Renderer>();
		Fielder10BallSkinRendererComponent = fielder10Ball.GetComponent<Renderer>();
		BallSkinRendererComponent = ball.GetComponent<Renderer>();
		WicketKeeperBallSkinRendererComponent = wicketKeeperBall.GetComponent<Renderer>();
		BowlerBallSkinRendererComponent = bowlerBall.GetComponent<Renderer>();
		DigitalScreenRendererComponent = digitalScreen.GetComponent<Renderer>();
		BatColliderComponent = batCollider.GetComponent<Collider>();
		if (showShadows)
		{
			GameObject item;
			GameObject item2;
			for (int n = 1; n <= noOfFielders; n++)
			{
				item = GameObject.Find("FielderShadow" + n);
				ShadowsArray.Add(item);
				item2 = GameObject.Find("Fielders/Fielder" + n + "/Armature/Bone/hip/ShadowRef");
				ShadowRefArray.Add(item2);
			}
			item = GameObject.Find("ShadowHolder/BowlerShadow");
			ShadowsArray.Add(item);
			item2 = GameObject.Find("Fielders/Fielder10/Armature/Bone/hip/ShadowRef");
			ShadowRefArray.Add(item2);
			item = GameObject.Find("ShadowHolder/WicketKeeperShadow");
			ShadowsArray.Add(item);
			item2 = GameObject.Find("WicketKeeper/Armature/Bone/hip/ShadowRef");
			ShadowRefArray.Add(item2);
			item = GameObject.Find("ShadowHolder/BatsmanShadow");
			ShadowsArray.Add(item);
			item2 = GameObject.Find("Batsman/Armature/Bone/hip/ShadowRef");
			ShadowRefArray.Add(item2);
			item = GameObject.Find("ShadowHolder/RunnerShadow");
			ShadowsArray.Add(item);
			item2 = GameObject.Find("Runner/Armature/Bone/hip/ShadowRef");
			ShadowRefArray.Add(item2);
			item = GameObject.Find("ShadowHolder/MainUmpireShadow");
			ShadowsArray.Add(item);
			item2 = GameObject.Find("MainUmpire/metarig/hips/ShadowRef");
			ShadowRefArray.Add(item2);
			item = GameObject.Find("ShadowHolder/SideUmpireShadow");
			ShadowsArray.Add(item);
			item2 = GameObject.Find("SideUmpire/metarig/hips/ShadowRef");
			ShadowRefArray.Add(item2);
			item = GameObject.Find("ShadowHolder/BallShadow");
			ShadowsArray.Add(item);
			item2 = GameObject.Find("Ball/ShadowRef");
			ShadowRefArray.Add(item2);
			bowlerShadowRef = GameObject.Find("Bowler/Armature/Bone/hip/ShadowRef");
			bowlerShadowRefTransform = bowlerShadowRef.transform;
			for (int num = 0; num < ShadowRefArray.Count; num++)
			{
				ShadowRefArrayTransform.Add(ShadowRefArray[num].transform);
				ShadowsArrayTransform.Add(ShadowsArray[num].transform);
			}
			UpdateShadow();
			shadowHolder.SetActiveRecursively(state: true);
		}
		else
		{
			shadowHolder.SetActiveRecursively(state: false);
		}
		batsmanRefPoint = GameObject.Find("Batsman/Armature/BatsmanRefPoint").transform;
		ShotVariables.InitShotVariables();
		resumeGO.SetActive(value: false);
		HideBatShadow();
		//if (!Singleton<NavigationBack>.instance.disableDeviceBack)
		//{
		//	Singleton<NavigationBack>.instance.disableDeviceBack = true;
		//}
		//Singleton<NavigationBack>.instance.deviceBack = OpenPause;
		if (CONTROLLER.PlayModeSelected != 7)
		{
			MainUmpireSkinRendererComponent.materials[0].SetTexture("_PatternTex", umpireTexture[mainUmpireIndex]);
			SideUmpireSkinRendererComponent.materials[0].SetTexture("_PatternTex", umpireTexture[sideUmpireIndex]);
		}
		else
		{
			MainUmpireSkinRendererComponent.materials[0].SetTexture("_PatternTex", umpireTexture[2]);
			SideUmpireSkinRendererComponent.materials[0].SetTexture("_PatternTex", umpireTexture[2]);
		}
	}

	private void OpenPause()
	{
		if (Singleton<Scoreboard>.instance.pauseBtn.gameObject.activeInHierarchy && Singleton<Scoreboard>.instance.pauseBtn.enabled)
		{
			Singleton<PauseGameScreen>.instance.Hide(boolean: false);
		}
	}

	private void DisableBG()
	{
		Time.timeScale = 1f;
		BG.gameObject.SetActive(value: false);
	}

	public void Start()
	{
		enhancedMode();
		ballPickedByFielder = false;
		speedReduceFactor = 0f;
		setfrictionRation = false;
		callCancelRunFunc = false;
		isLbwOut = false;
		edgeCatch = false;
		topEdge = false;
		hasReachedFirstBounce = false;
		Physics.IgnoreLayerCollision(11, 8, ignore: true);
		Physics.IgnoreLayerCollision(11, 8, ignore: true);
		rightSideCamera.enabled = false;
		leftSideCamera.enabled = false;
		showPreviewCamera(status: false);
		umpireCamera.enabled = false;
		closeUpCamera.enabled = false;
		UltraEdgeVariables.InitUltraEdgeVariables();
		if (CONTROLLER.isFreeHitBall && CONTROLLER.matchType == "oneday" && CONTROLLER.GameStartsFromSave)
		{
			lineFreeHit = true;
			lastBowledBall = "overstep";
			CONTROLLER.isFreeHitBall = false;
			Singleton<Scoreboard>.instance.showFreeHitBg(canShow: true);
		}
		ShowFielder10(fielder10Status: false, ball10Status: false);
		ResetAll();
		if (Singleton<GameModel>.instance.action == -20)
		{
			Singleton<Intro>.instance.initGameIntro();
		}
		setPreviewCamPanel();
		ActivateColliders(boolean: false);
		stump2Collider.active = false;
		boardCollider.active = false;
	}

	private void setPreviewCamPanel()
	{
		float num = 267f;
		float num2 = 217f;
		float num3 = 1.33f;
		float num4 = -151f;
		float num5 = Screen.width;
		float num6 = Screen.height;
		float num7 = num5 / num6;
		float num8 = num * num7 / num3;
		float num9 = num4 * num7 / num3;
	}

	public void showPreviewCamera(bool status)
	{
		previewCamera.enabled = status;
		previewPanel.SetActive(status);
		if (Singleton<Scoreboard>.instance.scoreBoard.activeSelf)
		{
			Singleton<PreviewScreen>.instance.previewScreen.SetActive(!status);
		}
	}

	public void ShowFielder10(bool fielder10Status, bool ball10Status)
	{
		fielder10SkinRendererComponent.enabled = fielder10Status;
		Fielder10BallSkinRendererComponent.enabled = ball10Status;
		if (!fielder10Status || canActivateBowler)
		{
			return;
		}
		canActivateBowler = true;
		fielder10Action = "idle";
		if (ballStatus == "bowled" && !lineFreeHit)
		{
			Fielder10AnimationComponent.Play("appeal");
			Fielder10AnimationComponent["appeal"].speed = 0.45f;
		}
		if (lbwAppeal)
		{
			stayStartTime = Time.time + 2f;
			if (!lineFreeHit)
			{
				fielder10Action = "lbwAppeal";
				Fielder10AnimationComponent.Play("lbwAppeal");
				Fielder10AnimationComponent["lbwAppeal"].speed = 0.45f;
			}
			else
			{
				lbwAppeal = false;
			}
		}
	}

	private void setFieldersPosition()
	{
		for (int i = 1; i <= noOfFielders; i++)
		{
			fielder[i] = GameObject.Find("/Fielders/Fielder" + i);
			FielderAnimationComponent[i] = fielder[i].GetComponent<Animation>();
			fielderTransform[i] = fielder[i].transform;
			FielderBallReleasePointGOBJ[i] = GameObject.Find("/Fielders/Fielder" + i + "/Armature/Bone/hand_r/fms_r/ballRef");
			FielderBallReleasePointGOTransform[i] = FielderBallReleasePointGOBJ[i].transform;
			fielderRef[i] = GameObject.Find("/Fielders/Fielder" + i + "/Ref");
			fielderBall[i] = GameObject.Find("/Fielders/Fielder" + i + "/Sphere");
			fielderTransform[i].eulerAngles = new Vector3(fielderTransform[i].eulerAngles.x, 0f, fielderTransform[i].eulerAngles.z);
			ref Vector3 reference = ref fielderInitPosition[i];
			reference = fielderTransform[i].position;
			ref Vector3 reference2 = ref fieldRestriction1FielderPosition[i];
			reference2 = GameObject.Find("FieldRestriction1_Fielder" + i).transform.position;
			ref Vector3 reference3 = ref fieldRestriction2FielderPosition[i];
			reference3 = GameObject.Find("FieldRestriction2_Fielder" + i).transform.position;
			ref Vector3 reference4 = ref fieldRestriction3FielderPosition[i];
			reference4 = GameObject.Find("FieldRestriction3_Fielder" + i).transform.position;
			ref Vector3 reference5 = ref fieldRestriction4FielderPosition[i];
			reference5 = GameObject.Find("FieldRestriction4_Fielder" + i).transform.position;
			ref Vector3 reference6 = ref fieldRestriction5FielderPosition[i];
			reference6 = GameObject.Find("FieldRestriction5_Fielder" + i).transform.position;
			ref Vector3 reference7 = ref fieldRestriction6FielderPosition[i];
			reference7 = GameObject.Find("FieldRestriction6_Fielder" + i).transform.position;
			ref Vector3 reference8 = ref fieldRestriction7FielderPosition[i];
			reference8 = GameObject.Find("FieldRestriction7_Fielder" + i).transform.position;
			ref Vector3 reference9 = ref fieldRestriction8FielderPosition[i];
			reference9 = GameObject.Find("FieldRestriction8_Fielder" + i).transform.position;
			ref Vector3 reference10 = ref fieldRestriction9FielderPosition[i];
			reference10 = GameObject.Find("FieldRestriction9_Fielder" + i).transform.position;
			ref Vector3 reference11 = ref fieldRestriction10FielderPosition[i];
			reference11 = GameObject.Find("FieldRestriction10_Fielder" + i).transform.position;
			ref Vector3 reference12 = ref fieldRestriction11FielderPosition[i];
			reference12 = GameObject.Find("FieldRestriction11_Fielder" + i).transform.position;
			ref Vector3 reference13 = ref fieldRestriction12FielderPosition[i];
			reference13 = GameObject.Find("FieldRestriction12_Fielder" + i).transform.position;
			ref Vector3 reference14 = ref fieldRestriction13FielderPosition[i];
			reference14 = GameObject.Find("FieldRestriction13_Fielder" + i).transform.position;
			ref Vector3 reference15 = ref fieldRestriction14FielderPosition[i];
			reference15 = GameObject.Find("FieldRestriction14_Fielder" + i).transform.position;
			ref Vector3 reference16 = ref fieldRestriction15FielderPosition[i];
			reference16 = GameObject.Find("FieldRestriction15_Fielder" + i).transform.position;
			ref Vector3 reference17 = ref fieldRestriction16FielderPosition[i];
			reference17 = GameObject.Find("FieldRestriction16_Fielder" + i).transform.position;
			ref Vector3 reference18 = ref fieldRestriction17FielderPosition[i];
			reference18 = GameObject.Find("FieldRestriction17_Fielder" + i).transform.position;
			ref Vector3 reference19 = ref fieldRestriction18FielderPosition[i];
			reference19 = GameObject.Find("FieldRestriction18_Fielder" + i).transform.position;
			ref Vector3 reference20 = ref fieldRestriction19FielderPosition[i];
			reference20 = GameObject.Find("FieldRestriction19_Fielder" + i).transform.position;
			ref Vector3 reference21 = ref fieldRestriction20FielderPosition[i];
			reference21 = GameObject.Find("FieldRestriction20_Fielder" + i).transform.position;
			ref Vector3 reference22 = ref fieldRestriction21FielderPosition[i];
			reference22 = GameObject.Find("FieldRestriction21_Fielder" + i).transform.position;
			ref Vector3 reference23 = ref fieldRestriction22FielderPosition[i];
			reference23 = GameObject.Find("FieldRestriction22_Fielder" + i).transform.position;
			ref Vector3 reference24 = ref fieldRestriction23FielderPosition[i];
			reference24 = GameObject.Find("FieldRestriction23_Fielder" + i).transform.position;
			ref Vector3 reference25 = ref fieldRestriction24FielderPosition[i];
			reference25 = GameObject.Find("FieldRestriction24_Fielder" + i).transform.position;
			ref Vector3 reference26 = ref fieldRestriction25FielderPosition[i];
			reference26 = GameObject.Find("FieldRestriction25_Fielder" + i).transform.position;
			fielderChasePoint[i] = GameObject.Find("FielderChasePoint" + i);
			fielderNearToPitch.Add(item: false);
			fielderSkin[i] = GameObject.Find("/Fielders/Fielder" + i + "/Fielder");
			FielderSkinRendererComponent[i] = fielderSkin[i].GetComponent<Renderer>();
		}
	}

	public void ShowBowler(bool showStatus)
	{
		BowlerSkinRendererComponent.enabled = showStatus;
	}

	public void ResetAll()
	{
		nextPitchDistance = 0f;
		ballHitTheBall = false;
		isNoBallSet = false;
		isSlowMotionSetForNoBall = false;
		noBallActionWaitTime = 0f;
		distanceBetweenUmpireAndFielder(boolean: true);
		keeperCaughtDiffCatch = false;
		fielderThrowElapsedTime = 0f;
		canShowFCLPowers = false;
		ballPickedByFielder = false;
		setfrictionRation = false;
		isBowled = false;
		umpireChance = 50;
		edgeChance = 50;
		bowlingSpotFullTossGO.SetActive(value: false);
		nearestFielderIndex = -1;
		goToBoundaryAnim = string.Empty;
		boundaryHeight = 0f;
		umpireAnimationPlayed = false;
		tempSpot1 = default(Vector3);
		tempSpot2 = default(Vector3);
		canSwipeNow = false;
		edgeRefs.localPosition = savedEdgeTransform;
		cancelRunDirectionFactor = 1;
		CancelRun = false;
		callCancelRunFunc = false;
		moveMainUmpire = true;
		perfectShot = false;
		isFielderthrown = false;
		Singleton<MainCameraMovement>.instance.canMove = false;
		topEdge = false;
		hasReachedFirstBounce = false;
		sixDistanceCamera.enabled = false;
		hardcoded = false;
		ShowUltraEdgeCam(canShow: false);
		DRSHardcode = false;
		savedSixDistance = 0f;
		if (currentBatsmanHand == "right")
		{
			batsmanTransform.position = RHBatsmanInitPosition;
		}
		else
		{
			batsmanTransform.position = LHBatsmanInitPosition;
		}
		canKeeperCollectBall = false;
		edgeCatch = false;
		updateBattingTimingMeterNeedle = false;
		Singleton<BattingControls>.instance.battingMeter.SetActive(value: false);
		Singleton<BattingControls>.instance.GreedyAdsImage.SetActive(value: true);
		isLbwOut = false;
		throwToGO = wicketKeeper;
		Singleton<UILookAt>.instance.show(flag: false);
		ballTrail.enabled = false;
		aiCancelRun = false;
		takeRun = false;
		if (!replayMode)
		{
			edgePositionSaved = false;
			elapsedTime = 0f;
		}
		if (CONTROLLER.BowlingTeamIndex == CONTROLLER.myTeamIndex)
		{
			fielderSpeed = 7f * (1f + agilityFactor);
		}
		else
		{
			fielderSpeed = 7f;
		}
		touchDeviceShotInput = false;
		ballNoOfBounce = 0;
		ballProjectileAngle = 270f;
		ballProjectileHeight = 2.15f;
		horizontalSpeed = 18f;
		swingProjectileAngle = 0f;
		swingValue = 0f;
		swingingBall = false;
		slipShot = false;
		ballToFineLeg = false;
		Singleton<Intro>.instance.tempCutScene.enabled = false;
		ballPreCatchingDistance = 1f;
		bowlerRunningSpeed = 5f;
		pauseTheBall = false;
		shotPlayed = string.Empty;
		ballResult = string.Empty;
		canTakeRun = false;
		applyBallFiction = false;
		currentBallNoOfRuns = 0;
		if (Singleton<MainCameraMovement>.instance != null)
		{
			Singleton<MainCameraMovement>.instance.Reset();
		}
		if (Singleton<LeftFovLerp>.instance != null)
		{
			Singleton<LeftFovLerp>.instance.Reset();
		}
		if (Singleton<RightFovLerp>.instance != null)
		{
			Singleton<RightFovLerp>.instance.Reset();
		}
		SmoothLookAt.canLookAt = true;
		wideBallChecked = false;
		shortestBallPickupDistance = 1000f;
		ballReleased = false;
		ballOverTheFence = false;
		lbwAppeal = false;
		LBW = false;
		ballInline = false;
		throwingFirstBounceDistance = 0f;
		canBe4or6 = 6;
		canAutoActivateBowler = false;
		fielderAppealForRunOut = false;
		Stump1AnimationComponent.Play("idle");
		Stump2AnimationComponent.Play("idle");
		boardCollider.active = false;
		mainUmpireTransform.position = mainUmpireInitPosition;
		mainUmpireTransform.localScale = new Vector3(1f, mainUmpireTransform.localScale.y, mainUmpireTransform.localScale.z);
		mainUmpireTransform.eulerAngles = new Vector3(mainUmpireTransform.eulerAngles.x, 0f, mainUmpireTransform.eulerAngles.z);
		MainUmpireAnimationComponent.Play("IdleGetReady");
		SideUmpireAnimationComponent.Play("Idle");
		umpireCameraTransform.eulerAngles = new Vector3(10f, umpireCameraTransform.eulerAngles.y, umpireCameraTransform.eulerAngles.z);
		canActivateBowler = false;
		fielder10Action = string.Empty;
		Fielder10AnimationComponent.Play("idle");
		fielder10Transform.eulerAngles = new Vector3(0f, 0f, 0f);
		if (UnityEngine.Random.Range(0, 2) == 0)
		{
			WicketKeeperAnimationComponent.Play("WCCLite_KeeperPreIdle01");
		}
		else
		{
			WicketKeeperAnimationComponent.Play("WCCLite_KeeperPreIdle01");
		}
		WicketKeeperBallSkinRendererComponent.enabled = false;
		wicketKeeperTransform.eulerAngles = new Vector3(0f, 180f, 0f);
		wicketKeeperCatchingAnimationSelected = false;
		ResetFielders();
		if (!replayMode)
		{
			Singleton<BowlingControls>.instance.SpeedArrow.transform.localPosition = new Vector3(-57f, Singleton<BowlingControls>.instance.SpeedArrow.transform.localPosition.y, Singleton<BowlingControls>.instance.SpeedArrow.transform.localPosition.z);
			Singleton<BowlingControls>.instance.YelloFillMeter.fillAmount = 0f;
			ballSpinningSpeedInX = UnityEngine.Random.Range(-3600, -1800);
			ballSpinningSpeedInZ = UnityEngine.Random.Range(-3600, -1800);
			savedBallSpinningSpeedInX = ballSpinningSpeedInX;
			savedBallSpinningSpeedInZ = ballSpinningSpeedInZ;
			IsFullTossBall = false;
		}
		else
		{
			ballSpinningSpeedInX = savedBallSpinningSpeedInX;
			ballSpinningSpeedInZ = savedBallSpinningSpeedInZ;
		}
		if (currentBowlerType == "fast")
		{
			fielder10Transform.position = new Vector3(fielder10FastInit.transform.position.x, 0f, -4.01f);
			if (BowlerAnimNumber == 1)
			{
				bowler.transform.position = new Vector3(bowler.transform.position.x, bowler.transform.position.y, -25.1f);
			}
			else if (BowlerAnimNumber == 2)
			{
				bowler.transform.position = new Vector3(bowler.transform.position.x, bowler.transform.position.y, -19.5f);
			}
			if (!replayMode)
			{
				ballSpinningSpeedInZ = UnityEngine.Random.Range(-500, 500);
				savedBallSpinningSpeedInZ = ballSpinningSpeedInZ;
			}
			else
			{
				ballSpinningSpeedInZ = savedBallSpinningSpeedInZ;
			}
			savedBallSpinningSpeedInZ = ballSpinningSpeedInZ;
			if (currentBatsmanHand == "right")
			{
				wicketKeeperTransform.position = wicketKeeperInitPosition4RHBFast;
			}
			else if (currentBatsmanHand == "left")
			{
				wicketKeeperTransform.position = wicketKeeperInitPosition4LHBFast;
			}
		}
		else if (currentBowlerType == "spin")
		{
			fielder10Transform.position = new Vector3(fielder10SpinInit.transform.position.x, 0f, -6.9f);
			if (BowlerAnimNumber == 1)
			{
				bowler.transform.position = new Vector3(bowler.transform.position.x, bowler.transform.position.y, -6f);
			}
			else if (BowlerAnimNumber == 2)
			{
				bowler.transform.position = new Vector3(bowler.transform.position.x, bowler.transform.position.y, -6f);
			}
			if (currentBatsmanHand == "right")
			{
				wicketKeeperTransform.position = wicketKeeperInitPosition4RHBSpin;
			}
			else if (currentBatsmanHand == "left")
			{
				wicketKeeperTransform.position = wicketKeeperInitPosition4LHBSpin;
			}
		}
		else if (currentBowlerType == "medium")
		{
			fielder10Transform.position = new Vector3(fielder10SpinInit.transform.position.x, 0f, -5.74f);
			if (BowlerAnimNumber == 1)
			{
				bowler.transform.position = new Vector3(0.91f, bowler.transform.position.y, -12.48f);
			}
			else if (BowlerAnimNumber == 2)
			{
				bowler.transform.position = new Vector3(bowler.transform.position.x, bowler.transform.position.y, -10.5f);
			}
			if (currentBatsmanHand == "right")
			{
				wicketKeeperTransform.position = wicketKeeperInitPosition4RHBFast;
			}
			else if (currentBatsmanHand == "left")
			{
				wicketKeeperTransform.position = wicketKeeperInitPosition4LHBFast;
			}
		}
		ballTransform.position = ballInitPosition;
		ballTransform.eulerAngles = new Vector3(0f, 2f, 180f);
		if (!replayMode)
		{
			saveEdgeCatch = false;
			powerShot = false;
			bowlerHeelPosForNoBall = 0f;
			overStepBall = false;
			noBall = false;
			noBallRunUpdateStatus = string.Empty;
		}
		if (!replayMode && lastBowledBall == "overstep")
		{
			Singleton<Scoreboard>.instance.showFreeHitBg(canShow: true);
		}
		if (!replayMode && !lineFreeHit)
		{
			CONTROLLER.isLineFreeHitBallCompleted = true;
		}
		if (!replayMode && lineFreeHit)
		{
			CONTROLLER.isLineFreeHitBallCompleted = false;
		}
		ShowBall(status: false);
		if (currentBatsmanHand == "right")
		{
			batsmanTransform.position = RHBatsmanInitPosition;
			batsmanTransform.localScale = new Vector3(1f, batsmanTransform.localScale.y, batsmanTransform.localScale.z);
			batsmanTransform.eulerAngles = new Vector3(batsmanTransform.eulerAngles.x, 270f, batsmanTransform.eulerAngles.z);
			batsmanInitXPos = batsmanTransform.position.x;
		}
		else if (currentBatsmanHand == "left")
		{
			batsmanTransform.position = LHBatsmanInitPosition;
			batsmanTransform.localScale = new Vector3(-1f, batsmanTransform.localScale.y, batsmanTransform.localScale.z);
			batsmanTransform.eulerAngles = new Vector3(batsmanTransform.eulerAngles.x, 90f, batsmanTransform.eulerAngles.z);
			batsmanInitXPos = batsmanTransform.position.x;
		}
		batsmanAnimationComponent.Play("WCCLite_BatsmanIdle");
		squareLegGlance = false;
		squareCutDrive = false;
		if (currentBowlerHand == "right")
		{
			bowler.transform.localScale = new Vector3(1f, bowler.transform.localScale.y, bowler.transform.localScale.z);
		}
		else if (currentBowlerHand == "left")
		{
			bowler.transform.localScale = new Vector3(-1f, bowler.transform.localScale.y, bowler.transform.localScale.z);
		}
		BowlerAnimationComponent.Play("WCCLite_BowlerIdle");
		BowlerBallSkinRendererComponent.enabled = true;
		hideBowlingInterface = false;
		userBowlerCanMoveBowlingSpot = false;
		userBowlingSpotSelected = false;
		ballStatus = string.Empty;
		canMakeShot = false;
		batsmanTriggeredShot = false;
		batsmanMadeShot = false;
		batsmanCanMoveLeftRight = false;
		batsmanOnLeftRightMovement = false;
		powerKeyDown = false;
		ballOnboundaryLine = false;
		wicketKeeperIsActive = false;
		wicketKeeperStatus = string.Empty;
		stopTheFielders = false;
		runnerTransform.position = runnerInitPosition;
		runnerInitPosition = runnerTransform.position;
		RunnerAnimationComponent.Play("WCCLite_RunnerIdle");
		RunnerAnimationComponent["WCCLite_RunnerIdle"].speed = 0.5f;
		runnerTransform.eulerAngles = new Vector3(runnerTransform.eulerAngles.x, 180f, runnerTransform.eulerAngles.z);
		strikerStatus = "idle";
		nonStrikerStatus = "idle";
		takingRun = false;
		isRunOut = false;
		runOut = false;
		boundaryAction = string.Empty;
		isStumped = false;
		stumped = false;
		mainCameraOnTopDownView = false;
		mainCamera.fieldOfView = 8f;
		mainCameraTransform.eulerAngles = new Vector3(7.5f, mainCameraTransform.eulerAngles.y, mainCameraTransform.eulerAngles.z);
		rightSideCamera.enabled = false;
		leftSideCamera.enabled = false;
		showPreviewCamera(status: false);
		umpireCamera.enabled = false;
		closeUpCamera.enabled = false;
		sideCameraSelected = false;
		if (CONTROLLER.cameraType == 1)
		{
			rightSideCamTransform.position = new Vector3(-28f, 7.5f, 0f);
			leftSideCamTransform.position = new Vector3(28f, 7.5f, 0f);
		}
		if (CONTROLLER.cameraType == 0)
		{
			rightSideCamera.fieldOfView = 50f;
			leftSideCamera.fieldOfView = 50f;
		}
		else
		{
			rightSideCamera.fieldOfView = 50f;
			leftSideCamera.fieldOfView = 50f;
		}
		cameraToKeeper = false;
		upArrowKeyDown = false;
		downArrowKeyDown = false;
		leftArrowKeyDown = false;
		rightArrowKeyDown = false;
		ShowBowler(showStatus: true);
		ShowFielder10(fielder10Status: false, ball10Status: false);
		zoomCameraToBowlerStartTime = -1f;
		if (playIntro)
		{
			action = -1;
			mainCamera.enabled = false;
			InitCamera();
		}
		else
		{
			action = -2;
			InitCamera();
			zoomCameraToBowlerStartTime = Time.time;
			FielderExtraActions();
			mainCamera.enabled = true;
			introCamera.enabled = false;
			StartCoroutine(FieldersRandomWarmUpAnimation());
		}
		SetBowlerSide();
		resetUltraMotionVariables();
		ball.GetComponent<Rigidbody>().Sleep();
		WicketKeeperReachedToStump = false;
		UpdateShadowsAndPreview();
	}

	private void resetUltraMotionVariables()
	{
		ultraMotionCamera.enabled = false;
		diff = 0;
		StopKeeper = false;
		if (currentBatsmanHand == "left")
		{
			ultraMotionCamTransform.position = new Vector3(9f, 2f, 9f);
			ultraMotionCamTransform.eulerAngles = new Vector3(7f, -90f, 0f);
		}
		else
		{
			ultraMotionCamTransform.position = new Vector3(-9f, 2f, 9f);
			ultraMotionCamTransform.eulerAngles = new Vector3(7f, 90f, 0f);
		}
	}

	private IEnumerator FieldersRandomWarmUpAnimation()
	{
		yield return new WaitForSeconds(0.5f);
		for (int i = 1; i <= noOfFielders; i++)
		{
			int num = UnityEngine.Random.Range(1, 6);
			FielderAnimationComponent[i].Play("WCCLite_FielderIdle0" + num);
		}
	}

	public bool isPowerShot()
	{
		return powerShot;
	}

	private void SetBowlerSide()
	{
		CONTROLLER.BowlerSide = bowlerSide;
		if (bowlerSide == "left")
		{
			runnerTransform.position = new Vector3(runnerInitPosition.x, runnerTransform.position.y, runnerTransform.position.z);
			runnerTransform.localScale = new Vector3(0f - Mathf.Abs(runnerTransform.localScale.x), runnerTransform.localScale.y, runnerTransform.localScale.z);
			Singleton<PreviewScreen>.instance.UpdateBowlerSideChangeUI("right");
			if (currentBowlerHand == "right")
			{
				ballOriginGO.transform.position = new Vector3(-0.7f, ballOriginGO.transform.position.y, ballOriginGO.transform.position.z);
				ballTransform.position = new Vector3(ballOriginGO.transform.position.x, ballTransform.position.y, ballTransform.position.z);
				if (currentBowlerType == "fast")
				{
					if (BowlerAnimNumber == 1)
					{
						bowler.transform.position = new Vector3(-0.6f, bowler.transform.position.y, bowler.transform.position.z);
						fielder10Transform.position = new Vector3(-1.24f, 0f, -3.38f);
					}
					else if (BowlerAnimNumber == 2)
					{
						bowler.transform.position = new Vector3(-0.78f, bowler.transform.position.y, bowler.transform.position.z);
						fielder10Transform.position = new Vector3(-1.23f, 0f, -6.75f);
					}
				}
				else if (currentBowlerType == "spin")
				{
					mainUmpireTransform.position = mainUmpireInitPosition;
					if (BowlerAnimNumber == 1)
					{
						bowler.transform.position = new Vector3(-1.35f, bowler.transform.position.y, bowler.transform.position.z);
						fielder10Transform.position = new Vector3(-0.85f, 0f, -7.87f);
					}
					else if (BowlerAnimNumber == 2)
					{
						bowler.transform.position = new Vector3(-1.7f, bowler.transform.position.y, bowler.transform.position.z);
						fielder10Transform.position = new Vector3(-0.96f, 0f, -8.5f);
					}
				}
				else if (currentBowlerType == "medium")
				{
					if (BowlerAnimNumber == 1)
					{
						bowler.transform.position = new Vector3(-0.95f, bowler.transform.position.y, bowler.transform.position.z);
						fielder10Transform.position = new Vector3(-0.77f, 0f, -6.11f);
					}
					else if (BowlerAnimNumber == 2)
					{
						bowler.transform.position = new Vector3(-0.9f, bowler.transform.position.y, bowler.transform.position.z);
						fielder10Transform.position = new Vector3(-0.9f, 0f, -7f);
					}
				}
			}
			else
			{
				if (!(currentBowlerHand == "left"))
				{
					return;
				}
				ballOriginGO.transform.position = new Vector3(-0.89f, ballOriginGO.transform.position.y, ballOriginGO.transform.position.z);
				ballTransform.position = new Vector3(ballOriginGO.transform.position.x, ballTransform.position.y, ballTransform.position.z);
				if (currentBowlerType == "fast")
				{
					if (BowlerAnimNumber == 1)
					{
						bowler.transform.position = new Vector3(-0.93f, bowler.transform.position.y, bowler.transform.position.z);
						fielder10Transform.position = new Vector3(-0.31f, 0f, -3.4f);
					}
					else if (BowlerAnimNumber == 2)
					{
						bowler.transform.position = new Vector3(-0.85f, bowler.transform.position.y, bowler.transform.position.z);
						fielder10Transform.position = new Vector3(-0.4f, 0f, -6.75f);
					}
				}
				else if (currentBowlerType == "spin")
				{
					if (BowlerAnimNumber == 1)
					{
						bowler.transform.position = new Vector3(-0.2f, bowler.transform.position.y, bowler.transform.position.z);
						mainUmpireTransform.position = new Vector3(0.037f, 0f, -19.5f);
						fielder10Transform.position = new Vector3(-0.71f, 0f, -7.85f);
					}
					else if (BowlerAnimNumber == 2)
					{
						bowler.transform.position = new Vector3(-0.68f, bowler.transform.position.y, bowler.transform.position.z);
						fielder10Transform.position = new Vector3(-1.5f, 0f, -8.5f);
					}
				}
				else if (currentBowlerType == "medium")
				{
					if (BowlerAnimNumber == 1)
					{
						bowler.transform.position = new Vector3(-0.68f, bowler.transform.position.y, bowler.transform.position.z);
						fielder10Transform.position = new Vector3(-0.91f, 0f, -6.11f);
					}
					else if (BowlerAnimNumber == 2)
					{
						bowler.transform.position = new Vector3(-0.95f, bowler.transform.position.y, bowler.transform.position.z);
						fielder10Transform.position = new Vector3(-0.9f, 0f, -7f);
					}
				}
			}
		}
		else
		{
			if (!(bowlerSide == "right"))
			{
				return;
			}
			runnerTransform.position = new Vector3(runnerInitPosition.x * -1f, runnerTransform.position.y, runnerTransform.position.z);
			runnerTransform.localScale = new Vector3(Mathf.Abs(runnerTransform.localScale.x), runnerTransform.localScale.y, runnerTransform.localScale.z);
			Singleton<PreviewScreen>.instance.UpdateBowlerSideChangeUI("left");
			if (currentBowlerHand == "right")
			{
				ballOriginGO.transform.position = new Vector3(0.91f, ballOriginGO.transform.position.y, ballOriginGO.transform.position.z);
				ballTransform.position = new Vector3(ballOriginGO.transform.position.x, ballTransform.position.y, ballTransform.position.z);
				if (currentBowlerType == "fast")
				{
					if (BowlerAnimNumber == 1)
					{
						bowler.transform.position = new Vector3(0.93f, bowler.transform.position.y, bowler.transform.position.z);
						fielder10Transform.position = new Vector3(0.31f, 0f, -3.39f);
					}
					else if (BowlerAnimNumber == 2)
					{
						bowler.transform.position = new Vector3(0.93f, bowler.transform.position.y, bowler.transform.position.z);
						fielder10Transform.position = new Vector3(0.5f, 0f, -6.75f);
					}
				}
				else if (currentBowlerType == "spin")
				{
					if (BowlerAnimNumber == 1)
					{
						bowler.transform.position = new Vector3(0.23f, bowler.transform.position.y, bowler.transform.position.z);
						mainUmpireTransform.position = new Vector3(0.037f, 0f, -19.5f);
						fielder10Transform.position = new Vector3(0.725f, 0f, -7.86f);
					}
					else if (BowlerAnimNumber == 2)
					{
						bowler.transform.position = new Vector3(0.75f, bowler.transform.position.y, bowler.transform.position.z);
						fielder10Transform.position = new Vector3(1.5f, 0f, -8.5f);
					}
				}
				else if (currentBowlerType == "medium")
				{
					if (BowlerAnimNumber == 1)
					{
						bowler.transform.position = new Vector3(0.77f, bowler.transform.position.y, bowler.transform.position.z);
						fielder10Transform.position = new Vector3(0.96f, 0f, -6.11f);
					}
					else if (BowlerAnimNumber == 2)
					{
						bowler.transform.position = new Vector3(1f, bowler.transform.position.y, bowler.transform.position.z);
						fielder10Transform.position = new Vector3(1f, 0f, -7f);
					}
				}
			}
			else
			{
				if (!(currentBowlerHand == "left"))
				{
					return;
				}
				ballOriginGO.transform.position = new Vector3(0.7f, ballOriginGO.transform.position.y, ballOriginGO.transform.position.z);
				ballTransform.position = new Vector3(ballOriginGO.transform.position.x, ballTransform.position.y, ballTransform.position.z);
				if (currentBowlerType == "fast")
				{
					if (BowlerAnimNumber == 1)
					{
						bowler.transform.position = new Vector3(0.7f, bowler.transform.position.y, bowler.transform.position.z);
						fielder10Transform.position = new Vector3(1.32f, 0f, -3.55f);
					}
					else if (BowlerAnimNumber == 2)
					{
						bowler.transform.position = new Vector3(0.7f, bowler.transform.position.y, bowler.transform.position.z);
						fielder10Transform.position = new Vector3(1.15f, 0f, -6.75f);
					}
				}
				else if (currentBowlerType == "spin")
				{
					mainUmpireTransform.position = mainUmpireInitPosition;
					if (BowlerAnimNumber == 1)
					{
						bowler.transform.position = new Vector3(1.4f, bowler.transform.position.y, bowler.transform.position.z);
						fielder10Transform.position = new Vector3(0.9f, 0f, -7.85f);
					}
					else if (BowlerAnimNumber == 2)
					{
						bowler.transform.position = new Vector3(1.8f, bowler.transform.position.y, bowler.transform.position.z);
						fielder10Transform.position = new Vector3(0.9f, 0f, -8.5f);
					}
				}
				else if (currentBowlerType == "medium")
				{
					if (BowlerAnimNumber == 1)
					{
						bowler.transform.position = new Vector3(0.91f, bowler.transform.position.y, bowler.transform.position.z);
						fielder10Transform.position = new Vector3(0.71f, 0f, -6.11f);
					}
					else if (BowlerAnimNumber == 2)
					{
						bowler.transform.position = new Vector3(1.3f, bowler.transform.position.y, -14.4f);
						fielder10Transform.position = new Vector3(0.75f, 0f, -7f);
					}
				}
			}
		}
	}

	public void resetNoBallVairables()
	{
		CONTROLLER.isFreeHitBall = false;
		lineFreeHit = false;
		lastBowledBall = "lineball";
	}

	public void AutoplaySettings()
	{
		if (CONTROLLER.isFromAutoPlay)
		{
			ResetAll();
			Singleton<Scoreboard>.instance.freeHitGO.SetActive(value: false);
			if (CONTROLLER.myTeamIndex == CONTROLLER.BattingTeamIndex)
			{
				Singleton<PreviewScreen>.instance.hideBtns(boolean: false);
			}
			HideBowlingSpot();
			ShowFullTossSpot(_Value: false);
			pauseCountdown();
			ActivateStadiumAndSkybox(boolean: true);
			CONTROLLER.isFromAutoPlay = false;
		}
	}

	public void NewInnings()
	{
		AutoplaySettings();
		SetDefaultDigitalDisplayContent();
		EnableFielders(boolean: true);
		setFieldersPosition();
		newInning = true;
		InitCamera();
		ActivateColliders(boolean: false);
		replayCamera.enabled = false;
		rightSideCamera.enabled = false;
		leftSideCamera.enabled = false;
		zoomCameraToBowlerStartTime = -1f;
		batsmanConfidenceLevel = CONTROLLER.isConfidenceLevel;
		if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
		{
			batsmanConfidenceLevel = false;
		}
		else
		{
			batsmanConfidenceLevel = true;
			if (!(CONTROLLER.difficultyMode == "hard"))
			{
			}
		}
		if (UnityEngine.Random.Range(0f, 10f) <= 5f)
		{
			bowlerSide = "left";
		}
		else
		{
			bowlerSide = "right";
		}
		if (UnityEngine.Random.Range(0f, 10f) < 5f)
		{
			mainUmpireIndex = 0;
			sideUmpireIndex = 1;
		}
		else
		{
			mainUmpireIndex = 1;
			sideUmpireIndex = 0;
		}
		//battingTeam = TrimSpaces(CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].teamName);
		//bowlingTeam = TrimSpaces(CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].teamName);
		//CONTROLLER.battingTeamUniform = battingTeam;
		//CONTROLLER.bowlingTeamUniform = bowlingTeam;
		InitializeEnvironment();
		GameObject gameObject;
		Renderer component;
		for (int i = 1; i <= 9; i++)
		{
			gameObject = fielderSkin[i];
			component = gameObject.GetComponent<Renderer>();
			component.materials[0].SetColor("_Color", teamUniformColor[CONTROLLER.BowlingTeamIndex]);
			component.materials[0].SetColor("_PatternColor", teamStripColor[CONTROLLER.BowlingTeamIndex]);
			component.materials[2].SetColor("_Color", teamSkinColor[CONTROLLER.BowlingTeamIndex]);
		}
		gameObject = fielderSkin[10];
		component = gameObject.GetComponent<Renderer>();
		component.materials[1].SetColor("_Color", teamUniformColor[CONTROLLER.BowlingTeamIndex]);
		component.materials[1].SetColor("_PatternColor", teamStripColor[CONTROLLER.BowlingTeamIndex]);
		component.materials[2].SetColor("_Color", teamSkinColor[CONTROLLER.BowlingTeamIndex]);
		WicketKeeperSkinRendererComponent.materials[1].SetColor("_Color", teamUniformColor[CONTROLLER.BowlingTeamIndex]);
		WicketKeeperSkinRendererComponent.materials[1].SetColor("_PatternColor", teamStripColor[CONTROLLER.BowlingTeamIndex]);
		WicketKeeperSkinRendererComponent.materials[2].SetColor("_Color", teamSkinColor[CONTROLLER.BowlingTeamIndex]);
		BowlerSkinRendererComponent.materials[1].SetColor("_Color", teamUniformColor[CONTROLLER.BowlingTeamIndex]);
		BowlerSkinRendererComponent.materials[1].SetColor("_PatternColor", teamStripColor[CONTROLLER.BowlingTeamIndex]);
		BowlerSkinRendererComponent.materials[2].SetColor("_Color", teamSkinColor[CONTROLLER.BowlingTeamIndex]);
		BatsmanSkinRendererComponent.materials[1].SetColor("_Color", teamUniformColor[CONTROLLER.BattingTeamIndex]);
		BatsmanSkinRendererComponent.materials[1].SetColor("_PatternColor", teamStripColor[CONTROLLER.BattingTeamIndex]);
		BatsmanSkinRendererComponent.materials[2].SetColor("_Color", teamSkinColor[CONTROLLER.BattingTeamIndex]);
		RunnerSkinRendererComponent.materials[1].SetColor("_Color", teamUniformColor[CONTROLLER.BattingTeamIndex]);
		RunnerSkinRendererComponent.materials[1].SetColor("_PatternColor", teamStripColor[CONTROLLER.BattingTeamIndex]);
		RunnerSkinRendererComponent.materials[2].SetColor("_Color", teamSkinColor[CONTROLLER.BattingTeamIndex]);
		if (CONTROLLER.PlayModeSelected != 2)
		{
			for (int j = 1; j <= 9; j++)
			{
				gameObject = fielderSkin[j];
				component = gameObject.GetComponent<Renderer>();
				component.materials[0].SetVector("_GrayScale", teamUniformGreyScale[CONTROLLER.BowlingTeamIndex]);
			}
			gameObject = fielderSkin[10];
			component = gameObject.GetComponent<Renderer>();
			component.materials[1].SetVector("_GrayScale", teamUniformGreyScale[CONTROLLER.BowlingTeamIndex]);
			WicketKeeperSkinRendererComponent.materials[1].SetVector("_GrayScale", teamUniformGreyScale[CONTROLLER.BowlingTeamIndex]);
			BowlerSkinRendererComponent.materials[1].SetVector("_GrayScale", teamUniformGreyScale[CONTROLLER.BowlingTeamIndex]);
			BatsmanSkinRendererComponent.materials[1].SetVector("_GrayScale", teamUniformGreyScale[CONTROLLER.BattingTeamIndex]);
			RunnerSkinRendererComponent.materials[1].SetVector("_GrayScale", teamUniformGreyScale[CONTROLLER.BattingTeamIndex]);
		}
		showPreviewCamera(status: false);
		Fielder10AnimationComponent.Play("idle");
		MainUmpireAnimationComponent.Play("Idle");
		SideUmpireAnimationComponent.Play("Idle");
		Stump1AnimationComponent.Play("idle");
		Stump2AnimationComponent.Play("idle");
		WicketKeeperAnimationComponent.Play("idle");
		batsmanAnimationComponent.Play("WCCLite_BatsmanIdle");
		RunnerAnimationComponent.Play("WCCLite_RunnerIdle");
		BowlerAnimationComponent.Play("Blitz_" + currentBowlerType + "Idle");
		ShowBall(status: false);
		replayMode = false;
		UpdateShadowsAndPreview();
	}

	public string TrimSpaces(string teamName)
	{
		string text = string.Empty;
		string[] array = teamName.Split(" "[0]);
		for (int i = 0; i < array.Length; i++)
		{
			text += array[i];
		}
		return text;
	}

	private void AiFieldScan()
	{
		aiFielderScanArray.Clear();
		for (int i = 1; i <= noOfFielders; i++)
		{
			if (DistanceBetweenTwoVector2(groundCenterPoint, fielder[i]) < 45f)
			{
				aiFielderScanArray.Add(fielder[i]);
			}
		}
	}

	private void getSlipFielders()
	{
		getSlipArray.Clear();
		slipFielderDoingWarmUpAction.Clear();
		int num = 7;
		if (currentBowlerType == "spin")
		{
			num = 15;
		}
		for (int i = 1; i <= noOfFielders; i++)
		{
			slipFielderDoingWarmUpAction.Add(item: false);
			if (currentBatsmanHand == "right" && fielderTransform[i].position.z > 0f)
			{
				if (wicketKeeperTransform.position.x > fielderTransform[i].position.x && DistanceBetweenTwoVector2(wicketKeeper, fielder[i]) < (float)num && AngleBetweenTwoGameObjects(stump1, fielder[i]) > 85f && AngleBetweenTwoGameObjects(stump1, fielder[i]) < 135f)
				{
					getSlipArray.Add(fielder[i]);
				}
			}
			else if (currentBatsmanHand == "left" && fielderTransform[i].position.z > 0f && wicketKeeperTransform.position.x < fielderTransform[i].position.x && DistanceBetweenTwoVector2(wicketKeeper, fielder[i]) < (float)num && AngleBetweenTwoGameObjects(stump1, fielder[i]) < 85f && AngleBetweenTwoGameObjects(stump1, fielder[i]) > 45f)
			{
				getSlipArray.Add(fielder[i]);
			}
		}
	}

	public void computerCanNowBowlNoBall()
	{
		if (!replayMode)
		{
			overStepBall = true;
			bowlerHeelPosForNoBall = UnityEngine.Random.Range(0.25f, 0.29f);
		}
	}

	public void StartBowling()
	{
		int num = ((CONTROLLER.BowlingTeamIndex == CONTROLLER.myTeamIndex) ? 1 : 0);
		if (!CONTROLLER.ReplayShowing)
		{
			CanShowCountDown = true;
		}
		float num2 = Mathf.Abs(CONTROLLER.BowlingAngle) * 0.1f;
		float num3;
		if (CONTROLLER.BowlingTeamIndex == CONTROLLER.myTeamIndex)
		{
			num3 = 4f * (1f + powerFactor);
			if (currentBowlerType == "fast" || currentBowlerType == "medium")
			{
				bowlingSpeed = (float)CONTROLLER.BowlingSpeed * (1f + powerFactor);
			}
			else if (currentBowlerType == "spin")
			{
				bowlingSpeed = CONTROLLER.BowlingSpeed;
			}
		}
		else
		{
			num3 = 4f;
			bowlingSpeed = CONTROLLER.BowlingSpeed;
		}
		if (CONTROLLER.myTeamIndex == CONTROLLER.BowlingTeamIndex)
		{
			float arrowPos = Singleton<BowlingControls>.instance.getArrowPos();
			if (arrowPos > 0.7f && !replayMode)
			{
				overStepBall = true;
				noBall = true;
				bowlerHeelPosForNoBall = UnityEngine.Random.Range(0.25f, 0.29f);
			}
		}
		AiFieldScan();
		getSlipFielders();
		if (noBall && !replayMode)
		{
			isFreeHit = true;
			freeHit = true;
			noBall = false;
		}
		if (bowlingSpeed < 10f * (1f + powerFactor * (float)num))
		{
			noBall = false;
		}
		spinValue = num3 * num2;
		if (currentBowlerSpinType == 1)
		{
			spinValue *= -1f;
		}
		if (currentBowlerType == "fast")
		{
			swingValue = CONTROLLER.BowlingSwing - 2f;
			swingValue = swingValue * 0.75f * (1f + controlFactor * (float)num);
			if (swingValue != 0f)
			{
				swingingBall = true;
				spinValue = (0f - swingValue) * 2f;
			}
		}
		else if (currentBowlerType == "medium")
		{
			swingValue = CONTROLLER.BowlingSwing - 2f;
			swingValue = swingValue * 0.75f * (1f + controlFactor * (float)num);
			if (swingValue != 0f)
			{
				swingingBall = true;
				spinValue = (0f - swingValue) * 2f;
			}
		}
		if (CONTROLLER.PlayModeSelected == 6)
		{
			swingValue = 0f;
			spinValue = 0f;
		}
		ballTransform.position = ballInitPosition;
		SetBowlerSide();
		ballTransform.eulerAngles = new Vector3(0f, 2f, 180f);
		if (CONTROLLER.cameraType == 0)
		{
			ActivateStadiumAndSkybox(boolean: false);
		}
		currentBallStartTime = Time.time;
		action = 0;
	}

	public void ResetFielders()
	{
		for (int i = 1; i <= noOfFielders; i++)
		{
			fielderNearToPitch.Add(item: false);
			if (CONTROLLER.myTeamIndex == CONTROLLER.BowlingTeamIndex)
			{
				if (fieldRestriction)
				{
					if (CONTROLLER.fielderChangeIndex == 1)
					{
						fielderTransform[i].position = fieldRestriction1FielderPosition[i];
					}
					else if (CONTROLLER.fielderChangeIndex == 2)
					{
						fielderTransform[i].position = fieldRestriction2FielderPosition[i];
					}
					else if (CONTROLLER.fielderChangeIndex == 3)
					{
						fielderTransform[i].position = fieldRestriction3FielderPosition[i];
					}
					else if (CONTROLLER.fielderChangeIndex == 4)
					{
						fielderTransform[i].position = fieldRestriction4FielderPosition[i];
					}
					else if (CONTROLLER.fielderChangeIndex == 5)
					{
						fielderTransform[i].position = fieldRestriction5FielderPosition[i];
					}
					else if (CONTROLLER.fielderChangeIndex == 6)
					{
						fielderTransform[i].position = fieldRestriction6FielderPosition[i];
					}
					else if (CONTROLLER.fielderChangeIndex == 7)
					{
						fielderTransform[i].position = fieldRestriction7FielderPosition[i];
					}
					else if (CONTROLLER.fielderChangeIndex == 8)
					{
						fielderTransform[i].position = fieldRestriction8FielderPosition[i];
					}
					else if (CONTROLLER.fielderChangeIndex == 9)
					{
						fielderTransform[i].position = fieldRestriction9FielderPosition[i];
					}
					else if (CONTROLLER.fielderChangeIndex == 10)
					{
						fielderTransform[i].position = fieldRestriction10FielderPosition[i];
					}
					if (currentBatsmanHand == "left")
					{
						if (CONTROLLER.fielderChangeIndex == 1)
						{
							fielderTransform[i].position = new Vector3(fieldRestriction1FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
						}
						else if (CONTROLLER.fielderChangeIndex == 2)
						{
							fielderTransform[i].position = new Vector3(fieldRestriction2FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
						}
						else if (CONTROLLER.fielderChangeIndex == 3)
						{
							fielderTransform[i].position = new Vector3(fieldRestriction3FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
						}
						else if (CONTROLLER.fielderChangeIndex == 4)
						{
							fielderTransform[i].position = new Vector3(fieldRestriction4FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
						}
						else if (CONTROLLER.fielderChangeIndex == 5)
						{
							fielderTransform[i].position = new Vector3(fieldRestriction5FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
						}
						else if (CONTROLLER.fielderChangeIndex == 6)
						{
							fielderTransform[i].position = new Vector3(fieldRestriction6FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
						}
						else if (CONTROLLER.fielderChangeIndex == 7)
						{
							fielderTransform[i].position = new Vector3(fieldRestriction7FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
						}
						else if (CONTROLLER.fielderChangeIndex == 8)
						{
							fielderTransform[i].position = new Vector3(fieldRestriction8FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
						}
						else if (CONTROLLER.fielderChangeIndex == 9)
						{
							fielderTransform[i].position = new Vector3(fieldRestriction9FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
						}
						else if (CONTROLLER.fielderChangeIndex == 10)
						{
							fielderTransform[i].position = new Vector3(fieldRestriction10FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
						}
					}
				}
				else if (!fieldRestriction)
				{
					if (CONTROLLER.fielderChangeIndex == 1)
					{
						fielderTransform[i].position = fieldRestriction1FielderPosition[i];
					}
					else if (CONTROLLER.fielderChangeIndex == 2)
					{
						fielderTransform[i].position = fieldRestriction2FielderPosition[i];
					}
					else if (CONTROLLER.fielderChangeIndex == 3)
					{
						fielderTransform[i].position = fieldRestriction3FielderPosition[i];
					}
					else if (CONTROLLER.fielderChangeIndex == 4)
					{
						fielderTransform[i].position = fieldRestriction4FielderPosition[i];
					}
					else if (CONTROLLER.fielderChangeIndex == 5)
					{
						fielderTransform[i].position = fieldRestriction5FielderPosition[i];
					}
					else if (CONTROLLER.fielderChangeIndex == 6)
					{
						fielderTransform[i].position = fieldRestriction6FielderPosition[i];
					}
					else if (CONTROLLER.fielderChangeIndex == 7)
					{
						fielderTransform[i].position = fieldRestriction7FielderPosition[i];
					}
					else if (CONTROLLER.fielderChangeIndex == 8)
					{
						fielderTransform[i].position = fieldRestriction8FielderPosition[i];
					}
					else if (CONTROLLER.fielderChangeIndex == 9)
					{
						fielderTransform[i].position = fieldRestriction9FielderPosition[i];
					}
					else if (CONTROLLER.fielderChangeIndex == 10)
					{
						fielderTransform[i].position = fieldRestriction10FielderPosition[i];
					}
					else if (CONTROLLER.fielderChangeIndex == 11)
					{
						fielderTransform[i].position = fieldRestriction11FielderPosition[i];
					}
					else if (CONTROLLER.fielderChangeIndex == 12)
					{
						fielderTransform[i].position = fieldRestriction12FielderPosition[i];
					}
					else if (CONTROLLER.fielderChangeIndex == 13)
					{
						fielderTransform[i].position = fieldRestriction13FielderPosition[i];
					}
					else if (CONTROLLER.fielderChangeIndex == 14)
					{
						fielderTransform[i].position = fieldRestriction14FielderPosition[i];
					}
					else if (CONTROLLER.fielderChangeIndex == 15)
					{
						fielderTransform[i].position = fieldRestriction15FielderPosition[i];
					}
					else if (CONTROLLER.fielderChangeIndex == 16)
					{
						fielderTransform[i].position = fieldRestriction16FielderPosition[i];
					}
					else if (CONTROLLER.fielderChangeIndex == 17)
					{
						fielderTransform[i].position = fieldRestriction17FielderPosition[i];
					}
					else if (CONTROLLER.fielderChangeIndex == 18)
					{
						fielderTransform[i].position = fieldRestriction18FielderPosition[i];
					}
					else if (CONTROLLER.fielderChangeIndex == 19)
					{
						fielderTransform[i].position = fieldRestriction19FielderPosition[i];
					}
					else if (CONTROLLER.fielderChangeIndex == 20)
					{
						fielderTransform[i].position = fieldRestriction20FielderPosition[i];
					}
					else if (CONTROLLER.fielderChangeIndex == 21)
					{
						fielderTransform[i].position = fieldRestriction21FielderPosition[i];
					}
					else if (CONTROLLER.fielderChangeIndex == 22)
					{
						fielderTransform[i].position = fieldRestriction22FielderPosition[i];
					}
					else if (CONTROLLER.fielderChangeIndex == 23)
					{
						fielderTransform[i].position = fieldRestriction23FielderPosition[i];
					}
					else if (CONTROLLER.fielderChangeIndex == 24)
					{
						fielderTransform[i].position = fieldRestriction24FielderPosition[i];
					}
					else if (CONTROLLER.fielderChangeIndex == 25)
					{
						fielderTransform[i].position = fieldRestriction25FielderPosition[i];
					}
					if (currentBatsmanHand == "left")
					{
						if (CONTROLLER.fielderChangeIndex == 1)
						{
							fielderTransform[i].position = new Vector3(fieldRestriction1FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
						}
						else if (CONTROLLER.fielderChangeIndex == 2)
						{
							fielderTransform[i].position = new Vector3(fieldRestriction2FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
						}
						else if (CONTROLLER.fielderChangeIndex == 3)
						{
							fielderTransform[i].position = new Vector3(fieldRestriction3FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
						}
						else if (CONTROLLER.fielderChangeIndex == 4)
						{
							fielderTransform[i].position = new Vector3(fieldRestriction4FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
						}
						else if (CONTROLLER.fielderChangeIndex == 5)
						{
							fielderTransform[i].position = new Vector3(fieldRestriction5FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
						}
						else if (CONTROLLER.fielderChangeIndex == 6)
						{
							fielderTransform[i].position = new Vector3(fieldRestriction6FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
						}
						else if (CONTROLLER.fielderChangeIndex == 7)
						{
							fielderTransform[i].position = new Vector3(fieldRestriction7FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
						}
						else if (CONTROLLER.fielderChangeIndex == 8)
						{
							fielderTransform[i].position = new Vector3(fieldRestriction8FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
						}
						else if (CONTROLLER.fielderChangeIndex == 9)
						{
							fielderTransform[i].position = new Vector3(fieldRestriction9FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
						}
						else if (CONTROLLER.fielderChangeIndex == 10)
						{
							fielderTransform[i].position = new Vector3(fieldRestriction10FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
						}
						else if (CONTROLLER.fielderChangeIndex == 11)
						{
							fielderTransform[i].position = new Vector3(fieldRestriction11FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
						}
						else if (CONTROLLER.fielderChangeIndex == 12)
						{
							fielderTransform[i].position = new Vector3(fieldRestriction12FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
						}
						else if (CONTROLLER.fielderChangeIndex == 13)
						{
							fielderTransform[i].position = new Vector3(fieldRestriction13FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
						}
						else if (CONTROLLER.fielderChangeIndex == 14)
						{
							fielderTransform[i].position = new Vector3(fieldRestriction14FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
						}
						else if (CONTROLLER.fielderChangeIndex == 15)
						{
							fielderTransform[i].position = new Vector3(fieldRestriction15FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
						}
						else if (CONTROLLER.fielderChangeIndex == 16)
						{
							fielderTransform[i].position = new Vector3(fieldRestriction16FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
						}
						else if (CONTROLLER.fielderChangeIndex == 17)
						{
							fielderTransform[i].position = new Vector3(fieldRestriction17FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
						}
						else if (CONTROLLER.fielderChangeIndex == 18)
						{
							fielderTransform[i].position = new Vector3(fieldRestriction18FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
						}
						else if (CONTROLLER.fielderChangeIndex == 19)
						{
							fielderTransform[i].position = new Vector3(fieldRestriction19FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
						}
						else if (CONTROLLER.fielderChangeIndex == 20)
						{
							fielderTransform[i].position = new Vector3(fieldRestriction20FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
						}
						else if (CONTROLLER.fielderChangeIndex == 21)
						{
							fielderTransform[i].position = new Vector3(fieldRestriction21FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
						}
						else if (CONTROLLER.fielderChangeIndex == 22)
						{
							fielderTransform[i].position = new Vector3(fieldRestriction22FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
						}
						else if (CONTROLLER.fielderChangeIndex == 23)
						{
							fielderTransform[i].position = new Vector3(fieldRestriction23FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
						}
						else if (CONTROLLER.fielderChangeIndex == 24)
						{
							fielderTransform[i].position = new Vector3(fieldRestriction24FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
						}
						else if (CONTROLLER.fielderChangeIndex == 25)
						{
							fielderTransform[i].position = new Vector3(fieldRestriction25FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
						}
					}
				}
			}
			else if (CONTROLLER.myTeamIndex == CONTROLLER.BattingTeamIndex)
			{
				if (CONTROLLER.computerFielderChangeIndex == 1 || CONTROLLER.computerFielderChangeIndex == 0)
				{
					fielderTransform[i].position = fieldRestriction1FielderPosition[i];
				}
				else if (CONTROLLER.computerFielderChangeIndex == 2)
				{
					fielderTransform[i].position = fieldRestriction2FielderPosition[i];
				}
				else if (CONTROLLER.computerFielderChangeIndex == 3)
				{
					fielderTransform[i].position = fieldRestriction3FielderPosition[i];
				}
				else if (CONTROLLER.computerFielderChangeIndex == 4)
				{
					fielderTransform[i].position = fieldRestriction4FielderPosition[i];
				}
				else if (CONTROLLER.computerFielderChangeIndex == 5)
				{
					fielderTransform[i].position = fieldRestriction5FielderPosition[i];
				}
				else if (CONTROLLER.computerFielderChangeIndex == 6)
				{
					fielderTransform[i].position = fieldRestriction6FielderPosition[i];
				}
				else if (CONTROLLER.computerFielderChangeIndex == 7)
				{
					fielderTransform[i].position = fieldRestriction7FielderPosition[i];
				}
				else if (CONTROLLER.computerFielderChangeIndex == 8)
				{
					fielderTransform[i].position = fieldRestriction8FielderPosition[i];
				}
				else if (CONTROLLER.computerFielderChangeIndex == 9)
				{
					fielderTransform[i].position = fieldRestriction9FielderPosition[i];
				}
				else if (CONTROLLER.computerFielderChangeIndex == 10)
				{
					fielderTransform[i].position = fieldRestriction10FielderPosition[i];
				}
				else if (CONTROLLER.computerFielderChangeIndex == 11)
				{
					fielderTransform[i].position = fieldRestriction11FielderPosition[i];
				}
				else if (CONTROLLER.computerFielderChangeIndex == 12)
				{
					fielderTransform[i].position = fieldRestriction12FielderPosition[i];
				}
				else if (CONTROLLER.computerFielderChangeIndex == 13)
				{
					fielderTransform[i].position = fieldRestriction13FielderPosition[i];
				}
				else if (CONTROLLER.computerFielderChangeIndex == 14)
				{
					fielderTransform[i].position = fieldRestriction14FielderPosition[i];
				}
				else if (CONTROLLER.computerFielderChangeIndex == 15)
				{
					fielderTransform[i].position = fieldRestriction15FielderPosition[i];
				}
				else if (CONTROLLER.computerFielderChangeIndex == 16)
				{
					fielderTransform[i].position = fieldRestriction16FielderPosition[i];
				}
				else if (CONTROLLER.computerFielderChangeIndex == 17)
				{
					fielderTransform[i].position = fieldRestriction17FielderPosition[i];
				}
				else if (CONTROLLER.computerFielderChangeIndex == 18)
				{
					fielderTransform[i].position = fieldRestriction18FielderPosition[i];
				}
				else if (CONTROLLER.computerFielderChangeIndex == 19)
				{
					fielderTransform[i].position = fieldRestriction19FielderPosition[i];
				}
				else if (CONTROLLER.computerFielderChangeIndex == 20)
				{
					fielderTransform[i].position = fieldRestriction20FielderPosition[i];
				}
				else if (CONTROLLER.computerFielderChangeIndex == 21)
				{
					fielderTransform[i].position = fieldRestriction21FielderPosition[i];
				}
				else if (CONTROLLER.computerFielderChangeIndex == 22)
				{
					fielderTransform[i].position = fieldRestriction22FielderPosition[i];
				}
				else if (CONTROLLER.computerFielderChangeIndex == 23)
				{
					fielderTransform[i].position = fieldRestriction23FielderPosition[i];
				}
				else if (CONTROLLER.computerFielderChangeIndex == 24)
				{
					fielderTransform[i].position = fieldRestriction24FielderPosition[i];
				}
				else if (CONTROLLER.computerFielderChangeIndex == 25)
				{
					fielderTransform[i].position = fieldRestriction25FielderPosition[i];
				}
				if (currentBatsmanHand == "left")
				{
					if (CONTROLLER.computerFielderChangeIndex == 1 || CONTROLLER.computerFielderChangeIndex == 0)
					{
						fielderTransform[i].position = new Vector3(fieldRestriction1FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
					}
					else if (CONTROLLER.computerFielderChangeIndex == 2)
					{
						fielderTransform[i].position = new Vector3(fieldRestriction2FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
					}
					else if (CONTROLLER.computerFielderChangeIndex == 3)
					{
						fielderTransform[i].position = new Vector3(fieldRestriction3FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
					}
					else if (CONTROLLER.computerFielderChangeIndex == 4)
					{
						fielderTransform[i].position = new Vector3(fieldRestriction4FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
					}
					else if (CONTROLLER.computerFielderChangeIndex == 5)
					{
						fielderTransform[i].position = new Vector3(fieldRestriction5FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
					}
					else if (CONTROLLER.computerFielderChangeIndex == 6)
					{
						fielderTransform[i].position = new Vector3(fieldRestriction6FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
					}
					else if (CONTROLLER.computerFielderChangeIndex == 7)
					{
						fielderTransform[i].position = new Vector3(fieldRestriction7FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
					}
					else if (CONTROLLER.computerFielderChangeIndex == 8)
					{
						fielderTransform[i].position = new Vector3(fieldRestriction8FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
					}
					else if (CONTROLLER.computerFielderChangeIndex == 9)
					{
						fielderTransform[i].position = new Vector3(fieldRestriction9FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
					}
					else if (CONTROLLER.computerFielderChangeIndex == 10)
					{
						fielderTransform[i].position = new Vector3(fieldRestriction10FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
					}
					else if (CONTROLLER.computerFielderChangeIndex == 11)
					{
						fielderTransform[i].position = new Vector3(fieldRestriction11FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
					}
					else if (CONTROLLER.computerFielderChangeIndex == 12)
					{
						fielderTransform[i].position = new Vector3(fieldRestriction12FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
					}
					else if (CONTROLLER.computerFielderChangeIndex == 13)
					{
						fielderTransform[i].position = new Vector3(fieldRestriction13FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
					}
					else if (CONTROLLER.computerFielderChangeIndex == 14)
					{
						fielderTransform[i].position = new Vector3(fieldRestriction14FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
					}
					else if (CONTROLLER.computerFielderChangeIndex == 15)
					{
						fielderTransform[i].position = new Vector3(fieldRestriction15FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
					}
					else if (CONTROLLER.computerFielderChangeIndex == 16)
					{
						fielderTransform[i].position = new Vector3(fieldRestriction16FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
					}
					else if (CONTROLLER.computerFielderChangeIndex == 17)
					{
						fielderTransform[i].position = new Vector3(fieldRestriction17FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
					}
					else if (CONTROLLER.computerFielderChangeIndex == 18)
					{
						fielderTransform[i].position = new Vector3(fieldRestriction18FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
					}
					else if (CONTROLLER.computerFielderChangeIndex == 19)
					{
						fielderTransform[i].position = new Vector3(fieldRestriction19FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
					}
					else if (CONTROLLER.computerFielderChangeIndex == 20)
					{
						fielderTransform[i].position = new Vector3(fieldRestriction20FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
					}
					else if (CONTROLLER.computerFielderChangeIndex == 21)
					{
						fielderTransform[i].position = new Vector3(fieldRestriction21FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
					}
					else if (CONTROLLER.computerFielderChangeIndex == 22)
					{
						fielderTransform[i].position = new Vector3(fieldRestriction22FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
					}
					else if (CONTROLLER.computerFielderChangeIndex == 23)
					{
						fielderTransform[i].position = new Vector3(fieldRestriction23FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
					}
					else if (CONTROLLER.computerFielderChangeIndex == 24)
					{
						fielderTransform[i].position = new Vector3(fieldRestriction24FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
					}
					else if (CONTROLLER.computerFielderChangeIndex == 25)
					{
						fielderTransform[i].position = new Vector3(fieldRestriction25FielderPosition[i].x * -1f, fielderTransform[i].position.y, fielderTransform[i].position.z);
					}
				}
			}
			FielderAnimationComponent[i].Play("idle");
			GameObject gameObject = fielderBall[i];
			gameObject.GetComponent<Renderer>().enabled = false;
			fielderTransform[i].LookAt(stump1Crease.transform);
		}
	}

	private void FielderExtraActions()
	{
		getSlipFielders();
		List<int> list = new List<int>(new int[11]
		{
			-5, -4, -3, -2, -1, 0, 1, 2, 3, 4,
			5
		});
		List<int> list2 = new List<int>(new int[3] { 1, 2, 3 });
		for (int i = 0; i < getSlipArray.Count; i++)
		{
			if (!replayMode && getSlipArray[i] != null && !slipFielderDoingWarmUpAction[i])
			{
				slipFielderExtraAction[i] = 0f;
				int index = UnityEngine.Random.Range(0, list.Count);
				slipFielderExtraAction[i] = list[index];
				list.RemoveAt(index);
				slipFielderWarmUpAnimationSpeed[i] = UnityEngine.Random.Range(1, 3);
				int num = (int)slipFielderExtraAction[i];
				if (num >= 1)
				{
					GameObject gameObject = getSlipArray[i];
					gameObject.GetComponent<Animation>().CrossFade("warmUp" + num);
				}
			}
		}
	}

	private void GetFieldersAngle()
	{
		for (int i = 1; i <= noOfFielders; i++)
		{
			float y = ballTimingOriginTransform.position.x - fielderTransform[i].position.x;
			float x = ballTimingOriginTransform.position.z - fielderTransform[i].position.z;
			float num = (Mathf.Atan2(y, x) * RAD2DEG + 360f) % 360f;
			num = (270f - num + 360f) % 360f;
			fielderAngle[i] = num;
		}
	}

	private void GetFieldersDistance()
	{
		for (int i = 1; i <= noOfFielders; i++)
		{
			fielderDistance[i] = DistanceBetweenTwoVector2(ballTimingOrigin, fielder[i]);
		}
	}

	public void SetActiveFielders()
	{
		if (canKeeperCollectBall)
		{
			return;
		}
		float num = 25f;
		float num2 = 100f;
		if (CONTROLLER.BowlingTeamIndex == CONTROLLER.myTeamIndex)
		{
			fielderSpeed = 7f * (1f + agilityFactor);
			num = 25f * (1f + agilityFactor);
		}
		else
		{
			fielderSpeed = 7f;
		}
		if (CONTROLLER.difficultyMode == "hard" && battingBy == "user")
		{
			num = 20f;
			fielderSpeed = 8f;
		}
		fielderToCatchTheBall = null;
		activeFielderNumber.Clear();
		activeFielderAction.Clear();
		fielderSetChasePoint.Clear();
		for (int i = 1; i <= noOfFielders; i++)
		{
			float num3 = fielderAngle[i];
			float num4 = fielderDistance[i];
			float num5 = Mathf.Abs(ballAngle - num3);
			if (num5 > 180f)
			{
				num5 = ((!(ballAngle > num3)) ? (360f - num3 + ballAngle) : (360f + num3 - ballAngle));
			}
			fielderSetChasePoint.Add(item: true);
			fielderBallDiffInAngle[i] = num5;
			if (!(num5 < num))
			{
				continue;
			}
			bool flag = false;
			if (deactivateSlipFielders(fielder[i]))
			{
				if (currentBatsmanHand == "left")
				{
					if (ballAngle > 50f && ballAngle < 75f && AngleBetweenTwoGameObjects(fielder[i], ball) <= 255f)
					{
						flag = true;
						activeFielderNumber.Add(i);
					}
					else if (ballAngle >= 75f && AngleBetweenTwoGameObjects(fielder[i], ball) > 255f)
					{
						flag = true;
						activeFielderNumber.Add(i);
					}
				}
				else if (ballAngle < 110f && AngleBetweenTwoGameObjects(fielder[i], ball) < 285f)
				{
					flag = true;
					activeFielderNumber.Add(i);
				}
				else if (ballAngle >= 110f && AngleBetweenTwoGameObjects(fielder[i], ball) >= 285f)
				{
					flag = true;
					activeFielderNumber.Add(i);
				}
			}
			else
			{
				flag = true;
				activeFielderNumber.Add(i);
			}
			if (flag && DistanceBetweenTwoGameObjects(fielder[i], ballTimingOrigin) > 8f)
			{
				FielderAnimationComponent[i].Play("run");
			}
			if (flag && DistanceBetweenTwoGameObjects(fielder[i], ballTimingOrigin) > 8f)
			{
				FielderAnimationComponent[i]["run"].speed = 1f;
			}
			if (!flag)
			{
				continue;
			}
			float length = FielderAnimationComponent[i]["run"].length;
			FielderAnimationComponent[i]["run"].time = UnityEngine.Random.Range(0f, length);
			float num6 = Vector3.Distance(fielderTransform[i].position, ballCatchingSpotTransform.position);
			float num7 = num6 / fielderSpeed;
			float num8 = (ballTimingFirstBounceDistance - ballPreCatchingDistance) / horizontalSpeed;
			if (num7 < num8)
			{
				activeFielderAction.Add("goForCatch");
				if (num7 < num2)
				{
					num2 = num7;
					fielderToCatchTheBall = fielder[i];
					shortestBallPickupDistance = ballTimingFirstBounceDistance;
				}
				continue;
			}
			activeFielderAction.Add("goForChase");
			float f = Mathf.Sin(num5 * DEG2RAD) * num4;
			float num9 = Mathf.Sqrt(Mathf.Pow(fielderDistance[i], 2f) - Mathf.Pow(f, 2f));
			float x = ballTimingOriginTransform.position.x + num9 * Mathf.Cos(ballAngle * DEG2RAD);
			float z = ballTimingOriginTransform.position.z + num9 * Mathf.Sin(ballAngle * DEG2RAD);
			GameObject gameObject = fielderChasePoint[i];
			gameObject.transform.position = new Vector3(x, 0f, z);
			bool flag2 = false;
			if (num9 < ballTimingFirstBounceDistance)
			{
				num9 = ballTimingFirstBounceDistance;
				x = ballTimingOriginTransform.position.x + num9 * Mathf.Cos(ballAngle * DEG2RAD);
				z = ballTimingOriginTransform.position.z + num9 * Mathf.Sin(ballAngle * DEG2RAD);
				gameObject.transform.position = new Vector3(x, gameObject.transform.position.y, z);
				GameObject go = fielderChasePoint[i];
				while (DistanceBetweenTwoVector2(groundCenterPoint, go) > groundRadius)
				{
					num9 -= 2f;
					x = ballTimingOriginTransform.position.x + num9 * Mathf.Cos(ballAngle * DEG2RAD);
					z = ballTimingOriginTransform.position.z + num9 * Mathf.Sin(ballAngle * DEG2RAD);
					gameObject.transform.position = new Vector3(x, gameObject.transform.position.y, z);
				}
				flag2 = true;
			}
			float num10 = 9f;
			float num11 = num10 * animationFPSDivide * horizontalSpeed;
			float num12 = Vector3.Distance(fielderTransform[i].position, gameObject.transform.position);
			float num13 = num12 / fielderSpeed;
			float num14 = (num9 - num11) / horizontalSpeed;
			if (!flag2)
			{
				if (num13 < num14)
				{
					while (num13 < num14)
					{
						num9 -= 1f;
						x = ballTimingOriginTransform.position.x + num9 * Mathf.Cos(ballAngle * DEG2RAD);
						z = ballTimingOriginTransform.position.z + num9 * Mathf.Sin(ballAngle * DEG2RAD);
						gameObject.transform.position = new Vector3(x, gameObject.transform.position.y, z);
						num12 = Vector3.Distance(fielderTransform[i].position, gameObject.transform.position);
						num13 = num12 / fielderSpeed;
						num14 = (num9 - num11) / horizontalSpeed;
					}
					num9 += 1f;
					x = ballTimingOriginTransform.position.x + num9 * Mathf.Cos(ballAngle * DEG2RAD);
					z = ballTimingOriginTransform.position.z + num9 * Mathf.Sin(ballAngle * DEG2RAD);
					gameObject.transform.position = new Vector3(x, gameObject.transform.position.y, z);
				}
				else
				{
					GameObject go2 = fielderChasePoint[i];
					while (num13 > num14 && DistanceBetweenTwoVector2(groundCenterPoint, go2) < groundRadius - 3f)
					{
						num9 += 1f;
						x = ballTimingOriginTransform.position.x + num9 * Mathf.Cos(ballAngle * DEG2RAD);
						z = ballTimingOriginTransform.position.z + num9 * Mathf.Sin(ballAngle * DEG2RAD);
						gameObject.transform.position = new Vector3(x, gameObject.transform.position.y, z);
						num12 = Vector3.Distance(fielderTransform[i].position, gameObject.transform.position);
						num13 = num12 / fielderSpeed;
						num14 = (num9 - num11) / horizontalSpeed;
					}
				}
			}
			if (num9 > ballTimingFirstBounceDistance && num9 < shortestBallPickupDistance)
			{
				shortestBallPickupDistance = num9;
			}
		}
		if (shortestBallPickupDistance > 80f)
		{
			shortestBallPickupDistance = 80f;
		}
		fielder10FocusGObjToCollectTheBall.transform.position = new Vector3(ballTimingOriginTransform.position.x + Mathf.Cos(ballAngle * DEG2RAD) * shortestBallPickupDistance, fielder10FocusGObjToCollectTheBall.transform.position.y, ballTimingOriginTransform.position.z + Mathf.Sin(ballAngle * DEG2RAD) * shortestBallPickupDistance);
	}

	public void DelayMakeFieldersToCelebrate()
	{
		makeFieldersToCelebrate(null);
	}

	public void makeFieldersToCelebrate(GameObject fielderToAvoid)
	{
		if (savedIsRunOut)
		{
			if (savedThrowTo == "Fielder10")
			{
				WicketKeeperAnimationComponent.Play("appealFast");
			}
			else if (savedThrowTo == "WicketKeeper")
			{
				Fielder10AnimationComponent.Play("appeal");
			}
		}
		for (int i = 1; i <= noOfFielders; i++)
		{
			if (fielder[i] != fielderToAvoid && (overStepBall || !lineFreeHit))
			{
				FielderAnimationComponent[i].Play("appeal");
				FielderAnimationComponent[i]["appeal"].speed = 1.3f + UnityEngine.Random.Range(0f, 1f);
				if (ballStatus != "bowled" && !lbwAppeal)
				{
					Vector3 position = ballTransform.position;
					position = new Vector3(position.x, 0f, position.z);
					fielderTransform[i].LookAt(position);
				}
			}
		}
	}

	public void ActivateBowler()
	{
		if (action == 0)
		{
			return;
		}
		if ((BowlerAnimationComponent.IsPlaying("Blitz_" + currentBowlerType + "PaceBowling0" + BowlerAnimNumber) || (CONTROLLER.ReplayShowing && activateBowlerForReplay)) && !canActivateBowler)
		{
			if (CONTROLLER.ReplayShowing)
			{
				activateBowlerForReplay = true;
			}
			if (BowlerAnimationComponent["Blitz_" + currentBowlerType + "PaceBowling0" + BowlerAnimNumber].time > BowlerAnimationComponent["Blitz_" + currentBowlerType + "PaceBowling0" + BowlerAnimNumber].length - 0.16f || BowlerAnimationComponent["Blitz_" + currentBowlerType + "PaceBowling0" + BowlerAnimNumber].time == 0f || (CONTROLLER.ReplayShowing && ballStatus == "shotSuccess"))
			{
				activateBowlerForReplay = false;
				ShowFielder10(fielder10Status: true, ball10Status: false);
				ShowBowler(showStatus: false);
			}
		}
		if (!canAutoActivateBowler && ballStatus == "onPads" && ballTransform.position.z < 6f)
		{
			ShowFielder10(fielder10Status: true, ball10Status: false);
			ShowBowler(showStatus: false);
			canAutoActivateBowler = true;
		}
		if (canActivateBowler && (((shotPlayed == "bt6Defense" || shotPlayed == "backFootDefenseHighBall" || shotPlayed == "frontFootOffSideDefense") && ballStatus == "shotSuccess") || (ballStatus == "onPads" && !lbwAppeal)))
		{
			if (fielder10Action == "idle")
			{
				fielder10Action = "run";
				Fielder10AnimationComponent.CrossFade("run");
			}
			if (fielder10Action == "run")
			{
				float num = AngleBetweenTwoGameObjects(fielder10, ball);
				fielder10Transform.position += new Vector3(Mathf.Cos(num * DEG2RAD) * fielderSpeed * Time.deltaTime, 0f, Mathf.Sin(num * DEG2RAD) * fielderSpeed * Time.deltaTime);
				fielder10Transform.LookAt(new Vector3(ballTransform.position.x, 0f, ballTransform.position.z));
				int num2 = 1;
				if (DistanceBetweenTwoVector2(fielder10, ball) < (float)num2)
				{
					fielder10Action = "pickupAttempt";
					Fielder10AnimationComponent.CrossFade("lowCatch");
					Fielder10AnimationComponent["lowCatch"].speed = 5f;
				}
			}
			else if (fielder10Action == "pickupAttempt")
			{
				float num3 = 16f;
				float num4 = num3 * animationFPSDivide;
				if (num4 < Fielder10AnimationComponent["lowCatch"].time || Fielder10AnimationComponent["lowCatch"].time == 0f)
				{
					fielder10Action = "pickedup";
					Fielder10BallSkinRendererComponent.enabled = true;
					Fielder10AnimationComponent["lowCatch"].speed = 1f;
					ShowBall(status: false);
					pauseTheBall = true;
					stopTheFielders = true;
					fielderSpeed = 0f;
				}
			}
			else if (fielder10Action == "pickedup")
			{
				if (Fielder10AnimationComponent["lowCatch"].time == 0f)
				{
					float num5 = AngleBetweenTwoGameObjects(fielder10, stump1) + 90f;
					iTween.RotateTo(fielder10, iTween.Hash("y", num5, "time", 0.2));
					Fielder10AnimationComponent.Play("idle");
					stayStartTime = Time.time;
					fielder10Action = "end";
				}
			}
			else if (fielder10Action == "end")
			{
				if (stayStartTime - 0.5f + timeBetweenBalls < Time.time)
				{
					if (overStepBall)
					{
						if (replayMode)
						{
							fielder10Action = string.Empty;
							HideReplay();
							CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
							if (CONTROLLER.BowlingTeamIndex == CONTROLLER.myTeamIndex)
							{
							}
							Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, 0, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
							return;
						}
						showMainUmpireForNoBallAction();
						fielder10Action = "umpireNoBallAction";
						bool flag = checkForMatchComplete(1, 0);
						if (CONTROLLER.matchType == "oneday" && !flag)
						{
							noBallActionWaitTime = 4f;
							MainUmpireAnimationComponent.Play("NoBallFreeHit_New");
							lineFreeHit = true;
							lastBowledBall = "overstep";
						}
						else
						{
							noBallActionWaitTime = 1.5f;
							MainUmpireAnimationComponent.Play("NoBallFreeHit_New");
						}
					}
					else if (lineFreeHit)
					{
						fielder10Action = string.Empty;
						Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, 0, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
						lineFreeHit = false;
						lastBowledBall = "lineball";
						Singleton<Scoreboard>.instance.showFreeHitBg(canShow: false);
					}
					else
					{
						fielder10Action = string.Empty;
						if (Singleton<GameModel>.instance != null)
						{
							if (noBall)
							{
								CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
								Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, 0, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
								CONTROLLER.isJokerCall = false;
								if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
								{
								}
							}
							else if (freeHit)
							{
								Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, 0, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
								freeHit = false;
								isFreeHit = false;
							}
							else if (!noBall && !freeHit)
							{
								Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, 0, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
							}
						}
					}
				}
			}
			else if (fielder10Action == "umpireNoBallAction" && stayStartTime + noBallActionWaitTime + timeBetweenBalls < Time.time)
			{
				fielder10Action = string.Empty;
				if (Singleton<GameModel>.instance != null)
				{
					if (overStepBall)
					{
						if (!replayMode)
						{
							noBallRunUpdateStatus = "bowlercollectsdotball";
							if (CONTROLLER.canShowReplay)
							{
								Singleton<GameModel>.instance.GameIsOnReplay();
								ShowReplay();
							}
							else
							{
								Singleton<GameModel>.instance.ReplayIsNotShown();
							}
						}
						else
						{
							HideReplay();
							CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
							Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, 0, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
							if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
							{
							}
						}
					}
					else if (lineFreeHit)
					{
						Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, 0, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
						lineFreeHit = false;
						lastBowledBall = "lineball";
					}
				}
			}
		}
		else if (canActivateBowler && ballStatus == "shotSuccess" && shotPlayed != "bt6Defense" && shotPlayed != "backFootDefenseHighBall" && shotPlayed != "frontFootOffSideDefense" && fielder10Action == "idle")
		{
			Fielder10AnimationComponent.Play("run");
			fielder10Transform.position += new Vector3(0f, 0f, 1f);
			float num6 = AngleBetweenTwoGameObjects(stump2Spot, fielder10FocusGObjToCollectTheBall);
			float num7 = 0.7f;
			if (currentBowlerType == "spin")
			{
				num7 = 0.4f;
			}
			if (num6 >= 45f && num6 <= 135f)
			{
				postBattingStumpFielderDirection = "straight";
				iTween.MoveTo(fielder10, iTween.Hash("position", fielderStraightBallStumping.transform.position, "time", num7, "easetype", "linear", "oncomplete", "EnableFielder10ToCollectBall", "oncompletetarget", base.gameObject));
				fielder10Transform.LookAt(fielderStraightBallStumping.transform);
			}
			else if (num6 >= 225f && num6 <= 315f)
			{
				postBattingStumpFielderDirection = "straightDown";
				Vector3 position = stump2Crease.transform.position;
				position = new Vector3(position.x, position.y, -9f);
				iTween.MoveTo(fielder10, iTween.Hash("position", position, "time", num7, "easetype", "linear", "oncomplete", "EnableFielder10ToCollectBall", "oncompletetarget", base.gameObject));
				fielder10Transform.LookAt(stump2Crease.transform);
			}
			else if (num6 >= 135f && num6 <= 225f)
			{
				postBattingStumpFielderDirection = "offSide";
				iTween.MoveTo(fielder10, iTween.Hash("position", fielderOffSideBallStumping.transform.position, "time", num7, "easetype", "linear", "oncomplete", "EnableFielder10ToCollectBall", "oncompletetarget", base.gameObject));
				fielder10Transform.LookAt(fielderOffSideBallStumping.transform);
			}
			else
			{
				postBattingStumpFielderDirection = "legSide";
				iTween.MoveTo(fielder10, iTween.Hash("position", fielderLegSideBallStumping.transform.position, "time", num7, "easetype", "linear", "oncomplete", "EnableFielder10ToCollectBall", "oncompletetarget", base.gameObject));
				fielder10Transform.LookAt(fielderLegSideBallStumping.transform);
			}
			float y = fielder10Transform.eulerAngles.y;
			fielder10Transform.eulerAngles = new Vector3(fielder10Transform.eulerAngles.x, 0f, fielder10Transform.eulerAngles.z);
			iTween.RotateTo(fielder10, iTween.Hash("y", y, "time", 0.2));
			fielder10Action = "reachedNonStickerStump";
		}
		else if (fielder10Action == "waitForBall")
		{
			if (ballOnboundaryLine)
			{
				Fielder10AnimationComponent.CrossFade("idle");
				fielder10Action = "finish";
			}
			if (ballResult == "wicket" && !lineFreeHit)
			{
				Fielder10AnimationComponent.Play("appeal");
				fielder10Action = "finish";
			}
		}
		else if (fielder10Action == "waitToCollect" && isFielderthrown)
		{
			float num8 = 7f;
			float num9 = num8 * animationFPSDivide * horizontalSpeed;
			if (DistanceBetweenTwoVector2(ball, fielder10) < num9)
			{
				canTakeRun = false;
				distanceBtwBallAndCollectingPlayerWhileThrowing = 10000f;
				if (Singleton<GameModel>.instance != null)
				{
					disableRunCancelBtn();
				}
				if ((!replayMode && runOut) || (replayMode && savedThrowAction == "collectTheThrowAndStump"))
				{
					if (Singleton<GameModel>.instance != null && !replayMode)
					{
						Singleton<GameModel>.instance.PlayGameSound("Cheer");
						Singleton<GameModel>.instance.PlayGameSound("Bowled");
					}
					Fielder10AnimationComponent.Play("collectAndStump");
					Fielder10AnimationComponent.PlayQueued("appeal");
					fielder10Action = "collectTheThrowAndStump";
					savedThrowAction = "collectTheThrowAndStump";
				}
				else if ((!replayMode && !runOut) || (replayMode && savedThrowAction == "collectTheThrow"))
				{
					Fielder10AnimationComponent.Play("collectAndStand");
					fielder10Action = "collectTheThrow";
					savedThrowAction = "collectTheThrow";
				}
			}
		}
		else if (fielder10Action == "collectTheThrow" || fielder10Action == "collectTheThrowAndStump")
		{
			float num10 = DistanceBetweenTwoVector2(ball, fielder10);
			if (distanceBtwBallAndCollectingPlayerWhileThrowing > num10)
			{
				distanceBtwBallAndCollectingPlayerWhileThrowing = num10;
			}
			else
			{
				distanceBtwBallAndCollectingPlayerWhileThrowing = -1f;
			}
			if (!replayMode)
			{
				runOut = isBatsmanRunOut();
			}
			if (num10 < 0.5f || distanceBtwBallAndCollectingPlayerWhileThrowing == -1f)
			{
				ballStatus = string.Empty;
				pauseTheBall = true;
				Fielder10BallSkinRendererComponent.enabled = true;
				ShowBall(status: false);
				if (fielder10Action == "collectTheThrow")
				{
					stayStartTime = Time.time;
					fielder10Action = "end";
				}
				else if (fielder10Action == "collectTheThrowAndStump")
				{
					float num11 = 9f;
					fielder10Transform.LookAt(stump2Spot.transform);
					fielder10Transform.eulerAngles -= new Vector3(0f, 5f, 0f);
					if (Fielder10AnimationComponent["collectAndStump"].time > num11 * animationFPSDivide)
					{
						if (!replayMode)
						{
							isRunOut = runOut;
							if (!lineFreeHit && !overStepBall)
							{
								tightRunoutCall = IsTightRunoutCall();
							}
							savedIsRunOut = isRunOut;
							savedCurrentBallNoOfRuns = currentBallNoOfRuns;
						}
						else
						{
							isRunOut = savedIsRunOut;
							currentBallNoOfRuns = savedCurrentBallNoOfRuns;
						}
						if (freeHit)
						{
							freeHit = false;
						}
						stayStartTime = Time.time;
						fielder10Action = "runOutAppeal";
						savedRunOutAppeal = true;
						if (replayMode)
						{
							StartCoroutine(UltraSlowMotion());
						}
						float num12 = 270f;
						if (umpireRunDirection == -1)
						{
							num12 = 90f;
						}
						iTween.RotateTo(fielder10, iTween.Hash("y", num12, "time", 0.5, "delay", 0.2));
						if (postBattingStumpFielderDirection == "straight")
						{
							Stump2AnimationComponent.Play("legSideStumping");
						}
						else if (postBattingStumpFielderDirection == "straightDown")
						{
							Stump2AnimationComponent.Play("offSideStumping");
						}
						else if (postBattingStumpFielderDirection == "offSide")
						{
							Stump2AnimationComponent.Play("fielderRunoutAway");
						}
						else if (postBattingStumpFielderDirection == "legSide")
						{
							Stump2AnimationComponent.Play("fielderRunoutIn");
						}
					}
				}
			}
		}
		else if (fielder10Action == "end")
		{
			if (stayStartTime + 1f + timeBetweenBalls < Time.time)
			{
				if (overStepBall)
				{
					if (replayMode)
					{
						fielder10Action = string.Empty;
						HideReplay();
						CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
						Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, currentBallNoOfRuns, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
						if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
						{
						}
						return;
					}
					showMainUmpireForNoBallAction();
					runsScoredInLineNoBall = currentBallNoOfRuns;
					fielder10Action = "umpireNoBallActionAfterBowlerCollectsBallFromFielder";
					bool flag2 = checkForMatchComplete(currentBallNoOfRuns + 1, 0);
					if (CONTROLLER.matchType == "oneday" && !flag2)
					{
						noBallActionWaitTime = 4f;
						MainUmpireAnimationComponent.Play("NoBallFreeHit_New");
						lineFreeHit = true;
						lastBowledBall = "overstep";
					}
					else
					{
						noBallActionWaitTime = 1.08f;
						MainUmpireAnimationComponent.Play("NoBallFreeHit_New");
					}
				}
				else if (lineFreeHit)
				{
					fielder10Action = string.Empty;
					Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, currentBallNoOfRuns, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
					lineFreeHit = false;
					lastBowledBall = "lineball";
					Singleton<Scoreboard>.instance.showFreeHitBg(canShow: false);
				}
				else
				{
					fielder10Action = string.Empty;
					if (Singleton<GameModel>.instance != null)
					{
						if (noBall)
						{
							CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
							Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, currentBallNoOfRuns, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
							CONTROLLER.isJokerCall = false;
							if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
							{
							}
						}
						else if (freeHit)
						{
							Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, currentBallNoOfRuns, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
							freeHit = false;
							isFreeHit = false;
						}
						else if (!noBall && !freeHit)
						{
							Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, currentBallNoOfRuns, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
						}
					}
				}
			}
		}
		else if (fielder10Action == "umpireNoBallActionAfterBowlerCollectsBallFromFielder")
		{
			if (stayStartTime + noBallActionWaitTime + timeBetweenBalls < Time.time)
			{
				fielder10Action = string.Empty;
				if (Singleton<GameModel>.instance != null)
				{
					if (overStepBall)
					{
						if (!replayMode)
						{
							noBallRunUpdateStatus = "bowlercollectstheball";
							if (CONTROLLER.canShowReplay)
							{
								Singleton<GameModel>.instance.GameIsOnReplay();
								ShowReplay();
							}
							else
							{
								Singleton<GameModel>.instance.ReplayIsNotShown();
							}
						}
						else
						{
							HideReplay();
							CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
							Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, currentBallNoOfRuns, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
							if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
							{
							}
						}
					}
					else if (lineFreeHit)
					{
						Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, currentBallNoOfRuns, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
						lineFreeHit = false;
						lastBowledBall = "lineball";
					}
				}
			}
		}
		else if (fielder10Action == "runOutAppeal")
		{
			if (!fielderAppealForRunOut)
			{
				makeFieldersToCelebrate(null);
				fielderAppealForRunOut = true;
			}
			if (stayStartTime + 1f + timeBetweenBalls < Time.time)
			{
				stayStartTime = Time.time;
				fielder10Action = "waitForResult";
				showPreviewCamera(status: false);
				mainCamera.enabled = false;
				rightSideCamera.enabled = false;
				leftSideCamera.enabled = false;
				if (replayMode)
				{
					if (!CONTROLLER.runoutThirdUmpireAppeal)
					{
						fielder10Action = string.Empty;
						HideReplay();
						if (!isRunOut)
						{
							UpdateRunAfterReplay();
						}
						return;
					}
					if (CONTROLLER.runoutThirdUmpireAppeal)
					{
						ShowThirdUmpireRunoutDecisionPendingScreen();
						return;
					}
				}
				if (!replayMode)
				{
					umpireCamera.enabled = true;
					umpireCameraTransform.position = stump2Crease.transform.position;
					umpireCameraTransform.position = new Vector3(umpireCameraTransform.position.x, 1.5f, umpireCameraTransform.position.z);
					iTween.MoveTo(umpireCamera.gameObject, iTween.Hash("y", UnityEngine.Random.Range(1.4f, 1.8f), "time", 2));
					if (isRunOut)
					{
						batsmanOutIndex = runOutScenario("b");
						if (tightRunoutCall && !overStepBall && CONTROLLER.isLineFreeHitBallCompleted && CONTROLLER.PlayModeSelected != 6)
						{
							MainUmpireAnimationComponent.Play("3rd Umpire_New");
							fielder10Action = "waitForThirdUmpireSignal";
							CONTROLLER.runoutThirdUmpireAppeal = true;
							Singleton<GameModel>.instance.CanPauseGame = false;
						}
						else
						{
							MainUmpireAnimationComponent.Play("Out2_New");
						}
						if (Singleton<GameModel>.instance != null && !replayMode)
						{
							Singleton<GameModel>.instance.PlayGameSound("Cheer");
						}
					}
					else
					{
						if (tightRunoutCall && !overStepBall && CONTROLLER.isLineFreeHitBallCompleted && CONTROLLER.PlayModeSelected != 6)
						{
							MainUmpireAnimationComponent.Play("3rd Umpire_New");
							fielder10Action = "waitForThirdUmpireSignal";
							CONTROLLER.runoutThirdUmpireAppeal = true;
							Singleton<GameModel>.instance.CanPauseGame = false;
						}
						else
						{
							MainUmpireAnimationComponent.CrossFade("Crouch_toNotOut_New");
						}
						if (Singleton<GameModel>.instance != null && !replayMode)
						{
							Singleton<GameModel>.instance.PlayGameSound("Beaten");
						}
					}
					if (umpireRunDirection == 1)
					{
						mainUmpireTransform.position = umpireLeftSideSpot.transform.position;
						mainUmpireTransform.eulerAngles = new Vector3(mainUmpireTransform.eulerAngles.x, 90f, mainUmpireTransform.eulerAngles.z);
						umpireCameraTransform.eulerAngles = new Vector3(umpireCameraTransform.eulerAngles.x, 270f, umpireCameraTransform.eulerAngles.z);
						umpireCameraTransform.position -= new Vector3(3f, 0f, 0f);
					}
					else
					{
						mainUmpireTransform.localScale = new Vector3(1f, mainUmpireTransform.localScale.y, mainUmpireTransform.localScale.z);
						mainUmpireTransform.position = umpireRightSideSpot.transform.position;
						mainUmpireTransform.eulerAngles = new Vector3(mainUmpireTransform.eulerAngles.x, 270f, mainUmpireTransform.eulerAngles.z);
						umpireCameraTransform.eulerAngles = new Vector3(umpireCameraTransform.eulerAngles.x, 90f, umpireCameraTransform.eulerAngles.z);
						umpireCameraTransform.position += new Vector3(3f, 0f, 0f);
					}
				}
			}
		}
		else if (fielder10Action == "waitForThirdUmpireSignal")
		{
			if (stayStartTime + 4f < Time.time)
			{
				fielder10Action = string.Empty;
				CONTROLLER.stumpingAttempted = false;
				Singleton<GameModel>.instance.GameIsOnThirdUmpireRunoutReplay();
				ShowReplay();
			}
		}
		else if (fielder10Action == "waitFor3rdUmpireResultForRunout")
		{
			float num13 = 2f;
			if (veryTightRunoutCall)
			{
				num13 = 4f;
			}
			if (stayStartTime + num13 < Time.time)
			{
				digitalScreen.transform.localScale = new Vector3(0f, 0f, 0f);
				iTween.ScaleTo(digitalScreen, iTween.Hash("scale", digitalScreenScale, "time", 0.4f, "easetype", "spring"));
				if (isRunOut)
				{
					DigitalScreenRendererComponent.material.mainTexture = digitalBoardContent[3];
					if (Singleton<GameModel>.instance != null)
					{
						Singleton<GameModel>.instance.PlayGameSound("Cheer");
					}
				}
				else
				{
					DigitalScreenRendererComponent.material.mainTexture = digitalBoardContent[2];
					if (Singleton<GameModel>.instance != null)
					{
						Singleton<GameModel>.instance.PlayGameSound("Beaten");
					}
				}
				stayStartTime = Time.time;
				fielder10Action = "Show3rdUmpireResultForRunout";
			}
		}
		else if (fielder10Action == "Show3rdUmpireResultForRunout")
		{
			if (stayStartTime + 1f < Time.time)
			{
				HideReplay();
				UpdateThirdUmpireRunoutResult();
				fielder10Action = string.Empty;
				stayStartTime = Time.time;
				return;
			}
		}
		else if (fielder10Action == "waitForResult")
		{
			float num14 = 3f;
			if (replayMode)
			{
				num14 = 2f;
			}
			if (getOverStepBall())
			{
				num14 = 0f;
			}
			if (stayStartTime + num14 + timeBetweenBalls < Time.time)
			{
				batsmanOutIndex = runOutScenario("b");
				if (isRunOut)
				{
					if (overStepBall)
					{
						fielder10Action = "bowlerrunoutappealsandout";
						bool flag3 = checkForMatchComplete(currentBallNoOfRuns + 1, CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWickets + 1);
						if (CONTROLLER.matchType == "oneday" && !flag3)
						{
							noBallActionWaitTime = 7f;
							MainUmpireAnimationComponent.Play("OutNoBallFreeHit_New");
							lineFreeHit = true;
							lastBowledBall = "overstep";
						}
						else
						{
							noBallActionWaitTime = 2.75f;
							MainUmpireAnimationComponent.Play("OutNoBallFreeHit_New");
						}
					}
					else if (lineFreeHit)
					{
						fielder10Action = string.Empty;
						Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, currentBallNoOfRuns, 0, CONTROLLER.StrikerIndex, 1, 4, CONTROLLER.CurrentBowlerIndex, 1, batsmanOutIndex, isBoundary: false);
						lineFreeHit = false;
						lastBowledBall = "lineball";
						Singleton<Scoreboard>.instance.showFreeHitBg(canShow: false);
					}
					else
					{
						if (replayMode)
						{
							fielder10Action = string.Empty;
							HideReplay();
							return;
						}
						fielder10Action = string.Empty;
						if (Singleton<GameModel>.instance != null)
						{
							if (noBall)
							{
								CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
								Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, currentBallNoOfRuns, 1, CONTROLLER.StrikerIndex, 1, 4, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
								CONTROLLER.isJokerCall = false;
								if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
								{
								}
							}
							else if (freeHit)
							{
								Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, currentBallNoOfRuns, 0, CONTROLLER.StrikerIndex, 1, 4, CONTROLLER.CurrentBowlerIndex, 0, batsmanOutIndex, isBoundary: false);
								freeHit = false;
							}
							else if (!noBall && !freeHit)
							{
								Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, currentBallNoOfRuns, 0, CONTROLLER.StrikerIndex, 1, 4, CONTROLLER.CurrentBowlerIndex, 1, batsmanOutIndex, isBoundary: false);
							}
						}
					}
				}
				else if (overStepBall)
				{
					fielder10Action = "bowlerrunoutappealbutnotout";
					bool flag4 = checkForMatchComplete(currentBallNoOfRuns + 1, 0);
					if (CONTROLLER.matchType == "oneday" && !flag4)
					{
						noBallActionWaitTime = 7f;
						MainUmpireAnimationComponent.Play("NoBallFreeHit_New");
						lineFreeHit = true;
						lastBowledBall = "overstep";
					}
					else
					{
						noBallActionWaitTime = 1.5f;
						MainUmpireAnimationComponent.Play("NoBallFreeHit_New");
					}
				}
				else if (lineFreeHit)
				{
					fielder10Action = string.Empty;
					Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, currentBallNoOfRuns, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
					lineFreeHit = false;
					lastBowledBall = "lineball";
					Singleton<Scoreboard>.instance.showFreeHitBg(canShow: false);
				}
				else
				{
					fielder10Action = string.Empty;
					if (Singleton<GameModel>.instance != null)
					{
						if (noBall)
						{
							CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
							Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, currentBallNoOfRuns, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
							if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
							{
							}
						}
						else if (freeHit)
						{
							Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, currentBallNoOfRuns, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
						}
						else if (!noBall && !freeHit)
						{
							if (!replayMode)
							{
								afterReplayUpdateRunForRunoutFailedAttempt = true;
								if (CONTROLLER.canShowReplay)
								{
									Singleton<GameModel>.instance.GameIsOnReplay();
									ShowReplay();
								}
								else
								{
									Singleton<GameModel>.instance.ReplayIsNotShown();
								}
							}
							else
							{
								HideReplay();
								UpdateRunAfterReplay();
							}
						}
					}
				}
			}
		}
		else if (fielder10Action == "bowlerrunoutappealsandout")
		{
			if (stayStartTime + noBallActionWaitTime + timeBetweenBalls < Time.time)
			{
				fielder10Action = string.Empty;
				if (Singleton<GameModel>.instance != null)
				{
					if (overStepBall)
					{
						runsScoredInLineNoBall = currentBallNoOfRuns;
						CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
						Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, currentBallNoOfRuns, 1, CONTROLLER.StrikerIndex, 1, 4, CONTROLLER.CurrentBowlerIndex, 0, batsmanOutIndex, isBoundary: false);
						if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
						{
						}
					}
					else if (lineFreeHit)
					{
						Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, currentBallNoOfRuns, 0, CONTROLLER.StrikerIndex, 1, 4, CONTROLLER.CurrentBowlerIndex, 0, batsmanOutIndex, isBoundary: false);
						lineFreeHit = false;
						lastBowledBall = "lineball";
					}
				}
			}
		}
		else if (fielder10Action == "bowlerrunoutappealbutnotout")
		{
			if (stayStartTime + noBallActionWaitTime + timeBetweenBalls < Time.time)
			{
				fielder10Action = string.Empty;
				if (Singleton<GameModel>.instance != null)
				{
					if (overStepBall)
					{
						if (!replayMode)
						{
							noBallRunUpdateStatus = "bowlerrunoutappealsandnotout";
							runsScoredInLineNoBall = currentBallNoOfRuns;
							if (CONTROLLER.canShowReplay)
							{
								Singleton<GameModel>.instance.GameIsOnReplay();
								ShowReplay();
							}
							else
							{
								Singleton<GameModel>.instance.ReplayIsNotShown();
							}
						}
						else
						{
							HideReplay();
							CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
							Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, currentBallNoOfRuns, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, batsmanOutIndex, isBoundary: false);
							if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
							{
							}
						}
					}
					else if (lineFreeHit)
					{
						Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, currentBallNoOfRuns, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, batsmanOutIndex, isBoundary: false);
						lineFreeHit = false;
						lastBowledBall = "lineball";
					}
				}
			}
		}
		else if (fielder10Action == "lbwAppeal")
		{
			if (stayStartTime + 1.5f + timeBetweenBalls < Time.time)
			{
				if (overStepBall)
				{
					if (replayMode)
					{
						fielder10Action = string.Empty;
						HideReplay();
						CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
						Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, 0, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
						if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
						{
						}
						return;
					}
					showMainUmpireForNoBallAction();
					fielder10Action = "umpireNoBallActionForLBW";
					bool flag5 = checkForMatchComplete(1, 0);
					if (CONTROLLER.matchType == "oneday" && !flag5)
					{
						noBallActionWaitTime = 4f;
						MainUmpireAnimationComponent.Play("NoBallFreeHit_New");
						lineFreeHit = true;
						lastBowledBall = "overstep";
					}
					else
					{
						noBallActionWaitTime = 1.5f;
						MainUmpireAnimationComponent.Play("NoBallFreeHit_New");
					}
				}
				else if (lineFreeHit)
				{
					fielder10Action = string.Empty;
					Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, 0, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
					lineFreeHit = false;
					lastBowledBall = "lineball";
					Singleton<Scoreboard>.instance.showFreeHitBg(canShow: false);
				}
				else
				{
					ResetFielders();
					fielder10Action = "waitForLbwResult";
					showPreviewCamera(status: false);
					if (!replayMode || LBW)
					{
					}
					if (replayMode)
					{
						fielder10Action = string.Empty;
						HideReplay();
						canShowDRS = false;
						return;
					}
					if (!noBall && !freeHit)
					{
						canShowDRS = true;
					}
					else
					{
						canShowDRS = false;
					}
					showMainUmpireForNoBallAction();
					if (LBW)
					{
						if (noBall || freeHit)
						{
							MainUmpireAnimationComponent.Play("NotOut");
						}
						if (!noBall && !freeHit)
						{
							MainUmpireAnimationComponent.Play("Out");
						}
					}
					else
					{
						MainUmpireAnimationComponent.Play("NotOut");
						if (Singleton<GameModel>.instance != null && !replayMode)
						{
							Singleton<GameModel>.instance.PlayGameSound("Beaten");
						}
					}
				}
			}
		}
		else if (fielder10Action == "umpireNoBallActionForLBW")
		{
			if (stayStartTime + noBallActionWaitTime + timeBetweenBalls < Time.time)
			{
				fielder10Action = string.Empty;
				if (Singleton<GameModel>.instance != null)
				{
					if (overStepBall)
					{
						if (!replayMode)
						{
							noBallRunUpdateStatus = "lbwappeal";
							if (CONTROLLER.canShowReplay)
							{
								Singleton<GameModel>.instance.GameIsOnReplay();
								ShowReplay();
							}
							else
							{
								Singleton<GameModel>.instance.ReplayIsNotShown();
							}
						}
						else
						{
							HideReplay();
							CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
							Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, 0, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
							if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
							{
							}
						}
					}
					else if (lineFreeHit)
					{
						Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, 0, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
						lineFreeHit = false;
						lastBowledBall = "lineball";
					}
				}
			}
		}
		else if (fielder10Action == "waitForLbwResult" && stayStartTime + 2f + timeBetweenBalls < Time.time)
		{
			if (LBW)
			{
				fielder10Action = string.Empty;
				if (replayMode)
				{
					HideReplay();
					return;
				}
				if (Singleton<GameModel>.instance != null)
				{
					if (noBall)
					{
						CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
						Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, 0, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
						CONTROLLER.isJokerCall = false;
						if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
						{
						}
					}
					else if (freeHit)
					{
						Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, 0, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
						freeHit = false;
						isFreeHit = false;
					}
					else if (!noBall && !freeHit)
					{
						Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, 0, 0, CONTROLLER.StrikerIndex, 1, 2, CONTROLLER.CurrentBowlerIndex, 0, CONTROLLER.StrikerIndex, isBoundary: false);
					}
				}
			}
			else
			{
				fielder10Action = string.Empty;
				action = 10;
				if (Singleton<GameModel>.instance != null)
				{
					if (noBall)
					{
						CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
						Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, 0, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
						CONTROLLER.isJokerCall = false;
						if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
						{
						}
					}
					else if (freeHit)
					{
						Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, 0, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
						freeHit = false;
						isFreeHit = false;
					}
					else
					{
						Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, 0, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
					}
				}
			}
		}
		if (canShowDRS && Singleton<DRS>.instance.enable && !Singleton<DRS>.instance.DRSreplay && CONTROLLER.PlayModeSelected != 4 && CONTROLLER.PlayModeSelected != 5 && CONTROLLER.PlayModeSelected != 6)
		{
			action = -1;
			waitForReview();
		}
	}

	public void SkipThirdUmpireRunoutReplay()
	{
		SkipReplay();
		action = 4;
		horizontalSpeed = 0f;
		fielder10Action = "runOutAppeal";
		isRunOut = savedIsRunOut;
		currentBallNoOfRuns = savedCurrentBallNoOfRuns;
		thirdUmpireRunoutReplaySkipped = true;
		ShowThirdUmpireRunoutDecisionPendingScreen();
	}

	private void ShowThirdUmpireRunoutDecisionPendingScreen()
	{
		Time.timeScale = 1f;
		Singleton<GameModel>.instance.GameIsNotOnStumpingReplay();
		replayCamera.enabled = false;
		introCamera.enabled = true;
		introCameraTransform.position = new Vector3(-76f, 17f, -3.8f);
		introCameraTransform.eulerAngles = new Vector3(0f, -90f, 0f);
		DigitalScreenRendererComponent.material.mainTexture = digitalBoardContent[1];
		digitalScreen.transform.localScale = digitalScreenScale;
		stayStartTime = Time.time;
		fielder10Action = "waitFor3rdUmpireResultForRunout";
	}

	private void UpdateThirdUmpireRunoutResult()
	{
		if (isRunOut)
		{
			if (!(Singleton<GameModel>.instance != null))
			{
				return;
			}
			if (noBall)
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
				Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, currentBallNoOfRuns, 1, CONTROLLER.StrikerIndex, 1, 4, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
				CONTROLLER.isJokerCall = false;
				if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
				{
				}
			}
			else if (freeHit)
			{
				Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, currentBallNoOfRuns, 0, CONTROLLER.StrikerIndex, 1, 4, CONTROLLER.CurrentBowlerIndex, 0, batsmanOutIndex, isBoundary: false);
				freeHit = false;
			}
			else if (!noBall && !freeHit)
			{
				Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, currentBallNoOfRuns, 0, CONTROLLER.StrikerIndex, 1, 4, CONTROLLER.CurrentBowlerIndex, 1, batsmanOutIndex, isBoundary: false);
			}
		}
		else
		{
			if (!(Singleton<GameModel>.instance != null))
			{
				return;
			}
			if (noBall)
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
				Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, currentBallNoOfRuns, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
				if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
				{
				}
			}
			else if (freeHit)
			{
				Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, currentBallNoOfRuns, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
			}
			else if (!noBall && !freeHit)
			{
				Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, savedCurrentBallNoOfRuns, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
			}
		}
	}

	[Skip]
	private void EnableFielder10ToCollectBall()
	{
		Fielder10AnimationComponent.Play("idle");
		fielder10Transform.LookAt(fielder10FocusGObjToCollectTheBall.transform);
		fielder10Action = "waitForBall";
	}

	private bool deactivateSlipFielders(GameObject fielderGO)
	{
		for (int i = 0; i < getSlipArray.Count; i++)
		{
			if (fielderGO == getSlipArray[i])
			{
				return true;
			}
		}
		return false;
	}

	private void ActivateFielders()
	{
		int num = 0;
		if (CONTROLLER.BowlingTeamIndex == CONTROLLER.myTeamIndex)
		{
			num = 1;
		}
		Vector3 vector = default(Vector3);
		int num2 = 180;
		if (activeFielderNumber.Count <= 0 && !powerShot)
		{
			speedReduceFactor = 10f;
		}
		for (int i = 0; i < activeFielderNumber.Count; i++)
		{
			GameObject gameObject = fielder[activeFielderNumber[i]];
			Animation animation = FielderAnimationComponent[activeFielderNumber[i]];
			GameObject gameObject2 = fielderSkin[activeFielderNumber[i]];
			GameObject gameObject3 = fielderRef[activeFielderNumber[i]];
			GameObject gameObject4 = fielderBall[activeFielderNumber[i]];
			GameObject gameObject5 = fielderChasePoint[activeFielderNumber[i]];
			int num3 = activeFielderNumber[i];
			if (activeFielderNumber.Count > 1 && (activeFielderAction[i] == "goForCatch" || activeFielderAction[i] == "goForChase" || activeFielderAction[i] == "waitToCatch"))
			{
				for (int j = 0; j < activeFielderNumber.Count - 1; j++)
				{
					GameObject gameObject6 = fielder[activeFielderNumber[j]];
					for (int k = j + 1; k < activeFielderNumber.Count; k++)
					{
						GameObject gameObject7 = fielder[activeFielderNumber[k]];
						if (!(DistanceBetweenTwoVector2(gameObject6, gameObject7) < 3f))
						{
							continue;
						}
						if (DistanceBetweenTwoGameObjects(gameObject6, ball) < DistanceBetweenTwoGameObjects(gameObject7, ball))
						{
							if (activeFielderAction[k] == "goForCatch" || activeFielderAction[k] == "goForChase")
							{
								activeFielderAction[k] = "waitAndSeeTheCatch";
								gameObject7.GetComponent<Animation>().Play("idle");
								gameObject = gameObject6;
							}
						}
						else if (activeFielderAction[j] == "goForCatch" || activeFielderAction[j] == "goForChase")
						{
							activeFielderAction[j] = "waitAndSeeTheCatch";
							gameObject6.GetComponent<Animation>().Play("idle");
							gameObject = gameObject7;
						}
					}
				}
			}
			if (activeFielderAction[i] == "goForCatch")
			{
				if ((ballOnboundaryLine || stopTheFielders) && ballResult != "wicket")
				{
					if (!animation.IsPlaying("runComplete") && DistanceBetweenTwoGameObjects(gameObject, ballTimingOrigin) > 8f)
					{
						animation.Play("runComplete");
						fielderSpeed -= fielderSpeed * Time.deltaTime * 0.5f;
						float num4 = AngleBetweenTwoGameObjects(gameObject, ball);
						fielderTransform[activeFielderNumber[i]].transform.position += new Vector3(Mathf.Cos(num4 * DEG2RAD) * fielderSpeed * Time.deltaTime, 0f, Mathf.Sin(num4 * DEG2RAD) * fielderSpeed * Time.deltaTime);
					}
					if (ballOnboundaryLine && DistanceBetweenTwoGameObjects(gameObject, groundCenterPoint) > 8f)
					{
						fielderSpeed -= fielderSpeed * Time.deltaTime * 0.5f;
						float num4 = AngleBetweenTwoGameObjects(gameObject, ball);
						fielderTransform[activeFielderNumber[i]].position += new Vector3(Mathf.Cos(num4 * DEG2RAD) * fielderSpeed * Time.deltaTime, 0f, Mathf.Sin(num4 * DEG2RAD) * fielderSpeed * Time.deltaTime);
						activeFielderAction[i] = "stopChasing";
					}
					if (stopTheFielders)
					{
						animation.Play("runComplete");
						fielderSpeed -= fielderSpeed * Time.deltaTime * 0.5f;
						float num4 = AngleBetweenTwoGameObjects(gameObject, ball);
						fielderTransform[activeFielderNumber[i]].position += new Vector3(Mathf.Cos(num4 * DEG2RAD) * fielderSpeed * Time.deltaTime, 0f, Mathf.Sin(num4 * DEG2RAD) * fielderSpeed * Time.deltaTime);
						fielderTransform[activeFielderNumber[i]].LookAt(new Vector3(ballTransform.position.x, 0f, ballTransform.position.z));
					}
				}
				if (DistanceBetweenTwoGameObjects(ballCatchingSpot, ballTimingFirstBounce) < 3f && topEdge)
				{
					ballCatchingSpotTransform.position = new Vector3(ballTimingFirstBounce.transform.position.x, ballCatchingSpotTransform.position.y, ballTimingFirstBounce.transform.position.z);
				}
				if (DistanceBetweenTwoGameObjects(gameObject, ballTimingOrigin) < 8f)
				{
					vector = fielderTransform[activeFielderNumber[i]].InverseTransformPoint(ballTimingOriginTransform.position);
					num2 = 180;
					if (vector.x > 0f)
					{
						num2 = -180;
					}
					if (!fielderNearToPitch[num3])
					{
						fielderNearToPitch[num3] = true;
						iTween.RotateTo(gameObject, iTween.Hash("y", fielderTransform[activeFielderNumber[i]].eulerAngles.y + (float)num2, "time", 0.4, "oncomplete", "stopITween", "oncompletetarget", base.gameObject, "oncompleteparams", gameObject));
					}
				}
				else if ((double)DistanceBetweenTwoGameObjects(gameObject, ballCatchingSpot) > 0.6)
				{
					if (fielderToCatchTheBall == gameObject || (fielderToCatchTheBall != gameObject && DistanceBetweenTwoGameObjects(gameObject, ballCatchingSpot) > 3f))
					{
						float num4 = AngleBetweenTwoGameObjects(gameObject, ballCatchingSpot);
						fielderTransform[activeFielderNumber[i]].position += new Vector3(Mathf.Cos(num4 * DEG2RAD) * fielderSpeed * Time.deltaTime, 0f, Mathf.Sin(num4 * DEG2RAD) * fielderSpeed * Time.deltaTime);
						fielderTransform[activeFielderNumber[i]].LookAt(ballCatchingSpotTransform);
					}
					else
					{
						activeFielderAction[i] = "waitAndSeeTheCatch";
						fielderTransform[activeFielderNumber[i]].LookAt(ballCatchingSpotTransform);
						animation.Play("idle");
					}
				}
				else
				{
					activeFielderAction[i] = "waitToCatch";
					fielderTransform[activeFielderNumber[i]].position = new Vector3(ballCatchingSpotTransform.position.x, fielderTransform[activeFielderNumber[i]].position.y, ballCatchingSpotTransform.position.z);
					float y = fielderTransform[activeFielderNumber[i]].eulerAngles.y;
					fielderTransform[activeFielderNumber[i]].LookAt(ballTimingOriginTransform);
					float y2 = fielderTransform[activeFielderNumber[i]].eulerAngles.y;
					fielderTransform[activeFielderNumber[i]].eulerAngles = new Vector3(fielderTransform[activeFielderNumber[i]].eulerAngles.x, y, fielderTransform[activeFielderNumber[i]].eulerAngles.z);
					iTween.RotateTo(gameObject, iTween.Hash("y", y2, "time", 0.1));
					animation.Play("idle");
				}
			}
			else if (activeFielderAction[i] == "waitToCatch")
			{
				float num5 = 4f;
				float num6 = num5 * animationFPSDivide * horizontalSpeed;
				if (DistanceBetweenTwoVector2(gameObject, ball) < num6)
				{
					float num7 = ballPreCatchingDistance / horizontalSpeed;
					float num8 = num7 * ballProjectileAnglePerSecond;
					float num9 = 360f - num8;
					float num10 = Mathf.Abs(ballProjectileHeight * Mathf.Sin(num9 * DEG2RAD));
					if (num10 < 0.2f)
					{
						animation.Play("lowCatch");
					}
					else if (num10 < 1.33f)
					{
						animation.Play("hipCatch");
						animation["hipCatch"].speed = 2.5f;
					}
					else if (num10 < 2f)
					{
						animation.Play("sideCatch");
					}
					else if (num10 < 2.5f)
					{
						animation.Play("highCatch");
					}
					if (Singleton<GameModel>.instance != null && !replayMode)
					{
						Singleton<GameModel>.instance.PlayGameSound("Cheer");
					}
					activeFielderAction[i] = "catchAttempt";
					stopTheFielders = true;
					fielderSpeed = 0f;
				}
			}
			else if (activeFielderAction[i] == "catchAttempt")
			{
				if (DistanceBetweenTwoVector2(gameObject, ball) < 0.5f || DistanceBetweenTwoVector2(groundCenterPoint, ball) > DistanceBetweenTwoVector2(groundCenterPoint, gameObject))
				{
					if (!replayMode)
					{
						limitReplayCameraHeight = true;
					}
					if ((ballTransform.position.y < 2.5f && !replayMode) || (savedSummary == "catch" && replayMode) || (savedSummary == "picked" && savedSummary == "picked" && isFreeHit) || (savedSummary == "picked" && (overStepBall || lineFreeHit || !CONTROLLER.isLineFreeHitBallCompleted)))
					{
						ShowBall(status: false);
						gameObject4.GetComponent<Renderer>().enabled = true;
						if (overStepBall || lineFreeHit || !CONTROLLER.isLineFreeHitBallCompleted)
						{
							animation.Play("throw");
							if (isEnhanced)
							{
								animation["throw"].speed = 7f;
							}
							else
							{
								animation["throw"].speed = 1f;
							}
							activeFielderAction[i] = "pickedup";
						}
						else if (noBall || freeHit || ballNoOfBounce >= 1)
						{
							animation.Play("throw");
							animation["throw"].speed = 7f / (7f * (1f - controlFactor * (float)num));
							if (Singleton<RightFovLerp>.instance != null)
							{
								Singleton<RightFovLerp>.instance.CallDisableBoundaryCam();
							}
							if (Singleton<LeftFovLerp>.instance != null)
							{
								Singleton<LeftFovLerp>.instance.CallDisableBoundaryCam();
							}
							activeFielderAction[i] = "pickedup";
							if (!noBall)
							{
								isJokerFreeHit = true;
							}
						}
						else if (isJokerFreeHit && replayMode && savedSummary == "picked")
						{
							animation.Play("throw");
							animation["throw"].speed = 7f / (7f * (1f - controlFactor * (float)num));
							if (Singleton<RightFovLerp>.instance != null)
							{
								Singleton<RightFovLerp>.instance.CallDisableBoundaryCam();
							}
							if (Singleton<LeftFovLerp>.instance != null)
							{
								Singleton<LeftFovLerp>.instance.CallDisableBoundaryCam();
							}
							activeFielderAction[i] = "pickedup";
							if (!noBall)
							{
								isJokerFreeHit = false;
							}
						}
						else
						{
							int num11;
							if (!replayMode)
							{
								savedSummary = "catch";
								num11 = (savedCelebrationAnimationIndex = UnityEngine.Random.Range(0, 3));
							}
							else
							{
								num11 = savedCelebrationAnimationIndex;
							}
							switch (num11)
							{
							case 0:
								animation.PlayQueued("celebration", QueueMode.CompleteOthers);
								break;
							case 1:
								animation.PlayQueued("celebration2", QueueMode.CompleteOthers);
								break;
							default:
								animation.PlayQueued("appeal", QueueMode.CompleteOthers);
								break;
							}
							animation.PlayQueued("celebrationRun", QueueMode.CompleteOthers);
							fielder10Action = string.Empty;
							Fielder10AnimationComponent.Play("appeal");
							activeFielderAction[i] = "catched";
							ballResult = "wicket";
							makeFieldersToCelebrate(gameObject);
						}
						pauseTheBall = true;
						if (!overStepBall && !lineFreeHit && CONTROLLER.isLineFreeHitBallCompleted)
						{
							canTakeRun = false;
							disableRunCancelBtn();
						}
						else
						{
							canTakeRun = true;
						}
					}
					else
					{
						if (ballTransform.position.y < 4f)
						{
							animation.Play("highCatch");
							animation["highCatch"].speed = 2f;
						}
						iTween.RotateTo(gameObject, iTween.Hash("y", fielderTransform[activeFielderNumber[i]].eulerAngles.y + 135f, "time", 2));
						activeFielderAction[i] = "waitingTooHighBall";
						if (topEdge)
						{
							activeFielderAction[i] = "goForChase";
							animation.Play("run");
							iTween.Stop();
							ballOnboundaryLine = false;
							stopTheFielders = false;
						}
					}
				}
			}
			else if (activeFielderAction[i] == "catched")
			{
				if (animation.IsPlaying("celebrationRun"))
				{
					stayStartTime = Time.time;
					activeFielderAction[i] = "celebrationRun";
				}
			}
			else if (activeFielderAction[i] == "celebrationRun")
			{
				if (replayMode)
				{
					ResetFielders();
					activeFielderAction[i] = string.Empty;
					HideReplay();
					break;
				}
				float num4 = AngleBetweenTwoGameObjects(gameObject, ballTimingOrigin);
				fielderTransform[activeFielderNumber[i]].position += new Vector3(Mathf.Cos(num4 * DEG2RAD) * fielderSpeed / 1.5f * Time.deltaTime, 0f, Mathf.Sin(num4 * DEG2RAD) * fielderSpeed / 1.5f * Time.deltaTime);
				if (stayStartTime - 0.5f + timeBetweenBalls < Time.time)
				{
					ResetFielders();
					stayStartTime = Time.time;
					activeFielderAction[i] = "waitForResult";
					mainUmpireTransform.position = mainUmpireInitPosition;
					mainUmpireTransform.eulerAngles = new Vector3(mainUmpireTransform.eulerAngles.x, 0f, mainUmpireTransform.eulerAngles.z);
					mainUmpireTransform.localScale = new Vector3(1f, mainUmpireTransform.localScale.y, mainUmpireTransform.localScale.z);
					rightSideCamera.enabled = false;
					leftSideCamera.enabled = false;
					mainCamera.enabled = false;
					showPreviewCamera(status: false);
					umpireCamera.enabled = true;
					umpireCameraTransform.position = mainUmpireInitPosition;
					umpireCameraTransform.position = new Vector3(umpireCameraTransform.position.x, 2f, umpireCameraTransform.position.z);
					umpireCameraTransform.position += new Vector3(0f, 0f, 3f);
					umpireCameraTransform.eulerAngles = new Vector3(umpireCameraTransform.eulerAngles.x, 180f, umpireCameraTransform.eulerAngles.z);
					umpireCameraTransform.eulerAngles = new Vector3(5f, umpireCameraTransform.eulerAngles.y, umpireCameraTransform.eulerAngles.z);
					iTween.MoveTo(umpireCamera.gameObject, iTween.Hash("position", new Vector3(mainUmpireInitPosition.x - UnityEngine.Random.Range(6f, 10f), UnityEngine.Random.Range(1.4f, 4f), UnityEngine.Random.Range(-6f, -8f)), "time", 4.5));
					iTween.RotateTo(umpireCamera.gameObject, iTween.Hash("y", 140, "time", 4.5));
					if (noBall || freeHit)
					{
						MainUmpireAnimationComponent.Play("NotOut");
					}
					if (!noBall && !freeHit)
					{
						MainUmpireAnimationComponent.Play("Out");
					}
					if (Singleton<GameModel>.instance != null && !replayMode)
					{
						Singleton<GameModel>.instance.PlayGameSound("Cheer");
					}
				}
			}
			else if (activeFielderAction[i] == "waitForResult")
			{
				if (stayStartTime + 2f + timeBetweenBalls < Time.time)
				{
					activeFielderAction[i] = string.Empty;
					if (Singleton<GameModel>.instance != null && !replayMode)
					{
						if (noBall)
						{
							CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
							Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, 0, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, activeFielderNumber[i], CONTROLLER.StrikerIndex, isBoundary: false);
							CONTROLLER.isJokerCall = false;
							if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
							{
							}
						}
						else if (freeHit)
						{
							Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, 0, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, activeFielderNumber[i], CONTROLLER.StrikerIndex, isBoundary: false);
							freeHit = false;
						}
						else if (!noBall && !freeHit)
						{
							Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, 0, 0, CONTROLLER.StrikerIndex, 1, 3, CONTROLLER.CurrentBowlerIndex, activeFielderNumber[i], CONTROLLER.StrikerIndex, isBoundary: false);
						}
					}
				}
			}
			else if (activeFielderAction[i] == "BadCallCaughtResult")
			{
				if (stayStartTime + 2f + timeBetweenBalls < Time.time)
				{
					activeFielderAction[i] = string.Empty;
					if (Singleton<GameModel>.instance != null && !replayMode)
					{
						Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, currentBallNoOfRuns, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
					}
				}
			}
			else if (activeFielderAction[i] == "goForChase")
			{
				bool flag = false;
				if (ballOnboundaryLine || stopTheFielders)
				{
					if (ballResult != "wicket")
					{
						if (!animation.IsPlaying("runComplete") && DistanceBetweenTwoGameObjects(gameObject, ballTimingOrigin) > 8f)
						{
							animation.Play("runComplete");
							fielderSpeed -= fielderSpeed * Time.deltaTime * 0.5f;
							float num4 = AngleBetweenTwoGameObjects(gameObject, ball);
							fielderTransform[activeFielderNumber[i]].position += new Vector3(Mathf.Cos(num4 * DEG2RAD) * fielderSpeed * Time.deltaTime, 0f, Mathf.Sin(num4 * DEG2RAD) * fielderSpeed * Time.deltaTime);
							activeFielderAction[i] = "stopChasing";
						}
						if (ballOnboundaryLine && DistanceBetweenTwoGameObjects(gameObject, groundCenterPoint) > 8f)
						{
							fielderSpeed -= fielderSpeed * Time.deltaTime * 0.5f;
							float num4 = AngleBetweenTwoGameObjects(gameObject, ball);
							fielderTransform[activeFielderNumber[i]].position += new Vector3(Mathf.Cos(num4 * DEG2RAD) * fielderSpeed * Time.deltaTime, 0f, Mathf.Sin(num4 * DEG2RAD) * fielderSpeed * Time.deltaTime);
							activeFielderAction[i] = "stopChasing";
						}
						if (stopTheFielders)
						{
							animation.Play("runComplete");
							fielderSpeed -= fielderSpeed * Time.deltaTime * 0.5f;
							float num4 = AngleBetweenTwoGameObjects(gameObject, ball);
							fielderTransform[activeFielderNumber[i]].position += new Vector3(Mathf.Cos(num4 * DEG2RAD) * fielderSpeed * Time.deltaTime, 0f, Mathf.Sin(num4 * DEG2RAD) * fielderSpeed * Time.deltaTime);
							fielderTransform[activeFielderNumber[i]].LookAt(new Vector3(ballTransform.position.x, 0f, ballTransform.position.z));
						}
						float time = animation["runComplete"].time;
						float num12 = time * animationFPS;
						if (num12 > 50f || DistanceBetweenTwoVector2(groundCenterPoint, gameObject) > 70f)
						{
							activeFielderAction[i] = "stopChasing";
						}
					}
				}
				else if (DistanceBetweenTwoGameObjects(gameObject, ballTimingOrigin) < 8f)
				{
					vector = fielderTransform[activeFielderNumber[i]].InverseTransformPoint(ballTimingOriginTransform.position);
					num2 = 180;
					if (vector.x > 0f)
					{
						num2 = -180;
					}
					if (!fielderNearToPitch[num3])
					{
						fielderNearToPitch[num3] = true;
						iTween.RotateTo(gameObject, iTween.Hash("y", fielderTransform[activeFielderNumber[i]].eulerAngles.y + (float)num2, "time", 0.4, "oncomplete", "stopITween", "oncompletetarget", base.gameObject, "oncompleteparams", gameObject));
					}
				}
				else if (DistanceBetweenTwoGameObjects(ballTimingOrigin, ball) > DistanceBetweenTwoGameObjects(ballTimingOrigin, gameObject5))
				{
					float num4 = AngleBetweenTwoGameObjects(gameObject, ball);
					fielderTransform[activeFielderNumber[i]].position += new Vector3(Mathf.Cos(num4 * DEG2RAD) * fielderSpeed * Time.deltaTime, 0f, Mathf.Sin(num4 * DEG2RAD) * fielderSpeed * Time.deltaTime);
					fielderTransform[activeFielderNumber[i]].LookAt(new Vector3(ballTransform.position.x, 0f, ballTransform.position.z));
				}
				else if (DistanceBetweenTwoGameObjects(gameObject, gameObject5) > 0.17f)
				{
					float num13 = DistanceBetweenTwoGameObjects(gameObject, gameObject5);
					float num14 = num13 / fielderSpeed;
					float num15 = DistanceBetweenTwoGameObjects(ball, gameObject5) / horizontalSpeed;
					float num16 = DistanceBetweenTwoVector2(groundCenterPoint, gameObject);
					if (fielderToCatchTheBall != gameObject && num13 <= 5f && num13 >= 2f && DistanceBetweenTwoGameObjects(ballTimingOrigin, ball) < DistanceBetweenTwoGameObjects(ballTimingOrigin, gameObject5) && num15 < num14 && ballTransform.position.y < 0.5f && num16 > groundRadius - 5f)
					{
						activeFielderAction[i] = "diveToField";
						animation.Play("diveStraight");
					}
					else if (fielderToCatchTheBall != gameObject && DistanceBetweenTwoGameObjects(gameObject, ballCatchingSpot) > 3f)
					{
						float num17 = DistanceBetweenTwoGameObjects(gameObject, gameObject5);
						float num4 = AngleBetweenTwoGameObjects(gameObject, gameObject5);
						fielderTransform[activeFielderNumber[i]].position += new Vector3(Mathf.Cos(num4 * DEG2RAD) * fielderSpeed * Time.deltaTime, 0f, Mathf.Sin(num4 * DEG2RAD) * fielderSpeed * Time.deltaTime);
						fielderTransform[activeFielderNumber[i]].LookAt(gameObject5.transform);
						float num18 = DistanceBetweenTwoGameObjects(gameObject, gameObject5);
						if (num17 < num18)
						{
							fielderTransform[activeFielderNumber[i]].position = new Vector3(gameObject5.transform.position.x, fielderTransform[activeFielderNumber[i]].position.y, gameObject5.transform.position.z);
						}
					}
					else
					{
						activeFielderAction[i] = "waitAndSeeTheCatch";
						animation.Play("idle");
					}
				}
				else
				{
					animation.Play("idle");
					fielderTransform[activeFielderNumber[i]].LookAt(ballTimingOriginTransform);
					activeFielderAction[i] = "waitToPick";
				}
			}
			else if (activeFielderAction[i] == "stopChasing")
			{
				if (fielderSpeed > 1f)
				{
					fielderSpeed -= 2.8f * Time.deltaTime;
					float num4 = AngleBetweenTwoGameObjects(gameObject, ball);
					fielderTransform[activeFielderNumber[i]].position += new Vector3(Mathf.Cos(num4 * DEG2RAD) * fielderSpeed * Time.deltaTime, 0f, Mathf.Sin(num4 * DEG2RAD) * fielderSpeed * Time.deltaTime);
				}
				if (fielderSpeed <= 1f && !animation.IsPlaying("WCCLite_FielderIdle0"))
				{
					fielderSpeed = 0f;
					animation.CrossFade("WCCLite_FielderIdle05", 0.1f);
				}
			}
			else if (activeFielderAction[i] == "diveToField")
			{
				float num19 = 15f;
				if (animation.IsPlaying("diveStraight") && animation["diveStraight"].time < num19 * animationFPSDivide)
				{
					float num4 = AngleBetweenTwoGameObjects(gameObject, gameObject5);
					fielderTransform[activeFielderNumber[i]].position += new Vector3(Mathf.Cos(num4 * DEG2RAD) * fielderSpeed / 1.5f * Time.deltaTime, 0f, Mathf.Sin(num4 * DEG2RAD) * fielderSpeed / 1.5f * Time.deltaTime);
				}
				else
				{
					activeFielderAction[i] = "diveEnd";
				}
			}
			else if (activeFielderAction[i] == "waitToPick")
			{
				if (ballAngle > 90f && ballAngle < 270f)
				{
					if (Singleton<RightFovLerp>.instance != null)
					{
						Singleton<RightFovLerp>.instance.setCameraPosition(gameObject.transform.position, gameObject);
					}
				}
				else if (Singleton<LeftFovLerp>.instance != null)
				{
					Singleton<LeftFovLerp>.instance.setCameraPosition(gameObject.transform.position, gameObject);
				}
				if (CONTROLLER.cameraType == 0)
				{
					if (ballAngle > 90f && ballAngle < 270f)
					{
						if (Singleton<RightFovLerp>.instance != null)
						{
							Singleton<RightFovLerp>.instance.setCameraPosition(gameObject.transform.position, gameObject);
						}
					}
					else if (Singleton<LeftFovLerp>.instance != null)
					{
						Singleton<LeftFovLerp>.instance.setCameraPosition(gameObject.transform.position, gameObject);
					}
				}
				if (stopTheFielders)
				{
					activeFielderAction[i] = "stopChasing";
				}
				float num20 = 7f;
				float num21 = num20 * animationFPSDivide * horizontalSpeed;
				if (DistanceBetweenTwoVector2(gameObject, ball) - 1f < num21)
				{
					float num22 = Mathf.Abs(nextPitchDistance - DistanceBetweenTwoVector2(ballTimingOrigin, gameObject));
					if (ballTransform.position.y < 0.7f || num22 < 2f)
					{
						pickingupAnimationToPlay = "lowCatch";
					}
					else if (ballTransform.position.y < 1.33f)
					{
						pickingupAnimationToPlay = "hipCatch";
					}
					else if (ballTransform.position.y < 2f)
					{
						pickingupAnimationToPlay = "sideCatch";
					}
					else
					{
						pickingupAnimationToPlay = "highCatch";
					}
					animation.Play(pickingupAnimationToPlay);
					animation[pickingupAnimationToPlay].speed = 1f;
					fielderTransform[activeFielderNumber[i]].LookAt(ballTimingOriginTransform);
					activeFielderAction[i] = "pickupAttempt";
					stopTheFielders = true;
					fielderSpeed = 0f;
				}
			}
			else if (activeFielderAction[i] == "pickupAttempt")
			{
				if (ballAngle > 90f && ballAngle < 270f)
				{
					if (Singleton<RightFovLerp>.instance != null)
					{
						Singleton<RightFovLerp>.instance.setCameraPosition(gameObject.transform.position, gameObject);
					}
				}
				else if (Singleton<LeftFovLerp>.instance != null)
				{
					Singleton<LeftFovLerp>.instance.setCameraPosition(gameObject.transform.position, gameObject);
				}
				if (CONTROLLER.cameraType == 0)
				{
					if (ballAngle > 90f && ballAngle < 270f)
					{
						if (Singleton<RightFovLerp>.instance != null)
						{
							Singleton<RightFovLerp>.instance.setCameraPosition(gameObject.transform.position, gameObject);
						}
					}
					else if (Singleton<LeftFovLerp>.instance != null)
					{
						Singleton<LeftFovLerp>.instance.setCameraPosition(gameObject.transform.position, gameObject);
					}
				}
				ballPickedByFielder = true;
				stopOtherFielders(i, gameObject);
				if (!replayMode || (replayMode && savedPickedUpFielderIndex == i))
				{
					if (ballTransform.position.y < 2.5f)
					{
						if (!replayMode)
						{
							savedPickedUpFielderIndex = i;
						}
						if ((!(DistanceBetweenTwoVector2(groundCenterPoint, gameObject) > 20f) && !takingRun) || !animation.IsPlaying("run") || !(DistanceBetweenTwoVector2(groundCenterPoint, gameObject) > 52f) || !replayMode)
						{
						}
						float time2 = animation[pickingupAnimationToPlay].time;
						float num23 = time2 * animationFPS;
						if (num23 >= 5f && (pickingupAnimationToPlay == "hipCatch" || pickingupAnimationToPlay == "highCatch") && ballTransform.position.y < 0.4f)
						{
							pickingupAnimationToPlay = "lowCatch";
							animation.CrossFade(pickingupAnimationToPlay, 0.3f);
							animation[pickingupAnimationToPlay].speed = 2f;
						}
						else if (num23 >= 5f && (pickingupAnimationToPlay == "lowCatch" || pickingupAnimationToPlay == "highCatch") && ballTransform.position.y > 0.5f && ballTransform.position.y < 1.3f)
						{
							pickingupAnimationToPlay = "hipCatch";
							animation.CrossFade(pickingupAnimationToPlay, 0.3f);
							animation[pickingupAnimationToPlay].speed = 2f;
						}
						else if (num23 >= 5f && (pickingupAnimationToPlay == "lowCatch" || pickingupAnimationToPlay == "hipCatch") && ballTransform.position.y >= 1.3f)
						{
							pickingupAnimationToPlay = "highCatch";
							animation.CrossFade(pickingupAnimationToPlay, 0.3f);
							animation[pickingupAnimationToPlay].speed = 2f;
						}
						if (num23 >= 7f || DistanceBetweenTwoVector2(ballTimingOrigin, gameObject) - 1f < DistanceBetweenTwoVector2(ballTimingOrigin, ball))
						{
							gameObject4.GetComponent<Renderer>().enabled = true;
							ShowBall(status: false);
							pauseTheBall = true;
							activeFielderAction[i] = "pickedup";
							if (batsmanTransform.position.z < 8f)
							{
								animation[pickingupAnimationToPlay].speed = 2f;
							}
						}
						fielderThrowElapsedTime = 0f;
					}
				}
				else if (DistanceBetweenTwoVector2(gameObject, ball) > 10f)
				{
					IEnumerator enumerator = animation.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							AnimationState animationState = (AnimationState)enumerator.Current;
							animationState.speed = 1f;
						}
					}
					finally
					{
						IDisposable disposable;
						if ((disposable = enumerator as IDisposable) != null)
						{
							disposable.Dispose();
						}
					}
					activeFielderAction[i] = "goForChase";
					animation.Play("run");
				}
			}
			else if (activeFielderAction[i] == "pickedup")
			{
				if (pickingupAnimationToPlay != string.Empty)
				{
					float time3 = animation[pickingupAnimationToPlay].time;
					float num24 = time3 * animationFPS;
					if (num24 > 23f || num24 == 0f)
					{
						animation.Play("throw");
						animation["throw"].speed = 7f / (7f * (1f - controlFactor * (float)num));
						if (Singleton<RightFovLerp>.instance != null)
						{
							Singleton<RightFovLerp>.instance.CallDisableBoundaryCam();
						}
						if (Singleton<LeftFovLerp>.instance != null)
						{
							Singleton<LeftFovLerp>.instance.CallDisableBoundaryCam();
						}
						pickingupAnimationToPlay = string.Empty;
					}
					break;
				}
				ballPickedByFielder = true;
				float num25 = ((CONTROLLER.BowlingTeamIndex != CONTROLLER.opponentTeamIndex) ? (7f * (1f - controlFactor * (float)num)) : 7f);
				float num26 = num25 * animationFPSDivide;
				if (animation.IsPlaying("throw"))
				{
					if (!replayMode && savedThrowTo == string.Empty)
					{
						if (DistanceBetweenTwoVector2(gameObject, fielder10) < DistanceBetweenTwoVector2(gameObject, wicketKeeper))
						{
							if (fielder10Action == "waitForBall")
							{
								savedThrowTo = "Fielder10";
								throwToGO = fielder10;
							}
							else
							{
								savedThrowTo = "WicketKeeper";
								throwToGO = wicketKeeper;
							}
						}
						else
						{
							savedThrowTo = "WicketKeeper";
							throwToGO = wicketKeeper;
						}
					}
					isFielderthrown = true;
					if (savedThrowTo == "Fielder10")
					{
						fielderTransform[activeFielderNumber[i]].LookAt(fielder10Transform);
					}
					fielderThrowElapsedTime += Time.deltaTime;
					if (fielderThrowElapsedTime >= num26)
					{
						float num27 = 2f;
						string text = gameObject.name;
						int num28 = int.Parse(text.Substring(text.Length - 1));
						FielderBallReleasePointGOTransform[num28].position = new Vector3(FielderBallReleasePointGOTransform[num28].position.x, num27, FielderBallReleasePointGOTransform[num28].position.z);
						gameObject4.GetComponent<Renderer>().enabled = false;
						ShowBall(status: true);
						ballTransform.position = FielderBallReleasePointGOTransform[num28].position;
						GameObject gameObject8 = null;
						float num29 = 0f;
						if (savedThrowTo == "Fielder10")
						{
							if (postBattingStumpFielderDirection == "straight")
							{
								gameObject8 = fielderStraightBallStumping;
							}
							else if (postBattingStumpFielderDirection == "straightDown")
							{
								gameObject8 = stump2Crease;
							}
							else if (postBattingStumpFielderDirection == "offSide")
							{
								gameObject8 = fielderOffSideBallStumping;
							}
							else if (postBattingStumpFielderDirection == "legSide")
							{
								gameObject8 = fielderLegSideBallStumping;
							}
							fielder10Action = "waitToCollect";
							fielder10Transform.LookAt(fielderTransform[activeFielderNumber[i]]);
							WicketKeeperAnimationComponent.CrossFade("idle");
							num29 = DistanceBetweenTwoGameObjects(gameObject, gameObject8) + 1f;
							if (num29 > 40f && UnityEngine.Random.Range(1, 10) <= 5)
							{
								throwingFirstBounceDistance = UnityEngine.Random.Range(5f, 8f);
								num29 -= throwingFirstBounceDistance;
							}
							if (!replayMode)
							{
								savedThrowingFirstBounceDistance = throwingFirstBounceDistance;
								savedThrowLength = num29;
								savedSummary = "picked";
							}
							else if (replayMode)
							{
								throwingFirstBounceDistance = savedThrowingFirstBounceDistance;
								num29 = savedThrowLength;
							}
						}
						else if (WicketKeeperReachedToStump)
						{
							if (postBattingWicketKeeperDirection == "straight")
							{
								gameObject8 = wicketKeeperStraightBallStumping;
							}
							else if (postBattingWicketKeeperDirection == "offSide")
							{
								gameObject8 = wicketKeeperOffSideBallStumping;
							}
							else if (postBattingWicketKeeperDirection == "legSide")
							{
								gameObject8 = wicketKeeperLegSideBallStumping;
							}
							wicketKeeperStatus = "waitToCollect";
							wicketKeeperTransform.LookAt(fielderTransform[activeFielderNumber[i]]);
							Fielder10AnimationComponent.CrossFade("idle");
							num29 = DistanceBetweenTwoGameObjects(gameObject, gameObject8);
							if (num29 > 40f && UnityEngine.Random.Range(1, 10) <= 5)
							{
								throwingFirstBounceDistance = UnityEngine.Random.Range(5f, num29 / 2f);
								num29 -= throwingFirstBounceDistance;
							}
							if (!replayMode)
							{
								savedThrowingFirstBounceDistance = throwingFirstBounceDistance;
								savedThrowLength = num29;
								savedSummary = "picked";
							}
							else if (replayMode)
							{
								throwingFirstBounceDistance = savedThrowingFirstBounceDistance;
								num29 = savedThrowLength;
							}
						}
						if (gameObject8 != null)
						{
							ballAngle = AngleBetweenTwoGameObjects(FielderBallReleasePointGOBJ[num28], gameObject8);
							horizontalSpeed = 24f;
							ballProjectileHeight = num29 / 6f;
							float num30 = Mathf.Asin(num27 / ballProjectileHeight) * RAD2DEG;
							if (num27 >= ballProjectileHeight)
							{
								num30 = 90f;
								ballProjectileHeight = num27;
							}
							ballProjectileAngle = 180f + num30;
							ballProjectileAnglePerSecond = (180f - num30) / num29 * horizontalSpeed;
							pauseTheBall = false;
							ballStatus = "throw";
							activeFielderAction[i] = "throw";
						}
					}
				}
			}
			else if (!(activeFielderAction[i] == "throw"))
			{
				if (activeFielderAction[i] == "end")
				{
					if (CONTROLLER.PlayModeSelected == 6 && (shotPlayed == "bt6Defense" || shotPlayed == "frontFootOffSideDefense" || shotPlayed == "backFootDefenseHighBall"))
					{
						break;
					}
					if (stayStartTime + 1f + timeBetweenBalls < Time.time)
					{
						activeFielderAction[i] = string.Empty;
						if (Singleton<GameModel>.instance != null)
						{
							if (noBall)
							{
								CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
								Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, currentBallNoOfRuns, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
								CONTROLLER.isJokerCall = false;
								if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
								{
								}
							}
							else if (freeHit)
							{
								Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, currentBallNoOfRuns, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
								freeHit = false;
								isFreeHit = false;
							}
							else if (!freeHit && !noBall)
							{
								Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, currentBallNoOfRuns, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
							}
						}
					}
				}
				else if (activeFielderAction[i] == "goToBoundary")
				{
					MoveToCollectBallAfterBoundary(activeFielderNumber[i], stop: false, first: true);
					activeFielderAction[i] = "goingToBoundary";
				}
				else if (activeFielderAction[i] == "goingToBoundary")
				{
					if (DistanceBetweenTwoVector2(fielder[num3], ball) < 1.25f)
					{
						if (!pauseTheBall)
						{
							if (goToBoundaryAnim.Equals(string.Empty))
							{
								float y3 = ballTransform.position.y;
								if (y3 >= 1.75f)
								{
									goToBoundaryAnim = "highCatch";
								}
								else if (y3 >= 1.25f)
								{
									goToBoundaryAnim = "chestCatch";
								}
								else if (y3 >= 1f)
								{
									goToBoundaryAnim = "hipCatch";
								}
								else
								{
									goToBoundaryAnim = "runAndFieldForward";
								}
							}
							float time4 = animation[goToBoundaryAnim].time;
							float num31 = time4 * animationFPS;
							if (!(num31 >= 26f))
							{
								if (num31 >= 8f)
								{
									animation.Play(goToBoundaryAnim);
									fielderBall[activeFielderNumber[i]].GetComponent<Renderer>().enabled = true;
									ShowBall(status: false);
									pauseTheBall = true;
								}
								else if (!animation.IsPlaying(goToBoundaryAnim))
								{
									animation.Play(goToBoundaryAnim);
									horizontalSpeed = Mathf.Min(horizontalSpeed, 0.5f);
								}
							}
						}
					}
					else
					{
						MoveToCollectBallAfterBoundary(activeFielderNumber[i]);
					}
				}
			}
			if (ballOnboundaryLine && (ballBoundaryReflection || horizontalSpeed < speedThresholdDuringBoundary) && nearestFielderIndex == -1 && !ballOverTheFence)
			{
				nearestFielderIndex = GetNearestFielderIndex(ballTransform.position, 20f, requireIdle: true);
				if (nearestFielderIndex != -1 && !IsFielderPlayingAnAnimation(activeFielderNumber[nearestFielderIndex]))
				{
					activeFielderAction[nearestFielderIndex] = "goToBoundary";
				}
			}
		}
	}

	public void MoveBatsman()
	{
		if (currentBatsmanHand == "right")
		{
			batsman.transform.position = new Vector3(batsman.transform.position.x + 5f, batsman.transform.position.y, batsman.transform.position.z);
		}
		else
		{
			batsman.transform.position = new Vector3(batsman.transform.position.x - 5f, batsman.transform.position.y, batsman.transform.position.z);
		}
	}

	public void AIBowlToStump()
	{
		bowlingSpotTransform.position = new Vector3(0f, 0.06f, 9f);
	}

	public float strikerZPos()
	{
		return batsman.transform.position.z;
	}

	[Skip]
	private void stopITween(GameObject FielderGO)
	{
		iTween.Stop(FielderGO);
	}

	private void setChasePoint(int val)
	{
		GameObject gameObject = fielder[val];
		GameObject gameObject2 = fielderChasePoint[val];
		float num = DistanceBetweenTwoGameObjects(gameObject, gameObject2);
		float num2 = num / fielderSpeed;
		float num3 = DistanceBetweenTwoGameObjects(ball, gameObject) / horizontalSpeed;
		if (fielderSetChasePoint[val])
		{
			float num4 = fielderBallDiffInAngle[val];
			float num5 = fielderDistance[val];
			float f = Mathf.Sin(num4 * DEG2RAD) * num5;
			float num6 = Mathf.Sqrt(Mathf.Pow(fielderDistance[val], 2f) - Mathf.Pow(f, 2f));
			float x = ballTimingOriginTransform.position.x + num6 * Mathf.Cos(ballAngle * DEG2RAD);
			float z = ballTimingOriginTransform.position.z + num6 * Mathf.Sin(ballAngle * DEG2RAD);
			gameObject2.transform.position = new Vector3(x, gameObject2.transform.position.y, z);
			fielderSetChasePoint[val] = false;
		}
		if (num2 <= num3 && DistanceBetweenTwoGameObjects(gameObject2, ball) > 1f)
		{
			float x = Mathf.Cos(ballAngle * DEG2RAD) * (1.05f * (num3 / num2) * Time.deltaTime);
			float z = Mathf.Sin(ballAngle * DEG2RAD) * (1.05f * (num3 / num2) * Time.deltaTime);
			gameObject2.transform.position -= new Vector3(x, 0f, z);
		}
		else if (num2 > num3 && DistanceBetweenTwoGameObjects(gameObject2, ball) > 1f)
		{
			float x = Mathf.Cos(ballAngle * DEG2RAD) * (3.85f * (num3 / num2) * Time.deltaTime);
			float z = Mathf.Sin(ballAngle * DEG2RAD) * (3.85f * (num3 / num2) * Time.deltaTime);
			gameObject2.transform.position += new Vector3(x, 0f, z);
		}
		else
		{
			gameObject2.transform.position = new Vector3(ballTransform.position.x, gameObject2.transform.position.y, ballTransform.position.z);
		}
	}

	private void stopOtherFielders(int val, GameObject thisFielder)
	{
		for (int i = 0; i < activeFielderNumber.Count; i++)
		{
			GameObject gameObject = fielder[activeFielderNumber[i]];
			if (gameObject != thisFielder && activeFielderNumber.Count > 1 && activeFielderAction[i] == "goForChase")
			{
				activeFielderAction[i] = "stopChasing";
			}
		}
	}

	private float DistanceBetweenTwoVector2(GameObject go1, GameObject go2)
	{
		float num = go1.transform.position.x - go2.transform.position.x;
		float num2 = go1.transform.position.z - go2.transform.position.z;
		return Mathf.Sqrt(num * num + num2 * num2);
	}

	private float DistanceBetweenTwoGameObjects(GameObject go1, GameObject go2)
	{
		return Vector3.Distance(go1.transform.position, go2.transform.position);
	}

	private float AngleBetweenTwoGameObjects(GameObject go1, GameObject go2)
	{
		float y = go1.transform.position.x - go2.transform.position.x;
		float x = go1.transform.position.z - go2.transform.position.z;
		float num = Mathf.Atan2(y, x) * RAD2DEG;
		return (270f - num + 360f) % 360f;
	}

	public float AngleBetweenTwoVector3(Vector3 v1, Vector3 v2)
	{
		float y = v1.x - v2.x;
		float x = v1.z - v2.z;
		float num = Mathf.Atan2(y, x) * RAD2DEG;
		return (270f - num + 360f) % 360f;
	}

	private void FindNewBowlingSpot()
	{
		ShowBowlingSpot();
		if (bowlingBy == "computer")
		{
			if (CONTROLLER.PlayModeSelected == 6)
			{
				if (currentBatsmanHand == "right")
				{
					bowlingSpotGO.transform.position = Multiplayer.oversData[CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls / 6].bowlingSpotR[CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls % 6];
				}
				else if (currentBatsmanHand == "left")
				{
					bowlingSpotGO.transform.position = Multiplayer.oversData[CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls / 6].bowlingSpotL[CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls % 6];
				}
				bowlingSpotGO.transform.position = new Vector3(bowlingSpotGO.transform.position.x, bowlingSpotGO.transform.position.y + 0.06f, bowlingSpotGO.transform.position.z);
				return;
			}
			float num = 0f;
			float num2 = 0f;
			if (FullTossChance == 0 || FullTossChance == 1)
			{
				FullTossChance = UnityEngine.Random.Range(3, 10);
			}
			else
			{
				FullTossChance = UnityEngine.Random.Range(0, 20);
			}
			if (currentBatsmanHand == "right")
			{
				if (CONTROLLER.PlayModeSelected == 6)
				{
					bowlingSpotGO.transform.position = Multiplayer.oversData[CONTROLLER.currentMatchBalls / 6].bowlingSpotR[CONTROLLER.currentMatchBalls % 6];
					bowlingSpotGO.transform.position = new Vector3(bowlingSpotGO.transform.position.x, bowlingSpotGO.transform.position.y + 0.06f, bowlingSpotGO.transform.position.z);
				}
				else if (FullTossChance <= 1)
				{
					num = UnityEngine.Random.Range(RHBatsmanMaxBowlLimitGO.transform.position.x, RHBatsmanMinBowlLimitGO.transform.position.x);
					num2 = UnityEngine.Random.Range(RHBatsmanMaxBowlLimitGO.transform.position.z, RHBatsmanMinBowlLimitGO.transform.position.z);
					bowlingSpotTransform.position = new Vector3(num, bowlingSpotTransform.position.y, UnityEngine.Random.Range(RHBatsmanMaxBowlLimitGO.transform.position.z, num2));
				}
				else
				{
					num = UnityEngine.Random.Range(RHBatsmanMaxBowlLimitGO.transform.position.x, RHBatsmanMinBowlLimitGO.transform.position.x);
					num2 = UnityEngine.Random.Range(RHBatsmanMaxBowlLimitGO.transform.position.z - 6f, RHBatsmanMinBowlLimitGO.transform.position.z);
					bowlingSpotTransform.position = new Vector3(num, bowlingSpotTransform.position.y, UnityEngine.Random.Range(RHBatsmanMaxBowlLimitGO.transform.position.z - 6f, num2));
				}
				float num3 = RHBatsmanMaxBowlLimitGO.transform.position.z - 6f;
				if (bowlerSide == "right" && bowlingSpotTransform.position.z < 6f && bowlingSpotTransform.position.x < -0.5f)
				{
					bowlingSpotTransform.position += new Vector3(0.5f, 0f, 0f);
				}
				if (bowlingSpotTransform.position.z > 9.8f)
				{
					FullTossFunction(AI: false);
				}
			}
			else if (currentBatsmanHand == "left")
			{
				if (FullTossChance == 0 || FullTossChance == 1)
				{
					FullTossChance = UnityEngine.Random.Range(3, 10);
				}
				else
				{
					FullTossChance = UnityEngine.Random.Range(0, 20);
				}
				if (CONTROLLER.PlayModeSelected == 6)
				{
					bowlingSpotGO.transform.position = Multiplayer.oversData[CONTROLLER.currentMatchBalls / 6].bowlingSpotL[CONTROLLER.currentMatchBalls % 6];
					bowlingSpotGO.transform.position = new Vector3(bowlingSpotGO.transform.position.x, bowlingSpotGO.transform.position.y + 0.06f, bowlingSpotGO.transform.position.z);
				}
				else if (FullTossChance <= 1)
				{
					num = UnityEngine.Random.Range(LHBatsmanMinBowlLimitGO.transform.position.x, LHBatsmanMaxBowlLimitGO.transform.position.x);
					num2 = UnityEngine.Random.Range(LHBatsmanMaxBowlLimitGO.transform.position.z, LHBatsmanMinBowlLimitGO.transform.position.z);
					bowlingSpotTransform.position = new Vector3(num, bowlingSpotTransform.position.y, UnityEngine.Random.Range(LHBatsmanMaxBowlLimitGO.transform.position.z - 6f, num2));
				}
				else
				{
					num = UnityEngine.Random.Range(LHBatsmanMinBowlLimitGO.transform.position.x, LHBatsmanMaxBowlLimitGO.transform.position.x);
					num2 = UnityEngine.Random.Range(LHBatsmanMaxBowlLimitGO.transform.position.z - 6f, LHBatsmanMinBowlLimitGO.transform.position.z);
					bowlingSpotTransform.position = new Vector3(num, bowlingSpotTransform.position.y, UnityEngine.Random.Range(LHBatsmanMaxBowlLimitGO.transform.position.z - 6f, num2));
				}
				if (bowlerSide == "left" && bowlingSpotTransform.position.z < 6f && bowlingSpotTransform.position.x > 0.5f)
				{
					bowlingSpotTransform.position -= new Vector3(0.5f, 0f, 0f);
				}
				if (bowlingSpotTransform.position.z > 9.8f)
				{
					FullTossFunction(AI: false);
				}
			}
			AvoidWideBall();
		}
		else if (bowlingBy == "user")
		{
			bowlingSpotTransform.position = new Vector3(UnityEngine.Random.Range(userBowlingMinLimit.transform.position.x, userBowlingMaxLimit.transform.position.x), bowlingSpotTransform.position.y, UnityEngine.Random.Range(userBowlingMaxLimit.transform.position.z, userBowlingMinLimit.transform.position.z - 2.75f));
			if (CONTROLLER.tutorialToggle == 1)
			{
				Singleton<GameModel>.instance.ShowTutorial(2);
			}
		}
	}

	private void AvoidWideBall()
	{
		int num = 0;
		int num2 = 0;
		int num3 = 5;
		int num4 = 2;
		float x = 0f;
		float num5 = 9f;
		int num6 = UnityEngine.Random.Range(0, 10);
		Vector3 vector = new Vector3(x, bowlingSpotTransform.position.y, bowlingSpotTransform.position.z);
		if (ObscuredPrefs.HasKey("bowlerBowlStraight" + CONTROLLER.PlayModeSelected))
		{
			num = ObscuredPrefs.GetInt("bowlerBowlStraight" + CONTROLLER.PlayModeSelected);
		}
		if (ObscuredPrefs.HasKey("stumpBallCount" + CONTROLLER.PlayModeSelected))
		{
			num2 = ObscuredPrefs.GetInt("stumpBallCount" + CONTROLLER.PlayModeSelected);
		}
		if (num < 5 && num6 <= num3)
		{
			if (num6 < num4 && num2 < 1)
			{
				AIBowlToStump();
				num2++;
				ObscuredPrefs.SetInt("stumpBallCount" + CONTROLLER.PlayModeSelected, num2);
			}
			else
			{
				x = ((bowlerSide == "left") ? ((!(currentBatsmanHand == "left")) ? UnityEngine.Random.Range(-0.6f, -0.2f) : UnityEngine.Random.Range(-0.15f, 0f)) : ((!(currentBatsmanHand == "left")) ? UnityEngine.Random.Range(0f, 0.15f) : UnityEngine.Random.Range(0.2f, 0.6f)));
				bowlingSpotTransform.position = new Vector3(x, bowlingSpotTransform.position.y, bowlingSpotTransform.position.z);
			}
			num++;
			ObscuredPrefs.SetInt("bowlerBowlStraight" + CONTROLLER.PlayModeSelected, num);
		}
	}

	public void BowlToStump()
	{
		bowlingSpotTransform.position = new Vector3(9.5f, bowlingSpotTransform.position.y, 0f);
	}

	public Vector3 BallTempPos(Vector3 BallTransformTemp)
	{
		if (bowlerSide == "left")
		{
			if (currentBowlerHand == "right")
			{
				BallTransformTemp = new Vector3(-0.7f, BallTransformTemp.y, BallTransformTemp.z);
			}
			else if (currentBowlerHand == "left")
			{
				BallTransformTemp = new Vector3(-0.8f, BallTransformTemp.y, BallTransformTemp.z);
			}
		}
		else if (bowlerSide == "right")
		{
			if (currentBowlerHand == "right")
			{
				BallTransformTemp = new Vector3(0.9f, BallTransformTemp.y, BallTransformTemp.z);
			}
			else if (currentBowlerHand == "left")
			{
				BallTransformTemp = new Vector3(0.7f, BallTransformTemp.y, BallTransformTemp.z);
			}
		}
		return BallTransformTemp;
	}

	private void FullTossFunction(bool AI)
	{
		IsFullTossBall = true;
		if (!AI)
		{
			FullBallLength = 0f;
			FullTossDisplacementFromCrease = 0f;
			FulltossCalculationVal1 = 0f;
			FulltossCalculationVal2 = 0f;
			Vector3 vector = new Vector3(0f, 0f, 8.9f);
			TempBallStartPoint.transform.position = ballTransform.position;
			TempBallStartPoint.transform.position = BallTempPos(TempBallStartPoint.transform.position);
			FullBallLength = bowlingSpotTransform.position.z + 8.8f;
			temp = userBowlingFullTossTrigger.transform.position.z + 8.8f;
			AngleBtwForFindingStraightLine = AngleBetweenTwoVector3(TempBallStartPoint.transform.position, bowlingSpotTransform.position);
			Hypotenuse = (userBowlingFullTossTrigger.transform.position.z + 8.8f) / Mathf.Sin(AngleBtwForFindingStraightLine * DEG2RAD);
			vector.x = Hypotenuse * Mathf.Cos(AngleBtwForFindingStraightLine * DEG2RAD);
			vector.x -= TempBallStartPoint.transform.position.x * -1f;
			Vector3 vector2 = ballOriginGO.transform.InverseTransformPoint(bowlingSpotTransform.position);
			float num = 90f - Mathf.Atan2(vector2.x, vector2.z) * RAD2DEG;
			bowlingSpotFullTossGO.transform.position = new Vector3(bowlingSpotTransform.position.x, bowlingSpotFullTossGO.transform.position.y, bowlingSpotFullTossGO.transform.position.z);
			xDiff = bowlingSpotFullTossGO.transform.position.x - bowlingSpotTransform.position.x;
			zDiff = bowlingSpotTransform.position.z - bowlingSpotFullTossGO.transform.position.z;
			creaseLineAdjacentLength = Mathf.Sqrt(xDiff * xDiff + zDiff * zDiff);
			creaseLineThetaBtwAdjAndHyp = Mathf.Atan2(xDiff, zDiff) * RAD2DEG - (90f - num - spinValue);
			creaseLineHypotenuse = creaseLineAdjacentLength / Mathf.Cos(creaseLineThetaBtwAdjAndHyp * DEG2RAD);
			creaseLineOppositeLength = Mathf.Sqrt(creaseLineHypotenuse * creaseLineHypotenuse - creaseLineAdjacentLength * creaseLineAdjacentLength);
			FullTossTriggerLength = userBowlingFullTossTrigger.transform.position.z + 8.8f;
			FullTossDisplacementFromCrease = FullBallLength - FullTossTriggerLength;
			SwingOffsetDisplacement = DistanceBetweenTwoVector2(vector, bowlingSpotTransform.position);
			FulltossCalculationVal1 = 90f / FullBallLength * FullTossDisplacementFromCrease;
			FulltossCalculationVal2 = 360f - FulltossCalculationVal1;
			FullTossY = Mathf.Sin(FulltossCalculationVal2 * DEG2RAD) * ballProjectileHeight - ballRadius;
			FullTossX /= 4f;
			if (FullTossY < 0f)
			{
				FullTossY *= -1f;
			}
			if (creaseLineThetaBtwAdjAndHyp > 0f && swingValue != 0f)
			{
				bowlingSpotFullTossGO.transform.position = new Vector3(bowlingSpotFullTossGO.transform.position.x + creaseLineOppositeLength, FullTossY, userBowlingFullTossTrigger.transform.position.z);
			}
			else if (creaseLineThetaBtwAdjAndHyp < 0f && swingValue != 0f)
			{
				bowlingSpotFullTossGO.transform.position = new Vector3(0f - creaseLineOppositeLength + bowlingSpotFullTossGO.transform.position.x, FullTossY, userBowlingFullTossTrigger.transform.position.z);
			}
			else
			{
				bowlingSpotFullTossGO.transform.position = new Vector3(vector.x, FullTossY, userBowlingFullTossTrigger.transform.position.z);
			}
			ballSpotAtCreaseLine.transform.position = new Vector3(bowlingSpotFullTossGO.transform.position.x, 0f, bowlingSpotFullTossGO.transform.position.z);
			ballSpotAtStump.transform.position = new Vector3(bowlingSpotFullTossGO.transform.position.x, ballSpotAtStump.transform.position.y, ballSpotAtStump.transform.position.z);
			float num2 = ballSpotAtStump.transform.position.x - bowlingSpotFullTossGO.transform.position.x;
			float num3 = ballSpotAtStump.transform.position.z - bowlingSpotFullTossGO.transform.position.z;
			float num4 = Mathf.Sqrt(num2 * num2 + num3 * num3);
			float num5 = Mathf.Atan2(num2, num3) * RAD2DEG - (90f - num - spinValue);
			float num6 = num4 / Mathf.Cos(num5 * DEG2RAD);
			float x = Mathf.Sqrt(num6 * num6 - num4 * num4);
			if (num5 < 0f)
			{
				ballSpotAtStump.transform.position += new Vector3(x, 0f, 0f);
			}
			else
			{
				ballSpotAtStump.transform.position -= new Vector3(x, 0f, 0f);
			}
			if ((double)bowlingSpotTransform.position.z > 9.8)
			{
				bowlingSpotFullTossGO.SetActive(value: true);
				bowlingSpotRenderer.enabled = false;
				if (tutorialArrow != null)
				{
					tutorialArrow.SetActive(value: false);
				}
			}
		}
		else
		{
			FullBallLength = 0f;
			FullTossDisplacementFromCrease = 0f;
			FulltossCalculationVal1 = 0f;
			FulltossCalculationVal2 = 0f;
			Vector3 vector3 = new Vector3(0f, 0f, 8.9f);
			TempBallStartPoint.transform.position = ballTransform.position;
			TempBallStartPoint.transform.position = BallTempPos(TempBallStartPoint.transform.position);
			FullBallLength = bowlingSpotTransform.position.z + 8.8f;
			temp = userBowlingFullTossTrigger.transform.position.z + 8.8f;
			AngleBtwForFindingStraightLine = AngleBetweenTwoVector3(TempBallStartPoint.transform.position, bowlingSpotTransform.position);
			Hypotenuse = (userBowlingFullTossTrigger.transform.position.z + 8.8f) / Mathf.Sin(AngleBtwForFindingStraightLine * DEG2RAD);
			vector3.x = Hypotenuse * Mathf.Cos(AngleBtwForFindingStraightLine * DEG2RAD);
			vector3.x -= TempBallStartPoint.transform.position.x * -1f;
			Vector3 vector4 = ballOriginGO.transform.InverseTransformPoint(bowlingSpotTransform.position);
			float num7 = 90f - Mathf.Atan2(vector4.x, vector4.z) * RAD2DEG;
			bowlingSpotFullTossGO.transform.position = new Vector3(bowlingSpotTransform.position.x, bowlingSpotFullTossGO.transform.position.y, bowlingSpotFullTossGO.transform.position.z);
			xDiff = bowlingSpotFullTossGO.transform.position.x - bowlingSpotTransform.position.x;
			zDiff = bowlingSpotTransform.position.z - bowlingSpotFullTossGO.transform.position.z;
			creaseLineAdjacentLength = Mathf.Sqrt(xDiff * xDiff + zDiff * zDiff);
			creaseLineThetaBtwAdjAndHyp = Mathf.Atan2(xDiff, zDiff) * RAD2DEG - (90f - num7 - spinValue);
			creaseLineHypotenuse = creaseLineAdjacentLength / Mathf.Cos(creaseLineThetaBtwAdjAndHyp * DEG2RAD);
			creaseLineOppositeLength = Mathf.Sqrt(creaseLineHypotenuse * creaseLineHypotenuse - creaseLineAdjacentLength * creaseLineAdjacentLength);
			FullTossTriggerLength = userBowlingFullTossTrigger.transform.position.z + 8.8f;
			FullTossDisplacementFromCrease = FullBallLength - FullTossTriggerLength;
			SwingOffsetDisplacement = DistanceBetweenTwoVector2(vector3, bowlingSpotTransform.position);
			FulltossCalculationVal1 = 90f / FullBallLength * FullTossDisplacementFromCrease;
			FulltossCalculationVal2 = 360f - FulltossCalculationVal1;
			FullTossY = Mathf.Sin(FulltossCalculationVal2 * DEG2RAD) * ballProjectileHeight - ballRadius;
			FullTossX /= 4f;
			if (FullTossY < 0f)
			{
				FullTossY *= -1f;
			}
			if (creaseLineThetaBtwAdjAndHyp > 0f && swingValue != 0f)
			{
				bowlingSpotFullTossGO.transform.position = new Vector3(bowlingSpotFullTossGO.transform.position.x + creaseLineOppositeLength, FullTossY, userBowlingFullTossTrigger.transform.position.z);
			}
			else if (creaseLineThetaBtwAdjAndHyp < 0f && swingValue != 0f)
			{
				bowlingSpotFullTossGO.transform.position = new Vector3(0f - creaseLineOppositeLength + bowlingSpotFullTossGO.transform.position.x, FullTossY, userBowlingFullTossTrigger.transform.position.z);
			}
			else
			{
				bowlingSpotFullTossGO.transform.position = new Vector3(vector3.x, FullTossY, userBowlingFullTossTrigger.transform.position.z);
			}
			bowlingSpotFullTossGO.SetActive(value: true);
			bowlingSpotRenderer.enabled = false;
			if (tutorialArrow != null)
			{
				tutorialArrow.SetActive(value: false);
			}
		}
	}

	private void UserChangingBowlingSpot()
	{
		if (!(bowlingBy == "user") || !userBowlerCanMoveBowlingSpot || !(Time.timeScale >= 1f))
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			upArrowKeyDown = true;
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			downArrowKeyDown = true;
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			leftArrowKeyDown = true;
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			rightArrowKeyDown = true;
		}
		if (Input.GetKeyUp(KeyCode.UpArrow))
		{
			upArrowKeyDown = false;
		}
		if (Input.GetKeyUp(KeyCode.DownArrow))
		{
			downArrowKeyDown = false;
		}
		if (Input.GetKeyUp(KeyCode.LeftArrow))
		{
			leftArrowKeyDown = false;
		}
		if (Input.GetKeyUp(KeyCode.RightArrow))
		{
			rightArrowKeyDown = false;
		}
		if (Input.GetKeyDown(KeyCode.S) && !userBowlingSpotSelected)
		{
			userBowlingSpotSelected = true;
			userBowlerCanMoveBowlingSpot = false;
			FreezeBowlingSpot();
			Singleton<GameModel>.instance.ShowTutorial(-1);
		}
		else if (!userBowlingSpotSelected && canShowFCLPowers)
		{
			float num = 2f;
			float num2 = 5f;
			if ((CONTROLLER.TargetPlatform != "ios" && CONTROLLER.TargetPlatform != "android") || Application.isEditor)
			{
				if (Input.GetMouseButton(0))
				{
					if (prevMousePos.x == 0f && prevMousePos.y == 0f)
					{
						prevMousePos = Input.mousePosition;
					}
					float num3 = 20f;
					Vector2 vector = new Vector2(Input.mousePosition.x - prevMousePos.x, Input.mousePosition.y - prevMousePos.y);
					prevMousePos = Input.mousePosition;
					bowlingSpotTransform.position += new Vector3(vector.x * 0.02f, 0f, vector.y * 0.02f);
					if (bowlingSpotTransform.position.z > userBowlingFullTossTrigger.transform.position.z)
					{
						FullTossFunction(AI: false);
					}
					if (bowlingSpotTransform.position.z < userBowlingFullTossTrigger.transform.position.z + 1f && bowlingSpotTransform.position.z < 11f)
					{
						IsFullTossBall = false;
						bowlingSpotRenderer.enabled = true;
						if (tutorialArrow != null)
						{
							tutorialArrow.SetActive(value: true);
						}
						bowlingSpotFullTossGO.transform.position = bowlingSpotTransform.position;
						bowlingSpotFullTossGO.SetActive(value: false);
					}
					if (bowlingSpotTransform.position.x < userBowlingMinLimit.transform.position.x)
					{
						bowlingSpotTransform.position = new Vector3(userBowlingMinLimit.transform.position.x, bowlingSpotTransform.position.y, bowlingSpotTransform.position.z);
					}
					if (bowlingSpotTransform.position.x > userBowlingMaxLimit.transform.position.x)
					{
						bowlingSpotTransform.position = new Vector3(userBowlingMaxLimit.transform.position.x, bowlingSpotTransform.position.y, bowlingSpotTransform.position.z);
					}
					if (bowlingSpotTransform.position.z > userBowlingMinLimit.transform.position.z)
					{
						bowlingSpotTransform.position = new Vector3(bowlingSpotTransform.position.x, bowlingSpotTransform.position.y, userBowlingMinLimit.transform.position.z);
					}
					if (bowlingSpotTransform.position.z < userBowlingMaxLimit.transform.position.z)
					{
						bowlingSpotTransform.position = new Vector3(bowlingSpotTransform.position.x, bowlingSpotTransform.position.y, userBowlingMaxLimit.transform.position.z);
					}
				}
				if (Input.GetMouseButtonUp(0))
				{
					leftArrowKeyDown = false;
					rightArrowKeyDown = false;
					upArrowKeyDown = false;
					downArrowKeyDown = false;
					prevMousePos = Vector2.zero;
				}
			}
			else
			{
				int num4 = 2;
				int num5 = 5;
				if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
				{
					Vector2 deltaPosition = Input.GetTouch(0).deltaPosition;
					bowlingSpotTransform.position += new Vector3(deltaPosition.x * 0.02f, 0f, deltaPosition.y * 0.06f);
					if (bowlingSpotTransform.position.z > userBowlingFullTossTrigger.transform.position.z)
					{
						FullTossFunction(AI: false);
					}
					if (bowlingSpotTransform.position.z < userBowlingFullTossTrigger.transform.position.z + 1f && bowlingSpotTransform.position.z < 11f)
					{
						IsFullTossBall = false;
						bowlingSpotRenderer.enabled = true;
						if (tutorialArrow != null)
						{
							tutorialArrow.SetActive(value: true);
						}
						bowlingSpotFullTossGO.transform.position = bowlingSpotTransform.position;
						bowlingSpotFullTossGO.SetActive(value: false);
					}
					if (bowlingSpotTransform.position.x < userBowlingMinLimit.transform.position.x)
					{
						bowlingSpotTransform.position = new Vector3(userBowlingMinLimit.transform.position.x, bowlingSpotTransform.position.y, bowlingSpotTransform.position.z);
					}
					if (bowlingSpotTransform.position.x > userBowlingMaxLimit.transform.position.x)
					{
						bowlingSpotTransform.position = new Vector3(userBowlingMaxLimit.transform.position.x, bowlingSpotTransform.position.y, bowlingSpotTransform.position.z);
					}
					if (bowlingSpotTransform.position.z > userBowlingMinLimit.transform.position.z)
					{
						bowlingSpotTransform.position = new Vector3(bowlingSpotTransform.position.x, bowlingSpotTransform.position.y, userBowlingMinLimit.transform.position.z);
					}
					if (bowlingSpotTransform.position.z < userBowlingMaxLimit.transform.position.z)
					{
						bowlingSpotTransform.position = new Vector3(bowlingSpotTransform.position.x, bowlingSpotTransform.position.y, userBowlingMaxLimit.transform.position.z);
					}
				}
				if (!Application.isEditor && Input.touchCount == 0)
				{
					leftArrowKeyDown = false;
					rightArrowKeyDown = false;
					upArrowKeyDown = false;
					downArrowKeyDown = false;
				}
			}
			if (leftArrowKeyDown)
			{
				bowlingSpotTransform.position -= new Vector3(num * Time.deltaTime, 0f, 0f);
				if (bowlingSpotTransform.position.x < userBowlingMinLimit.transform.position.x)
				{
					bowlingSpotTransform.position = new Vector3(userBowlingMinLimit.transform.position.x, bowlingSpotTransform.position.y, bowlingSpotTransform.position.z);
				}
			}
			if (rightArrowKeyDown)
			{
				bowlingSpotTransform.position += new Vector3(num * Time.deltaTime, 0f, 0f);
				if (bowlingSpotTransform.position.x > userBowlingMaxLimit.transform.position.x)
				{
					bowlingSpotTransform.position = new Vector3(userBowlingMaxLimit.transform.position.x, bowlingSpotTransform.position.y, bowlingSpotTransform.position.z);
				}
			}
			if (upArrowKeyDown)
			{
				bowlingSpotTransform.position += new Vector3(0f, 0f, num2 * Time.deltaTime);
				if (bowlingSpotTransform.position.z > userBowlingMinLimit.transform.position.z)
				{
					bowlingSpotTransform.position = new Vector3(bowlingSpotTransform.position.x, bowlingSpotTransform.position.y, userBowlingMinLimit.transform.position.z);
				}
			}
			if (downArrowKeyDown)
			{
				bowlingSpotTransform.position -= new Vector3(0f, 0f, num2 * Time.deltaTime);
				if (bowlingSpotTransform.position.z < userBowlingMaxLimit.transform.position.z)
				{
					bowlingSpotTransform.position = new Vector3(bowlingSpotTransform.position.x, bowlingSpotTransform.position.y, userBowlingMaxLimit.transform.position.z);
				}
			}
		}
		Singleton<Tutorial>.instance.updateBowlingHolderPos(bowlingSpotTransform.position);
	}

	private void turnOnFielders(bool boolean)
	{
		for (int i = 0; i < fielder.Length; i++)
		{
			if (fielder[i] != null)
			{
				FielderSkinRendererComponent[i].enabled = boolean;
			}
		}
		BatsmanSkinRendererComponent.enabled = true;
		RunnerSkinRendererComponent.enabled = boolean;
		mainUmpire.gameObject.SetActiveRecursively(boolean);
		sideUmpire.SetActiveRecursively(boolean);
		wicketKeeper.SetActiveRecursively(boolean);
	}

	public void EnablePauseCountDown()
	{
		Singleton<Scoreboard>.instance.pauseBtn.enabled = false;
		resumeGO.SetActive(value: true);
		Sequence sequence = DOTween.Sequence();
		sequence.Append(three.transform.DOScale(new Vector3(1f, 1f, 1f), 0.8f));
		sequence.Insert(0.8f, three.DOFade(0f, 0.2f));
		sequence.SetUpdate(isIndependentUpdate: true);
		sequence.OnComplete(EnablePauseCountDown2);
		userBowlerCanMoveBowlingSpot = false;
		Singleton<GameModel>.instance.HideUIMenu(hide: true);
	}

	private void EnablePauseCountDown2()
	{
		Sequence sequence = DOTween.Sequence();
		sequence.Insert(0f, two.transform.DOScale(new Vector3(1f, 1f, 1f), 0.8f));
		sequence.Insert(0.8f, two.DOFade(0f, 0.2f));
		sequence.SetUpdate(isIndependentUpdate: true);
		sequence.OnComplete(EnablePauseCountDown3);
	}

	private void EnablePauseCountDown3()
	{
		Sequence sequence = DOTween.Sequence();
		sequence.Insert(0f, one.transform.DOScale(new Vector3(1f, 1f, 1f), 0.8f));
		sequence.Insert(0.8f, one.DOFade(0f, 0.2f));
		sequence.SetUpdate(isIndependentUpdate: true);
		sequence.OnComplete(pauseCountdown);
	}

	public void destroyCountDown()
	{
		Singleton<Scoreboard>.instance.pauseBtn.enabled = true;
		resumeGO.SetActive(value: false);
		Sequence sequence = DOTween.Sequence();
		sequence.Append(three.transform.DOScale(new Vector3(0f, 0f, 1f), 0f));
		sequence.Insert(0f, two.transform.DOScale(new Vector3(0f, 0f, 1f), 0f));
		sequence.Insert(0f, one.transform.DOScale(new Vector3(0f, 0f, 1f), 0f));
		sequence.Insert(0f, three.DOFade(1f, 0f));
		sequence.Insert(0f, two.DOFade(1f, 0f));
		sequence.Insert(0f, one.DOFade(1f, 0f));
		sequence.SetUpdate(isIndependentUpdate: true);
		userBowlerCanMoveBowlingSpot = true;
		Singleton<GameModel>.instance.HideUIMenu(hide: false);
	}

	public void FindBatsmanCanMakeShot()
	{
		if (ballTransform.position.z > shotActivationMinLimit.transform.position.z && ballTransform.position.z < shotActivationMaxLimit.transform.position.z && !batsmanTriggeredShot)
		{
			canMakeShot = true;
		}
	}

	public void BallMovement()
	{
		float x = Mathf.Cos(ballAngle * DEG2RAD) * horizontalSpeed * Time.deltaTime;
		float z = Mathf.Sin(ballAngle * DEG2RAD) * horizontalSpeed * Time.deltaTime;
		float num = Mathf.Sin(ballProjectileAngle * DEG2RAD) * ballProjectileHeight - ballRadius;
		if (float.IsNaN(num))
		{
			num = 0f;
		}
		ballTransform.position = new Vector3(ballTransform.position.x, 0f - num, ballTransform.position.z);
		ballTransform.position += new Vector3(x, 0f, z);
		ballProjectileAngle += ballProjectileAnglePerSecond * Time.deltaTime;
		ballRayCastReferenceGOTransform.position = new Vector3(ballTransform.position.x, ballTransform.position.y, ballTransform.position.z);
		ballRayCastReferenceGOTransform.eulerAngles = new Vector3(ballRayCastReferenceGOTransform.eulerAngles.x, (90f - ballAngle + 360f) % 360f, ballRayCastReferenceGOTransform.eulerAngles.z);
		if (ballOnboundaryLine)
		{
			ballSpinningSpeedInX = 300 + UnityEngine.Random.Range(0, 61);
			ballSpinningSpeedInZ = 300 + UnityEngine.Random.Range(0, 61);
		}
		ballTransform.Rotate(Vector3.right * Time.deltaTime * ballSpinningSpeedInX, Space.World);
		ballTransform.Rotate(Vector3.forward * Time.deltaTime * ballSpinningSpeedInZ, Space.World);
		if (replayMode && action == 4 && ballProjectileAngle >= 360f && !ballOnboundaryLine)
		{
			ballSpinningSpeedInX = UnityEngine.Random.Range(-3600, -1800);
			ballSpinningSpeedInZ = UnityEngine.Random.Range(-3600, -1800);
		}
	}

	public void ActivateWicketKeeper()
	{
		wicketKeeperIsActive = true;
		float num = wicketKeeperTransform.position.x - ballTransform.position.x;
		float num2 = wicketKeeperTransform.position.z - ballTransform.position.z;
		wicketKeeperAdjacentLength = Mathf.Sqrt(num * num + num2 * num2);
		wicketKeeperThetaBtwAdjAndHyp = Mathf.Atan2(num, num2) * RAD2DEG - (90f - ballAngle);
		wicketKeeperHypotenuse = wicketKeeperAdjacentLength / Mathf.Cos(wicketKeeperThetaBtwAdjAndHyp * DEG2RAD);
		wicketKeeperOppositeLength = Mathf.Sqrt(wicketKeeperHypotenuse * wicketKeeperHypotenuse - wicketKeeperAdjacentLength * wicketKeeperAdjacentLength);
	}

	private bool keeperCatch()
	{
		if ((Mathf.Abs(wicketKeeperTransform.position.z - ballTransform.position.z) < 0.5f || wicketKeeperTransform.position.z < ballTransform.position.z) && wicketKeeperStatus == string.Empty && wicketKeeperOppositeLength > wicketKeeprMaxCatchingDistance)
		{
			return false;
		}
		return true;
	}

	private void WicketKeeperPreBattingActions()
	{
		if (!wicketKeeperIsActive)
		{
			return;
		}
		float num = wicketKeeperTransform.position.x - ballTransform.position.x;
		float num2 = wicketKeeperTransform.position.z - ballTransform.position.z;
		float num3 = Mathf.Sqrt(num * num + num2 * num2);
		if (!wicketKeeperCatchingAnimationSelected)
		{
			bool flag = keeperCatch();
			wicketKeeperCatchingAnimationSelected = true;
			float num4 = 4f;
			float num5 = 6f;
			float num6 = 10f;
			float num7 = 4f;
			float num8 = 8f;
			if (wicketKeeperOppositeLength < 0.3f)
			{
				if (edgeCatch && flag)
				{
					num4 = 7f;
					wicketKeeperAnimationClip = "chestCatchAppeal";
					keeperCaughtDiffCatch = false;
				}
				else
				{
					wicketKeeperAnimationClip = "chestCatch";
				}
				wicketKeeperCatchingFrame = num4;
				if (currentBowlerType == "spin" && DistanceBetweenTwoVector2(bowlingSpotGO, wicketKeeper) < 6f)
				{
					if (edgeCatch && flag)
					{
						num7 = 6f;
						wicketKeeperAnimationClip = "spinHipCatchAppeal";
						keeperCaughtDiffCatch = false;
					}
					else
					{
						wicketKeeperAnimationClip = "spinHipCatch";
					}
					wicketKeeperCatchingFrame = num7;
				}
			}
			else if (wicketKeeperThetaBtwAdjAndHyp > 0f)
			{
				if (wicketKeeperOppositeLength < 0.8f)
				{
					if (edgeCatch && flag)
					{
						num5 = 11f;
						wicketKeeperAnimationClip = "rightCatchAppeal";
						keeperCaughtDiffCatch = false;
					}
					else
					{
						wicketKeeperAnimationClip = "rightShortCatch";
					}
					wicketKeeperCatchingFrame = num5;
				}
				else if (wicketKeeperOppositeLength < 1.2f)
				{
					if (edgeCatch && flag)
					{
						num6 = 11f;
						wicketKeeperAnimationClip = "rightCatchAppeal";
						keeperCaughtDiffCatch = false;
					}
					else
					{
						wicketKeeperAnimationClip = "rightSideCatch";
					}
					wicketKeeperCatchingFrame = num6;
				}
				else if (wicketKeeperOppositeLength < 1.75f)
				{
					if (edgeCatch && flag)
					{
						num8 = 14f;
						wicketKeeperAnimationClip = "diveRightAppeal";
						keeperCaughtDiffCatch = true;
					}
					else
					{
						wicketKeeperAnimationClip = "extremeRightJump2";
					}
					wicketKeeperCatchingFrame = num8;
				}
				else
				{
					num8 = 18f;
					wicketKeeperAnimationClip = "extremeRightJump2";
					wicketKeeperCatchingFrame = num8;
				}
			}
			else if (wicketKeeperOppositeLength < 0.8f)
			{
				if (edgeCatch && flag)
				{
					num5 = 8f;
					wicketKeeperAnimationClip = "spinLeftShortAppeal";
					keeperCaughtDiffCatch = false;
				}
				else
				{
					wicketKeeperAnimationClip = "leftShortCatch";
				}
				wicketKeeperCatchingFrame = num5;
			}
			else if (wicketKeeperOppositeLength < 1.2f)
			{
				if (edgeCatch && flag)
				{
					num6 = 12f;
					wicketKeeperAnimationClip = "spinLeftAppeal";
					keeperCaughtDiffCatch = false;
				}
				else
				{
					wicketKeeperAnimationClip = "leftSideCatch";
				}
				wicketKeeperCatchingFrame = num6;
			}
			else if (wicketKeeperOppositeLength < 1.75f)
			{
				if (edgeCatch && flag)
				{
					num8 = 16f;
					wicketKeeperAnimationClip = "diveCatchLeftAppeal";
					keeperCaughtDiffCatch = true;
				}
				else
				{
					wicketKeeperAnimationClip = "extremeLeftJump";
				}
				wicketKeeperCatchingFrame = num8;
			}
			else
			{
				num8 = 18f;
				wicketKeeperAnimationClip = "extremeLeftJump";
				wicketKeeperCatchingFrame = num8;
			}
			if (!replayMode)
			{
				savedWicketKeeperAnimationClip = wicketKeeperAnimationClip;
				savedWicketKeeperCatchingFrame = wicketKeeperCatchingFrame;
				savedKeeperCaughtDiffCatch = keeperCaughtDiffCatch;
			}
			else if (replayMode)
			{
				wicketKeeperAnimationClip = savedWicketKeeperAnimationClip;
				wicketKeeperCatchingFrame = savedWicketKeeperCatchingFrame;
				keeperCaughtDiffCatch = savedKeeperCaughtDiffCatch;
			}
			CanShowCountDown = false;
		}
		if ((wicketKeeperStatus == string.Empty || wicketKeeperStatus == "catchAttempt") && ballStatus == "bowled")
		{
			if (overStepBall || !lineFreeHit)
			{
				WicketKeeperAnimationComponent.Play("appealFast");
			}
			stayStartTime = Time.time;
			wicketKeeperStatus = "bowledEnd";
		}
		else if (num3 < wicketKeeperCatchingFrame * animationFPSDivide * horizontalSpeed && wicketKeeperStatus == string.Empty)
		{
			if (currentBowlerType == "spin")
			{
				WicketKeeperAnimationComponent[wicketKeeperAnimationClip].speed = 1.5f;
			}
			else
			{
				WicketKeeperAnimationComponent[wicketKeeperAnimationClip].speed = 1f;
			}
			WicketKeeperAnimationComponent.CrossFade(wicketKeeperAnimationClip);
			wicketKeeperStatus = "catchAttempt";
			if (wicketKeeperAnimationClip == "chestCatch" || wicketKeeperAnimationClip == "spinHipCatch")
			{
				if (wicketKeeperThetaBtwAdjAndHyp > 0f)
				{
					iTween.MoveTo(wicketKeeper, iTween.Hash("x", wicketKeeperTransform.position.x - wicketKeeperOppositeLength, "time", 0.2));
				}
				else
				{
					iTween.MoveTo(wicketKeeper, iTween.Hash("x", wicketKeeperTransform.position.x + wicketKeeperOppositeLength, "time", 0.2));
				}
			}
		}
		else if ((Mathf.Abs(wicketKeeperTransform.position.z - ballTransform.position.z) < 0.5f || wicketKeeperTransform.position.z < ballTransform.position.z) && wicketKeeperStatus == "catchAttempt" && wicketKeeperOppositeLength <= wicketKeeprMaxCatchingDistance)
		{
			stayStartTime = Time.time;
			if (edgeCatch)
			{
				makeFieldersToCelebrate(null);
			}
			wicketKeeperStatus = "catchEnd";
			wicketKeeperBall.GetComponent<Renderer>().enabled = true;
			ShowBall(status: false);
			pauseTheBall = true;
			if (!(wicketKeeperStatus == "catchEnd") || !(currentBowlerType == "spin") || edgeCatch || freeHit || !(wicketKeeperAnimationClip != "extremeLeftJump") || !(wicketKeeperAnimationClip != "extremeRightJump2") || (!(batsmanAnimation == "bt6CoverDrive") && !(batsmanAnimation == "bt6LegGlance") && !(batsmanAnimation == "bt6OnDrive") && !(batsmanAnimation == "loftLegSide") && !(batsmanAnimation == "loftOffSide") && !(batsmanAnimation == "loftStraight") && !(batsmanAnimation == "frontFootOffDrive")) || CONTROLLER.isFreeHitBall)
			{
				return;
			}
			wicketKeeperStatus = "stumpingAttempt";
			iTween.Stop(wicketKeeper);
			Vector3 position = wicketKeeperTransform.position;
			position.z -= 0.5f;
			wicketKeeperTransform.position = position;
			WicketKeeperAnimationComponent.CrossFade("collectStumpAppeal");
			if (batsmanAnimation == "bt6CoverDrive" || batsmanAnimation == "bt6LegGlance")
			{
				batsmanAnimationComponent[batsmanAnimation].time = GetBatsmanReturnFrameTimeToCrease();
				batsmanAnimationComponent[batsmanAnimation].speed = 2.5f;
				stumpingReturnAnimationType = 1;
			}
			else
			{
				Vector3 position2 = batsmanTransform.position;
				position2.x = ShadowRefArrayTransform[11].position.x;
				position2.z = ShadowRefArrayTransform[11].position.z;
				if (!replayMode && position2.z > 7.5f && ((currentBatsmanHand == "right" && (double)position2.x > 0.1 && (double)position2.x < 0.6) || (currentBatsmanHand == "left" && (double)position2.x < -0.1 && (double)position2.x > -0.6)))
				{
					savedReturnToCreaseAnimationId = 1;
				}
				if (savedReturnToCreaseAnimationId == 1)
				{
					Vector3 eulerAngles = batsmanTransform.eulerAngles;
					if (currentBatsmanHand == "right")
					{
						eulerAngles.y = 290f;
					}
					else
					{
						eulerAngles.y = 70f;
					}
					batsmanTransform.eulerAngles = eulerAngles;
				}
				batsmanTransform.position = position2;
				batsmanAnimationComponent.Play("ReturnToCrease2");
				float speed = (replayMode ? savedBatsmanReturnToCreaseSpeed : (savedBatsmanReturnToCreaseSpeed = UnityEngine.Random.Range(1f, 1.2f)));
				batsmanAnimationComponent["ReturnToCrease2"].speed = speed;
				stumpingReturnAnimationType = 2;
			}
			if (Singleton<GameModel>.instance != null)
			{
				Singleton<GameModel>.instance.PlayGameSound("Beaten");
			}
		}
		else if (wicketKeeperStatus == "stumpingAttempt")
		{
			float num9 = 8f;
			wicketKeeperTransform.LookAt(stump1Spot.transform);
			if (!(WicketKeeperAnimationComponent["collectStumpAppeal"].time > num9 * animationFPSDivide))
			{
				return;
			}
			if (!replayMode)
			{
				if (stumpingReturnAnimationType == 1)
				{
					stumped = IsStumpOut();
				}
				else
				{
					stumped = IsStumpOut2();
				}
				isStumped = stumped;
				savedIsStumped = isStumped;
			}
			else if (replayMode && !overStepBall && !lineFreeHit)
			{
				Time.timeScale = 0.02f;
				isStumped = savedIsStumped;
			}
			stayStartTime = Time.time;
			if (overStepBall || !CONTROLLER.isLineFreeHitBallCompleted)
			{
				wicketKeeperStatus = "catchEnd";
			}
			else if (lineFreeHit || !CONTROLLER.isLineFreeHitBallCompleted)
			{
				wicketKeeperStatus = "catchEnd";
			}
			else
			{
				wicketKeeperStatus = "stumpingAppeal";
			}
			makeFieldersToCelebrate(null);
			float num10 = 90f;
			iTween.RotateTo(wicketKeeper, iTween.Hash("y", num10, "time", 0.5, "delay", 0.2));
			Stump1AnimationComponent.Play("legSideStumping");
		}
		else if (wicketKeeperStatus == "stumpingAppeal")
		{
			if (replayMode)
			{
				if ((double)stayStartTime + 0.1 < (double)Time.time)
				{
					Time.timeScale = 0.5f;
				}
			}
			else if (!replayMode && wideBall && !wideWithStumpingSignalShown && (double)stayStartTime + 1.0 < (double)Time.time)
			{
				wideWithStumpingSignalShown = true;
				wicketKeeperStatus = "showWideSignalBeforeStumpingResult";
				mainCamera.enabled = false;
				rightSideCamera.enabled = false;
				leftSideCamera.enabled = false;
				closeUpCamera.enabled = false;
				umpireCamera.enabled = true;
				umpireCameraTransform.position = mainUmpireInitPosition;
				umpireCameraTransform.position = new Vector3(umpireCameraTransform.position.x, 2f, umpireCameraTransform.position.z);
				umpireCameraTransform.position += new Vector3(0f, 0f, 3f);
				umpireCameraTransform.eulerAngles = new Vector3(5 + UnityEngine.Random.Range(0, 5), 180f, umpireCameraTransform.eulerAngles.z);
				stayStartTime = Time.time;
				MainUmpireAnimationComponent.Play("WideBall_New");
				if (Singleton<GameModel>.instance != null && !replayMode)
				{
					Singleton<GameModel>.instance.PlayGameSound("Beaten");
				}
			}
			if (!(stayStartTime + 1.5f < Time.time) || !(wicketKeeperStatus != "showWideSignalBeforeStumpingResult"))
			{
				return;
			}
			stayStartTime = Time.time;
			mainCamera.enabled = false;
			showPreviewCamera(status: false);
			rightSideCamera.enabled = false;
			leftSideCamera.enabled = false;
			if (!replayMode)
			{
				umpireCamera.enabled = true;
				if (sideUmpireCameraSpot != null)
				{
					Vector3 position3 = sideUmpireCameraSpot.transform.position;
					position3.x = 21f;
					umpireCameraTransform.position = position3;
					umpireCameraTransform.position = new Vector3(umpireCameraTransform.position.x, 1.5f, umpireCameraTransform.position.z);
				}
				umpireCameraPivot.transform.position = sideUmpire.transform.position;
				umpireCameraPivot.transform.eulerAngles = new Vector3(0f, 90f, 0f);
				umpireCameraTransform.eulerAngles = new Vector3(umpireCameraTransform.eulerAngles.x, 90f, umpireCameraTransform.eulerAngles.z);
				umpireCameraTransform.parent = umpireCameraPivot.transform;
				if (!isStumped && (UnityEngine.Random.Range(1, 100) < 90 || CONTROLLER.PlayModeSelected == 6))
				{
					SideUmpireAnimationComponent.CrossFade("Crouch_toNotOut_New");
					wicketKeeperStatus = "Show3rdUmpireResult";
				}
				else
				{
					iTween.RotateTo(umpireCameraPivot, iTween.Hash("y", 0, "time", 1.5f, "delay", 1.5, "easetype", "easeInOutSine"));
					SideUmpireAnimationComponent.CrossFade("3rd Umpire_New");
					wicketKeeperStatus = "waitForStumpingResult";
					Singleton<GameModel>.instance.CanPauseGame = false;
				}
			}
			else if (replayMode)
			{
				wicketKeeperStatus = "waitForStumpingResult";
			}
			distanceBetweenUmpireAndFielder(boolean: false);
			if (Singleton<GameModel>.instance != null && !replayMode)
			{
				Singleton<GameModel>.instance.PlayGameSound("Cheer");
			}
		}
		else if (wicketKeeperStatus == "showWideSignalBeforeStumpingResult")
		{
			if (stayStartTime + 2f < Time.time)
			{
				stayStartTime = Time.time;
				wicketKeeperStatus = "stumpingAppeal";
			}
		}
		else if (wicketKeeperStatus == "BadCallStumpingResult")
		{
			if (!(stayStartTime + 3f < Time.time))
			{
				return;
			}
			wicketKeeperStatus = "loopEnd";
			if (!(Singleton<GameModel>.instance != null))
			{
				return;
			}
			if (noBall)
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
				Singleton<GameModel>.instance.UpdateCurrentBall(0, 0, 0, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
				if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
				{
				}
			}
			else if (freeHit)
			{
				Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, 0, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
			}
			else if (wideBall)
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWideBall++;
				Singleton<GameModel>.instance.UpdateCurrentBall(0, 0, 0, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
				if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
				{
				}
			}
			else if (!freeHit && !noBall)
			{
				Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, 0, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
			}
		}
		else if (wicketKeeperStatus == "waitForStumpingResult")
		{
			if (stayStartTime + 4f < Time.time && !replayMode && CONTROLLER.PlayModeSelected != 6)
			{
				wicketKeeperStatus = "loopEnd";
				umpireCameraTransform.parent = null;
				CONTROLLER.stumpingAttempted = true;
				replayCameraTransform.position = new Vector3(-7f, 1.2f, 8.8f);
				Singleton<GameModel>.instance.GameIsOnStumpingReplay();
				ShowReplay();
			}
			else if ((stayStartTime + 0.1f < Time.time && replayMode) || CONTROLLER.PlayModeSelected == 6)
			{
				ShowThirdUmpireDecisionBoard();
			}
		}
		else if (wicketKeeperStatus == "waitFor3rdUmpireResult")
		{
			if (!(stayStartTime + 3f < Time.time))
			{
				return;
			}
			digitalScreen.transform.localScale = new Vector3(0f, 0f, 0f);
			iTween.ScaleTo(digitalScreen, iTween.Hash("scale", digitalScreenScale, "time", 0.4f, "easetype", "spring"));
			if (isStumped)
			{
				DigitalScreenRendererComponent.material.mainTexture = digitalBoardContent[3];
				if (Singleton<GameModel>.instance != null)
				{
					Singleton<GameModel>.instance.PlayGameSound("Cheer");
				}
			}
			else
			{
				DigitalScreenRendererComponent.material.mainTexture = digitalBoardContent[2];
				if (Singleton<GameModel>.instance != null)
				{
					Singleton<GameModel>.instance.PlayGameSound("Beaten");
				}
			}
			stayStartTime = Time.time;
			wicketKeeperStatus = "Show3rdUmpireResult";
		}
		else if (wicketKeeperStatus == "Show3rdUmpireResult")
		{
			if (!(stayStartTime + 2f < Time.time))
			{
				return;
			}
			if (isStumped)
			{
				HideReplay();
				if (Singleton<GameModel>.instance != null)
				{
					if (noBall)
					{
						CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
						Singleton<GameModel>.instance.UpdateCurrentBall(0, 0, 0, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
						if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
						{
						}
					}
					else if (freeHit)
					{
						Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, 0, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
					}
					else if (wideBall)
					{
						CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWideBall++;
						Singleton<GameModel>.instance.UpdateCurrentBall(0, 0, 0, 1, CONTROLLER.StrikerIndex, 1, 6, CONTROLLER.CurrentBowlerIndex, 0, CONTROLLER.StrikerIndex, isBoundary: false);
						if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
						{
						}
					}
					else if (!noBall && !freeHit)
					{
						Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, 0, 0, CONTROLLER.StrikerIndex, 1, 6, CONTROLLER.CurrentBowlerIndex, 0, CONTROLLER.StrikerIndex, isBoundary: false);
					}
				}
			}
			else
			{
				HideReplay();
				if (Singleton<GameModel>.instance != null)
				{
					if (noBall)
					{
						CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
						Singleton<GameModel>.instance.UpdateCurrentBall(0, 0, 0, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
						if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
						{
						}
					}
					else if (freeHit)
					{
						Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, 0, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
					}
					else if (wideBall)
					{
						CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWideBall++;
						Singleton<GameModel>.instance.UpdateCurrentBall(0, 0, 0, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
						if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
						{
						}
					}
					else if (!freeHit && !noBall)
					{
						Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, 0, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
					}
				}
			}
			stayStartTime = Time.time;
			wicketKeeperStatus = string.Empty;
		}
		else if ((Mathf.Abs(wicketKeeperTransform.position.z - ballTransform.position.z) < 0.5f || wicketKeeperTransform.position.z < ballTransform.position.z) && wicketKeeperStatus == "catchAttempt" && wicketKeeperOppositeLength > wicketKeeprMaxCatchingDistance)
		{
			wicketKeeperStatus = "catchMissed";
			canBe4or6 = 4;
			if (edgeCatch)
			{
				canKeeperCollectBall = false;
				SetActiveFielders();
			}
			if (CONTROLLER.cameraType != 0)
			{
				leftSideCamera.enabled = true;
				leftSideCamera.fieldOfView = 35f;
				mainCamera.enabled = false;
			}
		}
		else if (wicketKeeperStatus == "bowledEnd")
		{
			if (!(stayStartTime + 1f + timeBetweenBalls < Time.time))
			{
				return;
			}
			if (overStepBall)
			{
				if (replayMode)
				{
					wicketKeeperStatus = "loopEnd";
					HideReplay();
					CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
					Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, 0, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, CONTROLLER.StrikerIndex, isBoundary: false);
					if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
					{
					}
					return;
				}
				showMainUmpireForNoBallAction();
				wicketKeeperStatus = "umpireNoBallActionForBowled";
				bool flag2 = checkForMatchComplete(1, 0);
				if (CONTROLLER.matchType == "oneday" && !flag2)
				{
					noBallActionWaitTime = 4f;
					MainUmpireAnimationComponent.Play("NoBallFreeHit_New");
					lineFreeHit = true;
					lastBowledBall = "overstep";
				}
				else
				{
					noBallActionWaitTime = 1.5f;
					MainUmpireAnimationComponent.Play("NoBallFreeHit_New");
				}
				return;
			}
			if (lineFreeHit)
			{
				wicketKeeperStatus = "loopEnd";
				Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, 0, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, CONTROLLER.StrikerIndex, isBoundary: false);
				lineFreeHit = false;
				lastBowledBall = "lineball";
				Singleton<Scoreboard>.instance.showFreeHitBg(canShow: false);
				return;
			}
			wicketKeeperStatus = "loopEnd";
			if (replayMode)
			{
				HideReplay();
			}
			else
			{
				if (!(Singleton<GameModel>.instance != null))
				{
					return;
				}
				if (overStepBall)
				{
					CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
					Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, 0, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, CONTROLLER.StrikerIndex, isBoundary: false);
					CONTROLLER.isJokerCall = false;
					if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
					{
					}
				}
				else if (lineFreeHit)
				{
					Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, 0, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, CONTROLLER.StrikerIndex, isBoundary: false);
					freeHit = false;
				}
				else if (!overStepBall && !lineFreeHit)
				{
					Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, 0, 0, CONTROLLER.StrikerIndex, 1, 1, CONTROLLER.CurrentBowlerIndex, 0, CONTROLLER.StrikerIndex, isBoundary: false);
				}
			}
		}
		else if (wicketKeeperStatus == "umpireNoBallActionForBowled")
		{
			if (!(stayStartTime + noBallActionWaitTime + timeBetweenBalls < Time.time))
			{
				return;
			}
			wicketKeeperStatus = "loopEnd";
			if (!(Singleton<GameModel>.instance != null))
			{
				return;
			}
			if (overStepBall)
			{
				if (!replayMode)
				{
					noBallRunUpdateStatus = "cleanbowled";
					if (CONTROLLER.canShowReplay)
					{
						Singleton<GameModel>.instance.GameIsOnReplay();
						ShowReplay();
					}
					else
					{
						Singleton<GameModel>.instance.ReplayIsNotShown();
					}
					return;
				}
				HideReplay();
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
				Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, 0, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, CONTROLLER.StrikerIndex, isBoundary: false);
				if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
				{
				}
			}
			else if (lineFreeHit)
			{
				Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, 0, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, CONTROLLER.StrikerIndex, isBoundary: false);
				lineFreeHit = false;
				lastBowledBall = "lineball";
			}
		}
		else if (wicketKeeperStatus == "CaughtBehind")
		{
			stayStartTime = Time.time;
			wicketKeeperStatus = "waitForCaughtBehindResult";
		}
		else if (wicketKeeperStatus == "waitForCaughtBehindResult")
		{
			if (!(stayStartTime + 1f + timeBetweenBalls < Time.time))
			{
				return;
			}
			stayStartTime = Time.time;
			wicketKeeperStatus = "decisionPending";
			mainCamera.enabled = false;
			showPreviewCamera(status: false);
			rightSideCamera.enabled = false;
			leftSideCamera.enabled = false;
			if (replayMode)
			{
				wicketKeeperStatus = "loopEnd";
				HideReplay();
				return;
			}
			if (!replayMode)
			{
				umpireCamera.enabled = true;
				umpireCameraTransform.position = mainUmpireInitPosition;
				umpireCameraTransform.position = new Vector3(umpireCameraTransform.position.x, 2f, umpireCameraTransform.position.z);
				umpireCameraTransform.position += new Vector3(0f, 0f, 3f);
				umpireCameraTransform.eulerAngles = new Vector3(5 + UnityEngine.Random.Range(0, 5), 180f, 0f);
			}
			if (!noBall && !freeHit && ballNoOfBounce == 0)
			{
				if (UmpireInitialDecision == "out")
				{
					MainUmpireAnimationComponent.CrossFade("Out2_New");
				}
				else
				{
					MainUmpireAnimationComponent.CrossFade("NotOut");
				}
				umpireAnimationPlayed = true;
			}
			else
			{
				MainUmpireAnimationComponent.CrossFade("NotOut");
			}
			if (Singleton<GameModel>.instance != null && !replayMode)
			{
				Singleton<GameModel>.instance.PlayGameSound("Cheer");
			}
		}
		else if (wicketKeeperStatus == "decisionPending")
		{
			if (!(stayStartTime + 1f + timeBetweenBalls < Time.time))
			{
				return;
			}
			float num11 = 3f;
			if (replayMode)
			{
				num11 = 0.5f;
			}
			if (!(stayStartTime + num11 + timeBetweenBalls < Time.time))
			{
				return;
			}
			wicketKeeperStatus = "loopEnd";
			if (replayMode)
			{
				HideReplay();
			}
			else
			{
				if (!(Singleton<GameModel>.instance != null))
				{
					return;
				}
				if (noBall)
				{
					CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
					Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, currentBallNoOfRuns, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
					CONTROLLER.isJokerCall = false;
					if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
					{
					}
				}
				else if (freeHit)
				{
					Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, currentBallNoOfRuns, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
					freeHit = false;
					isFreeHit = false;
				}
				else if (ballNoOfBounce != 0)
				{
					Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, currentBallNoOfRuns, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, CONTROLLER.StrikerIndex, isBoundary: false);
				}
				else if (!noBall && !freeHit)
				{
					if (UmpireInitialDecision == "out")
					{
						Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, currentBallNoOfRuns, 0, CONTROLLER.StrikerIndex, 1, 5, CONTROLLER.CurrentBowlerIndex, 0, CONTROLLER.StrikerIndex, isBoundary: false);
					}
					else
					{
						Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, currentBallNoOfRuns, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, CONTROLLER.StrikerIndex, isBoundary: false);
					}
				}
				canKeeperCollectBall = false;
			}
		}
		else if (wicketKeeperStatus == "catchEnd")
		{
			if (!(stayStartTime + 1f + timeBetweenBalls < Time.time))
			{
				return;
			}
			if (overStepBall)
			{
				if (replayMode)
				{
					wicketKeeperStatus = "loopEnd";
					HideReplay();
					CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
					Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, 0, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
					if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
					{
					}
					return;
				}
				showMainUmpireForNoBallAction();
				wicketKeeperStatus = "umpireNoBallAction";
				iTween.MoveTo(umpireCamera.gameObject, iTween.Hash("y", UnityEngine.Random.Range(1.4f, 1.8f), "time", 2));
				bool flag3 = checkForMatchComplete(1, 0);
				if (CONTROLLER.matchType == "oneday" && !flag3)
				{
					noBallActionWaitTime = 4f;
					MainUmpireAnimationComponent.Play("NoBallFreeHit_New");
					lineFreeHit = true;
					lastBowledBall = "overstep";
				}
				else
				{
					noBallActionWaitTime = 1.5f;
					MainUmpireAnimationComponent.Play("NoBallFreeHit_New");
				}
				if (Singleton<GameModel>.instance != null && !replayMode)
				{
					Singleton<GameModel>.instance.PlayGameSound("Beaten");
				}
				return;
			}
			if (lineFreeHit)
			{
				if (wideBall)
				{
					showMainUmpireForNoBallAction();
					wicketKeeperStatus = "waitForWideSignal";
					MainUmpireAnimationComponent.Play("WideBall_New");
					if (bowlingBy == "computer")
					{
						bowlerSide = "left";
					}
					if (Singleton<GameModel>.instance != null && !replayMode)
					{
						Singleton<GameModel>.instance.PlayGameSound("Beaten");
					}
				}
				else
				{
					wicketKeeperStatus = "loopEnd";
					Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, 0, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
					lineFreeHit = false;
					lastBowledBall = "lineball";
					Singleton<Scoreboard>.instance.showFreeHitBg(canShow: false);
				}
				return;
			}
			if (edgeCatch)
			{
				wicketKeeperStatus = "CaughtBehind";
				stayStartTime = Time.time;
				WicketKeeperBallSkinRendererComponent.enabled = true;
				ShowBall(status: false);
				pauseTheBall = true;
				return;
			}
			if (wideBall)
			{
				showMainUmpireForNoBallAction();
				wicketKeeperStatus = "waitForWideSignal";
				if (noBall)
				{
					MainUmpireAnimationComponent.Play("NoBallFreeHit_New");
				}
				else
				{
					MainUmpireAnimationComponent.Play("WideBall_New");
				}
				if (bowlingBy == "computer")
				{
					bowlerSide = "left";
				}
				if (Singleton<GameModel>.instance != null && !replayMode)
				{
					Singleton<GameModel>.instance.PlayGameSound("Beaten");
				}
				return;
			}
			wicketKeeperStatus = "loopEnd";
			if (!(Singleton<GameModel>.instance != null))
			{
				return;
			}
			if (noBall)
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
				Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, 0, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
				CONTROLLER.isJokerCall = false;
				if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
				{
				}
			}
			else if (freeHit)
			{
				Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, 0, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
				freeHit = false;
				isFreeHit = false;
			}
			else if (!noBall && !freeHit)
			{
				Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, 0, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
			}
		}
		else if (wicketKeeperStatus == "umpireNoBallAction")
		{
			if (!(stayStartTime + noBallActionWaitTime + timeBetweenBalls < Time.time))
			{
				return;
			}
			wicketKeeperStatus = "loopEnd";
			if (!(Singleton<GameModel>.instance != null))
			{
				return;
			}
			if (overStepBall)
			{
				if (!replayMode)
				{
					noBallRunUpdateStatus = "beatenball";
					if (CONTROLLER.canShowReplay)
					{
						Singleton<GameModel>.instance.GameIsOnReplay();
						ShowReplay();
					}
					else
					{
						Singleton<GameModel>.instance.ReplayIsNotShown();
					}
					return;
				}
				HideReplay();
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
				Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, 0, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
				if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
				{
				}
			}
			else if (lineFreeHit)
			{
				Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, 0, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
				lineFreeHit = false;
				lastBowledBall = "lineball";
			}
		}
		else if (wicketKeeperStatus == "waitForWideSignal")
		{
			if (!(stayStartTime + 2f + timeBetweenBalls < Time.time))
			{
				return;
			}
			wicketKeeperStatus = "loopEnd";
			if (Singleton<GameModel>.instance != null)
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWideBall++;
				Singleton<GameModel>.instance.UpdateCurrentBall(0, 0, 0, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
				if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
				{
				}
			}
		}
		else if (wicketKeeperStatus == "BadCallBowledResult" && (double)stayStartTime + 1.5 < (double)Time.time)
		{
			wicketKeeperStatus = "loopEnd";
			if (Singleton<GameModel>.instance != null)
			{
				Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, 0, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
			}
		}
	}

	public void SkipStumpingReplay()
	{
		SkipReplay();
		action = 3;
		horizontalSpeed = 0f;
		wicketKeeperIsActive = true;
		isStumped = savedIsStumped;
		ShowThirdUmpireDecisionBoard();
	}

	public void ShowThirdUmpireDecisionBoard()
	{
		Time.timeScale = 1f;
		Singleton<GameModel>.instance.GameIsNotOnStumpingReplay();
		replayCamera.enabled = false;
		introCamera.enabled = true;
		introCameraTransform.position = new Vector3(-76f, 17f, -3.8f);
		introCameraTransform.eulerAngles = new Vector3(0f, -90f, 0f);
		DigitalScreenRendererComponent.material.mainTexture = digitalBoardContent[1];
		digitalScreen.transform.localScale = digitalScreenScale;
		stayStartTime = Time.time;
		wicketKeeperStatus = "waitFor3rdUmpireResult";
	}

	private void WicketKeeperPostBattingActions()
	{
		if (StopKeeper)
		{
			return;
		}
		if (wicketKeeperStatus == "waitForBall")
		{
			if (ballOnboundaryLine)
			{
				WicketKeeperAnimationComponent.CrossFade("idle");
				wicketKeeperStatus = "finish";
			}
			if (ballResult == "wicket")
			{
				WicketKeeperAnimationComponent.Play("idle");
				wicketKeeperStatus = "finish";
			}
		}
		else if (wicketKeeperStatus == "waitToCollect")
		{
			float num = 6f;
			float num2 = num * animationFPSDivide * horizontalSpeed;
			if (!(DistanceBetweenTwoVector2(ball, wicketKeeper) < num2))
			{
				return;
			}
			canTakeRun = false;
			distanceBtwBallAndCollectingPlayerWhileThrowing = 10000f;
			if (Singleton<GameModel>.instance != null)
			{
				disableRunCancelBtn();
			}
			if ((!replayMode && runOut) || (replayMode && savedThrowAction == "collectTheThrowAndStump"))
			{
				if (Singleton<GameModel>.instance != null && !replayMode)
				{
					Singleton<GameModel>.instance.PlayGameSound("Cheer");
					Singleton<GameModel>.instance.PlayGameSound("Bowled");
				}
				WicketKeeperAnimationComponent.Play("collectStumpAppeal");
				wicketKeeperStatus = "collectTheThrowAndStump";
				savedThrowAction = "collectTheThrowAndStump";
			}
			else if ((!replayMode && !runOut) || (replayMode && savedThrowAction == "collectTheThrow"))
			{
				WicketKeeperAnimationComponent.Play("collectAndStand");
				wicketKeeperStatus = "collectTheThrow";
				savedThrowAction = "collectTheThrow";
			}
		}
		else if (wicketKeeperStatus == "collectTheThrow" || wicketKeeperStatus == "collectTheThrowAndStump")
		{
			float num3 = DistanceBetweenTwoVector2(ball, wicketKeeper);
			if (distanceBtwBallAndCollectingPlayerWhileThrowing > num3)
			{
				distanceBtwBallAndCollectingPlayerWhileThrowing = num3;
			}
			else
			{
				distanceBtwBallAndCollectingPlayerWhileThrowing = -1f;
			}
			if (!replayMode)
			{
				runOut = isBatsmanRunOut();
			}
			if (!(num3 < 0.5f) && distanceBtwBallAndCollectingPlayerWhileThrowing != -1f)
			{
				return;
			}
			ballStatus = string.Empty;
			pauseTheBall = true;
			WicketKeeperBallSkinRendererComponent.enabled = true;
			ShowBall(status: false);
			if (wicketKeeperStatus == "collectTheThrow")
			{
				stayStartTime = Time.time;
				wicketKeeperStatus = "end";
			}
			else
			{
				if (!(wicketKeeperStatus == "collectTheThrowAndStump"))
				{
					return;
				}
				float num4 = 8f;
				wicketKeeperTransform.LookAt(stump1Spot.transform);
				if (WicketKeeperAnimationComponent["collectStumpAppeal"].time > num4 * animationFPSDivide)
				{
					if (!replayMode)
					{
						isRunOut = runOut;
						savedIsRunOut = isRunOut;
						savedCurrentBallNoOfRuns = currentBallNoOfRuns;
					}
					else
					{
						isRunOut = savedIsRunOut;
						currentBallNoOfRuns = savedCurrentBallNoOfRuns;
					}
					stayStartTime = Time.time;
					wicketKeeperStatus = "runOutAppeal";
					savedRunOutAppeal = true;
					if (replayMode)
					{
						StartCoroutine(UltraSlowMotion());
					}
					float num5 = 90f;
					iTween.RotateTo(wicketKeeper, iTween.Hash("y", num5, "time", 0.5, "delay", 0.2));
					if (postBattingWicketKeeperDirection == "straight")
					{
						Stump1AnimationComponent.Play("legSideStumping");
					}
					else if (postBattingWicketKeeperDirection == "offSide")
					{
						Stump1AnimationComponent.Play("fielderRunoutIn");
					}
					else if (postBattingWicketKeeperDirection == "legSide")
					{
						Stump1AnimationComponent.Play("fielderRunoutAway");
					}
				}
			}
		}
		else if (wicketKeeperStatus == "end")
		{
			if (!(stayStartTime + 1f + timeBetweenBalls < Time.time))
			{
				return;
			}
			if (overStepBall)
			{
				if (replayMode)
				{
					wicketKeeperStatus = "loopEnd";
					HideReplay();
					CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
					Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, currentBallNoOfRuns, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
					if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
					{
					}
					return;
				}
				showMainUmpireForNoBallAction();
				wicketKeeperStatus = "umpireNoBallActionAfterWKCollectsBallFromFielder";
				runsScoredInLineNoBall = currentBallNoOfRuns;
				bool flag = checkForMatchComplete(currentBallNoOfRuns + 1, 0);
				if (CONTROLLER.matchType == "oneday" && !flag)
				{
					noBallActionWaitTime = 4f;
					MainUmpireAnimationComponent.Play("NoBallFreeHit_New");
					lineFreeHit = true;
					lastBowledBall = "overstep";
				}
				else
				{
					noBallActionWaitTime = 1.5f;
					MainUmpireAnimationComponent.Play("NoBallFreeHit_New");
				}
				return;
			}
			if (lineFreeHit)
			{
				wicketKeeperStatus = "loopEnd";
				Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, currentBallNoOfRuns, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
				lineFreeHit = false;
				lastBowledBall = "lineball";
				Singleton<Scoreboard>.instance.showFreeHitBg(canShow: false);
				return;
			}
			wicketKeeperStatus = "loopEnd";
			if (!(Singleton<GameModel>.instance != null))
			{
				return;
			}
			if (noBall)
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
				Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, currentBallNoOfRuns, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
				CONTROLLER.isJokerCall = false;
				if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
				{
				}
			}
			else if (freeHit)
			{
				Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, currentBallNoOfRuns, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
				freeHit = false;
				isFreeHit = false;
			}
			else if (!noBall && !freeHit)
			{
				Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, currentBallNoOfRuns, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
			}
		}
		else if (wicketKeeperStatus == "umpireNoBallActionAfterWKCollectsBallFromFielder")
		{
			if (!(stayStartTime + noBallActionWaitTime + timeBetweenBalls < Time.time))
			{
				return;
			}
			wicketKeeperStatus = "loopEnd";
			if (!(Singleton<GameModel>.instance != null) || !overStepBall)
			{
				return;
			}
			if (!replayMode)
			{
				noBallRunUpdateStatus = "keepercollectstheball";
				if (CONTROLLER.canShowReplay)
				{
					Singleton<GameModel>.instance.GameIsOnReplay();
					ShowReplay();
				}
				else
				{
					Singleton<GameModel>.instance.ReplayIsNotShown();
				}
				return;
			}
			HideReplay();
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
			Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, currentBallNoOfRuns, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
			if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
			{
			}
		}
		else if (wicketKeeperStatus == "runOutAppeal")
		{
			if (!fielderAppealForRunOut)
			{
				makeFieldersToCelebrate(null);
				fielderAppealForRunOut = true;
			}
			if (!(stayStartTime + 1f + timeBetweenBalls < Time.time))
			{
				return;
			}
			stayStartTime = Time.time;
			wicketKeeperStatus = "waitForResult";
			mainCamera.enabled = false;
			showPreviewCamera(status: false);
			rightSideCamera.enabled = false;
			leftSideCamera.enabled = false;
			if (replayMode)
			{
				wicketKeeperStatus = "loopEnd";
				HideReplay();
				if (!isRunOut)
				{
					UpdateRunAfterReplay();
				}
				return;
			}
			if (!replayMode)
			{
				umpireCamera.enabled = true;
				umpireCameraTransform.position = sideUmpireCameraSpot.transform.position;
				umpireCameraTransform.position = new Vector3(umpireCameraTransform.position.x, 1.5f, umpireCameraTransform.position.z);
				umpireCameraTransform.eulerAngles = new Vector3(umpireCameraTransform.eulerAngles.x, 90f, umpireCameraTransform.eulerAngles.z);
				iTween.MoveTo(umpireCamera.gameObject, iTween.Hash("x", umpireCameraTransform.position.x + 3f, "time", 2, "easetype", "easeInOutSine"));
			}
			distanceBetweenUmpireAndFielder(boolean: false);
			if (isRunOut)
			{
				SideUmpireAnimationComponent.CrossFade("Out2_New");
				if (Singleton<GameModel>.instance != null && !replayMode)
				{
					Singleton<GameModel>.instance.PlayGameSound("Cheer");
				}
			}
			else
			{
				SideUmpireAnimationComponent.CrossFade("Crouch_toNotOut_New");
				if (Singleton<GameModel>.instance != null && !replayMode)
				{
					Singleton<GameModel>.instance.PlayGameSound("Beaten");
				}
			}
		}
		else if (wicketKeeperStatus == "waitForResult")
		{
			float num6 = 3f;
			if (replayMode)
			{
				num6 = 2f;
			}
			if (!(stayStartTime + num6 + timeBetweenBalls < Time.time))
			{
				return;
			}
			batsmanOutIndex = runOutScenario("w");
			if (isRunOut)
			{
				if (overStepBall)
				{
					wicketKeeperStatus = "keepercollectsandout";
					showMainUmpireForNoBallAction();
					MainUmpireAnimationComponent.Play("IdleGetReady");
					bool flag2 = checkForMatchComplete(currentBallNoOfRuns + 1, CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWickets + 1);
					if (CONTROLLER.matchType == "oneday" && !flag2)
					{
						noBallActionWaitTime = 4f;
						MainUmpireAnimationComponent.Play("NoBallFreeHit_New");
						lineFreeHit = true;
						lastBowledBall = "overstep";
					}
					else
					{
						noBallActionWaitTime = 1.5f;
						MainUmpireAnimationComponent.Play("NoBallFreeHit_New");
					}
					return;
				}
				if (lineFreeHit)
				{
					wicketKeeperStatus = "loopEnd";
					Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, currentBallNoOfRuns, 0, CONTROLLER.StrikerIndex, 1, 4, CONTROLLER.CurrentBowlerIndex, 0, batsmanOutIndex, isBoundary: false);
					lineFreeHit = false;
					lastBowledBall = "lineball";
					Singleton<Scoreboard>.instance.showFreeHitBg(canShow: false);
					return;
				}
				wicketKeeperStatus = "loopEnd";
				if (replayMode)
				{
					HideReplay();
				}
				else
				{
					if (!(Singleton<GameModel>.instance != null))
					{
						return;
					}
					if (noBall)
					{
						CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
						Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, currentBallNoOfRuns, 1, CONTROLLER.StrikerIndex, 1, 4, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
						CONTROLLER.isJokerCall = false;
						if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
						{
						}
					}
					else if (freeHit)
					{
						Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, currentBallNoOfRuns, 0, CONTROLLER.StrikerIndex, 1, 4, CONTROLLER.CurrentBowlerIndex, 0, batsmanOutIndex, isBoundary: false);
						freeHit = false;
					}
					else if (!noBall && !freeHit)
					{
						Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, currentBallNoOfRuns, 0, CONTROLLER.StrikerIndex, 1, 4, CONTROLLER.CurrentBowlerIndex, 0, batsmanOutIndex, isBoundary: false);
					}
				}
			}
			else
			{
				if (!(Singleton<GameModel>.instance != null))
				{
					return;
				}
				if (overStepBall)
				{
					wicketKeeperStatus = "keepercollectsandnotout";
					showMainUmpireForNoBallAction();
					MainUmpireAnimationComponent.Play("IdleGetReady");
					bool flag3 = checkForMatchComplete(currentBallNoOfRuns + 1, 0);
					if (CONTROLLER.matchType == "oneday" && !flag3)
					{
						noBallActionWaitTime = 4f;
						MainUmpireAnimationComponent.Play("NoBallFreeHit_New");
						lineFreeHit = true;
						lastBowledBall = "overstep";
					}
					else
					{
						noBallActionWaitTime = 1.5f;
						MainUmpireAnimationComponent.Play("NoBallFreeHit_New");
					}
					return;
				}
				if (lineFreeHit)
				{
					wicketKeeperStatus = "loopEnd";
					Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, currentBallNoOfRuns, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, batsmanOutIndex, isBoundary: false);
					lineFreeHit = false;
					lastBowledBall = "lineball";
					Singleton<Scoreboard>.instance.showFreeHitBg(canShow: false);
					return;
				}
				wicketKeeperStatus = "loopEnd";
				if (noBall)
				{
					CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
					Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, currentBallNoOfRuns, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
					if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
					{
					}
				}
				else if (freeHit)
				{
					Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, currentBallNoOfRuns, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
					freeHit = false;
					isFreeHit = false;
				}
				else
				{
					if (freeHit || noBall)
					{
						return;
					}
					if (!replayMode)
					{
						afterReplayUpdateRunForRunoutFailedAttempt = true;
						if (CONTROLLER.canShowReplay)
						{
							Singleton<GameModel>.instance.GameIsOnReplay();
							ShowReplay();
						}
						else
						{
							Singleton<GameModel>.instance.ReplayIsNotShown();
						}
					}
					else
					{
						HideReplay();
						UpdateRunAfterReplay();
					}
				}
			}
		}
		else if (wicketKeeperStatus == "keepercollectsandout")
		{
			if (!(stayStartTime + noBallActionWaitTime + timeBetweenBalls < Time.time))
			{
				return;
			}
			wicketKeeperStatus = "loopend";
			if (!(Singleton<GameModel>.instance != null))
			{
				return;
			}
			if (overStepBall)
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
				Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, currentBallNoOfRuns, 1, CONTROLLER.StrikerIndex, 1, 4, CONTROLLER.CurrentBowlerIndex, 0, batsmanOutIndex, isBoundary: false);
				if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
				{
				}
			}
			else if (lineFreeHit)
			{
				Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, currentBallNoOfRuns, 0, CONTROLLER.StrikerIndex, 1, 4, CONTROLLER.CurrentBowlerIndex, 0, batsmanOutIndex, isBoundary: false);
				lineFreeHit = false;
				lastBowledBall = "lineball";
			}
		}
		else
		{
			if (!(wicketKeeperStatus == "keepercollectsandnotout") || !(stayStartTime + noBallActionWaitTime + timeBetweenBalls < Time.time))
			{
				return;
			}
			wicketKeeperStatus = "loopend";
			if (!(Singleton<GameModel>.instance != null))
			{
				return;
			}
			if (overStepBall)
			{
				if (!replayMode)
				{
					noBallRunUpdateStatus = "keeperrunoutappealsandnotout";
					runsScoredInLineNoBall = currentBallNoOfRuns;
					if (CONTROLLER.canShowReplay)
					{
						Singleton<GameModel>.instance.GameIsOnReplay();
						ShowReplay();
					}
					else
					{
						Singleton<GameModel>.instance.ReplayIsNotShown();
					}
				}
				else
				{
					HideReplay();
					CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
					Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, currentBallNoOfRuns, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
					if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
					{
					}
				}
			}
			else if (lineFreeHit)
			{
				Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, currentBallNoOfRuns, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
				lineFreeHit = false;
				lastBowledBall = "lineball";
			}
		}
	}

	private void distanceBetweenUmpireAndFielder(bool boolean)
	{
		for (int i = 1; i <= noOfFielders; i++)
		{
			if (!boolean)
			{
				if (DistanceBetweenTwoVector2(sideUmpire, fielder[i]) < 4f)
				{
					FielderSkinRendererComponent[i].enabled = boolean;
				}
			}
			else
			{
				FielderSkinRendererComponent[i].enabled = boolean;
			}
		}
	}

	private int runOutScenario(string runOutBy)
	{
		int result = 0;
		if (runOutBy == "w")
		{
			result = ((!(batsmanTransform.position.z < 0f)) ? CONTROLLER.StrikerIndex : CONTROLLER.NonStrikerIndex);
		}
		else if (runOutBy == "b")
		{
			result = ((!(batsmanTransform.position.z < 0f)) ? CONTROLLER.NonStrikerIndex : CONTROLLER.StrikerIndex);
		}
		return result;
	}

	private void MoveWicketKeeperToStumps()
	{
		if (StopKeeper)
		{
			return;
		}
		float num = 0f;
		WicketKeeperAnimationComponent.CrossFade("keeperRun");
		if (currentBowlerType == "fast")
		{
			num = 1f;
		}
		else if (currentBowlerType == "medium")
		{
			num = 0.7f;
		}
		if (postBattingWicketKeeperDirection == "straight")
		{
			if (currentBowlerType == "spin")
			{
				num = 0.3f;
			}
			iTween.MoveTo(wicketKeeper, iTween.Hash("position", wicketKeeperStraightBallStumping.transform.position, "time", num, "easetype", "linear", "oncomplete", "EnableWicketKeeperToCollectBall", "oncompletetarget", base.gameObject));
			wicketKeeperTransform.LookAt(wicketKeeperStraightBallStumping.transform);
		}
		else if (postBattingWicketKeeperDirection == "offSide")
		{
			if (currentBowlerType == "spin")
			{
				num = 0.7f;
			}
			iTween.MoveTo(wicketKeeper, iTween.Hash("position", wicketKeeperOffSideBallStumping.transform.position, "time", num, "easetype", "linear", "oncomplete", "EnableWicketKeeperToCollectBall", "oncompletetarget", base.gameObject));
			wicketKeeperTransform.LookAt(wicketKeeperOffSideBallStumping.transform);
		}
		else if (postBattingWicketKeeperDirection == "legSide")
		{
			if (currentBowlerType == "spin")
			{
				num = 0.4f;
			}
			iTween.MoveTo(wicketKeeper, iTween.Hash("position", wicketKeeperLegSideBallStumping.transform.position, "time", num, "easetype", "linear", "oncomplete", "EnableWicketKeeperToCollectBall", "oncompletetarget", base.gameObject));
			wicketKeeperTransform.LookAt(wicketKeeperLegSideBallStumping.transform);
		}
	}

	[Skip]
	private void EnableWicketKeeperToCollectBall()
	{
		WicketKeeperReachedToStump = true;
		WicketKeeperAnimationComponent.Play("idle");
		wicketKeeperTransform.LookAt(fielder10FocusGObjToCollectTheBall.transform);
		wicketKeeperStatus = "waitToCollect";
	}

	public void ShowBall(bool status)
	{
		BallSkinRendererComponent.enabled = status;
		ballSphereCollider.enabled = status;
	}

	private void ThrowingBallMovement()
	{
		if (!pauseTheBall && ballStatus == "throw")
		{
			ballTrail.time = 0.03f;
			BallMovement();
			if (ballProjectileAngle >= 360f)
			{
				ballProjectileAngle = 180f;
				horizontalSpeed *= 0.8f;
				ballProjectileAnglePerSecond = 90f / throwingFirstBounceDistance * horizontalSpeed;
				ballProjectileHeight = 0.75f;
			}
		}
	}

	private void CustomRayCastForBattingBallMovement()
	{
		if (!replayMode)
		{
			Vector3 direction = ballRayCastReferenceGOTransform.TransformDirection(Vector3.forward);
			float maxDistance = 0.7f;
			int num = 256;
			num = ~num;
			if (Physics.Raycast(ballRayCastReferenceGOTransform.position, direction, out var hitInfo, maxDistance, num) && hitInfo.collider.gameObject.transform.parent.name == "Black Board Ad" && ballStatus == "shotSuccess")
			{
				BallRebouncesFromBoundary();
			}
		}
	}

	private void CustomRayCastForBowlingBallMovement()
	{
		if (!replayMode)
		{
			Vector3 direction = ballRayCastReferenceGOTransform.TransformDirection(Vector3.forward);
			float maxDistance = 1f;
			int num = 256;
			num = ~num;
			if (Physics.Raycast(ballRayCastReferenceGOTransform.position, direction, out var hitInfo, maxDistance, num))
			{
				OnCustomTriggerEnter(hitInfo.collider);
				savedBallRayCastConnectedZposition = ballRayCastReferenceGOTransform.position.z;
			}
		}
		else if (replayMode && !Singleton<DRS>.instance.DRSreplay && savedBallRayCastConnectedZposition != 0f && ballRayCastReferenceGOTransform.position.z > savedBallRayCastConnectedZposition)
		{
			if (savedSummary == "connected" || savedSummary == "catch" || savedSummary == "picked")
			{
				OnCustomTriggerEnter(BatColliderComponent);
			}
			else if (savedSummary == "onPads")
			{
				OnCustomTriggerEnter(savedPadCollider);
			}
			else if (savedSummary == "bowled" && savedBallRayCastConnectedZposition != -1f && ballStatus != "bowled")
			{
				OnCustomTriggerEnter(stump1Collider.GetComponent<Collider>());
				savedBallRayCastConnectedZposition = -1f;
			}
		}
	}

	public void BowlingBallMovement()
	{
		if (pauseTheBall)
		{
			return;
		}
		UpdateBattingTimingMeter();
		BallMovement();
		BallSwingMovement();
		if (CONTROLLER.tutorialToggle == 1 && CONTROLLER.myTeamIndex == CONTROLLER.BattingTeamIndex && isnextball)
		{
			Singleton<GameModel>.instance.ShowTutorial(1);
			isnextball = false;
		}
		if (Singleton<PreviewScreen>.instance.alertPopup.activeInHierarchy)
		{
			Singleton<PreviewScreen>.instance.alertPopup.SetActive(value: false);
		}
		if (ballTransform.position.z > 2.5f && rayCastOn)
		{
			CustomRayCastForBowlingBallMovement();
		}
		if (!hideBowlingInterface && hideBowlingInterfaceSpot.transform.position.z < ballTransform.position.z)
		{
			if (Singleton<GameModel>.instance != null)
			{
				Singleton<GameModel>.instance.ShowBowlingInterface(boolean: false);
			}
			hideBowlingInterface = true;
		}
		if (!wideBallChecked)
		{
			isWideBall();
		}
		if (ballProjectileAngle >= 360f)
		{
			ballNoOfBounce++;
			ballProjectileAngle = 180f;
			ballProjectileAnglePerSecond *= 1.1f;
			ballProjectileHeight *= 0.6f;
			if (!replayMode)
			{
				ballSpinningSpeedInZ = UnityEngine.Random.Range(-3600, -1800);
				savedFirstBounceBallSpinningSpeedInZ = ballSpinningSpeedInZ;
			}
			else
			{
				ballSpinningSpeedInZ = savedFirstBounceBallSpinningSpeedInZ;
			}
			if (ballNoOfBounce == 1)
			{
				if (currentBowlerType == "spin")
				{
					ballAngle += spinValue;
				}
				else if (currentBowlerType == "fast" && swingValue != 0f)
				{
					ballAngle += spinValue;
					swingValue = 0f;
				}
				else if (currentBowlerType == "medium" && swingValue != 0f)
				{
					ballAngle += spinValue;
					swingValue = 0f;
				}
				ActivateWicketKeeper();
				if (Mathf.Abs(ballTransform.position.x) <= 0.1f)
				{
					ballInline = true;
				}
			}
		}
		if (Singleton<DRS>.instance.DRSreplay && savedBallRayCastConnectedZposition != 0f && ballRayCastReferenceGOTransform.position.z > savedBallRayCastConnectedZposition - 10f)
		{
			replayCamera.enabled = false;
			Singleton<DRSCameraScript>.instance.FixAtStump2Crease();
			SetDRSReplayUI();
		}
		if (Singleton<DRS>.instance.DRSreplay && savedBallRayCastConnectedZposition != 0f && ballRayCastReferenceGOTransform.position.z > savedBallRayCastConnectedZposition)
		{
			Singleton<DRSCameraScript>.instance.DisableAllRenderers();
		}
		if (ballNoOfBounce > 0 && drsCount != 1 && bDRSPitchingOutsideLeg)
		{
			Singleton<DRSCameraScript>.instance.MoveCameraToTopOfPitch();
		}
		if (Singleton<DRS>.instance.DRSreplay && drsCount > 2 && !bDRSPitchingOutsideLeg)
		{
			if (hitting)
			{
				Singleton<DRSCameraScript>.instance.HitStump(0f);
			}
			else
			{
				Singleton<DRSCameraScript>.instance.HitStump(0f);
			}
		}
	}

	public void isWideBall()
	{
		if (!(stump1Crease.transform.position.z < ballTransform.position.z))
		{
			return;
		}
		wideBallChecked = true;
		Vector3 vector = Vector3.zero;
		Vector3 vector2 = Vector3.zero;
		if (currentBatsmanHand == "right")
		{
			if (!IsFullTossBall)
			{
				if (batsmanTransform.position.x >= 0f)
				{
					vector2 = RHBMinWideLimit.transform.position;
					vector = getMaxMinPoint();
				}
				else if (batsmanTransform.position.x < 0f)
				{
					vector2 = RHBMinWideLimit.transform.position;
					vector = stump1Crease.transform.position;
					vector += new Vector3(0.18f, 0f, 0f);
				}
			}
			else if (batsmanTransform.position.x >= 0f)
			{
				vector2 = RHBMinWideLimit.transform.position;
				vector = getMaxMinPoint();
			}
			else if (batsmanTransform.position.x < 0f)
			{
				vector2 = RHBMinWideLimit.transform.position;
				vector = stump1Crease.transform.position;
				vector += new Vector3(0.18f, 0f, 0f);
			}
		}
		else if (currentBatsmanHand == "left")
		{
			if (!IsFullTossBall)
			{
				if (batsmanTransform.position.x >= 0f)
				{
					vector2 = stump1Crease.transform.position;
					vector = LHBMaxWideLimit.transform.position;
					vector2 -= new Vector3(0.18f, 0f, 0f);
				}
				else if (batsmanTransform.position.x < 0f)
				{
					vector2 = getMaxMinPoint();
					vector = LHBMaxWideLimit.transform.position;
				}
			}
			else if (batsmanTransform.position.x >= 0f)
			{
				vector2 = stump1Crease.transform.position;
				vector = LHBMaxWideLimit.transform.position;
				vector2 -= new Vector3(0.18f, 0f, 0f);
			}
			else if (batsmanTransform.position.x < 0f)
			{
				vector2 = getMaxMinPoint();
				vector = LHBMaxWideLimit.transform.position;
			}
		}
		if (!(ballTransform.position.x < vector.x) || !(ballTransform.position.x > vector2.x))
		{
			wideBall = true;
		}
		if (CONTROLLER.PlayModeSelected == 6)
		{
			wideBall = false;
		}
	}

	public bool IsWide()
	{
		return wideBall;
	}

	public Vector3 getMaxMinPoint()
	{
		if (currentBatsmanHand == "right")
		{
			if (batsmanLeftShoeBackEdge.transform.position.x >= batsmanRightShoeBackEdge.transform.position.x && batsmanLeftShoeBackEdge.transform.position.x >= batsmanLeftLegEdgePoint.transform.position.x)
			{
				return batsmanLeftShoeBackEdge.transform.position;
			}
			if (batsmanRightShoeBackEdge.transform.position.x >= batsmanLeftShoeBackEdge.transform.position.x && batsmanRightShoeBackEdge.transform.position.x >= batsmanLeftLegEdgePoint.transform.position.x)
			{
				return batsmanRightShoeBackEdge.transform.position;
			}
			if (batsmanLeftLegEdgePoint.transform.position.x >= batsmanLeftShoeBackEdge.transform.position.x && batsmanLeftLegEdgePoint.transform.position.x >= batsmanRightShoeBackEdge.transform.position.x)
			{
				return batsmanLeftLegEdgePoint.transform.position;
			}
		}
		else if (currentBatsmanHand == "left")
		{
			if (batsmanLeftShoeBackEdge.transform.position.x <= batsmanRightShoeBackEdge.transform.position.x && batsmanLeftShoeBackEdge.transform.position.x <= batsmanLeftLegEdgePoint.transform.position.x)
			{
				return batsmanLeftShoeBackEdge.transform.position;
			}
			if (batsmanRightShoeBackEdge.transform.position.x <= batsmanLeftShoeBackEdge.transform.position.x && batsmanRightShoeBackEdge.transform.position.x <= batsmanLeftLegEdgePoint.transform.position.x)
			{
				return batsmanRightShoeBackEdge.transform.position;
			}
			if (batsmanLeftLegEdgePoint.transform.position.x <= batsmanLeftShoeBackEdge.transform.position.x && batsmanLeftLegEdgePoint.transform.position.x <= batsmanRightShoeBackEdge.transform.position.x)
			{
				return batsmanLeftLegEdgePoint.transform.position;
			}
		}
		return Vector3.zero;
	}

	private void BallSwingMovement()
	{
		if (swingValue != 0f)
		{
			float x = Mathf.Cos(swingProjectileAngle * DEG2RAD) * swingValue * Time.deltaTime;
			swingProjectileAngle += swingProjectileAnglePerSecond * Time.deltaTime;
			ballTransform.position -= new Vector3(x, 0f, 0f);
		}
	}

	private void BallSwingMovementCustom()
	{
	}

	private void FindBowlingParameters()
	{
		if (CONTROLLER.PlayModeSelected != 6)
		{
			Vector3 vector = ballOriginGO.transform.InverseTransformPoint(bowlingSpotTransform.position);
			ballAngle = 90f - Mathf.Atan2(vector.x, vector.z) * RAD2DEG;
			ballSpotLength = (ballOriginGO.transform.position - bowlingSpotTransform.position).magnitude;
			horizontalSpeed = 16f + bowlingSpeed / 10f * 2f;
			ballProjectileAnglePerSecond = 90f / ballSpotLength * horizontalSpeed;
			swingProjectileAnglePerSecond = 180f / ballSpotLength * horizontalSpeed;
			if (Singleton<BallSimulationManager>.instance.CanShowBallSimulation())
			{
				Singleton<BallSimulationManager>.instance.SetTempData(ballAngle, horizontalSpeed, ballProjectileHeight, ballSpotLength, swingValue, spinValue);
			}
			if (!IsFullTossBall)
			{
				ballSpotAtCreaseLine.transform.position = new Vector3(bowlingSpotTransform.position.x, ballSpotAtCreaseLine.transform.position.y, ballSpotAtCreaseLine.transform.position.z);
			}
			float num = ballSpotAtCreaseLine.transform.position.x - bowlingSpotTransform.position.x;
			float num2 = ballSpotAtCreaseLine.transform.position.z - bowlingSpotTransform.position.z;
			float num3 = Mathf.Sqrt(num * num + num2 * num2);
			float num4 = Mathf.Atan2(num, num2) * RAD2DEG - (90f - ballAngle - spinValue);
			float num5 = num3 / Mathf.Cos(num4 * DEG2RAD);
			float x = Mathf.Sqrt(num5 * num5 - num3 * num3);
			if (!IsFullTossBall)
			{
				if (num4 < 0f)
				{
					ballSpotAtCreaseLine.transform.position += new Vector3(x, 0f, 0f);
				}
				else
				{
					ballSpotAtCreaseLine.transform.position -= new Vector3(x, 0f, 0f);
				}
			}
			if (!IsFullTossBall)
			{
				ballSpotAtStump.transform.position = new Vector3(bowlingSpotTransform.position.x, ballSpotAtStump.transform.position.y, ballSpotAtStump.transform.position.z);
			}
			float num6 = ballSpotAtStump.transform.position.x - bowlingSpotTransform.position.x;
			float num7 = ballSpotAtStump.transform.position.z - bowlingSpotTransform.position.z;
			float num8 = Mathf.Sqrt(num6 * num6 + num7 * num7);
			float num9 = Mathf.Atan2(num6, num7) * RAD2DEG - (90f - ballAngle - spinValue);
			float num10 = num8 / Mathf.Cos(num9 * DEG2RAD);
			float x2 = Mathf.Sqrt(num10 * num10 - num8 * num8);
			if (!IsFullTossBall)
			{
				if (num9 < 0f)
				{
					ballSpotAtStump.transform.position += new Vector3(x2, 0f, 0f);
				}
				else
				{
					ballSpotAtStump.transform.position -= new Vector3(x2, 0f, 0f);
				}
			}
		}
		else
		{
			Vector3 vector2 = ballOriginGO.transform.InverseTransformPoint(bowlingSpotTransform.position);
			ballAngle = 90f - Mathf.Atan2(vector2.x, vector2.z) * RAD2DEG;
			ballSpotLength = (ballOriginGO.transform.position - bowlingSpotTransform.position).magnitude;
			horizontalSpeed = 16f + bowlingSpeed / 10f * 2f;
			float num11 = Multiplayer.oversData[CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls / 6].bowlingAngle[CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls % 6];
			float num12 = 4f;
			if (!(currentBowlerType == "fast") && currentBowlerType == "spin")
			{
				spinValue = num12 * num11;
				if (Multiplayer.oversData[CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls / 6].bowlerType == "offspin")
				{
					currentBowlerSpinType = 1;
				}
				else
				{
					currentBowlerSpinType = 2;
				}
				if (currentBowlerSpinType == 2)
				{
					spinValue *= -1f;
				}
			}
			ballProjectileAnglePerSecond = 90f / ballSpotLength * horizontalSpeed;
			swingProjectileAnglePerSecond = 180f / ballSpotLength * horizontalSpeed;
			ballSpotAtCreaseLine.transform.position = new Vector3(bowlingSpotTransform.position.x, ballSpotAtCreaseLine.transform.position.y, ballSpotAtCreaseLine.transform.position.z);
			float num13 = ballSpotAtCreaseLine.transform.position.x - bowlingSpotTransform.position.x;
			float num14 = ballSpotAtCreaseLine.transform.position.z - bowlingSpotTransform.position.z;
			float num15 = Mathf.Sqrt(num13 * num13 + num14 * num14);
			float num16 = Mathf.Atan2(num13, num14) * RAD2DEG - (90f - ballAngle - spinValue);
			float num17 = num15 / Mathf.Cos(num16 * DEG2RAD);
			float x3 = Mathf.Sqrt(num17 * num17 - num15 * num15);
			if (num16 < 0f)
			{
				ballSpotAtCreaseLine.transform.position += new Vector3(x3, 0f, 0f);
			}
			else
			{
				ballSpotAtCreaseLine.transform.position -= new Vector3(x3, 0f, 0f);
			}
			ballSpotAtStump.transform.position = new Vector3(bowlingSpotTransform.position.x, ballSpotAtStump.transform.position.y, ballSpotAtStump.transform.position.z);
			float num18 = ballSpotAtStump.transform.position.x - bowlingSpotTransform.position.x;
			float num19 = ballSpotAtStump.transform.position.z - bowlingSpotTransform.position.z;
			float num20 = Mathf.Sqrt(num18 * num18 + num19 * num19);
			float num21 = Mathf.Atan2(num18, num19) * RAD2DEG - (90f - ballAngle - spinValue);
			float num22 = num20 / Mathf.Cos(num21 * DEG2RAD);
			float x4 = Mathf.Sqrt(num22 * num22 - num20 * num20);
			if (num21 < 0f)
			{
				ballSpotAtStump.transform.position += new Vector3(x4, 0f, 0f);
			}
			else
			{
				ballSpotAtStump.transform.position -= new Vector3(x4, 0f, 0f);
			}
		}
		if (ballSpotLength > 17.4f)
		{
			ballSpotHeight = 0.2f;
			ballHeightAtStump = ballSpotHeight;
			return;
		}
		float num23 = ballProjectileAnglePerSecond;
		float num24 = ballProjectileHeight;
		float num25 = 180f / num23 * horizontalSpeed;
		float num26 = creaseLineHypotenuse / num25 * 180f;
		ballSpotHeight = Mathf.Sin(num26 * DEG2RAD) * num24;
		num26 = (creaseLineHypotenuse + 1.2f) / num25 * 180f;
		ballHeightAtStump = Mathf.Sin(num26 * DEG2RAD) * num24;
	}

	private void GetComputerBattingKeyInput()
	{
		int num = 3;
		if (CONTROLLER.difficultyMode == "hard")
		{
			num = 1;
		}
		else if (CONTROLLER.difficultyMode == "medium")
		{
			num = 2;
		}
		else if (CONTROLLER.difficultyMode == "easy")
		{
			num = 3;
		}
		if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls % num == 0 && CONTROLLER.PlayModeSelected != 7)
		{
			AIHitInGap = true;
		}
		else
		{
			AIHitInGap = false;
		}
		if (CONTROLLER.PlayModeSelected == 7)
		{
			if (UnityEngine.Random.Range(0, 10) <= num)
			{
				AIHitInGap = true;
			}
			else
			{
				AIHitInGap = false;
			}
		}
		leftArrowKeyDown = false;
		upArrowKeyDown = false;
		downArrowKeyDown = false;
		rightArrowKeyDown = false;
		powerKeyDown = false;
		if (AIHitInGap)
		{
			CheckCount = 0;
			AiFieldScan();
			ScanForUserFielders();
		}
		int num2 = UnityEngine.Random.Range(0, 100);
		int num3 = 0;
		float x = ballSpotAtCreaseLine.transform.position.x;
		float num4 = 0f;
		float num5 = 5f;
		float num6 = 1f;
		float num7 = 0.5f;
		float num8 = UnityEngine.Random.Range(0, 10);
		float confidenceVal = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].ConfidenceVal;
		if (batsmanConfidenceLevel)
		{
			num6 = ((CONTROLLER.PlayModeSelected == 7) ? 1f : ((CONTROLLER.currentInnings != 1) ? (10f - (float)int.Parse(CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank) / 5f + (float)(50 / CONTROLLER.totalOvers)) : CONTROLLER.ReqRunRate));
			num4 = confidenceVal / 4f + CONTROLLER.ReqRunRate / 5f + (float)UnityEngine.Random.Range(0, 2) + num7 / (float)CONTROLLER.totalOvers;
			num4 -= num4 * ((float)int.Parse(CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank) / 20f);
			if (CONTROLLER.difficultyMode == "hard")
			{
				num4 += 1f;
			}
			if (CONTROLLER.PowerPlay)
			{
				num4 += 0.5f;
			}
			num5 = num6 / ((float)((CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWickets + 1) / 2) + 0.5f) % 3f + 5f;
			if (num4 > num5)
			{
				num4 = num5;
			}
			if (num4 < 0f)
			{
				num4 = 0f;
			}
			if (CONTROLLER.PlayModeSelected != 7 && CONTROLLER.totalOvers * 6 - CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls <= 12)
			{
				num4 = ((!(CONTROLLER.ReqRunRate > 0f) || !(CONTROLLER.ReqRunRate <= 3f)) ? (num4 + 2f / (float)(CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWickets + 1)) : ((float)UnityEngine.Random.Range(1, 2)));
			}
			int num9 = UnityEngine.Random.Range(0, 10);
			float num10 = 100f;
			if (CONTROLLER.oversSelectedIndex < 4)
			{
				num10 = 100f;
			}
			else
			{
				num10 = 100f;
			}
			if ((float)num9 <= num4)
			{
				if (CONTROLLER.PlayModeSelected != 7)
				{
					powerKeyDown = true;
				}
				else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].LoftMeterFillVal >= 100f)
				{
					powerKeyDown = true;
				}
				else
				{
					powerKeyDown = false;
				}
			}
			else
			{
				powerKeyDown = false;
			}
		}
		else
		{
			float num11 = 85f;
			if (CONTROLLER.oversSelectedIndex < 4)
			{
				num11 = 100f;
			}
			else
			{
				num11 = 100f;
			}
			if (num8 < 2f || (num8 < 5f && CONTROLLER.ReqRunRate >= 6f))
			{
				if (CONTROLLER.PlayModeSelected != 7)
				{
					powerKeyDown = true;
				}
				else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].LoftMeterFillVal >= 100f)
				{
					powerKeyDown = true;
				}
				else
				{
					powerKeyDown = false;
				}
			}
			else
			{
				powerKeyDown = false;
			}
		}
		int max = 0;
		if (CONTROLLER.difficultyMode == "easy")
		{
			max = 20;
		}
		else if (CONTROLLER.difficultyMode == "medium")
		{
			max = 15;
		}
		else if (CONTROLLER.difficultyMode == "hard")
		{
			max = 8;
		}
		float num12 = UnityEngine.Random.Range(0, max);
		if ((num12 != 3f && num12 != 7f) || aiFielderScanArray.Count > 6)
		{
		}
		if (lineFreeHit)
		{
			powerKeyDown = true;
		}
		if (currentBatsmanHand == "right")
		{
			if ((double)x < -0.35)
			{
				leftArrowKeyDown = true;
				if (ballSpotLength > 13f)
				{
					downArrowKeyDown = true;
				}
				if (ballSpotLength < 13f && UnityEngine.Random.Range(0, 100) < 40)
				{
					rightArrowKeyDown = true;
					leftArrowKeyDown = false;
					downArrowKeyDown = false;
				}
				if (ballSpotLength > 16.8f && UnityEngine.Random.Range(0, 100) < 5)
				{
					rightArrowKeyDown = false;
					downArrowKeyDown = false;
					leftArrowKeyDown = true;
					upArrowKeyDown = true;
				}
			}
			else if ((double)x >= -0.35 && x < 0f)
			{
				downArrowKeyDown = true;
				if ((double)x >= -0.12)
				{
					rightArrowKeyDown = true;
					if (ballSpotLength > 14.5f)
					{
						downArrowKeyDown = false;
					}
				}
				if (ballSpotLength < 13f && UnityEngine.Random.Range(0, 100) > 80)
				{
					rightArrowKeyDown = true;
					downArrowKeyDown = false;
				}
			}
			else if (x >= 0f)
			{
				rightArrowKeyDown = true;
				if ((double)x >= 0.3)
				{
					if (currentBowlerType == "fast")
					{
						upArrowKeyDown = true;
					}
					else if (UnityEngine.Random.Range(0, 50) > 45)
					{
						upArrowKeyDown = true;
					}
				}
				if (x < 0.05f && ballSpotLength > 16f)
				{
					int num13 = UnityEngine.Random.Range(0, 100);
					int num14 = 5;
					if (CONTROLLER.PlayModeSelected == 7)
					{
						num14 = 50;
					}
					if (num13 < num14)
					{
						leftArrowKeyDown = false;
						upArrowKeyDown = false;
						downArrowKeyDown = false;
						rightArrowKeyDown = false;
					}
					else if (num13 < 55)
					{
						rightArrowKeyDown = true;
					}
					else
					{
						rightArrowKeyDown = true;
						downArrowKeyDown = true;
					}
				}
			}
		}
		else if (currentBatsmanHand == "left")
		{
			if (x > 0.35f)
			{
				leftArrowKeyDown = true;
				if (ballSpotLength > 13f)
				{
					downArrowKeyDown = true;
				}
				if (ballSpotLength < 13f && UnityEngine.Random.Range(0, 100) < 40)
				{
					rightArrowKeyDown = true;
					leftArrowKeyDown = false;
					downArrowKeyDown = false;
				}
				if (ballSpotLength > 16.8f && UnityEngine.Random.Range(0, 100) < 5)
				{
					rightArrowKeyDown = false;
					downArrowKeyDown = false;
					leftArrowKeyDown = true;
					upArrowKeyDown = true;
				}
			}
			else if (x <= 0.35f && x > -0.15f)
			{
				downArrowKeyDown = true;
				if (x <= 0.2f && x >= 0f)
				{
					rightArrowKeyDown = true;
					if (ballSpotLength < 13f && ballSpotLength > 14.5f)
					{
						downArrowKeyDown = false;
					}
				}
				if (x > 0f && ballSpotLength < 13f && UnityEngine.Random.Range(0, 100) > 90)
				{
					rightArrowKeyDown = true;
					downArrowKeyDown = false;
				}
			}
			else if (x <= -0.15f)
			{
				rightArrowKeyDown = true;
				if (x <= -0.2f)
				{
					if (currentBowlerType == "fast")
					{
						upArrowKeyDown = true;
					}
					else if (UnityEngine.Random.Range(0, 50) > 45)
					{
						upArrowKeyDown = true;
					}
				}
				if (x >= -0.1f && ballSpotLength > 16f)
				{
					leftArrowKeyDown = false;
					upArrowKeyDown = false;
					downArrowKeyDown = false;
					rightArrowKeyDown = false;
				}
			}
		}
		num3 = ((!(CONTROLLER.difficultyMode == "hard")) ? 5 : 6);
		int num15 = UnityEngine.Random.Range(0, 10);
		if (AIHitInGap)
		{
			DetermineAIShot();
		}
		float num16 = 0f;
		if (currentBatsmanHand == "right")
		{
			num16 = batsmanTransform.position.x - batsmanInitXPos - ballSpotAtCreaseLine.transform.position.x;
		}
		else if (currentBatsmanHand == "left")
		{
			num16 = batsmanInitXPos - batsmanTransform.position.x + ballSpotAtCreaseLine.transform.position.x;
		}
		if (UnityEngine.Random.Range(0, 10) < num3 && !AIHitInGap && CONTROLLER.PlayModeSelected != 7)
		{
			if (leftArrowKeyDown)
			{
				if (downArrowKeyDown)
				{
					int num17 = UnityEngine.Random.Range(0, 10);
					if (num17 < 3)
					{
						downArrowKeyDown = false;
						upArrowKeyDown = true;
					}
					else if (num17 < 8)
					{
						leftArrowKeyDown = false;
						rightArrowKeyDown = true;
					}
					else if (num16 > 0.35f)
					{
						downArrowKeyDown = false;
						leftArrowKeyDown = false;
						upArrowKeyDown = true;
						rightArrowKeyDown = true;
					}
				}
				else
				{
					int num17 = UnityEngine.Random.Range(0, 10);
					if (num17 < 3)
					{
						upArrowKeyDown = true;
					}
					else if (num17 < 6)
					{
						leftArrowKeyDown = false;
						downArrowKeyDown = true;
					}
					else if (num17 < 7 && num16 > 0.35f)
					{
						downArrowKeyDown = false;
						leftArrowKeyDown = false;
						upArrowKeyDown = true;
						rightArrowKeyDown = true;
					}
				}
			}
			if (downArrowKeyDown)
			{
				int num17 = UnityEngine.Random.Range(0, 10);
				if (num17 < 4)
				{
					rightArrowKeyDown = true;
				}
				else if (num17 < 8)
				{
					downArrowKeyDown = false;
					rightArrowKeyDown = true;
					upArrowKeyDown = true;
				}
				else
				{
					leftArrowKeyDown = true;
				}
			}
			if (rightArrowKeyDown)
			{
				if (downArrowKeyDown)
				{
					int num17 = UnityEngine.Random.Range(0, 10);
					if (num17 < 2)
					{
						downArrowKeyDown = true;
						rightArrowKeyDown = false;
					}
					else if (num17 < 5 && num16 > 0.35f)
					{
						downArrowKeyDown = false;
						leftArrowKeyDown = true;
						upArrowKeyDown = true;
						rightArrowKeyDown = false;
					}
				}
				else if (upArrowKeyDown)
				{
					int num17 = UnityEngine.Random.Range(0, 10);
					if (num17 < 4)
					{
						upArrowKeyDown = false;
						downArrowKeyDown = true;
					}
					else if (num17 < 6)
					{
						upArrowKeyDown = false;
					}
				}
				else
				{
					int num17 = UnityEngine.Random.Range(0, 10);
					if (num17 < 2)
					{
						upArrowKeyDown = true;
					}
					else if (num17 < 4)
					{
						leftArrowKeyDown = true;
						upArrowKeyDown = true;
					}
				}
			}
		}
		CONTROLLER.prevPowerShot = powerKeyDown;
	}

	private void BattingBallMovement()
	{
		if (pauseTheBall || !(ballStatus != "throw") || !(ballStatus != string.Empty))
		{
			return;
		}
		BallMovement();
		if (ballProjectileAngle >= 360f)
		{
			ballNoOfBounce++;
			ballProjectileAngle = 180f;
			if (ballNoOfBounce == 1 && DistanceBetweenTwoVector2(ball, groundCenterPoint) < groundRadius)
			{
				canBe4or6 = 4;
			}
			if (!edgeCatch)
			{
				if (!applyBallFiction)
				{
					ballProjectileAnglePerSecond *= 1.1f;
					nextPitchDistance += 180f / ballProjectileAnglePerSecond * horizontalSpeed;
				}
				else if (applyBallFiction)
				{
					ballProjectileAnglePerSecond *= 2f;
				}
				if (ballBoundaryReflection && ballOnboundaryLine)
				{
					ballProjectileHeight = boundaryHeight / 2f;
					boundaryHeight = ballProjectileHeight;
				}
				else
				{
					ballProjectileHeight *= 0.2f;
				}
				if (ballProjectileHeight > 2.5f)
				{
					ballProjectileHeight = 2.2f;
				}
			}
		}
		if (applyBallFiction && !edgeCatch)
		{
			horizontalSpeed *= (100f - 90f * Time.deltaTime) / 100f;
		}
		if (!powerShot && !edgeCatch)
		{
			if (!setfrictionRation && !replayMode)
			{
				setfrictionRation = true;
				speedReduceFactor = setRandomFriction();
			}
			horizontalSpeed *= (100f - speedReduceFactor * Time.deltaTime) / 100f;
			if ((double)horizontalSpeed <= 0.4)
			{
				horizontalSpeed = 0.4f;
			}
		}
		else if (!edgeCatch)
		{
			speedReduceFactor = 20f;
		}
		if (ballNoOfBounce > 3)
		{
			ballProjectileAnglePerSecond = 0f;
			if (horizontalSpeed < 0.4f)
			{
				horizontalSpeed = 0f;
			}
		}
		int num = Mathf.FloorToInt(DistanceBetweenTwoVector2(groundCenterPoint, ball));
		int num2 = Mathf.FloorToInt(DistanceBetweenTwoVector2(fielder10FocusGObjToCollectTheBall, groundCenterPoint));
		if ((float)num > 30f && ballTimingFirstBounceDistance > 70f)
		{
			Singleton<UILookAt>.instance.show(flag: true);
			sixDistanceCamera.enabled = true;
			if ((float)num >= savedSixDistance)
			{
				Singleton<UILookAt>.instance.stripText.text = string.Empty + num + " " + LocalizationData.instance.getText(538);
				Singleton<UILookAt>.instance.PositionSixDistanceProfile(ball, currentBatsmanHand);
				savedSixDistance = num;
			}
		}
	}

	private float setRandomFriction()
	{
		List<float> list = new List<float>(new float[1] { 10f });
		int count = list.Count;
		int index = UnityEngine.Random.Range(0, count);
		return list[index];
	}

	public void UpdateBallShadow()
	{
		if (BallSkinRendererComponent.enabled)
		{
			ShadowsArray[15].active = true;
			ShadowsArrayTransform[15].position = new Vector3(ShadowRefArrayTransform[15].position.x, 0f, ShadowRefArrayTransform[15].position.z);
		}
		else if (showShadows)
		{
			ShadowsArray[15].active = false;
		}
	}

	public void GetUserBattingKeyboardInput()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			upArrowKeyDown = true;
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			downArrowKeyDown = true;
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			if (currentBatsmanHand == "right")
			{
				leftArrowKeyDown = true;
			}
			else
			{
				rightArrowKeyDown = true;
			}
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			if (currentBatsmanHand == "right")
			{
				rightArrowKeyDown = true;
			}
			else
			{
				leftArrowKeyDown = true;
			}
		}
		if (Input.GetKeyDown(KeyCode.C))
		{
			if (nonStrikerStatus == "run")
			{
				if (currentBallNoOfRuns % 2 == 0)
				{
					if (batsmanTransform.position.z > 8.8f || batsmanTransform.position.z < 0f)
					{
						return;
					}
				}
				else if (batsmanTransform.position.z < -8.8f || batsmanTransform.position.z > 0f)
				{
					return;
				}
			}
			if (nonStrikerStatus == "run" && !CancelRun && !ballOnboundaryLine)
			{
				CancelRun = true;
				cancelRunningBetweenWicket();
			}
		}
		if (Input.GetKeyDown(KeyCode.A))
		{
			powerKeyDown = true;
		}
		if (Input.GetKeyDown(KeyCode.D))
		{
			if (DistanceBetweenTwoGameObjects(ball, throwToGO) < 1.5f && isFielderthrown)
			{
				disableRunCancelBtn();
				return;
			}
			if (!CancelRun && !edgeCatch && ballHitTheBall)
			{
				takeRun = true;
			}
		}
		if (Input.GetKeyUp(KeyCode.UpArrow))
		{
			upArrowKeyDown = false;
		}
		if (Input.GetKeyUp(KeyCode.DownArrow))
		{
			downArrowKeyDown = false;
		}
		if (Input.GetKeyUp(KeyCode.LeftArrow))
		{
			if (currentBatsmanHand == "right")
			{
				leftArrowKeyDown = false;
			}
			else
			{
				rightArrowKeyDown = false;
			}
		}
		if (Input.GetKeyUp(KeyCode.RightArrow))
		{
			if (currentBatsmanHand == "right")
			{
				rightArrowKeyDown = false;
			}
			else
			{
				leftArrowKeyDown = false;
			}
		}
		if (Input.GetKeyUp(KeyCode.A))
		{
			powerKeyDown = false;
		}
		if (Input.GetKeyUp(KeyCode.D))
		{
			takeRun = false;
		}
		if (Input.GetMouseButton(0))
		{
			batsmanStep = 1f;
		}
		if (Input.GetMouseButtonUp(0))
		{
			batsmanStep = 0.3f;
		}
	}

	private void GetUserBattingInput()
	{
		if (!replayMode)
		{
			GetUserBattingKeyboardInput();
		}
		if (batsmanCanMoveLeftRight)
		{
			if (!replayMode)
			{
				if (leftArrowKeyDown)
				{
					if (!savedLeftArrowDownTime)
					{
						savedLeftArrowKeyDownArray.Add(Time.time - bowlerRunupStartTime);
						savedLeftArrowDownTime = true;
					}
				}
				else if (!leftArrowKeyDown && savedLeftArrowDownTime)
				{
					savedLeftArrowKeyUpArray.Add(Time.time - bowlerRunupStartTime);
					savedLeftArrowDownTime = false;
				}
				if (rightArrowKeyDown)
				{
					if (!savedRightArrowDownTime)
					{
						savedRightArrowKeyDownArray.Add(Time.time - bowlerRunupStartTime);
						savedRightArrowDownTime = true;
					}
				}
				else if (!rightArrowKeyDown && savedRightArrowDownTime)
				{
					savedRightArrowKeyUpArray.Add(Time.time - bowlerRunupStartTime);
					savedRightArrowDownTime = false;
				}
			}
			if (replayMode)
			{
				float num = Time.time - bowlerRunupStartTime;
				bool flag = false;
				for (int i = 0; i < savedLeftArrowKeyDownArray.Count; i++)
				{
					float num2 = savedLeftArrowKeyDownArray[i];
					float num3 = savedLeftArrowKeyUpArray[i];
					if (num >= num2 && num <= num3)
					{
						flag = true;
					}
				}
				leftArrowKeyDown = flag;
				bool flag2 = false;
				for (int j = 0; j < savedRightArrowKeyDownArray.Count; j++)
				{
					float num4 = savedRightArrowKeyDownArray[j];
					float num5 = savedRightArrowKeyUpArray[j];
					if (num >= num4 && num <= num5)
					{
						flag2 = true;
					}
				}
				rightArrowKeyDown = flag2;
			}
			if (leftArrowKeyDown)
			{
				if (currentBatsmanHand == "right" && RHBatsmanForwardLimit.transform.position.x < batsmanTransform.position.x)
				{
					batsmanTransform.position -= new Vector3(batsmanStep * Time.deltaTime, 0f, 0f);
				}
				else if (currentBatsmanHand == "left" && LHBatsmanForwardLimit.transform.position.x > batsmanTransform.position.x)
				{
					batsmanTransform.position += new Vector3(batsmanStep * Time.deltaTime, 0f, 0f);
				}
				if (!batsmanOnLeftRightMovement)
				{
					batsmanAnimationComponent.CrossFade("bt6Forward");
					if (batsmanStep == 1f)
					{
						batsmanAnimationComponent["bt6Forward"].speed = 1f;
					}
					else
					{
						batsmanAnimationComponent["bt6Forward"].speed = 0.5f;
					}
					batsmanOnLeftRightMovement = true;
				}
			}
			else if (rightArrowKeyDown)
			{
				if (currentBatsmanHand == "right" && RHBatsmanBackwardLimit.transform.position.x > batsmanTransform.position.x)
				{
					batsmanTransform.position += new Vector3(batsmanStep * Time.deltaTime, 0f, 0f);
				}
				else if (currentBatsmanHand == "left" && LHBatsmanBackwardLimit.transform.position.x < batsmanTransform.position.x)
				{
					batsmanTransform.position -= new Vector3(batsmanStep * Time.deltaTime, 0f, 0f);
				}
				if (!batsmanOnLeftRightMovement)
				{
					batsmanAnimationComponent.CrossFade("bt6Backward");
					if (batsmanStep == 1f)
					{
						batsmanAnimationComponent["bt6Backward"].speed = 1f;
					}
					else
					{
						batsmanAnimationComponent["bt6Backward"].speed = 0.5f;
					}
					batsmanOnLeftRightMovement = true;
				}
			}
			else if (batsmanOnLeftRightMovement)
			{
				batsmanOnLeftRightMovement = false;
				batsmanAnimationComponent.CrossFade("WCCLite_BatsmanTapLoop");
			}
		}
		else if (!batsmanCanMoveLeftRight && batsmanOnLeftRightMovement)
		{
			batsmanOnLeftRightMovement = false;
			batsmanAnimationComponent.CrossFade("WCCLite_BatsmanTapLoop");
		}
	}

	private void GetBattingInput()
	{
		if (battingBy == "user")
		{
			GetUserBattingInput();
			if (action == 3 && ballTransform.position.z > 2.5f && !touchDeviceShotInput && !replayMode)
			{
				Singleton<GameModel>.instance.GetShotSelected();
			}
		}
		if (Input.GetMouseButtonDown(0))
		{
			mouseDownD = true;
		}
		if (Input.GetMouseButtonUp(0))
		{
			mouseDownD = false;
		}
		if (ballTransform.position.z > shotActivationMaxLimit.transform.position.z && action == 3)
		{
			HideBowlingSpot();
			ShowFullTossSpot(_Value: false);
			if ((!batsmanTriggeredShot && !replayMode) || (!batsmanTriggeredShot && replayMode && savedShotPlayed == "bt6Leave"))
			{
				batsmanTriggeredShot = true;
				batsmanAnimation = "bt6Leave";
				shotPlayed = "bt6Leave";
				CanShowCountDown = false;
			}
		}
		if (replayMode)
		{
			if (canMakeShot && !batsmanTriggeredShot && ballTransform.position.z >= savedShotExecutedZPosition)
			{
				batsmanTriggeredShot = true;
			}
		}
		else
		{
			if ((((!Input.GetKeyDown(KeyCode.S) && !touchDeviceShotInput) || !(battingBy == "user")) && !(battingBy == "computer")) || !canMakeShot || batsmanTriggeredShot)
			{
				return;
			}
			batsmanTriggeredShot = true;
			powerShot = powerKeyDown;
			if (!powerShot && battingBy == "user")
			{
				powerShot = Singleton<BattingControls>.instance.GetPowerIconStatus();
			}
			float num = 0f;
			if (currentBatsmanHand == "right")
			{
				num = batsmanTransform.position.x - batsmanInitXPos - ballSpotAtCreaseLine.transform.position.x;
			}
			else if (currentBatsmanHand == "left")
			{
				num = batsmanInitXPos - batsmanTransform.position.x + ballSpotAtCreaseLine.transform.position.x;
			}
			if (leftArrowKeyDown && upArrowKeyDown)
			{
				if ((double)ballSpotLength < 13.5)
				{
					batsmanAnimation = "bt6LateCut";
					shotPlayed = "bt6LateCut";
				}
				else if (num >= 0.5f)
				{
					batsmanAnimation = "lateCutLowHeight";
					shotPlayed = "lateCutLowHeight";
				}
				else if (currentBowlerType == "spin" || currentBowlerType == "medium")
				{
					batsmanAnimation = "reverseSweepSlowBall";
					shotPlayed = "reverseSweepSlowBall";
				}
				else if (num <= 0.05f)
				{
					batsmanAnimation = "bt6ReverseSweep";
					shotPlayed = "bt6ReverseSweep";
				}
				else
				{
					batsmanAnimation = "reverseSweepSlowBall";
					shotPlayed = "reverseSweepSlowBall";
				}
			}
			else if (leftArrowKeyDown && downArrowKeyDown)
			{
				if (powerShot)
				{
					if (num >= 0.65f)
					{
						batsmanAnimation = "WCCLite_LoftedSquareDrive";
						shotPlayed = "WCCLite_LoftedSquareDrive";
					}
					else if (num < 0.4f)
					{
						batsmanAnimation = "loftOffSide";
						shotPlayed = "loftOffSide";
					}
					else
					{
						batsmanAnimation = "WCCLite_HarbhajanShot";
						shotPlayed = "WCCLite_HarbhajanShot";
					}
				}
				else
				{
					if (num <= 0.1f && (double)ballSpotLength < 13.5)
					{
						batsmanAnimation = "extraCoverDrive";
						shotPlayed = "extraCoverDrive";
					}
					else if (num >= 0.3f && ballSpotLength > 13f)
					{
						batsmanAnimation = "backFootOffDrive";
						shotPlayed = "backFootOffDrive";
					}
					else if (ballSpotLength < 13f && num >= 0.3f)
					{
						batsmanAnimation = "bt6OffDrive";
						shotPlayed = "bt6OffDrive";
					}
					if (num >= 0.5f && ballSpotLength < 13f)
					{
						batsmanAnimation = "WCCLite_BackFootPunch";
						shotPlayed = "WCCLite_BackFootPunch";
					}
					else if (num <= 0.3f)
					{
						batsmanAnimation = "bt6CoverDrive";
						shotPlayed = "bt6CoverDrive";
					}
					else
					{
						batsmanAnimation = "insideOutCoverDrive";
						shotPlayed = "insideOutCoverDrive";
					}
				}
			}
			else if (rightArrowKeyDown && upArrowKeyDown)
			{
				if (currentBowlerType == "fast" && ballSpotLength <= 15f && powerShot)
				{
					batsmanAnimation = "WCCLite_Dilscoop";
					shotPlayed = "WCCLite_Dilscoop";
				}
				else if (num >= 0.5f && ballSpotLength <= 15f)
				{
					batsmanAnimation = "WCCLite_ABdeVilliers_Shot";
					shotPlayed = "WCCLite_ABdeVilliers_Shot";
				}
				else if (ballSpotLength >= 14.5f && num < 0.15f)
				{
					batsmanAnimation = "WCCLite_YorkerLegGlanceNew";
					shotPlayed = "WCCLite_YorkerLegGlanceNew";
				}
				else if (ballSpotLength <= 15f && !powerShot && num < 0.1f)
				{
					batsmanAnimation = "WCCLite_LegGlanceNew";
					shotPlayed = "WCCLite_LegGlanceNew";
				}
				else if (ballSpotLength < 15f)
				{
					if (currentBowlerType == "medium")
					{
						batsmanAnimation = "bt6LegGlance";
						shotPlayed = "bt6LegGlance";
					}
					else
					{
						batsmanAnimation = "WCCLite_LegGlanceNew";
						shotPlayed = "WCCLite_LegGlanceNew";
					}
				}
				else if (currentBowlerType == "spin")
				{
					if (powerShot)
					{
						batsmanAnimation = "legGlanceYorkerLength_new";
						shotPlayed = "legGlanceYorkerLength";
					}
					else if (ballSpotLength > 13.85f && num < 0.19f)
					{
						batsmanAnimation = "bt6Sweep";
						shotPlayed = "bt6Sweep";
					}
					else if (ballSpotLength > 14.5f && num < 0f)
					{
						batsmanAnimation = "WCCLite_YorkerLegGlanceNew";
						shotPlayed = "WCCLite_YorkerLegGlanceNew";
					}
					else
					{
						batsmanAnimation = "paddleSweep";
						shotPlayed = "paddleSweep";
					}
				}
				else if (powerShot)
				{
					batsmanAnimation = "legGlanceYorkerLength_new";
					shotPlayed = "legGlanceYorkerLength";
				}
				else if (ballSpotLength > 13.85f && num < 0.19f && currentBowlerType == "medium")
				{
					batsmanAnimation = "bt6Sweep";
					shotPlayed = "bt6Sweep";
				}
				else
				{
					batsmanAnimation = "WCCLite_YorkerLegGlanceNew";
					shotPlayed = "WCCLite_YorkerLegGlanceNew";
				}
			}
			else if (rightArrowKeyDown && downArrowKeyDown)
			{
				if (powerShot)
				{
					if (ballSpotLength <= 13.5f && num > 0.2f)
					{
						batsmanAnimation = "bt6HookShot";
						shotPlayed = "bt6HookShot";
					}
					if (ballSpotLength <= 12.8f && num > 0.15f)
					{
						batsmanAnimation = "bt6PullShot";
						shotPlayed = "bt6PullShot";
					}
					else if (ballSpotLength >= 15.8f && num < 0.15f)
					{
						batsmanAnimation = "WCCLite_HelicopterShot";
						shotPlayed = "WCCLite_HelicopterShot";
					}
					if (ballSpotLength <= 13f && num > 0.55f)
					{
						batsmanAnimation = "lowPullShot";
						shotPlayed = "lowPullShot";
					}
					else if (num >= 0.6f && ballSpotLength > 12f)
					{
						batsmanAnimation = "powerfulSweepShot";
						shotPlayed = "powerfulSweepShot";
					}
					else if (num <= 0.15f)
					{
						batsmanAnimation = "loftLegSide";
						shotPlayed = "loftLegSide";
					}
					else if (num > 0.15f)
					{
						batsmanAnimation = "loftStraight";
						shotPlayed = "loftStraight";
					}
					else
					{
						batsmanAnimation = "WCCLite_HelicopterShot";
						shotPlayed = "WCCLite_HelicopterShot";
					}
				}
				else if (ballSpotLength <= 13f && num > 0.55f)
				{
					batsmanAnimation = "lowPullShot";
					shotPlayed = "lowPullShot";
				}
				else if (ballSpotLength <= 13f && num > -0.5f && num < -0.1f)
				{
					batsmanAnimation = "runDownOnDrive";
					shotPlayed = "runDownOnDrive";
				}
				else if (ballSpotLength <= 13f && num < 0.1f)
				{
					batsmanAnimation = "backFootOnDrive_new";
					shotPlayed = "backFootOnDrive";
				}
				else
				{
					batsmanAnimation = "bt6OnDrive";
					shotPlayed = "bt6OnDrive";
				}
			}
			else if (downArrowKeyDown)
			{
				if (powerShot)
				{
					if (num <= 0.1f)
					{
						batsmanAnimation = "loftStraight";
						shotPlayed = "loftStraight";
					}
					else if (ballSpotLength <= 14f)
					{
						batsmanAnimation = "loftStraightShortBall";
						shotPlayed = "loftStraightShortBall";
					}
					else
					{
						batsmanAnimation = "straightDrivePowerShot";
						shotPlayed = "straightDrivePowerShot";
					}
				}
				else if (ballSpotLength <= 13.5f && num >= 0.3f)
				{
					batsmanAnimation = "backFootStraightDrive_new";
					shotPlayed = "backFootStraightDrive";
				}
				else
				{
					batsmanAnimation = "bt6StraightDrive";
					shotPlayed = "bt6StraightDrive";
				}
			}
			else if (rightArrowKeyDown)
			{
				squareLegGlance = true;
				if (powerShot && ballSpotLength > 14f && currentBowlerType == "spin" && num < 0f)
				{
					batsmanAnimation = "lowHookShot";
					shotPlayed = "lowHookShot";
				}
				else if (ballSpotLength < 15f && num < 0.1f && powerShot)
				{
					batsmanAnimation = "bt6LegGlance";
					shotPlayed = "bt6LegGlance";
				}
				else if (ballSpotLength < 15f && num >= 0.35f)
				{
					batsmanAnimation = "paddleSweep";
					shotPlayed = "paddleSweep";
				}
				else if (ballSpotLength >= 15f && num < 0.15f)
				{
					batsmanAnimation = "WCCLite_YorkerLegGlanceNew";
					shotPlayed = "WCCLite_YorkerLegGlanceNew";
				}
				else if (ballSpotLength < 13f)
				{
					batsmanAnimation = "lowPullShot";
					shotPlayed = "lowPullShot";
				}
				else
				{
					squareLegGlance = true;
					if (ballSpotLength <= 15f && !powerShot && num < 0.1f)
					{
						batsmanAnimation = "WCCLite_LegGlanceNew";
						shotPlayed = "WCCLite_LegGlanceNew";
					}
					else
					{
						batsmanAnimation = "legGlanceYorkerLength_new";
						shotPlayed = "legGlanceYorkerLength";
					}
				}
			}
			else if (leftArrowKeyDown)
			{
				squareCutDrive = true;
				if (num <= 0.3f && !powerShot)
				{
					if (ballSpotLength < 14f)
					{
						batsmanAnimation = "lateSquareDrive_new";
						shotPlayed = "lateSquareDrive";
					}
					else
					{
						batsmanAnimation = "frontFootOffDrive";
						shotPlayed = "frontFootOffDrive";
					}
				}
				else if (ballSpotLength < 13f && num >= 0.3f && !powerShot)
				{
					batsmanAnimation = "bt6OffDrive";
					shotPlayed = "bt6OffDrive";
				}
				else if (ballSpotLength < 13f)
				{
					batsmanAnimation = "bt6SquareCut";
					shotPlayed = "bt6SquareCut";
				}
				else if (ballSpotLength < 15f && num <= 0.8f)
				{
					batsmanAnimation = "lateSquareDrive_new";
					shotPlayed = "lateSquareDrive";
				}
				else
				{
					batsmanAnimation = "frontFootOffDrive";
					shotPlayed = "frontFootOffDrive";
				}
			}
			else if (ballSpotLength < 15f)
			{
				batsmanAnimation = "backFootDefenseHighBall";
				shotPlayed = "backFootDefenseHighBall";
			}
			else if (Mathf.Abs(ballSpotAtCreaseLine.transform.position.x) > 0.6f)
			{
				batsmanAnimation = "frontFootOffSideDefense";
				shotPlayed = "frontFootOffSideDefense";
			}
			else
			{
				batsmanAnimation = "bt6Defense";
				shotPlayed = "bt6Defense";
			}
			if (CONTROLLER.BattingTeamIndex == CONTROLLER.opponentTeamIndex && num < -0.8f)
			{
				batsmanAnimation = "bt6Leave";
				shotPlayed = "bt6Leave";
			}
			if (hardcoded)
			{
				batsmanAnimation = "bt6StraightDrive";
				shotPlayed = "bt6StraightDrive";
			}
			if (shotPlayed != "bt6Leave")
			{
				float num2 = ShotVariables.optimalShotTable[shotPlayed + "OptimalShotLength"];
				float num3 = ShotVariables.optimalShotTable[shotPlayed + "OptimalShotFrame"];
				optimalShotTime = num2 / horizontalSpeed;
				batReachingTimeForOptimalShotLength = num3 * animationFPSDivide;
				optimalShotActivationTime = optimalShotTime - batReachingTimeForOptimalShotLength - 0.1f;
			}
		}
	}

	public void EnableHardCode()
	{
		hardcoded = true;
	}

	private void ExecuteTheShot()
	{
		if (!batsmanTriggeredShot)
		{
			return;
		}
		if (Time.timeScale == 0.4f)
		{
			Time.timeScale = 1f;
		}
		if (!(Time.time > ballReleasedTime + optimalShotActivationTime) || batsmanMadeShot)
		{
			return;
		}
		if (!replayMode)
		{
			savedShotExecutedZPosition = ballTransform.position.z;
			savedBatsmanShotExecutedPosition = batsmanTransform.position;
			savedShotPlayed = shotPlayed;
			savedPowerShotStatus = powerShot;
		}
		else if (replayMode)
		{
			batsmanTransform.position = savedBatsmanShotExecutedPosition;
		}
		batsmanMadeShot = true;
		if (shotPlayed == "bt6Leave")
		{
			batsmanAnimationComponent[batsmanAnimation].speed = 2f;
			if (Singleton<GameModel>.instance != null)
			{
				Singleton<GameModel>.instance.EnableShot(boolean: false);
			}
		}
		if (shotPlayed != "bt6Leave" && shotPlayed != string.Empty)
		{
			float num = ShotVariables.optimalShotTable[shotPlayed + "OptimalShotLength"];
			float num2 = ShotVariables.optimalShotTable[shotPlayed + "OptimalShotFrame"];
			float num3 = num - DistanceBetweenTwoVector2(ballOriginGO, ball);
			float num4 = num3 / horizontalSpeed;
			float num5 = num2 * animationFPSDivide;
			wantedAnimationSpeed = num5 / num4;
			if (wantedAnimationSpeed > 3f)
			{
				wantedAnimationSpeed = 3f;
			}
		}
		if (CONTROLLER.BattingTeamIndex == CONTROLLER.opponentTeamIndex && ballSpotAtCreaseLine.transform.position.x > -0.1f && ballSpotAtCreaseLine.transform.position.x < 0.1f)
		{
			float num6 = 0.35f;
			if (CONTROLLER.PlayModeSelected == 7)
			{
				num6 = 0.05f;
			}
			if (UnityEngine.Random.value <= num6 && Singleton<GameModel>.instance.CounterDot >= UnityEngine.Random.Range(CONTROLLER.totalOvers, 60))
			{
				batCollider.SetActive(value: false);
				batCollider2.SetActive(value: false);
			}
		}
		if (shotPlayed == "bt6Defense" || shotPlayed == "backFootDefenseHighBall" || shotPlayed == "frontFootOffSideDefense")
		{
			batCollider.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
			batCollider2.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
		}
		if (CONTROLLER.opponentTeamIndex == CONTROLLER.BattingTeamIndex)
		{
			UpdateBattingTimingMeter();
			updateBattingTimingMeterNeedle = false;
		}
		batsmanAnimationComponent.CrossFade(batsmanAnimation, 0.02f);
		batsmanAnimationComponent[batsmanAnimation].speed = wantedAnimationSpeed;
		batsmanAnimationComponent.PlayQueued("WCCLite_BatsmanIdle", QueueMode.CompleteOthers);
		if (ballStatus != "bowled")
		{
			for (int i = 0; i < getSlipArray.Count; i++)
			{
				if (getSlipArray[i] != null)
				{
					GameObject gameObject = getSlipArray[i];
					gameObject.GetComponent<Animation>().Play("backToIdle");
				}
			}
		}
		shotExecuted = true;
		shotExecutionTime = Time.time;
		computerBatsmanNewRunAttempt = true;
		if (Singleton<GameModel>.instance != null)
		{
			Singleton<GameModel>.instance.EnableShot(boolean: false);
		}
	}

	public void LookForRunByComputerBatsman()
	{
		if (!(battingBy != "computer") && canTakeRun)
		{
			int num = ((CONTROLLER.PlayModeSelected == 7) ? 50 : 45);
			if (ballPickedByFielder && DistanceBetweenTwoVector2(ball, throwToGO) < (float)num)
			{
				takeRun = false;
				aiCancelRun = true;
			}
			else if (canTakeRun && !edgeCatch && !aiCancelRun && Time.time > shotExecutionTime + 1f && !ballPickedByFielder && shortestBallPickupDistance > 35f)
			{
				takeRun = true;
				computerBatsmanNewRunAttempt = false;
			}
		}
	}

	public void BallAngle(float angle)
	{
		shotAngle = angle;
	}

	public void BallTiming()
	{
		float num = ballAngle;
		int num2 = ((CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex) ? 1 : 0);
		animval = ballTransform.position.z;
		bool flag = false;
		if (shotPlayed == "lowHookShot")
		{
			ballAngle = 20f + UnityEngine.Random.Range(-20f * (1f - controlFactor * (float)num2), 20f * (1f - controlFactor * (float)num2));
		}
		if (shotPlayed == "bt6HookShot")
		{
			ballAngle = 330f + UnityEngine.Random.Range(-20f * (1f - controlFactor * (float)num2), 20f * (1f - controlFactor * (float)num2));
		}
		else if (shotPlayed == "bt6LegGlance" || shotPlayed == "legGlanceYorkerLength" || shotPlayed == "sweep" || shotPlayed == "paddleSweep")
		{
			if ((shotPlayed == "bt6LegGlance" || shotPlayed == "legGlanceYorkerLength") && squareLegGlance)
			{
				ballAngle = 15f + UnityEngine.Random.Range(-15f * (1f - controlFactor * (float)num2), 15f * (1f - controlFactor * (float)num2));
			}
			else if (shotPlayed != "bt6LegGlance")
			{
				ballAngle = 55f + UnityEngine.Random.Range(-15f * (1f - controlFactor * (float)num2), 15f * (1f - controlFactor * (float)num2));
			}
			else
			{
				ballAngle = 30f + UnityEngine.Random.Range(-15f * (1f - controlFactor * (float)num2), 15f * (1f - controlFactor * (float)num2));
			}
		}
		else if (shotPlayed == "WCCLite_YorkerLegGlanceNew")
		{
			if (squareLegGlance)
			{
				ballAngle = 19f + UnityEngine.Random.Range(-15f * (1f - controlFactor * (float)num2), 15f * (1f - controlFactor * (float)num2));
			}
			else
			{
				ballAngle = 45f + UnityEngine.Random.Range(-10f * (1f - controlFactor * (float)num2), 10f * (1f - controlFactor * (float)num2));
			}
		}
		else if (shotPlayed == "WCCLite_Dilscoop")
		{
			ballAngle = 67.5f + UnityEngine.Random.Range(-10f * (1f - controlFactor * (float)num2), 10f * (1f - controlFactor * (float)num2));
		}
		else if (shotPlayed == "bt6LateCut" || shotPlayed == "bt6ReverseSweep" || shotPlayed == "lateCutLowHeight" || shotPlayed == "reverseSweepSlowBall")
		{
			ballAngle = 120f + UnityEngine.Random.Range(-15f * (1f - controlFactor * (float)num2), 15f * (1f - controlFactor * (float)num2));
		}
		else if (shotPlayed == "bt6SquareCut")
		{
			ballAngle = 160f + UnityEngine.Random.Range(-15f * (1f - controlFactor * (float)num2), 15f * (1f - controlFactor * (float)num2));
		}
		else if (shotPlayed == "squareDrive" || shotPlayed == "lateSquareDrive" || shotPlayed == "frontFootOffDrive")
		{
			ballAngle = 180f + UnityEngine.Random.Range(-15f * (1f - controlFactor * (float)num2), 15f * (1f - controlFactor * (float)num2));
		}
		else if (shotPlayed == "bt6CoverDrive" || shotPlayed == "extraCoverDrive")
		{
			ballAngle = 210f + UnityEngine.Random.Range(-15f * (1f - controlFactor * (float)num2), 15f * (1f - controlFactor * (float)num2));
			flag = true;
		}
		else if (shotPlayed == "insideOutCoverDrive")
		{
			ballAngle = 210f + UnityEngine.Random.Range(-30f * (1f - controlFactor * (float)num2), 30f * (1f - controlFactor * (float)num2));
		}
		else if (shotPlayed == "bt6OffDrive" || shotPlayed == "backFootOffDrive" || shotPlayed == "loftOffSide" || shotPlayed == "WCCLite_HarbhajanShot" || shotPlayed == "WCCLite_LoftedSquareDrive")
		{
			ballAngle = 240f + UnityEngine.Random.Range(-15f * (1f - controlFactor * (float)num2), 15f * (1f - controlFactor * (float)num2));
			if (shotPlayed != "WCCLite_HarbhajanShot" || shotPlayed != "WCCLite_LoftedSquareDrive")
			{
				flag = true;
			}
			if (squareCutDrive && shotPlayed == "bt6OffDrive")
			{
				ballAngle = 180f + UnityEngine.Random.Range(-15f * (1f - controlFactor * (float)num2), 15f * (1f - controlFactor * (float)num2));
			}
		}
		else if (shotPlayed == "bt6StraightDrive" || shotPlayed == "backFootStraightDrive" || shotPlayed == "loftStraight" || shotPlayed == "bt6Defense" || shotPlayed == "backFootDefenseHighBall" || shotPlayed == "frontFootOffSideDefense" || shotPlayed == "loftStraightShortBall" || shotPlayed == "straightDrivePowerShot")
		{
			ballAngle = 270f + UnityEngine.Random.Range(-15f * (1f - controlFactor * (float)num2), 15f * (1f - controlFactor * (float)num2));
			if (shotPlayed == "bt6StraightDrive" || shotPlayed == "backFootStraightDrive" || shotPlayed == "loftStraight")
			{
				flag = true;
			}
		}
		else if (shotPlayed == "WCCLite_BackFootPunch")
		{
			ballAngle = 225f + UnityEngine.Random.Range(-15f * (1f - controlFactor * (float)num2), 15f * (1f - controlFactor * (float)num2));
		}
		else if (shotPlayed == "WCCLite_ABdeVilliers_Shot")
		{
			ballAngle = 40f + UnityEngine.Random.Range(-10f * (1f - controlFactor * (float)num2), 10f * (1f - controlFactor * (float)num2));
		}
		else if (shotPlayed == "WCCLite_LegGlanceNew")
		{
			if (squareLegGlance)
			{
				ballAngle = 17f + UnityEngine.Random.Range(-15f * (1f - controlFactor * (float)num2), 15f * (1f - controlFactor * (float)num2));
			}
			else
			{
				ballAngle = 22.5f + UnityEngine.Random.Range(0f * (1f - controlFactor * (float)num2), 15f * (1f - controlFactor * (float)num2));
			}
		}
		else if (shotPlayed == "bt6Sweep")
		{
			ballAngle = 26.5f + UnityEngine.Random.Range(0f * (1f - controlFactor * (float)num2), 15f * (1f - controlFactor * (float)num2));
		}
		else if (shotPlayed == "pullShot" || shotPlayed == "lowPullShot")
		{
			ballAngle = 341f + UnityEngine.Random.Range(-19f * (1f - controlFactor * (float)num2), 19f * (1f - controlFactor * (float)num2));
		}
		else if (shotPlayed == "lowPullShot1")
		{
			ballAngle = 355f + UnityEngine.Random.Range(-19f * (1f - controlFactor * (float)num2), 19f * (1f - controlFactor * (float)num2));
		}
		else if (shotPlayed == "bt6OnDrive" || shotPlayed == "backFootOnDrive" || shotPlayed == "loftLegSide" || shotPlayed == "runDownOnDrive")
		{
			ballAngle = 303f + UnityEngine.Random.Range(-19f * (1f - controlFactor * (float)num2), 19f * (1f - controlFactor * (float)num2));
		}
		else if (shotPlayed == "backFootOnDrive_new")
		{
			ballAngle = 325f + UnityEngine.Random.Range(-19f * (1f - controlFactor * (float)num2), 19f * (1f - controlFactor * (float)num2));
		}
		else if (shotPlayed == "WCCLite_HelicopterShot" || shotPlayed == "powerfulSweepShot")
		{
			ballAngle = 320f + UnityEngine.Random.Range(-15f * (1f - controlFactor * (float)num2), 15f * (1f - controlFactor * (float)num2));
		}
		else if (shotPlayed == "bt6PullShot")
		{
			ballAngle = 335f + UnityEngine.Random.Range(-15f * (1f - controlFactor * (float)num2), 15f * (1f - controlFactor * (float)num2));
		}
		if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
		{
			ballAngle = shotAngle + UnityEngine.Random.Range(-10f * (1f - controlFactor * (float)num2), 10f * (1f - controlFactor * (float)num2));
			if (shotPlayed == "bt6Defense" || shotPlayed == "backFootDefenseHighBall" || shotPlayed == "frontFootOffSideDefense")
			{
				ballAngle = 270 + UnityEngine.Random.Range(-10, 10);
				flag = true;
			}
			if (CONTROLLER.StrikerHand == "left")
			{
				ballAngle = 180f - ballAngle + 360f;
				ballAngle %= 360f;
			}
		}
		if (CONTROLLER.BattingTeamIndex == CONTROLLER.opponentTeamIndex && AIHitInGap)
		{
			ballAngle = AIBallAngle;
			if (shotPlayed == "bt6Defense" || shotPlayed == "backFootDefenseHighBall" || shotPlayed == "frontFootOffSideDefense")
			{
				ballAngle = 270 + UnityEngine.Random.Range(-10, 10);
				flag = true;
			}
		}
		if (flag && UnityEngine.Random.Range(0f, 100f) > 98f * (1f + controlFactor * (float)num2))
		{
			slipShot = true;
			ballAngle = 105f;
		}
		if (Singleton<LeftFovLerp>.instance != null)
		{
			Singleton<LeftFovLerp>.instance.setBallAngle(ballAngle);
		}
		if (Singleton<RightFovLerp>.instance != null)
		{
			Singleton<RightFovLerp>.instance.setBallAngle(ballAngle);
		}
		if (Singleton<MainCameraMovement>.instance != null)
		{
			Singleton<MainCameraMovement>.instance.setBallAngle(ballAngle);
		}
		Singleton<GameModel>.instance.GetFinalBallAngle(ballAngle);
		if (ballAngle > 20f && ballAngle < 90f)
		{
			ballToFineLeg = true;
		}
		bool flag2 = false;
		if (currentBatsmanHand == "right")
		{
			if (ballTransform.position.x > batsmanTransform.position.x - 0.95f)
			{
				flag2 = false;
			}
			else
			{
				flag2 = true;
			}
		}
		else if (currentBatsmanHand == "left")
		{
			if ((double)ballTransform.position.x < (double)batsmanTransform.position.x + 0.75)
			{
				flag2 = false;
			}
			else
			{
				flag2 = true;
			}
		}
		DebugLogger.PrintWithSize("Shot played: " + shotPlayed);
		if (shotPlayed == "bt6StraightDrive" && !replayMode)
		{
			if (!hardcoded && CONTROLLER.PlayModeSelected != 4 && CONTROLLER.PlayModeSelected != 5 && CONTROLLER.PlayModeSelected != 6 && CanProduceEdge() && !lineFreeHit && !IsFullTossBall && !overStepBall && ballSpotLength <= 14.8f)
			{
				slipShot = false;
				ballAngle = 95f + UnityEngine.Random.Range(0.1f, 0.6f);
				edgeCatch = true;
				flag2 = true;
				wicketKeeperStatus = string.Empty;
				if (Singleton<GameModel>.instance != null)
				{
					disableRunCancelBtn();
				}
				if (activeFielderNumber.Count > 0)
				{
					activeFielderNumber.Clear();
				}
				saveEdgeCatch = edgeCatch;
				SetUltraEdgeDecision();
				if (AiCanAskReview || UserCanAskReview)
				{
					UltraEdgeCutscenePlaying = true;
					PlaceUltraEdgeCam();
				}
			}
		}
		else if (powerShot && (shotPlayed == "bt6HookShot" || shotPlayed == "bt6SquareCut" || shotPlayed == "pullShot") && topEdge)
		{
			if (UnityEngine.Random.Range(0, 10) > 3)
			{
				slipShot = false;
				ballAngle = UnityEngine.Random.Range(95, 125);
			}
			else
			{
				slipShot = false;
				ballAngle = UnityEngine.Random.Range(50, 80);
			}
		}
		if (hardcoded && CONTROLLER.PlayModeSelected != 4 && CONTROLLER.PlayModeSelected != 5 && CONTROLLER.PlayModeSelected != 6 && !lineFreeHit && !IsFullTossBall && ballSpotLength <= 14.8f)
		{
			slipShot = false;
			ballAngle = 95f + UnityEngine.Random.Range(0.1f, 0.6f);
			edgeCatch = true;
			flag2 = true;
			wicketKeeperStatus = string.Empty;
			if (Singleton<GameModel>.instance != null)
			{
				disableRunCancelBtn();
			}
			if (activeFielderNumber.Count > 0)
			{
				activeFielderNumber.Clear();
			}
			saveEdgeCatch = edgeCatch;
			SetUltraEdgeDecision();
			if (AiCanAskReview || UserCanAskReview)
			{
				UltraEdgeCutscenePlaying = true;
				PlaceUltraEdgeCam();
			}
		}
		if (currentBatsmanHand == "left")
		{
			ballAngle = 180f - ballAngle + 360f;
			ballAngle %= 360f;
		}
		if (Singleton<GameModel>.instance != null && !edgeCatch && !UltraEdgeCutscenePlaying)
		{
			Singleton<GameModel>.instance.PlayGameSound("Bat");
		}
		Singleton<AIFieldingSetupManager>.instance.lastHittedAngle = ballAngle;
		if (shotPlayed == "bt6Defense" || shotPlayed == "backFootDefenseHighBall" || shotPlayed == "frontFootOffSideDefense")
		{
			horizontalSpeed = 5 + UnityEngine.Random.Range(-1, 1);
			ballProjectileAngle = 270f;
			ballProjectileHeight = ballBatMeetingHeight;
			ballTimingFirstBounceDistance = ballBatMeetingHeight * 3f;
			ballProjectileAnglePerSecond = 90f / ballTimingFirstBounceDistance * horizontalSpeed;
		}
		else if (slipShot)
		{
			ballTimingFirstBounceDistance = 19f;
			horizontalSpeed = 20f + UnityEngine.Random.Range(0f, 4f);
			ballProjectileHeight = UnityEngine.Random.Range(2, 4);
			float num3 = Mathf.Asin(ballBatMeetingHeight / ballProjectileHeight) * RAD2DEG;
			ballProjectileAngle = 180f + num3;
			ballProjectileAnglePerSecond = (180f - num3) / ballTimingFirstBounceDistance * horizontalSpeed;
		}
		else if (powerShot || savedPowerShotStatus)
		{
			if (Singleton<GameModel>.instance != null && !replayMode)
			{
				Singleton<GameModel>.instance.PlayGameSound("Beaten");
			}
			float num4 = 0f;
			float confidenceVal = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].ConfidenceVal;
			num4 = ((!(confidenceVal * 6f < 20f)) ? (confidenceVal * 6f) : 20f);
			if (CONTROLLER.totalOvers * 6 - CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls < 12)
			{
				num4 += 10f;
			}
			if (batsmanConfidenceLevel)
			{
				if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
				{
					ballTimingFirstBounceDistance = confidenceVal * 4f + (float)UnityEngine.Random.Range(25, 30);
					ballTimingFirstBounceDistance *= 1f + powerFactor * (float)num2;
				}
				else
				{
					ballTimingFirstBounceDistance = confidenceVal * 6.5f;
				}
				horizontalSpeed = confidenceVal + 15f + (float)UnityEngine.Random.Range(1, 6);
			}
			else
			{
				ballTimingFirstBounceDistance = confidenceVal * 6.5f - Mathf.Abs(0.8f - wantedAnimationSpeed) * 10f;
				ballTimingFirstBounceDistance *= 1f + powerFactor * (float)num2;
				horizontalSpeed = 22 + UnityEngine.Random.Range(1, 6);
			}
			float num5 = 0f;
			num5 = ((CONTROLLER.PlayModeSelected != 7) ? (confidenceVal / 10f * 2f) : (confidenceVal / 10f * 1f));
			if (CONTROLLER.BattingTeamIndex == CONTROLLER.opponentTeamIndex)
			{
				num5 += 3.5f;
			}
			if (CONTROLLER.difficultyMode == "hard" && CONTROLLER.BattingTeamIndex == CONTROLLER.opponentTeamIndex)
			{
				num5 += 3f;
			}
			if ((float)UnityEngine.Random.Range(0, 10) < num5 && shotPlayed != "bt6LateCut" && shotPlayed != "lateCutLowHeight")
			{
				ballTimingFirstBounceDistance = UnityEngine.Random.Range(80, 105);
			}
			else
			{
				if ((float)UnityEngine.Random.Range(0, 10) > confidenceVal || ballTimingFirstBounceDistance > 90f)
				{
					if (CONTROLLER.totalOvers * 6 - CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls < 12)
					{
						if (UnityEngine.Random.Range(0, 10) < 2)
						{
							ballTimingFirstBounceDistance -= UnityEngine.Random.Range(10, 20);
						}
					}
					else
					{
						ballTimingFirstBounceDistance -= UnityEngine.Random.Range(10, 20);
					}
				}
				if (ballTimingFirstBounceDistance < 40f)
				{
					ballTimingFirstBounceDistance = 40 + UnityEngine.Random.Range(10, 20);
				}
			}
			if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
			{
				if (perfectShot)
				{
					ballTimingFirstBounceDistance = 80 + UnityEngine.Random.Range(5, 15);
				}
				else if (mistimedShot && ballTimingFirstBounceDistance > 60f)
				{
					ballTimingFirstBounceDistance = 40 + UnityEngine.Random.Range(10, 20);
				}
				else
				{
					ballTimingFirstBounceDistance *= firstBounceMultiplier;
				}
			}
			if (CONTROLLER.difficultyMode == "hard" && battingBy == "user" && !edgeCatch)
			{
				ballTimingFirstBounceDistance *= 0.9f;
				ballTimingFirstBounceDistance *= 1f + powerFactor * (float)num2;
			}
			if (shotPlayed == "bt6LateCut" || shotPlayed == "lateCutLowHeight" || shotPlayed == "bt6ReverseSweep")
			{
				ballTimingFirstBounceDistance /= 4f;
				ballTimingFirstBounceDistance *= 1f + powerFactor * (float)num2;
				horizontalSpeed = 18 + UnityEngine.Random.Range(0, 5);
			}
			if (topEdge && !replayMode)
			{
				horizontalSpeed = 9 + UnityEngine.Random.Range(2, 3);
				ballTimingFirstBounceDistance /= 2f;
				ballTimingFirstBounceDistance *= 1f + powerFactor * (float)num2;
				ballProjectileHeight = ballTimingFirstBounceDistance / (float)(2 + UnityEngine.Random.Range(0, 2));
				float num6 = Mathf.Asin(ballBatMeetingHeight / ballProjectileHeight) * RAD2DEG;
				ballProjectileAngle = 180f + num6;
				ballProjectileAnglePerSecond = (180f - num6) / ballTimingFirstBounceDistance * horizontalSpeed;
			}
			else if (!edgeCatch && !replayMode)
			{
				ballProjectileHeight = ballTimingFirstBounceDistance / (float)(6 + UnityEngine.Random.Range(-1, 2));
				float num7 = Mathf.Asin(ballBatMeetingHeight / ballProjectileHeight) * RAD2DEG;
				ballProjectileAngle = 180f + num7;
				ballProjectileAnglePerSecond = (180f - num7) / ballTimingFirstBounceDistance * horizontalSpeed;
			}
		}
		else if (!powerShot && !topEdge && !edgeCatch && !saveEdgeCatch)
		{
			if (Singleton<GameModel>.instance != null && !replayMode)
			{
				Singleton<GameModel>.instance.PlayGameSound("Beaten");
			}
			if (ballAngle > 250f && ballAngle < 290f)
			{
				horizontalSpeed = 15 + UnityEngine.Random.Range(0, 10);
				ballProjectileAngle = 270f;
				horizontalSpeed *= horizontalSpeedMultiplier;
				ballProjectileHeight = ballBatMeetingHeight;
				ballTimingFirstBounceDistance = ballBatMeetingHeight * 10f;
				ballTimingFirstBounceDistance *= 1f + powerFactor * (float)num2;
				ballProjectileAnglePerSecond = 90f / ballTimingFirstBounceDistance * horizontalSpeed;
			}
			else
			{
				if (ballBatMeetingHeight < 0.5f)
				{
					horizontalSpeed = 15 + UnityEngine.Random.Range(0, 10);
				}
				else
				{
					horizontalSpeed = 25f;
				}
				ballProjectileHeight = ballBatMeetingHeight * 2f;
				if (shotPlayed == "bt6OffDrive" && UnityEngine.Random.Range(0, 10) < 8)
				{
					ballProjectileHeight = ballBatMeetingHeight;
				}
				horizontalSpeed *= horizontalSpeedMultiplier;
				ballTimingFirstBounceDistance = ballProjectileHeight * 10f;
				ballTimingFirstBounceDistance *= 1f + powerFactor * (float)num2;
				float num8 = Mathf.Asin(ballBatMeetingHeight / ballProjectileHeight) * RAD2DEG;
				ballProjectileAngle = 180f + num8;
				ballProjectileAnglePerSecond = (180f - num8) / ballTimingFirstBounceDistance * horizontalSpeed;
			}
		}
		if (!replayMode)
		{
			savedBallAngle = ballAngle;
			savedHorizontalSpeed = horizontalSpeed;
			savedBallProjectileAngle = ballProjectileAngle;
			savedBallProjectileHeight = ballProjectileHeight;
			savedBallTimingFirstBounceDistance = ballTimingFirstBounceDistance;
			savedBallProjectileAnglePerSecond = ballProjectileAnglePerSecond;
			savedSlipShot = slipShot;
			savedBallToFineLeg = ballToFineLeg;
			saveEdgeCatch = edgeCatch;
		}
		else if (replayMode)
		{
			edgeCatch = saveEdgeCatch;
			if (edgeCatch)
			{
				wicketKeeperStatus = string.Empty;
			}
			ballAngle = savedBallAngle;
			horizontalSpeed = savedHorizontalSpeed;
			ballProjectileAngle = savedBallProjectileAngle;
			ballProjectileHeight = savedBallProjectileHeight;
			ballTimingFirstBounceDistance = savedBallTimingFirstBounceDistance;
			ballProjectileAnglePerSecond = savedBallProjectileAnglePerSecond;
			slipShot = savedSlipShot;
			ballToFineLeg = savedBallToFineLeg;
		}
		if (edgeCatch)
		{
			wicketKeeperCatchingAnimationSelected = false;
			ActivateWicketKeeper();
		}
		nextPitchDistance = ballTimingFirstBounceDistance;
		ballConnectedTiming = Time.time;
		ballTimingFirstBounce.transform.position = new Vector3(ballTimingOriginTransform.position.x + ballTimingFirstBounceDistance * Mathf.Cos(ballAngle * DEG2RAD), ballTimingFirstBounce.transform.position.y, ballTimingOriginTransform.position.z + ballTimingFirstBounceDistance * Mathf.Sin(ballAngle * DEG2RAD));
		if (Singleton<LeftFovLerp>.instance != null)
		{
			Singleton<LeftFovLerp>.instance.setBallFirstBounce(ballTimingFirstBounceDistance);
		}
		if (Singleton<RightFovLerp>.instance != null)
		{
			Singleton<RightFovLerp>.instance.setBallFirstBounce(ballTimingFirstBounceDistance);
		}
		if (Singleton<MainCameraMovement>.instance != null)
		{
			Singleton<MainCameraMovement>.instance.setBallFirstBounce(ballTimingFirstBounceDistance);
		}
		if (ballTimingFirstBounceDistance > 55f && !replayMode && !UltraEdgeCutscenePlaying)
		{
			Transform transform = UnityEngine.Object.Instantiate(BallHitEffect, batCollider.transform.position, batCollider.transform.rotation);
			transform.transform.position = ball.transform.position;
			transform.transform.localScale = Vector3.one * 0.2f;
		}
		FixBallCatchingSpot();
		if (ballAngle >= 90f && ballAngle <= 210f)
		{
			postBattingWicketKeeperDirection = "offSide";
		}
		else if (ballAngle > 210f && ballAngle < 330f)
		{
			postBattingWicketKeeperDirection = "straight";
		}
		else
		{
			postBattingWicketKeeperDirection = "legSide";
		}
		RepositionSideCamera();
	}

	private bool CanProduceEdge()
	{
		if (CONTROLLER.PlayModeSelected != 7)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 4;
			if (ObscuredPrefs.HasKey("perMatchEdgeCount" + CONTROLLER.PlayModeSelected))
			{
				num2 = ObscuredPrefs.GetInt("perMatchEdgeCount" + CONTROLLER.PlayModeSelected);
			}
			if (CONTROLLER.Overs[CONTROLLER.oversSelectedIndex] < 2)
			{
				num3 = 2;
			}
			if (num2 < num3)
			{
				if (ObscuredPrefs.HasKey("newUserEdgeCount"))
				{
					num = ObscuredPrefs.GetInt("newUserEdgeCount");
				}
				else
				{
					ObscuredPrefs.SetInt("newUserEdgeCount", num);
				}
				int num4 = 11;
				int num5 = 11;
				if (CONTROLLER.Overs[CONTROLLER.oversSelectedIndex] < 3 && CONTROLLER.BattingTeamIndex == CONTROLLER.opponentTeamIndex)
				{
					num5 = 11;
				}
				if (num < 5)
				{
					if (UnityEngine.Random.Range(0, 10) < num4)
					{
						num++;
						ObscuredPrefs.SetInt("newUserEdgeCount", num);
						num2++;
						ObscuredPrefs.SetInt("perMatchEdgeCount" + CONTROLLER.PlayModeSelected, num2);
						return true;
					}
				}
				else if (UnityEngine.Random.Range(0, 10) < num5)
				{
					num2++;
					ObscuredPrefs.SetInt("perMatchEdgeCount" + CONTROLLER.PlayModeSelected, num2);
					return true;
				}
			}
		}
		else
		{
			int num6 = 0;
			int num7 = 0;
			if (ObscuredPrefs.HasKey("perMatchEdgeCount" + CONTROLLER.PlayModeSelected))
			{
				num7 = ObscuredPrefs.GetInt("perMatchEdgeCount" + CONTROLLER.PlayModeSelected);
			}
			if (num7 < 2)
			{
				if (ObscuredPrefs.HasKey("newUserEdgeCount"))
				{
					num6 = ObscuredPrefs.GetInt("newUserEdgeCount");
				}
				else
				{
					ObscuredPrefs.SetInt("newUserEdgeCount", num6);
				}
				int num8 = 30;
				int num9 = 15;
				if (num6 < 5)
				{
					if (UnityEngine.Random.Range(0, 100) < num8)
					{
						num6++;
						ObscuredPrefs.SetInt("newUserEdgeCount", num6);
						num7++;
						ObscuredPrefs.SetInt("perMatchEdgeCount" + CONTROLLER.PlayModeSelected, num7);
						return true;
					}
				}
				else if (UnityEngine.Random.Range(0, 100) < num9)
				{
					num7++;
					ObscuredPrefs.SetInt("perMatchEdgeCount" + CONTROLLER.PlayModeSelected, num7);
					return true;
				}
			}
		}
		return false;
	}

	private void RepositionSideCamera()
	{
		if ((!(ballAngle >= 180f) || !(ballAngle <= 210f)) && ballAngle >= 330f && !(ballAngle <= 359f))
		{
		}
	}

	private void FixBallCatchingSpot()
	{
		ballCatchingSpotTransform.position = new Vector3(ballTimingOriginTransform.position.x + (ballTimingFirstBounceDistance - ballPreCatchingDistance) * Mathf.Cos(ballAngle * DEG2RAD), ballCatchingSpotTransform.position.y, ballTimingOriginTransform.position.z + (ballTimingFirstBounceDistance - ballPreCatchingDistance) * Mathf.Sin(ballAngle * DEG2RAD));
		while (DistanceBetweenTwoVector2(groundCenterPoint, ballCatchingSpot) > groundRadius - 2f)
		{
			ballPreCatchingDistance += 1f;
			ballCatchingSpotTransform.position = new Vector3(ballTimingOriginTransform.position.x + (ballTimingFirstBounceDistance - ballPreCatchingDistance) * Mathf.Cos(ballAngle * DEG2RAD), ballCatchingSpotTransform.position.y, ballTimingOriginTransform.position.z + (ballTimingFirstBounceDistance - ballPreCatchingDistance) * Mathf.Sin(ballAngle * DEG2RAD));
		}
	}

	public void FreezeTheBowlingSpot()
	{
		canShowFCLPowers = false;
		RunnerAnimationComponent.CrossFade("getReadyNew");
		RunnerAnimationComponent["getReadyNew"].speed = 1.3f;
		nonStrikerStatus = "getReady";
		batsmanCanMoveLeftRight = false;
		batsmanAnimationComponent.CrossFade("WCCLite_BatsmanTapLoop");
		if (!replayMode)
		{
			if (savedLeftArrowKeyDownArray.Count > savedLeftArrowKeyUpArray.Count)
			{
				savedLeftArrowKeyUpArray.Add(Time.time - bowlerRunupStartTime);
			}
			if (savedRightArrowKeyDownArray.Count > savedRightArrowKeyUpArray.Count)
			{
				savedRightArrowKeyUpArray.Add(Time.time - bowlerRunupStartTime);
			}
		}
		if (!userBowlingSpotSelected)
		{
			userBowlerCanMoveBowlingSpot = false;
			FreezeBowlingSpot();
			Singleton<GameModel>.instance.ShowTutorial(-1);
		}
		if (Singleton<GameModel>.instance != null && CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex && !replayMode)
		{
			Singleton<GameModel>.instance.EnableMovement(boolean: false);
		}
		if (replayMode && !overStepBall)
		{
			replayActionStatus = "follow";
			if (CONTROLLER.stumpingAttempted)
			{
				replayCameraScript.enabled = false;
			}
			else
			{
				replayCameraScript.enabled = true;
			}
			Time.timeScale = 0.5f;
		}
	}

	private void checkForLineNoBall()
	{
		float num = 0f;
		if (BowlerAnimationComponent.IsPlaying("Blitz_" + currentBowlerType + "PaceBowling0" + BowlerAnimNumber))
		{
			num = BowlerAnimationComponent["Blitz_" + currentBowlerType + "PaceBowling0" + BowlerAnimNumber].time * 25f;
		}
		if (currentBowlerType == "fast")
		{
			if (num >= 78f && !isNoBallSet)
			{
				isNoBallSet = true;
				setLineNoBall();
			}
			else if (num >= 84f && !isSlowMotionSetForNoBall)
			{
				isSlowMotionSetForNoBall = true;
				freezeTimeForLineNoBall();
			}
		}
		else if (currentBowlerType == "medium")
		{
			if (num >= 156f && !isNoBallSet)
			{
				isNoBallSet = true;
				setLineNoBall();
			}
			else if (num >= 166f && !isSlowMotionSetForNoBall)
			{
				isSlowMotionSetForNoBall = true;
				freezeTimeForLineNoBall();
			}
		}
		else if (currentBowlerType == "spin")
		{
			if (num >= 156f && !isNoBallSet)
			{
				isNoBallSet = true;
				setLineNoBall();
			}
			else if (num >= 166f && !isSlowMotionSetForNoBall)
			{
				isSlowMotionSetForNoBall = true;
				freezeTimeForLineNoBall();
			}
		}
	}

	private void setLineNoBall()
	{
		if (overStepBall)
		{
			bowler.transform.position = new Vector3(bowler.transform.position.x, bowler.transform.position.y, bowler.transform.position.z + bowlerHeelPosForNoBall);
			ballTransform.position = new Vector3(ballTransform.position.x, ballTransform.position.y, ballTransform.position.z + bowlerHeelPosForNoBall);
			fielder10Transform.position = new Vector3(fielder10Transform.position.x, fielder10Transform.position.y, fielder10Transform.position.z + bowlerHeelPosForNoBall);
		}
	}

	private void freezeTimeForLineNoBall()
	{
		if (overStepBall && replayMode)
		{
			Time.timeScale = 0.05f;
		}
	}

	public void ReleaseTheBall()
	{
		Singleton<GameModel>.instance.CanPauseGame = false;
		Singleton<GameModel>.instance.CanResumeGame = false;
		if (overStepBall)
		{
			noBallActionWaitTime = 4f;
		}
		updateBattingTimingMeterNeedle = true;
		if (replayMode && overStepBall)
		{
			replayActionStatus = "follow";
			replayCameraScript.enabled = true;
			Time.timeScale = 0.5f;
		}
		if (!replayMode)
		{
			if (CONTROLLER.PlayModeSelected != 7)
			{
				if (CONTROLLER.currentInnings == 1 || CONTROLLER.PlayModeSelected == 4 || CONTROLLER.PlayModeSelected == 5)
				{
					targetToWin = CONTROLLER.TargetToChase - CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchScores;
				}
				else
				{
					targetToWin = 0;
				}
			}
			else if (CONTROLLER.currentInnings == 3)
			{
				targetToWin = CONTROLLER.TargetToChase - CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchScores2;
			}
			else
			{
				targetToWin = 0;
			}
		}
		if (ballReleased)
		{
			ballTrail.enabled = true;
			ballTrail.time = 0.08f;
			ballTrail.colorGradient = ballTrialColorGradient;
			ballTrail.material = ballTrailMaterial;
		}
		ActivateColliders(boolean: true);
		ballReleased = true;
		ballStatus = "bowling";
		FindBowlingParameters();
		ballReleasedTime = Time.time;
		ShowBall(status: true);
		ball.GetComponent<Rigidbody>().WakeUp();
		BowlerBallSkinRendererComponent.enabled = false;
		if (battingBy == "computer" && !replayMode)
		{
			GetComputerBattingKeyInput();
		}
		action = 3;
		if (Singleton<GameModel>.instance != null && CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex && !replayMode)
		{
			Singleton<GameModel>.instance.EnableShotSelection(boolean: true);
		}
	}

	public void ZoomCameraToPitch()
	{
		if (CONTROLLER.cameraType == 0)
		{
			iTween.MoveTo(mainCamera.gameObject, iTween.Hash("position", mainCameraZoomInPosition, "time", 3, "easetype", "easeInOutSine"));
		}
		else
		{
			iTween.MoveTo(mainCamera.gameObject, iTween.Hash("position", mainCameraZoomInPosition, "time", 5, "easetype", "easeInOutSine"));
		}
		for (int i = 0; i < getSlipArray.Count; i++)
		{
			if (getSlipArray[i] != null)
			{
				GameObject gameObject = getSlipArray[i];
				gameObject.GetComponent<Animation>().Play("getReadyInSlip");
				gameObject.GetComponent<Animation>()["getReadyInSlip"].speed = UnityEngine.Random.Range(1, 3);
			}
		}
	}

	public void ZoomCameraToBowler()
	{
		if (zoomCameraToBowlerStartTime != -1f && Time.time > zoomCameraToBowlerStartTime + 0.2f)
		{
			zoomCameraToBowlerStartTime = -1f;
			if (currentBowlerType == "fast")
			{
				iTween.MoveTo(mainCamera.gameObject, iTween.Hash("position", fastBowlerZoomOutPosition, "time", 0, "easetype", "easeInOutSine", "oncomplete", "ZoomCameraToBowlerOnComplete", "oncompletetarget", base.gameObject));
			}
			else
			{
				iTween.MoveTo(mainCamera.gameObject, iTween.Hash("position", mainCameraZoomOutPosition, "time", 0, "easetype", "easeInOutSine", "oncomplete", "ZoomCameraToBowlerOnComplete", "oncompletetarget", base.gameObject));
			}
		}
	}

	private int getNoOfWicket()
	{
		int num = 0;
		for (int i = 0; i < CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].ballUpdate.Length; i++)
		{
			if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].ballUpdate[i] == "W")
			{
				num++;
			}
		}
		return num;
	}

	[Skip]
	private void ZoomCameraToBowlerOnComplete()
	{
		ActivateColliders(boolean: false);
		if (CONTROLLER.cameraType == 0)
		{
		}
		batsmanTriggeredShot = false;
		canMakeShot = false;
		bowlerIsWaiting = true;
		canShowFCLPowers = true;
		if (Singleton<GameModel>.instance != null)
		{
			Singleton<GameModel>.instance.ShowBowlingInterface(boolean: true);
		}
	}

	private void ZoomCameraToBatsman()
	{
		Vector3 vector = mainCameraZoomInPosition;
		vector += new Vector3(0f, 0f, 2f);
		if (CONTROLLER.cameraType == 0)
		{
			iTween.MoveTo(mainCamera.gameObject, iTween.Hash("position", vector, "time", 0.5, "easetype", "easeInOutSine"));
		}
		else
		{
			iTween.MoveTo(mainCamera.gameObject, iTween.Hash("position", vector, "time", 0.3, "easetype", "easeInOutSine"));
		}
	}

	private void ZoomCameraToUmpire()
	{
		Vector3 vector = mainCameraZoomInPosition;
		vector -= new Vector3(0f, 0.5f, 0f);
		iTween.MoveTo(mainCamera.gameObject, iTween.Hash("position", vector, "time", 1, "easetype", "easeInOutSine"));
	}

	private void ZoomCameraToWicketKeeper()
	{
		closeUpCamTransform.position = wicketKeeperTransform.position;
		closeUpCamTransform.position = new Vector3(closeUpCamTransform.position.x, 2f, closeUpCamTransform.position.z);
		closeUpCamTransform.position -= new Vector3(0f, 0f, 8f);
		closeUpCamTransform.eulerAngles = new Vector3(closeUpCamTransform.eulerAngles.x, 0f, closeUpCamTransform.eulerAngles.z);
		closeUpCamera.enabled = true;
	}

	private void InitCamera()
	{
		if (currentBowlerType == "fast")
		{
			mainCameraTransform.position = fastBowlerZoomOutPosition;
		}
		else
		{
			mainCameraTransform.position = mainCameraZoomOutPosition;
		}
		mainCameraTransform.eulerAngles = mainCameraInitRotation;
	}

	private void BatsmanWaiting()
	{
		if (!CONTROLLER.ReplayShowing)
		{
			Singleton<GameModel>.instance.CanPauseGame = true;
		}
		else
		{
			Singleton<GameModel>.instance.CanPauseGame = false;
		}
		if (Time.time > currentBallStartTime + batsmanWaitSeconds)
		{
			batsmanAnimationComponent.CrossFade("WCCLite_BatsmanStanceReady");
			batsmanAnimationComponent.PlayQueued("WCCLite_BatsmanTapLoop", QueueMode.CompleteOthers);
			if (currentBowlerType == "fast")
			{
				WicketKeeperAnimationComponent.CrossFade("getReady", 0.5f);
			}
			else if (currentBowlerType == "medium")
			{
				WicketKeeperAnimationComponent.CrossFade("getReady", 0.5f);
			}
			else
			{
				WicketKeeperAnimationComponent.CrossFade("getReadyForSpin", 0.5f);
			}
			if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
			{
				action = 1;
			}
			else
			{
				action = 1;
			}
		}
	}

	public void BlockBowlerSideChange()
	{
		bowlerIsWaiting = false;
	}

	private void BowlerWaiting()
	{
		if (!CONTROLLER.ReplayShowing)
		{
			Singleton<GameModel>.instance.CanPauseGame = true;
		}
		else
		{
			Singleton<GameModel>.instance.CanPauseGame = false;
		}
		if (Time.time > currentBallStartTime + bowlerWaitSeconds)
		{
			BowlerAnimationComponent.Play("Blitz_" + currentBowlerType + "PaceBowling0" + BowlerAnimNumber);
			BowlerAnimationComponent["Blitz_" + currentBowlerType + "PaceBowling0" + BowlerAnimNumber].speed = 1f;
			batsmanCanMoveLeftRight = true;
			bowlerRunupStartTime = Time.time;
			if (!replayMode)
			{
				FindNewBowlingSpot();
				userBowlerCanMoveBowlingSpot = true;
				Singleton<Scoreboard>.instance.BowlerToBatsman();
				savedLeftArrowDownTime = false;
				savedRightArrowDownTime = false;
				savedLeftArrowKeyDownArray = new List<float>();
				savedLeftArrowKeyUpArray = new List<float>();
				savedRightArrowKeyDownArray = new List<float>();
				savedRightArrowKeyUpArray = new List<float>();
				savedTakeRunTimingArray = new List<float>();
				cancelRunArray = new float[10, 10];
				cancelRunCount = 0;
				saveCount = 0;
				cancelCount = 0;
				saveRunAgainArray = new float[10, 10];
				saveRunAgainCount = 0;
			}
			action = 2;
			if (Singleton<GameModel>.instance != null && CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex && !replayMode)
			{
				Singleton<GameModel>.instance.EnableMovement(boolean: true);
			}
		}
	}

	private void HidePause()
	{
		Singleton<Scoreboard>.instance.HidePause(boolean: true);
	}

	private void LookForMainCameraTopDownView()
	{
		if (thirdUmpireRunoutReplaySkipped)
		{
			return;
		}
		if (!replayMode)
		{
			if (ballTransform.position.z > outOfPitchZPos && ballStatus == "bowling" && !cameraToKeeper)
			{
				cameraToKeeper = true;
				ActivateStadiumAndSkybox(boolean: true);
				batsmanAnimationComponent[batsmanAnimation].speed = 1f;
				if (!(wicketKeeperOppositeLength > 1f) || UnityEngine.Random.Range(0, 10) <= 5 || !replayMode)
				{
				}
				if (batsmanConfidenceLevel)
				{
					DecreaseConfidenceLevel(shotPlayed);
				}
			}
			else if (ballStatus == "shotSuccess" && !mainCameraOnTopDownView && ballStatus != "bowled" && !slipShot)
			{
				mainCameraOnTopDownView = true;
				topDownViewStartTime = Time.time;
			}
			if (slipShot && !replayMode && !ballPickedByFielder)
			{
				mainCameraTransform.position += new Vector3(0f, 0f, 3f * Time.deltaTime);
				mainCameraTransform.LookAt(ballRayCastReferenceGOTransform);
			}
			else if (slipShot && !replayMode && ballPickedByFielder)
			{
				mainCameraTransform.position -= new Vector3(0f, 0f, 3f * Time.deltaTime);
				mainCameraTransform.LookAt(ballRayCastReferenceGOTransform);
			}
			if (!mainCameraOnTopDownView || replayMode)
			{
				return;
			}
			if (Time.time < topDownViewStartTime + topDownViewZoomingSecs)
			{
				mainCameraTransform.position = new Vector3(ballTransform.position.x, mainCameraTransform.position.y, mainCameraTransform.position.z);
				mainCameraTransform.position += new Vector3(0f, 3f * Time.deltaTime, 28f * Time.deltaTime);
				mainCameraTransform.eulerAngles += new Vector3(17f * Time.deltaTime, 0f, 0f);
				if (CONTROLLER.cameraType == 0)
				{
					mainCamera.fieldOfView += 40f * Time.deltaTime;
				}
				else
				{
					mainCamera.fieldOfView += 25f * Time.deltaTime;
				}
				mainCameraTransform.LookAt(ballTransform);
				return;
			}
			if (CONTROLLER.cameraType == 0)
			{
				Singleton<MainCameraMovement>.instance.StartFollowTween();
				return;
			}
			if (ballStatus == "shotSuccess" && !sideCameraSelected)
			{
				if (ballAngle >= 90f && ballAngle <= 270f)
				{
					leftSideCamera.enabled = true;
				}
				else
				{
					rightSideCamera.enabled = true;
				}
				sideCameraSelected = true;
				mainCamera.enabled = false;
			}
			float magnitude = (ballTransform.position - rightSideCamTransform.position).magnitude;
			if (magnitude > 25f && magnitude < 90f && (!ballOnboundaryLine || ballNoOfBounce <= 0))
			{
				rightSideCamTransform.LookAt(ballTransform);
			}
			float magnitude2 = (ballTransform.position - leftSideCamTransform.position).magnitude;
			if (magnitude2 > 25f && magnitude2 < 90f && (!ballOnboundaryLine || ballNoOfBounce <= 0))
			{
				leftSideCamTransform.LookAt(ballTransform);
			}
			return;
		}
		if (ballTransform.position.z > outOfPitchZPos && ballStatus == "bowling" && !cameraToKeeper)
		{
			cameraToKeeper = true;
			ActivateStadiumAndSkybox(boolean: true);
			batsmanAnimationComponent[batsmanAnimation].speed = 1f;
			if (!(wicketKeeperOppositeLength > 1f) || UnityEngine.Random.Range(0, 10) > 5)
			{
			}
			if (batsmanConfidenceLevel)
			{
				DecreaseConfidenceLevel(shotPlayed);
			}
		}
		else if (ballStatus == "shotSuccess" && !mainCameraOnTopDownView && ballStatus != "bowled" && !slipShot)
		{
			mainCameraOnTopDownView = true;
			topDownViewStartTime = Time.time;
		}
		if (slipShot && !ballPickedByFielder)
		{
			mainCameraTransform.position += new Vector3(0f, 0f, 3f * Time.deltaTime);
			mainCameraTransform.LookAt(ballRayCastReferenceGOTransform);
		}
		else if (slipShot && ballPickedByFielder)
		{
			mainCameraTransform.position -= new Vector3(0f, 0f, 3f * Time.deltaTime);
			mainCameraTransform.LookAt(ballRayCastReferenceGOTransform);
		}
		if (!mainCameraOnTopDownView)
		{
			return;
		}
		if (Time.time < topDownViewStartTime + topDownViewZoomingSecs)
		{
			mainCameraTransform.position = new Vector3(ballTransform.position.x, mainCameraTransform.position.y, mainCameraTransform.position.z);
			mainCameraTransform.position += new Vector3(0f, 3f * Time.deltaTime, 28f * Time.deltaTime);
			mainCameraTransform.eulerAngles += new Vector3(17f * Time.deltaTime, 0f, 0f);
			if (CONTROLLER.cameraType == 0)
			{
				mainCamera.fieldOfView += 40f * Time.deltaTime;
			}
			else
			{
				mainCamera.fieldOfView += 25f * Time.deltaTime;
			}
			mainCameraTransform.LookAt(ballTransform);
			return;
		}
		if (CONTROLLER.cameraType == 0)
		{
			Singleton<MainCameraMovement>.instance.StartFollowTween();
			return;
		}
		if (ballStatus == "shotSuccess" && !sideCameraSelected)
		{
			if (ballAngle >= 90f && ballAngle <= 270f)
			{
				leftSideCamera.enabled = true;
			}
			else
			{
				rightSideCamera.enabled = true;
			}
			sideCameraSelected = true;
			mainCamera.enabled = false;
		}
		float magnitude3 = (ballTransform.position - rightSideCamTransform.position).magnitude;
		if (magnitude3 > 25f && magnitude3 < 90f)
		{
			rightSideCamera.fieldOfView = 45f - magnitude3 / 2f;
			if (!ballOnboundaryLine || ballNoOfBounce <= 0)
			{
				rightSideCamTransform.LookAt(ballTransform);
			}
		}
		float magnitude4 = (ballTransform.position - leftSideCamTransform.position).magnitude;
		if (magnitude4 > 25f && magnitude4 < 90f)
		{
			leftSideCamera.fieldOfView = 45f - magnitude4 / 2f;
			if (!ballOnboundaryLine || ballNoOfBounce <= 0)
			{
				leftSideCamTransform.LookAt(ballTransform);
			}
		}
	}

	public void ScanForBoundaryOrSix()
	{
		float num = ballTransform.position.x - groundCenterPointTransform.position.x;
		float num2 = ballTransform.position.z - groundCenterPointTransform.position.z;
		float num3 = Mathf.Sqrt(num * num + num2 * num2);
		if (num3 > groundRadius && !ballOnboundaryLine && ballStatus != "bowled")
		{
			ballOnboundaryLine = true;
			canTakeRun = false;
			ball.GetComponent<Rigidbody>().WakeUp();
			if (Singleton<GameModel>.instance != null)
			{
				disableRunCancelBtn();
			}
			stayStartTime = Time.time;
			ballBoundaryReflection = false;
			boundaryAction = "boundary";
			showPreviewCamera(status: false);
			if (canBe4or6 == 6 && !replayMode)
			{
				CONTROLLER.SixDistance = Mathf.FloorToInt(ballTimingFirstBounceDistance);
				currentBallNoOfRuns = 6;
				if (Singleton<GameModel>.instance != null)
				{
					Singleton<GameModel>.instance.PlayGameSound("Boundary");
				}
			}
			else if (!replayMode)
			{
				currentBallNoOfRuns = 4;
				if (Singleton<GameModel>.instance != null)
				{
					Singleton<GameModel>.instance.PlayGameSound("Boundary");
				}
				if (ballStatus == "shotSuccess" && Singleton<GameModel>.instance != null && !replayMode && slipShot)
				{
				}
			}
			if (wicketKeeperStatus == "catchMissed")
			{
				if (Singleton<GameModel>.instance != null && !replayMode)
				{
					Singleton<GameModel>.instance.PlayGameSound("Beaten");
				}
				if (!edgeCatch && wideBall)
				{
					currentBallNoOfRuns = 5;
				}
				else if (!wideBall)
				{
					currentBallNoOfRuns = 4;
				}
				boundaryAction = "wideAndBoundary";
			}
		}
		if (boundaryAction == "boundary")
		{
			if (overStepBall)
			{
				float num4 = 0.5f;
				if (CONTROLLER.PlayModeSelected == 7)
				{
					num4 = 0f;
				}
				if (stayStartTime + num4 + timeBetweenBalls < Time.time)
				{
					if (replayMode)
					{
						boundaryAction = string.Empty;
						HideReplay();
						return;
					}
					boundaryAction = "lineNoBallBoundary";
					runsScoredInLineNoBall = currentBallNoOfRuns;
					if (currentBallNoOfRuns == 4)
					{
						Singleton<GameModel>.instance.InitAnimation(0);
					}
					else if (currentBallNoOfRuns == 6)
					{
						Singleton<GameModel>.instance.InitAnimation(1);
					}
				}
			}
			else if (lineFreeHit)
			{
				if (stayStartTime + 1.5f + timeBetweenBalls < Time.time)
				{
					boundaryAction = string.Empty;
					Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, currentBallNoOfRuns, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: true);
					lineFreeHit = false;
					lastBowledBall = "lineball";
					Singleton<Scoreboard>.instance.showFreeHitBg(canShow: false);
				}
			}
			else
			{
				if (replayMode && stayStartTime + 0.5f + timeBetweenBalls < Time.time)
				{
					HideReplay();
					return;
				}
				if (stayStartTime + 2f + timeBetweenBalls < Time.time)
				{
					boundaryAction = string.Empty;
					if (Singleton<GameModel>.instance != null && !replayMode)
					{
						if (noBall)
						{
							Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, currentBallNoOfRuns, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: true);
							CONTROLLER.isJokerCall = false;
						}
						else if (freeHit)
						{
							Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, currentBallNoOfRuns, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: true);
							freeHit = false;
							CONTROLLER.isFreeHit = false;
						}
						else
						{
							Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, currentBallNoOfRuns, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: true);
						}
					}
				}
			}
		}
		else if (boundaryAction == "lineNoBallBoundary")
		{
			if (stayStartTime + 4f + timeBetweenBalls < Time.time)
			{
				boundaryAction = string.Empty;
				Singleton<GameModel>.instance.setReplayCompletedVariable();
				showMainUmpireForNoBallAction();
				bool flag = checkForMatchComplete(currentBallNoOfRuns + 1, 0);
				if (CONTROLLER.matchType == "oneday" && !flag)
				{
					MainUmpireAnimationComponent.Play("NoBallFreeHit_New");
					lineFreeHit = true;
					lastBowledBall = "overstep";
				}
				else
				{
					MainUmpireAnimationComponent.Play("NoBallFreeHit_New");
				}
			}
		}
		else if (boundaryAction == "wideAndBoundary")
		{
			if (stayStartTime + 2.5f + timeBetweenBalls < Time.time)
			{
				showMainUmpireForNoBallAction();
				if (overStepBall)
				{
					if (replayMode)
					{
						boundaryAction = string.Empty;
						HideReplay();
						CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball += 5;
						Singleton<GameModel>.instance.UpdateCurrentBall(0, 0, 0, 5, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
						if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
						{
						}
						return;
					}
					boundaryAction = "umpireNoBallAction";
					bool flag2 = checkForMatchComplete(5, 0);
					if (CONTROLLER.matchType == "oneday" && !flag2)
					{
						noBallActionWaitTime = 4f;
						MainUmpireAnimationComponent.Play("NoBallFreeHit_New");
						lineFreeHit = true;
						lastBowledBall = "overstep";
					}
					else
					{
						noBallActionWaitTime = 1.5f;
						MainUmpireAnimationComponent.Play("NoBallFreeHit_New");
					}
					if (Singleton<GameModel>.instance != null && !replayMode)
					{
						Singleton<GameModel>.instance.PlayGameSound("Beaten");
					}
				}
				else if (lineFreeHit)
				{
					umpireCameraTransform.position = new Vector3(0f, 2f, -11f);
					boundaryAction = "waitForWideSignal";
					iTween.MoveTo(umpireCamera.gameObject, iTween.Hash("y", UnityEngine.Random.Range(1.4f, 1.8f), "time", 2));
					if (wideBall)
					{
						MainUmpireAnimationComponent.Play("WideBall_New");
						lineFreeHit = true;
						lastBowledBall = "overstep";
						Singleton<Scoreboard>.instance.showFreeHitBg(canShow: true);
					}
					else
					{
						mainUmpire.GetComponent<Animation>().Play("ByesFour_New");
						lineFreeHit = false;
						lastBowledBall = "lineball";
						Singleton<Scoreboard>.instance.showFreeHitBg(canShow: false);
					}
					if (bowlingBy == "computer")
					{
						bowlerSide = "left";
					}
					if (Singleton<GameModel>.instance != null && !replayMode)
					{
						Singleton<GameModel>.instance.PlayGameSound("Beaten");
					}
				}
				else
				{
					boundaryAction = "waitForWideSignal";
					umpireCameraTransform.position += new Vector3(0f, 0f, 0f);
					iTween.MoveTo(umpireCamera.gameObject, iTween.Hash("y", UnityEngine.Random.Range(1.4f, 1.8f), "time", 2));
					if (wideBall)
					{
						MainUmpireAnimationComponent.Play("WideBall_New");
					}
					else
					{
						mainUmpire.GetComponent<Animation>().Play("ByesFour_New");
					}
					if (bowlingBy == "computer")
					{
						bowlerSide = "left";
					}
					if (Singleton<GameModel>.instance != null && !replayMode)
					{
						Singleton<GameModel>.instance.PlayGameSound("Beaten");
					}
				}
			}
		}
		else if (boundaryAction == "umpireNoBallAction")
		{
			if (stayStartTime + noBallActionWaitTime + timeBetweenBalls < Time.time)
			{
				boundaryAction = "loopEnd";
				if (Singleton<GameModel>.instance != null)
				{
					if (overStepBall)
					{
						if (!replayMode)
						{
							noBallRunUpdateStatus = "wideboundary";
							if (CONTROLLER.canShowReplay)
							{
								Singleton<GameModel>.instance.GameIsOnReplay();
								ShowReplay();
							}
							else
							{
								Singleton<GameModel>.instance.ReplayIsNotShown();
							}
						}
						else
						{
							HideReplay();
							CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball += 5;
							Singleton<GameModel>.instance.UpdateCurrentBall(0, 0, 0, 5, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
							if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
							{
							}
						}
					}
					else if (lineFreeHit)
					{
						Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, 0, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
						lineFreeHit = false;
						lastBowledBall = "lineball";
					}
				}
			}
		}
		else if (boundaryAction == "waitForWideSignal" && stayStartTime + 4f + timeBetweenBalls < Time.time)
		{
			boundaryAction = string.Empty;
			if (Singleton<GameModel>.instance != null)
			{
				if (wideBall)
				{
					CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWideBall += currentBallNoOfRuns;
					Singleton<GameModel>.instance.UpdateCurrentBall(0, 0, 0, currentBallNoOfRuns, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
				}
				else
				{
					CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWideBall += currentBallNoOfRuns;
					Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, 0, currentBallNoOfRuns, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
				}
			}
		}
		if (num3 > groundBannerRadius - 2f && !ballBoundaryReflection && !ballOverTheFence)
		{
			CustomRayCastForBattingBallMovement();
		}
		if (num3 > groundBannerRadius && !ballBoundaryReflection && !ballOverTheFence)
		{
			if (ballTransform.position.y > boundaryFenceHeight)
			{
				ballOverTheFence = true;
			}
			else
			{
				BallRebouncesFromBoundary();
			}
		}
		if (num3 > groundRadius + 15f && !ballBoundaryReflection && ballOverTheFence)
		{
			horizontalSpeed *= 0.2f;
			applyBallFiction = true;
			ballBoundaryReflection = true;
		}
	}

	public string getWicketKeeperStatus()
	{
		return wicketKeeperStatus;
	}

	public bool getOverStepBall()
	{
		return overStepBall;
	}

	public bool getLineFreeHitBall()
	{
		return lineFreeHit;
	}

	public void EnableAllSkinRenderers(bool state)
	{
		fielder10SkinRendererComponent.enabled = state;
		BatsmanSkinRendererComponent.enabled = state;
		RunnerSkinRendererComponent.enabled = state;
		MainUmpireSkinRendererComponent.enabled = state;
		SideUmpireSkinRendererComponent.enabled = state;
		BowlerSkinRendererComponent.enabled = state;
		WicketKeeperSkinRendererComponent.enabled = state;
		Fielder10BallSkinRendererComponent.enabled = state;
		WicketKeeperBallSkinRendererComponent.enabled = state;
		BowlerBallSkinRendererComponent.enabled = state;
		DigitalScreenRendererComponent.enabled = state;
		for (int i = 0; i < fielder.Length; i++)
		{
			if (FielderSkinRendererComponent[i] != null)
			{
				FielderSkinRendererComponent[i].enabled = state;
			}
		}
	}

	public void SetDRSTrailRenderer()
	{
		ActivateColliders(boolean: false);
		Singleton<DRS>.instance.DRSRedTrail.SetActive(value: true);
		Singleton<DRS>.instance.DRSBallTrailMaterial.color = Color.red;
		Singleton<DRS>.instance.DRSRedTrail.GetComponent<TrailRenderer>().time = 15f;
	}

	public void ProcessOnImpact()
	{
		impactBall.SetActive(value: true);
		impactBall.GetComponent<Renderer>().material.mainTexture = ballTextureRenderer.material.mainTexture;
		impactBall.transform.position = ballTransform.position;
		showImpact = true;
		pauseTheBall = false;
		Singleton<DRS>.instance.DRSBallTrailMaterial.color = Color.blue;
		Singleton<DRS>.instance.DRSRedTrail.transform.SetParent(impactBall.transform);
		Singleton<DRS>.instance.DRSRedTrail.transform.localPosition = Vector3.zero;
		Singleton<DRS>.instance.DRSRedTrail.transform.localEulerAngles = Vector3.zero;
		Singleton<DRS>.instance.DRSBlueTrail.SetActive(value: true);
		Singleton<DRS>.instance.DRSBlueTrail.GetComponent<TrailRenderer>().time = 30f;
		CheckDRSImpactOnPad();
	}

	public void waitForReview()
	{
		int myTeamIndex = CONTROLLER.myTeamIndex;
		int opponentTeamIndex = CONTROLLER.opponentTeamIndex;
		if (battingBy == "user" && CONTROLLER.TeamList[myTeamIndex].noofDRSLeft > 0 && umpireOriginalDecision)
		{
			canAiGoForDRS = false;
			drsCalledByBattingTeam = 1;
			drsCalledByUser = 1;
			ShowDRSReplayPanel();
			return;
		}
		if (bowlingBy == "user" && CONTROLLER.TeamList[myTeamIndex].noofDRSLeft > 0 && !umpireOriginalDecision)
		{
			canAiGoForDRS = false;
			drsCalledByBattingTeam = 0;
			drsCalledByUser = 1;
			ShowDRSReplayPanel();
			return;
		}
		pauseTheBall = true;
		canAiGoForDRS = false;
		int num = UnityEngine.Random.Range(0, 50);
		int num2 = UnityEngine.Random.Range(0, 10);
		if (bowlingBy == "user")
		{
			if (!ballInline && Mathf.Abs(ballTransform.position.x) > 0.16f && num2 <= 8)
			{
				num = 45;
			}
		}
		else if (battingBy == "user" && ballInline && Mathf.Abs(ballTransform.position.x) < 0.172f && num2 <= 8)
		{
			num = 45;
		}
		if (bowlingBy == "user")
		{
			drsCalledByBattingTeam = 1;
			drsCalledByUser = 0;
		}
		else if (battingBy == "user")
		{
			drsCalledByBattingTeam = 0;
			drsCalledByUser = 0;
		}
		if (num > 25 && bowlingBy == "user" && CONTROLLER.TeamList[opponentTeamIndex].noofDRSLeft > 0 && umpireOriginalDecision)
		{
			canAiGoForDRS = !ballInline;
			if (num > 30)
			{
				canAiGoForDRS = true;
			}
			if (ballHeightAtStump > 0.75f)
			{
				canAiGoForDRS = true;
			}
			ShowDRSReplayPanel();
		}
		else if (num > 25 && battingBy == "user" && CONTROLLER.TeamList[opponentTeamIndex].noofDRSLeft > 0 && !umpireOriginalDecision)
		{
			canAiGoForDRS = ballInline;
			if (num > 30)
			{
				canAiGoForDRS = true;
			}
			if (ballHeightAtStump < 0.74f)
			{
				canAiGoForDRS = true;
			}
			ShowDRSReplayPanel();
		}
		else
		{
			canShowDRS = false;
			action = 3;
		}
	}

	public bool aiGoForDRS()
	{
		return canAiGoForDRS;
	}

	private void ShowDRSReplayPanel()
	{
		Singleton<DRS>.instance.ShowMe();
	}

	public void showUmpireAfterDrs()
	{
		action = -1;
		Time.timeScale = 1f;
		replayMode = false;
		drsCount = 0;
		Singleton<DRS>.instance.DRSBlueTrail.GetComponent<TrailRenderer>().Clear();
		Singleton<DRS>.instance.DRSRedTrail.GetComponent<TrailRenderer>().Clear();
		Singleton<DRS>.instance.DRSRedTrail.transform.SetParent(ball.transform);
		Singleton<DRS>.instance.DRSRedTrail.transform.localPosition = Vector3.zero;
		Singleton<DRS>.instance.DRSRedTrail.transform.localEulerAngles = Vector3.zero;
		Singleton<DRS>.instance.DRSRedTrail.SetActive(value: false);
		Singleton<DRS>.instance.DRSBlueTrail.SetActive(value: false);
		impactBall.SetActive(value: false);
		int num = 0;
		num = ((drsCalledByBattingTeam == 1) ? ((!(battingBy == "user")) ? CONTROLLER.opponentTeamIndex : CONTROLLER.myTeamIndex) : ((!(battingBy == "user")) ? CONTROLLER.myTeamIndex : CONTROLLER.opponentTeamIndex));
		showMainUmpireForNoBallAction();
		if (drsCalledByBattingTeam == 1)
		{
			if (isOutByDrs == savedLBW)
			{
				MainUmpireAnimationComponent.Play("DRS_Out");
				if (!isUmpiresCall)
				{
					CONTROLLER.TeamList[num].noofDRSLeft--;
				}
				StartCoroutine(playDrsAnimation("DRS_Out", 0, 0));
			}
			else
			{
				MainUmpireAnimationComponent.Play("DRS_SorryNotOut");
				StartCoroutine(playDrsAnimation("DRS_SorryNotOut", 1, 1));
				Singleton<GameModel>.instance.PlayGameSound("Cheer");
			}
		}
		else if (isOutByDrs == savedLBW)
		{
			MainUmpireAnimationComponent.Play("DRS_NotOut");
			if (!PlayerPrefs.HasKey("drs"))
			{
				PlayerPrefs.SetInt("drs", 1);
			}
			if (!isUmpiresCall)
			{
				CONTROLLER.TeamList[num].noofDRSLeft--;
			}
			StartCoroutine(playDrsAnimation("DRS_NotOut", 1, 2));
		}
		else
		{
			MainUmpireAnimationComponent["DRS_SorryOut"].speed = 0.2f;
			MainUmpireAnimationComponent.Play("DRS_SorryOut");
			StartCoroutine(playDrsAnimation("DRS_SorryOut", 0, 3));
			Singleton<GameModel>.instance.PlayGameSound("Cheer");
		}
	}

	private IEnumerator playDrsAnimation(string animName, int status, int animationType)
	{
		float waitTime2 = 0f;
		waitTime2 = ((!MainUmpireAnimationComponent.IsPlaying("DRS_SorryOut")) ? MainUmpireAnimationComponent[animName].length : (MainUmpireAnimationComponent[animName].length / 1.5f));
		yield return new WaitForSeconds(waitTime2);
		if (status == 0)
		{
			drsOut();
		}
		else
		{
			drsNotOut();
		}
		Singleton<GameModel>.instance.ActionTxt.gameObject.SetActive(value: false);
		CONTROLLER.ReplayShowing = false;
		CONTROLLER.canShowReplay = false;
		CONTROLLER.reviewReplay = false;
		Singleton<DRS>.instance.DRSreplay = false;
		isOutByDrs = true;
		drsCalledByBattingTeam = -1;
		drsCalledByUser = -1;
	}

	private void drsOut()
	{
		if (Singleton<GameModel>.instance != null)
		{
			noBallRunUpdateStatus = "drsOut";
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
			Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, 0, 0, CONTROLLER.StrikerIndex, 1, 2, CONTROLLER.CurrentBowlerIndex, 0, CONTROLLER.StrikerIndex, isBoundary: false);
			if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
			{
			}
		}
	}

	private void drsNotOut()
	{
		if (Singleton<GameModel>.instance != null)
		{
			noBallRunUpdateStatus = "drsNotOut";
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
			Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, 0, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
			if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
			{
			}
		}
	}

	public void SetDRSReplayUI()
	{
		if (drsCount == 0)
		{
			isUmpiresCall = false;
			if (savedLBW)
			{
				Singleton<DRS>.instance.ShowDRSResultPanel(0, "OUT", 0);
			}
			else
			{
				Singleton<DRS>.instance.ShowDRSResultPanel(0, "NOT OUT", 1);
			}
			drsCount = 1;
		}
		else if (drsCount == 1)
		{
			if (IsFullTossBall)
			{
				pitching = true;
				if (ballTransform.position.z >= 9.3f)
				{
					drsCount = 2;
				}
			}
			else
			{
				if (ballNoOfBounce <= 0)
				{
					return;
				}
				if (Mathf.Abs(ballTransform.position.x) < 0.14f)
				{
					pitching = true;
					Singleton<DRS>.instance.ShowDRSResultPanel(1, "IN-LINE", 1);
				}
				else if (ballTransform.position.x <= -0.14f)
				{
					if (currentBatsmanHand == "right")
					{
						pitching = true;
						if (IsFullTossBall)
						{
							Singleton<DRS>.instance.ShowDRSResultPanel(1, "FULL TOSS", 1);
						}
						else
						{
							Singleton<DRS>.instance.ShowDRSResultPanel(1, "OUTSIDE OFF", 1);
						}
					}
					else if (!IsFullTossBall)
					{
						pitching = false;
						Singleton<DRS>.instance.ShowDRSResultPanel(1, "OUTSIDE LEG", 0);
					}
					else
					{
						pitching = true;
						if (IsFullTossBall)
						{
							Singleton<DRS>.instance.ShowDRSResultPanel(1, "FULL TOSS", 1);
						}
						else
						{
							Singleton<DRS>.instance.ShowDRSResultPanel(1, "OUTSIDE OFF", 1);
						}
					}
				}
				else if (ballTransform.position.x >= 0.14f)
				{
					if (currentBatsmanHand == "left")
					{
						pitching = true;
						if (IsFullTossBall)
						{
							Singleton<DRS>.instance.ShowDRSResultPanel(1, "FULL TOSS", 1);
						}
						else
						{
							Singleton<DRS>.instance.ShowDRSResultPanel(1, "OUTSIDE OFF", 1);
						}
					}
					else if (!IsFullTossBall)
					{
						pitching = false;
						Singleton<DRS>.instance.ShowDRSResultPanel(1, "OUTSIDE LEG", 0);
					}
					else
					{
						pitching = true;
						if (IsFullTossBall)
						{
							Singleton<DRS>.instance.ShowDRSResultPanel(1, "FULL TOSS", 1);
						}
						else
						{
							Singleton<DRS>.instance.ShowDRSResultPanel(1, "OUTSIDE OFF", 1);
						}
					}
				}
				if (!pitching)
				{
					bDRSPitchingOutsideLeg = true;
					drsCount = 4;
					isOutByDrs = false;
				}
				else
				{
					drsCount = 2;
				}
			}
		}
		else
		{
			if (drsCount != 2 || !(ballTransform.position.z >= 9.92f))
			{
				return;
			}
			if (ballTransform.position.y <= 0.775f && Mathf.Abs(ballTransform.position.x) < 0.13f)
			{
				hitting = true;
				Singleton<DRS>.instance.ShowDRSResultPanel(3, "HITTING", 1);
			}
			else if (ballTransform.position.y <= 0.817f && Mathf.Abs(ballTransform.position.x) <= 0.171f)
			{
				Singleton<DRS>.instance.ShowDRSResultPanel(3, "UMPIRE'S CALL", 2);
				if (savedLBW)
				{
					hitting = true;
				}
				else
				{
					hitting = false;
				}
				isUmpiresCall = true;
				isOutByDrs = hitting;
			}
			else
			{
				isOutByDrs = false;
				hitting = false;
				Singleton<DRS>.instance.ShowDRSResultPanel(3, "MISSING", 0);
			}
			drsCount = 3;
		}
	}

	public void CheckDRSImpactOnPad()
	{
		impactOffSideWithAttemptedShot = false;
		if (!showImpact || !isImpactedDuringReplay)
		{
			return;
		}
		if (Mathf.Abs(impactBall.transform.position.x) < 0.12f)
		{
			impacting = true;
			Singleton<DRS>.instance.ShowDRSResultPanel(2, "IN-LINE", 1);
		}
		else if (impactBall.transform.position.x <= -0.1f)
		{
			if (currentBatsmanHand == "right")
			{
				Singleton<DRS>.instance.ShowDRSResultPanel(2, "OUTSIDE OFF", 0);
				if (batsmanAnimation != "bt6Leave")
				{
					impacting = false;
					impactOffSideWithAttemptedShot = true;
					Singleton<DRS>.instance.ShowDRSResultPanel(4, string.Empty, 0);
				}
				else
				{
					impacting = true;
				}
			}
			else if (impactBall.transform.position.x <= -0.16f)
			{
				impacting = false;
				Singleton<DRS>.instance.ShowDRSResultPanel(2, "OUTSIDE LEG", 0);
			}
			else if (impactBall.transform.position.x < -0.1f && impactBall.transform.position.x > -0.16f)
			{
				Singleton<DRS>.instance.ShowDRSResultPanel(2, "UMPIRE'S CALL", 2);
				umpireDecision = true;
				impacting = true;
			}
		}
		else if (impactBall.transform.position.x >= 0.1f)
		{
			if (currentBatsmanHand == "left")
			{
				Singleton<DRS>.instance.ShowDRSResultPanel(2, "OUTSIDE OFF", 0);
				impacting = false;
				if (batsmanAnimation != "bt6Leave")
				{
					impacting = false;
					impactOffSideWithAttemptedShot = true;
					Singleton<DRS>.instance.ShowDRSResultPanel(4, string.Empty, 0);
				}
				else
				{
					impacting = true;
				}
			}
			else if (impactBall.transform.position.x >= 0.16f)
			{
				impacting = false;
				Singleton<DRS>.instance.ShowDRSResultPanel(2, "OUTSIDE LEG", 0);
			}
			else if (impactBall.transform.position.x > 0.1f && impactBall.transform.position.x < 0.16f)
			{
				Singleton<DRS>.instance.ShowDRSResultPanel(2, "UMPIRE'S CALL", 2);
				umpireDecision = true;
				impacting = true;
			}
		}
		if (!impacting)
		{
			isOutByDrs = false;
			if (!umpireDecision)
			{
			}
		}
	}

	public IEnumerator updateBoundaryBall()
	{
		bool isMatchOver = checkForMatchComplete(runsScoredInLineNoBall + 1, 0);
		if (CONTROLLER.matchType == "oneday" && !isMatchOver)
		{
			yield return new WaitForSeconds(4f);
		}
		else
		{
			yield return new WaitForSeconds(2f);
		}
		CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
		Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, runsScoredInLineNoBall, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: true);
		if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
		{
		}
	}

	private void BallRebouncesFromBoundary()
	{
		ballAngle = ballAngle + 180f + (float)UnityEngine.Random.Range(-20, 20);
		ballAngle %= 360f;
		horizontalSpeed *= 0.2f;
		boundaryHeight = ballTransform.position.y;
		ballProjectileAnglePerSecond *= 1.5f;
		applyBallFiction = true;
		ballBoundaryReflection = true;
	}

	private bool isBatsmanRunOut()
	{
		if (batsmanLeftLegEdge.transform.position.z < creaseEdge && batsmanLeftLegEdge.transform.position.z > creaseEdge * -1f && batsmanRightLegEdge.transform.position.z < creaseEdge && batsmanRightLegEdge.transform.position.z > creaseEdge * -1f && batEdgeGO.transform.position.z < creaseEdge && batEdgeGO.transform.position.z > creaseEdge * -1f)
		{
			return true;
		}
		return false;
	}

	private void RunnerActions()
	{
		if (thirdUmpireRunoutReplaySkipped)
		{
			return;
		}
		if (nonStrikerStatus == "getReady" && (ballTransform.position.z > stump1Crease.transform.position.z || ballStatus == "shotSuccess"))
		{
			strikerStatus = "backToCrease";
			nonStrikerStatus = "backToCrease";
			RunnerAnimationComponent.Play("BackToCreaseNew");
		}
		if (nonStrikerStatus == "run" && canTakeRun)
		{
			if (currentBallNoOfRuns % 2 == 0)
			{
				if (batsmanTransform.position.z > 8.8f || batsmanTransform.position.z < 0f)
				{
					Singleton<BattingControls>.instance.hideCancelBtn();
				}
				else if (!CancelRun && !Singleton<GameModel>.instance.isGamePaused)
				{
					showRunInterface(boolean: false);
				}
			}
			else if (batsmanTransform.position.z < -8.8f || batsmanTransform.position.z > 0f)
			{
				Singleton<BattingControls>.instance.hideCancelBtn();
			}
			else if (!CancelRun && !Singleton<GameModel>.instance.isGamePaused)
			{
				showRunInterface(boolean: false);
			}
		}
		else if (nonStrikerStatus == "reachTheCrease")
		{
			if (!CancelRun && canTakeRun)
			{
				showRunInterface(boolean: true);
			}
			if (savedIsRunOut)
			{
				disableRunCancelBtn();
			}
		}
		if (replayMode && savedTakeRunTimingArray.Count > 0 && savedTakeRunTimingArray.Count > currentBallNoOfRuns && (nonStrikerStatus == "backToCrease" || nonStrikerStatus == "comeToHalt") && saveRunAgainArray[currentBallNoOfRuns, saveCount] != 0f)
		{
			float num = saveRunAgainArray[currentBallNoOfRuns, saveCount];
			if (Time.time - ballConnectedTiming >= num)
			{
				takeRun = true;
				if (saveRunAgainArray.Length > saveCount)
				{
					saveCount++;
					CancelRun = false;
				}
			}
		}
		else if (replayMode && cancelRunArray.Length > 0 && nonStrikerStatus == "run" && cancelRunArray[currentBallNoOfRuns, cancelCount] != 0f)
		{
			float num2 = cancelRunArray[currentBallNoOfRuns, cancelCount];
			if (Time.time - ballConnectedTiming >= num2)
			{
				if (!callCancelRunFunc)
				{
					cancelRunningBetweenWicket();
					callCancelRunFunc = true;
				}
				if (cancelRunArray.Length > cancelCount)
				{
					CancelRun = true;
					callCancelRunFunc = false;
					cancelCount++;
				}
			}
		}
		if (canTakeRun && takeRun && (nonStrikerStatus == "backToCrease" || nonStrikerStatus == "comeToHalt"))
		{
			int num3 = ((CONTROLLER.BattingTeamIndex != CONTROLLER.opponentTeamIndex) ? 1 : 0);
			takeRun = false;
			takingRun = true;
			runOut = true;
			nonStrikerSpeed = 6.5f * (1f + agilityFactor * (float)num3);
			strikerSpeed = 6.5f * (1f + agilityFactor * (float)num3);
			strikerStatus = "run";
			nonStrikerStatus = "run";
			if (replayMode && savedTakeRunTimingArray.Count - currentBallNoOfRuns == 1)
			{
				if (savedIsRunOut)
				{
					nonStrikerSpeed = 6.3f * (1f + agilityFactor * (float)num3);
					strikerSpeed = 6.3f * (1f + agilityFactor * (float)num3);
				}
				else
				{
					nonStrikerSpeed = 6.7f * (1f + agilityFactor * (float)num3);
					strikerSpeed = 6.7f * (1f + agilityFactor * (float)num3);
				}
			}
			if (!replayMode && !CancelRun)
			{
				savedTakeRunTimingArray.Add(Time.time - ballConnectedTiming);
				saveRunAgainArray[currentBallNoOfRuns, saveRunAgainCount] = Time.time - ballConnectedTiming;
				saveRunAgainCount++;
			}
			if (!CancelRun)
			{
				if (currentBallNoOfRuns % 2 == 0)
				{
					striker = batsman;
					nonStriker = runner;
					striker.transform.eulerAngles = new Vector3(striker.transform.eulerAngles.x, 180f, striker.transform.eulerAngles.z);
					nonStriker.transform.eulerAngles = new Vector3(nonStriker.transform.eulerAngles.x, 0f, nonStriker.transform.eulerAngles.z);
					strikerRunningAngle = AngleBetweenTwoGameObjects(striker, RHBStrikerRunningSpot);
					if (bowlerSide == "left")
					{
						nonStrikerRunningAngle = AngleBetweenTwoVector3(nonStriker.transform.position, runnerNonStrikerRunningSpot.transform.position);
					}
					else if (bowlerSide == "right")
					{
						tempSpot1 = runnerNonStrikerRunningSpot.transform.position;
						tempSpot1 = new Vector3(tempSpot1.x * -1f, tempSpot1.y, tempSpot1.z);
						nonStrikerRunningAngle = AngleBetweenTwoVector3(nonStriker.transform.position, tempSpot1);
					}
				}
				else
				{
					striker = runner;
					nonStriker = batsman;
					striker.transform.eulerAngles = new Vector3(striker.transform.eulerAngles.x, 180f, striker.transform.eulerAngles.z);
					nonStriker.transform.eulerAngles = new Vector3(nonStriker.transform.eulerAngles.x, 0f, nonStriker.transform.eulerAngles.z);
					if (bowlerSide == "left")
					{
						strikerRunningAngle = AngleBetweenTwoVector3(striker.transform.position, runnerStrikerRunningSpot.transform.position);
					}
					else if (bowlerSide == "right")
					{
						Vector3 position = runnerStrikerRunningSpot.transform.position;
						strikerRunningAngle = AngleBetweenTwoVector3(v2: new Vector3(position.x * -1f, position.y, position.z), v1: striker.transform.position);
					}
					nonStrikerRunningAngle = AngleBetweenTwoGameObjects(nonStriker, RHBNonStrikerRunningSpot);
				}
			}
			StrikerAnimationComponent = striker.GetComponent<Animation>();
			NonStrikerAnimationComponent = nonStriker.GetComponent<Animation>();
			StrikerAnimationComponent.Play("run");
			NonStrikerAnimationComponent.Play("run");
			NonStrikerAnimationComponent["run"].speed = 1f;
			StrikerAnimationComponent["run"].speed = 1f;
			if (currentBallNoOfRuns == 0)
			{
				if (!replayMode)
				{
					showPreviewCamera(status: true);
					previewCamera.rect = new Rect(0.02f, 0.72f, 0.25f, 0.25f);
				}
				if (moveMainUmpire)
				{
					mainUmpireTransform.localScale = new Vector3(umpireRunDirection, mainUmpireTransform.localScale.x, mainUmpireTransform.localScale.z);
					MainUmpireAnimationComponent.Play("UmpireRun_New");
					MainUmpireAnimationComponent["UmpireRun_New"].speed = 2f;
					moveMainUmpire = false;
				}
			}
		}
		else if (nonStrikerStatus == "run" || nonStrikerStatus == "reachTheCrease")
		{
			nonStriker.transform.position += new Vector3(Mathf.Cos(nonStrikerRunningAngle * DEG2RAD) * nonStrikerSpeed * (float)cancelRunDirectionFactor * Time.deltaTime, 0f, Mathf.Sin(nonStrikerRunningAngle * DEG2RAD) * nonStrikerSpeed * (float)cancelRunDirectionFactor * Time.deltaTime);
			if (!CancelRun)
			{
				if (nonStriker.transform.position.z > nonStrikerNearCreaseSpot.transform.position.z && (nonStrikerStatus == "run" || nonStrikerStatus == "reachTheCrease"))
				{
					if (currentBallNoOfRuns >= 3)
					{
						canTakeRun = false;
						if (Singleton<GameModel>.instance != null)
						{
							disableRunCancelBtn();
						}
					}
					Singleton<GameModel>.instance.ReachedCrease();
					nonStrikerStatus = "reachTheCrease";
					NonStrikerAnimationComponent.CrossFade("reachTheCrease");
					NonStrikerAnimationComponent["reachTheCrease"].speed = 1f;
				}
			}
			else if (nonStriker.transform.position.z < strikerNearCreaseSpot.transform.position.z && nonStrikerStatus == "run")
			{
				Singleton<GameModel>.instance.ReachedCrease();
				nonStrikerStatus = "reachTheCrease";
				NonStrikerAnimationComponent.CrossFade("reachTheCrease");
				NonStrikerAnimationComponent["reachTheCrease"].speed = 1f;
			}
			float num4;
			if (CancelRun)
			{
				num4 = strikerReachSpot.transform.position.z;
			}
			else
			{
				num4 = nonStrikerReachSpot.transform.position.z;
				if (nonStriker == batsman)
				{
					num4 = 7.28f;
				}
			}
			if (nonStriker.transform.position.z < num4 && CancelRun)
			{
				runOut = false;
				if (isRunOut || !ballOnboundaryLine)
				{
				}
				nonStrikerStatus = "comeToHalt";
			}
			else if (nonStriker.transform.position.z > num4 && !CancelRun)
			{
				runOut = false;
				if (!isRunOut && !ballOnboundaryLine)
				{
					currentBallNoOfRuns++;
					if ((CONTROLLER.currentInnings == 1 && CONTROLLER.PlayModeSelected != 7) || CONTROLLER.PlayModeSelected == 5 || (CONTROLLER.PlayModeSelected == 7 && CONTROLLER.currentInnings == 3))
					{
						targetToWin--;
						if (targetToWin <= 0 && !replayMode)
						{
							canTakeRun = false;
							takeRun = false;
							disableRunCancelBtn();
						}
					}
					cancelRunCount = 0;
					saveRunAgainCount = 0;
					saveCount = 0;
					cancelCount = 0;
				}
				nonStrikerStatus = "comeToHalt";
				if (battingBy == "computer" && canTakeRun && !ballPickedByFielder && !edgeCatch && !computerBatsmanNewRunAttempt)
				{
					if (!canTakeRun)
					{
						nonStrikerStatus = "comeToHalt";
						computerBatsmanNewRunAttempt = true;
						return;
					}
					takeRun = true;
				}
			}
		}
		else if (nonStrikerStatus == "comeToHalt")
		{
			nonStriker.transform.position += new Vector3(Mathf.Cos(nonStrikerRunningAngle * DEG2RAD) * nonStrikerSpeed * (float)cancelRunDirectionFactor * Time.deltaTime, 0f, Mathf.Sin(nonStrikerRunningAngle * DEG2RAD) * nonStrikerSpeed * (float)cancelRunDirectionFactor * Time.deltaTime);
			nonStrikerSpeed *= (100f - 70f * Time.deltaTime) / 100f;
			if (nonStrikerSpeed < 3f && nonStrikerSpeed != 0f)
			{
				CancelRun = false;
				cancelRunDirectionFactor = 1;
				nonStrikerSpeed = 0f;
				NonStrikerAnimationComponent.CrossFade("idle");
			}
		}
		if (strikerStatus == "run" || strikerStatus == "reachTheCrease")
		{
			striker.transform.position += new Vector3(Mathf.Cos(strikerRunningAngle * DEG2RAD) * strikerSpeed * (float)cancelRunDirectionFactor * Time.deltaTime, 0f, Mathf.Sin(strikerRunningAngle * DEG2RAD) * strikerSpeed * (float)cancelRunDirectionFactor * Time.deltaTime);
			if (!CancelRun)
			{
				if (striker.transform.position.z < strikerNearCreaseSpot.transform.position.z && strikerStatus == "run")
				{
					strikerStatus = "reachTheCrease";
					StrikerAnimationComponent.CrossFade("reachTheCrease");
					StrikerAnimationComponent["reachTheCrease"].speed = 1f;
				}
			}
			else if (striker.transform.position.z > nonStrikerNearCreaseSpot.transform.position.z && strikerStatus == "run")
			{
				strikerStatus = "reachTheCrease";
				StrikerAnimationComponent.CrossFade("reachTheCrease");
				StrikerAnimationComponent["reachTheCrease"].speed = 1f;
			}
			if (striker.transform.position.z > nonStrikerReachSpot.transform.position.z && CancelRun)
			{
				strikerStatus = "comeToHalt";
			}
			else if (striker.transform.position.z < strikerReachSpot.transform.position.z && !CancelRun)
			{
				strikerStatus = "comeToHalt";
			}
		}
		else
		{
			if (!(strikerStatus == "comeToHalt"))
			{
				return;
			}
			striker.transform.position += new Vector3(Mathf.Cos(strikerRunningAngle * DEG2RAD) * strikerSpeed * (float)cancelRunDirectionFactor * Time.deltaTime, 0f, Mathf.Sin(strikerRunningAngle * DEG2RAD) * strikerSpeed * (float)cancelRunDirectionFactor * Time.deltaTime);
			strikerSpeed *= (100f - 70f * Time.deltaTime) / 100f;
			if (strikerSpeed < 3f && strikerSpeed != 0f)
			{
				strikerSpeed = 0f;
				if (canTakeRun && !ballOnboundaryLine && DistanceBetweenTwoGameObjects(ball, throwToGO) > 3f)
				{
					showRunInterface(boolean: true);
				}
				StrikerAnimationComponent.CrossFade("idle");
			}
		}
	}

	private void cancelRunningBetweenWicket()
	{
		disableRunCancelBtn();
		if (!replayMode)
		{
			cancelRunArray[currentBallNoOfRuns, cancelRunCount] = Time.time - ballConnectedTiming;
			cancelRunCount++;
		}
		if (currentBallNoOfRuns % 2 == 0)
		{
			striker.transform.eulerAngles = new Vector3(striker.transform.eulerAngles.x, 0f, striker.transform.eulerAngles.z);
			nonStriker.transform.eulerAngles = new Vector3(nonStriker.transform.eulerAngles.x, 180f, nonStriker.transform.eulerAngles.z);
			cancelRunDirectionFactor = -1;
			strikerRunningAngle = AngleBetweenTwoGameObjects(striker, RHBStrikerRunningSpot);
			if (bowlerSide == "left")
			{
				nonStrikerRunningAngle = AngleBetweenTwoVector3(nonStriker.transform.position, runnerNonStrikerRunningSpot.transform.position);
			}
			else if (bowlerSide == "right")
			{
				tempSpot1 = runnerNonStrikerRunningSpot.transform.position;
				tempSpot1 = new Vector3(tempSpot1.x * -1f, tempSpot1.y, tempSpot1.z);
				nonStrikerRunningAngle = AngleBetweenTwoVector3(nonStriker.transform.position, tempSpot1);
			}
		}
		else
		{
			striker.transform.eulerAngles = new Vector3(striker.transform.eulerAngles.x, 0f, striker.transform.eulerAngles.z);
			nonStriker.transform.eulerAngles = new Vector3(nonStriker.transform.eulerAngles.x, 180f, nonStriker.transform.eulerAngles.z);
			cancelRunDirectionFactor = -1;
			if (bowlerSide == "left")
			{
				strikerRunningAngle = AngleBetweenTwoVector3(striker.transform.position, runnerStrikerRunningSpot.transform.position);
			}
			else if (bowlerSide == "right")
			{
				tempSpot2 = runnerStrikerRunningSpot.transform.position;
				tempSpot2 = new Vector3(tempSpot2.x * -1f, tempSpot2.y, tempSpot2.z);
				strikerRunningAngle = AngleBetweenTwoVector3(striker.transform.position, tempSpot2);
			}
			nonStrikerRunningAngle = AngleBetweenTwoGameObjects(nonStriker, RHBNonStrikerRunningSpot);
		}
	}

	private void showRunInterface(bool boolean)
	{
		if (battingBy != "computer")
		{
			Singleton<BattingControls>.instance.Hide(boolean: false);
		}
		if (boolean)
		{
			Singleton<GameModel>.instance.EnableRun(boolean);
			Singleton<GameModel>.instance.EnableCancelRun(!boolean);
		}
		else
		{
			Singleton<GameModel>.instance.EnableCancelRun(!boolean);
			Singleton<GameModel>.instance.EnableRun(boolean);
		}
	}

	private void disableRunCancelBtn()
	{
		Singleton<GameModel>.instance.EnableRun(boolean: false);
		Singleton<GameModel>.instance.EnableCancelRun(boolean: false);
	}

	public void touchCancelRun()
	{
		if (nonStrikerStatus == "run")
		{
			if (currentBallNoOfRuns % 2 == 0)
			{
				if (batsmanTransform.position.z > 8.8f || batsmanTransform.position.z < 0f)
				{
					return;
				}
			}
			else if (batsmanTransform.position.z < -8.8f || batsmanTransform.position.z > 0f)
			{
				return;
			}
		}
		if (nonStrikerStatus == "run" && !CancelRun && !ballOnboundaryLine)
		{
			CancelRun = true;
			cancelRunningBetweenWicket();
		}
	}

	private void GetInputs()
	{
		GetBattingInput();
		StartCoroutine(BowlerSideChange("key"));
	}

	public void ActivateBowlerSideChangeViaUI()
	{
		if (bowlerSide == "left")
		{
			StartCoroutine(BowlerSideChange("right"));
		}
		else if (bowlerSide == "right")
		{
			StartCoroutine(BowlerSideChange("left"));
		}
	}

	public void RestartBowlerSide(string from)
	{
		bowlerSide = from;
		SetBowlerSide();
	}

	public IEnumerator BowlerSideChange(string from)
	{
		if (!(bowlingBy == "user") || !bowlerIsWaiting)
		{
			yield break;
		}
		float fadeInOutTime = 0.2f;
		if (Input.GetKeyDown(KeyCode.LeftArrow) || from == "left")
		{
			if (bowlerSide == "right")
			{
				yield return new WaitForSeconds(fadeInOutTime);
				bowlerSide = "left";
				SetBowlerSide();
				if (currentBowlerHand == "right")
				{
					Singleton<Scoreboard>.instance.UpdateStripText("Bowling over the wicket");
				}
				else if (currentBowlerHand == "left")
				{
					Singleton<Scoreboard>.instance.UpdateStripText("Bowling round the wicket");
				}
				yield return new WaitForSeconds(fadeInOutTime);
			}
		}
		else if ((Input.GetKeyDown(KeyCode.RightArrow) || from == "right") && bowlerSide == "left")
		{
			yield return new WaitForSeconds(fadeInOutTime);
			bowlerSide = "right";
			SetBowlerSide();
			if (currentBowlerHand == "right")
			{
				Singleton<Scoreboard>.instance.UpdateStripText("Bowling round the wicket");
			}
			else if (currentBowlerHand == "left")
			{
				Singleton<Scoreboard>.instance.UpdateStripText("Bowling over the wicket");
			}
			yield return new WaitForSeconds(fadeInOutTime);
		}
	}

	private void UpdateShadowsAndPreview()
	{
		UpdateBallShadow();
		UpdatePreview();
		if (showShadows)
		{
			UpdateShadow();
		}
	}

	private void Update()
	{
		if (tutorialArrow.activeSelf)
		{
			tutorialArrow.transform.eulerAngles = new Vector3(0f, 0f, 0f);
		}
		if (!CONTROLLER.GameIsOnFocus)
		{
			return;
		}
		if (IsFullTossBall && bowlingSpotFullTossGO.transform.localPosition.y > 0.58f)
		{
			bowlingSpotFullTossGO.transform.position = new Vector3(bowlingSpotFullTossGO.transform.position.x, 0.56f, bowlingSpotFullTossGO.transform.position.z);
		}
		if (IsFullTossBall && (double)bowlingSpotFullTossGO.transform.localPosition.x > 1.2)
		{
			bowlingSpotFullTossGO.transform.position = new Vector3(1.2f, bowlingSpotFullTossGO.transform.position.y, bowlingSpotFullTossGO.transform.position.z);
		}
		if (IsFullTossBall && (double)bowlingSpotFullTossGO.transform.localPosition.x < -1.02)
		{
			bowlingSpotFullTossGO.transform.position = new Vector3(-1.02f, bowlingSpotFullTossGO.transform.position.y, bowlingSpotFullTossGO.transform.position.z);
		}
		if (IsFullTossBall && (double)bowlingSpotTransform.localPosition.z < 9.3 && CONTROLLER.BowlingTeamIndex == CONTROLLER.opponentTeamIndex && bowlingSpotFullTossGO.activeInHierarchy)
		{
			ShowFullTossSpot(_Value: false);
			bowlingSpotGO.SetActive(value: true);
			if (battingBy == "user")
			{
				bowlingSpotRenderer.enabled = false;
				if (tutorialArrow != null)
				{
					tutorialArrow.SetActive(value: false);
				}
			}
			else
			{
				bowlingSpotRenderer.enabled = true;
				if (tutorialArrow != null)
				{
					tutorialArrow.SetActive(value: true);
				}
			}
		}
		GetInputs();
		switch (action)
		{
		case -2:
			ZoomCameraToBowler();
			UpdateShadowsAndPreview();
			if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
			{
				Singleton<GameModel>.instance.CanPauseGame = false;
			}
			else if (!CONTROLLER.ReplayShowing)
			{
				Singleton<GameModel>.instance.CanPauseGame = true;
			}
			else
			{
				Singleton<GameModel>.instance.CanPauseGame = false;
			}
			break;
		case -1:
			UpdateShadowsAndPreview();
			break;
		case -11:
			RotateUltraMotionCameraBatsmanCelebration();
			break;
		case -10:
			UpdateShadowsAndPreview();
			break;
		case 0:
			BatsmanWaiting();
			UpdateShadowsAndPreview();
			break;
		case 1:
			BowlerWaiting();
			break;
		case 2:
			enableBowlingSpot();
			UserChangingBowlingSpot();
			checkForLineNoBall();
			UpdateShadowsAndPreview();
			break;
		case 3:
			Action3Functions();
			break;
		case 4:
			Action4Functions();
			break;
		case 22:
			moveFielders();
			break;
		}
		ReplayCameraMovement();
		CheckForHotspotPosition();
	}

	private void WarmUpOnce()
	{
		int num = action;
		action = 3;
		Action3Functions();
		action = 4;
		Action4Functions();
		action = num;
	}

	private void Action3Functions()
	{
		WicketKeeperPreBattingActions();
		BowlingBallMovement();
		FindBatsmanCanMakeShot();
		ExecuteTheShot();
		RunnerActions();
		ActivateBowler();
		LookForMainCameraTopDownView();
		ScanForBoundaryOrSix();
		UpdateShadowsAndPreview();
	}

	private void Action4Functions()
	{
		if (edgeCatch)
		{
			WicketKeeperPreBattingActions();
		}
		else
		{
			WicketKeeperPostBattingActions();
		}
		BattingBallMovement();
		ActivateFielders();
		ActivateBowler();
		LookForRunByComputerBatsman();
		ThrowingBallMovement();
		RunnerActions();
		LookForMainCameraTopDownView();
		ScanForBoundaryOrSix();
		UpdateShadowsAndPreview();
	}

	public void RotateUltraMotionCameraBatsmanCelebration()
	{
		ultraMotionCamTransform.LookAt(new Vector3(batsmanRefPoint.position.x, batsmanRefPoint.position.y - 1f, batsmanRefPoint.position.z));
		if (currentBatsmanHand == "right")
		{
			ultraMotionCamTransform.RotateAround(batsmanTransform.position, -Vector3.down, 20f * Time.deltaTime);
		}
		else
		{
			ultraMotionCamTransform.RotateAround(batsmanTransform.position, Vector3.up, 20f * Time.deltaTime);
		}
	}

	public void batsmanCelebration(string celebration)
	{
		showPreviewCamera(status: false);
		rightSideCamera.enabled = false;
		leftSideCamera.enabled = false;
		umpireCamera.enabled = false;
		introCamera.enabled = false;
		replayCamera.enabled = false;
		mainCamera.enabled = false;
		ultraMotionCamera.enabled = true;
		ultraMotionCamTransform.position = new Vector3(ultraMotionCamTransform.position.x, 1f, batsmanTransform.position.z);
		if (currentBatsmanHand == "right")
		{
			ultraMotionCamTransform.position = new Vector3(-9f, ultraMotionCamTransform.position.y, ultraMotionCamTransform.position.z);
			batsmanTransform.eulerAngles = new Vector3(batsmanTransform.eulerAngles.x, 270f, batsmanTransform.eulerAngles.z);
		}
		else
		{
			ultraMotionCamTransform.position = new Vector3(12f, ultraMotionCamTransform.position.y, ultraMotionCamTransform.position.z);
			batsmanTransform.eulerAngles = new Vector3(batsmanTransform.eulerAngles.x, 90f, batsmanTransform.eulerAngles.z);
		}
		if (Singleton<GameModel>.instance.stateVar == 2)
		{
			ultraMotionCamTransform.position = new Vector3(ultraMotionCamTransform.position.x, 2.5f, ultraMotionCamTransform.position.z);
		}
		loadCelebrationBatsman(celebration);
	}

	private void loadCelebrationBatsman(string celebration)
	{
		MainUmpireAnimationComponent.Play("Idle");
		SideUmpireAnimationComponent.Play("Idle");
		if (celebration == "halfcentury")
		{
			batsmanAnimationComponent.Play("FiftyCelebration");
		}
		else if (celebration == "century")
		{
			batsmanAnimationComponent.Play("HundredCelebration");
		}
		action = -11;
	}

	public void destroyCelebrationBatsman()
	{
		mainCameraTransform.position = new Vector3(-30f, 6.8f, 0f);
		mainCameraTransform.eulerAngles = new Vector3(10f, 90f, 0f);
		mainCamera.fieldOfView = 45f;
		mainCamera.enabled = true;
		ultraMotionCamera.enabled = false;
		BatsmanSkinRendererComponent.enabled = true;
	}

	private void OnCustomTriggerEnter(Collider other)
	{
		if (DRSHardcode)
		{
			if (!replayMode || !(savedSummary != "onPads"))
			{
				ballStatus = "onPads";
				RunnerAnimationComponent.Play("BackToCreaseNew");
				if (batsmanConfidenceLevel)
				{
					DecreaseConfidenceLevel(ballStatus);
				}
				ActivateColliders(boolean: false);
				ActivateStadiumAndSkybox(boolean: true);
				if (batsmanAnimation != string.Empty)
				{
					batsmanAnimationComponent[batsmanAnimation].speed = 1f;
				}
				HideBowlingSpot();
				ShowFullTossSpot(_Value: false);
				if (!replayMode)
				{
					savedSummary = "onPads";
					savedPadCollider = other;
					ballAngle = ballAngle + 180f + (float)UnityEngine.Random.Range(-20, 20);
					savedBallAngleAfterHittingPads = ballAngle;
					savedBallRayCastConnectedZposition = ballRayCastReferenceGOTransform.position.z;
				}
				else
				{
					ballAngle = savedBallAngleAfterHittingPads;
				}
				ballAngle %= 360f;
				horizontalSpeed *= 0.05f;
				ballProjectileAnglePerSecond *= 1.5f;
				ballProjectileHeight *= 0.5f;
				applyBallFiction = true;
				lbwAppeal = true;
				if (currentBowlerType == "spin")
				{
					canActivateBowler = false;
					ShowFielder10(fielder10Status: true, ball10Status: false);
					ShowBowler(showStatus: false);
				}
				if (!replayMode)
				{
					savedLbwAppeal = lbwAppeal;
				}
				BowlerAnimationComponent["Blitz_" + currentBowlerType + "PaceBowling0" + BowlerAnimNumber].speed = 3f;
				if (currentBowlerType == "fast" && (overStepBall || !lineFreeHit))
				{
					WicketKeeperAnimationComponent.Play("appealFast");
				}
				if (currentBowlerType == "medium" && (overStepBall || !lineFreeHit))
				{
					WicketKeeperAnimationComponent.Play("appealFast");
				}
				else if (currentBowlerType == "spin" && (overStepBall || !lineFreeHit))
				{
					WicketKeeperAnimationComponent.Play("appealSpin");
				}
				makeFieldersToCelebrate(null);
				if (Singleton<GameModel>.instance != null && !replayMode)
				{
					Singleton<GameModel>.instance.PlayGameSound("Cheer");
				}
				if (ballNoOfBounce == 0 && Mathf.Abs(ballTransform.position.x) <= 0.1f)
				{
					ballInline = true;
				}
				if (ballTransform.position.y < 0.6f && Mathf.Abs(ballSpotAtStump.transform.position.x) < 0.12f && ballInline)
				{
					LBW = true;
				}
				if (!replayMode)
				{
					savedLBW = LBW;
				}
				else if (replayMode)
				{
					LBW = savedLBW;
				}
				umpireOriginalDecision = LBW;
			}
			return;
		}
		if ((other.gameObject.name == "BatCollider" || other.gameObject.name == "BatCollider2") && ballStatus == "bowling" && ballStatus != "onPads" && battingBy == "user")
		{
			ballHitTheBall = true;
		}
		if (((other.gameObject.name == "BatCollider" || other.gameObject.name == "BatCollider2") && ballStatus == "bowling" && ballStatus != "onPads") || (replayMode && savedSummary != "onPads" && savedSummary != "bowled" && savedBallRayCastConnectedZposition != 0f))
		{
			HideBowlingSpot();
			ShowFullTossSpot(_Value: false);
			if (shotPlayed != "bt6Leave" && shotPlayed != string.Empty)
			{
				if (replayMode && savedSummary != "connected" && savedSummary != "catch" && savedSummary != "picked")
				{
					return;
				}
				if (!replayMode)
				{
					savedSummary = "connected";
					savedBallConnectedPosition = ballTransform.position;
					ballSpinningSpeedInZ = UnityEngine.Random.Range(-3600, -1800);
					savedBallConnectedSpinningSpeedInZ = ballSpinningSpeedInZ;
				}
				else if (replayMode)
				{
					ballTransform.position = savedBallConnectedPosition;
					ballSpinningSpeedInZ = savedBallConnectedSpinningSpeedInZ;
				}
				BowlerAnimationComponent["Blitz_" + currentBowlerType + "PaceBowling0" + BowlerAnimNumber].speed = 3f;
				ballTimingOriginTransform.position = new Vector3(ballTransform.position.x, ballTimingOriginTransform.position.y, ballTransform.position.z);
				ballBatMeetingHeight = ballTransform.position.y;
				ballNoOfBounce = 0;
				ballStatus = "shotSuccess";
				ActivateColliders(boolean: false);
				ActivateStadiumAndSkybox(boolean: true);
				boardCollider.SetActive(value: true);
				if (shotPlayed != "bt6Defense" && shotPlayed != "backFootDefenseHighBall" && shotPlayed != "frontFootOffSideDefense")
				{
					canTakeRun = true;
					if (Singleton<GameModel>.instance != null)
					{
						Singleton<GameModel>.instance.EnableRun(boolean: true);
					}
				}
				batsmanAnimationComponent[batsmanAnimation].speed = 1f;
				BallTiming();
				if (edgeCatch && DistanceBetweenTwoGameObjects(wicketKeeper, ball) < 13.5f)
				{
					canKeeperCollectBall = true;
				}
				else if (!StopKeeper)
				{
					MoveWicketKeeperToStumps();
				}
				GetFieldersAngle();
				GetFieldersDistance();
				if (!StopKeeper)
				{
					SetActiveFielders();
				}
				if (ballAngle >= 90f && ballAngle <= 270f)
				{
					umpireRunDirection = -1;
				}
				else
				{
					umpireRunDirection = 1;
				}
				ballHitTheBall = true;
				action = 4;
			}
		}
		else if ((other.gameObject.name == "Stump1Collider" && ballStatus == "bowling") || (replayMode && savedSummary == "bowled"))
		{
			if (replayMode && savedSummary != "bowled")
			{
				return;
			}
			ActivateColliders(boolean: false);
			ActivateStadiumAndSkybox(boolean: true);
			ballStatus = "bowled";
			if (!replayMode)
			{
				savedSummary = "bowled";
				savedBallRayCastConnectedZposition = ballRayCastReferenceGOTransform.position.z;
			}
			else if (replayMode)
			{
				replayActionStatus = "bowledSlowDown";
			}
			float x = ballTransform.position.x;
			StumpAnimation(stump1, x);
			if (Singleton<GameModel>.instance != null && !replayMode)
			{
				Singleton<GameModel>.instance.PlayGameSound("Bowled");
				Singleton<GameModel>.instance.PlayGameSound("Cheer");
			}
			batsmanAnimationComponent[batsmanAnimation].speed = 1f;
			BowlerAnimationComponent["Blitz_" + currentBowlerType + "PaceBowling0" + BowlerAnimNumber].speed = 3f;
			if (overStepBall || lineFreeHit)
			{
				MainUmpireAnimationComponent.CrossFade("Crouch_toNotOut_New");
			}
			else
			{
				MainUmpireAnimationComponent.Play("Out");
			}
			ZoomCameraToUmpire();
			makeFieldersToCelebrate(null);
		}
		else if (other.gameObject.name == "Stump2Collider" && ballStatus == "shotSuccess")
		{
			float x2 = ballTransform.position.x;
			StumpAnimation(stump2, x2);
			if (Singleton<GameModel>.instance != null && !replayMode)
			{
				Singleton<GameModel>.instance.PlayGameSound("Bowled");
			}
		}
		else if (other.gameObject.name == "Board" && ballStatus == "shotSuccess")
		{
			BallRebouncesFromBoundary();
		}
		else if (ballStatus == "bowling" && (other.gameObject.name == "LeftLowerLeg" || other.gameObject.name == "RightLowerLeg" || other.gameObject.name == "LeftUpperLeg" || other.gameObject.name == "RightUpperLeg" || (replayMode && savedSummary == "onPads")))
		{
			if (replayMode && savedSummary != "onPads")
			{
				return;
			}
			ballStatus = "onPads";
			RunnerAnimationComponent.Play("BackToCreaseNew");
			if (batsmanConfidenceLevel)
			{
				DecreaseConfidenceLevel(ballStatus);
			}
			ActivateColliders(boolean: false);
			ActivateStadiumAndSkybox(boolean: true);
			if (batsmanAnimation != string.Empty)
			{
				batsmanAnimationComponent[batsmanAnimation].speed = 1f;
			}
			HideBowlingSpot();
			ShowFullTossSpot(_Value: false);
			if (!replayMode)
			{
				savedSummary = "onPads";
				savedPadCollider = other;
				ballAngle = ballAngle + 180f + (float)UnityEngine.Random.Range(-20, 20);
				savedBallAngleAfterHittingPads = ballAngle;
				savedBallRayCastConnectedZposition = ballRayCastReferenceGOTransform.position.z;
			}
			else
			{
				ballAngle = savedBallAngleAfterHittingPads;
			}
			ballAngle %= 360f;
			horizontalSpeed *= 0.05f;
			ballProjectileAnglePerSecond *= 1.5f;
			ballProjectileHeight *= 0.5f;
			applyBallFiction = true;
			if ((ballTransform.position.y < 0.7f && Mathf.Abs(ballSpotAtStump.transform.position.x) < 0.2f) || (replayMode && savedLbwAppeal))
			{
				lbwAppeal = true;
				if (currentBowlerType == "spin")
				{
					canActivateBowler = false;
					ShowFielder10(fielder10Status: true, ball10Status: false);
					ShowBowler(showStatus: false);
				}
				if (!replayMode)
				{
					savedLbwAppeal = lbwAppeal;
				}
				BowlerAnimationComponent["Blitz_" + currentBowlerType + "PaceBowling0" + BowlerAnimNumber].speed = 3f;
				if (currentBowlerType == "fast" && (overStepBall || !lineFreeHit))
				{
					WicketKeeperAnimationComponent.Play("appealFast");
				}
				if (currentBowlerType == "medium" && (overStepBall || !lineFreeHit))
				{
					WicketKeeperAnimationComponent.Play("appealFast");
				}
				else if (currentBowlerType == "spin" && (overStepBall || !lineFreeHit))
				{
					WicketKeeperAnimationComponent.Play("appealSpin");
				}
				makeFieldersToCelebrate(null);
				if (Singleton<GameModel>.instance != null && !replayMode)
				{
					Singleton<GameModel>.instance.PlayGameSound("Cheer");
				}
				if (ballNoOfBounce == 0 && Mathf.Abs(ballTransform.position.x) <= 0.1f)
				{
					ballInline = true;
				}
				if (ballTransform.position.y < 0.6f && Mathf.Abs(ballSpotAtStump.transform.position.x) < 0.12f && ballInline)
				{
					LBW = true;
				}
				if (!replayMode)
				{
					savedLBW = LBW;
				}
				else if (replayMode)
				{
					LBW = savedLBW;
				}
				umpireOriginalDecision = LBW;
			}
			else
			{
				WicketKeeperAnimationComponent.Play("waitForBall");
				WicketKeeperAnimationComponent["waitForBall"].time = WicketKeeperAnimationComponent["waitForBall"].length;
				WicketKeeperAnimationComponent["waitForBall"].speed = -1f;
			}
		}
		CanShowCountDown = false;
	}

	private void StumpAnimation(GameObject stump, float contactPoint)
	{
		string empty = string.Empty;
		if (!replayMode)
		{
			empty = ((currentBowlerType != "medium") ? ((contactPoint < -0.06f) ? (currentBowlerType + "OffStump") : ((contactPoint > 0.06f) ? (currentBowlerType + "LegStump") : ((!(Mathf.Abs(contactPoint) < 0.03f)) ? "allThreeStumps" : (currentBowlerType + "MidStump")))) : ((contactPoint < -0.06f) ? "fastOffStump" : ((contactPoint > 0.06f) ? "fastLegStump" : ((!(Mathf.Abs(contactPoint) < 0.03f)) ? "allThreeStumps" : "fastMidStump"))));
			stump.GetComponent<Animation>().Play(empty);
			savedStumpAnimationToPlay = empty;
		}
		else if (replayMode)
		{
			stump.GetComponent<Animation>().Play(savedStumpAnimationToPlay);
		}
	}

	private void ActivateColliders(bool boolean)
	{
		batCollider.SetActive(boolean);
		batCollider2.SetActive(boolean);
		leftLowerLegObject.SetActive(boolean);
		rightLowerLegObject.SetActive(boolean);
		leftUpperLegObject.SetActive(boolean);
		rightUpperLegObject.SetActive(boolean);
		stump1Collider.SetActive(boolean);
	}

	public void GameIsPaused(bool pauseStatus)
	{
		if (pauseStatus)
		{
			gamePausedTimeScale = Time.timeScale;
			Time.timeScale = 0f;
			ActivateColliders(boolean: false);
			return;
		}
		gamePaused = true;
		if (!CONTROLLER.isQuit)
		{
			if (!CanShowCountDown)
			{
				pauseCountdown();
			}
			else if (!CONTROLLER.isFromAutoPlay)
			{
				EnablePauseCountDown();
			}
			else
			{
				CONTROLLER.isFromAutoPlay = false;
				ResetAll();
				EnablePauseCountDown();
			}
		}
		ActivateColliders(boolean: true);
	}

	private void pauseCountdown()
	{
		if (gamePaused)
		{
			Time.timeScale = gamePausedTimeScale;
		}
		else
		{
			Time.timeScale = 1f;
		}
		gamePaused = false;
		destroyCountDown();
		Singleton<Tutorial>.instance.setBoolean();
	}

	private void ActivateReplayCamera()
	{
		replayCamera.enabled = true;
		rightSideCamera.enabled = false;
		leftSideCamera.enabled = false;
		showPreviewCamera(status: false);
		umpireCamera.enabled = false;
		closeUpCamera.enabled = false;
		mainCamera.enabled = false;
	}

	public void ShowReplay()
	{
		if (CONTROLLER.reviewReplay)
		{
			ReviewReplay();
			return;
		}
		destroyCelebrationBatsman();
		replayMode = true;
		replayCameraScript.distance = 10f;
		ResetAll();
		ActivateReplayCamera();
		Time.timeScale = 1f;
		replayCameraRotationAngleInBoundary = 5 + UnityEngine.Random.Range(-2, 3) * 10;
		shotPlayed = savedShotPlayed;
		StartBowling();
		ActivateStadiumAndSkybox(boolean: true);
		if (!CONTROLLER.stumpingAttempted)
		{
			ReplayLookAtBowler();
		}
		else
		{
			replayActionStatus = "StumpingLookAt";
		}
	}

	private void ReviewReplay()
	{
		replayMode = true;
		ResetAll();
		Time.timeScale = 1f;
		shotPlayed = savedShotPlayed;
		StartBowling();
		ActivateStadiumAndSkybox(boolean: true);
	}

	public void SetReplayCamera()
	{
		int num = 0;
		num++;
		if (num > 5)
		{
			num = 1;
		}
		AssignTrace(string.Empty + num);
		float z = fielder10Transform.position.z;
		if (currentBatsmanHand == "right")
		{
			replayCamera.gameObject.transform.position = new Vector3(10f, 4.5f, z + 15.5f);
			replayCameraTransform.LookAt(fielder10Transform.position);
		}
		else
		{
			replayCamera.gameObject.transform.position = new Vector3(-10f, 4.5f, z + 15.5f);
			replayCameraTransform.LookAt(fielder10Transform.position);
		}
	}

	private void ReplayLookAtBowler()
	{
		Debug.LogError("ReplayLookAtBowler");
		replayActionStatus = "lookAt";
		replayControllerTransform.eulerAngles = new Vector3(replayControllerTransform.eulerAngles.x, 0f, replayControllerTransform.eulerAngles.z);
		replayCameraScript.enabled = false;
		if (currentBowlerType == "fast")
		{
			replayControllerTransform.position = new Vector3(0f, 2.3f, -24.9f);
			iTween.MoveTo(replayController, iTween.Hash("position", new Vector3(0f, 2.3f, -8.88f), "time", 3.2f, "easetype", "easeInOutSine", "delay", 0.5f));
		}
		else if (currentBowlerType == "spin")
		{
			replayControllerTransform.position = new Vector3(0f, 2.3f, -12f);
			iTween.MoveTo(replayController, iTween.Hash("position", new Vector3(0f, 2.3f, -8.88f), "time", 4, "easetype", "easeInOutSine", "delay", 2.5f));
		}
		else if (currentBowlerType == "medium")
		{
			replayControllerTransform.position = new Vector3(0f, 2.3f, -16f);
			iTween.MoveTo(replayController, iTween.Hash("position", new Vector3(0f, 2.3f, -8.88f), "time", 4, "easetype", "easeInOutSine", "delay", 2.5f));
		}
		if (!overStepBall)
		{
			if (bowlerSide == "left")
			{
				replayCameraTransform.position = new Vector3(-6f, 2.3f, 0f);
				if (UnityEngine.Random.Range(-10f, 10f) > 0f)
				{
					if (currentBowlerType == "spin")
					{
						iTween.MoveTo(replayCamera.gameObject, iTween.Hash("position", new Vector3(-6f, 5.3f, -5f), "time", 4, "easetype", "easeInOutSine", "delay", 2));
					}
					else if (currentBowlerType == "fast")
					{
						replayCameraTransform.position = new Vector3(-16f, 1.3f, -37f);
						iTween.MoveTo(replayCamera.gameObject, iTween.Hash("position", new Vector3(-10f, 2.3f, -15f), "time", 3.2f, "easetype", "easeInOutSine", "delay", 0.5f));
					}
					else if (currentBowlerType == "medium")
					{
						iTween.MoveTo(replayCamera.gameObject, iTween.Hash("position", new Vector3(-6f, 5.3f, -5f), "time", 4, "easetype", "easeInOutSine", "delay", 2));
					}
				}
			}
			else if (bowlerSide == "right")
			{
				replayCameraTransform.position = new Vector3(6f, 2.3f, 0f);
				if (UnityEngine.Random.Range(-10f, 10f) > 0f)
				{
					if (currentBowlerType == "spin")
					{
						iTween.MoveTo(replayCamera.gameObject, iTween.Hash("position", new Vector3(6f, 5.3f, -5f), "time", 4, "easetype", "easeInOutSine", "delay", 2));
					}
					else if (currentBowlerType == "fast")
					{
						replayCameraTransform.position = new Vector3(16f, 1.3f, -37f);
						iTween.MoveTo(replayCamera.gameObject, iTween.Hash("position", new Vector3(10f, 2.3f, -15f), "time", 3.2, "easetype", "easeInOutSine", "delay", 0.5));
					}
					else if (currentBowlerType == "medium")
					{
						iTween.MoveTo(replayCamera.gameObject, iTween.Hash("position", new Vector3(6f, 5.3f, -5f), "time", 4, "easetype", "easeInOutSine", "delay", 2));
					}
				}
			}
		}
		else if (bowlerSide == "left")
		{
			replayCameraTransform.position = new Vector3(-11f, 2.3f, -8.8f);
			if (UnityEngine.Random.Range(-10f, 10f) > 0f)
			{
				replayCameraTransform.position = new Vector3(-11f, 2.3f, -8.8f);
				if (UnityEngine.Random.Range(-10f, 10f) > 0f)
				{
					if (currentBowlerType == "spin")
					{
						iTween.MoveTo(replayCamera.gameObject, iTween.Hash("position", new Vector3(-10f, 2.3f, -8.8f), "time", 4, "easetype", "easeInOutSine", "delay", 2));
					}
					else if (currentBowlerType == "fast")
					{
						replayCameraTransform.position = new Vector3(-11f, 1.3f, -10f);
						iTween.MoveTo(replayCamera.gameObject, iTween.Hash("position", new Vector3(-10f, 2.3f, -8.8f), "time", 3, "easetype", "easeInOutSine", "delay", 0.5f));
					}
					else if (currentBowlerType == "medium")
					{
						iTween.MoveTo(replayCamera.gameObject, iTween.Hash("position", new Vector3(-10f, 2.3f, -8.8f), "time", 4, "easetype", "easeInOutSine", "delay", 2));
					}
				}
			}
		}
		else if (bowlerSide == "right")
		{
			replayCameraTransform.position = new Vector3(11f, 2.3f, -8.8f);
			if (UnityEngine.Random.Range(-10f, 10f) > 0f)
			{
				if (currentBowlerType == "spin")
				{
					iTween.MoveTo(replayCamera.gameObject, iTween.Hash("position", new Vector3(10f, 2.3f, -8.8f), "time", 4, "easetype", "easeInOutSine", "delay", 2));
				}
				else if (currentBowlerType == "fast")
				{
					replayCameraTransform.position = new Vector3(11f, 1.3f, -10f);
					iTween.MoveTo(replayCamera.gameObject, iTween.Hash("position", new Vector3(10f, 2.3f, -8.8f), "time", 3, "easetype", "easeInOutSine", "delay", 0.5f));
				}
				else if (currentBowlerType == "medium")
				{
					iTween.MoveTo(replayCamera.gameObject, iTween.Hash("position", new Vector3(10f, 2.3f, -8.8f), "time", 4, "easetype", "easeInOutSine", "delay", 2));
				}
			}
		}
		replayCamera.gameObject.transform.LookAt(replayControllerTransform);
	}

	public void BowlNextBall(string batting, string bowling)
	{
		int num = 0;
		if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
		{
			num = 1;
		}
		if (!warmUpOnce)
		{
			WarmUpOnce();
			warmUpOnce = true;
		}
		wideBall = false;
		playIntro = false;
		newInning = false;
		replayCamera.enabled = false;
		Time.timeScale = 1f;
		Singleton<BattingControls>.instance.updateBatsmanTiming(GetBattingAbility());
		AIHitInGap = true;
		takeRun = false;
		savedRunOutAppeal = false;
		savedSummary = string.Empty;
		savedPickedUpFielderIndex = -1;
		savedIsRunOut = false;
		savedCurrentBallNoOfRuns = 0;
		savedBallRayCastConnectedZposition = 0f;
		savedLbwAppeal = false;
		savedLBW = false;
		savedThrowAction = string.Empty;
		savedThrowTo = string.Empty;
		savedStumpAnimationToPlay = string.Empty;
		savedPowerShotStatus = false;
		savedIsStumped = false;
		savedReturnToCreaseAnimationId = 0;
		HideBatShadow();
		SetDefaultDigitalDisplayContent();
		wideWithStumpingSignalShown = false;
		CONTROLLER.runoutThirdUmpireAppeal = false;
		tightRunoutCall = false;
		veryTightRunoutCall = false;
		thirdUmpireRunoutReplaySkipped = false;
		afterReplayUpdateRunForRunoutFailedAttempt = false;
		limitReplayCameraHeight = false;
		battingBy = batting;
		bowlingBy = bowling;
		currentBatsmanHand = CONTROLLER.StrikerHand;
		currentBowlerHand = CONTROLLER.BowlerHand;
		fieldRestriction = CONTROLLER.PowerPlay;
		hattrickBall = CONTROLLER.HattrickBall;
		spinValue = 0f;
		bowlerIsWaiting = false;
		if (bowlingBy == "computer" && CONTROLLER.PlayModeSelected != 6)
		{
			LookToChangeBowlerSideInBetweenTheOver();
		}
		Vector3 vector = new Vector3(0f, 0f, 0f);
		vector = ((!(battingBy == "user") || !(CONTROLLER.difficultyMode == "hard")) ? new Vector3(2f * (1f + powerFactor * (float)num * 5f), 1.5f * (1f + powerFactor * (float)num * 5f), 2f * (1f + powerFactor * (float)num * 5f)) : new Vector3(1f * (1f + powerFactor * (float)num * 5f), 1.2f * (1f + powerFactor * (float)num * 5f), 1f * (1f + powerFactor * (float)num * 5f)));
		batCollider.transform.localScale = vector;
		batCollider2.transform.localScale = Vector3.one;
		if (CONTROLLER.PlayModeSelected == 6)
		{
			CONTROLLER.BowlerHand = (currentBowlerHand = Multiplayer.oversData[CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls / 6].bowlerHand);
			bowlerSide = Multiplayer.oversData[CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls / 6].bowlerSide;
			if (Multiplayer.oversData[CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls / 6].bowlerType == "fast")
			{
				CONTROLLER.BowlerType = 0;
			}
			else
			{
				CONTROLLER.BowlerType = 3;
			}
		}
		if (CONTROLLER.PlayModeSelected == 4 && CONTROLLER.SuperOverMode == "bat")
		{
			if (CONTROLLER.LevelId % 2 == 0)
			{
				currentBowlerType = "fast";
			}
			else
			{
				currentBowlerType = "spin";
			}
		}
		else if (CONTROLLER.PlayModeSelected == 5)
		{
			if (!PlayerPrefs.HasKey("CTBowler"))
			{
				if (randomBowler == 0)
				{
					currentBowlerType = "fast";
				}
				else if (randomBowler == 1)
				{
					currentBowlerType = "spin";
				}
				else if (randomBowler == 2)
				{
					currentBowlerType = "medium";
				}
				PlayerPrefs.SetString("CTBowler", currentBowlerType);
			}
			else
			{
				currentBowlerType = PlayerPrefs.GetString("CTBowler");
			}
		}
		else if (CONTROLLER.BowlerType == 0)
		{
			currentBowlerType = "fast";
		}
		else if (CONTROLLER.BowlerType == 1 || CONTROLLER.BowlerType == 2)
		{
			currentBowlerType = "spin";
			currentBowlerSpinType = CONTROLLER.BowlerType;
			if (currentBowlerHand == "left")
			{
				if (currentBowlerSpinType == 1)
				{
					currentBowlerSpinType = 2;
				}
				else if (currentBowlerSpinType == 2)
				{
					currentBowlerSpinType = 1;
				}
			}
		}
		else if (CONTROLLER.BowlerType == 3)
		{
			currentBowlerType = "medium";
		}
		if (batsmanConfidenceLevel && CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex && !fieldRestriction)
		{
			if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].ConfidenceVal > 7f)
			{
				SetComputerFieldIndex(UnityEngine.Random.Range(8, 11));
			}
			else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].ConfidenceVal > 5f)
			{
				SetComputerFieldIndex(UnityEngine.Random.Range(6, 8));
			}
			else
			{
				SetComputerFieldIndex(UnityEngine.Random.Range(1, 6));
			}
		}
		if (bowlingBy == "computer" && ballToFineLeg && (currentBallNoOfRuns == 6 || currentBallNoOfRuns == 4))
		{
			if (fieldRestriction)
			{
				SetComputerFieldIndex(1);
			}
			else if (!fieldRestriction)
			{
				SetComputerFieldIndex(8);
			}
		}
		if (Singleton<AIFieldingSetupManager>.instance.enabled && CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
		{
			CONTROLLER.computerFielderChangeIndex = Singleton<AIFieldingSetupManager>.instance.GetAIFieldingIndex();
		}
		if (CONTROLLER.computerFielderChangeIndex != Singleton<AIFieldingSetupManager>.instance.previousFieldSet && CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
		{
			Singleton<PreviewScreen>.instance.alertPopup.SetActive(value: true);
		}
		if (noBall)
		{
			StartCoroutine(freehitscenario());
		}
		else
		{
			ResetAll();
		}
		if (CONTROLLER.cameraType == 0)
		{
			ActivateStadiumAndSkybox(boolean: false);
		}
		if (battingBy == "user" && CONTROLLER.PlayModeSelected == 6)
		{
			BatsmenInfo batsmanList = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList;
			if (batsmanList.BattingHand == "L")
			{
				bowlerSide = "right";
			}
			else if (batsmanList.BattingHand == "R")
			{
				bowlerSide = "left";
			}
			RestartBowlerSide(bowlerSide);
		}
	}

	private void SetComputerFieldIndex(int _index)
	{
		if (CONTROLLER.noBallFacedBatsmanId != CONTROLLER.StrikerIndex || !lineFreeHit)
		{
			CONTROLLER.computerFielderChangeIndex = _index;
		}
	}

	private void LookToChangeBowlerSideInBetweenTheOver()
	{
		int num = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls % 6;
		string empty = string.Empty;
		if (num <= 1 || !(bowlingBy == "computer"))
		{
			return;
		}
		int num2 = 0;
		for (int i = num - 2; i < num; i++)
		{
			empty = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].ballUpdate[i];
			if (empty == "4" || empty == "6")
			{
				num2++;
			}
		}
		if (num2 == 2)
		{
			if (bowlerSide == "left")
			{
				bowlerSide = "right";
			}
			else
			{
				bowlerSide = "left";
			}
		}
	}

	public void NewOver()
	{
		if (PlayerPrefs.HasKey("CTBowler"))
		{
			PlayerPrefs.DeleteKey("CTBowler");
		}
		if (ObscuredPrefs.HasKey("bowlerBowlStraight" + CONTROLLER.PlayModeSelected))
		{
			ObscuredPrefs.DeleteKey("bowlerBowlStraight" + CONTROLLER.PlayModeSelected);
		}
		if (ObscuredPrefs.HasKey("stumpBallCount" + CONTROLLER.PlayModeSelected))
		{
			ObscuredPrefs.DeleteKey("stumpBallCount" + CONTROLLER.PlayModeSelected);
		}
		randomBowler = UnityEngine.Random.Range(1, 3);
		if (bowlingBy == "computer")
		{
			if (UnityEngine.Random.Range(0f, 10f) > 5f)
			{
				bowlerSide = "right";
			}
			else
			{
				bowlerSide = "left";
			}
		}
		canShowPartnerShip = true;
		int num = mainUmpireIndex;
		mainUmpireIndex = sideUmpireIndex;
		sideUmpireIndex = num;
		if (CONTROLLER.PlayModeSelected != 7)
		{
			MainUmpireSkinRendererComponent.materials[0].SetTexture("_PatternTex", umpireTexture[mainUmpireIndex]);
			SideUmpireSkinRendererComponent.materials[0].SetTexture("_PatternTex", umpireTexture[sideUmpireIndex]);
		}
		ShowAllPlayers();
	}

	private IEnumerator freehitscenario()
	{
		stayStartTime = Time.time;
		showPreviewCamera(status: false);
		mainCamera.enabled = false;
		rightSideCamera.enabled = false;
		leftSideCamera.enabled = false;
		closeUpCamera.enabled = false;
		umpireCamera.enabled = true;
		if (currentBallNoOfRuns == 0 || currentBallNoOfRuns == 4 || currentBallNoOfRuns == 6)
		{
			umpireRunDirection = 0;
		}
		if (umpireRunDirection == 1)
		{
			umpireCameraTransform.position = stump2Crease.transform.position;
			umpireCameraTransform.position = new Vector3(umpireCameraTransform.position.x, 1.5f, umpireCameraTransform.position.z);
			mainUmpireTransform.position = umpireLeftSideSpot.transform.position;
			mainUmpireTransform.eulerAngles = new Vector3(mainUmpireTransform.eulerAngles.x, 90f, mainUmpireTransform.eulerAngles.z);
			umpireCameraTransform.eulerAngles = new Vector3(umpireCameraTransform.eulerAngles.x, 270f, umpireCameraTransform.eulerAngles.z);
			umpireCameraTransform.position -= new Vector3(1f, 0f, 0f);
			umpireCamera.fieldOfView = 40f;
		}
		else if (umpireRunDirection == -1)
		{
			umpireCameraTransform.position = stump2Crease.transform.position;
			umpireCameraTransform.position = new Vector3(umpireCameraTransform.position.x, 1.5f, umpireCameraTransform.position.z);
			mainUmpireTransform.localScale = new Vector3(1f, mainUmpireTransform.localScale.y, mainUmpireTransform.localScale.z);
			mainUmpireTransform.position = umpireRightSideSpot.transform.position;
			mainUmpireTransform.eulerAngles = new Vector3(mainUmpireTransform.eulerAngles.x, 270f, mainUmpireTransform.eulerAngles.z);
			umpireCameraTransform.eulerAngles = new Vector3(umpireCameraTransform.eulerAngles.x, 90f, umpireCameraTransform.eulerAngles.z);
			umpireCameraTransform.position += new Vector3(1f, 0f, 0f);
			umpireCamera.fieldOfView = 40f;
		}
		else
		{
			umpireCameraTransform.position = mainUmpireInitPosition;
			umpireCameraTransform.position = new Vector3(umpireCameraTransform.position.x, 2f, umpireCameraTransform.position.z);
			umpireCameraTransform.position += new Vector3(0f, 0f, 3f);
			umpireCameraTransform.eulerAngles = new Vector3(5 + UnityEngine.Random.Range(0, 5), 180f, umpireCameraTransform.eulerAngles.z);
			umpireCamera.fieldOfView = 40f;
		}
		MainUmpireAnimationComponent.Play("NoBallFreeHit_New");
		Singleton<GameModel>.instance.InitAnimation(5);
		noBall = false;
		freeHit = true;
		CONTROLLER.isFreeHit = true;
		isFreeHit = true;
		yield return new WaitForSeconds(2f);
		ResetAll();
		Singleton<GameModel>.instance.CanPauseGame = true;
		Singleton<Scoreboard>.instance.HidePause(boolean: false);
	}

	public void UpdatePreview()
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary.Add("Striker", batsmanTransform.position);
		dictionary.Add("NonStriker", runnerTransform.position);
		dictionary.Add("field_01", fielderTransform[1].position);
		dictionary.Add("field_02", fielderTransform[2].position);
		dictionary.Add("field_03", fielderTransform[3].position);
		dictionary.Add("field_04", fielderTransform[4].position);
		dictionary.Add("field_05", fielderTransform[5].position);
		dictionary.Add("field_06", fielderTransform[6].position);
		dictionary.Add("field_07", fielderTransform[7].position);
		dictionary.Add("field_08", fielderTransform[8].position);
		dictionary.Add("field_09", fielderTransform[9].position);
		if (showShadows)
		{
			dictionary.Add("field_10", ShadowsArrayTransform[9].position);
		}
		if (ballReleased)
		{
			dictionary.Add("Ball", ballTransform.position);
		}
		else if (showShadows)
		{
			dictionary.Add("Ball", ShadowsArrayTransform[9].position);
		}
		if (showShadows)
		{
			dictionary.Add("field_11", ShadowsArrayTransform[10].position);
		}
		if (Singleton<GameModel>.instance != null)
		{
			Singleton<GameModel>.instance.UpdatePreview(dictionary);
		}
	}

	public void EnableFielders(bool boolean)
	{
		for (int i = 0; i < fielder.Length; i++)
		{
			if (fielder[i] != null)
			{
				FielderSkinRendererComponent[i].enabled = boolean;
			}
		}
		RunnerSkinRendererComponent.enabled = boolean;
	}

	public void initGameIntro()
	{
		introCameraPivot.transform.eulerAngles = new Vector3(0f, 0f, 0f);
		introCameraTransform.parent = introCameraPivot.transform;
		introCameraTransform.localPosition = new Vector3(115f, 80f, 0f);
		introCameraTransform.localEulerAngles = new Vector3(40f, -90f, introCameraTransform.localEulerAngles.z);
		introCamera.fieldOfView = 40f;
		closeUpCamera.enabled = false;
		rightSideCamera.enabled = false;
		leftSideCamera.enabled = false;
		showPreviewCamera(status: false);
		umpireCamera.enabled = false;
		replayCamera.enabled = false;
		mainCamera.enabled = false;
		introCamera.enabled = true;
		showPreviewCamera(status: false);
	}

	public void introCompleted()
	{
		BatsmanSkinRendererComponent.enabled = true;
		RunnerSkinRendererComponent.enabled = true;
		playIntro = false;
		introCamera.enabled = false;
		mainCamera.enabled = true;
		EnableFielders(boolean: true);
		StopIntroFielderAnimation();
	}

	public void initBatsmanExit()
	{
		closeUpCamera.enabled = false;
		rightSideCamera.enabled = false;
		leftSideCamera.enabled = false;
		showPreviewCamera(status: false);
		umpireCamera.enabled = false;
		replayCamera.enabled = false;
		mainCamera.enabled = false;
		introCamera.enabled = true;
		batsmanAnimationComponent.Play("WCCLite_BatsmanIdle");
		RunnerAnimationComponent.Play("WCCLite_RunnerIdle");
	}

	public void disableCamOnShowScore()
	{
		introCamera.enabled = false;
		mainCamera.enabled = true;
	}

	private void CheckAndHideShadow(GameObject targetGameObject, int arrayIndex)
	{
		if (targetGameObject != null && !targetGameObject.GetComponent<Renderer>().enabled)
		{
			tempShadowPosition = ShadowsArrayTransform[arrayIndex].position;
			tempShadowPosition.y = -100f;
			ShadowsArrayTransform[arrayIndex].position = tempShadowPosition;
		}
	}

	private void UpdateShadow()
	{
		ShadowsArrayTransform[0].position = new Vector3(ShadowRefArrayTransform[0].position.x, shadowY, ShadowRefArrayTransform[0].position.z);
		CheckAndHideShadow(fielderSkin[1], 0);
		ShadowsArrayTransform[1].position = new Vector3(ShadowRefArrayTransform[1].position.x, shadowY, ShadowRefArrayTransform[1].position.z);
		CheckAndHideShadow(fielderSkin[2], 1);
		ShadowsArrayTransform[2].position = new Vector3(ShadowRefArrayTransform[2].position.x, shadowY, ShadowRefArrayTransform[2].position.z);
		CheckAndHideShadow(fielderSkin[3], 2);
		ShadowsArrayTransform[3].position = new Vector3(ShadowRefArrayTransform[3].position.x, shadowY, ShadowRefArrayTransform[3].position.z);
		CheckAndHideShadow(fielderSkin[4], 3);
		ShadowsArrayTransform[4].position = new Vector3(ShadowRefArrayTransform[4].position.x, shadowY, ShadowRefArrayTransform[4].position.z);
		CheckAndHideShadow(fielderSkin[5], 4);
		ShadowsArrayTransform[5].position = new Vector3(ShadowRefArrayTransform[5].position.x, shadowY, ShadowRefArrayTransform[5].position.z);
		CheckAndHideShadow(fielderSkin[6], 5);
		ShadowsArrayTransform[6].position = new Vector3(ShadowRefArrayTransform[6].position.x, shadowY, ShadowRefArrayTransform[6].position.z);
		CheckAndHideShadow(fielderSkin[7], 6);
		ShadowsArrayTransform[7].position = new Vector3(ShadowRefArrayTransform[7].position.x, shadowY, ShadowRefArrayTransform[7].position.z);
		CheckAndHideShadow(fielderSkin[8], 7);
		ShadowsArrayTransform[8].position = new Vector3(ShadowRefArrayTransform[8].position.x, shadowY, ShadowRefArrayTransform[8].position.z);
		CheckAndHideShadow(fielderSkin[9], 8);
		if (!canActivateBowler)
		{
			ShadowsArrayTransform[9].position = new Vector3(bowlerShadowRefTransform.position.x, shadowY, bowlerShadowRefTransform.position.z);
			CheckAndHideShadow(bowlerSkin, 9);
		}
		else if (canActivateBowler && (fielder10SkinRendererComponent.enabled || Singleton<BallSimulationManager>.instance.showingBallSimulation))
		{
			ShadowsArrayTransform[9].position = new Vector3(ShadowRefArrayTransform[9].position.x, shadowY, ShadowRefArrayTransform[9].position.z);
			CheckAndHideShadow(fielder10Skin, 9);
		}
		ShadowsArrayTransform[10].position = new Vector3(ShadowRefArrayTransform[10].position.x, shadowY, ShadowRefArrayTransform[10].position.z);
		CheckAndHideShadow(wicketKeeperSkin, 10);
		ShadowsArrayTransform[11].position = new Vector3(ShadowRefArrayTransform[11].position.x, shadowY, ShadowRefArrayTransform[11].position.z);
		CheckAndHideShadow(batsmanSkin, 11);
		ShadowsArrayTransform[12].position = new Vector3(ShadowRefArrayTransform[12].position.x, shadowY, ShadowRefArrayTransform[12].position.z);
		CheckAndHideShadow(runnerSkin, 12);
		ShadowsArrayTransform[13].position = new Vector3(ShadowRefArrayTransform[13].position.x, shadowY, ShadowRefArrayTransform[13].position.z);
		CheckAndHideShadow(mainUmpireSkin, 13);
		ShadowsArrayTransform[14].position = new Vector3(ShadowRefArrayTransform[14].position.x, shadowY, ShadowRefArrayTransform[14].position.z);
		CheckAndHideShadow(sideUmpireSkin, 14);
	}

	public void MoveLeftSide(bool boolean)
	{
		if (boolean)
		{
			if (currentBatsmanHand == "right")
			{
				leftArrowKeyDown = true;
			}
			else
			{
				rightArrowKeyDown = true;
			}
		}
		else if (currentBatsmanHand == "right")
		{
			leftArrowKeyDown = false;
		}
		else
		{
			rightArrowKeyDown = false;
		}
	}

	public void MoveRightSide(bool boolean)
	{
		if (boolean)
		{
			if (currentBatsmanHand == "right")
			{
				rightArrowKeyDown = true;
			}
			else
			{
				leftArrowKeyDown = true;
			}
		}
		else if (currentBatsmanHand == "right")
		{
			rightArrowKeyDown = false;
		}
		else
		{
			leftArrowKeyDown = false;
		}
	}

	public void ShotSelected(bool isPower, int SelectedAngle)
	{
		powerKeyDown = isPower;
		touchDeviceShotInput = true;
		if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
		{
			updateBattingTimingMeterNeedle = false;
			if (CONTROLLER.PlayModeSelected != 6)
			{
				Singleton<BattingControls>.instance.battingMeter.SetActive(value: true);
				Singleton<BattingControls>.instance.GreedyAdsImage.SetActive(value: false);
			}
		}
		leftArrowKeyDown = false;
		downArrowKeyDown = false;
		upArrowKeyDown = false;
		rightArrowKeyDown = false;
		switch (SelectedAngle)
		{
		case 1:
			downArrowKeyDown = true;
			break;
		case 2:
			leftArrowKeyDown = true;
			downArrowKeyDown = true;
			break;
		case 3:
			leftArrowKeyDown = true;
			break;
		case 4:
			upArrowKeyDown = true;
			leftArrowKeyDown = true;
			break;
		case 6:
			upArrowKeyDown = true;
			rightArrowKeyDown = true;
			break;
		case 7:
			rightArrowKeyDown = true;
			break;
		case 8:
			rightArrowKeyDown = true;
			downArrowKeyDown = true;
			break;
		}
		if (currentBatsmanHand == "left")
		{
			if (leftArrowKeyDown)
			{
				leftArrowKeyDown = false;
				rightArrowKeyDown = true;
			}
			else if (rightArrowKeyDown)
			{
				leftArrowKeyDown = true;
				rightArrowKeyDown = false;
			}
		}
	}

	public void InitRun(bool boolean)
	{
		takeRun = boolean;
	}

	public void ActivateStadiumAndSkybox(bool boolean)
	{
		hideableObjects.SetActive(boolean);
	}

	private void DecreaseConfidenceLevel(string confidenceStatus)
	{
		float confidenceVal = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].ConfidenceVal;
		float confidenceDec = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].ConfidenceDec;
		confidenceDec = ((CONTROLLER.totalOvers >= 10) ? (confidenceDec * (10f / (float)CONTROLLER.totalOvers)) : (confidenceDec * (2f / (float)CONTROLLER.totalOvers)));
		confidenceVal = ((!(confidenceVal - confidenceDec > 0f)) ? 0f : (confidenceVal - confidenceDec));
		CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].ConfidenceVal = confidenceVal;
	}

	public Vector3 UpdateCameraPosition()
	{
		return mainCamera.WorldToScreenPoint(batsmanRefPoint.position);
	}

	public Vector3 getPerspectiveCamPos()
	{
		return mainCamera.gameObject.transform.position;
	}

	private void ReplayCameraMovement()
	{
		if (!replayMode)
		{
			return;
		}
		if (!CONTROLLER.stumpingAttempted)
		{
			if (replayActionStatus == "follow")
			{
				if (action == 3 || action == 4)
				{
					if (!ballOnboundaryLine)
					{
						replayControllerTransform.position = ballRayCastReferenceGOTransform.position;
						replayControllerTransform.eulerAngles = ballRayCastReferenceGOTransform.eulerAngles;
					}
					else
					{
						replayControllerTransform.eulerAngles += new Vector3(0f, replayCameraRotationAngleInBoundary * Time.deltaTime, 0f);
					}
				}
				if (action == 3)
				{
					replayControllerTransform.position = new Vector3(0f, replayControllerTransform.position.y, replayControllerTransform.position.z);
					replayControllerTransform.eulerAngles = new Vector3(replayControllerTransform.eulerAngles.x, 0f, replayControllerTransform.eulerAngles.z);
				}
			}
			else if (replayActionStatus == "lookAt")
			{
				replayCameraTransform.LookAt(replayControllerTransform);
			}
			else if (replayActionStatus == "bowledSlowDown")
			{
				replayCameraScript.enabled = false;
			}
		}
		else if (CONTROLLER.stumpingAttempted)
		{
			UpdateBatShadow();
			if (ballTransform.position.z < 8.8f)
			{
				replayCameraTransform.LookAt(ballTransform);
			}
		}
	}

	public void HideReplay()
	{
		replayMode = false;
		if (ballTween != null && ballTween.IsPlaying())
		{
			ballTween.Pause();
		}
		ballTransform.position = ballInitPosition;
		ballTransform.eulerAngles = new Vector3(0f, 2f, 180f);
		Time.timeScale = 1f;
		Singleton<RewindTime>.instance.canRecord = false;
		Singleton<RewindTime>.instance.StopRewind();
		pauseTheBall = false;
		CONTROLLER.cameraType = 1;
		IrCam.SetActive(value: false);
		replayCameraScript.enabled = false;
		action = -10;
		replayCameraScript.enabled = false;
		if (bowlingBy == "computer" && (currentBallNoOfRuns == 4 || currentBallNoOfRuns == 6))
		{
			if (fieldRestriction)
			{
				SetComputerFieldIndex(UnityEngine.Random.Range(1, 6));
			}
			else
			{
				SetComputerFieldIndex(UnityEngine.Random.Range(6, 10));
			}
		}
		Singleton<GameModel>.instance.ReplayCompleted();
		if (CONTROLLER.isFreeHit)
		{
			CONTROLLER.isFreeHit = false;
		}
	}

	private IEnumerator UltraSlowMotion()
	{
		Time.timeScale = 0.03f;
		yield return new WaitForSeconds(0.1f);
		if (replayMode)
		{
			Time.timeScale = 0.5f;
		}
		else
		{
			Time.timeScale = 1f;
		}
	}

	public void SkipReplay()
	{
		if (CONTROLLER.reviewReplay)
		{
			return;
		}
		pauseTheBall = false;
		CONTROLLER.cameraType = 1;
		IrCam.SetActive(value: false);
		rightSideCamTransform.position = new Vector3(-28f, 7.5f, 0f);
		leftSideCamTransform.position = new Vector3(28f, 7.5f, 0f);
		iTween.Stop(wicketKeeper);
		iTween.Stop(fielder10);
		iTween.Stop(replayController);
		iTween.Stop(batsman);
		iTween.Stop(replayCamera.gameObject);
		iTween.Stop(umpireCamera.gameObject);
		iTween.Stop(mainCamera.gameObject);
		if (ballTween != null && ballTween.IsPlaying())
		{
			ballTween.Pause();
		}
		ballTransform.position = ballInitPosition;
		ballTransform.eulerAngles = new Vector3(0f, 2f, 180f);
		Singleton<RewindTime>.instance.canRecord = false;
		Singleton<RewindTime>.instance.StopRewind();
		wicketKeeperStatus = string.Empty;
		fielder10Action = string.Empty;
		for (int i = 0; i < noOfFielders; i++)
		{
			activeFielderAction.Add(string.Empty);
			GameObject gameObject = fielder[i + 1];
			gameObject.GetComponent<Animation>().Play("idle");
		}
		WicketKeeperAnimationComponent.Play("idle");
		BowlerAnimationComponent.Play("Blitz_" + currentBowlerType + "Idle");
		Fielder10AnimationComponent.Play("idle");
		HideReplay();
		if (afterReplayUpdateRunForRunoutFailedAttempt)
		{
			UpdateRunAfterReplay();
		}
		if (noBallRunUpdateStatus == "beatenball")
		{
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
			Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, 0, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
			if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
			{
			}
		}
		else if (noBallRunUpdateStatus == "bowlercollectsdotball")
		{
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
			Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, 0, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
			if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
			{
			}
		}
		else if (noBallRunUpdateStatus == "wideboundary")
		{
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball += 5;
			Singleton<GameModel>.instance.UpdateCurrentBall(0, 0, 0, 5, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
			if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
			{
			}
		}
		else if (noBallRunUpdateStatus == "cleanbowled")
		{
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
			Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, 0, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, CONTROLLER.StrikerIndex, isBoundary: false);
			if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
			{
			}
		}
		else if (noBallRunUpdateStatus == "lbwappeal")
		{
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
			Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, 0, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
			if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
			{
			}
		}
		else if (noBallRunUpdateStatus == "keepercollectstheball")
		{
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
			Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, runsScoredInLineNoBall, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
			if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
			{
			}
		}
		else if (noBallRunUpdateStatus == "bowlercollectstheball")
		{
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
			Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, runsScoredInLineNoBall, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
			if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
			{
			}
		}
		else if (noBallRunUpdateStatus == "bowlerrunoutappealsandnotout")
		{
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
			Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, runsScoredInLineNoBall, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
			if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
			{
			}
		}
		else if (noBallRunUpdateStatus == "keeperrunoutappealsandnotout")
		{
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
			Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, currentBallNoOfRuns, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
			if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
			{
			}
		}
	}

	private void UpdateRunAfterReplay()
	{
		if (overStepBall)
		{
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchNoball++;
			Singleton<GameModel>.instance.UpdateCurrentBall(0, 1, savedCurrentBallNoOfRuns, 1, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
			if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
			{
			}
		}
		else
		{
			Singleton<GameModel>.instance.UpdateCurrentBall(1, 1, savedCurrentBallNoOfRuns, 0, CONTROLLER.StrikerIndex, 0, 0, CONTROLLER.CurrentBowlerIndex, 0, 0, isBoundary: false);
		}
	}

	public void TurnOffSkins(bool boolean)
	{
		Stump1AnimationComponent.Play("idle");
		Stump2AnimationComponent.Play("idle");
		iTween.Stop(introCamera.gameObject);
		mainCamera.enabled = !boolean;
		rightSideCamera.enabled = false;
		leftSideCamera.enabled = false;
		showPreviewCamera(status: false);
		umpireCamera.enabled = false;
		ultraMotionCamera.enabled = false;
		closeUpCamera.enabled = false;
		replayCamera.enabled = false;
		introCamera.enabled = boolean;
		BallSkinRendererComponent.enabled = !boolean;
		BatsmanSkinRendererComponent.enabled = !boolean;
		RunnerSkinRendererComponent.enabled = !boolean;
		WicketKeeperSkinRendererComponent.enabled = !boolean;
		BowlerSkinRendererComponent.enabled = !boolean;
		fielder10SkinRendererComponent.enabled = !boolean;
		MainUmpireSkinRendererComponent.enabled = !boolean;
		SideUmpireSkinRendererComponent.enabled = !boolean;
		Fielder10BallSkinRendererComponent.enabled = !boolean;
		WicketKeeperBallSkinRendererComponent.enabled = !boolean;
		for (int i = 1; i <= noOfFielders; i++)
		{
			GameObject gameObject = fielderSkin[i];
			gameObject.GetComponent<Renderer>().enabled = !boolean;
		}
		HideShadows(boolean);
		if (CONTROLLER.currentInnings == 1 && Singleton<GameModel>.instance.inningsCompleted)
		{
			hideBatsmenAndUmpires();
		}
		else if (CONTROLLER.currentInnings == 0 && Singleton<GameModel>.instance.inningsCompleted)
		{
			fielder10SkinRendererComponent.enabled = false;
			BatsmanSkinRendererComponent.enabled = false;
			RunnerSkinRendererComponent.enabled = false;
			BowlerSkinRendererComponent.enabled = false;
		}
	}

	private void HideShadows(bool boolean)
	{
		if (boolean)
		{
			if (shadowHolder != null)
			{
				shadowHolder.SetActiveRecursively(state: false);
			}
		}
		else if (shadowHolder != null)
		{
			shadowHolder.SetActiveRecursively(state: true);
		}
	}

	private void SetFieldersSizeBackToNormal()
	{
		for (int i = 1; i <= noOfFielders; i++)
		{
			fielderTransform[i].localScale = new Vector3(1f, 1f, 1f);
		}
	}

	public void fireworksStarted()
	{
		hideBatsmenAndUmpires();
		gatherFielders();
		action = 22;
		showPreviewCamera(status: false);
		rightSideCamera.enabled = false;
		leftSideCamera.enabled = false;
		umpireCamera.enabled = false;
		introCamera.enabled = false;
		ultraMotionCamera.enabled = false;
		closeUpCamera.enabled = false;
		replayCamera.enabled = false;
		for (int i = 0; i < fielder.Length; i++)
		{
			if (fielder[i] != null)
			{
				FielderSkinRendererComponent[i].enabled = true;
			}
		}
		for (int j = 1; j <= 9; j++)
		{
			if (!FielderSkinRendererComponent[j].enabled)
			{
				FielderSkinRendererComponent[j].enabled = true;
			}
		}
		mainCamera.enabled = true;
		mainCameraTransform.position = new Vector3(48f, 2f, -43f);
		mainCameraTransform.eulerAngles = new Vector3(350f, 308f, 0f);
		mainCamera.fieldOfView = 35f;
	}

	private void gatherFielders()
	{
		for (int i = 1; i <= noOfFielders; i++)
		{
			fielderTransform[i].position = new Vector3(UnityEngine.Random.Range(15, 25), 0f, UnityEngine.Random.Range(-15, 0));
			fielderTransform[i].eulerAngles = new Vector3(0f, 0f, 0f);
			FielderAnimationComponent[i].Play("walk");
			FielderAnimationComponent[i]["walk"].time = UnityEngine.Random.Range(1, 5);
			FielderAnimationComponent[i]["walk"].speed = UnityEngine.Random.Range(1f, 1.5f);
		}
		hideBatsmenAndUmpires();
	}

	private void moveFielders()
	{
		for (int i = 1; i <= noOfFielders; i++)
		{
			if (fielderTransform[i].position.z < 64f)
			{
				fielderTransform[i].position += new Vector3(0f, 0f, 0.009f);
			}
		}
	}

	private void hideBatsmenAndUmpires()
	{
		fielder10SkinRendererComponent.enabled = false;
		BatsmanSkinRendererComponent.enabled = false;
		RunnerSkinRendererComponent.enabled = false;
		MainUmpireSkinRendererComponent.enabled = false;
		SideUmpireSkinRendererComponent.enabled = false;
		BowlerSkinRendererComponent.enabled = false;
	}

	public void hideAllCamera()
	{
		iTween.Stop(mainCamera.gameObject);
		mainCamera.enabled = true;
		mainCameraTransform.position = new Vector3(-30f, 6.8f, 0f);
		mainCameraTransform.eulerAngles = new Vector3(10f, 90f, 0f);
		mainCamera.fieldOfView = 45f;
		rightSideCamera.enabled = false;
		leftSideCamera.enabled = false;
		showPreviewCamera(status: false);
		umpireCamera.enabled = false;
		introCamera.enabled = false;
		ultraMotionCamera.enabled = false;
		closeUpCamera.enabled = false;
		replayCamera.enabled = false;
		rightSideBoundaryCamera.enabled = false;
		leftSideBoundaryCamera.enabled = false;
	}

	public void AssignTrace(string str)
	{
		traceStr = traceStr + str + "\n";
	}

	public void ClearTrace()
	{
		traceStr = string.Empty;
	}

	private void enableBowlingSpot()
	{
		if (animationStatus == "idle")
		{
			return;
		}
		if (animationStatus == "spin")
		{
			bowlingSpotTransform.localEulerAngles += new Vector3(0f, rotationSpeed * Time.deltaTime, 0f);
			if (tutorialArrow != null)
			{
				tutorialArrow.transform.localEulerAngles = new Vector3(tutorialArrow.transform.localEulerAngles.x, 0f, tutorialArrow.transform.localEulerAngles.z);
			}
			float num = Mathf.PingPong(Time.time * zoomSpeed, zoomMax - zoomMin) + zoomMin;
		}
		else if (animationStatus == "spinToFreeze")
		{
			rotationSpeed -= initRotationSpeed / freezingDuring * Time.deltaTime;
			bowlingSpotTransform.localScale -= Vector3.one * ((scaleDuringFreeze - zoomMin) / freezingDuring) * Time.deltaTime;
			if (Time.time > freezingStartTime + freezingDuring)
			{
				animationStatus = "idle";
			}
		}
	}

	public void ShowFullTossSpot(bool _Value)
	{
		bowlingSpotFullTossGO.SetActive(_Value);
	}

	private void HideBowlingSpot()
	{
		bowlingSpotModel.GetComponent<Renderer>().enabled = false;
		if (tutorialArrow != null)
		{
			tutorialArrow.SetActive(value: false);
		}
	}

	private void ShowBowlingSpot()
	{
		bowlingSpotModel.GetComponent<Renderer>().enabled = true;
		tutorialArrow.SetActive(value: true);
		rotationSpeed = initRotationSpeed;
		bowlingSpotTransform.localScale = Vector3.one;
		bowlingSpotTransform.localEulerAngles = new Vector3(0f, 0f, 0f);
		animationStatus = "spin";
		if (!Singleton<GameModel>.instance.canShowTutorial())
		{
		}
	}

	private void FreezeBowlingSpot()
	{
		freezingStartTime = Time.time;
		scaleDuringFreeze = bowlingSpotTransform.localScale.x;
		animationStatus = "spinToFreeze";
		if (tutorialArrow != null)
		{
			tutorialArrow.SetActive(value: false);
		}
	}

	private bool IsStumpOut()
	{
		bool result = false;
		float time = batsmanAnimationComponent[batsmanAnimation].time;
		int num = 0;
		int num2 = 0;
		if (batsmanAnimation == "bt6CoverDrive")
		{
			num = 22;
			num2 = 87;
		}
		else if (batsmanAnimation == "bt6LegGlance")
		{
			num = 16;
			num2 = 83;
		}
		else if (batsmanAnimation == "bt6OffDrive")
		{
			num = 16;
			num2 = 67;
		}
		else if (batsmanAnimation == "bt6OnDrive")
		{
			num = 14;
			num2 = 115;
		}
		else if (batsmanAnimation == "bt6StraightDrive")
		{
			num = 17;
			num2 = 81;
		}
		else if (batsmanAnimation == "loftLegSide")
		{
			num = 3;
			num2 = 99;
		}
		else if (batsmanAnimation == "loftOffSide")
		{
			num = 3;
			num2 = 136;
		}
		else if (batsmanAnimation == "loftStraight")
		{
			num = 1;
			num2 = 188;
		}
		else if (batsmanAnimation == "frontFootOffDrive")
		{
			num = 20;
			num2 = 64;
		}
		if (time >= (float)num * animationFPSDivide && time <= (float)num2 * animationFPSDivide)
		{
			result = true;
		}
		return result;
	}

	private bool IsStumpOut2()
	{
		bool flag = false;
		float time = batsmanAnimationComponent["ReturnToCrease2"].time;
		int num = 9;
		if (time < (float)num * animationFPSDivide || Mathf.Abs(batEdgeGO.transform.position.z) < 8.7f)
		{
			flag = true;
		}
		if (flag && (batsmanLeftLegEdge.transform.position.z > 8.7f || batsmanRightLegEdge.transform.position.z > 8.7f || batsmanLeftShoeBackEdge.transform.position.z > 8.7f || batsmanRightShoeBackEdge.transform.position.z > 8.7f))
		{
			flag = false;
		}
		return flag;
	}

	private float GetBatsmanReturnFrameTimeToCrease()
	{
		float num = 0f;
		int num2 = 0;
		if (batsmanAnimation == "bt6CoverDrive")
		{
			num2 = 71;
		}
		else if (batsmanAnimation == "bt6LegGlance")
		{
			num2 = 61;
		}
		else if (batsmanAnimation == "bt6OffDrive")
		{
			num2 = 61;
		}
		else if (batsmanAnimation == "bt6OnDrive")
		{
			num2 = 91;
		}
		else if (batsmanAnimation == "bt6StraightDrive")
		{
			num2 = 65;
		}
		else if (batsmanAnimation == "loftLegSide")
		{
			num2 = 71;
		}
		else if (batsmanAnimation == "loftOffSide")
		{
			num2 = 111;
		}
		else if (batsmanAnimation == "loftStraight")
		{
			num2 = 115;
		}
		else if (batsmanAnimation == "frontFootOffDrive")
		{
			num2 = 61;
		}
		return (float)num2 * animationFPSDivide;
	}

	public void SetDefaultDigitalDisplayContent()
	{
		DigitalScreenRendererComponent.material.mainTexture = digitalBoardContent[0];
		digitalScreen.transform.localScale = digitalScreenScale;
	}

	private bool IsTightRunoutCall()
	{
		bool result = false;
		if (Mathf.Abs(batEdgeGO.transform.position.z) > 7.5f && Mathf.Abs(batEdgeGO.transform.position.z) < 9f)
		{
			result = true;
		}
		if (Mathf.Abs(batEdgeGO.transform.position.z) > 8.4f && Mathf.Abs(batEdgeGO.transform.position.z) < 9f)
		{
			veryTightRunoutCall = true;
		}
		return result;
	}

	private void showMainUmpireForNoBallAction()
	{
		showPreviewCamera(status: false);
		mainUmpireTransform.localScale = new Vector3(1f, 1f, 1f);
		stayStartTime = Time.time;
		mainCamera.enabled = false;
		rightSideCamera.enabled = false;
		leftSideCamera.enabled = false;
		closeUpCamera.enabled = false;
		umpireCamera.enabled = true;
		umpireCameraTransform.position = mainUmpireInitPosition;
		umpireCameraTransform.position = new Vector3(umpireCameraTransform.position.x, 2f, umpireCameraTransform.position.z);
		umpireCameraTransform.position += new Vector3(0f, 0f, 3f);
		umpireCameraTransform.eulerAngles = new Vector3(umpireCameraTransform.eulerAngles.x, 180f, umpireCameraTransform.eulerAngles.z);
		umpireCameraTransform.eulerAngles = new Vector3(5 + UnityEngine.Random.Range(0, 5), umpireCameraTransform.eulerAngles.y, umpireCameraTransform.eulerAngles.z);
		Singleton<Scoreboard>.instance.Hide(boolean: true);
		Singleton<PreviewScreen>.instance.Hide(boolean: true);
		Singleton<BowlingControls>.instance.Hide(boolean: true);
		Singleton<BattingControls>.instance.Hide(boolean: true);
		Singleton<PauseGameScreen>.instance.Hide(boolean: true);
	}

	private bool checkForMatchComplete(int runInThisBall, int currentMatchWickets)
	{
		int num = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchScores + runInThisBall;
		if (CONTROLLER.PlayModeSelected == 4 && CONTROLLER.InningsCompleted)
		{
			return true;
		}
		if (CONTROLLER.currentInnings == 1 && num >= CONTROLLER.TargetToChase)
		{
			return true;
		}
		if (currentMatchWickets >= CONTROLLER.totalWickets)
		{
			return true;
		}
		return false;
	}

	public void ShowAllPlayers()
	{
		for (int i = 0; i < fielder.Length; i++)
		{
			if (fielder[i] != null)
			{
				FielderSkinRendererComponent[i].enabled = true;
			}
		}
		for (int j = 1; j <= 9; j++)
		{
			if (!FielderSkinRendererComponent[j].enabled)
			{
				FielderSkinRendererComponent[j].enabled = true;
			}
		}
		BatsmanSkinRendererComponent.enabled = true;
		RunnerSkinRendererComponent.enabled = true;
		MainUmpireSkinRendererComponent.enabled = true;
		SideUmpireSkinRendererComponent.enabled = true;
		BowlerSkinRendererComponent.enabled = true;
		WicketKeeperSkinRendererComponent.enabled = true;
	}

	private void UpdateBatShadow()
	{
		batShadowHolderTransform.position = new Vector3(batTopEdgeTransform.position.x, 0f, batTopEdgeTransform.position.z);
		batShadowHolderTransform.eulerAngles = new Vector3(0f, 270f - AngleBetweenTwoVector3(batEdgeGO.transform.position, batTopEdgeTransform.position), 0f);
		Vector3 localScale = batShadowHolderTransform.localScale;
		localScale.z = DistanceBetweenTwoVector2(batTopEdgeTransform.gameObject, batEdgeGO) / 0.7f + 0.05f;
		batShadowHolderTransform.localScale = localScale;
	}

	private void HideBatShadow()
	{
		batShadowHolderTransform.position = new Vector3(batTopEdgeTransform.position.x, -3000f, batTopEdgeTransform.position.z);
	}

	private void SetUmpireCameraPosition(string umpire)
	{
		mainUmpireTransform.position = mainUmpireInitPosition;
		if (umpire == "MainUmpire")
		{
			int num = UnityEngine.Random.Range(0, 7);
			if ((num == 0 || num == 1) && (batsmanTransform.position.z < -10f || runnerTransform.position.z < -10f))
			{
				num = 6;
			}
			switch (num)
			{
			case 0:
				umpireCameraTransform.position = new Vector3(-4.5f, 3f, -10f);
				umpireCameraTransform.eulerAngles = new Vector3(15f, 144f, 0f);
				umpireCamera.fieldOfView = 20f;
				break;
			case 1:
				umpireCameraTransform.position = new Vector3(4.5f, 3f, -10f);
				umpireCameraTransform.eulerAngles = new Vector3(15f, 215f, 0f);
				umpireCamera.fieldOfView = 20f;
				break;
			case 2:
				umpireCameraTransform.position = new Vector3(0.8f, 1f, -11f);
				umpireCameraTransform.eulerAngles = new Vector3(358f, 188f, 0f);
				umpireCamera.fieldOfView = 18f;
				break;
			case 3:
				umpireCameraTransform.position = new Vector3(-0.8f, 1f, -11f);
				umpireCameraTransform.eulerAngles = new Vector3(358f, 170f, 0f);
				umpireCamera.fieldOfView = 18f;
				break;
			case 4:
				umpireCameraTransform.position = new Vector3(-5f, 2f, -13f);
				umpireCameraTransform.eulerAngles = new Vector3(6f, 120f, 0f);
				umpireCamera.fieldOfView = 20f;
				break;
			case 5:
				umpireCameraTransform.position = new Vector3(5f, 2f, -13f);
				umpireCameraTransform.eulerAngles = new Vector3(6f, 237f, 0f);
				umpireCamera.fieldOfView = 20f;
				break;
			case 6:
				umpireCameraTransform.position = new Vector3(0f, 1.3f, -12f);
				umpireCameraTransform.eulerAngles = new Vector3(0f, 180f, 0f);
				umpireCamera.fieldOfView = 25f;
				break;
			}
			iTween.MoveTo(umpireCamera.gameObject, iTween.Hash("y", umpireCameraTransform.position.y + 0.2f, "time", 2, "easetype", "easeInOutSine"));
		}
		else if (!(umpire == "SideUmpire"))
		{
		}
	}

	public void StartIntroFielderAnimation()
	{
		InvokeRepeating("SetFielderAnimation", 0f, 3f);
	}

	public void StopIntroFielderAnimation()
	{
		CancelInvoke("SetFielderAnimation");
	}

	public void SetFielderAnimation()
	{
		for (int i = 1; i < fielder.Length - 1; i++)
		{
			switch (UnityEngine.Random.Range(1, 3))
			{
			case 1:
				FielderAnimationComponent[i].CrossFade("warmUp" + UnityEngine.Random.Range(1, 6));
				break;
			case 2:
				FielderAnimationComponent[i].CrossFade("fielderArrangingPlayers");
				break;
			case 3:
				FielderAnimationComponent[i].CrossFade("getReady");
				break;
			case 4:
				FielderAnimationComponent[i].CrossFade("celebration1");
				break;
			}
		}
	}

	public void Greedy_clickable_ad()
	{
		GreedyGameAgent.Instance.showEngagementWindow("float-3634");
	}

	private float DistanceBetweenTwoVector2(Vector3 vector1, Vector3 vector2)
	{
		float num = vector1.x - vector2.x;
		float num2 = vector1.z - vector2.z;
		return Mathf.Sqrt(num * num + num2 * num2);
	}

	private int GetNearestFielderIndex(Vector3 initialPosition, float fielderScanDistance, bool requireIdle = false)
	{
		float num = float.PositiveInfinity;
		for (int i = 0; i < activeFielderNumber.Count; i++)
		{
			float num2 = DistanceBetweenTwoVector2(initialPosition, fielderTransform[activeFielderNumber[i]].position);
			if (num2 < num)
			{
				num = num2;
				nearestFielderIndex = i;
			}
		}
		return (!(num <= fielderScanDistance)) ? (-1) : nearestFielderIndex;
	}

	private void MoveToCollectBallAfterBoundary(int fielderIndex, bool stop = false, bool first = false)
	{
		if (stop)
		{
			FielderAnimationComponent[fielderIndex].Play("runComplete");
			return;
		}
		if (first)
		{
			FielderAnimationComponent[fielderIndex].Stop();
			FielderAnimationComponent[fielderIndex].Play("walk");
		}
		fielderTransform[fielderIndex].LookAt(new Vector3(ballTransform.position.x, 0f, ballTransform.position.z));
		fielderTransform[fielderIndex].position += fielderTransform[fielderIndex].forward * 1.6f * Time.deltaTime;
	}

	private bool IsFielderPlayingAnAnimation(int fielderIndex)
	{
		bool flag = false;
		flag |= FielderAnimationComponent[fielderIndex].IsPlaying("diveStraight");
		flag |= FielderAnimationComponent[fielderIndex].IsPlaying("highCatch");
		flag |= FielderAnimationComponent[fielderIndex].IsPlaying("hipCatch");
		flag |= FielderAnimationComponent[fielderIndex].IsPlaying("lowCatch");
		flag |= FielderAnimationComponent[fielderIndex].IsPlaying("sideCatch");
		return flag | FielderAnimationComponent[fielderIndex].IsPlaying("slideAndField");
	}

	private void ScanForUserFielders()
	{
		userFielderScanList.Clear();
		foreach (GameObject item in aiFielderScanArray)
		{
			userFielderScanList.Add(AngleBetweenTwoGameObjects(batsman, item));
		}
		userFielderScanList.Sort();
		if (CONTROLLER.PlayModeSelected != 7)
		{
			MakeAIHitInGap();
		}
		else
		{
			MakeAIHitToFielder();
		}
	}

	private void CheckShotRegion()
	{
		userFielderScanListRefined.Clear();
		int num = 0;
		float x = ballSpotAtCreaseLine.transform.position.x;
		for (int i = 0; i < userFielderScanList.Count; i++)
		{
		}
		foreach (float userFielderScan in userFielderScanList)
		{
			float num2 = userFielderScan;
			if (x < 0f)
			{
				if (num == 0)
				{
					userFielderScanListRefined.Add(110f);
				}
				if (!(num2 < 110f) && !(num2 > 270f))
				{
					userFielderScanListRefined.Add(num2);
				}
				if (num == userFielderScanList.Count - 1)
				{
					userFielderScanListRefined.Add(270f);
				}
			}
			else if (x >= 0f)
			{
				if (num == 0)
				{
					userFielderScanListRefined.Add(270f);
				}
				if (!(num2 > 70f) || !(num2 <= 270f))
				{
					if (num2 <= 70f)
					{
						userFielderScanListRefined.Add(num2 + 360f);
					}
					else
					{
						userFielderScanListRefined.Add(num2);
					}
				}
				if (num == userFielderScanList.Count - 1)
				{
					userFielderScanListRefined.Add(430f);
				}
			}
			num++;
		}
		userFielderScanListRefined.Sort();
		for (int j = 0; j < userFielderScanListRefined.Count; j++)
		{
		}
	}

	private void MakeAIHitInGap()
	{
		CheckShotRegion();
		int num = UnityEngine.Random.Range(0, userFielderScanListRefined.Count - 1);
		AIBallAngle = (userFielderScanListRefined[num] + userFielderScanListRefined[num + 1]) / 2f;
		if (AIBallAngle > 360f)
		{
			AIBallAngle %= 360f;
		}
		if (CONTROLLER.StrikerHand == "left")
		{
			AIBallAngle = 180f - AIBallAngle + 360f;
			AIBallAngle %= 360f;
		}
	}

	public void DetermineAIShot()
	{
		int selectedAngle = 0;
		float aIBallAngle = AIBallAngle;
		if (aIBallAngle == 0f)
		{
			selectedAngle = 5;
		}
		else if ((aIBallAngle < 22.5f && aIBallAngle >= -22.5f) || (aIBallAngle < 360f && aIBallAngle >= 337.5f))
		{
			selectedAngle = 7;
		}
		else if (aIBallAngle < 67.5f && aIBallAngle >= 22.5f)
		{
			selectedAngle = 6;
		}
		else if (aIBallAngle < 112.5f && aIBallAngle >= 67.5f)
		{
			selectedAngle = 5;
		}
		else if (aIBallAngle < 157.5f && aIBallAngle >= 112.5f)
		{
			selectedAngle = 4;
		}
		else if (aIBallAngle < 202.5f && aIBallAngle >= 157.5f)
		{
			selectedAngle = 3;
		}
		else if (aIBallAngle < 247.5f && aIBallAngle >= 202.5f)
		{
			selectedAngle = 2;
		}
		else if (aIBallAngle < 292.5f && aIBallAngle >= 247.5f)
		{
			selectedAngle = 1;
		}
		else if (aIBallAngle < 337.5f && aIBallAngle >= 292.5f)
		{
			selectedAngle = 8;
		}
		AIShotSelected(selectedAngle);
	}

	public void AIShotSelected(int SelectedAngle)
	{
		leftArrowKeyDown = false;
		downArrowKeyDown = false;
		upArrowKeyDown = false;
		rightArrowKeyDown = false;
		switch (SelectedAngle)
		{
		case 1:
			downArrowKeyDown = true;
			break;
		case 2:
			leftArrowKeyDown = true;
			downArrowKeyDown = true;
			break;
		case 3:
			leftArrowKeyDown = true;
			break;
		case 4:
			upArrowKeyDown = true;
			leftArrowKeyDown = true;
			break;
		case 6:
			upArrowKeyDown = true;
			rightArrowKeyDown = true;
			break;
		case 7:
			rightArrowKeyDown = true;
			break;
		case 8:
			rightArrowKeyDown = true;
			downArrowKeyDown = true;
			break;
		}
	}

	private void MakeAIHitToFielder()
	{
		int index = UnityEngine.Random.Range(0, userFielderScanList.Count);
		AIBallAngle = userFielderScanList[index];
		if (AIBallAngle > 360f)
		{
			AIBallAngle %= 360f;
		}
		if (CONTROLLER.StrikerHand == "left")
		{
			AIBallAngle = 180f - AIBallAngle + 360f;
			AIBallAngle %= 360f;
		}
	}

	private void SaveEdgePosition()
	{
		if (!edgePositionSaved && shotExecuted)
		{
			SetFrames();
			midFrame = (StartingFrame + EndingFrame) / 2f;
			if (batsmanAnimationComponent[batsmanAnimation].time >= midFrame)
			{
				edgeRefs.position = new Vector3(edgeRefs.position.x, ball.transform.position.y, edgeRefs.position.z);
				savedEdgePosition = edgeRefs.position;
				hotspotReference[0].transform.position = new Vector3(hotspotReference[0].transform.position.x, hotspotReference[0].transform.position.y, savedEdgePosition.z - 0.25f);
				hotspotReference[1].transform.position = new Vector3(hotspotReference[1].transform.position.x, hotspotReference[1].transform.position.y, savedEdgePosition.z + 0.25f);
				edgePositionSaved = true;
			}
		}
	}

	private void PlaceUltraEdgeCam()
	{
		if (currentBatsmanHand == "right")
		{
			SnickoMeter.transform.localPosition = new Vector3(0f - snickoPosition.x, snickoPosition.y, snickoPosition.z);
			IrCam.transform.position = UltraEdgeCamPosition;
			IrCam.transform.eulerAngles = UltraEdgeCamRotation;
			SideCam.transform.position = sideCamPos;
			SideCam.transform.eulerAngles = sideCamRot;
		}
		else
		{
			SnickoMeter.transform.localPosition = snickoPosition;
			IrCam.transform.position = new Vector3(0f - UltraEdgeCamPosition.x, UltraEdgeCamPosition.y, UltraEdgeCamPosition.z);
			IrCam.transform.eulerAngles = new Vector3(UltraEdgeCamRotation.x, 0f - UltraEdgeCamRotation.y, UltraEdgeCamRotation.z);
			SideCam.transform.position = new Vector3(0f - sideCamPos.x, sideCamPos.y, sideCamPos.z);
			SideCam.transform.eulerAngles = new Vector3(sideCamRot.x, 0f - sideCamRot.y, sideCamRot.z);
		}
	}

	private void SetUltraEdgeDecision()
	{
		UserCanAskReview = false;
		AiCanAskReview = false;
		Singleton<BattingControls>.instance.Hide(boolean: true);
		if (UnityEngine.Random.Range(1, 101) <= umpireChance)
		{
			UmpireInitialDecision = "out";
		}
		else
		{
			UmpireInitialDecision = "notout";
		}
		if (UnityEngine.Random.Range(1, 101) <= edgeChance)
		{
			isEdged = true;
		}
		else
		{
			isEdged = false;
		}
		if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
		{
			if (UmpireInitialDecision == "out")
			{
				if (CONTROLLER.TeamList[CONTROLLER.myTeamIndex].noofDRSLeft > 0)
				{
					UserCanAskReview = true;
				}
			}
			else if (CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].noofDRSLeft > 0)
			{
				AiCanAskReview = true;
			}
		}
		else if (UmpireInitialDecision == "out")
		{
			if (CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].noofDRSLeft > 0)
			{
				AiCanAskReview = true;
			}
		}
		else if (CONTROLLER.TeamList[CONTROLLER.myTeamIndex].noofDRSLeft > 0)
		{
			UserCanAskReview = true;
		}
	}

	private void SaveBallTravelTime()
	{
		if (!ballTimeSaved && action > 2)
		{
			if (ball.transform.position.z - hotspotReference[3].transform.position.z <= 0f && ball.transform.position.z - hotspotReference[0].transform.position.z >= 0f)
			{
				elapsedTime += Time.deltaTime;
			}
			else if (ball.transform.position.z - hotspotReference[3].transform.position.z > 0f)
			{
				ballTimeSaved = true;
			}
		}
	}

	private void CheckForHotspotPosition()
	{
		SaveEdgePosition();
		SaveBallTravelTime();
		if (!replayMode || !UltraEdgeCutscenePlaying)
		{
			return;
		}
		DebugLogger.PrintWithSize("=============" + RewindTime.count + "==============");
		if (batsmanAnimationComponent[batsmanAnimation].time >= StartingFrame)
		{
			mainCamera.enabled = false;
			pauseTheBall = true;
			Time.timeScale = 0.035f;
			if (canRecord)
			{
				if (!animStarted)
				{
					SetFrames();
					batsmanAnimationComponent[batsmanAnimation].time = StartingFrame;
					batsmanAnimationComponent[batsmanAnimation].speed = 1f;
					batsmanAnimationComponent.Play(batsmanAnimation);
					animStarted = true;
				}
				if (batsmanAnimationComponent[batsmanAnimation].time >= EndingFrame)
				{
					Singleton<RewindTime>.instance.StartRewind();
					batsmanAnimationComponent[batsmanAnimation].speed = -1f;
					canRecord = false;
				}
			}
			SetBallPositionForCutscene();
			CheckForEdge();
			BallMovementCustom();
			if (!waveTweenPlayed)
			{
				Sequence s = DOTween.Sequence();
				s.Insert(0f, waveImg.transform.DOLocalMoveX(-94f, 0f));
				s.Insert(0.001f, waveImg.transform.DOLocalMoveX(-800f, 2f));
				waveTweenPlayed = true;
			}
			ShowUltraEdgeCam();
		}
		if (RewindTime.count > 4)
		{
			ShowUmpireAnim();
			ResetEdgeDetectionVariables();
			if ((!isEdged && UmpireInitialDecision == "notout") || (isEdged && UmpireInitialDecision == "out"))
			{
				Invoke("EndCutScene", 1f);
			}
			else
			{
				Invoke("EndCutScene", 4f);
			}
		}
	}

	private void SetFrames()
	{
		if (shotPlayed == "bt6StraightDrive")
		{
			StartingFrame = 0.45f;
			EndingFrame = 0.5f;
		}
	}

	private void ShowUltraEdgeCam(bool canShow = true)
	{
		IEnumerator enumerator = UltraEdgeCam.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				if (transform.GetComponent<Camera>() != null)
				{
					transform.GetComponent<Camera>().enabled = canShow;
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = enumerator as IDisposable) != null)
			{
				disposable.Dispose();
			}
		}
		UltraEdgeCam.SetActive(canShow);
	}

	private void SetBallPositionForCutscene()
	{
		if (!ballPlaced)
		{
			if (currentBatsmanHand == "right")
			{
				edgeDistance = 0.1f;
				safeDistance = 0.13f;
				deviation = 0.1f;
			}
			else
			{
				edgeDistance = -0.1f;
				safeDistance = -0.13f;
				deviation = -0.1f;
			}
			float num = 0.3f;
			if (ballSpotLength < 12f)
			{
				num = 0f;
			}
			ballPath1[2].transform.parent.eulerAngles = new Vector3(ballPath1[2].transform.parent.eulerAngles.x, 90f, ballPath1[2].transform.parent.eulerAngles.y);
			hotspotReference[3].transform.position = new Vector3(savedEdgePosition.x - edgeDistance, savedEdgePosition.y + num, hotspotReference[1].transform.position.z + 0.1f);
			referencePath.transform.position = new Vector3(ballPath1[1].transform.position.x, ballPath1[1].transform.position.y - 0.3f, ball.transform.position.z);
			ball.transform.position = new Vector3(ballPath1[1].transform.position.x, ballPath1[1].transform.position.y, ball.transform.position.z);
			if (!isEdged)
			{
				ball.transform.position = new Vector3(ultraEdgeImpact.transform.position.x - safeDistance, ultraEdgeImpact.transform.position.y, hotspotReference[0].transform.position.z);
				hotspotReference[3].transform.localPosition = new Vector3(hotspotReference[3].transform.localPosition.x - safeDistance, hotspotReference[3].transform.localPosition.y, hotspotReference[3].transform.localPosition.z);
			}
			else
			{
				ball.transform.position = new Vector3(ultraEdgeImpact.transform.position.x - edgeDistance, ultraEdgeImpact.transform.position.y, hotspotReference[0].transform.position.z);
				hotspotReference[3].transform.localPosition = new Vector3(hotspotReference[3].transform.localPosition.x, hotspotReference[3].transform.localPosition.y, hotspotReference[3].transform.localPosition.z);
			}
			Singleton<RewindTime>.instance.canRecord = true;
			ballPlaced = true;
		}
	}

	private void CheckForEdge()
	{
		if (isEdged)
		{
			if (batsmanAnimationComponent[batsmanAnimation].time >= 0.48f)
			{
				if (batsmanAnimationComponent[batsmanAnimation].time <= 0.485f)
				{
					if (!changedBallMovement && ballTween != null && customBallMovement)
					{
						Vector3 vector = new Vector3(ballPath1[0].transform.position.x - deviation, ballPath1[0].transform.position.y, hotspotReference[3].transform.position.z);
						ballTween.ChangeEndValue(vector, snapStartValue: true);
						changedBallMovement = true;
					}
					waveImg.SetActive(value: false);
					impactImg.SetActive(value: true);
					if (RewindTime.count == 4 && !ballPausedAtImpact)
					{
						snickoStatus.sprite = snickoEdged;
						StartCoroutine(PauseAtImpact());
					}
				}
				else
				{
					waveImg.SetActive(value: true);
					impactImg.SetActive(value: false);
				}
			}
			else
			{
				waveImg.SetActive(value: true);
				impactImg.SetActive(value: false);
				ultraEdgeImpact.SetActive(value: false);
			}
		}
		else if (batsmanAnimationComponent[batsmanAnimation].time >= 0.475f && batsmanAnimationComponent[batsmanAnimation].time <= 0.48f && RewindTime.count == 4 && !ballPausedAtImpact)
		{
			snickoStatus.sprite = snickoNotEdged;
			StartCoroutine(PauseAtImpact());
		}
	}

	private IEnumerator PauseAtImpact()
	{
		Time.timeScale = 0f;
		UltraEdgeCutscenePlaying = false;
		Singleton<RewindTime>.instance.StopRewind();
		snickoStatus.DOFade(1f, 0.75f).SetUpdate(isIndependentUpdate: true);
		yield return new WaitForSecondsRealtime(3.5f);
		ballPausedAtImpact = true;
		UltraEdgeCutscenePlaying = true;
		RewindTime.count++;
	}

	public void BallMovementCustom()
	{
		if (!customBallMovement)
		{
			travelTime = UnityEngine.Random.Range(0.09f, 0.1f);
			float num = 0f;
			num = Mathf.Round(hotspotReference[3].transform.position.x * 1000f) / 1000f;
			Vector3 vector = new Vector3(num, hotspotReference[3].transform.position.y, hotspotReference[3].transform.position.z);
			if (isEdged)
			{
				ballPath1[0].transform.position = new Vector3(ultraEdgeImpact.transform.position.x - edgeDistance, ultraEdgeImpact.transform.position.y, hotspotReference[1].transform.position.z);
				ballTween = ball.transform.DOMove(ballPath1[0].transform.position, travelTime);
			}
			else
			{
				ballPath1[0].transform.position = new Vector3(ultraEdgeImpact.transform.position.x - safeDistance, ultraEdgeImpact.transform.position.y, hotspotReference[1].transform.position.z);
				ballTween = ball.transform.DOMove(ballPath1[0].transform.position, travelTime);
			}
			customBallMovement = true;
		}
		Singleton<RewindTime>.instance.canRecord = true;
	}

	private void EndCutScene()
	{
		UltraEdgeCutscenePlaying = false;
		mainCamera.enabled = true;
		Singleton<Scoreboard>.instance.UpdateScoreCard();
		if (!isEdged)
		{
			ShowNotOutAnim = true;
			Singleton<GameModel>.instance.UpdateCurrentBall(validBall, canCountBall, runsScored, extraRun, batsmanID, 0, 0, bowlerID, catcherID, batsmanOut, isBoundary);
		}
		else
		{
			Singleton<GameModel>.instance.UpdateCurrentBall(validBall, canCountBall, runsScored, extraRun, batsmanID, 1, 5, bowlerID, catcherID, batsmanOut, isBoundary);
		}
		isEdged = false;
	}

	private void ShowUmpireAnim()
	{
		umpireCamera.enabled = true;
		umpireCameraTransform.position = mainUmpireInitPosition;
		umpireCameraTransform.position = new Vector3(umpireCameraTransform.position.x, 2f, umpireCameraTransform.position.z);
		umpireCameraTransform.position += new Vector3(0f, 0f, 3f);
		umpireCameraTransform.eulerAngles = new Vector3(5 + UnityEngine.Random.Range(0, 5), 180f, 0f);
		if (!isEdged)
		{
			if (UmpireInitialDecision == "notout")
			{
				MainUmpireAnimationComponent.Play("NotOut");
			}
			else
			{
				MainUmpireAnimationComponent.Play("DRS_SorryNotOut");
			}
		}
		else
		{
			makeFieldersToCelebrate(null);
			if (UmpireInitialDecision == "out")
			{
				MainUmpireAnimationComponent.Play("Out2_New");
			}
			else
			{
				MainUmpireAnimationComponent["DRS_SorryOut"].speed = 0.2f;
				MainUmpireAnimationComponent.Play("DRS_SorryOut");
			}
		}
		if ((UmpireInitialDecision == "out" && !isEdged) || (UmpireInitialDecision == "notout" && isEdged))
		{
			CONTROLLER.TeamList[Singleton<ReviewSystem>.instance.TeamIndex].noofDRSLeft++;
		}
		Singleton<GameModel>.instance.PlayGameSound("Cheer");
	}

	private void ResetEdgeDetectionVariables()
	{
		action = 20;
		umpireChance = 50;
		edgeChance = 50;
		ultraEdgeImpact.SetActive(value: false);
		CONTROLLER.canShowReplay = false;
		CONTROLLER.reviewReplay = false;
		CONTROLLER.ReplayShowing = false;
		RewindTime.count = 0;
		IrCam.SetActive(value: false);
		ShowUltraEdgeCam(canShow: false);
		waveTweenPlayed = false;
		waveImg.SetActive(value: true);
		canRecord = true;
		impactImg.SetActive(value: false);
		positionSaved = false;
		if (ballTween != null && ballTween.IsPlaying())
		{
			ballTween.Pause();
		}
		animStarted = false;
		CONTROLLER.cameraType = 1;
		ballTransform.position = ballInitPosition;
		ballTransform.eulerAngles = new Vector3(0f, 2f, 180f);
		Time.timeScale = 1f;
		Singleton<RewindTime>.instance.canRecord = false;
		Singleton<RewindTime>.instance.StopRewind();
		ballPlaced = false;
		shotExecuted = false;
		elapsedTime = 0f;
		hardcoded = false;
		snickoStatus.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		edgeCatch = false;
		ShowNotOutAnim = false;
		timeToTravel = 0f;
		replayMode = false;
		customBallMovement = false;
		changedBallMovement = false;
		umpireAnimationPlayed = false;
		ballTimeSaved = false;
		edgePositionSaved = false;
		ballPausedAtImpact = false;
		for (int i = 0; i < 4; i++)
		{
			hotspotReference[i].transform.localPosition = DefaultHotspotPositions[i];
		}
		for (int j = 0; j < 4; j++)
		{
			ballPath1[j].transform.localPosition = DefaultBallPath[j];
		}
		Singleton<GameModel>.instance.ReplayCompleted();
		Singleton<GameModel>.instance.ActionTxt.gameObject.SetActive(value: false);
		HideReplay();
		edgeRefs.localPosition = savedEdgeTransform;
	}

	private void UpdateBattingTimingMeter()
	{
		if (updateBattingTimingMeterNeedle && !replayMode)
		{
			battingTimingMeter = ballTransform.position.z / 5f * 100f;
			if (battingTimingMeter < -100f)
			{
				battingTimingMeter = -100f;
			}
			else if (battingTimingMeter > 100f)
			{
				battingTimingMeter = 100f;
			}
			int num = 8;
			if (CONTROLLER.PowerPlay)
			{
				num = 6;
			}
			if (Mathf.Abs(battingTimingMeter) <= (float)Singleton<BattingControls>.instance.btmPerfectValue)
			{
				perfectShot = true;
				horizontalSpeedMultiplier = 1.2f;
				mistimedShot = false;
				Singleton<BattingControls>.instance.battingTimingNeedleText.text = LocalizationData.instance.getText(539);
			}
			else if (battingTimingMeter < -65f)
			{
				firstBounceMultiplier = 0.7f;
				horizontalSpeedMultiplier = 0.9f;
				perfectShot = false;
				mistimedShot = true;
				Singleton<BattingControls>.instance.battingTimingNeedleText.text = LocalizationData.instance.getText(540);
			}
			else if (battingTimingMeter < -30f)
			{
				firstBounceMultiplier = 0.9f;
				horizontalSpeedMultiplier = 0.95f;
				perfectShot = false;
				mistimedShot = true;
				Singleton<BattingControls>.instance.battingTimingNeedleText.text = LocalizationData.instance.getText(541);
			}
			else if (battingTimingMeter < (float)(-Singleton<BattingControls>.instance.btmPerfectValue))
			{
				firstBounceMultiplier = 1f;
				horizontalSpeedMultiplier = 1f;
				perfectShot = false;
				mistimedShot = false;
				Singleton<BattingControls>.instance.battingTimingNeedleText.text = LocalizationData.instance.getText(541) + " - " + LocalizationData.instance.getText(542);
			}
			else if (battingTimingMeter > 65f)
			{
				firstBounceMultiplier = 0.7f;
				horizontalSpeedMultiplier = 0.9f;
				perfectShot = false;
				mistimedShot = true;
				Singleton<BattingControls>.instance.battingTimingNeedleText.text = LocalizationData.instance.getText(543);
			}
			else if (battingTimingMeter > 30f)
			{
				firstBounceMultiplier = 0.9f;
				horizontalSpeedMultiplier = 0.95f;
				perfectShot = false;
				mistimedShot = true;
				Singleton<BattingControls>.instance.battingTimingNeedleText.text = LocalizationData.instance.getText(544);
			}
			else
			{
				firstBounceMultiplier = 1f;
				perfectShot = false;
				mistimedShot = true;
				Singleton<BattingControls>.instance.battingTimingNeedleText.text = LocalizationData.instance.getText(545);
			}
			Singleton<BattingControls>.instance.battingMeterPointer.transform.localPosition = new Vector3(battingTimingMeter, Singleton<BattingControls>.instance.battingMeterPointer.transform.localPosition.y);
		}
	}

	private void BattingMeterCheck()
	{
		if (CONTROLLER.BattingTeamIndex != CONTROLLER.myTeamIndex)
		{
			return;
		}
		if (perfectShot)
		{
			if (powerShot)
			{
				ballTimingFirstBounceDistance = 70 + UnityEngine.Random.Range(5, 15);
			}
			else
			{
				horizontalSpeed *= 1.25f;
			}
		}
		else if (powerShot)
		{
			ballTimingFirstBounceDistance *= firstBounceMultiplier;
		}
		else
		{
			horizontalSpeed *= horizontalSpeedMultiplier;
		}
	}

	private float GetBattingAbility()
	{
		int num = int.Parse(CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank);
		float num2 = 70f;
		if (CONTROLLER.StrikerIndex > 6)
		{
			num2 -= 15f;
		}
		else if (CONTROLLER.StrikerIndex > 3)
		{
			num2 -= 10f;
		}
		return num2 - (float)num;
	}

	public void EdgeOut()
	{
		edgeChance = 105;
	}

	public void EdgeNotOut()
	{
		edgeChance = -1;
	}

	public void UmpireOut()
	{
		umpireChance = 105;
	}

	public void UmpireNotOut()
	{
		umpireChance = -1;
	}

	public void DrsHardcode()
	{
		DRSHardcode = true;
	}
}
