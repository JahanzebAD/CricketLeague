using UnityEngine;

public class SoundController : MonoBehaviour
{
	protected AudioSource CrowdSource;

	protected AudioSource source;

	protected AudioSource BGMSource;

	protected AudioSource btnSource;

	protected AudioSource CoinSource;

	protected AudioClip BGM;

	protected AudioClip CrowdSound;

	protected AudioClip SlowMotionSound;

	protected AudioClip btnSound;

	protected AudioClip CoinSound;

	protected AudioClip BatHitSound;

	protected AudioClip BowledSound;

	protected AudioClip BoundarySound;

	protected AudioClip CheerSound;

	protected AudioClip BeatenSound;

	protected AudioClip CommentarySound;

	protected AudioClip SpinWheelSound;

	public float timeDiff;

	private bool playOnce;

	private void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
		CONTROLLER.sndController = this;
		BGMSource = base.gameObject.AddComponent<AudioSource>();
		BGMSource.playOnAwake = false;
		BGMSource.loop = true;
		CrowdSource = base.gameObject.AddComponent<AudioSource>();
		CrowdSource.playOnAwake = false;
		CrowdSource.loop = true;
		btnSource = base.gameObject.AddComponent<AudioSource>();
		btnSource.playOnAwake = false;
		CoinSource = base.gameObject.AddComponent<AudioSource>();
		CoinSource.playOnAwake = false;
		source = base.gameObject.AddComponent<AudioSource>();
		source.playOnAwake = false;
	}

	private void Start()
	{
		btnSound = Resources.Load("Sound/ButtonSound") as AudioClip;
		CoinSound = Resources.Load("Sound/CoinSound") as AudioClip;
		CrowdSound = Resources.Load("Sound/CrowdSound") as AudioClip;
		BGM = Resources.Load("Sound/music") as AudioClip;
		BatHitSound = Resources.Load("Sound/BatSound") as AudioClip;
		SpinWheelSound = Resources.Load("Sound/wheel") as AudioClip;
	}

	protected void OnLevelWasLoaded(int index)
	{
		playOnce = false;
		if ((index != 1 && index != 2) || playOnce)
		{
			return;
		}
		playOnce = true;
		if (CONTROLLER.bgMusicVal == 0)
		{
			if (Application.loadedLevelName == "MainMenu")
			{
				CrowdSource.Stop();
				CrowdSource.clip = null;
				BGMSource.Stop();
				BGMSource.clip = null;
			}
			else
			{
				BGMSource.Stop();
				BGMSource.clip = null;
			}
		}
		else if (Application.loadedLevelName == "MainMenu")
		{
			CrowdSource.Stop();
			CrowdSource.clip = null;
			BGMSource.clip = BGM;
			BGMSource.Play();
			BGMSource.volume = CONTROLLER.menuBgVolume;
		}
		else
		{
			BGMSource.Stop();
			BGMSource.clip = null;
		}
		if (Application.loadedLevelName == "Ground")
		{
			if (CONTROLLER.ambientVal == 0)
			{
				source.GetComponent<AudioSource>().Stop();
				source.clip = null;
				CrowdSource.Stop();
				CrowdSource.clip = null;
			}
			else if (CONTROLLER.ambientVal == 1)
			{
				CrowdSource.clip = CrowdSound;
				CrowdSource.Play();
				CrowdSource.volume = CONTROLLER.sfxVolume;
			}
		}
	}

	public void CallGarbageCollection()
	{
	}

	public void PlayButtonSnd()
	{
		btnSource.clip = btnSound;
		if (CONTROLLER.ambientVal == 0)
		{
			btnSource.Stop();
			CrowdSource.clip = null;
		}
		else
		{
			btnSource.Play();
			btnSource.volume = CONTROLLER.sfxVolume;
		}
	}

	public void PlayCoinSnd()
	{
		btnSource.clip = CoinSound;
		if (CONTROLLER.ambientVal == 0)
		{
			btnSource.Stop();
			CrowdSource.clip = null;
		}
		else
		{
			btnSource.Play();
			btnSource.volume = CONTROLLER.sfxVolume;
		}
	}

	public void updateBGMVolume()
	{
		BGMSource.volume = CONTROLLER.menuBgVolume;
	}

	public void updateSFXVolume()
	{
		source.volume = CONTROLLER.sfxVolume;
		CrowdSource.volume = CONTROLLER.sfxVolume;
	}

	public void PlayGameSnd(string SoundType)
	{
		if (CONTROLLER.ambientVal == 1)
		{
			float sfxVolume = CONTROLLER.sfxVolume;
			if (SoundType == "wheel")
			{
				SpinWheelSound = Resources.Load("Sound/wheel") as AudioClip;
				source.clip = SpinWheelSound;
				source.GetComponent<AudioSource>().PlayOneShot(SpinWheelSound, sfxVolume);
				BGMSource.volume = CONTROLLER.menuBgVolume;
			}
			if (SoundType == "Bat")
			{
				BatHitSound = Resources.Load("Sound/BatSound") as AudioClip;
				source.clip = BatHitSound;
				source.GetComponent<AudioSource>().PlayOneShot(BatHitSound, sfxVolume);
				BGMSource.volume = CONTROLLER.sfxVolume;
			}
			if (SoundType == "Bowled")
			{
				BowledSound = Resources.Load("Sound/BowledSnd") as AudioClip;
				source.clip = BowledSound;
				source.GetComponent<AudioSource>().PlayOneShot(BowledSound, sfxVolume);
			}
			if (SoundType == "Boundary")
			{
				BoundarySound = Resources.Load("Sound/BoundarySnd") as AudioClip;
				source.clip = BoundarySound;
				source.GetComponent<AudioSource>().PlayOneShot(BoundarySound, sfxVolume);
			}
			if (SoundType == "Cheer")
			{
				CheerSound = Resources.Load("Sound/Cheer") as AudioClip;
				source.clip = CheerSound;
				source.GetComponent<AudioSource>().PlayOneShot(CheerSound, sfxVolume);
			}
			if (SoundType == "Beaten")
			{
				BeatenSound = Resources.Load("Sound/BeatenSnd") as AudioClip;
				source.clip = BeatenSound;
				source.GetComponent<AudioSource>().PlayOneShot(BeatenSound, sfxVolume);
			}
			if (SoundType == "Won")
			{
				BowledSound = Resources.Load("Sound/WinningSound") as AudioClip;
				source.clip = BowledSound;
				source.GetComponent<AudioSource>().PlayOneShot(BowledSound, sfxVolume);
			}
			if (SoundType == "Lost")
			{
				BowledSound = Resources.Load("Sound/LosingSound") as AudioClip;
				source.clip = BowledSound;
				source.GetComponent<AudioSource>().PlayOneShot(BowledSound, sfxVolume);
			}
		}
	}

	public void PlayCommentarySnd(string SoundType)
	{
	}

	public void stopCommentary()
	{
	}

	public void bgMusicToggle()
	{
		if (CONTROLLER.bgMusicVal == 0)
		{
			BGMSource.Stop();
			BGMSource.clip = null;
		}
		else if (Application.loadedLevelName == "MainMenu" || Application.loadedLevelName == "Preloader")
		{
			if (BGMSource.clip == null)
			{
				BGMSource.clip = BGM;
				BGMSource.Play();
				BGMSource.volume = CONTROLLER.menuBgVolume;
			}
		}
		else if (CrowdSource.clip == null)
		{
			CrowdSource.clip = CrowdSound;
			CrowdSource.Play();
			CrowdSource.volume = CONTROLLER.sfxVolume;
		}
	}

	public void muteBGMVolume(float _vol)
	{
		BGMSource.volume = _vol;
	}

	public void ambientToggle()
	{
		if (CONTROLLER.ambientVal == 0)
		{
			if (Application.loadedLevelName == "Ground")
			{
				source.GetComponent<AudioSource>().Stop();
				source.clip = null;
				CrowdSource.Stop();
				CrowdSource.clip = null;
			}
		}
		else if (Application.loadedLevelName == "Ground")
		{
			CrowdSource.clip = CrowdSound;
			CrowdSource.Play();
			CrowdSource.volume = CONTROLLER.sfxVolume;
		}
	}

	public void RemoveGameSounds()
	{
		source.clip = null;
	}
}
