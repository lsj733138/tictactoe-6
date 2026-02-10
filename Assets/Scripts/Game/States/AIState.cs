using Unity.VisualScripting;
using UnityEngine;

public class AIState : BaseState
{
    private Constants.PlayerType _playerType;

    public AIState(bool isFirstPlayer)
    {
        _playerType = isFirstPlayer ? Constants.PlayerType.Player1 : Constants.PlayerType.Player2;
    }
    
    public override void OnEnter(GameLogic gameLogic)
    {
        // OX UI 업데이트
        GameManager.Instance.SetGameTurn(_playerType);

        var board = gameLogic.Board;
        var result = TicTacToeAI.GetBestMove(board);

        if (result.HasValue)
        {
            int col = result.Value.col;
            int row = result.Value.row;
            int index = row * Constants.BOARD_SIZE + col;

            HandleMove(gameLogic, index);
        }
    }

    public override void HandleMove(GameLogic gameLogic, int index)
    {
        ProcessMove(gameLogic, index, _playerType);
    }

    public override void OnExit(GameLogic gameLogic)
    {
    }

    public override void HandleNextTurn(GameLogic gameLogic)
    {
        gameLogic.ChangeGameState();
    }
    
}
