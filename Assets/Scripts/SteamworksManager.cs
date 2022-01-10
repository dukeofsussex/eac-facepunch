using System;
using System.Threading.Tasks;
using Steamworks;
using UnityEngine;

public class SteamworksManager : MonoBehaviour
{
    public static SteamworksManager Instance { get; private set; }

    public async void Awake()
    {
        Instance = this;

        try
        {
            SteamClient.Init(518150);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            return;
        }

        if (!SteamApps.IsSubscribed)
        {
            return;
        }

        print("Steam Id: " + SteamClient.SteamId);
        print("Steam Name: " + SteamClient.Name);
        print("Is Vac Banned: " + SteamApps.IsVACBanned);

        if (SteamApps.IsVACBanned)
        {
            return;
        }

        print("Is Logged On: " + SteamClient.IsLoggedOn);

        if (!SteamClient.IsLoggedOn)
        {
            return;
        }

        print("Is Valid: " + SteamClient.IsValid);

        if (!SteamClient.IsValid)
        {
            return;
        }

        SteamFriends.OnGameRichPresenceJoinRequested += OnGameRichPresenceJoinRequested;

        SteamFriends.SetRichPresence("PlayingIntruder", "Playing Intruder");

        string ticket = await AuthAsync();
        Debug.Log($"Ticket: {ticket}");

        SteamInventory.LoadItemDefinitions();
        SteamInventory.GetAllItems();
        SteamInventory.OnInventoryUpdated += OnInventoryUpdated;
    }

    public async Task<string> AuthAsync()
    {
        Debug.Log("Requesting auth ticket");
        AuthTicket ticket = await SteamUser.GetAuthSessionTicketAsync();
        Debug.Log("Received auth ticket");
        return BitConverter.ToString(ticket.Data).Replace("-", "");
    }

    private void OnInventoryUpdated(InventoryResult obj)
    {
        Debug.Log($"Inventory contains {obj.ItemCount} item(s)");
        SteamInventory.OnInventoryUpdated -= OnInventoryUpdated;
    }

    private void OnGameRichPresenceJoinRequested(Friend friend, string key)
    {
        Debug.Log("GameRichPresenceJoinRequested: " + friend.Name + ":" + key);
    }
}
