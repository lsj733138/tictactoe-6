using System;
using Newtonsoft.Json;
using SocketIOClient;

public class RoomData
{
    [JsonProperty("roomId")]
    public string RoomId { get; set; }
}

public class UserData
{
    [JsonProperty("userId")]
    public string UserId { get; set; }
}

public class MoveData
{
    [JsonProperty("position")]
    public int Position { get; set; }
}

public enum MultiplayManagerState
{
    CreateRoom,     // 방 생성
    JoinRoom,       // 방 참가
    StartGame,      // 두 유저가 방에 모두 들어와서 게임 시작할 때
    ExitRoom,       // 자신이 방 빠져나왔을 때
    EndGame,        // 상대방이 접속을 끊거나 방을 나갔을 때
}

public class MultiplayManager : IDisposable
{
    private SocketIOUnity _socket;
    private event Action<MultiplayManagerState, string> _onMultiplayStateChanged;
    public Action<MoveData> OnOpponentMove;
    
    public MultiplayManager(Action<MultiplayManagerState, string> onMultiplayStateChanged)
    {
        _onMultiplayStateChanged = onMultiplayStateChanged;

        var uri = new Uri(Constants.SocketURL);
        _socket = new SocketIOUnity(uri, new SocketIOOptions()
        {
            Transport = SocketIOClient.Transport.TransportProtocol.WebSocket
        });

        _socket.On("createRoom", CreateRoom);
        _socket.On("joinRoom", JoinRoom);
        _socket.On("startGame", StartGame);
        _socket.On("exitRoom", ExitRoom);
        _socket.On("endGame", EndGame);
        _socket.On("doOpponent", DoOpponent);
        
        // 서버에 접속
        _socket.Connect();
    }

    #region 
    
    /// <summary>
    /// 클라이언트가 서버에 접속했더니 아무도 없어서 방을 새롭게 만들었을 때 서버가 호출해주는 함수
    /// </summary>
    private void CreateRoom(SocketIOResponse response)
    {
        var data = response.GetValue<RoomData>();
        _onMultiplayStateChanged?.Invoke(MultiplayManagerState.CreateRoom, data.RoomId);
    }

    /// <summary>
    /// 클라아언트가 서버에 접속했더니 대기 중인 방이 있어서 그 방에 참가했을 때 서버가 호출해주는 함수
    /// </summary>
    private void JoinRoom(SocketIOResponse response)
    {
        var data = response.GetValue<RoomData>();
        _onMultiplayStateChanged?.Invoke(MultiplayManagerState.JoinRoom, data.RoomId);
    }

    /// <summary>
    ///방에 참가한 유저가 게임을 시작할 때 서버가 호출해주는 함수
    /// </summary>
    private void StartGame(SocketIOResponse response)
    {
        var data = response.GetValue<RoomData>();
        _onMultiplayStateChanged?.Invoke(MultiplayManagerState.StartGame, data.RoomId);
    }
    
    /// <summary>
    /// 방에 참가한 유저가 방을 나갔을 때 서버가 호출해주는 함수
    /// </summary>
    private void ExitRoom(SocketIOResponse response)
    {
        _onMultiplayStateChanged?.Invoke(MultiplayManagerState.ExitRoom, null);
    }

    /// <summary>
    /// 방에 참가한 유저가 접속을 끊었을 때 서버가 호출해주는 함수
    /// </summary>
    private void EndGame(SocketIOResponse response)
    {
        _onMultiplayStateChanged?.Invoke(MultiplayManagerState.EndGame, null);
    }

    /// <summary>
    /// 상대방이 게임에서 움직임을 보냈을 때 서버가 호출해주는 함수
    /// </summary>
    private void DoOpponent(SocketIOResponse response)
    {
        var data = response.GetValue<MoveData>();
        OnOpponentMove?.Invoke(data);
    }

    #endregion
    
    #region 서버로 이벤트를 보내는 함수

    /// <summary>
    /// 플레이어가 마커를 놓았을 때 서버로 이동 정보 전송
    /// </summary>
    public void SendPlayerMove(string roomId, int position)
    {
        _socket.Emit("doPlayer", new {roomId, position});
    }
    
    /// <summary>
    /// 클라이언트가 방을 나갈 때 호출하는 함수
    /// </summary>
    public void LeaveRoom(string roomId)
    {
        _socket.Emit("leaveRoom", new { roomId });
    }
    
    #endregion
    public void Dispose()
    {
        if (_socket != null)
        {
            _socket.Disconnect();
            _socket.Dispose();
        }
    }
}
