namespace BookStore.Models
{
    public enum OrderState
    {
        Sended = 0,
        Received = 1,
        ToSend = 2,
        SelfReceived = 3
    }
}