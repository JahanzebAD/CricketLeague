using System;
using GooglePlayGames.BasicApi;
using GooglePlayGames.Native.Cwrapper;
using UnityEngine;

namespace GooglePlayGames.Native
{
	internal static class ConversionUtils
	{
		internal static ResponseStatus ConvertResponseStatus(CommonErrorStatus.ResponseStatus status)
		{
			return status switch
			{
				CommonErrorStatus.ResponseStatus.VALID => ResponseStatus.Success, 
				CommonErrorStatus.ResponseStatus.VALID_BUT_STALE => ResponseStatus.SuccessWithStale, 
				CommonErrorStatus.ResponseStatus.ERROR_INTERNAL => ResponseStatus.InternalError, 
				CommonErrorStatus.ResponseStatus.ERROR_LICENSE_CHECK_FAILED => ResponseStatus.LicenseCheckFailed, 
				CommonErrorStatus.ResponseStatus.ERROR_NOT_AUTHORIZED => ResponseStatus.NotAuthorized, 
				CommonErrorStatus.ResponseStatus.ERROR_TIMEOUT => ResponseStatus.Timeout, 
				CommonErrorStatus.ResponseStatus.ERROR_VERSION_UPDATE_REQUIRED => ResponseStatus.VersionUpdateRequired, 
				_ => throw new InvalidOperationException("Unknown status: " + status), 
			};
		}

		internal static CommonStatusCodes ConvertResponseStatusToCommonStatus(CommonErrorStatus.ResponseStatus status)
		{
			switch (status)
			{
			case CommonErrorStatus.ResponseStatus.VALID:
				return CommonStatusCodes.Success;
			case CommonErrorStatus.ResponseStatus.VALID_BUT_STALE:
				return CommonStatusCodes.SuccessCached;
			case CommonErrorStatus.ResponseStatus.ERROR_INTERNAL:
				return CommonStatusCodes.InternalError;
			case CommonErrorStatus.ResponseStatus.ERROR_LICENSE_CHECK_FAILED:
				return CommonStatusCodes.LicenseCheckFailed;
			case CommonErrorStatus.ResponseStatus.ERROR_NOT_AUTHORIZED:
				return CommonStatusCodes.AuthApiAccessForbidden;
			case CommonErrorStatus.ResponseStatus.ERROR_TIMEOUT:
				return CommonStatusCodes.Timeout;
			case CommonErrorStatus.ResponseStatus.ERROR_VERSION_UPDATE_REQUIRED:
				return CommonStatusCodes.ServiceVersionUpdateRequired;
			default:
				Debug.LogWarning(string.Concat("Unknown ResponseStatus: ", status, ", defaulting to CommonStatusCodes.Error"));
				return CommonStatusCodes.Error;
			}
		}

		internal static UIStatus ConvertUIStatus(CommonErrorStatus.UIStatus status)
		{
			return status switch
			{
				CommonErrorStatus.UIStatus.VALID => UIStatus.Valid, 
				CommonErrorStatus.UIStatus.ERROR_INTERNAL => UIStatus.InternalError, 
				CommonErrorStatus.UIStatus.ERROR_NOT_AUTHORIZED => UIStatus.NotAuthorized, 
				CommonErrorStatus.UIStatus.ERROR_TIMEOUT => UIStatus.Timeout, 
				CommonErrorStatus.UIStatus.ERROR_VERSION_UPDATE_REQUIRED => UIStatus.VersionUpdateRequired, 
				CommonErrorStatus.UIStatus.ERROR_CANCELED => UIStatus.UserClosedUI, 
				CommonErrorStatus.UIStatus.ERROR_UI_BUSY => UIStatus.UiBusy, 
				_ => throw new InvalidOperationException("Unknown status: " + status), 
			};
		}

		internal static GooglePlayGames.Native.Cwrapper.Types.DataSource AsDataSource(DataSource source)
		{
			return source switch
			{
				DataSource.ReadCacheOrNetwork => GooglePlayGames.Native.Cwrapper.Types.DataSource.CACHE_OR_NETWORK, 
				DataSource.ReadNetworkOnly => GooglePlayGames.Native.Cwrapper.Types.DataSource.NETWORK_ONLY, 
				_ => throw new InvalidOperationException("Found unhandled DataSource: " + source), 
			};
		}

		internal static GooglePlayGames.Native.Cwrapper.Types.VideoCaptureMode ConvertVideoCaptureMode(VideoCaptureMode captureMode)
		{
			switch (captureMode)
			{
			case VideoCaptureMode.File:
				return GooglePlayGames.Native.Cwrapper.Types.VideoCaptureMode.FILE;
			case VideoCaptureMode.Stream:
				return GooglePlayGames.Native.Cwrapper.Types.VideoCaptureMode.STREAM;
			case VideoCaptureMode.Unknown:
				return GooglePlayGames.Native.Cwrapper.Types.VideoCaptureMode.UNKNOWN;
			default:
				Debug.LogWarning(string.Concat("Unknown VideoCaptureMode: ", captureMode, ", defaulting to Types.VideoCaptureMode.UNKNOWN."));
				return GooglePlayGames.Native.Cwrapper.Types.VideoCaptureMode.UNKNOWN;
			}
		}

		internal static VideoCaptureMode ConvertNativeVideoCaptureMode(GooglePlayGames.Native.Cwrapper.Types.VideoCaptureMode nativeCaptureMode)
		{
			switch (nativeCaptureMode)
			{
			case GooglePlayGames.Native.Cwrapper.Types.VideoCaptureMode.FILE:
				return VideoCaptureMode.File;
			case GooglePlayGames.Native.Cwrapper.Types.VideoCaptureMode.STREAM:
				return VideoCaptureMode.Stream;
			case GooglePlayGames.Native.Cwrapper.Types.VideoCaptureMode.UNKNOWN:
				return VideoCaptureMode.Unknown;
			default:
				Debug.LogWarning(string.Concat("Unknown Types.VideoCaptureMode: ", nativeCaptureMode, ", defaulting to VideoCaptureMode.Unknown."));
				return VideoCaptureMode.Unknown;
			}
		}

		internal static VideoQualityLevel ConvertNativeVideoQualityLevel(GooglePlayGames.Native.Cwrapper.Types.VideoQualityLevel nativeQualityLevel)
		{
			switch (nativeQualityLevel)
			{
			case GooglePlayGames.Native.Cwrapper.Types.VideoQualityLevel.SD:
				return VideoQualityLevel.SD;
			case GooglePlayGames.Native.Cwrapper.Types.VideoQualityLevel.HD:
				return VideoQualityLevel.HD;
			case GooglePlayGames.Native.Cwrapper.Types.VideoQualityLevel.XHD:
				return VideoQualityLevel.XHD;
			case GooglePlayGames.Native.Cwrapper.Types.VideoQualityLevel.FULLHD:
				return VideoQualityLevel.FullHD;
			case GooglePlayGames.Native.Cwrapper.Types.VideoQualityLevel.UNKNOWN:
				return VideoQualityLevel.Unknown;
			default:
				Debug.LogWarning(string.Concat("Unknown Types.VideoQualityLevel: ", nativeQualityLevel, ", defaulting to VideoQualityLevel.Unknown."));
				return VideoQualityLevel.Unknown;
			}
		}

		internal static VideoCaptureOverlayState ConvertNativeVideoCaptureOverlayState(GooglePlayGames.Native.Cwrapper.Types.VideoCaptureOverlayState nativeOverlayState)
		{
			switch (nativeOverlayState)
			{
			case GooglePlayGames.Native.Cwrapper.Types.VideoCaptureOverlayState.DISMISSED:
				return VideoCaptureOverlayState.Dismissed;
			case GooglePlayGames.Native.Cwrapper.Types.VideoCaptureOverlayState.SHOWN:
				return VideoCaptureOverlayState.Shown;
			case GooglePlayGames.Native.Cwrapper.Types.VideoCaptureOverlayState.STARTED:
				return VideoCaptureOverlayState.Started;
			case GooglePlayGames.Native.Cwrapper.Types.VideoCaptureOverlayState.STOPPED:
				return VideoCaptureOverlayState.Stopped;
			case GooglePlayGames.Native.Cwrapper.Types.VideoCaptureOverlayState.UNKNOWN:
				return VideoCaptureOverlayState.Unknown;
			default:
				Debug.LogWarning(string.Concat("Unknown Types.VideoCaptureOverlayState: ", nativeOverlayState, ", defaulting to VideoCaptureOverlayState.Unknown."));
				return VideoCaptureOverlayState.Unknown;
			}
		}
	}
}
