using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GooglePlayGames.BasicApi.Multiplayer;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.Native.PInvoke
{
	internal class NativeTurnBasedMatch : BaseReferenceHolder
	{
		internal NativeTurnBasedMatch(IntPtr selfPointer)
			: base(selfPointer)
		{
		}

		internal uint AvailableAutomatchSlots()
		{
			return GooglePlayGames.Native.Cwrapper.TurnBasedMatch.TurnBasedMatch_AutomatchingSlotsAvailable(SelfPtr());
		}

		internal ulong CreationTime()
		{
			return GooglePlayGames.Native.Cwrapper.TurnBasedMatch.TurnBasedMatch_CreationTime(SelfPtr());
		}

		internal IEnumerable<MultiplayerParticipant> Participants()
		{
			return PInvokeUtilities.ToEnumerable(GooglePlayGames.Native.Cwrapper.TurnBasedMatch.TurnBasedMatch_Participants_Length(SelfPtr()), (UIntPtr index) => new MultiplayerParticipant(GooglePlayGames.Native.Cwrapper.TurnBasedMatch.TurnBasedMatch_Participants_GetElement(SelfPtr(), index)));
		}

		internal uint Version()
		{
			return GooglePlayGames.Native.Cwrapper.TurnBasedMatch.TurnBasedMatch_Version(SelfPtr());
		}

		internal uint Variant()
		{
			return GooglePlayGames.Native.Cwrapper.TurnBasedMatch.TurnBasedMatch_Variant(SelfPtr());
		}

		internal ParticipantResults Results()
		{
			return new ParticipantResults(GooglePlayGames.Native.Cwrapper.TurnBasedMatch.TurnBasedMatch_ParticipantResults(SelfPtr()));
		}

		internal MultiplayerParticipant ParticipantWithId(string participantId)
		{
			foreach (MultiplayerParticipant item in Participants())
			{
				if (item.Id().Equals(participantId))
				{
					return item;
				}
				item.Dispose();
			}
			return null;
		}

		internal MultiplayerParticipant PendingParticipant()
		{
			MultiplayerParticipant multiplayerParticipant = new MultiplayerParticipant(GooglePlayGames.Native.Cwrapper.TurnBasedMatch.TurnBasedMatch_PendingParticipant(SelfPtr()));
			if (!multiplayerParticipant.Valid())
			{
				multiplayerParticipant.Dispose();
				return null;
			}
			return multiplayerParticipant;
		}

		internal Types.MatchStatus MatchStatus()
		{
			return GooglePlayGames.Native.Cwrapper.TurnBasedMatch.TurnBasedMatch_Status(SelfPtr());
		}

		internal string Description()
		{
			return PInvokeUtilities.OutParamsToString((byte[] out_string, UIntPtr size) => GooglePlayGames.Native.Cwrapper.TurnBasedMatch.TurnBasedMatch_Description(SelfPtr(), out_string, size));
		}

		internal bool HasRematchId()
		{
			string text = RematchId();
			return string.IsNullOrEmpty(text) || !text.Equals("(null)");
		}

		internal string RematchId()
		{
			return PInvokeUtilities.OutParamsToString((byte[] out_string, UIntPtr size) => GooglePlayGames.Native.Cwrapper.TurnBasedMatch.TurnBasedMatch_RematchId(SelfPtr(), out_string, size));
		}

		internal byte[] Data()
		{
			if (!GooglePlayGames.Native.Cwrapper.TurnBasedMatch.TurnBasedMatch_HasData(SelfPtr()))
			{
				Logger.d("Match has no data.");
				return null;
			}
			return PInvokeUtilities.OutParamsToArray((byte[] bytes, UIntPtr size) => GooglePlayGames.Native.Cwrapper.TurnBasedMatch.TurnBasedMatch_Data(SelfPtr(), bytes, size));
		}

		internal string Id()
		{
			return PInvokeUtilities.OutParamsToString((byte[] out_string, UIntPtr size) => GooglePlayGames.Native.Cwrapper.TurnBasedMatch.TurnBasedMatch_Id(SelfPtr(), out_string, size));
		}

		protected override void CallDispose(HandleRef selfPointer)
		{
			GooglePlayGames.Native.Cwrapper.TurnBasedMatch.TurnBasedMatch_Dispose(selfPointer);
		}

		internal GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch AsTurnBasedMatch(string selfPlayerId)
		{
			List<Participant> list = new List<Participant>();
			string selfParticipantId = null;
			string pendingParticipantId = null;
			using (MultiplayerParticipant multiplayerParticipant = PendingParticipant())
			{
				if (multiplayerParticipant != null)
				{
					pendingParticipantId = multiplayerParticipant.Id();
				}
			}
			foreach (MultiplayerParticipant item in Participants())
			{
				using (item)
				{
					using (NativePlayer nativePlayer = item.Player())
					{
						if (nativePlayer != null && nativePlayer.Id().Equals(selfPlayerId))
						{
							selfParticipantId = item.Id();
						}
					}
					list.Add(item.AsParticipant());
				}
			}
			bool canRematch = MatchStatus() == Types.MatchStatus.COMPLETED && !HasRematchId();
			return new GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch(Id(), Data(), canRematch, selfParticipantId, list, AvailableAutomatchSlots(), pendingParticipantId, ToTurnStatus(MatchStatus()), ToMatchStatus(pendingParticipantId, MatchStatus()), Variant(), Version());
		}

		private static GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch.MatchTurnStatus ToTurnStatus(Types.MatchStatus status)
		{
			return status switch
			{
				Types.MatchStatus.CANCELED => GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch.MatchTurnStatus.Complete, 
				Types.MatchStatus.COMPLETED => GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch.MatchTurnStatus.Complete, 
				Types.MatchStatus.EXPIRED => GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch.MatchTurnStatus.Complete, 
				Types.MatchStatus.INVITED => GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch.MatchTurnStatus.Invited, 
				Types.MatchStatus.MY_TURN => GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch.MatchTurnStatus.MyTurn, 
				Types.MatchStatus.PENDING_COMPLETION => GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch.MatchTurnStatus.Complete, 
				Types.MatchStatus.THEIR_TURN => GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch.MatchTurnStatus.TheirTurn, 
				_ => GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch.MatchTurnStatus.Unknown, 
			};
		}

		private static GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch.MatchStatus ToMatchStatus(string pendingParticipantId, Types.MatchStatus status)
		{
			return status switch
			{
				Types.MatchStatus.CANCELED => GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch.MatchStatus.Cancelled, 
				Types.MatchStatus.COMPLETED => GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch.MatchStatus.Complete, 
				Types.MatchStatus.EXPIRED => GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch.MatchStatus.Expired, 
				Types.MatchStatus.INVITED => GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch.MatchStatus.Active, 
				Types.MatchStatus.MY_TURN => GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch.MatchStatus.Active, 
				Types.MatchStatus.PENDING_COMPLETION => GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch.MatchStatus.Complete, 
				Types.MatchStatus.THEIR_TURN => (pendingParticipantId == null) ? GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch.MatchStatus.AutoMatching : GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch.MatchStatus.Active, 
				_ => GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch.MatchStatus.Unknown, 
			};
		}

		internal static NativeTurnBasedMatch FromPointer(IntPtr selfPointer)
		{
			if (PInvokeUtilities.IsNull(selfPointer))
			{
				return null;
			}
			return new NativeTurnBasedMatch(selfPointer);
		}
	}
}
