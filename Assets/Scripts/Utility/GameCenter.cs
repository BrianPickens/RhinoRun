using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEngine.SocialPlatforms;
using System;

public static class GameCenter
{

    public static bool initialLogInAttempted;

    public static bool loggedIn;

    private static string leaderboardId = "1b2r3i4a5n";

    private static Action<bool> OnAuthenticationComplete;

    public static void AuthenticateUser(Action<bool> _callback = null)
    {
        initialLogInAttempted = true;
        Debug.Log("Attempting Authentication");
        if (!loggedIn)
        {
            OnAuthenticationComplete = null;
            OnAuthenticationComplete += _callback;

            Social.localUser.Authenticate(AuthenticationComplete);
        }
        else
        {
            Debug.Log("already logged in, showing leader board");
            ShowLeaderBoard();
        }
    }

    private static void AuthenticationComplete(bool _success)
    {
        Debug.Log("game center success: " + _success);
        loggedIn = _success;
        if (OnAuthenticationComplete != null)
        {
            OnAuthenticationComplete(_success);
        }
    }

    public static void PostScoreToLeaderBoard(int _score)
    {
        Debug.Log("attempted post to leaderboard");
        if (loggedIn)
        {
            Debug.Log("Posting to leaderboard");
            Social.ReportScore(_score, leaderboardId, ScorePostComplete);
        }
    }

    private static void ScorePostComplete(bool _success)
    {
        Debug.Log("Score post: " + _success);
    }

    public static void ShowLeaderBoard()
    {
        if (loggedIn)
        {
            Debug.Log("Showing leaderboard");
            Social.ShowLeaderboardUI();
        }
        else
        {
            Debug.Log("not logged in, attempting to authenticate");
            AuthenticateUser();
        }
    }


}
