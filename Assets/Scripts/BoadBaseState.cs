namespace TicTacToe
{
    public abstract class BoardBaseState
    {
        public abstract void EnterState(BoardStateManager board);

        public abstract void UpdateState(BoardStateManager board);
    }
}