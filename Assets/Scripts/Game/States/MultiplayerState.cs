using UnityEngine;

public class MultiplayerState : BaseState
{
    private Constants.PlayerType _playerType;
    private MultiplayManager _multiplayManager;

    public MultiplayerState(bool isFirstPlayer, MultiplayManager multiplayManager)
    {
        _playerType = isFirstPlayer ? Constants.PlayerType.Player1 : Constants.PlayerType.Player2;
        _multiplayManager = multiplayManager;
    }
    
    public override void OnEnter(GameLogic gameLogic)
    {
        _multiplayManager.OnOpponentMove = moveData =>
        {
            if (moveData.Position >= 0 && moveData.Position < Constants.BOARD_SIZE * Constants.BOARD_SIZE)
            {
                UnityThread.executeInUpdate(() =>
                {
                    // 블록이 클릭되었을 때 처리될 로
                    HandleMove(gameLogic, moveData.Position);
                    // OX UI 업데이트
                    GameManager.Instance.SetGameTurn(_playerType);
                });
            }
            else
            {
                // TODO : 유효하지 않은 이동 처리`
            }
        };
    }

    public override void HandleMove(GameLogic gameLogic, int index)
    {
        ProcessMove(gameLogic, index, _playerType);
    }

    public override void OnExit(GameLogic gameLogic)
    {
        _multiplayManager.OnOpponentMove = null;
    }

    public override void HandleNextTurn(GameLogic gameLogic)
    {
        gameLogic.ChangeGameState();
    }
}
